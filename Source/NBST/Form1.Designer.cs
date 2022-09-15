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
            this.tabCtrl1 = new System.Windows.Forms.TabControl();
            this.tabGraph = new System.Windows.Forms.TabPage();
            this.zedGraph1 = new ZedGraph.ZedGraphControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.rtb_Info = new System.Windows.Forms.RichTextBox();
            this.bt_Scan = new System.Windows.Forms.Button();
            this.bt_RFTest = new System.Windows.Forms.Button();
            this.cb_Port1 = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.bt_Download = new System.Windows.Forms.Button();
            this.tb_Url = new System.Windows.Forms.TextBox();
            this.tb_Md5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lb_Percent = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_ViewMode = new System.Windows.Forms.ComboBox();
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
            this.tabGraph.Controls.Add(this.zedGraph1);
            this.tabGraph.Location = new System.Drawing.Point(4, 22);
            this.tabGraph.Name = "tabGraph";
            this.tabGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabGraph.Size = new System.Drawing.Size(700, 378);
            this.tabGraph.TabIndex = 0;
            this.tabGraph.Text = "Graph";
            // 
            // zedGraph1
            // 
            this.zedGraph1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraph1.BackColor = System.Drawing.SystemColors.Info;
            this.zedGraph1.Location = new System.Drawing.Point(6, 6);
            this.zedGraph1.Name = "zedGraph1";
            this.zedGraph1.ScrollGrace = 0D;
            this.zedGraph1.ScrollMaxX = 0D;
            this.zedGraph1.ScrollMaxY = 0D;
            this.zedGraph1.ScrollMaxY2 = 0D;
            this.zedGraph1.ScrollMinX = 0D;
            this.zedGraph1.ScrollMinY = 0D;
            this.zedGraph1.ScrollMinY2 = 0D;
            this.zedGraph1.Size = new System.Drawing.Size(688, 366);
            this.zedGraph1.TabIndex = 3;
            this.zedGraph1.UseExtendedPrintDialog = true;
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
            this.rtb_Info.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Info.ForeColor = System.Drawing.Color.Blue;
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
            this.cb_Port1.Text = "No COM Port";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.ReadBufferSize = 10240;
            this.serialPort1.WriteBufferSize = 10240;
            // 
            // bt_Download
            // 
            this.bt_Download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Download.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Download.Location = new System.Drawing.Point(726, 444);
            this.bt_Download.Name = "bt_Download";
            this.bt_Download.Size = new System.Drawing.Size(270, 23);
            this.bt_Download.TabIndex = 5;
            this.bt_Download.Text = "Download";
            this.bt_Download.UseVisualStyleBackColor = true;
            this.bt_Download.Click += new System.EventHandler(this.bt_Download_Click);
            // 
            // tb_Url
            // 
            this.tb_Url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Url.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Url.ForeColor = System.Drawing.Color.OrangeRed;
            this.tb_Url.Location = new System.Drawing.Point(85, 418);
            this.tb_Url.Name = "tb_Url";
            this.tb_Url.Size = new System.Drawing.Size(625, 20);
            this.tb_Url.TabIndex = 7;
            this.tb_Url.Text = "https://raw.githubusercontent.com/MicrochipTech/XPRESS-Loader/master/utilities/Xp" +
    "ressBL.hex";
            // 
            // tb_Md5
            // 
            this.tb_Md5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Md5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Md5.ForeColor = System.Drawing.Color.OrangeRed;
            this.tb_Md5.Location = new System.Drawing.Point(762, 418);
            this.tb_Md5.Name = "tb_Md5";
            this.tb_Md5.Size = new System.Drawing.Size(234, 20);
            this.tb_Md5.TabIndex = 7;
            this.tb_Md5.Text = "7E0925717541AF545BA1EF8ABEEA4B69";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 422);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "URL:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(723, 422);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "MD5:";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(85, 444);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(625, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // lb_Percent
            // 
            this.lb_Percent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_Percent.AutoSize = true;
            this.lb_Percent.BackColor = System.Drawing.Color.Transparent;
            this.lb_Percent.ForeColor = System.Drawing.Color.OrangeRed;
            this.lb_Percent.Location = new System.Drawing.Point(90, 449);
            this.lb_Percent.Name = "lb_Percent";
            this.lb_Percent.Size = new System.Drawing.Size(36, 13);
            this.lb_Percent.TabIndex = 11;
            this.lb_Percent.Text = "0 byte";
            this.lb_Percent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 449);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Downloaded:";
            // 
            // cb_ViewMode
            // 
            this.cb_ViewMode.FormattingEnabled = true;
            this.cb_ViewMode.Items.AddRange(new object[] {
            "Compact",
            "Scroll"});
            this.cb_ViewMode.Location = new System.Drawing.Point(12, 345);
            this.cb_ViewMode.Name = "cb_ViewMode";
            this.cb_ViewMode.Size = new System.Drawing.Size(81, 21);
            this.cb_ViewMode.TabIndex = 4;
            this.cb_ViewMode.Text = "Compact";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1008, 473);
            this.Controls.Add(this.lb_Percent);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Md5);
            this.Controls.Add(this.tb_Url);
            this.Controls.Add(this.rtb_Info);
            this.Controls.Add(this.bt_Scan);
            this.Controls.Add(this.tabCtrl1);
            this.Controls.Add(this.bt_Download);
            this.Controls.Add(this.bt_RFTest);
            this.Controls.Add(this.cb_Port1);
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
        private ZedGraph.ZedGraphControl zedGraph1;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox rtb_Log;
        private System.Windows.Forms.RichTextBox rtb_Info;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button bt_Scan;
        private System.Windows.Forms.Button bt_Download;
        private System.Windows.Forms.TextBox tb_Url;
        private System.Windows.Forms.TextBox tb_Md5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lb_Percent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_ViewMode;
    }
}

