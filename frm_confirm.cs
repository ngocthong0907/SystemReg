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
    public partial class frm_confirm : Form
    {
        public frm_confirm()
        {
            InitializeComponent();
        }
        setupAdmin setup = new setupAdmin();
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frm_confirm_Load(object sender, EventArgs e)
        {
           string path = String.Format("{0}\\Config\\{1}.data", Application.StartupPath, "setupSecurity");
           if ( File.Exists(path) )
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    setup = JsonConvert.DeserializeObject<setupAdmin>(json);
                    if (setup.HideShow == "Y")
                        chkHideShow.Checked = true;
                    else
                        chkHideShow.Checked = false;

                    if (setup.HideEmail == "Y")
                        chkHideEmail.Checked = true;
                    else
                        chkHideEmail.Checked = false;

                    if (setup.HideUid == "Y")
                        chkHideUID.Checked = true;
                    else
                        chkHideUID.Checked = false;

                    if (setup.HidePrivate == "Y")
                        chkPrivate.Checked = true;
                    else
                        chkPrivate.Checked = false;

                    if (setup.PassSecurity == "")
                    {
                        lblFirst.Visible = true;
                        lblSecond.Visible = false;
                    }
                    else
                    {
                        lblFirst.Visible = false;
                        lblSecond.Visible = true;
                    }
                }
            }
            else
            {
                lblFirst.Visible = true;
                lblSecond.Visible = false;
            }
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                MessageBox.Show("Hãy nhập mật mã bảo vệ");
                return;
            }
            string path = String.Format("{0}\\Config\\{1}.data", Application.StartupPath, "setupSecurity");
            if (!File.Exists(path))
            {
                saveSetup(path);
                this.Close();
            }
            else
            {
                if (setup.PassSecurity == "")
                {
                    saveSetup(path);
                    this.Close();
                }
                 else if (txtPass.Text != setup.PassSecurity)
                {
                    MessageBox.Show("Bạn chưa nhập đúng mã bảo mật \r\n Hãy nhập đúng mã");
                    return;
                }
                else
                {
                    saveSetup(path);
                    this.Close();
                }
            }
        }
        private void saveSetup(string path)
        {
            setup.PassSecurity = txtPass.Text;
            if (chkHideShow.Checked)
                setup.HideShow = "Y";
            else
                setup.HideShow = "N";

            if (chkHideEmail.Checked)
                setup.HideEmail = "Y";
            else
                setup.HideEmail = "N";

            if (chkHideUID.Checked)
                setup.HideUid = "Y";
            else
                setup.HideUid = "N";

            if (chkPrivate.Checked)
                setup.HidePrivate = "Y";
            else
                setup.HidePrivate = "N";

            File.WriteAllText(path, JsonConvert.SerializeObject(setup));
        }
    }
}
