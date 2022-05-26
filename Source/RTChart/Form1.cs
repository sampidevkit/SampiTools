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
using System.Management;
using System.Windows.Forms;

namespace RTChart
{
    public partial class Form1 : Form
    {
        private bool PlotEna = false;
        private int Baudrate = 9600;
        private string Portname = "No COM";

        public Form1()
        {
            InitializeComponent();
        }

        private void bt_Plot_Click(object sender, EventArgs e)
        {
            if (PlotEna == false)
            {
                PlotEna = true;
                bt_Plot.Text = "Plot";
                bt_Plot.Image = RTChart.Properties.Resources.On;
            }
            else
            {
                PlotEna = false;
                bt_Plot.Text = "Off";
                bt_Plot.Image = RTChart.Properties.Resources.Off;
            }
        }

        private void cb_Baud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Baud.SelectedIndex > 0)
                Baudrate = int.Parse(cb_Baud.Text);

        }
    }
}
