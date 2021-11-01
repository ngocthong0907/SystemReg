using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_DangKy : Form
    {
        public frm_DangKy()
        {
            InitializeComponent();
            txtHID.Text = SettingTool.hid;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            MessageBox.Show("chưa hỗ trợ tính năng đăng ký", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;

            CustomerTrialModel cus = new CustomerTrialModel();

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Họ tên không được phép để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Số điện thoại không được phép để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email của bạn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Mật khẩu không được phép để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtRepass.Text.Trim() != txtPass.Text.Trim())
            {
                MessageBox.Show("Vui lòng điền mật khẩu giống nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Random rd = new Random();
            cus.Name = txtName.Text.Trim();
            cus.Phone = txtPhone.Text.Trim();
            cus.Email = txtEmail.Text.Trim();
            cus.Password = txtPass.Text.Trim();
            cus.Refer = txtNguoiGioiThieu.Text.Trim();
            cus.HID = SettingTool.hid;
            cus.Random = rd.Next(100000);
            CustomerController customercontroller = new CustomerController();
            ResultRequest result = customercontroller.method_Register(cus);
            if (result.status)
            {

                MessageBox.Show(result.mess, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(result.mess, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Application.Exit();
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
