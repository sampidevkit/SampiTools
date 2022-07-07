using System;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Management;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Text;

namespace MakeReg
{
    public partial class Form1 : Form
    {
        private UInt64 valMap = 0;
        private bool hexBusy = false;
        private bool decBusy = false;
        private bool binBusy = false;
        private bool bitBusy = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void bitMapsSet(UInt32 v)
        {
            valMap = v;

            if ((v & 0x80000000) > 0)
                bt31.BackColor = Color.Salmon;
            else
                bt31.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt30.BackColor = Color.Salmon;
            else
                bt30.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt29.BackColor = Color.Salmon;
            else
                bt29.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt28.BackColor = Color.Salmon;
            else
                bt28.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt27.BackColor = Color.Salmon;
            else
                bt27.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt26.BackColor = Color.Salmon;
            else
                bt26.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt25.BackColor = Color.Salmon;
            else
                bt25.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt24.BackColor = Color.Salmon;
            else
                bt24.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt23.BackColor = Color.Salmon;
            else
                bt23.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt22.BackColor = Color.Salmon;
            else
                bt22.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt21.BackColor = Color.Salmon;
            else
                bt21.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt20.BackColor = Color.Salmon;
            else
                bt20.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt19.BackColor = Color.Salmon;
            else
                bt19.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt18.BackColor = Color.Salmon;
            else
                bt18.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt17.BackColor = Color.Salmon;
            else
                bt17.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt16.BackColor = Color.Salmon;
            else
                bt16.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt15.BackColor = Color.Salmon;
            else
                bt15.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt14.BackColor = Color.Salmon;
            else
                bt14.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt13.BackColor = Color.Salmon;
            else
                bt13.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt12.BackColor = Color.Salmon;
            else
                bt12.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt11.BackColor = Color.Salmon;
            else
                bt11.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt10.BackColor = Color.Salmon;
            else
                bt10.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt9.BackColor = Color.Salmon;
            else
                bt9.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt8.BackColor = Color.Salmon;
            else
                bt8.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt7.BackColor = Color.Salmon;
            else
                bt7.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt6.BackColor = Color.Salmon;
            else
                bt6.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt5.BackColor = Color.Salmon;
            else
                bt5.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt4.BackColor = Color.Salmon;
            else
                bt4.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt3.BackColor = Color.Salmon;
            else
                bt3.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt2.BackColor = Color.Salmon;
            else
                bt2.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt1.BackColor = Color.Salmon;
            else
                bt1.BackColor = Color.LightGreen;

            v <<= 1;

            if ((v & 0x80000000) > 0)
                bt0.BackColor = Color.Salmon;
            else
                bt0.BackColor = Color.LightGreen;
        }

        private string removeChar(string s, char c)
        {
            char[] a = s.ToCharArray();
            int i = 0;

            foreach (char x in a)
            {
                if (x == c)
                    i++;
            }

            char[] ah = new char[a.Length - i];

            i = 0;

            foreach (char x in a)
            {
                if (x != c)
                    ah[i++] = x;
            }

            return new string(ah);
        }

        private string hexDisplay(UInt32 v)
        {
            char[] ah = new char[11];
            char[] a = v.ToString("X8").ToCharArray();

            int i = 0, j = 0;

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];

            return new string(ah);
        }

        private string decDisplay(UInt32 v)
        {
            char[] ah = new char[13];
            char[] a = v.ToString("D10").ToCharArray();

            int i = 0, j = 0;

            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];

