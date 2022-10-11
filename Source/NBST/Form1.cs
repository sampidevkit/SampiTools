using System;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Management;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Contexts;
using ZedGraph;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Runtime.Remoting.Messaging;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms.VisualStyles;
using System.Net;
using System.Device.Location;

namespace NBST
{
    public partial class Form1 : Form
    {
        private enum HeadTail
        {
            head,
            tail
        }

        private enum ThreadMode
        {
            RF_TEST,
            DOWNLOAD
        }

        private enum ThreadTask
        {
            CMD_ECHO_OFF = 0,
            CMD_DISPLAY_ERROR,
            CMD_NO_FLOW_CONTROL,
            CMD_GET_MODULE_NAME,
            CMD_GET_MODULE_IMEI,
            CMD_GET_CIMI,
            CMD_GET_ALT_CIMI,
            CMD_GET_CCID,
            CMD_GET_ALT_CCID,
            CMD_GET_OPERATOR_INFO,
            CMD_DEACT_PDP,
            CMD_SET_APN,
            CMD_SET_DNS,
            CMD_ACT_PDP,
            CMD_GET_CURRENT_PDP,
            CMD_SETUP_CELL_INFO,
            GET_LOCATION,
            CMD_GET_SIGNAL_QUALITY,
            CMD_GET_CELL_INFO,
            PARSE_INFO,
            CMD_CFG_HTTPS_HOST,
            CMD_CFG_HTTPS_FILE,
            CMD_GET_CURRENT_IP,
            CMD_GET_CURRENT_DNS,
            HTTPS_GET_FILE_INFO,
            CMD_GET_HTTPS_1500,
            CMD_CLOSE_SOCKET,
            CMD_RF_OFF,
            CMD_RF_ON,
            CMD_MODULE_REBOOT,
            CLOSE_APP
        }

        private struct FileParseCxt
        {
            public byte[] Data;
            public int Size;
            public int Index;
            public int Count;
            public int DoNext;
        }

        private struct CmdCxt
        {
            public char[] buffer;
            public int len;
            public int findIdx;
            public int donext;
            public UInt32 tick;
            public UInt32 proctime;
        }

        private struct DownloadCxt
        {
            public bool SslEn;
            public string Host;
            public string Port;
            public string FilePath;
            public string FileName;
            public string Md5;
            public int FileSize;
            public int DownloadedSize;
            public BinaryWriter sW;
        }

        private struct ModuleInfo
        {
            public string Name;
            public string Imei;
            public string Cimi;
            public string Ccid;
            public string Operator;
            public string NetworkType;
            public string Rsrp;
            public string Rsrq;
            public string Tac;
            public string CellID;
            public string Csq;
            public string BitErrRate;
            public string Apn; // user's APN
            public string Ip;
            public string OpApn; // operator's APN
            public string OpDns; // operator DNS
            public string OpAltDns; // operator alternator DNS
            public string UserDns;
        }

        private DownloadCxt downloadCxt = new DownloadCxt
        {
            SslEn = false,
            Host = null,
            Port = null,
            FilePath = null,
            FileName = null,
            Md5 = null,
            FileSize = 0,
            DownloadedSize = 0,
            sW = null
        };

        private ModuleInfo moduleInfo = new ModuleInfo
        {
            Name = null,
            Imei = null,
            Cimi = null,
            Ccid = null,
            Operator = null,
            NetworkType = null,
            Rsrp = null,
            Rsrq = null,
            Tac = null,
            CellID = null,
            Csq = null,
            BitErrRate = null,
            Apn = null,
            Ip = null,
            OpApn = null,
            OpDns = null,
            OpAltDns = null,
            UserDns = null
        };

        private volatile int tryCount = 0;
        private volatile bool debugEn = true;
        private volatile bool viewMode = false;
        private static Thread Thread_Task = null;
        private volatile ThreadMode Thread_Mode = ThreadMode.RF_TEST;
        private volatile int Thread_Enbale = 0;
        private volatile ThreadTask DoNext = ThreadTask.CMD_ECHO_OFF;
        private volatile ThreadTask ToDo = ThreadTask.CMD_ECHO_OFF;
        private StreamWriter sW = null;
        private string logfileName = null;
        private long line = 0;
        private UInt32 TickStart = 0;
        private volatile string portName = null;

        private string[] UsbPid = new string[4]
        { 
        /* MICROCHIP USB CDC */ "VID_04D8&PID_000A",
        /* xE866 */             "VID_1BC7&PID_0021", 
        /* MEx10G1 */           "VID_1BC7&PID_110A",
        /* LE910 */             "VID_1BC7&PID_1201"
        };

        private UInt32 Tick_Get()
        {
            return (UInt32)Environment.TickCount;
        }

        private UInt32 Tick_DifMs(UInt32 tk)
        {
            UInt32 t = (UInt32)Environment.TickCount;
            t = (UInt32)(t - tk);

            return t;
        }

        private bool Tick_IsOverMs(ref UInt32 tk, UInt32 ms)
        {
            UInt32 dif = Tick_DifMs(tk);

            if (dif > ms)
            {
                tk = Tick_Get();
                return true;
            }

            return false;
        }

        private bool FindString(char c, ref int pIdx, string StrSample)
        {
            char[] pStrSample = StrSample.ToCharArray();

            if (c == pStrSample[pIdx])
            {
                pIdx++;

                if (pIdx == pStrSample.Length)
                {
                    pIdx = 0;
                    return true; // matched
                }
            }
            else if (c == pStrSample[0])
                pIdx = 1;
            else
                pIdx = 0;

            return false;
        }

        private int GetUnixTimeSeconds(DateTime date)
        {
            DateTime point = new DateTime(1970, 1, 1);
            TimeSpan time = date.Subtract(point);

            return (int)time.TotalSeconds;
        }

        private int ToUnixTimeSeconds()
        {
            return GetUnixTimeSeconds(DateTime.Now);
        }

        private void ReplaceCharArray(ref char[] ChrArr, char oldChar, char newChar)
        {
            int i, j;
            int len = ChrArr.Length;

            /*
            PrintDebug("\nOld: ");

            if (isPrintable(oldChar))
                PrintDebug(oldChar.ToString());
            else
                PrintDebug("<" + Convert.ToByte(oldChar).ToString("X2") + ">");

            PrintDebug(", New: ");

            if (isPrintable(newChar))
                PrintDebug(newChar.ToString());
            else
                PrintDebug("<" + Convert.ToByte(newChar).ToString("X2") + ">");

            PrintChar("\nData: ", ChrArr, null);
            */
            for (i = 0; i < len; i++)
            {
                if (ChrArr[i] == oldChar)
                {
                    if (newChar != (char)0x00)
                        ChrArr[i] = newChar;
                    else
                    {
                        for (j = i; j < (len - 1); j++)
                            ChrArr[j] = ChrArr[j + 1];

                        i = 0;
                        ChrArr[j] = (char)0x00;
                        len--;
                    }
                }
            }

            char[] newChrArr = new char[len];

            for (i = 0; i < len; i++)
                newChrArr[i] = ChrArr[i];

            //PrintChar("\nReplace: ", newChrArr, null);

            ChrArr = null;
            ChrArr = newChrArr;
        }

        private void ReplaceStr(ref string Str, char oldChar, char newChar)
        {
            char[] Arr = Str.ToCharArray();

            ReplaceCharArray(ref Arr, oldChar, newChar);
            Str = new string(Arr);
        }

        private string RemoveInvalidName(string s)
        {
            int i, j, accept;
            char[] a = s.ToCharArray();

            for (i = 0, accept = 0; i < a.Length; i++)
            {
                if (a[i] == '_')
                    accept++;
                else if ((a[i] >= '0') && (a[i] <= '9'))
                    accept++;
                else if ((a[i] >= 'A') && (a[i] <= 'Z'))
                    accept++;
                else if ((a[i] >= 'a') && (a[i] <= 'z'))
                    accept++;
            }

            char[] arr = new char[accept];

            for (i = 0, j = 0; i < a.Length; i++)
            {
                if (a[i] == '_')
                    arr[j++] = a[i];
                else if ((a[i] >= '0') && (a[i] <= '9'))
                    arr[j++] = a[i];
                else if ((a[i] >= 'A') && (a[i] <= 'Z'))
                    arr[j++] = a[i];
                else if ((a[i] >= 'a') && (a[i] <= 'z'))
                    arr[j++] = a[i];
            }

            return new string(arr);
        }

