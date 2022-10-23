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
using Spire.Xls;

namespace NBST
{
    public partial class Form1 : Form
    {
        #region "Type and Variables"
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
            INIT_APP = 0,
            CMD_ECHO_OFF,
            CMD_DISPLAY_ERROR,
            CMD_NO_FLOW_CONTROL,
            CMD_GET_MODULE_NAME,
            CMD_GET_MODULE_IMEI,
            CMD_GET_CIMI,
            CMD_GET_ALT_CIMI,
            CMD_GET_CCID,
            CMD_GET_ALT_CCID,
            CMD_GET_NETWORK_REGIS,
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
            CMD_PING,
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

        private struct ThreadCxt
        {
            public int Enbale;
            public Thread Task;
            public ThreadMode Mode;
            public ThreadTask DoNext;
            public ThreadTask ToDo;
        }

        private struct SignalCxt
        {
            public int value;
            public int cout;
            public int average;
            public long sum;
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
            public bool Loop;
            public bool SslEn;
            public string Host;
            public string Port;
            public string FilePath;
            public string FileName;
            public string Md5;
            public int FileSize;
            public int DownloadedSize;
            public int Count;
            public BinaryWriter sW;
        }

        private struct RfCxt
        {
            public SignalCxt Rssi;
            public SignalCxt Rsrp;
            public SignalCxt Rsrq;
            public UInt32 Delay;
            public int Count;
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

        private ThreadCxt threadCxt = new ThreadCxt
        {
            Enbale = 0,
            Task = null,
            Mode = ThreadMode.RF_TEST,
            DoNext = ThreadTask.INIT_APP,
            ToDo = ThreadTask.INIT_APP
        };

