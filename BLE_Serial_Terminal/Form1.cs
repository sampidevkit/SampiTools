using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;        // AsBuffer
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Storage.Streams;

namespace BLE_Serial_Terminal
{
    public partial class Form1 : Form
    {
        private BluetoothLEAdvertisementWatcher watcher;
        private bool deviceConnected = false;
        private bool isReconnecting = false;
        private readonly object reconnectLock = new object();
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            ScanBle(); // start scanning on launch
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            // set window title with version
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = "BLE Serial Terminal v" + version.Major + "." + version.Minor + "." + version.Build;
            // Restore settings

            // Line endings for sending
            this.cmbBoxLBSend.Items.Clear();
            this.cmbBoxLBSend.Items.Add("CR");
            this.cmbBoxLBSend.Items.Add("LF");
            this.cmbBoxLBSend.Items.Add("CR+LF");
            this.cmbBoxLBSend.Items.Add("NONE");
            this.cmbBoxLBSend.SelectedIndex = Math.Max(0, Math.Min(Properties.Settings.Default.linebreaks, this.cmbBoxLBSend.Items.Count - 1));

            // Local echo
            this.cBoxLocalEcho.Checked = Properties.Settings.Default.localecho;

            // Timestamp
            this.cBoxTimeStamp.Checked = Properties.Settings.Default.timestamp;

            // Device combo
            this.cmbBoxDevice.Items.Clear();
            this.cmbBoxDevice.SelectedIndex = -1;
            NumItems = 0;

            // input textbox / send button
            this.textToBeSent.Enabled = false;
            this.btnSend.Enabled = false;

            // custom buttons
            generateCustumButton();
        }

        private async void ScanBle()
        {
            try
            {
                watcher = new BluetoothLEAdvertisementWatcher();
                watcher.Received += Watcher_Received;
                watcher.ScanningMode = BluetoothLEScanningMode.Active;
                watcher.Start();
                this.btnScan.Enabled = false;

                // scan for 5 seconds (non-blocking)
                await Task.Delay(5000);

                watcher.Stop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ScanBle error: " + ex);
            }
            finally
            {
                if (!this.deviceConnected) this.btnScan.Enabled = true;
            }
        }

