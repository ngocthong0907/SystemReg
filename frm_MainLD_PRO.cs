using MessagingToolkit.QRCode.Codec.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NinjaSystem.Controller;
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
    public partial class frm_MainLD_PRO : Form
    {
        xProxyController xcontroller = new xProxyController();
        public frm_MainLD_PRO(CustomerTrialModel lic)
        {
            InitializeComponent();
           
            CheckForIllegalCrossThreadCalls = false;

            if (string.IsNullOrEmpty(lic.Note) == false)
            {
                if (lic.Note == "blacklist")
                {
                    Application.Exit();
                }
                if(lic.Note.Contains("money"))
                {
                    mnumoney.Visible = true;
                }
            }

        }
        public List<userLD> list_ldopen = new List<userLD>();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
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
        private void frm_MainLD_Load(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(btnmanage, "Quản lý account");
            tool.SetToolTip(btn_addUser, "Thêm Nguời dùng");
            tool.SetToolTip(btnSync, "Chuyển dữ liệu");
            tool.SetToolTip(btnreset, "Khởi động lại chương trình");
            tool.SetToolTip(btnCheckToken, "Check Live Token");

            SettingTool.lang = new Language();
            SettingTool.lang.setDataLD();

            DetechModel model_dump = new DetechModel();
            model_dump.parent = "video";
            model_dump.content = "com.facebook.katana/com.facebook.video.activity";
            SettingTool.lang.list_dump.Add(model_dump);

            if (File.Exists(SettingTool.pathLD) == false)
            {
                MessageBox.Show("Bạn chưa cấu hình đường dẫn LDPlayer! Vui lòng cài đặt trước khi sử dụng phần mềm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            if (string.IsNullOrEmpty(SettingTool.configld.pathsavedata))
            {
                MessageBox.Show("Bạn chưa cấu hình đường dẫn thư mục backup data!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            loadDanhMuc();
            timer1.Interval = 60 * 60 * 1000;
            timer1.Start();
            checkLicense();

            SettingTool.list_xproxy = new List<string>();
           
            if (SettingTool.configld.typeip == 7)
            {
                string path = Application.StartupPath + "\\xproxy.txt";
                if (File.Exists(path))
                {
                    SettingTool.list_xproxy = File.ReadAllLines(path).ToList();
                }
            }
            else
            {
                if (SettingTool.configld.typeip == 10)
                {
                    string path = Application.StartupPath + "\\obcproxy.txt";
                    if (File.Exists(path))
                    {
                        SettingTool.list_xproxy = File.ReadAllLines(path).ToList();
                    }
                }
            }


            PointDefault.setValue();

            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }
            try
            {
                load_user_by_groupId(-1);
                Process[] processesByName = Process.GetProcessesByName("adb");
                foreach (Process p in processesByName)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch
                    { }
                }
            }
            catch
            { }
            tabControl2.TabPages.Remove(tabPage2);
            changeIpHelper.createLDID(SettingTool.configld.numthread);
        }
        public void setupLanguage()
        {
            //main
            bunifuThinButton21.ButtonText = "Add group LD";
            tabPage2.Text = "List LDplayer";
            groupBox1.Text = "List Account";

            //menu
            loadDanhSáchToolStripMenuItem.Text = "Load Account";
            chọnDòngToolStripMenuItem.Text = "Select Account";
            toolStripMenuItem11.Text = "Move Account to another LD";
            mnu_Run.Text = "Run Reaction";
            mnuReactionUID.Text = "Interact with the UID/Page id";
            mnuPost.Text = "Post Now";
            kếtBạnToolStripMenuItem.Text = "Friend";
            mnuKetBan.Text = "Add Friend UID";
            mnuCancelRequest.Text = "Cancel Friend Request";
            mnuinvitefirend.Text = "Invite Like Fanpage/Group";
            mnuJoinGroup1.Text = "Group Interaction";
            mnuJoinGroup.Text = "Join Group UID";
            mnu_commentGroupID.Text = "Interact with the Group ID";
            mnuLeaveGroup.Text = "Leave Group";
            mnuPostInGroup.Text = "Post Group";
            cấuHìnhTàiKhoảnToolStripMenuItem.Text = "Setting Account Facebook";
            mnu_profile.Text = "Change info, password Facebook";
            mnu_2Fa.Text = "Turn on 2Fa Facebook";
            xóaAccKhỏiThiếtBịToolStripMenuItem.Text = "Delete Account";

            mnuShowHide.Text = "Other utilities";
            ẩnHiểnCộtToolStripMenuItem.Text = "Hide Column";
            pasteGhiChúToolStripMenuItem1.Text = "Paste Note";
            mnuRemoveAcc.Text = "Delete Account LD";
            mnuDeleteAcc.Text = "Delete Account Database";

            //
            label1.Text = "Number Account Selected";

            tạoBàiViếtToolStripMenuItem.Text = "Create Data Post";
            mởDataBàiViếtToolStripMenuItem.Text = "Open Data Post";
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
                cus.Refer = "Login Ninja System";
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
                      //  this.Text = string.Format("Ninja System PRO - Email: {0} - {1} days - PRO Version {2}", cus.Email, cusrequest.Time, SettingTool.versiontext);
                        if (cusrequest.Time <= 3)
                        {
                            //MessageBox.Show("Phiên bản dùng thử của bạn đã hết hạn.Vui lòng liên hệ Ninja Team để kích hoạt bản quyền. Hotline : 0979.090.897", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
        public void loadDanhMuc()
        {
            try
            {
                dgvDanhMuc.Rows.Clear();
                //total
                DanhMuc_Bll danhmuc_bll = new DanhMuc_Bll();
                List<DanhMuc> list_danhmuc = new List<DanhMuc>();
                list_danhmuc = danhmuc_bll.loadDanhMuc();
                foreach (DanhMuc group in list_danhmuc)
                {
                    method_DatagridviewGroupDanhMuc(group);
                }


            }
            catch
            { }
        }
        private void method_DatagridviewGroupDanhMuc(DanhMuc group)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewImageCell check = new DataGridViewImageCell();
                //   check.Value = true;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = dgvDanhMuc.Rows.Count + "." + group.tendanhmuc;
                dataGridViewRow.Cells.Add(cell1);

                dataGridViewRow.Tag = group;
                dataGridViewRow.Height = 50;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvDanhMuc.Rows.Add(dataGridViewRow);

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
                method_DatagridviewAcount(acc);
            }

            //string path = String.Format("{0}\\Config\\{1}.data", Application.StartupPath, "setupSecurity");
            //if (File.Exists(path))
            //{
            //    using (StreamReader r = new StreamReader(path))
            //    {
            //        string json = r.ReadToEnd();
            //        setupAdmin setup = JsonConvert.DeserializeObject<setupAdmin>(json);
            //        if (setup.HideShow == "Y")
            //        {
            //            dgvUser.Columns["Password"].Visible = false;
            //            copyPassToolStripMenuItem.Visible = false;
            //            copyUIDPassToolStripMenuItem.Visible = false;
            //            copyUIDPassPrivateKeyToolStripMenuItem.Visible = false;
            //        }
            //        else
            //        {
            //            copyPassToolStripMenuItem.Visible = true;
            //            copyUIDPassToolStripMenuItem.Visible = true;
            //            copyUIDPassPrivateKeyToolStripMenuItem.Visible = true;
            //            dgvUser.Columns["Password"].Visible = true;
            //        }
            //        if (setup.HideEmail == "Y")
            //            dgvUser.Columns["User"].Visible = false;
            //        else
            //            dgvUser.Columns["User"].Visible = true;

            //        if (setup.HideUid == "Y")
            //            dgvUser.Columns["UId"].Visible = false;
            //        else
            //            dgvUser.Columns["UId"].Visible = true;

            //        if (setup.HidePrivate == "Y")
            //            dgvUser.Columns["PrivateKey"].Visible = false;
            //        else
            //            dgvUser.Columns["PrivateKey"].Visible = true;

            //    }
            //}

        }
        private void method_DatagridviewAcount(Account acc)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewCheckBoxCell cell0 = new DataGridViewCheckBoxCell();
                cell0.Value = true;
                dataGridViewRow.Cells.Add(cell0);

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
                cell4.Value = acc.email;
                dataGridViewRow.Cells.Add(cell4);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = acc.Password;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.privatekey;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                try
                {
                    cell8.Value = Convert.ToInt32(acc.friend_count);
                }
                catch
                {
                    cell8.Value = 0;
                }

                dataGridViewRow.Cells.Add(cell8);

                DataGridViewLinkCell cell9 = new DataGridViewLinkCell();
                try
                {
                    cell9.Value = Convert.ToInt32(acc.group_count);
                }
                catch
                {
                    cell9.Value = 0;
                }

                dataGridViewRow.Cells.Add(cell9);

                DataGridViewTextBoxCell cell92 = new DataGridViewTextBoxCell();
                cell92.Value = acc.birthday;
                dataGridViewRow.Cells.Add(cell92);

                DataGridViewTextBoxCell cell93 = new DataGridViewTextBoxCell();
                cell93.Value = acc.avatar;
                dataGridViewRow.Cells.Add(cell93);

                DataGridViewTextBoxCell cell10 = new DataGridViewTextBoxCell();
                cell10.Value = acc.tendanhmuc;
                dataGridViewRow.Cells.Add(cell10);

                DataGridViewTextBoxCell cell11 = new DataGridViewTextBoxCell();
                cell11.Value = acc.dataprofile;
                dataGridViewRow.Cells.Add(cell11);

                DataGridViewTextBoxCell cell12 = new DataGridViewTextBoxCell();
                cell12.Value = acc.datagroup;
                dataGridViewRow.Cells.Add(cell12);

                DataGridViewTextBoxCell cell13 = new DataGridViewTextBoxCell();
                cell13.Value = acc.nox;
                dataGridViewRow.Cells.Add(cell13);

                DataGridViewTextBoxCell cell14 = new DataGridViewTextBoxCell();
                cell14.Value = acc.TrangThai;
                dataGridViewRow.Cells.Add(cell14);


                DataGridViewTextBoxCell cell15 = new DataGridViewTextBoxCell();
                cell15.Value = acc.Thongbao;
                dataGridViewRow.Cells.Add(cell15);

                DataGridViewTextBoxCell cell16 = new DataGridViewTextBoxCell();
                cell16.Value = acc.token;
                dataGridViewRow.Cells.Add(cell16);

                DataGridViewTextBoxCell cell17 = new DataGridViewTextBoxCell();
                cell17.Value = acc.cookies;
                dataGridViewRow.Cells.Add(cell17);

                DataGridViewTextBoxCell cell18 = new DataGridViewTextBoxCell();
                cell18.Value = acc.lastrun;
                dataGridViewRow.Cells.Add(cell18);

                DataGridViewTextBoxCell cell19 = new DataGridViewTextBoxCell();
                cell19.Value = acc.Device_mobile;
                dataGridViewRow.Cells.Add(cell19);


                DataGridViewTextBoxCell cell20 = new DataGridViewTextBoxCell();
                cell20.Value = acc.backupld;
                dataGridViewRow.Cells.Add(cell20);

                dataGridViewRow.Tag = acc;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvUser.Rows.Add(dataGridViewRow);

                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            foreach (DataGridViewRow row2 in this.dgvUser.SelectedRows)
            {
                row2.Cells[0].Value = true;
            }
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
                frm_LoginLD_PRO frm = new frm_LoginLD_PRO(this, list_acc);
                frm.Show();
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
                RichTextBox richLogs = new RichTextBox();
                TinSoftModel tinsoft = new TinSoftModel();
                tinsoft.success = false;
                #region doi ip truoc khi mo ld
                if (SettingTool.configld.typeip == 6)
                {
                    tinsoft = changeIpHelper.method_GetProxyTinSoft(SettingTool.configld.apitinsoft);
                    if (tinsoft.success)
                    {


                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (SettingTool.configld.typeip == 2 || SettingTool.configld.typeip == 3)
                    {

                        ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                        if (kq.status)
                        {
                        }
                        else
                        {

                            return;
                        }
                        //hma
                    }
                }
                #endregion
                CancellationTokenSource tokensource = new CancellationTokenSource();
                var token = tokensource.Token;
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
                    if (ld.autoRunLDSetPosition(ldID, u, token))
                    {
                        u.setStatus(ldID, "Kết nối thành công LD...");
                    }
                    else
                    {
                        u.setStatus(ldID, "Disconnected...");
                        return;
                    }
                }
                #region doi ip sau khi mo ld thanh cong
                if (tinsoft.success)
                {
                    u.setStatus(ldID, "Đổi proxy tinsoft : " + tinsoft.proxy);
                    changeIpHelper.changeProxyAdb(ldID, tinsoft.proxy);
                }
                // changeIpHelper.connectAfterOpen(u, richLogs, ldID,acc, token);
                #endregion

            }
            catch
            { }

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
                //ld.quit(model.LDID.ToString());
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
            //this.thread_1 = new Thread(new ThreadStart(this.closeAllLD));
            //thread_1.IsBackground = true;
            //this.thread_1.Start();

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
            //    pibStatus.Visible = true;
            NinjaADB ninjadb = new NinjaADB();
            stop = false;
            ClearMessage();
            startLogin();
            //this.thread_1 = new Thread(new ThreadStart(this.openAppFacebook));
            //thread_1.IsBackground = true;
            //this.thread_1.Start();

        }
        public void openAppFacebook()
        {

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
            RichTextBox richLogs = new RichTextBox();
            TinsoftResult ts = new TinsoftResult();

            #region doi ip truoc khi mo ld
            if (SettingTool.configld.typeip == 6)
            {
                ts = changeIpHelper.method_ChangeTinSoft(SettingTool.configld.apitinsoft);
                //if (ts.success)
                //{
                //}
                //else
                //{
                //    MessageBox.Show("Lỗi lấy proxy tinsoft : " + ts.description);
                //    return;
                //}
            }
            else
            {
                if (SettingTool.configld.typeip == 2 || SettingTool.configld.typeip == 3)
                {
                    ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                    if (kq.status)
                    {
                    }
                    else
                    {
                        MessageBox.Show("Lỗi đổi ip: " + kq.data);
                        return;
                    }
                    //hma
                }
            }
            #endregion
            CancellationTokenSource tokensource = new CancellationTokenSource();
            var token = tokensource.Token;
            foreach (DataGridViewRow dr in list_dr)
            {
                if (list_dr.Count > 0)
                {
                    Account acc = (Account)dr.Tag;
                    string ldid = changeIpHelper.getLD();
                    if (ldid != "-1")
                    {
                        acc.ldid = ldid;
                        string ldID = acc.ldid;
                        ld.setupLD(acc, ldID);
                        userLD u = checkExits(ldID);
                        addLDToPanel(u);
                        if (ld.launchSetPosion(ldID, u, token))
                        {
                            u.setStatus(ldID, "Kết nối thành công LD...");
                        }
                        else
                        {
                            if (ld.autoRunLDSetPosition(ldID, u, token))
                            {
                                u.setStatus(ldID, "Kết nối thành công LD...");
                            }
                            else
                            {
                                u.setStatus(ldID, "Disconnected...");
                                return;
                            }
                        }
                        ld.restoredatafb(acc.ldid, acc.id);
                        #region doi ip sau khi mo ld thanh cong
                        //if (ts.success)
                        //{
                        //    u.setStatus(ldID, "Đổi proxy tinsoft : " + ts.proxy);
                        //    changeIpHelper.changeProxyAdb(ldID, ts.proxy);
                        //}
                        changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
                      
                        #endregion

                        dr.Cells["Message"].Value = "Open App Facebook";

                        ld.killApp(acc.ldid, "com.facebook.katana");
                        dr.Cells["Message"].Value = "Open Facebook";

                        ld.restoredatafb(acc.ldid, acc.id);
                        ld.runApp(acc.ldid, "com.facebook.katana");
                        if (ld.checkIsLogin(acc))
                        {
                            dr.Cells["Status"].Value = "Live";
                            acc.TrangThai = "Live";
                            acc.Thongbao = "Đăng nhập thành công";
                            if (SettingTool.configld.has_savetoken)
                            {
                                dr.Cells["Message"].Value = "Cập nhật token, cookies";
                                ld.SaveTokenCookies(acc);
                            }
                        }
                        else
                        {

                            dr.Cells["Status"].Value = "Die";
                            acc.TrangThai = "Die";
                        }
                        dr.Cells["Message"].Value = "";
                        nguoidung.updateNoti(acc);

                    }
                    else
                    {
                        dr.Cells["Status"].Value = "LD" + ldid + " đang được sử dụng tài khoản khác";
                    }
                }

            }


            stopAll();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

            NinjaADB ninjadb = new NinjaADB();
            stop = false;

            this.thread_1 = new Thread(new ThreadStart(this.CheckName));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        private void CheckName()
        {
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
            pibStatus.Visible = true;
            list_device = ld.getLdPlay();
            list_devicerunning = new List<LDModel>();
            //chon cau hinh
            foreach (LDModel model in list_device)
            {
                if (ld.checkIsRunning(model.id.ToString()))
                {
                    list_devicerunning.Add(model);
                }
            }
            if (list_devicerunning.Count > 0)
            {
                list_dr = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    if ((bool)row.Cells[0].Value)
                    {
                        Account acc = (Account)row.Tag;
                        if (checkDeviceAccount(acc))
                        {
                            list_dr.Add(row);
                        }
                        else
                        {
                            row.Cells["Message"].Value = "Thiết bị chưa được bật.Vui lòng bật LDPlayer";
                        }
                    }
                }
                foreach (DataGridViewRow dr in list_dr)
                {
                    if (list_dr.Count > 0)
                    {
                        Account acc = (Account)dr.Tag;

                        if (ld.checkAppInstall(acc.ldid))
                        {
                            dr.Cells["Message"].Value = "Open App Facebook";
                            if (ld.checkAppCurrent(acc) == false)
                                ld.restoreAccount(acc.ldid, acc);

                            string path = string.Format("c:\\test\\{0}\\pictures\\temp\\{0}.txt", acc.ldid);


                            ld.runApp(acc.ldid, "com.facebook.katana");


                            if (ld.checkIsLogin(acc))
                            {
                                dr.Cells["Status"].Value = "Live";

                                ld.copyfileToken(acc.ldid);
                                string cmd = String.Format("pull \"/storage/emulated/0/authentication\" \"{0}\"", path);
                                ld.runAdb(acc.ldid, cmd);

                                if (File.Exists(path))
                                {
                                    string html = File.ReadAllText(path);
                                    string uid = FunctionHelper.smethod_6(html, html.IndexOf("EAAAAUa"), "\\").Trim();
                                    string[] arr = html.Split('[');
                                    if (arr.Length > 1)
                                    {
                                        string data = "[" + arr[1];
                                        JArray jarr = JArray.Parse(data);
                                        string cookie = "";
                                        foreach (var item in jarr)
                                        {
                                            cookie = String.Format("{0};{1}={2}", cookie, item["name"].ToString(), item["value"].ToString());
                                        }
                                    }

                                }

                            }
                            else
                            {
                                dr.Cells["Status"].Value = "Die";
                            }


                        }
                        else
                        {
                            dr.Cells["Message"].Value = "Chưa cài đặt App Facebook";
                        }

                    }
                }

            }
            else
            {
                MessageBox.Show("Thiết bị chưa được bật.Vui lòng bật LDPlayer", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            stopAll();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            NinjaADB ninjadb = new NinjaADB();
            stop = false;

            this.thread_1 = new Thread(new ThreadStart(this.logoutFacebook));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        public void logoutFacebook()
        {

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
            RichTextBox richLogs = new RichTextBox();
            TinsoftResult ts = new TinsoftResult();

            #region doi ip truoc khi mo ld
            if (SettingTool.configld.typeip == 6)
            {


                ts = changeIpHelper.method_ChangeTinSoft(SettingTool.configld.apitinsoft);

            }
            else
            {
                if (SettingTool.configld.typeip == 2 || SettingTool.configld.typeip == 3)
                {
                    ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                    if (kq.status)
                    {
                    }
                    else
                    {
                        MessageBox.Show("Lỗi đổi ip: " + kq.data);
                        return;
                    }
                    //hma
                }
            }
            #endregion
            CancellationTokenSource tokensource = new CancellationTokenSource();
            var token = tokensource.Token;
            foreach (DataGridViewRow dr in list_dr)
            {
                if (list_dr.Count > 0)
                {
                    Account acc = (Account)dr.Tag;
                    string ldid = changeIpHelper.getLD();
                    if (ldid != "-1")
                    {
                        acc.ldid = ldid;
                        string ldID = acc.ldid;
                        ld.setupLD(acc, ldID);
                        userLD u = checkExits(ldID);
                        addLDToPanel(u);
                        if (ld.launchSetPosion(ldID, u, token))
                        {
                            u.setStatus(ldID, "Kết nối thành công LD...");
                        }
                        else
                        {
                            if (ld.autoRunLDSetPosition(ldID, u, token))
                            {
                                u.setStatus(ldID, "Kết nối thành công LD...");
                            }
                            else
                            {
                                u.setStatus(ldID, "Disconnected...");
                                return;
                            }
                        }
                        #region doi ip sau khi mo ld thanh cong

                        changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
                        #endregion

                        dr.Cells["Message"].Value = "Open App Facebook";

                        ld.killApp(acc.ldid, "com.facebook.katana");
                        dr.Cells["Message"].Value = "Open Facebook";
                        ld.runApp(acc.ldid, "com.facebook.katana");
                        if (ld.logoutLD(acc))
                        {
                            dr.Cells["Status"].Value = "Logout Facebook";

                        }
                        else
                        {

                            dr.Cells["Status"].Value = "Die";
                            acc.TrangThai = "Die";
                        }

                    }
                    else
                    {
                        dr.Cells["Status"].Value = "LD" + ldid + " đang được sử dụng tài khoản khác";
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
            //frm_CreateLD frm = new frm_CreateLD(this);
            //frm.Show();
        }
        public void addNewLD()
        {
            ld.add();

            loadDanhMuc();
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
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa LD này không? Xóa có thể mất tài khoản Facebook được setup", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow dr in dgvLD.SelectedRows)
                    {

                        DetailLDModel model = (DetailLDModel)dr.Tag;
                        ld.remove(model.LDID.ToString());
                        detail_bll.delete(model);
                    }
                    loadDanhMuc();
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
            frm_TuongTacLD_PRO frmadd = new frm_TuongTacLD_PRO(list_acc, this);

            frmadd.Show();
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


        private void mnuPost_Click(object sender, EventArgs e)
        {

            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_PostLD_PRO frmadd = new frm_PostLD_PRO(list_acc, this);

            frmadd.Show();
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
            frm_Config_PRO frm = new frm_Config_PRO();
            // frm.AccessibleDescription = userId;
            frm.ShowDialog();

        }

        private void btnRecycleBin_Click(object sender, EventArgs e)
        {
            //frmListDelete frm = new frmListDelete();
            frm_ListDelete frm = new frm_ListDelete();
            frm.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_ShareNhomLD_PRO frmadd = new frm_ShareNhomLD_PRO(list_acc, this);

            frmadd.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text.ToString()))
                return;
            string where = "";
            try
            {
                List<Account> list_acc = new List<Account>();
                dgvUser.Rows.Clear();
                string[] arr = txtSearch.Text.Trim().Split(',');

                if (arr.Count() > 0)
                {
                    for (int i = 0; i < arr.Count(); i++)
                    {
                        where = string.Format("Where id like '%{0}%' or email like '%{0}%' ", arr[i].Trim());
                        NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                        list_acc.AddRange(nguoidung_bll.loadUserbySql(string.Format("select * from Account {0}", where)));
                    }

                }

                foreach (Account acc in list_acc)
                {
                    method_DatagridviewAcount(acc);
                }
            }
            catch
            {
            }
        }


        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dr = dgvUser.CurrentRow;
                Account acc = (Account)dr.Tag;

                Random rd = new Random();
                string filename = rd.Next(0, 1000000).ToString() + ".xml";
                string cmdCommand = "";
                if (SettingTool.configld.versionld == "3.x")
                {
                    cmdCommand = string.Format("shell uiautomator dump storage/emulated/legacy/pictures/temp/{0}", filename);
                }
                else
                {
                    cmdCommand = string.Format("shell uiautomator dump storage/emulated/0/pictures/temp/{0}", filename);
                }
                string data = ld.runAdb(acc.ldid, cmdCommand);

                var screen = ld.ScreenShoot(acc.ldid);
                screen.Save("c:\\1.png");
                ld.checkScreen(acc.ldid);
            }
            catch { }

            // ld.OpenLink("1", "com.facebook.katana", "fb://friends/");
        }

        private void mnuCancelReques_Click(object sender, EventArgs e)
        {

        }

        private void mnuUser_Opening(object sender, CancelEventArgs e)
        {

        }

        private void mnuInvite_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_InviteLD_PRO frmadd = new frm_InviteLD_PRO(list_acc, this);

            frmadd.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkLicense();
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
                    ld.rename("LD", model.id.ToString());
                    //thoong update
                    ldmodel = detail_bll.selectOneDetailLD(model.id);
                    ldmodel.LDName = model.name;
                    detail_bll.update(ldmodel);
                }
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            loadDanhMuc();
        }

        private void selectGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadAccountDanhMuc();

        }
        private void loadAccountDanhMuc()
        {
            try
            {

                dgvUser.Rows.Clear();
                List<string> list_danhmuc = new List<string>();
                foreach (DataGridViewRow dr in dgvDanhMuc.SelectedRows)
                {
                    DanhMuc group = (DanhMuc)dr.Tag;
                    list_danhmuc.Add(group.id_danhmuc);

                }
                NguoiDung_Bll nguoi_dung = new NguoiDung_Bll();
                List<Account> list_acc = nguoi_dung.loadAccountByDanhMuc(list_danhmuc);
                if (list_acc.Count > 0)
                {
                    foreach (Account acc in list_acc)
                    {
                        method_DatagridviewAcount(acc);
                    }
                }

            }
            catch
            { }
        }


        private void dgvGroupLD_DoubleClick(object sender, EventArgs e)
        {
            loadAccountDanhMuc();
            tabControl1.SelectedIndex = 0;
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
            List<DanhMuc> list_danhmuc = new List<DanhMuc>();
            foreach (DataGridViewRow dr in dgvDanhMuc.SelectedRows)
            {
                DanhMuc dm = (DanhMuc)dr.Tag;
                list_danhmuc.Add(dm);
            }
            if (list_danhmuc.Count > 0)
            {
                if (MessageBox.Show("Bạn sẽ mất tài khoản hiện có trong danh mục đó.Bạn có muốn xóa Danh mục không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DanhMuc_Bll danhmuc_bll = new DanhMuc_Bll();
                    foreach (DanhMuc dm in list_danhmuc)
                    {
                        danhmuc_bll.removeDanhMuc(dm);
                    }
                    loadDanhMuc();
                }
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_TuongTacLD_PRO frmadd = new frm_TuongTacLD_PRO(list_acc, this);

            frmadd.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {

            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_TuongTacLD_PRO frmadd = new frm_TuongTacLD_PRO(list_acc, this);

            frmadd.Show();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            //frm_GroupLD frm = new frm_GroupLD(this);
            //frm.Show();
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

                dr.Cells["Message"].Value = "Open App Facebook";
                if (ld.checkAppCurrent(acc) == false)
                    ld.restoreAccount(acc.ldid, acc);

                ld.runApp(acc.ldid, "com.facebook.katana");
                Thread.Sleep(6000);
                if (ld.checkIsLogin(acc))
                {
                    dr.Cells["Status"].Value = "Live";
                    ld.copyfileToken(acc.ldid);
                    string path = string.Format("c:\\test\\{0}\\pictures\\temp\\{0}.txt", acc.ldid);
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
            this.thread_1 = new Thread(new ThreadStart(this.closeAllLD));
            thread_1.IsBackground = true;
            this.thread_1.Start();
            //openAllLD(20);
        }
        private void closeAllLD()
        {
            ld.quitall();
        }
        private void openAllLD(int delay)
        {
            try
            {
                dgvLD.Rows.Clear();
                dgvUser.Rows.Clear();
                tabControl2.SelectedIndex = 1;
                foreach (DataGridViewRow dr in dgvDanhMuc.SelectedRows)
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
            frm_AddGroup_PRO frm = new NinjaSystem.frm_AddGroup_PRO();
            frm.ShowDialog();
            dgvDanhMuc.Rows.Clear();
            loadDanhMuc();
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
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                if ((bool)dgvUser.Rows[i].Cells[0].Value)
                    dgvUser.Rows[i].Cells["Message"].Value = "";

                DataGridViewRow dr = dgvUser.Rows[i];
              

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
                if (string.IsNullOrEmpty(SettingTool.configld.token) == false)
                {
                    string useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36";
                    if (profile.checkLiveToken(SettingTool.configld.token, useragent))
                    {
                        foreach (DataGridViewRow dr in dgvUser.SelectedRows)
                        {
                            if ((bool)dr.Cells[0].Value)
                            {
                                setColorRow(dr, Color.Yellow);
                                Account acc = (Account)dr.Tag;
                                if (profile.LoadInfoUIDToken(SettingTool.configld.token, acc, useragent))
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
                                    dr.Cells["Message"].Value = "Check không thành công";
                                }
                                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                                nguoidung.updateName(acc);
                                setColorRow(dr, Color.White);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Token Die vui lòng thêm Token quét bài viết trong mục Cấu hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                else
                {
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
                                    dr.Cells["Message"].Value = "Check không thành công";
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

       

        private void mnuTwoFa_Click(object sender, EventArgs e)
        {


            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }

            frm_2Fa_PRO frmadd = new frm_2Fa_PRO(list_acc, this);

            frmadd.Show();

        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {

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
                loadAccountDanhMuc();
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
                loadAccountDanhMuc();
            }
        }

        private void mnuJoinGroup_Click_1(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_JoinGroupManualLD_PRO frmadd = new frm_JoinGroupManualLD_PRO(list_acc, this);
            frmadd.Show();
        }

        private void mnu_commentGroupID_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_CommentGroupLD_PRO frmadd = new frm_CommentGroupLD_PRO(list_acc, this);
            frmadd.Show();


        }

        private void mnuKetBan_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_AddFriendLD_PRO frmadd = new frm_AddFriendLD_PRO(list_acc, this);

            frmadd.Show();
        }

        private void mnuCancelRequest_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_CancelRequestLD_PRO frmadd = new frm_CancelRequestLD_PRO(list_acc, this);

            frmadd.Show();
        }

        private void mnuReactionUID_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_ReactionLD_PRO frmadd = new frm_ReactionLD_PRO(list_acc, this);
            frmadd.Show();
        }

        private void mnupostGroup_Click(object sender, EventArgs e)
        {

        }

        private void mnu_loginMess_Click(object sender, EventArgs e)
        {
            //List<Account> list_acc = new List<Account>();
            //foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            //{
            //    Account acc = (Account)dr.Tag;
            //    list_acc.Add(acc);
            //}
            //LoginMessenger frmadd = new LoginMessenger(list_acc, this);

            //frmadd.Show();
        }

        private void mnuLeaveGroup_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_LeaveGroup_PRO frmadd = new frm_LeaveGroup_PRO(list_acc, this);

            frmadd.Show();
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

        private void mnuPostInGroup_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_PostLD_Group_PRO frmadd = new frm_PostLD_Group_PRO(list_acc, this);

            frmadd.Show();
        }

        private void mnuUpdateAccount_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dgvUser.CurrentRow;
                Account acc = (Account)dr.Tag;
                frmAddUser frm = new frmAddUser(acc, "Update");

                userId = acc.Id_account.ToString();
                frm.AccessibleDescription = "Update$" + userId;
                frm.ShowDialog();

                loadDanhMuc();
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
                    u.list_ldopen = list_ldopen;
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
            //xProxyController xcontroller = new xProxyController();
            //string[] proxy = u.ip_proxy.Split('-');
            //xcontroller.finishProxy(proxy[0].Trim());
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

        private void mnuImportContact_Click(object sender, EventArgs e)
        {
            List<DetailLDModel> list_ld = new List<DetailLDModel>();
            foreach (DataGridViewRow dr in dgvLD.SelectedRows)
            {
                DetailLDModel ld = (DetailLDModel)dr.Tag;
                list_ld.Add(ld);
            }
            frmImportContact frm = new frmImportContact(this, list_ld);
            frm.Show();
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
                    NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                    bool has_live = profile.LoadInfoToken(acc, "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-G965N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]");
                    //   bool has_live = profile.getInfoToken(acc, "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-G965N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]");
                    if (has_live)
                    {
                        dr.Cells["clGroup"].Value = acc.group_count;
                        dr.Cells["clFriend"].Value = acc.friend_count;
                        dr.Cells["clBirthday"].Value = acc.birthday;
                        dr.Cells["Status"].Value = "Live";
                        dr.Cells["User"].Value = acc.email;
                        nguoidung_bll.updateName(acc);
                        acc.TrangThai = "Live";

                    }
                    else
                    {

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

        private void mnuProfile_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_Profile_PRO frmadd = new frm_Profile_PRO(list_acc, this);

            frmadd.Show();
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

            this.thread_1 = new Thread(new ThreadStart(this.logoutFacebook));
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
            frm_LoginMessenger_PRO frmadd = new frm_LoginMessenger_PRO(list_acc, this);
            

            frmadd.Show();
        }

        private void mnuinvitefirend_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_InviteLD_PRO frmadd = new frm_InviteLD_PRO(list_acc, this);

            frmadd.Show();
        }

        private void ẩnHiểnCộtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_confirm frm = new frm_confirm();
            frm.ShowDialog();
            loadDanhMuc();
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

        private void checkUIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.checkUID));
            this.thread_1.Start();
        }
        public void checkUID()
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
                            checkLiveUID(dr);

                        }
                    });

                }
                Task.WaitAll(tasks);
                if (list_dr.Count > 0)
                    goto Lb_Start;
            }
        }
        private void checkLiveUID(DataGridViewRow dr)
        {
            try
            {
                setColorRow(dr, Color.Yellow);
                Account acc = (Account)dr.Tag;

                Profile_Controller profile = new Profile_Controller();

                bool has_live = profile.checkLiveUID(acc.id);
                if (has_live)
                {
                    dr.Cells["Status"].Value = "Live";
                    acc.TrangThai = "Live";
                }
                else
                {
                    acc.TrangThai = "Die";
                    dr.Cells["Status"].Value = "Die";
                }
                NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                nguoidung_bll.updateStatus(acc);
                dr.Cells["Message"].Value = "Kiểm tra uid hoàn thành";


                setColorRow(dr, Color.White);
            }
            catch
            { }
        }

        private void copyUIDPassTokenCookieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    builder.AppendLine(acc.id + "|" + acc.Password + "|" + acc.token + "|" + acc.cookies);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {

            }
        }

        private void mnu_profile_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_Profile_PRO frmadd = new frm_Profile_PRO(list_acc, this);

            frmadd.Show();
        }

        private void mnu_2Fa_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }

            frm_2Fa_PRO frmadd = new frm_2Fa_PRO(list_acc, this);

            frmadd.Show();
        }

        private void tạoBàiViếtToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }

            frm_PostManager frmadd = new frm_PostManager(list_acc);

            frmadd.Show();
        }

        private void mởDataBàiViếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Account acc = (Account)dgvUser.CurrentRow.Tag;
                string path = string.Format("{0}\\Schedule\\{1}", Application.StartupPath, acc.id);
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                Process.Start(path);
            }
            catch
            { }
        }

        private void checkAvatarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.thread_1 = new Thread(new ThreadStart(this.checkAvatar));
            this.thread_1.Start();
        }
        public void checkAvatar()
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
                            checkAvatar(dr);

                        }
                    });

                }
                Task.WaitAll(tasks);
                if (list_dr.Count > 0)
                    goto Lb_Start;
            }
        }
        private void checkAvatar(DataGridViewRow dr)
        {
            try
            {
                setColorRow(dr, Color.Yellow);
                Account acc = (Account)dr.Tag;
                string token = SettingTool.configld.token;
                Profile_Controller profile = new Profile_Controller();
                string avatar = "";
                bool has_live = profile.checkAvatar(acc.id, token);
                if (has_live)
                {
                    dr.Cells["clAvatar"].Value = "Yes";
                    avatar = "Yes";

                }
                else
                {
                    dr.Cells["clAvatar"].Value = "No";
                    avatar = "No";

                }
                NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                acc.avatar = avatar;
                nguoidung_bll.updateAvatar(acc);
                dr.Cells["Message"].Value = "Kiểm tra uid hoàn thành";


                setColorRow(dr, Color.White);
            }
            catch
            { }
        }

        private void mởCheckpointNgàySinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unlock();
        }
        public void unlock()
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;

                    list_acc.Add(acc);

                }
            }
            if (list_acc.Count > 0)
            {
                frm_Unlock_PRO frm = new frm_Unlock_PRO(this, list_acc);
                frm.Show();
            }


        }

        private void copyUIDPassPrivateKeyBirthdayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    builder.AppendLine(acc.id + "|" + acc.Password + "|" + acc.privatekey + "|" + acc.birthday);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {

            }
        }

        private void toolStripMenuItem11_Click_1(object sender, EventArgs e)
        {
            List<DetailLDModel> list_detail = new List<DetailLDModel>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                DetailLDModel model = new DetailLDModel();
                Account acc = (Account)dr.Tag;
                model.LDID = Convert.ToInt32(acc.ldid);
                list_detail.Add(model);
            }
            if (list_detail.Count > 0)
            {
                frm_MoveGroupLD frm = new frm_MoveGroupLD(list_detail);
                frm.ShowDialog();

            }
        }

        private void quétUIDGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }

            frm_QuetUID frmadd = new frm_QuetUID(list_acc);

            frmadd.Show();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;

                list_acc.Add(acc);
            }
            if (list_acc.Count > 0)
            {
                frm_ImportAccountLD_PRO frm = new frm_ImportAccountLD_PRO(list_acc);
                frm.ShowDialog();

            }
        }

        private void xóaDataLDPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa data LDplayer bạn phải đăng nhập và cài lại app Facebook.Thông số kĩ thuật của LDplayer vẫn như cũ. Bạn có chắc chắn xóa không", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow dr in dgvLD.SelectedRows)
                    {
                        DetailLDModel model = (DetailLDModel)dr.Tag;
                        ld.removeData(model.LDID.ToString());

                    }
                    loadDanhMuc();
                }
                catch { }
            }
        }

        private void xóaCácUIDTrùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.method_deletedoubleUID));
            this.thread_1.Start();
        }
        private void method_deletedoubleUID()
        {

            NguoiDung_Bll bll = new NguoiDung_Bll();

            DataTable doubleId = dt.select(" select Id_account, id,count(*) as dem  from Account  GROUP by id   HAVING dem > 1");

            if (doubleId.Rows.Count > 0)
            {
                if (MessageBox.Show("Nên lưu trữ dự phòng trước khi xóa! \r\n Bạn vẫn tiếp tục xóa " + doubleId.Rows.Count.ToString() + " UID trùng? ", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    controlUpdate up = new controlUpdate();
                    bool kq = up.deletedoubleUID("DELETE from Account WHERE Id_account in (select Id_account from  ( select Id_account, id,count(*) as dem  from Account  GROUP by id   HAVING dem > 1 ))");
                    if (kq)
                    {
                        MessageBox.Show("Xóa dữ liệu trùng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadAccountDanhMuc();
                    }

                    else
                        MessageBox.Show("Xóa dữ liệu trùng không thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Không có UID trùng nhau", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void xoaProfileBAckupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.thread_1 = new Thread(new ThreadStart(this.removeProifleBackup));
            thread_1.IsBackground = true;
            this.thread_1.Start();
        }
        private void removeProifleBackup()
        {
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                try
                {
                    Account acc = (Account)dr.Tag;
                    string path = string.Format("{0}\\{1}\\{1}.7z", SettingTool.configld.pathsavedata, acc.id);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    path = string.Format("{0}\\{1}\\datafb2.tar.gz", SettingTool.configld.pathsavedata, acc.id);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch
                { }
            }
        }

        private void cấuHìnhTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnurenamegroup_Click(object sender, EventArgs e)
        {
            DataGridViewRow rd = new DataGridViewRow();
            rd = dgvDanhMuc.CurrentRow;
            DanhMuc group = (DanhMuc)rd.Tag;
            frm_AddGroup_PRO frm = new NinjaSystem.frm_AddGroup_PRO();
            frm.AccessibleDescription = "update$" + group.id_danhmuc.ToString();
            frm.ShowDialog();
            dgvDanhMuc.Rows.Clear();
            loadDanhMuc();
        }

        private void mnusetupProxy_Click(object sender, EventArgs e)
        {
            List<Account> list_ld = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account ld = (Account)dr.Tag;
                list_ld.Add(ld);
            }
            frm_ProxyInfo_PRO frm = new frm_ProxyInfo_PRO(list_ld, this);
            frm.ShowDialog();
        }

        private void mnuContact_Click(object sender, EventArgs e)
        {
            List<DetailLDModel> list_ld = new List<DetailLDModel>();

            frmImportContact frm = new frmImportContact(this, list_ld);
            frm.Show();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void startLogin()
        {
            stop = false;

            //chon cau hinh
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    list_dr.Add(row);
                }
            }

            if (list_dr.Count <= 0)
            {
                MessageBox.Show("Hãy chọn những tài khoản cần chạy");
                pibStatus.Visible = false;
                return;
            }
            else
            {
                if (changeIpHelper.checkWaitAny())
                {
                    this.thread_1 = new Thread(new ThreadStart(this.runLoginWaitAny));
                    thread_1.IsBackground = true;
                    this.thread_1.Start();
                }
                else
                {
                    this.thread_1 = new Thread(new ThreadStart(this.runLogin));
                    thread_1.IsBackground = true;
                    this.thread_1.Start();
                }
            }
        }

        private void resetproxy()
        {
            try
            {
                if (changeIpHelper.checkGetProxyWaitAny())
                {
                    List<string> lsproxy = new List<string>();
                    xProxyController xcontroller = new xProxyController();

                    foreach (userLD ldopen in this.list_ldopen)
                    {
                        string[] proxy = ldopen.ip_proxy.Split('-');
                        if (SettingTool.configld.typeip == 6 || SettingTool.configld.typeip == 8)
                        {
                            string proxyapi = xcontroller.getApifromproxy(proxy[0].Trim());//chuyen doi thanh api
                            lsproxy.Add(proxyapi);

                        }
                        else
                            lsproxy.Add(proxy[0].Trim());
                    }

                    int runs = SettingTool.list_running.Count;
                    lock (SettingTool.lockobj)
                    {
                        while (runs > 0)
                        {
                            foreach (string proxy in SettingTool.list_running)
                            {
                                if (!lsproxy.Contains(proxy))
                                {
                                    foreach (xproxy xp in SettingTool.list_freeproxy)
                                    {
                                        if (xp.proxy == proxy)
                                        {
                                            xp.proxysucess = "";
                                            xp.use = false;
                                        }
                                    }
                                    SettingTool.list_running.Remove(proxy);
                                    break;
                                }
                            }

                            runs--;
                        }
                    }
                }

            }
            catch
            {

            }
        }
        private void runLoginWaitAny()
        {
            var token = tokenSource.Token;
            int numthread = SettingTool.configld.numthread;
            if (numthread > list_dr.Count)
            {
                numthread = list_dr.Count;
            }

            //khoi tao list task
            Task[] list_task = TaskController.createTask(numthread);
            xcontroller.createProxy(numthread);
            int maxproxy = 0;
        Lb_quayvong:
            if (list_dr.Count > 0)
            {
                #region doi ip truoc khi mo ld
                List<string> list_proxy = new List<string>();

                if (SettingTool.configld.typeip == 6)
                {

                Lb_Start:
                    // method_log("Bắt đầu đổi ip bằng Tinsoft");
                    TinsoftResult tinsoftresult = changeIpHelper.method_ChangeTinSoft(SettingTool.configld.apitinsoft);

                    foreach (TinSoftModel ts in tinsoftresult.list_model)
                    {
                        // method_log(String.Format("Api {0} - IP {1} - Next change {2} - Timout - {3}", ts.api, ts.proxy, ts.next_change, ts.timeout));
                    }
                    if (tinsoftresult.list_proxy.Count <= 0)
                    {

                        //method_log("Lỗi lấy proxy tinsoft.Tiếp tục request");
                        Thread.Sleep(5000);
                        goto Lb_Start;
                    }
                    else
                    {
                        list_proxy = tinsoftresult.list_proxy;
                    }
                }
                else
                {

                    if (SettingTool.configld.typeip == 7)
                    {
                        ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                        if (kq.status)
                        {
                            list_proxy = SettingTool.list_xproxy;
                            //  method_log("Total Proxy: " + list_proxy.Count);
                        }
                    }
                    else
                    {
                        if (SettingTool.configld.typeip == 2 || SettingTool.configld.typeip == 3)
                        {
                            ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                            if (kq.status)
                            {
                                // method_log(kq.data);
                            }
                            else
                            {
                                // method_log("Lỗi đổi ip: " + kq.data);
                                return;
                            }
                            //hma
                        }
                    }
                }
                #endregion

                //chay luon sau khi 1 ld hoan thanh
                object synDevice = new object();
                int i = 0;
                while (TaskController.checkAvailableTask(list_task))
                {
                    if (stop == false)
                    {
                        if (list_dr.Count <= 0)
                        {
                            break;
                        }
                        int index = TaskController.getAvailableTask(list_task);
                        if (index >= 0)
                        {
                            if (changeIpHelper.checkGetProxyWaitAny())
                            {
                                method_log("Đang lấy IP ");
                                string proxy = xcontroller.getProxy();
                                if (!string.IsNullOrEmpty(proxy))
                                {
                                    method_log("Đã lấy IP: " + proxy);
                                    string ldid = changeIpHelper.getLD();
                                    if (ldid != "-1")
                                    {
                                        Task task = Task.Factory.StartNew(() =>
                                        {

                                            if (list_dr.Count > 0)
                                            {

                                                DataGridViewRow dr = new DataGridViewRow();
                                                lock (synDevice)
                                                {
                                                    dr = list_dr[0];
                                                    list_dr.Remove(dr);
                                                }
                                                method_Start(ldid, dr, proxy, token);


                                            }
                                            else
                                            {
                                                method_log("Đã hết tài khoản");
                                            }

                                        }, token);
                                        list_task[index] = task;
                                        Thread.Sleep(SettingTool.configld.timedelay * 1000);
                                    }
                                    else
                                    {
                                        xcontroller.finishProxy(proxy);
                                        method_log("Tất cả LD đang được sử dụng");
                                    }
                                }
                                else
                                {
                                    Thread.Sleep(3000);
                                    method_log("Proxy chưa sẵn sàng: " + proxy);
                                    maxproxy++;
                                    if (maxproxy > 50)
                                    {
                                        maxproxy = 0;
                                        resetproxy();
                                    }
                                }
                            }
                            else
                            {
                                Task task = Task.Factory.StartNew(() =>
                                {
                                    string proxy = "";
                                    string ldid = changeIpHelper.getLD();
                                    if (ldid != "-1")
                                    {
                                        if (list_dr.Count > 0)
                                        {
                                            DataGridViewRow dr = new DataGridViewRow();
                                            lock (synDevice)
                                            {
                                                dr = list_dr[0];
                                                list_dr.Remove(dr);

                                                if (list_proxy.Count > 0)
                                                {
                                                    proxy = list_proxy[i];
                                                    i++;
                                                    if (i >= list_proxy.Count)
                                                    {
                                                        i = 0;
                                                    }
                                                }
                                            }
                                            method_Start(ldid, dr, proxy, token);
                                        }
                                        else
                                        {
                                            // method_log("Đã hết tài khoản");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Tất cả LD đang được sử dụng");
                                    }
                                }, token);
                                list_task[index] = task;
                            }

                        }

                        Thread.Sleep(SettingTool.configld.timedelay * 1000);
                    }
                }
                tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                try
                {
                    Task.WaitAny(list_task);
                }
                catch
                { }
                if (list_dr.Count > 0 && stop == false)
                {

                    goto Lb_quayvong;
                }
                else
                {
                    try
                    {
                        Task.WaitAll(list_task);

                    }
                    catch
                    { }
                    // stopLogin();

                }
            }
        }
        private void runLogin()
        {
            // setupLDGoc();
            //  method_logStatus("Bắt đầu đăng nhập tài khoản");
            var token = tokenSource.Token;
        Lb_quayvong:
            int numthread = SettingTool.configld.numthread;
            if (numthread > list_dr.Count)
            {
                numthread = list_dr.Count;
            }
            if (list_dr.Count > 0)
            {
                #region doi ip truoc khi mo ld
                List<string> list_proxy = new List<string>();

                if (SettingTool.configld.typeip == 6)
                {

                Lb_Start:
                    //  method_log("Bắt đầu đổi ip bằng Tinsoft");
                    TinsoftResult tinsoftresult = changeIpHelper.method_ChangeTinSoft(SettingTool.configld.apitinsoft);

                    foreach (TinSoftModel ts in tinsoftresult.list_model)
                    {
                        // method_log(String.Format("Api {0} - IP {1} - Next change {2} - Timout - {3} - {4}", ts.api, ts.proxy, ts.next_change, ts.timeout, ts.description));
                    }
                    if (tinsoftresult.list_proxy.Count <= 0)
                    {

                        // method_log("Lỗi lấy proxy tinsoft.Tiếp tục request");
                        Thread.Sleep(5000);
                        goto Lb_Start;
                    }
                    else
                    {
                        list_proxy = tinsoftresult.list_proxy;
                    }

                }
                else
                {
                    if (SettingTool.configld.typeip == 7)
                    {
                        ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                        if (kq.status)
                        {
                            list_proxy = SettingTool.list_xproxy;
                            // method_log("Total Proxy: " + list_proxy.Count);
                        }
                    }
                    else
                    {
                        if (SettingTool.configld.typeip == 2 || SettingTool.configld.typeip == 3)
                        {
                            ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                            if (kq.status)
                            {
                                //  method_log(kq.data);
                            }
                            else
                            {
                                // method_log("Lỗi đổi ip: " + kq.data);
                                return;
                            }
                            //hma
                        }
                    }
                }
                #endregion

                int i = 0;
                object synDevice = new object();
                Task[] tasks = new Task[numthread];
                int int_proxy = 0;
                for (int p = 0; p < numthread; p++)
                {
                    int t = p;
                    if (stop == false)
                    {

                        tasks[t] = Task.Factory.StartNew(() =>
                        {
                            string proxy = "";
                            string ldid = changeIpHelper.getLD();
                            if (ldid != "-1")
                            {
                                if (list_dr.Count > 0)
                                {
                                    DataGridViewRow dr = new DataGridViewRow();
                                    lock (synDevice)
                                    {
                                        dr = list_dr[0];
                                        list_dr.Remove(dr);

                                        if (list_proxy.Count > 0)
                                        {
                                            proxy = list_proxy[int_proxy];
                                            int_proxy++;
                                            if (int_proxy >= list_proxy.Count)
                                            {
                                                int_proxy = 0;
                                            }

                                        }
                                    }
                                    method_Start(ldid, dr, proxy, token);
                                }
                                else
                                {
                                    //   method_log("Đã hết tài khoản");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tất cả LD đang được sử dụng");
                            }
                        }, token);
                        Thread.Sleep(SettingTool.configld.timedelay * 1000);
                    }
                }
                tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                try
                {
                    Task.WaitAll(tasks);
                }
                catch
                { }
                if (list_dr.Count > 0 && stop == false)
                {
                    goto Lb_quayvong;
                }
                else
                {
                    // stopLogin();

                }
            }
        }

        private void method_Start(string ldID, DataGridViewRow dr, string proxy, CancellationToken token)
        {
            // changeColor(dr, Color.Yellow);
            Account acc = (Account)dr.Tag;
            Thread.Sleep(5000);
            acc.ldid = ldID;
            dr.Cells["Message"].Value = "Restore Data LD";
            acc.ldid = ldID;
            method_log("Open LDPlayer Id: " + ldID);
            //dr.Cells["LdId"].Value = ldID;
            ld.setupLD(acc, ldID);
            dr.Cells["Message"].Value = "Open LDPlayer Id: " + ldID;
            userLD u = this.checkExits(ldID);
            this.addLDToPanel(u);
            if (ld.launchSetPosion(ldID, u, token))
            {
                u.setStatus(ldID, "Connect successful LD...");
            }
            else
            {
                if (ld.autoRunLDSetPosition(ldID, u, token))
                {
                    u.setStatus(ldID, "Connect successful LD...");
                }
                else
                {
                    u.setStatus(ldID, "Disconnected...");
                    method_log("Disconnected LD: " + ldID);
                    goto Lb_Finish;
                }
            }
            changeIpHelper.createLDID2nd(SettingTool.configld.numthread);
            // ld.restoredatafb(acc.ldid, acc.id);
            try
            {
                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                if (ld.checkApp(ldID, "com.android.adbkeyboard") == false)
                {
                    string path = Application.StartupPath + "\\app\\ADBKeyboard.apk";
                    if (File.Exists(path))
                    {
                        u.setStatus(ldID, "Install Adbkeyboard...");
                        ld.installApp(ldID, path);
                        Thread.Sleep(3000);
                    }
                }

                ld.disableGPS(ldID);
                ld.setKeyboard(ldID);
                #region doi ip sau khi mo ld thanh cong
                if (string.IsNullOrEmpty(proxy) == false)
                {
                    u.setDevice(ldID, proxy);
                    u.setStatus(ldID, "Change proxy : " + proxy);
                    if (SettingTool.configld.sock5)
                    {
                        SettingTool.configld.proxytype = "socks5";
                        ld.setProxyAuthentica_proxydroid(ldID, proxy, token);
                        string yourip = ld.checkIPSock5(proxy, 3);
                        u.setDevice(ldID, proxy + " - " + yourip);

                        if (SettingTool.configld.checkproxy)
                        {
                            if (string.IsNullOrEmpty(yourip))
                            {
                                //sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                                //goto Lb_Finish;

                            }
                        }

                    }
                    else
                    {
                        changeIpHelper.changeProxyAdb(ldID, proxy);
                        //check ip
                        string yourip = ld.checkIP(proxy, 3);
                        u.setDevice(ldID, proxy + " - " + yourip);

                        if (SettingTool.configld.checkproxy)
                        {
                            if (string.IsNullOrEmpty(yourip))
                            {
                                //sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                                //goto Lb_Finish;

                            }
                        }
                    }
                }
                RichTextBox richLogs = new RichTextBox();
                changeIpHelper.connectAfterOpen(u, richLogs, ldID, token);
                #endregion

                try
                {
                    /// changeColor(dr, Color.Yellow);
                    dr.Cells["Message"].Value = "Running";

                    ld.killApp(acc.ldid, "com.facebook.katana");
                    ld.restoredatafb(acc.ldid, acc.id);
                    ld.runApp(acc.ldid, "com.facebook.katana");

                    //DetechModel kq = ld.checkOpenFacebookFinish(u, acc.ldid);
                    //if (kq.status)
                    //{
                    //    switch (kq.parent)
                    //    {

                    //        case "loginavatar":
                    //            {
                    //                //dr.Cells["Message"].Value = "Logout";
                    //                //u.setStatus(ldID, " Logout...");
                    //                ld.loginAvatarLD(acc);
                    //            }
                    //            break;
                    //    }

                    //}

                    dr.Cells["Message"].Value = "Login";

                    u.setStatus(ldID, " Login facebook...");

                    bool haslogin = ld.loginFacebookTuongTac(u,acc, token);
                    if (haslogin)
                    {
                        if (stop)
                        {
                            goto Lb_Finish;
                        }
                        dr.Cells["Message"].Value = "Login successful";
                        // dr.Cells["clstatus"].Value = "Live";
                        acc.TrangThai = "Live";
                        acc.Thongbao = "Login successful";
                        u.setStatus(ldID, "Login successful...");
                        if (SettingTool.configld.has_savetoken)
                        {

                            ld.copyfileToken(acc.ldid);
                            string path = string.Format("c:\\test\\{0}\\pictures\\temp\\{0}.txt", ldID);
                            if (File.Exists(path))
                            {
                                string html = File.ReadAllText(path);
                                string uid = FunctionHelper.smethod_6(html, html.IndexOf("EAAAAUa"), "\\").Trim();
                                acc.token = Regex.Match(uid, @"([A-Z])\w+").Value;
                                string[] arr = html.Split('[');
                                if (arr.Length > 1)
                                {
                                    string data = "[" + arr[1];
                                    JArray jarr = JArray.Parse(data);
                                    string cookie = "";
                                    foreach (var item in jarr)
                                    {
                                        cookie = String.Format("{0};{1}={2}", cookie, item["name"].ToString(), item["value"].ToString());
                                    }
                                    cookie = cookie.Remove(0, 1);
                                    acc.cookies = cookie;
                                }
                                if (acc.id.Contains('@'))
                                {
                                    string[] cokie = acc.cookies.Split(';');
                                    acc.email = acc.id;
                                    acc.id = FunctionHelper.smethod_6(cokie[0], cokie[0].IndexOf("user=") + 5, "");
                                    if (!string.IsNullOrEmpty(acc.id))
                                        nguoidung.updateuidEmail(acc);
                                }
                                nguoidung.updateTokenCookie(acc);
                            }
                        }

                        // u.setStatus(ldID, "Đang backup dataprofile");
                        //  ld.Zip(acc, ldID);
                    }
                    else
                    {
                        if (SettingTool.configld.language == "English")
                        {
                            dr.Cells["Message"].Value = "Login fail";
                            u.setStatus(ldID, " Login fail...");
                        }
                        else
                        {
                            dr.Cells["Message"].Value = "Đăng nhập không thành công";
                            u.setStatus(ldID, " Đăng nhập không thành công...");
                        }

                        //acc.TrangThai = "Die";
                        //dr.Cells["clstatus"].Value = "Die";
                        //u.setStatus(ldID, "Đang backup dataprofile");
                    }
                    // changeColor(dr, Color.White);
                    // nguoidung.updateNoti(acc);
                }
                catch (Exception ee)
                {
                    File.AppendAllText(String.Format("\n {0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + " Error: " + ee.Message + "\n");
                }
            }
            catch
            { }
        Lb_Finish:
            if (changeIpHelper.checkGetProxyWaitAny())
            {
                xcontroller.finishProxy(proxy);
            }
            //if (string.IsNullOrEmpty(proxy) == false)
            //{
            //    //remove proxy tinsoft
            //    ld.setProxyAdb(ldID, ":0");
            //}
            // ld.quit(acc, ldID);
            //this.removeLDToPanel(u);

            Thread.Sleep(1000);
        }
        private void method_log(string string_15)
        {
            MethodInvoker method = null;
            Class31 class2 = new Class31
            {
                richTextBox_0 = richLogs,
                string_0 = string_15
            };
            try
            {
                if (method == null)
                {
                    method = new MethodInvoker(class2.method_0);
                }
                this.Invoke(method);
            }
            catch (Exception)
            {
            }
        }
        [CompilerGenerated]
        private sealed class Class31
        {
            public RichTextBox richTextBox_0;
            public string string_0;

            public void method_0()
            {
                try
                {
                    if (richTextBox_0.Lines.Length > 50)
                        richTextBox_0.Text = "";

                    if (this.string_0.Contains("being aborted"))
                    {
                        this.string_0 = "Luồng đang chạy bị tạm ngừng -> STOP !!!";
                    }
                    this.richTextBox_0.Text = string.Format("{0}:{1}\n", DateTime.Now.ToString("HH:mm:ss"), this.string_0) + this.richTextBox_0.Text;

                }
                catch { }
            }

        }

        private void copyTùyChỉnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    list_dr.Add(row);
                    list_acc.Add(acc);
                }
            }

            frm_Copy frm = new frm_Copy(list_acc);
            frm.ShowDialog();
        }

        private void mnugetbirthday_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_Getbirthday frmadd = new frm_Getbirthday(this, list_acc);

            frmadd.Show();
        }

        private void mnuexportbackup_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_ExportAcc_PRO frmadd = new frm_ExportAcc_PRO(this, list_acc);

            frmadd.Show();
        }

        private void mnureactionofnick_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            frm_reactionMyfriendgroup_PRO frmadd = new frm_reactionMyfriendgroup_PRO(list_acc, this);

            frmadd.Show();
        }

        private void mnumoney_Click(object sender, EventArgs e)
        {
            //List<Account> list_acc = new List<Account>();
            //foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            //{
            //    Account acc = (Account)dr.Tag;
            //    list_acc.Add(acc);
            //}
            //frm_Makemoney_PRO frmadd = new frm_Makemoney_PRO(list_acc, this);

            //frmadd.Show();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                DataGridViewRow dr = dgvUser.CurrentRow;
                Account acc = (Account)dr.Tag;
               
                string path = String.Format("{0}\\logs\\reportGr" + acc.id + ".txt", Application.StartupPath);
                if (File.Exists(path))
                    System.Diagnostics.Process.Start(path);
            }
        }

        private void bunifuImageButton3_Click_1(object sender, EventArgs e)
        {
            frm_regnick frm = new frm_regnick(this);
            frm.Show();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            frm_regnick_novery frm = new frm_regnick_novery(this);
            frm.Show();
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            frm_regnick_email frm = new frm_regnick_email(this);
            frm.Show();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();
        }
       
       
       
    }
}