        private DownloadCxt downloadCxt = new DownloadCxt
        {
            Loop = false,
            SslEn = false,
            Host = null,
            Port = null,
            FilePath = null,
            FileName = null,
            Md5 = null,
            FileSize = 0,
            DownloadedSize = 0,
            Count = 5,
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

        static ASCIIEncoding encoding = new ASCIIEncoding();
        private RfCxt rfCxt = new RfCxt();
        private volatile string[] historyCmd = new string[10];
        private volatile string[] historyUrl = new string[10];
        private volatile int RebootTryCount = 0;
        private volatile bool debugEn = true;
        private volatile bool viewMode = false;
        private volatile bool printableMode = true;
        private StreamWriter sW = null;
        private string logfileName = null;
        private volatile int LogCount = 0;
        private long line = 0;
        private UInt32 TickStart = 0;
        private volatile string portName = null;
        private int sysStart = 0;
        private volatile UInt32 rebootWait = 5000;
        private volatile UInt32 respWait = 250;
        private Workbook workbook = new Workbook();

        private string[] UsbPid = new string[5]
        { 
        /* MICROCHIP USB CDC */ "VID_04D8&PID_000A",
        /* xE866 */             "VID_1BC7&PID_0021", 
        /* MEx10G1 */           "VID_1BC7&PID_110A",
        /* LE910 */             "VID_1BC7&PID_1201",
        /* FTDI */              "VID_0403+PID_6001"
        };
        #endregion

        #region "RTCC functions"
        private double Get_RealTime()
        {
            return 0;
        }

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
        #endregion

        #region "String processing functions"
        private bool isPrintable(char c)
        {
            if ((c >= 0x20) && (c <= 0x7E))
                return true;

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

        private void ReplaceCharArray(ref char[] ChrArr, char oldChar, char newChar)
        {
            int i, j;
            int len = ChrArr.Length;

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

        private int Get_Index(char[] buffer, int offset, char chr, int chr_count)
        {
            if (chr_count == 0)
                return (-1);

            int len = buffer.Length - offset;

            for (int i = offset; i < len; i++)
            {
                if (chr == buffer[i])
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

            return SubArray(buffer, head, tail);
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

        private string ChangeSpecialByte(string s)
        {
            byte[] BIn = encoding.GetBytes(s);
            byte[] BOut = new byte[BIn.Length];
            int i, j;

            for (i = 0, j = 0; i < BIn.Length; i++)
            {
                if (BIn[i] == '\\')
                {
                    try
                    {
                        switch (BIn[i + 1])
                        {
                            case (byte)'r':
                                BOut[j++] = (byte)'\r';
                                i++;
                                break;

                            case (byte)'n':
                                BOut[j++] = (byte)'\n';
                                i++;
                                break;

                            default:
                                if ((BIn[i + 1] >= (byte)'0') && (BIn[i + 1] <= (byte)'9'))
                                {
                                    BOut[j++] = (byte)(BIn[i + 1] - '0');
                                    i++;
                                }
                                else
                                    BOut[j++] = BIn[i];
                                break;
                        }
                    }
                    catch
                    {
                        BOut[j++] = BIn[i];
                    }
                }
                else
                    BOut[j++] = BIn[i];
            }

            if (j > 0)
            {
                char[] Arr = new char[j];

                for (i = 0; i < j; i++)
                    Arr[i] = (char)BOut[i];

                return new string(Arr);
            }

            return null;
        }
        #endregion

        #region "Debug functions"
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

            string s = prefix + "Char len=" + chr.Length.ToString() + ": ";

            foreach (char c in chr)
            {
                if (isPrintable(c))
                    s += c.ToString();
                else if (printableMode == true)
                {
                    if ((c == '\r') || (c == '\n'))
                        s += c.ToString();
                    else
                        s += ".";
                }
                else
                    s += "<" + Convert.ToByte(c).ToString("X2") + ">";
            }

            s += subfix;
            PrintDebug(s);
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
                else if (printableMode == true)
                {
                    if ((data[i] == '\r') || (data[i] == '\n'))
                        s += data[i].ToString();
                    else
                        s += ".";
                }
                else
                    s += "<" + Convert.ToByte(data[i]).ToString("X2") + ">";
            }

            PrintDebug(s + "\n\n", color);
        }
        #endregion

        #region "Log functions"

        private string FileNameGenerate(string prefix)
        {
            string s = RemoveInvalidName(prefix + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString());

            return s;
        }

        private void WriteExcelFile(string device, string imei, string cimi, string ccid, string apn, 
            int tac, int cellid, int rsrp, int rsrq, int rssi, double lat, double lon,
            int pass, int fail, string note)
        {

        }

        private void ExcelFile_Deinit()
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
                sW = null;
            }
        }

        private void ExcelFille_Init(string fileName)
        {
            ExcelFile_Deinit();
            logfileName = FileNameGenerate("NBST_");

            try
            {
                line = 0;
                sW = new StreamWriter(logfileName + ".m");
                sW.WriteLine("%{");
                sW.WriteLine(rtb_Info.Text);
                sW.WriteLine("%}");
                sW.WriteLine(NBST.Properties.Resources.header);
                sW.WriteLine("\nstartTime=" + ToUnixTimeSeconds().ToString() + ";");
                sW.WriteLine("\na=[");

                rtb_Log.Text = "New log file " + logfileName + ".m has been created";
            }
            catch
            {
                PrintDebug("\nFile name \"" + logfileName + "\"error");
            }
        }

        private void WriteLogFile(int rsrp, int rsrq, int rssi, double lat, double lon)
        {
            if (sW != null)
            {
                line++;

                string s = "\n% ";

                if ((lat < (-999.0)) || (lon < (-999.0)))
                    s += "https://maps.google.com/maps?hl=en&q=" + lat.ToString("0.000000") + "," + lon.ToString("0.000000") + "\n";
                else
                    s += "Can not find your location\n";

                s += line.ToString() + " " + rsrp.ToString() + " " + rsrq.ToString() + " " + rssi.ToString();
                sW.WriteLine(s);

                s = "\n" + line.ToString("D4") + " ";
                s += DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString() + ": ";
                s += lat.ToString() + ", " + lon.ToString();
                s += ", RSRP=" + rsrp.ToString() + "dBm, RSRQ=" + rsrq.ToString() + "dB, RSSI=" + rssi.ToString() + "dB";
                PrintDebug(s);
            }
        }

        private void Log_Deinit()
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

        private void Log_Init()
        {
            Log_Deinit();
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
        #endregion

        #region "Serial port functions"
        private string Parse_COMPort(string str)
        {
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

        private bool SendAT(string pName, string cmd, UInt32 wait)
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

                serialPort1.PortName = pName;
                serialPort1.BaudRate = 115200;
                serialPort1.WriteTimeout = 10;
                serialPort1.DtrEnable = true;
                serialPort1.RtsEnable = true;
                serialPort1.Open();

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
                    else if (Tick_IsOverMs(ref tick, wait))
                    {
                        loop++;
                    }
                }
                while (loop < 2);
            }
            catch { }

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

        private bool SendAT(string pName, string cmd)
        {
            return SendAT(pName, cmd, 250);
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

                                if (portlist == null)
                                {
                                    portlist += port;

                                    if (SendAT(port, "ATE0\r"))
                                    {
                                        found++;
                                        cb_Port1.Items.Add(port);
                                    }
                                }
                                else if (!portlist.Contains(port))
                                {
                                    portlist += port;

                                    if (SendAT(port, "ATE0\r"))
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

                        if (portlist == null)
                        {
                            portlist += port;

                            if (SendAT(port, "ATE0\r"))
                            {
                                found++;
                                cb_Port1.Items.Add(port);
                            }
                        }
                        else if (!portlist.Contains(port))
                        {
                            portlist += port;

                            if (SendAT(port, "ATE0\r"))
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
                            else if (printableMode == true)
                            {
                                if ((chr[i] == '\r') || (chr[i] == '\n'))
                                    s += chr[i].ToString();
                                else
                                    s += ".";
                            }
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
                            else if (printableMode == true)
                            {
                                if ((chr[i] == '\r') || (chr[i] == '\n'))
                                    s += chr[i].ToString();
                                else
                                    s += ".";
                            }
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

        /*
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
        */
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
                            else if (printableMode == true)
                            {
                                if ((chr[i] == '\r') || (chr[i] == '\n'))
                                    s += chr[i].ToString();
                                else
                                    s += ".";
                            }
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
        #endregion

        #region "System functions"
        public Form1()
        {
            InitializeComponent();
            sysStart = Environment.TickCount;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("HistoryCMD.txt"))
            {
                StreamReader sR = new StreamReader("HistoryCMD.txt");
                string deviceStr = sR.ReadToEnd();

                sR.Close();
                ReplaceStr(ref deviceStr, '\r', (char)0x00);

                char[] deviceArr = deviceStr.ToCharArray();
                int lineCount = 1;

                foreach (char c in deviceArr)
                {
                    if (c == '\n')
                        lineCount++;
                }

                int lineIdx = 0;
                historyCmd[lineIdx] = null;

                foreach (char c in deviceArr)
                {
                    switch (c)
                    {
                        case '\n':
                            lineIdx++;

                            if (lineIdx < lineCount)
                                historyCmd[lineIdx] = null;
                            break;

                        default:
                            if (lineIdx < lineCount)
                                historyCmd[lineIdx] += c.ToString();
                            break;
                    }
                }

                bool not_empty = false;

                cb_CMD.Items.Clear();

                foreach (string s in historyCmd)
                {
                    if (s != null)
                    {
                        not_empty = true;
                        cb_CMD.Items.Add(s);
                    }
                }

                if (not_empty == false)
                    cb_CMD.Items.Add("AT\\r");

                cb_CMD.SelectedIndex = 0;
            }

            if (File.Exists("HistoryUrl.txt"))
            {
                StreamReader sR = new StreamReader("HistoryUrl.txt");
                string deviceStr = sR.ReadToEnd();

                sR.Close();
                ReplaceStr(ref deviceStr, '\r', (char)0x00);

                char[] deviceArr = deviceStr.ToCharArray();
                int lineCount = 1;

                foreach (char c in deviceArr)
                {
                    if (c == '\n')
                        lineCount++;
                }

                int lineIdx = 0;
                historyUrl[lineIdx] = null;

                foreach (char c in deviceArr)
                {
                    switch (c)
                    {
                        case '\n':
                            lineIdx++;

                            if (lineIdx < lineCount)
                                historyUrl[lineIdx] = null;
                            break;

                        default:
                            if (lineIdx < lineCount)
                                historyUrl[lineIdx] += c.ToString();
                            break;
                    }
                }

                cb_Url.Items.Clear();
                bool _1st = true;

                foreach (string s in historyUrl)
                {
                    if (s != null)
                    {
                        if (_1st == true)
                        {
                            _1st = false;
                            tb_Md5.Text = s;
                        }
                        else
                            cb_Url.Items.Add(s);
                    }
                }

                cb_Url.SelectedIndex = 0;
            }

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
            if (threadCxt.Task != null)
            {
                if (threadCxt.Task.IsAlive)
                {
                    threadCxt.Enbale = 0;
                    threadCxt.Task.Abort();
                    threadCxt.Task.Interrupt();
                    threadCxt.Task.Abort();
                    threadCxt.Task.Join();
                    while (threadCxt.Task.IsAlive) ;
                }
            }

            StreamWriter sW = new StreamWriter("HistoryCMD.txt");
            bool _1st = true;

            foreach (string s in historyCmd)
            {
                if (s != null)
                {
                    if (_1st)
                    {
                        _1st = false;
                        sW.Write(s);
                    }
                    else
                        sW.Write("\n" + s);
                }
            }

            sW.Close();
            Log_Deinit();
        }
        #endregion

        #region "Button functions"
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

            Log_Deinit();
            bt_Download.Enabled = true;
            bt_Reboot.Enabled = true;
            bt_Scan.Enabled = true;
            bt_CMD.Enabled = true;
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
            bt_CMD.Enabled = true;
            bt_Reboot.Enabled = true;
            cb_Apn.Enabled = true;
            cb_Dns.Enabled = true;
            cb_Url.Enabled = true;
            tb_Md5.Enabled = true;
            bt_Download.Text = "Download";
        }

        private void bt_Download_Click(object sender, EventArgs e)
        {
            try
            {
                if (bt_Download.Text == "Download")
                {
                    tabCtrl1.SelectedTab = tabCtrl1.TabPages["tabLog"];
                    bt_RFTest.Enabled = false;
                    bt_Scan.Enabled = false;
                    bt_Reboot.Enabled = false;
                    bt_CMD.Enabled = false;
                    cb_Apn.Enabled = false;
                    cb_Dns.Enabled = false;
                    cb_Url.Enabled = false;
                    tb_Md5.Enabled = false;
                    bt_Download.Text = "Stop";
                    pgb_Percent.Value = 0;
                    lb_Percent.Text = "0 byte";
                    Graph_Init();
                    Log_Init();
                    Thread_Init(ThreadMode.DOWNLOAD, 10, 250);
                }
                else
                {
                    threadCxt.Enbale = 0;
                    Log_Deinit();
                }
            }
            catch { }
        }

        private void bt_RFTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (bt_RFTest.Text == "RF Test")
                {
                    tabCtrl1.SelectedTab = tabCtrl1.TabPages["tabGraph"];
                    bt_Download.Enabled = false;
                    bt_Reboot.Enabled = false;
                    bt_Scan.Enabled = false;
                    bt_CMD.Enabled = false;
                    cb_Apn.Enabled = false;
                    cb_Dns.Enabled = false;
                    bt_RFTest.Text = "Stop";
                    Log_Init();
                    Graph_Init();
                    Thread_Init(ThreadMode.RF_TEST, 60, 1000);
                }
                else
                {
                    threadCxt.Enbale = 0;
                    Log_Deinit();
                }
            }
            catch { }
        }

