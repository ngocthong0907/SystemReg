namespace NinjaSystem
{
    partial class frm_LeaveGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LeaveGroup));
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chọnDòngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numMember = new System.Windows.Forms.NumericUpDown();
            this.rdoCondition = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdopostAfter = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_config = new Bunifu.Framework.UI.BunifuImageButton();
            this.cboConfig = new System.Windows.Forms.ComboBox();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.txtgroupid = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.btnPathReaction = new Bunifu.Framework.UI.BunifuFlatButton();
            this.rbBygroupId = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_config)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 440);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(982, 128);
            this.richLogs.TabIndex = 12;
            this.richLogs.Text = "";
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(97, 75);
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
            this.btnRun.Location = new System.Drawing.Point(19, 53);
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
            this.dgvUser.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.Location = new System.Drawing.Point(0, 91);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(982, 477);
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chọnDòngToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 26);
            // 
            // chọnDòngToolStripMenuItem
            // 
            this.chọnDòngToolStripMenuItem.Image = global::NinjaSystem.Properties.Resources.Checked_50px;
            this.chọnDòngToolStripMenuItem.Name = "chọnDòngToolStripMenuItem";
            this.chọnDòngToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.chọnDòngToolStripMenuItem.Text = "Chọn dòng";
            this.chọnDòngToolStripMenuItem.Click += new System.EventHandler(this.chọnDòngToolStripMenuItem_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.txtgroupid);
            this.panel4.Controls.Add(this.btnPathReaction);
            this.panel4.Controls.Add(this.rbBygroupId);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.numMember);
            this.panel4.Controls.Add(this.rdoCondition);
            this.panel4.Controls.Add(this.rdoAll);
            this.panel4.Controls.Add(this.rdopostAfter);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.numDelay);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.btn_config);
            this.panel4.Controls.Add(this.cboConfig);
            this.panel4.Controls.Add(this.bunifuImageButton1);
            this.panel4.Controls.Add(this.pibStatus);
            this.panel4.Controls.Add(this.btnRun);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(982, 91);
            this.panel4.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(798, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 156;
            this.label3.Text = "thành viên";
            // 
            // numMember
            // 
            this.numMember.Location = new System.Drawing.Point(732, 51);
            this.numMember.Margin = new System.Windows.Forms.Padding(2);
            this.numMember.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMember.Name = "numMember";
            this.numMember.Size = new System.Drawing.Size(65, 20);
            this.numMember.TabIndex = 155;
            this.numMember.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // rdoCondition
            // 
            this.rdoCondition.AutoSize = true;
            this.rdoCondition.Location = new System.Drawing.Point(548, 53);
            this.rdoCondition.Margin = new System.Windows.Forms.Padding(2);
            this.rdoCondition.Name = "rdoCondition";
            this.rdoCondition.Size = new System.Drawing.Size(186, 17);
            this.rdoCondition.TabIndex = 154;
            this.rdoCondition.Text = "Rời nhóm có số thành viên ít hơn ";
            this.rdoCondition.UseVisualStyleBackColor = true;
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(548, 32);
            this.rdoAll.Margin = new System.Windows.Forms.Padding(2);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(121, 17);
            this.rdoAll.TabIndex = 153;
            this.rdoAll.Text = "Rời tất cả các nhóm";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // rdopostAfter
            // 
            this.rdopostAfter.AutoSize = true;
            this.rdopostAfter.Checked = true;
            this.rdopostAfter.Location = new System.Drawing.Point(548, 11);
            this.rdopostAfter.Margin = new System.Windows.Forms.Padding(2);
            this.rdopostAfter.Name = "rdopostAfter";
            this.rdopostAfter.Size = new System.Drawing.Size(153, 17);
            this.rdopostAfter.TabIndex = 152;
            this.rdopostAfter.TabStop = true;
            this.rdopostAfter.Text = "Rời những nhóm chờ duyệt";
            this.rdopostAfter.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "giây";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(298, 70);
            this.numDelay.Margin = new System.Windows.Forms.Padding(2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(48, 20);
            this.numDelay.TabIndex = 55;
            this.numDelay.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(254, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 54;
            this.label1.Text = "Delay: ";
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
            this.bunifuImageButton1.Location = new System.Drawing.Point(58, 53);
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
            this.checkBox2.Location = new System.Drawing.Point(13, 95);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 174;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // txtgroupid
            // 
            this.txtgroupid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtgroupid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtgroupid.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtgroupid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtgroupid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtgroupid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtgroupid.HintForeColor = System.Drawing.Color.Empty;
            this.txtgroupid.HintText = "";
            this.txtgroupid.isPassword = false;
            this.txtgroupid.LineFocusedColor = System.Drawing.Color.Blue;
            this.txtgroupid.LineIdleColor = System.Drawing.Color.Gray;
            this.txtgroupid.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txtgroupid.LineThickness = 1;
            this.txtgroupid.Location = new System.Drawing.Point(136, 8);
            this.txtgroupid.Margin = new System.Windows.Forms.Padding(4);
            this.txtgroupid.MaxLength = 32767;
            this.txtgroupid.Name = "txtgroupid";
            this.txtgroupid.Size = new System.Drawing.Size(262, 25);
            this.txtgroupid.TabIndex = 162;
            this.txtgroupid.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnPathReaction
            // 
            this.btnPathReaction.Active = true;
            this.btnPathReaction.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnPathReaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnPathReaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPathReaction.BorderRadius = 0;
            this.btnPathReaction.ButtonText = "Open";
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
            this.btnPathReaction.Location = new System.Drawing.Point(417, 13);
            this.btnPathReaction.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnPathReaction.Name = "btnPathReaction";
            this.btnPathReaction.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnPathReaction.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnPathReaction.OnHoverTextColor = System.Drawing.Color.White;
            this.btnPathReaction.selected = true;
            this.btnPathReaction.Size = new System.Drawing.Size(52, 23);
            this.btnPathReaction.TabIndex = 161;
            this.btnPathReaction.Text = "Open";
            this.btnPathReaction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPathReaction.Textcolor = System.Drawing.Color.White;
            this.btnPathReaction.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPathReaction.Click += new System.EventHandler(this.btnPathReaction_Click);
            // 
            // rbBygroupId
            // 
            this.rbBygroupId.AutoSize = true;
            this.rbBygroupId.Location = new System.Drawing.Point(19, 14);
            this.rbBygroupId.Margin = new System.Windows.Forms.Padding(2);
            this.rbBygroupId.Name = "rbBygroupId";
            this.rbBygroupId.Size = new System.Drawing.Size(111, 17);
            this.rbBygroupId.TabIndex = 160;
            this.rbBygroupId.Text = "Rời theo ID Group";
            this.rbBygroupId.UseVisualStyleBackColor = true;
            // 
            // frm_LeaveGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 568);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.richLogs);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_LeaveGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rời nhóm";
            this.Load += new System.EventHandler(this.frmLeaveGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdopostAfter;
        private System.Windows.Forms.NumericUpDown numMember;
        private System.Windows.Forms.RadioButton rdoCondition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chọnDòngToolStripMenuItem;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtgroupid;
        private Bunifu.Framework.UI.BunifuFlatButton btnPathReaction;
        private System.Windows.Forms.RadioButton rbBygroupId;
    }
}