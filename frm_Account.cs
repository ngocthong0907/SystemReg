using MessagingToolkit.QRCode.Codec.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class frm_Account : Form
    {
        public frm_Account(CustomerTrialModel lic)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;



        }
        public List<userLD> list_ldopen = new List<userLD>();
        CancellationTokenSource tokenSource;
        string cookies = "";
        string tokens = "";
        GroupLD_BLL group_bll = new GroupLD_BLL();
        DetailLD_BLL detail_bll = new DetailLD_BLL();
        LDController ld = new LDController();
        bool stop = false;
        object synAcc = new object();
        object synUID = new object();
        Thread thread_1;
        Thread thread_loop;
        CustomerTrialModel lic;
        Data dt = new Data();
        string userId = "";

        string email = "";
        string pass = "";
        List<LDModel> list_device = new List<LDModel>();
        List<LDModel> list_devicerunning = new List<LDModel>();

        static object syncObj = new object();

        CheckBox headerCheckBox = new CheckBox();
        SettingTuongTac tuongtac = new SettingTuongTac();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        List<string> list_uid = new List<string>();
        List<string> lsKeyword = new List<string>();
        CancellationTokenSource ts = new CancellationTokenSource();
        CancellationToken ct = new CancellationToken();

        Random rdom = new Random();
        List<int> list_tuongtac = new List<int>();
        ninjaDroidHelper droid = new ninjaDroidHelper();
        private void frmAccount_Load(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(btnmanage, "Quản lý account");
            tool.SetToolTip(btn_addUser, "Thêm Nguời dùng");
            tool.SetToolTip(btnSync, "Chuyển dữ liệu");
            tool.SetToolTip(btnreset, "Khởi động lại chương trình");
            tool.SetToolTip(btnCheckToken, "Check Live Token");

            SettingTool.lang = new Language();
            SettingTool.lang.setDataLD();
            //if (File.Exists(SettingTool.configld.pathdnconsole) == false)
            //{
            //    MessageBox.Show("Bạn chưa cấu hình đường dẫn LDPlayer! Vui lòng cài đặt trước khi sử dụng phần mềm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //}
            loadGroup();
            timer1.Interval = 60 * 60 * 1000;
            timer1.Start();
            checkLicense();
            tabControl1.TabPages.Remove(tabPage4);
        }

        private void checkLicense()
        {
            try
            {
                string path = String.Format("{0}\\login.data", Application.StartupPath);
                AccountNinja acc = new AccountNinja();
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    acc = JsonConvert.DeserializeObject<AccountNinja>(json);
                }
                Random rd = new Random();

                CustomerTrialModel cus = new CustomerTrialModel();
                cus.Email = acc.email.Trim();
                cus.Password = acc.pass.Trim();
                cus.HID = SettingTool.hid;
                cus.Refer = "Login Ninja Zalo";
                cus.Random = rd.Next(888888);
                CustomerController customercontroller = new CustomerController();
                ResultRequest result = customercontroller.method_Login(cus);
                if (result.status)
                {
                    CustomerTrialModel cusrequest = new CustomerTrialModel();
                    cusrequest = JsonConvert.DeserializeObject<CustomerTrialModel>(result.data);
                    if (cusrequest.Random == cus.Random && cusrequest.Email == cus.Email)
                    {
                        SettingTool.ntoken = cusrequest.ntoken;
                        SettingTool.key = cusrequest.privatekey;
                       // this.Text = string.Format("Ninja System Zalo - Email: {0} - {1} days - PRO Version {2}", cus.Email, cusrequest.Time, SettingTool.versiontext);
                        if (cusrequest.Time <= 3)
                        {
                            MessageBox.Show("Phiên bản dùng thử của bạn đã hết hạn.Vui lòng liên hệ Ninja Team để kích hoạt bản quyền. Hotline : 0979.090.897", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            Process.Start("http://phanmemfacebookninja.com/lien-he");
                            Application.Exit();
                        }
                        SettingTool.ninjatoken = cusrequest.Token;
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không dúng! Vui lòng kiểm tra lại");
                        Application.Exit();
                    }
                    // MessageBox.Show(result.message);
                }
                else
                {
                    MessageBox.Show(result.mess);
                    Application.Exit();
                }
            }
            catch
            {
                Application.Exit();
            }
        }
        public void loadGroup()
        {
            try
            {
                dgvGroupLD.Rows.Clear();
                //  List<LDModel> list_ld = new List<LDModel>();
                //  list_ld = ld.getLdPlay();

                //total
                List<GroupLDModel> list_group = new List<GroupLDModel>();
                list_group = group_bll.selectGroupLD();
                foreach (GroupLDModel group in list_group)
                {
                    method_DatagridviewGroupLD(group);
                }
                //dgvLD.Rows.Clear(); 
                //foreach (LDModel l in list_ld)
                //{
                //    method_DatagridviewLD(l);
                //} 

            }
            catch
            { }
        }
        private void method_DatagridviewGroupLD(GroupLDModel group)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewImageCell check = new DataGridViewImageCell();
                //   check.Value = true;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = dgvGroupLD.Rows.Count + "." + group.Name;
                dataGridViewRow.Cells.Add(cell1);

                dataGridViewRow.Tag = group;
                dataGridViewRow.Height = 50;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvGroupLD.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }

        private void method_DatagridviewLD(DetailLDModel l)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewImageCell check = new DataGridViewImageCell();
                //   check.Value = true;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                if (l.Proxy != null & l.Proxy != "")
                    cell1.Value = l.LDID + "." + l.LDName + " Proxy: " + l.Proxy;
                else
                    cell1.Value = l.LDID + "." + l.LDName;
                dataGridViewRow.Cells.Add(cell1);

                dataGridViewRow.Tag = l;
                dataGridViewRow.Height = 50;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvLD.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {

        }

        private void trvNhom_Click(object sender, EventArgs e)
        {

        }

        private void load_user_by_groupId(int id)
        {

            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            List<Account> list_acc = new List<Account>();
            list_acc = nguoidung_bll.loadUserByLDID(id);
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }

            string path = String.Format("{0}\\Config\\{1}.data", Application.StartupPath, "setupSecurity");
            if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    setupAdmin setup = JsonConvert.DeserializeObject<setupAdmin>(json);
                    if (setup.HideShow == "Y")
                    {
                        dgvUser.Columns["Password"].Visible = false;
                        copyPassToolStripMenuItem.Visible = false;
                        copyUIDPassToolStripMenuItem.Visible = false;
                        copyUIDPassPrivateKeyToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        copyPassToolStripMenuItem.Visible = true;
                        copyUIDPassToolStripMenuItem.Visible = true;
                        copyUIDPassPrivateKeyToolStripMenuItem.Visible = true;
                        dgvUser.Columns["Password"].Visible = true;
                    }
                    //if (setup.HideEmail == "Y")
                    //    dgvUser.Columns["User"].Visible = false;
                    //else
                    //    dgvUser.Columns["User"].Visible = true;

                    //if (setup.HideUid == "Y")
                    //    dgvUser.Columns["UId"].Visible = false;
                    //else
                    //    dgvUser.Columns["UId"].Visible = true;

                    //if (setup.HidePrivate == "Y")
                    //    dgvUser.Columns["PrivateKey"].Visible = false;
                    //else
                    //    dgvUser.Columns["PrivateKey"].Visible = true;

                }
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

                DataGridViewTextBoxCell cell1a = new DataGridViewTextBoxCell();
                cell1a.Value = acc.id;
                dataGridViewRow.Cells.Add(cell1a);

                DataGridViewTextBoxCell cell1b = new DataGridViewTextBoxCell();
                cell1b.Value = acc.name;
                dataGridViewRow.Cells.Add(cell1b);

                //DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                //cell2.Value = acc.email;
                //dataGridViewRow.Cells.Add(cell2);

                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = acc.Password;
                dataGridViewRow.Cells.Add(cell3);

                //DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                //cell4.Value = acc.privatekey;
                //dataGridViewRow.Cells.Add(cell4);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell5);

                //DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                //cell6.Value = acc.friend_count;
                //dataGridViewRow.Cells.Add(cell6);

                //DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                //cell7.Value = acc.group_count;
                //dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell72 = new DataGridViewTextBoxCell();
                cell72.Value = acc.tendanhmuc;
                dataGridViewRow.Cells.Add(cell72);

                DataGridViewTextBoxCell cell71 = new DataGridViewTextBoxCell();
                cell71.Value = acc.nox;
                dataGridViewRow.Cells.Add(cell71);

                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                cell8.Value = acc.TrangThai;
                dataGridViewRow.Cells.Add(cell8);
              
                DataGridViewTextBoxCell cell9 = new DataGridViewTextBoxCell();
                cell9.Value = acc.Thongbao;
                dataGridViewRow.Cells.Add(cell9);

                //DataGridViewTextBoxCell cell10 = new DataGridViewTextBoxCell();
                //cell10.Value = acc.token;
                //dataGridViewRow.Cells.Add(cell10);

                //DataGridViewTextBoxCell cell11 = new DataGridViewTextBoxCell();
                //cell11.Value = acc.cookies;
                //dataGridViewRow.Cells.Add(cell11);

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



        private void loadDanhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {


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

        private void chọnDòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {

                Account acc = (Account)row.Tag;
                if (string.IsNullOrEmpty(acc.id.Trim()))
                    list_uid.Add(acc.email.Trim());
                else
                    list_uid.Add(acc.id.Trim());

            }
            this.Tag = list_uid;
            this.Close();
        }

        private void mnu_Setup_Click(object sender, EventArgs e)
        {

            NinjaADB ninjadb = new NinjaADB();
            //  ClearMessage();
            stop = false;


            this.thread_1 = new Thread(new ThreadStart(this.setupAccount));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        public void setData()
        {
            CustomerController customer = new CustomerController();
            ResultRequest kq = customer.sendLogs("login");
            Dictionary<string, string> mydata = new Dictionary<string, string>();
            if (kq.status)
            {
                JArray jarr = JArray.Parse(kq.data);
                foreach (var item in jarr)
                {
                    mydata.Add(item["Key"].ToString(), item["Value"].ToString());
                }
            }
            SettingTool.data = mydata;
        }
        public void setupAccount()
        {
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
            pibStatus.Visible = true;
            //chon cau hinh
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    if (String.IsNullOrEmpty(acc.Device_mobile))
                    {
                        list_dr.Add(row);
                    }
                    else
                    {
                        row.Cells["Message"].Value = "Tài khoản đã được setup trước đó";
                    }
                }
            }
            //foreach (DeviceData device in list_devices)
            //{
            //    if (list_dr.Count > 0)
            //    {

            //        //get list app setup
            //        List<string> list_app = droid.getAppName(device);
            //        if (list_app.Count > 0)
            //        {
            //            foreach (string appid in list_app)
            //            {
            //                //check exit app
            //                if (nguoidung.loadUserbyApp(device.Serial, appid).Count <= 0)
            //                {
            //                    if (list_dr.Count > 0)
            //                    {

            //                        DataGridViewRow dr = list_dr[0];
            //                        Account acc = (Account)dr.Tag;
            //                        dr.Cells["Message"].Value = "Running";

            //                        dr.Cells["Message"].Value = "Setup thành công";
            //                        dr.Cells["Status"].Value = "Live";
            //                        dr.Cells["clDevice"].Value = device.Serial;
            //                        acc.TrangThai = "Live";
            //                        acc.Thongbao = "Setup thành công";
            //                        acc.Device_mobile = device.Serial;
            //                        acc.app = appid;
            //                        acc.os = device.Model;
            //                        acc.appname = checkNameApp(acc.app);
            //                        dr.Cells["clApp"].Value = acc.appname;
            //                        dr.Cells["clPhone"].Value = acc.os;

            //                        nguoidung.updateDevice(acc);
            //                        list_dr.Remove(dr);
            //                    }
            //                    else
            //                    {
            //                        break;
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}
            stopAll();
        }
        private void stopAll()
        {
            stop = true;
            this.pibStatus.Visible = false;

            if (this.thread_1 != null)
            {
                this.thread_1.Abort();
            }
        }

        private void btn_addUser_Click(object sender, EventArgs e)
        {
            frm_ImportAccountLD_PRO frm = new frm_ImportAccountLD_PRO();
            frm.Show();
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //  ClearMessage();
            NinjaADB ninjadb = new NinjaADB();
            stop = false;
            //this.thread_1 = new Thread(new ThreadStart(this.logoutLoginApp));
            //thread_1.IsBackground = true;
            //this.thread_1.Start();
            logoutLoginApp();
        }
        public void logoutLoginApp()
        {
            SettingTool.lang.setDataLD();
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
            pibStatus.Visible = true;

            List<Account> list_acc = new List<Account>();
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    list_dr.Add(row);
                    list_acc.Add(acc);
                }
            }
            if (list_acc.Count > 0)
            {
                //frm_LoginLD frm = new frm_LoginLD(this, list_acc);
                //frm.Show();
            }
            else
            {
                //foreach (DataGridViewRow dr in list_dr)
                //{
                //    if (list_dr.Count > 0)
                //    {
                //        Account acc = (Account)dr.Tag;
                //        ld.autoRunLD(acc.ldid);
                //        // if (ld.checkAppInstall(acc.ldid))
                //        if (true)
                //        {
                //            dr.Cells["Message"].Value = "Running";
                //            if (ld.checkAppCurrent(acc) == false)
                //                ld.restoreAccount(acc.ldid, acc);
                //            ld.killApp(acc.ldid, "com.facebook.katana");
                //            ld.launchex(acc.ldid, "com.facebook.katana");


                //            Thread.Sleep(10000);


                //            dr.Cells["Message"].Value = "Login";
                //            ld.check_Facebook_has_stopped(acc.ldid, acc);
                //            bool haslogin = ld.loginFacebookLD(acc);
                //            if (haslogin)
                //            {
                //                dr.Cells["Message"].Value = "Đăng nhập thành công";
                //                dr.Cells["Status"].Value = "Live";
                //                acc.TrangThai = "Live";
                //                acc.Thongbao = "Đăng nhập thành công";
                //                //  ld.backupAccount(acc.ldid, "acc" + acc.Id_account);
                //            }
                //            else
                //            {
                //                dr.Cells["Message"].Value = acc.Thongbao;
                //                acc.TrangThai = "Die";

                //            }
                //            nguoidung.updateNoti(acc);

                //        }
                //        else
                //        {
                //            dr.Cells["Message"].Value = "Chưa cài đặt App Facebook";
                //        }

                //    }
                //}
            }

            //}
            //else
            //{
            //    MessageBox.Show("Thiết bị chưa được bật.Vui lòng bật LDPlayer", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            //}
            stopAll();
        }

        public bool checkDeviceAccount(Account acc)
        {
            try
            {
                foreach (LDModel device in list_devicerunning)
                {
                    if (acc.ldid == device.id.ToString())
                        return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.openLD));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        private void openLD()
        {
            try
            {
                CancellationTokenSource tokenSource;
                tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;

                DataGridViewRow node = dgvLD.CurrentRow;
                DetailLDModel model = (DetailLDModel)node.Tag;
                string ldID = model.LDID.ToString();
                userLD u = checkExits(ldID);
                addLDToPanel(u);
                if (ld.launchSetPosion(ldID, u, token))
                {
                    u.setStatus(ldID, "Kết nối thành công LD...");
                }
                else
                {
                    //if (ld.autoRunLDSetPosition(ldID, u, stop))
                    //{
                    //    u.setStatus(ldID, "Kết nối thành công LD...");
                    //}
                    //else
                    //{
                    //    u.setStatus(ldID, "Disconnected...");
                    //    return;
                    //}
                }

            }
            catch
            { }

        }
        public bool openLD(string ldID)
        {
            try
            {
                tabControl1.SelectedIndex = 1;
                userLD controlLD = new userLD(ldID);

                pnLD.Controls.Add(controlLD);
                return true;
            }
            catch
            { }
            return false;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.closeLD));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        private void closeLD()
        {
            try
            {
                DataGridViewRow node = dgvLD.CurrentRow;
                DetailLDModel model = (DetailLDModel)node.Tag;
                ld.quit(model.LDID.ToString());
                string ldid = model.LDID.ToString();
                foreach (userLD u in list_ldopen)
                {
                    if (u.ldid == ldid)
                    {
                        this.Invoke(new MethodInvoker(delegate()
                        {
                            pnLD.Controls.Remove(u);
                            list_ldopen.Remove(u);
                            return;

                        }));
                    }

                }


            }
            catch
            { }

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.closeAllLD));
            thread_1.IsBackground = true;
            this.thread_1.Start();

        }
        private void closeAllLD()
        {
            ld.quitall();
            this.Invoke(new MethodInvoker(delegate()
            {
                pnLD.Controls.Clear();
            }));
            list_ldopen = new List<userLD>();

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.installApp));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        public void installApp()
        {
            try
            {

                string path = String.Format("{0}\\App\\Facebook.apk", Application.StartupPath);
                if (File.Exists(path))
                {
                    DataGridViewRow node = dgvLD.CurrentRow;
                    DetailLDModel model = (DetailLDModel)node.Tag;
                    if (ld.checkIsRunning(model.LDID.ToString()) == false)
                    {
                        ld.launch(model.LDID.ToString());
                        int i = 300;
                        while (i > 0)
                        {
                            if (ld.checkIsRunning(model.LDID.ToString()))
                                break;

                            Thread.Sleep(1000);
                            i--;
                        }
                    }
                    ld.installApp(model.LDID.ToString(), path);
                    string pathapk = String.Format("{0}\\app\\ADBKeyboard.apk", Application.StartupPath);
                    if (File.Exists(pathapk))
                    {
                        ld.installApp(model.LDID.ToString(), pathapk);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng thêm file Facebook.apk vào đường dẫn : " + path, "Thông Báo");
                }
            }
            catch
            { }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pibStatus.Visible = true;
            NinjaADB ninjadb = new NinjaADB();
            stop = false;
            
            this.thread_1 = new Thread(new ThreadStart(this.openAppFacebook));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        public void openAppFacebook()
        {

            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
            pibStatus.Visible = true;
            CancellationTokenSource tokenSource;
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    list_dr.Add(row);
                }
            }
            foreach (DataGridViewRow dr in list_dr)
            {
                if (list_dr.Count > 0)
                {
                    Account acc = (Account)dr.Tag;

                    string ldID = acc.ldid;
                    userLD u = checkExits(ldID);
                    addLDToPanel(u);
                    if (ld.launchSetPosion(ldID, u, token))
                    {
                        u.setStatus(ldID, "Kết nối thành công LD...");
                    }
                    else
                    {
                        //if (ld.autoRunLDSetPosition(ldID, u, stop))
                        //{
                        //    u.setStatus(ldID, "Kết nối thành công LD...");
                        //}
                        //else
                        //{
                        //    u.setStatus(ldID, "Disconnected...");
                        //    return;
                        //}
                    }
                    dr.Cells["Message"].Value = "Open App Zalo";
                    u.setStatus(ldID, "Open App Zalo");
                    if (ld.checkAppCurrent(acc) == false)
                        ld.restoreAccount(acc.ldid, acc);
                    ld.killApp(acc.ldid, "com.zing.zalo");
                    dr.Cells["Message"].Value = "Open Zalo";
                  //  ld.launchex(acc.ldid, "com.zing.zalo");
                    if (ld.checkIsLogin(acc))
                    {
                        dr.Cells["Status"].Value = "Live";
                        acc.TrangThai = "Live";
                        acc.Thongbao = "Đăng nhập thành công";                     
                    }
                    else
                    {
                        dr.Cells["Status"].Value = "Die";
                        acc.TrangThai = "Die";
                    }
                    u.setStatus(ldID, "Hoàn thành mở app Zalo");
                    dr.Cells["Message"].Value = "";
                    nguoidung.updateNoti(acc);
                }
            }

            stopAll();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            //frmPostLD frmadd = new frmPostLD(list_acc, this);
            //frmadd.Show();
        }


        public void logoutApp()
        {
            pibStatus.Visible = true;
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
            pibStatus.Visible = true;
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    list_dr.Add(row);
                }
            }
            foreach (DataGridViewRow dr in list_dr)
            {
                if (list_dr.Count > 0)
                {
                    Account acc = (Account)dr.Tag;
                    string ldID = acc.ldid;
                    userLD u = checkExits(ldID);
                    addLDToPanel(u);
                    if (ld.launchSetPosion(ldID, u, token))
                    {
                        u.setStatus(ldID, "Kết nối thành công LD...");
                    }
                    else
                    {
                        //if (ld.autoRunLDSetPosition(ldID, u, stop))
                        //{
                        //    u.setStatus(ldID, "Kết nối thành công LD...");
                        //}
                        //else
                        //{
                        //    u.setStatus(ldID, "Disconnected...");
                        //    return;
                        //}
                    }
                    dr.Cells["Message"].Value = "Open App Zalo";
                    u.setStatus(ldID, "Open App Zalo");
                    if (ld.checkAppCurrent(acc) == false)
                        ld.restoreAccount(acc.ldid, acc);

                    ld.killApp(acc.ldid, "com.zing.zalo");
                    dr.Cells["Message"].Value = "Open Zalo";
                   // ld.launchex(acc.ldid, "com.zing.zalo");
                    if (ld.checkIsLogin(acc))
                    {
                        dr.Cells["Status"].Value = "Live";
                        dr.Cells["Status"].Value = "Logout Zalo...";
                        u.setStatus(ldID, "Logout Zalo...");
                        ld.logoutLD(acc);
                        u.setStatus(ldID, "Logout success...");
                        dr.Cells["Status"].Value = "Logout success...";
                    }
                    else
                    {
                        dr.Cells["Status"].Value = "Die";
                        acc.TrangThai = "Die";
                    }
                }
            }
            stopAll();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                dgvUser.Rows.Clear();
                foreach (DataGridViewRow node in dgvLD.SelectedRows)
                {
                    DetailLDModel model = (DetailLDModel)node.Tag;

                    load_user_by_groupId(model.LDID);

                }

                tabControl1.SelectedTab = tabControl1.TabPages[0];
            }
            catch
            {

            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.addNewLD));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        public void addNewLD()
        {
            ld.add();
            loadGroup();
        }

        private void cloneLDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.copyLD));
            thread_1.IsBackground = true;
            this.thread_1.Start();

        }
        public void copyLD()
        {
            try
            {
                List<LDModel> list_ld = new List<LDModel>();
                list_ld = ld.getLdPlay();
                DataGridViewRow node = dgvLD.CurrentRow;
                DetailLDModel model = (DetailLDModel)node.Tag;
                ld.copy("LD-" + list_ld.Count, model.LDID.ToString());
            }
            catch { }

        }

        private void deleteLDPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa LD này không? Xóa có thể mất tài khoản Zalo được setup", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DataGridViewRow node = dgvLD.CurrentRow;
                    DetailLDModel model = (DetailLDModel)node.Tag;
                    ld.remove(model.LDID.ToString());
                    detail_bll.delete(model);
                    loadGroup();
                }
                catch { }
            }

        }

        private void kếtBạnToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void mnu_Run_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            //frm_TuongTacLD frmadd = new frm_TuongTacLD(list_acc, this);

            //frmadd.Show();
        }

        private void pasteGhiChúToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                foreach (DataGridViewRow dr in dgvUser.SelectedRows)
                {

                    Account acc = (Account)dr.Tag;

                    RequestParams para = new RequestParams();
                    RequestParams para_where = new RequestParams();

                    para_where["Id_account"] = acc.Id_account.ToString();
                    para["Nox"] = clipboardText.Trim();
                    if (dt.update(para, "Account", para_where))
                        dr.Cells["clNote"].Value = clipboardText;
                }
            }
            catch
            {

            }
        }




        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    if (string.IsNullOrEmpty(acc.id))
                        builder.AppendLine(acc.email);
                    else
                        builder.AppendLine(acc.id);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {
            }
        }

        private void copyPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    builder.AppendLine(acc.Password);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {
            }
        }

        private void copyUIDPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    builder.AppendLine(acc.id + "|" + acc.Password);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {
            }
        }

        private void copyPrivateKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    builder.AppendLine(acc.privatekey);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {
            }
        }

        private void copyUIDPassPrivateKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    builder.AppendLine(acc.id + "|" + acc.Password + "|" + acc.privatekey);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingTool.lang.setDataLD();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            SyncAccount frm = new SyncAccount();
            frm.ShowDialog();

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frm_Config frm = new frm_Config();
            // frm.AccessibleDescription = userId;
            frm.ShowDialog();

        }

        private void btnRecycleBin_Click(object sender, EventArgs e)
        {
            frm_ListDelete frm = new frm_ListDelete();
            frm.ShowDialog();
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            string[] keySearch = txtSearch.Text.ToString().Split(',');
            string where = "";

            if (keySearch.Length > 0)
            {
                for (int i = 0; i < keySearch.Length; i++)
                {
                    if (i == 0)
                        where += string.Format("Where id like '%{0}%' or id like '%{0}%' ", keySearch[i]);
                    else
                        where += string.Format("or id like '%{0}%' or id like '%{0}%' ", keySearch[i]);
                }
            }
            dgvUser.Rows.Clear();
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            List<Account> list_acc = new List<Account>();
            list_acc = nguoidung_bll.loadUserbySql(string.Format("select * from Account {0}", where));
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }
        }


        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            try
            {
                //DataGridViewRow dr = dgvUser.CurrentRow;
                //Account acc = (Account)dr.Tag;

                //ld.caturehtml(acc.ldid);

               // ld.uiautomator(dgvUser.CurrentRow.Cells["clPhone"].Value.ToString());
            }
            catch { }


        }

        private void mnuCancelReques_Click(object sender, EventArgs e)
        {

        }

        private void mnuUser_Opening(object sender, CancelEventArgs e)
        {

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            // checkLicense();
            //SettingTool.lang.setDataLD();
        }

        private void copyCookieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //NinjaADB ninjadb = new NinjaADB();
            // stop = false;

            //this.thread_1 = new Thread(new ThreadStart(this.copyCookie));
            //thread_1.IsBackground = true;
            //this.thread_1.Start();
            copyCookie();

        }
        private void copyCookie()
        {
            cookies = "";
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {

                Account acc = (Account)dr.Tag;
                if (string.IsNullOrEmpty(acc.cookies))
                    Thread.Sleep(100);//copyTokenCookieLive(dr);
                else
                {
                    cookies += "\n" + acc.cookies;
                }
            }
            if (!string.IsNullOrEmpty(cookies))
            {
                Clipboard.Clear();
                Clipboard.SetText(cookies);
            }
            else
            {

            }
        }



        private void renameAllLDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đổi toàn bộ tên LD theo Số Thứ Tự?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DetailLDModel ldmodel = new DetailLDModel();
                List<LDModel> list_ld = new List<LDModel>();
                list_ld = ld.getLdPlay();
                foreach (LDModel model in list_ld)
                {
                   // ld.rename(model.id.ToString());
                    //thoong update
                    ldmodel = detail_bll.selectOneDetailLD(model.id);
                    ldmodel.LDName = model.name;
                    detail_bll.update(ldmodel);
                }
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            loadGroup();
        }

        private void selectGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadLD();

        }
        private void loadLD()
        {
            try
            {
                dgvLD.Rows.Clear();
                dgvUser.Rows.Clear();
                tabControl2.SelectedIndex = 1;
                foreach (DataGridViewRow dr in dgvGroupLD.SelectedRows)
                {
                    GroupLDModel group = (GroupLDModel)dr.Tag;
                    List<DetailLDModel> list_detail = new List<DetailLDModel>();
                    list_detail = detail_bll.selectDetailLD(group.GroupLDID);
                    foreach (DetailLDModel l in list_detail)
                    {
                        method_DatagridviewLD(l);
                        load_user_by_groupId(l.LDID);
                    }
                }

                //total 
            }
            catch
            { }
        }


        private void dgvGroupLD_DoubleClick(object sender, EventArgs e)
        {
            loadLD();
        }

        private void dgvLD_Click(object sender, EventArgs e)
        {
            loadAccountClick();
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }
        private void loadAccountClick()
        {
            try
            {
                dgvUser.Rows.Clear();
                DataGridViewRow node = dgvLD.CurrentRow;
                DetailLDModel model = (DetailLDModel)node.Tag;

                load_user_by_groupId(model.LDID);

            }
            catch
            {

            }

        }
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.closeAllLD));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            //frm_TuongTacLD frmadd = new frm_TuongTacLD(list_acc, this);

            //frmadd.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            loadLD();
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            //frm_TuongTacLD frmadd = new frm_TuongTacLD(list_acc, this);

            //frmadd.Show();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
           // frm_GroupLD frm = new frm_GroupLD();
           // frm.Show();
        }

        private void copyTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //NinjaADB ninjadb = new NinjaADB();
            //stop = false;
            //this.thread_1 = new Thread(new ThreadStart(this.copyToken));
            //thread_1.IsBackground = true;
            //this.thread_1.Start();

            copyToken();


        }
        private void copyToken()
        {
            tokens = "";
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                if (string.IsNullOrEmpty(acc.token))
                    Thread.Sleep(100);//copyTokenCookieLive(dr);
                else
                {
                    tokens += "\n" + acc.token;

                }
            }
            if (!string.IsNullOrEmpty(tokens))
            {
                Clipboard.Clear();
                Clipboard.SetText(tokens);
                MessageBox.Show("Hoàn thành");
            }

        }
        private void copyTokenCookieLive(DataGridViewRow dr)
        {
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
            ClearMessage();
            if ((bool)dr.Cells[0].Value)
            {
                Account acc = (Account)dr.Tag;
                dr.Cells["Message"].Value = "Run LD";
                if (!ld.autoRunLD(acc.ldid))
                {
                    MessageBox.Show("Không kết nối được với LD: " + acc.ldid);
                    return;
                }

                dr.Cells["Message"].Value = "Open App Zalo";
                if (ld.checkAppCurrent(acc) == false)
                    ld.restoreAccount(acc.ldid, acc);

              //  ld.launchex(acc.ldid, "com.zing.zalo");
                Thread.Sleep(6000);
                if (ld.checkIsLogin(acc))
                {
                    dr.Cells["Status"].Value = "Live";
                    ld.copyfileToken(acc.ldid);
                    string path = string.Format("{0}\\XuanZhi\\Pictures\\temp\\{1}", SettingTool.configld, acc.ldid + ".txt");
                    if (File.Exists(path))
                    {
                        string html = File.ReadAllText(path);
                        string uid = FunctionHelper.smethod_6(html, html.IndexOf("EAAAAUa"), "\\").Trim();
                        string code = Regex.Match(uid, @"([A-Z])\w+").Value;

                        if (string.IsNullOrEmpty(code))
                            dr.Cells["Message"].Value = "Không copy được ";
                        else
                        {
                            acc.token = code;

                        }
                        string cookie = "";
                        string[] arr = html.Split('[');
                        if (arr.Length > 1)
                        {
                            string data = "[" + arr[1];
                            JArray jarr = JArray.Parse(data);

                            foreach (var item in jarr)
                            {
                                cookie = String.Format("{0};{1}={2}", cookie, item["name"].ToString(), item["value"].ToString());
                            }
                            cookie = cookie.Remove(0, 1);
                            acc.cookies = cookie;
                        }
                        dr.Cells["token"].Value = code;
                        dr.Cells["Cookies"].Value = cookie;
                        dr.Cells["Message"].Value = "Hoàn thành";
                        nguoidung.updateTokenCookie(acc);

                        this.Invoke(new MethodInvoker(delegate()
                        {
                            Clipboard.SetText(code + "\n " + cookie);
                        }));
                    }
                }
                else
                {
                    dr.Cells["Status"].Value = "Die";
                    dr.Cells["Message"].Value = "Chưa đăng nhập. Không copy được";
                }

            }
        }

        private void sapwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ld.sortLD();
        }

        private void openAllLDToolStripMenuItem_Click(object sender, EventArgs e)
        {

         //   frm_openAllLD frm = new frm_openAllLD();
           // frm.ShowDialog();
            int delay = 20;
            //if (frm.AccessibleDescription != null)
            //    delay = int.Parse(frm.AccessibleDescription);
            openAllLD(delay);
        }
        private void openAllLD(int delay)
        {
            try
            {
                dgvLD.Rows.Clear();
                dgvUser.Rows.Clear();
                tabControl2.SelectedIndex = 1;
                foreach (DataGridViewRow dr in dgvGroupLD.SelectedRows)
                {
                    //
                    GroupLDModel group = (GroupLDModel)dr.Tag;
                    List<DetailLDModel> list_detail = new List<DetailLDModel>();
                    list_detail = detail_bll.selectDetailLD(group.GroupLDID);
                    foreach (DetailLDModel l in list_detail)
                    {
                        method_DatagridviewLD(l);
                        load_user_by_groupId(l.LDID);
                        ld.launch(l.LDID.ToString());
                        Thread.Sleep(delay * 1000);
                    }
                }

                ld.sortLD();
                //total 
            }
            catch
            { }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            //frm_GroupLD frm = new frm_GroupLD();
            //frm.Show();
        }

        private void mnuProxy_Click(object sender, EventArgs e)
        {
            //DataGridViewRow node = dgvLD.CurrentRow;
            //frmProxyInfo frmProxy = new frmProxyInfo();
            //frmProxy.Tag = (DetailLDModel)node.Tag;
            //frmProxy.ShowDialog();


            List<DetailLDModel> list_ld = new List<DetailLDModel>();
            foreach (DataGridViewRow dr in dgvLD.SelectedRows)
            {
                DetailLDModel ld = (DetailLDModel)dr.Tag;
                list_ld.Add(ld);
            }


            //frmProxyInfo frm = new frmProxyInfo(list_ld, this);

            //frm.ShowDialog();

        }

        private void mnuJoinGroup_Click(object sender, EventArgs e)
        {
            //List<Account> list_acc = new List<Account>();
            //foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            //{
            //    Account acc = (Account)dr.Tag;
            //    list_acc.Add(acc);
            //}
            //frm_JoinGroupManualLD frmadd = new frm_JoinGroupManualLD(list_acc);
            //frmadd.Show();
        }
        private void ClearMessage()
        {
            //for (int i = 0; i < dgvUser.SelectedRows.Count; i++)
            //{
            //    dgvUser.Rows[i].Cells["Message"].Value = "";

            //}
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                row.Cells["Message"].Value = "";
            }
        }

        private void mnuChangceAcc_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }

            frm_ImportAccountLD_PRO frmadd = new frm_ImportAccountLD_PRO(list_acc);

            frmadd.Show();
        }

        private void checkInfoUIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.method_CheckInfoUID));
            this.thread_1.Start();
        }
        private void method_CheckInfoUID()
        {
            try
            {
                Profile_Controller profile = new Profile_Controller();
                Account accchinh = new Account();
                string dtsg = profile.checkCookies(SettingTool.configld.cookies);
                if (dtsg != null)
                {
                    foreach (DataGridViewRow dr in dgvUser.SelectedRows)
                    {
                        if ((bool)dr.Cells[0].Value)
                        {
                            setColorRow(dr, Color.Yellow);
                            Account acc = (Account)dr.Tag;
                            if (profile.LoadInfo2(SettingTool.configld.cookies, dtsg, acc, null))
                            {
                                dr.Cells["Status"].Value = "Live";
                                dr.Cells["clName"].Value = acc.name;
                                dr.Cells["clFriend"].Value = acc.friend_count;
                                dr.Cells["clGroup"].Value = acc.group_count;
                                acc.TrangThai = "Live";
                                acc.Thongbao = "Đăng nhập thành công";
                                dr.Cells["Message"].Value = "Hoàn thành check";

                            }
                            else
                            {
                                dr.Cells["Status"].Value = "Die";
                                acc.TrangThai = "Live";
                                acc.Thongbao = "Check UID fail";
                                dr.Cells["Message"].Value = "Không check thành công";
                            }
                            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                            nguoidung.updateName(acc);
                            setColorRow(dr, Color.White);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Cookies Die vui lòng thêm cookies trung gian mới trong mục Cấu hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

                }
            }
            catch
            {

            }

        }
        private void setColorRow(DataGridViewRow dataGridViewRow_0, Color color_0)
        {
            Class34 class2 = new Class34
            {
                dataGridViewRow_0 = dataGridViewRow_0,
                color_0 = color_0
            };
            this.Invoke(new MethodInvoker(class2.method_0));
        }
        [CompilerGenerated]
        private sealed class Class34
        {
            public Color color_0;
            public DataGridViewRow dataGridViewRow_0;

            public void method_0()
            {
                this.dataGridViewRow_0.DefaultCellStyle.BackColor = this.color_0;
            }
        }





        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }

            frm_ImportAccountLD_PRO frmadd = new frm_ImportAccountLD_PRO(list_acc);

            frmadd.Show();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            List<DetailLDModel> list_detail = new List<DetailLDModel>();
            foreach (DataGridViewRow dr in dgvLD.SelectedRows)
            {
                DetailLDModel model = (DetailLDModel)dr.Tag;
                list_detail.Add(model);
            }
            if (list_detail.Count > 0)
            {
                frm_MoveGroupLD frm = new frm_MoveGroupLD(list_detail);
                frm.ShowDialog();
                loadLD();
            }

        }

        private void btn_config_devices_Click(object sender, EventArgs e)
        {
            string message = "Tính năng này giúp máy tính kết nối với LDPlayer. \r\n Trong trường hợp tự động bị thất bại. \r\n Bạn có tiếp tục thực hiện?";
            string caption = "Thông báo";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                droid.startAdbserver("adb kill-server && adb start-server");

                MessageBox.Show("Đã hoàn thành!");
            }
        }

        private void mnuDeleteAcc_Click(object sender, EventArgs e)
        {
            Data dt = new Data();
            if (MessageBox.Show("Bạn có chắc chắn muốn xoá tài khoản ra khỏi hệ thống không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    if ((bool)row.Cells[0].Value)
                    {
                        RequestParams para_where = new RequestParams();
                        Account acc = (Account)row.Tag;
                        para_where["Id_account"] = acc.Id_account.ToString();
                        dt.delete("Account", para_where);

                    }
                }
                loadAccountClick();
            }
        }

        private void mnuRemoveAcc_Click(object sender, EventArgs e)
        {
            Data dt = new Data();
            if (MessageBox.Show("Bạn có chắc chắn muốn xoá tài khoản ra khỏi LD?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    if ((bool)row.Cells[0].Value)
                    {
                        RequestParams para_where = new RequestParams();
                        Account acc = (Account)row.Tag;

                        RequestParams para = new RequestParams();
                        para["LdId"] = "";

                        para_where["Id_account"] = acc.Id_account.ToString();
                        dt.update(para, "Account", para_where);

                    }
                }
                loadAccountClick();
            }
        }



        private void copyEmailPassPrivateKey_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    builder.AppendLine(acc.email + "|" + acc.Password + "|" + acc.privatekey);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {

            }
        }



        private void mnuUpdateAccount_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dgvUser.CurrentRow;
                Account acc = (Account)dr.Tag;
                frm_AddUser frm = new frm_AddUser(acc, "Update");

                userId = acc.Id_account.ToString();
                frm.AccessibleDescription = "Update$" + userId;
                frm.ShowDialog();

                loadGroup();
            }
        }

        private void mnuShowHide_Click(object sender, EventArgs e)
        {
            //frm_confirm frm = new frm_confirm();
            //frm.ShowDialog();
            //loadGroup();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            bunifuImageButton2.Enabled = true;
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                setColorRow(dr, Color.White);
            }
            stopAll();
        }

        private void btnmanage_Click(object sender, EventArgs e)
        {
            frm_ManageAccountLd frm = new frm_ManageAccountLd();
            frm.Show();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void dgvUser_SelectionChanged(object sender, EventArgs e)
        {
            lblcountAcc.Text = dgvUser.SelectedRows.Count.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            userLD controlLD = new userLD("1");

            pnLD.Controls.Add(controlLD);
            // controlLD.runLD();

        }
        public userLD checkExits(string ldID)
        {
            userLD user = new userLD(ldID);
            foreach (userLD u in list_ldopen)
            {
                if (u.ldid == ldID)
                {
                    user = u;
                    return u;
                }
            }
            return user;
        }
        public void addLDToPanel(userLD u)
        {
            bool has_exits = false;
            foreach (userLD user in list_ldopen)
            {
                if (user.ldid == u.ldid)
                {
                    has_exits = true;
                    break;
                }
            }

            this.Invoke(new MethodInvoker(delegate()
            {
                if (has_exits == false)
                {
                    tabControl1.SelectedIndex = 1;
                    pnLD.Controls.Add(u);
                    list_ldopen.Add(u);
                }

            }));
        }
        public void removeLDToPanel(userLD u)
        {
            if (u != null)
            {
                //  pnLD.SuspendLayout();
                u.Dispose();
                pnLD.Controls.Remove(u);

                list_ldopen.Remove(u);
                // int i= pnLD.Controls.Count;
                // pnLD.ResumeLayout(false);
                //foreach (Control c in pnLD.Controls)
                //{

                //}
            }
            //foreach (Control c in pnLD.Controls)
            //{
            //    try
            //    {
            //        userLD user = (userLD)c;
            //        if (user.ldid == u.ldid)
            //        {
            //            try
            //            {
            //                if (!u.p.WaitForExit(2000))
            //                {
            //                    if (!u.p.HasExited)
            //                        u.p.Kill();
            //                }
            //            }
            //            catch
            //            { }

            //            pnLD.Controls.Remove(c);
            //            list_ldopen.Remove(u);
            //            return;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": Lỗi clear :" + ex.Message + "\n");
            //    }

            //}
            //   pnLD.Controls.Remove(u);
            //   list_ldopen.Remove(u);
        }



        private void btnCheckToken_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.checkLiveToken));
            thread_1.IsBackground = true;
            this.thread_1.Start();


        }
        public void checkLiveToken()
        {
            List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                list_dr.Add(dr);
            }
        Lb_Start:
            int numthread = 5;
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
                            DataGridViewRow dr = null;
                            lock (synDevice)
                            {
                                dr = list_dr[0];
                                list_dr.Remove(dr);

                            }
                            checkLiveToken(dr);

                        }
                    });

                }
                Task.WaitAll(tasks);
                if (list_dr.Count > 0)
                    goto Lb_Start;
            }
        }
        private void checkLiveToken(DataGridViewRow dr)
        {
            try
            {
                setColorRow(dr, Color.Yellow);
                Account acc = (Account)dr.Tag;

                Profile_Controller profile = new Profile_Controller();
                if (string.IsNullOrEmpty(acc.token))
                {
                    dr.Cells["Message"].Value = "Không có token";
                }
                else
                {
                    bool has_live = profile.LoadInfoToken(acc, "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-G965N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]");
                    //   bool has_live = profile.getInfoToken(acc, "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-G965N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]");
                    if (has_live)
                    {
                        dr.Cells["clGroup"].Value = acc.group_count;
                        dr.Cells["clFriend"].Value = acc.friend_count;
                        dr.Cells["Status"].Value = "Live";

                        acc.TrangThai = "Live";

                    }
                    else
                    {
                        NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                        acc.TrangThai = "Die";
                        dr.Cells["Status"].Value = "Die";
                        nguoidung_bll.updateStatus(acc);
                    }

                    dr.Cells["Message"].Value = "Kiểm tra token hoàn thành";
                }

                setColorRow(dr, Color.White);
            }
            catch
            { }
        }

        private void copy2FACodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.method_GetCode));
            this.thread_1.Start();
        }
        private void method_GetCode()
        {
            try
            {
                Account acc = (Account)dgvUser.CurrentRow.Tag;
                if (!string.IsNullOrEmpty(acc.privatekey))
                {
                    CustomerController control = new CustomerController();
                    TwoFaModel kq = control.getCodeTwofa(acc.email, acc.privatekey);
                    if (kq.status)
                    {
                        this.Invoke(new MethodInvoker(delegate()
                        {

                            Clipboard.SetText(kq.message.Trim());
                            MessageBox.Show("Đã copy Two-Fa Code: " + kq.message.Trim(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }));
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản của bạn chưa bật Two-Fa trên phần mềm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            catch
            {
                MessageBox.Show("Không thể get code Two-FA cho tài khoản này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }



        private void mnuLogoutIn_Click(object sender, EventArgs e)
        {
            //  ClearMessage();
            NinjaADB ninjadb = new NinjaADB();
            stop = false;
            //this.thread_1 = new Thread(new ThreadStart(this.logoutLoginApp));
            //thread_1.IsBackground = true;
            //this.thread_1.Start();

            logoutLoginApp();
        }

        private void mnulogout_Click(object sender, EventArgs e)
        {
            NinjaADB ninjadb = new NinjaADB();
            stop = false;

            this.thread_1 = new Thread(new ThreadStart(this.logoutApp));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }

        private void mnuloginMessenger_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            //LoginMessenger frmadd = new LoginMessenger(list_acc, this);

            //frmadd.Show();
        }



        private void ẩnHiểnCộtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_confirm frm = new frm_confirm();
            frm.ShowDialog();
            loadGroup();
        }

        private void pasteGhiChúToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                foreach (DataGridViewRow dr in dgvUser.SelectedRows)
                {

                    Account acc = (Account)dr.Tag;

                    RequestParams para = new RequestParams();
                    RequestParams para_where = new RequestParams();

                    para_where["Id_account"] = acc.Id_account.ToString();
                    para["Nox"] = clipboardText.Trim();
                    if (dt.update(para, "Account", para_where))
                        dr.Cells["clNote"].Value = clipboardText;
                }
            }
            catch
            {

            }
        }

        private void gánKey1111ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DetailLDModel> list_ld = new List<DetailLDModel>();
            foreach (DataGridViewRow dr in dgvLD.SelectedRows)
            {
                DetailLDModel ld = (DetailLDModel)dr.Tag;
                list_ld.Add(ld);
            }


            //frm_SetVPN1111 frm = new frm_SetVPN1111(list_ld, this);

            //frm.ShowDialog();
        }

        private void mnuImportContact_Click(object sender, EventArgs e)
        {
            List<DetailLDModel> list_ld = new List<DetailLDModel>();
            foreach (DataGridViewRow dr in dgvLD.SelectedRows)
            {
                DetailLDModel ld = (DetailLDModel)dr.Tag;
                list_ld.Add(ld);
            }
            //frmImportContact frm = new frmImportContact(this, list_ld);
            //frm.Show();
        }

        private void mnuFindFriend_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            //frmfindFriend frmadd = new frmfindFriend(list_acc, this);
            //frmadd.Show();
        }

        private void mnunhantin_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            //frmMessageFriend frmadd = new frmMessageFriend(list_acc, this);
            //frmadd.Show();
        }

        private void mnucreatgroup_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            //frmCreateGroup frmadd = new frmCreateGroup(list_acc, this);
            //frmadd.Show();
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            frm_ManageAccountLd frm = new frm_ManageAccountLd();
            frm.Show();
        }
      
    }
}
