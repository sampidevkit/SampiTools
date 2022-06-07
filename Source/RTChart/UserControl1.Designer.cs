namespace RTChart
{
    partial class UserControl1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_SignCount = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_ChartTitile = new System.Windows.Forms.TextBox();
            this.tb_YAxisTitile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_Signal1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cb_Color1 = new System.Windows.Forms.ComboBox();
            this.tb_Name1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_BitCount = new System.Windows.Forms.ComboBox();
            this.lb_Name1 = new System.Windows.Forms.Label();
            this.lb_Name2 = new System.Windows.Forms.Label();
            this.tb_Name2 = new System.Windows.Forms.TextBox();
            this.cb_Color2 = new System.Windows.Forms.ComboBox();
            this.lb_Signal2 = new System.Windows.Forms.Label();
            this.lb_Name3 = new System.Windows.Forms.Label();
            this.tb_Name3 = new System.Windows.Forms.TextBox();
            this.cb_Color3 = new System.Windows.Forms.ComboBox();
            this.lb_Signal3 = new System.Windows.Forms.Label();
            this.lb_Name4 = new System.Windows.Forms.Label();
            this.tb_Name4 = new System.Windows.Forms.TextBox();
            this.cb_Color4 = new System.Windows.Forms.ComboBox();
            this.lb_Signal4 = new System.Windows.Forms.Label();
            this.lb_Name5 = new System.Windows.Forms.Label();
            this.tb_Name5 = new System.Windows.Forms.TextBox();
            this.cb_Color5 = new System.Windows.Forms.ComboBox();
            this.lb_Signal5 = new System.Windows.Forms.Label();
            this.lb_Name6 = new System.Windows.Forms.Label();
            this.tb_Name6 = new System.Windows.Forms.TextBox();
            this.cb_Color6 = new System.Windows.Forms.ComboBox();
            this.lb_Signal6 = new System.Windows.Forms.Label();
            this.lb_Name7 = new System.Windows.Forms.Label();
            this.tb_Name7 = new System.Windows.Forms.TextBox();
            this.cb_Color7 = new System.Windows.Forms.ComboBox();
            this.lb_Signal7 = new System.Windows.Forms.Label();
            this.lb_Name8 = new System.Windows.Forms.Label();
            this.tb_Name8 = new System.Windows.Forms.TextBox();
            this.cb_Color8 = new System.Windows.Forms.ComboBox();
            this.lb_Signal8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_SignCount
            // 
            this.cb_SignCount.FormattingEnabled = true;
            this.cb_SignCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cb_SignCount.Location = new System.Drawing.Point(79, 29);
            this.cb_SignCount.Name = "cb_SignCount";
            this.cb_SignCount.Size = new System.Drawing.Size(64, 21);
            this.cb_SignCount.TabIndex = 0;
            this.cb_SignCount.Text = "1";
            this.cb_SignCount.SelectedIndexChanged += new System.EventHandler(this.cb_SignCount_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chart Titile:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Signal Count:";
            // 
            // tb_ChartTitile
            // 
            this.tb_ChartTitile.Location = new System.Drawing.Point(79, 3);
            this.tb_ChartTitile.Name = "tb_ChartTitile";
            this.tb_ChartTitile.Size = new System.Drawing.Size(678, 20);
            this.tb_ChartTitile.TabIndex = 3;
            this.tb_ChartTitile.Text = "SAMPI DEV KIT REAL TIME CHART";
            // 
            // tb_YAxisTitile
            // 
            this.tb_YAxisTitile.Location = new System.Drawing.Point(385, 29);
            this.tb_YAxisTitile.Name = "tb_YAxisTitile";
            this.tb_YAxisTitile.Size = new System.Drawing.Size(372, 20);
            this.tb_YAxisTitile.TabIndex = 6;
            this.tb_YAxisTitile.Text = "Temperature (°C)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Y Axis Titile:";
            // 
            // lb_Signal1
            // 
            this.lb_Signal1.AutoSize = true;
            this.lb_Signal1.Location = new System.Drawing.Point(3, 59);
            this.lb_Signal1.Name = "lb_Signal1";
            this.lb_Signal1.Size = new System.Drawing.Size(48, 13);
            this.lb_Signal1.TabIndex = 8;
            this.lb_Signal1.Text = "Signal 1:";
            // 
            // cb_Color1
            // 
            this.cb_Color1.BackColor = System.Drawing.Color.Red;
            this.cb_Color1.FormattingEnabled = true;
            this.cb_Color1.Location = new System.Drawing.Point(79, 56);
            this.cb_Color1.Name = "cb_Color1";
            this.cb_Color1.Size = new System.Drawing.Size(64, 21);
            this.cb_Color1.TabIndex = 9;
            this.cb_Color1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Color1_MouseClick);
            // 
            // tb_Name1
            // 
            this.tb_Name1.Location = new System.Drawing.Point(208, 56);
            this.tb_Name1.Name = "tb_Name1";
            this.tb_Name1.Size = new System.Drawing.Size(549, 20);
            this.tb_Name1.TabIndex = 10;
            this.tb_Name1.Text = "Signal 1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Bit Count:";
            // 
            // cb_BitCount
            // 
            this.cb_BitCount.FormattingEnabled = true;
            this.cb_BitCount.Items.AddRange(new object[] {
            "float 32-bit",
            "signed 8-bit",
            "unsigned 8-bit",
            "signed 16-bit",
            "unsigned 16-bit",
            "signed 32-bit",
            "unsigned 32-bit"});
            this.cb_BitCount.Location = new System.Drawing.Point(208, 29);
            this.cb_BitCount.Name = "cb_BitCount";
            this.cb_BitCount.Size = new System.Drawing.Size(101, 21);
            this.cb_BitCount.TabIndex = 12;
            this.cb_BitCount.Text = "float 32-bit";
            // 
            // lb_Name1
            // 
            this.lb_Name1.AutoSize = true;
            this.lb_Name1.Location = new System.Drawing.Point(164, 59);
            this.lb_Name1.Name = "lb_Name1";
            this.lb_Name1.Size = new System.Drawing.Size(38, 13);
            this.lb_Name1.TabIndex = 13;
            this.lb_Name1.Text = "Name:";
            // 
            // lb_Name2
            // 
            this.lb_Name2.AutoSize = true;
            this.lb_Name2.Enabled = false;
            this.lb_Name2.Location = new System.Drawing.Point(164, 85);
            this.lb_Name2.Name = "lb_Name2";
            this.lb_Name2.Size = new System.Drawing.Size(38, 13);
            this.lb_Name2.TabIndex = 17;
            this.lb_Name2.Text = "Name:";
            // 
            // tb_Name2
            // 
            this.tb_Name2.Enabled = false;
            this.tb_Name2.Location = new System.Drawing.Point(208, 82);
            this.tb_Name2.Name = "tb_Name2";
            this.tb_Name2.Size = new System.Drawing.Size(549, 20);
            this.tb_Name2.TabIndex = 16;
            this.tb_Name2.Text = "Signal 2";
            // 
            // cb_Color2
            // 
            this.cb_Color2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cb_Color2.Enabled = false;
            this.cb_Color2.FormattingEnabled = true;
            this.cb_Color2.Location = new System.Drawing.Point(79, 82);
            this.cb_Color2.Name = "cb_Color2";
            this.cb_Color2.Size = new System.Drawing.Size(64, 21);
            this.cb_Color2.TabIndex = 15;
            this.cb_Color2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Color2_MouseClick);
            // 
            // lb_Signal2
            // 
            this.lb_Signal2.AutoSize = true;
            this.lb_Signal2.Enabled = false;
            this.lb_Signal2.Location = new System.Drawing.Point(3, 85);
            this.lb_Signal2.Name = "lb_Signal2";
            this.lb_Signal2.Size = new System.Drawing.Size(48, 13);
            this.lb_Signal2.TabIndex = 14;
            this.lb_Signal2.Text = "Signal 2:";
            // 
            // lb_Name3
            // 
            this.lb_Name3.AutoSize = true;
            this.lb_Name3.Enabled = false;
            this.lb_Name3.Location = new System.Drawing.Point(164, 111);
            this.lb_Name3.Name = "lb_Name3";
            this.lb_Name3.Size = new System.Drawing.Size(38, 13);
            this.lb_Name3.TabIndex = 21;
            this.lb_Name3.Text = "Name:";
            // 
            // tb_Name3
            // 
            this.tb_Name3.Enabled = false;
            this.tb_Name3.Location = new System.Drawing.Point(208, 108);
            this.tb_Name3.Name = "tb_Name3";
            this.tb_Name3.Size = new System.Drawing.Size(549, 20);
            this.tb_Name3.TabIndex = 20;
            this.tb_Name3.Text = "Signal 3";
            // 
            // cb_Color3
            // 
            this.cb_Color3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cb_Color3.Enabled = false;
            this.cb_Color3.FormattingEnabled = true;
            this.cb_Color3.Location = new System.Drawing.Point(79, 108);
            this.cb_Color3.Name = "cb_Color3";
            this.cb_Color3.Size = new System.Drawing.Size(64, 21);
            this.cb_Color3.TabIndex = 19;
            this.cb_Color3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Color3_MouseClick);
            // 
            // lb_Signal3
            // 
            this.lb_Signal3.AutoSize = true;
            this.lb_Signal3.Enabled = false;
            this.lb_Signal3.Location = new System.Drawing.Point(3, 111);
            this.lb_Signal3.Name = "lb_Signal3";
            this.lb_Signal3.Size = new System.Drawing.Size(48, 13);
            this.lb_Signal3.TabIndex = 18;
            this.lb_Signal3.Text = "Signal 3:";
            // 
            // lb_Name4
            // 
            this.lb_Name4.AutoSize = true;
            this.lb_Name4.Enabled = false;
            this.lb_Name4.Location = new System.Drawing.Point(164, 137);
            this.lb_Name4.Name = "lb_Name4";
            this.lb_Name4.Size = new System.Drawing.Size(38, 13);
            this.lb_Name4.TabIndex = 25;
            this.lb_Name4.Text = "Name:";
            // 
            // tb_Name4
            // 
            this.tb_Name4.Enabled = false;
            this.tb_Name4.Location = new System.Drawing.Point(208, 134);
            this.tb_Name4.Name = "tb_Name4";
            this.tb_Name4.Size = new System.Drawing.Size(549, 20);
            this.tb_Name4.TabIndex = 24;
            this.tb_Name4.Text = "Signal 4";
            // 
            // cb_Color4
            // 
            this.cb_Color4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cb_Color4.Enabled = false;
            this.cb_Color4.FormattingEnabled = true;
            this.cb_Color4.Location = new System.Drawing.Point(79, 134);
            this.cb_Color4.Name = "cb_Color4";
            this.cb_Color4.Size = new System.Drawing.Size(64, 21);
            this.cb_Color4.TabIndex = 23;
            this.cb_Color4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Color4_MouseClick);
            // 
            // lb_Signal4
            // 
            this.lb_Signal4.AutoSize = true;
            this.lb_Signal4.Enabled = false;
            this.lb_Signal4.Location = new System.Drawing.Point(3, 137);
            this.lb_Signal4.Name = "lb_Signal4";
            this.lb_Signal4.Size = new System.Drawing.Size(48, 13);
            this.lb_Signal4.TabIndex = 22;
            this.lb_Signal4.Text = "Signal 4:";
            // 
            // lb_Name5
            // 
            this.lb_Name5.AutoSize = true;
            this.lb_Name5.Enabled = false;
            this.lb_Name5.Location = new System.Drawing.Point(164, 163);
            this.lb_Name5.Name = "lb_Name5";
            this.lb_Name5.Size = new System.Drawing.Size(38, 13);
            this.lb_Name5.TabIndex = 29;
            this.lb_Name5.Text = "Name:";
            // 
            // tb_Name5
            // 
            this.tb_Name5.Enabled = false;
            this.tb_Name5.Location = new System.Drawing.Point(208, 160);
            this.tb_Name5.Name = "tb_Name5";
            this.tb_Name5.Size = new System.Drawing.Size(549, 20);
            this.tb_Name5.TabIndex = 28;
            this.tb_Name5.Text = "Signal 5";
            // 
            // cb_Color5
            // 
            this.cb_Color5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cb_Color5.Enabled = false;
            this.cb_Color5.FormattingEnabled = true;
            this.cb_Color5.Location = new System.Drawing.Point(79, 160);
            this.cb_Color5.Name = "cb_Color5";
            this.cb_Color5.Size = new System.Drawing.Size(64, 21);
            this.cb_Color5.TabIndex = 27;
            this.cb_Color5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Color5_MouseClick);
            // 
            // lb_Signal5
            // 
            this.lb_Signal5.AutoSize = true;
            this.lb_Signal5.Enabled = false;
            this.lb_Signal5.Location = new System.Drawing.Point(3, 163);
            this.lb_Signal5.Name = "lb_Signal5";
            this.lb_Signal5.Size = new System.Drawing.Size(48, 13);
            this.lb_Signal5.TabIndex = 26;
            this.lb_Signal5.Text = "Signal 5:";
            // 
            // lb_Name6
            // 
            this.lb_Name6.AutoSize = true;
            this.lb_Name6.Enabled = false;
            this.lb_Name6.Location = new System.Drawing.Point(164, 189);
            this.lb_Name6.Name = "lb_Name6";
            this.lb_Name6.Size = new System.Drawing.Size(38, 13);
            this.lb_Name6.TabIndex = 33;
            this.lb_Name6.Text = "Name:";
            // 
            // tb_Name6
            // 
            this.tb_Name6.Enabled = false;
            this.tb_Name6.Location = new System.Drawing.Point(208, 186);
            this.tb_Name6.Name = "tb_Name6";
            this.tb_Name6.Size = new System.Drawing.Size(549, 20);
            this.tb_Name6.TabIndex = 32;
            this.tb_Name6.Text = "Signal 6";
            // 
            // cb_Color6
            // 
            this.cb_Color6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cb_Color6.Enabled = false;
            this.cb_Color6.FormattingEnabled = true;
            this.cb_Color6.Location = new System.Drawing.Point(79, 186);
            this.cb_Color6.Name = "cb_Color6";
            this.cb_Color6.Size = new System.Drawing.Size(64, 21);
            this.cb_Color6.TabIndex = 31;
            this.cb_Color6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Color6_MouseClick);
            // 
            // lb_Signal6
            // 
            this.lb_Signal6.AutoSize = true;
            this.lb_Signal6.Enabled = false;
            this.lb_Signal6.Location = new System.Drawing.Point(3, 189);
            this.lb_Signal6.Name = "lb_Signal6";
            this.lb_Signal6.Size = new System.Drawing.Size(48, 13);
            this.lb_Signal6.TabIndex = 30;
            this.lb_Signal6.Text = "Signal 6:";
            // 
            // lb_Name7
            // 
            this.lb_Name7.AutoSize = true;
            this.lb_Name7.Enabled = false;
            this.lb_Name7.Location = new System.Drawing.Point(164, 215);
            this.lb_Name7.Name = "lb_Name7";
            this.lb_Name7.Size = new System.Drawing.Size(38, 13);
            this.lb_Name7.TabIndex = 37;
            this.lb_Name7.Text = "Name:";
            // 
            // tb_Name7
            // 
            this.tb_Name7.Enabled = false;
            this.tb_Name7.Location = new System.Drawing.Point(208, 212);
            this.tb_Name7.Name = "tb_Name7";
            this.tb_Name7.Size = new System.Drawing.Size(549, 20);
            this.tb_Name7.TabIndex = 36;
            this.tb_Name7.Text = "Signal 7";
            // 
            // cb_Color7
            // 
            this.cb_Color7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cb_Color7.Enabled = false;
            this.cb_Color7.FormattingEnabled = true;
            this.cb_Color7.Location = new System.Drawing.Point(79, 212);
            this.cb_Color7.Name = "cb_Color7";
            this.cb_Color7.Size = new System.Drawing.Size(64, 21);
            this.cb_Color7.TabIndex = 35;
            this.cb_Color7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Color7_MouseClick);
            // 
            // lb_Signal7
            // 
            this.lb_Signal7.AutoSize = true;
            this.lb_Signal7.Enabled = false;
            this.lb_Signal7.Location = new System.Drawing.Point(3, 215);
            this.lb_Signal7.Name = "lb_Signal7";
            this.lb_Signal7.Size = new System.Drawing.Size(48, 13);
            this.lb_Signal7.TabIndex = 34;
            this.lb_Signal7.Text = "Signal 7:";
            // 
            // lb_Name8
            // 
            this.lb_Name8.AutoSize = true;
            this.lb_Name8.Enabled = false;
            this.lb_Name8.Location = new System.Drawing.Point(164, 241);
            this.lb_Name8.Name = "lb_Name8";
            this.lb_Name8.Size = new System.Drawing.Size(38, 13);
            this.lb_Name8.TabIndex = 41;
            this.lb_Name8.Text = "Name:";
            // 
            // tb_Name8
            // 
            this.tb_Name8.Enabled = false;
            this.tb_Name8.Location = new System.Drawing.Point(208, 238);
            this.tb_Name8.Name = "tb_Name8";
            this.tb_Name8.Size = new System.Drawing.Size(549, 20);
            this.tb_Name8.TabIndex = 40;
            this.tb_Name8.Text = "Signal 8";
            // 
            // cb_Color8
            // 
            this.cb_Color8.BackColor = System.Drawing.Color.Black;
            this.cb_Color8.Enabled = false;
            this.cb_Color8.FormattingEnabled = true;
            this.cb_Color8.Location = new System.Drawing.Point(79, 238);
            this.cb_Color8.Name = "cb_Color8";
            this.cb_Color8.Size = new System.Drawing.Size(64, 21);
            this.cb_Color8.TabIndex = 39;
            this.cb_Color8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Color8_MouseClick);
            // 
            // lb_Signal8
            // 
            this.lb_Signal8.AutoSize = true;
            this.lb_Signal8.Enabled = false;
            this.lb_Signal8.Location = new System.Drawing.Point(3, 241);
            this.lb_Signal8.Name = "lb_Signal8";
            this.lb_Signal8.Size = new System.Drawing.Size(48, 13);
            this.lb_Signal8.TabIndex = 38;
            this.lb_Signal8.Text = "Signal 8:";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_Name8);
            this.Controls.Add(this.tb_Name8);
            this.Controls.Add(this.cb_Color8);
            this.Controls.Add(this.lb_Signal8);
            this.Controls.Add(this.lb_Name7);
            this.Controls.Add(this.tb_Name7);
            this.Controls.Add(this.cb_Color7);
            this.Controls.Add(this.lb_Signal7);
            this.Controls.Add(this.lb_Name6);
            this.Controls.Add(this.tb_Name6);
            this.Controls.Add(this.cb_Color6);
            this.Controls.Add(this.lb_Signal6);
            this.Controls.Add(this.lb_Name5);
            this.Controls.Add(this.tb_Name5);
            this.Controls.Add(this.cb_Color5);
            this.Controls.Add(this.lb_Signal5);
            this.Controls.Add(this.lb_Name4);
            this.Controls.Add(this.tb_Name4);
            this.Controls.Add(this.cb_Color4);
            this.Controls.Add(this.lb_Signal4);
            this.Controls.Add(this.lb_Name3);
            this.Controls.Add(this.tb_Name3);
            this.Controls.Add(this.cb_Color3);
            this.Controls.Add(this.lb_Signal3);
            this.Controls.Add(this.lb_Name2);
            this.Controls.Add(this.tb_Name2);
            this.Controls.Add(this.cb_Color2);
            this.Controls.Add(this.lb_Signal2);
            this.Controls.Add(this.lb_Name1);
            this.Controls.Add(this.cb_BitCount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_Name1);
            this.Controls.Add(this.cb_Color1);
            this.Controls.Add(this.lb_Signal1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_YAxisTitile);
            this.Controls.Add(this.tb_ChartTitile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_SignCount);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(760, 385);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_SignCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_ChartTitile;
        private System.Windows.Forms.TextBox tb_YAxisTitile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_Signal1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox cb_Color1;
        private System.Windows.Forms.TextBox tb_Name1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_BitCount;
        private System.Windows.Forms.Label lb_Name1;
        private System.Windows.Forms.Label lb_Name2;
        private System.Windows.Forms.TextBox tb_Name2;
        private System.Windows.Forms.ComboBox cb_Color2;
        private System.Windows.Forms.Label lb_Signal2;
        private System.Windows.Forms.Label lb_Name3;
        private System.Windows.Forms.TextBox tb_Name3;
        private System.Windows.Forms.ComboBox cb_Color3;
        private System.Windows.Forms.Label lb_Signal3;
        private System.Windows.Forms.Label lb_Name4;
        private System.Windows.Forms.TextBox tb_Name4;
        private System.Windows.Forms.ComboBox cb_Color4;
        private System.Windows.Forms.Label lb_Signal4;
        private System.Windows.Forms.Label lb_Name5;
        private System.Windows.Forms.TextBox tb_Name5;
        private System.Windows.Forms.ComboBox cb_Color5;
        private System.Windows.Forms.Label lb_Signal5;
        private System.Windows.Forms.Label lb_Name6;
        private System.Windows.Forms.TextBox tb_Name6;
        private System.Windows.Forms.ComboBox cb_Color6;
        private System.Windows.Forms.Label lb_Signal6;
        private System.Windows.Forms.Label lb_Name7;
        private System.Windows.Forms.TextBox tb_Name7;
        private System.Windows.Forms.ComboBox cb_Color7;
        private System.Windows.Forms.Label lb_Signal7;
        private System.Windows.Forms.Label lb_Name8;
        private System.Windows.Forms.TextBox tb_Name8;
        private System.Windows.Forms.ComboBox cb_Color8;
        private System.Windows.Forms.Label lb_Signal8;
    }
}
