using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using KAutoHelper;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Data.SQLite;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SharpAdbClient;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace NinjaSystem
{
    public partial class frm_ManageAccountLd : Form
    {
        public frm_ManageAccountLd()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;


        }
        int loop = 0;
        bool stop = false;
        object synAcc = new object();
        object synUID = new object();
        Thread thread_1;
        Thread thread_loop;
        CustomerTrialModel lic;
        Data dt = new Data();
        string userId = "";
        int groupId = 0;
        string email = "";
        string pass = "";
        ArrayList lstUser = new ArrayList();
        static object syncObj = new object();

        CheckBox headerCheckBox = new CheckBox();
        SettingTuongTac tuongtac = new SettingTuongTac();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        List<string> list_uid = new List<string>();

        List<DeviceData> list_devices;
        List<string> lsKeyword = new List<string>();
        CancellationTokenSource ts = new CancellationTokenSource();
        CancellationToken ct = new CancellationToken();
        int nodeActive;
        Random rdom = new Random();
        List<int> list_tuongtac = new List<int>();
        ninjaDroidHelper droid = new ninjaDroidHelper();
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();

            }
            catch
            { }

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            frm_AddGroup frm = new NinjaSystem.frm_AddGroup();
            frm.ShowDialog();
            trvNhom.Nodes.Clear();
            load_group();
        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            frm_Config frm = new frm_Config();
            frm.AccessibleDescription = userId;
            frm.ShowDialog();
            method_Config();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

            frm_ListDelete frm = new frm_ListDelete();
            frm.ShowDialog();
            load_group();
            trvNhom.SelectedNode = trvNhom.Nodes[nodeActive];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManageAccountLd_Load(object sender, EventArgs e)
        {
            //droid.frm = this;
            ToolTip tool = new ToolTip();
            tool.SetToolTip(btnAddGroup, "Thêm Nhóm");
            tool.SetToolTip(btn_addUser, "Thêm Nguời dùng");
            tool.SetToolTip(btnUpdate, "Sửa Nguời dùng");
            tool.SetToolTip(btnXoa, "Xóa Nguời dùng");
            tool.SetToolTip(btnChuyenNhom, "Chuyển nhóm");

            load_group();

            SettingTool.lang = new Language();
            SettingTool.lang.setData();
            SettingTool.lang.setDataLD();

            //
            loadDevice();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        }
        private void loadDevice()
        {



        }
        private void method_Config()
        {

        }

        private void load_group()
        {
            DataTable source = new DataTable();
            DataTable source_child = new DataTable();
            trvNhom.Nodes.Clear();
            source = dt.select("Select * from danhmuc");
            if (source.Rows.Count > 0)
            {
                groupId = int.Parse(source.Rows[0]["Id_danhmuc"].ToString());
                for (int i = 0; i < source.Rows.Count; i++)
                {
                    source_child = dt.select(string.Format("select * from Account where Id_danhmuc = {0}", source.Rows[i]["Id_danhmuc"].ToString()));
                    trvNhom.Nodes.Add(source.Rows[i]["Id_danhmuc"].ToString(), string.Format("{0}  ({1})", source.Rows[i]["Tendanhmuc"].ToString(), source_child.Rows.Count.ToString()), 0, 0);
                }
            }

        }
        private void changeColor(DataGridViewRow dataGridViewRow_0, Color color_0)
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
        private void load_user_by_groupId(int id)
        {
            dgvUser.Rows.Clear();
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            List<Account> list_acc = new List<Account>();
            list_acc = nguoidung_bll.loadUserbyNhomID(id);
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

                    }
                    else
                    {

                        dgvUser.Columns["Password"].Visible = true;
                    }
                    if (setup.HideEmail == "Y")
                        dgvUser.Columns["User"].Visible = false;
                    else
                        dgvUser.Columns["User"].Visible = true;

                    if (setup.HideUid == "Y")
                        dgvUser.Columns["UId"].Visible = false;
                    else
                        dgvUser.Columns["UId"].Visible = true;

                    if (setup.HidePrivate == "Y")
                        dgvUser.Columns["PrivateKey"].Visible = false;
                    else
                        dgvUser.Columns["PrivateKey"].Visible = true;

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

                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = acc.email;
                dataGridViewRow.Cells.Add(cell2);

                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = acc.Password;
                dataGridViewRow.Cells.Add(cell3);

                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = acc.privatekey;
                dataGridViewRow.Cells.Add(cell4);

                DataGridViewTextBoxCell cell4_1 = new DataGridViewTextBoxCell();
                cell4_1.Value = acc.friend_count;
                dataGridViewRow.Cells.Add(cell4_1);

                DataGridViewTextBoxCell cell4_2 = new DataGridViewTextBoxCell();
                cell4_2.Value = acc.group_count;
                dataGridViewRow.Cells.Add(cell4_2);


                DataGridViewTextBoxCell cell4_3 = new DataGridViewTextBoxCell();
                cell4_3.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell4_3);


                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = acc.os;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.Device_mobile;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = acc.appname;
                dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell71 = new DataGridViewTextBoxCell();
                cell71.Value = acc.nox;
                dataGridViewRow.Cells.Add(cell71);

                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                cell8.Value = acc.TrangThai;
                dataGridViewRow.Cells.Add(cell8);
                dataGridViewRow.Tag = acc;

                DataGridViewTextBoxCell cell9 = new DataGridViewTextBoxCell();
                cell9.Value = acc.Thongbao;
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



        private void btn_addUser_Click(object sender, EventArgs e)
        {
            frm_Import frmUser = new frm_Import();
            frmUser.AccessibleDescription = "Update$" + userId;
            frmUser.ShowDialog();
            trvNhom.Nodes.Clear();
            load_group();
            trvNhom.SelectedNode = trvNhom.Nodes[nodeActive];

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dgvUser.CurrentRow;
                Account acc = (Account)dr.Tag;
                frm_AddUser frm = new frm_AddUser(acc, "Update");

                userId = acc.Id_account.ToString();
                frm.AccessibleDescription = "Update$" + userId;
                frm.ShowDialog();
                load_group();
                //trvNhom.SelectedNode = nodeActive;
                trvNhom.SelectedNode = trvNhom.Nodes[nodeActive];
                // load_user_by_groupId(groupId);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string message = "Bạn có muốn xóa tài khoản?";
            string caption = "Thông báo";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            RequestParams para = new RequestParams();
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvUser.SelectedRows)
                {
                    Account acc = (Account)row.Tag;
                    para.Clear();
                    para.Add(new KeyValuePair<string, string>("Id_account", acc.Id_account.ToString()));
                    if (dt.delete("Account", para))
                    {
                        para.Clear();
                        para.Add(new KeyValuePair<string, string>("id_danhmuc", acc.id_danhmuc.ToString()));
                        para.Add(new KeyValuePair<string, string>("Email", acc.email));
                        para.Add(new KeyValuePair<string, string>("Password", acc.Password.Replace("'", "''")));
                        para.Add(new KeyValuePair<string, string>("TrangThai", acc.TrangThai));

                        dt.insert(para, "Account_Delete");
                    }

                }
                load_group();
                trvNhom.SelectedNode = trvNhom.Nodes[nodeActive];
            }
        }

        private void btnChuyenNhom_Click(object sender, EventArgs e)
        {
            lstUser.Clear();
            string message = string.Empty;
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                Account acc = (Account)row.Tag;
                lstUser.Add(acc.Id_account.ToString());
            }

            if (lstUser.Count > 0)
            {
                frm_TransUser frm = new frm_TransUser();
                frm.lstUserTrans = lstUser;
                frm.ShowDialog();

                load_group();
                trvNhom.SelectedNode = trvNhom.Nodes[nodeActive];
            }
            else
                MessageBox.Show("Hãy chọn những nguời dùng cần chuyển nhóm");
        }



        private void dgvUser_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                userId = dgvUser.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                email = dgvUser.Rows[e.RowIndex].Cells["User"].Value.ToString();
                pass = dgvUser.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                groupId = int.Parse(dgvUser.Rows[e.RowIndex].Cells["Nhom_id"].Value.ToString());
            }
            catch
            {

            }
        }

        private void ManageAccountLd_Activated(object sender, EventArgs e)
        {
            // trvNhom.Nodes.Clear();
            // load_group();

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

            SettingTool.lang.setDataLD();
            SettingTool.data = mydata;
        }

        private void runTuongTac()
        {
        Lb_Start:
            ct = ts.Token;
            int numthread = list_devices.Count;
            if (list_dr.Count > 0)
            {
                object synDevice = new object();
                Task[] tasks = new Task[numthread];
                for (int p = 0; p < numthread; p++)
                {
                    int t = p;
                    tasks[t] = Task.Factory.StartNew(() =>
                    {
                        DeviceData device = new DeviceData();
                        List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
                        lock (synDevice)
                        {
                            device = list_devices[0];
                            list_devices.RemoveAt(0);
                            //chon tai khoan cua device

                            foreach (DataGridViewRow dr in list_dr)
                            {
                                Account acc = (Account)dr.Tag;
                                if (acc.Device_mobile == device.Serial)
                                {
                                    list_acc.Add(dr);
                                }
                            }

                        }
                        method_Start(device, list_acc);

                    });
                    Delay(3);
                }
                Task.WaitAll(tasks);
                if (tuongtac.chkLoop_tuongtac)
                {
                    sendLogs(String.Format("Vui lòng đợi {0} phút để tiếp tục tương tác", tuongtac.numLoop_tuongtac));
                    Thread.Sleep(tuongtac.numLoop_tuongtac * 60000);
                    list_devices = droid.getDevices();
                    if (list_devices.Count > 0)
                    {
                        goto Lb_Start;
                    }
                    else
                    {
                        sendLogs("Không tìm thấy thiết bị để kết nối.");
                    }

                }
            }

        }

        private void method_Start(DeviceData device, List<DataGridViewRow> list_acc)
        {
            string pathapk = String.Format("{0}\\app\\ADBKeyboard.apk", Application.StartupPath);
            if (File.Exists(pathapk))
            {
                droid.installApk(device, pathapk);
            }
        Lb_Acc:
            if (list_acc.Count > 0)
            {
                try
                {

                    DataGridViewRow dr = null;
                    lock (synAcc)
                    {
                        dr = list_acc[0];
                        list_acc.Remove(dr);
                    }
                    if (dr != null)
                    {
                        int hour = DateTime.Now.Hour;
                        if (hour < tuongtac.timestart || hour >= tuongtac.timestop)
                        {
                            dr.Cells["Message"].Value = "Thời gian tương tác đã hết.Vui lòng kiểm tra trong cấu hình tương tác";

                            return;
                        }
                        
                        changeColor(dr, Color.Yellow);
                        dr.Cells["Message"].Value = "Running";
                        //close all app
                        droid.closeAllAppFacebook(device);

                        bool haslogin = false;
                        Account acc = new Account();
                        acc = (Account)dr.Tag;
                        //buoc 1 kiem tra app     
                        dr.Cells["Message"].Value = "Open Facebook";

                        droid.openAppFacebook(device, acc.app);
                        dr.Cells["Message"].Value = "Login Facebook";
                        bool status = droid.checkIsLogin(device);
                        if (status)
                        {
                            haslogin = true;
                        }
                        else
                        {
                            //haslogin = droid.loginAvatarLD(device, acc);
                            //if (haslogin == false)
                            //    haslogin = droid.LoginFacebook(device, acc.app, acc);
                            // mr thoong sua 
                            droid.loginAvatarLD(device, acc);
                            haslogin = droid.LoginFacebook(device, acc.app, acc);
                        }

                        if (haslogin)
                        {
                            dr.Cells["Message"].Value = "Đăng nhập thành công";
                            acc.TrangThai = "Live";
                            dr.Cells["Status"].Value = "Live";
                            string message = "";
                            int type = 0;
                            int step = 0;
                            List<string> list_tuongtac_done = new List<string>();
                            for (int i = 0; i < tuongtac.action; i++)
                            {
                                if (!droid.checkIsLogin(device))
                                    break;

                                if (tuongtac.runRandom) //neu chay tuan tu
                                {
                                    if (list_tuongtac_done.Count == list_tuongtac.Count)
                                        list_tuongtac_done.Clear();

                                lb_step:
                                    step = list_tuongtac[rdom.Next(0, list_tuongtac.Count)];

                                    if (list_tuongtac_done.Contains(step.ToString()))
                                        goto lb_step;
                                    else
                                        list_tuongtac_done.Add(step.ToString());

                                    type = step;

                                }

                                else
                                    type = list_tuongtac[rdom.Next(0, list_tuongtac.Count)];

                                #region bat dau tuong tac

                                int delay = rdom.Next(tuongtac.delaymin, tuongtac.delaymax);
                                 
                                switch (type)
                                {
                                    case 1:
                                        dr.Cells["Message"].Value = "Lướt Newfeed";
                                        droid.scrollNewfeed(device, acc.app, tuongtac.numslidemin);
                                        message += "Lướt Newfeed hoàn thành";
                                        break;
                                    case 2:
                                        dr.Cells["Message"].Value = "Like Newfeed";
                                        message += droid.scrollLikeNewFeed(device, acc.app, tuongtac.numlikenewfeedmin);
                                        break;
                                    case 3:
                                        dr.Cells["Message"].Value = "Comment Newfeed";
                                        message += droid.scrollCommentNewFeed(device, acc.app, tuongtac.numcommentnewfeedmin, tuongtac.message);
                                        break;
                                    case 4:
                                        dr.Cells["Message"].Value = "Share video";
                                        droid.viewVideoShare(device, tuongtac.numsharevideo);
                                        break;
                                    case 5:
                                        string keyword = "";
                                        if (tuongtac.strkeywordfanpage.Contains(","))
                                        {
                                            Random rd = new Random();
                                            string[] info = tuongtac.strkeywordfanpage.Split(',');
                                            keyword = info[rd.Next(0, info.Length)];
                                        }
                                        else
                                            keyword = tuongtac.strkeywordfanpage;

                                        if (!string.IsNullOrEmpty(keyword))
                                        {
                                            dr.Cells["Message"].Value = "Like Page";
                                            droid.seachLikePage(device, acc.app, keyword, tuongtac.numlikefanpagemin);
                                        }
                                        break;
                                    case 6:
                                        dr.Cells["Message"].Value = "Like Post Fanpage";
                                        message += droid.scrollLikePostPage(device, acc.app, tuongtac.numlikefanpagemin);
                                        break;
                                    case 7:
                                        dr.Cells["Message"].Value = "Comment Page";
                                        message += droid.scrollCommentPostPage(device, acc.app, tuongtac.numcommentpostfanpagemin, tuongtac.message); break;
                                    case 8:
                                        dr.Cells["Message"].Value = "Add Friend";
                                        message += droid.viewAddFriend(device, acc.app, tuongtac.numaddfriendmin, delay);
                                        break;
                                    case 9:
                                        dr.Cells["Message"].Value = "Accept Friend";
                                        message += droid.scrollAceeptFriend(device, acc.app, tuongtac.numacceptfriendmin, delay);
                                        break;
                                    case 10:
                                        string keyword2 = "";
                                        if (tuongtac.strkeywordseach.Contains(","))
                                        {
                                            Random rd = new Random();
                                            string[] info = tuongtac.strkeywordseach.Split(',');
                                            keyword2 = info[rd.Next(0, info.Length)];
                                        }
                                        else
                                            keyword2 = tuongtac.strkeywordseach;

                                        if (!string.IsNullOrEmpty(keyword2))
                                        {
                                            dr.Cells["Message"].Value = "Join Group";
                                            message += droid.scrollJoinGroup(device, acc.app, tuongtac.numjoingroupkeywordmin, keyword2, delay, tuongtac.chkAutoAnwser);

                                        }
                                        break;
                                    case 11:
                                        dr.Cells["Message"].Value = "Like Group";
                                        message += droid.scrollLikeGroup(device, acc.app, tuongtac.numlikepostgroupmin);
                                        break;
                                    case 12:
                                        dr.Cells["Message"].Value = "Comment Group";
                                        message += droid.scrollCommentGroup(device, acc.app, tuongtac.numcommentpostgroupmin, tuongtac.message);
                                        break;
                                    case 13:
                                        dr.Cells["Message"].Value = "Share Post";
                                        message += droid.scrollSharePost(device, acc.app, tuongtac.numsharepostmin, tuongtac.message);
                                        break;
                                    case 14:
                                        dr.Cells["Message"].Value = "Đọc thông báo";
                                        droid.Notification(device, acc.app);
                                        message += "| Đọc thông báo hoàn thành";
                                        break;

                                    case 15:
                                        dr.Cells["Message"].Value = "Seeding";
                                        droid.Seeding(device, tuongtac, acc);
                                        break;

                                    case 18:
                                        dr.Cells["Message"].Value = "Join group by ID";
                                        string uid;
                                        if (list_uid.Count == 0)
                                        {
                                            MessageBox.Show("Hãy thêm ID group vào tệp");
                                            return;
                                        }
                                        int count = 0;
                                        int max = 0;
                                        while (count < tuongtac.numGroupUIDMin)
                                        {
                                            if (list_uid.Count > 0)
                                            {
                                                lock (synUID)
                                                {
                                                    uid = list_uid[0];
                                                    list_uid.Remove(uid);
                                                }

                                                if (droid.scrollJoinGroupbyUID(device, acc.app, uid, delay, tuongtac.chkAutoAnwser))
                                                {
                                                    count++;
                                                    max = 0;
                                                }
                                                else
                                                {
                                                    max++;
                                                    if (max > 5)
                                                        break;

                                                }
                                            }
                                            else
                                                break;
                                        }

                                        File.WriteAllLines(tuongtac.strPath, list_uid);
                                        droid.activeNewfeed(device, acc.app);
                                        message += "Join group by ID hoàn thành" + count.ToString() + "/" + tuongtac.numGroupUIDMin.ToString();
                                        break;

                                    case 19:
                                        dr.Cells["Message"].Value = "Add friend by Newfeed";
                                        message += droid.viewAddFriendbyNewFeed(device, acc.app, tuongtac.numaddfriendNewfeedmin, delay);
                                        break;

                                    case 20:
                                        dr.Cells["Message"].Value = "Add friend by UID";
                                        message += AddFriendbyUID(device, acc.app, acc, tuongtac.numaddfrienduidmin, delay);
                                        break;
                                    //case 21:
                                    //    dr.Cells["Message"].Value = "Hủy mời kết bạn";
                                    //    droid.viewCancelFriend(device, acc.app, tuongtac.numCancelfriend, delay);
                                    //    break;

                                }
                                #endregion
                                if (stop)
                                    break;
                                dr.Cells["Message"].Value = "Delay : " + delay + "s";
                                Delay(delay);
                            }
                            //dr.Cells["Message"].Value = "Logout Facebook";
                            //  droid.logOut(deviceID, "");
                            dr.Cells["Message"].Value = message;

                        }
                        else
                        {
                            if (acc.Thongbao != null)
                                dr.Cells["Message"].Value = acc.Thongbao;
                            else
                                dr.Cells["Message"].Value = "Đăng nhập thất bại";
                            dr.Cells["Status"].Value = "Die";
                            acc.TrangThai = "Die";

                        }
                        NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                        nguoidung.updateStatus(acc);
                        changeColor(dr, Color.White);
                        if (list_acc.Count > 0 && stop == false)
                        {
                            goto Lb_Acc;
                        }

                    }
                }
                catch
                { }
            }
        }

        private string AddFriendbyUID(DeviceData deviceid, string app, Account acc, int numFriend, int delay)
        {
            string uid;
            int count = 0;
            string path = String.Format("{0}\\logs\\{1}_add.txt", Application.StartupPath, acc.email);
            string historyadd = "";
            try
            {
                historyadd = File.ReadAllText(path);
            }
            catch { }
            if (acc.pathUID == "")
            {
                MessageBox.Show("Chưa thiết lập file UID cho account " + acc.email);
                return "Chưa thiết lập file UID cho account";
            }
            List<string> list_uid = new List<string>();
            if (!File.Exists(acc.pathUID))
            {
                MessageBox.Show("Chưa thiết lập file UID cho account " + acc.email, "Thông Báo");
                return "|Add friend by UID Chưa thiết lập file UID cho account ";
            }

            list_uid = File.ReadAllLines(acc.pathUID).ToList().Distinct().ToList();
            if (list_uid.Count <= 0)
            {
                MessageBox.Show("Vui lòng thêm UID để add friend: " + acc.pathpost, "Thông Báo");
                return "|Add friend by UID vui lòng thêm UID để add friend: ";
            }
            StringBuilder list_history = new StringBuilder();
            int int_loilientiep = 0;
            while (count < numFriend)
            {
            Lb_Start:
                if (list_uid.Count <= 0)
                {
                    sendLogs("Đã hết UID");
                    goto Lb_FinishAcc;
                }
                uid = list_uid[0];
                list_uid.Remove(uid);
                if (historyadd.Contains(uid))
                {
                    goto Lb_Start;
                }
                int statusadd = droid.addFriendUID(deviceid, acc, uid);
                if (statusadd == 1)
                {
                    int_loilientiep = 0;
                    list_history.AppendLine(uid);
                    count++;
                    sendLogs(String.Format("Tài Khoan {0} kết bạn với {1} thành công", acc.email, uid));
                }
                else
                {
                    int_loilientiep++;
                    if (int_loilientiep >= SettingTool.configadd.maxerror)
                    {
                        sendLogs(String.Format("Dừng Tài Khoan {0} đã Lỗi liên tiếp {1}lần", acc.email, int_loilientiep));
                        goto Lb_FinishAcc;
                    }
                }
                if (count >= numFriend)
                {
                    goto Lb_FinishAcc;
                }
            }
        Lb_FinishAcc:
            sendLogs("Finish" + acc.email);

            File.AppendAllText(path, list_history.ToString());
            File.WriteAllLines(acc.pathUID, list_uid);
            return "|Add friend by UID hoàn thành " + count.ToString() + "/" + numFriend.ToString();
        }

        void Delay(double delay)
        {
            double delayTime = 0;
            while (delayTime < delay)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delayTime++;
            }
        }
        void PressOnLeapdroid_Khelper(IntPtr i_handle, string keys)
        {
            ADBHelper.InputText(i_handle.ToString(), keys);
            Delay(1);
            AutoControl.SendTextKeyBoard(i_handle, keys);

            // ADBHelper.InputText(i_handle, keys);
            //SendTextKeyBoard.PressOnLeapdroid(i_handle, keys);
        }
        void PressOnLeapdroid(string i_handle, string keys)
        {
            ADBHelper.InputText(i_handle, keys);
            //SendTextKeyBoard.PressOnLeapdroid(i_handle, keys);
        }



        private void deleteGroup_Click(object sender, EventArgs e)
        {
            string message = string.Format("Bạn có muốn xóa nhóm này?", trvNhom.SelectedNode.Name.ToString());
            string caption = "Thông báo";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            RequestParams para = new RequestParams();
            RequestParams para_GroupId = new RequestParams();

            para.Add(new KeyValuePair<string, string>("Id_danhmuc", trvNhom.SelectedNode.Name.ToString()));
            para_GroupId.Add(new KeyValuePair<string, string>("Id_danhmuc", trvNhom.SelectedNode.Name.ToString()));
            if (result == DialogResult.Yes)
            {
                dt.delete("danhmuc", para);
                dt.delete("Account", para_GroupId);
                load_group();
            }
        }

        private void btn_rename_Click(object sender, EventArgs e)
        {
            frm_AddGroup frm = new frm_AddGroup();
            frm.AccessibleDescription = "Update$" + groupId;
            frm.ShowDialog();
            trvNhom.Nodes.Clear();
            load_group();
        }
        private void ActiveFacebook()
        {


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
                        where += string.Format("Where email like '%{0}%' or id like '%{0}%'  ", keySearch[i]);
                    else
                        where += string.Format("or email like '%{0}%'  or id like '%{0}%' ", keySearch[i]);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkLicense();
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
                cus.Refer = "Login Ninja Add Friend";
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


        private void btn_config_Click(object sender, EventArgs e)
        {
            frm_Config frm = new frm_Config();
            frm.AccessibleDescription = userId;
            frm.ShowDialog();
            method_Config();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                if (checkBox1.Checked)
                {
                    dr.Cells[0].Value = true;
                }
                else
                    dr.Cells[0].Value = false;
            }
        }

        private void trvNhom_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvNhom.SelectedNode.Level == 0)
            {
                groupId = Convert.ToInt32(trvNhom.SelectedNode.Name.ToString());
                load_user_by_groupId(groupId);
            }
            nodeActive = trvNhom.SelectedNode.Index;
        }

        private void mnu_Login_Click(object sender, EventArgs e)
        {
            setData();
            NinjaADB ninjadb = new NinjaADB();
            //  ClearMessage();
            stop = false;


            list_devices = droid.getDevices();
            if (list_devices.Count > 0)
            {
                this.thread_1 = new Thread(new ThreadStart(this.setupAccount));
                thread_1.IsBackground = true;
                this.thread_1.Start();

            }
            else
            {
                MessageBox.Show("Vui lòng bật thiết bị Nox", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        public void setupAccount()
        {
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();

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
            foreach (DeviceData device in list_devices)
            {
                if (list_dr.Count > 0)
                {

                    //get list app setup
                    List<string> list_app = droid.getAppName(device);
                    if (list_app.Count > 0)
                    {
                        foreach (string appid in list_app)
                        {
                            //check exit app
                            if (nguoidung.loadUserbyApp(device.Serial, appid).Count <= 0)
                            {
                                if (list_dr.Count > 0)
                                {

                                    DataGridViewRow dr = list_dr[0];
                                    Account acc = (Account)dr.Tag;
                                    dr.Cells["Message"].Value = "Running";

                                    //droid.openAppFacebook(device, appid);
                                    //if(droid.checkIsLogin(device))
                                    //{
                                    //    droid.logOut(device, appid);
                                    //}
                                    //bool haslogin = droid.LoginFacebook(device, appid, acc);
                                    //if (haslogin)
                                    //{
                                    dr.Cells["Message"].Value = "Setup thành công";
                                    dr.Cells["Status"].Value = "Live";
                                    dr.Cells["clDevice"].Value = device.Serial;
                                    acc.TrangThai = "Live";
                                    acc.Thongbao = "Setup thành công";
                                    acc.Device_mobile = device.Serial;
                                    acc.app = appid;
                                    acc.os = device.Model;
                                    acc.appname = checkNameApp(acc.app);
                                    dr.Cells["clApp"].Value = acc.appname;
                                    dr.Cells["clPhone"].Value = acc.os;
                                    //}
                                    //else
                                    //{
                                    //    dr.Cells["Message"].Value = acc.message;
                                    //    acc.status = "Die";
                                    //}
                                    nguoidung.updateDevice(acc);
                                    list_dr.Remove(dr);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                }
            }

        }
        public void sendLogs(string string_15)
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
                if (richTextBox_0.Lines.Length > 500)
                    richTextBox_0.Text = "";
                if (this.string_0.Contains("being aborted"))
                {
                    this.string_0 = "Luồng đang chạy bị tạm ngừng -> STOP !!!";
                }
                this.richTextBox_0.Text = string.Format("{0}:{1}\n", DateTime.Now.ToString("HH:mm:ss dd/MM/yyy"), this.string_0) + this.richTextBox_0.Text;

            }
        }
        public string checkNameApp(string app)
        {
            if (app.Contains("facebook.katanb"))
            {
                return "Facebook01";
            }
            if (app.Contains("facebook.katanc"))
            {
                return "Facebook02";
            }
            if (app.Contains("facebook.katand"))
            {
                return "Facebook03";
            }
            if (app.Contains("facebook.katane"))
            {
                return "Facebook04";
            }
            if (app.Contains("facebook.katanf"))
            {
                return "Facebook05";
            }
            if (app.Contains("facebook.katang"))
            {
                return "Facebook06";
            }
            if (app.Contains("facebook.katanh"))
            {
                return "Facebook07";
            }
            if (app.Contains("facebook.katani"))
            {
                return "Facebook08";
            }
            if (app.Contains("facebook.katanj"))
            {
                return "Facebook09";
            }
            if (app.Contains("facebook.katank"))
            {
                return "Facebook10";
            }
            if (app.Contains("facebook.katanl"))
            {
                return "Facebook11";
            }
            if (app.Contains("facebook.katanm"))
            {
                return "Facebook12";
            }
            if (app.Contains("facebook.katann"))
            {
                return "Facebook13";
            }
            if (app.Contains("facebook.katano"))
            {
                return "Facebook14";
            }
            if (app.Contains("facebook.katanp"))
            {
                return "Facebook15";
            }
            if (app.Contains("facebook.katanq"))
            {
                return "Facebook16";
            }
            if (app.Contains("facebook.katanr"))
            {
                return "Facebook17";
            }
            if (app.Contains("facebook.katans"))
            {
                return "Facebook18";
            }
            if (app.Contains("facebook.katant"))
            {
                return "Facebook19";
            }
            if (app.Contains("facebook.katanu"))
            {
                return "Facebook20";
            }
            if (app.Contains("facebook.katanv"))
            {
                return "Facebook21";
            }
            if (app.Contains("facebook.katanw"))
            {
                return "Facebook22";
            }
            if (app.Contains("facebook.katanx"))
            {
                return "Facebook23";
            }
            if (app.Contains("facebook.katany"))
            {
                return "Facebook24";
            }
            if (app.Contains("facebook.katanz"))
            {
                return "Facebook25";
            }
            if (app.Contains("facebook.kataoa"))
            {
                return "Facebook26";
            }
            if (app.Contains("facebook.kataob"))
            {
                return "Facebook27";
            }
            if (app.Contains("facebook.kataoc"))
            {
                return "Facebook28";
            }
            if (app.Contains("facebook.kataod"))
            {
                return "Facebook29";
            }
            if (app.Contains("facebook.kataoe"))
            {
                return "Facebook30";
            }
            if (app.Contains("facebook.kataof"))
            {
                return "Facebook31";
            }
            if (app.Contains("facebook.kataog"))
            {
                return "Facebook32";
            }
            if (app.Contains("facebook.kataoh"))
            {
                return "Facebook33";
            }
            if (app.Contains("facebook.kataoi"))
            {
                return "Facebook34";
            }
            if (app.Contains("facebook.kataoj"))
            {
                return "Facebook35";
            }
            if (app.Contains("facebook.kataok"))
            {
                return "Facebook36";
            }
            if (app.Contains("facebook.kataol"))
            {
                return "Facebook37";
            }
            if (app.Contains("facebook.kataom"))
            {
                return "Facebook38";
            }
            if (app.Contains("facebook.kataon"))
            {
                return "Facebook39";
            }
            if (app.Contains("facebook.kataoo"))
            {
                return "Facebook40";
            }
            if (app.Contains("facebook.kataop"))
            {
                return "Facebook41";
            }
            if (app.Contains("facebook.kataoq"))
            {
                return "Facebook42";
            }
            if (app.Contains("facebook.kataor"))
            {
                return "Facebook43";
            }
            if (app.Contains("facebook.kataos"))
            {
                return "Facebook44";
            }
            if (app.Contains("facebook.kataot"))
            {
                return "Facebook45";
            }
            if (app.Contains("facebook.kataou"))
            {
                return "Facebook46";
            }
            if (app.Contains("facebook.kataov"))
            {
                return "Facebook47";
            }
            if (app.Contains("facebook.kataow"))
            {
                return "Facebook48";
            }
            if (app.Contains("facebook.kataoz"))
            {
                return "Facebook49";
            }
            if (app.Contains("facebook.katapa"))
            {
                return "Facebook50";
            }
            return "Facebook";
        }

        private void btnShareIntoGroup_Click(object sender, EventArgs e)
        {
            //frmShareNhom frm = new frmShareNhom();
            //frm.ShowDialog();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setData();
            NinjaADB ninjadb = new NinjaADB();
            //ClearMessage();
            stop = false;

            list_devices = droid.getDevices();
            if (list_devices.Count > 0)
            {
                this.thread_1 = new Thread(new ThreadStart(this.loginApp));
                thread_1.IsBackground = true;
                this.thread_1.Start();

            }
            else
            {
                MessageBox.Show("Vui lòng bật thiết bị Nox", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        public bool checkDeviceAccount(Account acc)
        {
            try
            {
                foreach (DeviceData device in list_devices)
                {
                    if (acc.Device_mobile == device.Serial)
                        return true;
                }
            }
            catch
            {
            }
            return false;
        }
        public bool checkAppAccount(Account acc, string deviceID)
        {
            try
            {
                DeviceData device = new DeviceData()
                {
                    State = DeviceState.Online,
                    Serial = deviceID
                };
                List<string> list_app = droid.getAppName(device);
                foreach (string appid in list_app)
                {
                    if (acc.app == appid)
                        return true;
                }
            }
            catch
            {
            }
            return false;
        }
        public void loginApp()
        {
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();

            //chon cau hinh
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    row.Cells["Message"].Value = "";
                    Account acc = (Account)row.Tag;
                    if (!String.IsNullOrEmpty(acc.Device_mobile))
                    {
                        if (checkDeviceAccount(acc))
                        {
                            list_dr.Add(row);
                        }
                        else
                        {
                            row.Cells["Message"].Value = "Thiết bị chưa được bật.Vui lòng bật Nox";
                        }

                    }
                    else
                    {
                        row.Cells["Message"].Value = "Tài khoản chưa được Setup";
                    }
                }
            }
            foreach (DataGridViewRow dr in list_dr)
            {
                if (list_dr.Count > 0)
                {
                    Account acc = (Account)dr.Tag;

                    if (checkAppAccount(acc, acc.Device_mobile))
                    {
                        dr.Cells["Message"].Value = "Running";
                        sendLogs(acc.Device_mobile);
                        DeviceData device = new DeviceData
                        {
                            State = DeviceState.Online,
                            Serial = acc.Device_mobile
                        };
                        droid.closeAllAppFacebook(device);
                        droid.openAppFacebook(device, acc.app);
                        bool status = droid.checkIsLogin(device);
                        if (status)
                        {
                            dr.Cells["Message"].Value = "Đăng nhập thành công";
                            dr.Cells["Status"].Value = "Live";
                            acc.TrangThai = "Live";
                            acc.Thongbao = "Đăng nhập thành công";
                            //acc.appname = checkNameApp(acc.app);
                            // dr.Cells["clApp"].Value = acc.appname;
                        }
                        else
                        {
                            dr.Cells["Message"].Value = acc.Thongbao;
                            acc.TrangThai = "Die";
                        }

                        nguoidung.updateDevice(acc);
                        dr.Cells["Message"].Value = "Hoàn thành";

                    }

                }
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
                    builder.AppendLine(acc.email);

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
                    builder.AppendLine(acc.email + "|" + acc.Password);

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
                    builder.AppendLine(acc.email + "|" + acc.Password + "|" + acc.privatekey);

                }
                Clipboard.Clear();
                Clipboard.SetText(builder.ToString());
            }
            catch
            {

            }
        }

        private void xóaAccKhỏiThiếtBịToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data dt = new Data();

            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    RequestParams para = new RequestParams();
                    RequestParams para_where = new RequestParams();
                    Account acc = (Account)row.Tag;
                    para_where["Id_account"] = acc.Id_account.ToString();
                    para["Nox"] = "";
                    para["Device_mobile"] = "";
                    para["App"] = "";
                    para["AppName"] = "";
                    para["Os"] = "";
                    dt.update(para, "Account", para_where);
                    row.Cells["clDevice"].Value = "";
                    row.Cells["clPhone"].Value = "";
                    row.Cells["clApp"].Value = "";
                }
            }

        }

      

        private void dgvUser_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.RowIndex >= 0)
            {
                DataGridViewRow dr = dgvUser.CurrentRow;
                Account acc = (Account)dr.Tag;
                string nox = "";
                try
                {
                    nox = dr.Cells["clNox"].Value.ToString();
                }
                catch
                { }

                RequestParams para = new RequestParams();
                RequestParams para_where = new RequestParams();

                para_where["Id_account"] = acc.Id_account.ToString();
                para["Nox"] = nox;
                dt.update(para, "Account", para_where);
            }
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
                        dr.Cells["clNox"].Value = clipboardText;
                }
            }
            catch
            {

            }
        }

        private void bunifuImageButton5_Click_1(object sender, EventArgs e)
        {
            setData();
            bool bl = txtSearch.Text.Any(char.IsLetter);
            DeviceData device = new DeviceData();
            Account acc = (Account)dgvUser.CurrentRow.Tag;
            device.Serial = acc.Device_mobile;

            //droid.scrollJoinGroup(device,acc.app, 3, "https://www.facebook.com/groups/TimBanDuLichNuocNgoai/permalink/1114005602131972/", 2);


            //setData();
            //Account acc = (Account)dgvUser.CurrentRow.Tag;
            //DeviceData device = new DeviceData();
            //device.Serial = acc.device;
            //device.State = DeviceState.Online;

            //var point = droid.FindXPath(device, "//node[contains(@class,'android.widget.TextView')]", "Log Into Another Account");
            // droid.scrollLikeNewFeed(device, 3);
            // droid.scrollCommentNewFeed(device, 3, "like");
            //  droid.scrollSharePost(device, 3);
            // droid.viewAddFriend(device, 3, 1);
            //  droid.skipLogin(device);
            // droid.scrollAceeptFriend(device, 3, 1);
            // droid.Notification(device);
            //  droid.scrollCommentGroup(device, 3, "hi");
            // droid.scrollLikeGroup(device, 3);
            // droid.scrollJoinGroup(device, 3, "watch", 2);
            // droid.scrollLikePostPage(device, 3);
            // droid.scrollCommentPostPage(device, 3, "hello");
            droid.open(device, acc.app);
        }

        private void loadDanhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvUser.Rows.Clear();
            load_user_by_groupId(groupId);
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            loadDevice();
        }


        private void loadUserbyDevice(DeviceData device)
        {
            dgvUser.Rows.Clear();
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            List<Account> list_acc = new List<Account>();
            list_acc = nguoidung_bll.loadUserbyDevice(device.Serial);
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }
        }
        private void loadUserbyListDevice(List<DeviceData> list_device)
        {
            dgvUser.Rows.Clear();
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            List<Account> list_acc = new List<Account>();
            string data = "'1'";
            foreach (DeviceData device in list_devices)
            {
                data = string.Format("{0},'{1}'", data, device.Serial);
            }
            list_acc = nguoidung_bll.loadUserbyListDevice(data);
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }

        }
        bool has_visible = true;
        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            if (has_visible)
            {
                has_visible = false;
                richLogs.Visible = true;
            }
            else
            {
                richLogs.Visible = false;
                has_visible = true;
            }

        }
        private void ClearMessage()
        {
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                if ((bool)dgvUser.Rows[i].Cells[0].Value)
                    dgvUser.Rows[i].Cells["Message"].Value = "";

                DataGridViewRow dr = dgvUser.Rows[i];
                changeColor(dr, Color.White);

            }
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setData();
            NinjaADB ninjadb = new NinjaADB();
            ClearMessage();
            stop = false;
            list_devices = droid.getDevices();
            if (list_devices.Count > 0)
            {
                this.thread_1 = new Thread(new ThreadStart(this.logoutApp));
                thread_1.IsBackground = true;
                this.thread_1.Start();
            }
            else
            {
                MessageBox.Show("Vui lòng bật thiết bị Nox", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        public void logoutApp()
        {
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();

            //chon cau hinh
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.SelectedRows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    row.Cells["Message"].Value = "";
                    Account acc = (Account)row.Tag;
                    if (!String.IsNullOrEmpty(acc.Device_mobile))
                    {
                        if (checkDeviceAccount(acc))
                        {
                            list_dr.Add(row);
                        }
                        else
                        {
                            row.Cells["Message"].Value = "Thiết bị chưa được bật.Vui lòng bật Nox";
                        }

                    }
                    else
                    {
                        row.Cells["Message"].Value = "Tài khoản chưa được Setup";
                    }
                }
            }
            foreach (DataGridViewRow dr in list_dr)
            {
                if (list_dr.Count > 0)
                {
                    Account acc = (Account)dr.Tag;

                    if (checkAppAccount(acc, acc.Device_mobile))
                    {
                        dr.Cells["Message"].Value = "Running";
                        sendLogs(acc.Device_mobile);
                        DeviceData device = new DeviceData
                        {
                            State = DeviceState.Online,
                            Serial = acc.Device_mobile
                        };
                        droid.closeAllAppFacebook(device);
                        droid.openAppFacebook(device, acc.app);
                        if (droid.checkIsLogin(device))
                        {
                            bool status = droid.logOut(device, acc.app);
                            if (status)
                            {
                                dr.Cells["Message"].Value = "Logout";
                                dr.Cells["Status"].Value = "Logout";
                            }
                            else
                            {
                                dr.Cells["Message"].Value = "Logout";
                            }
                        }
                    }

                }
            }

        }



     

        private void btnSync_Click(object sender, EventArgs e)
        {
            SyncAccount frm = new SyncAccount();
            frm.ShowDialog();
            trvNhom.Nodes.Clear();
            load_group();
            trvNhom.SelectedNode = trvNhom.Nodes[nodeActive];
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {


        }

        private void btn_config_devices_Click(object sender, EventArgs e)
        {
            string message = "Tính năng này giúp máy tính kết nối với điện thoại. \r\n Trong trường hợp tự động bị thất bại. \r\n Bạn có tiếp tục thực hiện?";
            string caption = "Thông báo";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                droid.startAdbserver("adb kill-server && adb start-server");
                //  Delay(2);
                //droid.startAdbserver("adb start-server");

                MessageBox.Show("Đã hoàn thành!");
            }
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

    

        private void luuwDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Account> list_acc = new List<Account>(0);
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                Account acc = (Account)dr.Tag;
                list_acc.Add(acc);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = string.Concat("Account_System", DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss"));
            saveFileDialog.Filter = "Backup Account System (*.ninjasystem)|*.ninjasystem";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string json1 = JsonConvert.SerializeObject(list_acc);
                //write string to file
                System.IO.File.WriteAllText(saveFileDialog.FileName, json1);

                MessageBox.Show("Lưu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }




       

        

      


        private void mnuJoinGroup_Click(object sender, EventArgs e)
        {

        }
        private void clearAll()
        {
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                dgvUser.Rows[i].Cells["Message"].Value = "";

            }
        }

        private void mnu_CheckInfo_Click(object sender, EventArgs e)
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
                            dr.Cells["Message"].Value = "Đang check";

                            if (profile.LoadInfo2Mobile(SettingTool.configld.cookies, dtsg, acc, null))
                            {
                                dr.Cells["Status"].Value = "Live";
                                dr.Cells["clName"].Value = acc.name;
                                dr.Cells["clFriend"].Value = acc.friend_count;
                                dr.Cells["clGroup"].Value = acc.group_count;
                                acc.TrangThai = "Live";
                                acc.Thongbao = "Đăng nhập thành công";
                                Thread.Sleep(2000);
                                dr.Cells["Message"].Value = "Hoàn thành check";

                            }
                            else
                            {
                                dr.Cells["Status"].Value = "Die";
                                acc.TrangThai = "Die";
                                acc.Thongbao = "Check UID Die";
                                Thread.Sleep(2000);
                                dr.Cells["Message"].Value = "Hoàn thành check";
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
    }
}

