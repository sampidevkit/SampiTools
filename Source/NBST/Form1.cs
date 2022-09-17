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

namespace NBST
{
    public partial class Form1 : Form
    {
        private enum HeadTail
        {
            head,
            tail
        }

        private struct CmdCxt
        {
            public char[] buffer;
            public int len;
            public int donext;
            public long tick;
        }

        private PointPairList pointPairList_RSRP = null;
        private LineItem lineItem_RSRP = null;

        private PointPairList pointPairList_RSRQ = null;
        private LineItem lineItem_RSRQ = null;

        private PointPairList pointPairList_RSSI = null;
        private LineItem lineItem_RSSI = null;

        private volatile bool viewMode = true;

        private GraphPane graphPane = null;
        private static Thread Thread_Task = null;
        private volatile int DoNext = 0;
        private string ModuleName = null;
        private string ModuleImei = null;
        private StreamWriter sW = null;
        private string fileName = null;
        private long line = 0;
        private long TickStart = 0;
        private string[] UsbPid = new string[3] { 
        /* xE866 */ "VID_1BC7&PID_0021", 
        /* MEx10G1 */ "VID_1BC7&PID_110A",
        /* LE910 */ "VID_1BC7&PID_1201"};

        private long Tick_Get()
        {
            return DateTime.Now.Ticks;
        }

        private long Tick_DifUs(long tk)
        {
            return (long)((UInt64)(DateTime.Now.Ticks - tk));
        }

        private long Tick_DifMs(long tk)
        {
            return (long)((UInt64)(DateTime.Now.Ticks - tk) / 1000);
        }

        private bool Tick_IsOverUs(ref long tk, long ms)
        {
            long dif = (long)((UInt64)(DateTime.Now.Ticks - tk));

            if (dif > ms)
            {
                tk = Tick_Get();
                return true;
            }

            return false;
        }

        private bool Tick_IsOverMs(ref long tk, long ms)
        {
            long dif = (long)((UInt64)(DateTime.Now.Ticks - tk) / 1000);

            if(dif > ms)
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

        private void RemoveStr(ref string Str, char oldChar, char newChar)
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
                StreamWriter slog = new StreamWriter(fileName + ".txt");

                slog.WriteLine(rtb_Log.Text);
                slog.Close();
                sW.WriteLine(NBST.Properties.Resources.footer);

                sW.Close();
                PrintDebug("\nLog file " + fileName + ".m has been saved");
                Thread.Sleep(1000);
                //rtb_Log.Clear();
                sW = null;
            }
        }

