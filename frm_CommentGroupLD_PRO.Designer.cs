namespace NinjaSystem
{
    partial class frm_CommentGroupLD_PRO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CommentGroupLD_PRO));
            this.richLogs = new System.Windows.Forms.RichTextBox();
            this.pibStatus = new System.Windows.Forms.PictureBox();
            this.btnRun = new Bunifu.Framework.UI.BunifuImageButton();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chọnDòngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.nummaxFail = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.chkgidOfacc = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numWait = new System.Windows.Forms.NumericUpDown();
            this.chkLoopRun = new System.Windows.Forms.CheckBox();
            this.lbAddress = new System.Windows.Forms.Label();
            this.numLike = new System.Windows.Forms.NumericUpDown();
            this.chkLike = new System.Windows.Forms.CheckBox();
            this.chkComment = new System.Windows.Forms.CheckBox();
            this.numComment = new System.Windows.Forms.NumericUpDown();
            this.txtComment = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.numLuot = new System.Windows.Forms.NumericUpDown();
            this.chkLuot = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxFail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLike)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuot)).BeginInit();
            this.SuspendLayout();
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 472);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(982, 96);
            this.richLogs.TabIndex = 12;
            this.richLogs.Text = "";
            this.richLogs.Visible = false;
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(531, 135);
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
            this.btnRun.Location = new System.Drawing.Point(453, 113);
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
            this.clID,
            this.clUID,
            this.clName,
            this.clStatus,
            this.Message});
            this.dgvUser.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.Location = new System.Drawing.Point(0, 157);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(982, 411);
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
            // clID
            // 
            this.clID.HeaderText = "LDPlayer ID";
            this.clID.Name = "clID";
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
            this.panel4.Controls.Add(this.numLuot);
            this.panel4.Controls.Add(this.chkLuot);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.nummaxFail);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.chkgidOfacc);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.numWait);
            this.panel4.Controls.Add(this.chkLoopRun);
            this.panel4.Controls.Add(this.lbAddress);
            this.panel4.Controls.Add(this.numLike);
            this.panel4.Controls.Add(this.chkLike);
            this.panel4.Controls.Add(this.chkComment);
            this.panel4.Controls.Add(this.numComment);
            this.panel4.Controls.Add(this.txtComment);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtID);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.numDelay);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.bunifuImageButton1);
            this.panel4.Controls.Add(this.pibStatus);
            this.panel4.Controls.Add(this.btnRun);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(982, 157);
            this.panel4.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(714, 90);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 202;
            this.label12.Text = "lướt";
            // 
            // nummaxFail
            // 
            this.nummaxFail.Location = new System.Drawing.Point(660, 88);
            this.nummaxFail.Margin = new System.Windows.Forms.Padding(2);
            this.nummaxFail.Name = "nummaxFail";
            this.nummaxFail.Size = new System.Drawing.Size(50, 20);
            this.nummaxFail.TabIndex = 201;
            this.nummaxFail.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(450, 91);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(206, 13);
            this.label13.TabIndex = 200;
            this.label13.Text = "Kết thúc khi không thấy nút comment, like";
            // 
            // chkgidOfacc
            // 
            this.chkgidOfacc.AutoSize = true;
            this.chkgidOfacc.Location = new System.Drawing.Point(453, 67);
            this.chkgidOfacc.Margin = new System.Windows.Forms.Padding(2);
            this.chkgidOfacc.Name = "chkgidOfacc";
            this.chkgidOfacc.Size = new System.Drawing.Size(221, 17);
            this.chkgidOfacc.TabIndex = 184;
            this.chkgidOfacc.Text = "Kiểm tra đã tham gia các ID group chưa?";
            this.chkgidOfacc.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(688, 42);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 179;
            this.label10.Text = "phút";
            // 
            // numWait
            // 
            this.numWait.Location = new System.Drawing.Point(630, 38);
            this.numWait.Name = "numWait";
            this.numWait.Size = new System.Drawing.Size(46, 20);
            this.numWait.TabIndex = 178;
            this.numWait.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkLoopRun
            // 
            this.chkLoopRun.AutoSize = true;
            this.chkLoopRun.Location = new System.Drawing.Point(453, 39);
            this.chkLoopRun.Margin = new System.Windows.Forms.Padding(2);
            this.chkLoopRun.Name = "chkLoopRun";
            this.chkLoopRun.Size = new System.Drawing.Size(138, 17);
            this.chkLoopRun.TabIndex = 177;
            this.chkLoopRun.Text = "Chạy lại hành động sau";
            this.chkLoopRun.UseVisualStyleBackColor = true;
            // 
            // lbAddress
            // 
            this.lbAddress.AutoSize = true;
            this.lbAddress.Location = new System.Drawing.Point(663, 132);
            this.lbAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(0, 13);
            this.lbAddress.TabIndex = 173;
            // 
            // numLike
            // 
            this.numLike.Location = new System.Drawing.Point(678, 8);
            this.numLike.Margin = new System.Windows.Forms.Padding(2);
            this.numLike.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLike.Name = "numLike";
            this.numLike.Size = new System.Drawing.Size(46, 20);
            this.numLike.TabIndex = 171;
            this.numLike.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chkLike
            // 
            this.chkLike.AutoSize = true;
            this.chkLike.Location = new System.Drawing.Point(628, 9);
            this.chkLike.Margin = new System.Windows.Forms.Padding(2);
            this.chkLike.Name = "chkLike";
            this.chkLike.Size = new System.Drawing.Size(46, 17);
            this.chkLike.TabIndex = 169;
            this.chkLike.Text = "Like";
            this.chkLike.UseVisualStyleBackColor = true;
            // 
            // chkComment
            // 
            this.chkComment.AutoSize = true;
            this.chkComment.Location = new System.Drawing.Point(831, 14);
            this.chkComment.Margin = new System.Windows.Forms.Padding(2);
            this.chkComment.Name = "chkComment";
            this.chkComment.Size = new System.Drawing.Size(73, 17);
            this.chkComment.TabIndex = 170;
            this.chkComment.Text = "Comment ";
            this.chkComment.UseVisualStyleBackColor = true;
            // 
            // numComment
            // 
            this.numComment.Location = new System.Drawing.Point(908, 9);
            this.numComment.Margin = new System.Windows.Forms.Padding(2);
            this.numComment.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numComment.Name = "numComment";
            this.numComment.Size = new System.Drawing.Size(46, 20);
            this.numComment.TabIndex = 167;
            this.numComment.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(233, 24);
            this.txtComment.Margin = new System.Windows.Forms.Padding(2);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(202, 126);
            this.txtComment.TabIndex = 158;
            this.txtComment.Text = resources.GetString("txtComment.Text");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 13);
            this.label4.TabIndex = 157;
            this.label4.Text = "Nội dung comment {Noidung1|Noidung2}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 13);
            this.label3.TabIndex = 156;
            this.label3.Text = "Danh sách ID Group (mỗi ID 1 dòng)";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(18, 23);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(178, 126);
            this.txtID.TabIndex = 155;
            this.txtID.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(738, 132);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "giây";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(688, 127);
            this.numDelay.Margin = new System.Windows.Forms.Padding(2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(45, 20);
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(643, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "Delay: ";
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(492, 112);
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
            this.checkBox2.Location = new System.Drawing.Point(9, 162);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 173;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // numLuot
            // 
            this.numLuot.Location = new System.Drawing.Point(504, 8);
            this.numLuot.Margin = new System.Windows.Forms.Padding(2);
            this.numLuot.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLuot.Name = "numLuot";
            this.numLuot.Size = new System.Drawing.Size(46, 20);
            this.numLuot.TabIndex = 209;
            this.numLuot.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chkLuot
            // 
            this.chkLuot.AutoSize = true;
            this.chkLuot.Location = new System.Drawing.Point(453, 11);
            this.chkLuot.Margin = new System.Windows.Forms.Padding(2);
            this.chkLuot.Name = "chkLuot";
            this.chkLuot.Size = new System.Drawing.Size(47, 17);
            this.chkLuot.TabIndex = 208;
            this.chkLuot.Text = "Lướt";
            this.chkLuot.UseVisualStyleBackColor = true;
            // 
            // frm_CommentGroupLD_PRO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 568);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.richLogs);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_CommentGroupLD_PRO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tương tác vào danh sách nhóm ";
            this.Load += new System.EventHandler(this.frmCommentGroupLD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxFail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLike)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuot)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtComment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtID;
        private System.Windows.Forms.NumericUpDown numLike;
        private System.Windows.Forms.CheckBox chkLike;
        private System.Windows.Forms.CheckBox chkComment;
        private System.Windows.Forms.NumericUpDown numComment;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numWait;
        private System.Windows.Forms.CheckBox chkLoopRun;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chọnDòngToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.CheckBox chkgidOfacc;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nummaxFail;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numLuot;
        private System.Windows.Forms.CheckBox chkLuot;
    }
}