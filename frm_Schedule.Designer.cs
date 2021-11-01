namespace NinjaSystem
{
    partial class frm_Schedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Schedule));
            this.label3 = new System.Windows.Forms.Label();
            this.cboConfig = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHouse = new System.Windows.Forms.RichTextBox();
            this.dtpHour = new System.Windows.Forms.DateTimePicker();
            this.txtAccounts = new System.Windows.Forms.RichTextBox();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.todate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.config = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnSelectAcc = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnPathReaction = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnUpdate = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnXoa = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_addUser = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_save = new Bunifu.Framework.UI.BunifuImageButton();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnXoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_addUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_save)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(300, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Cấu hình";
            this.label3.Visible = false;
            // 
            // cboConfig
            // 
            this.cboConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboConfig.FormattingEnabled = true;
            this.cboConfig.Location = new System.Drawing.Point(361, 59);
            this.cboConfig.Margin = new System.Windows.Forms.Padding(2);
            this.cboConfig.MaxDropDownItems = 20;
            this.cboConfig.Name = "cboConfig";
            this.cboConfig.Size = new System.Drawing.Size(189, 23);
            this.cboConfig.TabIndex = 21;
            this.cboConfig.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Từ ngày";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(205, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Đến ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(88, 114);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowUpDown = true;
            this.dtpFromDate.Size = new System.Drawing.Size(89, 20);
            this.dtpFromDate.TabIndex = 155;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(283, 116);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.ShowUpDown = true;
            this.dtpToDate.Size = new System.Drawing.Size(89, 20);
            this.dtpToDate.TabIndex = 156;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 157;
            this.label4.Text = "Thời gian";
            // 
            // txtHouse
            // 
            this.txtHouse.Location = new System.Drawing.Point(192, 164);
            this.txtHouse.Margin = new System.Windows.Forms.Padding(2);
            this.txtHouse.Name = "txtHouse";
            this.txtHouse.Size = new System.Drawing.Size(164, 96);
            this.txtHouse.TabIndex = 158;
            this.txtHouse.Text = "";
            // 
            // dtpHour
            // 
            this.dtpHour.CustomFormat = "HH:mm";
            this.dtpHour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHour.Location = new System.Drawing.Point(88, 165);
            this.dtpHour.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHour.Name = "dtpHour";
            this.dtpHour.ShowUpDown = true;
            this.dtpHour.Size = new System.Drawing.Size(89, 20);
            this.dtpHour.TabIndex = 160;
            // 
            // txtAccounts
            // 
            this.txtAccounts.Location = new System.Drawing.Point(630, 24);
            this.txtAccounts.Margin = new System.Windows.Forms.Padding(2);
            this.txtAccounts.Name = "txtAccounts";
            this.txtAccounts.Size = new System.Drawing.Size(194, 236);
            this.txtAccounts.TabIndex = 162;
            this.txtAccounts.Text = "";
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.AllowUserToAddRows = false;
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.fromdate,
            this.todate,
            this.config,
            this.hours,
            this.Account,
            this.id});
            this.dgvSchedule.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvSchedule.Location = new System.Drawing.Point(0, 266);
            this.dgvSchedule.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.RowTemplate.Height = 28;
            this.dgvSchedule.Size = new System.Drawing.Size(824, 179);
            this.dgvSchedule.TabIndex = 163;
            this.dgvSchedule.SelectionChanged += new System.EventHandler(this.dgvSchedule_SelectionChanged);
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.Width = 50;
            // 
            // fromdate
            // 
            this.fromdate.HeaderText = "Từ ngày";
            this.fromdate.Name = "fromdate";
            this.fromdate.Width = 120;
            // 
            // todate
            // 
            this.todate.HeaderText = "Đến ngày";
            this.todate.Name = "todate";
            this.todate.Width = 120;
            // 
            // config
            // 
            this.config.HeaderText = "Lập lịch";
            this.config.Name = "config";
            this.config.Visible = false;
            this.config.Width = 120;
            // 
            // hours
            // 
            this.hours.HeaderText = "Giờ";
            this.hours.Name = "hours";
            // 
            // Account
            // 
            this.Account.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Account.HeaderText = "Tài khoản đã chọn";
            this.Account.Name = "Account";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 165;
            this.label5.Text = "Tùy chọn";
            this.label5.Visible = false;
            // 
            // cboType
            // 
            this.cboType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "Nhắn tin cho bạn bè",
            "Kết bạn theo số điện thoại",
            "Đăng bài nhanh vào profile",
            "Chạy tương tác",
            "Tương tác nhóm, OA"});
            this.cboType.Location = new System.Drawing.Point(97, 67);
            this.cboType.Margin = new System.Windows.Forms.Padding(2);
            this.cboType.MaxDropDownItems = 20;
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(189, 23);
            this.cboType.TabIndex = 166;
            this.cboType.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(302, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(248, 15);
            this.label6.TabIndex = 167;
            this.label6.Text = "Y/c chọn khi chọn lập lịch cho chạy tương tác";
            this.label6.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Active = true;
            this.btnSave.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.BorderRadius = 0;
            this.btnSave.ButtonText = "Lưu";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DisabledColor = System.Drawing.Color.Gray;
            this.btnSave.Iconcolor = System.Drawing.Color.Transparent;
            this.btnSave.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnSave.Iconimage")));
            this.btnSave.Iconimage_right = null;
            this.btnSave.Iconimage_right_Selected = null;
            this.btnSave.Iconimage_Selected = null;
            this.btnSave.IconMarginLeft = 0;
            this.btnSave.IconMarginRight = 0;
            this.btnSave.IconRightVisible = false;
            this.btnSave.IconRightZoom = 0D;
            this.btnSave.IconVisible = false;
            this.btnSave.IconZoom = 40D;
            this.btnSave.IsTab = false;
            this.btnSave.Location = new System.Drawing.Point(196, 14);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnSave.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnSave.OnHoverTextColor = System.Drawing.Color.White;
            this.btnSave.selected = true;
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 164;
            this.btnSave.Text = "Lưu";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.Textcolor = System.Drawing.Color.White;
            this.btnSave.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelectAcc
            // 
            this.btnSelectAcc.Active = true;
            this.btnSelectAcc.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnSelectAcc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnSelectAcc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectAcc.BorderRadius = 0;
            this.btnSelectAcc.ButtonText = "Chọn account";
            this.btnSelectAcc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectAcc.DisabledColor = System.Drawing.Color.Gray;
            this.btnSelectAcc.Iconcolor = System.Drawing.Color.Transparent;
            this.btnSelectAcc.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnSelectAcc.Iconimage")));
            this.btnSelectAcc.Iconimage_right = null;
            this.btnSelectAcc.Iconimage_right_Selected = null;
            this.btnSelectAcc.Iconimage_Selected = null;
            this.btnSelectAcc.IconMarginLeft = 0;
            this.btnSelectAcc.IconMarginRight = 0;
            this.btnSelectAcc.IconRightVisible = false;
            this.btnSelectAcc.IconRightZoom = 0D;
            this.btnSelectAcc.IconVisible = false;
            this.btnSelectAcc.IconZoom = 40D;
            this.btnSelectAcc.IsTab = false;
            this.btnSelectAcc.Location = new System.Drawing.Point(97, 92);
            this.btnSelectAcc.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSelectAcc.Name = "btnSelectAcc";
            this.btnSelectAcc.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnSelectAcc.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnSelectAcc.OnHoverTextColor = System.Drawing.Color.White;
            this.btnSelectAcc.selected = true;
            this.btnSelectAcc.Size = new System.Drawing.Size(103, 23);
            this.btnSelectAcc.TabIndex = 161;
            this.btnSelectAcc.Text = "Chọn account";
            this.btnSelectAcc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSelectAcc.Textcolor = System.Drawing.Color.White;
            this.btnSelectAcc.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAcc.Visible = false;
            this.btnSelectAcc.Click += new System.EventHandler(this.btnSelectAcc_Click);
            // 
            // btnPathReaction
            // 
            this.btnPathReaction.Active = true;
            this.btnPathReaction.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnPathReaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnPathReaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPathReaction.BorderRadius = 0;
            this.btnPathReaction.ButtonText = "Đặt giờ";
            this.btnPathReaction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPathReaction.DisabledColor = System.Drawing.Color.Gray;
            this.btnPathReaction.Iconcolor = System.Drawing.Color.Transparent;
            this.btnPathReaction.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnPathReaction.Iconimage")));
            this.btnPathReaction.Iconimage_right = null;
            this.btnPathReaction.Iconimage_right_Selected = null;
            this.btnPathReaction.Iconimage_Selected = null;
            this.btnPathReaction.IconMarginLeft = 0;
            this.btnPathReaction.IconMarginRight = 0;
            this.btnPathReaction.IconRightVisible = false;
            this.btnPathReaction.IconRightZoom = 0D;
            this.btnPathReaction.IconVisible = false;
            this.btnPathReaction.IconZoom = 40D;
            this.btnPathReaction.IsTab = false;
            this.btnPathReaction.Location = new System.Drawing.Point(88, 192);
            this.btnPathReaction.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnPathReaction.Name = "btnPathReaction";
            this.btnPathReaction.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnPathReaction.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnPathReaction.OnHoverTextColor = System.Drawing.Color.White;
            this.btnPathReaction.selected = true;
            this.btnPathReaction.Size = new System.Drawing.Size(89, 23);
            this.btnPathReaction.TabIndex = 159;
            this.btnPathReaction.Text = "Đặt giờ";
            this.btnPathReaction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPathReaction.Textcolor = System.Drawing.Color.White;
            this.btnPathReaction.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPathReaction.Click += new System.EventHandler(this.btnPathReaction_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Image = global::NinjaSystem.Properties.Resources.Edit_File_48px;
            this.btnUpdate.ImageActive = null;
            this.btnUpdate.Location = new System.Drawing.Point(45, 8);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(31, 48);
            this.btnUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnUpdate.TabIndex = 23;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Zoom = 10;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.Image = global::NinjaSystem.Properties.Resources.icons8_cancel_48;
            this.btnXoa.ImageActive = null;
            this.btnXoa.Location = new System.Drawing.Point(118, 8);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(34, 48);
            this.btnXoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnXoa.TabIndex = 24;
            this.btnXoa.TabStop = false;
            this.btnXoa.Zoom = 10;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btn_addUser
            // 
            this.btn_addUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_addUser.Image = global::NinjaSystem.Properties.Resources.Add_48px;
            this.btn_addUser.ImageActive = null;
            this.btn_addUser.Location = new System.Drawing.Point(8, 8);
            this.btn_addUser.Margin = new System.Windows.Forms.Padding(2);
            this.btn_addUser.Name = "btn_addUser";
            this.btn_addUser.Size = new System.Drawing.Size(34, 48);
            this.btn_addUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_addUser.TabIndex = 25;
            this.btn_addUser.TabStop = false;
            this.btn_addUser.Zoom = 10;
            this.btn_addUser.Click += new System.EventHandler(this.btn_addUser_Click);
            // 
            // btn_save
            // 
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.Image = global::NinjaSystem.Properties.Resources.Save_40px;
            this.btn_save.ImageActive = null;
            this.btn_save.Location = new System.Drawing.Point(80, 8);
            this.btn_save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(31, 48);
            this.btn_save.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_save.TabIndex = 168;
            this.btn_save.TabStop = false;
            this.btn_save.Zoom = 10;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(631, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 15);
            this.label7.TabIndex = 169;
            this.label7.Text = "Danh sách tài khoản";
            this.label7.Visible = false;
            // 
            // frmSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 445);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvSchedule);
            this.Controls.Add(this.txtAccounts);
            this.Controls.Add(this.btnSelectAcc);
            this.Controls.Add(this.dtpHour);
            this.Controls.Add(this.btnPathReaction);
            this.Controls.Add(this.txtHouse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btn_addUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lập lịch ";
            this.Load += new System.EventHandler(this.frmSchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnXoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_addUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_save)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboConfig;
        private Bunifu.Framework.UI.BunifuImageButton btnUpdate;
        private Bunifu.Framework.UI.BunifuImageButton btnXoa;
        private Bunifu.Framework.UI.BunifuImageButton btn_addUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtHouse;
        private Bunifu.Framework.UI.BunifuFlatButton btnPathReaction;
        private System.Windows.Forms.DateTimePicker dtpHour;
        private Bunifu.Framework.UI.BunifuFlatButton btnSelectAcc;
        private System.Windows.Forms.RichTextBox txtAccounts;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private Bunifu.Framework.UI.BunifuFlatButton btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn todate;
        private System.Windows.Forms.DataGridViewTextBoxColumn config;
        private System.Windows.Forms.DataGridViewTextBoxColumn hours;
        private System.Windows.Forms.DataGridViewTextBoxColumn Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private Bunifu.Framework.UI.BunifuImageButton btn_save;
        private System.Windows.Forms.Label label7;
    }
}