using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace NinjaSystem
{
    public partial class frm_ImportAccountLD : Form
    {
        public frm_ImportAccountLD()
        {
            InitializeComponent();
        }
        Data dt = new Data();
        Thread thread_1;
        List<Account> list_acc;
        public frm_ImportAccountLD(List<Account> list_acc)
        {
            InitializeComponent();
            this.list_acc = list_acc;
        }
        private void frm_ImportAccountLD_Load(object sender, EventArgs e)
        {
            DataTable source = new DataTable();
            source = dt.select("select * from Danhmuc");
            cboNhom.DisplayMember = "Tendanhmuc";
            cboNhom.ValueMember = "Id_danhmuc";
            cboNhom.DataSource = source;
            loadPhone();
            if (list_acc != null)
                method_LoadAccount();
            loadGroupLD();
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }
            if (File.Exists(String.Format("{0}\\Config\\{1}.txt", Application.StartupPath, "ListUser")))
            {
                List<string> lsUser = new List<string>();
                lsUser = File.ReadAllLines(Application.StartupPath + "\\Config\\ListUser.txt").ToList();
                if (lsUser.Contains(SettingTool.email))
                {
                    dgvUser.Columns["country"].Visible = true;
                    dgvUser.Columns["State"].Visible = true;
                    dgvUser.Columns["City"].Visible = true;
                   
                }

            }
        }
        private void loadGroupLD()
        {
            try
            {
                cboDanhMucLD.Items.Clear();
                ComboboxItem item1 = new ComboboxItem();
                item1.Text = "Chọn nhóm LD";
                cboDanhMucLD.Items.Add(item1);
                List<GroupLDModel> list_groupld = new List<GroupLDModel>();
                GroupLD_BLL groupld_bll = new GroupLD_BLL();
                list_groupld = groupld_bll.selectGroupLD();
                foreach (GroupLDModel group in list_groupld)
                {
                    ComboboxItem item = new ComboboxItem();
                    item = new ComboboxItem();
                    item.Text = group.Name;
                    item.Tag = group;
                    cboDanhMucLD.Items.Add(item);

                }
                cboDanhMucLD.SelectedIndex = 0;
            }
            catch
            {

            }


        }
        private void method_LoadAccount()
        {
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }
            if (File.Exists(String.Format("{0}\\Config\\{1}.txt", Application.StartupPath, "ListUser")))
            {
                List<string> lsUser = new List<string>();
                lsUser = File.ReadAllLines(Application.StartupPath + "\\Config\\ListUser.txt").ToList();
                if (lsUser.Contains(SettingTool.email))
                {
                    dgvUser.Columns["Country"].Visible = true;
                    dgvUser.Columns["State"].Visible = true;
                    dgvUser.Columns["City"].Visible = true;
                }
            }
        }
        private void method_Datagridview(Account acc)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                DataGridViewCheckBoxCell check = new DataGridViewCheckBoxCell();
                check.Value = false;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = (dgvUser.Rows.Count + 1).ToString();
                dataGridViewRow.Cells.Add(cell1);

                DataGridViewTextBoxCell cell1a = new DataGridViewTextBoxCell();
                cell1a.Value = acc.id;
                dataGridViewRow.Cells.Add(cell1a);

                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = acc.Password;
                dataGridViewRow.Cells.Add(cell3);

                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = acc.privatekey;
                dataGridViewRow.Cells.Add(cell4);

                DataGridViewTextBoxCell cell41 = new DataGridViewTextBoxCell();
                cell41.Value = acc.birthday;
                dataGridViewRow.Cells.Add(cell41);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = "";
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell51 = new DataGridViewTextBoxCell();
                cell51.Value = acc.ghichu;
                dataGridViewRow.Cells.Add(cell51);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.Id_account;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                cell8.Value = acc.country;
                dataGridViewRow.Cells.Add(cell8);

                DataGridViewTextBoxCell cell9 = new DataGridViewTextBoxCell();
                cell9.Value = acc.state;
                dataGridViewRow.Cells.Add(cell9);

                DataGridViewTextBoxCell cell10 = new DataGridViewTextBoxCell();
                cell10.Value = acc.city;
                dataGridViewRow.Cells.Add(cell10);

                dataGridViewRow.Tag = acc;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvUser.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }


        private void btnInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            string textFile = "";
            openFile.Filter = "File txt|*.txt";
            openFile.FileName = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                textFile = openFile.FileName;

                string[] lines = File.ReadAllLines(textFile);
                copyToGrid(lines);
            }
        }
        private void copyToGrid(string[] lines)
        {

            foreach (string line in lines)
            {
                try
                {
                    string[] arr = line.Split('|');

                    Account acc = new Account();
                    acc.id = arr[0].Trim();
                    acc.Password = arr[1].Trim();
                    try
                    {
                        acc.privatekey = arr[2].Trim();
                    }
                    catch
                    {
                        acc.privatekey = "";
                    }
                    try
                    {
                        acc.birthday = arr[3].Trim();
                    }
                    catch
                    {
                        acc.birthday = "";
                    }

                    try
                    {
                        acc.country = arr[4].Trim();
                    }
                    catch
                    {
                        acc.country = "";
                    }
                    try
                    {
                        acc.state = arr[5].Trim();
                    }
                    catch
                    {
                        acc.state = "";
                    }
                    try
                    {
                        acc.city = arr[6].Trim();
                    }
                    catch
                    {
                        acc.city = "";
                    }

                    acc.ghichu = "";
                    method_Datagridview(acc);

                }
                catch
                { }
            }
           

        }
        public void loadPhone()
        {
            dgvLD.Rows.Clear();
            LDController ld = new LDController();
            List<LDModel> list_ld = new List<LDModel>();
            list_ld = ld.getLdPlay();
            DataTable source_child = new DataTable();
            foreach (LDModel l in list_ld)
            {
                source_child = dt.select(string.Format("select * from Account where ldid = {0}", l.id));
                l.numacc = source_child.Rows.Count;
                method_DatagridviewLD(l);
            }
        }
        private void method_DatagridviewLD(LDModel acc)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                //DataGridViewCheckBoxCell check = new DataGridViewCheckBoxCell();
                //check.Value = true;
                //dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = (dgvLD.Rows.Count + 1).ToString();
                dataGridViewRow.Cells.Add(cell1);

                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = acc.id;
                dataGridViewRow.Cells.Add(cell2);

                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = acc.name;
                dataGridViewRow.Cells.Add(cell3);

                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = acc.numacc;
                dataGridViewRow.Cells.Add(cell4);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = "";
                dataGridViewRow.Cells.Add(cell5);

                dataGridViewRow.Tag = acc;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvLD.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                if (clipboardText.Length > 0)
                {
                    if (clipboardText.Contains("\r\n"))
                    {
                        char[] chars = new char[2];
                        chars[0] = '\r';
                        chars[1] = '\n';
                        string[] lines = clipboardText.Split(chars);
                        copyToGrid(lines);

                    }
                    else if (clipboardText.Contains("\n"))
                    {
                        char[] chars = new char[1];
                        chars[0] = '\n';
                        string[] lines = clipboardText.Split(chars);
                        copyToGrid(lines);
                    }
                    else
                    {
                        char[] chars = new char[1];
                        chars[0] = '\n';
                        string[] lines = clipboardText.Split(chars);
                        copyToGrid(lines);
                    }

                    foreach (DataGridViewRow row2 in this.dgvUser.Rows)
                    {
                        row2.Cells[0].Value = true;

                    }
                }
                else
                {
                    MessageBox.Show("Không có nội dung để Paste");
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool check = false;
            if (checkBox2.Checked)
            {
                check = true;
            }
            else
                check = false;
            foreach (DataGridViewRow row2 in this.dgvUser.Rows)
            {
                row2.Cells[0].Value = check;

            }
        }



        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.saveAccount));
            thread_1.IsBackground = true;
            this.thread_1.Start();
            // saveAccount();
        }
        private void saveAccount()
        {
            try
            {
                int total = (int)numMax.Value;
                int id_danhmuc = Convert.ToInt32(cboNhom.SelectedValue.ToString());
                string tendanhmuc = cboNhom.Text.ToString();
                List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
                foreach (DataGridViewRow dr in dgvUser.Rows)
                {
                    if ((bool)dr.Cells[0].Value)
                        list_acc.Add(dr);
                }
                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                DataTable check = new DataTable();
                List<DataGridViewRow> list_devices = new List<DataGridViewRow>();
                foreach (DataGridViewRow row2 in this.dgvLD.SelectedRows)
                {
                    list_devices.Add(row2);
                }
                foreach (DataGridViewRow row2 in list_devices)
                {
                    //if((bool)row2.Cells[0].Value)
                    {
                        LDModel ldmodel = (LDModel)row2.Tag;
                        if (ldmodel.numacc < total)
                        {
                            row2.Cells["status"].Value = "Đang lưu tài khoản";
                            int num = total - ldmodel.numacc;
                            for (int i = 0; i < num; i++)
                            {
                                if (list_acc.Count > 0)
                                {
                                    DataGridViewRow dracc = list_acc[0];
                                    list_acc.Remove(dracc);
                                    Account acc = (Account)dracc.Tag;
                                    acc.ldid = ldmodel.id.ToString();
                                    acc.id_danhmuc = id_danhmuc;
                                    acc.tendanhmuc = tendanhmuc;
                                    acc.TrangThai = "Live";
                                    check = dt.select(string.Format("select * from Account where Id = {0}", acc.id.Trim()));
                                    if (check.Rows.Count == 0)
                                    {
                                        nguoidung.insertAccount(acc);
                                    }
                                    else
                                    {
                                        bool kq =  nguoidung.updateAccountByUID(acc);
                                    }
                                    dracc.Cells[0].Value = false;
                                    dracc.Cells["clstatus"].Value = "Đã lưu tài khoản";
                                    row2.Cells["status"].Value = "Đã lưu tài khoản";
                                    ldmodel.numacc++;
                                    row2.Cells["clTotal"].Value = ldmodel.numacc;
                                }
                            }
                            row2.Cells["status"].Value = "Đã lưu tài khoản";
                        }
                        else
                        {
                            row2.Cells["status"].Value = "Đã đủ số lượng";
                        }

                    }


                }
            }
            catch
            {

            }

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            dgvUser.Rows.Clear();
            int id_danhmuc = Convert.ToInt32(cboNhom.SelectedValue.ToString());
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            List<Account> list_acc = new List<Account>();
            list_acc = nguoidung_bll.loadUserbyNhomID(id_danhmuc);
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }

            foreach (DataGridViewRow row2 in this.dgvUser.Rows)
            {
                row2.Cells[0].Value = true;

            }
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    foreach (DataGridViewRow row2 in this.dgvUser.SelectedRows)
            //    {
            //        row2.Cells[0].Value = true;

            //    }
            //}
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            loadLD();
        }
        private void loadLD()
        {
            try
            {
                ComboboxItem item2 = (ComboboxItem)cboDanhMucLD.SelectedItem;
                if (item2.Text == "Chọn nhóm LD")
                {
                    MessageBox.Show("Vui lòng chọn nhóm LD", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {

                    GroupLDModel group = (GroupLDModel)item2.Tag;


                    dgvLD.Rows.Clear();

                    DetailLD_BLL detail_bll = new DetailLD_BLL();
                    List<DetailLDModel> list_detail = new List<DetailLDModel>();
                    list_detail = detail_bll.selectDetailLD(group.GroupLDID);
                    foreach (DetailLDModel l in list_detail)
                    {
                        LDModel ld = new LDModel();
                        ld.id = l.LDID;
                        DataTable source_child = dt.select(string.Format("select * from Account where ldid = {0}", ld.id));
                        ld.numacc = source_child.Rows.Count;
                        ld.name = l.LDName;

                        method_DatagridviewLD(ld);


                    }

                }

                //total 
            }
            catch
            { }
        }

        private void chọnDòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row2 in this.dgvUser.SelectedRows)
            {
                row2.Cells[0].Value = true;
            }
        }
        private void setupLanguage()
        {
            this.Text = "Input facebook account";
            label3.Text = "Category";
            btnInput.Text = "Input file";
            label9.Text = "1 LD save";
            label10.Text = "Accounts";
            bunifuFlatButton1.Text = "Save Account";
            label1.Text = "Category LD";
            groupBox1.Text = "List of Accounts";
            groupBox2.Text = "Choose device LD";
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.updatePassword));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        private void updatePassword()
        {
            try
            {
                int total = (int)numMax.Value;

                List<DataGridViewRow> list_acc = new List<DataGridViewRow>();

                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                DataTable check = new DataTable();

                foreach (DataGridViewRow dr in dgvUser.Rows)
                {

                    if ((bool)dr.Cells[0].Value)
                    {

                        Account acc = (Account)dr.Tag;
                        acc.id = dr.Cells["UID"].Value.ToString().Trim();
                        acc.Password = dr.Cells["Password"].Value.ToString().Trim();
                        // acc.privatekey = dr.Cells["clName"].Value.ToString().Trim();

                        check = dt.select(string.Format("select * from Account where id = {0}", acc.id));
                        if (check.Rows.Count == 0)
                        {
                            // nguoidung.insertAccount(acc);
                            dr.Cells["clstatus"].Value = "TK chưa lưu vào hệ thống";
                        }
                        else
                        {
                            nguoidung.updatePassword(acc);
                            dr.Cells["clstatus"].Value = "Đã cập nhật mật khẩu";
                        }
                        dr.Cells[0].Value = false;

                    }

                }

            }
            catch
            {

            }

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.updateNgaySinh));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        private void updateNgaySinh()
        {
            try
            {
                int total = (int)numMax.Value;

                List<DataGridViewRow> list_acc = new List<DataGridViewRow>();

                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                DataTable check = new DataTable();

                foreach (DataGridViewRow dr in dgvUser.Rows)
                {

                    if ((bool)dr.Cells[0].Value)
                    {

                        Account acc = (Account)dr.Tag;
                        acc.id = dr.Cells["UID"].Value.ToString().Trim();
                        //acc.Password = dr.Cells["Password"].Value.ToString().Trim();
                       // acc.birthday = dr.Cells["clName"].Value.ToString().Trim();

                        check = dt.select(string.Format("select * from Account where id = {0}", acc.id));
                        if (check.Rows.Count == 0)
                        {
                            // nguoidung.insertAccount(acc);
                            dr.Cells["clstatus"].Value = "TK chưa lưu vào hệ thống";
                        }
                        else
                        {
                            nguoidung.updateBirthday(acc);
                            dr.Cells["clstatus"].Value = "Đã cập nhật ngày sinh";
                        }
                        dr.Cells[0].Value = false;

                    }

                }

            }
            catch
            {

            }

        }
    }
}
