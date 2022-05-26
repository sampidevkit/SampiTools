namespace RTChart
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Text.ASCIIEncoding asciiEncodingSealed1 = new System.Text.ASCIIEncoding();
            System.Text.DecoderReplacementFallback decoderReplacementFallback1 = new System.Text.DecoderReplacementFallback();
            System.Text.EncoderReplacementFallback encoderReplacementFallback1 = new System.Text.EncoderReplacementFallback();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.cb_Port = new System.Windows.Forms.ComboBox();
            this.cb_Baud = new System.Windows.Forms.ComboBox();
            this.pic_expand = new System.Windows.Forms.PictureBox();
            this.bt_Plot = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pic_expand)).BeginInit();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.zedGraphControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.zedGraphControl1.Location = new System.Drawing.Point(12, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(680, 420);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // cb_Port
            // 
            this.cb_Port.FormattingEnabled = true;
            this.cb_Port.Items.AddRange(new object[] {
            "COM Port"});
            this.cb_Port.Location = new System.Drawing.Point(435, 443);
            this.cb_Port.Name = "cb_Port";
            this.cb_Port.Size = new System.Drawing.Size(77, 23);
            this.cb_Port.TabIndex = 2;
            this.cb_Port.Text = "COM Port";
            // 
            // cb_Baud
            // 
            this.cb_Baud.FormattingEnabled = true;
            this.cb_Baud.Items.AddRange(new object[] {
            "Baud Rate",
            "9600",
            "19200",
            "38400",
            "57600",
            "86400",
            "115200",
            "144000",
            "192000"});
            this.cb_Baud.Location = new System.Drawing.Point(518, 443);
            this.cb_Baud.Name = "cb_Baud";
            this.cb_Baud.Size = new System.Drawing.Size(77, 23);
            this.cb_Baud.TabIndex = 2;
            this.cb_Baud.Text = "Baud Rate";
            this.cb_Baud.SelectedIndexChanged += new System.EventHandler(this.cb_Baud_SelectedIndexChanged);
            // 
            // pic_expand
            // 
            this.pic_expand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_expand.Image = global::RTChart.Properties.Resources.Expand;
            this.pic_expand.Location = new System.Drawing.Point(668, 438);
            this.pic_expand.Name = "pic_expand";
            this.pic_expand.Size = new System.Drawing.Size(24, 31);
            this.pic_expand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_expand.TabIndex = 4;
            this.pic_expand.TabStop = false;
            // 
            // bt_Plot
            // 
            this.bt_Plot.BackgroundImage = global::RTChart.Properties.Resources.Off;
            this.bt_Plot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bt_Plot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Plot.Location = new System.Drawing.Point(601, 438);
            this.bt_Plot.Name = "bt_Plot";
            this.bt_Plot.Size = new System.Drawing.Size(61, 31);
            this.bt_Plot.TabIndex = 7;
            this.bt_Plot.Text = "Plot";
            this.bt_Plot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_Plot.UseVisualStyleBackColor = true;
            this.bt_Plot.Click += new System.EventHandler(this.bt_Plot_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 9600;
            this.serialPort1.DataBits = 8;
            this.serialPort1.DiscardNull = false;
            this.serialPort1.DtrEnable = false;
            asciiEncodingSealed1.DecoderFallback = decoderReplacementFallback1;
            asciiEncodingSealed1.EncoderFallback = encoderReplacementFallback1;
            this.serialPort1.Encoding = asciiEncodingSealed1;
            this.serialPort1.Handshake = System.IO.Ports.Handshake.None;
            this.serialPort1.NewLine = "\n";
            this.serialPort1.Parity = System.IO.Ports.Parity.None;
            this.serialPort1.ParityReplace = ((byte)(63));
            this.serialPort1.PortName = "COM1";
            this.serialPort1.ReadBufferSize = 4096;
            this.serialPort1.ReadTimeout = -1;
            this.serialPort1.ReceivedBytesThreshold = 1;
            this.serialPort1.RtsEnable = false;
            this.serialPort1.StopBits = System.IO.Ports.StopBits.One;
            this.serialPort1.WriteBufferSize = 2048;
            this.serialPort1.WriteTimeout = -1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(704, 481);
            this.Controls.Add(this.bt_Plot);
            this.Controls.Add(this.pic_expand);
            this.Controls.Add(this.cb_Baud);
            this.Controls.Add(this.cb_Port);
            this.Controls.Add(this.zedGraphControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic_expand)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ComboBox cb_Port;
        private System.Windows.Forms.ComboBox cb_Baud;
        private System.Windows.Forms.PictureBox pic_expand;
        private System.Windows.Forms.Button bt_Plot;
        private System.IO.Ports.SerialPort serialPort1;
    }
}
