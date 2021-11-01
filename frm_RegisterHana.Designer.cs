namespace NinjaSystem
{
    partial class frm_RegisterHana
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RegisterHana));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnThoat = new Bunifu.Framework.UI.BunifuFlatButton();
            this.txtPass = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.txtRePass = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txtUID = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTao = new Bunifu.Framework.UI.BunifuFlatButton();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mật khẩu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nhập lại mật khẩu";
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
            this.btnThoat.Location = new System.Drawing.Point(246, 211);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Normalcolor = System.Drawing.Color.Red;
            this.btnThoat.OnHovercolor = System.Drawing.Color.Red;
            this.btnThoat.OnHoverTextColor = System.Drawing.Color.White;
            this.btnThoat.selected = false;
            this.btnThoat.Size = new System.Drawing.Size(87, 30);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "Hủy";
            this.btnThoat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnThoat.Textcolor = System.Drawing.Color.White;
            this.btnThoat.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
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
            this.txtPass.isPassword = true;
            this.txtPass.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtPass.LineIdleColor = System.Drawing.Color.Silver;
            this.txtPass.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtPass.LineThickness = 1;
            this.txtPass.Location = new System.Drawing.Point(136, 98);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtPass.MaxLength = 32767;
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(237, 29);
            this.txtPass.TabIndex = 11;
            this.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileDialog1";
            // 
            // txtRePass
            // 
            this.txtRePass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtRePass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtRePass.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtRePass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRePass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtRePass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRePass.HintForeColor = System.Drawing.Color.Empty;
            this.txtRePass.HintText = "";
            this.txtRePass.isPassword = true;
            this.txtRePass.LineFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtRePass.LineIdleColor = System.Drawing.Color.Silver;
            this.txtRePass.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtRePass.LineThickness = 1;
            this.txtRePass.Location = new System.Drawing.Point(136, 141);
            this.txtRePass.Margin = new System.Windows.Forms.Padding(4);
            this.txtRePass.MaxLength = 32767;
            this.txtRePass.Name = "txtRePass";
            this.txtRePass.Size = new System.Drawing.Size(237, 29);
            this.txtRePass.TabIndex = 12;
            this.txtRePass.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.txtUID.Location = new System.Drawing.Point(136, 62);
            this.txtUID.Margin = new System.Windows.Forms.Padding(4);
            this.txtUID.MaxLength = 32767;
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(237, 29);
            this.txtUID.TabIndex = 18;
            this.txtUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Tên đăng nhập";
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
            this.btnTao.Location = new System.Drawing.Point(137, 211);
            this.btnTao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTao.Name = "btnTao";
            this.btnTao.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnTao.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnTao.OnHoverTextColor = System.Drawing.Color.White;
            this.btnTao.selected = false;
            this.btnTao.Size = new System.Drawing.Size(101, 30);
            this.btnTao.TabIndex = 9;
            this.btnTao.Text = "Thêm";
            this.btnTao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTao.Textcolor = System.Drawing.Color.White;
            this.btnTao.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTao.Click += new System.EventHandler(this.btnTao_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(11, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "Đăng ký kiếm tiền";
            // 
            // frm_RegisterHana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(413, 263);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRePass);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnTao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frm_RegisterHana";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmAddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuFlatButton btnTao;
        private Bunifu.Framework.UI.BunifuFlatButton btnThoat;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtPass;
        private System.Windows.Forms.OpenFileDialog openFile;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtRePass;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtUID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
    }
}