using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace PIC32MM_RTOS_Int_Asm_Gen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ckb_DefaultName_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_DefaultName.Checked == true)
                tb_DefaultName.Enabled = false;
            else
            {
                tb_DefaultName.Enabled = true;
                tb_DefaultName.Text = "interrupt_handler";
            }
        }

        private string MakeFileName()
        {
            string fileName;

            if (ckb_DefaultName.Checked == true)
                fileName = cbx_Vector.Text + "_Isr.S";
            else
                fileName = tb_DefaultName.Text + ".S";

            return fileName;
        }

        private void bt_New_Click(object sender, EventArgs e)
        {
            string src;
            StreamWriter sW = new StreamWriter(MakeFileName());

            sW.Write(PIC32MM_RTOS_Int_Asm_Gen.Properties.Resources.header);
            sW.Flush();
            src = PIC32MM_RTOS_Int_Asm_Gen.Properties.Resources.source.Replace("VECTOR", cbx_Vector.Text);
            sW.WriteLine(src);
            sW.Flush();
            sW.Close();
            lb_Status.Text = MakeFileName() + " has been created";
            lb_Status.Visible = true;
            timer1.Start();
        }

        private bool validFilename(string s)
        {
            int i = 0;
            char[] arr = s.ToCharArray();

            foreach(char c in arr)
            {
                if(i==0)
                {
                    if ((c >= '0') && (c <= '9'))
                        return false;
                }

                if (c > 'z')
                    return false;

                if ((c > 'Z') && (c < 'a') && (c != '_'))
                    return false;

                if ((c > 9) && (c < 'A'))
                    return false;

                if (c < '0')
                    return false;

                i++;
            }

            return true;
        }

        private void tb_DefaultName_TextChanged(object sender, EventArgs e)
        {
            if (validFilename(tb_DefaultName.Text) == false)
            {
                bt_New.Enabled = false;
                bt_Append.Enabled = false;
                MessageBox.Show("Invalid file name", "Error");
            }
            else
            {
                bt_New.Enabled = false;
                bt_Append.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            lb_Status.Visible = false;
        }

        private void bt_Append_Click(object sender, EventArgs e)
        {
            bool NewFile;
            string src;
            FileStream fS;
            StreamReader sR;
            StreamWriter sW;
            string fileName = tb_DefaultName.Text + ".S";

            if (File.Exists(fileName) == false)
            {
                NewFile = true;
                fS = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                src = PIC32MM_RTOS_Int_Asm_Gen.Properties.Resources.header;
            }
            else
            {
                NewFile = false;
                fS = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                sR = new StreamReader(fS);
                src = sR.ReadToEnd();
                sR.Close();
                fS.Close();
                fS = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            }

            sW = new StreamWriter(fS);
            sW.Write(src);
            sW.Flush();
            src = PIC32MM_RTOS_Int_Asm_Gen.Properties.Resources.source.Replace("VECTOR", cbx_Vector.Text);
            sW.Write(src);
            sW.Flush();
            sW.Close();
            fS.Close();

            if (NewFile == true)
                lb_Status.Text = fileName + " has been created";
            else
                lb_Status.Text = fileName + " has been updated";

            lb_Status.Visible = true;
            timer1.Start();
        }

        private void bt_MakeAll_Click(object sender, EventArgs e)
        {
            bool cont = true;
            string src, fileName;

            cbx_Vector.SelectedIndex = 0;

            while (cont == true) 
            {
                try
                {
                    fileName = cbx_Vector.Text + "_Isr.S";
                    StreamWriter sW = new StreamWriter(fileName);

                    sW.Write(PIC32MM_RTOS_Int_Asm_Gen.Properties.Resources.header);
                    sW.Flush();
                    src = PIC32MM_RTOS_Int_Asm_Gen.Properties.Resources.source.Replace("VECTOR", cbx_Vector.Text);
                    sW.WriteLine(src);
                    sW.Flush();
                    sW.Close();

                    cbx_Vector.SelectedIndex++;
                }
                catch
                {
                    cont = false;
                }
            }

            lb_Status.Text = "All files have been created";
            lb_Status.Visible = true;
            timer1.Start();
        }
    }
}
