namespace MakeReg
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
            this.tb_Dec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Hex = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Bin = new System.Windows.Forms.TextBox();
            this.bt31 = new System.Windows.Forms.Button();
            this.bt30 = new System.Windows.Forms.Button();
            this.bt29 = new System.Windows.Forms.Button();
            this.bt28 = new System.Windows.Forms.Button();
            this.bt27 = new System.Windows.Forms.Button();
            this.bt26 = new System.Windows.Forms.Button();
            this.bt25 = new System.Windows.Forms.Button();
            this.bt24 = new System.Windows.Forms.Button();
            this.bt16 = new System.Windows.Forms.Button();
            this.bt17 = new System.Windows.Forms.Button();
            this.bt18 = new System.Windows.Forms.Button();
            this.bt19 = new System.Windows.Forms.Button();
            this.bt20 = new System.Windows.Forms.Button();
            this.bt21 = new System.Windows.Forms.Button();
            this.bt22 = new System.Windows.Forms.Button();
            this.bt23 = new System.Windows.Forms.Button();
            this.bt8 = new System.Windows.Forms.Button();
            this.bt9 = new System.Windows.Forms.Button();
            this.bt10 = new System.Windows.Forms.Button();
            this.bt11 = new System.Windows.Forms.Button();
            this.bt12 = new System.Windows.Forms.Button();
            this.bt13 = new System.Windows.Forms.Button();
            this.bt14 = new System.Windows.Forms.Button();
            this.bt15 = new System.Windows.Forms.Button();
            this.bt0 = new System.Windows.Forms.Button();
            this.bt1 = new System.Windows.Forms.Button();
            this.bt2 = new System.Windows.Forms.Button();
            this.bt3 = new System.Windows.Forms.Button();
            this.bt4 = new System.Windows.Forms.Button();
            this.bt5 = new System.Windows.Forms.Button();
            this.bt6 = new System.Windows.Forms.Button();
            this.bt7 = new System.Windows.Forms.Button();
            this.bt_Set = new System.Windows.Forms.Button();
            this.bt_Clear = new System.Windows.Forms.Button();
            this.lb_Stt = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.rbt_8bit = new System.Windows.Forms.RadioButton();
            this.rbt_16bit = new System.Windows.Forms.RadioButton();
            this.rbt_32bit = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // tb_Dec
            // 
            this.tb_Dec.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_Dec.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Dec.Location = new System.Drawing.Point(240, 12);
            this.tb_Dec.MaxLength = 13;
            this.tb_Dec.Name = "tb_Dec";
            this.tb_Dec.Size = new System.Drawing.Size(119, 25);
            this.tb_Dec.TabIndex = 0;
            this.tb_Dec.Text = "0.000.000.000";
            this.tb_Dec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Dec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Dec_KeyDown);
            this.tb_Dec.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tb_Dec_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(204, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dec:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hex:";
            // 
            // tb_Hex
            // 
            this.tb_Hex.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_Hex.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Hex.Location = new System.Drawing.Point(68, 12);
            this.tb_Hex.MaxLength = 11;
            this.tb_Hex.Name = "tb_Hex";
            this.tb_Hex.Size = new System.Drawing.Size(119, 25);
            this.tb_Hex.TabIndex = 3;
            this.tb_Hex.Text = "00.00.00.00";
            this.tb_Hex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Hex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Hex_KeyDown);
            this.tb_Hex.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tb_Hex_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Bin:";
            // 
            // tb_Bin
            // 
            this.tb_Bin.BackColor = System.Drawing.Color.NavajoWhite;
            this.tb_Bin.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Bin.Location = new System.Drawing.Point(68, 43);
            this.tb_Bin.MaxLength = 35;
            this.tb_Bin.Name = "tb_Bin";
            this.tb_Bin.Size = new System.Drawing.Size(291, 25);
            this.tb_Bin.TabIndex = 5;
            this.tb_Bin.Text = "00000000.00000000.00000000.00000000";
            this.tb_Bin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Bin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Bin_KeyDown);
            this.tb_Bin.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tb_Bin_MouseDoubleClick);
            // 
            // bt31
            // 
            this.bt31.BackColor = System.Drawing.Color.LightGreen;
            this.bt31.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt31.ForeColor = System.Drawing.Color.Black;
            this.bt31.Location = new System.Drawing.Point(68, 74);
            this.bt31.Name = "bt31";
            this.bt31.Size = new System.Drawing.Size(29, 23);
            this.bt31.TabIndex = 7;
            this.bt31.Text = "31";
            this.bt31.UseVisualStyleBackColor = false;
            this.bt31.Click += new System.EventHandler(this.bt31_Click);
            // 
            // bt30
            // 
            this.bt30.BackColor = System.Drawing.Color.LightGreen;
            this.bt30.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt30.ForeColor = System.Drawing.Color.Black;
            this.bt30.Location = new System.Drawing.Point(103, 74);
            this.bt30.Name = "bt30";
            this.bt30.Size = new System.Drawing.Size(29, 23);
            this.bt30.TabIndex = 8;
            this.bt30.Text = "30";
            this.bt30.UseVisualStyleBackColor = false;
            this.bt30.Click += new System.EventHandler(this.bt30_Click);
            // 
            // bt29
            // 
            this.bt29.BackColor = System.Drawing.Color.LightGreen;
            this.bt29.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt29.ForeColor = System.Drawing.Color.Black;
            this.bt29.Location = new System.Drawing.Point(138, 74);
            this.bt29.Name = "bt29";
            this.bt29.Size = new System.Drawing.Size(29, 23);
            this.bt29.TabIndex = 9;
            this.bt29.Text = "29";
            this.bt29.UseVisualStyleBackColor = false;
            this.bt29.Click += new System.EventHandler(this.bt29_Click);
            // 
            // bt28
            // 
            this.bt28.BackColor = System.Drawing.Color.LightGreen;
            this.bt28.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt28.ForeColor = System.Drawing.Color.Black;
            this.bt28.Location = new System.Drawing.Point(173, 74);
            this.bt28.Name = "bt28";
            this.bt28.Size = new System.Drawing.Size(29, 23);
            this.bt28.TabIndex = 10;
            this.bt28.Text = "28";
            this.bt28.UseVisualStyleBackColor = false;
            this.bt28.Click += new System.EventHandler(this.bt28_Click);
            // 
            // bt27
            // 
            this.bt27.BackColor = System.Drawing.Color.LightGreen;
            this.bt27.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt27.ForeColor = System.Drawing.Color.Black;
            this.bt27.Location = new System.Drawing.Point(225, 74);
            this.bt27.Name = "bt27";
            this.bt27.Size = new System.Drawing.Size(29, 23);
            this.bt27.TabIndex = 11;
            this.bt27.Text = "27";
            this.bt27.UseVisualStyleBackColor = false;
            this.bt27.Click += new System.EventHandler(this.bt27_Click);
            // 
            // bt26
            // 
            this.bt26.BackColor = System.Drawing.Color.LightGreen;
            this.bt26.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt26.ForeColor = System.Drawing.Color.Black;
            this.bt26.Location = new System.Drawing.Point(260, 74);
            this.bt26.Name = "bt26";
            this.bt26.Size = new System.Drawing.Size(29, 23);
            this.bt26.TabIndex = 12;
            this.bt26.Text = "26";
            this.bt26.UseVisualStyleBackColor = false;
            this.bt26.Click += new System.EventHandler(this.bt26_Click);
            // 
            // bt25
            // 
            this.bt25.BackColor = System.Drawing.Color.LightGreen;
            this.bt25.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt25.ForeColor = System.Drawing.Color.Black;
            this.bt25.Location = new System.Drawing.Point(295, 74);
            this.bt25.Name = "bt25";
            this.bt25.Size = new System.Drawing.Size(29, 23);
            this.bt25.TabIndex = 13;
            this.bt25.Text = "25";
            this.bt25.UseVisualStyleBackColor = false;
            this.bt25.Click += new System.EventHandler(this.bt25_Click);
            // 
            // bt24
            // 
            this.bt24.BackColor = System.Drawing.Color.LightGreen;
            this.bt24.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt24.ForeColor = System.Drawing.Color.Black;
            this.bt24.Location = new System.Drawing.Point(330, 74);
            this.bt24.Name = "bt24";
            this.bt24.Size = new System.Drawing.Size(29, 23);
            this.bt24.TabIndex = 14;
            this.bt24.Text = "24";
            this.bt24.UseVisualStyleBackColor = false;
            this.bt24.Click += new System.EventHandler(this.bt24_Click);
            // 
            // bt16
            // 
            this.bt16.BackColor = System.Drawing.Color.LightGreen;
            this.bt16.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt16.ForeColor = System.Drawing.Color.Black;
            this.bt16.Location = new System.Drawing.Point(330, 103);
            this.bt16.Name = "bt16";
            this.bt16.Size = new System.Drawing.Size(29, 23);
            this.bt16.TabIndex = 22;
            this.bt16.Text = "16";
            this.bt16.UseVisualStyleBackColor = false;
            this.bt16.Click += new System.EventHandler(this.bt16_Click);
            // 
            // bt17
            // 
            this.bt17.BackColor = System.Drawing.Color.LightGreen;
            this.bt17.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt17.ForeColor = System.Drawing.Color.Black;
            this.bt17.Location = new System.Drawing.Point(295, 103);
            this.bt17.Name = "bt17";
            this.bt17.Size = new System.Drawing.Size(29, 23);
            this.bt17.TabIndex = 21;
            this.bt17.Text = "17";
            this.bt17.UseVisualStyleBackColor = false;
            this.bt17.Click += new System.EventHandler(this.bt17_Click);
            // 
            // bt18
            // 
            this.bt18.BackColor = System.Drawing.Color.LightGreen;
            this.bt18.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt18.ForeColor = System.Drawing.Color.Black;
            this.bt18.Location = new System.Drawing.Point(260, 103);
            this.bt18.Name = "bt18";
            this.bt18.Size = new System.Drawing.Size(29, 23);
            this.bt18.TabIndex = 20;
            this.bt18.Text = "18";
            this.bt18.UseVisualStyleBackColor = false;
            this.bt18.Click += new System.EventHandler(this.bt18_Click);
            // 
            // bt19
            // 
            this.bt19.BackColor = System.Drawing.Color.LightGreen;
            this.bt19.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt19.ForeColor = System.Drawing.Color.Black;
            this.bt19.Location = new System.Drawing.Point(225, 103);
            this.bt19.Name = "bt19";
            this.bt19.Size = new System.Drawing.Size(29, 23);
            this.bt19.TabIndex = 19;
            this.bt19.Text = "19";
            this.bt19.UseVisualStyleBackColor = false;
            this.bt19.Click += new System.EventHandler(this.bt19_Click);
            // 
            // bt20
            // 
            this.bt20.BackColor = System.Drawing.Color.LightGreen;
            this.bt20.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt20.ForeColor = System.Drawing.Color.Black;
            this.bt20.Location = new System.Drawing.Point(173, 103);
            this.bt20.Name = "bt20";
            this.bt20.Size = new System.Drawing.Size(29, 23);
            this.bt20.TabIndex = 18;
            this.bt20.Text = "20";
            this.bt20.UseVisualStyleBackColor = false;
            this.bt20.Click += new System.EventHandler(this.bt20_Click);
            // 
            // bt21
            // 
            this.bt21.BackColor = System.Drawing.Color.LightGreen;
            this.bt21.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt21.ForeColor = System.Drawing.Color.Black;
            this.bt21.Location = new System.Drawing.Point(138, 103);
            this.bt21.Name = "bt21";
            this.bt21.Size = new System.Drawing.Size(29, 23);
            this.bt21.TabIndex = 17;
            this.bt21.Text = "21";
            this.bt21.UseVisualStyleBackColor = false;
            this.bt21.Click += new System.EventHandler(this.bt21_Click);
            // 
            // bt22
            // 
            this.bt22.BackColor = System.Drawing.Color.LightGreen;
            this.bt22.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt22.ForeColor = System.Drawing.Color.Black;
            this.bt22.Location = new System.Drawing.Point(103, 103);
            this.bt22.Name = "bt22";
            this.bt22.Size = new System.Drawing.Size(29, 23);
            this.bt22.TabIndex = 16;
            this.bt22.Text = "22";
            this.bt22.UseVisualStyleBackColor = false;
            this.bt22.Click += new System.EventHandler(this.bt22_Click);
            // 
            // bt23
            // 
            this.bt23.BackColor = System.Drawing.Color.LightGreen;
            this.bt23.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt23.ForeColor = System.Drawing.Color.Black;
            this.bt23.Location = new System.Drawing.Point(68, 103);
            this.bt23.Name = "bt23";
            this.bt23.Size = new System.Drawing.Size(29, 23);
            this.bt23.TabIndex = 15;
            this.bt23.Text = "23";
            this.bt23.UseVisualStyleBackColor = false;
            this.bt23.Click += new System.EventHandler(this.bt23_Click);
            // 
            // bt8
            // 
            this.bt8.BackColor = System.Drawing.Color.LightGreen;
            this.bt8.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt8.ForeColor = System.Drawing.Color.Black;
            this.bt8.Location = new System.Drawing.Point(330, 132);
            this.bt8.Name = "bt8";
            this.bt8.Size = new System.Drawing.Size(29, 23);
            this.bt8.TabIndex = 30;
            this.bt8.Text = "8";
            this.bt8.UseVisualStyleBackColor = false;
            this.bt8.Click += new System.EventHandler(this.bt8_Click);
            // 
            // bt9
            // 
            this.bt9.BackColor = System.Drawing.Color.LightGreen;
            this.bt9.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt9.ForeColor = System.Drawing.Color.Black;
            this.bt9.Location = new System.Drawing.Point(295, 132);
            this.bt9.Name = "bt9";
            this.bt9.Size = new System.Drawing.Size(29, 23);
            this.bt9.TabIndex = 29;
            this.bt9.Text = "9";
            this.bt9.UseVisualStyleBackColor = false;
            this.bt9.Click += new System.EventHandler(this.bt9_Click);
            // 
            // bt10
            // 
            this.bt10.BackColor = System.Drawing.Color.LightGreen;
            this.bt10.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt10.ForeColor = System.Drawing.Color.Black;
            this.bt10.Location = new System.Drawing.Point(260, 132);
            this.bt10.Name = "bt10";
            this.bt10.Size = new System.Drawing.Size(29, 23);
            this.bt10.TabIndex = 28;
            this.bt10.Text = "10";
            this.bt10.UseVisualStyleBackColor = false;
            this.bt10.Click += new System.EventHandler(this.bt10_Click);
            // 
            // bt11
            // 
            this.bt11.BackColor = System.Drawing.Color.LightGreen;
            this.bt11.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt11.ForeColor = System.Drawing.Color.Black;
            this.bt11.Location = new System.Drawing.Point(225, 132);
            this.bt11.Name = "bt11";
            this.bt11.Size = new System.Drawing.Size(29, 23);
            this.bt11.TabIndex = 27;
            this.bt11.Text = "11";
            this.bt11.UseVisualStyleBackColor = false;
            this.bt11.Click += new System.EventHandler(this.bt11_Click);
            // 
            // bt12
            // 
            this.bt12.BackColor = System.Drawing.Color.LightGreen;
            this.bt12.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt12.ForeColor = System.Drawing.Color.Black;
            this.bt12.Location = new System.Drawing.Point(173, 132);
            this.bt12.Name = "bt12";
            this.bt12.Size = new System.Drawing.Size(29, 23);
            this.bt12.TabIndex = 26;
            this.bt12.Text = "12";
            this.bt12.UseVisualStyleBackColor = false;
            this.bt12.Click += new System.EventHandler(this.bt12_Click);
            // 
            // bt13
            // 
            this.bt13.BackColor = System.Drawing.Color.LightGreen;
            this.bt13.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt13.ForeColor = System.Drawing.Color.Black;
            this.bt13.Location = new System.Drawing.Point(138, 132);
            this.bt13.Name = "bt13";
            this.bt13.Size = new System.Drawing.Size(29, 23);
            this.bt13.TabIndex = 25;
            this.bt13.Text = "13";
            this.bt13.UseVisualStyleBackColor = false;
            this.bt13.Click += new System.EventHandler(this.bt13_Click);
            // 
            // bt14
            // 
            this.bt14.BackColor = System.Drawing.Color.LightGreen;
            this.bt14.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt14.ForeColor = System.Drawing.Color.Black;
            this.bt14.Location = new System.Drawing.Point(103, 132);
            this.bt14.Name = "bt14";
            this.bt14.Size = new System.Drawing.Size(29, 23);
            this.bt14.TabIndex = 24;
            this.bt14.Text = "14";
            this.bt14.UseVisualStyleBackColor = false;
            this.bt14.Click += new System.EventHandler(this.bt14_Click);
            // 
            // bt15
            // 
            this.bt15.BackColor = System.Drawing.Color.LightGreen;
            this.bt15.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt15.ForeColor = System.Drawing.Color.Black;
            this.bt15.Location = new System.Drawing.Point(68, 132);
            this.bt15.Name = "bt15";
            this.bt15.Size = new System.Drawing.Size(29, 23);
            this.bt15.TabIndex = 23;
            this.bt15.Text = "15";
            this.bt15.UseVisualStyleBackColor = false;
            this.bt15.Click += new System.EventHandler(this.bt15_Click);
            // 
            // bt0
            // 
            this.bt0.BackColor = System.Drawing.Color.LightGreen;
            this.bt0.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt0.ForeColor = System.Drawing.Color.Black;
            this.bt0.Location = new System.Drawing.Point(330, 161);
            this.bt0.Name = "bt0";
            this.bt0.Size = new System.Drawing.Size(29, 23);
            this.bt0.TabIndex = 38;
            this.bt0.Text = "0";
            this.bt0.UseVisualStyleBackColor = false;
            this.bt0.Click += new System.EventHandler(this.bt0_Click);
            // 
            // bt1
            // 
            this.bt1.BackColor = System.Drawing.Color.LightGreen;
            this.bt1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt1.ForeColor = System.Drawing.Color.Black;
            this.bt1.Location = new System.Drawing.Point(295, 161);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(29, 23);
            this.bt1.TabIndex = 37;
            this.bt1.Text = "1";
            this.bt1.UseVisualStyleBackColor = false;
            this.bt1.Click += new System.EventHandler(this.bt1_Click);
            // 
            // bt2
            // 
            this.bt2.BackColor = System.Drawing.Color.LightGreen;
            this.bt2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt2.ForeColor = System.Drawing.Color.Black;
            this.bt2.Location = new System.Drawing.Point(260, 161);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(29, 23);
            this.bt2.TabIndex = 36;
            this.bt2.Text = "2";
            this.bt2.UseVisualStyleBackColor = false;
            this.bt2.Click += new System.EventHandler(this.bt2_Click);
            // 
            // bt3
            // 
            this.bt3.BackColor = System.Drawing.Color.LightGreen;
            this.bt3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt3.ForeColor = System.Drawing.Color.Black;
            this.bt3.Location = new System.Drawing.Point(225, 161);
            this.bt3.Name = "bt3";
            this.bt3.Size = new System.Drawing.Size(29, 23);
            this.bt3.TabIndex = 35;
            this.bt3.Text = "3";
            this.bt3.UseVisualStyleBackColor = false;
            this.bt3.Click += new System.EventHandler(this.bt3_Click);
            // 
            // bt4
            // 
            this.bt4.BackColor = System.Drawing.Color.LightGreen;
            this.bt4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt4.ForeColor = System.Drawing.Color.Black;
            this.bt4.Location = new System.Drawing.Point(173, 161);
            this.bt4.Name = "bt4";
            this.bt4.Size = new System.Drawing.Size(29, 23);
            this.bt4.TabIndex = 34;
            this.bt4.Text = "4";
            this.bt4.UseVisualStyleBackColor = false;
            this.bt4.Click += new System.EventHandler(this.bt4_Click);
            // 
            // bt5
            // 
            this.bt5.BackColor = System.Drawing.Color.LightGreen;
            this.bt5.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt5.ForeColor = System.Drawing.Color.Black;
            this.bt5.Location = new System.Drawing.Point(138, 161);
            this.bt5.Name = "bt5";
            this.bt5.Size = new System.Drawing.Size(29, 23);
            this.bt5.TabIndex = 33;
            this.bt5.Text = "5";
            this.bt5.UseVisualStyleBackColor = false;
            this.bt5.Click += new System.EventHandler(this.bt5_Click);
            // 
            // bt6
            // 
            this.bt6.BackColor = System.Drawing.Color.LightGreen;
            this.bt6.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt6.ForeColor = System.Drawing.Color.Black;
            this.bt6.Location = new System.Drawing.Point(103, 161);
            this.bt6.Name = "bt6";
            this.bt6.Size = new System.Drawing.Size(29, 23);
            this.bt6.TabIndex = 32;
            this.bt6.Text = "6";
            this.bt6.UseVisualStyleBackColor = false;
            this.bt6.Click += new System.EventHandler(this.bt6_Click);
            // 
            // bt7
            // 
            this.bt7.BackColor = System.Drawing.Color.LightGreen;
            this.bt7.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt7.ForeColor = System.Drawing.Color.Black;
            this.bt7.Location = new System.Drawing.Point(68, 161);
            this.bt7.Name = "bt7";
            this.bt7.Size = new System.Drawing.Size(29, 23);
            this.bt7.TabIndex = 31;
            this.bt7.Text = "7";
            this.bt7.UseVisualStyleBackColor = false;
            this.bt7.Click += new System.EventHandler(this.bt7_Click);
            // 
            // bt_Set
            // 
            this.bt_Set.BackColor = System.Drawing.Color.Salmon;
            this.bt_Set.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Set.Location = new System.Drawing.Point(13, 103);
            this.bt_Set.Name = "bt_Set";
            this.bt_Set.Size = new System.Drawing.Size(49, 37);
            this.bt_Set.TabIndex = 39;
            this.bt_Set.Text = "Set";
            this.bt_Set.UseVisualStyleBackColor = false;
            this.bt_Set.Click += new System.EventHandler(this.bt_Set_Click);
            // 
            // bt_Clear
            // 
            this.bt_Clear.BackColor = System.Drawing.Color.LightGreen;
            this.bt_Clear.Location = new System.Drawing.Point(13, 146);
            this.bt_Clear.Name = "bt_Clear";
            this.bt_Clear.Size = new System.Drawing.Size(49, 37);
            this.bt_Clear.TabIndex = 40;
            this.bt_Clear.Text = "Clear";
            this.bt_Clear.UseVisualStyleBackColor = false;
            this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
            // 
            // lb_Stt
            // 
            this.lb_Stt.AutoSize = true;
            this.lb_Stt.Location = new System.Drawing.Point(12, 194);
            this.lb_Stt.Name = "lb_Stt";
            this.lb_Stt.Size = new System.Drawing.Size(40, 13);
            this.lb_Stt.TabIndex = 41;
            this.lb_Stt.Text = "Copied";
            this.lb_Stt.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Bit Field:";
            // 
            // rbt_8bit
            // 
            this.rbt_8bit.AutoSize = true;
            this.rbt_8bit.Location = new System.Drawing.Point(68, 192);
            this.rbt_8bit.Name = "rbt_8bit";
            this.rbt_8bit.Size = new System.Drawing.Size(45, 17);
            this.rbt_8bit.TabIndex = 43;
            this.rbt_8bit.Text = "8-bit";
            this.rbt_8bit.UseVisualStyleBackColor = true;
            this.rbt_8bit.CheckedChanged += new System.EventHandler(this.rbt_8bit_CheckedChanged);
            // 
            // rbt_16bit
            // 
            this.rbt_16bit.AutoSize = true;
            this.rbt_16bit.Location = new System.Drawing.Point(187, 192);
            this.rbt_16bit.Name = "rbt_16bit";
            this.rbt_16bit.Size = new System.Drawing.Size(51, 17);
            this.rbt_16bit.TabIndex = 44;
            this.rbt_16bit.Text = "16-bit";
            this.rbt_16bit.UseVisualStyleBackColor = true;
            this.rbt_16bit.CheckedChanged += new System.EventHandler(this.rbt_16bit_CheckedChanged);
            // 
            // rbt_32bit
            // 
            this.rbt_32bit.AutoSize = true;
            this.rbt_32bit.Checked = true;
            this.rbt_32bit.Location = new System.Drawing.Point(308, 192);
            this.rbt_32bit.Name = "rbt_32bit";
            this.rbt_32bit.Size = new System.Drawing.Size(51, 17);
            this.rbt_32bit.TabIndex = 45;
            this.rbt_32bit.TabStop = true;
            this.rbt_32bit.Text = "32-bit";
            this.rbt_32bit.UseVisualStyleBackColor = true;
            this.rbt_32bit.CheckedChanged += new System.EventHandler(this.rbt_32bit_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(374, 221);
            this.Controls.Add(this.rbt_32bit);
            this.Controls.Add(this.rbt_16bit);
            this.Controls.Add(this.rbt_8bit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_Stt);
            this.Controls.Add(this.bt_Clear);
            this.Controls.Add(this.bt_Set);
            this.Controls.Add(this.bt0);
            this.Controls.Add(this.bt1);
            this.Controls.Add(this.bt2);
            this.Controls.Add(this.bt3);
            this.Controls.Add(this.bt4);
            this.Controls.Add(this.bt5);
            this.Controls.Add(this.bt6);
            this.Controls.Add(this.bt7);
            this.Controls.Add(this.bt8);
            this.Controls.Add(this.bt9);
            this.Controls.Add(this.bt10);
            this.Controls.Add(this.bt11);
            this.Controls.Add(this.bt12);
            this.Controls.Add(this.bt13);
            this.Controls.Add(this.bt14);
            this.Controls.Add(this.bt15);
            this.Controls.Add(this.bt16);
            this.Controls.Add(this.bt17);
            this.Controls.Add(this.bt18);
            this.Controls.Add(this.bt19);
            this.Controls.Add(this.bt20);
            this.Controls.Add(this.bt21);
            this.Controls.Add(this.bt22);
            this.Controls.Add(this.bt23);
            this.Controls.Add(this.bt24);
            this.Controls.Add(this.bt25);
            this.Controls.Add(this.bt26);
            this.Controls.Add(this.bt27);
            this.Controls.Add(this.bt28);
            this.Controls.Add(this.bt29);
            this.Controls.Add(this.bt30);
            this.Controls.Add(this.bt31);
            this.Controls.Add(this.tb_Bin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_Hex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Dec);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Make Register";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Dec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Hex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Bin;
        private System.Windows.Forms.Button bt31;
        private System.Windows.Forms.Button bt30;
        private System.Windows.Forms.Button bt29;
        private System.Windows.Forms.Button bt28;
        private System.Windows.Forms.Button bt27;
        private System.Windows.Forms.Button bt26;
        private System.Windows.Forms.Button bt25;
        private System.Windows.Forms.Button bt24;
        private System.Windows.Forms.Button bt16;
        private System.Windows.Forms.Button bt17;
        private System.Windows.Forms.Button bt18;
        private System.Windows.Forms.Button bt19;
        private System.Windows.Forms.Button bt20;
        private System.Windows.Forms.Button bt21;
        private System.Windows.Forms.Button bt22;
        private System.Windows.Forms.Button bt23;
        private System.Windows.Forms.Button bt8;
        private System.Windows.Forms.Button bt9;
        private System.Windows.Forms.Button bt10;
        private System.Windows.Forms.Button bt11;
        private System.Windows.Forms.Button bt12;
        private System.Windows.Forms.Button bt13;
        private System.Windows.Forms.Button bt14;
        private System.Windows.Forms.Button bt15;
        private System.Windows.Forms.Button bt0;
        private System.Windows.Forms.Button bt1;
        private System.Windows.Forms.Button bt2;
        private System.Windows.Forms.Button bt3;
        private System.Windows.Forms.Button bt4;
        private System.Windows.Forms.Button bt5;
        private System.Windows.Forms.Button bt6;
        private System.Windows.Forms.Button bt7;
        private System.Windows.Forms.Button bt_Set;
        private System.Windows.Forms.Button bt_Clear;
        private System.Windows.Forms.Label lb_Stt;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbt_8bit;
        private System.Windows.Forms.RadioButton rbt_16bit;
        private System.Windows.Forms.RadioButton rbt_32bit;
    }
}

