using Newtonsoft.Json;
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

namespace NinjaSystem
{
    public partial class frm_Import : Form
    {
        public frm_Import()
        {
            InitializeComponent();
        }
        Data dt = new Data();
        private void btnInput_Click(object sender, EventArgs e)
        {
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
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            dt.Columns.Add("Email");
            dt.Columns.Add("Pass");
            dt.Columns.Add("Privatekey");
            int count = 1;
            foreach (string line in lines)
            {
                if (line.Contains("|"))
                {
                    string[] info = line.Split('|');
                    DataRow row = dt.NewRow();
                    if (info.Length == 3)
                    {
                        row["STT"] = count;
                        row["Email"] = info[0].ToString();
                        row["Pass"] = info[1].ToString();
                        row["Privatekey"] = info[2].ToString();
                    }
                    else
                    {
                        row["STT"] = count;
                        row["Email"] = info[0].ToString();
                        row["Pass"] = info[1].ToString();
                    }

                    dt.Rows.Add(row);
                    count++;
                }
            }
            dgvListUser.DataSource = dt;
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
                }
                else
                {
                    MessageBox.Show("Không có nội dung để Paste");
                }
            }
        }

        private void btnTao_Click(object sender, EventArgs e)
        {

            RequestParams para = new RequestParams();
            RequestParams para_where = new RequestParams();
            DataTable check = new DataTable();
            if (dgvListUser.Rows.Count > 0)
            {
                for (int i = 0; i < dgvListUser.Rows.Count; i++)
                {
                    para.Clear();
                    para_where.Clear();
                    if (dgvListUser.Rows[i].Cells["Email"].Value != null)
                        para.Add(new KeyValuePair<string, string>("Email", dgvListUser.Rows[i].Cells["Email"].Value.ToString()));
                    if (dgvListUser.Rows[i].Cells["Pass"].Value != null)
                        para.Add(new KeyValuePair<string, string>("Password", dgvListUser.Rows[i].Cells["Pass"].Value.ToString()));
                    if (dgvListUser.Rows[i].Cells["PrivateKey"].Value != null)
                        para.Add(new KeyValuePair<string, string>("PrivateKey", dgvListUser.Rows[i].Cells["PrivateKey"].Value.ToString()));
                    para.Add(new KeyValuePair<string, string>("id_danhmuc", cboNhom.SelectedValue.ToString()));
                    para.Add(new KeyValuePair<string, string>("display", "Yes"));
                    //para.Add(new KeyValuePair<string, string>("Display", "Y"));

                    check = dt.select(string.Format("select * from Account where email = {0}", dgvListUser.Rows[i].Cells["Email"].Value.ToString()));
                    if (check.Rows.Count == 0)
                        dt.insert(para, "Account");
                    else
                    {
                        para_where.Add(new KeyValuePair<string, string>("Id_account", check.Rows[0]["Id_account"].ToString()));
                        dt.update(para, "Account", para_where);
                    }
                }
            } 
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Import_Load(object sender, EventArgs e)
        {
            DataTable source = new DataTable();
            source = dt.select("select * from Danhmuc");
            cboNhom.DisplayMember = "Tendanhmuc";
            cboNhom.ValueMember = "Id_danhmuc";
            cboNhom.DataSource = source;
            
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                method_OpenFile(dialog.FileName);
            }

        }
        private void method_OpenFile(string filename)
        {
            try
            {
                string text = File.ReadAllText(filename);
                List<Account> list_account = new List<Account>();
                list_account = JsonConvert.DeserializeObject<List<Account>>(text);
                DataTable check = new DataTable();
                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                int id_danhmuc =Convert.ToInt32( cboNhom.SelectedValue.ToString());
                if (list_account.Count>0)
                {
                    foreach (Account acc in list_account)
                    {
                        acc.id_danhmuc = id_danhmuc;
                        check = dt.select(string.Format("select * from Account where email = {0}", acc.email));
                        if (check.Rows.Count == 0)
                        {
                            nguoidung.insertAccount(acc);
                        }
                        else
                        {
                            nguoidung.updateAccount(acc);
                        }
                    }
                    MessageBox.Show("Đã nhập tài khoản vào database.Vui lòng load lại danh sách tài khoản", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

                }
                
            }
            catch
            { }

        }

    }
}