        private void PrintDebug(string msg, Color color)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, Color>(PrintDebug), new object[] { msg, color });
                return;
            }

            if (debugEn == false)
                return;

            if (msg == null)
                return;

            rtb_Log.SelectionColor = color;
            rtb_Log.AppendText(msg);
            //rtb_Log.ForeColor = color;
        }

        private void PrintChar(string prefix, char[] chr, string subfix)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, char[], string>(PrintChar), new object[] { prefix, chr, subfix });
                return;
            }

            if (debugEn == false)
                return;

            PrintDebug(prefix + "Char len=" + chr.Length.ToString() + ": ");

            foreach (char c in chr)
            {
                if (isPrintable(c))
                    PrintDebug(c.ToString());
                else
                    PrintDebug("<" + Convert.ToByte(c).ToString("X2") + ">");
            }

            PrintDebug(subfix);
        }

        private void PrintDebug(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PrintDebug), new object[] { msg });
                return;
            }

            if (debugEn == false)
                return;

            PrintDebug(msg, Color.Black);
        }

        private void PrintTxDebug(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PrintTxDebug), new object[] { msg });
                return;
            }

            if (debugEn == false)
                return;

            PrintDebug(msg, Color.Blue);
        }

        private void PrintRxDebug(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PrintRxDebug), new object[] { msg });
                return;
            }

            if (debugEn == false)
                return;

            PrintDebug(msg, Color.Red);
        }

        private void PrintData(string msg, char[] data, int len, Color color)
        {
            if (debugEn == false)
                return;

            string s = msg + len.ToString() + "byte(s)\n";

            for (int i = 0; i < len; i++)
            {
                if (isPrintable(data[i]))
                    s += data[i].ToString();
                else
                    s += "<" + Convert.ToByte(data[i]).ToString("X2") + ">";
            }

            PrintDebug(s + "\n\n", color);
        }

        private void FileParseInit(ref FileParseCxt FpCxt, int BufferSize)
        {
            FpCxt.DoNext = 0;
            FpCxt.Count = 0;
            FpCxt.Data = new byte[BufferSize];
        }

        private string FileNameGenerate(string prefix)
        {
            string s = RemoveInvalidName(prefix + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString());

            return s;
        }

        private void WriteLogFile(int rsrp, int rsrq, int rssi, double lat, double lon)
        {
            if (sW != null)
            {
                line++;

                string s = "\n% https://maps.google.com/maps?hl=en&q=" + lat.ToString() + "," + lon.ToString() + "\n";
                s += line.ToString() + " " + rsrp.ToString() + " " + rsrq.ToString() + " " + rssi.ToString();
                sW.WriteLine(s);
                s = "\n" + line.ToString("D4") + " ";
                s += DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString() + ": ";
                s += lat.ToString() + ", " + lon.ToString();
                s += ", RSRP=" + rsrp.ToString() + "dBm, RSRQ=" + rsrq.ToString() + "dB, RSSI=" + rssi.ToString() + "dB";
                PrintDebug(s);
            }
        }

        private void CloseLogFile()
        {
            if (sW != null)
            {
                StreamWriter slog = new StreamWriter(logfileName + ".txt");

                slog.WriteLine(rtb_Log.Text);
                slog.Close();
                sW.WriteLine(NBST.Properties.Resources.footer);

                sW.Close();
                PrintDebug("\nLog file " + logfileName + ".m has been saved");
                Thread.Sleep(1000);
                //rtb_Log.Clear();
                sW = null;
            }
        }

        private void OpenLogFile()
        {
            CloseLogFile();
            logfileName = FileNameGenerate("NBST_");

            try
            {
                sW = new StreamWriter(logfileName + ".m");
                sW.WriteLine("%{");
                sW.WriteLine(rtb_Info.Text);
                sW.WriteLine("%}");
                sW.WriteLine(NBST.Properties.Resources.header);
                sW.WriteLine("\nstartTime=" + ToUnixTimeSeconds().ToString() + ";");
                sW.WriteLine("\na=[");

                rtb_Log.Text = "New log file " + logfileName + ".m has been created";
                line = 0;
            }
            catch
            {
                PrintDebug("\nFile name \"" + logfileName + "\"error");
            }
        }

        private string Parse_COMPort(string str)
        {
            // ...(COM10)
            int i, j, k;
            string s = null;
            char[] arr = str.ToCharArray();

            for (i = 0; i < (arr.Length - 4); i++)
            {
                if ((arr[i] == 'C') && (arr[i + 1] == 'O') && (arr[i + 2] == 'M'))
                {
                    i += 3;
                    break;
                }
            }

            for (j = i, k = 0; j < arr.Length; j++)
            {
                if (arr[j] == ')')
                {
                    if (j > i)
                        s = "COM" + k.ToString();

                    break;
                }

                if ((arr[j] >= '0') && (arr[j] <= '9'))
                {
                    k *= 10;
                    k += (int)(arr[j] - '0');
                }

            }

            return s;
        }

        private bool TestATPort(string portName, string cmd)
        {
            int loop = 0;
            bool found = false;
            string resp = null;
            UInt32 tick = Tick_Get();

            CmdCxt cmdCxt = new CmdCxt();
            cmdCxt.buffer = new char[4096];
            cmdCxt.donext = 0;
            cmdCxt.findIdx = 0;

            try
            {
                if (serialPort1.IsOpen)
                    return false;

                serialPort1.PortName = portName;
                serialPort1.BaudRate = 115200;
                serialPort1.WriteTimeout = 10;
                serialPort1.DtrEnable = true;
                serialPort1.RtsEnable = true;
                serialPort1.Open();
                //PrintDebug("\nOpen " + portName);

                do
                {
                    resp = SendCmd_GetRes(ref cmdCxt, cmd);

                    if (resp != null)
                    {
                        if (resp.Contains("\r\nOK\r\n"))
                        {
                            loop = 2;
                            found = true;
                        }
                    }
                    else if (Tick_IsOverMs(ref tick, 250))
                    {
                        loop++;
                        //PrintDebug("\nRX Timeout");
                    }
                }
                while (loop < 2);
            }
            catch
            {
                //PrintDebug("\nTX Timeout");
            }

            try
            {
                serialPort1.DtrEnable = false;
                serialPort1.RtsEnable = false;
                serialPort1.Close();
                serialPort1.Dispose();
            }
            catch { }

            return found;
        }

        private void Scan_AT_Port()
        {
            //string[] ports;
            int found = 0;
            string port = null;
            string portlist = null;

            
            PrintDebug("\nSupport device: ");

            foreach (string Pid in UsbPid)
            {
                PrintDebug("\n" + Pid);
            }
            

            try
            {
                cb_Port1.Items.Clear();
                cb_Port1.Enabled = false;
                cb_Port1.Text = "Searching...";
                ManagementObjectSearcher deviceList = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'");

                if (deviceList != null)
                {
                    foreach (ManagementObject device in deviceList.Get())
                    {
                        foreach (string Pid in UsbPid)
                        {
                            if (device["DeviceID"].ToString().Contains(Pid))
                            {
                                port = Parse_COMPort(device["Caption"].ToString()); // new
                                //PrintDebug("\nFound " + port);

                                if (portlist == null)
                                {
                                    portlist += port;
                                    //PrintDebug("\nFound: " + port);

                                    if (TestATPort(port, "ATE0\r"))
                                    {
                                        found++;
                                        cb_Port1.Items.Add(port);
                                    }
                                }
                                else if (!portlist.Contains(port))
                                {
                                    portlist += port;
                                    //PrintDebug("\nFound: " + port);

                                    if (TestATPort(port, "ATE0\r"))
                                    {
                                        found++;
                                        cb_Port1.Items.Add(port);
                                    }
                                }
                            }
                        }
                    }
                }
                /*********************************************************************************************************/
                deviceList = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem");

                if (deviceList != null)
                {
                    foreach (ManagementObject device in deviceList.Get())
                    {
                        port = device["AttachedTo"].ToString();
                        //PrintDebug("\nFound " + port);

                        if (portlist == null)
                        {
                            portlist += port;
                            //PrintDebug("\nFound: " + port);

                            if (TestATPort(port, "ATE0\r"))
                            {
                                found++;
                                cb_Port1.Items.Add(port);
                            }
                        }
                        else if (!portlist.Contains(port))
                        {
                            portlist += port;
                            //PrintDebug("\nFound: " + port);

                            if (TestATPort(port, "ATE0\r"))
                            {
                                found++;
                                cb_Port1.Items.Add(port);
                            }
                        }

                    }

                }
                /*********************************************************************************************************/
                if (found == 0)
                    cb_Port1.Items.Add("Empty");

                cb_Port1.SelectedIndex = 0;
            }
            catch
            {
                cb_Port1.Items.Add("Empty");
                cb_Port1.SelectedIndex = 0;
            }

            cb_Port1.Enabled = true;
            PrintDebug("\nFound: " + found.ToString() + " AT command port(s)");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("SupportedDevices.txt"))
            {
                StreamReader sRUsbDevice = new StreamReader("SupportedDevices.txt");
                string deviceStr = sRUsbDevice.ReadToEnd();
                
                sRUsbDevice.Close();
                ReplaceStr(ref deviceStr, '\r', (char)0x00);

                char[] deviceArr = deviceStr.ToCharArray();
                int lineCount = 1;

                foreach(char c in deviceArr)
                {
                    if (c == '\n')
                        lineCount++;
                }

                UsbPid = new string[lineCount];
                int lineIdx = 0;
                UsbPid[lineIdx] = null;

                foreach (char c in deviceArr)
                {
                    switch (c)
                    {
                        case '\n':
                            lineIdx++;

                            if (lineIdx < lineCount)
                                UsbPid[lineIdx] = null;
                            break;

                        default:
                            if (lineIdx < lineCount)
                                UsbPid[lineIdx] += c.ToString();
                            break;
                    }
                }
            }
            else
            {
                StreamWriter sW = new StreamWriter("SupportedDevices.txt");
                bool _1st = true;

                foreach (string s in UsbPid)
                {
                    if (_1st)
                    {
                        _1st = false;
                        sW.Write(s);
                    }
                    else
                        sW.Write("\n" + s);
                }

                sW.Close();
                MessageBox.Show("SupportedDevices.txt not found, use default supported devices", "Warning");
            }

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text += " v." + $"{version}";
            this.Update();

            downloadCxt.Md5 = tb_Md5.Text;
            moduleInfo.Apn = cb_Apn.Text;
            moduleInfo.UserDns = cb_Dns.Text;
            LoadUrl(false);
            Scan_AT_Port();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Thread_Task != null)
            {
                if (Thread_Task.IsAlive)
                {
                    Thread_Enbale = 0;
                    Thread_Task.Abort();
                    Thread_Task.Interrupt();
                    Thread_Task.Abort();
                    Thread_Task.Join();
                    while (Thread_Task.IsAlive) ;
                }
            }

            CloseLogFile();
        }

        private void rtb_Log_TextChanged(object sender, EventArgs e)
        {
            rtb_Log.SelectionStart = rtb_Log.Text.Length;
            rtb_Log.ScrollToCaret();

            if (line >= 3600)
                OpenLogFile();
        }

        private void bt_Scan_Click(object sender, EventArgs e)
        {
            Scan_AT_Port();
        }

        private void bt_RFTest_Update(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(bt_RFTest_Update), new object[] { msg });
                return;
            }

            CloseLogFile();
            bt_Download.Enabled = true;
            bt_Reboot.Enabled = true;
            bt_Scan.Enabled = true;
            cb_Apn.Enabled = true;
            cb_Dns.Enabled = true;
            bt_RFTest.Text = "RF Test";
        }

        private void bt_Download_Update(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(bt_Download_Update), new object[] { msg });
                return;
            }

            bt_RFTest.Enabled = true;
            bt_Scan.Enabled = true;
            cb_Apn.Enabled = true;
            cb_Dns.Enabled=true;
            cb_Url.Enabled = true;
            tb_Md5.Enabled = true;
            bt_Download.Text = "Download";
        }

        private void bt_RFTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (bt_RFTest.Text == "RF Test")
                {
                    tabCtrl1.SelectedTab = tabCtrl1.TabPages["tabGraph"];

                    zedGraphControl1.GraphPane.CurveList.Clear();
                    zedGraphControl1.GraphPane.GraphObjList.Clear();
                    zedGraphControl1.Refresh();

                    zedGraphControl1.GraphPane.Title.Text = "RF Measurement";
                    zedGraphControl1.GraphPane.XAxis.Title.Text = "Time";
                    zedGraphControl1.GraphPane.YAxis.Title.Text = "Strength";

                    RollingPointPairList list1 = new RollingPointPairList(65536);
                    RollingPointPairList list2 = new RollingPointPairList(65536);
                    RollingPointPairList list3 = new RollingPointPairList(65536);

                    LineItem curve1 = zedGraphControl1.GraphPane.AddCurve("RSRP (dBm)", list1, Color.Red, SymbolType.None);
                    LineItem curve2 = zedGraphControl1.GraphPane.AddCurve("RSRQ (dB)", list2, Color.Green, SymbolType.None);
                    LineItem curve3 = zedGraphControl1.GraphPane.AddCurve("RSSI (dBm)", list3, Color.Blue, SymbolType.None);

                    curve1.Line.Width = 2;
                    curve2.Line.Width = 2;
                    curve3.Line.Width = 2;

                    zedGraphControl1.GraphPane.XAxis.Scale.Min = 0;
                    zedGraphControl1.GraphPane.XAxis.Scale.Max = 60;
                    zedGraphControl1.GraphPane.XAxis.Scale.MinorStep = 1;
                    zedGraphControl1.GraphPane.XAxis.Scale.MajorStep = 5;
                    zedGraphControl1.AxisChange();

                    Thread_Mode = ThreadMode.RF_TEST;
                    Thread_Enbale = 1;
                    OpenLogFile();
                    bt_Download.Enabled = false;
                    bt_Reboot.Enabled = false;
                    bt_Scan.Enabled = false;
                    cb_Apn.Enabled = false;
                    cb_Dns.Enabled = false;
                    Thread_Task = new Thread(() => Thread_Tasks()); // Create new app tasks
                    Thread_Task.Start();
                    bt_RFTest.Text = "Stop";
                }
                else
                {
                    Thread_Enbale = 0;
                }
            }
            catch { }
        }

        private bool isPrintable(char c)
        {
            if ((c >= 0x20) && (c <= 0x7E))
                return true;

            return false;
        }

        private int Get_Index(char[] buffer, int offset, char chr, int chr_count)
        {
            if (chr_count == 0)
                return (-1);

            int len = buffer.Length - offset;

            for (int i = offset; i < len; i++)
            {
                if(chr == buffer[i])
                {
                    if (--chr_count == 0)
                        return i;
                }
            }

            return (-1);
        }

        private int Get_1stIndex(char[] buffer, int offset, string para, HeadTail head_tail)
        {
            int i, j;
            char[] p = para.ToCharArray();

            for (i = offset, j = 0; i < (buffer.Length - offset); i++)
            {
                if (buffer[i] == p[j])
                {
                    if (++j == p.Length)
                    {
                        if (head_tail == HeadTail.head)
                            i -= (j - 1);
                        /*
                        PrintDebug("\nIdx=" + i.ToString() + ", ");

                        if (isPrintable(buffer[i]))
                            PrintDebug(buffer[i].ToString());
                        else
                            PrintDebug("<" + Convert.ToByte(buffer[i]).ToString("X2") + ">");
                        */
                        return i;
                    }
                }
                else if (buffer[i] == p[0])
                    j = 1;
                else
                    j = 0;
            }

            return (-1);
        }

        private int Get_LastIndex(char[] buffer, string para, HeadTail head_tail)
        {
            int i, j;
            int l = (-1);
            char[] p = para.ToCharArray();

            for (i = 0, j = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == p[j])
                {
                    if (++j == p.Length)
                    {
                        if (head_tail == HeadTail.head)
                            i -= (j - 1);
                        /*
                        PrintDebug("\nIdx=" + i.ToString() + ", ");

                        if (isPrintable(buffer[i]))
                            PrintDebug(buffer[i].ToString());
                        else
                            PrintDebug("<" + Convert.ToByte(buffer[i]).ToString("X2") + ">");
                        */
                        l = i;
                        j = 0;
                    }
                }
                else if (buffer[i] == p[0])
                    j = 1;
                else
                    j = 0;
            }

            return l;
        }

        private char[] SubArray(char[] buffer, int headIdx, int tailIdx)
        {
            if ((headIdx >= 0) && (tailIdx >= 0) && (headIdx <= tailIdx))
            {
                char[] chr = new char[tailIdx - headIdx + 1];
                int i;

                for (i = 0; i < chr.Length; i++)
                    chr[i] = buffer[headIdx + i];

                return chr;
            }

            return null;
        }

        private char[] SubArray(char[] buffer, string str_head, string str_tail)
        {
            int head = Get_1stIndex(buffer, 0, str_head, HeadTail.tail) + 1;
            int tail = Get_1stIndex(buffer, 0, str_tail, HeadTail.head) - 1;

            //PrintDebug("\n--> head " + head.ToString()+ " tail " + tail.ToString());

            return SubArray(buffer, head, tail);
        }

        private char[] SubArray(char[] buffer, char begin, int beginCount, char end, int endCount)
        {
            int beginIdx = Get_Index(buffer, 0, begin, beginCount) + 1;
            int endIdx = Get_Index(buffer, 0, end, endCount) - 1;

            if (endIdx >= beginIdx)
            {
                char[] chars = new char[endIdx - beginIdx + 1];

                for (int i = 0; i < chars.Length; i++)
                    chars[i] = buffer[beginIdx + i];

                return chars;
            }

            return null;
        }

        private char[] SubArray(char[] buffer, string str_head, string str_tail, bool last)
        {
            int head, tail;

            if (last)
            {
                head = Get_LastIndex(buffer, str_head, HeadTail.tail) + 1;
                tail = Get_LastIndex(buffer, str_tail, HeadTail.head) - 1;
            }
            else
            {
                head = Get_1stIndex(buffer, 0, str_head, HeadTail.tail) + 1;
                tail = Get_1stIndex(buffer, 0, str_tail, HeadTail.head) - 1;
            }

            //PrintDebug("\n--> head " + head.ToString() + " tail " + tail.ToString());

            return SubArray(buffer, head, tail);
        }

        private string SendCmd_GetRes(ref CmdCxt cmdCxt, string cmd, UInt32 timeout, UInt32 wait, bool AT_Track)
        {
            try
            {
                switch (cmdCxt.donext)
                {
                    case 0:
                        cmdCxt.donext++;
                        cmdCxt.buffer = new char[4096];
                        cmdCxt.tick = Tick_Get();
                        serialPort1.Write(cmd);
                        PrintTxDebug("\nTX: " + cmd);
                        break;

                    case 1:
                        if (serialPort1.BytesToRead > 0)
                        {
                            cmdCxt.donext++;
                            cmdCxt.len = 0;
                            cmdCxt.tick = Tick_Get();
                            PrintRxDebug("\nRX: ");
                        }
                        else if (Tick_IsOverMs(ref cmdCxt.tick, timeout))
                        {
                            cmdCxt.donext--;
                            PrintDebug("\nRX Timeout");
                            return "RX TIMEOUT";
                        }
                        break;

                    case 2:
                        if (serialPort1.BytesToRead > 0)
                        {
                            for (int i = 0; i < serialPort1.BytesToRead; i++)
                            {
                                char c = (char)serialPort1.ReadByte();

                                cmdCxt.buffer[cmdCxt.len++] = c;

                                if (cmdCxt.len > cmdCxt.buffer.Length)
                                    cmdCxt.len = 0;

                                if ((AT_Track == true) && FindString(c, ref cmdCxt.findIdx, "\r\nOK\r\n"))
                                {
                                    cmdCxt.donext++;
                                    //PrintRxDebug("\n-->OK\n");
                                }

                                //PrintRxDebug("\nIdx: " + cmdCxt.findIdx.ToString() + ", ");
                            }

                            cmdCxt.tick = Tick_Get();
                        }
                        else if (Tick_IsOverMs(ref cmdCxt.tick, wait))
                            cmdCxt.donext++;
                        break;

                    default:
                        char[] chr = new char[cmdCxt.len];
                        string s = null;

                        for (int i = 0; i < cmdCxt.len; i++)
                        {
                            chr[i] = cmdCxt.buffer[i];

                            if (isPrintable(chr[i]))
                                s += chr[i].ToString();
                            else
                                s += "<" + Convert.ToByte(chr[i]).ToString("X2") + ">";
                        }

                        PrintRxDebug(s);
                        cmdCxt.buffer = chr;
                        cmdCxt.donext = 0;

                        return new string(chr);
                }
            }
            catch
            {
                //PrintDebug(ex.ToString());
            }

            return null;
        }

        private string SendCmd_GetData(ref CmdCxt cmdCxt, string cmd, UInt32 timeout, UInt32 wait, int datasize, bool AT_Track)
        {
            try
            {
                switch (cmdCxt.donext)
                {
                    case 0:
                        cmdCxt.donext++;
                        cmdCxt.buffer = new char[4096];
                        cmdCxt.tick = Tick_Get();
                        cmdCxt.proctime = Tick_Get();
                        serialPort1.Write(cmd);
                        PrintTxDebug("\nTX: " + cmd);
                        break;

                    case 1:
                        if (serialPort1.BytesToRead > 0)
                        {
                            cmdCxt.donext++;
                            cmdCxt.len = 0;
                            cmdCxt.tick = Tick_Get();
                            PrintRxDebug("\nRX: ");
                        }
                        else if (Tick_IsOverMs(ref cmdCxt.tick, timeout))
                        {
                            cmdCxt.donext--;
                            cmdCxt.proctime = Tick_DifMs(cmdCxt.proctime) - timeout;
                            PrintDebug("\nRX Timeout");
                            return "RX TIMEOUT";
                        }
                        break;

                    case 2:
                        if (serialPort1.BytesToRead > 0)
                        {
                            for (int i = 0; i < serialPort1.BytesToRead; i++)
                            {
                                char c = (char)serialPort1.ReadByte();

                                cmdCxt.buffer[cmdCxt.len++] = c;

                                if (cmdCxt.len > cmdCxt.buffer.Length)
                                    cmdCxt.len = 0;

                                if ((AT_Track == true) && FindString(c, ref cmdCxt.findIdx, "\r\nOK\r\n"))
                                {
                                    cmdCxt.donext++;
                                    cmdCxt.proctime = Tick_DifMs(cmdCxt.proctime);
                                    PrintRxDebug("\n-->OK\n");
                                }

                                //PrintRxDebug("\nIdx: " + cmdCxt.findIdx.ToString() + ", ");
                            }

                            if (/*cmdCxt.len >= 1511*/cmdCxt.len >= (datasize + 11))
                            {
                                cmdCxt.donext++;
                                cmdCxt.proctime = Tick_DifMs(cmdCxt.proctime);
                                PrintRxDebug("\nDownload " + cmdCxt.len.ToString() + " byte(s)");
                            }

                            cmdCxt.tick = Tick_Get();
                        }
                        else if (Tick_IsOverMs(ref cmdCxt.tick, wait))
                        {
                            cmdCxt.donext++;
                            cmdCxt.proctime = Tick_DifMs(cmdCxt.proctime) - wait;
                        }
                        break;

                    default:
                        char[] chr = new char[cmdCxt.len];
                        string s = null;

                        for (int i = 0; i < cmdCxt.len; i++)
                        {
                            chr[i] = cmdCxt.buffer[i];

                            if (isPrintable(chr[i]))
                                s += chr[i].ToString();
                            else
                                s += "<" + Convert.ToByte(chr[i]).ToString("X2") + ">";
                        }

                        PrintRxDebug(s);
                        cmdCxt.buffer = chr;
                        cmdCxt.donext = 0;

                        return new string(chr);
                }
            }
            catch
            {
                //PrintDebug(ex.ToString());
            }

            return null;
        }

        private int GetData1500(ref CmdCxt cmdCxt, ref FileParseCxt FpCxt)
        {
            switch (cmdCxt.donext)
            {
                case 0:
                    cmdCxt.donext++;
                    cmdCxt.tick = Tick_Get();
                    cmdCxt.proctime = Tick_Get();
                    serialPort1.Write("AT#HTTPRCV=0,1500\r");
                    PrintTxDebug("\nTX: AT#HTTPRCV=0,1500\r");
                    break;

                case 1: // wait for responding
                    if (serialPort1.BytesToRead > 0)
                    {

                    }
                    else if (Tick_IsOverMs(ref cmdCxt.tick, 60000))
                    {

                    }

                    break;

                case 1: // header \r
                    if (b == '\r')
                        FpCxt.DoNext++;
                    else
                        FpCxt.DoNext = 0;
                    break;

                case 1: // header \n
                    if (b == '\n')
                    {
                        FpCxt.DoNext++;
                        FpCxt.Count = 0;
                    }
                    else
                        FpCxt.DoNext = 0;
                    break;

                case 2: // >>>
                    if (b == '>')
                    {
                        FpCxt.Count++;

                        if (FpCxt.Count == 3)
                        {
                            FpCxt.DoNext++;
                            FpCxt.Count = 0;
                            FpCxt.Index = 0;
                        }
                    }
                    else
                        FpCxt.DoNext = 0;
                    break;

                case 3: // data
                    FpCxt.Data[FpCxt.Index++] = b;
                    break;

                default:
                    break;
            }

            return 0; // busy
        }

        private string SendCmd_GetRes(ref CmdCxt cmdCxt, string cmd, UInt32 timeout, bool AT_Track)
        {
            return SendCmd_GetRes(ref cmdCxt, cmd, timeout, 100, AT_Track);
        }

        private string Get_ExtResp(ref CmdCxt cmdCxt, string escSeq, UInt32 timeout)
        {
            try
            {
                switch (cmdCxt.donext)
                {
                    case 0:
                        cmdCxt.donext++;
                        cmdCxt.buffer = new char[4096];
                        cmdCxt.tick = Tick_Get();
                        break;

                    case 1:
                        if (serialPort1.BytesToRead > 0)
                        {
                            cmdCxt.donext++;
                            cmdCxt.len = 0;
                            cmdCxt.tick = Tick_Get();
                            PrintRxDebug("\nRX: ");
                        }
                        else if (Tick_IsOverMs(ref cmdCxt.tick, timeout))
                        {
                            cmdCxt.donext--;
                            PrintDebug("\nRX Timeout");
                            return "RX TIMEOUT";
                        }
                        break;

                    case 2:
                        if (serialPort1.BytesToRead > 0)
                        {
                            for (int i = 0; i < serialPort1.BytesToRead; i++)
                            {
                                char c = (char)serialPort1.ReadByte();

                                cmdCxt.buffer[cmdCxt.len++] = c;

                                if (cmdCxt.len > cmdCxt.buffer.Length)
                                    cmdCxt.len = 0;

                                if (escSeq != null)
                                {
                                    if (FindString(c, ref cmdCxt.findIdx, escSeq))
                                        cmdCxt.donext++;
                                }

                                //PrintRxDebug("\nIdx: " + cmdCxt.findIdx.ToString() + ", ");
                            }

                            cmdCxt.tick = Tick_Get();
                        }
                        else if (Tick_IsOverMs(ref cmdCxt.tick, 3000))
                            cmdCxt.donext++;
                        break;

                    default:
                        char[] chr = new char[cmdCxt.len];
                        string s = null;

                        for (int i = 0; i < cmdCxt.len; i++)
                        {
                            chr[i] = cmdCxt.buffer[i];

                            if (isPrintable(chr[i]))
                                s += chr[i].ToString();
                            else
                                s += "<" + Convert.ToByte(chr[i]).ToString("X2") + ">";
                        }

                        PrintRxDebug(s);
                        cmdCxt.buffer = chr;
                        cmdCxt.donext = 0;

                        return new string(chr);
                }
            }
            catch
            {
                //PrintDebug(ex.ToString());
            }

            return null;
        }

        private string SendCmd_GetRes(ref CmdCxt cmdCxt, string cmd, UInt32 timeout)
        {
            return SendCmd_GetRes(ref cmdCxt, cmd, timeout, true);
        }

        private string SendCmd_GetRes(ref CmdCxt cmdCxt, string cmd)
        {
            return SendCmd_GetRes(ref cmdCxt, cmd, 500, true);
        }

        private void InfoAppendText(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(InfoAppendText), new object[] { msg });
                return;
            }

            if (msg == null)
                return;

            rtb_Info.SelectionColor = Color.Black;
            rtb_Info.AppendText(msg);
        }

        private void InfoAppendText(string msg, Color color)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, Color>(InfoAppendText), new object[] { msg, color });
                return;
            }

            if (msg == null)
                return;

            rtb_Info.SelectionColor = color;
            rtb_Info.AppendText(msg);
        }

        private void InfoWriteText(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(InfoWriteText), new object[] { msg });
                return;
            }

            if (msg == null)
                return;

            rtb_Info.SelectionColor = Color.Black;
            rtb_Info.Text = msg;
        }

        private void ProgressBar_Update(int maxSize, int curSize)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int, int>(ProgressBar_Update), new object[] { maxSize, curSize });
                return;
            }

            float per = ((float)curSize * 100.0f) / (float)maxSize + 0.5f;

            pgb_Percent.Value = (int)per;
            lb_Percent.Text = curSize.ToString() + " / " + maxSize.ToString();
        }

        private string RemoveAT(string atResp)
        {
            char[] arr = atResp.ToCharArray();
            int head = Get_1stIndex(arr, 0, "\r\nOK\r\n", HeadTail.head);

            if (head >= 0)
            {
                arr[head + 2] = '\r';
                arr[head + 3] = '\n';
            }

            atResp = new string(arr);
            ReplaceStr(ref atResp, '\r', '\0');
            ReplaceStr(ref atResp, '\n', '\0');

            return atResp;
        }

        private void PlotData(int rsrp, int rsrq, int rssi, long count)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int, int, int, long>(PlotData), new object[] { rsrp, rsrq, rssi, count });
                return;
            }

            LineItem curve1 = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
            LineItem curve2 = zedGraphControl1.GraphPane.CurveList[1] as LineItem;
            LineItem curve3 = zedGraphControl1.GraphPane.CurveList[2] as LineItem;

            if ((curve1 == null) || (curve2 == null) || (curve3 == null))
                return;

            IPointListEdit list1 = curve1.Points as IPointListEdit;
            IPointListEdit list2 = curve2.Points as IPointListEdit;
            IPointListEdit list3 = curve3.Points as IPointListEdit;

            if ((list1 == null) || (list2 == null) || (list3 == null))
                return;

            list1.Add(TickStart, (double)rsrp);
            list2.Add(TickStart, (double)rsrq);
            list3.Add(TickStart, (double)rssi);

            Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;

            if (TickStart > xScale.Max - xScale.MajorStep)
            {
                if (viewMode)
                {
                    xScale.Max = TickStart + xScale.MajorStep;
                    xScale.Min = 0;
                }
                else
                {
                    xScale.Max = TickStart + xScale.MajorStep;
                    xScale.Min = xScale.Max - 60.0;
                }
            }

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private char[] HttpRcv_GetData(in char[] datain, int lenin)
        {
            char[] dataout = null;
            int beginIdx = Get_1stIndex(datain, 0, "<<<", HeadTail.tail) + 1;
            int endIdx = Get_LastIndex(datain, "\r\nOK\r\n", HeadTail.head);

            //PrintDebug("\n\nFirst=" + beginIdx.ToString() + "\nLast=" + endIdx.ToString());
            //PrintData("\nInput: ", datain, lenin, Color.Blue);

            if ((lenin > 9) && (endIdx > beginIdx))
            {
                dataout = new char[endIdx - beginIdx];

                for (int i = beginIdx, j = 0; i < endIdx; i++)
                {
                    dataout[j] = datain[beginIdx + j];
                    j++;
                }
            }

            //PrintData("\nOutput: ", dataout, dataout.Length, Color.Red);

            return dataout;
        }

        private void Thread_Tasks()
        {
            int csq = 99;
            int ber = 99;
            int rssi = -150;
            int rsrp = -150;
            int rsrq = -150;
            double lon = 0, lat = 0;
            string tmpStr = null;
            char[] tmpArr = null;
            UInt32 DownloadTime = 0;
            CmdCxt cmdCxt = new CmdCxt();

            cmdCxt.buffer = new char[4096];
            cmdCxt.donext = 0;
            cmdCxt.findIdx = 0;

            moduleInfo.Name = null;
            moduleInfo.Imei = null;
            moduleInfo.Cimi = null;
            moduleInfo.Ccid = null;
            moduleInfo.Operator = null;
            moduleInfo.NetworkType = null;
            moduleInfo.Rsrp = null;
            moduleInfo.Rsrq = null;
            moduleInfo.Tac = null;
            moduleInfo.CellID = null;
            moduleInfo.Csq = null;
            moduleInfo.BitErrRate = null;
            moduleInfo.Ip = null;
            moduleInfo.OpApn = null;
            moduleInfo.OpDns = null;
            moduleInfo.OpAltDns = null;

            try
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();

                serialPort1.PortName = portName;
                serialPort1.BaudRate = 115200;
                serialPort1.WriteTimeout = 10;
                serialPort1.DtrEnable = true;
                serialPort1.RtsEnable = true;
                serialPort1.Open();
            }
            catch
            {
                Thread_Enbale = 0;

                if (Thread_Mode == ThreadMode.RF_TEST)
                    bt_RFTest_Update(null);
                else
                    bt_Download_Update(null);

                Thread_Task.Abort();
                Thread_Task.Interrupt();
                Thread_Task.Abort();
                Thread_Task.Join();
                return;
            }

            DoNext = ThreadTask.CMD_ECHO_OFF;
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
            InfoWriteText("Reading module info...");

            UInt32 thisTick = Tick_Get();

            while (Thread_Enbale == 1)
            {
                switch (DoNext)
                {
                    case ThreadTask.CMD_ECHO_OFF:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "ATE0\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;

                        }
                        break;

                    case ThreadTask.CMD_DISPLAY_ERROR:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CMEE=2\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n") || tmpStr.Contains("ERROR"))
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_NO_FLOW_CONTROL:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT&K0\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_GET_MODULE_NAME:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGMM\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "+CGMM: ", "\r\nOK"));
                                moduleInfo.Name = RemoveAT(tmpStr);
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_MODULE_IMEI:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGSN\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                moduleInfo.Imei = RemoveAT(tmpStr);
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CIMI:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CIMI\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext = ThreadTask.CMD_GET_CCID;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "+CIMI: ", "\r\nOK"));
                                moduleInfo.Cimi = RemoveAT(tmpStr);
                            }
                            else if (tmpStr.Contains("ERROR"))
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_GET_ALT_CIMI:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#CIMI\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "#CIMI: ", "\r\nOK"));
                                moduleInfo.Cimi = RemoveAT(tmpStr);
                            }
                            else if (tmpStr.Contains("ERROR"))
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_GET_CCID:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CCID\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext += 2;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "+CCID: ", "\r\nOK"));
                                moduleInfo.Ccid = RemoveAT(tmpStr);
                            }
                            else if (tmpStr.Contains("ERROR"))
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_GET_ALT_CCID:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#CCID\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "#CCID: ", "\r\nOK"));
                                moduleInfo.Ccid = RemoveAT(tmpStr);
                            }
                            else if (tmpStr.Contains("ERROR"))
                                DoNext++;
                            else
                            {
                                DoNext--;
                                Thread.Sleep(250);
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_OPERATOR_INFO:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+COPS?\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                TickStart = 0;
                                moduleInfo.Operator = new string(SubArray(cmdCxt.buffer, ",\"", "\","));
                                moduleInfo.NetworkType = new string(SubArray(cmdCxt.buffer, "\",", "\r\nOK"));
                                ReplaceStr(ref moduleInfo.NetworkType, ' ', '\0');
                                ReplaceStr(ref moduleInfo.NetworkType, '\r', '\0');
                                ReplaceStr(ref moduleInfo.NetworkType, '\n', '\0');
                            }
                        }
                        break;

                    case ThreadTask.CMD_DEACT_PDP:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SGACT=1,0\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                            else if (tmpStr.Contains("ERROR") || tmpStr.Contains("RX TIMEOUT"))
                                DoNext = ThreadTask.CMD_SET_APN;
                        }
                        break;

                    case ThreadTask.CMD_SET_APN:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGDCONT=1,\"IP\",\"" + moduleInfo.Apn + "\"\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_SET_DNS:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#DNS=1," + moduleInfo.UserDns + "\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n") || tmpStr.Contains("ERROR"))
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_ACT_PDP:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SGACT=1,1\r", 30000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "#SGACT: ", "\r\nOK"));
                                moduleInfo.Ip = RemoveAT(tmpStr);
                            }
                            else if (tmpStr.Contains("ERROR") || tmpStr.Contains("RX TIMEOUT"))
                            {
                                DoNext = ThreadTask.CMD_GET_CURRENT_PDP;
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CURRENT_PDP:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGCONTRDP=1\r", 10000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                tmpStr = new string(SubArray(cmdCxt.buffer, ",\"", "\","));
                                moduleInfo.OpApn = RemoveAT(tmpStr);

                                if (moduleInfo.Ip == null)
                                {
                                    tmpStr = new string(SubArray(cmdCxt.buffer, ',', 3, ',', 4));
                                    ReplaceStr(ref tmpStr, '\"', '\0');
                                    ReplaceStr(ref tmpStr, ',', '\0');
                                    moduleInfo.Ip = tmpStr;
                                }

                                tmpStr = new string(SubArray(cmdCxt.buffer, ',', 5, ',', 6));
                                ReplaceStr(ref tmpStr, '\"', '\0');
                                ReplaceStr(ref tmpStr, ',', '\0');
                                moduleInfo.OpDns = tmpStr;

                                tmpStr = new string(SubArray(cmdCxt.buffer, ',', 6, '\r', 2));
                                ReplaceStr(ref tmpStr, '\"', '\0');
                                ReplaceStr(ref tmpStr, ',', '\0');
                                moduleInfo.OpAltDns = tmpStr;
                            }
                            else if (tmpStr.Contains("ERROR"))
                            {
                                DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.CMD_SETUP_CELL_INFO:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#MONI=0\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                thisTick = Tick_Get();
                            }
                            else if (tmpStr.Contains("ERROR"))
                            {
                                DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.GET_LOCATION:
                        // Do not suppress prompt, and wait 1000 milliseconds to start.
                        watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

                        GeoCoordinate coord = watcher.Position.Location;

                        if (coord.IsUnknown != true)
                        {
                            //PrintDebug("\nLat: " + coord.Latitude.ToString() + ", Long: " + coord.Longitude.ToString());
                            lon = coord.Longitude;
                            lat = coord.Latitude;
                        }
                        //else
                            //PrintDebug("\nUnknown latitude and longitude.");

                        DoNext++;
                        break;

                    case ThreadTask.CMD_GET_SIGNAL_QUALITY:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CSQ\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                moduleInfo.Csq = new string(SubArray(cmdCxt.buffer, "+CSQ: ", ","));
                                moduleInfo.BitErrRate = new string(SubArray(cmdCxt.buffer, ",", "\r\nOK"));
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CELL_INFO:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#MONI\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;

                                if (moduleInfo.Operator == null)
                                    moduleInfo.Operator = new string(SubArray(cmdCxt.buffer, "#MONI: ", " RSRP:"));

                                moduleInfo.Rsrp = new string(SubArray(cmdCxt.buffer, "RSRP:", " RSRQ:"));
                                moduleInfo.Rsrq = new string(SubArray(cmdCxt.buffer, "RSRQ:", " TAC:"));
                                moduleInfo.Tac = new string(SubArray(cmdCxt.buffer, "TAC:", " Id:"));
                                moduleInfo.CellID = new string(SubArray(cmdCxt.buffer, " Id:", " EARFCN:"));
                                //PrintDebug("\nRSRP: " + moduleInfo.Rsrp);
                                //PrintDebug("\nRSRQ: " + moduleInfo.Rsrq);
                            }
                            else if (tmpStr.Contains("ERROR"))
                            {
                                DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.PARSE_INFO:
                        if (moduleInfo.Name != null)
                            InfoWriteText("Module:          " + moduleInfo.Name);

                        if (moduleInfo.Imei != null)
                            InfoAppendText("\nIMEI:            " + moduleInfo.Imei);

                        if (moduleInfo.Cimi != null)
                            InfoAppendText("\nCIMI:            " + moduleInfo.Cimi);

                        if (moduleInfo.Ccid != null)
                            InfoAppendText("\nCCID:            " + moduleInfo.Ccid);

                        if (moduleInfo.Operator != null)
                            InfoAppendText("\nOperator:        " + moduleInfo.Operator);

                        if (moduleInfo.NetworkType != null)
                        {
                            InfoAppendText("\nNetwork " + "(" + moduleInfo.NetworkType + "):     ");

                            int netType;

                            try
                            {
                                netType = int.Parse(moduleInfo.NetworkType);
                            }
                            catch
                            {
                                netType = (-1);
                                moduleInfo.NetworkType = "-1";
                            }

                            switch (netType)
                            {
                                case 0:
                                    InfoAppendText("GSM");
                                    break;

                                case 1:
                                    InfoAppendText("GSM Compact");
                                    break;

                                case 2:
                                    InfoAppendText("UTRAN");
                                    break;

                                case 3:
                                    InfoAppendText("GSM/EGPRS");
                                    break;

                                case 4:
                                    InfoAppendText("UTRAN/HSDPA");
                                    break;

                                case 5:
                                    InfoAppendText("UTRAN/HSUPA");
                                    break;

                                case 6:
                                    InfoAppendText("UTRAN/HSDPA&HSUPA");
                                    break;

                                case 7:
                                    InfoAppendText("E-UTRAN");
                                    break;

                                case 8:
                                    InfoAppendText("CAT M1");
                                    break;

                                case 9:
                                    InfoAppendText("NB IoT");
                                    break;

                                default:
                                    InfoAppendText("Unknown");
                                    break;
                            }
                        }

                        if (moduleInfo.Ip != null)
                            InfoAppendText("\nIP:              " + moduleInfo.Ip);

                        if (moduleInfo.OpDns != null)
                            InfoAppendText("\nDNS:             " + moduleInfo.OpDns);

                        if (moduleInfo.OpAltDns != null)
                            InfoAppendText("\nALT DNS:         " + moduleInfo.OpAltDns);

                        if (moduleInfo.OpApn != null)
                            InfoAppendText("\nAPN:             " + moduleInfo.OpApn);

                        if (moduleInfo.Tac != null)
                        {
                            InfoAppendText("\nTAC(LAC):        ");

                            try
                            {
                                InfoAppendText(moduleInfo.Tac + " (" + Convert.ToUInt32(moduleInfo.Tac, 16).ToString() + ")");
                            }
                            catch
                            {
                                InfoAppendText("Unknown", Color.Red);
                            }
                        }

                        if (moduleInfo.CellID != null)
                        {
                            InfoAppendText("\nCell ID:         ");

                            try
                            {
                                InfoAppendText(moduleInfo.CellID + " (" + Convert.ToUInt32(moduleInfo.CellID, 16).ToString() + ")");
                            }
                            catch
                            {
                                InfoAppendText("Unknown");
                            }
                        }

                        try
                        {
                            csq = int.Parse(moduleInfo.Csq);
                            InfoAppendText("\nQuality" + " (" + csq.ToString("D2") + "):    ");
                        }
                        catch
                        {
                            csq = 99;
                            InfoAppendText("\nQuality: Unknown");
                        }

                        switch (csq)
                        {
                            case 0:
                                rssi = -113;
                                break;

                            case 1:
                                rssi = -111;
                                break;

                            case 31:
                                rssi = -51;
                                break;

                            case 99:
                                rssi = -150;
                                break;

                            default:
                                rssi = 2 * csq - 113;
                                break;
                        }

                        if (rssi > (-60))
                            InfoAppendText(rssi.ToString() + "dBm (Excellent)", Color.Violet);
                        else if (rssi > (-70))
                            InfoAppendText(rssi.ToString() + "dBm (Good)", Color.Blue);
                        else if (rssi > (-80))
                            InfoAppendText(rssi.ToString() + "dBm (Normal)", Color.Green);
                        else if (rssi > (-90))
                            InfoAppendText(rssi.ToString() + "dBm (Low)", Color.Orange);
                        else if (rssi > (-100))
                            InfoAppendText(rssi.ToString() + "dBm (Very low)", Color.OrangeRed);
                        else
                            InfoAppendText(rssi.ToString() + "dBm (No signal)", Color.Red);

                        /*
                        if (int.Parse(moduleInfo.NetworkType) == 0) // 2G network
                        {
                            ber = int.Parse(moduleInfo.BitErrRate);
                            InfoAppendText("\nError rate " + "(" + ber.ToString("D2") + "): ");

                            switch (ber)
                            {
                                case 0:
                                    InfoAppendText("Less than 0.2%", Color.Blue);
                                    break;

                                case 1:
                                    InfoAppendText("0.2% to 0.4%", Color.Blue);
                                    break;

                                case 2:
                                    InfoAppendText("0.4% to 0.8%", Color.Green);
                                    break;

                                case 3:
                                    InfoAppendText("0.8% to 1.6%", Color.Green);
                                    break;

                                case 4:
                                    InfoAppendText("1.6% to 3.2%", Color.Orange);
                                    break;

                                case 5:
                                    InfoAppendText("3.2% to 6.4%", Color.Orange);
                                    break;

                                case 6:
                                    InfoAppendText("6.4% to 12.8%", Color.OrangeRed);
                                    break;

                                case 7:
                                    InfoAppendText("More than 12.8%", Color.OrangeRed);
                                    break;

                                default:
                                    InfoAppendText("Not known or not detectable", Color.Red);
                                    break;
                            }
                        }*/

                        InfoAppendText("\nRSRP:            ");

                        try
                        {
                            rsrp = int.Parse(moduleInfo.Rsrp);

                            if (rsrp >= -70)
                                InfoAppendText(moduleInfo.Rsrp + "dBm (Excellent)", Color.Violet);
                            else if (rsrp >= -80)
                                InfoAppendText(moduleInfo.Rsrp + "dBm (Good)", Color.Blue);
                            else if (rsrp >= -90)
                                InfoAppendText(moduleInfo.Rsrp + "dBm (Normal)", Color.Green);
                            else if (rsrp >= -100)
                                InfoAppendText(moduleInfo.Rsrp + "dBm (Low)", Color.Orange);
                            else if (rsrp >= -110)
                                InfoAppendText(moduleInfo.Rsrp + "dBm (Very low)", Color.OrangeRed);
                            else
                                InfoAppendText(moduleInfo.Rsrp + "dBm (No signal)", Color.Red);
                        }
                        catch
                        {
                            InfoAppendText("Unknown", Color.Red);
                        }

                        InfoAppendText("\nRSRQ:            ");

                        try
                        {
                            rsrq = int.Parse(moduleInfo.Rsrq);

                            if (rsrq >= -8)
                                InfoAppendText(moduleInfo.Rsrq + "dB (Excellent)", Color.Violet);
                            else if (rsrq > -10)
                                InfoAppendText(moduleInfo.Rsrq + "dB (Good)", Color.Blue);
                            else if (rsrq > -15)
                                InfoAppendText(moduleInfo.Rsrq + "dB (Normal)", Color.Green);
                            else if (rsrq > -18)
                                InfoAppendText(moduleInfo.Rsrq + "dB (Low)", Color.Orange);
                            else if (rsrq > -20)
                                InfoAppendText(moduleInfo.Rsrq + "dB (Very low)", Color.OrangeRed);
                            else
                                InfoAppendText(moduleInfo.Rsrq + "dB (No signal)", Color.Red);
                        }
                        catch
                        {
                            InfoAppendText("Unknown", Color.Red);
                        }

                        if ((lat > (double)0) || (lon > (double)0))
                        {
                            tmpStr = "\nLocation:        " + lat.ToString() + "," + lon.ToString();
                            InfoAppendText(tmpStr);
                        }

                        if (Thread_Mode == ThreadMode.RF_TEST)
                        {
                            DoNext = ThreadTask.GET_LOCATION;
                            WriteLogFile(rsrp, rsrq, rssi, lat, lon);
                            PlotData(rsrp, rsrq, rssi, TickStart++);

                            UInt32 t = Tick_DifMs(thisTick);

                            if ((t > 0) && (t < 1000))
                                Thread.Sleep(1000 - (int)t);

                            thisTick = Tick_Get();
                        }
                        else
                            DoNext++;
                        break;

                    case ThreadTask.CMD_CFG_HTTPS_HOST:
                        if (moduleInfo.Ip == null)
                        {
                            DoNext = ThreadTask.CLOSE_APP;
                            InfoAppendText("\n\nCan not access the internet", Color.Red);
                        }

                        if (downloadCxt.SslEn == true)
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPCFG=0,\"" + downloadCxt.Host + "\"," + downloadCxt.Port + ",0,,,1,30,1\r", 60000);
                            //tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPCFG=0,\"" + downloadCxt.Host + "\",443\r", 60000);
                        }
                        else
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPCFG=0,\"" + downloadCxt.Host + "\"," + downloadCxt.Port + ",0,,,0,30,1\r", 60000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                InfoAppendText("\n\nConnected to host " + downloadCxt.Host, Color.Blue);
                            }
                            else if (tmpStr.Contains("ERROR"))
                            {
                                DoNext = ThreadTask.CMD_CLOSE_SOCKET;
                                ToDo = ThreadTask.CMD_RF_OFF;
                                InfoAppendText("\n\nCan not connect to host " + downloadCxt.Host, Color.Red);
                            }
                        }
                        break;

                    case ThreadTask.CMD_CFG_HTTPS_FILE:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPQRY=0,0,\"" + downloadCxt.FilePath + "\"\r", 60000);

                        if (tmpStr != null)
                        {
                            //PrintRxDebug("\n--> " + tmpStr);

                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext = ThreadTask.HTTPS_GET_FILE_INFO;
                                InfoAppendText("\n\nGet info of file " + downloadCxt.FileName, Color.Blue);
                            }
                            else
                            {
                                DoNext++;
                                InfoAppendText("\n\nCan not get info of file " + downloadCxt.FileName, Color.Red);
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CURRENT_IP:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGCONTRDP\r", 10000);

                        if (tmpStr != null)
                        {
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_GET_CURRENT_DNS:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#NWDNS=\r", 10000);

                        if (tmpStr != null)
                        {
                            DoNext = ThreadTask.CMD_CLOSE_SOCKET;
                            ToDo = ThreadTask.CMD_RF_OFF;
                        }
                        break;

                    case ThreadTask.HTTPS_GET_FILE_INFO:
                        tmpStr = Get_ExtResp(ref cmdCxt, null, 60000);

                        if (tmpStr != null)
                        {
                            DoNext = ThreadTask.CMD_CLOSE_SOCKET;
                            ToDo = ThreadTask.CMD_RF_OFF;

                            if (tmpStr.Contains("#HTTPRING:"))
                            {
                                tmpStr = new string(SubArray(cmdCxt.buffer, "\",", "\r\n", true));
                                ReplaceStr(ref tmpStr, '\r', '\0');
                                ReplaceStr(ref tmpStr, '\n', '\0');

                                if (tmpStr != null)
                                {
                                    try
                                    {
                                        downloadCxt.FileSize = int.Parse(tmpStr);
                                    }
                                    catch
                                    {
                                        downloadCxt.FileSize = 0;
                                    }
                                }

                                if (downloadCxt.FileSize == 0)
                                {
                                    downloadCxt.sW = null;
                                    InfoAppendText("\nFile size = 0, error", Color.Red); ;
                                }
                                else
                                {
                                    DoNext = ThreadTask.CMD_GET_HTTPS_1500;
                                    DownloadTime = 0;// Tick_Get();
                                    downloadCxt.DownloadedSize = 0;
                                    ProgressBar_Update(downloadCxt.FileSize, downloadCxt.DownloadedSize);
                                    //downloadCxt.sW = new StreamWriter(downloadCxt.FileName);
                                    var stream = File.Open(downloadCxt.FileName, FileMode.Create);
                                    downloadCxt.sW = new BinaryWriter(stream);
                                    InfoAppendText("\nFile size = " + downloadCxt.FileSize + " byte(s)", Color.Blue);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_HTTPS_1500:
                        int dlsize = downloadCxt.FileSize - downloadCxt.DownloadedSize;

                        if (dlsize > 1500)
                            dlsize = 1500;

                        tmpStr = SendCmd_GetData(ref cmdCxt, "AT#HTTPRCV=0,1500\r", 60000, 3000, dlsize, false);

                        if (tmpStr != null)
                        {
                            tmpArr = HttpRcv_GetData(cmdCxt.buffer, cmdCxt.len);
                            DownloadTime += cmdCxt.proctime;

                            if (tmpArr != null)
                            {
                                byte[] data = new byte[tmpArr.Length];

                                for (int i = 0; i < tmpArr.Length; i++)
                                    data[i] = Convert.ToByte(tmpArr[i]);

                                downloadCxt.sW.Write(data);
                                downloadCxt.sW.Flush();
                                downloadCxt.DownloadedSize += tmpArr.Length;
                                ProgressBar_Update(downloadCxt.FileSize, downloadCxt.DownloadedSize);

                                if (downloadCxt.DownloadedSize >= downloadCxt.FileSize)
                                {
                                    DoNext = ThreadTask.CMD_CLOSE_SOCKET;
                                    ToDo = ThreadTask.CLOSE_APP;
                                    downloadCxt.sW.Close();
                                    downloadCxt.sW = null;

                                    double speed = (((double)downloadCxt.FileSize * 1000.0f / (double)DownloadTime) * 8.0f) / 1024.0f;

                                    InfoAppendText("\n\nDownload complete: " + DownloadTime.ToString() + "(ms)", Color.Green);
                                    InfoAppendText("\nSpeed: " + speed.ToString("0.00") + "(kbps)", Color.Green);

                                    MD5 md5 = MD5.Create();
                                    var stream = File.OpenRead(downloadCxt.FileName);
                                    var hash = md5.ComputeHash(stream);
                                    stream.Close();

                                    string md5_result = BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();

                                    if (md5_result == downloadCxt.Md5.ToUpperInvariant())
                                    {
                                        md5_result = "MD5: " + md5_result + " correct";
                                        InfoAppendText("\n" + md5_result, Color.Green);
                                    }
                                    else
                                    {
                                        md5_result = "MD5: " + md5_result + " incorrect";
                                        InfoAppendText("\n" + md5_result, Color.Red);
                                    }

                                    MessageBox.Show(md5_result, "Info");
                                }
                            }
                            else if (tmpStr.Contains("ERROR") || tmpStr.Contains("RX TIMEOUT"))
                            {
                                DoNext = ThreadTask.CMD_CLOSE_SOCKET;
                                ToDo = ThreadTask.CMD_RF_OFF;
                                InfoAppendText("\n\nDownload fail", Color.Red);
                            }
                        }
                        break;

                    case ThreadTask.CMD_CLOSE_SOCKET:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SH=1\r", 3000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext = ToDo;
                            else
                                DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_RF_OFF:
                        DoNext++;
                        /*
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CFUN=4\r", 30000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                            else
                                DoNext = ThreadTask.CMD_MODULE_REBOOT;
                        }
                        */
                        break;

                    case ThreadTask.CMD_RF_ON:
                        DoNext = ThreadTask.CLOSE_APP;
                        /*
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CFUN=1\r", 30000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext = ThreadTask.CLOSE_APP;
                            else
                                DoNext = ThreadTask.CMD_MODULE_REBOOT;
                        }
                        */
                        break;

                    case ThreadTask.CMD_MODULE_REBOOT:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#REBOOT\r", 3000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                PrintDebug("\n\nModule is rebooting...\n\n");
                                InfoAppendText("\n\nModule is rebooting...\n\n");
                            }
                            else
                            {
                                PrintDebug("\n\nCan not reboot\n\n", Color.Red);
                                InfoAppendText("\n\nCan not reboot\n\n", Color.Red);
                            }

                            DoNext++;
                        }
                        break;

                    case ThreadTask.CLOSE_APP:
                    default:
                        if (downloadCxt.sW != null)
                            downloadCxt.sW.Close();

                        Thread_Enbale = 0;
                        break;
                }
            }

            serialPort1.DtrEnable = false;
            serialPort1.RtsEnable = false;
            serialPort1.Close();
            serialPort1.Dispose();

            if (Thread_Mode == ThreadMode.RF_TEST)
                bt_RFTest_Update(null);
            else
                bt_Download_Update(null);

            Thread_Task.Abort();
            Thread_Task.Interrupt();
            Thread_Task.Abort();
            Thread_Task.Join();
        }

        private void rtb_Log_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sW != null)
                OpenLogFile();

            rtb_Log.Clear();
        }

        private void bt_Download_Click(object sender, EventArgs e)
        {
            try
            {
                if (bt_Download.Text == "Download")
                {
                    tabCtrl1.SelectedTab = tabCtrl1.TabPages["tabLog"];
                    Thread_Mode = ThreadMode.DOWNLOAD;
                    Thread_Enbale = 1;
                    bt_RFTest.Enabled = false;
                    bt_Scan.Enabled = false;
                    cb_Apn.Enabled = false;
                    cb_Dns.Enabled = false;
                    cb_Url.Enabled = false;
                    tb_Md5.Enabled = false;
                    pgb_Percent.Value = 0;
                    lb_Percent.Text = "0 byte";

                    Thread_Task = new Thread(() => Thread_Tasks()); // Create new app tasks
                    Thread_Task.Start();

                    bt_Download.Text = "Stop";
                }
                else
                {
                    Thread_Enbale = 0;
                }
            }
            catch
            {

            }
        }

        private void cb_ViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_ViewMode.Text == "Scroll")
                viewMode = true;
            else
                viewMode = false;
        }

        private void cb_Port1_SelectedIndexChanged(object sender, EventArgs e)
        {
            portName = cb_Port1.Text;
        }

        private void LoadUrl(bool online)
        {
            Uri myUri;
            bool result = Uri.TryCreate(cb_Url.Text, UriKind.Absolute, out myUri);

            if (result)
            {
                if (myUri.Scheme == Uri.UriSchemeHttps)
                {
                    downloadCxt.SslEn = true;
                    //downloadCxt.Port = "443";
                    downloadCxt.Port = myUri.Port.ToString();
                }
                else if (myUri.Scheme == Uri.UriSchemeHttp)
                {
                    downloadCxt.SslEn = false;
                    downloadCxt.Port = myUri.Port.ToString();
                }
                else
                {
                    MessageBox.Show("Invalid URL", "Error");
                    return;
                }

                //MessageBox.Show("Port: " + downloadCxt.Port, "Info");
                downloadCxt.Host = myUri.Host;
                downloadCxt.FilePath = myUri.AbsolutePath;
                downloadCxt.FileName = Path.GetFileName(downloadCxt.FilePath);

                if (downloadCxt.FileName == null)
                {
                    MessageBox.Show("No file name", "Error");
                    return;
                }

                if (downloadCxt.FilePath == null)
                {
                    MessageBox.Show("No file path", "Error");
                    return;
                }

                if (online)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile(cb_Url.Text, "Raw_" + downloadCxt.FileName);

                        if (File.Exists("Raw_" + downloadCxt.FileName))
                        {
                            MD5 md5 = MD5.Create();
                            var stream = File.OpenRead("Raw_" + downloadCxt.FileName);
                            var hash = md5.ComputeHash(stream);
                            stream.Close();

                            string md5_result = BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
                            tb_Md5.Text = md5_result;
                            MessageBox.Show("MD5: " + md5_result, "Info");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Can not calculate MD5", "Info");
                    }
                }
            }
            else
                MessageBox.Show("Invalid URL", "Error");
        }

        private void cb_Url_TextChanged(object sender, EventArgs e)
        {
            LoadUrl(true);
        }

        private void tb_Md5_TextChanged(object sender, EventArgs e)
        {
            downloadCxt.Md5 = tb_Md5.Text;
        }

        private void cb_Port1_TextChanged(object sender, EventArgs e)
        {
            if (cb_Port1.Text != "Empty")
            {
                bt_Download.Enabled = true;
                bt_Reboot.Enabled = true;
                bt_RFTest.Enabled = true;
            }
            else
            {
                bt_Download.Enabled = false;
                bt_Reboot.Enabled = false;
                bt_RFTest.Enabled = false;
            }
        }

        private void ckb_DebugEn_CheckedChanged(object sender, EventArgs e)
        {
            debugEn = ckb_DebugEn.Checked;
        }

        private void cb_Apn_TextChanged(object sender, EventArgs e)
        {
            moduleInfo.Apn = cb_Apn.Text;
        }

        private void cb_Dns_TextChanged(object sender, EventArgs e)
        {
            moduleInfo.UserDns = cb_Dns.Text;
        }

        private void bt_Reboot_Click(object sender, EventArgs e)
        {
            bt_Download.Enabled = false;
            bt_Reboot.Enabled = false;
            bt_Scan.Enabled = false;
            cb_Apn.Enabled = false;
            cb_Dns.Enabled = false;

            if (TestATPort(cb_Port1.Text, "AT#REBOOT\r"))
            {
                PrintDebug("\n\nModule is rebooting...\n\n");
                InfoAppendText("\n\nModule is rebooting...\n\n");
                tryCount = 0;
                timer1.Interval = 5000;
                timer1.Start();
            }
            else
            {
                bt_Download.Enabled = true;
                bt_Scan.Enabled = true;
                cb_Apn.Enabled = true;
                cb_Dns.Enabled = true;
                PrintDebug("\n\nCan not reboot\n\n", Color.Red);
                InfoAppendText("\n\nCan not reboot\n\n", Color.Red);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tryCount += 5;

            if (TestATPort(cb_Port1.Text, "ATE0\r"))
            {
                timer1.Stop();
                bt_Reboot.Enabled = true;
                bt_Download.Enabled = true;
                bt_Scan.Enabled = true;
                cb_Apn.Enabled = true;
                cb_Dns.Enabled = true;
                PrintDebug(" Done\n");
                InfoAppendText("Done\n");
            }
            else if(tryCount>=60)
            {
                timer1.Stop();
                bt_Reboot.Enabled = true;
                bt_Download.Enabled = true;
                bt_Scan.Enabled = true;
                cb_Apn.Enabled = true;
                cb_Dns.Enabled = true;
                Scan_AT_Port();
                PrintDebug("Failed\n", Color.Red);
                InfoAppendText("Failed\n", Color.Red);
            }
        }
    }
}
