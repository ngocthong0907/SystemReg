namespace NinjaSystem
{
    partial class frm_InviteLD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_InviteLD));
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
            this.lbAddress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabLivestream = new System.Windows.Forms.TabPage();
            this.txt_namepage = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkLoopInvitePage = new System.Windows.Forms.CheckBox();
            this.numLoopPage = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.numInvitePage = new System.Windows.Forms.NumericUpDown();
            this.rdoLikePage = new System.Windows.Forms.RadioButton();
            this.tabVideo = new System.Windows.Forms.TabPage();
            this.txtGoupsId = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.numInviteGroup = new System.Windows.Forms.NumericUpDown();
            this.rdoLikeGroup = new System.Windows.Forms.RadioButton();
            this.btn_config = new Bunifu.Framework.UI.BunifuImageButton();
            this.cboConfig = new System.Windows.Forms.ComboBox();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabLivestream.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInvitePage)).BeginInit();
            this.tabVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInviteGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_config)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 483);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(982, 85);
            this.richLogs.TabIndex = 12;
            this.richLogs.Text = "";
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(703, 138);
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
            this.btnRun.Location = new System.Drawing.Point(625, 116);
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
            this.dgvUser.Location = new System.Drawing.Point(0, 192);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(982, 291);
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
            this.panel4.Controls.Add(this.lbAddress);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.numDelay);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.tabControl);
            this.panel4.Controls.Add(this.btn_config);
            this.panel4.Controls.Add(this.cboConfig);
            this.panel4.Controls.Add(this.bunifuImageButton1);
            this.panel4.Controls.Add(this.pibStatus);
            this.panel4.Controls.Add(this.btnRun);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(982, 192);
            this.panel4.TabIndex = 11;
            // 
            // lbAddress
            // 
            this.lbAddress.AutoSize = true;
            this.lbAddress.Location = new System.Drawing.Point(711, 138);
            this.lbAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(0, 13);
            this.lbAddress.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(776, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "giây";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(680, 88);
            this.numDelay.Margin = new System.Windows.Forms.Padding(2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(80, 20);
            this.numDelay.TabIndex = 55;
            this.numDelay.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(629, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 54;
            this.label1.Text = "Delay: ";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabLivestream);
            this.tabControl.Controls.Add(this.tabVideo);
            this.tabControl.Location = new System.Drawing.Point(8, 8);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(564, 179);
            this.tabControl.TabIndex = 53;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabLivestream
            // 
            this.tabLivestream.Controls.Add(this.txt_namepage);
            this.tabLivestream.Controls.Add(this.label4);
            this.tabLivestream.Controls.Add(this.label3);
            this.tabLivestream.Controls.Add(this.chkLoopInvitePage);
            this.tabLivestream.Controls.Add(this.numLoopPage);
            this.tabLivestream.Controls.Add(this.label27);
            this.tabLivestream.Controls.Add(this.label26);
            this.tabLivestream.Controls.Add(this.numInvitePage);
            this.tabLivestream.Controls.Add(this.rdoLikePage);
            this.tabLivestream.Location = new System.Drawing.Point(4, 22);
            this.tabLivestream.Margin = new System.Windows.Forms.Padding(2);
            this.tabLivestream.Name = "tabLivestream";
            this.tabLivestream.Padding = new System.Windows.Forms.Padding(2);
            this.tabLivestream.Size = new System.Drawing.Size(556, 153);
            this.tabLivestream.TabIndex = 0;
            this.tabLivestream.Text = "Fanpage";
            this.tabLivestream.UseVisualStyleBackColor = true;
            // 
            // txt_namepage
            // 
            this.txt_namepage.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_namepage.Location = new System.Drawing.Point(2, 2);
            this.txt_namepage.Margin = new System.Windows.Forms.Padding(2);
            this.txt_namepage.Name = "txt_namepage";
            this.txt_namepage.Size = new System.Drawing.Size(552, 89);
            this.txt_namepage.TabIndex = 50;
            this.txt_namepage.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(194, 105);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(349, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Do Facebook giới hạn 50 bạn bè 1 lượt, tăng số lượng bằng cách lặp lại ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 125);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "lần";
            // 
            // chkLoopInvitePage
            // 
            this.chkLoopInvitePage.AutoSize = true;
            this.chkLoopInvitePage.Location = new System.Drawing.Point(197, 124);
            this.chkLoopInvitePage.Name = "chkLoopInvitePage";
            this.chkLoopInvitePage.Size = new System.Drawing.Size(131, 17);
            this.chkLoopInvitePage.TabIndex = 47;
            this.chkLoopInvitePage.Text = "Lặp lại hành động mời";
            this.chkLoopInvitePage.UseVisualStyleBackColor = true;
            // 
            // numLoopPage
            // 
            this.numLoopPage.Location = new System.Drawing.Point(333, 123);
            this.numLoopPage.Margin = new System.Windows.Forms.Padding(2);
            this.numLoopPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLoopPage.Name = "numLoopPage";
            this.numLoopPage.Size = new System.Drawing.Size(42, 20);
            this.numLoopPage.TabIndex = 46;
            this.numLoopPage.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(0, 104);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(80, 13);
            this.label27.TabIndex = 45;
            this.label27.Text = "(Mỗi ID 1 dòng)";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(71, 125);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 13);
            this.label26.TabIndex = 44;
            this.label26.Text = "bạn / 1 lần";
            // 
            // numInvitePage
            // 
            this.numInvitePage.Location = new System.Drawing.Point(7, 122);
            this.numInvitePage.Margin = new System.Windows.Forms.Padding(2);
            this.numInvitePage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numInvitePage.Name = "numInvitePage";
            this.numInvitePage.Size = new System.Drawing.Size(59, 20);
            this.numInvitePage.TabIndex = 43;
            this.numInvitePage.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // rdoLikePage
            // 
            this.rdoLikePage.AutoSize = true;
            this.rdoLikePage.Checked = true;
            this.rdoLikePage.Location = new System.Drawing.Point(415, 123);
            this.rdoLikePage.Margin = new System.Windows.Forms.Padding(2);
            this.rdoLikePage.Name = "rdoLikePage";
            this.rdoLikePage.Size = new System.Drawing.Size(139, 17);
            this.rdoLikePage.TabIndex = 27;
            this.rdoLikePage.TabStop = true;
            this.rdoLikePage.Text = "Mời bạn bè like fanpage";
            this.rdoLikePage.UseVisualStyleBackColor = true;
            this.rdoLikePage.Visible = false;
            // 
            // tabVideo
            // 
            this.tabVideo.Controls.Add(this.txtGoupsId);
            this.tabVideo.Controls.Add(this.label6);
            this.tabVideo.Controls.Add(this.label30);
            this.tabVideo.Controls.Add(this.numInviteGroup);
            this.tabVideo.Controls.Add(this.rdoLikeGroup);
            this.tabVideo.Location = new System.Drawing.Point(4, 22);
            this.tabVideo.Margin = new System.Windows.Forms.Padding(2);
            this.tabVideo.Name = "tabVideo";
            this.tabVideo.Padding = new System.Windows.Forms.Padding(2);
            this.tabVideo.Size = new System.Drawing.Size(556, 153);
            this.tabVideo.TabIndex = 1;
            this.tabVideo.Text = "Group";
            this.tabVideo.UseVisualStyleBackColor = true;
            // 
            // txtGoupsId
            // 
            this.txtGoupsId.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtGoupsId.Location = new System.Drawing.Point(2, 2);
            this.txtGoupsId.Margin = new System.Windows.Forms.Padding(2);
            this.txtGoupsId.Name = "txtGoupsId";
            this.txtGoupsId.Size = new System.Drawing.Size(552, 89);
            this.txtGoupsId.TabIndex = 156;
            this.txtGoupsId.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 101);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 155;
            this.label6.Text = "(Mỗi ID 1 dòng)";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(70, 125);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(59, 13);
            this.label30.TabIndex = 154;
            this.label30.Text = "bạn / 1 lần";
            // 
            // numInviteGroup
            // 
            this.numInviteGroup.Location = new System.Drawing.Point(12, 122);
            this.numInviteGroup.Margin = new System.Windows.Forms.Padding(2);
            this.numInviteGroup.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numInviteGroup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numInviteGroup.Name = "numInviteGroup";
            this.numInviteGroup.Size = new System.Drawing.Size(57, 20);
            this.numInviteGroup.TabIndex = 153;
            this.numInviteGroup.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // rdoLikeGroup
            // 
            this.rdoLikeGroup.AutoSize = true;
            this.rdoLikeGroup.Location = new System.Drawing.Point(346, 100);
            this.rdoLikeGroup.Margin = new System.Windows.Forms.Padding(2);
            this.rdoLikeGroup.Name = "rdoLikeGroup";
            this.rdoLikeGroup.Size = new System.Drawing.Size(129, 17);
            this.rdoLikeGroup.TabIndex = 42;
            this.rdoLikeGroup.Text = "Mời bạn bè like Group";
            this.rdoLikeGroup.UseVisualStyleBackColor = true;
            this.rdoLikeGroup.Visible = false;
            // 
            // btn_config
            // 
            this.btn_config.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_config.Image = global::NinjaSystem.Properties.Resources.Settings_64px;
            this.btn_config.ImageActive = null;
            this.btn_config.Location = new System.Drawing.Point(967, 8);
            this.btn_config.Margin = new System.Windows.Forms.Padding(2);
            this.btn_config.Name = "btn_config";
            this.btn_config.Size = new System.Drawing.Size(7, 23);
            this.btn_config.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_config.TabIndex = 30;
            this.btn_config.TabStop = false;
            this.btn_config.Visible = false;
            this.btn_config.Zoom = 10;
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // cboConfig
            // 
            this.cboConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboConfig.FormattingEnabled = true;
            this.cboConfig.Location = new System.Drawing.Point(957, 8);
            this.cboConfig.Margin = new System.Windows.Forms.Padding(2);
            this.cboConfig.MaxDropDownItems = 20;
            this.cboConfig.Name = "cboConfig";
            this.cboConfig.Size = new System.Drawing.Size(8, 23);
            this.cboConfig.TabIndex = 28;
            this.cboConfig.Text = "Chọn cấu hình";
            this.cboConfig.Visible = false;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(664, 116);
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
            this.checkBox2.Location = new System.Drawing.Point(17, 197);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 174;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // frmInviteLD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 568);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.richLogs);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInviteLD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmInviteLD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabLivestream.ResumeLayout(false);
            this.tabLivestream.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoopPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInvitePage)).EndInit();
            this.tabVideo.ResumeLayout(false);
            this.tabVideo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInviteGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_config)).EndInit();
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
        private Bunifu.Framework.UI.BunifuImageButton btn_config;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn clUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.ComboBox cboConfig;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabLivestream;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown numInvitePage;
        private System.Windows.Forms.RadioButton rdoLikePage;
        private System.Windows.Forms.TabPage tabVideo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown numInviteGroup;
        private System.Windows.Forms.RadioButton rdoLikeGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkLoopInvitePage;
        private System.Windows.Forms.NumericUpDown numLoopPage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txt_namepage;
        private System.Windows.Forms.RichTextBox txtGoupsId;
    }
}