        private void OpenLogFile()
        {
            CloseLogFile();
            fileName = FileNameGenerate("NBST_");

            try
            {
                sW = new StreamWriter(fileName + ".m");
                sW.WriteLine("%{");
                sW.WriteLine(rtb_Info.Text);
                sW.WriteLine("%}");
                sW.WriteLine(NBST.Properties.Resources.header);
                sW.WriteLine("\nstartTime=" + ToUnixTimeSeconds().ToString() + ";");
                sW.WriteLine("\na=[");

                rtb_Log.Text = "New log file " + fileName + ".m has been created";
                line = 0;
            }
            catch (Exception e)
            {
                PrintDebug("\nFile name \"" + fileName + "\"error");
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
            long tick = Tick_Get();

            CmdCxt cmdCxt = new CmdCxt();
            cmdCxt.buffer = new char[1024];
            cmdCxt.donext = 0;

            try
            {
                if (serialPort1.IsOpen)
                    return false;

                serialPort1.PortName = portName;
                serialPort1.BaudRate = 115200;
                serialPort1.WriteTimeout = 1000;
                serialPort1.DtrEnable = true;
                serialPort1.Open();
                PrintDebug("\nOpen " + portName);

                do
                {
                    resp = SendCmd_GetRes(ref cmdCxt, "ATE0\r");

                    if (resp != null)
                    {
                        if (resp.Contains("\r\nOK\r\n"))
                        {
                            loop = 5;
                            found = true;
                        }
                    }
                    else if (Tick_IsOverMs(ref tick, 500))
                    {
                        loop++;
                        PrintDebug("\nRX Timeout");
                    }
                }
                while (loop < 5);
            }
            catch
            { 
                PrintDebug("\nTX Timeout");
            }

            serialPort1.DtrEnable = false;
            serialPort1.Close();
            serialPort1.Dispose();

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
            TickStart = Tick_Get();
            graphPane = zedGraph1.GraphPane;
            zedGraph1.GraphPane.CurveList.Clear();
            zedGraph1.GraphPane.YAxisList.Clear();
            zedGraph1.GraphPane.Title.Text = "RF Measurement";
            zedGraph1.GraphPane.XAxis.Title.Text = "Time";

            lineItem_RSRP = graphPane.AddCurve("RSRP, ", pointPairList_RSRP, Color.Red, SymbolType.Circle);
            zedGraph1.GraphPane.AddYAxis("dBm");
            zedGraph1.GraphPane.YAxisList[0].Scale.Max = 0;
            zedGraph1.GraphPane.YAxisList[0].Scale.Min = -150;
            zedGraph1.GraphPane.YAxisList[0].Scale.FontSpec.FontColor = Color.Red;
            zedGraph1.GraphPane.YAxisList[0].Title.FontSpec.FontColor = Color.Red;
            zedGraph1.GraphPane.YAxisList[0].Color = Color.Red;

            lineItem_RSRQ = graphPane.AddCurve("RSRQ, ", pointPairList_RSRQ, Color.Green, SymbolType.Triangle);
            zedGraph1.GraphPane.AddYAxis("dB");
            zedGraph1.GraphPane.YAxisList[1].Scale.Max = 0;
            zedGraph1.GraphPane.YAxisList[1].Scale.Min = -150;
            zedGraph1.GraphPane.YAxisList[1].Scale.FontSpec.FontColor = Color.Green;
            zedGraph1.GraphPane.YAxisList[1].Title.FontSpec.FontColor = Color.Green;
            zedGraph1.GraphPane.YAxisList[1].Color = Color.Green;

            lineItem_RSSI = graphPane.AddCurve("RSSI, ", pointPairList_RSSI, Color.Blue, SymbolType.Square);
            zedGraph1.GraphPane.AddYAxis("dBm");
            zedGraph1.GraphPane.YAxisList[2].Scale.Max = 31;
            zedGraph1.GraphPane.YAxisList[2].Scale.Min = -1;
            zedGraph1.GraphPane.YAxisList[2].Scale.FontSpec.FontColor = Color.Blue;
            zedGraph1.GraphPane.YAxisList[2].Title.FontSpec.FontColor = Color.Blue;
            zedGraph1.GraphPane.YAxisList[2].Color = Color.Blue;

            zedGraph1.GraphPane.XAxis.ResetAutoScale(zedGraph1.GraphPane, CreateGraphics());
            zedGraph1.GraphPane.YAxis.ResetAutoScale(zedGraph1.GraphPane, CreateGraphics());
            zedGraph1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text += " v." + $"{version}";
            this.Update();
            Scan_AT_Port();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Thread_Task != null)
            {
                if (Thread_Task.IsAlive)
                {
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

        private void bt_RFTest_Click(object sender, EventArgs e)
        {
            if (bt_RFTest.Text == "RF Test")
            {
                DoNext = 0;
                serialPort1.PortName = cb_Port1.Text;
                serialPort1.BaudRate = 115200;
                serialPort1.WriteTimeout = 500;
                serialPort1.DtrEnable = true;
                serialPort1.Open();
                OpenLogFile();
                bt_Download.Enabled = false;
                bt_Scan.Enabled = false;
                Thread_Task = new Thread(() => RFTest()); // Create new app tasks
                Thread_Task.Start();
                bt_RFTest.Text = "Stop";
            }
            else
            {
                DoNext = 0xFF;
                while (Thread_Task.IsAlive) ;

                CloseLogFile();
                serialPort1.DtrEnable = false;
                serialPort1.Close();
                serialPort1.Dispose();
                bt_Download.Enabled = true;
                bt_Scan.Enabled = true;
                bt_RFTest.Text = "RF Test";
            }
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

        private string SendCmd_GetRes(ref CmdCxt cmdCxt, string cmd)
        {
            switch(cmdCxt.donext)
            {
                case 0:
                    cmdCxt.donext++;
                    cmdCxt.buffer = new char[1024];
                    cmdCxt.tick = Tick_Get();
                    serialPort1.Write(cmd + "\r");
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
                    else if (Tick_IsOverMs(ref cmdCxt.tick, 500))
                    {
                        cmdCxt.donext--;
                        PrintDebug("\nRX Timeout");
                    }
                    break;

                default:
                case 2:
                    if (serialPort1.BytesToRead > 0)
                    {
                        for (int i = 0; i < serialPort1.BytesToRead; i++)
                        {
                            char c = (char)serialPort1.ReadByte();

                            cmdCxt.buffer[cmdCxt.len++] = c;

                            if (cmdCxt.len > cmdCxt.buffer.Length)
                                cmdCxt.len = 0;

                            PrintRxDebug(c.ToString());
                        }

                        cmdCxt.tick = Tick_Get();
                    }
                    else if (Tick_IsOverMs(ref cmdCxt.tick, 500))
                    {
                        char[] chr = new char[cmdCxt.len];

                        for (int i = 0; i < cmdCxt.len; i++)
                            chr[i] = cmdCxt.buffer[i];

                        cmdCxt.buffer = chr;
                        cmdCxt.donext = 0;

                        return new string(chr);
                    }
                    break;
            }

            return null;
        }

        private void InfoAppendText(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(InfoAppendText), new object[] { msg });
                return;
            }

            rtb_Info.AppendText(msg);
        }

        private void InfoWriteText(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(InfoWriteText), new object[] { msg });
                return;
            }

            rtb_Info.AppendText(msg);
        }

