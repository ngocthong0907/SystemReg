namespace NinjaSystem
{
    partial class frm_2Fa_PRO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_2Fa_PRO));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDeleteDevices = new System.Windows.Forms.CheckBox();
            this.lbAddress = new System.Windows.Forms.Label();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.chkTwoFA = new System.Windows.Forms.CheckBox();
            this.chkLogout = new System.Windows.Forms.CheckBox();
            this.pibStatus = new System.Windows.Forms.PictureBox();
            this.button1 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnInput = new Bunifu.Framework.UI.BunifuFlatButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richLogs = new System.Windows.Forms.RichTextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.chkDeleteDevices);
            this.panel1.Controls.Add(this.lbAddress);
            this.panel1.Controls.Add(this.chkEmail);
            this.panel1.Controls.Add(this.chkTwoFA);
            this.panel1.Controls.Add(this.chkLogout);
            this.panel1.Controls.Add(this.pibStatus);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(955, 92);
            this.panel1.TabIndex = 1;
            // 
            // chkDeleteDevices
            // 
            this.chkDeleteDevices.AutoSize = true;
            this.chkDeleteDevices.Location = new System.Drawing.Point(13, 37);
            this.chkDeleteDevices.Margin = new System.Windows.Forms.Padding(2);
            this.chkDeleteDevices.Name = "chkDeleteDevices";
            this.chkDeleteDevices.Size = new System.Drawing.Size(171, 17);
            this.chkDeleteDevices.TabIndex = 156;
            this.chkDeleteDevices.Text = "Xóa các thiết bị đã đăng nhập";
            this.chkDeleteDevices.UseVisualStyleBackColor = true;
            // 
            // lbAddress
            // 
            this.lbAddress.AutoSize = true;
            this.lbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAddress.Location = new System.Drawing.Point(143, 11);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(0, 15);
            this.lbAddress.TabIndex = 155;
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Location = new System.Drawing.Point(145, 11);
            this.chkEmail.Margin = new System.Windows.Forms.Padding(2);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(120, 17);
            this.chkEmail.TabIndex = 154;
            this.chkEmail.Text = "Thêm Email 10 phút";
            this.chkEmail.UseVisualStyleBackColor = true;
            this.chkEmail.Visible = false;
            // 
            // chkTwoFA
            // 
            this.chkTwoFA.AutoSize = true;
            this.chkTwoFA.Checked = true;
            this.chkTwoFA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTwoFA.Location = new System.Drawing.Point(13, 11);
            this.chkTwoFA.Margin = new System.Windows.Forms.Padding(2);
            this.chkTwoFA.Name = "chkTwoFA";
            this.chkTwoFA.Size = new System.Drawing.Size(109, 17);
            this.chkTwoFA.TabIndex = 154;
            this.chkTwoFA.Text = "Bật bảo mật 2 lớp";
            this.chkTwoFA.UseVisualStyleBackColor = true;
            // 
            // chkLogout
            // 
            this.chkLogout.AutoSize = true;
            this.chkLogout.Location = new System.Drawing.Point(885, 20);
            this.chkLogout.Margin = new System.Windows.Forms.Padding(2);
            this.chkLogout.Name = "chkLogout";
            this.chkLogout.Size = new System.Drawing.Size(59, 17);
            this.chkLogout.TabIndex = 154;
            this.chkLogout.Text = "Logout";
            this.chkLogout.UseVisualStyleBackColor = true;
            this.chkLogout.Visible = false;
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(311, 73);
            this.pibStatus.Name = "pibStatus";
            this.pibStatus.Size = new System.Drawing.Size(97, 10);
            this.pibStatus.TabIndex = 136;
            this.pibStatus.TabStop = false;
            this.pibStatus.Visible = false;
            // 
            // button1
            // 
            this.button1.Active = false;
            this.button1.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.BorderRadius = 0;
            this.button1.ButtonText = "                  Chạy ";
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
            this.button1.Location = new System.Drawing.Point(13, 59);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.button1.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.button1.OnHoverTextColor = System.Drawing.Color.White;
            this.button1.selected = false;
            this.button1.Size = new System.Drawing.Size(138, 27);
            this.button1.TabIndex = 135;
            this.button1.Text = "                  Chạy ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Textcolor = System.Drawing.Color.White;
            this.button1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInput
            // 
            this.btnInput.Active = false;
            this.btnInput.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInput.BorderRadius = 0;
            this.btnInput.ButtonText = "Dừng";
            this.btnInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInput.DisabledColor = System.Drawing.Color.Gray;
            this.btnInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInput.Iconcolor = System.Drawing.Color.Transparent;
            this.btnInput.Iconimage = null;
            this.btnInput.Iconimage_right = null;
            this.btnInput.Iconimage_right_Selected = null;
            this.btnInput.Iconimage_Selected = null;
            this.btnInput.IconMarginLeft = 0;
            this.btnInput.IconMarginRight = 0;
            this.btnInput.IconRightVisible = true;
            this.btnInput.IconRightZoom = 0D;
            this.btnInput.IconVisible = true;
            this.btnInput.IconZoom = 90D;
            this.btnInput.IsTab = false;
            this.btnInput.Location = new System.Drawing.Point(196, 56);
            this.btnInput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnInput.Name = "btnInput";
            this.btnInput.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnInput.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnInput.OnHoverTextColor = System.Drawing.Color.White;
            this.btnInput.selected = false;
            this.btnInput.Size = new System.Drawing.Size(87, 30);
            this.btnInput.TabIndex = 33;
            this.btnInput.Text = "Dừng";
            this.btnInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInput.Textcolor = System.Drawing.Color.White;
            this.btnInput.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richLogs);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.dgvUser);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(955, 416);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách tài khoản";
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(3, 350);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(949, 63);
            this.richLogs.TabIndex = 136;
            this.richLogs.Text = "Hướng dẫn bật bảo mật 2 lớp : https://www.youtube.com/watch?v=gcbYvvF7LCo";
            this.richLogs.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richLogs_LinkClicked);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(13, 21);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 135;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToResizeRows = false;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.UID,
            this.Ten,
            this.Password,
            this.clName,
            this.clID,
            this.clstatus,
            this.Message});
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.Location = new System.Drawing.Point(3, 16);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(949, 397);
            this.dgvUser.TabIndex = 4;
            this.dgvUser.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUser_CellMouseDown);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "STT";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 40;
            // 
            // UID
            // 
            this.UID.HeaderText = "UID";
            this.UID.Name = "UID";
            this.UID.Width = 150;
            // 
            // Ten
            // 
            this.Ten.HeaderText = "Name";
            this.Ten.Name = "Ten";
            // 
            // Password
            // 
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            this.Password.Width = 80;
            // 
            // clName
            // 
            this.clName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clName.HeaderText = "Private Key";
            this.clName.Name = "clName";
            this.clName.Width = 120;
            // 
            // clID
            // 
            this.clID.HeaderText = "LdPlayer ID";
            this.clID.Name = "clID";
            // 
            // clstatus
            // 
            this.clstatus.HeaderText = "Status";
            this.clstatus.Name = "clstatus";
            // 
            // Message
            // 
            this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            // 
            // frm_2Fa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 508);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_2Fa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bật bảo mật 2 lớp";
            this.Load += new System.EventHandler(this.frm_2Fa_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuFlatButton btnInput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.DataGridView dgvUser;
        private Bunifu.Framework.UI.BunifuFlatButton button1;
        private System.Windows.Forms.PictureBox pibStatus;
        private System.Windows.Forms.RichTextBox richLogs;
        private System.Windows.Forms.CheckBox chkLogout;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.CheckBox chkTwoFA;
        private System.Windows.Forms.CheckBox chkDeleteDevices;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ten;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
    }
}