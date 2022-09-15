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

namespace NBST
{
    public partial class Form1 : Form
    {
        private PointPairList pointPairList_RSRP = null;
        private LineItem lineItem_RSRP = null;

        private PointPairList pointPairList_RSRQ = null;
        private LineItem lineItem_RSRQ = null;

        private PointPairList pointPairList_RSSI = null;
        private LineItem lineItem_RSSI = null;

        private GraphPane graphPane = null;
        private static Thread Thread_Task = null;
        private volatile int DoNext = 0;
        private string ModuleName = null;
        private string ModuleImei = null;
        private StreamWriter sW = null;
        private string fileName = null;
        private long line = 0;
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

        private string strRemove(string s)
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
            rtb_Log.SelectionColor = color;
            rtb_Log.AppendText(msg);
            //rtb_Log.ForeColor = color;
        }

        private void PrintDebug(string msg)
        {
            PrintDebug(msg, Color.Black);
        }

        private void PrintTxDebug(string msg)
        {
            PrintDebug(msg, Color.Blue);
        }

        private void PrintRxDebug(string msg)
        {
            PrintDebug(msg, Color.Red);
        }

        private string FileNameGenerate(string prefix)
        {
            string s = strRemove(prefix + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString());

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
                rtb_Log.Clear();
                sW = null;
            }
        }

        private void OpenLogFile()
        {
            CloseLogFile();
            fileName = FileNameGenerate("NBST_" + ModuleName + "_" + ModuleImei + "_");

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
            int doNext = 0;

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

                int idx = 0;
                bool first = true;
                bool found = false;
                long tick = Tick_Get();

                do
                {
                    switch (doNext)
                    {
                        case 0:
                            idx = 0;
                            doNext++;
                            serialPort1.WriteLine("AT\r");
                            PrintTxDebug("\nTX: AT");
                            break;

                        default:
                            if (serialPort1.BytesToRead > 0)
                            {
                                found = false;

                                if (first == true)
                                {
                                    first = false;
                                    PrintRxDebug("\nRX: ");
                                }

                                for (int i = 0; i < serialPort1.BytesToRead; i++)
                                {
                                    char c = (char)serialPort1.ReadByte();

                                    PrintRxDebug(c.ToString());

                                    if (FindString(c, ref idx, "OK"))
                                        found = true;
                                }

                                if (found == true)
                                {
                                    serialPort1.DtrEnable = false;
                                    serialPort1.Close();
                                    serialPort1.Dispose();

                                    return true;
                                }
                            }
                            else if (Tick_IsOverMs(ref tick, 500))
                            {
                                loop++;
                                doNext = 0;
                                PrintDebug("\nRX Timeout");
                            }
                            break;
                    }
                }
                while (loop < 5);
            }
            catch (Exception ex)
            {
                //PrintDebug(ex.ToString());
                PrintDebug("\nTX Timeout");
            }

            serialPort1.DtrEnable = false;
            serialPort1.Close();
            serialPort1.Dispose();

            return false;
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

        private void Plot(int rsrp, int rsrq, int rssi)
        {

        }

        public Form1()
        {
            InitializeComponent();
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
            try
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
                    Thread_Task.Interrupt();
                    Thread_Task.Abort();
                    Thread_Task.Join();
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
            catch(Exception ex)
            {

            }
        }

        private void RFTest()
        {

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

            Thread_Task.Suspend();
        }
    }
}