        private string RemoveAT(string atResp)
        {
            char[] arr = atResp.ToCharArray();
            int head = Get_1stIndex(arr, "\r\nOK\r\n", HeadTail.head);
            /*
            PrintDebug("\nIdx=" + head.ToString() + ", ");

            for (int i = head; i < arr.Length; i++)
            {
                if (isPrintable(arr[i]))
                    PrintDebug(arr[i].ToString());
                else
                    PrintDebug("<" + Convert.ToByte(arr[i]).ToString("X2") + ">");
            }
            */
            if (head >= 0)
            {
                arr[head + 2] = '\r';
                arr[head + 3] = '\n';
            }

            atResp = new string(arr);
            RemoveStr(ref atResp, '\r', '\0');
            /*
            arr = atResp.ToCharArray();
            PrintDebug("\nLen=" + arr.Length.ToString() + ": ");

            for (int i = 0; i < arr.Length; i++)
            {
                if (isPrintable(arr[i]))
                    PrintDebug(arr[i].ToString());
                else
                    PrintDebug("<" + Convert.ToByte(arr[i]).ToString("X2") + ">");
            }
            */
            RemoveStr(ref atResp, '\n', '\0');
            /*
            arr = atResp.ToCharArray();
            PrintDebug("\nLen=" + arr.Length.ToString() + ": ");

            for (int i = 0; i < arr.Length; i++)
            {
                if (isPrintable(arr[i]))
                    PrintDebug(arr[i].ToString());
                else
                    PrintDebug("<" + Convert.ToByte(arr[i]).ToString("X2") + ">");
            }
            */
            return atResp;
        }

        private void RFTest()
        {
            int csq = 99;
            int rsrp = -150;
            int rsrq = -150;
            bool cont = true;
            string resp = null;

            CmdCxt cmdCxt = new CmdCxt();

            cmdCxt.donext = 0;

            while (cont)
            {
                Thread.Sleep(100);

                switch (DoNext)
                {
                    case 0:
                        resp = SendCmd_GetRes(ref cmdCxt, "ATE0\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                                DoNext++;
                        }
                        break;

                    case 1:
                        resp = SendCmd_GetRes(ref cmdCxt, "ATI4\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                InfoWriteText("Module: ");
                                InfoAppendText(RemoveAT(resp));
                                //InfoAppendText(new string(SubArray(cmdCxt.buffer, "ATI4\r\n", "\r\nOK")));
                            }
                        }
                        break;

                    case 2:
                        resp = SendCmd_GetRes(ref cmdCxt, "AT+CGSN\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                InfoAppendText("\nIMEI: ");
                                InfoAppendText(RemoveAT(resp));
                                //InfoAppendText(new string(SubArray(cmdCxt.buffer, "AT+CGSN\r\n", "\r\nOK")));
                            }
                        }
                        break;

                    case 3:
                        resp = SendCmd_GetRes(ref cmdCxt, "AT#CIMI\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                InfoAppendText("\nCIMI: ");
                                resp = new string(SubArray(cmdCxt.buffer, "#CIMI: ", "\r\nOK"));
                                InfoAppendText(RemoveAT(resp));
                            }
                            else if (resp.Contains("\r\nERROR\r\n"))
                                DoNext++;
                        }
                        break;

