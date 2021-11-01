namespace NinjaSystem
{
    partial class frm_CancelRequestLD_PRO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CancelRequestLD_PRO));
            this.richLogs = new System.Windows.Forms.RichTextBox();
            this.pibStatus = new System.Windows.Forms.PictureBox();
            this.btnRun = new Bunifu.Framework.UI.BunifuImageButton();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chọnDòngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numCancelRequest = new System.Windows.Forms.NumericUpDown();
            this.btn_config = new Bunifu.Framework.UI.BunifuImageButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cboConfig = new System.Windows.Forms.ComboBox();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCancelRequest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_config)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 352);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(1238, 128);
            this.richLogs.TabIndex = 12;
            this.richLogs.Text = "";
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(385, 21);
            this.pibStatus.Name = "pibStatus";
            this.pibStatus.Size = new System.Drawing.Size(76, 14);
            this.pibStatus.TabIndex = 14;
            this.pibStatus.TabStop = false;
            this.pibStatus.Visible = false;
            // 
            // btnRun
            // 
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Image = global::NinjaSystem.Properties.Resources.Circled_Play_48px;
            this.btnRun.ImageActive = null;
            this.btnRun.Location = new System.Drawing.Point(292, 3);
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
            this.dgvUser.Location = new System.Drawing.Point(0, 39);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(1238, 441);
            this.dgvUser.TabIndex = 13;
            this.dgvUser.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUser_CellMouseDown);
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
            this.panel4.Controls.Add(this.numDelay);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.numCancelRequest);
            this.panel4.Controls.Add(this.btn_config);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.cboConfig);
            this.panel4.Controls.Add(this.bunifuImageButton1);
            this.panel4.Controls.Add(this.pibStatus);
            this.panel4.Controls.Add(this.btnRun);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1238, 39);
            this.panel4.TabIndex = 11;
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(239, 12);
            this.numDelay.Margin = new System.Windows.Forms.Padding(2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(44, 20);
            this.numDelay.TabIndex = 33;
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
            this.label1.Location = new System.Drawing.Point(190, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 32;
            this.label1.Text = "Delay: ";
            // 
            // numCancelRequest
            // 
            this.numCancelRequest.Location = new System.Drawing.Point(139, 12);
            this.numCancelRequest.Margin = new System.Windows.Forms.Padding(2);
            this.numCancelRequest.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numCancelRequest.Name = "numCancelRequest";
            this.numCancelRequest.Size = new System.Drawing.Size(48, 20);
            this.numCancelRequest.TabIndex = 31;
            this.numCancelRequest.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btn_config
            // 
            this.btn_config.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_config.Image = global::NinjaSystem.Properties.Resources.Settings_64px;
            this.btn_config.ImageActive = null;
            this.btn_config.Location = new System.Drawing.Point(1216, 12);
            this.btn_config.Margin = new System.Windows.Forms.Padding(2);
            this.btn_config.Name = "btn_config";
            this.btn_config.Size = new System.Drawing.Size(17, 23);
            this.btn_config.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_config.TabIndex = 30;
            this.btn_config.TabStop = false;
            this.btn_config.Visible = false;
            this.btn_config.Zoom = 10;
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "Số lời mời cần hủy:";
            // 
            // cboConfig
            // 
            this.cboConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboConfig.FormattingEnabled = true;
            this.cboConfig.Location = new System.Drawing.Point(1191, 12);
            this.cboConfig.Margin = new System.Windows.Forms.Padding(2);
            this.cboConfig.MaxDropDownItems = 20;
            this.cboConfig.Name = "cboConfig";
            this.cboConfig.Size = new System.Drawing.Size(22, 23);
            this.cboConfig.TabIndex = 28;
            this.cboConfig.Text = "Chọn cấu hình";
            this.cboConfig.Visible = false;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(345, 3);
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
            this.checkBox2.Location = new System.Drawing.Point(12, 44);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 151;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
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
            // frm_CancelRequestLD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 480);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.richLogs);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_CancelRequestLD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hủy lời mời kết bạn đã gửi";
            this.Load += new System.EventHandler(this.frm_CancelRequestLD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCancelRequest)).EndInit();
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
        private System.Windows.Forms.ComboBox cboConfig;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numCancelRequest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chọnDòngToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
    }
}