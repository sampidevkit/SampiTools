using System;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Management;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Text;

namespace Form1
{
    public partial class FormSPT : Form
    {
        #region Variables
        // File IO
        private struct FileInfo
        {
            public long TotalLength;
            public long NumOfLine;
        }

        private FileInfo HexFileDetail;
        private string HexFilePath = null;
        private FileInfo ScriptFileDetail;
        private string ScriptFilePath = null;
        private int scriptLineIdx = 0;
        private FileStream DfuStream = null;
        private StreamReader DfuReader = null;
        private char[] ScriptData = null;
        // Buffer
        //private RingBuffer RingBuffer2 = new RingBuffer();
        //private RingBuffer.RingTxBuf Port2TxBuf;
        //private RingBuffer.RingRxBuf Port2RxBuf;
        // Threads
        private static Thread Thread_Task;
        private static string Pid = "PID_000A";
        private volatile bool DebugWait = false;
        private volatile bool InternalPort = true;
        private volatile bool AppBusy = false;
        // Common
        UTF8Encoding ENC = new System.Text.UTF8Encoding();
        #endregion

        #region User's Functions

        private void ClearLog()
        {
            string logpath = "Log " + DateTime.Now.ToLongDateString() + ".txt";
            logpath.Replace('/', '_');
            logpath.Replace('\\', '_');
            logpath.Replace(':', '_');
            StreamWriter sw = File.CreateText(logpath);
            sw.WriteLine(rtb_Log.Text);
            sw.Close();
            rtb_Log.Clear();
        }

        private bool Is_ASCII_Printable_Character(byte b, ref long Line)
        {
            if ((b >= 0x20) && (b <= 0x7F))
                return true;
            else if (b == '\r')
                return true;
            else if (b == '\n')
            {
                Line++;
                return true;
            }
            else if (b == 0x09)// tab value
                return true;
            else
            {
                Line = 0;
                return false;
            }
        }

        private bool CheckFileFormat(string HexFilePath, out FileInfo fileInfo)
        {
            long i;
            FileStream fileStream = File.OpenRead(HexFilePath);
            BinaryReader reader = new BinaryReader(fileStream);

            fileInfo.NumOfLine = 1;
            fileInfo.TotalLength = fileStream.Length;

            for (i = 0; i < fileInfo.TotalLength; i++)
            {
                if (!Is_ASCII_Printable_Character(reader.ReadByte(), ref fileInfo.NumOfLine)) 
                {
                    fileInfo.NumOfLine = 0;
                    fileInfo.TotalLength = 0;
                    break;
                }
            }

            fileStream.Close();
            reader.Close();

            return (i == fileInfo.TotalLength ? true : false);
        }

        private char[] MakeFrame(char[] pChr, int len)
        {
            int i = 0, j = 0;
            bool lat = false;
            char[] Chr = new char[len];

            for (i = 0, j = 0; i < len; i++)
            {
                switch(pChr[i])
                {
                    case '\\':
                        if (lat == true)
                        {
                            Chr[j++] = '\\';
                            lat = false;
                        }
                        else
                            lat = true;
                        break;

                    case 'r':
                        if (lat == true)
                        {
                            Chr[j++] = '\r';
                            lat = false;
                        }
                        else
                            Chr[j++] = pChr[i];
                        break;

                    case 'n':
                        if (lat == true)
                        {
                            Chr[j++] = '\n';
                            lat = false;
                        }
                        else
                            Chr[j++] = pChr[i];
                        break;

                    case 'a':
                        if (lat == true)
                        {
                            Chr[j++] = '\a';
                            lat = false;
                        }
                        else
                            Chr[j++] = pChr[i];
                        break;

                    default:
                        Chr[j++] = pChr[i];
                        lat = false;
                        break;
                }
            }

            char[] resChr = new char[j];

            for (i = 0; i < j; i++)
                resChr[i] = Chr[i];

            return resChr;
        }

        private void WriteInternalData(byte[] pD, int len)
        {
            string sdata = null;

            for (int i = 0; i < len; i++)
            {
                if (((pD[i] >= 0x20) && (pD[i] <= 0x7F)) || (pD[i] == '\r') || (pD[i] == '\n')) // printable characters
                    sdata += new string(new char[1] { (char)pD[i] });
                else
                    sdata += ("<" + pD[i].ToString("X2") + ">");
            }

            while (DebugWait) ;
            //DebugLog("\nRX: " + sdata, Color.Green);
            DebugLog(sdata, Color.Green);
        }

