namespace SystemNetSpeedTest
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.cbx_NetCard = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rtb_Log
            // 
            this.rtb_Log.BackColor = System.Drawing.Color.AntiqueWhite;
            this.rtb_Log.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Log.ForeColor = System.Drawing.Color.Blue;
            this.rtb_Log.Location = new System.Drawing.Point(12, 12);
            this.rtb_Log.Name = "rtb_Log";
            this.rtb_Log.Size = new System.Drawing.Size(440, 230);
            this.rtb_Log.TabIndex = 0;
            this.rtb_Log.Text = "";
            this.rtb_Log.TextChanged += new System.EventHandler(this.rtb_Log_TextChanged);
            // 
            // cbx_NetCard
            // 
            this.cbx_NetCard.FormattingEnabled = true;
            this.cbx_NetCard.Location = new System.Drawing.Point(12, 248);
            this.cbx_NetCard.Name = "cbx_NetCard";
            this.cbx_NetCard.Size = new System.Drawing.Size(440, 21);
            this.cbx_NetCard.TabIndex = 1;
            this.cbx_NetCard.SelectedIndexChanged += new System.EventHandler(this.cbx_NetCard_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.cbx_NetCard);
            this.Controls.Add(this.rtb_Log);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "System Net Speed Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox rtb_Log;
        private System.Windows.Forms.ComboBox cbx_NetCard;
    }
}

