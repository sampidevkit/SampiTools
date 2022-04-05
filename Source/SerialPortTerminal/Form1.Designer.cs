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
            this.cb_Port1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_Port2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_Baud = new System.Windows.Forms.ComboBox();
            this.cb_Pid = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.lkl_Homepage = new System.Windows.Forms.LinkLabel();
            this.serialDfu = new System.IO.Ports.SerialPort(this.components);
            this.tb_FilePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabForward = new System.Windows.Forms.TabPage();
            this.bt_FwdStart = new System.Windows.Forms.Button();
            this.bt_FwdStop = new System.Windows.Forms.Button();
            this.bt_FwdScan = new System.Windows.Forms.Button();
            this.tabInternalPort = new System.Windows.Forms.TabPage();
            this.ckb_Script = new System.Windows.Forms.CheckBox();
            this.nud_Interval = new System.Windows.Forms.NumericUpDown();
            this.ckb_Loop = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbx_CmdData = new System.Windows.Forms.TextBox();
            this.tbx_CmdStop = new System.Windows.Forms.TextBox();
            this.tbx_CmdStart = new System.Windows.Forms.TextBox();
            this.bt_Script = new System.Windows.Forms.Button();
            this.bt_Send = new System.Windows.Forms.Button();
            this.bt_StopSend = new System.Windows.Forms.Button();
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
            this.tabCalc = new System.Windows.Forms.TabPage();
            this.bt_Sign = new System.Windows.Forms.Button();
            this.bt_Float = new System.Windows.Forms.Button();
            this.bt_NotB = new System.Windows.Forms.Button();
            this.bt_NotA = new System.Windows.Forms.Button();
            this.bt_Xor = new System.Windows.Forms.Button();
            this.bt_Or = new System.Windows.Forms.Button();
            this.bt_And = new System.Windows.Forms.Button();
            this.bt_Mod = new System.Windows.Forms.Button();
            this.bt_Div = new System.Windows.Forms.Button();
            this.bt_Mul = new System.Windows.Forms.Button();
            this.bt_Sub = new System.Windows.Forms.Button();
            this.bt_Add = new System.Windows.Forms.Button();
            this.tb_BinB = new System.Windows.Forms.TextBox();
            this.tb_BinA = new System.Windows.Forms.TextBox();
            this.tb_HexB = new System.Windows.Forms.TextBox();
            this.tb_HexA = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tb_DecB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_DecA = new System.Windows.Forms.TextBox();
            this.tabShortcut = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.bt_DiskMngr = new System.Windows.Forms.Button();
            this.bt_ResetMPLAB = new System.Windows.Forms.Button();
            this.bt_CleanTemp = new System.Windows.Forms.Button();
            this.bt_DeviceMngr = new System.Windows.Forms.Button();
            this.bt_TaskMngr = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.pic_OpenLog = new System.Windows.Forms.PictureBox();
            this.picConnected = new System.Windows.Forms.PictureBox();
            this.picHomePage = new System.Windows.Forms.PictureBox();
            this.picDisconnected = new System.Windows.Forms.PictureBox();
            this.lb_OpenLog = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabForward.SuspendLayout();
            this.tabInternalPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Interval)).BeginInit();
            this.tabDfu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_FrameDelay)).BeginInit();
            this.tabCalc.SuspendLayout();
            this.tabShortcut.SuspendLayout();
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
            // cb_Port1
            // 
            this.cb_Port1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Port1.FormattingEnabled = true;
            this.cb_Port1.Items.AddRange(new object[] {
            "Empty"});
            this.cb_Port1.Location = new System.Drawing.Point(303, 6);
            this.cb_Port1.Name = "cb_Port1";
            this.cb_Port1.Size = new System.Drawing.Size(133, 23);
            this.cb_Port1.TabIndex = 1;
            this.cb_Port1.Text = "Empty";
            this.cb_Port1.TextChanged += new System.EventHandler(this.cb_Port1_TextChanged);
            this.cb_Port1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Port1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(255, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port 1:";
            // 
            // cb_Port2
            // 
            this.cb_Port2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Port2.FormattingEnabled = true;
            this.cb_Port2.Items.AddRange(new object[] {
            "Internal"});
            this.cb_Port2.Location = new System.Drawing.Point(303, 35);
            this.cb_Port2.Name = "cb_Port2";
            this.cb_Port2.Size = new System.Drawing.Size(133, 23);
            this.cb_Port2.TabIndex = 0;
            this.cb_Port2.Text = "Internal";
            this.cb_Port2.SelectedIndexChanged += new System.EventHandler(this.cb_Port2_SelectedIndexChanged);
            this.cb_Port2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_Port2_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Baud:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(255, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port 2:";
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
            this.cb_Baud.Location = new System.Drawing.Point(48, 36);
            this.cb_Baud.Name = "cb_Baud";
            this.cb_Baud.Size = new System.Drawing.Size(201, 23);
            this.cb_Baud.TabIndex = 4;
            this.cb_Baud.Text = "9600";
            // 
            // cb_Pid
            // 
            this.cb_Pid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Pid.FormattingEnabled = true;
            this.cb_Pid.Items.AddRange(new object[] {
            "USB CDC",
            "VCP",
            "CP2102N",
            "FT232RL",
            "xE866",
            "ME910G1",
            "SAMPI"});
            this.cb_Pid.Location = new System.Drawing.Point(48, 7);
            this.cb_Pid.Name = "cb_Pid";
            this.cb_Pid.Size = new System.Drawing.Size(201, 23);
            this.cb_Pid.TabIndex = 5;
            this.cb_Pid.Text = "USB CDC";
            this.cb_Pid.TextChanged += new System.EventHandler(this.cb_Pid_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "PID:";
            // 
            // rtb_Log
            // 
            this.rtb_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rtb_Log.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Log.Location = new System.Drawing.Point(9, 140);
            this.rtb_Log.Name = "rtb_Log";
            this.rtb_Log.ReadOnly = true;
            this.rtb_Log.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rtb_Log.Size = new System.Drawing.Size(603, 420);
            this.rtb_Log.TabIndex = 10;
            this.rtb_Log.Text = "";
            this.rtb_Log.TextChanged += new System.EventHandler(this.rtb_Log_TextChanged);
            this.rtb_Log.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rtb_Log_MouseDoubleClick);
            // 
            // lkl_Homepage
            // 
            this.lkl_Homepage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lkl_Homepage.AutoSize = true;
            this.lkl_Homepage.Location = new System.Drawing.Point(534, 573);
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
            this.tb_FilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_FilePath.Location = new System.Drawing.Point(41, 38);
            this.tb_FilePath.Name = "tb_FilePath";
            this.tb_FilePath.ReadOnly = true;
            this.tb_FilePath.Size = new System.Drawing.Size(337, 20);
            this.tb_FilePath.TabIndex = 16;
            this.tb_FilePath.TextChanged += new System.EventHandler(this.tb_FilePath_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Hex files (*.hex)|*.hex";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabForward);
            this.tabControl1.Controls.Add(this.tabInternalPort);
            this.tabControl1.Controls.Add(this.tabDfu);
            this.tabControl1.Controls.Add(this.tabCalc);
            this.tabControl1.Controls.Add(this.tabShortcut);
            this.tabControl1.Location = new System.Drawing.Point(9, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(603, 126);
            this.tabControl1.TabIndex = 18;
            // 
            // tabForward
            // 
            this.tabForward.Controls.Add(this.bt_FwdStart);
            this.tabForward.Controls.Add(this.bt_FwdStop);
            this.tabForward.Controls.Add(this.bt_FwdScan);
            this.tabForward.Controls.Add(this.label3);
            this.tabForward.Controls.Add(this.cb_Baud);
            this.tabForward.Controls.Add(this.label2);
            this.tabForward.Controls.Add(this.cb_Port2);
            this.tabForward.Controls.Add(this.cb_Port1);
            this.tabForward.Controls.Add(this.label4);
            this.tabForward.Controls.Add(this.label1);
            this.tabForward.Controls.Add(this.cb_Pid);
            this.tabForward.Location = new System.Drawing.Point(4, 22);
            this.tabForward.Name = "tabForward";
            this.tabForward.Padding = new System.Windows.Forms.Padding(3);
            this.tabForward.Size = new System.Drawing.Size(595, 100);
            this.tabForward.TabIndex = 0;
            this.tabForward.Text = "Forward";
            this.tabForward.UseVisualStyleBackColor = true;
            // 
            // bt_FwdStart
            // 
            this.bt_FwdStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_FwdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_FwdStart.ForeColor = System.Drawing.Color.Red;
            this.bt_FwdStart.ImageIndex = 0;
            this.bt_FwdStart.ImageList = this.imageList1;
            this.bt_FwdStart.Location = new System.Drawing.Point(537, 7);
            this.bt_FwdStart.Name = "bt_FwdStart";
            this.bt_FwdStart.Size = new System.Drawing.Size(52, 52);
            this.bt_FwdStart.TabIndex = 5;
            this.bt_FwdStart.UseVisualStyleBackColor = true;
            this.bt_FwdStart.Click += new System.EventHandler(this.bt_FwdStart_Click);
            // 
            // bt_FwdStop
            // 
            this.bt_FwdStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_FwdStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_FwdStop.ForeColor = System.Drawing.Color.Red;
            this.bt_FwdStop.ImageIndex = 1;
            this.bt_FwdStop.ImageList = this.imageList1;
            this.bt_FwdStop.Location = new System.Drawing.Point(537, 7);
            this.bt_FwdStop.Name = "bt_FwdStop";
            this.bt_FwdStop.Size = new System.Drawing.Size(52, 52);
            this.bt_FwdStop.TabIndex = 12;
            this.bt_FwdStop.UseVisualStyleBackColor = true;
            this.bt_FwdStop.Visible = false;
            this.bt_FwdStop.Click += new System.EventHandler(this.bt_FwdStop_Click);
            // 
            // bt_FwdScan
            // 
            this.bt_FwdScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_FwdScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_FwdScan.ForeColor = System.Drawing.Color.Red;
            this.bt_FwdScan.ImageIndex = 3;
            this.bt_FwdScan.ImageList = this.imageList1;
            this.bt_FwdScan.Location = new System.Drawing.Point(479, 6);
            this.bt_FwdScan.Name = "bt_FwdScan";
            this.bt_FwdScan.Size = new System.Drawing.Size(52, 52);
            this.bt_FwdScan.TabIndex = 13;
            this.bt_FwdScan.UseVisualStyleBackColor = true;
            this.bt_FwdScan.Click += new System.EventHandler(this.bt_FwdScan_Click);
            // 
            // tabInternalPort
            // 
            this.tabInternalPort.BackColor = System.Drawing.SystemColors.Window;
            this.tabInternalPort.Controls.Add(this.ckb_Script);
            this.tabInternalPort.Controls.Add(this.nud_Interval);
            this.tabInternalPort.Controls.Add(this.ckb_Loop);
            this.tabInternalPort.Controls.Add(this.label11);
            this.tabInternalPort.Controls.Add(this.label10);
            this.tabInternalPort.Controls.Add(this.label9);
            this.tabInternalPort.Controls.Add(this.tbx_CmdData);
            this.tabInternalPort.Controls.Add(this.tbx_CmdStop);
            this.tabInternalPort.Controls.Add(this.tbx_CmdStart);
            this.tabInternalPort.Controls.Add(this.bt_Script);
            this.tabInternalPort.Controls.Add(this.bt_Send);
            this.tabInternalPort.Controls.Add(this.bt_StopSend);
            this.tabInternalPort.Location = new System.Drawing.Point(4, 22);
            this.tabInternalPort.Name = "tabInternalPort";
            this.tabInternalPort.Padding = new System.Windows.Forms.Padding(3);
            this.tabInternalPort.Size = new System.Drawing.Size(595, 100);
            this.tabInternalPort.TabIndex = 2;
            this.tabInternalPort.Text = "Internal Port";
            // 
            // ckb_Script
            // 
            this.ckb_Script.AutoSize = true;
            this.ckb_Script.Location = new System.Drawing.Point(383, 8);
            this.ckb_Script.Name = "ckb_Script";
            this.ckb_Script.Size = new System.Drawing.Size(53, 17);
            this.ckb_Script.TabIndex = 24;
            this.ckb_Script.Text = "Script";
            this.ckb_Script.UseVisualStyleBackColor = true;
            this.ckb_Script.CheckedChanged += new System.EventHandler(this.ckb_Script_CheckedChanged);
            // 
            // nud_Interval
            // 
            this.nud_Interval.Location = new System.Drawing.Point(280, 5);
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
            this.nud_Interval.Size = new System.Drawing.Size(97, 20);
            this.nud_Interval.TabIndex = 24;
            this.nud_Interval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_Interval.ValueChanged += new System.EventHandler(this.nud_Interval_ValueChanged);
            // 
            // ckb_Loop
            // 
            this.ckb_Loop.AutoSize = true;
            this.ckb_Loop.Location = new System.Drawing.Point(224, 7);
            this.ckb_Loop.Name = "ckb_Loop";
            this.ckb_Loop.Size = new System.Drawing.Size(50, 17);
            this.ckb_Loop.TabIndex = 23;
            this.ckb_Loop.Text = "Loop";
            this.ckb_Loop.UseVisualStyleBackColor = true;
            this.ckb_Loop.CheckedChanged += new System.EventHandler(this.ckb_Loop_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(115, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Stop";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Data";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Start";
            // 
            // tbx_CmdData
            // 
            this.tbx_CmdData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_CmdData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbx_CmdData.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_CmdData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbx_CmdData.Location = new System.Drawing.Point(41, 34);
            this.tbx_CmdData.MaxLength = 256;
            this.tbx_CmdData.Name = "tbx_CmdData";
            this.tbx_CmdData.Size = new System.Drawing.Size(395, 25);
            this.tbx_CmdData.TabIndex = 0;
            this.tbx_CmdData.Text = "AT";
            // 
            // tbx_CmdStop
            // 
            this.tbx_CmdStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbx_CmdStop.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_CmdStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbx_CmdStop.Location = new System.Drawing.Point(150, 3);
            this.tbx_CmdStop.MaxLength = 16;
            this.tbx_CmdStop.Name = "tbx_CmdStop";
            this.tbx_CmdStop.Size = new System.Drawing.Size(68, 25);
            this.tbx_CmdStop.TabIndex = 0;
            this.tbx_CmdStop.Text = "\\r";
            // 
            // tbx_CmdStart
            // 
            this.tbx_CmdStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbx_CmdStart.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_CmdStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbx_CmdStart.Location = new System.Drawing.Point(41, 3);
            this.tbx_CmdStart.MaxLength = 16;
            this.tbx_CmdStart.Name = "tbx_CmdStart";
            this.tbx_CmdStart.Size = new System.Drawing.Size(68, 25);
            this.tbx_CmdStart.TabIndex = 0;
            // 
            // bt_Script
            // 
            this.bt_Script.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Script.Enabled = false;
            this.bt_Script.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Script.ForeColor = System.Drawing.Color.Red;
            this.bt_Script.ImageIndex = 8;
            this.bt_Script.ImageList = this.imageList1;
            this.bt_Script.Location = new System.Drawing.Point(442, 7);
            this.bt_Script.Name = "bt_Script";
            this.bt_Script.Size = new System.Drawing.Size(52, 52);
            this.bt_Script.TabIndex = 26;
            this.bt_Script.UseVisualStyleBackColor = true;
            this.bt_Script.Click += new System.EventHandler(this.bt_Script_Click);
            // 
            // bt_Send
            // 
            this.bt_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Send.Enabled = false;
            this.bt_Send.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Send.ForeColor = System.Drawing.Color.Red;
            this.bt_Send.ImageIndex = 5;
            this.bt_Send.ImageList = this.imageList1;
            this.bt_Send.Location = new System.Drawing.Point(500, 7);
            this.bt_Send.Name = "bt_Send";
            this.bt_Send.Size = new System.Drawing.Size(52, 52);
            this.bt_Send.TabIndex = 6;
            this.bt_Send.UseVisualStyleBackColor = true;
            this.bt_Send.Click += new System.EventHandler(this.bt_Send_Click);
            // 
            // bt_StopSend
            // 
            this.bt_StopSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_StopSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_StopSend.ForeColor = System.Drawing.Color.Red;
            this.bt_StopSend.ImageIndex = 1;
            this.bt_StopSend.ImageList = this.imageList1;
            this.bt_StopSend.Location = new System.Drawing.Point(500, 7);
            this.bt_StopSend.Name = "bt_StopSend";
            this.bt_StopSend.Size = new System.Drawing.Size(52, 52);
            this.bt_StopSend.TabIndex = 25;
            this.bt_StopSend.UseVisualStyleBackColor = true;
            this.bt_StopSend.Visible = false;
            this.bt_StopSend.Click += new System.EventHandler(this.bt_StopSend_Click);
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
            this.tabDfu.Size = new System.Drawing.Size(595, 100);
            this.tabDfu.TabIndex = 1;
            this.tabDfu.Text = "DFU";
            // 
            // nud_FrameDelay
            // 
            this.nud_FrameDelay.Location = new System.Drawing.Point(297, 9);
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
            this.label8.Location = new System.Drawing.Point(250, 9);
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
            this.cb_DfuBaud.Location = new System.Drawing.Point(173, 6);
            this.cb_DfuBaud.Name = "cb_DfuBaud";
            this.cb_DfuBaud.Size = new System.Drawing.Size(71, 23);
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
            this.label5.Location = new System.Drawing.Point(5, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "File:";
            // 
            // bt_Browse
            // 
            this.bt_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Browse.ImageIndex = 7;
            this.bt_Browse.ImageList = this.imageList1;
            this.bt_Browse.Location = new System.Drawing.Point(384, 6);
            this.bt_Browse.Name = "bt_Browse";
            this.bt_Browse.Size = new System.Drawing.Size(52, 52);
            this.bt_Browse.TabIndex = 17;
            this.bt_Browse.UseVisualStyleBackColor = true;
            this.bt_Browse.Click += new System.EventHandler(this.bt_Browse_Click);
            // 
            // bt_DfuScan
            // 
            this.bt_DfuScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_DfuScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DfuScan.ForeColor = System.Drawing.Color.Red;
            this.bt_DfuScan.ImageIndex = 3;
            this.bt_DfuScan.ImageList = this.imageList1;
            this.bt_DfuScan.Location = new System.Drawing.Point(442, 6);
            this.bt_DfuScan.Name = "bt_DfuScan";
            this.bt_DfuScan.Size = new System.Drawing.Size(52, 52);
            this.bt_DfuScan.TabIndex = 16;
            this.bt_DfuScan.UseVisualStyleBackColor = true;
            this.bt_DfuScan.Click += new System.EventHandler(this.bt_DfuScan_Click);
            // 
            // bt_DfuStart
            // 
            this.bt_DfuStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_DfuStart.Enabled = false;
            this.bt_DfuStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DfuStart.ForeColor = System.Drawing.Color.Red;
            this.bt_DfuStart.ImageIndex = 4;
            this.bt_DfuStart.ImageList = this.imageList1;
            this.bt_DfuStart.Location = new System.Drawing.Point(500, 6);
            this.bt_DfuStart.Name = "bt_DfuStart";
            this.bt_DfuStart.Size = new System.Drawing.Size(52, 52);
            this.bt_DfuStart.TabIndex = 14;
            this.bt_DfuStart.UseVisualStyleBackColor = true;
            this.bt_DfuStart.Click += new System.EventHandler(this.bt_DfuStart_Click);
            // 
            // bt_DfuStop
            // 
            this.bt_DfuStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_DfuStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_DfuStop.ForeColor = System.Drawing.Color.Red;
            this.bt_DfuStop.ImageIndex = 1;
            this.bt_DfuStop.ImageList = this.imageList1;
            this.bt_DfuStop.Location = new System.Drawing.Point(500, 6);
            this.bt_DfuStop.Name = "bt_DfuStop";
            this.bt_DfuStop.Size = new System.Drawing.Size(52, 52);
            this.bt_DfuStop.TabIndex = 15;
            this.bt_DfuStop.UseVisualStyleBackColor = true;
            this.bt_DfuStop.Visible = false;
            this.bt_DfuStop.Click += new System.EventHandler(this.bt_DfuStop_Click);
            // 
            // tabCalc
            // 
            this.tabCalc.Controls.Add(this.bt_Sign);
            this.tabCalc.Controls.Add(this.bt_Float);
            this.tabCalc.Controls.Add(this.bt_NotB);
            this.tabCalc.Controls.Add(this.bt_NotA);
            this.tabCalc.Controls.Add(this.bt_Xor);
            this.tabCalc.Controls.Add(this.bt_Or);
            this.tabCalc.Controls.Add(this.bt_And);
            this.tabCalc.Controls.Add(this.bt_Mod);
            this.tabCalc.Controls.Add(this.bt_Div);
            this.tabCalc.Controls.Add(this.bt_Mul);
            this.tabCalc.Controls.Add(this.bt_Sub);
            this.tabCalc.Controls.Add(this.bt_Add);
            this.tabCalc.Controls.Add(this.tb_BinB);
            this.tabCalc.Controls.Add(this.tb_BinA);
            this.tabCalc.Controls.Add(this.tb_HexB);
            this.tabCalc.Controls.Add(this.tb_HexA);
            this.tabCalc.Controls.Add(this.label16);
            this.tabCalc.Controls.Add(this.tb_DecB);
            this.tabCalc.Controls.Add(this.label15);
            this.tabCalc.Controls.Add(this.label14);
            this.tabCalc.Controls.Add(this.label13);
            this.tabCalc.Controls.Add(this.label12);
            this.tabCalc.Controls.Add(this.tb_DecA);
            this.tabCalc.Location = new System.Drawing.Point(4, 22);
            this.tabCalc.Name = "tabCalc";
            this.tabCalc.Padding = new System.Windows.Forms.Padding(3);
            this.tabCalc.Size = new System.Drawing.Size(595, 100);
            this.tabCalc.TabIndex = 3;
            this.tabCalc.Text = "Calculator";
            this.tabCalc.UseVisualStyleBackColor = true;
            // 
            // bt_Sign
            // 
            this.bt_Sign.Location = new System.Drawing.Point(544, 69);
            this.bt_Sign.Name = "bt_Sign";
            this.bt_Sign.Size = new System.Drawing.Size(45, 23);
            this.bt_Sign.TabIndex = 18;
            this.bt_Sign.Text = "SIGN";
            this.bt_Sign.UseVisualStyleBackColor = true;
            this.bt_Sign.Click += new System.EventHandler(this.bt_Sign_Click);
            // 
            // bt_Float
            // 
            this.bt_Float.Location = new System.Drawing.Point(493, 69);
            this.bt_Float.Name = "bt_Float";
            this.bt_Float.Size = new System.Drawing.Size(45, 23);
            this.bt_Float.TabIndex = 19;
            this.bt_Float.Text = "FLT";
            this.bt_Float.UseVisualStyleBackColor = true;
            this.bt_Float.Click += new System.EventHandler(this.bt_Float_Click);
            // 
            // bt_NotB
            // 
            this.bt_NotB.Location = new System.Drawing.Point(442, 69);
            this.bt_NotB.Name = "bt_NotB";
            this.bt_NotB.Size = new System.Drawing.Size(45, 23);
            this.bt_NotB.TabIndex = 20;
            this.bt_NotB.Text = "~B";
            this.bt_NotB.UseVisualStyleBackColor = true;
            this.bt_NotB.Click += new System.EventHandler(this.bt_NotB_Click);
            // 
            // bt_NotA
            // 
            this.bt_NotA.Location = new System.Drawing.Point(391, 69);
            this.bt_NotA.Name = "bt_NotA";
            this.bt_NotA.Size = new System.Drawing.Size(45, 23);
            this.bt_NotA.TabIndex = 17;
            this.bt_NotA.Text = "~A";
            this.bt_NotA.UseVisualStyleBackColor = true;
            this.bt_NotA.Click += new System.EventHandler(this.bt_NotA_Click);
            // 
            // bt_Xor
            // 
            this.bt_Xor.Location = new System.Drawing.Point(544, 43);
            this.bt_Xor.Name = "bt_Xor";
            this.bt_Xor.Size = new System.Drawing.Size(45, 23);
            this.bt_Xor.TabIndex = 14;
            this.bt_Xor.Text = "A^B";
            this.bt_Xor.UseVisualStyleBackColor = true;
            this.bt_Xor.Click += new System.EventHandler(this.bt_Xor_Click);
            // 
            // bt_Or
            // 
            this.bt_Or.Location = new System.Drawing.Point(493, 43);
            this.bt_Or.Name = "bt_Or";
            this.bt_Or.Size = new System.Drawing.Size(45, 23);
            this.bt_Or.TabIndex = 15;
            this.bt_Or.Text = "A|B";
            this.bt_Or.UseVisualStyleBackColor = true;
            this.bt_Or.Click += new System.EventHandler(this.bt_Or_Click);
            // 
            // bt_And
            // 
            this.bt_And.Location = new System.Drawing.Point(442, 43);
            this.bt_And.Name = "bt_And";
            this.bt_And.Size = new System.Drawing.Size(45, 23);
            this.bt_And.TabIndex = 16;
            this.bt_And.Text = "A&&B";
            this.bt_And.UseVisualStyleBackColor = true;
            this.bt_And.Click += new System.EventHandler(this.bt_And_Click);
            // 
            // bt_Mod
            // 
            this.bt_Mod.Location = new System.Drawing.Point(391, 43);
            this.bt_Mod.Name = "bt_Mod";
            this.bt_Mod.Size = new System.Drawing.Size(45, 23);
            this.bt_Mod.TabIndex = 13;
            this.bt_Mod.Text = "A%B";
            this.bt_Mod.UseVisualStyleBackColor = true;
            this.bt_Mod.Click += new System.EventHandler(this.bt_Mod_Click);
            // 
            // bt_Div
            // 
            this.bt_Div.Location = new System.Drawing.Point(544, 17);
            this.bt_Div.Name = "bt_Div";
            this.bt_Div.Size = new System.Drawing.Size(45, 23);
            this.bt_Div.TabIndex = 12;
            this.bt_Div.Text = "A/B";
            this.bt_Div.UseVisualStyleBackColor = true;
            this.bt_Div.Click += new System.EventHandler(this.bt_Div_Click);
            // 
            // bt_Mul
            // 
            this.bt_Mul.Location = new System.Drawing.Point(493, 17);
            this.bt_Mul.Name = "bt_Mul";
            this.bt_Mul.Size = new System.Drawing.Size(45, 23);
            this.bt_Mul.TabIndex = 12;
            this.bt_Mul.Text = "A*B";
            this.bt_Mul.UseVisualStyleBackColor = true;
            this.bt_Mul.Click += new System.EventHandler(this.bt_Mul_Click);
            // 
            // bt_Sub
            // 
            this.bt_Sub.Location = new System.Drawing.Point(442, 17);
            this.bt_Sub.Name = "bt_Sub";
            this.bt_Sub.Size = new System.Drawing.Size(45, 23);
            this.bt_Sub.TabIndex = 12;
            this.bt_Sub.Text = "A-B";
            this.bt_Sub.UseVisualStyleBackColor = true;
            this.bt_Sub.Click += new System.EventHandler(this.bt_Sub_Click);
            // 
            // bt_Add
            // 
            this.bt_Add.Location = new System.Drawing.Point(391, 17);
            this.bt_Add.Name = "bt_Add";
            this.bt_Add.Size = new System.Drawing.Size(45, 23);
            this.bt_Add.TabIndex = 11;
            this.bt_Add.Text = "A+B";
            this.bt_Add.UseVisualStyleBackColor = true;
            this.bt_Add.Click += new System.EventHandler(this.bt_Add_Click);
            // 
            // tb_BinB
            // 
            this.tb_BinB.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_BinB.ForeColor = System.Drawing.Color.OrangeRed;
            this.tb_BinB.Location = new System.Drawing.Point(215, 71);
            this.tb_BinB.Name = "tb_BinB";
            this.tb_BinB.Size = new System.Drawing.Size(167, 22);
            this.tb_BinB.TabIndex = 10;
            this.tb_BinB.Text = "0";
            this.tb_BinB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_BinB.TextChanged += new System.EventHandler(this.tb_BinB_TextChanged);
            // 
            // tb_BinA
            // 
            this.tb_BinA.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_BinA.ForeColor = System.Drawing.Color.DarkBlue;
            this.tb_BinA.Location = new System.Drawing.Point(42, 71);
            this.tb_BinA.Name = "tb_BinA";
            this.tb_BinA.Size = new System.Drawing.Size(167, 22);
            this.tb_BinA.TabIndex = 9;
            this.tb_BinA.Text = "0";
            this.tb_BinA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_BinA.TextChanged += new System.EventHandler(this.tb_BinA_TextChanged);
            // 
            // tb_HexB
            // 
            this.tb_HexB.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_HexB.ForeColor = System.Drawing.Color.OrangeRed;
            this.tb_HexB.Location = new System.Drawing.Point(215, 45);
            this.tb_HexB.Name = "tb_HexB";
            this.tb_HexB.Size = new System.Drawing.Size(167, 22);
            this.tb_HexB.TabIndex = 8;
            this.tb_HexB.Text = "0";
            this.tb_HexB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_HexB.TextChanged += new System.EventHandler(this.tb_HexB_TextChanged);
            // 
            // tb_HexA
            // 
            this.tb_HexA.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_HexA.ForeColor = System.Drawing.Color.DarkBlue;
            this.tb_HexA.Location = new System.Drawing.Point(42, 45);
            this.tb_HexA.Name = "tb_HexA";
            this.tb_HexA.Size = new System.Drawing.Size(167, 22);
            this.tb_HexA.TabIndex = 7;
            this.tb_HexA.Text = "0";
            this.tb_HexA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_HexA.TextChanged += new System.EventHandler(this.tb_HexA_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(291, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "B";
            // 
            // tb_DecB
            // 
            this.tb_DecB.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_DecB.ForeColor = System.Drawing.Color.OrangeRed;
            this.tb_DecB.Location = new System.Drawing.Point(215, 19);
            this.tb_DecB.Name = "tb_DecB";
            this.tb_DecB.Size = new System.Drawing.Size(167, 22);
            this.tb_DecB.TabIndex = 5;
            this.tb_DecB.Text = "0";
            this.tb_DecB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_DecB.TextChanged += new System.EventHandler(this.tb_DecB_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(118, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "A";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Bin:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Hex:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Dec:";
            // 
            // tb_DecA
            // 
            this.tb_DecA.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_DecA.ForeColor = System.Drawing.Color.DarkBlue;
            this.tb_DecA.Location = new System.Drawing.Point(42, 19);
            this.tb_DecA.Name = "tb_DecA";
            this.tb_DecA.Size = new System.Drawing.Size(167, 22);
            this.tb_DecA.TabIndex = 0;
            this.tb_DecA.Text = "0";
            this.tb_DecA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_DecA.TextChanged += new System.EventHandler(this.tb_DecA_TextChanged);
            // 
            // tabShortcut
            // 
            this.tabShortcut.Controls.Add(this.label21);
            this.tabShortcut.Controls.Add(this.label20);
            this.tabShortcut.Controls.Add(this.label19);
            this.tabShortcut.Controls.Add(this.label18);
            this.tabShortcut.Controls.Add(this.label17);
            this.tabShortcut.Controls.Add(this.bt_DiskMngr);
            this.tabShortcut.Controls.Add(this.bt_ResetMPLAB);
            this.tabShortcut.Controls.Add(this.bt_CleanTemp);
            this.tabShortcut.Controls.Add(this.bt_DeviceMngr);
            this.tabShortcut.Controls.Add(this.bt_TaskMngr);
            this.tabShortcut.Location = new System.Drawing.Point(4, 22);
            this.tabShortcut.Name = "tabShortcut";
            this.tabShortcut.Padding = new System.Windows.Forms.Padding(3);
            this.tabShortcut.Size = new System.Drawing.Size(595, 100);
            this.tabShortcut.TabIndex = 4;
            this.tabShortcut.Text = "Shortcut";
            this.tabShortcut.UseVisualStyleBackColor = true;
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
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(241, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 26);
            this.label20.TabIndex = 22;
            this.label20.Text = "Reset\r\nMPLAB";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(176, 61);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 26);
            this.label19.TabIndex = 21;
            this.label19.Text = "Clean\r\nTemporary";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // bt_ResetMPLAB
            // 
            this.bt_ResetMPLAB.Enabled = false;
            this.bt_ResetMPLAB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_ResetMPLAB.ForeColor = System.Drawing.Color.Red;
            this.bt_ResetMPLAB.ImageIndex = 14;
            this.bt_ResetMPLAB.ImageList = this.imageList1;
            this.bt_ResetMPLAB.Location = new System.Drawing.Point(236, 6);
            this.bt_ResetMPLAB.Name = "bt_ResetMPLAB";
            this.bt_ResetMPLAB.Size = new System.Drawing.Size(52, 52);
            this.bt_ResetMPLAB.TabIndex = 24;
            this.bt_ResetMPLAB.UseVisualStyleBackColor = true;
            this.bt_ResetMPLAB.Click += new System.EventHandler(this.bt_ResetMPLAB_Click);
            // 
            // bt_CleanTemp
            // 
            this.bt_CleanTemp.Enabled = false;
            this.bt_CleanTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_CleanTemp.ForeColor = System.Drawing.Color.Red;
            this.bt_CleanTemp.ImageIndex = 12;
            this.bt_CleanTemp.ImageList = this.imageList1;
            this.bt_CleanTemp.Location = new System.Drawing.Point(178, 6);
            this.bt_CleanTemp.Name = "bt_CleanTemp";
            this.bt_CleanTemp.Size = new System.Drawing.Size(52, 52);
            this.bt_CleanTemp.TabIndex = 23;
            this.bt_CleanTemp.UseVisualStyleBackColor = true;
            this.bt_CleanTemp.Click += new System.EventHandler(this.bt_CleanTemp_Click);
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
            this.pic_OpenLog.Location = new System.Drawing.Point(404, 566);
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
            this.picHomePage.Location = new System.Drawing.Point(499, 566);
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
            this.lb_OpenLog.Location = new System.Drawing.Point(439, 573);
            this.lb_OpenLog.Name = "lb_OpenLog";
            this.lb_OpenLog.Size = new System.Drawing.Size(54, 13);
            this.lb_OpenLog.TabIndex = 23;
            this.lb_OpenLog.Text = "Open Log";
            this.lb_OpenLog.Click += new System.EventHandler(this.lb_OpenLog_Click);
            // 
            // FormSPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(624, 601);
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
            this.Name = "FormSPT";
            this.Text = "SPT-Serial Port Terminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabForward.ResumeLayout(false);
            this.tabForward.PerformLayout();
            this.tabInternalPort.ResumeLayout(false);
            this.tabInternalPort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Interval)).EndInit();
            this.tabDfu.ResumeLayout(false);
            this.tabDfu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_FrameDelay)).EndInit();
            this.tabCalc.ResumeLayout(false);
            this.tabCalc.PerformLayout();
            this.tabShortcut.ResumeLayout(false);
            this.tabShortcut.PerformLayout();
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
        private System.Windows.Forms.ComboBox cb_Port1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_Port2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_Baud;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_Pid;
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
        private System.Windows.Forms.TabPage tabInternalPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbx_CmdData;
        private System.Windows.Forms.TextBox tbx_CmdStop;
        private System.Windows.Forms.TextBox tbx_CmdStart;
        private System.Windows.Forms.Button bt_Send;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox picDisconnected;
        private System.Windows.Forms.PictureBox picHomePage;
        private System.Windows.Forms.PictureBox picConnected;
        private System.Windows.Forms.CheckBox ckb_Loop;
        private System.Windows.Forms.NumericUpDown nud_Interval;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button bt_StopSend;
        private System.Windows.Forms.Button bt_Script;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.CheckBox ckb_Script;
        private System.Windows.Forms.TabPage tabCalc;
        private System.Windows.Forms.TextBox tb_DecA;
        private System.Windows.Forms.TextBox tb_BinB;
        private System.Windows.Forms.TextBox tb_BinA;
        private System.Windows.Forms.TextBox tb_HexB;
        private System.Windows.Forms.TextBox tb_HexA;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tb_DecB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button bt_Sign;
        private System.Windows.Forms.Button bt_Float;
        private System.Windows.Forms.Button bt_NotB;
        private System.Windows.Forms.Button bt_NotA;
        private System.Windows.Forms.Button bt_Xor;
        private System.Windows.Forms.Button bt_Or;
        private System.Windows.Forms.Button bt_And;
        private System.Windows.Forms.Button bt_Mod;
        private System.Windows.Forms.Button bt_Div;
        private System.Windows.Forms.Button bt_Mul;
        private System.Windows.Forms.Button bt_Sub;
        private System.Windows.Forms.Button bt_Add;
        private System.Windows.Forms.TabPage tabShortcut;
        private System.Windows.Forms.Button bt_TaskMngr;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button bt_DeviceMngr;
        private System.Windows.Forms.Button bt_ResetMPLAB;
        private System.Windows.Forms.Button bt_CleanTemp;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button bt_DiskMngr;
        private System.Windows.Forms.PictureBox pic_OpenLog;
        private System.Windows.Forms.Label lb_OpenLog;
    }
}

