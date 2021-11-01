namespace NinjaSystem
{
    partial class frm_Makemoney_PRO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Makemoney_PRO));
            this.richLogs = new System.Windows.Forms.RichTextBox();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Friend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Coins = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Job = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bunifuThinButton21 = new Bunifu.Framework.UI.BunifuThinButton2();
            this.btListproxy = new System.Windows.Forms.Button();
            this.btnXoa = new Bunifu.Framework.UI.BunifuFlatButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numAmountmax = new System.Windows.Forms.NumericUpDown();
            this.numAmountmin = new System.Windows.Forms.NumericUpDown();
            this.chklooprun = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numloopmax = new System.Windows.Forms.NumericUpDown();
            this.numloopmin = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numJobMax = new System.Windows.Forms.NumericUpDown();
            this.numJobMin = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtprefer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblKc2lanchay = new System.Windows.Forms.Label();
            this.numDelayMax = new System.Windows.Forms.NumericUpDown();
            this.numDelayMin = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.lbLuyke = new System.Windows.Forms.Label();
            this.lbltotalcoin = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_amaikey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.pibStatus = new System.Windows.Forms.PictureBox();
            this.btnRun = new Bunifu.Framework.UI.BunifuImageButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numloopmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numloopmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).BeginInit();
            this.SuspendLayout();
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 456);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(982, 112);
            this.richLogs.TabIndex = 12;
            this.richLogs.Text = resources.GetString("richLogs.Text");
            this.richLogs.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richLogs_LinkClicked);
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
            this.clID,
            this.clUID,
            this.clName,
            this.Friend,
            this.Coins,
            this.clStatus,
            this.Message,
            this.Job});
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.Location = new System.Drawing.Point(0, 87);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(982, 369);
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
            this.Column2.Width = 40;
            // 
            // clID
            // 
            this.clID.HeaderText = "LDPlayer ID";
            this.clID.Name = "clID";
            this.clID.Width = 70;
            // 
            // clUID
            // 
            this.clUID.HeaderText = "UID";
            this.clUID.Name = "clUID";
            this.clUID.Width = 130;
            // 
            // clName
            // 
            this.clName.HeaderText = "Name";
            this.clName.Name = "clName";
            this.clName.Width = 170;
            // 
            // Friend
            // 
            this.Friend.HeaderText = "Friend";
            this.Friend.Name = "Friend";
            this.Friend.Width = 60;
            // 
            // Coins
            // 
            this.Coins.HeaderText = "Coins";
            this.Coins.Name = "Coins";
            this.Coins.Width = 70;
            // 
            // clStatus
            // 
            this.clStatus.HeaderText = "Status";
            this.clStatus.Name = "clStatus";
            this.clStatus.Width = 50;
            // 
            // Message
            // 
            this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            // 
            // Job
            // 
            this.Job.HeaderText = "Job";
            this.Job.Name = "Job";
            this.Job.Width = 50;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.bunifuThinButton21);
            this.panel4.Controls.Add(this.btListproxy);
            this.panel4.Controls.Add(this.btnXoa);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.numericUpDown1);
            this.panel4.Controls.Add(this.numAmountmax);
            this.panel4.Controls.Add(this.numAmountmin);
            this.panel4.Controls.Add(this.chklooprun);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.numloopmax);
            this.panel4.Controls.Add(this.numloopmin);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.numJobMax);
            this.panel4.Controls.Add(this.numJobMin);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.linkLabel1);
            this.panel4.Controls.Add(this.txtprefer);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.lblKc2lanchay);
            this.panel4.Controls.Add(this.numDelayMax);
            this.panel4.Controls.Add(this.numDelayMin);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.lbLuyke);
            this.panel4.Controls.Add(this.lbltotalcoin);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.txt_amaikey);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtPass);
            this.panel4.Controls.Add(this.txtUser);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.bunifuImageButton1);
            this.panel4.Controls.Add(this.pibStatus);
            this.panel4.Controls.Add(this.btnRun);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(982, 87);
            this.panel4.TabIndex = 11;
            // 
            // bunifuThinButton21
            // 
            this.bunifuThinButton21.ActiveBorderThickness = 1;
            this.bunifuThinButton21.ActiveCornerRadius = 20;
            this.bunifuThinButton21.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton21.ActiveForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bunifuThinButton21.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton21.BackColor = System.Drawing.Color.White;
            this.bunifuThinButton21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuThinButton21.BackgroundImage")));
            this.bunifuThinButton21.ButtonText = "Đăng ký tài khoản";
            this.bunifuThinButton21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuThinButton21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuThinButton21.ForeColor = System.Drawing.Color.White;
            this.bunifuThinButton21.IdleBorderThickness = 1;
            this.bunifuThinButton21.IdleCornerRadius = 20;
            this.bunifuThinButton21.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(148)))), ((int)(((byte)(29)))));
            this.bunifuThinButton21.IdleForecolor = System.Drawing.Color.White;
            this.bunifuThinButton21.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(148)))), ((int)(((byte)(29)))));
            this.bunifuThinButton21.Location = new System.Drawing.Point(832, 0);
            this.bunifuThinButton21.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuThinButton21.Name = "bunifuThinButton21";
            this.bunifuThinButton21.Size = new System.Drawing.Size(146, 37);
            this.bunifuThinButton21.TabIndex = 178;
            this.bunifuThinButton21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuThinButton21.Click += new System.EventHandler(this.bunifuThinButton21_Click);
            // 
            // btListproxy
            // 
            this.btListproxy.Location = new System.Drawing.Point(637, 30);
            this.btListproxy.Name = "btListproxy";
            this.btListproxy.Size = new System.Drawing.Size(103, 21);
            this.btListproxy.TabIndex = 176;
            this.btListproxy.Text = "Proxy đang dùng";
            this.btListproxy.UseVisualStyleBackColor = true;
            this.btListproxy.Click += new System.EventHandler(this.btListproxy_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Active = false;
            this.btnXoa.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnXoa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnXoa.BorderRadius = 0;
            this.btnXoa.ButtonText = "Tổng Coin:";
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.DisabledColor = System.Drawing.Color.Gray;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Iconcolor = System.Drawing.Color.Transparent;
            this.btnXoa.Iconimage = null;
            this.btnXoa.Iconimage_right = null;
            this.btnXoa.Iconimage_right_Selected = null;
            this.btnXoa.Iconimage_Selected = null;
            this.btnXoa.IconMarginLeft = 0;
            this.btnXoa.IconMarginRight = 0;
            this.btnXoa.IconRightVisible = false;
            this.btnXoa.IconRightZoom = 0D;
            this.btnXoa.IconVisible = false;
            this.btnXoa.IconZoom = 90D;
            this.btnXoa.IsTab = false;
            this.btnXoa.Location = new System.Drawing.Point(591, 60);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnXoa.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnXoa.OnHoverTextColor = System.Drawing.Color.White;
            this.btnXoa.selected = false;
            this.btnXoa.Size = new System.Drawing.Size(81, 24);
            this.btnXoa.TabIndex = 175;
            this.btnXoa.Text = "Tổng Coin:";
            this.btnXoa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnXoa.Textcolor = System.Drawing.Color.White;
            this.btnXoa.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(375, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 174;
            this.label12.Text = "lần";
            this.label12.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(177, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(141, 13);
            this.label9.TabIndex = 173;
            this.label9.Text = "Thử lại khi không lấy đc Job";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(323, 65);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 20);
            this.numericUpDown1.TabIndex = 172;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numAmountmax
            // 
            this.numAmountmax.Location = new System.Drawing.Point(591, 35);
            this.numAmountmax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numAmountmax.Name = "numAmountmax";
            this.numAmountmax.Size = new System.Drawing.Size(38, 20);
            this.numAmountmax.TabIndex = 170;
            this.numAmountmax.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numAmountmin
            // 
            this.numAmountmin.Location = new System.Drawing.Point(543, 35);
            this.numAmountmin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numAmountmin.Name = "numAmountmin";
            this.numAmountmin.Size = new System.Drawing.Size(38, 20);
            this.numAmountmin.TabIndex = 171;
            this.numAmountmin.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chklooprun
            // 
            this.chklooprun.AutoSize = true;
            this.chklooprun.Location = new System.Drawing.Point(413, 6);
            this.chklooprun.Name = "chklooprun";
            this.chklooprun.Size = new System.Drawing.Size(125, 17);
            this.chklooprun.TabIndex = 169;
            this.chklooprun.Text = "Lặp lại tương tác sau";
            this.chklooprun.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(631, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 168;
            this.label11.Text = "phút";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(469, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 167;
            this.label10.Text = "Số lần lặp lại";
            // 
            // numloopmax
            // 
            this.numloopmax.Location = new System.Drawing.Point(591, 5);
            this.numloopmax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numloopmax.Name = "numloopmax";
            this.numloopmax.Size = new System.Drawing.Size(38, 20);
            this.numloopmax.TabIndex = 164;
            this.numloopmax.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numloopmin
            // 
            this.numloopmin.Location = new System.Drawing.Point(543, 5);
            this.numloopmin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numloopmin.Name = "numloopmin";
            this.numloopmin.Size = new System.Drawing.Size(38, 20);
            this.numloopmin.TabIndex = 165;
            this.numloopmin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(375, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 163;
            this.label8.Text = "job";
            // 
            // numJobMax
            // 
            this.numJobMax.Location = new System.Drawing.Point(323, 8);
            this.numJobMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numJobMax.Name = "numJobMax";
            this.numJobMax.Size = new System.Drawing.Size(46, 20);
            this.numJobMax.TabIndex = 161;
            this.numJobMax.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numJobMin
            // 
            this.numJobMin.Location = new System.Drawing.Point(266, 8);
            this.numJobMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numJobMin.Name = "numJobMin";
            this.numJobMin.Size = new System.Drawing.Size(46, 20);
            this.numJobMin.TabIndex = 162;
            this.numJobMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(181, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 160;
            this.label7.Text = "1 acc thực hiện";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(10, 66);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(156, 13);
            this.linkLabel1.TabIndex = 159;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Link đăng kí tham gia kiếm tiền";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtprefer
            // 
            this.txtprefer.Location = new System.Drawing.Point(63, 61);
            this.txtprefer.Name = "txtprefer";
            this.txtprefer.Size = new System.Drawing.Size(44, 20);
            this.txtprefer.TabIndex = 47;
            this.txtprefer.Text = "AM224536";
            this.txtprefer.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Prefer";
            this.label6.Visible = false;
            // 
            // lblKc2lanchay
            // 
            this.lblKc2lanchay.AutoSize = true;
            this.lblKc2lanchay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKc2lanchay.Location = new System.Drawing.Point(179, 42);
            this.lblKc2lanchay.Name = "lblKc2lanchay";
            this.lblKc2lanchay.Size = new System.Drawing.Size(83, 13);
            this.lblKc2lanchay.TabIndex = 45;
            this.lblKc2lanchay.Text = "Delay giữa 2 job";
            // 
            // numDelayMax
            // 
            this.numDelayMax.Location = new System.Drawing.Point(323, 40);
            this.numDelayMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numDelayMax.Name = "numDelayMax";
            this.numDelayMax.Size = new System.Drawing.Size(46, 20);
            this.numDelayMax.TabIndex = 42;
            this.numDelayMax.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numDelayMin
            // 
            this.numDelayMin.Location = new System.Drawing.Point(266, 40);
            this.numDelayMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDelayMin.Name = "numDelayMin";
            this.numDelayMin.Size = new System.Drawing.Size(46, 20);
            this.numDelayMin.TabIndex = 43;
            this.numDelayMin.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(372, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "giây";
            // 
            // lbLuyke
            // 
            this.lbLuyke.AutoSize = true;
            this.lbLuyke.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLuyke.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbLuyke.Location = new System.Drawing.Point(667, 65);
            this.lbLuyke.Name = "lbLuyke";
            this.lbLuyke.Size = new System.Drawing.Size(15, 15);
            this.lbLuyke.TabIndex = 41;
            this.lbLuyke.Text = "0";
            // 
            // lbltotalcoin
            // 
            this.lbltotalcoin.AutoSize = true;
            this.lbltotalcoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalcoin.ForeColor = System.Drawing.Color.Blue;
            this.lbltotalcoin.Location = new System.Drawing.Point(477, 65);
            this.lbltotalcoin.Name = "lbltotalcoin";
            this.lbltotalcoin.Size = new System.Drawing.Size(15, 15);
            this.lbltotalcoin.TabIndex = 39;
            this.lbltotalcoin.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(400, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Coin theo lượt:";
            // 
            // txt_amaikey
            // 
            this.txt_amaikey.Location = new System.Drawing.Point(141, 64);
            this.txt_amaikey.Name = "txt_amaikey";
            this.txt_amaikey.Size = new System.Drawing.Size(31, 20);
            this.txt_amaikey.TabIndex = 37;
            this.txt_amaikey.Text = "qB8VtVreIaxzNqmEeeqqquB";
            this.txt_amaikey.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Amai_key";
            this.label3.Visible = false;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(63, 36);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(109, 20);
            this.txtPass.TabIndex = 35;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(63, 8);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(109, 20);
            this.txtUser.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "User name:";
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(826, 53);
            this.bunifuImageButton1.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(39, 35);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 26;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(869, 72);
            this.pibStatus.Name = "pibStatus";
            this.pibStatus.Size = new System.Drawing.Size(101, 10);
            this.pibStatus.TabIndex = 14;
            this.pibStatus.TabStop = false;
            this.pibStatus.Visible = false;
            // 
            // btnRun
            // 
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Image = global::NinjaSystem.Properties.Resources.Circled_Play_48px;
            this.btnRun.ImageActive = null;
            this.btnRun.Location = new System.Drawing.Point(788, 54);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(39, 35);
            this.btnRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRun.TabIndex = 0;
            this.btnRun.TabStop = false;
            this.btnRun.Zoom = 10;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(13, 97);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 39;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // frm_Makemoney_PRO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 568);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.richLogs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Makemoney_PRO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiếm tiền nào";
            this.Load += new System.EventHandler(this.frm_TuongTacLD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numloopmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numloopmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJobMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_amaikey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbltotalcoin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbLuyke;
        private System.Windows.Forms.Label lblKc2lanchay;
        private System.Windows.Forms.NumericUpDown numDelayMax;
        private System.Windows.Forms.NumericUpDown numDelayMin;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtprefer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numJobMax;
        private System.Windows.Forms.NumericUpDown numJobMin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numloopmax;
        private System.Windows.Forms.NumericUpDown numloopmin;
        private System.Windows.Forms.NumericUpDown numAmountmax;
        private System.Windows.Forms.NumericUpDown numAmountmin;
        private System.Windows.Forms.CheckBox chklooprun;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Friend;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coins;
        private System.Windows.Forms.DataGridViewTextBoxColumn clStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn Job;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private Bunifu.Framework.UI.BunifuFlatButton btnXoa;
        private System.Windows.Forms.Button btListproxy;
        private Bunifu.Framework.UI.BunifuThinButton2 bunifuThinButton21;
    }
}