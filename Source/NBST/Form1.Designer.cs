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
            this.ckb_Printable = new System.Windows.Forms.CheckBox();
            this.ckb_DebugEn = new System.Windows.Forms.CheckBox();
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
            this.cb_Apn = new System.Windows.Forms.ComboBox();
            this.cb_Url = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_Dns = new System.Windows.Forms.ComboBox();
            this.bt_Reboot = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.bt_CMD = new System.Windows.Forms.Button();
            this.cb_CMD = new System.Windows.Forms.ComboBox();
            this.nud_RebootWait = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nud_RespWait = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nud_DlCount = new System.Windows.Forms.NumericUpDown();
            this.lklb_GMaps = new System.Windows.Forms.LinkLabel();
            this.lklb_CellFind = new System.Windows.Forms.LinkLabel();
            this.lklb_LogFolder = new System.Windows.Forms.LinkLabel();
            this.lklb_Screenshot = new System.Windows.Forms.LinkLabel();
            this.tabCtrl1.SuspendLayout();
            this.tabGraph.SuspendLayout();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RebootWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RespWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_DlCount)).BeginInit();
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
            this.tabCtrl1.Size = new System.Drawing.Size(632, 486);
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
            this.tabGraph.Size = new System.Drawing.Size(624, 460);
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
            this.cb_ViewMode.Location = new System.Drawing.Point(8, 431);
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
            this.zedGraphControl1.Size = new System.Drawing.Size(612, 448);
            this.zedGraphControl1.TabIndex = 3;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // tabLog
            // 
            this.tabLog.BackColor = System.Drawing.Color.Transparent;
            this.tabLog.Controls.Add(this.ckb_Printable);
            this.tabLog.Controls.Add(this.ckb_DebugEn);
            this.tabLog.Controls.Add(this.rtb_Log);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(624, 460);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            // 
            // ckb_Printable
            // 
            this.ckb_Printable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckb_Printable.AutoSize = true;
            this.ckb_Printable.BackColor = System.Drawing.SystemColors.Info;
            this.ckb_Printable.Checked = true;
            this.ckb_Printable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_Printable.Location = new System.Drawing.Point(531, 10);
            this.ckb_Printable.Name = "ckb_Printable";
            this.ckb_Printable.Size = new System.Drawing.Size(67, 17);
            this.ckb_Printable.TabIndex = 22;
            this.ckb_Printable.Text = "Printable";
            this.ckb_Printable.UseVisualStyleBackColor = false;
            this.ckb_Printable.CheckedChanged += new System.EventHandler(this.ckb_Printable_CheckedChanged);
            // 
            // ckb_DebugEn
            // 
            this.ckb_DebugEn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckb_DebugEn.AutoSize = true;
            this.ckb_DebugEn.BackColor = System.Drawing.SystemColors.Info;
            this.ckb_DebugEn.Checked = true;
            this.ckb_DebugEn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_DebugEn.Location = new System.Drawing.Point(467, 10);
            this.ckb_DebugEn.Name = "ckb_DebugEn";
            this.ckb_DebugEn.Size = new System.Drawing.Size(58, 17);
            this.ckb_DebugEn.TabIndex = 12;
            this.ckb_DebugEn.Text = "Debug";
            this.ckb_DebugEn.UseVisualStyleBackColor = false;
            this.ckb_DebugEn.CheckedChanged += new System.EventHandler(this.ckb_DebugEn_CheckedChanged);
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
            this.rtb_Log.Size = new System.Drawing.Size(612, 448);
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
            this.rtb_Info.Location = new System.Drawing.Point(650, 39);
            this.rtb_Info.Name = "rtb_Info";
            this.rtb_Info.Size = new System.Drawing.Size(357, 449);
            this.rtb_Info.TabIndex = 6;
            this.rtb_Info.Text = "";
            // 
            // bt_Scan
            // 
            this.bt_Scan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Scan.Location = new System.Drawing.Point(736, 11);
            this.bt_Scan.Name = "bt_Scan";
            this.bt_Scan.Size = new System.Drawing.Size(80, 23);
            this.bt_Scan.TabIndex = 5;
            this.bt_Scan.Text = "Scan AT Port";
            this.bt_Scan.UseVisualStyleBackColor = true;
            this.bt_Scan.Click += new System.EventHandler(this.bt_Scan_Click);
            // 
            // bt_RFTest
            // 
            this.bt_RFTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_RFTest.Enabled = false;
            this.bt_RFTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_RFTest.Location = new System.Drawing.Point(929, 527);
            this.bt_RFTest.Name = "bt_RFTest";
            this.bt_RFTest.Size = new System.Drawing.Size(78, 48);
            this.bt_RFTest.TabIndex = 5;
            this.bt_RFTest.Text = "RF Test";
            this.bt_RFTest.UseVisualStyleBackColor = true;
            this.bt_RFTest.Click += new System.EventHandler(this.bt_RFTest_Click);
            // 
            // cb_Port1
            // 
            this.cb_Port1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Port1.FormattingEnabled = true;
            this.cb_Port1.Location = new System.Drawing.Point(650, 12);
            this.cb_Port1.Name = "cb_Port1";
            this.cb_Port1.Size = new System.Drawing.Size(80, 21);
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
            this.tb_Md5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Md5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Md5.ForeColor = System.Drawing.Color.OrangeRed;
            this.tb_Md5.Location = new System.Drawing.Point(50, 527);
            this.tb_Md5.Name = "tb_Md5";
            this.tb_Md5.Size = new System.Drawing.Size(252, 20);
            this.tb_Md5.TabIndex = 7;
            this.tb_Md5.Text = "834406D30F4F1FD20362A93CB3042696";
            this.tb_Md5.TextChanged += new System.EventHandler(this.tb_Md5_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 504);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "URL:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 531);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "MD5:";
            // 
            // pgb_Percent
            // 
            this.pgb_Percent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pgb_Percent.Location = new System.Drawing.Point(650, 498);
            this.pgb_Percent.Name = "pgb_Percent";
            this.pgb_Percent.Size = new System.Drawing.Size(357, 23);
            this.pgb_Percent.TabIndex = 10;
            // 
            // lb_Percent
            // 
            this.lb_Percent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Percent.AutoSize = true;
            this.lb_Percent.BackColor = System.Drawing.Color.Transparent;
            this.lb_Percent.ForeColor = System.Drawing.Color.OrangeRed;
            this.lb_Percent.Location = new System.Drawing.Point(655, 503);
            this.lb_Percent.Name = "lb_Percent";
            this.lb_Percent.Size = new System.Drawing.Size(36, 13);
            this.lb_Percent.TabIndex = 11;
            this.lb_Percent.Text = "0 byte";
            this.lb_Percent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(498, 531);
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
            this.bt_Download.Location = new System.Drawing.Point(845, 527);
            this.bt_Download.Name = "bt_Download";
            this.bt_Download.Size = new System.Drawing.Size(78, 48);
            this.bt_Download.TabIndex = 5;
            this.bt_Download.Text = "Download";
            this.bt_Download.UseVisualStyleBackColor = true;
            this.bt_Download.Click += new System.EventHandler(this.bt_Download_Click);
            // 
            // cb_Apn
            // 
            this.cb_Apn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Apn.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Apn.ForeColor = System.Drawing.Color.OrangeRed;
            this.cb_Apn.FormattingEnabled = true;
            this.cb_Apn.Items.AddRange(new object[] {
            "m-nbiot",
            "m-wap",
            "v-internet"});
            this.cb_Apn.Location = new System.Drawing.Point(536, 527);
            this.cb_Apn.Name = "cb_Apn";
            this.cb_Apn.Size = new System.Drawing.Size(108, 21);
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
            "http://elsa.rpc.vn/assets/images/logo-rp.png",
            "http://103.156.0.37:8081/Test.txt",
            "https://raw.githubusercontent.com/MicrochipTech/XPRESS-Loader/master/utilities/Xp" +
                "ressBL.hex",
            "https://www.mobifone.vn/assets/source/image/logo.png",
            "https://vietteltelecom.vn/images/logo.png",
            "https://www.telit.com/wp-content/uploads/2021/11/Telit_logo.svg",
            "https://s1.vnecdn.net/vnexpress/restruct/i/v658/v2_2019/pc/graphics/logo.svg",
            "https://www.microchip.com/content/experience-fragments/mchp/en_us/site/header/mas" +
                "ter/_jcr_content/root/responsivegrid/header/logo.coreimg.100.300.png/16058280814" +
                "63/microchip.png"});
            this.cb_Url.Location = new System.Drawing.Point(50, 500);
            this.cb_Url.Name = "cb_Url";
            this.cb_Url.Size = new System.Drawing.Size(594, 21);
            this.cb_Url.TabIndex = 14;
            this.cb_Url.Text = "https://raw.githubusercontent.com/dungliem92/DFUTest/master/Test.txt";
            this.cb_Url.TextChanged += new System.EventHandler(this.cb_Url_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 531);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "DNS:";
            // 
            // cb_Dns
            // 
            this.cb_Dns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Dns.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Dns.ForeColor = System.Drawing.Color.OrangeRed;
            this.cb_Dns.FormattingEnabled = true;
            this.cb_Dns.Items.AddRange(new object[] {
            "\"0.0.0.0\",\"0.0.0.0\"",
            "\"1.1.1.1\",\"8.8.8.8\"",
            "\"8.8.8.8\",\"8.8.4.4\""});
            this.cb_Dns.Location = new System.Drawing.Point(347, 527);
            this.cb_Dns.Name = "cb_Dns";
            this.cb_Dns.Size = new System.Drawing.Size(145, 21);
            this.cb_Dns.TabIndex = 16;
            this.cb_Dns.Text = "\"0.0.0.0\",\"0.0.0.0\"";
            this.cb_Dns.TextChanged += new System.EventHandler(this.cb_Dns_TextChanged);
            // 
            // bt_Reboot
            // 
            this.bt_Reboot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Reboot.Location = new System.Drawing.Point(927, 11);
            this.bt_Reboot.Name = "bt_Reboot";
            this.bt_Reboot.Size = new System.Drawing.Size(80, 23);
            this.bt_Reboot.TabIndex = 17;
            this.bt_Reboot.Text = "Reboot";
            this.bt_Reboot.UseVisualStyleBackColor = true;
            this.bt_Reboot.Click += new System.EventHandler(this.bt_Reboot_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 556);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "CMD:";
            // 
            // bt_CMD
            // 
            this.bt_CMD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_CMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_CMD.Location = new System.Drawing.Point(719, 527);
            this.bt_CMD.Name = "bt_CMD";
            this.bt_CMD.Size = new System.Drawing.Size(76, 48);
            this.bt_CMD.TabIndex = 20;
            this.bt_CMD.Text = "Send CMD";
            this.bt_CMD.UseVisualStyleBackColor = true;
            this.bt_CMD.Click += new System.EventHandler(this.bt_CMD_Click);
            // 
            // cb_CMD
            // 
            this.cb_CMD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_CMD.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_CMD.ForeColor = System.Drawing.Color.Blue;
            this.cb_CMD.FormattingEnabled = true;
            this.cb_CMD.Location = new System.Drawing.Point(50, 554);
            this.cb_CMD.Name = "cb_CMD";
            this.cb_CMD.Size = new System.Drawing.Size(594, 21);
            this.cb_CMD.TabIndex = 21;
            this.cb_CMD.Text = "AT\\r";
            // 
            // nud_RebootWait
            // 
            this.nud_RebootWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_RebootWait.Location = new System.Drawing.Point(860, 12);
            this.nud_RebootWait.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.nud_RebootWait.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nud_RebootWait.Name = "nud_RebootWait";
            this.nud_RebootWait.Size = new System.Drawing.Size(61, 20);
            this.nud_RebootWait.TabIndex = 24;
            this.nud_RebootWait.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nud_RebootWait.ValueChanged += new System.EventHandler(this.nud_RebootWait_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(822, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Boot:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(652, 529);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Response:";
            // 
            // nud_RespWait
            // 
            this.nud_RespWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_RespWait.Location = new System.Drawing.Point(650, 554);
            this.nud_RespWait.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.nud_RespWait.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nud_RespWait.Name = "nud_RespWait";
            this.nud_RespWait.Size = new System.Drawing.Size(63, 20);
            this.nud_RespWait.TabIndex = 26;
            this.nud_RespWait.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nud_RespWait.ValueChanged += new System.EventHandler(this.nud_RespWait_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(801, 531);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Count:";
            // 
            // nud_DlCount
            // 
            this.nud_DlCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_DlCount.Location = new System.Drawing.Point(804, 554);
            this.nud_DlCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nud_DlCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_DlCount.Name = "nud_DlCount";
            this.nud_DlCount.Size = new System.Drawing.Size(35, 20);
            this.nud_DlCount.TabIndex = 28;
            this.nud_DlCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_DlCount.ValueChanged += new System.EventHandler(this.nud_DlCount_ValueChanged);
            // 
            // lklb_GMaps
            // 
            this.lklb_GMaps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lklb_GMaps.AutoSize = true;
            this.lklb_GMaps.Location = new System.Drawing.Point(47, 582);
            this.lklb_GMaps.Name = "lklb_GMaps";
            this.lklb_GMaps.Size = new System.Drawing.Size(70, 13);
            this.lklb_GMaps.TabIndex = 23;
            this.lklb_GMaps.TabStop = true;
            this.lklb_GMaps.Text = "Google Maps";
            this.lklb_GMaps.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklb_GMaps_LinkClicked);
            // 
            // lklb_CellFind
            // 
            this.lklb_CellFind.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lklb_CellFind.AutoSize = true;
            this.lklb_CellFind.Location = new System.Drawing.Point(317, 582);
            this.lklb_CellFind.Name = "lklb_CellFind";
            this.lklb_CellFind.Size = new System.Drawing.Size(70, 13);
            this.lklb_CellFind.TabIndex = 30;
            this.lklb_CellFind.TabStop = true;
            this.lklb_CellFind.Text = "Cell ID Finder";
            this.lklb_CellFind.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklb_CellFind_LinkClicked);
            // 
            // lklb_LogFolder
            // 
            this.lklb_LogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lklb_LogFolder.AutoSize = true;
            this.lklb_LogFolder.Location = new System.Drawing.Point(587, 582);
            this.lklb_LogFolder.Name = "lklb_LogFolder";
            this.lklb_LogFolder.Size = new System.Drawing.Size(57, 13);
            this.lklb_LogFolder.TabIndex = 31;
            this.lklb_LogFolder.TabStop = true;
            this.lklb_LogFolder.Text = "Log Folder";
            this.lklb_LogFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklb_LogFolder_LinkClicked);
            // 
            // lklb_Screenshot
            // 
            this.lklb_Screenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lklb_Screenshot.AutoSize = true;
            this.lklb_Screenshot.Location = new System.Drawing.Point(941, 582);
            this.lklb_Screenshot.Name = "lklb_Screenshot";
            this.lklb_Screenshot.Size = new System.Drawing.Size(66, 13);
            this.lklb_Screenshot.TabIndex = 32;
            this.lklb_Screenshot.TabStop = true;
            this.lklb_Screenshot.Text = "Screen Shot";
            this.lklb_Screenshot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklb_Screenshot_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1019, 601);
            this.Controls.Add(this.lklb_Screenshot);
            this.Controls.Add(this.lklb_LogFolder);
            this.Controls.Add(this.lklb_CellFind);
            this.Controls.Add(this.lklb_GMaps);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nud_DlCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nud_RespWait);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nud_RebootWait);
            this.Controls.Add(this.cb_CMD);
            this.Controls.Add(this.bt_CMD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bt_Reboot);
            this.Controls.Add(this.cb_Dns);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_Url);
            this.Controls.Add(this.cb_Apn);
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
            this.tabLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RebootWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RespWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_DlCount)).EndInit();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bt_CMD;
        private System.Windows.Forms.ComboBox cb_CMD;
        private System.Windows.Forms.CheckBox ckb_Printable;
        private System.Windows.Forms.NumericUpDown nud_RebootWait;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nud_RespWait;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nud_DlCount;
        private System.Windows.Forms.LinkLabel lklb_GMaps;
        private System.Windows.Forms.LinkLabel lklb_CellFind;
        private System.Windows.Forms.LinkLabel lklb_LogFolder;
        private System.Windows.Forms.LinkLabel lklb_Screenshot;
    }
}