        private int NumItems;
        public void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            try
            {
                string adr = args.BluetoothAddress.ToString("X12");
                if (adr.Length != 12) return;

                string devaddress = adr.Substring(0, 2) + ":" + adr.Substring(2, 2) + ":" + adr.Substring(4, 2) + ":" +
                                    adr.Substring(6, 2) + ":" + adr.Substring(8, 2) + ":" + adr.Substring(10, 2);

                string devname = args.Advertisement?.LocalName;
                if (string.IsNullOrEmpty(devname)) return;

                string tempItem = devname + " :  " + devaddress;

                this.Invoke(new MethodInvoker(delegate
                {
                    // avoid duplicates
                    foreach (string item in this.cmbBoxDevice.Items)
                    {
                        if (item.Contains(devaddress) && item.Contains(devname)) return;
                    }

                    cmbBoxDevice.Items.Add(tempItem);
                    if (++NumItems == 1) this.cmbBoxDevice.SelectedIndex = 0;
                }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Watcher_Received error: " + ex);
            }
        }

        private BluetoothLEDevice device;

        // RX/TX from device perspective (this app is GATT client)
        private GattCharacteristic cTX;
        private GattCharacteristic cRX;

        private UInt64 address;

        async Task connectSelectedDevice()
        {
            // connect and subscribe to notifications
            try
            {
                // dispose previous device if any
                try
                {
                    if (device != null)
                    {
                        try { if (cTX != null) cTX.ValueChanged -= characteristicBleDevice; } catch { }
                        try { device.ConnectionStatusChanged -= Device_ConnectionStatusChanged; } catch { }
                        try { device.Dispose(); } catch { }
                        device = null;
                        cTX = null;
                        cRX = null;
                        deviceConnected = false;
                    }
                }
                catch { /* ignore */ }

                device = await BluetoothLEDevice.FromBluetoothAddressAsync(address);
                if (device == null)
                {
                    Debug.WriteLine("Failed to get device for address.");
                    return;
                }

                // subscribe to connection status changes so we can detect disconnects
                try
                {
                    device.ConnectionStatusChanged += Device_ConnectionStatusChanged;
                }
                catch { /* non-fatal */ }

                var servicesResult = await device.GetGattServicesForUuidAsync(new Guid("6E400001-B5A3-F393-E0A9-E50E24DCCA9E"));
                if (servicesResult.Status != GattCommunicationStatus.Success || servicesResult.Services.Count == 0)
                {
                    Debug.WriteLine("Service not found or error: " + servicesResult.Status);
                    return;
                }

                var service = servicesResult.Services[0];

                var txResult = await service.GetCharacteristicsForUuidAsync(new Guid("6E400003-B5A3-F393-E0A9-E50E24DCCA9E"));
                if (txResult.Status != GattCommunicationStatus.Success || txResult.Characteristics.Count == 0)
                {
                    Debug.WriteLine("TX characteristic not found or error: " + txResult.Status);
                    return;
                }
                cTX = txResult.Characteristics[0];

                var rxResult = await service.GetCharacteristicsForUuidAsync(new Guid("6E400002-B5A3-F393-E0A9-E50E24DCCA9E"));
                if (rxResult.Status != GattCommunicationStatus.Success || rxResult.Characteristics.Count == 0)
                {
                    Debug.WriteLine("RX characteristic not found or error: " + rxResult.Status);
                    return;
                }
                cRX = rxResult.Characteristics[0];

                // subscribe to notifications (TX characteristic notifies us)
                cTX.ValueChanged += characteristicBleDevice;

                var status = await cTX.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
                if (status == GattCommunicationStatus.Success)
                {
                    this.deviceConnected = true;
                    Debug.WriteLine("Subscribed to notifications.");
                    // stop any reconnect loop if running
                    lock (reconnectLock) { isReconnecting = false; }
                }
                else
                {
                    Debug.WriteLine("Failed to subscribe: " + status);
                    // cleanup subscription handler in case of failure
                    try { cTX.ValueChanged -= characteristicBleDevice; } catch { }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("connectSelectedDevice error: " + ex);
            }
        }

        private void disconnectDevice()
        {
            try
            {
                // stop any reconnect attempts
                lock (reconnectLock) { isReconnecting = false; }

                if (cTX != null)
                {
                    try { cTX.ValueChanged -= characteristicBleDevice; } catch { }
                }

                try
                {
                    if (device != null)
                    {
                        try { device.ConnectionStatusChanged -= Device_ConnectionStatusChanged; } catch { }
                    }
                }
                catch { }

                try
                {
                    // best-effort dispose services & device
                    try { cTX?.Service?.Dispose(); } catch { }
                }
                catch { }
                try
                {
                    try { cRX?.Service?.Dispose(); } catch { }
                }
                catch { }
                try
                {
                    cTX = null;
                    cRX = null;
                    try { device?.Dispose(); } catch { }
                    device = null;
                }
                catch { }

                this.deviceConnected = false;
                this.btnScan.Enabled = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("disconnectDevice error: " + ex);
            }
        }
        // Add these methods to implement connection-status handling and auto-reconnect

        private void Device_ConnectionStatusChanged(BluetoothLEDevice sender, object args)
        {
            try
            {
                if (sender.ConnectionStatus == BluetoothConnectionStatus.Disconnected)
                {
                    // Notify UI and mark disconnected
                    this.Invoke(new MethodInvoker(delegate
                    {
                        addtextbox(">device disconnected\r\n");
                        this.deviceConnected = false;
                        this.btnConnect.Text = "Connect";
                        this.textToBeSent.Enabled = false;
                        this.btnSend.Enabled = false;
                        this.btnScan.Enabled = true;
                    }));

                    // Start auto-reconnect attempts
                    StartAutoReconnectLoop();
                }
                else if (sender.ConnectionStatus == BluetoothConnectionStatus.Connected)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        addtextbox(">device connected\r\n");
                        this.deviceConnected = true;
                        this.btnConnect.Text = "Disconnect";
                        this.btnScan.Enabled = false;
                        this.textToBeSent.Enabled = true;
                        this.btnSend.Enabled = true;
                    }));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Device_ConnectionStatusChanged error: " + ex);
            }
        }

        private void StartAutoReconnectLoop()
        {
            lock (reconnectLock)
            {
                if (isReconnecting) return;
                isReconnecting = true;
            }

            Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        lock (reconnectLock)
                        {
                            if (!isReconnecting) break;
                        }

                        if (deviceConnected) break;

                        // show attempt message on UI and mark Connect button as "Disconnect"
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                addtextbox(">attempting to reconnect...\r\n");
                                this.btnConnect.Text = "Disconnect";
                                this.btnConnect.Enabled = true;
                            }));
                        }
                        catch { }

                        // try to connect (connectSelectedDevice uses the existing 'address')
                        try
                        {
                            await connectSelectedDevice();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Reconnect attempt error: " + ex);
                        }

                        // stop loop if connected or if user cancelled
                        lock (reconnectLock)
                        {
                            if (!isReconnecting || deviceConnected) break;
                        }

                        // wait before next attempt
                        await Task.Delay(3000);
                    }
                }
                finally
                {
                    lock (reconnectLock) { isReconnecting = false; }
                }
            });
        }
        // Notification handler (safe read via DataReader)
        async void characteristicBleDevice(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            try
            {
                var buffer = args.CharacteristicValue;
                if (buffer == null) return;

                using (var reader = DataReader.FromBuffer(buffer))
                {
                    var length = reader.UnconsumedBufferLength;
                    byte[] bytes = new byte[length];
                    if (length > 0)
                    {
                        reader.ReadBytes(bytes);
                    }

                    // decode payload (use UTF8; change to ASCII if your device uses ASCII)
                    string payload = Encoding.UTF8.GetString(bytes);

                    this.Invoke(new MethodInvoker(delegate
                    {
                        addtextbox(payload);
                    }));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("characteristicBleDevice error: " + ex);
            }
        }

        private void btnScan_clicked(object sender, EventArgs e)
        {
            ScanBle();
        }

        private async void btnConnect_clicked(object sender, EventArgs e)
        {
            if (this.btnConnect.Text == "Connect")
            {
                if (cmbBoxDevice.SelectedItem == null)
                {
                    MessageBox.Show("Select a device first.");
                    return;
                }

                this.btnConnect.Enabled = false;

                string itemstr = (string)cmbBoxDevice.SelectedItem;
                if (string.IsNullOrWhiteSpace(itemstr) || itemstr.Length < 17)
                {
                    MessageBox.Show("Invalid device selection.");
                    this.btnConnect.Enabled = true;
                    return;
                }

                string adr1 = itemstr.Substring(itemstr.Length - 17);
                string adr2 = adr1.Replace(":", "");
                if (!UInt64.TryParse(adr2, System.Globalization.NumberStyles.HexNumber, null, out address))
                {
                    MessageBox.Show("Invalid device address.");
                    this.btnConnect.Enabled = true;
                    return;
                }

                string devicename = itemstr;
                int index = itemstr.IndexOf(" :  ");
                if (index >= 0) devicename = itemstr.Substring(0, index);

                addtextbox(">connecting " + devicename + " ..\r\n");

                await connectSelectedDevice();

                if (this.deviceConnected)
                {
                    this.btnConnect.Text = "Disconnect";
                    addtextbox(">connected\r\n");
                    this.btnScan.Enabled = false;
                    this.textToBeSent.Enabled = true;
                    this.btnSend.Enabled = true;
                }
                else
                {
                    addtextbox(">** not connected **\r\n");
                }

                this.btnConnect.Enabled = true;
            }
            else // Disconnect
            {
                this.btnConnect.Text = "Connect";
                disconnectDevice();
                addtextbox(">disconnected\r\n");
                this.textToBeSent.Enabled = false;
                this.btnSend.Enabled = false;
            }
        }

        private void btnSend_click(object sender, EventArgs e)
        {
            sendCustumbuttonstr(this.textToBeSent.Text);
        }

        private async void sendCustumbuttonstr(string str)
        {
            string[] linebreaks = { "\r", "\n", "\r\n", "" };
            if (string.IsNullOrEmpty(str)) return;

            if (this.cBoxLocalEcho.Checked)
            {
                addtextbox("$$" + str + "\r\n");
            }

            int idx = this.cmbBoxLBSend.SelectedIndex;
            if (idx < 0 || idx >= linebreaks.Length) idx = 0;
            str += linebreaks[idx];

            byte[] byte_str = System.Text.Encoding.ASCII.GetBytes(str);
            try
            {
                if (cRX == null)
                {
                    Debug.WriteLine("No RX characteristic available.");
                    return;
                }

                var writeBuffer = byte_str.AsBuffer();
                var status = await cRX.WriteValueAsync(writeBuffer);
                if (status != GattCommunicationStatus.Success)
                {
                    Debug.WriteLine("Write failed: " + status);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("sendCustumbuttonstr error: " + ex);
            }

            if (this.cBoxClearSending.Checked) this.textToBeSent.Clear();
        }

        void addtextbox(string text1)
        {
            if (this.cBoxTimeStamp.Checked)
            {
                DateTime dt = DateTime.Now;
                text1 = dt.ToString("HH:mm:ss") + ": " + text1;
            }
            this.textBoxReceived.AppendText(text1);
        }

        private void clearLog_clicked(object sender, EventArgs e)
        {
            this.textBoxReceived.Clear();
        }

        private String Getnowstringforfile()
        {
            DateTime dt = DateTime.Now;
            String now = dt.ToString("yyyyMMdd_HHmmss");
            return now;
        }

        private void btnSave_clicked(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                FileName = "BLESerialTerminal" + Getnowstringforfile(),
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, this.textBoxReceived.Text);
            }
        }

        // ApplicationExit handler
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.linebreaks = this.cmbBoxLBSend.SelectedIndex;
                Properties.Settings.Default.localecho = this.cBoxLocalEcho.Checked;
                Properties.Settings.Default.timestamp = this.cBoxTimeStamp.Checked;

                // Save custom buttons safely using join and ensure arrays are correct length
                for (int i = 0; i < 10; i++)
                {
                    string cmd = (i < custumbuttons.Length && custumbuttons[i] != null) ? custumbuttons[i].Text : "";
                    Properties.Settings.Default.commandstring = (i == 0) ? cmd : Properties.Settings.Default.commandstring + '\t' + cmd;
                }

                // stringtobesent and justinsert arrays may vary in length; ensure we store 10 values
                for (int i = 0; i < 10; i++)
                {
                    string s = (i < stringtobesent.Length) ? stringtobesent[i] ?? "" : "";
                    Properties.Settings.Default.stringforsend = (i == 0) ? s : Properties.Settings.Default.stringforsend + '\t' + s;
                }
                Properties.Settings.Default.stringforsend += "\tdummy";

                for (int i = 0; i < 10; i++)
                {
                    string s = (i < justinsert.Length) ? justinsert[i] ?? "yes" : "yes";
                    Properties.Settings.Default.justinsert = (i == 0) ? s : Properties.Settings.Default.justinsert + '\t' + s;
                }

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Application_ApplicationExit error: " + ex);
            }
            finally
            {
                Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);
                disconnectDevice();
            }
        }

        private Button[] custumbuttons;
        private string[] stringtobesent;
        private string[] justinsert;
        private const String Notsetyet = "undefined";
        private const String Notsetyet_old = "Not set yet";

        private void generateCustumButton()
        {
            this.custumbuttons = new Button[10];
            string[] buttontext = new string[10];
            this.stringtobesent = new string[10 + 1]; // includes dummy
            this.justinsert = new string[10];

            // restore settings if present (defensive parsing)
            if (!string.IsNullOrEmpty(Properties.Settings.Default.commandstring) && Properties.Settings.Default.commandstring != "none")
            {
                var parts = Properties.Settings.Default.commandstring.Split('\t');
                for (int i = 0; i < Math.Min(parts.Length, 10); i++) buttontext[i] = parts[i];
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.stringforsend) && Properties.Settings.Default.stringforsend != "none")
            {
                var parts = Properties.Settings.Default.stringforsend.Split('\t');
                for (int i = 0; i < Math.Min(parts.Length, this.stringtobesent.Length); i++) this.stringtobesent[i] = parts[i];
            }
            else
            {
                for (int i0 = 0; i0 < custumbuttons.Length; i0++) this.stringtobesent[i0] = "";
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.justinsert) && Properties.Settings.Default.justinsert != "none")
            {
                var parts = Properties.Settings.Default.justinsert.Split('\t');
                for (int i = 0; i < Math.Min(parts.Length, 10); i++) this.justinsert[i] = parts[i];
            }
            else
            {
                for (int i0 = 0; i0 < custumbuttons.Length; i0++) this.justinsert[i0] = "yes";
            }

            for (int i0 = 0; i0 < custumbuttons.Length; i0++)
            {
                int i = i0 % 10;
                int j = i0 / 10;

                this.custumbuttons[i0] = new Button
                {
                    Name = "custumbtn" + (i0 + 1).ToString(),
                    Text = string.IsNullOrEmpty(buttontext[i0]) ? Notsetyet : buttontext[i0],
                    Top = this.textToBeSent.Bottom + 20 + j * 45,
                    Height = 20,
                    Width = 62,
                    Left = this.textToBeSent.Left + 67 * i + (i / 5) * 13 - 3,
                    Tag = i0
                };

                this.Controls.Add(this.custumbuttons[i0]);
                this.custumbuttons[i0].Click += new System.EventHandler(custumbtnclick);
                this.custumbuttons[i0].MouseDown += new MouseEventHandler(Buttons_MouseDown);
            }
        }

        private void custumbtnclick(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            int no = (int)(btn.Tag);
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift || rightbutton)
            {
                List<object> sendList = new List<object>
                {
                    this.custumbuttons[no].Text,
                    (no < stringtobesent.Length) ? this.stringtobesent[no] : "",
                    (no < justinsert.Length) ? this.justinsert[no] : "yes"
                };
                List<object> resultObjs = Form2.ShowForm2(sendList);
                if ((string)resultObjs[0] != "")
                {
                    this.custumbuttons[no].Text = (string)resultObjs[0];
                }
                else
                {
                    this.custumbuttons[no].Text = Notsetyet;
                }
                this.stringtobesent[no] = (string)resultObjs[1];
                this.justinsert[no] = (string)resultObjs[2];
            }
            else if (btn.Text != Notsetyet && btn.Text != Notsetyet_old && !rightbutton)
            {
                if (this.justinsert[no] == "yes")
                {
                    textToBeSent.Text = this.stringtobesent[no];
                }
                else
                {
                    sendCustumbuttonstr(this.stringtobesent[no]);
                }
            }
            rightbutton = false;
        }

        private bool rightbutton;
        private void Buttons_MouseDown(object sender, MouseEventArgs e)
        {
            rightbutton = false;
            if (e.Button == MouseButtons.Right)
            {
                rightbutton = true;
                custumbtnclick(sender, (EventArgs)e);
            }
        }

        private void exportSettinfs(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                FileName = "BLESerialTerminal_buttons",
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StringBuilder mystring = new StringBuilder();
                for (int i = 0; i < custumbuttons.Length; i++)
                {
                    string text = (custumbuttons[i] != null) ? custumbuttons[i].Text : "";
                    string ssend = (i < stringtobesent.Length) ? stringtobesent[i] : "";
                    string js = (i < justinsert.Length) ? justinsert[i] : "";
                    mystring.AppendLine($"{i},{text},{ssend},{js}");
                }
                File.WriteAllText(saveFileDialog1.FileName, mystring.ToString());
            }
        }

        private void importSettings(object sender, EventArgs e)
        {
            OpenFileDialog loadFileDialog1 = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                FileName = "SerialTerminalPlus_buttons",
                RestoreDirectory = true
            };

            if (loadFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] txtArray = File.ReadAllLines(loadFileDialog1.FileName);
                foreach (var line in txtArray)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var textb = line.Split(',');
                    if (textb.Length < 4) continue;
                    if (!int.TryParse(textb[0], out int index)) continue;
                    if (index < 0 || index >= custumbuttons.Length) continue;

                    this.custumbuttons[index].Text = textb[1];
                    this.stringtobesent[index] = textb[2];
                    this.justinsert[index] = textb[3];
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var url = "https://github.com/healthywalk/BLE-Serial-Terminal";
                var psi = new ProcessStartInfo(url) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Open link error: " + ex);
            }
        }
    }
}