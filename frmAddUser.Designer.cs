namespace NinjaSystem
{
    partial class frmAddUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddUser));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboNhom = new System.Windows.Forms.ComboBox();
            this.btnThoat = new Bunifu.Framework.UI.BunifuFlatButton();
            this.txtEmail = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.btnTao = new Bunifu.Framework.UI.BunifuFlatButton();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPass = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txtPrivatekey = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txtUID = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtToken = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCookie = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBirthday = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nhóm người dùng";
            // 
            // cboNhom
            // 
            this.cboNhom.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNhom.FormattingEnabled = true;
            this.cboNhom.Location = new System.Drawing.Point(141, 24);
            this.cboNhom.Name = "cboNhom";
            this.cboNhom.Size = new System.Drawing.Size(211, 24);
            this.cboNhom.TabIndex = 8;
            // 
            // btnThoat
            // 
            this.btnThoat.Active = false;
            this.btnThoat.Activecolor = System.Drawing.Color.Red;
            this.btnThoat.BackColor = System.Drawing.Color.Red;
            this.btnThoat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnThoat.BorderRadius = 0;
            this.btnThoat.ButtonText = "Hủy";
            this.btnThoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThoat.DisabledColor = System.Drawing.Color.Gray;
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Iconcolor = System.Drawing.Color.Transparent;
            this.btnThoat.Iconimage = null;
            this.btnThoat.Iconimage_right = null;
            this.btnThoat.Iconimage_right_Selected = null;
            this.btnThoat.Iconimage_Selected = null;
            this.btnThoat.IconMarginLeft = 0;
            this.btnThoat.IconMarginRight = 0;
            this.btnThoat.IconRightVisible = true;
            this.btnThoat.IconRightZoom = 0D;
            this.btnThoat.IconVisible = true;
            this.btnThoat.IconZoom = 90D;
            this.btnThoat.IsTab = false;
            this.btnThoat.Location = new System.Drawing.Point(196, 346);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Normalcolor = System.Drawing.Color.Red;
            this.btnThoat.OnHovercolor = System.Drawing.Color.Red;
            this.btnThoat.OnHoverTextColor = System.Drawing.Color.White;
            this.btnThoat.selected = false;
            this.btnThoat.Size = new System.Drawing.Size(132, 30);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "Hủy";
            this.btnThoat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnThoat.Textcolor = System.Drawing.Color.White;
            this.btnThoat.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtEmail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtEmail.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmail.HintForeColor = System.Drawing.Color.Empty;
            this.txtEmail.HintText = "";
            this.txtEmail.isPassword = false;
            this.txtEmail.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtEmail.LineIdleColor = System.Drawing.Color.Silver;
            this.txtEmail.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtEmail.LineThickness = 1;
            this.txtEmail.Location = new System.Drawing.Point(117, 98);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmail.MaxLength = 32767;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(242, 29);
            this.txtEmail.TabIndex = 11;
            this.txtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnTao
            // 
            this.btnTao.Active = false;
            this.btnTao.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnTao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnTao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTao.BorderRadius = 0;
            this.btnTao.ButtonText = "Thêm";
            this.btnTao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTao.DisabledColor = System.Drawing.Color.Gray;
            this.btnTao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTao.Iconcolor = System.Drawing.Color.Transparent;
            this.btnTao.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnTao.Iconimage")));
            this.btnTao.Iconimage_right = null;
            this.btnTao.Iconimage_right_Selected = null;
            this.btnTao.Iconimage_Selected = null;
            this.btnTao.IconMarginLeft = 0;
            this.btnTao.IconMarginRight = 0;
            this.btnTao.IconRightVisible = true;
            this.btnTao.IconRightZoom = 0D;
            this.btnTao.IconVisible = true;
            this.btnTao.IconZoom = 90D;
            this.btnTao.IsTab = false;
            this.btnTao.Location = new System.Drawing.Point(28, 346);
            this.btnTao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTao.Name = "btnTao";
            this.btnTao.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnTao.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnTao.OnHoverTextColor = System.Drawing.Color.White;
            this.btnTao.selected = false;
            this.btnTao.Size = new System.Drawing.Size(148, 30);
            this.btnTao.TabIndex = 9;
            this.btnTao.Text = "Thêm";
            this.btnTao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTao.Textcolor = System.Drawing.Color.White;
            this.btnTao.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTao.Click += new System.EventHandler(this.btnTao_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Private key";
            // 
            // txtPass
            // 
            this.txtPass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPass.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPass.HintForeColor = System.Drawing.Color.Empty;
            this.txtPass.HintText = "";
            this.txtPass.isPassword = false;
            this.txtPass.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtPass.LineIdleColor = System.Drawing.Color.Silver;
            this.txtPass.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtPass.LineThickness = 1;
            this.txtPass.Location = new System.Drawing.Point(117, 141);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPass.MaxLength = 32767;
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(242, 29);
            this.txtPass.TabIndex = 12;
            this.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtPrivatekey
            // 
            this.txtPrivatekey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPrivatekey.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPrivatekey.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPrivatekey.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrivatekey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPrivatekey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPrivatekey.HintForeColor = System.Drawing.Color.Empty;
            this.txtPrivatekey.HintText = "";
            this.txtPrivatekey.isPassword = false;
            this.txtPrivatekey.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtPrivatekey.LineIdleColor = System.Drawing.Color.Silver;
            this.txtPrivatekey.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtPrivatekey.LineThickness = 1;
            this.txtPrivatekey.Location = new System.Drawing.Point(119, 182);
            this.txtPrivatekey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPrivatekey.MaxLength = 32767;
            this.txtPrivatekey.Name = "txtPrivatekey";
            this.txtPrivatekey.Size = new System.Drawing.Size(242, 29);
            this.txtPrivatekey.TabIndex = 16;
            this.txtPrivatekey.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtUID
            // 
            this.txtUID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtUID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtUID.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtUID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtUID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUID.HintForeColor = System.Drawing.Color.Empty;
            this.txtUID.HintText = "";
            this.txtUID.isPassword = false;
            this.txtUID.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtUID.LineIdleColor = System.Drawing.Color.Silver;
            this.txtUID.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtUID.LineThickness = 1;
            this.txtUID.Location = new System.Drawing.Point(117, 62);
            this.txtUID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUID.MaxLength = 32767;
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(242, 29);
            this.txtUID.TabIndex = 18;
            this.txtUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(36, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "UID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(38, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Token";
            // 
            // txtToken
            // 
            this.txtToken.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtToken.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtToken.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtToken.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtToken.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtToken.HintForeColor = System.Drawing.Color.Empty;
            this.txtToken.HintText = "";
            this.txtToken.isPassword = false;
            this.txtToken.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtToken.LineIdleColor = System.Drawing.Color.Silver;
            this.txtToken.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtToken.LineThickness = 1;
            this.txtToken.Location = new System.Drawing.Point(119, 223);
            this.txtToken.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtToken.MaxLength = 32767;
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(242, 29);
            this.txtToken.TabIndex = 12;
            this.txtToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(38, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Cookie";
            // 
            // txtCookie
            // 
            this.txtCookie.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtCookie.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtCookie.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtCookie.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCookie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtCookie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCookie.HintForeColor = System.Drawing.Color.Empty;
            this.txtCookie.HintText = "";
            this.txtCookie.isPassword = false;
            this.txtCookie.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtCookie.LineIdleColor = System.Drawing.Color.Silver;
            this.txtCookie.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtCookie.LineThickness = 1;
            this.txtCookie.Location = new System.Drawing.Point(122, 264);
            this.txtCookie.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCookie.MaxLength = 32767;
            this.txtCookie.Name = "txtCookie";
            this.txtCookie.Size = new System.Drawing.Size(242, 29);
            this.txtCookie.TabIndex = 16;
            this.txtCookie.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(38, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "Birthday";
            // 
            // txtBirthday
            // 
            this.txtBirthday.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBirthday.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBirthday.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtBirthday.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBirthday.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtBirthday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBirthday.HintForeColor = System.Drawing.Color.Empty;
            this.txtBirthday.HintText = "";
            this.txtBirthday.isPassword = false;
            this.txtBirthday.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtBirthday.LineIdleColor = System.Drawing.Color.Silver;
            this.txtBirthday.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtBirthday.LineThickness = 1;
            this.txtBirthday.Location = new System.Drawing.Point(122, 301);
            this.txtBirthday.Margin = new System.Windows.Forms.Padding(4);
            this.txtBirthday.MaxLength = 32767;
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.Size = new System.Drawing.Size(242, 29);
            this.txtBirthday.TabIndex = 16;
            this.txtBirthday.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // frmAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(413, 448);
            this.ControlBox = false;
            this.Controls.Add(this.txtUID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBirthday);
            this.Controls.Add(this.txtCookie);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPrivatekey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnTao);
            this.Controls.Add(this.cboNhom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmAddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmAddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboNhom;
        private Bunifu.Framework.UI.BunifuFlatButton btnTao;
        private Bunifu.Framework.UI.BunifuFlatButton btnThoat;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtEmail;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Label label4;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtPass;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtPrivatekey;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtUID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtToken;
        private System.Windows.Forms.Label label7;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtCookie;
        private System.Windows.Forms.Label label8;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtBirthday;
    }
}