        private void SendCmdString()
        {
            string cmdStr = tbx_CmdStart.Text + tbx_CmdData.Text + tbx_CmdStop.Text;
            char[] cmdChr = cmdStr.ToCharArray();
            //cmdStr.Replace("\\r", "\r");
            //cmdStr.Replace("\\n", "\n");
            //cmdStr.Replace("\\a", "\a");
            //return cmdStr;

            try
            {
                while (DebugWait) ;
                //DebugLog("\nTX: " + GetCmdString(), Color.Blue);
                DebugLog("\nTX: " + cmdStr, Color.Blue);
                serialCdc.Write(new string(MakeFrame(cmdChr, cmdChr.Length)));
                DebugLog("\nRX: ", Color.Green);
            }
            catch
            {
                while (DebugWait) ;
                DebugLog("\nCan not write to Port 1", Color.Red);
            }
        }

        private int Char2Integer(char[] pChr, int offset, int len)
        {
            int i;
            int value = 0;

            for (i = 0; i < len; i++) 
            {
                value *= 10;

                if ((pChr[offset + i] >= '0') && (pChr[offset + i] <= '9'))
                    value += (int)(pChr[offset + i] - '0');
                else
                    return (-1);
            }

            return value;
        }

        private void SendScript()
        {
            int i = 0;

            if (scriptLineIdx >= ScriptData.Length)
                scriptLineIdx = 0;

            while (scriptLineIdx + i < ScriptData.Length)
            {
                if (ScriptData[scriptLineIdx + i] == '\n')
                    break;

                i++;
            }

            //while (DebugWait) ;
            //DebugLog("\n-->i=" + i.ToString() + ", idx=" + scriptLineIdx.ToString(), Color.Red);
            //scriptLineIdx += (i+1);

            
            char[] cmdChr = new char[i];

            for (i = 0; i < cmdChr.Length; i++)
                cmdChr[i] = ScriptData[scriptLineIdx + i];

            if (cmdChr[0] == '%') // command of script
            {
                scriptLineIdx += (cmdChr.Length + 1);
                SendScript();
            }
            else if (cmdChr[0] == '$') // delay
            {
                int newInterval = Char2Integer(cmdChr, 1, cmdChr.Length - 2);

                if (newInterval == (-1))
                {
                    while (DebugWait) ;
                    DebugLog("\nScript error: " + new string(cmdChr), Color.Red);
                }
                else if (ckb_Loop.Checked == true)
                {
                    bool bk = timer1.Enabled;

                    timer1.Enabled = false;
                    timer1.Interval = newInterval;
                    timer1.Enabled = bk;
                    while (DebugWait) ;
                    DebugLog("\nDelay: " + newInterval.ToString() + "ms", Color.Orange);
                    scriptLineIdx += (cmdChr.Length + 1);
                }
                else
                {
                    scriptLineIdx += (cmdChr.Length + 1);
                    SendScript();
                }
            }
            else // data
            {
                try
                {
                    string cmdStr = new string(cmdChr);

                    while (DebugWait) ;
                    //DebugLog("\nTX: " + GetCmdString(), Color.Blue);
                    DebugLog("\nTX: " + cmdStr, Color.Blue);
                    serialCdc.Write(new string(MakeFrame(cmdChr, cmdChr.Length)));
                    DebugLog("\nRX: ", Color.Green);
                    scriptLineIdx += (cmdChr.Length + 1);
                }
                catch
                {
                    while (DebugWait) ;
                    DebugLog("\nCan not write to Port 1", Color.Red);
                }
            }
            
        }

        private void DebugLog(string Msg, Color color)
        {
            DebugWait = true;
            rtb_Log.SelectionColor = color;
            rtb_Log.AppendText(Msg);
            rtb_Log.SelectionColor = rtb_Log.ForeColor;
            DebugWait = false;
        }

