namespace Form1
{
    partial class FormSPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSPT));
            this.serialCdc = new System.IO.Ports.SerialPort(this.components);
            this.serialForward = new System.IO.Ports.SerialPort(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lb_Status = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lb_Port1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_Port2 = new System.Windows.Forms.Label();
            this.cb_Baud = new System.Windows.Forms.ComboBox();
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.lkl_Homepage = new System.Windows.Forms.LinkLabel();
            this.serialDfu = new System.IO.Ports.SerialPort(this.components);
            this.tb_FilePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabForward = new System.Windows.Forms.TabPage();
            this.rtb_CmdData = new System.Windows.Forms.RichTextBox();
            this.bt_SetPort2 = new System.Windows.Forms.Button();
            this.bt_SetPort1 = new System.Windows.Forms.Button();
            this.ckb_Script = new System.Windows.Forms.CheckBox();
            this.nud_Interval = new System.Windows.Forms.NumericUpDown();
            this.ckb_LF = new System.Windows.Forms.CheckBox();
            this.ckb_CR = new System.Windows.Forms.CheckBox();
            this.ckb_Loop = new System.Windows.Forms.CheckBox();
            this.bt_Script = new System.Windows.Forms.Button();
            this.bt_Send = new System.Windows.Forms.Button();
            this.bt_StopSend = new System.Windows.Forms.Button();
            this.bt_FwdStart = new System.Windows.Forms.Button();
            this.bt_FwdStop = new System.Windows.Forms.Button();
            this.bt_FwdScan = new System.Windows.Forms.Button();
            this.cb_Port = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabDfu = new System.Windows.Forms.TabPage();
            this.nud_FrameDelay = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_DfuBaud = new System.Windows.Forms.ComboBox();
            this.cb_DfuPort = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bt_Browse = new System.Windows.Forms.Button();
            this.bt_DfuScan = new System.Windows.Forms.Button();
            this.bt_DfuStart = new System.Windows.Forms.Button();
            this.bt_DfuStop = new System.Windows.Forms.Button();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.bt_DiskMngr = new System.Windows.Forms.Button();
            this.bt_DeviceMngr = new System.Windows.Forms.Button();
            this.bt_TaskMngr = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.pic_OpenLog = new System.Windows.Forms.PictureBox();
            this.picConnected = new System.Windows.Forms.PictureBox();
            this.picHomePage = new System.Windows.Forms.PictureBox();
            this.picDisconnected = new System.Windows.Forms.PictureBox();
            this.lb_OpenLog = new System.Windows.Forms.Label();
            this.lb_Notic = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabForward.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Interval)).BeginInit();
            this.tabDfu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_FrameDelay)).BeginInit();
            this.tabTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_OpenLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConnected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHomePage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDisconnected)).BeginInit();
            this.SuspendLayout();
            // 
            // serialCdc
            // 
            this.serialCdc.DtrEnable = true;
            this.serialCdc.RtsEnable = true;
            this.serialCdc.WriteBufferSize = 4096;
            this.serialCdc.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // serialForward
            // 
            this.serialForward.DtrEnable = true;
            this.serialForward.RtsEnable = true;
            this.serialForward.WriteBufferSize = 4096;
            this.serialForward.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Icons-Land-Play-Stop-Pause-Step-Forward-Normal.ico");
            this.imageList1.Images.SetKeyName(1, "Icons-Land-Play-Stop-Pause-Stop-Normal-Red.ico");
            this.imageList1.Images.SetKeyName(2, "Praveen-Minimal-Outline-Next.ico");
            this.imageList1.Images.SetKeyName(3, "Oxygen-Icons.org-Oxygen-Actions-page-zoom.ico");
            this.imageList1.Images.SetKeyName(4, "Oxygen-Icons.org-Oxygen-Actions-arrow-down-double.ico");
            this.imageList1.Images.SetKeyName(5, "Visualpharm-Must-Have-Next.ico");
            this.imageList1.Images.SetKeyName(6, "SAMPI Logo.ico");
            this.imageList1.Images.SetKeyName(7, "Oxygen-Icons.org-Oxygen-Mimetypes-text-x-hex.ico");
            this.imageList1.Images.SetKeyName(8, "Oxygen-Icons.org-Oxygen-Mimetypes-text-x-script.ico");
            this.imageList1.Images.SetKeyName(9, "Chrisbanks2-Cold-Fusion-Hd-Task-manager.ico");
            this.imageList1.Images.SetKeyName(10, "Custom-Icon-Design-Pretty-Office-5-Refresh.ico");
            this.imageList1.Images.SetKeyName(11, "Saki-NuoveXT-2-Devices-drive-removable-usb.ico");
            this.imageList1.Images.SetKeyName(12, "Seanau-Email-Clear.ico");
            this.imageList1.Images.SetKeyName(13, "Visualpharm-Must-Have-Next.ico");
            this.imageList1.Images.SetKeyName(14, "Untitled-Diagram.ico");
            this.imageList1.Images.SetKeyName(15, "Oxygen-Icons.org-Oxygen-Devices-audio-card.ico");
            // 
            // lb_Status
            // 
            this.lb_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_Status.AutoSize = true;
            this.lb_Status.ForeColor = System.Drawing.Color.Blue;
            this.lb_Status.Location = new System.Drawing.Point(44, 573);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(78, 13);
            this.lb_Status.TabIndex = 6;
            this.lb_Status.Text = "No Connection";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SPF";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // lb_Port1
            // 
            this.lb_Port1.AutoSize = true;
            this.lb_Port1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Port1.Location = new System.Drawing.Point(173, 9);
            this.lb_Port1.Name = "lb_Port1";
            this.lb_Port1.Size = new System.Drawing.Size(79, 15);
            this.lb_Port1.TabIndex = 2;
            this.lb_Port1.Text = "Port 1: Empty";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Baud:";
            // 
            // lb_Port2
            // 
            this.lb_Port2.AutoSize = true;
            this.lb_Port2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Port2.Location = new System.Drawing.Point(173, 41);
            this.lb_Port2.Name = "lb_Port2";
            this.lb_Port2.Size = new System.Drawing.Size(79, 15);
            this.lb_Port2.TabIndex = 3;
            this.lb_Port2.Text = "Port 2: Empty";
            // 
            // cb_Baud
            // 
            this.cb_Baud.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Baud.FormattingEnabled = true;
            this.cb_Baud.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.cb_Baud.Location = new System.Drawing.Point(51, 38);
            this.cb_Baud.Name = "cb_Baud";
            this.cb_Baud.Size = new System.Drawing.Size(68, 23);
            this.cb_Baud.TabIndex = 4;
            this.cb_Baud.Text = "9600";
            // 
            // rtb_Log
            // 
            this.rtb_Log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rtb_Log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rtb_Log.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Log.Location = new System.Drawing.Point(9, 257);
            this.rtb_Log.Name = "rtb_Log";
            this.rtb_Log.ReadOnly = true;
            this.rtb_Log.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rtb_Log.Size = new System.Drawing.Size(388, 303);
            this.rtb_Log.TabIndex = 10;
            this.rtb_Log.Text = "";
            this.rtb_Log.TextChanged += new System.EventHandler(this.rtb_Log_TextChanged);
            this.rtb_Log.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rtb_Log_MouseDoubleClick);
            // 
            // lkl_Homepage
            // 
            this.lkl_Homepage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lkl_Homepage.AutoSize = true;
            this.lkl_Homepage.Location = new System.Drawing.Point(319, 573);
            this.lkl_Homepage.Name = "lkl_Homepage";
            this.lkl_Homepage.Size = new System.Drawing.Size(78, 13);
            this.lkl_Homepage.TabIndex = 11;
            this.lkl_Homepage.TabStop = true;
            this.lkl_Homepage.Text = "SAMPI Dev Kit";
            this.lkl_Homepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_Homepage_LinkClicked);
            // 
            // serialDfu
            // 
            this.serialDfu.DtrEnable = true;
            this.serialDfu.RtsEnable = true;
            this.serialDfu.WriteBufferSize = 4096;
            // 
            // tb_FilePath
            // 
            this.tb_FilePath.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.tb_FilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tb_FilePath.Location = new System.Drawing.Point(41, 70);
            this.tb_FilePath.Name = "tb_FilePath";
            this.tb_FilePath.ReadOnly = true;
            this.tb_FilePath.Size = new System.Drawing.Size(337, 25);
            this.tb_FilePath.TabIndex = 16;
            this.tb_FilePath.Text = "Empty";
            this.tb_FilePath.TextChanged += new System.EventHandler(this.tb_FilePath_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Hex files (*.hex)|*.hex";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabForward);
            this.tabControl1.Controls.Add(this.tabDfu);
            this.tabControl1.Controls.Add(this.tabTools);
            this.tabControl1.Location = new System.Drawing.Point(9, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(392, 243);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 18;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabForward
            // 
            this.tabForward.Controls.Add(this.rtb_CmdData);
            this.tabForward.Controls.Add(this.bt_SetPort2);
            this.tabForward.Controls.Add(this.bt_SetPort1);
            this.tabForward.Controls.Add(this.ckb_Script);
            this.tabForward.Controls.Add(this.nud_Interval);
            this.tabForward.Controls.Add(this.ckb_LF);
            this.tabForward.Controls.Add(this.ckb_CR);
            this.tabForward.Controls.Add(this.ckb_Loop);
            this.tabForward.Controls.Add(this.bt_Script);
            this.tabForward.Controls.Add(this.bt_Send);
            this.tabForward.Controls.Add(this.bt_StopSend);
            this.tabForward.Controls.Add(this.bt_FwdStart);
            this.tabForward.Controls.Add(this.bt_FwdStop);
            this.tabForward.Controls.Add(this.bt_FwdScan);
            this.tabForward.Controls.Add(this.label3);
            this.tabForward.Controls.Add(this.cb_Port);
            this.tabForward.Controls.Add(this.cb_Baud);
            this.tabForward.Controls.Add(this.lb_Port2);
            this.tabForward.Controls.Add(this.label4);
            this.tabForward.Controls.Add(this.lb_Port1);
            this.tabForward.Location = new System.Drawing.Point(4, 22);
            this.tabForward.Name = "tabForward";
            this.tabForward.Padding = new System.Windows.Forms.Padding(3);
            this.tabForward.Size = new System.Drawing.Size(384, 217);
            this.tabForward.TabIndex = 0;
            this.tabForward.Text = "Forward";
            this.tabForward.UseVisualStyleBackColor = true;
            // 
            // rtb_CmdData
            // 
            this.rtb_CmdData.BackColor = System.Drawing.SystemColors.Control;
            this.rtb_CmdData.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_CmdData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.rtb_CmdData.Location = new System.Drawing.Point(6, 93);
            this.rtb_CmdData.Name = "rtb_CmdData";
            this.rtb_CmdData.Size = new System.Drawing.Size(311, 116);
            this.rtb_CmdData.TabIndex = 38;
            this.rtb_CmdData.Text = "AT";
            // 
            // bt_SetPort2
            // 
            this.bt_SetPort2.Location = new System.Drawing.Point(125, 38);
            this.bt_SetPort2.Name = "bt_SetPort2";
            this.bt_SetPort2.Size = new System.Drawing.Size(42, 23);
            this.bt_SetPort2.TabIndex = 37;
            this.bt_SetPort2.Text = ">>>";
            this.bt_SetPort2.UseVisualStyleBackColor = true;
            // 
            // bt_SetPort1
            // 
            this.bt_SetPort1.ImageList = this.imageList1;
            this.bt_SetPort1.Location = new System.Drawing.Point(125, 6);
            this.bt_SetPort1.Name = "bt_SetPort1";
            this.bt_SetPort1.Size = new System.Drawing.Size(42, 23);
            this.bt_SetPort1.TabIndex = 37;
            this.bt_SetPort1.Text = ">>>";
            this.bt_SetPort1.UseVisualStyleBackColor = true;
            // 
            // ckb_Script
            // 
            this.ckb_Script.AutoSize = true;
            this.ckb_Script.Location = new System.Drawing.Point(182, 67);
            this.ckb_Script.Name = "ckb_Script";
            this.ckb_Script.Size = new System.Drawing.Size(53, 17);
            this.ckb_Script.TabIndex = 33;
            this.ckb_Script.Text = "Script";
            this.ckb_Script.UseVisualStyleBackColor = true;
            this.ckb_Script.CheckedChanged += new System.EventHandler(this.ckb_Script_CheckedChanged);
            // 
            // nud_Interval
            // 
            this.nud_Interval.Location = new System.Drawing.Point(323, 67);
            this.nud_Interval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_Interval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nud_Interval.Name = "nud_Interval";
            this.nud_Interval.Size = new System.Drawing.Size(55, 20);
            this.nud_Interval.TabIndex = 34;
            this.nud_Interval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // ckb_LF
            // 
            this.ckb_LF.AutoSize = true;
            this.ckb_LF.Location = new System.Drawing.Point(118, 67);
            this.ckb_LF.Name = "ckb_LF";
            this.ckb_LF.Size = new System.Drawing.Size(38, 17);
            this.ckb_LF.TabIndex = 30;
            this.ckb_LF.Text = "LF";
            this.ckb_LF.UseVisualStyleBackColor = true;
            // 
            // ckb_CR
            // 
            this.ckb_CR.AutoSize = true;
            this.ckb_CR.Checked = true;
            this.ckb_CR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_CR.Location = new System.Drawing.Point(51, 67);
            this.ckb_CR.Name = "ckb_CR";
            this.ckb_CR.Size = new System.Drawing.Size(41, 17);
            this.ckb_CR.TabIndex = 31;
            this.ckb_CR.Text = "CR";
            this.ckb_CR.UseVisualStyleBackColor = true;
            // 
            // ckb_Loop
            // 
            this.ckb_Loop.AutoSize = true;
            this.ckb_Loop.Location = new System.Drawing.Point(262, 67);
            this.ckb_Loop.Name = "ckb_Loop";
            this.ckb_Loop.Size = new System.Drawing.Size(50, 17);
            this.ckb_Loop.TabIndex = 32;
            this.ckb_Loop.Text = "Loop";
            this.ckb_Loop.UseVisualStyleBackColor = true;
            // 
            // bt_Script
            // 
            this.bt_Script.Enabled = false;
            this.bt_Script.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Script.ForeColor = System.Drawing.Color.Red;
            this.bt_Script.ImageIndex = 8;
            this.bt_Script.ImageList = this.imageList1;
            this.bt_Script.Location = new System.Drawing.Point(323, 93);
            this.bt_Script.Name = "bt_Script";
            this.bt_Script.Size = new System.Drawing.Size(55, 55);
            this.bt_Script.TabIndex = 36;
            this.bt_Script.UseVisualStyleBackColor = true;
            // 
            // bt_Send
            // 
            this.bt_Send.Enabled = false;
            this.bt_Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Send.ForeColor = System.Drawing.Color.Red;
            this.bt_Send.ImageIndex = 5;
            this.bt_Send.ImageList = this.imageList1;
            this.bt_Send.Location = new System.Drawing.Point(323, 154);
            this.bt_Send.Name = "bt_Send";
            this.bt_Send.Size = new System.Drawing.Size(55, 55);
            this.bt_Send.TabIndex = 29;
            this.bt_Send.UseVisualStyleBackColor = true;
            // 
            // bt_StopSend
            // 
            this.bt_StopSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_StopSend.ForeColor = System.Drawing.Color.Red;
            this.bt_StopSend.ImageIndex = 1;
            this.bt_StopSend.ImageList = this.imageList1;
            this.bt_StopSend.Location = new System.Drawing.Point(323, 154);
            this.bt_StopSend.Name = "bt_StopSend";
            this.bt_StopSend.Size = new System.Drawing.Size(55, 55);
            this.bt_StopSend.TabIndex = 35;
            this.bt_StopSend.UseVisualStyleBackColor = true;
            this.bt_StopSend.Visible = false;
            // 
            // bt_FwdStart
            // 
            this.bt_FwdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_FwdStart.ForeColor = System.Drawing.Color.Red;
            this.bt_FwdStart.ImageIndex = 0;
            this.bt_FwdStart.ImageList = this.imageList1;
            this.bt_FwdStart.Location = new System.Drawing.Point(323, 6);
            this.bt_FwdStart.Name = "bt_FwdStart";
            this.bt_FwdStart.Size = new System.Drawing.Size(55, 55);
            this.bt_FwdStart.TabIndex = 5;
            this.bt_FwdStart.UseVisualStyleBackColor = true;
            this.bt_FwdStart.Click += new System.EventHandler(this.bt_FwdStart_Click);
            // 
            // bt_FwdStop
            // 
            this.bt_FwdStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_FwdStop.ForeColor = System.Drawing.Color.Red;
            this.bt_FwdStop.ImageIndex = 1;
            this.bt_FwdStop.ImageList = this.imageList1;
            this.bt_FwdStop.Location = new System.Drawing.Point(323, 6);
            this.bt_FwdStop.Name = "bt_FwdStop";
            this.bt_FwdStop.Size = new System.Drawing.Size(55, 55);
            this.bt_FwdStop.TabIndex = 12;
            this.bt_FwdStop.UseVisualStyleBackColor = true;
            this.bt_FwdStop.Visible = false;
            this.bt_FwdStop.Click += new System.EventHandler(this.bt_FwdStop_Click);
            // 
            // bt_FwdScan
            // 
            this.bt_FwdScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_FwdScan.ForeColor = System.Drawing.Color.Red;
            this.bt_FwdScan.ImageIndex = 3;
            this.bt_FwdScan.ImageList = this.imageList1;
            this.bt_FwdScan.Location = new System.Drawing.Point(262, 6);
            this.bt_FwdScan.Name = "bt_FwdScan";
            this.bt_FwdScan.Size = new System.Drawing.Size(55, 55);
            this.bt_FwdScan.TabIndex = 13;
            this.bt_FwdScan.UseVisualStyleBackColor = true;
            this.bt_FwdScan.Click += new System.EventHandler(this.bt_FwdScan_Click);
            // 
            // cb_Port
            // 
            this.cb_Port.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Port.FormattingEnabled = true;
            this.cb_Port.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.cb_Port.Location = new System.Drawing.Point(51, 6);
            this.cb_Port.Name = "cb_Port";
            this.cb_Port.Size = new System.Drawing.Size(68, 23);
            this.cb_Port.TabIndex = 4;
            this.cb_Port.Text = "Empty";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Port:";
            // 
            // tabDfu
            // 
            this.tabDfu.BackColor = System.Drawing.SystemColors.Window;
            this.tabDfu.Controls.Add(this.nud_FrameDelay);
            this.tabDfu.Controls.Add(this.label8);
            this.tabDfu.Controls.Add(this.label6);
            this.tabDfu.Controls.Add(this.cb_DfuBaud);
            this.tabDfu.Controls.Add(this.cb_DfuPort);
            this.tabDfu.Controls.Add(this.label7);
            this.tabDfu.Controls.Add(this.label5);
            this.tabDfu.Controls.Add(this.tb_FilePath);
            this.tabDfu.Controls.Add(this.bt_Browse);
            this.tabDfu.Controls.Add(this.bt_DfuScan);
            this.tabDfu.Controls.Add(this.bt_DfuStart);
            this.tabDfu.Controls.Add(this.bt_DfuStop);
            this.tabDfu.Location = new System.Drawing.Point(4, 22);
            this.tabDfu.Name = "tabDfu";
            this.tabDfu.Padding = new System.Windows.Forms.Padding(3);
            this.tabDfu.Size = new System.Drawing.Size(384, 217);
            this.tabDfu.TabIndex = 1;
            this.tabDfu.Text = "DFU";
            // 
            // nud_FrameDelay
            // 
            this.nud_FrameDelay.Location = new System.Drawing.Point(175, 41);
            this.nud_FrameDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_FrameDelay.Name = "nud_FrameDelay";
            this.nud_FrameDelay.Size = new System.Drawing.Size(81, 20);
            this.nud_FrameDelay.TabIndex = 19;
            this.nud_FrameDelay.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(128, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "Delay:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(128, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "Baud:";
            // 
            // cb_DfuBaud
            // 
            this.cb_DfuBaud.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_DfuBaud.FormattingEnabled = true;
            this.cb_DfuBaud.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.cb_DfuBaud.Location = new System.Drawing.Point(175, 6);
            this.cb_DfuBaud.Name = "cb_DfuBaud";
            this.cb_DfuBaud.Size = new System.Drawing.Size(81, 23);
            this.cb_DfuBaud.TabIndex = 22;
            this.cb_DfuBaud.Text = "9600";
            // 
            // cb_DfuPort
            // 
            this.cb_DfuPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_DfuPort.FormattingEnabled = true;
            this.cb_DfuPort.Items.AddRange(new object[] {
            "Empty"});
            this.cb_DfuPort.Location = new System.Drawing.Point(41, 6);
            this.cb_DfuPort.Name = "cb_DfuPort";
            this.cb_DfuPort.Size = new System.Drawing.Size(81, 23);
            this.cb_DfuPort.TabIndex = 19;
            this.cb_DfuPort.Text = "Empty";
            this.cb_DfuPort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_DfuPort_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 15);
            this.label7.TabIndex = 21;
            this.label7.Text = "Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "File:";
            // 
            // bt_Browse
            // 
            this.bt_Browse.ImageList = this.imageList1;
            this.bt_Browse.Location = new System.Drawing.Point(41, 38);
            this.bt_Browse.Name = "bt_Browse";
            this.bt_Browse.Size = new System.Drawing.Size(81, 23);
            this.bt_Browse.TabIndex = 17;
            this.bt_Browse.Text = "Browse";
            this.bt_Browse.UseVisualStyleBackColor = true;
            this.bt_Browse.Click += new System.EventHandler(this.bt_Browse_Click);
            // 
            // bt_DfuScan
            // 
            this.bt_DfuScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DfuScan.ForeColor = System.Drawing.Color.Red;
            this.bt_DfuScan.ImageIndex = 3;
            this.bt_DfuScan.ImageList = this.imageList1;
            this.bt_DfuScan.Location = new System.Drawing.Point(262, 6);
            this.bt_DfuScan.Name = "bt_DfuScan";
            this.bt_DfuScan.Size = new System.Drawing.Size(55, 55);
            this.bt_DfuScan.TabIndex = 16;
            this.bt_DfuScan.UseVisualStyleBackColor = true;
            this.bt_DfuScan.Click += new System.EventHandler(this.bt_DfuScan_Click);
            // 
            // bt_DfuStart
            // 
            this.bt_DfuStart.Enabled = false;
            this.bt_DfuStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DfuStart.ForeColor = System.Drawing.Color.Red;
            this.bt_DfuStart.ImageIndex = 4;
            this.bt_DfuStart.ImageList = this.imageList1;
            this.bt_DfuStart.Location = new System.Drawing.Point(323, 6);
            this.bt_DfuStart.Name = "bt_DfuStart";
            this.bt_DfuStart.Size = new System.Drawing.Size(55, 55);
            this.bt_DfuStart.TabIndex = 14;
            this.bt_DfuStart.UseVisualStyleBackColor = true;
            this.bt_DfuStart.Click += new System.EventHandler(this.bt_DfuStart_Click);
            // 
            // bt_DfuStop
            // 
            this.bt_DfuStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DfuStop.ForeColor = System.Drawing.Color.Red;
            this.bt_DfuStop.ImageIndex = 1;
            this.bt_DfuStop.ImageList = this.imageList1;
            this.bt_DfuStop.Location = new System.Drawing.Point(323, 6);
            this.bt_DfuStop.Name = "bt_DfuStop";
            this.bt_DfuStop.Size = new System.Drawing.Size(55, 55);
            this.bt_DfuStop.TabIndex = 15;
            this.bt_DfuStop.UseVisualStyleBackColor = true;
            this.bt_DfuStop.Visible = false;
            this.bt_DfuStop.Click += new System.EventHandler(this.bt_DfuStop_Click);
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.label21);
            this.tabTools.Controls.Add(this.label18);
            this.tabTools.Controls.Add(this.label17);
            this.tabTools.Controls.Add(this.bt_DiskMngr);
            this.tabTools.Controls.Add(this.bt_DeviceMngr);
            this.tabTools.Controls.Add(this.bt_TaskMngr);
            this.tabTools.Location = new System.Drawing.Point(4, 22);
            this.tabTools.Name = "tabTools";
            this.tabTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabTools.Size = new System.Drawing.Size(384, 217);
            this.tabTools.TabIndex = 4;
            this.tabTools.Text = "Tools";
            this.tabTools.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(122, 61);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(49, 26);
            this.label21.TabIndex = 26;
            this.label21.Text = "Disk \r\nManager";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(64, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 26);
            this.label18.TabIndex = 20;
            this.label18.Text = "Device\r\nManager";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 61);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 26);
            this.label17.TabIndex = 19;
            this.label17.Text = "Task\r\nManager";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_DiskMngr
            // 
            this.bt_DiskMngr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DiskMngr.ForeColor = System.Drawing.Color.Red;
            this.bt_DiskMngr.ImageIndex = 11;
            this.bt_DiskMngr.ImageList = this.imageList1;
            this.bt_DiskMngr.Location = new System.Drawing.Point(120, 6);
            this.bt_DiskMngr.Name = "bt_DiskMngr";
            this.bt_DiskMngr.Size = new System.Drawing.Size(52, 52);
            this.bt_DiskMngr.TabIndex = 25;
            this.bt_DiskMngr.UseVisualStyleBackColor = true;
            this.bt_DiskMngr.Click += new System.EventHandler(this.bt_DiskMngr_Click);
            // 
            // bt_DeviceMngr
            // 
            this.bt_DeviceMngr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DeviceMngr.ForeColor = System.Drawing.Color.Red;
            this.bt_DeviceMngr.ImageIndex = 15;
            this.bt_DeviceMngr.ImageList = this.imageList1;
            this.bt_DeviceMngr.Location = new System.Drawing.Point(62, 6);
            this.bt_DeviceMngr.Name = "bt_DeviceMngr";
            this.bt_DeviceMngr.Size = new System.Drawing.Size(52, 52);
            this.bt_DeviceMngr.TabIndex = 18;
            this.bt_DeviceMngr.UseVisualStyleBackColor = true;
            this.bt_DeviceMngr.Click += new System.EventHandler(this.bt_DeviceMngr_Click);
            // 
            // bt_TaskMngr
            // 
            this.bt_TaskMngr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_TaskMngr.ForeColor = System.Drawing.Color.Red;
            this.bt_TaskMngr.ImageIndex = 9;
            this.bt_TaskMngr.ImageList = this.imageList1;
            this.bt_TaskMngr.Location = new System.Drawing.Point(4, 6);
            this.bt_TaskMngr.Name = "bt_TaskMngr";
            this.bt_TaskMngr.Size = new System.Drawing.Size(52, 52);
            this.bt_TaskMngr.TabIndex = 17;
            this.bt_TaskMngr.UseVisualStyleBackColor = true;
            this.bt_TaskMngr.Click += new System.EventHandler(this.bt_TaskMngr_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "Script files (*.scrpt)|*.scrpt";
            // 
            // pic_OpenLog
            // 
            this.pic_OpenLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_OpenLog.Image = global::SerialPortTerminal.Properties.Resources.text_x_log_icon;
            this.pic_OpenLog.Location = new System.Drawing.Point(189, 566);
            this.pic_OpenLog.Name = "pic_OpenLog";
            this.pic_OpenLog.Size = new System.Drawing.Size(29, 26);
            this.pic_OpenLog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic_OpenLog.TabIndex = 22;
            this.pic_OpenLog.TabStop = false;
            this.pic_OpenLog.Click += new System.EventHandler(this.pic_OpenLog_Click);
            // 
            // picConnected
            // 
            this.picConnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picConnected.Image = global::SerialPortTerminal.Properties.Resources.plug_connect_icon;
            this.picConnected.Location = new System.Drawing.Point(9, 566);
            this.picConnected.Name = "picConnected";
            this.picConnected.Size = new System.Drawing.Size(29, 26);
            this.picConnected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picConnected.TabIndex = 21;
            this.picConnected.TabStop = false;
            this.picConnected.Visible = false;
            // 
            // picHomePage
            // 
            this.picHomePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picHomePage.Image = global::SerialPortTerminal.Properties.Resources.home_icon;
            this.picHomePage.Location = new System.Drawing.Point(284, 566);
            this.picHomePage.Name = "picHomePage";
            this.picHomePage.Size = new System.Drawing.Size(29, 26);
            this.picHomePage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picHomePage.TabIndex = 20;
            this.picHomePage.TabStop = false;
            this.picHomePage.Click += new System.EventHandler(this.picHomePage_Click);
            // 
            // picDisconnected
            // 
            this.picDisconnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picDisconnected.Image = global::SerialPortTerminal.Properties.Resources.plug_disconnect_prohibition_icon;
            this.picDisconnected.Location = new System.Drawing.Point(9, 566);
            this.picDisconnected.Name = "picDisconnected";
            this.picDisconnected.Size = new System.Drawing.Size(29, 26);
            this.picDisconnected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDisconnected.TabIndex = 19;
            this.picDisconnected.TabStop = false;
            // 
            // lb_OpenLog
            // 
            this.lb_OpenLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_OpenLog.AutoSize = true;
            this.lb_OpenLog.BackColor = System.Drawing.Color.Transparent;
            this.lb_OpenLog.ForeColor = System.Drawing.Color.Blue;
            this.lb_OpenLog.Location = new System.Drawing.Point(224, 573);
            this.lb_OpenLog.Name = "lb_OpenLog";
            this.lb_OpenLog.Size = new System.Drawing.Size(54, 13);
            this.lb_OpenLog.TabIndex = 23;
            this.lb_OpenLog.Text = "Open Log";
            this.lb_OpenLog.Click += new System.EventHandler(this.lb_OpenLog_Click);
            // 
            // lb_Notic
            // 
            this.lb_Notic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_Notic.AutoSize = true;
            this.lb_Notic.ForeColor = System.Drawing.Color.Tomato;
            this.lb_Notic.Location = new System.Drawing.Point(19, 267);
            this.lb_Notic.Name = "lb_Notic";
            this.lb_Notic.Size = new System.Drawing.Size(168, 13);
            this.lb_Notic.TabIndex = 24;
            this.lb_Notic.Text = "Text has been copied to clipboard";
            this.lb_Notic.Visible = false;
            // 
            // timer2
            // 
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // FormSPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(409, 601);
            this.Controls.Add(this.lb_Notic);
            this.Controls.Add(this.lb_OpenLog);
            this.Controls.Add(this.pic_OpenLog);
            this.Controls.Add(this.picConnected);
            this.Controls.Add(this.picHomePage);
            this.Controls.Add(this.picDisconnected);
            this.Controls.Add(this.lkl_Homepage);
            this.Controls.Add(this.lb_Status);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.rtb_Log);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSPT";
            this.Text = "SAMPI UTILITIES";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabForward.ResumeLayout(false);
            this.tabForward.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Interval)).EndInit();
            this.tabDfu.ResumeLayout(false);
            this.tabDfu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_FrameDelay)).EndInit();
            this.tabTools.ResumeLayout(false);
            this.tabTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_OpenLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConnected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHomePage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDisconnected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialCdc;
        private System.IO.Ports.SerialPort serialForward;
        private System.Windows.Forms.Button bt_FwdStart;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label lb_Port1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_Port2;
        private System.Windows.Forms.ComboBox cb_Baud;
        private System.Windows.Forms.RichTextBox rtb_Log;
        private System.Windows.Forms.LinkLabel lkl_Homepage;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button bt_FwdStop;
        private System.Windows.Forms.Button bt_FwdScan;
        private System.Windows.Forms.Button bt_DfuStop;
        private System.Windows.Forms.Button bt_DfuStart;
        private System.IO.Ports.SerialPort serialDfu;
        private System.Windows.Forms.TextBox tb_FilePath;
        private System.Windows.Forms.Button bt_Browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabForward;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabDfu;
        private System.Windows.Forms.Button bt_DfuScan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_DfuBaud;
        private System.Windows.Forms.ComboBox cb_DfuPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nud_FrameDelay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox picDisconnected;
        private System.Windows.Forms.PictureBox picHomePage;
        private System.Windows.Forms.PictureBox picConnected;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.TabPage tabTools;
        private System.Windows.Forms.Button bt_TaskMngr;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button bt_DeviceMngr;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button bt_DiskMngr;
        private System.Windows.Forms.PictureBox pic_OpenLog;
        private System.Windows.Forms.Label lb_OpenLog;
        private System.Windows.Forms.Label lb_Notic;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox ckb_Script;
        private System.Windows.Forms.NumericUpDown nud_Interval;
        private System.Windows.Forms.CheckBox ckb_LF;
        private System.Windows.Forms.CheckBox ckb_CR;
        private System.Windows.Forms.CheckBox ckb_Loop;
        private System.Windows.Forms.Button bt_Script;
        private System.Windows.Forms.Button bt_Send;
        private System.Windows.Forms.Button bt_StopSend;
        private System.Windows.Forms.Button bt_SetPort2;
        private System.Windows.Forms.Button bt_SetPort1;
        private System.Windows.Forms.ComboBox cb_Port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtb_CmdData;
    }
}

