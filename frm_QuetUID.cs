using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_QuetUID : Form
    {
        public frm_QuetUID(List<Account> list_acc)
        {
            InitializeComponent();
            this.list_acc = list_acc;
        }
        List<Account> list_acc;
        Thread thread_1;
        LDController ld = new LDController();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void frm_QuetUID_Load(object sender, EventArgs e)
        {
            method_LoadAccount();
        }
        private void method_LoadAccount()
        {
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }
        }
        private void method_Datagridview(Account acc)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                DataGridViewCheckBoxCell check = new DataGridViewCheckBoxCell();
                check.Value = true;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = (dgvUser.Rows.Count + 1).ToString();
                dataGridViewRow.Cells.Add(cell1);

                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = acc.id;
                dataGridViewRow.Cells.Add(cell2);

                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = acc.name;
                dataGridViewRow.Cells.Add(cell3);

                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = acc.token;
                dataGridViewRow.Cells.Add(cell4);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell5);


                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.TrangThai;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = 0;
                dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                cell8.Value = acc.Thongbao;
                dataGridViewRow.Cells.Add(cell8);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = false;
            if (checkBox1.Checked)
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

        private void dgvUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (DataGridViewRow row2 in dgvUser.SelectedRows)
                {
                    row2.Cells[0].Value = true;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.runTuongTac));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        private void runTuongTac()
        {
            List<DataGridViewRow> list_dr = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dgvUser.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    list_dr.Add(row);

                }
            }
            int max = (int)numMax.Value;
            Profile_Controller profile = new Profile_Controller();
            string gender = "All";
            if(rbAll.Checked)
            {
                gender = "All";
            }
            else
            {
                if(rbMale.Checked)
                {
                    gender = "MALE";
                }
                else
                {
                    gender = "FEMALE";
                }
            }
        Lb_quayvong:
            bool has_live = profile.checkLiveToken(SettingTool.configld.token);
            if (has_live)
            {
                tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                int numthread = SettingTool.configld.numthread;
                if (numthread > list_dr.Count)
                {
                    numthread = list_dr.Count;
                }
                if (list_dr.Count > 0)
                {

                    object synDevice = new object();
                    Task[] tasks = new Task[numthread];

                    for (int p = 0; p < numthread; p++)
                    {
                        int t = p;
                        tasks[t] = Task.Factory.StartNew(() =>
                        {
                            if (list_dr.Count > 0)
                            {
                                DataGridViewRow dr = list_dr[0];
                                list_dr.Remove(dr);
                                startQuetUID(dr, max,gender, token);
                            }
                        }, token);

                    }

                    try
                    {
                        Task.WaitAll(tasks);
                    }
                    catch
                    { }

                    if (list_dr.Count > 0)
                    {
                        goto Lb_quayvong;
                    }
                    else
                    {
                        method_Stop();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền token trung gian trong mục cấu hình", "Thông báo");

            }

        }
        public void startQuetUID(DataGridViewRow dr, int max,string gender, CancellationToken token)
        {
            Account acc = (Account)dr.Tag;

            Profile_Controller profile = new Profile_Controller();
            List<GroupFB> ls_group = profile.LoadInfoGroup(acc.token, "", acc, null);
            List<GroupFB> list_groupchoduyet = new List<GroupFB>();
            foreach (GroupFB gr in ls_group)
            {
                if (gr.status == "CAN_POST_WITHOUT_APPROVAL")
                {
                    list_groupchoduyet.Add(gr);
                }
            }
            if (list_groupchoduyet.Count > 0)
            {
                int total = 0;
                string next_page = "";
                string pathsave = Application.StartupPath + "\\uid\\" + acc.id.ToString() + ".txt";
                foreach (GroupFB gr in list_groupchoduyet)
                {
                    if (gr.status == "CAN_POST_WITHOUT_APPROVAL")
                    {
                        dr.Cells["Message"].Value = "Đang quét Group : " + gr.id;
                    Lb_Next:
                        List<string> list_uid = profile.scanMemberGroup(gr.id, SettingTool.configld.token, gender, ref next_page);
                        if (list_uid.Count > 0)
                        {
                            total = total + list_uid.Count;
                            dr.Cells["clTotal"].Value = total;
                            File.AppendAllLines(pathsave, list_uid);
                            if (total >= max)
                            {
                                break;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(next_page) == false)
                                {
                                    goto Lb_Next;
                                }
                            }
                        }


                    }
                }
                if (total > 0)
                {
                    NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                    acc.pathUID = pathsave;
                    dr.Cells["Message"].Value = "Đã lưu UID tại : " + pathsave;
                    nguoidung_bll.updatePathUID(acc);
                }
            }
            else
            {
                dr.Cells["Message"].Value = "Không có group ko kiểm duyệt";
            }
        }
        private void method_Stop()
        {
            if (thread_1 != null)
                thread_1.Abort();
            tokenSource.Cancel();
        }
    }
}
