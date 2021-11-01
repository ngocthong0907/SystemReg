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
namespace NinjaSystem
{
    public partial class frm_AddUser : Form
    {
        public frm_AddUser(Account acc,string type)
        {
            InitializeComponent();
            this.acc = acc;
            this.type = type;
        }
        Data dt = new Data();
     
        Account acc;
        string type;
        private void frmAddUser_Load(object sender, EventArgs e)
        {
            DataTable source = new DataTable();
            source = dt.select("select * from danhmuc");
            cboNhom.DisplayMember = "Tendanhmuc";
            cboNhom.ValueMember = "Id_danhmuc";
            cboNhom.DataSource = source;

            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }


            if (type == "Update")
            {
                btnTao.Text = "Cập nhật";
                btnTao.Text = "Update";
                if (acc.Id_account>0)
                {
                    cboNhom.SelectedValue = acc.id_danhmuc.ToString();
                    txtUID.Text = acc.id;
                    txtEmail.Text = acc.email;  
                    txtPass.Text = acc.Password;
                    txtPrivatekey.Text = acc.privatekey;
                    txtToken.Text = acc.token;
                    txtCookie.Text = acc.cookies;
                    txtBirthday.Text = acc.birthday;
                }
            }
            else
            {
                cboNhom.SelectedValue = acc.id_danhmuc;
            }
        }
        private void setupLanguage()
        {
            label3.Text = "User Group";
            btnThoat.Text = "Cancel";
            btnTao.Text = "Add";
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
           
            RequestParams para = new RequestParams();
            RequestParams para_where = new RequestParams();
            DataTable check = new DataTable();         
            if ( txtPass.Text != "")
            {
               NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                if (type == "Update")
                {
                    acc.id = txtUID.Text.Trim();
                    acc.email = txtEmail.Text.Trim();
                    acc.Password = txtPass.Text.Trim();
                    acc.privatekey = txtPrivatekey.Text.Trim();
                    acc.token = txtToken.Text.Trim();
                    acc.cookies = txtCookie.Text.Trim();
                    acc.birthday = txtBirthday.Text.Trim();


                    
                    nguoidung_bll.updateAccount(acc);
                }
                else
                {
                    acc = new Account();
                    acc.id = txtUID.Text.Trim();
                    acc.email = txtEmail.Text.Trim();
                    acc.Password = txtPass.Text.Trim();
                    acc.privatekey = txtPrivatekey.Text.Trim();
                    acc.token = txtToken.Text.Trim();
                    acc.cookies = txtCookie.Text.Trim();
                    nguoidung_bll.insertAccount(acc);
                        
                }
            }
            this.Close();
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
      
    }
}
