namespace NinjaSystem
{
    partial class SyncAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncAccount));
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new Bunifu.Framework.UI.BunifuFlatButton();
            this.txtPathAdd = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.button1 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnCancel = new Bunifu.Framework.UI.BunifuFlatButton();
            this.rdoNinjaCare = new System.Windows.Forms.RadioButton();
            this.rdoVersion = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lbLink = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đường dẫn Database:";
            // 
            // btnOpen
            // 
            this.btnOpen.Active = true;
            this.btnOpen.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpen.BorderRadius = 0;
            this.btnOpen.ButtonText = "Open";
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.DisabledColor = System.Drawing.Color.Gray;
            this.btnOpen.Iconcolor = System.Drawing.Color.Transparent;
            this.btnOpen.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnOpen.Iconimage")));
            this.btnOpen.Iconimage_right = null;
            this.btnOpen.Iconimage_right_Selected = null;
            this.btnOpen.Iconimage_Selected = null;
            this.btnOpen.IconMarginLeft = 0;
            this.btnOpen.IconMarginRight = 0;
            this.btnOpen.IconRightVisible = true;
            this.btnOpen.IconRightZoom = 0D;
            this.btnOpen.IconVisible = true;
            this.btnOpen.IconZoom = 40D;
            this.btnOpen.IsTab = false;
            this.btnOpen.Location = new System.Drawing.Point(652, 99);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnOpen.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnOpen.OnHoverTextColor = System.Drawing.Color.White;
            this.btnOpen.selected = true;
            this.btnOpen.Size = new System.Drawing.Size(152, 46);
            this.btnOpen.TabIndex = 146;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOpen.Textcolor = System.Drawing.Color.White;
            this.btnOpen.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtPathAdd
            // 
            this.txtPathAdd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPathAdd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPathAdd.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPathAdd.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPathAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPathAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPathAdd.HintForeColor = System.Drawing.Color.Empty;
            this.txtPathAdd.HintText = "";
            this.txtPathAdd.isPassword = false;
            this.txtPathAdd.LineFocusedColor = System.Drawing.Color.Blue;
            this.txtPathAdd.LineIdleColor = System.Drawing.Color.Gray;
            this.txtPathAdd.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txtPathAdd.LineThickness = 1;
            this.txtPathAdd.Location = new System.Drawing.Point(176, 100);
            this.txtPathAdd.Margin = new System.Windows.Forms.Padding(6);
            this.txtPathAdd.MaxLength = 32767;
            this.txtPathAdd.Name = "txtPathAdd";
            this.txtPathAdd.Size = new System.Drawing.Size(464, 37);
            this.txtPathAdd.TabIndex = 145;
            this.txtPathAdd.Text = "Chọn đường dẫn data mới";
            this.txtPathAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPathAdd.OnValueChanged += new System.EventHandler(this.txtPathAdd_OnValueChanged);
            // 
            // button1
            // 
            this.button1.Active = false;
            this.button1.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.BorderRadius = 0;
            this.button1.ButtonText = "Chuyển database";
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DisabledColor = System.Drawing.Color.Gray;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Iconcolor = System.Drawing.Color.Transparent;
            this.button1.Iconimage = ((System.Drawing.Image)(resources.GetObject("button1.Iconimage")));
            this.button1.Iconimage_right = null;
            this.button1.Iconimage_right_Selected = null;
            this.button1.Iconimage_Selected = null;
            this.button1.IconMarginLeft = 0;
            this.button1.IconMarginRight = 0;
            this.button1.IconRightVisible = true;
            this.button1.IconRightZoom = 0D;
            this.button1.IconVisible = true;
            this.button1.IconZoom = 90D;
            this.button1.IsTab = false;
            this.button1.Location = new System.Drawing.Point(176, 232);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button1.Name = "button1";
            this.button1.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.button1.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.button1.OnHoverTextColor = System.Drawing.Color.White;
            this.button1.selected = false;
            this.button1.Size = new System.Drawing.Size(276, 46);
            this.button1.TabIndex = 147;
            this.button1.Text = "Chuyển database";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.Textcolor = System.Drawing.Color.White;
            this.button1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Active = false;
            this.btnCancel.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.BorderRadius = 0;
            this.btnCancel.ButtonText = "Đóng";
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DisabledColor = System.Drawing.Color.Gray;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Iconcolor = System.Drawing.Color.Transparent;
            this.btnCancel.Iconimage = null;
            this.btnCancel.Iconimage_right = null;
            this.btnCancel.Iconimage_right_Selected = null;
            this.btnCancel.Iconimage_Selected = null;
            this.btnCancel.IconMarginLeft = 0;
            this.btnCancel.IconMarginRight = 0;
            this.btnCancel.IconRightVisible = true;
            this.btnCancel.IconRightZoom = 0D;
            this.btnCancel.IconVisible = true;
            this.btnCancel.IconZoom = 90D;
            this.btnCancel.IsTab = false;
            this.btnCancel.Location = new System.Drawing.Point(492, 232);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCancel.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnCancel.OnHoverTextColor = System.Drawing.Color.White;
            this.btnCancel.selected = false;
            this.btnCancel.Size = new System.Drawing.Size(130, 46);
            this.btnCancel.TabIndex = 148;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.Textcolor = System.Drawing.Color.White;
            this.btnCancel.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdoNinjaCare
            // 
            this.rdoNinjaCare.AutoSize = true;
            this.rdoNinjaCare.Checked = true;
            this.rdoNinjaCare.Location = new System.Drawing.Point(176, 144);
            this.rdoNinjaCare.Name = "rdoNinjaCare";
            this.rdoNinjaCare.Size = new System.Drawing.Size(195, 24);
            this.rdoNinjaCare.TabIndex = 151;
            this.rdoNinjaCare.TabStop = true;
            this.rdoNinjaCare.Text = "Đồng bộ với Ninja Care";
            this.rdoNinjaCare.UseVisualStyleBackColor = true;
            this.rdoNinjaCare.Visible = false;
            // 
            // rdoVersion
            // 
            this.rdoVersion.AutoSize = true;
            this.rdoVersion.Location = new System.Drawing.Point(176, 193);
            this.rdoVersion.Name = "rdoVersion";
            this.rdoVersion.Size = new System.Drawing.Size(314, 24);
            this.rdoVersion.TabIndex = 152;
            this.rdoVersion.Text = "Đồng bộ với phiên bản Ninja System 2.7";
            this.rdoVersion.UseVisualStyleBackColor = true;
            this.rdoVersion.Visible = false;
            this.rdoVersion.CheckedChanged += new System.EventHandler(this.rdoVersion_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 153;
            this.label2.Text = "Database hiện tại:";
            // 
            // lbLink
            // 
            this.lbLink.AutoSize = true;
            this.lbLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLink.Location = new System.Drawing.Point(172, 29);
            this.lbLink.Name = "lbLink";
            this.lbLink.Size = new System.Drawing.Size(0, 20);
            this.lbLink.TabIndex = 154;
            // 
            // SyncAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 312);
            this.Controls.Add(this.lbLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rdoVersion);
            this.Controls.Add(this.rdoNinjaCare);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtPathAdd);
            this.Controls.Add(this.label1);
            this.Name = "SyncAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.SyncAccount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuFlatButton btnOpen;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtPathAdd;
        private Bunifu.Framework.UI.BunifuFlatButton button1;
        private Bunifu.Framework.UI.BunifuFlatButton btnCancel;
        private System.Windows.Forms.RadioButton rdoNinjaCare;
        private System.Windows.Forms.RadioButton rdoVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbLink;
    }
}