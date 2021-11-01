namespace NinjaSystem
{
    partial class frm_confirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_confirm));
            this.lblFirst = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnOK = new Bunifu.Framework.UI.BunifuFlatButton();
            this.chkHideShow = new System.Windows.Forms.CheckBox();
            this.lblSecond = new System.Windows.Forms.Label();
            this.chkHideEmail = new System.Windows.Forms.CheckBox();
            this.chkHideUID = new System.Windows.Forms.CheckBox();
            this.chkPrivate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblFirst
            // 
            this.lblFirst.AutoSize = true;
            this.lblFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirst.Location = new System.Drawing.Point(12, 242);
            this.lblFirst.Name = "lblFirst";
            this.lblFirst.Size = new System.Drawing.Size(349, 25);
            this.lblFirst.TabIndex = 0;
            this.lblFirst.Text = "Lần đầu, hãy đặt mật mã của riêng bạn";
            this.lblFirst.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(17, 281);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(179, 26);
            this.txtPass.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Active = false;
            this.btnOK.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.BorderRadius = 0;
            this.btnOK.ButtonText = "Đồng ý";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DisabledColor = System.Drawing.Color.Gray;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Iconcolor = System.Drawing.Color.Transparent;
            this.btnOK.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnOK.Iconimage")));
            this.btnOK.Iconimage_right = null;
            this.btnOK.Iconimage_right_Selected = null;
            this.btnOK.Iconimage_Selected = null;
            this.btnOK.IconMarginLeft = 0;
            this.btnOK.IconMarginRight = 0;
            this.btnOK.IconRightVisible = true;
            this.btnOK.IconRightZoom = 0D;
            this.btnOK.IconVisible = true;
            this.btnOK.IconZoom = 90D;
            this.btnOK.IsTab = false;
            this.btnOK.Location = new System.Drawing.Point(17, 315);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnOK.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnOK.OnHoverTextColor = System.Drawing.Color.White;
            this.btnOK.selected = false;
            this.btnOK.Size = new System.Drawing.Size(179, 46);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.Textcolor = System.Drawing.Color.White;
            this.btnOK.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkHideShow
            // 
            this.chkHideShow.AutoSize = true;
            this.chkHideShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHideShow.Location = new System.Drawing.Point(17, 22);
            this.chkHideShow.Name = "chkHideShow";
            this.chkHideShow.Size = new System.Drawing.Size(228, 29);
            this.chkHideShow.TabIndex = 16;
            this.chkHideShow.Text = "Ẩn /hiện cột Mật khẩu";
            this.chkHideShow.UseVisualStyleBackColor = true;
            // 
            // lblSecond
            // 
            this.lblSecond.AutoSize = true;
            this.lblSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecond.Location = new System.Drawing.Point(12, 240);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(165, 25);
            this.lblSecond.TabIndex = 17;
            this.lblSecond.Text = "Hãy nhập mật mã";
            this.lblSecond.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkHideEmail
            // 
            this.chkHideEmail.AutoSize = true;
            this.chkHideEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHideEmail.Location = new System.Drawing.Point(17, 57);
            this.chkHideEmail.Name = "chkHideEmail";
            this.chkHideEmail.Size = new System.Drawing.Size(195, 29);
            this.chkHideEmail.TabIndex = 18;
            this.chkHideEmail.Text = "Ẩn /hiện cột Email";
            this.chkHideEmail.UseVisualStyleBackColor = true;
            // 
            // chkHideUID
            // 
            this.chkHideUID.AutoSize = true;
            this.chkHideUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHideUID.Location = new System.Drawing.Point(17, 92);
            this.chkHideUID.Name = "chkHideUID";
            this.chkHideUID.Size = new System.Drawing.Size(180, 29);
            this.chkHideUID.TabIndex = 19;
            this.chkHideUID.Text = "Ẩn /hiện cột UID";
            this.chkHideUID.UseVisualStyleBackColor = true;
            // 
            // chkPrivate
            // 
            this.chkPrivate.AutoSize = true;
            this.chkPrivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrivate.Location = new System.Drawing.Point(17, 136);
            this.chkPrivate.Name = "chkPrivate";
            this.chkPrivate.Size = new System.Drawing.Size(243, 29);
            this.chkPrivate.TabIndex = 20;
            this.chkPrivate.Text = "Ẩn /hiện cột Private key";
            this.chkPrivate.UseVisualStyleBackColor = true;
            // 
            // frm_confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 379);
            this.Controls.Add(this.chkPrivate);
            this.Controls.Add(this.chkHideUID);
            this.Controls.Add(this.chkHideEmail);
            this.Controls.Add(this.lblSecond);
            this.Controls.Add(this.chkHideShow);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.lblFirst);
            this.Name = "frm_confirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frm_confirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFirst;
        private System.Windows.Forms.TextBox txtPass;
        private Bunifu.Framework.UI.BunifuFlatButton btnOK;
        private System.Windows.Forms.CheckBox chkHideShow;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.CheckBox chkHideEmail;
        private System.Windows.Forms.CheckBox chkHideUID;
        private System.Windows.Forms.CheckBox chkPrivate;
    }
}