            return new string(ah);
        }

        private string binDisplay(UInt32 v)
        {
            char[] ah = new char[35];
            char[] a = Convert.ToString(v, 2).PadLeft(32, '0').ToCharArray();

            int i = 0, j = 0;

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = '.';

            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];
            ah[i++] = a[j++];

            return new string(ah);
        }

        private void bt_Set_Click(object sender, EventArgs e)
        {
            tb_Dec.Text = "4.294.967.295";
        }

        private void bt_Clear_Click(object sender, EventArgs e)
        {
            tb_Dec.Text = "0.000.000.000";
        }

        private void tb_Hex_TextChanged(object sender, EventArgs e)
        {
            hexBusy = true;

            string s = removeChar(tb_Hex.Text, '.');

            try
            {
                UInt64 v = UInt64.Parse(s, System.Globalization.NumberStyles.HexNumber);

                if (!decBusy)
                    tb_Dec.Text = decDisplay((UInt32)v);

                if (!binBusy)
                    tb_Bin.Text = binDisplay((UInt32)v);

                if(!bitBusy)
                    bitMapsSet((UInt32)v);
            }
            catch
            {
                MessageBox.Show("Incorrect input: " + s, "Error");
            }

            hexBusy = false;
        }

        private void tb_Dec_TextChanged(object sender, EventArgs e)
        {
            decBusy = true;

            string s = removeChar(tb_Dec.Text, '.');

            try
            {
                UInt64 v = UInt64.Parse(s);

                if (!hexBusy)
                    tb_Hex.Text = hexDisplay((UInt32)v);

                if (!binBusy)
                    tb_Bin.Text = binDisplay((UInt32)v);

                if (!bitBusy)
                    bitMapsSet((UInt32)v);
            }
            catch
            {
                MessageBox.Show("Incorrect input: " + s, "Error");
            }

            decBusy = false;
        }

        private void tb_Bin_TextChanged(object sender, EventArgs e)
        {
            binBusy = true;

            string s = removeChar(tb_Bin.Text, '.');

            try
            {
                UInt64 v = Convert.ToUInt64(s, 2);

                if (!hexBusy)
                    tb_Hex.Text = hexDisplay((UInt32)v);

                if (!decBusy)
                    tb_Dec.Text = decDisplay((UInt32)v);

                if (!bitBusy)
                    bitMapsSet((UInt32)v);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Incorrect input: " + s + "\n" + ex.ToString(), "Error");
            }

            binBusy = false;
        }

        private void tb_Hex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText("0x" + removeChar(tb_Hex.Text, '.'));
            lb_Stt.Visible = true;
            timer1.Start();
        }

        private void tb_Dec_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(removeChar(tb_Dec.Text, '.'));
            lb_Stt.Visible = true;
            timer1.Start();
        }

        private void tb_Bin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText("0b" + removeChar(tb_Bin.Text, '.'));
            lb_Stt.Visible = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lb_Stt.Visible = false;
            timer1.Stop();
        }

        private void bt31_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt31.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 31));
                bt31.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 31);
                bt31.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt30_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt30.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 30));
                bt30.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 30);
                bt30.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt29_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt29.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 29));
                bt29.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 29);
                bt29.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt28_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt28.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 28));
                bt28.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 28);
                bt28.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt27_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt27.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 27));
                bt27.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 27);
                bt27.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt26_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt26.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 26));
                bt26.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 26);
                bt26.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt25_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt25.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 25));
                bt25.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 25);
                bt25.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt24_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt24.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 24));
                bt24.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 24);
                bt24.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt23_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt23.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 23));
                bt23.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 23);
                bt23.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt22_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt22.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 22));
                bt22.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 22);
                bt22.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt21_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt21.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 21));
                bt21.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 21);
                bt21.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt20_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt20.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 20));
                bt20.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 20);
                bt20.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt19_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt19.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 19));
                bt19.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 19);
                bt19.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt18_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt18.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 18));
                bt18.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 18);
                bt18.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt17_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt17.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 17));
                bt17.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 17);
                bt17.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt16_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt16.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 16));
                bt16.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 16);
                bt16.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt15_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt15.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 15));
                bt15.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 15);
                bt15.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt14_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt14.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 14));
                bt14.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 14);
                bt14.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt13_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt13.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 13));
                bt13.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 13);
                bt13.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt12_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt12.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 12));
                bt12.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 12);
                bt12.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt11_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt11.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 11));
                bt11.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 11);
                bt11.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt10_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt10.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 10));
                bt10.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 10);
                bt10.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt9_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt9.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 9));
                bt9.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 9);
                bt9.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt8.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 8));
                bt8.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 8);
                bt8.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt7_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt7.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 7));
                bt7.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 7);
                bt7.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt6.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 6));
                bt6.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 6);
                bt6.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt5.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 5));
                bt5.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 5);
                bt5.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt4.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 4));
                bt4.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 4);
                bt4.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt3.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 3));
                bt3.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 3);
                bt3.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt2.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 2));
                bt2.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 2);
                bt2.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt1.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 1));
                bt1.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 1);
                bt1.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void bt0_Click(object sender, EventArgs e)
        {
            bitBusy = true;

            if (bt0.BackColor == Color.Salmon)
            {
                valMap &= (~((UInt64)1 << 0));
                bt0.BackColor = Color.LightGreen;
            }
            else
            {
                valMap |= ((UInt64)1 << 0);
                bt0.BackColor = Color.Salmon;
            }

            tb_Hex.Text = hexDisplay((UInt32)valMap);
            tb_Dec.Text = decDisplay((UInt32)valMap);
            tb_Bin.Text = binDisplay((UInt32)valMap);

            bitBusy = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text += " v." + $"{version}";
            this.Update();
        }
    }
}
