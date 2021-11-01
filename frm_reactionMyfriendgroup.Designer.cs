namespace NinjaSystem
{
    partial class frm_reactionMyfriendgroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_reactionMyfriendgroup));
            this.richLogs = new System.Windows.Forms.RichTextBox();
            this.pibStatus = new System.Windows.Forms.PictureBox();
            this.btnRun = new Bunifu.Framework.UI.BunifuImageButton();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.numLuot = new System.Windows.Forms.NumericUpDown();
            this.chkLuot = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numdelayMax = new System.Windows.Forms.NumericUpDown();
            this.numdelayMin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numGroups = new System.Windows.Forms.NumericUpDown();
            this.chkCommentgroup = new System.Windows.Forms.CheckBox();
            this.chkCommentfriend = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btncommentFriend = new Bunifu.Framework.UI.BunifuFlatButton();
            this.txtpathcommentfriend = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.btncommentGroup = new Bunifu.Framework.UI.BunifuFlatButton();
            this.txtPathcommentGroup = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label33 = new System.Windows.Forms.Label();
            this.nummaxCommentgroup = new System.Windows.Forms.NumericUpDown();
            this.numMaxCommentfriend = new System.Windows.Forms.NumericUpDown();
            this.numminCommentgroup = new System.Windows.Forms.NumericUpDown();
            this.numminCommentfriend = new System.Windows.Forms.NumericUpDown();
            this.nummaxLikegroup = new System.Windows.Forms.NumericUpDown();
            this.numminLikegroup = new System.Windows.Forms.NumericUpDown();
            this.nummaxLikefriend = new System.Windows.Forms.NumericUpDown();
            this.numminLikefriend = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkLikegroup = new System.Windows.Forms.CheckBox();
            this.chkLikefriend = new System.Windows.Forms.CheckBox();
            this.lbAddress = new System.Windows.Forms.Label();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLuot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numdelayMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numdelayMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxCommentgroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCommentfriend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numminCommentgroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numminCommentfriend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxLikegroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numminLikegroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxLikefriend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numminLikefriend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 521);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(1055, 112);
            this.richLogs.TabIndex = 12;
            this.richLogs.Text = "";
            this.richLogs.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richLogs_LinkClicked);
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(939, 98);
            this.pibStatus.Name = "pibStatus";
            this.pibStatus.Size = new System.Drawing.Size(97, 10);
            this.pibStatus.TabIndex = 14;
            this.pibStatus.TabStop = false;
            this.pibStatus.Visible = false;
            // 
            // btnRun
            // 
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Image = global::NinjaSystem.Properties.Resources.Circled_Play_48px;
            this.btnRun.ImageActive = null;
            this.btnRun.Location = new System.Drawing.Point(861, 77);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(35, 35);
            this.btnRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRun.TabIndex = 0;
            this.btnRun.TabStop = false;
            this.btnRun.Zoom = 10;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
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
            this.User,
            this.clUID,
            this.clName,
            this.clStatus,
            this.Message});
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.Location = new System.Drawing.Point(0, 117);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(1055, 404);
            this.dgvUser.TabIndex = 13;
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
            this.Column2.Width = 50;
            // 
            // User
            // 
            this.User.HeaderText = "LDPlayer ID";
            this.User.Name = "User";
            // 
            // clUID
            // 
            this.clUID.HeaderText = "UID";
            this.clUID.Name = "clUID";
            this.clUID.Width = 150;
            // 
            // clName
            // 
            this.clName.HeaderText = "Name";
            this.clName.Name = "clName";
            this.clName.Width = 150;
            // 
            // clStatus
            // 
            this.clStatus.HeaderText = "Status";
            this.clStatus.Name = "clStatus";
            // 
            // Message
            // 
            this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.numLuot);
            this.panel4.Controls.Add(this.chkLuot);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.numdelayMax);
            this.panel4.Controls.Add(this.numdelayMin);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.numGroups);
            this.panel4.Controls.Add(this.chkCommentgroup);
            this.panel4.Controls.Add(this.chkCommentfriend);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btncommentFriend);
            this.panel4.Controls.Add(this.txtpathcommentfriend);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.btncommentGroup);
            this.panel4.Controls.Add(this.txtPathcommentGroup);
            this.panel4.Controls.Add(this.label33);
            this.panel4.Controls.Add(this.nummaxCommentgroup);
            this.panel4.Controls.Add(this.numMaxCommentfriend);
            this.panel4.Controls.Add(this.numminCommentgroup);
            this.panel4.Controls.Add(this.numminCommentfriend);
            this.panel4.Controls.Add(this.nummaxLikegroup);
            this.panel4.Controls.Add(this.numminLikegroup);
            this.panel4.Controls.Add(this.nummaxLikefriend);
            this.panel4.Controls.Add(this.numminLikefriend);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.chkLikegroup);
            this.panel4.Controls.Add(this.chkLikefriend);
            this.panel4.Controls.Add(this.lbAddress);
            this.panel4.Controls.Add(this.bunifuImageButton1);
            this.panel4.Controls.Add(this.pibStatus);
            this.panel4.Controls.Add(this.btnRun);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1055, 117);
            this.panel4.TabIndex = 11;
            // 
            // numLuot
            // 
            this.numLuot.Location = new System.Drawing.Point(372, 84);
            this.numLuot.Margin = new System.Windows.Forms.Padding(2);
            this.numLuot.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLuot.Name = "numLuot";
            this.numLuot.Size = new System.Drawing.Size(46, 20);
            this.numLuot.TabIndex = 228;
            this.numLuot.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chkLuot
            // 
            this.chkLuot.AutoSize = true;
            this.chkLuot.Location = new System.Drawing.Point(300, 87);
            this.chkLuot.Margin = new System.Windows.Forms.Padding(2);
            this.chkLuot.Name = "chkLuot";
            this.chkLuot.Size = new System.Drawing.Size(47, 17);
            this.chkLuot.TabIndex = 227;
            this.chkLuot.Text = "Lướt";
            this.chkLuot.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(228, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 15);
            this.label7.TabIndex = 226;
            this.label7.Text = "bài";
            // 
            // numdelayMax
            // 
            this.numdelayMax.Location = new System.Drawing.Point(670, 88);
            this.numdelayMax.Name = "numdelayMax";
            this.numdelayMax.Size = new System.Drawing.Size(43, 20);
            this.numdelayMax.TabIndex = 225;
            this.numdelayMax.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numdelayMin
            // 
            this.numdelayMin.Location = new System.Drawing.Point(616, 88);
            this.numdelayMin.Name = "numdelayMin";
            this.numdelayMin.Size = new System.Drawing.Size(43, 20);
            this.numdelayMin.TabIndex = 224;
            this.numdelayMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(455, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 15);
            this.label1.TabIndex = 223;
            this.label1.Text = "Delay mỗi lần chuyển ID";
            // 
            // numGroups
            // 
            this.numGroups.Location = new System.Drawing.Point(179, 83);
            this.numGroups.Name = "numGroups";
            this.numGroups.Size = new System.Drawing.Size(43, 20);
            this.numGroups.TabIndex = 222;
            this.numGroups.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkCommentgroup
            // 
            this.chkCommentgroup.AutoSize = true;
            this.chkCommentgroup.Location = new System.Drawing.Point(300, 49);
            this.chkCommentgroup.Name = "chkCommentgroup";
            this.chkCommentgroup.Size = new System.Drawing.Size(70, 17);
            this.chkCommentgroup.TabIndex = 220;
            this.chkCommentgroup.Text = "Comment";
            this.chkCommentgroup.UseVisualStyleBackColor = true;
            // 
            // chkCommentfriend
            // 
            this.chkCommentfriend.AutoSize = true;
            this.chkCommentfriend.Checked = true;
            this.chkCommentfriend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommentfriend.Location = new System.Drawing.Point(301, 16);
            this.chkCommentfriend.Name = "chkCommentfriend";
            this.chkCommentfriend.Size = new System.Drawing.Size(70, 17);
            this.chkCommentfriend.TabIndex = 219;
            this.chkCommentfriend.Text = "Comment";
            this.chkCommentfriend.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(54, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 15);
            this.label2.TabIndex = 218;
            this.label2.Text = "Mỗi nhóm tương tác";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 15);
            this.label6.TabIndex = 218;
            this.label6.Text = "Tương tác nhóm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 15);
            this.label3.TabIndex = 217;
            this.label3.Text = "Tương tác bạn bè";
            // 
            // btncommentFriend
            // 
            this.btncommentFriend.Active = true;
            this.btncommentFriend.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btncommentFriend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btncommentFriend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncommentFriend.BorderRadius = 0;
            this.btncommentFriend.ButtonText = "Open";
            this.btncommentFriend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncommentFriend.DisabledColor = System.Drawing.Color.Gray;
            this.btncommentFriend.Iconcolor = System.Drawing.Color.Transparent;
            this.btncommentFriend.Iconimage = ((System.Drawing.Image)(resources.GetObject("btncommentFriend.Iconimage")));
            this.btncommentFriend.Iconimage_right = null;
            this.btncommentFriend.Iconimage_right_Selected = null;
            this.btncommentFriend.Iconimage_Selected = null;
            this.btncommentFriend.IconMarginLeft = 0;
            this.btncommentFriend.IconMarginRight = 0;
            this.btncommentFriend.IconRightVisible = false;
            this.btncommentFriend.IconRightZoom = 0D;
            this.btncommentFriend.IconVisible = false;
            this.btncommentFriend.IconZoom = 40D;
            this.btncommentFriend.IsTab = false;
            this.btncommentFriend.Location = new System.Drawing.Point(953, 12);
            this.btncommentFriend.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btncommentFriend.Name = "btncommentFriend";
            this.btncommentFriend.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btncommentFriend.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btncommentFriend.OnHoverTextColor = System.Drawing.Color.White;
            this.btncommentFriend.selected = true;
            this.btncommentFriend.Size = new System.Drawing.Size(55, 23);
            this.btncommentFriend.TabIndex = 216;
            this.btncommentFriend.Text = "Open";
            this.btncommentFriend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btncommentFriend.Textcolor = System.Drawing.Color.White;
            this.btncommentFriend.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncommentFriend.Click += new System.EventHandler(this.btncommentFriend_Click);
            // 
            // txtpathcommentfriend
            // 
            this.txtpathcommentfriend.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtpathcommentfriend.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtpathcommentfriend.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtpathcommentfriend.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtpathcommentfriend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtpathcommentfriend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtpathcommentfriend.HintForeColor = System.Drawing.Color.Empty;
            this.txtpathcommentfriend.HintText = "";
            this.txtpathcommentfriend.isPassword = false;
            this.txtpathcommentfriend.LineFocusedColor = System.Drawing.Color.Blue;
            this.txtpathcommentfriend.LineIdleColor = System.Drawing.Color.Gray;
            this.txtpathcommentfriend.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txtpathcommentfriend.LineThickness = 1;
            this.txtpathcommentfriend.Location = new System.Drawing.Point(616, 3);
            this.txtpathcommentfriend.Margin = new System.Windows.Forms.Padding(4);
            this.txtpathcommentfriend.MaxLength = 32767;
            this.txtpathcommentfriend.Name = "txtpathcommentfriend";
            this.txtpathcommentfriend.Size = new System.Drawing.Size(330, 26);
            this.txtpathcommentfriend.TabIndex = 215;
            this.txtpathcommentfriend.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(483, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 214;
            this.label5.Text = "Nội dung comment {spin}";
            // 
            // btncommentGroup
            // 
            this.btncommentGroup.Active = true;
            this.btncommentGroup.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btncommentGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btncommentGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncommentGroup.BorderRadius = 0;
            this.btncommentGroup.ButtonText = "Open";
            this.btncommentGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncommentGroup.DisabledColor = System.Drawing.Color.Gray;
            this.btncommentGroup.Iconcolor = System.Drawing.Color.Transparent;
            this.btncommentGroup.Iconimage = ((System.Drawing.Image)(resources.GetObject("btncommentGroup.Iconimage")));
            this.btncommentGroup.Iconimage_right = null;
            this.btncommentGroup.Iconimage_right_Selected = null;
            this.btncommentGroup.Iconimage_Selected = null;
            this.btncommentGroup.IconMarginLeft = 0;
            this.btncommentGroup.IconMarginRight = 0;
            this.btncommentGroup.IconRightVisible = false;
            this.btncommentGroup.IconRightZoom = 0D;
            this.btncommentGroup.IconVisible = false;
            this.btncommentGroup.IconZoom = 40D;
            this.btncommentGroup.IsTab = false;
            this.btncommentGroup.Location = new System.Drawing.Point(953, 41);
            this.btncommentGroup.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btncommentGroup.Name = "btncommentGroup";
            this.btncommentGroup.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btncommentGroup.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btncommentGroup.OnHoverTextColor = System.Drawing.Color.White;
            this.btncommentGroup.selected = true;
            this.btncommentGroup.Size = new System.Drawing.Size(55, 23);
            this.btncommentGroup.TabIndex = 213;
            this.btncommentGroup.Text = "Open";
            this.btncommentGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btncommentGroup.Textcolor = System.Drawing.Color.White;
            this.btncommentGroup.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncommentGroup.Click += new System.EventHandler(this.btncommentGroup_Click);
            // 
            // txtPathcommentGroup
            // 
            this.txtPathcommentGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPathcommentGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPathcommentGroup.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPathcommentGroup.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPathcommentGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPathcommentGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPathcommentGroup.HintForeColor = System.Drawing.Color.Empty;
            this.txtPathcommentGroup.HintText = "";
            this.txtPathcommentGroup.isPassword = false;
            this.txtPathcommentGroup.LineFocusedColor = System.Drawing.Color.Blue;
            this.txtPathcommentGroup.LineIdleColor = System.Drawing.Color.Gray;
            this.txtPathcommentGroup.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txtPathcommentGroup.LineThickness = 1;
            this.txtPathcommentGroup.Location = new System.Drawing.Point(616, 37);
            this.txtPathcommentGroup.Margin = new System.Windows.Forms.Padding(4);
            this.txtPathcommentGroup.MaxLength = 32767;
            this.txtPathcommentGroup.Name = "txtPathcommentGroup";
            this.txtPathcommentGroup.Size = new System.Drawing.Size(330, 26);
            this.txtPathcommentGroup.TabIndex = 212;
            this.txtPathcommentGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(483, 22);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(126, 13);
            this.label33.TabIndex = 211;
            this.label33.Text = "Nội dung comment {spin}";
            // 
            // nummaxCommentgroup
            // 
            this.nummaxCommentgroup.Location = new System.Drawing.Point(426, 46);
            this.nummaxCommentgroup.Name = "nummaxCommentgroup";
            this.nummaxCommentgroup.Size = new System.Drawing.Size(43, 20);
            this.nummaxCommentgroup.TabIndex = 45;
            this.nummaxCommentgroup.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numMaxCommentfriend
            // 
            this.numMaxCommentfriend.Location = new System.Drawing.Point(426, 13);
            this.numMaxCommentfriend.Name = "numMaxCommentfriend";
            this.numMaxCommentfriend.Size = new System.Drawing.Size(43, 20);
            this.numMaxCommentfriend.TabIndex = 44;
            this.numMaxCommentfriend.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numminCommentgroup
            // 
            this.numminCommentgroup.Location = new System.Drawing.Point(372, 46);
            this.numminCommentgroup.Name = "numminCommentgroup";
            this.numminCommentgroup.Size = new System.Drawing.Size(43, 20);
            this.numminCommentgroup.TabIndex = 43;
            this.numminCommentgroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numminCommentfriend
            // 
            this.numminCommentfriend.Location = new System.Drawing.Point(372, 13);
            this.numminCommentfriend.Name = "numminCommentfriend";
            this.numminCommentfriend.Size = new System.Drawing.Size(43, 20);
            this.numminCommentfriend.TabIndex = 42;
            this.numminCommentfriend.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nummaxLikegroup
            // 
            this.nummaxLikegroup.Location = new System.Drawing.Point(238, 48);
            this.nummaxLikegroup.Name = "nummaxLikegroup";
            this.nummaxLikegroup.Size = new System.Drawing.Size(43, 20);
            this.nummaxLikegroup.TabIndex = 41;
            this.nummaxLikegroup.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numminLikegroup
            // 
            this.numminLikegroup.Location = new System.Drawing.Point(179, 48);
            this.numminLikegroup.Name = "numminLikegroup";
            this.numminLikegroup.Size = new System.Drawing.Size(43, 20);
            this.numminLikegroup.TabIndex = 40;
            this.numminLikegroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nummaxLikefriend
            // 
            this.nummaxLikefriend.Location = new System.Drawing.Point(238, 15);
            this.nummaxLikefriend.Name = "nummaxLikefriend";
            this.nummaxLikefriend.Size = new System.Drawing.Size(43, 20);
            this.nummaxLikefriend.TabIndex = 39;
            this.nummaxLikefriend.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numminLikefriend
            // 
            this.numminLikefriend.Location = new System.Drawing.Point(179, 15);
            this.numminLikefriend.Name = "numminLikefriend";
            this.numminLikefriend.Size = new System.Drawing.Size(43, 20);
            this.numminLikefriend.TabIndex = 38;
            this.numminLikefriend.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(143, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 36;
            this.label4.Text = "Like";
            // 
            // chkLikegroup
            // 
            this.chkLikegroup.AutoSize = true;
            this.chkLikegroup.Location = new System.Drawing.Point(127, 54);
            this.chkLikegroup.Name = "chkLikegroup";
            this.chkLikegroup.Size = new System.Drawing.Size(15, 14);
            this.chkLikegroup.TabIndex = 33;
            this.chkLikegroup.UseVisualStyleBackColor = true;
            // 
            // chkLikefriend
            // 
            this.chkLikefriend.AutoSize = true;
            this.chkLikefriend.Checked = true;
            this.chkLikefriend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLikefriend.Location = new System.Drawing.Point(127, 18);
            this.chkLikefriend.Name = "chkLikefriend";
            this.chkLikefriend.Size = new System.Drawing.Size(46, 17);
            this.chkLikefriend.TabIndex = 32;
            this.chkLikefriend.Text = "Like";
            this.chkLikefriend.UseVisualStyleBackColor = true;
            // 
            // lbAddress
            // 
            this.lbAddress.AutoSize = true;
            this.lbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAddress.Location = new System.Drawing.Point(957, 87);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(0, 15);
            this.lbAddress.TabIndex = 31;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(899, 76);
            this.bunifuImageButton1.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(35, 35);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 26;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(12, 121);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 39;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // frm_reactionMyfriendgroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 633);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.richLogs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_reactionMyfriendgroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chạy tương tác trong danh sách bạn bè, group của nick";
            this.Load += new System.EventHandler(this.frm_reactionMyfriendgroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLuot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numdelayMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numdelayMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxCommentgroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCommentfriend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numminCommentgroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numminCommentfriend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxLikegroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numminLikegroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxLikefriend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numminLikefriend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richLogs;
        private System.Windows.Forms.PictureBox pibStatus;
        private Bunifu.Framework.UI.BunifuImageButton btnRun;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.Panel panel4;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn clUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.NumericUpDown nummaxCommentgroup;
        private System.Windows.Forms.NumericUpDown numMaxCommentfriend;
        private System.Windows.Forms.NumericUpDown numminCommentgroup;
        private System.Windows.Forms.NumericUpDown numminCommentfriend;
        private System.Windows.Forms.NumericUpDown nummaxLikegroup;
        private System.Windows.Forms.NumericUpDown numminLikegroup;
        private System.Windows.Forms.NumericUpDown nummaxLikefriend;
        private System.Windows.Forms.NumericUpDown numminLikefriend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkLikegroup;
        private System.Windows.Forms.CheckBox chkLikefriend;
        private Bunifu.Framework.UI.BunifuFlatButton btncommentFriend;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtpathcommentfriend;
        private System.Windows.Forms.Label label5;
        private Bunifu.Framework.UI.BunifuFlatButton btncommentGroup;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtPathcommentGroup;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.CheckBox chkCommentgroup;
        private System.Windows.Forms.CheckBox chkCommentfriend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numGroups;
        private System.Windows.Forms.NumericUpDown numdelayMax;
        private System.Windows.Forms.NumericUpDown numdelayMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numLuot;
        private System.Windows.Forms.CheckBox chkLuot;
    }
}