namespace NinjaSystem
{
    partial class runLDs
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
            this.pnLD = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pnLD
            // 
            this.pnLD.AutoScroll = true;
            this.pnLD.BackColor = System.Drawing.Color.Black;
            this.pnLD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLD.Location = new System.Drawing.Point(0, 0);
            this.pnLD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnLD.Name = "pnLD";
            this.pnLD.Size = new System.Drawing.Size(2648, 1329);
            this.pnLD.TabIndex = 1;
            // 
            // runLDs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(2648, 1329);
            this.Controls.Add(this.pnLD);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "runLDs";
            this.Text = "LƯU Ý KHÔNG DI CHUYỂN LD KHỎI VỊ TRÍ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel pnLD;

    }
}