namespace NBST
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabCtrl1 = new System.Windows.Forms.TabControl();
            this.tabGraph = new System.Windows.Forms.TabPage();
            this.cb_ViewMode = new System.Windows.Forms.ComboBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.rtb_Info = new System.Windows.Forms.RichTextBox();
            this.bt_Scan = new System.Windows.Forms.Button();
            this.bt_RFTest = new System.Windows.Forms.Button();
            this.cb_Port1 = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.tb_Md5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pgb_Percent = new System.Windows.Forms.ProgressBar();
            this.lb_Percent = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bt_Download = new System.Windows.Forms.Button();
            this.ckb_DebugEn = new System.Windows.Forms.CheckBox();
            this.cb_Apn = new System.Windows.Forms.ComboBox();
            this.cb_Url = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_Dns = new System.Windows.Forms.ComboBox();
            this.bt_Reboot = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabCtrl1.SuspendLayout();
            this.tabGraph.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrl1
            // 
            this.tabCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrl1.Controls.Add(this.tabGraph);
            this.tabCtrl1.Controls.Add(this.tabLog);
            this.tabCtrl1.Location = new System.Drawing.Point(12, 12);
            this.tabCtrl1.Name = "tabCtrl1";
            this.tabCtrl1.SelectedIndex = 0;
            this.tabCtrl1.Size = new System.Drawing.Size(708, 404);
            this.tabCtrl1.TabIndex = 3;
            // 
            // tabGraph
            // 
            this.tabGraph.BackColor = System.Drawing.Color.Transparent;
            this.tabGraph.Controls.Add(this.cb_ViewMode);
            this.tabGraph.Controls.Add(this.zedGraphControl1);
            this.tabGraph.Location = new System.Drawing.Point(4, 22);
            this.tabGraph.Name = "tabGraph";
            this.tabGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabGraph.Size = new System.Drawing.Size(700, 378);
            this.tabGraph.TabIndex = 0;
            this.tabGraph.Text = "Graph";
            // 
            // cb_ViewMode
            // 
            this.cb_ViewMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_ViewMode.FormattingEnabled = true;
            this.cb_ViewMode.Items.AddRange(new object[] {
            "Compact",
            "Scroll"});
            this.cb_ViewMode.Location = new System.Drawing.Point(12, 345);
            this.cb_ViewMode.Name = "cb_ViewMode";
            this.cb_ViewMode.Size = new System.Drawing.Size(81, 21);
            this.cb_ViewMode.TabIndex = 4;
            this.cb_ViewMode.Text = "Compact";
            this.cb_ViewMode.SelectedIndexChanged += new System.EventHandler(this.cb_ViewMode_SelectedIndexChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.BackColor = System.Drawing.SystemColors.Info;
            this.zedGraphControl1.Location = new System.Drawing.Point(6, 6);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(688, 366);
            this.zedGraphControl1.TabIndex = 3;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // tabLog
            // 
            this.tabLog.BackColor = System.Drawing.Color.Transparent;
            this.tabLog.Controls.Add(this.rtb_Log);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(700, 378);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            // 
            // rtb_Log
            // 
            this.rtb_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Log.BackColor = System.Drawing.SystemColors.Info;
            this.rtb_Log.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Log.Location = new System.Drawing.Point(6, 6);
            this.rtb_Log.Name = "rtb_Log";
            this.rtb_Log.Size = new System.Drawing.Size(780, 366);
            this.rtb_Log.TabIndex = 0;
            this.rtb_Log.Text = "";
            this.rtb_Log.TextChanged += new System.EventHandler(this.rtb_Log_TextChanged);
            this.rtb_Log.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rtb_Log_MouseDoubleClick);
            // 
            // rtb_Info
            // 
            this.rtb_Info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Info.BackColor = System.Drawing.Color.MistyRose;
            this.rtb_Info.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Info.ForeColor = System.Drawing.Color.Black;
            this.rtb_Info.Location = new System.Drawing.Point(726, 40);
            this.rtb_Info.Name = "rtb_Info";
            this.rtb_Info.Size = new System.Drawing.Size(270, 365);
            this.rtb_Info.TabIndex = 6;
            this.rtb_Info.Text = "";
            // 
            // bt_Scan
            // 
            this.bt_Scan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Scan.Location = new System.Drawing.Point(818, 12);
            this.bt_Scan.Name = "bt_Scan";
            this.bt_Scan.Size = new System.Drawing.Size(86, 23);
            this.bt_Scan.TabIndex = 5;
            this.bt_Scan.Text = "Scan AT Port";
            this.bt_Scan.UseVisualStyleBackColor = true;
            this.bt_Scan.Click += new System.EventHandler(this.bt_Scan_Click);
            // 
            // bt_RFTest
            // 
            this.bt_RFTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_RFTest.Enabled = false;
            this.bt_RFTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_RFTest.Location = new System.Drawing.Point(910, 12);
            this.bt_RFTest.Name = "bt_RFTest";
            this.bt_RFTest.Size = new System.Drawing.Size(86, 23);
            this.bt_RFTest.TabIndex = 5;
            this.bt_RFTest.Text = "RF Test";
            this.bt_RFTest.UseVisualStyleBackColor = true;
            this.bt_RFTest.Click += new System.EventHandler(this.bt_RFTest_Click);
            // 
            // cb_Port1
            // 
            this.cb_Port1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Port1.FormattingEnabled = true;
            this.cb_Port1.Location = new System.Drawing.Point(726, 13);
            this.cb_Port1.Name = "cb_Port1";
            this.cb_Port1.Size = new System.Drawing.Size(86, 21);
            this.cb_Port1.TabIndex = 4;
            this.cb_Port1.Text = "Empty";
            this.cb_Port1.SelectedIndexChanged += new System.EventHandler(this.cb_Port1_SelectedIndexChanged);
            this.cb_Port1.TextChanged += new System.EventHandler(this.cb_Port1_TextChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DtrEnable = true;
            this.serialPort1.ReadBufferSize = 10240;
            this.serialPort1.WriteBufferSize = 10240;
            // 
            // tb_Md5
            // 
            this.tb_Md5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Md5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Md5.ForeColor = System.Drawing.Color.OrangeRed;
            this.tb_Md5.Location = new System.Drawing.Point(500, 448);
            this.tb_Md5.Name = "tb_Md5";
            this.tb_Md5.Size = new System.Drawing.Size(210, 20);
            this.tb_Md5.TabIndex = 7;
            this.tb_Md5.Text = "834406D30F4F1FD20362A93CB3042696";
            this.tb_Md5.TextChanged += new System.EventHandler(this.tb_Md5_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 423);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "URL:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(461, 452);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "MD5:";
            // 
            // pgb_Percent
            // 
            this.pgb_Percent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pgb_Percent.Location = new System.Drawing.Point(726, 418);
            this.pgb_Percent.Name = "pgb_Percent";
            this.pgb_Percent.Size = new System.Drawing.Size(270, 23);
            this.pgb_Percent.TabIndex = 10;
            // 
            // lb_Percent
            // 
            this.lb_Percent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Percent.AutoSize = true;
            this.lb_Percent.BackColor = System.Drawing.Color.Transparent;
            this.lb_Percent.ForeColor = System.Drawing.Color.OrangeRed;
            this.lb_Percent.Location = new System.Drawing.Point(732, 423);
            this.lb_Percent.Name = "lb_Percent";
            this.lb_Percent.Size = new System.Drawing.Size(36, 13);
            this.lb_Percent.TabIndex = 11;
            this.lb_Percent.Text = "0 byte";
            this.lb_Percent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 452);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "APN:";
            // 
            // bt_Download
            // 
            this.bt_Download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Download.Enabled = false;
            this.bt_Download.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Download.Location = new System.Drawing.Point(882, 447);
            this.bt_Download.Name = "bt_Download";
            this.bt_Download.Size = new System.Drawing.Size(114, 23);
            this.bt_Download.TabIndex = 5;
            this.bt_Download.Text = "Download";
            this.bt_Download.UseVisualStyleBackColor = true;
            this.bt_Download.Click += new System.EventHandler(this.bt_Download_Click);
            // 
            // ckb_DebugEn
            // 
            this.ckb_DebugEn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ckb_DebugEn.AutoSize = true;
            this.ckb_DebugEn.Checked = true;
            this.ckb_DebugEn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_DebugEn.Location = new System.Drawing.Point(726, 450);
            this.ckb_DebugEn.Name = "ckb_DebugEn";
            this.ckb_DebugEn.Size = new System.Drawing.Size(58, 17);
            this.ckb_DebugEn.TabIndex = 12;
            this.ckb_DebugEn.Text = "Debug";
            this.ckb_DebugEn.UseVisualStyleBackColor = true;
            this.ckb_DebugEn.CheckedChanged += new System.EventHandler(this.ckb_DebugEn_CheckedChanged);
            // 
            // cb_Apn
            // 
            this.cb_Apn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_Apn.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Apn.ForeColor = System.Drawing.Color.OrangeRed;
            this.cb_Apn.FormattingEnabled = true;
            this.cb_Apn.Items.AddRange(new object[] {
            "m-nbiot",
            "m-wap",
            "v-internet"});
            this.cb_Apn.Location = new System.Drawing.Point(50, 448);
            this.cb_Apn.Name = "cb_Apn";
            this.cb_Apn.Size = new System.Drawing.Size(115, 21);
            this.cb_Apn.TabIndex = 13;
            this.cb_Apn.Text = "m-nbiot";
            this.cb_Apn.TextChanged += new System.EventHandler(this.cb_Apn_TextChanged);
            // 
            // cb_Url
            // 
            this.cb_Url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Url.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Url.ForeColor = System.Drawing.Color.OrangeRed;
            this.cb_Url.FormattingEnabled = true;
            this.cb_Url.Items.AddRange(new object[] {
            "https://raw.githubusercontent.com/dungliem92/DFUTest/master/Test.txt",
            "http://103.156.0.37:8081/Test.txt",
            "https://raw.githubusercontent.com/MicrochipTech/XPRESS-Loader/master/utilities/Xp" +
                "ressBL.hex"});
            this.cb_Url.Location = new System.Drawing.Point(50, 420);
            this.cb_Url.Name = "cb_Url";
            this.cb_Url.Size = new System.Drawing.Size(660, 21);
            this.cb_Url.TabIndex = 14;
            this.cb_Url.Text = "https://raw.githubusercontent.com/dungliem92/DFUTest/master/Test.txt";
            this.cb_Url.TextChanged += new System.EventHandler(this.cb_Url_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "DNS:";
            // 
            // cb_Dns
            // 
            this.cb_Dns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_Dns.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Dns.ForeColor = System.Drawing.Color.OrangeRed;
            this.cb_Dns.FormattingEnabled = true;
            this.cb_Dns.Items.AddRange(new object[] {
            "\"0.0.0.0\",\"0.0.0.0\"",
            "\"1.1.1.1\",\"8.8.8.8\"",
            "\"8.8.8.8\",\"8.8.4.4\""});
            this.cb_Dns.Location = new System.Drawing.Point(210, 448);
            this.cb_Dns.Name = "cb_Dns";
            this.cb_Dns.Size = new System.Drawing.Size(245, 21);
            this.cb_Dns.TabIndex = 16;
            this.cb_Dns.Text = "\"0.0.0.0\",\"0.0.0.0\"";
            this.cb_Dns.TextChanged += new System.EventHandler(this.cb_Dns_TextChanged);
            // 
            // bt_Reboot
            // 
            this.bt_Reboot.Location = new System.Drawing.Point(790, 447);
            this.bt_Reboot.Name = "bt_Reboot";
            this.bt_Reboot.Size = new System.Drawing.Size(86, 23);
            this.bt_Reboot.TabIndex = 17;
            this.bt_Reboot.Text = "Reboot";
            this.bt_Reboot.UseVisualStyleBackColor = true;
            this.bt_Reboot.Click += new System.EventHandler(this.bt_Reboot_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1008, 473);
            this.Controls.Add(this.bt_Reboot);
            this.Controls.Add(this.cb_Dns);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_Url);
            this.Controls.Add(this.cb_Apn);
            this.Controls.Add(this.ckb_DebugEn);
            this.Controls.Add(this.lb_Percent);
            this.Controls.Add(this.pgb_Percent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Md5);
            this.Controls.Add(this.rtb_Info);
            this.Controls.Add(this.bt_Scan);
            this.Controls.Add(this.tabCtrl1);
            this.Controls.Add(this.bt_Download);
            this.Controls.Add(this.bt_RFTest);
            this.Controls.Add(this.cb_Port1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NB Test Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabCtrl1.ResumeLayout(false);
            this.tabGraph.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrl1;
        private System.Windows.Forms.TabPage tabGraph;
        private System.Windows.Forms.Button bt_RFTest;
        private System.Windows.Forms.ComboBox cb_Port1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox rtb_Log;
        private System.Windows.Forms.RichTextBox rtb_Info;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button bt_Scan;
        private System.Windows.Forms.TextBox tb_Md5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pgb_Percent;
        private System.Windows.Forms.Label lb_Percent;
        private System.Windows.Forms.ComboBox cb_ViewMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bt_Download;
        private System.Windows.Forms.CheckBox ckb_DebugEn;
        private System.Windows.Forms.ComboBox cb_Apn;
        private System.Windows.Forms.ComboBox cb_Url;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_Dns;
        private System.Windows.Forms.Button bt_Reboot;
        private System.Windows.Forms.Timer timer1;
    }
}

