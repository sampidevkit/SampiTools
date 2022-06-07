using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTChart
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void Signal_OnOff(int sig, bool ena)
        {
            switch(sig)
            {
                case 2:
                    lb_Signal2.Enabled = ena;
                    lb_Name2.Enabled = ena;
                    cb_Color2.Enabled = ena;
                    tb_Name2.Enabled = ena;
                    break;

                case 3:
                    lb_Signal3.Enabled = ena;
                    lb_Name3.Enabled = ena;
                    cb_Color3.Enabled = ena;
                    tb_Name3.Enabled = ena;
                    break;

                case 4:
                    lb_Signal4.Enabled = ena;
                    lb_Name4.Enabled = ena;
                    cb_Color4.Enabled = ena;
                    tb_Name4.Enabled = ena;
                    break;

                case 5:
                    lb_Signal5.Enabled = ena;
                    lb_Name5.Enabled = ena;
                    cb_Color5.Enabled = ena;
                    tb_Name5.Enabled = ena;
                    break;

                case 6:
                    lb_Signal6.Enabled = ena;
                    lb_Name6.Enabled = ena;
                    cb_Color6.Enabled = ena;
                    tb_Name6.Enabled = ena;
                    break;

                case 7:
                    lb_Signal7.Enabled = ena;
                    lb_Name7.Enabled = ena;
                    cb_Color7.Enabled = ena;
                    tb_Name7.Enabled = ena;
                    break;

                case 8:
                    lb_Signal8.Enabled = ena;
                    lb_Name8.Enabled = ena;
                    cb_Color8.Enabled = ena;
                    tb_Name8.Enabled = ena;
                    break;

                default:
                    lb_Signal2.Enabled = false;
                    lb_Name2.Enabled = false;
                    cb_Color2.Enabled = false;
                    tb_Name2.Enabled = false;

                    lb_Signal3.Enabled = false;
                    lb_Name3.Enabled = false;
                    cb_Color3.Enabled = false;
                    tb_Name3.Enabled = false;

                    lb_Signal4.Enabled = false;
                    lb_Name4.Enabled = false;
                    cb_Color4.Enabled = false;
                    tb_Name4.Enabled = false;

                    lb_Signal5.Enabled = false;
                    lb_Name5.Enabled = false;
                    cb_Color5.Enabled = false;
                    tb_Name5.Enabled = false;

                    lb_Signal6.Enabled = false;
                    lb_Name6.Enabled = false;
                    cb_Color6.Enabled = false;
                    tb_Name6.Enabled = false;

                    lb_Signal7.Enabled = false;
                    lb_Name7.Enabled = false;
                    cb_Color7.Enabled = false;
                    tb_Name7.Enabled = false;

                    lb_Signal8.Enabled = false;
                    lb_Name8.Enabled = false;
                    cb_Color8.Enabled = false;
                    tb_Name8.Enabled = false;
                    break;
            }
        }

        private void cb_Color1_MouseClick(object sender, MouseEventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cb_Color1.BackColor = colorDialog1.Color;
            }
        }

        private void cb_Color2_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cb_Color2.BackColor = colorDialog1.Color;
            }
        }

        private void cb_Color3_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cb_Color3.BackColor = colorDialog1.Color;
            }
        }

        private void cb_Color4_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cb_Color4.BackColor = colorDialog1.Color;
            }
        }

        private void cb_Color5_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cb_Color5.BackColor = colorDialog1.Color;
            }
        }

        private void cb_Color6_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cb_Color6.BackColor = colorDialog1.Color;
            }
        }

        private void cb_Color7_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cb_Color7.BackColor = colorDialog1.Color;
            }
        }

        private void cb_Color8_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cb_Color8.BackColor = colorDialog1.Color;
            }
        }

        private void cb_SignCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sigCount = int.Parse(cb_SignCount.Text);

            Signal_OnOff(0, false);

            while (sigCount > 1)
            {
                Signal_OnOff(sigCount, true);
                sigCount--;
            }
        }
    }
}