        private bool USBCDC_Is_Ready(string PortName)
        {
            string deviceid = null;

            if (PortName == null)
                return false;

            try
            {
                ManagementObjectSearcher deviceList = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");

                if (deviceList != null)
                {
                    foreach (ManagementObject device in deviceList.Get())// Check present device with supported list
                    {
                        if (device["InstanceName"].ToString().Contains(Pid))
                        {
                            deviceid = device["PortName"].ToString();

                            if (deviceid.Contains(PortName))
                                return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                while (DebugWait) ;
                DebugLog("\n\n[USBCDC_Is_Ready]\n" + e.ToString(), Color.Red);
            }

            return false;
        }

        private bool Port_Is_Present(string PortName)
        {
            string[] ports;

            if (InternalPort == true) 
                return true;

            ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                if (port == PortName)
                    return true;
            }

            return false;
        }

        private void Scan_UsbPort()
        {
            int found = 0;

            cb_Pid.SelectedIndex = 0;

            while (found == 0)
            {
                found = Get_Port1();

                if (found == 0)
                {
                    try
                    {
                        if (cb_Pid.SelectedIndex < (cb_Pid.Items.Count - 1))
                            cb_Pid.SelectedIndex++;
                        else
                        {
                            cb_Pid.SelectedIndex = 0;
                            while (DebugWait) ;
                            DebugLog("\n\nNo USB serial port", Color.OrangeRed);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        cb_Pid.SelectedIndex = 0;
                        while (DebugWait) ;
                        DebugLog("\n\n[Scan_UsbPort]\n" + ex.ToString() + "\n\nNo USB serial port", Color.Red);
                        break;
                    }
                }
            }
        }

        private void Disable_Uart_Components()
        {
            cb_Pid.Enabled = false;
            cb_Baud.Enabled = false;
            cb_DfuBaud.Enabled = false;
            cb_Port1.Enabled = false;
            cb_Port2.Enabled = false;
            cb_DfuPort.Enabled = false;
            bt_FwdScan.Enabled = false;
            bt_DfuScan.Enabled = false;
            bt_Browse.Enabled = false;
            nud_FrameDelay.Enabled = false;
        }

        private void Enable_Uart_Components()
        {
            cb_Pid.Enabled = true;
            cb_Baud.Enabled = true;
            cb_DfuBaud.Enabled = true;
            cb_Port1.Enabled = true;
            cb_Port2.Enabled = true;
            cb_DfuPort.Enabled = true;
            bt_FwdScan.Enabled = true;
            bt_DfuScan.Enabled = true;
            bt_Browse.Enabled = true;
            nud_FrameDelay.Enabled = true;
        }
        #endregion

        #region App Forward
        private int Get_Port1()
        {
            string[] ports;
            int found = 0;
            string deviceid = null;
            string portlist = null;

            try
            {
                cb_Port1.Items.Clear();
                ports = SerialPort.GetPortNames();
                ManagementObjectSearcher deviceList = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");

                if (deviceList != null)
                {
                    foreach (ManagementObject device in deviceList.Get())
                    {
                        //DebugLog("\n" + device["PortName"].ToString(), Color.Green);

                        if (device["InstanceName"].ToString().Contains(Pid))
                        {
                            deviceid = device["PortName"].ToString();

                            if (portlist == null)
                            {
                                found++;
                                portlist += deviceid;

                                if (deviceid != cb_Port2.Text)
                                    cb_Port1.Items.Add(deviceid);
                                //DebugLog("\n" + deviceid, Color.Green);
                            }
                            else if (!portlist.Contains(deviceid))
                            {
                                found++;
                                portlist += deviceid;

                                if (deviceid != cb_Port2.Text)
                                    cb_Port1.Items.Add(deviceid);
                                //DebugLog("\n" + deviceid, Color.Green);
                            }
                        }
                    }
                }

                if (found == 0)
                {
                    cb_Port1.Items.Add("Empty");
                    //DebugLog("\n\nEmpty COM port", Color.OrangeRed);
                }

                cb_Port1.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                cb_Port1.Items.Add("Empty");
                cb_Port1.SelectedIndex = 0;
                while (DebugWait) ;
                DebugLog("\n\n[Get_Port1]\n" + e.ToString(), Color.Red);
            }

            return found;
        }

        private int Get_Port2()
        {
            int found = 0;
            string[] ports;

            try
            {
                cb_Port2.Items.Clear();
                ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    if (port != cb_Port1.Text)
                    {
                        cb_Port2.Items.Add(port);
                        found++;
                    }
                }

                //if (found == 0)
                //{
                cb_Port2.Items.Add("Internal");
                    //DebugLog("\n\nEmpty COM port", Color.OrangeRed);
                //}

                cb_Port2.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                cb_Port2.Items.Add("Internal");
                cb_Port2.SelectedIndex = 0;
                while (DebugWait) ;
                DebugLog("\n\n[Get_Port2]\n" + e.ToString(), Color.Red);
            }

            return found;
        }

        private bool FwdPort_Is_Closed()
        {
            // Check USB first
            if (!USBCDC_Is_Ready(cb_Port1.Text))
                return true;

            if (!Port_Is_Present(cb_Port2.Text))
                return true;

            return false;
        }

        private bool FwdPort_Is_Openned()
        {
            return !FwdPort_Is_Closed();
        }

        private bool Open_Fwd_Connection()
        {
            bool success = false;

            if (cb_Port1.Text == "Empty") //((cb_Port1.Text == "Empty") || (cb_Port2.Text == "Empty"))
            {
                MessageBox.Show("Port1 is not empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                if (Port_Is_Present(cb_Port1.Text))
                {
                    if (serialCdc.IsOpen)
                    {
                        serialCdc.DtrEnable = false;
                        serialCdc.Dispose();
                        serialCdc.Close();
                    }

                    serialCdc.PortName = cb_Port1.Text;
                }
                else
                    return false;

                if (InternalPort == false)
                {
                    if (Port_Is_Present(cb_Port2.Text))
                    {
                        if (serialForward.IsOpen)
                        {
                            serialForward.DtrEnable = false;
                            serialForward.Dispose();
                            serialForward.Close();
                        }

                        serialForward.PortName = cb_Port2.Text;
                    }
                    else
                        return false;
                }

                serialCdc.BaudRate = int.Parse(cb_Baud.Text);
                serialCdc.Open();
                serialCdc.DtrEnable = true;

                if (InternalPort == false)
                {
                    serialForward.BaudRate = serialCdc.BaudRate;
                    serialForward.Open();
                    serialForward.DtrEnable = true;
                }

                lb_Status.Text = "Forwarding...";
                picConnected.Visible = true;
                picDisconnected.Visible = false;
                success = true;
                while (DebugWait) ;
                DebugLog("\n\nForwarding", Color.Blue);
            }
            catch (Exception e)
            {
                while (DebugWait) ;
                DebugLog("\n\n[Open_Fwd_Connection]\n" + e.ToString(), Color.Red);
                lb_Status.Text = "Can not open port!";
                picConnected.Visible = false;
                picDisconnected.Visible = true;
            }

            return success;
        }

        private void Close_Fwd_Connection()
        {
            try
            {
                if (serialCdc.IsOpen)
                {
                    serialCdc.DtrEnable = false;
                    serialCdc.Dispose();
                    serialCdc.Close();
                }

                if (serialForward.IsOpen)
                {
                    serialForward.DtrEnable = false;
                    serialForward.Dispose();
                    serialForward.Close();
                }

                lb_Status.Text = "No Connection";
                picConnected.Visible = false;
                picDisconnected.Visible = true;
                while (DebugWait) ;
                DebugLog("\n\nNo Connection", Color.Blue);
            }
            catch (Exception e)
            {
                while (DebugWait) ;
                DebugLog("\n\n[Close_Fwd_Connection]\n" + e.ToString(), Color.Red);
                lb_Status.Text = "Can not close port!";
                picConnected.Visible = false;
                picDisconnected.Visible = true;
            }
        }

        private bool App_Forward_Init()
        {
            bool success = false;

            if (cb_Port1.Text != "Empty")//((cb_Port1.Text != "Empty") && (cb_Port2.Text != "Empty"))
            {
                if (Open_Fwd_Connection())
                {
                    Thread_Task = new Thread(() => App_Forward()); // Create new app tasks
                    Thread_Task.Start();
                    success = true;
                }
                else
                    Scan_UsbPort();
            }
            else
                Scan_UsbPort();

            return success;
        }

        private void App_Forward_Deinit()
        {
            Close_Fwd_Connection();
            Thread_Task.Interrupt();
            Thread_Task.Abort();
            Thread_Task.Join();
            while (Thread_Task.IsAlive) ;
        }

        private void App_Forward()
        {
            try
            {
                int Count = 0;
                int DoNext = 0;

                while (true)
                {
                    try
                    {
                        switch (DoNext)
                        {
                            case 0: // Check closed port
                                if (FwdPort_Is_Closed())
                                {
                                    while (DebugWait) ;
                                    DebugLog("\n\nConnection is suspended", Color.OrangeRed);
                                    Close_Fwd_Connection();
                                    DoNext = 1;
                                    Count = 0;
                                }
                                break;

                            case 1:
                                if (++Count >= 2)
                                    DoNext = 2;
                                break;

                            case 2:
                                if (FwdPort_Is_Openned())
                                {
                                    Count = 0;
                                    DoNext = 3;
                                }
                                break;

                            case 3:
                            default:
                                if (++Count >= 1)
                                {
                                    if (Open_Fwd_Connection())
                                    {
                                        DoNext = 0;
                                        while (DebugWait) ;
                                        DebugLog("\n\nConnection is resumed", Color.Green);
                                    }
                                    else
                                    {
                                        DoNext = 2;
                                        Close_Fwd_Connection();
                                    }
                                }
                                break;
                        }
                    }
                    catch //(Exception e)
                    {
                        //DebugLog("\n\n[App_Forward-while(true)]\n" + e.ToString(), Color.Red);
                    }

                    Thread.Sleep(1000);
                }
            }
            catch //(Exception e)
            {
                //DebugLog("\n\n[App_Forward]\n" + e.ToString(), Color.Red);
            }
        }
        #endregion

        #region App Device Firmware Update
        private void Get_DfuPort()
        {
            string[] ports;

            try
            {
                cb_DfuPort.Items.Clear();
                ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    cb_DfuPort.Items.Add(port);
                }

                cb_DfuPort.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                cb_DfuPort.Items.Add("Empty");
                cb_DfuPort.SelectedIndex = 0;
                while (DebugWait) ;
                DebugLog("\n\n[Get_DfuPort]\n" + e.ToString(), Color.Red);
            }
        }

        private bool DfuPort_Is_Closed()
        {
            // Check USB first
            if (USBCDC_Is_Ready(cb_DfuPort.Text))
                return false;

            if (Port_Is_Present(cb_DfuPort.Text))
                return false;

            return true;
        }

        private bool DfuPort_Is_Openned()
        {
            return !DfuPort_Is_Closed();
        }

        private bool Open_Dfu_Connection()
        {
            bool success = false;

            if (cb_DfuPort.Text == "Empty")
            {
                MessageBox.Show("Ports are not empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                if (Port_Is_Present(cb_DfuPort.Text))
                {
                    if (serialDfu.IsOpen)
                    {
                        serialDfu.DtrEnable = false;
                        serialDfu.Dispose();
                        serialDfu.Close();
                    }

                    serialDfu.PortName = cb_DfuPort.Text;
                }
                else
                    return false;

                serialDfu.BaudRate = int.Parse(cb_DfuBaud.Text);
                serialDfu.Open();
                serialDfu.DtrEnable = true;
                lb_Status.Text = "Downloading... 0%";
                success = true;
                //while (DebugWait) ;
                //DebugLog("\n\nDownloading", Color.Blue);
            }
            catch (Exception e)
            {
                while (DebugWait) ;
                DebugLog("\n\n[Open_Dfu_Connection]\n" + e.ToString(), Color.Red);
                lb_Status.Text = "Can not open port!";
            }

            return success;
        }

        private void Close_Dfu_Connection()
        {
            try
            {
                if (serialDfu.IsOpen)
                {
                    serialDfu.DtrEnable = false;
                    serialDfu.Dispose();
                    serialDfu.Close();
                }
                else
                    return;

                lb_Status.Text = "No Connection";
                while (DebugWait) ;
                DebugLog("\n\nNo Connection", Color.Blue);
            }
            catch (Exception e)
            {
                while (DebugWait) ;
                DebugLog("\n\n[Close_Dfu_Connection]\n" + e.ToString(), Color.Red);
                lb_Status.Text = "Can not close port!";
            }
        }

        private bool App_Dfu_Init()
        {
            bool success = false;

            if (cb_DfuPort.Text != "Empty")
            {
                if (Open_Dfu_Connection())
                {
                    DfuStream = File.OpenRead(HexFilePath);
                    DfuReader = new StreamReader(DfuStream);
                    Thread_Task = new Thread(() => App_Dfu()); // Create new app tasks
                    Thread_Task.Start();
                    success = true;
                }
            }

            return success;
        }

        private void App_Dfu_Deinit()
        {
            Close_Dfu_Connection();
            DfuStream.Close();
            DfuStream.Close();
            Thread_Task.Interrupt();
            Thread_Task.Abort();
            Thread_Task.Join();
            while (Thread_Task.IsAlive) ;
        }

        private void App_Dfu()
        {
            try
            {
                int Per = 0;
                bool Resend = false;
                int LineCount = 0;
                int Count = 0;
                int DoNext = 1;
                string Data = null;
                int BuffLen = 0;
                byte[] Buff = new byte[4096];
                int Tdelay = (int)nud_FrameDelay.Value;

                while (DebugWait) ;
                DebugLog("\n\nStart: " + DateTime.Now.ToString(), Color.Green);

                while (true)
                {
                    try
                    {
                        switch (DoNext)
                        {
                            case 0: // Check closed port
                                Enable_Uart_Components();
                                bt_DfuStart.Visible = true;
                                bt_DfuStop.Visible = false;
                                bt_FwdStart.Enabled = true;

                                if (LineCount == HexFileDetail.NumOfLine)
                                {
                                    while (DebugWait) ;
                                    DebugLog("\nComplete", Color.Green);
                                }

                                while (DebugWait) ;
                                DebugLog("\nStop: " + DateTime.Now.ToString(), Color.Green);
                                DebugLog("\n\nConnection is suspended", Color.OrangeRed);
                                App_Dfu_Deinit();
                                break;

                            case 1: // Open port
                                if (Open_Dfu_Connection())
                                {
                                    DoNext++;
                                    LineCount = 0;
                                    while (DebugWait) ;
                                    DebugLog("\n\nPort openned", Color.Green);
                                }
                                else
                                    DoNext--;
                                break;

                            case 2: // Read 1 line
                                LineCount++;
                                Data = DfuReader.ReadLine();

                                if (Data != null)
                                {
                                    Resend = false;
                                    DoNext++;
                                }
                                else
                                    DoNext = 0;
                                break;

                            case 3: // Check openned port
                                if (DfuPort_Is_Openned())
                                    DoNext++;
                                else
                                    DoNext=0;
                                break;

                            case 4: // Send data
                                serialDfu.WriteLine(Data);

                                if (Resend)
                                {
                                    while (DebugWait) ;
                                    DebugLog("\nTX " + LineCount.ToString("D8") + ": " + Data, Color.OrangeRed);
                                }
                                else if (Per != ((100 * LineCount) / HexFileDetail.NumOfLine)) 
                                {
                                    Per = (100 * LineCount) / (int)HexFileDetail.NumOfLine;

                                    //while (DebugWait) ;
                                    //DebugLog("\n" + Per.ToString() + "%", Color.Blue);
                                    lb_Status.Text = "Downloading... " + Per.ToString() + "%";
                                }

                                Count = 0;
                                DoNext++;
                                break;

                            case 5: // Waiting for response
                                if (++Count < (3000 / Tdelay)) 
                                {
                                    BuffLen = 0;

                                    while (serialDfu.BytesToRead > 0)
                                    {
                                        int i;
                                        int tmp = serialDfu.BytesToRead;

                                        for (i = 0; i < tmp; i++)
                                            Buff[BuffLen + i] = (byte)serialDfu.ReadByte();

                                        BuffLen += tmp;
                                        Thread.Sleep(1);
                                    }

                                    if (BuffLen > 0) 
                                    {
                                        DoNext++;
                                        break;
                                    }
                                }
                                else
                                    DoNext = 0;
                                break;

                            case 6:
                                if ((Buff[0] == 'A') // New version
                                    || ((Buff[0] == 'O') && (Buff[1] == 'K'))) // Old version
                                {
                                    DoNext = 2;
                                    //while (DebugWait) ;
                                    //DebugLog("\nRX: OK", Color.Green);
                                }
                                else if ((Buff[0] == 'N') // New version
                                    || ((Buff[0] == 'E') && (Buff[1] == 'R')))  // Old version
                                {
                                    int i;
                                    string str;
                                    byte[] barr = new byte[BuffLen];

                                    for (i = 0; i < BuffLen; i++)
                                        barr[i] = Buff[i];

                                    str = ENC.GetString(barr);
                                    DoNext = 4;
                                    Resend = true;
                                    while (DebugWait) ;
                                    DebugLog("\nRX: " + str, Color.Green);
                                    DebugLog("\nERROR", Color.OrangeRed);
                                }
                                else
                                {
                                    while (DebugWait) ;
                                    DebugLog("\nResponse ERROR", Color.OrangeRed);
                                    DoNext = 0;
                                }
                                break;

                            default:
                                DoNext = 0;
                                break;
                        }
                    }
                    catch //(Exception e)
                    {
                        //while (DebugWait) ;
                        //DebugLog("\n\n[App_Forward-while(true)]\n" + e.ToString(), Color.Red);
                    }

                    Thread.Sleep(Tdelay);
                }
            }
            catch //(Exception e)
            {
                //DebugLog("\n\n[App_Forward]\n" + e.ToString(), Color.Red);
            }
        }
        #endregion

        #region System Functions
        public FormSPT()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.WindowState = FormWindowState.Normal;

            if (this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }

            cb_Baud.SelectedIndex = 9;
            cb_DfuBaud.SelectedIndex = 9;
            Get_Port2();
            Get_DfuPort();
            Scan_UsbPort();
            USBCDC_Is_Ready(null);

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text += " v."+$"{version}";
            this.Update();
        }

        private void cb_Port1_MouseClick(object sender, MouseEventArgs e)
        {
            Get_Port1();
        }

        private void cb_Port2_MouseClick(object sender, MouseEventArgs e)
        {
            Get_Port2();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bt_FwdScan.Enabled == false) 
            {
                App_Forward_Deinit();
                Enable_Uart_Components();

                bt_FwdStart.Visible = true;
                bt_FwdStop.Visible = false;
                bt_DfuStart.Enabled = true;

                Thread.Sleep(1000);
            }
            else if (bt_DfuStop.Visible == true) 
            {
                App_Dfu_Deinit();
                Enable_Uart_Components();

                bt_DfuStart.Visible = true;
                bt_DfuStop.Visible = false;
                bt_FwdStart.Enabled = true;

                Thread.Sleep(1000);
            }
        }

        private void cb_Port1_TextChanged(object sender, EventArgs e)
        {
            if ((cb_Port1.Text == cb_Port2.Text) && (cb_Port1.Text != "Empty"))
                Get_Port2();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] temp;
            int byte2read;
            bool sttBk = AppBusy;

            AppBusy = true;

            try
            {
                byte2read = serialCdc.BytesToRead;// Get number of bytes of received buffer
                temp = new byte[byte2read];// Create array to save data in received buffer
                serialCdc.Read(temp, 0, byte2read);// Read buffer data, save to array

                if (InternalPort == false)
                    serialForward.Write(temp, 0, byte2read);// Forward to serialport 2
                else
                    WriteInternalData(temp, byte2read);
            }
            catch (Exception ex)
            {
                while (DebugWait) ;
                DebugLog("\n\n[serialPort1_DataReceived]\n" + ex.ToString(), Color.Red);
            }

            AppBusy = sttBk;
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] temp;
            int byte2read;

