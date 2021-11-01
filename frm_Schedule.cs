using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NinjaSystem.Model;

namespace NinjaSystem
{
    public partial class frm_Schedule : Form
    {
        public frm_Schedule(List<Account> ls_id, int i_idConfig, string i_nameConfig, int i_type)
        {
            InitializeComponent();
            ls_acc = ls_id;
            type = i_type;
            idconfig = i_idConfig;
            nameconfig = i_nameConfig;
        }
        List<Account> ls_acc;
        int type = 0;
        int idconfig = 0;
        string nameconfig = "";
        string flag = "";

        private void btnPathReaction_Click(object sender, EventArgs e)
        {
            List<string> listHour = new List<string>();
            listHour = txtHouse.Lines.ToList();
            if (!listHour.Contains(dtpHour.Text))
            {
                listHour.Add(dtpHour.Text);
                txtHouse.Lines = listHour.ToArray();
            }
        }
        private void method_Config()
        {
            try
            {
                cboConfig.Items.Clear();
                List<CauHinh> list_danhmuc = new List<CauHinh>();
                CauHinh_Bll cauhinh_bll = new CauHinh_Bll();
                list_danhmuc = cauhinh_bll.selectAll();
                ComboboxItem item = new ComboboxItem();

                foreach (CauHinh dm in list_danhmuc)
                {
                    item = new ComboboxItem();
                    item.Text = dm.Name;
                    item.Tag = dm;
                    cboConfig.Items.Add(item);
                }
            }
            catch
            { }
        }

        private void btnSelectAcc_Click(object sender, EventArgs e)
        {
            CustomerTrialModel lic = new CustomerTrialModel();
            frm_Account frm = new frm_Account(lic);
            frm.ShowDialog();
            List<string> ls = (List<string>)frm.Tag;
            if (ls != null)
            {
                txtAccounts.Clear();
                txtAccounts.Lines = ls.ToArray();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAccounts.Text) & cboType.SelectedIndex != 4)
                {
                    MessageBox.Show("Hãy chọn những tài khoản cần chạy");
                    return;
                }
                if (string.IsNullOrEmpty(txtHouse.Text))
                {
                    MessageBox.Show("Hãy chọn giờ cần lập lịch");
                    return;
                }

                Data dt = new Data();
                string create_table = "";
                create_table = "CREATE TABLE \"Schedule\" (\"id\"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\"fromdate\"	TEXT,\"todate\"	TEXT,\"hours\"	TEXT,\"accounts\"	TEXT,\"idconfig\"	INTEGER,\"Type\"	INTEGER,\"Name\"	TEXT,\"numDay\"	INTEGER)";
                dt.method_DeleteData(create_table);
                RequestParams para = new RequestParams();
                RequestParams para_where = new RequestParams();

                CauHinh dm = new CauHinh();
                try
                {
                    ComboboxItem item2 = (ComboboxItem)cboConfig.SelectedItem;
                    dm = (CauHinh)item2.Tag;
                }
                catch
                {
                }
                para.Add(new KeyValuePair<string, string>("idConfig", dm.ID.ToString()));
                para.Add(new KeyValuePair<string, string>("fromDate", dtpFromDate.Text));
                para.Add(new KeyValuePair<string, string>("toDate", dtpToDate.Text));
                para.Add(new KeyValuePair<string, string>("accounts", txtAccounts.Text));
                para.Add(new KeyValuePair<string, string>("hours", txtHouse.Text));
                para.Add(new KeyValuePair<string, string>("name", dm.Name));
                para.Add(new KeyValuePair<string, string>("type", cboType.SelectedIndex.ToString()));

