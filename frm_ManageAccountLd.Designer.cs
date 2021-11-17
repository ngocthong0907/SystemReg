namespace NinjaSystem
{
    partial class frm_ManageAccountLd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ManageAccountLd));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.trvNhom = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrivateKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clFriend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDevice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clApp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clNox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richLogs = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnSearch = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnChuyenNhom = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnUpdate = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnXoa = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_addUser = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddGroup = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChuyenNhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnXoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_addUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = null;
            this.bunifuDragControl1.Vertical = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.trvNhom);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(217, 638);
            this.panel2.TabIndex = 2;
            // 
            // trvNhom
            // 
            this.trvNhom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvNhom.ContextMenuStrip = this.contextMenuStrip1;
            this.trvNhom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvNhom.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvNhom.FullRowSelect = true;
            this.trvNhom.ImageIndex = 0;
            this.trvNhom.ImageList = this.imageList1;
            this.trvNhom.Indent = 19;
            this.trvNhom.ItemHeight = 50;
            this.trvNhom.Location = new System.Drawing.Point(0, 48);
            this.trvNhom.Margin = new System.Windows.Forms.Padding(2);
            this.trvNhom.Name = "trvNhom";
            this.trvNhom.SelectedImageIndex = 0;
            this.trvNhom.ShowLines = false;
            this.trvNhom.ShowPlusMinus = false;
            this.trvNhom.ShowRootLines = false;
            this.trvNhom.Size = new System.Drawing.Size(217, 590);
            this.trvNhom.TabIndex = 1;
            this.trvNhom.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvNhom_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_rename,
            this.deleteGroup});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 56);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "teamwork.png");
            this.imageList1.Images.SetKeyName(1, "boy.png");
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnAddGroup);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(217, 48);
            this.panel6.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Danh mục";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(217, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 638);
            this.panel3.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(1016, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1, 638);
            this.panel5.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.checkBox2);
            this.panel7.Controls.Add(this.dgvUser);
            this.panel7.Controls.Add(this.richLogs);
            this.panel7.Controls.Add(this.checkBox1);
            this.panel7.Controls.Add(this.panel4);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(218, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(798, 638);
            this.panel7.TabIndex = 6;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(13, 57);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 7;
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
            this.clName,
            this.User,
            this.Password,
            this.PrivateKey,
            this.clFriend,
            this.clGroup,
            this.LdId,
            this.clPhone,
            this.clDevice,
            this.clApp,
            this.clNox,
            this.Status,
            this.Message});
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.Location = new System.Drawing.Point(0, 55);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(798, 485);
            this.dgvUser.TabIndex = 3;
            this.dgvUser.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUser_CellValueChanged);
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
            this.UID.Width = 70;
            // 
            // clName
            // 
            this.clName.HeaderText = "Name";
            this.clName.Name = "clName";
            // 
            // User
            // 
            this.User.HeaderText = "Email";
            this.User.Name = "User";
            this.User.Width = 80;
            // 
            // Password
            // 
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            this.Password.Width = 80;
            // 
            // PrivateKey
            // 
            this.PrivateKey.HeaderText = "PrivateKey";
            this.PrivateKey.Name = "PrivateKey";
            this.PrivateKey.Width = 80;
            // 
            // clFriend
            // 
            this.clFriend.HeaderText = "Friends";
            this.clFriend.Name = "clFriend";
            this.clFriend.Width = 70;
            // 
            // clGroup
            // 
            this.clGroup.HeaderText = "Groups";
            this.clGroup.Name = "clGroup";
            this.clGroup.Width = 70;
            // 
            // LdId
            // 
            this.LdId.HeaderText = "Ld";
            this.LdId.Name = "LdId";
            this.LdId.Width = 40;
            // 
            // clPhone
            // 
            this.clPhone.HeaderText = "Phone";
            this.clPhone.Name = "clPhone";
            this.clPhone.Visible = false;
            this.clPhone.Width = 90;
            // 
            // clDevice
            // 
            this.clDevice.HeaderText = "Device";
            this.clDevice.Name = "clDevice";
            this.clDevice.Visible = false;
            this.clDevice.Width = 80;
            // 
            // clApp
            // 
            this.clApp.HeaderText = "App";
            this.clApp.Name = "clApp";
            this.clApp.Visible = false;
            this.clApp.Width = 70;
            // 
            // clNox
            // 
            this.clNox.HeaderText = "Note";
            this.clNox.Name = "clNox";
            this.clNox.Width = 50;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 50;
            // 
            // Message
            // 
            this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 540);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(798, 98);
            this.richLogs.TabIndex = 5;
            this.richLogs.Text = "";
            this.richLogs.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(15, 91);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 49);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(798, 6);
            this.panel4.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.bunifuImageButton1);
            this.panel8.Controls.Add(this.txtSearch);
            this.panel8.Controls.Add(this.btnSearch);
            this.panel8.Controls.Add(this.btnChuyenNhom);
            this.panel8.Controls.Add(this.btnUpdate);
            this.panel8.Controls.Add(this.btnXoa);
            this.panel8.Controls.Add(this.btn_addUser);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(798, 49);
            this.panel8.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(525, 16);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(219, 20);
            this.txtSearch.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Image = global::NinjaSystem.Properties.Resources.inputcodenew;
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(278, 7);
            this.bunifuImageButton1.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(35, 29);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 2;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Visible = false;
            this.bunifuImageButton1.Zoom = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Image = global::NinjaSystem.Properties.Resources.icons8_google_web_search_48;
            this.btnSearch.ImageActive = null;
            this.btnSearch.Location = new System.Drawing.Point(747, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 48);
            this.btnSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSearch.TabIndex = 4;
            this.btnSearch.TabStop = false;
            this.btnSearch.Zoom = 10;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnChuyenNhom
            // 
            this.btnChuyenNhom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChuyenNhom.Image = global::NinjaSystem.Properties.Resources.icons8_sign_out_48;
            this.btnChuyenNhom.ImageActive = null;
            this.btnChuyenNhom.Location = new System.Drawing.Point(126, 0);
            this.btnChuyenNhom.Margin = new System.Windows.Forms.Padding(2);
            this.btnChuyenNhom.Name = "btnChuyenNhom";
            this.btnChuyenNhom.Size = new System.Drawing.Size(34, 48);
            this.btnChuyenNhom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnChuyenNhom.TabIndex = 2;
            this.btnChuyenNhom.TabStop = false;
            this.btnChuyenNhom.Zoom = 10;
            this.btnChuyenNhom.Click += new System.EventHandler(this.btnChuyenNhom_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Image = global::NinjaSystem.Properties.Resources.Edit_File_48px;
            this.btnUpdate.ImageActive = null;
            this.btnUpdate.Location = new System.Drawing.Point(50, 0);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(31, 48);
            this.btnUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Zoom = 10;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.Image = global::NinjaSystem.Properties.Resources.icons8_cancel_48;
            this.btnXoa.ImageActive = null;
            this.btnXoa.Location = new System.Drawing.Point(86, 0);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(34, 48);
            this.btnXoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnXoa.TabIndex = 0;
            this.btnXoa.TabStop = false;
            this.btnXoa.Zoom = 10;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btn_addUser
            // 
            this.btn_addUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_addUser.Image = global::NinjaSystem.Properties.Resources.Add_48px;
            this.btn_addUser.ImageActive = null;
            this.btn_addUser.Location = new System.Drawing.Point(13, 0);
            this.btn_addUser.Margin = new System.Windows.Forms.Padding(2);
            this.btn_addUser.Name = "btn_addUser";
            this.btn_addUser.Size = new System.Drawing.Size(34, 48);
            this.btn_addUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_addUser.TabIndex = 0;
            this.btn_addUser.TabStop = false;
            this.btn_addUser.Zoom = 10;
            this.btn_addUser.Click += new System.EventHandler(this.btn_addUser_Click);
            // 
            // btn_rename
            // 
            this.btn_rename.Image = global::NinjaSystem.Properties.Resources.Rename_48px;
            this.btn_rename.Name = "btn_rename";
            this.btn_rename.Size = new System.Drawing.Size(116, 26);
            this.btn_rename.Text = "Đổi tên";
            this.btn_rename.Click += new System.EventHandler(this.btn_rename_Click);
            // 
            // deleteGroup
            // 
            this.deleteGroup.Image = global::NinjaSystem.Properties.Resources.Minus_48px;
            this.deleteGroup.Name = "deleteGroup";
            this.deleteGroup.Size = new System.Drawing.Size(116, 26);
            this.deleteGroup.Text = "Xóa";
            this.deleteGroup.Click += new System.EventHandler(this.deleteGroup_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Image = global::NinjaSystem.Properties.Resources.Add2_48px;
            this.btnAddGroup.ImageActive = null;
            this.btnAddGroup.Location = new System.Drawing.Point(172, 10);
            this.btnAddGroup.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(35, 29);
            this.btnAddGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAddGroup.TabIndex = 1;
            this.btnAddGroup.TabStop = false;
            this.btnAddGroup.Zoom = 10;
            this.btnAddGroup.Click += new System.EventHandler(this.bunifuImageButton5_Click);
            // 
            // frm_ManageAccountLd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1017, 638);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_ManageAccountLd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý account";
            this.Activated += new System.EventHandler(this.ManageAccountLd_Activated);
            this.Load += new System.EventHandler(this.ManageAccountLd_Load);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChuyenNhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnXoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_addUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private Bunifu.Framework.UI.BunifuImageButton btnAddGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView trvNhom;
        private System.Windows.Forms.Panel panel8;
        private Bunifu.Framework.UI.BunifuImageButton btn_addUser;
        private Bunifu.Framework.UI.BunifuImageButton btnXoa;
        private Bunifu.Framework.UI.BunifuImageButton btnUpdate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btn_rename;
        private System.Windows.Forms.ToolStripMenuItem deleteGroup;
        private Bunifu.Framework.UI.BunifuImageButton btnChuyenNhom;
        private Bunifu.Framework.UI.BunifuImageButton btnSearch;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.RichTextBox richLogs;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrivateKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn clFriend;
        private System.Windows.Forms.DataGridViewTextBoxColumn clGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn LdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDevice;
        private System.Windows.Forms.DataGridViewTextBoxColumn clApp;
        private System.Windows.Forms.DataGridViewTextBoxColumn clNox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
    }
}