            bool sttBk = AppBusy;

            AppBusy = true;

            try
            {
                byte2read = serialForward.BytesToRead;// Get number of bytes of received buffer
                temp = new byte[byte2read];// Create array to save data in received buffer
                serialForward.Read(temp, 0, byte2read);// Read buffer data, save to array
                serialCdc.Write(temp, 0, byte2read);// Forward to serialport 1
            }
            catch (Exception ex)
            {
                while (DebugWait) ;
                DebugLog("\n\n[serialPort2_DataReceived]\n" + ex.ToString(), Color.Red);
            }

            AppBusy = sttBk;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            bool MousePointerNotOnTaskbar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if ((this.WindowState == FormWindowState.Minimized) && MousePointerNotOnTaskbar)
            {
                notifyIcon1.BalloonTipText = "SPT has been minimized to system tray";
                notifyIcon1.ShowBalloonTip(2000);
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

            if (this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void rtb_Log_DoubleClick(object sender, EventArgs e)
        {
            rtb_Log.Clear();
        }

        private void cb_Pid_TextChanged(object sender, EventArgs e)
        {
            if (cb_Pid.Text == "VCP")
                Pid = "VID_04D8&PID_0057";
            else if (cb_Pid.Text == "CP2102N")
                Pid = "VID_10C4&PID_EA60";
            else if (cb_Pid.Text == "FT232RL")
                Pid = "VID_0403+PID_6001";
            else if (cb_Pid.Text == "TELIT")
                Pid = "VID_1BC7+PID_1100";
            else if (cb_Pid.Text == "SAMPI")
                Pid = "VID_0C00&PID_0123";
            else // USB CDC
                Pid = "PID_000A";

            Get_Port1();
        }

        private void rtb_Log_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearLog();
        }

        private void rtb_Log_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtb_Log.SelectionStart = rtb_Log.Text.Length;
            // scroll it automatically
            rtb_Log.ScrollToCaret();

            if(rtb_Log.Text.Length>=65000)
                ClearLog();
        }