        private void bt_Reboot_Click(object sender, EventArgs e)
        {
            tabCtrl1.SelectedTab = tabCtrl1.TabPages["tabLog"];
            bt_Download.Enabled = false;
            bt_Reboot.Enabled = false;
            bt_Scan.Enabled = false;
            bt_CMD.Enabled = false;
            cb_Apn.Enabled = false;
            cb_Dns.Enabled = false;

            if (SendAT(cb_Port1.Text, "AT#REBOOT\r"))
            {
                PrintDebug("\n\nModule is rebooting...\n\n");
                InfoAppendText("\n\nModule is rebooting...\n\n");
                RebootTryCount = 0;
                timer1.Interval = 5000;
                timer1.Start();
            }
            else
            {
                bt_Download.Enabled = true;
                bt_Scan.Enabled = true;
                bt_CMD.Enabled = true;
                cb_Apn.Enabled = true;
                cb_Dns.Enabled = true;
                PrintDebug("\n\nCan not reboot\n\n", Color.Red);
                InfoAppendText("\n\nCan not reboot\n\n", Color.Red);
            }
        }

        private void bt_CMD_Click(object sender, EventArgs e)
        {
            tabCtrl1.SelectedTab = tabCtrl1.TabPages["tabLog"];
            bt_RFTest.Enabled = false;
            bt_Download.Enabled = false;
            bt_Reboot.Enabled = false;
            bt_Scan.Enabled = false;
            bt_CMD.Enabled = false;
            cb_Apn.Enabled = false;
            cb_Dns.Enabled = false;

            string cmdStr = cb_CMD.Text;

            if (cmdStr != null)
            {
                bool found = false;

                foreach (string s in historyCmd)
                {
                    if (s == cmdStr)
                    {
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    for (int i = 9; i > 0; i--)
                        historyCmd[i] = historyCmd[i - 1];

                    historyCmd[0] = cmdStr;
                }

                cb_CMD.Items.Clear();

                foreach (string s in historyCmd)
                {
                    if (s != null)
                        cb_CMD.Items.Add(s);
                }

                cmdStr = ChangeSpecialByte(cmdStr);
                CmdCxt cmdCxt = new CmdCxt();
                cmdCxt.buffer = new char[4096];
                cmdCxt.donext = 0;
                cmdCxt.findIdx = 0;

                try
                {
                    if (serialPort1.IsOpen == false)
                    {
                        serialPort1.PortName = portName;
                        serialPort1.BaudRate = 115200;
                        serialPort1.WriteTimeout = 10;
                        serialPort1.DtrEnable = true;
                        serialPort1.RtsEnable = true;
                        serialPort1.Open();

                        while (SendCmd_GetRes(ref cmdCxt, cmdStr, respWait, respWait, true) == null) ;

                        serialPort1.DtrEnable = false;
                        serialPort1.RtsEnable = false;
                        serialPort1.Close();
                        serialPort1.Dispose();
                    }
                }
                catch
                {
                    PrintDebug("\nSend CMD error");
                }
            }

            bt_RFTest.Enabled = true;
            bt_Download.Enabled = true;
            bt_Scan.Enabled = true;
            bt_Reboot.Enabled = true;
            bt_CMD.Enabled = true;
            cb_Apn.Enabled = true;
            cb_Dns.Enabled = true;
        }

        #endregion

        #region "Combobox functions"
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

        private void cb_Url_TextChanged(object sender, EventArgs e)
        {
            if (LoadUrl(true))
            {
                /*
                string cmdStr = cb_Url.Text;

                if (cmdStr != null)
                {
                    bool found = false;

                    foreach (string s in historyUrl)
                    {
                        if (s == cmdStr)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found == false)
                    {
                        for (int i = 9; i > 0; i--)
                            historyUrl[i] = historyUrl[i - 1];

                        historyUrl[0] = cmdStr;
                    }

                    cb_Url.Items.Clear();

                    foreach (string s in historyUrl)
                    {
                        if (s != null)
                            cb_Url.Items.Add(s);
                    }
                }
                */
            }
        }

        private void cb_Port1_TextChanged(object sender, EventArgs e)
        {
            if (cb_Port1.Text != "Empty")
            {
                bt_Download.Enabled = true;
                bt_Reboot.Enabled = true;
                bt_RFTest.Enabled = true;
                bt_CMD.Enabled = true;
            }
            else
            {
                bt_Download.Enabled = false;
                bt_Reboot.Enabled = false;
                bt_RFTest.Enabled = false;
                bt_CMD.Enabled = false;
            }
        }

        private void cb_Apn_TextChanged(object sender, EventArgs e)
        {
            moduleInfo.Apn = cb_Apn.Text;
        }

        private void cb_Dns_TextChanged(object sender, EventArgs e)
        {
            moduleInfo.UserDns = cb_Dns.Text;
        }
        #endregion

        #region "Text functions"
        private void rtb_Log_TextChanged(object sender, EventArgs e)
        {
            rtb_Log.SelectionStart = rtb_Log.Text.Length;
            rtb_Log.ScrollToCaret();

            if (line >= 1000)
                Log_Init();
        }

        private void rtb_Log_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sW != null)
                Log_Init();

