using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace RTChart
{
    public partial class Form1 : Form
    {
        private bool PlotEna = false;
        private int Baud = 9600;
        private int BaudIdx = 0;
        private string PortName = null;
        private string Directory = null;
        private byte Header = 52;
        private byte HeaderBk = 52;
        private byte Suffing = 203;

        public Form1()
        {
            InitializeComponent();
        }

        private void bt_Import_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Directory = openFileDialog1.FileName;
                tb_Directory.Text = Directory;
                bt_Plot.Enabled = true;

                StreamReader streamReader = new StreamReader(Directory);

                BaudIdx = int.Parse(streamReader.ReadLine());
                cbx_Baud.SelectedIndex = BaudIdx;

                Header = byte.Parse(streamReader.ReadLine());
                HeaderBk = Header;

                Suffing = (byte)((int)Header ^ 0xFF);

                streamReader.Close();
            }
        }

        private void bt_Plot_Click(object sender, EventArgs e)
        {
            if (PlotEna == false)
            {
                try
                {
                    if (serialPort1.IsOpen)
                        serialPort1.Close();

                    serialPort1.PortName = PortName;
                    serialPort1.BaudRate = Baud;
                    serialPort1.Open();
                    bt_Plot.Image = RTChart.Properties.Resources.On;
                    bt_Plot.Text = "Chart On";
                    PlotEna = true;
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
            else
            {
                try
                {
                    if (serialPort1.IsOpen)
                        serialPort1.Close();

                    bt_Plot.Image = RTChart.Properties.Resources.Off;
                    bt_Plot.Text = "Chart Off";
                    PlotEna = false;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void bt_Options_Click(object sender, EventArgs e)
        {

        }

        private void cbx_Ports_MouseClick(object sender, MouseEventArgs e)
        {
            int found = 0;
            string[] ports = SerialPort.GetPortNames();

            cbx_Ports.Items.Clear();
            cbx_Ports.Items.Add("COM Port");

            foreach (string port in ports)
            {
                found++;
                cbx_Ports.Items.Add(port);
            }

            if (found > 0)
            {
                cbx_Ports.SelectedIndex = 1;
                PortName = cbx_Ports.Text;
            }
            else
            {
                MessageBox.Show("There is no COM Port");
                cbx_Ports.SelectedIndex = 0;
                PortName = null;
            }
        }

        private void cbx_Ports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_Ports.SelectedIndex == 0)
                PortName = null;
            else
                PortName = cbx_Ports.Text;
        }

        private void cbx_Baud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_Baud.SelectedIndex == 0)
                Baud = 9600;
            else
                Baud = int.Parse(cbx_Baud.Text);

            if (cbx_Baud.SelectedIndex != BaudIdx)
                this.Text = "RT Chart*";
            else
                this.Text = "RT Chart";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();

            if (Directory != null)
            {
                if ((HeaderBk != Header) || (cbx_Baud.SelectedIndex != BaudIdx))
                {
                    StreamWriter streamWriter = new StreamWriter(Directory);

                    streamWriter.WriteLine(cbx_Baud.SelectedIndex.ToString());
                    streamWriter.WriteLine(Header);
                    streamWriter.Close();
                }
            }
        }
    }
}