        private void bt_Browse_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                HexFilePath = openFileDialog1.FileName;
                tb_FilePath.Text = HexFilePath;

                if (CheckFileFormat(HexFilePath, out HexFileDetail))
                {
                    DebugLog("\n\nFile name: " + openFileDialog1.FileName, Color.Blue);
                    DebugLog("\nTotal size: " + HexFileDetail.TotalLength.ToString(), Color.Blue);
                    DebugLog("\nNumber of lines: " + HexFileDetail.NumOfLine.ToString(), Color.Blue);
                }
                else
                {
                    HexFilePath = null;
                    tb_FilePath.Text = null;
                    MessageBox.Show("File is not in text format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bt_DfuScan_Click(object sender, EventArgs e)
        {
            Get_DfuPort();
        }

        private void cb_DfuPort_MouseClick(object sender, MouseEventArgs e)
        {
            Get_DfuPort();
        }

        private void bt_DfuStop_Click(object sender, EventArgs e)
        {
            App_Dfu_Deinit();
            Enable_Uart_Components();

            bt_DfuStart.Visible = true;
            bt_DfuStop.Visible = false;

            bt_FwdStart.Enabled = true;
        }

        private void bt_DfuStart_Click(object sender, EventArgs e)
        {
            if (App_Dfu_Init())
            {
                Disable_Uart_Components();

                bt_DfuStart.Visible = false;
                bt_DfuStop.Visible = true;

                bt_FwdStart.Enabled = false;
            }
        }

        private void bt_FwdStart_Click(object sender, EventArgs e)
        {
            if (App_Forward_Init())
            {
                Disable_Uart_Components();

                bt_FwdStart.Visible = false;
                bt_FwdStop.Visible = true;
                bt_DfuStart.Enabled = false;
            }
        }

        private void bt_FwdStop_Click(object sender, EventArgs e)
        {
            App_Forward_Deinit();
            Enable_Uart_Components();

            bt_FwdStart.Visible = true;
            bt_FwdStop.Visible = false;
            bt_DfuStart.Enabled = true;
        }

        private void tb_FilePath_TextChanged(object sender, EventArgs e)
        {
            if (HexFilePath.Length >= 3)
                bt_DfuStart.Enabled = true;
            else
                bt_DfuStart.Enabled = false;
        }

        private void bt_FwdScan_Click(object sender, EventArgs e)
        {
            //Get_Port1();
            Scan_UsbPort();
            Get_Port2();
        }

        private void lkl_Homepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lkl_Homepage.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/sampidevkit/");
        }

        private void picHomePage_Click(object sender, EventArgs e)
        {
            lkl_Homepage.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/sampidevkit/");
        }

        private void cb_Port2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Port2.Text == "Internal")
            {
                bt_Send.Enabled = true;
                InternalPort = true;
            }
            else
            {
                bt_Send.Enabled = false;
                InternalPort = false;
            }
        }