            rtb_Log.Clear();
        }

        private void tb_Md5_TextChanged(object sender, EventArgs e)
        {
            downloadCxt.Md5 = tb_Md5.Text;
        }
        #endregion

        #region "Graph functions"
        private void Graph_Init()
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
        #endregion

        #region "Checkbox functions"
        private void ckb_DebugEn_CheckedChanged(object sender, EventArgs e)
        {
            debugEn = ckb_DebugEn.Checked;
        }

        private void ckb_Printable_CheckedChanged(object sender, EventArgs e)
        {
            printableMode = ckb_Printable.Checked;
        }

        private void ckb_DlLoop_CheckedChanged(object sender, EventArgs e)
        {
            downloadCxt.Loop = ckb_DlLoop.Checked;
        }
        #endregion

        #region "Thread functions"
        private void Thread_Init(ThreadMode mode, int rfTestCount, UInt32 rfTestDelay)
        {
            threadCxt.Mode = mode;
            threadCxt.Enbale = 1;
            rfCxt.Count = rfTestCount;
            rfCxt.Delay = rfTestDelay;
            threadCxt.Task = new Thread(() => Thread_Tasks()); // Create new app tasks
            threadCxt.Task.Start();
        }

        private void Thread_Tasks()
        {
            double lon = -999.0, lat = -999.0;
            string tmpStr = null;
            char[] tmpArr = null;
            UInt32 DownloadTime = 0;
            CmdCxt cmdCxt = new CmdCxt();
            int failCount = 0;
            int tryCount = 0;

            rfCxt.Rssi = new SignalCxt
            {
                sum = 0,
                cout = 0
            };

            rfCxt.Rsrp = new SignalCxt
            {
                sum = 0,
                cout = 0
            };

            rfCxt.Rsrq = new SignalCxt
            {
                sum = 0,
                cout = 0
            };

            downloadCxt.Count = 0;

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

            threadCxt.DoNext = ThreadTask.INIT_APP;
            threadCxt.ToDo = ThreadTask.INIT_APP;

            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
            UInt32 thisTick = rebootWait + Tick_Get();

            InfoWriteText("Reading module info... ");

            while (threadCxt.Enbale == 1)
            {
                switch (threadCxt.DoNext)
                {
                    case ThreadTask.INIT_APP:
                        {
                            if (Tick_IsOverMs(ref thisTick, rebootWait))
                            {
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

                                    if (threadCxt.ToDo == ThreadTask.CMD_MODULE_REBOOT)
                                    {
                                        PrintDebug(" Done\n");
                                        InfoAppendText("Done\n");
                                    }

                                    thisTick = Tick_Get();
                                    threadCxt.DoNext++;
                                }
                                catch
                                {
                                    if (++tryCount > 4)
                                        threadCxt.Enbale = 0;
                                }
                            }
                            else
                                Thread.Sleep(250);
                        }
                        break;

                    case ThreadTask.CMD_ECHO_OFF:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "ATE0\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                    threadCxt.DoNext++;
                                else
                                {
                                    threadCxt.DoNext = ThreadTask.INIT_APP;
                                    threadCxt.ToDo = ThreadTask.CMD_MODULE_REBOOT;
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_DISPLAY_ERROR:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CMEE=2\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n") || tmpStr.Contains("ERROR"))
                                    threadCxt.DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.CMD_NO_FLOW_CONTROL:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT&K0\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                    threadCxt.DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_MODULE_NAME:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGMM\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    tmpStr = new string(SubArray(cmdCxt.buffer, "+CGMM: ", "\r\nOK"));
                                    moduleInfo.Name = RemoveAT(tmpStr);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_MODULE_IMEI:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGSN\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    moduleInfo.Imei = RemoveAT(tmpStr);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CIMI:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CIMI\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext = ThreadTask.CMD_GET_CCID;
                                    tmpStr = new string(SubArray(cmdCxt.buffer, "+CIMI: ", "\r\nOK"));
                                    moduleInfo.Cimi = RemoveAT(tmpStr);
                                }
                                else if (tmpStr.Contains("ERROR"))
                                    threadCxt.DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_ALT_CIMI:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#CIMI\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    tmpStr = new string(SubArray(cmdCxt.buffer, "#CIMI: ", "\r\nOK"));
                                    moduleInfo.Cimi = RemoveAT(tmpStr);
                                }
                                else if (tmpStr.Contains("ERROR"))
                                    threadCxt.DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CCID:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CCID\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext += 2;
                                    tmpStr = new string(SubArray(cmdCxt.buffer, "+CCID: ", "\r\nOK"));
                                    moduleInfo.Ccid = RemoveAT(tmpStr);
                                }
                                else if (tmpStr.Contains("ERROR"))
                                    threadCxt.DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_ALT_CCID:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#CCID\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    tmpStr = new string(SubArray(cmdCxt.buffer, "#CCID: ", "\r\nOK"));
                                    moduleInfo.Ccid = RemoveAT(tmpStr);
                                }
                                else if (tmpStr.Contains("ERROR"))
                                    threadCxt.DoNext++;
                                else
                                {
                                    threadCxt.DoNext--;
                                    Thread.Sleep(250);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_NETWORK_REGIS:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CEREG?\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    tmpStr = new string(SubArray(cmdCxt.buffer, ",", "\r\nOK"));
                                    tmpStr = RemoveAT(tmpStr);

                                    if (tmpStr == "1")
                                    {
                                        threadCxt.DoNext++;
                                        break;
                                    }
                                }

