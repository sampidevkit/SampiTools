using System;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Management;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Text;
using System.Diagnostics;
using SerialPortTerminal.Properties;
using System.Resources;
using Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Form1
{
    public partial class FormSPT : Form
    {
        #region Variables
        enum DfuStt : ushort
        {
            CHECK_CLOSE_PORT = 0,
            OPEN_PORT, // 1
            READ_LINE, // 2
            CHECK_OPEN_PORT, // 3
            SEND_DATA, // 4
            WAIT_RESPONSE, // 5
            CHECK_RESPONSE, // 6
            ERROR // 7
        }

        // File IO
        private struct FileInfo
        {
            public long TotalLength;
            public long NumOfLine;
        }
        private Utils.Utils Utils = new Utils.Utils();
        private FileInfo HexFileDetail;
        private string HexFilePath = null;
        private FileInfo ScriptFileDetail;
        private string ScriptFilePath = null;
        private int scriptLineIdx = 0;
        private FileStream DfuStream = null;
        private StreamReader DfuReader = null;
        private char[] ScriptData = null;
        // Threads
        private static Thread Thread_Task;
        private volatile bool DebugWait = false;
        private volatile bool InternalPort = true;
        private volatile bool AppBusy = false;
        private volatile bool FirstRx = true;
        // Common
        UTF8Encoding ENC = new System.Text.UTF8Encoding();
        #endregion

        #region User's Functions

        private void ClearLog()
        {
            string logpath = "Log " + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
            char[] logpathchr = logpath.ToCharArray();

            Utils.ReplaceCharArray(ref logpathchr, '/', (char)0x00);
            Utils.ReplaceCharArray(ref logpathchr, '\\', (char)0x00);
            Utils.ReplaceCharArray(ref logpathchr, ':', (char)0x00);
            Utils.ReplaceCharArray(ref logpathchr, '.', (char)0x00);
            logpath = new string(logpathchr) + ".txt";
            //DebugLog(logpath, Color.Green);
            StreamWriter sw = File.CreateText(logpath);
            sw.WriteLine(rtb_Log.Text);
            sw.Close();
            rtb_Log.Clear();
            lb_Notic.Text = logpath + " has been created";
            lb_Notic.Visible = true;
            timer2.Start();
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
                if (!Utils.Is_ASCII_Printable_Character(reader.ReadByte(), ref fileInfo.NumOfLine))
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
                switch (pChr[i])
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

            if (FirstRx == true)
            {
                FirstRx = false;

                if (ckb_TS.Checked)
                    DebugLog("\n[" + ((DateTime.Now.Ticks / 10000) & 0xFFFFFF).ToString("D8") + "] RX: ", Color.Green);
                else
                    DebugLog("\nRX: ", Color.Green);
            }

            DebugLog(sdata, Color.Green);
        }

        private void SendCmdString()
        {
            string cmdStr = rtb_CmdData.Text;

            if (ckb_CR.Checked)
                cmdStr += "\r";

            if (ckb_LF.Checked)
                cmdStr += "\n";

            char[] cmdChr = cmdStr.ToCharArray();

            try
            {
                while (DebugWait) ;

                if (ckb_TS.Checked)
                    DebugLog("\n[" + ((DateTime.Now.Ticks / 10000) & 0xFFFFFF).ToString("D8") + "] TX: " + cmdStr, Color.Blue);
                else
                    DebugLog("\nTX: " + cmdStr, Color.Blue);

                FirstRx = true;
                serial_Port1.Write(new string(MakeFrame(cmdChr, cmdChr.Length)));
            }
            catch
            {
                while (DebugWait) ;
                DebugLog("\nCan not write to Port 1", Color.Red);
            }
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
                int newInterval = Utils.Char2Integer(cmdChr, 1, cmdChr.Length - 2);

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

                    if (ckb_TS.Checked)
                        DebugLog("\n[" + ((DateTime.Now.Ticks / 10000) & 0xFFFFFF).ToString("D8") + "] TX: ", Color.Blue);
                    else
                        DebugLog("\nTX: " + cmdStr, Color.Blue);

                    FirstRx = true;
                    serial_Port1.Write(new string(MakeFrame(cmdChr, cmdChr.Length)));
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

        private void Disable_Uart_Components()
        {
            cb_Baud.Enabled = false;
            cb_DfuBaud.Enabled = false;
            bt_SetPort1.Enabled = false;
            bt_SetPort2.Enabled = false;
            cb_DfuPort.Enabled = false;
            bt_FwdScan.Enabled = false;
            bt_DfuScan.Enabled = false;
            bt_Browse.Enabled = false;
            nud_FrameDelay.Enabled = false;
        }

        private void Enable_Uart_Components()
        {
            cb_Baud.Enabled = true;
            cb_DfuBaud.Enabled = true;
            bt_SetPort1.Enabled = true;
            bt_SetPort2.Enabled = true;
            cb_DfuPort.Enabled = true;
            bt_FwdScan.Enabled = true;
            bt_DfuScan.Enabled = true;
            bt_Browse.Enabled = true;
            nud_FrameDelay.Enabled = true;
        }
        #endregion

        #region App Forward

        private bool FwdPort_Is_Closed()
        {
            if (!Port_Is_Present(serial_Port1.PortName))
                return true;

            if (!Port_Is_Present(serial_Port2.PortName))
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

            try
            {
                if (Port_Is_Present(lb_Port1.Text))
                {
                    if (serial_Port1.IsOpen)
                    {
                        serial_Port1.DtrEnable = false;
                        serial_Port1.Dispose();
                        serial_Port1.Close();
                    }

                    serial_Port1.PortName = lb_Port1.Text;
                }
                else
                    return false;

                if (InternalPort == false)
                {
                    if (Port_Is_Present(lb_Port2.Text))
                    {
                        if (serial_Port2.IsOpen)
                        {
                            serial_Port2.DtrEnable = false;
                            serial_Port2.Dispose();
                            serial_Port2.Close();
                        }

                        serial_Port2.PortName = lb_Port2.Text;
                    }
                    else
                        return false;
                }

                serial_Port1.BaudRate = int.Parse(cb_Baud.Text);
                serial_Port1.Open();
                serial_Port1.DtrEnable = true;

                if (InternalPort == false)
                {
                    serial_Port2.BaudRate = serial_Port1.BaudRate;
                    serial_Port2.Open();
                    serial_Port2.DtrEnable = true;
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
                if (serial_Port1.IsOpen)
                {
                    serial_Port1.DtrEnable = false;
                    serial_Port1.Dispose();
                    serial_Port1.Close();
                }

                if (serial_Port2.IsOpen)
                {
                    serial_Port2.DtrEnable = false;
                    serial_Port2.Dispose();
                    serial_Port2.Close();
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

            if ((lb_Port1.Text != "Empty") && (lb_Port2.Text != "Empty"))
            {
                if (Open_Fwd_Connection())
                {
                    Thread_Task = new Thread(() => App_Forward()); // Create new app tasks
                    Thread_Task.Start();
                    success = true;
                }
            }

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
        private void Get_Port()
        {
            int found = 0;
            string[] ports;

            cb_Port.Items.Clear();
            cb_Port.Items.Add("Internal");
            cb_DfuPort.Items.Clear();
            cb_DfuPort.Items.Add("Empty");

            try
            {
                ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    cb_Port.Items.Add(port);
                    cb_DfuPort.Items.Add(port);
                    found++;
                }
            }
            catch (Exception ex)
            {
                while (DebugWait) ;
                DebugLog("\n\n[Get_Port]\n" + ex.ToString(), Color.Red);
            }

            cb_Port.SelectedIndex = 0;
            cb_DfuPort.SelectedIndex = 0;
        }

        private bool DfuPort_Is_Closed()
        {
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
                DfuStt DoNext = DfuStt.OPEN_PORT;
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
                            case DfuStt.CHECK_CLOSE_PORT: // Check closed port
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

                            case DfuStt.OPEN_PORT: // Open port
                                if (Open_Dfu_Connection())
                                {
                                    DoNext = DfuStt.READ_LINE;
                                    LineCount = 0;
                                    while (DebugWait) ;
                                    DebugLog("\n\nPort openned", Color.Green);
                                }
                                else
                                    DoNext = DfuStt.CHECK_CLOSE_PORT;
                                break;

                            case DfuStt.READ_LINE: // Read 1 line
                                LineCount++;
                                Data = DfuReader.ReadLine();

                                if (Data != null)
                                {
                                    Resend = false;
                                    DoNext = DfuStt.CHECK_OPEN_PORT;
                                }
                                else
                                    DoNext = DfuStt.CHECK_CLOSE_PORT;
                                break;

                            case DfuStt.CHECK_OPEN_PORT: // Check opened port
                                if (DfuPort_Is_Openned())
                                    DoNext = DfuStt.SEND_DATA;
                                else
                                    DoNext = DfuStt.CHECK_CLOSE_PORT;
                                break;

                            case DfuStt.SEND_DATA: // Send data
                                serialDfu.WriteLine(Data);
                                while (DebugWait) ;
                                DebugLog("\nTX " + LineCount.ToString("D8") + ": " + Data, Color.OrangeRed);

                                if (Resend)
                                {
                                   // while (DebugWait) ;
                                    //DebugLog("\nTX " + LineCount.ToString("D8") + ": " + Data, Color.OrangeRed);
                                }
                                else if (Per != ((100 * LineCount) / HexFileDetail.NumOfLine))
                                {
                                    Per = (100 * LineCount) / (int)HexFileDetail.NumOfLine;

                                    //while (DebugWait) ;
                                    //DebugLog("\n" + Per.ToString() + "%", Color.Blue);
                                    lb_Status.Text = "Downloading... " + Per.ToString() + "%";
                                }

                                Count = 0;
                                BuffLen = 0;
                                DoNext = DfuStt.WAIT_RESPONSE;
                                break;

                            case DfuStt.WAIT_RESPONSE: // Waiting for response
                                if (++Count < (5000 / Tdelay))
                                {
                                    if (serialDfu.BytesToRead > 0)
                                    {
                                        int i;

                                        if (BuffLen == 0)
                                        {
                                            for (i = 0; i < Buff.Length; i++)
                                                Buff[i] = 0;

                                            while (DebugWait) ;
                                            DebugLog("\nRX: ", Color.Green);
                                        }

                                        for (i = 0; i < serialDfu.BytesToRead; i++)
                                        {
                                            int c;

                                            if (BuffLen >= Buff.Length)
                                                BuffLen = 0;

                                            c = serialDfu.ReadByte();
                                            Buff[BuffLen++] = (byte)c;
                                            while (DebugWait) ;
                                            DebugLog(new String((char)c, 1), Color.Green);
                                        }

                                        if (BuffLen >= 2)
                                            DoNext = DfuStt.CHECK_RESPONSE;
                                    }
                                }
                                else
                                    DoNext = DfuStt.CHECK_CLOSE_PORT;
                                break;

                            case DfuStt.CHECK_RESPONSE: // Check response
                                if (Buff[0] == 'A')
                                {
                                    DoNext = DfuStt.READ_LINE;
                                }
                                else if (Buff[0] == 'K')
                                {
                                    DoNext = DfuStt.SEND_DATA;
                                    Resend = true;
                                    while (DebugWait) ;
                                    DebugLog("\nERROR ", Color.Green);
                                }
                                else
                                {
                                    while (DebugWait) ;
                                    DebugLog("\nResponse incorrect code: " + Buff[0].ToString("X2") + Buff[1].ToString("X2"), Color.OrangeRed);
                                    DoNext = DfuStt.CHECK_CLOSE_PORT;
                                }
                                break;

                            default: // Error
                                DoNext = DfuStt.CHECK_CLOSE_PORT;
                                break;
                        }

                        Thread.Sleep(Tdelay);
                    }
                    catch //(Exception e)
                    {
                        //while (DebugWait) ;
                        //DebugLog("\n\n[App_Forward-while(true)]\n" + e.ToString(), Color.Red);
                    }
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

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text += " v." + $"{version}";
            this.Update();

            rtb_Log.Size = new System.Drawing.Size(392, 303);
            rtb_Log.Location = new System.Drawing.Point(9, 257);
            tabControl1.Size = new System.Drawing.Size(392, 243);

            cb_Baud.SelectedIndex = 9;
            cb_DfuBaud.SelectedIndex = 9;
            Get_Port();
            Port2_Selection();
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

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] temp;
            int byte2read;
            bool sttBk = AppBusy;

            AppBusy = true;

            try
            {
                byte2read = serial_Port1.BytesToRead;// Get number of bytes of received buffer
                temp = new byte[byte2read];// Create array to save data in received buffer
                serial_Port1.Read(temp, 0, byte2read);// Read buffer data, save to array

                if (InternalPort == false)
                    serial_Port2.Write(temp, 0, byte2read);// Forward to serialport 2
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
                byte2read = serial_Port2.BytesToRead;// Get number of bytes of received buffer
                temp = new byte[byte2read];// Create array to save data in received buffer
                serial_Port2.Read(temp, 0, byte2read);// Read buffer data, save to array
                serial_Port1.Write(temp, 0, byte2read);// Forward to serialport 1
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

            if (rtb_Log.Text.Length >= 2147483647)
                ClearLog();
        }

        private void bt_Browse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
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
            Get_Port();
        }

        private void cb_DfuPort_MouseClick(object sender, MouseEventArgs e)
        {
            Get_Port();
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
            Get_Port();
        }

        private void lb_OpenLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory());
        }

        private void pic_OpenLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory());
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

        private void bt_SetPort2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bt_Send_Click(object sender, EventArgs e)
        {
            rtb_CmdData.Enabled = false;
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

                rtb_CmdData.Enabled = true;
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

            rtb_CmdData.Enabled = true;
            ckb_Loop.Enabled = true;
            ckb_Script.Enabled = true;

            if ((ckb_Script.Checked == true) || (ckb_Loop.Checked == true))
            {
                rtb_CmdData.Enabled = false;
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
                    rtb_CmdData.Enabled = false;
                }
                else
                {
                    rtb_CmdData.Enabled = true;
                }
            }
        }

        private void bt_Script_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                scriptLineIdx = 0;
                ScriptFilePath = openFileDialog2.FileName;
                rtb_CmdData.Text = ScriptFilePath;

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
                    rtb_CmdData.Text = null;
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
                rtb_CmdData.Enabled = !ckb_Script.Checked;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    rtb_Log.Size = new System.Drawing.Size(392, 303);
                    rtb_Log.Location = new System.Drawing.Point(9, 257);
                    tabControl1.Size = new System.Drawing.Size(392, 243);
                    break;

                case 1:
                    rtb_Log.Size = new System.Drawing.Size(392, 416);
                    rtb_Log.Location = new System.Drawing.Point(9, 144);
                    tabControl1.Size = new System.Drawing.Size(392, 130);
                    break;

                case 2:
                    rtb_Log.Size = new System.Drawing.Size(392, 425);
                    rtb_Log.Location = new System.Drawing.Point(9, 135);
                    tabControl1.Size = new System.Drawing.Size(392, 121);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Shortcuts
        private void bt_TaskMngr_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("taskmgr.exe");
        }

        private void bt_DeviceMngr_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("devmgmt.msc");
        }

        private void bt_DiskMngr_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("diskmgmt.msc");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lb_Notic.Visible = false;
        }

        #endregion

        private void bt_SetPort1_Click(object sender, EventArgs e)
        {
            if (cb_Port.Items.Count > 0)
            {
                if (cb_Port.Text != "Internal")
                {
                    lb_Port1.Text = cb_Port.Text;
                    cb_Port.Items.RemoveAt(cb_Port.SelectedIndex);

                    if (cb_Port.Items.Count > 0)
                        cb_Port.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("You can not set Internal port as Port 1!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Port2_Selection()
        {
            if (cb_Port.Items.Count > 0)
            {
                lb_Port2.Text = cb_Port.Text;
                cb_Port.Items.RemoveAt(cb_Port.SelectedIndex);

                if (cb_Port.Items.Count > 0)
                    cb_Port.SelectedIndex = 0;

                if (lb_Port2.Text == "Internal")
                {
                    bt_Send.Enabled = true;
                    bt_Script.Enabled = true;
                    InternalPort = true;
                }
                else
                {
                    bt_Send.Enabled = false;
                    bt_Script.Enabled = false;
                    InternalPort = false;
                }
            }
        }

        private void bt_SetPort2_Click(object sender, EventArgs e)
        {
            Port2_Selection();
        }
    }
}