        private void bt_Send_Click(object sender, EventArgs e)
        {
            tbx_CmdStart.Enabled = false;
            tbx_CmdData.Enabled = false;
            tbx_CmdStop.Enabled = false;
            ckb_Loop.Enabled = false;
            ckb_Script.Enabled = false;

            if (ckb_Loop.Checked == true)
            {
                timer1.Enabled = true;
                bt_Send.Visible = false;
                bt_StopSend.Visible = true;
            }
            else
            {
                if (ckb_Script.Checked == true)
                    SendScript();
                else
                    SendCmdString();

                tbx_CmdStart.Enabled = true;
                tbx_CmdData.Enabled = true;
                tbx_CmdStop.Enabled = true;
                ckb_Loop.Enabled = true;
                ckb_Script.Enabled = true;
            }
        }

        private void nud_Interval_ValueChanged(object sender, EventArgs e)
        {
            bool bk = timer1.Enabled;

            timer1.Enabled = false;
            timer1.Interval = (int)nud_Interval.Value;
            timer1.Enabled = bk;
        }

        private void bt_StopSend_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                scriptLineIdx = 0;
                bt_StopSend.Visible = false;
                bt_Send.Visible = true;
            }
            catch
            {
                
            }

            tbx_CmdStart.Enabled = true;
            tbx_CmdData.Enabled = true;
            tbx_CmdStop.Enabled = true;
            ckb_Loop.Enabled = true;
            ckb_Script.Enabled = true;

            if ((ckb_Script.Checked == true) || (ckb_Loop.Checked == true))
            {
                tbx_CmdStart.Enabled = false;
                tbx_CmdData.Enabled = false;
                tbx_CmdStop.Enabled = false;
            }

            ckb_Loop.Enabled = true;
            ckb_Script.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ckb_Script.Checked == true)
                SendScript();
            else
                SendCmdString();
        }

        private void ckb_Loop_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_Script.Checked == false)
            {
                if (ckb_Loop.Checked == true)
                {
                    tbx_CmdStart.Enabled = false;
                    tbx_CmdData.Enabled = false;
                    tbx_CmdStop.Enabled = false;
                }
                else
                {
                    tbx_CmdStart.Enabled = true;
                    tbx_CmdData.Enabled = true;
                    tbx_CmdStop.Enabled = true;
                }
            }
        }

        private void bt_Script_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                scriptLineIdx = 0;
                ScriptFilePath = openFileDialog2.FileName;
                tbx_CmdData.Text = ScriptFilePath;

                if (CheckFileFormat(ScriptFilePath, out ScriptFileDetail))
                {
                    StreamReader sr = new StreamReader(ScriptFilePath);

                    ScriptData = sr.ReadToEnd().ToCharArray();
                    sr.Close();
                    //DebugLog("\n" + new string(ScriptData), Color.Black);
                    DebugLog("\n\nFile name: " + openFileDialog2.FileName, Color.Blue);
                    DebugLog("\nTotal size: " + ScriptFileDetail.TotalLength.ToString(), Color.Blue);
                    DebugLog("\nNumber of lines: " + ScriptFileDetail.NumOfLine.ToString(), Color.Blue);
                }
                else
                {
                    ScriptData = null;
                    ScriptFilePath = null;
                    tbx_CmdData.Text = null;
                    MessageBox.Show("File is not in text format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ckb_Script_CheckedChanged(object sender, EventArgs e)
        {
            scriptLineIdx = 0;
            bt_Script.Enabled = ckb_Script.Checked;

            if (ckb_Loop.Checked == false)
            {
                tbx_CmdData.Enabled = !ckb_Script.Checked;
                tbx_CmdStart.Enabled = !ckb_Script.Checked;
                tbx_CmdStop.Enabled = !ckb_Script.Checked;
            }
        }

        #endregion

        private void bt_TaskMngr_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("taskmgr.exe");
        }

        private void bt_DeviceMngr_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("diskmgmt.msc");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
