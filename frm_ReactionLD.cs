using Newtonsoft.Json;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_ReactionLD : Form
    {
        public frm_ReactionLD(List<Account> list_acc, frm_MainLD frm_main, bool auto)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
            autorun = auto;
        }
        bool autorun = false;
        List<Account> list_acc;
        bool stop = false;
        object synAcc = new object();
        //  int countComplete = 0;

        ninjaDroidHelper droid = new ninjaDroidHelper();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        Thread thread_1;
        static object syncObjUID = new object();
        //List<string> list_uid = new List<string>();
        Random rd = new Random();
        List<int> list_tuongtac = new List<int>();
        List<LDRun> list_ldrun = new List<LDRun>();
        List<string> list_ld = new List<string>();
        LDController ld = new LDController();
        List<string> list_uid = new List<string>();
        List<string> list_Pageid = new List<string>();
        Random rdom = new Random();
        frm_MainLD frm_main;
        object synUID = new object();
        // List<string> ls_idlink = new List<string>();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void frmReactionLD_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\Config\\ListUser.txt"))
                {
                    List<string> lsUser = new List<string>();
                    lsUser = File.ReadAllLines(Application.StartupPath + "\\Config\\ListUser.txt").ToList();
                    if (!lsUser.Contains(SettingTool.email))
                    {
                        tabControl1.TabPages.Remove(tabPage2);
                    }
                   
                }

                string path = String.Format("{0}\\Config\\ConfigCarePage.data", Application.StartupPath);
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    SettingTool.configCarepage = JsonConvert.DeserializeObject<configFormCareFanpage>(json);

                    txtIDpage.Text = SettingTool.configCarepage.linkID;
                    txt_comment.Text = SettingTool.configCarepage.contentComment;
                    numfromLike.Value = SettingTool.configCarepage.numLikefrom;
                    numtoLike.Value = SettingTool.configCarepage.numLiketo;

                    numfromComment.Value = SettingTool.configCarepage.numCommentfrom;
                    numtoComment.Value = SettingTool.configCarepage.numCommentto;

                    numfromTag.Value = SettingTool.configCarepage.numTagfrom;
                    numtoTag.Value = SettingTool.configCarepage.numTagto;

                    numfromshare.Value = SettingTool.configCarepage.numSharefrom;
                    numtoShare.Value = SettingTool.configCarepage.numShareto;

                    numfromLienquan.Value = SettingTool.configCarepage.numLienquanfrom;
                    numtoLienquan.Value = SettingTool.configCarepage.numLienquanto;

                    numfromQuangcao.Value = SettingTool.configCarepage.numQuangcaofrom;
                    numtoQuangcao.Value = SettingTool.configCarepage.numQuangcaoto;

                    numfromDelay.Value = SettingTool.configCarepage.numDelayfrom;
                    numtoDelay.Value = SettingTool.configCarepage.numDelayto;

                    numdlfromscroll.Value = SettingTool.configCarepage.numdelayscrollfrom;
                    numdltoscroll.Value = SettingTool.configCarepage.numdelayscrollto;

                    chkLikefanpage.Checked = SettingTool.configCarepage.chkLikeFanpage;
                    chkrandomAction.Checked = SettingTool.configCarepage.chkRandomaction;

                    numAcction.Value = SettingTool.configCarepage.numrandomaction;

                    chklikepost.Checked = SettingTool.configCarepage.chkLike;
                    chkcommentpost.Checked = SettingTool.configCarepage.chkComment;
                    chkTag.Checked = SettingTool.configCarepage.chkTag;
                    chkShare.Checked = SettingTool.configCarepage.chkShare;

                    chkLienquan.Checked = SettingTool.configCarepage.chkLienquan;
                    chkQuangcao.Checked = SettingTool.configCarepage.chkQuangcao;

                }
            }
            catch
            {

            }

            method_LoadAccount();
            method_Config();
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }
            if (autorun)
            {
                Thread.Sleep(3000);
                tabControl1.SelectedIndex = 1;
                bunifuImageButton2_Click

                    (new object(), new EventArgs());
            }
        }
        private void setupLanguage()
        {
            label10.Text = "Minutes";
            chkLoopRun.Text = "Loop run after";
            chkLimitAcc.Text = "Limited nick 1 LD run ";
            chkSplit.Text = "Split list ID ";
            label2.Text = "seconds";
            label3.Text = "List ID (1 ID each Line)";
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

        private bool setupCauHinh()
        {

            return true;
        }
        private void method_Config()
        {
            try
            {
                cboConfig.Items.Clear();
                List<CauHinh> list_danhmuc = new List<CauHinh>();
                CauHinh_Bll cauhinh_bll = new CauHinh_Bll();
                list_danhmuc = cauhinh_bll.selectAll();
                ComboboxItem item = new ComboboxItem();

                foreach (CauHinh dm in list_danhmuc)
                {
                    item = new ComboboxItem();
                    item.Text = dm.Name;
                    item.Tag = dm;
                    cboConfig.Items.Add(item);

                }
                item.Text = "Ngẫu Nhiên";
                cboConfig.Items.Add(item);

                if (cboConfig.Items.Count > 0)
                {
                    cboConfig.SelectedIndex = 0;
                }
            }
            catch
            { }
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
                cell2.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell2);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = acc.id;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.name;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = acc.TrangThai;
                dataGridViewRow.Cells.Add(cell7);

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

        private void btnRun_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            if (txtID.Text == "")
            {
                if (SettingTool.configld.language == "English")
                    MessageBox.Show("Let input list ID");
                else
                    MessageBox.Show("Hãy nhập Id của Group");
                return;
            }
            if (txtComment.Text == "")
            {
                if (SettingTool.configld.language == "English")
                    MessageBox.Show("Let input comment content");
                else
                    MessageBox.Show("Hãy nhập nội dung bình luận");
                return;
            }
            ClearMessage();
            list_ldrun = new List<LDRun>();
            list_ld = new List<string>();

            list_uid = txtID.Lines.ToList();

            list_Pageid = txtIDpage.Lines.ToList();
            string pathlog = Application.StartupPath + "\\logs";
            if (!Directory.Exists(pathlog))
            {
                Directory.CreateDirectory(pathlog);
            }
            startTuongTac();
        }
        // string idlink = "";
        private void startTuongTac()
        {
            stop = false;

            pibStatus.Visible = true;



            //chon cau hinh
            if (setupCauHinh())
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
            else
            {
                MessageBox.Show("Vui lòng chọn cấu hình trước khi chạy tương tác", "Thông báo");
            }
        }

        private void startTuongTacCarePage()
        {
            stop = false;
            pibStatus.Visible = true;
            picstatus.Visible = true;
            //ls_idlink = txtIDpage.Lines.ToList();

            //if (autorun)
            //    ls_idlink.Remove(idlink);

            //idlink = ls_idlink[0];

            //string stridlink = string.Join(" ", ls_idlink.ToArray());
            //stridlink = stridlink.Replace(" ", "\n");
            //txtIDpage.Text = stridlink;

            saveinfo();
            //chon cau hinh
            if (setupCauHinh())
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
                list_ld = list_ld.Distinct().ToList();
                if (list_dr.Count == 0)
                {
                    MessageBox.Show("Hãy chọn những tài khoản cần chạy");
                    pibStatus.Visible = false;
                    picstatus.Visible = false;
                    return;
                }
                else
                {
                    pibStatus.Visible = true;
                    picstatus.Visible = true;
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
            else
            {
                MessageBox.Show("Vui lòng chọn cấu hình trước khi chạy tương tác", "Thông báo");
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
            int maxproxy = 0;
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
                while (list_ld.Count > 0 & TaskController.checkAvailableTask(list_task))
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

                    if (chkLoopRun.Checked)
                    {
                        method_log(String.Format("Vui lòng đợi {0} phút để tiếp tục tương tác", numWait.Value.ToString()));
                        Thread.Sleep((int)numWait.Value * 60000);
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
                        if (list_dr.Count > 0)
                            goto Lb_quayvong;
                        else
                            method_StopAddFriend();
                    }
                    else
                        method_StopAddFriend();
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
                    method_log("Đang dừng tương tác");
                }

                if (list_ld.Count > 0)
                {
                    goto Lb_quayvong;
                }
                else
                {
                    if (chkLoopRun.Checked)
                    {
                        method_log(String.Format("Vui lòng đợi {0} phút để tiếp tục tương tác", numWait.Value.ToString()));
                        Thread.Sleep((int)numWait.Value * 60000);
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
                        if (list_dr.Count > 0)
                            goto Lb_quayvong;
                        else
                            method_StopAddFriend();
                    }
                    else
                        method_StopAddFriend();
                }
            }

        }
        private void method_StopAddFriend()
        {
            pibStatus.Visible = false;
            picstatus.Visible = false;
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
                    goto Lb_Finish;
                }
            }
            try
            {
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
                                sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
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
                                sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                                goto Lb_Finish;

                            }
                        }
                    }
                }

                changeIpHelper.connectAfterOpen(u, richLogs, ldID, token);
                #endregion

                Random rd = new Random();
                if (chkLimitAcc.Checked)
                {
                    if ((int)numLimitAcc.Value < list_acc.Count)
                    {
                        int deleteAcc = list_acc.Count - (int)numLimitAcc.Value;
                        for (int n = 0; n < deleteAcc; n++)
                        {
                            list_acc.RemoveAt(rd.Next(0, list_acc.Count));
                        }
                    }
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
                            changeColor(dr, Color.Yellow);
                            Account acc = (Account)dr.Tag;
                            if (ld.checkAppCurrent(acc) == false)
                                ld.restoreAccount(acc.ldid, acc);
                            ld.killApp(acc.ldid, "com.facebook.katana");
                            ld.runApp(acc.ldid, "com.facebook.katana");
                            ld.checkOpenFacebookFinish(u, acc.ldid);
                            dr.Cells["Message"].Value = "Login Facebook";
                            u.setStatus(ldID, "Login Facebook");
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
                                    goto Lb_Finish;
                                dr.Cells["Message"].Value = "Login Facebook successfull";
                                acc.TrangThai = "Live";
                                dr.Cells["clStatus"].Value = acc.TrangThai;
                                u.setStatus(ldID, "Login Facebook successfull");
                                #region bat dau tuong tac
                                string message = "";
                                int delay = (int)numDelay.Value;
                                acc.app = "com.facebook.katana";

                                DeviceData device = new DeviceData();
                                device.Serial = ldID;

                                if (tabControl1.SelectedTab == tabPage1)
                                {
                                    if (SettingTool.configld.language == "English")
                                    {
                                        dr.Cells["Message"].Value = "Running reaction UId";
                                        u.setStatus(ldID, "Running reaction UId");
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Tương tác UId";
                                        u.setStatus(ldID, "Tương tác UId");
                                    }

                                    int numIds = 0;

                                    int typeID = 0;

                                    if (rdoProfile.Checked)
                                        typeID = 1;
                                    else
                                        typeID = 3;

                                    List<string> ls_id = new List<string>();

                                    if (chkSplit.Checked)
                                    {
                                        if (txtID.Lines.Count() >= dgvUser.RowCount)
                                        {
                                            object synDevice = new object();

                                            numIds = txtID.Lines.Count() / dgvUser.RowCount;
                                            for (int i = 0; i < numIds; i++)
                                            {
                                                if (list_uid.Count == 0)
                                                    break;
                                                lock (synDevice)
                                                {
                                                    int index = rd.Next(0, list_uid.Count);
                                                    ls_id.Add(list_uid[index]);
                                                    list_uid.RemoveAt(index);
                                                }

                                            }
                                        }
                                    }
                                    else
                                        ls_id = txtID.Lines.ToList();



                                    if (chkcommentImage.Checked)
                                    {
                                        List<string> list_file_image = new List<string>();
                                        list_file_image = System.IO.Directory.GetFiles(txtPathIamgecomment.Text, "*.*").ToList();
                                        message += ld.commentImageID(u, acc, dr, (int)numcommentImage.Value, chkComment.Checked, ls_id, list_file_image, txtComment.Text, typeID, delay, true, chkdeleteIamgecomment.Checked, (int)nummaxFail.Value, token);
                                    }

                                    if (chkLike.Checked || chkComment.Checked)
                                    {
                                        if (chkcommentImage.Checked)
                                            chkComment.Checked = false; //neu da chon comment Image thi ko thuc hien comment text nua.

                                        message += ld.likecommentID(u,acc, dr, ldID, acc.app, (int)numLike.Value, (int)numComment.Value, chkLike.Checked, chkComment.Checked, ls_id, txtComment.Text, typeID, delay, false, token, (int)nummaxFail.Value);
                                    }

                                    if (chkfollow.Checked)
                                    {
                                        if (rdoProfile.Checked)
                                        {
                                            dr.Cells["Message"].Value = "Follow UId";
                                            u.setStatus(ldID, "Follow UId");
                                            message += followFriendbyUID(ldID, dr, ls_id, acc, (int)numfollow.Value, delay, token);
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Follow UId";
                                            u.setStatus(ldID, "Follow UId");
                                            message += ld.searchFollowPage(ldID, acc, (int)numfollow.Value, ls_id, token);
                                        }
                                    }
                                }
                                else
                                {
                                    List<string> ls_pid = new List<string>();
                                    if (chkdevice.Checked)
                                    {
                                        if (txtIDpage.Lines.Count() >= dgvUser.RowCount)
                                        {
                                            object synDevice = new object();
                                            int numIds = txtIDpage.Lines.Count() / dgvUser.RowCount;
                                            if (numIds > 0)
                                            {
                                                for (int i = 0; i < numIds; i++)
                                                {
                                                    if (list_Pageid.Count == 0)
                                                        break;
                                                    lock (synDevice)
                                                    {
                                                        int index = rd.Next(0, list_Pageid.Count);
                                                        ls_pid.Add(list_Pageid[index]);
                                                        list_Pageid.RemoveAt(index);
                                                    }
                                                }
                                            }
                                            else
                                                ls_pid = txtIDpage.Lines.ToList();
                                        }
                                        else
                                            ls_pid = txtIDpage.Lines.ToList();
                                    }
                                    else
                                        ls_pid = txtIDpage.Lines.ToList();

                                    List<int> list_tuongtac_done = new List<int>();
                                    if (chkrandomAction.Checked)
                                    {
                                        int step = 0;
                                        for (int i = 0; i < (int)numAcction.Value; i++)
                                        {
                                        lb_step:
                                            step = list_tuongtac[rdom.Next(0, list_tuongtac.Count)];
                                            if (list_tuongtac_done.Contains(step))
                                            {
                                                if (list_tuongtac_done.Count < list_tuongtac.Count)
                                                    goto lb_step;
                                            }

                                            else
                                                list_tuongtac_done.Add(step);
                                        }
                                    }
                                    else
                                    {
                                        list_tuongtac_done = list_tuongtac;
                                    }

                                    foreach (string pid in ls_pid)
                                    {
                                        message += "|" + pid + " - " + runPageId(u, acc, ldID, dr, pid, list_tuongtac_done, token);
                                    }

                                }
                                #endregion
                                dr.Cells["Message"].Value = message;
                                u.setStatus(ldID, message);
                                changeColor(dr, Color.White);
                            }
                            else
                            {
                                if (acc.Thongbao != null)
                                    dr.Cells["Message"].Value = acc.Thongbao;
                                else
                                    dr.Cells["Message"].Value = "Login fail";
                                dr.Cells["clStatus"].Value = "Die";
                                acc.TrangThai = "Die";
                                changeColor(dr, Color.White);
                            }
                            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                            nguoidung.updateNoti(acc);
                            dr.Cells[0].Value = false;
                            changeColor(dr, Color.White);
                        }
                    }
                    catch
                    {

                    }
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
        private string runPageId(userLD u, Account acc, string ldID, DataGridViewRow dr, string idlink, List<int> list_tuongtac_done, CancellationToken token)
        {
            Random rd = new Random();
            int numlike = 0;
            int numcomment = 0;
            int numshare = 0;
            int numtag = 0;
            int numlienquan = 0;
            int numquangcao = 0;
            bool likeFanpage = false;
            int i_delay = rd.Next(SettingTool.configCarepage.numDelayfrom, SettingTool.configCarepage.numDelayto);
            string message = "";
            foreach (int type in list_tuongtac_done)
            {
                switch (type)
                {
                    case 1:
                        if (txtIDpage.Text.Contains("https"))
                            numlike = 1;
                        else
                            numlike = rd.Next(SettingTool.configCarepage.numLikefrom, SettingTool.configCarepage.numLiketo);
                        break;
                    case 2:
                        if (txtIDpage.Text.Contains("https"))
                            numcomment = 1;
                        else
                            numcomment = rd.Next(SettingTool.configCarepage.numCommentfrom, SettingTool.configCarepage.numCommentto);
                        break;
                    case 3:
                        if (txtIDpage.Text.Contains("https"))
                            numtag = 1;
                        else
                            numtag = rd.Next(SettingTool.configCarepage.numTagfrom, SettingTool.configCarepage.numTagto);
                        break;
                    case 4:
                        if (txtIDpage.Text.Contains("https"))
                            numshare = 1;
                        else
                            numshare = rd.Next(SettingTool.configCarepage.numSharefrom, SettingTool.configCarepage.numShareto);
                        break;
                    case 5:
                        if (txtIDpage.Text.Contains("https"))
                            numlienquan = 1;
                        else
                            numlienquan = rd.Next(SettingTool.configCarepage.numLienquanfrom, SettingTool.configCarepage.numLienquanto);
                        break;
                    case 6:
                        if (txtIDpage.Text.Contains("https"))
                            numquangcao = 1;
                        else
                            numquangcao = rd.Next(SettingTool.configCarepage.numQuangcaofrom, SettingTool.configCarepage.numQuangcaoto);
                        break;
                    case 7:
                        if (txtIDpage.Text.Contains("https"))
                            likeFanpage = false;
                        else
                            likeFanpage = true;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(idlink))
            {
                idlink = FunctionHelper.method_Spin(idlink);
                if (idlink.Contains("https"))
                {
                    if (chkaddfriend.Checked)
                    {
                        dr.Cells["Message"].Value = "Add friend";
                        u.setStatus(ldID, "Add friend");
                        message += ld.viewAddFriendbyLinkpost(idlink, ldID, acc.app, (int)numaddfriend.Value, i_delay, token);
                    }

                    if (chklikepost.Checked || chklikepost.Checked || chkComment.Checked || chkShare.Checked || chkTag.Checked || chkLienquan.Checked || chkQuangcao.Checked || chkLikefanpage.Checked)
                        message += ld.reactionLinkPage(u, acc, dr, ldID, acc.app, likeFanpage, numlike, numcomment, numtag, numshare, numlienquan, numquangcao, idlink, txt_comment.Text, i_delay, (int)numdlfromscroll.Value, (int)numdltoscroll.Value, token);
                }
                else
                    message += ld.reactionPremiumPage(u, acc, dr, ldID, acc.app, likeFanpage, numlike, numcomment, numtag, numshare, numlienquan, numquangcao, idlink, txt_comment.Text, i_delay, (int)numdlfromscroll.Value, (int)numdltoscroll.Value, token);
            }
            else
            {
                dr.Cells["Message"].Value = "Hết UId";
                u.setStatus(ldID, "Hết UId");
            }
            return message;
        }
        xProxyController xcontroller = new xProxyController();
        void Delay(double delay)
        {
            double delayTime = 0;
            while (delayTime < delay)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delayTime++;
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
        private void ClearMessage()
        {
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                dgvUser.Rows[i].Cells["Message"].Value = "";
                changeColor(dgvUser.Rows[i], Color.White);
            }
        }
        private void btn_config_Click(object sender, EventArgs e)
        {

            frm_Config frm = new frm_Config();
            frm.ShowDialog();
            method_Config();
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
        private void changeColor(DataGridViewRow dataGridViewRow_0, Color color_0)
        {
            Class34 class2 = new Class34
            {
                dataGridViewRow_0 = dataGridViewRow_0,
                color_0 = color_0
            };
            this.Invoke(new MethodInvoker(class2.method_0));
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            method_StopAddFriend();
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

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (txtIDpage.Text == "")
            {
                MessageBox.Show("Hãy nhập Id của page");
                return;
            }
            if (chklikepost.Checked)
                list_tuongtac.Add(1); //like post
            if (chkcommentpost.Checked)
                list_tuongtac.Add(2); //comment
            if (chkTag.Checked)
                list_tuongtac.Add(3); //tag
            if (chkShare.Checked)
                list_tuongtac.Add(4); //share
            if (chkLienquan.Checked)
                list_tuongtac.Add(5); //link lien quan
            if (chkQuangcao.Checked)
                list_tuongtac.Add(6); //click quang cao
            if (chkLikefanpage.Checked)
                list_tuongtac.Add(7);
            list_Pageid = txtIDpage.Lines.ToList();
            tokenSource = new CancellationTokenSource();
            ClearMessage();
            list_ldrun = new List<LDRun>();
            list_ld = new List<string>();
            string pathlog = Application.StartupPath + "\\logs";
            if (!Directory.Exists(pathlog))
            {
                Directory.CreateDirectory(pathlog);
            }
            startTuongTacCarePage();
        }
        private void saveinfo()
        {
            //save config 
            try
            {
                SettingTool.configCarepage = new configFormCareFanpage();
                SettingTool.configCarepage.linkID = txtIDpage.Text;
                SettingTool.configCarepage.contentComment = txt_comment.Text;
                SettingTool.configCarepage.numLikefrom = (int)numfromLike.Value;
                SettingTool.configCarepage.numLiketo = (int)numtoLike.Value;

                SettingTool.configCarepage.numCommentfrom = (int)numfromComment.Value;
                SettingTool.configCarepage.numCommentto = (int)numtoComment.Value;

                SettingTool.configCarepage.numTagfrom = (int)numfromTag.Value;
                SettingTool.configCarepage.numTagto = (int)numtoTag.Value;

                SettingTool.configCarepage.numSharefrom = (int)numfromshare.Value;
                SettingTool.configCarepage.numShareto = (int)numtoShare.Value;

                SettingTool.configCarepage.numLienquanfrom = (int)numfromLienquan.Value;
                SettingTool.configCarepage.numLienquanto = (int)numtoLienquan.Value;

                SettingTool.configCarepage.numQuangcaofrom = (int)numfromQuangcao.Value;
                SettingTool.configCarepage.numQuangcaoto = (int)numtoQuangcao.Value;

                SettingTool.configCarepage.numDelayfrom = (int)numfromDelay.Value;
                SettingTool.configCarepage.numDelayto = (int)numtoDelay.Value;

                SettingTool.configCarepage.numdelayscrollfrom = (int)numdlfromscroll.Value;
                SettingTool.configCarepage.numdelayscrollto = (int)numdltoscroll.Value;

                SettingTool.configCarepage.chkLikeFanpage = chkLikefanpage.Checked;
                SettingTool.configCarepage.chkRandomaction = chkrandomAction.Checked;
                SettingTool.configCarepage.numrandomaction = (int)numAcction.Value;

                SettingTool.configCarepage.chkLike = chklikepost.Checked;
                SettingTool.configCarepage.chkComment = chkcommentpost.Checked;
                SettingTool.configCarepage.chkTag = chkTag.Checked;
                SettingTool.configCarepage.chkShare = chkShare.Checked;

                SettingTool.configCarepage.chkLienquan = chkLienquan.Checked;
                SettingTool.configCarepage.chkQuangcao = chkQuangcao.Checked;

                SettingTool.configCarepage.chkRandomaction = chkrandomAction.Checked;

                string path = String.Format("{0}\\Config\\ConfigCarePage.data", Application.StartupPath);
                File.WriteAllText(path, JsonConvert.SerializeObject(SettingTool.configCarepage));
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + " :" + ex.Message + "\n");
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            method_StopAddFriend();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            saveinfo();
            frm_Schedule frm = new frm_Schedule(this.list_acc, -1, "", 0);
            frm.Show();
        }

        private void btnPathIamge_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathIamgecomment.Text = fldrDlg.SelectedPath;
            }
        }
        private string followFriendbyUID(string deviceid, DataGridViewRow dr, List<string> list_uid, Account acc, int numFriend, int delay, CancellationToken token)
        {
            string path = "";
            string uid;
            int count = 0;
            if (string.IsNullOrEmpty(acc.id))
                path = String.Format("{0}\\logs\\{1}_follow.txt", Application.StartupPath, acc.email);
            else
                path = String.Format("{0}\\logs\\{1}_follow.txt", Application.StartupPath, acc.id);
            string historyadd = "";
            try
            {
                historyadd = File.ReadAllText(path);
            }
            catch { }

            if (list_uid.Count <= 0)
            {
                sendLogs("Vui lòng thêm UID để follow friend: ");
                return "|follow friend by UID: thêm UID để follow friend";
            }
            StringBuilder list_history = new StringBuilder();
            int int_loilientiep = 0;
            while (count < numFriend)
            {
                if (token.IsCancellationRequested)
                    break;
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

                int status = ld.followFriendUID(acc, uid, token);

                if (status == 1)
                {
                    int_loilientiep = 0;
                    list_history.AppendLine(uid);
                    count++;
                    sendLogs(String.Format("Tài Khoan {0} following với {1} thành công", acc.email, uid));

                }
                else
                {
                    int_loilientiep++;
                    if (int_loilientiep >= 30)
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
            try
            {
                File.AppendAllText(path, list_history.ToString());

                if (SettingTool.configadd.DeleteUID)
                {
                    if (!string.IsNullOrEmpty(acc.pathUID))
                        File.WriteAllLines(acc.pathUID, list_uid);
                }
            }
            catch
            {
            }
            return "Follow friend by UID hoàn thành " + count.ToString() + "/" + numFriend.ToString();
        }
    }
}
