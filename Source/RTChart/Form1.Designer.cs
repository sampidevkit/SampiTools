namespace RTChart
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
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.bt_Options = new System.Windows.Forms.Button();
            this.bt_Import = new System.Windows.Forms.Button();
            this.bt_Plot = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cbx_Ports = new System.Windows.Forms.ComboBox();
            this.tb_Directory = new System.Windows.Forms.TextBox();
            this.cbx_Baud = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(12, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(760, 385);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // bt_Options
            // 
            this.bt_Options.Image = global::RTChart.Properties.Resources.Options;
            this.bt_Options.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Options.Location = new System.Drawing.Point(692, 411);
            this.bt_Options.Name = "bt_Options";
            this.bt_Options.Size = new System.Drawing.Size(80, 32);
            this.bt_Options.TabIndex = 2;
            this.bt_Options.Text = "Options";
            this.bt_Options.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_Options.UseVisualStyleBackColor = true;
            this.bt_Options.Click += new System.EventHandler(this.bt_Options_Click);
            // 
            // bt_Import
            // 
            this.bt_Import.Image = global::RTChart.Properties.Resources.Import;
            this.bt_Import.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Import.Location = new System.Drawing.Point(12, 411);
            this.bt_Import.Name = "bt_Import";
            this.bt_Import.Size = new System.Drawing.Size(80, 32);
            this.bt_Import.TabIndex = 1;
            this.bt_Import.Text = "Import";
            this.bt_Import.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_Import.UseVisualStyleBackColor = true;
            this.bt_Import.Click += new System.EventHandler(this.bt_Import_Click);
            // 
            // bt_Plot
            // 
            this.bt_Plot.Enabled = false;
            this.bt_Plot.Image = global::RTChart.Properties.Resources.Off;
            this.bt_Plot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Plot.Location = new System.Drawing.Point(606, 411);
            this.bt_Plot.Name = "bt_Plot";
            this.bt_Plot.Size = new System.Drawing.Size(80, 32);
            this.bt_Plot.TabIndex = 3;
            this.bt_Plot.Text = "Chart Off";
            this.bt_Plot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bt_Plot.UseVisualStyleBackColor = true;
            this.bt_Plot.Click += new System.EventHandler(this.bt_Plot_Click);
            // 
            // cbx_Ports
            // 
            this.cbx_Ports.FormattingEnabled = true;
            this.cbx_Ports.Items.AddRange(new object[] {
            "COM Port"});
            this.cbx_Ports.Location = new System.Drawing.Point(434, 418);
            this.cbx_Ports.Name = "cbx_Ports";
            this.cbx_Ports.Size = new System.Drawing.Size(80, 21);
            this.cbx_Ports.TabIndex = 5;
            this.cbx_Ports.Text = "COM Port";
            this.cbx_Ports.SelectedIndexChanged += new System.EventHandler(this.cbx_Ports_SelectedIndexChanged);
            this.cbx_Ports.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbx_Ports_MouseClick);
            // 
            // tb_Directory
            // 
            this.tb_Directory.Location = new System.Drawing.Point(98, 418);
            this.tb_Directory.Name = "tb_Directory";
            this.tb_Directory.ReadOnly = true;
            this.tb_Directory.Size = new System.Drawing.Size(330, 20);
            this.tb_Directory.TabIndex = 6;
            // 
            // cbx_Baud
            // 
            this.cbx_Baud.FormattingEnabled = true;
            this.cbx_Baud.Items.AddRange(new object[] {
            "COM Port",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbx_Baud.Location = new System.Drawing.Point(520, 418);
            this.cbx_Baud.Name = "cbx_Baud";
            this.cbx_Baud.Size = new System.Drawing.Size(80, 21);
            this.cbx_Baud.TabIndex = 5;
            this.cbx_Baud.Text = "Baud Rate";
            this.cbx_Baud.SelectedIndexChanged += new System.EventHandler(this.cbx_Baud_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Configure files (*.rtcfg)|*.rtcfg";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(784, 455);
            this.Controls.Add(this.tb_Directory);
            this.Controls.Add(this.cbx_Baud);
            this.Controls.Add(this.cbx_Ports);
            this.Controls.Add(this.bt_Plot);
            this.Controls.Add(this.bt_Options);
            this.Controls.Add(this.bt_Import);
            this.Controls.Add(this.zedGraphControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "RT Chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button bt_Import;
        private System.Windows.Forms.Button bt_Options;
        private System.Windows.Forms.Button bt_Plot;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cbx_Ports;
        private System.Windows.Forms.TextBox tb_Directory;
        private System.Windows.Forms.ComboBox cbx_Baud;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