                if (flag == "insert")
                {
                    if (dt.insert(para, "Schedule"))
                    {
                        MessageBox.Show("Thêm mới thành công");
                        load_schedule();
                    }
                    else
                        MessageBox.Show("Có lỗi xảy ra, thêm mới không thành công");
                }
                else if (flag == "update")
                {
                    Schedule schecurrent = new Schedule();
                    schecurrent = (Schedule)dgvSchedule.CurrentRow.Tag;
                    para_where.Add(new KeyValuePair<string, string>("id", schecurrent.id.ToString()));

                    if (dt.update(para, "Schedule", para_where))
                    {
                        MessageBox.Show("Cập nhật thành công");
                        load_schedule();
                    }
                    else
                        MessageBox.Show("Có lỗi xảy ra, cập nhật không thành công");
                }
                Enable(false);
                btn_addUser.Enabled = true;
                btnUpdate.Enabled = true;
            }
            catch
            {

            }

        }

        private void btn_addUser_Click(object sender, EventArgs e)
        {
            flag = "insert";
            Enable(true);
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            btn_save.Enabled = true;
            //txtAccounts.Text = "";
            txtHouse.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            flag = "update";
            Enable(true);
            btn_addUser.Enabled = false;
            btnSave.Enabled = true;
            btn_save.Enabled = true;
        }

        private void frmSchedule_Load(object sender, EventArgs e)
        {
            method_Config();
            load_schedule();
            Enable(false);
            ToolTip tool = new ToolTip();
            tool.SetToolTip(btn_addUser, "Thêm lập lịch");
            tool.SetToolTip(btnXoa, "Xóa lập lịch");
            tool.SetToolTip(btnUpdate, "Sửa lập lịch");
            tool.SetToolTip(btn_save, "Lưu lập lịch");
            if (ls_acc != null)
            {
                List<string> lsacc = new List<string>();
                foreach (Account acc in ls_acc)
                {
                    lsacc.Add(acc.id);
                }
                txtAccounts.Clear();
                txtAccounts.Lines = lsacc.ToArray();
            }
            cboType.SelectedIndex = type;
            if (!string.IsNullOrEmpty(nameconfig))
                cboConfig.SelectedIndex = idconfig;
        }

        private void load_schedule()
        {
            dgvSchedule.Rows.Clear();
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            List<Schedule> list_sche = new List<Schedule>();
            list_sche = nguoidung_bll.loadSchedule();
            foreach (Schedule sche in list_sche)
            {
                method_Datagridview(sche);
            }
        }
        private void method_Datagridview(Schedule schedule)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = (dgvSchedule.Rows.Count + 1).ToString();
                dataGridViewRow.Cells.Add(cell1);
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = schedule.fromDate;
                dataGridViewRow.Cells.Add(cell2);
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = schedule.toDate;
                dataGridViewRow.Cells.Add(cell3);

                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = schedule.idConfig;
                dataGridViewRow.Cells.Add(cell4);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = schedule.hours;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = schedule.accounts;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = schedule.id;
                dataGridViewRow.Cells.Add(cell7);

                dataGridViewRow.Tag = schedule;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvSchedule.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }

        private void dgvSchedule_SelectionChanged(object sender, EventArgs e)
        {

            DataGridViewRow dr = dgvSchedule.CurrentRow;
            Schedule sche = (Schedule)dr.Tag;

            txtAccounts.Text = sche.accounts;
            txtHouse.Text = sche.hours;

            dtpFromDate.Value = DateTime.ParseExact(sche.fromDate, "dd/MM/yyyy", null);
            dtpToDate.Value = DateTime.ParseExact(sche.toDate, "dd/MM/yyyy", null);
            cboType.SelectedIndex = sche.type;
            ComboboxItem item = new ComboboxItem();
            item.Text = sche.name;
            cboConfig.Text = sche.name;
        }

        private void Enable(bool key)
        {
            dtpFromDate.Enabled = key;
            dtpToDate.Enabled = key;
            dtpHour.Enabled = key;
            txtHouse.Enabled = key;
            txtAccounts.Enabled = key;

            btnSave.Enabled = key;
            btn_save.Enabled = key;
            btnSelectAcc.Enabled = key;
            btnPathReaction.Enabled = key;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string message = "Bạn có muốn xóa lịch chạy này?";

            string caption = "Thông báo";


            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            RequestParams para = new RequestParams();
            if (result == DialogResult.Yes)
            {
                Data dt = new Data();
                if (dgvSchedule.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvSchedule.SelectedRows)
                    {
                        Schedule sche = (Schedule)row.Tag;
                        para.Clear();
                        para.Add(new KeyValuePair<string, string>("id", sche.id.ToString()));
                        if (dt.delete("schedule", para))
                        {

                            MessageBox.Show("Xóa thành công");
                            dgvSchedule.Rows.Clear();
                            load_schedule();
                        }

                    }
                }
                else
                {
                    Schedule sche = (Schedule)dgvSchedule.CurrentRow.Tag;
                    para.Clear();
                    para.Add(new KeyValuePair<string, string>("id", sche.id.ToString()));
                    if (dt.delete("schedule", para))
                    {

                        MessageBox.Show("Xóa thành công");

                        dgvSchedule.Rows.Clear();
                        load_schedule();
                    }
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAccounts.Text) & cboType.SelectedIndex != 4)
                {
                    MessageBox.Show("Hãy chọn những tài khoản cần chạy");
                    return;
                }
                if (string.IsNullOrEmpty(txtHouse.Text))
                {
                    MessageBox.Show("Hãy chọn giờ cần lập lịch");
                    return;
                }

                Data dt = new Data();
                string create_table = "";
                create_table = "CREATE TABLE \"Schedule\" (\"id\"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\"fromdate\"	TEXT,\"todate\"	TEXT,\"hours\"	TEXT,\"accounts\"	TEXT,\"idconfig\"	INTEGER,\"Type\"	INTEGER,\"Name\"	TEXT,\"numDay\"	INTEGER)";
                dt.method_DeleteData(create_table);
                RequestParams para = new RequestParams();
                RequestParams para_where = new RequestParams();

                CauHinh dm = new CauHinh();
                try
                {
                    ComboboxItem item2 = (ComboboxItem)cboConfig.SelectedItem;
                    dm = (CauHinh)item2.Tag;
                }
                catch
                {
                }
                para.Add(new KeyValuePair<string, string>("idConfig", dm.ID.ToString()));
                para.Add(new KeyValuePair<string, string>("fromDate", dtpFromDate.Text));
                para.Add(new KeyValuePair<string, string>("toDate", dtpToDate.Text));
                para.Add(new KeyValuePair<string, string>("accounts", txtAccounts.Text));
                para.Add(new KeyValuePair<string, string>("hours", txtHouse.Text));
                para.Add(new KeyValuePair<string, string>("name", dm.Name));
                para.Add(new KeyValuePair<string, string>("type", cboType.SelectedIndex.ToString()));

                if (flag == "insert")
                {
                    if (dt.insert(para, "Schedule"))
                    {
                        MessageBox.Show("Thêm mới thành công");
                        load_schedule();
                    }
                    else
                        MessageBox.Show("Có lỗi xảy ra, thêm mới không thành công");
                }
                else if (flag == "update")
                {
                    Schedule schecurrent = new Schedule();
                    schecurrent = (Schedule)dgvSchedule.CurrentRow.Tag;
                    para_where.Add(new KeyValuePair<string, string>("id", schecurrent.id.ToString()));

                    if (dt.update(para, "Schedule", para_where))
                    {
                        MessageBox.Show("Cập nhật thành công");
                        load_schedule();
                    }
                    else
                        MessageBox.Show("Có lỗi xảy ra, cập nhật không thành công");
                }
                Enable(false);
                btn_addUser.Enabled = true;
                btnUpdate.Enabled = true;
            }
            catch
            {

            }
        }

    }
}
