using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.Threading;
using System.Reflection;

namespace SystemNetSpeedTest
{
    public partial class Form1 : Form
    {
        private StreamWriter sW = null;
        private string fileName = null;
        private long LastByteRx = 0;
        private long LastByteTx = 0;
        private long line = 0;

        public Form1()
        {
            InitializeComponent();
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

        private string FileNameGenerate(string prefix)
        {
            string s = strRemove(prefix + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString());

            return s;
        }

        private void WriteLogFile(double txSpeed, double rxSpeed)
        {
            if (sW != null)
            {
                line++;

                string s = line.ToString() + " " + txSpeed.ToString("0.000") + " " + rxSpeed.ToString("0.000");
                
                sW.WriteLine(s);
                rtb_Log.AppendText("\n" + line.ToString("D4") + " ");
                rtb_Log.AppendText(DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString() + " U/D=");
                rtb_Log.AppendText(txSpeed.ToString("0.000") + "/" + rxSpeed.ToString("0.000") + " Mbps");
            }
        }

        private void CloseLogFile()
        {
            if (sW != null)
            {
                StreamWriter slog = new StreamWriter(fileName + ".txt");

                slog.WriteLine(rtb_Log.Text);
                slog.Close();

                if (File.Exists("footer.txt"))
                {
                    StreamReader sR = new StreamReader("footer.txt");

                    sW.WriteLine(sR.ReadToEnd());
                    sR.Close();
                }
                else
                    sW.WriteLine(SystemNetSpeedTest.Properties.Resources.footer);

                sW.Close();
                rtb_Log.AppendText("\nLog file " + fileName + ".m has been saved");
                Thread.Sleep(1000);
                rtb_Log.Clear();
            }
        }

        private void OpenLogFile()
        {
            timer1.Stop();
            CloseLogFile();
            fileName = FileNameGenerate("SNST_" + cbx_NetCard.Text + "_");

            try
            {
                sW = new StreamWriter(fileName + ".m");

                if (File.Exists("header.txt"))
                {
                    StreamReader sR = new StreamReader("header.txt");

                    sW.WriteLine(sR.ReadToEnd());
                    sR.Close();
                }
                else
                    sW.WriteLine(SystemNetSpeedTest.Properties.Resources.header);

                rtb_Log.Text = "New log file " + fileName + ".m has been created";
                line = 5;
                timer1.Start();
            }
            catch(Exception e)
            {
                rtb_Log.AppendText("\nFile name \"" + fileName + "\"error");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Description == cbx_NetCard.Text)
                {
                    double RxSpeed, TxSpeed;
                    var ipv4Info = adapter.GetIPv4Statistics();

                    RxSpeed = (double)((ipv4Info.BytesReceived - LastByteRx) * 8); // byte to bit
                    RxSpeed /= 1024.0f; // convert to kbit
                    RxSpeed /= 1024.0f; // convert to Mbit
                    LastByteRx = ipv4Info.BytesReceived;

                    TxSpeed = (double)((ipv4Info.BytesSent - LastByteTx) * 8); // byte to bit
                    TxSpeed /= 1024.0f; // convert to kbit
                    TxSpeed /= 1024.0f; // convert to Mbit
                    LastByteTx = ipv4Info.BytesSent;

                    WriteLogFile(TxSpeed, RxSpeed);
                }
            }
        }

        private void Load_NetCard()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            cbx_NetCard.Items.Clear();

            if (adapters != null)
            {
                foreach (NetworkInterface adapter in adapters)
                {
                    cbx_NetCard.Items.Add(adapter.Description);
                }
            }
            else
                cbx_NetCard.Items.Add("No network card found");

            cbx_NetCard.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text += " v." + $"{version}";
            this.Update();
            Load_NetCard();
        }

        private void cbx_NetCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Description == cbx_NetCard.Text)
                {
                    OpenLogFile();
                    var ipv4Info = adapter.GetIPv4Statistics();
                    LastByteRx = ipv4Info.BytesReceived;
                    LastByteTx = ipv4Info.BytesSent;
                }
            }
        }

        private void rtb_Log_TextChanged(object sender, EventArgs e)
        {
            rtb_Log.SelectionStart = rtb_Log.Text.Length;
            rtb_Log.ScrollToCaret();

            if (line >= 3600)
                OpenLogFile();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseLogFile();
        }
    }
}
