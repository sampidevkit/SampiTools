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

        private struct CmdCxt
        {
            public char[] buffer;
            public int len;
            public int findIdx;
            public int donext;
            public UInt32 tick;
        }

        private struct DownloadCxt
        {
            public string Host;
            public string FilePath;
            public string FileName;
            public string Md5;
            public int FileSize;
            public int DownloadedSize;
            public StreamWriter sW;
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
            public string Apn;
            public string Ip;
        }

        private DownloadCxt downloadCxt = new DownloadCxt
        {
            Host = null,
            FilePath = null,
            FileName = null,
            Md5 = null,
            FileSize = 0,
            DownloadedSize=0,
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
            Ip = null
        };

        private volatile bool viewMode = true;
        private static Thread Thread_Task = null;
        private volatile ThreadMode Thread_Mode = ThreadMode.RF_TEST;
        private volatile int Thread_Enbale = 0;
        private volatile int DoNext = 0;
        private StreamWriter sW = null;
        private string logfileName = null;
        private long line = 0;
        private UInt32 TickStart = 0;
        private volatile string portName = null;

        private string[] UsbPid = new string[3]
        { 
        /* xE866 */ "VID_1BC7&PID_0021", 
        /* MEx10G1 */ "VID_1BC7&PID_110A",
        /* LE910 */ "VID_1BC7&PID_1201"
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
            char[] pStrSample=StrSample.ToCharArray();

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

            PrintDebug(msg, Color.Black);
        }

        private void PrintTxDebug(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PrintTxDebug), new object[] { msg });
                return;
            }

            PrintDebug(msg, Color.Blue);
        }

        private void PrintRxDebug(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PrintRxDebug), new object[] { msg });
                return;
            }

            PrintDebug(msg, Color.Red);
        }

        private string FileNameGenerate(string prefix)
        {
            string s = RemoveInvalidName(prefix + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString());

            return s;
        }

        private void WriteLogFile(int rsrp, int rsrq, int rssi)
        {
            if (sW != null)
            {
                line++;

                string s = line.ToString() + " " + rsrp.ToString() + " " + rsrq.ToString() + " " + rssi.ToString();

                sW.WriteLine(s);
                PrintDebug("\n" + line.ToString("D4") + " ");
                PrintDebug(DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString() + ": ");
                PrintDebug("RSRP=" + rsrp.ToString() + "dBm, RSRQ=" + rsrq.ToString() + "dB, RSSI=" + rssi.ToString() + "dB");
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
            catch (Exception e)
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

        private bool TestATPort(string portName)
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
                    resp = SendCmd_GetRes(ref cmdCxt, "ATE0\r");

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
            string[] ports;
            int found = 0;
            string port = null;
            string portlist = null;

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

                                    if (TestATPort(port))
                                    {
                                        found++;
                                        cb_Port1.Items.Add(port);
                                    }
                                }
                                else if (!portlist.Contains(port))
                                {
                                    portlist += port;
                                    //PrintDebug("\nFound: " + port);

                                    if (TestATPort(port))
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

                            if (TestATPort(port))
                            {
                                found++;
                                cb_Port1.Items.Add(port);
                            }
                        }
                        else if (!portlist.Contains(port))
                        {
                            portlist += port;
                            //PrintDebug("\nFound: " + port);

                            if (TestATPort(port))
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
            catch (Exception e)
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
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text += " v." + $"{version}";
            this.Update();

            downloadCxt.Md5 = tb_Md5.Text;
            moduleInfo.Apn = tb_Apn.Text;

            Uri myUri;
            bool result = Uri.TryCreate(tb_Url.Text, UriKind.Absolute, out myUri) && myUri.Scheme == Uri.UriSchemeHttps;

            if (result)
            {
                downloadCxt.Host = myUri.Host;
                downloadCxt.FilePath = myUri.AbsolutePath;
                downloadCxt.FileName = Path.GetFileName(downloadCxt.FilePath);

                if (downloadCxt.FileName == null)
                    MessageBox.Show("No file name", "Error");

                if (downloadCxt.FilePath == null)
                    MessageBox.Show("No file path", "Error");
            }
            else
                MessageBox.Show("Invalid HTTPS URL", "Error");

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
                this.Invoke(new Action<string>(bt_RFTest_Update), new object[] { msg  });
                return;
            }

            CloseLogFile();
            bt_Download.Enabled = true;
            bt_Scan.Enabled = true;
            tb_Apn.Enabled = true;
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
            tb_Apn.Enabled = true;
            tb_Url.Enabled = true;
            tb_Md5.Enabled = true;
            bt_Download.Text = "Download";
        }

        private void bt_RFTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (bt_RFTest.Text == "RF Test")
                {
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
                    bt_Scan.Enabled = false;
                    tb_Apn.Enabled = false;
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
            if ((c >= 0x21) && (c <= 0x7E))
                return true;

            return false;
        }

        private int Get_1stIndex(char[] buffer, string para, HeadTail head_tail)
        {
            int i, j;
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
            if ((headIdx >= 0) && (tailIdx >= 0) && (headIdx < tailIdx))
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
            int head = Get_1stIndex(buffer, str_head, HeadTail.tail) + 1;
            int tail = Get_1stIndex(buffer, str_tail, HeadTail.head) - 1;

            //PrintDebug("\n--> head " + head.ToString()+ "tail " + tail.ToString());

            return SubArray(buffer, head, tail);
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
                head = Get_1stIndex(buffer, str_head, HeadTail.tail) + 1;
                tail = Get_1stIndex(buffer, str_tail, HeadTail.head) - 1;
            }

            //PrintDebug("\n--> head " + head.ToString() + "tail " + tail.ToString());

            return SubArray(buffer, head, tail);
        }

        private string SendCmd_GetRes(ref CmdCxt cmdCxt, string cmd, UInt32 timeout, bool AT_Track)
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

                                /*
                                PrintRxDebug("\nIdx: " + cmdCxt.findIdx.ToString() + ", ");

                                if (isPrintable(c))
                                    PrintRxDebug(c.ToString());
                                else
                                    PrintRxDebug("<" + Convert.ToByte(c).ToString("X2") + ">");
                                */
                            }

                            cmdCxt.tick = Tick_Get();
                        }
                        else if (Tick_IsOverMs(ref cmdCxt.tick, 100))
                            cmdCxt.donext++;
                        break;

                    default:
                        char[] chr = new char[cmdCxt.len];

                        for (int i = 0; i < cmdCxt.len; i++)
                            chr[i] = cmdCxt.buffer[i];

                        cmdCxt.buffer = chr;
                        cmdCxt.donext = 0;

                        return new string(chr);
                }
            }
            catch(Exception ex) 
            {
                //PrintDebug(ex.ToString());
            }

            return null;
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

                                if (isPrintable(c))
                                    PrintRxDebug(c.ToString());
                                else
                                    PrintRxDebug("<" + Convert.ToByte(c).ToString("X2") + ">");
                                
                            }

                            cmdCxt.tick = Tick_Get();
                        }
                        else if (Tick_IsOverMs(ref cmdCxt.tick, 100))
                            cmdCxt.donext++;
                        break;

                    default:
                        char[] chr = new char[cmdCxt.len];

                        for (int i = 0; i < cmdCxt.len; i++)
                            chr[i] = cmdCxt.buffer[i];

                        cmdCxt.buffer = chr;
                        cmdCxt.donext = 0;

                        return new string(chr);
                }
            }
            catch (Exception ex)
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
            int head = Get_1stIndex(arr, "\r\nOK\r\n", HeadTail.head);

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

            if ((curve1 == null)|| (curve2 == null)|| (curve3 == null))
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
            int startIdx = Get_1stIndex(datain, "<<<:", HeadTail.tail) + 1;

            if (startIdx < 0)
                return null;

            if ((datain[lenin - 6] != '\r')|| (datain[lenin - 5] != '\n'))
                return null;

            if ((datain[lenin - 4] != 'O') || (datain[lenin - 3] != 'K'))
                return null;

            if ((datain[lenin - 2] != '\r') || (datain[lenin - 1] != '\n'))
                return null;

            char[] dataout = new char[lenin - 6 - startIdx];

            for (int i = 0; i < dataout.Length; i++)
                dataout[i] = datain[startIdx + i];

            return dataout;
        }

        private void Thread_Tasks()
        {
            int csq = 99;
            int ber = 99;
            int rssi = -150;
            int rsrp = -150;
            int rsrq = -150;
            string tmpStr = null;
            char[] tmpArr = null;

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

            serialPort1.PortName = portName;
            serialPort1.BaudRate = 115200;
            serialPort1.WriteTimeout = 10;
            serialPort1.DtrEnable = true;
            serialPort1.RtsEnable = true;
            serialPort1.Open();

            DoNext = 0;
            InfoWriteText("Reading module info...");

            UInt32 thisTick = Tick_Get();

            while (Thread_Enbale == 1)
            {
                switch (DoNext)
                {
                    case 0:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "ATI4\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                moduleInfo.Name = RemoveAT(tmpStr);
                            }
                        }
                        break;

                    case 1:
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

                    case 2:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#CIMI\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "#CIMI: ", "\r\nOK"));
                                moduleInfo.Cimi = RemoveAT(tmpStr);
                            }
                            else if (tmpStr.Contains("\r\nERROR\r\n"))
                                DoNext++;
                        }
                        break;

                    case 3:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CCID\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext+=2;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "+CCID: ", "\r\nOK"));
                                moduleInfo.Ccid = RemoveAT(tmpStr);
                            }
                            else if (tmpStr.Contains("\r\nERROR\r\n"))
                                DoNext++;
                        }
                        break;

                    case 4:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#CCID\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "#CCID: ", "\r\nOK"));
                                moduleInfo.Ccid = RemoveAT(tmpStr);
                            }
                            else
                                DoNext--;
                        }
                        break;

                    case 5:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+COPS?\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                TickStart = 0;
                                moduleInfo.Operator = new string(SubArray(cmdCxt.buffer, ",\"", "\","));
                                moduleInfo.NetworkType = new string(SubArray(cmdCxt.buffer, "\",", "\r\nOK"));
                                ReplaceStr(ref moduleInfo.NetworkType, '\r', '\0');
                                ReplaceStr(ref moduleInfo.NetworkType, '\n', '\0');
                            }
                        }
                        break;

                    case 6:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SGACT=1,0\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                        }
                        break;

                    case 7:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGDCONT=1,\"IP\",\"" + moduleInfo.Apn + "\"\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                        }
                        break;

                    case 8:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SGACT=1,1\r", 10000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                tmpStr = new string(SubArray(cmdCxt.buffer, "#SGACT: ", "\r\nOK"));
                                moduleInfo.Ip = RemoveAT(tmpStr);
                            }
                        }
                        break;

                    case 9:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#MONI=0\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                thisTick = Tick_Get();
                            }
                        }
                        break;

                    case 10:
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

                    case 11:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#MONI\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                moduleInfo.Rsrp = new string(SubArray(cmdCxt.buffer, "RSRP:", " RSRQ:"));
                                moduleInfo.Rsrq = new string(SubArray(cmdCxt.buffer, "RSRQ:", " TAC:"));
                                moduleInfo.Tac = new string(SubArray(cmdCxt.buffer, "TAC:", " Id:"));
                                moduleInfo.CellID = new string(SubArray(cmdCxt.buffer, " Id:", " EARFCN:"));
                            }
                        }
                        break;

                    case 12:
                        InfoWriteText("Module:          " + moduleInfo.Name);
                        InfoAppendText("\nIMEI:            " + moduleInfo.Imei);
                        InfoAppendText("\nCIMI:            " + moduleInfo.Cimi);
                        InfoAppendText("\nCCID:            " + moduleInfo.Ccid);
                        InfoAppendText("\nOperator:        " + moduleInfo.Operator);
                        InfoAppendText("\nNetwork " + "(" + moduleInfo.NetworkType + "):     ");

                        switch (int.Parse(moduleInfo.NetworkType))
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
                                InfoAppendText("GSM w/EGPRS");
                                break;

                            case 4:
                                InfoAppendText("UTRAN w/HSDPA");
                                break;

                            case 5:
                                InfoAppendText("UTRAN w/HSUPA");
                                break;

                            case 6:
                                InfoAppendText("UTRAN w/HSDPA and HSUPA");
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

                        InfoAppendText("\nIP:              " + moduleInfo.Ip);
                        InfoAppendText("\nTAC(LAC):        " + moduleInfo.Tac + " (" + Convert.ToUInt32(moduleInfo.Tac, 16).ToString() + ")");
                        InfoAppendText("\nCell ID:         " + moduleInfo.CellID + " (" + Convert.ToUInt32(moduleInfo.CellID, 16).ToString() + ")");
                        csq = int.Parse(moduleInfo.Csq);
                        InfoAppendText("\nQuality" + " (" + csq.ToString("D2") + "):    ");

                        switch(csq)
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
                            InfoAppendText(rssi.ToString() + "dBm (Excellent)", Color.Blue);
                        else if (rssi > (-70))
                            InfoAppendText(rssi.ToString() + "dBm (Good)", Color.Green);
                        else if (rssi > (-80))
                            InfoAppendText(rssi.ToString() + "dBm (Low)", Color.Green);
                        else if (rssi > (-90))
                            InfoAppendText(rssi.ToString() + "dBm (Very low)", Color.OrangeRed);
                        else
                            InfoAppendText(rssi.ToString() + "dBm (No signal)", Color.Red);

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

                        rsrp = int.Parse(moduleInfo.Rsrp);
                        InfoAppendText("\nRSRP:            ");

                        if (rsrp >= -80)
                            InfoAppendText(moduleInfo.Rsrp, Color.Blue);
                        else if (rsrp >= -90)
                            InfoAppendText(moduleInfo.Rsrp, Color.Green);
                        else if (rsrp >= -100)
                            InfoAppendText(moduleInfo.Rsrp, Color.Orange);
                        else if (rsrp >= -110)
                            InfoAppendText(moduleInfo.Rsrp, Color.OrangeRed);
                        else
                            InfoAppendText(moduleInfo.Rsrp, Color.Red);

                        rsrq = int.Parse(moduleInfo.Rsrq);
                        InfoAppendText("\nRSRQ:            ");

                        if (rsrq >= -10)
                            InfoAppendText(moduleInfo.Rsrq, Color.Blue);
                        else if (rsrq > -15)
                            InfoAppendText(moduleInfo.Rsrq, Color.Green);
                        else if (rsrq > -18)
                            InfoAppendText(moduleInfo.Rsrq, Color.Orange);
                        else if (rsrq > -20)
                            InfoAppendText(moduleInfo.Rsrq, Color.OrangeRed);
                        else
                            InfoAppendText(moduleInfo.Rsrq, Color.Red);

                        if (Thread_Mode == ThreadMode.RF_TEST)
                        {
                            DoNext = 10;
                            WriteLogFile(rsrp, rsrq, rssi);
                            PlotData(rsrp, rsrq, rssi, TickStart++);

                            UInt32 t = Tick_DifMs(thisTick);

                            if ((t > 0) && (t < 1000))
                                Thread.Sleep(1000 - (int)t);

                            thisTick = Tick_Get();
                        }
                        else
                            DoNext++;
                        break;

                    case 13:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPCFG=0,\"" + downloadCxt.Host + "\",443,0,,,1,120,1\r", 5000);

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                            else if (tmpStr.Contains("\r\nERROR\r\n"))
                            {
                                DoNext = 17;
                                InfoAppendText("\n\nCan not connect to host " + downloadCxt.Host, Color.Red);
                            }
                        }
                        break;

                    case 14:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPQRY=0,0,\"" + downloadCxt.FilePath + "\"\r", 30000);

                        if (tmpStr != null)
                        {
                            //PrintRxDebug("\n--> " + tmpStr);

                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                            else
                            {
                                DoNext = 17;
                                InfoAppendText("\n\nCan not download file " + downloadCxt.FileName, Color.Red);
                            }
                        }
                        break;

                    case 15:
                        tmpStr = Get_ExtResp(ref cmdCxt, null, 30000);

                        if (tmpStr != null)
                        {
                            DoNext++;

                            if (tmpStr.Contains("#HTTPRING:"))
                            {
                                tmpStr = new string(SubArray(cmdCxt.buffer, "\",", "\r\n", true));
                                ReplaceStr(ref tmpStr, '\r', '\0');
                                ReplaceStr(ref tmpStr, '\n', '\0');

                                if (tmpStr != null)
                                {
                                    PrintDebug("\n\nFound: " + tmpStr);

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
                                    DoNext++;
                                    downloadCxt.sW = null;
                                    InfoAppendText("\n\nFile size = 0, error", Color.Red); ;
                                }
                                else
                                {
                                    downloadCxt.DownloadedSize = 0;
                                    ProgressBar_Update(downloadCxt.FileSize, downloadCxt.DownloadedSize);
                                    downloadCxt.sW = new StreamWriter(downloadCxt.FileName);
                                    InfoAppendText("\n\nFile size = " + downloadCxt.FileSize + " byte(s)", Color.Blue);
                                }
                            }
                        }
                        break;

                    case 16:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPRCV=1500\r", 30000, false);

                        if (tmpStr != null)
                        {
                            tmpArr = HttpRcv_GetData(cmdCxt.buffer, cmdCxt.len);

                            if (tmpArr != null)
                            {
                                downloadCxt.sW.WriteLine(new String(tmpArr));
                                downloadCxt.DownloadedSize += tmpArr.Length;
                                ProgressBar_Update(downloadCxt.FileSize, downloadCxt.DownloadedSize);

                                if (downloadCxt.DownloadedSize >= downloadCxt.FileSize)
                                {
                                    DoNext++;
                                    InfoAppendText("\n\nDownload complete", Color.Green);
                                }
                            }
                            else if (tmpStr.Contains("\r\nERROR\r\n"))
                            {
                                DoNext++;
                                InfoAppendText("\n\nDownload fail", Color.Red);
                            }
                        }
                        break;

                    case 17:
                        tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SH=1\r");

                        if (tmpStr != null)
                        {
                            if (tmpStr.Contains("\r\nOK\r\n"))
                                DoNext++;
                        }
                        break;

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
                    Thread_Mode = ThreadMode.DOWNLOAD;
                    Thread_Enbale = 1;
                    bt_RFTest.Enabled = false;
                    bt_Scan.Enabled = false;
                    tb_Apn.Enabled = false;
                    tb_Url.Enabled = false;
                    tb_Md5.Enabled = false;

                    Thread_Task = new Thread(() => Thread_Tasks()); // Create new app tasks
                    Thread_Task.Start();

                    bt_Download.Text = "Stop";
                }
                else
                {
                    Thread_Enbale = 0;
                }
            }
            catch (Exception ex)
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

        private void tb_Url_TextChanged(object sender, EventArgs e)
        {
            Uri myUri;
            bool result = Uri.TryCreate(tb_Url.Text, UriKind.Absolute, out myUri) && myUri.Scheme == Uri.UriSchemeHttps;

            if (result)
            {
                downloadCxt.Host = myUri.Host;
                downloadCxt.FilePath = myUri.AbsolutePath;
                downloadCxt.FileName = Path.GetFileName(downloadCxt.FilePath);

                if (downloadCxt.FileName == null)
                    MessageBox.Show("No file name", "Error");

                if (downloadCxt.FilePath == null)
                    MessageBox.Show("No file path", "Error");
            }
            else
                MessageBox.Show("Invalid HTTPS URL", "Error");
        }

        private void tb_Apn_TextChanged(object sender, EventArgs e)
        {
            moduleInfo.Apn=tb_Apn.Text;
        }

        private void tb_Md5_TextChanged(object sender, EventArgs e)
        {
            downloadCxt.Md5 = tb_Md5.Text;
        }
    }
}
