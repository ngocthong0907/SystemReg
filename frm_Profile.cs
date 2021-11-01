using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace NinjaSystem
{
    public partial class frm_Profile : Form
    {
        public frm_Profile(List<Account> list_acc, frm_MainLD frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        List<Account> list_acc;
        bool stop = false;
        object synAcc = new object();
        frm_MainLD frm_main;
     
        ninjaDroidHelper droid = new ninjaDroidHelper();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        Thread thread_1;
        static object syncObjUID = new object();
        //List<string> list_uid = new List<string>();
        Random rd = new Random();
     
        List<LDRun> list_ldrun = new List<LDRun>();
        List<string> list_ld = new List<string>();
        LDController ld = new LDController();
        string result = "";
        // runLDs formLD = new runLDs();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void btnOpen_Click(object sender, EventArgs e)
        {
            PathFolderImage();
        }
        private void PathFolderImage()
        {
            var fldrDlg = new FolderBrowserDialog();
            fldrDlg.ShowDialog();
            string pathpic = fldrDlg.SelectedPath;
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                dr.Cells["clPic"].Value = pathpic;
                Account acc = (Account)dr.Tag;
                acc.pathpic = pathpic;
                nguoidung_bll.updatePost(acc);
            }
        }
        private void PathFolderCover()
        {
            var fldrDlg = new FolderBrowserDialog();
            fldrDlg.ShowDialog();
            string pathpic = fldrDlg.SelectedPath;
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                dr.Cells["clCover"].Value = pathpic;
                Account acc = (Account)dr.Tag;
                acc.pathCover = pathpic;
                nguoidung_bll.updateAvatarCover(acc);
            }
        }
        private void frmProfile_Load(object sender, EventArgs e)
        {
            ConfigFormProfile config = new ConfigFormProfile();
            try
            {
                string path = String.Format("{0}\\Config\\ConfigChangeProfile.data", Application.StartupPath);

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    config = JsonConvert.DeserializeObject<ConfigFormProfile>(json);
                }
                chkChangeName.Checked = config.has_name;
                txtTen.Text = config.ten;
                txtHo.Text = config.ho;
                chkchangePass.Checked = config.has_password;
                txtpass.Text = config.password;
                chkChangeAvatar.Checked = config.has_avatar;
                chkChangeCover.Checked = config.has_cover;
                numDelay.Value = config.delay;
                chkXoaAnh.Checked = config.has_removepic;
                chkLimitAcc.Checked = config.has_nick;
                numLimitAcc.Value = config.num_nick;
                chkRandomPassword.Checked = config.randompass;
                chkTurnoffNoti.Checked = config.turnoffnoti;

                txtCtity.Text = config.pathcity;
                txtCompany.Text = config.pathcompany;
                txtSchool.Text = config.pathschool;
                txtQuequan.Text = config.pathhometown;
            }
            catch
            { }
            method_LoadAccount();
        }
        private void method_LoadAccount()
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
                cell2.Value = acc.name;
                dataGridViewRow.Cells.Add(cell2);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell71 = new DataGridViewTextBoxCell();
                cell71.Value = acc.pathpic;
                dataGridViewRow.Cells.Add(cell71);

                DataGridViewTextBoxCell cell72 = new DataGridViewTextBoxCell();
                cell72.Value = acc.pathpost;
                dataGridViewRow.Cells.Add(cell72);

                DataGridViewTextBoxCell cell73 = new DataGridViewTextBoxCell();
                cell73.Value = acc.pathCover;
                dataGridViewRow.Cells.Add(cell73);

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
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };


            dialog.Filter = "File txt (*.txt)|*.txt";
            dialog.ShowDialog();
            string pathpost = dialog.FileName;
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                dr.Cells["clPost"].Value = pathpost;
                Account acc = (Account)dr.Tag;
                acc.pathpost = pathpost;
                nguoidung_bll.updatePost(acc);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (chkchangePass.Checked)
            {
                if (chkRandomPassword.Checked == false)
                {
                    if (string.IsNullOrEmpty(txtpass.Text))
                    {
                        MessageBox.Show("Hãy nhập password mới");
                        return;
                    }
                }
            }
            if (chkChangeAvatar.Checked)
            {
                foreach (DataGridViewRow dr in dgvUser.Rows)
                {
                    if ((bool)dr.Cells[0].Value & string.IsNullOrEmpty(dr.Cells["clPic"].Value.ToString()))
                    {
                        MessageBox.Show("Chưa thiết lập folder ảnh avatar");
                        return;
                    }
                }
            }
            if (chkChangeCover.Checked)
            {
                foreach (DataGridViewRow dr in dgvUser.Rows)
                {
                    if ((bool)dr.Cells[0].Value & string.IsNullOrEmpty(dr.Cells["clCover"].Value.ToString()))
                    {
                        MessageBox.Show("Chưa thiết lập folder ảnh cover");
                        return;
                    }
                }
            }


            tokenSource = new CancellationTokenSource();

            ConfigFormProfile config = new ConfigFormProfile();
            if (chkChangeName.Checked)
            {
                config.has_name = true;
            }
            else
            {
                config.has_name = false;
            }
            config.ten = txtTen.Text.Trim();
            config.ho = txtHo.Text.Trim();

            config.pathcity = txtCtity.Text;
            config.pathcompany = txtCompany.Text;
            config.pathschool = txtSchool.Text;
            config.pathhometown = txtQuequan.Text;

            if (chkchangePass.Checked)
            {
                config.has_password = true;
            }
            else
            {
                config.has_password = false;
            }
            config.password = txtpass.Text.Trim();
            if (chkChangeAvatar.Checked)
            {
                config.has_avatar = true;
            }
            else
            {
                config.has_avatar = false;
            }
            if (chkChangeCover.Checked)
            {
                config.has_cover = true;
            }
            else
            {
                config.has_cover = false;
            }
            config.delay = (int)numDelay.Value;
            if (chkXoaAnh.Checked)
            {
                config.has_removepic = true;
            }
            else
            {
                config.has_removepic = false;
            }
            if (chkLimitAcc.Checked)
            {
                config.has_nick = true;
            }
            else
            {
                config.has_nick = false;
            }
            config.num_nick = (int)numLimitAcc.Value;

            if (chkRandomPassword.Checked)
            {
                config.randompass = true;
            }
            else
            {
                config.randompass = false;
            }
            if (chkTurnoffNoti.Checked)
            {
                config.turnoffnoti = true;
            }
            else
            {
                config.turnoffnoti = false;
            }
            string path = String.Format("{0}\\Config\\ConfigChangeProfile.data", Application.StartupPath);
            File.WriteAllText(path, JsonConvert.SerializeObject(config));

            stop = false;
            setData();
            startTuongTac();

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
        private void startTuongTac()
        {
            stop = false;
            pibStatus.Visible = true;

            //chon cau hinh
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    list_dr.Add(row);
                    list_ld.Add(acc.ldid);
                }
            }
            list_ld = list_ld.Distinct().ToList();
            if (list_dr.Count == 0)
            {
                MessageBox.Show("Hãy chọn những tài khoản cần chạy");
                pibStatus.Visible = false;
                return;
            }
            else
            {
                pibStatus.Visible = true;
                if (changeIpHelper.checkWaitAny())
                {
                    this.thread_1 = new Thread(new ThreadStart(this.runLoginWaitAny));
                    thread_1.IsBackground = true;
                    this.thread_1.Start();
                }
                else
                {
                    this.thread_1 = new Thread(new ThreadStart(this.runTuongTac));
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

                    foreach (userLD ldopen in this.frm_main.list_ldopen)
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
        Lb_quayvong:
            var token = tokenSource.Token;
            int numthread = SettingTool.configld.numthread;
            if (numthread > list_ld.Count)
            {
                numthread = list_ld.Count;
            }
            xcontroller.createProxy(numthread);

            int maxproxy = 0;
            Task[] list_task = TaskController.createTask(numthread);
            if (list_ld.Count > 0)
            {

                #region doi ip truoc khi mo ld
                List<string> list_proxy = new List<string>();

                //if (SettingTool.configld.typeip == 6)
                //{

                //Lb_Start:
                //    method_log("Bắt đầu đổi ip bằng Tinsoft");
                //    TinsoftResult tinsoftresult = changeIpHelper.method_ChangeTinSoft(SettingTool.configld.apitinsoft);

                //    foreach (TinSoftModel ts in tinsoftresult.list_model)
                //    {
                //        method_log(String.Format("Api {0} - IP {1} - Next change {2} - Timout - {3}", ts.api, ts.proxy, ts.next_change, ts.timeout));
                //    }
                //    if (tinsoftresult.list_proxy.Count <= 0)
                //    {

                //        method_log("Lỗi lấy proxy tinsoft.Tiếp tục request");
                //        Thread.Sleep(5000);
                //        goto Lb_Start;
                //    }
                //    else
                //    {
                //        list_proxy = tinsoftresult.list_proxy;
                //    }
                //}
                //else
                {
                    if (SettingTool.configld.typeip == 7)
                    {
                        //ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                        //if (kq.status)
                        //{
                        //    list_proxy = SettingTool.list_xproxy;
                        //    method_log("Total Proxy: " + list_proxy.Count);
                        //}
                    }
                    else
                    {
                        if (SettingTool.configld.typeip == 2 || SettingTool.configld.typeip == 3)
                        {
                            ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                            if (kq.status)
                            {
                                method_log(kq.data);
                            }
                            else
                            {
                                method_log("Lỗi đổi ip: " + kq.data);
                                return;
                            }
                            //hma
                        }
                    }
                }
                #endregion
                int i = 0;

                object synDevice = new object();
                while (list_ld.Count > 0 &  TaskController.checkAvailableTask(list_task))
                {
                    string ldid = "";
                    string proxy = "";
                    List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
                    if (stop == false)
                    {
                        int index = TaskController.getAvailableTask(list_task);
                        if (index >= 0)
                        {
                            if (changeIpHelper.checkGetProxyWaitAny())
                            {
                                method_log("Đang lấy IP ");
                                proxy = xcontroller.getProxy();
                                if (!string.IsNullOrEmpty(proxy))
                                {
                                    method_log("Đã lấy IP " + proxy);
                                    Task task = Task.Factory.StartNew(() =>
                                    {
                                        if (list_ld.Count > 0)
                                        {
                                            lock (synDevice)
                                            {
                                                ldid = list_ld[0];
                                                list_ld.Remove(ldid);
                                                foreach (DataGridViewRow dr in list_dr)
                                                {
                                                    Account acc = (Account)dr.Tag;
                                                    if (acc.ldid == ldid)
                                                    {
                                                        list_acc.Add(dr);
                                                    }
                                                }

                                            }
                                            method_Start(ldid, list_acc, proxy, token);

                                        }


                                    }, token);
                                    list_task[index] = task;
                                    Thread.Sleep(SettingTool.configld.timedelay * 1000);

                                }
                                else
                                {
                                    Thread.Sleep(3000);
                                    method_log("Proxy chưa sẵn sàng: " + proxy);
                                    xcontroller.reset = "One";
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
                                    if (list_ld.Count > 0)
                                    {

                                        lock (synDevice)
                                        {
                                            ldid = list_ld[0];
                                            list_ld.Remove(ldid);
                                            foreach (DataGridViewRow dr in list_dr)
                                            {
                                                Account acc = (Account)dr.Tag;
                                                if (acc.ldid == ldid)
                                                {
                                                    list_acc.Add(dr);
                                                }
                                            }
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
                                        method_Start(ldid, list_acc, proxy, token);
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
                if (list_ld.Count > 0 && stop == false)
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
                    if (chkLimitAcc.Checked)
                    {
                        list_dr = new List<DataGridViewRow>();
                        foreach (DataGridViewRow row in dgvUser.Rows)
                        {
                            if ((bool)row.Cells[0].Value)
                            {
                                Account acc = (Account)row.Tag;
                                list_dr.Add(row);
                                list_ld.Add(acc.ldid);
                            }
                        }
                        if (list_dr.Count > 0)
                        {
                            goto Lb_quayvong;
                        }
                    }
                    method_Stop();
                }
            }
        }
        private void runTuongTac()
        {
        Lb_quayvong:
            var token = tokenSource.Token;
            int numthread = SettingTool.configld.numthread;
            if (numthread > list_ld.Count)
            {
                numthread = list_ld.Count;
            }
            if (list_dr.Count > 0)
            {

                #region doi ip truoc khi mo ld
                List<string> list_proxy = new List<string>();

                if (SettingTool.configld.typeip == 6)
                {

                Lb_Start:
                    method_log("Bắt đầu đổi ip bằng Tinsoft");
                    TinsoftResult tinsoftresult = changeIpHelper.method_ChangeTinSoft(SettingTool.configld.apitinsoft);

                    foreach (TinSoftModel ts in tinsoftresult.list_model)
                    {
                        method_log(String.Format("Api {0} - IP {1} - Next change {2} - Timout - {3}", ts.api, ts.proxy, ts.next_change, ts.timeout));
                    }
                    if (tinsoftresult.list_proxy.Count <= 0)
                    {

                        method_log("Lỗi lấy proxy tinsoft.Tiếp tục request");
                        Thread.Sleep(5000);
                        goto Lb_Start;
                    }
                    else
                    {
                        list_proxy = tinsoftresult.list_proxy;
                    }

                }
                else if (SettingTool.configld.typeip == 8)
                {
                Lb_Start:
                    method_log("Bắt đầu đổi ip bằng TM proxy");
                    TMproxyResult tinsoftresult = changeIpHelper.method_ChangeTMproxy(SettingTool.configld.apiTMproxy);

                    foreach (TMproxyModel ts in tinsoftresult.list_model)
                    {
                        method_log(String.Format("Api {0} - IP {1} - Next change {2}s - Timout - {3}ms", ts.api, ts.proxy, ts.next_request.ToString(), ts.timeout));
                    }
                    if (tinsoftresult.list_proxy.Count <= 0)
                    {

                        method_log("Lỗi lấy proxy tinsoft.Tiếp tục request");
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
                    if (SettingTool.configld.typeip == 9)
                    {
                        SettingTool.list_xproxy = new List<string>();
                        string path = SettingTool.linkproxyninja;
                        if (File.Exists(path))
                        {
                            SettingTool.list_xproxy = File.ReadLines(path).ToList();
                            xcontroller.createProxy(numthread);
                        }
                    }
                    else if (SettingTool.configld.typeip == 7)
                    {
                        ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                        if (kq.status)
                        {
                            list_proxy = SettingTool.list_xproxy;
                            method_log("Total Proxy: " + list_proxy.Count);
                        }
                    }
                    else
                    {
                        if (SettingTool.configld.typeip == 2 || SettingTool.configld.typeip == 3)
                        {
                            ResultRequest kq = changeIpHelper.connectBeforeOpen(richLogs);
                            if (kq.status)
                            {
                                method_log(kq.data);
                            }
                            else
                            {
                                method_log("Lỗi đổi ip: " + kq.data);
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
                for (int p = 0; p < numthread; p++)
                {
                    int t = p;
                    tasks[t] = Task.Factory.StartNew(() =>
                    {
                        string ldid = "";
                        string proxy = "";
                        List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
                        lock (synDevice)
                        {
                            ldid = list_ld[0];
                            list_ld.Remove(ldid);
                            foreach (DataGridViewRow dr in list_dr)
                            {
                                Account acc = (Account)dr.Tag;
                                if (acc.ldid == ldid)
                                {
                                    list_acc.Add(dr);
                                }
                            }
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
                        method_Start(ldid, list_acc, proxy, token);
                    }, token);
                    Thread.Sleep(SettingTool.configld.timedelay * 1000);
                }
                tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                try
                {
                    Task.WaitAll(tasks);
                }
                catch
                {
                    method_log("Đang dừng");
                }
                if (list_ld.Count > 0)
                {
                    goto Lb_quayvong;
                }
                else
                {
                    if (chkLimitAcc.Checked)
                    {
                        list_dr = new List<DataGridViewRow>();
                        foreach (DataGridViewRow row in dgvUser.Rows)
                        {
                            if ((bool)row.Cells[0].Value)
                            {
                                Account acc = (Account)row.Tag;
                                list_dr.Add(row);
                                list_ld.Add(acc.ldid);
                            }
                        }
                        if (list_dr.Count > 0)
                        {
                            goto Lb_quayvong;
                        }
                    }
                    method_Stop();
                }
            }
        }
        private void method_Stop()
        {
            pibStatus.Visible = false;

            stop = true;
            if (thread_1 != null)
                thread_1.Abort();
        }
        private void method_Start(string ldID, List<DataGridViewRow> list_acc, string proxy, CancellationToken token)
        {
            string messeage = "";
            method_log("Open LDPlayer Id: " + ldID);
            userLD u = frm_main.checkExits(ldID);
            frm_main.addLDToPanel(u);
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
                    method_log("Không kết nối được với LD: " + ldID);
                    goto Lb_Finish;
                }
            }
            try
            {
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

                ld.setKeyboard(ldID);

                DetailLD_BLL detail_bll = new DetailLD_BLL();
                DetailLDModel detailLd = new DetailLDModel();

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
                                method_log("Tắt LD do không lấy được ip public proxy: " + proxy);
                                goto Lb_Finish;

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
                                method_log("Tắt LD do không lấy được ip public proxy: " + proxy);
                                goto Lb_Finish;

                            }
                        }
                    }
                }

                changeIpHelper.connectAfterOpen(u, richLogs, ldID, token);
                #endregion

                //if (chkLimitAcc.Checked)
                //{
                //    if ((int)numLimitAcc.Value < list_acc.Count)
                //    {
                //        int deleteAcc = list_acc.Count - (int)numLimitAcc.Value;
                //        for (int n = 0; n < deleteAcc; n++)
                //        {
                //            list_acc.RemoveAt(rd.Next(0, list_acc.Count));
                //        }
                //    }
                //}
                int dem = 0;
                int int_limit = (int)numLimitAcc.Value;
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
                            dem++;
                            if (chkLimitAcc.Checked)
                            {
                                if (dem > int_limit)
                                {
                                    goto Lb_Finish;
                                }
                            }
                            messeage = "";
                            dr.Cells["Message"].Value = "Running";
                            Account acc = (Account)dr.Tag;
                            if (ld.checkAppCurrent(acc) == false)
                                ld.restoreAccount(acc.ldid, acc);
                            ld.killApp(acc.ldid, "com.facebook.katana");
                            ld.runApp(acc.ldid, "com.facebook.katana");


                            if (chkTurnoffNoti.Checked)
                            {
                                ld.killApp(acc.ldid, "com.facebook.katana");
                                ld.disableNotificationFacebook(acc.ldid);
                                ld.killApp(acc.ldid, "android.settings.APPLICATION_SETTINGS");
                                ld.runApp(acc.ldid, "com.facebook.katana");
                                goto Lb_Acc;
                            }
                            ld.checkOpenFacebookFinish(u, acc.ldid);
                            dr.Cells["Message"].Value = "Login Facebook";
                            u.setStatus(ldID, "Login Facebook...");

                            //ld.check_Facebook_has_stopped(u,acc.ldid, acc, token);
                            //bool status = ld.checkIsLogin(acc);
                            //if (status)
                            //{
                            //    status = true;
                            //}
                            //else
                            //{
                            //    ld.loginAvatarLD(acc);
                            //    status = ld.loginFacebookLD(acc, token);
                            //}
                            bool status = ld.loginFacebookTuongTac(u, acc, token);
                            if (status)
                            {
                                if (stop)
                                {
                                    goto Lb_Finish;
                                }
                                dr.Cells[0].Value = false;
                                dr.Cells["Message"].Value = "Đăng nhập thành công";
                                u.setStatus(ldID, "Đăng nhập thành công...");
                                acc.TrangThai = "Live";
                                dr.Cells["Status"].Value = "Live";
                                string ghichu = acc.nox;
                                bool removepic = false;
                                if (chkXoaAnh.Checked)
                                    removepic = true;
                                else
                                    removepic = false;
                                List<string> list_file = new List<string>();
                                List<string> list_file_cover = new List<string>();

                                messeage = "";
                                #region avatar
                                if (chkChangeAvatar.Checked)
                                {
                                    if (!System.IO.Directory.Exists(acc.pathpic))
                                    {
                                        dr.Cells["Message"].Value = "Chưa thiết lập file ảnh avatar";
                                    }
                                    else
                                    {
                                        list_file = System.IO.Directory.GetFiles(acc.pathpic, "*.jpg").ToList();

                                        if (list_file.Count > 0)
                                        {
                                            dr.Cells["Message"].Value = "Bắt đầu đổi avatar";
                                            u.setStatus(ldID, "Bắt đầu đổi avatar");
                                            if (ld.ChangeAvatar(acc.ldid, "com.facebook.katana", list_file, token, removepic))
                                            {
                                                messeage += "Đã thay đổi avatar";
                                                dr.Cells["Message"].Value = messeage;
                                                ghichu += " avatar";
                                                u.setStatus(ldID, "Đã thay đổi avatar");
                                            }
                                            else
                                            {
                                                messeage += "Không thể thay đổi avatar";
                                                dr.Cells["Message"].Value = messeage;
                                                u.setStatus(ldID, "Không thể thay đổi avatar");
                                            }
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Vui lòng chọn folder chứa ảnh";
                                        }
                                    }

                                }
                                #endregion
                                #region cover
                                if (chkChangeCover.Checked)
                                {

                                    list_file_cover = System.IO.Directory.GetFiles(acc.pathCover, "*.jpg").ToList();
                                    if (list_file_cover.Count > 0)
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu đổi cover";
                                        u.setStatus(ldID, "Bắt đầu đổi Cover");
                                        if (ld.ChangeCover(acc.ldid, "com.facebook.katana", list_file_cover, token, removepic))
                                        {
                                            dr.Cells["Message"].Value = "Đã thay đổi Cover";
                                            u.setStatus(ldID, "Đã thay đổi Cover");
                                            ghichu += " Cover";
                                            messeage += "Đã thay đổi Cover";
                                            dr.Cells["Message"].Value = messeage;
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Không thể thay đổi Cover";
                                            u.setStatus(ldID, "Không thể thay đổi Cover");
                                            messeage += "Không thể thay đổi Cover";
                                            dr.Cells["Message"].Value = messeage;
                                        }
                                    }
                                    else
                                        dr.Cells["Message"].Value = messeage + " Vui lòng chọn folder chứa ảnh";

                                }
                                #endregion

                                #region change_name
                                if (chkChangeName.Checked)
                                {
                                    //string cookie = ld.getcookie(acc);
                                    //string uid = acc.id;
                                    //if (string.IsNullOrEmpty(uid))
                                    //    uid = acc.email;

                                    //if (cookie.Contains(uid))
                                    //{
                                    if (!File.Exists(txtHo.Text))
                                    {
                                        dr.Cells["Message"].Value = "Vui lòng chọn file txt Ho";
                                        return;
                                    }
                                    if (!File.Exists(txtTen.Text))
                                    {
                                        dr.Cells["Message"].Value = "Vui lòng chọn file txt Ten";
                                        return;
                                    }
                                    List<string> ls_ho = File.ReadAllLines(txtHo.Text).ToList();
                                    List<string> ls_ten = File.ReadAllLines(txtTen.Text).ToList();
                                    if (ls_ho.Count > 0)
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu thay đổi Profile";
                                        u.setStatus(ldID, "Bắt đầu thay đổi Profile");
                                        if (ld.ChangeProfile(acc.ldid, acc, ls_ho, ls_ten, token))
                                        {
                                            dr.Cells["Message"].Value = "Đã thay đổi Profile";
                                            u.setStatus(ldID, "Đã thay đổi Profile");
                                            messeage += "Đã thay đổi Profile";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += " Đã thay đổi Profile";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Không thể thay đổi Profile";
                                            u.setStatus(ldID, "Không thể thay đổi Profile");
                                            messeage += "Không thể thay đổi Profile";
                                            dr.Cells["Message"].Value = messeage;
                                        }
                                    }
                                    else
                                        dr.Cells["Message"].Value = messeage + " Vui lòng chọn file txt Ten";
                                    //}
                                    //else
                                    //    dr.Cells["Message"].Value = "Không phải là account đang mở. Hãy kiểm tra lại";


                                }
                                #endregion

                                #region change_passaf
                                if (chkchangePass.Checked)
                                {
                                    string cookie = ld.getcookie(acc);
                                    if (cookie.Contains(acc.email) || cookie.Contains(acc.id))
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "Start change password";
                                            u.setStatus(ldID, " Start change password ");
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Bắt đầu thay đổi Password";
                                            u.setStatus(ldID, "Bắt đầu thay đổi Password ");
                                        }

                                        string newpass = txtpass.Text.Trim();
                                        if (chkRandomPassword.Checked)
                                        {
                                            newpass = "ST" + FunctionHelper.RandomString1(9);
                                        }
                                        else
                                        {
                                            newpass = txtpass.Text.Trim();
                                        }
                                        bool has_logout = false;
                                        if (chkLogoutAll.Checked)
                                        {
                                            has_logout = true;
                                        }
                                        else
                                        {
                                            has_logout = false;
                                        }
                                        if (ld.ChangePass(acc.ldid, acc, newpass, has_logout, token))
                                        {
                                            if (SettingTool.configld.language == "English")
                                            {
                                                dr.Cells["Message"].Value = "Changed Password sucessfull";
                                                u.setStatus(ldID, " Changed Password sucessfull");
                                                messeage += " Changed Password sucessfull";
                                                dr.Cells["Message"].Value = messeage;
                                                ghichu += " Changed Password sucessfull";
                                            }
                                            else
                                            {
                                                dr.Cells["Message"].Value = "Đã thay đổi Password";
                                                u.setStatus(ldID, "Đã thay đổi Password");
                                                messeage += "Đã thay đổi Password";
                                                dr.Cells["Message"].Value = messeage;
                                                ghichu += " Đã thay đổi Password";
                                            }

                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Không thể thay đổi Password";
                                            u.setStatus(ldID, "Không thể thay đổi Password");
                                            messeage += "Không thể thay đổi Password";
                                            dr.Cells["Message"].Value = messeage;
                                        }
                                    }
                                    else
                                        dr.Cells["Message"].Value = "Không phải là account đang mở. Hãy kiểm tra lại";

                                }
                                #endregion

                                #region change_city
                                if (chktinh.Checked)
                                {
                                    if (!File.Exists(txtCtity.Text))
                                        return;

                                    List<string> ls_city = File.ReadAllLines(txtCtity.Text).ToList();
                                    if (ls_city.Count == 0)
                                        return;

                                    if (SettingTool.configld.language == "English")
                                    {
                                        dr.Cells["Message"].Value = "Start change city";
                                        u.setStatus(ldID, " Start change city ");
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu thay đổi tỉnh thành phố";
                                        u.setStatus(ldID, "Bắt đầu thay đổi tỉnh thành phố");
                                    }

                                    if (ld.Changecity(acc.ldid, acc,ls_city[rd.Next(0,ls_city.Count)], token))
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "Changed city sucessfull";
                                            u.setStatus(ldID, " Changed city sucessfull");
                                            messeage += " Changed city sucessfull";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += " Changed city sucessfull";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Thay đổi tỉnh thành phố thành công";
                                            u.setStatus(ldID, "Thay đổi tỉnh thành phố thành công");
                                            messeage += "Thay đổi tỉnh thành phố thành công";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += "|Thay đổi tỉnh thành phố thành công";
                                        }

                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Không thể thay đổi tỉnh thành phố";
                                        u.setStatus(ldID, "Không thể thay đổi tỉnh thành phố");
                                        messeage += "Không thể thay đổi tỉnh thành phố";
                                        dr.Cells["Message"].Value = messeage;
                                    }
                                }
                                #endregion
                                #region change_workcompany
                                if (chkNoilamviec.Checked)
                                {
                                    if (!File.Exists(txtCompany.Text))
                                        return;

                                    List<string> ls_company = File.ReadAllLines(txtCompany.Text).ToList();
                                    if (ls_company.Count == 0)
                                        return;

                                    if (SettingTool.configld.language == "English")
                                    {
                                        dr.Cells["Message"].Value = "Start change work";
                                        u.setStatus(ldID, " Start change work");
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu thay đổi nghề nghiệp";
                                        u.setStatus(ldID, "Bắt đầu thay đổi nghề nghiệp");
                                    }

                                    if (ld.ChangeWork(acc.ldid, acc, ls_company[rd.Next(0,ls_company.Count)], token))
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "Changed work sucessfull";
                                            u.setStatus(ldID, " Changed work sucessfull");
                                            messeage += " Changed work sucessfull";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += " Changed work sucessfull";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "|Thay đổi nghề nghiệp thành công";
                                            u.setStatus(ldID, "|Thay đổi nghề nghiệp thành công");
                                            messeage += "|Thay đổi nghề nghiệp thành công";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += "|Thay đổi nghề nghiệp thành công";
                                        }
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Không thể thay đổi nghề nghiệp";
                                        u.setStatus(ldID, "Không thể thay đổi nghề nghiệp");
                                        messeage += "Không thể thay đổi nghề nghiệp";
                                        dr.Cells["Message"].Value = messeage;
                                    }
                                }
                                #endregion

                                #region change_school
                                if (chktruonghoc.Checked)
                                {
                                    if (!File.Exists(txtSchool.Text))
                                        return;

                                    List<string> ls_shool = File.ReadAllLines(txtSchool.Text).ToList();
                                    if (ls_shool.Count == 0)
                                        return;

                                    if (SettingTool.configld.language == "English")
                                    {
                                        dr.Cells["Message"].Value = "Start change school";
                                        u.setStatus(ldID, " Start change school");
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu thay đổi trường học";
                                        u.setStatus(ldID, "Bắt đầu thay đổi trường học");
                                    }

                                    if (ld.ChangeSchool(acc.ldid, acc, ls_shool[rd.Next(0, ls_shool.Count)], token))
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "Changed school sucessfull";
                                            u.setStatus(ldID, " Changed school sucessfull");
                                            messeage += " Changed school sucessfull";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += " Changed school sucessfull";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Thay đổi trường học thành công";
                                            u.setStatus(ldID, "Thay đổi trường học thành công");
                                            messeage += "Thay đổi trường học thành công";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += "|Thay đổi trường học thành công";
                                        }
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Không thể thay đổi trường học";
                                        u.setStatus(ldID, "Không thể thay đổi trường học");
                                        messeage += "Không thể thay đổi trường học";
                                        dr.Cells["Message"].Value = messeage;
                                    }
                                }
                                #endregion

                                #region change_Quequan
                                if (chkquequan.Checked)
                                {
                                    if (!File.Exists(txtQuequan.Text))
                                        return;

                                    List<string> ls_hometown = File.ReadAllLines(txtQuequan.Text).ToList();
                                    if (ls_hometown.Count == 0)
                                        return;

                                    if (SettingTool.configld.language == "English")
                                    {
                                        dr.Cells["Message"].Value = "Start change home town";
                                        u.setStatus(ldID, " Start change home town");
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu thay đổi quê quán";
                                        u.setStatus(ldID, "Bắt đầu thay đổi quê quán");
                                    }

                                    if (ld.ChangeQuequan(acc.ldid, acc, ls_hometown[rd.Next(0, ls_hometown.Count)], token))
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "Changed home town sucessfull";
                                            u.setStatus(ldID, " Changed home town sucessfull");
                                            messeage += " Changed home town sucessfull";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += " Changed home town sucessfull";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "|Thay đổi quê quán thành công";
                                            u.setStatus(ldID, "Thay đổi quê quán thành công");
                                            messeage += "Thay đổi quê quán thành công";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += "|Thay đổi quê quán thành công";
                                        }
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Không thể thay đổi quê quán";
                                        u.setStatus(ldID, "Không thể thay đổi quê quán");
                                        messeage += "Không thể thay đổi quê quán";
                                        dr.Cells["Message"].Value = messeage;
                                    }
                                }
                                #endregion

                                #region Them_sothich
                                if (chkhobby.Checked)
                                {
                                    if (SettingTool.configld.language == "English")
                                    {
                                        dr.Cells["Message"].Value = "Start change hobby";
                                        u.setStatus(ldID, " Start change hobby");
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu thay đổi sở thích";
                                        u.setStatus(ldID, "Bắt đầu thay đổi sở thích");
                                    }

                                    if (ld.ChangeHobby(acc.ldid, acc, "", token))
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "Changed hobby sucessfull";
                                            u.setStatus(ldID, " Changed hobby sucessfull");
                                            messeage += " Changed hobby sucessfull";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += " Changed hobby sucessfull";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "|Thay đổi sở thích thành công";
                                            u.setStatus(ldID, "Thay đổi sở thích thành công");
                                            messeage += "Thay đổi sở thích thành công";
                                            dr.Cells["Message"].Value = messeage;
                                            ghichu += "|Thay đổi sở thích thành công";
                                        }
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Không thể thay đổi sở thích";
                                        u.setStatus(ldID, "Không thể thay đổi sở thích");
                                        messeage += "Không thể thay đổi sở thích";
                                        dr.Cells["Message"].Value = messeage;
                                    }
                                }
                                #endregion

                                RequestParams para = new RequestParams();
                                RequestParams para_where = new RequestParams();
                                para_where["Id_account"] = acc.Id_account.ToString();
                                para["Nox"] = ghichu;
                                Data dt = new Data();
                                dt.update(para, "Account", para_where);
                                Thread.Sleep((int)numDelay.Value * 1000);
                            }
                            else
                            {

                                dr.Cells["Message"].Value = "Đăng nhập thất bại";
                                u.setStatus(ldID, "Đăng nhập thất bại ");
                                dr.Cells["Status"].Value = "Die";
                                acc.TrangThai = "Die";
                            }
                            if (chkLimitAcc.Checked)
                                dr.Cells[0].Value = false;
                        }

                    }
                    catch
                    { }

                    if (list_acc.Count > 0 && stop == false)
                    {
                        goto Lb_Acc;
                    }
                }

            }
            catch
            { }
        Lb_Finish:
            if (string.IsNullOrEmpty(proxy) == false)
            {
                ld.setProxyAdb(ldID, ":0");
            }

            if (!SettingTool.configld.has_quitLD)
            {
                ld.quit(ldID);
                frm_main.removeLDToPanel(u);
            }
            if (changeIpHelper.checkGetProxyWaitAny())
            {
                xcontroller.finishProxy(proxy);
            }

        }
        xProxyController xcontroller = new xProxyController();
        private void btnStop_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                chkChangeAvatar.Checked = false;
                chkChangeCover.Checked = false;
                chkChangeName.Checked = false;
                chkchangePass.Checked = false;
                chkLogoutAll.Checked = false;
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                chkquequan.Checked = false;
                chktinh.Checked = false;
                chkNoilamviec.Checked = false;
                chktruonghoc.Checked = false;
                chkhobby.Checked = false;
            }
        }



        private void btnImageAvatar_Click(object sender, EventArgs e)
        {
            PathFolderImage();
        }

        private void btnPathCover_Click(object sender, EventArgs e)
        {
            PathFolderCover();
        }

        private void ClearMessage()
        {
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                dgvUser.Rows[i].Cells["Message"].Value = "";

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = false;
            if (checkBox1.Checked)
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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };
            dialog.Filter = "File txt (*.txt)|*.txt";
            dialog.ShowDialog();
            txtHo.Text = dialog.FileName;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };
            dialog.Filter = "File txt (*.txt)|*.txt";
            dialog.ShowDialog();
            txtTen.Text = dialog.FileName;
        }

        private void dgvUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (DataGridViewRow row2 in dgvUser.SelectedRows)
                {
                    row2.Cells[0].Value = true;

                }
            }
        }

        private void btnpathcity_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };
            dialog.Filter = "File txt (*.txt)|*.txt";
            dialog.ShowDialog();
            txtCtity.Text = dialog.FileName;
        }

        private void btnpathcompany_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };
            dialog.Filter = "File txt (*.txt)|*.txt";
            dialog.ShowDialog();
            txtCompany.Text = dialog.FileName;
        }

        private void btnpathschool_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };
            dialog.Filter = "File txt (*.txt)|*.txt";
            dialog.ShowDialog();
            txtSchool.Text = dialog.FileName;
        }

        private void btnhometown_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };
            dialog.Filter = "File txt (*.txt)|*.txt";
            dialog.ShowDialog();
            txtQuequan.Text = dialog.FileName;
        }

    }
}
