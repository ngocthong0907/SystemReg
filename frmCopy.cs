using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SharpAdbClient;
using Newtonsoft.Json.Linq;
namespace NinjaSystem
{
    public partial class frmCopy : Form
    {
        public frmCopy(List<Account> ls_acc)
        {
            lsacc = ls_acc;
            InitializeComponent();
        }
        setupAdmin setup = new setupAdmin();
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        List<Account> lsacc = new List<Account>();
        private void frmCopy_Load(object sender, EventArgs e)
        {
          
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> lscopy = new List<string>();
                for (int i = 0; i < dgvLDin.Rows.Count; i++)
                {
                    lscopy.Add(dgvLDin.Rows[i].Cells[0].Value.ToString());
                }
                if (lscopy.Count > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (Account acc in lsacc)
                    {
                        string line = "";
                        foreach (string copy in lscopy)
                        {
                            switch (copy)
                            {
                                case "UID":
                                    line += acc.id + "|";
                                    break;
                                case "email":
                                    line += acc.email + "|";
                                    break;
                                case "pass":
                                    line += acc.Password + "|";
                                    break;
                                case "key":
                                    line += acc.privatekey + "|";
                                    break;
                                case "token":
                                    line += acc.token + "|";
                                    break;
                                case "cookie":
                                    line += acc.cookies + "|";
                                    break;
                                case "birthday":
                                    line += acc.birthday + "|";
                                    break;
                            }
                        }
                        builder.AppendLine(line);
                    }
                    Clipboard.Clear();
                    Clipboard.SetText(builder.ToString());
                    MessageBox.Show("Successful");
                }

            }
            catch
            {
            }
        }
       
        private void uid_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell cell = new DataGridViewLinkCell();
            cell.Value = "UID";
            row.Cells.Add(cell);
            dgvLDin.Rows.Add(row);
            uid.Enabled = false;
        }

        private void btnkey_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell cell = new DataGridViewLinkCell();
            cell.Value = "key";
            row.Cells.Add(cell);
            dgvLDin.Rows.Add(row);
            btnkey.Enabled = false;
        }

       

        private void pass_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell cell = new DataGridViewLinkCell();
            cell.Value = "pass";
            row.Cells.Add(cell);
            dgvLDin.Rows.Add(row);
            pass.Enabled = false;
        }

        private void token_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell cell = new DataGridViewLinkCell();
            cell.Value = "token";
            row.Cells.Add(cell);
            dgvLDin.Rows.Add(row);
            token.Enabled = false;
        }

        private void birthday_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell cell = new DataGridViewLinkCell();
            cell.Value = "birthday";
            row.Cells.Add(cell);
            dgvLDin.Rows.Add(row);
            birthday.Enabled = false;
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string str = dgvLDin.CurrentRow.Cells[0].Value.ToString();

                dgvLDin.Rows.Remove(dgvLDin.CurrentRow);

                switch (str)
                {
                    case "UID":
                        uid.Enabled = true;
                        break;
                    case "email":
                        email.Enabled = true;
                        break;
                    case "birthday":
                        birthday.Enabled = true;
                        break;
                    case "token":
                        token.Enabled = true;
                        break;
                    case "pass":
                        pass.Enabled = true;
                        break;
                    case "key":
                        btnkey.Enabled = true;
                        break;
                    case "cookie":
                        cookie.Enabled = true;
                        break;
                }
            }
            catch
            {

            }
           
        }

        private void uid_Click_1(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell cell = new DataGridViewLinkCell();
            cell.Value = "UID";
            row.Cells.Add(cell);
            dgvLDin.Rows.Add(row);
            uid.Enabled = false;
        }

        private void email_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell cell = new DataGridViewLinkCell();
            cell.Value = "email";
            row.Cells.Add(cell);
            dgvLDin.Rows.Add(row);
            email.Enabled = false;
        }

        private void cookie_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewCell cell = new DataGridViewLinkCell();
            cell.Value = "cookie";
            row.Cells.Add(cell);
            dgvLDin.Rows.Add(row);
            cookie.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string str = dgvLDin.CurrentRow.Cells[0].Value.ToString();

                dgvLDin.Rows.Remove(dgvLDin.CurrentRow);

                switch (str)
                {
                    case "UID":
                        uid.Enabled = true;
                        break;
                    case "email":
                        email.Enabled = true;
                        break;
                    case "birthday":
                        birthday.Enabled = true;
                        break;
                    case "token":
                        token.Enabled = true;
                        break;
                    case "pass":
                        pass.Enabled = true;
                        break;
                    case "key":
                        btnkey.Enabled = true;
                        break;
                    case "cookie":
                        cookie.Enabled = true;
                        break;
                }
            }
            catch
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
            }
        }
       
    }
}
