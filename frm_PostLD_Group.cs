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
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
namespace NinjaSystem
{
    public partial class frm_PostLD_Group : Form
    {
        public frm_PostLD_Group(List<Account> list_acc, frm_MainLD frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        frm_MainLD frm_main;
        List<Account> list_acc;
        bool stop = false;
        object synAcc = new object();
        int countComplete = 0;
       
        ninjaDroidHelper droid = new ninjaDroidHelper();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        Thread thread_1;
        static object syncObjUID = new object();
        //List<string> list_uid = new List<string>();
        Random rd = new Random();

        List<LDRun> list_ldrun = new List<LDRun>();
        List<string> list_ld = new List<string>();
        LDController ld = new LDController();
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
        private void frmPostLD_Group_Load(object sender, EventArgs e)
        {
            SettingTool.configPost = new configFormPost();
            try
            {
                string path = String.Format("{0}\\Config\\ConfigFormPost.data", Application.StartupPath);

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    SettingTool.configPost = JsonConvert.DeserializeObject<configFormPost>(json);
                }

                txtPathImagePast.Text = SettingTool.configPost.folderAnh;
                txtContent.Text = SettingTool.configPost.noidung;
                numPhoto.Value = SettingTool.configPost.soluongAnh;
                numDelay.Value = SettingTool.configPost.delay;
                chkXoaAnh.Checked = SettingTool.configPost.chkDelete;
                // chkChangeProxy.Checked = SettingTool.configPost.chkProxy;
                if (SettingTool.configld.language == "English")
                {
                    setupLanguage();
                }
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
                cell2.Value = acc.id;
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
            tokenSource = new CancellationTokenSource();
            if (txt_id.Text == "" & rdoListID.Checked)
            {
                MessageBox.Show("Vui lòng nhập Id group");
                return;
            }
            if (txtPathImagePast.Text == "" & txtContent.Text == "")
            {
                if (SettingTool.configld.language == "English")
                    MessageBox.Show("Select image and content path folder to post");
                else
                    MessageBox.Show("Yêu cầu thông tin folder ảnh và nội dung bài viết");
                return;
            }
            SettingTool.configPost = new configFormPost();
            SettingTool.configPost.folderAnh = txtPathImagePast.Text;
            SettingTool.configPost.noidung = txtContent.Text;
            SettingTool.configPost.soluongAnh = (int)numPhoto.Value;
            SettingTool.configPost.delay = (int)numDelay.Value;

            //if (chkChangeProxy.Checked)
            //{
            //    SettingTool.configPost.chkProxy = true;
            //}
            //else
            //{
            //    SettingTool.configPost.chkProxy = false;
            //}

            if (chkXoaAnh.Checked)
            {
                SettingTool.configPost.chkDelete = true;
            }
            else
            {
                SettingTool.configPost.chkDelete = false;
            }

            string path = String.Format("{0}\\Config\\ConfigFormPost.data", Application.StartupPath);
            File.WriteAllText(path, JsonConvert.SerializeObject(SettingTool.configPost));

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
                if (SettingTool.configld.language == "English")
                    MessageBox.Show("Let select accounts to run!");
                else
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
        private void runLoginWaitAny()
        {
            var token = tokenSource.Token;

            int numthread = SettingTool.configld.numthread;
            if (numthread > list_ld.Count)
            {
                numthread = list_ld.Count;
            }
            Task[] list_task = TaskController.createTask(numthread);            
            xcontroller.createProxy(numthread);
        Lb_quayvong:
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
                    if (stop == false)
                    {
                        int index = TaskController.getAvailableTask(list_task);
                        if (index >= 0)
                        {
                            if (changeIpHelper.checkGetProxyWaitAny())
                            {
                                method_log("Đang lấy IP ");
                               string proxy = xcontroller.getProxy();
                               if (!string.IsNullOrEmpty(proxy))
                                {

                                    Task task = Task.Factory.StartNew(() =>
                                    {
                                        if (list_ld.Count > 0)
                                        {
                                            string ldid = "";
                                            
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
                                                 
                                            }
                                            method_Start(ldid, list_acc, proxy, token);
                                        }
                                    }, token);
                                    list_task[index] = task;
                                }
                                else
                                {
                                    xcontroller.reset = "One";
                                }
                            }
                            else
                            {
                                Task task = Task.Factory.StartNew(() =>
                                {
                                    if (list_ld.Count > 0)
                                    {
                                        string ldid = "";
                                        List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
                                        string proxy = "";
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
                    method_Stop();
                }
            }
        }
        private void runTuongTac()
        {
            var token = tokenSource.Token;
        Lb_quayvong:
            int numthread = SettingTool.configld.numthread;
            if (numthread > list_ld.Count)
            {
                numthread = list_ld.Count;
            }
            if (list_ld.Count > 0)
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
                        method_log(String.Format("Api {0} - IP {1} - Next change {2} - Timout - {3} - {4}", ts.api, ts.proxy, ts.next_change, ts.timeout, ts.description));
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
                    if (SettingTool.configld.typeip == 7)
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
                            list_ld.RemoveAt(0);
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
                    }, token);
                    Thread.Sleep(SettingTool.configld.timedelay * 1000);
                }
                tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                try
                {
                    Task.WaitAll(tasks);
                }
                catch
                { }

                if (list_ld.Count > 0)
                {
                    goto Lb_quayvong;
                }
                else
                {
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
            method_log("Open LDPlayer Id: " + ldID);
            userLD u = frm_main.checkExits(ldID);
            frm_main.addLDToPanel(u);
            if (ld.launchSetPosion(ldID, u, token))
            {
                u.setStatus(ldID, "Connect successfull LD...");
            }
            else
            {
                if (ld.autoRunLDSetPosition(ldID, u, token))
                {
                    u.setStatus(ldID, "Connect successfull LD...");
                }
                else
                {
                    u.setStatus(ldID, "Disconnected...");
                    method_log("Disconnected LD: " + ldID);
                    return;
                }
            }
            try
            {
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
                //get id_account running
                int Id_account = 0;
                DataGridViewRow drLD = list_acc[0];
                Account accLD = (Account)drLD.Tag;
                string pathLD = string.Format("{0}\\LD\\{1}.ninja", Application.StartupPath, accLD.ldid);
                List<DataGridViewRow> list_acc_sort = new List<DataGridViewRow>();
                if (File.Exists(pathLD))
                {
                    try
                    {
                        Id_account = Convert.ToInt16(File.ReadAllText(pathLD));
                    }
                    catch { }
                }
                if (Id_account > 0)
                {
                    foreach (DataGridViewRow dr in list_acc)
                    {
                        accLD = (Account)dr.Tag;
                        if (accLD.Id_account == Id_account)
                        {
                            list_acc_sort.Add(dr);
                            list_acc.Remove(dr);
                            break;
                        }
                    }
                    foreach (DataGridViewRow dr in list_acc)
                    {
                        list_acc_sort.Add(dr);

                    }
                }
                if (list_acc_sort.Count > 0)
                {
                    list_acc = list_acc_sort;
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
                            dr.Cells["Message"].Value = "Running";
                            Account acc = (Account)dr.Tag;
                            if (ld.checkAppCurrent(acc) == false)
                                ld.restoreAccount(acc.ldid, acc);
                            ld.killApp(acc.ldid, "com.facebook.katana");
                            ld.runApp(acc.ldid, "com.facebook.katana");
                           // ld.checkOpenFacebookFinish(u, acc.ldid);

                            dr.Cells["Message"].Value = "Login Facebook";
                            u.setStatus(ldID, "Login Facebook...");
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
                                u.setStatus(ldID, "Login successfull...");
                                dr.Cells["Message"].Value = "Login successful";
                                acc.TrangThai = "Live";
                                string messenger = "";
                                string ghichu = acc.nox;
                                bool removepic = false;
                                if (chkXoaAnh.Checked)
                                    removepic = true;
                                else
                                    removepic = false;
                                List<string> list_file = new List<string>();
                                List<string> list_file_cover = new List<string>();
                                string content = "";
                                if (rdoPost.Checked)
                                {
                                    #region dang bai
                                    int delay = (int)numDelay.Value;

                                    if (File.Exists(acc.pathpost))
                                    {
                                        string contentFile = File.ReadAllText(acc.pathpost);
                                        content = FunctionHelper.method_Spin(contentFile);
                                    }
                                    u.setStatus(ldID, "Đăng bài...");
                                    dr.Cells["Message"].Value = "Đăng bài";

                                    List<string> ls_id = new List<string>();
                                    List<GroupFB> ls_group = new List<GroupFB>();
                                    if (rdoWithout.Checked || rdoJoined.Checked)
                                    {

                                        string path = string.Format("c:\\test\\{0}\\pictures\\temp\\{0}.txt", acc.ldid);

                                        LDController controler = new LDController();
                                        ld.copyfileToken(acc.ldid);

                                        string access_token = "";
                                        if (File.Exists(path))
                                        {
                                            string html = File.ReadAllText(path);
                                            string uid = FunctionHelper.smethod_6(html, html.IndexOf("EAAAAUa"), "\\").Trim();
                                            access_token = Regex.Match(uid, @"([A-Z])\w+").Value;
                                        }
                                        Profile_Controller profile = new Profile_Controller();
                                        ls_group  = profile.LoadInfoGroup(access_token, "", acc, null);
                                        foreach (GroupFB gr in ls_group)
                                        {
                                            if (gr.status == "CAN_POST_WITHOUT_APPROVAL" & rdoWithout.Checked)
                                            {
                                                ls_id.Add(gr.id);
                                            }
                                            else
                                                ls_id.Add(gr.id);
                                        }

                                        if (ls_id.Count > 0)
                                        {
                                        lb_remove:
                                            if (ls_id.Count > numMaxPost.Value)
                                            {
                                                ls_id.RemoveAt(rd.Next(0, ls_id.Count));
                                                goto lb_remove;
                                            }
                                        }
                                        else
                                        {
                                            messenger += "Không có nhóm đáp ứng yêu cầu";
                                            u.setStatus(ldID, "Không có nhóm đáp ứng yêu cầu...");
                                            return;
                                        }
                                    }
                                    else
                                        ls_id = txt_id.Lines.ToList();

                                    for (int i = 0; i < ls_id.Count(); i++)
                                    {
                                        string result = ld.OpenLink(ldID, "com.facebook.katana", "fb://group/" + ls_id[i]);
                                        Thread.Sleep(4000);
                                        if (!result.Contains("Error"))
                                        {
                                            if (Directory.Exists(acc.pathpic))
                                            {
                                                list_file = System.IO.Directory.GetFiles(acc.pathpic, "*.*").ToList();
                                            }
                                            string namegr = "";
                                            if (list_file.Count > 0)
                                            {
                                                string resultone = "";
                                                resultone += "| Group Id " + (i + 1).ToString() + ld.PostImagesGroup(acc.ldid, acc.app, content, list_file, (int)numPhoto.Value, token, removepic);
                                                messenger += resultone;
                                                if (resultone.Contains("bài thành công"))
                                                {

                                                    foreach (GroupFB gr in ls_group)
                                                    {
                                                        if (gr.id == ls_id[i])
                                                        {
                                                            namegr = gr.name;
                                                            break;
                                                        }
                                                    }
                                                    File.AppendAllText(String.Format("{0}\\logs\\reportGr" + acc.id + ".txt", Application.StartupPath), DateTime.Now.ToString() + ": " + ls_id[i] + " - " + namegr + "\n");
                                                }

                                                Thread.Sleep((int)numDelay.Value);
                                            }
                                            else
                                            {
                                                string resultone = "";
                                                resultone +=  "| Group Id " + (i + 1).ToString() + ld.PostContent(u,acc,acc.ldid, acc.app, content, chkDeletecontent.Checked, token);
                                                messenger += resultone;
                                                if (resultone.Contains("bài thành công"))
                                                {
                                                    foreach (GroupFB gr in ls_group)
                                                    {
                                                        if (gr.id == ls_id[i])
                                                        {
                                                            namegr = gr.name;
                                                            break;
                                                        }
                                                    }
                                                    File.AppendAllText(String.Format("{0}\\logs\\reportGr" + acc.id + ".txt", Application.StartupPath), DateTime.Now.ToString() + ": " + ls_id[i] + " - " + namegr + "\n");
                                                }

                                                Thread.Sleep((int)numDelay.Value);
                                            }
                                        }
                                        else
                                            messenger += "| Group Id " + (i + 1).ToString() + " không mở được";

                                        Thread.Sleep((int)numDelay.Value * 1000);
                                    }
                                    dr.Cells["Message"].Value = messenger;
                                    u.setStatus(ldID, messenger);

                                    #endregion
                                }
                                else if (rdo_postquick.Checked)
                                {
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Postting...");
                                        dr.Cells["Message"].Value = "Possting..";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Đăng bài...");
                                        dr.Cells["Message"].Value = "Đăng bài";
                                    }

                                    List<string> ls_id = new List<string>();
                                    List<GroupFB> ls_group = new List<GroupFB>();
                                    if (rdoWithout.Checked || rdoJoined.Checked)
                                    {
                                        //string path = "c:\\test\\" + acc.ldid + ".txt";

                                        string path = string.Format("c:\\test\\{0}\\pictures\\temp\\{0}.txt", acc.ldid);
                                        ld.copyfileToken(acc.ldid);
                                        //  string cmd = String.Format("pull \"/storage/emulated/0/authentication\" \"{0}\"", path);
                                        // ld.runAdb(acc.ldid, cmd);
                                        string access_token = "";
                                        if (File.Exists(path))
                                        {
                                            string html = File.ReadAllText(path);
                                            string uid = FunctionHelper.smethod_6(html, html.IndexOf("EAAAAUa"), "\\").Trim();
                                            access_token = Regex.Match(uid, @"([A-Z])\w+").Value;
                                        }
                                        Profile_Controller profile = new Profile_Controller();
                                        ls_group = profile.LoadInfoGroup(access_token, "", acc, null);
                                        foreach (GroupFB gr in ls_group)
                                        {
                                            if (gr.status == "CAN_POST_WITHOUT_APPROVAL" & rdoWithout.Checked)
                                            {
                                                ls_id.Add(gr.id);
                                            }
                                            else
                                                ls_id.Add(gr.id);

                                        }

                                        if (ls_id.Count > 0)
                                        {
                                        lb_remove:
                                            if (ls_id.Count > (int)numMaxPost.Value)
                                            {
                                                ls_id.RemoveAt(rd.Next(0, ls_id.Count));
                                                goto lb_remove;
                                            }
                                        }
                                        else
                                        {

                                            if (rdoWithout.Checked)
                                            {
                                                if (SettingTool.configld.language == "English")
                                                    dr.Cells["Message"].Value = "not available no moderation group";
                                                else
                                                    dr.Cells["Message"].Value = "Không có nhóm không phê duyệt";
                                            }

                                            else if (rdoJoined.Checked)
                                            {
                                                if (SettingTool.configld.language == "English")
                                                    dr.Cells["Message"].Value = "not available joined group";
                                                else
                                                    dr.Cells["Message"].Value = "Chưa tham gia nhóm nào!";
                                            }

                                            goto Lb_Acc;
                                        }
                                    }

                                    else
                                        ls_id = txt_id.Lines.ToList();

                                    for (int i = 0; i < ls_id.Count(); i++)
                                    {
                                        content = FunctionHelper.method_Spin(txtContent.Text);
                                        Thread.Sleep(1000);
                                        string result = ld.OpenLink(ldID, "com.facebook.katana", "fb://group/" + ls_id[i]);
                                      
                                        if (!result.Contains("Error"))
                                        {
                                            if (Directory.Exists(txtPathImagePast.Text))
                                            {
                                                list_file = System.IO.Directory.GetFiles(txtPathImagePast.Text, "*.*").ToList();
                                            }
                                            string namegr = "";
                                            if (list_file.Count > 0)
                                            {
                                                string resultone = "";
                                                resultone += "| Group Id " + (i + 1).ToString() + ld.PostImagesGroup(acc.ldid, acc.app, content, list_file, (int)numPhoto.Value, token, removepic);
                                                messenger += resultone;
                                                if (resultone.Contains("bài thành công"))
                                                {
                                                    foreach (GroupFB gr in ls_group)
                                                    {
                                                        if (gr.id == ls_id[i])
                                                        {
                                                            namegr = gr.name;
                                                            break;
                                                        }
                                                    }
                                                    File.AppendAllText(String.Format("{0}\\logs\\reportGr" + acc.id + ".txt", Application.StartupPath), DateTime.Now.ToString() + ": " + ls_id[i] + " - " + namegr + "\n");
                                                }

                                                Thread.Sleep((int)numDelay.Value);
                                            }
                                            else
                                            {
                                                string resultone = "";
                                                resultone += "| Group Id " + (i + 1).ToString() + ld.PostContent(u, acc, acc.ldid, acc.app, content, chkDeletecontent.Checked, token);
                                                messenger += resultone;
                                                if (resultone.Contains("bài thành công"))
                                                {
                                                    foreach (GroupFB gr in ls_group)
                                                    {
                                                        if (gr.id == ls_id[i])
                                                        {
                                                            namegr = gr.name;
                                                            break;
                                                        }
                                                    }
                                                    File.AppendAllText(String.Format("{0}\\logs\\reportGr" + acc.id + ".txt", Application.StartupPath), DateTime.Now.ToString() + ": " + ls_id[i] + " - " + namegr + "\n");
                                                }

                                                Thread.Sleep((int)numDelay.Value);
                                            }
                                        }
                                        else
                                            messenger += "| Group Id " + (i + 1).ToString() + " don't open";
                                        Thread.Sleep((int)numDelay.Value * 1000);
                                    }
                                    dr.Cells["Message"].Value = messenger;
                                    u.setStatus(ldID, messenger);
                                    //list_file = System.IO.Directory.GetFiles(txtPathImagePast.Text, "*.jpg").ToList();
                                    //content = FunctionHelper.method_Spin(txtContent.Text);
                                    //dr.Cells["Message"].Value = ld.PostImages(acc.ldid, acc.app, content, list_file, (int)numPhoto.Value, removepic);
                                    Thread.Sleep((int)numDelay.Value);
                                }
                                Thread.Sleep((int)numDelay.Value * 1000);
                                countComplete++;
                                //if (countComplete >= SettingTool.configld.accountIP)
                                //{

                                //    countComplete = 0;
                                //    ResultRequest kq = changeIpHelper.connect(u, lbAddress.Text.Trim(), ldID, token);
                                //    u.setStatus(ldID, kq.data);

                                //}
                                //if (SettingTool.configld.changeHma)
                                //{
                                //    countComplete++;
                                //    if (countComplete >= SettingTool.configld.accountIP)
                                //    {
                                //        countComplete = 0;
                                //        u.setStatus(ldID, "Đang thay đổi IP...");
                                //        method_log("Đang thay đổi IP");
                                //        ld.changeIp();
                                //        int i = 10;
                                //        while (i > 0)
                                //        {
                                //            if (lbAddress.Text != ld.checkIP())
                                //            {
                                //                lbAddress.Text = ld.checkIP();
                                //                method_log("Đã thay đổi IP: " + lbAddress.Text);
                                //                u.setStatus(ldID, "Đã thay đổi IP: " + lbAddress.Text);
                                //            }
                                //            Thread.Sleep(1000);
                                //            i--;
                                //        }
                                //    }
                                //}
                            }
                            else
                            {
                                if (acc.Thongbao != null)
                                    dr.Cells["Message"].Value = acc.Thongbao;
                                else
                                    dr.Cells["Message"].Value = "Login fail";
                                u.setStatus(ldID, "Login fail...");
                                dr.Cells["Status"].Value = "Die";
                                acc.TrangThai = "Die";
                            }

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
            {

            }
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
            rdo_postquick.Checked = false;

            rdoPost.Checked = false;

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    rdo_postquick.Checked = true;
                    break;
                case 1:
                    rdoPost.Checked = true;
                    break;

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathImagePast.Text = fldrDlg.SelectedPath;
            }
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void chọnDòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row2 in this.dgvUser.SelectedRows)
            {
                row2.Cells[0].Value = true;
            }
        }
        private void setupLanguage()
        {
            this.Text = "Post";
            tabControl1.TabPages[0].Text = "Post quick";
            tabControl1.TabPages[1].Text = "Post manual";


            label10.Text = "this function allow post base on config for every acc"; //Tính năng này cho phép đăng bài theo thiết lập chung cho tất cả acc
            label7.Text = "Select image path";
            label9.Text = "Content share";

            groupBox1.Text = "Configuaration Post";
            label2.Text = "Amount image for 1 post";
            chkXoaAnh.Text = "Delete image when finish post";
            chkDeletecontent.Text = "Delete content when content is link";

            label4.Text = "this function allow post base on config for each one acc"; //Tính năng này cho phép đăng bài theo thiết lập chung cho tất cả acc
            btnOpen.Text = "Choose folder image";
            btnOpenFile.Text = "Choose file content (*.txt)";
            label3.Text = "Attention content format {noidung1|noidung1....}";

            label11.Text = "groups";
            label12.Text = "Post into ";

            rdoListID.Text = "Post into list ID group";
            rdoJoined.Text = "Post into group joined";
            rdoWithout.Text = "Post into group without approval ";
        }

    }
}
