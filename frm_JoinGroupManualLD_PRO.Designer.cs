namespace NinjaSystem
{
    partial class frm_JoinGroupManualLD_PRO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_JoinGroupManualLD_PRO));
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkJoinTrung = new System.Windows.Forms.CheckBox();
            this.txtPathAnswer = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label32 = new System.Windows.Forms.Label();
            this.lbAddress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numGroupJoin = new System.Windows.Forms.NumericUpDown();
            this.numDelayMax = new System.Windows.Forms.NumericUpDown();
            this.numDelayMin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtUID = new System.Windows.Forms.RichTextBox();
            this.richLogs = new System.Windows.Forms.RichTextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numError = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupJoin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numError)).BeginInit();
            this.SuspendLayout();
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(373, 178);
            this.pibStatus.Name = "pibStatus";
            this.pibStatus.Size = new System.Drawing.Size(97, 10);
            this.pibStatus.TabIndex = 14;
            this.pibStatus.TabStop = false;
            this.pibStatus.Visible = false;
            this.pibStatus.Click += new System.EventHandler(this.pibStatus_Click);
            // 
            // btnRun
            // 
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Image = global::NinjaSystem.Properties.Resources.Circled_Play_48px;
            this.btnRun.ImageActive = null;
            this.btnRun.Location = new System.Drawing.Point(295, 157);
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
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.Location = new System.Drawing.Point(0, 196);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(1021, 410);
            this.dgvUser.TabIndex = 13;
            this.dgvUser.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUser_CellMouseClick);
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
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.numError);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.chkJoinTrung);
            this.panel4.Controls.Add(this.txtPathAnswer);
            this.panel4.Controls.Add(this.label32);
            this.panel4.Controls.Add(this.lbAddress);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.numGroupJoin);
            this.panel4.Controls.Add(this.numDelayMax);
            this.panel4.Controls.Add(this.numDelayMin);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.bunifuImageButton1);
            this.panel4.Controls.Add(this.pibStatus);
            this.panel4.Controls.Add(this.btnRun);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1021, 196);
            this.panel4.TabIndex = 11;
            // 
            // chkJoinTrung
            // 
            this.chkJoinTrung.AutoSize = true;
            this.chkJoinTrung.Location = new System.Drawing.Point(295, 130);
            this.chkJoinTrung.Name = "chkJoinTrung";
            this.chkJoinTrung.Size = new System.Drawing.Size(256, 17);
            this.chkJoinTrung.TabIndex = 161;
            this.chkJoinTrung.Text = "Tất cả tài khoản cùng tham gia danh sách nhóm";
            this.chkJoinTrung.UseVisualStyleBackColor = true;
            // 
            // txtPathAnswer
            // 
            this.txtPathAnswer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPathAnswer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPathAnswer.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPathAnswer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPathAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPathAnswer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPathAnswer.HintForeColor = System.Drawing.Color.Empty;
            this.txtPathAnswer.HintText = "";
            this.txtPathAnswer.isPassword = false;
            this.txtPathAnswer.LineFocusedColor = System.Drawing.Color.Blue;
            this.txtPathAnswer.LineIdleColor = System.Drawing.Color.Gray;
            this.txtPathAnswer.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txtPathAnswer.LineThickness = 1;
            this.txtPathAnswer.Location = new System.Drawing.Point(474, 64);
            this.txtPathAnswer.Margin = new System.Windows.Forms.Padding(4);
            this.txtPathAnswer.MaxLength = 32767;
            this.txtPathAnswer.Name = "txtPathAnswer";
            this.txtPathAnswer.Size = new System.Drawing.Size(486, 24);
            this.txtPathAnswer.TabIndex = 158;
            this.txtPathAnswer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(292, 75);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(174, 13);
            this.label32.TabIndex = 157;
            this.label32.Text = "Nội dung trả lời câu hỏi (hỗ trợ spin)";
            // 
            // lbAddress
            // 
            this.lbAddress.AutoSize = true;
            this.lbAddress.Location = new System.Drawing.Point(506, 135);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(0, 13);
            this.lbAddress.TabIndex = 153;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(623, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 134;
            this.label4.Text = "giây";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(554, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 134;
            this.label2.Text = "nhóm";
            // 
            // numGroupJoin
            // 
            this.numGroupJoin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.numGroupJoin.ForeColor = System.Drawing.Color.Black;
            this.numGroupJoin.Location = new System.Drawing.Point(477, 8);
            this.numGroupJoin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numGroupJoin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGroupJoin.Name = "numGroupJoin";
            this.numGroupJoin.Size = new System.Drawing.Size(60, 20);
            this.numGroupJoin.TabIndex = 133;
            this.numGroupJoin.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numDelayMax
            // 
            this.numDelayMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.numDelayMax.ForeColor = System.Drawing.Color.Black;
            this.numDelayMax.Location = new System.Drawing.Point(557, 37);
            this.numDelayMax.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDelayMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDelayMax.Name = "numDelayMax";
            this.numDelayMax.Size = new System.Drawing.Size(60, 20);
            this.numDelayMax.TabIndex = 133;
            this.numDelayMax.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numDelayMin
            // 
            this.numDelayMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.numDelayMin.ForeColor = System.Drawing.Color.Black;
            this.numDelayMin.Location = new System.Drawing.Point(477, 37);
            this.numDelayMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDelayMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDelayMin.Name = "numDelayMin";
            this.numDelayMin.Size = new System.Drawing.Size(60, 20);
            this.numDelayMin.TabIndex = 133;
            this.numDelayMin.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Khoảng cách giữa 2 lần tham gia";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Mỗi tài khoản tham gia ";
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(333, 157);
            this.bunifuImageButton1.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(35, 35);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 26;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUID);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 196);
            this.groupBox1.TabIndex = 160;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách group ID - Mỗi id 1 dòng";
            // 
            // txtUID
            // 
            this.txtUID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUID.Location = new System.Drawing.Point(3, 16);
            this.txtUID.Margin = new System.Windows.Forms.Padding(2);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(263, 177);
            this.txtUID.TabIndex = 27;
            this.txtUID.Text = "";
            this.txtUID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtUID_MouseClick);
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 606);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(1021, 82);
            this.richLogs.TabIndex = 32;
            this.richLogs.Text = "Hướng dẫn tham gia nhóm : https://www.youtube.com/watch?v=cIaLcxcnuN8";
            this.richLogs.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richLogs_LinkClicked);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(12, 201);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 153;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 13);
            this.label5.TabIndex = 162;
            this.label5.Text = "Chuyển tài khoản khi lỗi liên tục";
            // 
            // numError
            // 
            this.numError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.numError.ForeColor = System.Drawing.Color.Black;
            this.numError.Location = new System.Drawing.Point(477, 93);
            this.numError.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numError.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numError.Name = "numError";
            this.numError.Size = new System.Drawing.Size(60, 20);
            this.numError.TabIndex = 163;
            this.numError.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(554, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 164;
            this.label6.Text = "UID";
            // 
            // frm_JoinGroupManualLD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 688);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.richLogs);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_JoinGroupManualLD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tham gia nhóm";
            this.Load += new System.EventHandler(this.frm_JoinGroupManualLD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupJoin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pibStatus;
        private Bunifu.Framework.UI.BunifuImageButton btnRun;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.Panel panel4;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private System.Windows.Forms.RichTextBox txtUID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numDelayMin;
        private System.Windows.Forms.RichTextBox richLogs;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label lbAddress;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtPathAnswer;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numGroupJoin;
        private System.Windows.Forms.NumericUpDown numDelayMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkJoinTrung;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numError;
    }
}