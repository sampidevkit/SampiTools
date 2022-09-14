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
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.rtb_Info = new System.Windows.Forms.RichTextBox();
            this.bt_Scan = new System.Windows.Forms.Button();
            this.bt_RFTest = new System.Windows.Forms.Button();
            this.cb_Port1 = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.bt_Download = new System.Windows.Forms.Button();
            this.tabCtrl1.SuspendLayout();
            this.tabGraph.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrl1
            // 
            this.tabCtrl1.Controls.Add(this.tabGraph);
            this.tabCtrl1.Controls.Add(this.tabLog);
            this.tabCtrl1.Location = new System.Drawing.Point(12, 12);
            this.tabCtrl1.Name = "tabCtrl1";
            this.tabCtrl1.SelectedIndex = 0;
            this.tabCtrl1.Size = new System.Drawing.Size(800, 449);
            this.tabCtrl1.TabIndex = 3;
            // 
            // tabGraph
            // 
            this.tabGraph.BackColor = System.Drawing.Color.Transparent;
            this.tabGraph.Controls.Add(this.zedGraph);
            this.tabGraph.Location = new System.Drawing.Point(4, 22);
            this.tabGraph.Name = "tabGraph";
            this.tabGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabGraph.Size = new System.Drawing.Size(792, 423);
            this.tabGraph.TabIndex = 0;
            this.tabGraph.Text = "Graph";
            // 
            // zedGraph
            // 
            this.zedGraph.BackColor = System.Drawing.SystemColors.Info;
            this.zedGraph.Location = new System.Drawing.Point(6, 6);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(780, 411);
            this.zedGraph.TabIndex = 3;
            this.zedGraph.UseExtendedPrintDialog = true;
            // 
            // tabLog
            // 
            this.tabLog.BackColor = System.Drawing.Color.Transparent;
            this.tabLog.Controls.Add(this.rtb_Log);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(792, 423);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            // 
            // rtb_Log
            // 
            this.rtb_Log.BackColor = System.Drawing.SystemColors.Info;
            this.rtb_Log.Location = new System.Drawing.Point(6, 6);
            this.rtb_Log.Name = "rtb_Log";
            this.rtb_Log.Size = new System.Drawing.Size(964, 411);
            this.rtb_Log.TabIndex = 0;
            this.rtb_Log.Text = "";
            this.rtb_Log.TextChanged += new System.EventHandler(this.rtb_Log_TextChanged);
            this.rtb_Log.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rtb_Log_MouseDoubleClick);
            // 
            // rtb_Info
            // 
            this.rtb_Info.Location = new System.Drawing.Point(818, 71);
            this.rtb_Info.Name = "rtb_Info";
            this.rtb_Info.Size = new System.Drawing.Size(178, 380);
            this.rtb_Info.TabIndex = 6;
            this.rtb_Info.Text = "";
            // 
            // bt_Scan
            // 
            this.bt_Scan.Location = new System.Drawing.Point(910, 13);
            this.bt_Scan.Name = "bt_Scan";
            this.bt_Scan.Size = new System.Drawing.Size(86, 23);
            this.bt_Scan.TabIndex = 5;
            this.bt_Scan.Text = "Scan AT Port";
            this.bt_Scan.UseVisualStyleBackColor = true;
            this.bt_Scan.Click += new System.EventHandler(this.bt_Scan_Click);
            // 
            // bt_RFTest
            // 
            this.bt_RFTest.Location = new System.Drawing.Point(818, 42);
            this.bt_RFTest.Name = "bt_RFTest";
            this.bt_RFTest.Size = new System.Drawing.Size(86, 23);
            this.bt_RFTest.TabIndex = 5;
            this.bt_RFTest.Text = "RF Test";
            this.bt_RFTest.UseVisualStyleBackColor = true;
            this.bt_RFTest.Click += new System.EventHandler(this.bt_RFTest_Click);
            // 
            // cb_Port1
            // 
            this.cb_Port1.FormattingEnabled = true;
            this.cb_Port1.Location = new System.Drawing.Point(818, 14);
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
            this.bt_Download.Location = new System.Drawing.Point(910, 42);
            this.bt_Download.Name = "bt_Download";
            this.bt_Download.Size = new System.Drawing.Size(86, 23);
            this.bt_Download.TabIndex = 5;
            this.bt_Download.Text = "Download";
            this.bt_Download.UseVisualStyleBackColor = true;
            this.bt_Download.Click += new System.EventHandler(this.bt_RFTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1008, 473);
            this.Controls.Add(this.rtb_Info);
            this.Controls.Add(this.bt_Scan);
            this.Controls.Add(this.tabCtrl1);
            this.Controls.Add(this.bt_Download);
            this.Controls.Add(this.bt_RFTest);
            this.Controls.Add(this.cb_Port1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "NB Test Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabCtrl1.ResumeLayout(false);
            this.tabGraph.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrl1;
        private System.Windows.Forms.TabPage tabGraph;
        private System.Windows.Forms.Button bt_RFTest;
        private System.Windows.Forms.ComboBox cb_Port1;
        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox rtb_Log;
        private System.Windows.Forms.RichTextBox rtb_Info;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button bt_Scan;
        private System.Windows.Forms.Button bt_Download;
    }
}

