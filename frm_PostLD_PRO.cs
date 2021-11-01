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
using SharpAdbClient;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using RestSharp;
using System.Diagnostics;
namespace NinjaSystem
{
    public partial class frm_PostLD_PRO : Form
    {
        public frm_PostLD_PRO(List<Account> list_acc, frm_MainLD_PRO frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        List<Account> list_acc;
        bool stop = false;
        object synAcc = new object();
        frm_MainLD_PRO frm_main;
        
        ninjaDroidHelper droid = new ninjaDroidHelper();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        Thread thread_1;
        static object syncObjUID = new object();
        //List<string> list_uid = new List<string>();
        Random rd = new Random();
        LDController ld = new LDController();
        string result = "";
        // runLDs formLD = new runLDs();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        string contentIDpost = "";
        List<string> list_file = new List<string>();
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
        private void frmPostLD_Load(object sender, EventArgs e)
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

                txtLink.Text = SettingTool.configPost.linkpost;
                txtNoidungDangLink.Text = SettingTool.configPost.contentlink;

                if (SettingTool.configld.language == "English")
                {
                    setupLanguage();
                }

            }
            catch
            { }
            method_LoadAccount();
        }
        private void setupLanguage()
        {
            this.Text = "Post";
            tabControl1.TabPages[0].Text = "Post quick";
            tabControl1.TabPages[1].Text = "Post manual";
            tabControl1.TabPages[2].Text = "Post from post id ";

            label10.Text = "this function allow post base on config for every acc"; //Tính năng này cho phép đăng bài theo thiết lập chung cho tất cả acc
            label7.Text = "Select image path";
            label9.Text = "Content share";

            groupBox1.Text = "Configuaration Post";
            rdLimit.Text = "Amount image for 1 post";
            rdAllFile.Text = "all file in folder";
            chkXoaAnh.Text = "Delete image when finish post";

            chkDeletecontent.Text = "Delete content when content is link";
            chkLoopRun.Text = "Loop run";
            label4.Text = "this function allow post base on config for each one acc"; //Tính năng này cho phép đăng bài theo thiết lập chung cho tất cả acc
            btnOpen.Text = "Choose folder image";
            btnOpenFile.Text = "Choose file content (*.txt)";
            label3.Text = "Attention content format {noidung1|noidung1....}";

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
            changeIpHelper.createLDID(SettingTool.configld.numthread);
            tokenSource = new CancellationTokenSource();
            SettingTool.configPost = new configFormPost();
            SettingTool.configPost.folderAnh = txtPathImagePast.Text;
            SettingTool.configPost.noidung = txtContent.Text;
            SettingTool.configPost.soluongAnh = (int)numPhoto.Value;
            SettingTool.configPost.delay = (int)numDelay.Value;

            SettingTool.configPost.linkpost = txtLink.Text;
            SettingTool.configPost.contentlink = txtNoidungDangLink.Text;

            ClearMessage();
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

            //chon cau hinh
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    Account acc = (Account)row.Tag;
                    list_dr.Add(row);

                }
            }

            if (list_dr.Count == 0)
            {
                if (SettingTool.configld.language == "English")
                    MessageBox.Show("Let to select several accounts to run");
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
                    this.thread_1 = new Thread(new ThreadStart(this.runTuongTacWaitAny));
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
        xProxyController xcontroller = new xProxyController();
        private void runTuongTacWaitAny()
        {
            try
            {
               
                int lap = 0;

                int numthread = SettingTool.configld.numthread;
                if (numthread > list_dr.Count)
                {
                    numthread = list_dr.Count;
                }
                if (SettingTool.configld.timeout == 0)
                {
                    SettingTool.configld.timeout = 20;
                }

                xcontroller.createProxy(numthread);

                //khoi tao list task
                Task[] list_task = TaskController.createTask(numthread);

                //if (SettingTool.configld.typeip == 9)
            //{
            //    SettingTool.list_xproxy = new List<string>();
            //    string path = SettingTool.linkproxyninja;
            //    if (File.Exists(path))
            //    {
            //        SettingTool.list_xproxy = File.ReadLines(path).ToList();
            //        xcontroller.createProxy(numthread);
            //    }
            //}
                int maxproxy = 0;
            Lb_quayvong:
                tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                if (list_dr.Count > 0)
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
                    while (TaskController.checkAvailableTask(list_task))
                    {
                        if (list_dr.Count <= 0)
                        {
                            break;
                        }
                        if (token.IsCancellationRequested == false)
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
                                                method_log("Đã hết tài khoản");
                                            }
                                        }
                                        else
                                        {
                                            method_log("Tất cả LD đang được sử dụng");
                                        }
                                    }, token);
                                    list_task[index] = task;
                                    Thread.Sleep(SettingTool.configld.timedelay * 1000);
                                }
                            }


                        }
                        else
                        {
                            break;
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
                        method_Stop();
                    }


                }
            }
            catch (Exception ex)
            {
                method_log(ex.ToString());
            }
        }
        private void runTuongTac()
        {
            var token = tokenSource.Token;
            xProxyController xcontroller = new xProxyController();
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
                        method_log(String.Format("Api {0} - IP {1} - Next change {2} - Timout - {3}", ts.api, ts.proxy, ts.next_request.ToString(), ts.timeout));
                    }
                    if (tinsoftresult.list_proxy.Count <= 0)
                    {

                        method_log("Lỗi lấy TM proxy.Tiếp tục request");
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
                                method_log("Đã hết tài khoản");
                            }
                        }
                        else
                        {
                            method_log("Tất cả LD đang được sử dụng");
                        }

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
                if (list_dr.Count > 0)
                {
                    goto Lb_quayvong;
                }
                else
                {
                    //if (chkLoopRun.Checked)
                    //{
                    //    method_log(String.Format("Vui lòng đợi {0} phút để tiếp tục tương tác", numWait.Value.ToString()));
                    //    Thread.Sleep((int)numWait.Value * 60000);
                    //    list_dr = new List<DataGridViewRow>();
                    //    foreach (DataGridViewRow row in dgvUser.Rows)
                    //    {
                    //        if ((bool)row.Cells[0].Value)
                    //        {
                    //            list_dr.Add(row);

                    //        }
                    //    }
                    //    list_dr = list_dr.Distinct().ToList();
                    //    if (list_dr.Count > 0)
                    //        goto Lb_quayvong;
                    //    else
                    //        method_Stop();
                    //}
                    //else
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
        private void method_Start(string ldID, DataGridViewRow dr, string proxy, CancellationToken token)
        {
            int loop = 0;
            string messeage = "";
            Account acc = (Account)dr.Tag;
            dr.Cells["clID"].Value = ldID;
            acc.ldid = ldID;
            dr.Cells["Message"].Value = "Restore Data LD";
            ld.setupLD(acc, ldID);
            dr.Cells["Message"].Value = "Open LD";
            userLD u = frm_main.checkExits(ldID);
            frm_main.addLDToPanel(u);
            if (ld.launchSetPosion(ldID, u, token))
            {
                u.setStatus(ldID, "Connect successful LD...");
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
                    goto Lb_Finish;
                }
            }
            ld.restoredatafb(acc.ldid, acc.id);
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
                if (ld.checkApp(ldID, "com.facebook.katana") == false)
                {
                    string path = Application.StartupPath + "\\app\\Facebook.apk";
                    if (File.Exists(path))
                    {
                        u.setStatus(ldID, "Install App Facebook...");
                        ld.installApp(ldID, path);
                        while (ld.checkApp(ldID, "com.facebook.katana") == false)
                        {
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(3000);
                    }
                }
                //set status

                ld.setKeyboard(ldID);
                DetailLD_BLL detail_bll = new DetailLD_BLL();
                DetailLDModel detailLd = new DetailLDModel();

                #region doi ip sau khi mo ld thanh cong
                if (string.IsNullOrEmpty(proxy) == false)
                {
                    u.setDevice(ldID, acc.id, proxy);
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
                        if (SettingTool.configld.checkproxy)
                        {
                            if (string.IsNullOrEmpty(yourip))
                            {
                                method_log("Tắt LD do không lấy được ip public proxy: " + proxy);
                                goto Lb_Finish;

                            }
                        }
                        u.setDevice(ldID, acc.id, proxy + " - " + yourip);
                    }
                }

                changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
                #endregion
                try
                {
                    messeage = "";
                    changeColor(dr, Color.Yellow);
                    dr.Cells["Message"].Value = "Running";
                    ld.killApp(acc.ldid, "com.facebook.katana");
                    ld.restoredatafb(acc.ldid, acc.id);
                    ld.runApp(acc.ldid, "com.facebook.katana");
                    ld.checkOpenFacebookFinish(u, acc.ldid);
                    dr.Cells["Message"].Value = "Login Facebook";
                    u.setStatus(ldID, " - Login Facebook...");
                    //ld.check_Facebook_has_stopped(u,acc.ldid, acc, token);
                    //bool status = ld.checkIsLogin(acc);
                    //if (status==false)
                    //{
                    //    ld.loginAvatarLD(acc);
                    //    status = ld.loginFacebookLD(acc, token);

                    //}
                    bool status = ld.loginFacebookTuongTac(u, acc, token);
                    if (status)
                    {
                        if (File.Exists(string.Format("c:\\test\\{0}\\tagfriend.txt", acc.ldid)))
                        {
                            File.Delete(string.Format("c:\\test\\{0}\\tagfriend.txt", acc.ldid));
                        }
                        StringBuilder strbuilder = new StringBuilder();
                        strbuilder.AppendLine("Gợi ý");
                        strbuilder.AppendLine("Gắn thẻ mọi người");
                        strbuilder.AppendLine("tất cả bạn bè");
                        strbuilder.AppendLine("tìm kiếm");
                        File.WriteAllText(string.Format("c:\\test\\{0}\\tagfriend.txt", acc.ldid), strbuilder.ToString());
                    lb_loop:
                        ld.activeNewfeed(acc.ldid, acc.app);
                        if (stop)
                        {
                            goto Lb_Finish;
                        }
                        dr.Cells["Message"].Value = "Connected successful";
                        u.setStatus(ldID, " - Connected successful...");
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
                            if (SettingTool.configld.language == "English")
                            {
                                dr.Cells["Message"].Value = "Postting..";
                                u.setStatus(ldID, " - Postting...");
                            }

                            else
                            {
                                dr.Cells["Message"].Value = "Đăng bài";
                                u.setStatus(ldID, " - Đăng bài...");
                            }


                            if (Directory.Exists(acc.pathpic))
                            {
                                list_file = System.IO.Directory.GetFiles(acc.pathpic, "*.*").ToList();
                            }
                            if (list_file.Count > 0)
                            {
                                dr.Cells["Message"].Value = ld.PostImages(u,acc,acc.ldid, acc.app, content, list_file, (int)numPhoto.Value, token, removepic, rdAllFile.Checked);
                                Thread.Sleep((int)numDelay.Value);
                            }
                            else
                            {
                                dr.Cells["Message"].Value = ld.PostContent(u,acc,acc.ldid, acc.app, content, chkDeletecontent.Checked, token);
                                Thread.Sleep((int)numDelay.Value);
                            }
                            #endregion
                        }
                        else if (rdo_postquick.Checked)
                        {
                            //if (txtPathImagePast.Text == "" || txtContent.Text == "")
                            //{
                            //    MessageBox.Show("Yêu cầu thông tin folder ảnh và nội dung bài viết");
                            //    return;
                            //}
                            #region dang nhanh
                            content = FunctionHelper.method_Spin(txtContent.Text);
                            dr.Cells["Message"].Value = "Đăng bài nhanh";
                            u.setStatus(ldID, " - Đăng bài nhanh...");
                            if (Directory.Exists(txtPathImagePast.Text))
                            {
                                list_file = System.IO.Directory.GetFiles(txtPathImagePast.Text, "*.*").ToList();

                                if (list_file.Count > 0)
                                {
                                    result = ld.PostImages(u,acc,acc.ldid, acc.app, content, list_file, (int)numPhoto.Value, token, removepic, rdAllFile.Checked);


                                    dr.Cells["Message"].Value = result;
                                    u.setStatus(ldID, result);
                                }

                                else
                                {
                                    result = ld.PostContent(u,acc,acc.ldid, acc.app, content, chkDeletecontent.Checked, token);
                                    dr.Cells["Message"].Value = result;
                                    u.setStatus(ldID, result);
                                }

                            }
                            else
                            {
                                result = ld.PostContent(u,acc,acc.ldid, acc.app, content, chkDeletecontent.Checked, token);
                                dr.Cells["Message"].Value = result;
                                u.setStatus(ldID, result);
                            }
                            #endregion
                            Thread.Sleep((int)numDelay.Value);
                        }
                        else if (rdoIDlink.Checked)
                        {
                            if (SettingTool.configld.language == "English")
                            {
                                dr.Cells["Message"].Value = "Post from ID other post ";
                                u.setStatus(ldID, "Post from ID other post... ");
                            }
                            else
                            {
                                dr.Cells["Message"].Value = "Đăng bài từ ID bài viết khác ";
                                u.setStatus(ldID, "Đăng bài từ ID bài viết khác ");
                            }
                            string tokens = ld.getToken(acc);

                            if (string.IsNullOrEmpty(tokens))
                            {

                                if (SettingTool.configld.language == "English")
                                    dr.Cells["Message"].Value = "Don't get Token, post fail";
                                else

                                    dr.Cells["Message"].Value = "Không copy được Token, đăng bài không thành công";
                            }

                            else if (rdoIDlink.Checked)
                            {
                                string path = Application.StartupPath + "\\img\\" + acc.ldid;
                                if (Directory.Exists(path))
                                {
                                    Directory.Delete(path, true);
                                }
                                Directory.CreateDirectory(path);
                                GetContent(acc.ldid, tokens, txtIDPost.Text, path);
                                list_file = System.IO.Directory.GetFiles(path, "*.*").ToList();
                                if (list_file.Count > 0)
                                {
                                    messeage += ld.PostImages(u,acc,acc.ldid, acc.app, contentIDpost, list_file, list_file.Count, token, false, rdAllFile.Checked);
                                    Thread.Sleep((int)numDelay.Value);
                                    dr.Cells["Message"].Value = messeage;
                                    u.setStatus(ldID, messeage);
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(contentIDpost))
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "Don't copy content";
                                            u.setStatus(ldID, "Don't copy content");
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Không copy được nội dung ";
                                            u.setStatus(ldID, "Không copy được nội dung ");
                                        }
                                    }
                                    else
                                    {
                                        messeage += ld.PostContent(u,acc,acc.ldid, acc.app, contentIDpost, false, token);
                                        u.setStatus(ldID, messeage);
                                    }
                                    Thread.Sleep((int)numDelay.Value);
                                }
                            }
                        }
                        else
                        {
                            #region dang kem link
                            content = FunctionHelper.method_Spin(txtNoidungDangLink.Text);
                            dr.Cells["Message"].Value = "Đăng bài kèm link";
                            u.setStatus(ldID, " - Đăng bài kèm link...");
                            result = ld.PostContentLink(acc.ldid, acc.app, content, txtLink.Text, chktag.Checked, (int)numfriend.Value, token);
                            dr.Cells["Message"].Value = result;
                            u.setStatus(ldID, result);
                            #endregion

                        }
                        Thread.Sleep((int)numDelay.Value * 1000);

                        if (chkLoopRun.Checked)
                        {

                            loop++;
                            if (loop <= (int)numaction.Value)
                            {
                                method_log(string.Format("vui lòng đợi {0} phút để tiếp tục tương tác", numWait.Value.ToString()));
                                Thread.Sleep((int)numWait.Value * 60000);
                                goto lb_loop;
                            }

                        }
                    }
                    else
                    {
                        if (SettingTool.configld.language == "English")
                        {
                            dr.Cells["Message"].Value = "Login fail";
                            u.setStatus(ldID, "Login fail ");
                        }
                        else
                        {
                            dr.Cells["Message"].Value = "Đăng nhập không thành công";
                            u.setStatus(ldID, "Đăng nhập không thành công");
                        }
                        dr.Cells["Status"].Value = "Die";
                        acc.TrangThai = "Die";
                    }
                    //if (chkLimitAcc.Checked)
                    //    dr.Cells[0].Value = false;

                }
                catch
                { }
                changeColor(dr, Color.White);
            }
            catch
            { }
        Lb_Finish:
            u.setStatus(ldID, "Backup Profile LD...");
            ld.Zip(acc, ldID);
            if (string.IsNullOrEmpty(proxy) == false)
            {
                ld.setProxyAdb(ldID, ":0");
            }
            ld.quit(acc, ldID);
            frm_main.removeLDToPanel(u);
            if (changeIpHelper.checkGetProxyWaitAny())
            {
                xcontroller.finishProxy(proxy);
            }
        }

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
            rdoIDlink.Checked = false;
            rdoPost.Checked = false;
            rdoDangLink.Checked = false;

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    rdo_postquick.Checked = true;
                    break;
                case 1:
                    rdoPost.Checked = true;
                    break;
                case 2:
                    rdoIDlink.Checked = true;
                    break;
                case 3:
                    rdoDangLink.Checked = true;
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
                if ((bool)dgvUser.Rows[i].Cells[0].Value)
                    dgvUser.Rows[i].Cells["Message"].Value = "";

                DataGridViewRow dr = dgvUser.Rows[i];
                changeColor(dr, Color.White);

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
        private void GetContent(string ldid, string token, string id_post, string path)
        {
            try
            {
                contentIDpost = "";
                list_file = new List<string>();
                string link = "https://graph.facebook.com/" + id_post + "?fields=id,from{id}&access_token=" + token;
                var client = new RestClient(link);
                var request = new RestRequest(Method.GET);
                string UserAgent = ld.getUserAgentLD(ldid);
                client.UserAgent = UserAgent;
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);
                string checkID = obj["id"].ToString();
                string id_poster = "";
                if (checkID.Contains("_"))
                {
                    string[] ls = checkID.Split('_');
                    id_poster = ls[0];
                }
                else
                    id_poster = obj["from"]["id"].ToString();


                link = string.Format("https://graph.facebook.com/fql?q=SELECT%20likes.count,%20comments.count,message,message_tags,attachment.media.fullsize_src,attachment.media.type%20FROM%20stream%20WHERE%20source_id%20=%20{0}%20and%20post_id%20=%20%27{0}_{1}%27&access_token={2}", id_poster, id_post, token);
                client = new RestClient(link);
                client.UserAgent = UserAgent;
                response = client.Execute(request);
                data = response.Content;
                obj = JObject.Parse(data);
                Random rd = new Random();
                contentIDpost = obj["data"][0]["message"].ToString();
                List<string> ls_picture = new List<string>();

                foreach (var item in obj["data"][0]["attachment"]["media"])
                {
                    if (item["type"].ToString() == "photo")
                    {
                        System.Drawing.Image image = DownloadImageFromUrl(item["fullsize_src"].ToString());
                        string fileName = System.IO.Path.Combine(path, "pic" + rd.Next(0, 10000000) + ".jpg");
                        image.Save(fileName);
                    }
                }
            }
            catch
            { }
        }
        private System.Drawing.Image DownloadImageFromUrl(string imageUrl)
        {
            System.Drawing.Image image = null;
            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();
                System.IO.Stream stream = webResponse.GetResponseStream();
                image = System.Drawing.Image.FromStream(stream);
                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            return image;
        }
        private sealed class Class34
        {
            public Color color_0;
            public DataGridViewRow dataGridViewRow_0;

            public void method_0()
            {
                this.dataGridViewRow_0.DefaultCellStyle.BackColor = this.color_0;
            }
        }

        private void richLogs_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("https://youtu.be/fTVIFWt3fbw");
        }

        private void chonDongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row2 in this.dgvUser.SelectedRows)
            {
                row2.Cells[0].Value = true;
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

    }
}
