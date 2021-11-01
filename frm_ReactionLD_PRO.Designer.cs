namespace NinjaSystem
{
    partial class frm_ReactionLD_PRO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ReactionLD_PRO));
            this.richLogs = new System.Windows.Forms.RichTextBox();
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
            this.label6 = new System.Windows.Forms.Label();
            this.nummaxFail = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkdeleteIamgecomment = new System.Windows.Forms.CheckBox();
            this.btnPathIamge = new Bunifu.Framework.UI.BunifuFlatButton();
            this.txtPathIamgecomment = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label59 = new System.Windows.Forms.Label();
            this.numcommentImage = new System.Windows.Forms.NumericUpDown();
            this.chkcommentImage = new System.Windows.Forms.CheckBox();
            this.numfollow = new System.Windows.Forms.NumericUpDown();
            this.chkfollow = new System.Windows.Forms.CheckBox();
            this.chkSplit = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numWait = new System.Windows.Forms.NumericUpDown();
            this.chkLoopRun = new System.Windows.Forms.CheckBox();
            this.lbAddress = new System.Windows.Forms.Label();
            this.rdoProfile = new System.Windows.Forms.RadioButton();
            this.rdoIdPage = new System.Windows.Forms.RadioButton();
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
            this.pibStatus = new System.Windows.Forms.PictureBox();
            this.btnRun = new Bunifu.Framework.UI.BunifuImageButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxFail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numcommentImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numfollow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLike)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun)).BeginInit();
            this.SuspendLayout();
            // 
            // richLogs
            // 
            this.richLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richLogs.Location = new System.Drawing.Point(0, 484);
            this.richLogs.Name = "richLogs";
            this.richLogs.Size = new System.Drawing.Size(982, 84);
            this.richLogs.TabIndex = 12;
            this.richLogs.Text = "";
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
            this.dgvUser.Location = new System.Drawing.Point(0, 171);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(982, 313);
            this.dgvUser.TabIndex = 13;
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
            this.chọnDòngToolStripMenuItem.Name = "chọnDòngToolStripMenuItem";
            this.chọnDòngToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.chọnDòngToolStripMenuItem.Text = "Chọn dòng";
            this.chọnDòngToolStripMenuItem.Click += new System.EventHandler(this.chọnDòngToolStripMenuItem_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.nummaxFail);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.chkdeleteIamgecomment);
            this.panel4.Controls.Add(this.btnPathIamge);
            this.panel4.Controls.Add(this.txtPathIamgecomment);
            this.panel4.Controls.Add(this.label59);
            this.panel4.Controls.Add(this.numcommentImage);
            this.panel4.Controls.Add(this.chkcommentImage);
            this.panel4.Controls.Add(this.numfollow);
            this.panel4.Controls.Add(this.chkfollow);
            this.panel4.Controls.Add(this.chkSplit);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.numWait);
            this.panel4.Controls.Add(this.chkLoopRun);
            this.panel4.Controls.Add(this.lbAddress);
            this.panel4.Controls.Add(this.rdoProfile);
            this.panel4.Controls.Add(this.rdoIdPage);
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
            this.panel4.Size = new System.Drawing.Size(982, 171);
            this.panel4.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(928, 145);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 193;
            this.label6.Text = "lướt";
            // 
            // nummaxFail
            // 
            this.nummaxFail.Location = new System.Drawing.Point(874, 143);
            this.nummaxFail.Margin = new System.Windows.Forms.Padding(2);
            this.nummaxFail.Name = "nummaxFail";
            this.nummaxFail.Size = new System.Drawing.Size(50, 20);
            this.nummaxFail.TabIndex = 192;
            this.nummaxFail.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(664, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(206, 13);
            this.label5.TabIndex = 191;
            this.label5.Text = "Kết thúc khi không thấy nút comment, like";
            // 
            // chkdeleteIamgecomment
            // 
            this.chkdeleteIamgecomment.AutoSize = true;
            this.chkdeleteIamgecomment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdeleteIamgecomment.Location = new System.Drawing.Point(668, 52);
            this.chkdeleteIamgecomment.Name = "chkdeleteIamgecomment";
            this.chkdeleteIamgecomment.Size = new System.Drawing.Size(186, 17);
            this.chkdeleteIamgecomment.TabIndex = 190;
            this.chkdeleteIamgecomment.Text = "Xóa ảnh khi comment thành công";
            this.chkdeleteIamgecomment.UseVisualStyleBackColor = true;
            // 
            // btnPathIamge
            // 
            this.btnPathIamge.Active = true;
            this.btnPathIamge.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnPathIamge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnPathIamge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPathIamge.BorderRadius = 0;
            this.btnPathIamge.ButtonText = "Open";
            this.btnPathIamge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPathIamge.DisabledColor = System.Drawing.Color.Gray;
            this.btnPathIamge.Iconcolor = System.Drawing.Color.Transparent;
            this.btnPathIamge.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnPathIamge.Iconimage")));
            this.btnPathIamge.Iconimage_right = null;
            this.btnPathIamge.Iconimage_right_Selected = null;
            this.btnPathIamge.Iconimage_Selected = null;
            this.btnPathIamge.IconMarginLeft = 0;
            this.btnPathIamge.IconMarginRight = 0;
            this.btnPathIamge.IconRightVisible = false;
            this.btnPathIamge.IconRightZoom = 0D;
            this.btnPathIamge.IconVisible = false;
            this.btnPathIamge.IconZoom = 40D;
            this.btnPathIamge.IsTab = false;
            this.btnPathIamge.Location = new System.Drawing.Point(861, 75);
            this.btnPathIamge.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnPathIamge.Name = "btnPathIamge";
            this.btnPathIamge.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(73)))), ((int)(((byte)(0)))));
            this.btnPathIamge.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(66)))), ((int)(((byte)(8)))));
            this.btnPathIamge.OnHoverTextColor = System.Drawing.Color.White;
            this.btnPathIamge.selected = true;
            this.btnPathIamge.Size = new System.Drawing.Size(89, 23);
            this.btnPathIamge.TabIndex = 189;
            this.btnPathIamge.Text = "Open";
            this.btnPathIamge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPathIamge.Textcolor = System.Drawing.Color.White;
            this.btnPathIamge.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPathIamge.Click += new System.EventHandler(this.btnPathIamge_Click);
            // 
            // txtPathIamgecomment
            // 
            this.txtPathIamgecomment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPathIamgecomment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPathIamgecomment.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPathIamgecomment.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPathIamgecomment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPathIamgecomment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPathIamgecomment.HintForeColor = System.Drawing.Color.Empty;
            this.txtPathIamgecomment.HintText = "";
            this.txtPathIamgecomment.isPassword = false;
            this.txtPathIamgecomment.LineFocusedColor = System.Drawing.Color.Blue;
            this.txtPathIamgecomment.LineIdleColor = System.Drawing.Color.Gray;
            this.txtPathIamgecomment.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txtPathIamgecomment.LineThickness = 1;
            this.txtPathIamgecomment.Location = new System.Drawing.Point(594, 76);
            this.txtPathIamgecomment.Margin = new System.Windows.Forms.Padding(4);
            this.txtPathIamgecomment.MaxLength = 32767;
            this.txtPathIamgecomment.Name = "txtPathIamgecomment";
            this.txtPathIamgecomment.Size = new System.Drawing.Size(260, 22);
            this.txtPathIamgecomment.TabIndex = 188;
            this.txtPathIamgecomment.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(485, 85);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(81, 13);
            this.label59.TabIndex = 187;
            this.label59.Text = "Đường dẫn ảnh";
            // 
            // numcommentImage
            // 
            this.numcommentImage.Location = new System.Drawing.Point(594, 52);
            this.numcommentImage.Name = "numcommentImage";
            this.numcommentImage.Size = new System.Drawing.Size(51, 20);
            this.numcommentImage.TabIndex = 186;
            this.numcommentImage.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chkcommentImage
            // 
            this.chkcommentImage.AutoSize = true;
            this.chkcommentImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcommentImage.Location = new System.Drawing.Point(453, 52);
            this.chkcommentImage.Name = "chkcommentImage";
            this.chkcommentImage.Size = new System.Drawing.Size(118, 17);
            this.chkcommentImage.TabIndex = 185;
            this.chkcommentImage.Text = "Comment bằng ảnh";
            this.chkcommentImage.UseVisualStyleBackColor = true;
            // 
            // numfollow
            // 
            this.numfollow.Location = new System.Drawing.Point(903, 11);
            this.numfollow.Margin = new System.Windows.Forms.Padding(2);
            this.numfollow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numfollow.Name = "numfollow";
            this.numfollow.Size = new System.Drawing.Size(50, 20);
            this.numfollow.TabIndex = 184;
            this.numfollow.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chkfollow
            // 
            this.chkfollow.AutoSize = true;
            this.chkfollow.Checked = true;
            this.chkfollow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkfollow.Location = new System.Drawing.Point(843, 11);
            this.chkfollow.Margin = new System.Windows.Forms.Padding(2);
            this.chkfollow.Name = "chkfollow";
            this.chkfollow.Size = new System.Drawing.Size(56, 17);
            this.chkfollow.TabIndex = 183;
            this.chkfollow.Text = "Follow";
            this.chkfollow.UseVisualStyleBackColor = true;
            // 
            // chkSplit
            // 
            this.chkSplit.AutoSize = true;
            this.chkSplit.Location = new System.Drawing.Point(453, 112);
            this.chkSplit.Margin = new System.Windows.Forms.Padding(2);
            this.chkSplit.Name = "chkSplit";
            this.chkSplit.Size = new System.Drawing.Size(220, 17);
            this.chkSplit.TabIndex = 182;
            this.chkSplit.Text = "Chia đều danh sách ID cho các account";
            this.chkSplit.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(932, 112);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 181;
            this.label10.Text = "phút";
            // 
            // numWait
            // 
            this.numWait.Location = new System.Drawing.Point(874, 109);
            this.numWait.Name = "numWait";
            this.numWait.Size = new System.Drawing.Size(50, 20);
            this.numWait.TabIndex = 180;
            this.numWait.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkLoopRun
            // 
            this.chkLoopRun.AutoSize = true;
            this.chkLoopRun.Location = new System.Drawing.Point(681, 112);
            this.chkLoopRun.Margin = new System.Windows.Forms.Padding(2);
            this.chkLoopRun.Name = "chkLoopRun";
            this.chkLoopRun.Size = new System.Drawing.Size(138, 17);
            this.chkLoopRun.TabIndex = 179;
            this.chkLoopRun.Text = "Chạy lại hành động sau";
            this.chkLoopRun.UseVisualStyleBackColor = true;
            // 
            // lbAddress
            // 
            this.lbAddress.AutoSize = true;
            this.lbAddress.Location = new System.Drawing.Point(566, 159);
            this.lbAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(0, 13);
            this.lbAddress.TabIndex = 175;
            // 
            // rdoProfile
            // 
            this.rdoProfile.AutoSize = true;
            this.rdoProfile.Checked = true;
            this.rdoProfile.Location = new System.Drawing.Point(18, 152);
            this.rdoProfile.Margin = new System.Windows.Forms.Padding(2);
            this.rdoProfile.Name = "rdoProfile";
            this.rdoProfile.Size = new System.Drawing.Size(68, 17);
            this.rdoProfile.TabIndex = 174;
            this.rdoProfile.TabStop = true;
            this.rdoProfile.Text = "ID Profile";
            this.rdoProfile.UseVisualStyleBackColor = true;
            // 
            // rdoIdPage
            // 
            this.rdoIdPage.AutoSize = true;
            this.rdoIdPage.Location = new System.Drawing.Point(130, 153);
            this.rdoIdPage.Margin = new System.Windows.Forms.Padding(2);
            this.rdoIdPage.Name = "rdoIdPage";
            this.rdoIdPage.Size = new System.Drawing.Size(64, 17);
            this.rdoIdPage.TabIndex = 173;
            this.rdoIdPage.Text = "ID Page";
            this.rdoIdPage.UseVisualStyleBackColor = true;
            // 
            // numLike
            // 
            this.numLike.Location = new System.Drawing.Point(595, 11);
            this.numLike.Margin = new System.Windows.Forms.Padding(2);
            this.numLike.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLike.Name = "numLike";
            this.numLike.Size = new System.Drawing.Size(50, 20);
            this.numLike.TabIndex = 171;
            this.numLike.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLike.ValueChanged += new System.EventHandler(this.numLike_ValueChanged);
            // 
            // chkLike
            // 
            this.chkLike.AutoSize = true;
            this.chkLike.Checked = true;
            this.chkLike.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLike.Location = new System.Drawing.Point(453, 11);
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
            this.chkComment.Checked = true;
            this.chkComment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkComment.Location = new System.Drawing.Point(668, 11);
            this.chkComment.Margin = new System.Windows.Forms.Padding(2);
            this.chkComment.Name = "chkComment";
            this.chkComment.Size = new System.Drawing.Size(70, 17);
            this.chkComment.TabIndex = 170;
            this.chkComment.Text = "Comment";
            this.chkComment.UseVisualStyleBackColor = true;
            // 
            // numComment
            // 
            this.numComment.Location = new System.Drawing.Point(742, 11);
            this.numComment.Margin = new System.Windows.Forms.Padding(2);
            this.numComment.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numComment.Name = "numComment";
            this.numComment.Size = new System.Drawing.Size(50, 20);
            this.numComment.TabIndex = 167;
            this.numComment.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numComment.ValueChanged += new System.EventHandler(this.numComment_ValueChanged);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(218, 24);
            this.txtComment.Margin = new System.Windows.Forms.Padding(2);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(217, 126);
            this.txtComment.TabIndex = 158;
            this.txtComment.Text = resources.GetString("txtComment.Text");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 13);
            this.label4.TabIndex = 157;
            this.label4.Text = "Comment {Noidung1|Noidung2}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 13);
            this.label3.TabIndex = 156;
            this.label3.Text = "Danh sách UID (mỗi ID 1 dòng)";
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
            this.label2.Location = new System.Drawing.Point(407, 155);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "giây";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(349, 151);
            this.numDelay.Margin = new System.Windows.Forms.Padding(2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(50, 20);
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
            this.label1.Location = new System.Drawing.Point(300, 154);
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
            this.bunifuImageButton1.Location = new System.Drawing.Point(492, 134);
            this.bunifuImageButton1.Margin = new System.Windows.Forms.Padding(2);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(35, 35);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 26;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // pibStatus
            // 
            this.pibStatus.Image = ((System.Drawing.Image)(resources.GetObject("pibStatus.Image")));
            this.pibStatus.Location = new System.Drawing.Point(531, 156);
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
            this.btnRun.Location = new System.Drawing.Point(453, 134);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(35, 35);
            this.btnRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRun.TabIndex = 0;
            this.btnRun.TabStop = false;
            this.btnRun.Zoom = 10;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(17, 175);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 182;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(18, 177);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 183;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // frm_ReactionLD_PRO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 568);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.richLogs);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_ReactionLD_PRO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reaction UID, Page ID";
            this.Load += new System.EventHandler(this.frmReactionLD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nummaxFail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numcommentImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numfollow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLike)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
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
        private System.Windows.Forms.RadioButton rdoProfile;
        private System.Windows.Forms.RadioButton rdoIdPage;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numWait;
        private System.Windows.Forms.CheckBox chkLoopRun;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox chkSplit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chọnDòngToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.NumericUpDown numfollow;
        private System.Windows.Forms.CheckBox chkfollow;
        private System.Windows.Forms.CheckBox chkdeleteIamgecomment;
        private Bunifu.Framework.UI.BunifuFlatButton btnPathIamge;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtPathIamgecomment;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.NumericUpDown numcommentImage;
        private System.Windows.Forms.CheckBox chkcommentImage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nummaxFail;
        private System.Windows.Forms.Label label5;
    }
}