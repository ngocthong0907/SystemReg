namespace NinjaSystem
{
    partial class userLD
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lbnStatus = new System.Windows.Forms.Label();
            this.lbnIP = new System.Windows.Forms.Label();
            this.lbnDevice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbnTong = new System.Windows.Forms.Label();
            this.lbnThanhCong = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pibStatus = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.lbnStatus);
            this.panel1.Controls.Add(this.lbnIP);
            this.panel1.Controls.Add(this.lbnDevice);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbnTong);
            this.panel1.Controls.Add(this.lbnThanhCong);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 486);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 40);
            this.panel1.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(13, 13);
            this.linkLabel1.TabIndex = 142;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "?";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lbnStatus
            // 
            this.lbnStatus.AutoSize = true;
            this.lbnStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbnStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnStatus.ForeColor = System.Drawing.Color.White;
            this.lbnStatus.Location = new System.Drawing.Point(38, 26);
            this.lbnStatus.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbnStatus.Name = "lbnStatus";
            this.lbnStatus.Size = new System.Drawing.Size(37, 13);
            this.lbnStatus.TabIndex = 139;
            this.lbnStatus.Text = "Status";
            // 
            // lbnIP
            // 
            this.lbnIP.AutoSize = true;
            this.lbnIP.BackColor = System.Drawing.Color.Transparent;
            this.lbnIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnIP.ForeColor = System.Drawing.Color.White;
            this.lbnIP.Location = new System.Drawing.Point(31, 15);
            this.lbnIP.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbnIP.Name = "lbnIP";
            this.lbnIP.Size = new System.Drawing.Size(26, 13);
            this.lbnIP.TabIndex = 140;
            this.lbnIP.Text = "IP : ";
            // 
            // lbnDevice
            // 
            this.lbnDevice.AutoSize = true;
            this.lbnDevice.BackColor = System.Drawing.Color.Transparent;
            this.lbnDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnDevice.ForeColor = System.Drawing.Color.White;
            this.lbnDevice.Location = new System.Drawing.Point(31, 3);
            this.lbnDevice.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbnDevice.Name = "lbnDevice";
            this.lbnDevice.Size = new System.Drawing.Size(50, 13);
            this.lbnDevice.TabIndex = 140;
            this.lbnDevice.Text = "LDPlayer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(31)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 138;
            this.label2.Text = "X";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbnTong
            // 
            this.lbnTong.AutoSize = true;
            this.lbnTong.ForeColor = System.Drawing.Color.White;
            this.lbnTong.Location = new System.Drawing.Point(20, 26);
            this.lbnTong.Name = "lbnTong";
            this.lbnTong.Size = new System.Drawing.Size(13, 13);
            this.lbnTong.TabIndex = 141;
            this.lbnTong.Text = "0";
            // 
            // lbnThanhCong
            // 
            this.lbnThanhCong.AutoSize = true;
            this.lbnThanhCong.ForeColor = System.Drawing.Color.White;
            this.lbnThanhCong.Location = new System.Drawing.Point(1, 26);
            this.lbnThanhCong.Name = "lbnThanhCong";
            this.lbnThanhCong.Size = new System.Drawing.Size(13, 13);
            this.lbnThanhCong.TabIndex = 141;
            this.lbnThanhCong.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 141;
            this.label1.Text = "/";
            // 
            // pibStatus
            // 
            this.pibStatus.BackColor = System.Drawing.SystemColors.Desktop;
            this.pibStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pibStatus.Location = new System.Drawing.Point(0, 0);
            this.pibStatus.Name = "pibStatus";
            this.pibStatus.Size = new System.Drawing.Size(302, 526);
            this.pibStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibStatus.TabIndex = 137;
            this.pibStatus.TabStop = false;
            // 
            // userLD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = global::NinjaSystem.Properties.Resources.ninjasystemv4chuan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pibStatus);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "userLD";
            this.Size = new System.Drawing.Size(302, 526);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pibStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbnStatus;
        private System.Windows.Forms.Label lbnDevice;
        private System.Windows.Forms.Label lbnThanhCong;
        private System.Windows.Forms.Label lbnIP;
        private System.Windows.Forms.Label lbnTong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