                    case 4:
                        resp = SendCmd_GetRes(ref cmdCxt, "AT+CCID\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                InfoAppendText("\nCCID: ");
                                resp = new string(SubArray(cmdCxt.buffer, "+CCID: ", "\r\nOK"));
                                InfoAppendText(RemoveAT(resp));
                            }
                        }
                        break;

                    case 5:
                        resp = SendCmd_GetRes(ref cmdCxt, "AT+COPS?\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                InfoAppendText("\nOperator: ");
                                InfoAppendText(new string(SubArray(cmdCxt.buffer, ",\"", "\",")));
                            }
                        }
                        break;

                    case 6:
                        resp = SendCmd_GetRes(ref cmdCxt, "AT#MONI=0\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                                DoNext++;
                        }
                        break;

                    case 7:
                        resp = SendCmd_GetRes(ref cmdCxt, "AT+CSQ\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                csq = int.Parse(new string(SubArray(cmdCxt.buffer, "+CSQ: ", ",")));
                            }
                        }
                        break;

                    case 8:
                        resp = SendCmd_GetRes(ref cmdCxt, "AT#MONI\r");

                        if (resp != null)
                        {
                            if (resp.Contains("\r\nOK\r\n"))
                            {
                                DoNext++;
                                rsrp = int.Parse(new string(SubArray(cmdCxt.buffer, "RSRP:", " RSRQ:")));
                                rsrq = int.Parse(new string(SubArray(cmdCxt.buffer, "RSRQ:", " TAC:")));
                                InfoAppendText("\nTAC: ");
                                InfoAppendText(new string(SubArray(cmdCxt.buffer, "TAC:", " Id:")));
                                InfoAppendText(", Cell ID: ");
                                InfoAppendText(new string(SubArray(cmdCxt.buffer, " Id:", " EARFCN:")));
                            }
                        }
                        break;

                    case 0xFF:
                        cont = false;
                        break;

                    default:
                        DoNext = 7;
                        WriteLogFile(rsrp, rsrq, csq);

                        if (zedGraph1.GraphPane.CurveList.Count <= 0)
                            break;

                        LineItem curve1 = zedGraph1.GraphPane.CurveList[0] as LineItem;
                        LineItem curve2 = zedGraph1.GraphPane.CurveList[1] as LineItem;
                        LineItem curve3 = zedGraph1.GraphPane.CurveList[2] as LineItem;

                        if (curve1 == null)
                            break;

                        if (curve2 == null)
                            break;

                        if (curve3 == null)
                            break;

                        IPointListEdit list1 = curve1.Points as IPointListEdit;
                        IPointListEdit list2 = curve2.Points as IPointListEdit;
                        IPointListEdit list3 = curve3.Points as IPointListEdit;

                        if (list1 == null)
                            break;

                        if (list2 == null)
                            break;

                        if (list3 == null)
                            break;

                        double time = (Environment.TickCount - TickStart) / 1000.0;

                        list1.Add(time, rsrp);
                        list2.Add(time, rsrq);
                        list2.Add(time, csq);

                        Scale xScale = zedGraph1.GraphPane.XAxis.Scale;

                        if (time > xScale.Max - xScale.MajorStep)
                        {
                            if (viewMode)
                            {
                                xScale.Max = time + xScale.MajorStep;
                                xScale.Min = 0;
                            }
                            else
                            {
                                xScale.Max = time + xScale.MajorStep;
                                xScale.Min = xScale.Max - 30.0;
                            }
                        }

                        zedGraph1.AxisChange();
                        zedGraph1.Invalidate();
                        zedGraph1.Update();
                        Thread.Sleep(900);
                        break;
                }
            }

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
                    DoNext = 0;
                    serialPort1.PortName = cb_Port1.Text;
                    serialPort1.BaudRate = 115200;
                    serialPort1.WriteTimeout = 1000;
                    serialPort1.DtrEnable = true;
                    serialPort1.Open();

                    bt_RFTest.Enabled = false;
                    bt_Scan.Enabled = false;

                    Thread_Task = new Thread(() => Download()); // Create new app tasks
                    Thread_Task.Start();

                    bt_Download.Text = "Stop";
                }
                else
                {
                    DoNext = 0xFF;
                    Thread_Task.Interrupt();
                    Thread_Task.Abort();
                    Thread_Task.Join();
                    while (Thread_Task.IsAlive) ;

                    serialPort1.DtrEnable = false;
                    serialPort1.Close();
                    serialPort1.Dispose();
                    bt_RFTest.Enabled = true;
                    bt_Scan.Enabled = true;
                    bt_Download.Text = "Download";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Download()
        {
            int idx = 0;
            long tick = 0;
            bool cont = true;
            string res = null;
            /*
            while(cont)
            {
                switch(DoNext)
                {
                    case 0:
                        DoNext++;
                        tick = Tick_Get();
                        serialPort1.Write("AT#MONI=0\r");
                        PrintTxDebug("\nTX: AT#MONI=0");
                        res = "OK";
                        break;

                    case 2:
                        DoNext++;
                        tick = Tick_Get();
                        serialPort1.Write("AT#MONI\r");
                        PrintTxDebug("\nTX: AT#MONI");
                        res = "OK";
                        break;

                    case 1:
                    case 3:
                        if (serialPort1.BytesToRead > 0)
                        {
                            PrintRxDebug("\nRX: ");

                            for (int i = 0; i < serialPort1.BytesToRead; i++)
                            {
                                char c = (char)serialPort1.ReadByte();

                                PrintRxDebug(c.ToString());

                                if (FindString(c, ref idx, res))
                                    DoNext++;
                            }
                        }
                        else if (Tick_IsOverMs(ref tick, 500))
                        {
                            DoNext --;
                            PrintDebug("\nRX Timeout");
                        }
                        break;

                    case 0xFF:
                        cont = false;
                        break;
                }
            }
            */
            Thread_Task.Suspend();
        }

        private void cb_ViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_ViewMode.Text == "Scroll")
                viewMode = true;
            else
                viewMode = false;
        }
    }
}
