namespace PIC32MM_RTOS_Int_Asm_Gen
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_Vector = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_DefaultName = new System.Windows.Forms.TextBox();
            this.ckb_DefaultName = new System.Windows.Forms.CheckBox();
            this.bt_New = new System.Windows.Forms.Button();
            this.bt_Append = new System.Windows.Forms.Button();
            this.lb_Status = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bt_MakeAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vector:";
            // 
            // cbx_Vector
            // 
            this.cbx_Vector.FormattingEnabled = true;
            this.cbx_Vector.Items.AddRange(new object[] {
            "CORE_TIMER",
            "CORE_SOFTWARE_0",
            "CORE_SOFTWARE_1",
            "EXTERNAL_0",
            "EXTERNAL_1",
            "EXTERNAL_2",
            "EXTERNAL_3",
            "EXTERNAL_4",
            "CHANGE_NOTICE_A",
            "CHANGE_NOTICE_B",
            "CHANGE_NOTICE_C",
            "CHANGE_NOTICE_D",
            "TIMER_1",
            "TIMER_2",
            "TIMER_3",
            "COMPARATOR_1",
            "COMPARATOR_2",
            "COMPARATOR_3",
            "USB",
            "RTCC",
            "ADC",
            "HLVD",
            "CLC1",
            "CLC2",
            "CLC3",
            "CLC4",
            "SPI1_ERR",
            "SPI1_TX",
            "SPI1_RX",
            "SPI2_ERR",
            "SPI2_TX",
            "SPI2_RX",
            "SPI3_ERR",
            "SPI3_TX",
            "SPI3_RX",
            "UART1_RX",
            "UART1_TX",
            "UART1_ERR",
            "UART2_RX",
            "UART2_TX",
            "UART2_ERR",
            "UART3_RX",
            "UART3_TX",
            "UART3_ERR",
            "I2C1_SLAVE",
            "I2C1_MASTER",
            "I2C1_BUS",
            "I2C2_SLAVE",
            "I2C2_MASTER",
            "I2C2_BUS",
            "I2C3_SLAVE",
            "I2C3_MASTER",
            "I2C3_BUS",
            "CCP1",
            "CCT1",
            "CCP2",
            "CCT2",
            "CCP3",
            "CCT3",
            "CCP4",
            "CCT4",
            "CCP5",
            "CCT5",
            "CCP6",
            "CCT6",
            "CCP7",
            "CCT7",
            "CCP8",
            "CCT8",
            "CCP9",
            "CCT9",
            "FRC_TUNE",
            "NVM",
            "PERFORMANCE_COUNTER",
            "ECCSB_ERR",
            "DMA0",
            "DMA1",
            "DMA2",
            "DMA3"});
            this.cbx_Vector.Location = new System.Drawing.Point(75, 38);
            this.cbx_Vector.Name = "cbx_Vector";
            this.cbx_Vector.Size = new System.Drawing.Size(149, 21);
            this.cbx_Vector.TabIndex = 1;
            this.cbx_Vector.Text = "CORE_TIMER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "File Name:";
            // 
            // tb_DefaultName
            // 
            this.tb_DefaultName.Enabled = false;
            this.tb_DefaultName.Location = new System.Drawing.Point(75, 12);
            this.tb_DefaultName.MaxLength = 128;
            this.tb_DefaultName.Name = "tb_DefaultName";
            this.tb_DefaultName.Size = new System.Drawing.Size(149, 20);
            this.tb_DefaultName.TabIndex = 3;
            this.tb_DefaultName.Text = "interrupt_handler";
            this.tb_DefaultName.TextChanged += new System.EventHandler(this.tb_DefaultName_TextChanged);
            // 
            // ckb_DefaultName
            // 
            this.ckb_DefaultName.AutoSize = true;
            this.ckb_DefaultName.Checked = true;
            this.ckb_DefaultName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_DefaultName.Location = new System.Drawing.Point(230, 14);
            this.ckb_DefaultName.Name = "ckb_DefaultName";
            this.ckb_DefaultName.Size = new System.Drawing.Size(60, 17);
            this.ckb_DefaultName.TabIndex = 4;
            this.ckb_DefaultName.Text = "Default";
            this.ckb_DefaultName.UseVisualStyleBackColor = true;
            this.ckb_DefaultName.CheckedChanged += new System.EventHandler(this.ckb_DefaultName_CheckedChanged);
            // 
            // bt_New
            // 
            this.bt_New.Location = new System.Drawing.Point(320, 11);
            this.bt_New.Name = "bt_New";
            this.bt_New.Size = new System.Drawing.Size(52, 21);
            this.bt_New.TabIndex = 7;
            this.bt_New.Text = "New";
            this.bt_New.UseVisualStyleBackColor = true;
            this.bt_New.Click += new System.EventHandler(this.bt_New_Click);
            // 
            // bt_Append
            // 
            this.bt_Append.Location = new System.Drawing.Point(320, 38);
            this.bt_Append.Name = "bt_Append";
            this.bt_Append.Size = new System.Drawing.Size(52, 21);
            this.bt_Append.TabIndex = 8;
            this.bt_Append.Text = "Append";
            this.bt_Append.UseVisualStyleBackColor = true;
            this.bt_Append.Click += new System.EventHandler(this.bt_Append_Click);
            // 
            // lb_Status
            // 
            this.lb_Status.AutoSize = true;
            this.lb_Status.Location = new System.Drawing.Point(72, 62);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(35, 13);
            this.lb_Status.TabIndex = 9;
            this.lb_Status.Text = "label3";
            this.lb_Status.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // bt_MakeAll
            // 
            this.bt_MakeAll.Location = new System.Drawing.Point(230, 36);
            this.bt_MakeAll.Name = "bt_MakeAll";
            this.bt_MakeAll.Size = new System.Drawing.Size(84, 23);
            this.bt_MakeAll.TabIndex = 10;
            this.bt_MakeAll.Text = "Make All";
            this.bt_MakeAll.UseVisualStyleBackColor = true;
            this.bt_MakeAll.Click += new System.EventHandler(this.bt_MakeAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(384, 82);
            this.Controls.Add(this.bt_MakeAll);
            this.Controls.Add(this.lb_Status);
            this.Controls.Add(this.bt_Append);
            this.Controls.Add(this.bt_New);
            this.Controls.Add(this.ckb_DefaultName);
            this.Controls.Add(this.tb_DefaultName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbx_Vector);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "PIC32MM RTOS INT ASM GEN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_Vector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_DefaultName;
        private System.Windows.Forms.CheckBox ckb_DefaultName;
        private System.Windows.Forms.Button bt_New;
        private System.Windows.Forms.Button bt_Append;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button bt_MakeAll;
    }
}

