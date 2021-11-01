using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_ExportAcc_PRO : Form
    {
        public frm_ExportAcc_PRO(frm_MainLD_PRO frm_main, List<Account> list_acc)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        List<Account> list_acc;
        List<LDRun> list_ldrun = new List<LDRun>();


        object synAcc = new object();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        LDController ld = new LDController();

        //  runLDs formLD = new runLDs();
        public frm_MainLD_PRO frm_main;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void frm_LoginLD_Load(object sender, EventArgs e)
        {
            method_LoadAccount();

        }
        public void method_LoadAccount()
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

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = acc.name;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.Password;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = acc.privatekey;
                dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell7a = new DataGridViewTextBoxCell();
                cell7a.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell7a);

                DataGridViewTextBoxCell cell9 = new DataGridViewTextBoxCell();
                cell9.Value = "";
                dataGridViewRow.Cells.Add(cell9);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtpathSave.Text))
                {
                    MessageBox.Show("Chọn thư mục lưu!");
                    return;
                }
                StringBuilder builder = new StringBuilder();

                string folder = txtpathSave.Text + "\\" + dgvUser.Rows.Count.ToString() + "_Acc_" + DateTime.Now.ToShortTimeString().Replace(":", "_");

                if (Directory.Exists(folder) == false)
                {
                    Directory.CreateDirectory(folder);
                }

                string path = folder + "\\xproxy.txt";

                foreach (DataGridViewRow row in dgvUser.Rows)
                {
                    Account acc = (Account)row.Tag;
                    builder.AppendLine(acc.id + "|" + acc.Password + "|" + acc.privatekey + "|" + acc.birthday);
                    if (Directory.Exists(SettingTool.configld.pathsavedata + "\\" + acc.id))
                    {
                        string[] files = System.IO.Directory.GetFiles(SettingTool.configld.pathsavedata + "\\" + acc.id);
                        if (files.Count() > 0)
                        {
                            if (Directory.Exists(folder + "\\" + acc.id) == false)
                                Directory.CreateDirectory(folder + "\\" + acc.id);
                            foreach (string s in files)
                            {
                                string destFile = System.IO.Path.Combine(folder + "\\" + acc.id, System.IO.Path.GetFileName(s));
                                System.IO.File.Copy(s, destFile, true);
                            }
                        }
                    }
                }
                
                if (File.Exists(path) == false)
                {
                    File.WriteAllText(path, builder.ToString());
                }
                MessageBox.Show("Hoàn thành");

            }
            catch
            {
                MessageBox.Show("Không hoàn thành");
            }

        }
        xProxyController xcontroller = new xProxyController();

        private void btnInput_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtpathSave.Text = fldrDlg.SelectedPath;
            }
        }
    }
}
