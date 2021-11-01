using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
namespace NinjaSystem
{
    public partial class frm_PostManager : Form
    {
        public frm_PostManager(List<Account> list_account)
        {
            InitializeComponent();
            this.list_account = list_account;
        }
        Thread thread_1;
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathImagePast.Text = fldrDlg.SelectedPath;
            }
        }
        List<Account> list_account = new List<Account>();
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.SetPost));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        public void SetPost()
        {
            if (string.IsNullOrEmpty(txtPathImagePast.Text))
                return;
            try
            {
                List<PostModel> list_post = new List<PostModel>();
                List<string> list_pathpic = System.IO.Directory.GetFiles(txtPathImagePast.Text.Trim(), "*.jpg").ToList();

                if (list_pathpic.Count == 0)
                    list_pathpic = System.IO.Directory.GetFiles(txtPathImagePast.Text.Trim(), "*.png").ToList();

                if (list_pathpic.Count > 0)
                {
                    List<string> list_content = File.ReadAllLines(txtPathContent.Text.Trim()).ToList();
                    if (list_content.Count > 0)
                    {
                        int minphoto = (int)numPhoto.Value;
                        int maxphoto = (int)numPhoto2.Value;
                        Random rd = new Random();
                        bool has_remove = false;
                        if (chkXoaAnh.Checked)
                            has_remove = true;
                        while (list_content.Count > 0)
                        {

                            PostModel post = new PostModel();
                            post.message = list_content[rd.Next(0, list_content.Count)];
                            post.list_path = new List<string>();
                            list_content.Remove(post.message);
                            int randomphoto = rd.Next(minphoto, maxphoto);
                            if (list_pathpic.Count > 0)
                            {
                                List<string> list_photo = new List<string>();
                                for (int i = 0; i < randomphoto; i++)
                                {
                                    if (list_pathpic.Count > 0)
                                    {
                                        string linkphoto = list_pathpic[rd.Next(0, list_pathpic.Count)];
                                        list_photo.Add(linkphoto);
                                        if (has_remove)
                                        {
                                            list_pathpic.Remove(linkphoto);
                                        }
                                    }
                                }
                                post.list_path = list_photo;
                            }
                            list_post.Add(post);
                        }

                    }
                }
                foreach (PostModel p in list_post)
                {
                    method_Datagridview(p);
                }
            }
            catch
            {

            }
           
        }
        private void method_Datagridview(PostModel post)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewCheckBoxCell check = new DataGridViewCheckBoxCell();
                check.Value = true;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = (dgvContent.Rows.Count + 1).ToString();
                dataGridViewRow.Cells.Add(cell1);

                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = post.message;
                dataGridViewRow.Cells.Add(cell2);


                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = string.Join(",", post.list_path);
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell71 = new DataGridViewTextBoxCell();
                cell71.Value = post.list_path.Count;
                dataGridViewRow.Cells.Add(cell71);

                dataGridViewRow.Tag = post;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvContent.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            var fldrDlg = new OpenFileDialog();
            fldrDlg.RestoreDirectory = true;
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathContent.Text = fldrDlg.FileName;
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            dgvContent.Rows.Clear();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Account acc in list_account)
                {
                    string pathgroup = string.Format("{0}\\Schedule\\{1}\\PostGroup", Application.StartupPath, acc.id.Trim());
                    string pathprofile = string.Format("{0}\\Schedule\\{1}\\PostProfile", Application.StartupPath, acc.id.Trim());
                    if (Directory.Exists(pathgroup) == false)
                    {
                        Directory.CreateDirectory(pathgroup);
                    }
                    if (Directory.Exists(pathprofile) == false)
                    {
                        Directory.CreateDirectory(pathprofile);
                    }
                    List<string> list_filegroup = System.IO.Directory.GetFiles(pathgroup, "*.txt").ToList();
                    List<string> list_fileprofile = System.IO.Directory.GetFiles(pathprofile, "*.txt").ToList();

                    int int_0 = list_filegroup.Count;
                    int int_1 = list_fileprofile.Count;
                    bool has_group = false;
                    if (chkGroup.Checked)
                    {
                        has_group = true;
                    }
                    else
                    {
                        has_group = false;
                    }
                    bool has_profile = false;
                    if (chkProfile.Checked)
                    {
                        has_profile = true;
                    }
                    else
                    {
                        has_profile = false;
                    }
                    NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                    foreach (DataGridViewRow dr in dgvContent.Rows)
                    {


                        PostModel post = new PostModel();
                        post = (PostModel)dr.Tag;
                        if (has_group)
                        {
                            int_0++;
                            File.WriteAllText(string.Format("{0}\\{1}.txt", pathgroup, int_0), JsonConvert.SerializeObject(post));

                        }
                        if (has_profile)
                        {
                            int_1++;
                            File.WriteAllText(string.Format("{0}\\{1}.txt", pathprofile, int_1), JsonConvert.SerializeObject(post));

                        }
                    }
                    acc.dataprofile = int_1.ToString();
                    acc.datagroup = int_0.ToString();
                    nguoidung_bll.updateDataPost(acc);
                }
                FunctionHelper.showMessage("Hoàn thành");
            }
            catch
            {

            }

        }

        private void frm_PostManager_Load(object sender, EventArgs e)
        {
            string path = string.Format("{0}\\Schedule", Application.StartupPath);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            load_Account();
        }
        public void load_Account()
        {
            foreach (Account acc in list_account)
            {
                method_DatagridviewAccount(acc);
            }

        }
        private void method_DatagridviewAccount(Account acc)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewCheckBoxCell cell0 = new DataGridViewCheckBoxCell();
                cell0.Value = true;
                dataGridViewRow.Cells.Add(cell0);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = (dgvAccount.Rows.Count + 1).ToString();
                dataGridViewRow.Cells.Add(cell1);

                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = acc.id;
                dataGridViewRow.Cells.Add(cell2);


                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = acc.name;
                dataGridViewRow.Cells.Add(cell3);

                string pathgroup = string.Format("{0}\\Schedule\\{1}\\PostGroup", Application.StartupPath, acc.id.Trim());
                string pathprofile = string.Format("{0}\\Schedule\\{1}\\PostProfile", Application.StartupPath, acc.id.Trim());
                if (Directory.Exists(pathgroup) == false)
                {
                    Directory.CreateDirectory(pathgroup);
                }
                if (Directory.Exists(pathprofile) == false)
                {
                    Directory.CreateDirectory(pathprofile);
                }
                List<string> list_fileprofile = System.IO.Directory.GetFiles(pathprofile, "*.txt").ToList();
                List<string> list_filegroup = System.IO.Directory.GetFiles(pathgroup, "*.txt").ToList();

                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = list_fileprofile.Count;
                dataGridViewRow.Cells.Add(cell4);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = list_filegroup.Count;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewButtonCell cell6 = new DataGridViewButtonCell();
                cell6.Value = "Xem Nội Dung";
                dataGridViewRow.Cells.Add(cell6);

                dataGridViewRow.Tag = acc;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvAccount.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }

        private void dgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            try
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
               e.RowIndex >= 0)
                {
                    Account acc = (Account)dgvAccount.CurrentRow.Tag;
                    string path = string.Format("{0}\\Schedule\\{1}", Application.StartupPath, acc.id.Trim());
                    Process.Start(path);
                }
            }
            catch
            {
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExcelController ex = new ExcelController();
                    List<PostModel> list_baiviet = ex.getPostExcel(dialog.FileName);
                    foreach (PostModel bv in list_baiviet)
                    {
                        if (!string.IsNullOrEmpty(bv.message))
                            method_Datagridview(bv);
                    }

                }
                catch
                {
                    MessageBox.Show("Vui lòng tải file mẫu và điền dữ liệu trong đó", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://tai.ninjateam.vn/datapost.xlsx");
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvAccount.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    string path = string.Format("{0}\\Schedule\\{1}", Application.StartupPath, acc.id);

                    if (Directory.Exists(path))
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(path);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }
                    acc.dataprofile = "0";
                    acc.datagroup = "0";
                    NguoiDung_Bll bll = new NguoiDung_Bll();
                    bll.updateDataPost(acc);
                }
                FunctionHelper.showMessage("Hoàn thành");
            }
            catch
            {
            }

        }
    }
}