                                Thread.Sleep(250);
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_OPERATOR_INFO:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+COPS?\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    TickStart = 0;
                                    moduleInfo.Operator = new string(SubArray(cmdCxt.buffer, ",\"", "\","));
                                    moduleInfo.NetworkType = new string(SubArray(cmdCxt.buffer, "\",", "\r\nOK"));
                                    ReplaceStr(ref moduleInfo.NetworkType, ' ', '\0');
                                    ReplaceStr(ref moduleInfo.NetworkType, '\r', '\0');
                                    ReplaceStr(ref moduleInfo.NetworkType, '\n', '\0');
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_DEACT_PDP:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SGACT=1,0\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                    threadCxt.DoNext++;
                                else if (tmpStr.Contains("ERROR") || tmpStr.Contains("RX TIMEOUT"))
                                    threadCxt.DoNext = ThreadTask.CMD_SET_APN;
                            }
                        }
                        break;

                    case ThreadTask.CMD_SET_APN:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGDCONT=1,\"IP\",\"" + moduleInfo.Apn + "\"\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                    threadCxt.DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.CMD_SET_DNS:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#DNS=1," + moduleInfo.UserDns + "\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n") || tmpStr.Contains("ERROR"))
                                    threadCxt.DoNext++;
                            }
                        }
                        break;

                    case ThreadTask.CMD_ACT_PDP:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SGACT=1,1\r", 30000);

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    tmpStr = new string(SubArray(cmdCxt.buffer, "#SGACT: ", "\r\nOK"));
                                    moduleInfo.Ip = RemoveAT(tmpStr);
                                }
                                else if (tmpStr.Contains("ERROR") || tmpStr.Contains("RX TIMEOUT"))
                                {
                                    threadCxt.DoNext = ThreadTask.CMD_GET_CURRENT_PDP;
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CURRENT_PDP:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CGCONTRDP=1\r", 10000);

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
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
                                    threadCxt.DoNext++;
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_SETUP_CELL_INFO:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#MONI=0\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    thisTick = Tick_Get();
                                }
                                else if (tmpStr.Contains("ERROR"))
                                {
                                    threadCxt.DoNext++;
                                }
                            }
                        }
                        break;

                    case ThreadTask.GET_LOCATION:
                        {
                            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

                            GeoCoordinate coord = watcher.Position.Location;

                            if (coord.IsUnknown != true)
                            {
                                lon = coord.Longitude;
                                lat = coord.Latitude;
                            }

                            threadCxt.DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_GET_SIGNAL_QUALITY:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT+CSQ\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    moduleInfo.Csq = new string(SubArray(cmdCxt.buffer, "+CSQ: ", ","));
                                    moduleInfo.BitErrRate = new string(SubArray(cmdCxt.buffer, ",", "\r\nOK"));
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CELL_INFO:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#MONI\r");

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;

                                    if (moduleInfo.Operator == null)
                                        moduleInfo.Operator = new string(SubArray(cmdCxt.buffer, "#MONI: ", " RSRP:"));

                                    moduleInfo.Rsrp = new string(SubArray(cmdCxt.buffer, "RSRP:", " RSRQ:"));
                                    moduleInfo.Rsrq = new string(SubArray(cmdCxt.buffer, "RSRQ:", " TAC:"));
                                    moduleInfo.Tac = new string(SubArray(cmdCxt.buffer, "TAC:", " Id:"));
                                    moduleInfo.CellID = new string(SubArray(cmdCxt.buffer, " Id:", " EARFCN:"));
                                }
                                else if (tmpStr.Contains("ERROR"))
                                {
                                    threadCxt.DoNext++;
                                }
                            }
                        }
                        break;

                    case ThreadTask.PARSE_INFO:
                        {
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

                            int csq = 99;

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
                                    rfCxt.Rssi.value = -113;
                                    break;

                                case 1:
                                    rfCxt.Rssi.value = -111;
                                    break;

                                case 31:
                                    rfCxt.Rssi.value = -51;
                                    break;

                                case 99:
                                    rfCxt.Rssi.value = -150;
                                    break;

                                default:
                                    rfCxt.Rssi.value = 2 * csq - 113;
                                    break;
                            }

                            if (csq != 99)
                            {
                                rfCxt.Rssi.sum += rfCxt.Rssi.value;
                                rfCxt.Rssi.cout++;
                                rfCxt.Rssi.average = (int)(rfCxt.Rssi.sum / (long)rfCxt.Rssi.cout);
                            }

                            if (rfCxt.Rssi.average > (-60))
                                InfoAppendText(rfCxt.Rssi.average.ToString() + "dBm (Excellent)", Color.Violet);
                            else if (rfCxt.Rssi.average > (-70))
                                InfoAppendText(rfCxt.Rssi.average.ToString() + "dBm (Good)", Color.Blue);
                            else if (rfCxt.Rssi.average > (-80))
                                InfoAppendText(rfCxt.Rssi.average.ToString() + "dBm (Normal)", Color.Green);
                            else if (rfCxt.Rssi.average > (-90))
                                InfoAppendText(rfCxt.Rssi.average.ToString() + "dBm (Low)", Color.Orange);
                            else if (rfCxt.Rssi.average > (-100))
                                InfoAppendText(rfCxt.Rssi.average.ToString() + "dBm (Very low)", Color.OrangeRed);
                            else if (csq != 99)
                                InfoAppendText(rfCxt.Rssi.average.ToString() + "dBm (No signal)", Color.Red);
                            else
                                InfoAppendText("Unknown", Color.Red);

                            InfoAppendText("\nRSRP:            ");

                            try
                            {
                                rfCxt.Rsrp.value = int.Parse(moduleInfo.Rsrp);
                                rfCxt.Rsrp.sum += rfCxt.Rsrp.value;
                                rfCxt.Rsrp.cout++;
                                rfCxt.Rsrp.average = (int)(rfCxt.Rsrp.sum / (long)rfCxt.Rsrp.cout);

                                if (rfCxt.Rsrp.average >= -70)
                                    InfoAppendText(rfCxt.Rsrp.average.ToString() + "dBm (Excellent)", Color.Violet);
                                else if (rfCxt.Rsrp.average >= -80)
                                    InfoAppendText(rfCxt.Rsrp.average.ToString() + "dBm (Good)", Color.Blue);
                                else if (rfCxt.Rsrp.average >= -90)
                                    InfoAppendText(rfCxt.Rsrp.average.ToString() + "dBm (Normal)", Color.Green);
                                else if (rfCxt.Rsrp.average >= -100)
                                    InfoAppendText(rfCxt.Rsrp.average.ToString() + "dBm (Low)", Color.Orange);
                                else if (rfCxt.Rsrp.average >= -110)
                                    InfoAppendText(rfCxt.Rsrp.average.ToString() + "dBm (Very low)", Color.OrangeRed);
                                else
                                    InfoAppendText(rfCxt.Rsrp.average.ToString() + "dBm (No signal)", Color.Red);
                            }
                            catch
                            {
                                InfoAppendText("Unknown", Color.Red);
                            }

                            InfoAppendText("\nRSRQ:            ");

                            try
                            {
                                rfCxt.Rsrq.value = int.Parse(moduleInfo.Rsrq);
                                rfCxt.Rsrq.sum += rfCxt.Rsrq.value;
                                rfCxt.Rsrq.cout++;
                                rfCxt.Rsrq.average = (int)(rfCxt.Rsrq.sum / (long)rfCxt.Rsrq.cout);

                                if (rfCxt.Rsrq.average >= -8)
                                    InfoAppendText(rfCxt.Rsrq.average.ToString() + "dB (Excellent)", Color.Violet);
                                else if (rfCxt.Rsrq.average > -10)
                                    InfoAppendText(rfCxt.Rsrq.average.ToString() + "dB (Good)", Color.Blue);
                                else if (rfCxt.Rsrq.average > -15)
                                    InfoAppendText(rfCxt.Rsrq.average.ToString() + "dB (Normal)", Color.Green);
                                else if (rfCxt.Rsrq.average > -18)
                                    InfoAppendText(rfCxt.Rsrq.average.ToString() + "dB (Low)", Color.Orange);
                                else if (rfCxt.Rsrq.average > -20)
                                    InfoAppendText(rfCxt.Rsrq.average.ToString() + "dB (Very low)", Color.OrangeRed);
                                else
                                    InfoAppendText(rfCxt.Rsrq.average.ToString() + "dB (No signal)", Color.Red);
                            }
                            catch
                            {
                                InfoAppendText("Unknown", Color.Red);
                            }

                            if ((lat < (-999.0)) || (lon < (-999.0)))
                            {
                                tmpStr = "\nLocation:        " + lat.ToString("0.000000") + "," + lon.ToString("0.000000");
                                InfoAppendText(tmpStr);
                            }

                            WriteLogFile(rfCxt.Rsrp.value, rfCxt.Rsrq.value, rfCxt.Rssi.value, lat, lon);
                            PlotData(rfCxt.Rsrp.value, rfCxt.Rsrq.value, rfCxt.Rssi.value, TickStart++);

                            if (rfCxt.Count > 0)
                            {
                                UInt32 t = Tick_DifMs(thisTick);

                                if ((t > 0) && (t < rfCxt.Delay))
                                    Thread.Sleep((int)(rfCxt.Delay - t));

                                rfCxt.Count--;
                                thisTick = Tick_Get();
                                threadCxt.DoNext = ThreadTask.GET_LOCATION;
                            }
                            else if (threadCxt.Mode == ThreadMode.RF_TEST)
                                threadCxt.DoNext = ThreadTask.CLOSE_APP;
                            else
                            {
                                if (moduleInfo.Ip != null)
                                    threadCxt.DoNext++;
                                else
                                {
                                    threadCxt.DoNext = ThreadTask.CLOSE_APP;
                                    InfoAppendText("\n\nCan not access the internet", Color.Red);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_PING:
                        {
                            threadCxt.DoNext++;
                            downloadCxt.Count++;

                            string s = "\n\nDownload: " + downloadCxt.Count.ToString();

                            if (downloadCxt.Count > 1)
                                s += "\nPass: " + (downloadCxt.Count - failCount - 1).ToString();

                            InfoAppendText(s);
                            PrintDebug(s);
                        }
                        break;

                    case ThreadTask.CMD_CFG_HTTPS_HOST:
                        {
                            if (downloadCxt.SslEn == true)
                                tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPCFG=0,\"" + downloadCxt.Host + "\"," + downloadCxt.Port + ",0,,,1,30,1\r", 60000);
                            else
                                tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPCFG=0,\"" + downloadCxt.Host + "\"," + downloadCxt.Port + ",0,,,0,30,1\r", 60000);

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext++;
                                    InfoAppendText("\n\nConnected to host " + downloadCxt.Host, Color.Blue);
                                }
                                else if (tmpStr.Contains("ERROR"))
                                {
                                    failCount++;
                                    threadCxt.DoNext = ThreadTask.CMD_CLOSE_SOCKET;

                                    if (downloadCxt.Loop == true)
                                        threadCxt.ToDo = ThreadTask.CMD_MODULE_REBOOT;
                                    else
                                        threadCxt.ToDo = ThreadTask.CMD_RF_OFF;

                                    InfoAppendText("\n\nCan not connect to host " + downloadCxt.Host, Color.Red);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_CFG_HTTPS_FILE:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#HTTPQRY=0,0,\"" + downloadCxt.FilePath + "\"\r", 60000);

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    threadCxt.DoNext = ThreadTask.HTTPS_GET_FILE_INFO;
                                    InfoAppendText("\nGet info of file " + downloadCxt.FileName, Color.Blue);
                                }
                                else
                                {
                                    threadCxt.DoNext++;
                                    failCount++;
                                    InfoAppendText("\nCan not get info of file " + downloadCxt.FileName, Color.Red);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_CURRENT_IP:
                        {
                            threadCxt.DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_GET_CURRENT_DNS:
                        {
                            threadCxt.DoNext = ThreadTask.CMD_CLOSE_SOCKET;

                            if (downloadCxt.Loop == true)
                                threadCxt.ToDo = ThreadTask.CMD_MODULE_REBOOT;
                            else
                                threadCxt.ToDo = ThreadTask.CMD_RF_OFF;
                        }
                        break;

                    case ThreadTask.HTTPS_GET_FILE_INFO:
                        {
                            tmpStr = Get_ExtResp(ref cmdCxt, null, 60000);

                            if (tmpStr != null)
                            {
                                threadCxt.DoNext = ThreadTask.CMD_CLOSE_SOCKET;

                                if (downloadCxt.Loop == true)
                                    threadCxt.ToDo = ThreadTask.CMD_MODULE_REBOOT;
                                else
                                    threadCxt.ToDo = ThreadTask.CMD_RF_OFF;

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
                                        failCount++;
                                        downloadCxt.sW = null;
                                        InfoAppendText("\nFile size = 0, error", Color.Red); ;
                                    }
                                    else
                                    {
                                        threadCxt.DoNext = ThreadTask.CMD_GET_HTTPS_1500;
                                        DownloadTime = 0;
                                        downloadCxt.DownloadedSize = 0;
                                        ProgressBar_Update(downloadCxt.FileSize, downloadCxt.DownloadedSize);
                                        var stream = File.Open(downloadCxt.FileName, FileMode.Create);
                                        downloadCxt.sW = new BinaryWriter(stream);
                                        InfoAppendText("\nFile size = " + downloadCxt.FileSize + " byte(s)", Color.Blue);
                                    }
                                }
                                else
                                {
                                    failCount++;
                                    InfoAppendText("\nResponse error: " + tmpStr, Color.Red);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_GET_HTTPS_1500:
                        {
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
                                        threadCxt.DoNext = ThreadTask.CMD_CLOSE_SOCKET;

                                        if (downloadCxt.Loop == true)
                                            threadCxt.ToDo = ThreadTask.CMD_MODULE_REBOOT;
                                        else
                                            threadCxt.ToDo = ThreadTask.CLOSE_APP;

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
                                            failCount++;
                                            md5_result = "MD5: " + md5_result + " incorrect";
                                            InfoAppendText("\n" + md5_result, Color.Red);
                                        }

                                        //MessageBox.Show(md5_result, "Info");
                                    }
                                }
                                else if (tmpStr.Contains("ERROR") || tmpStr.Contains("RX TIMEOUT"))
                                {
                                    failCount++;
                                    threadCxt.DoNext = ThreadTask.CMD_CLOSE_SOCKET;

                                    if (downloadCxt.Loop == true)
                                        threadCxt.ToDo = ThreadTask.CMD_MODULE_REBOOT;
                                    else
                                        threadCxt.ToDo = ThreadTask.CMD_RF_OFF;

                                    InfoAppendText("\n\nDownload fail", Color.Red);
                                }
                            }
                        }
                        break;

                    case ThreadTask.CMD_CLOSE_SOCKET:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#SH=1\r", 3000);

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                    threadCxt.DoNext = threadCxt.ToDo;
                                else
                                    threadCxt.DoNext++;

                                string msg = "\nPass: " + (downloadCxt.Count - failCount).ToString() + "/" + downloadCxt.Count.ToString();
                                PrintDebug(msg);
                                InfoAppendText(msg);
                            }
                        }
                        break;

                    case ThreadTask.CMD_RF_OFF:
                        {
                            threadCxt.DoNext++;
                        }
                        break;

                    case ThreadTask.CMD_RF_ON:
                        {
                            threadCxt.DoNext = ThreadTask.CLOSE_APP;
                        }
                        break;

                    case ThreadTask.CMD_MODULE_REBOOT:
                        {
                            tmpStr = SendCmd_GetRes(ref cmdCxt, "AT#REBOOT\r", 3000);

                            if (tmpStr != null)
                            {
                                if (tmpStr.Contains("\r\nOK\r\n"))
                                {
                                    PrintDebug("\n\nModule is rebooting...\n\n");
                                    InfoAppendText("\n\nModule is rebooting...\n\n");

                                    if (downloadCxt.Loop == true)
                                    {
                                        serialPort1.DtrEnable = false;
                                        serialPort1.RtsEnable = false;
                                        serialPort1.Close();
                                        serialPort1.Dispose();
                                        threadCxt.DoNext = ThreadTask.INIT_APP;
                                        threadCxt.ToDo = ThreadTask.CMD_MODULE_REBOOT;
                                        tryCount = 0;
                                        thisTick = Tick_Get();
                                        //PrintDebug("\nNew download...\n");
                                    }
                                    else
                                        threadCxt.DoNext = ThreadTask.CLOSE_APP;
                                }
                                else
                                {
                                    PrintDebug("\n\nCan not reboot\n\n", Color.Red);
                                    InfoAppendText("\n\nCan not reboot\n\n", Color.Red);
                                    threadCxt.DoNext = ThreadTask.CLOSE_APP;
                                }
                            }
                        }
                        break;

                    case ThreadTask.CLOSE_APP:
                    default:
                        {
                            if (downloadCxt.sW != null)
                                downloadCxt.sW.Close();

                            threadCxt.Enbale = 0;
                        }
                        break;
                }
            }

            serialPort1.DtrEnable = false;
            serialPort1.RtsEnable = false;
            serialPort1.Close();
            serialPort1.Dispose();

            if (threadCxt.Mode == ThreadMode.RF_TEST)
                bt_RFTest_Update(null);
            else
                bt_Download_Update(null);

            threadCxt.Task.Abort();
            threadCxt.Task.Interrupt();
            threadCxt.Task.Abort();
            threadCxt.Task.Join();
        }
        #endregion

        #region "Numeric Up/Down"
        private void nud_RebootWait_ValueChanged(object sender, EventArgs e)
        {
            rebootWait = (UInt32)nud_RebootWait.Value;
        }

        private void nud_RespWait_ValueChanged(object sender, EventArgs e)
        {
            respWait = (UInt32)nud_RespWait.Value;
        }
        #endregion

        #region "Other functions"
        private bool LoadUrl(bool online)
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
                    return false;
                }

                //MessageBox.Show("Port: " + downloadCxt.Port, "Info");
                downloadCxt.Host = myUri.Host;
                downloadCxt.FilePath = myUri.AbsolutePath;
                downloadCxt.FileName = Path.GetFileName(downloadCxt.FilePath);

                if (downloadCxt.FileName == null)
                {
                    MessageBox.Show("No file name", "Error");
                    return false;
                }

                if (downloadCxt.FilePath == null)
                {
                    MessageBox.Show("No file path", "Error");
                    return false;
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
                            //MessageBox.Show("MD5: " + md5_result, "Info");
                            return true;
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

            return false;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            RebootTryCount += 5;

            if (SendAT(cb_Port1.Text, "ATE0\r"))
            {
                timer1.Stop();
                bt_Reboot.Enabled = true;
                bt_Download.Enabled = true;
                bt_Scan.Enabled = true;
                bt_CMD.Enabled = true;
                cb_Apn.Enabled = true;
                cb_Dns.Enabled = true;
                PrintDebug(" Done\n");
                InfoAppendText("Done\n");
            }
            else if (RebootTryCount >= 60)
            {
                timer1.Stop();
                bt_Reboot.Enabled = true;
                bt_Download.Enabled = true;
                bt_Scan.Enabled = true;
                bt_CMD.Enabled = true;
                cb_Apn.Enabled = true;
                cb_Dns.Enabled = true;
                Scan_AT_Port();
                PrintDebug("Failed\n", Color.Red);
                InfoAppendText("Failed\n", Color.Red);
            }
        }
        #endregion
    }
}
