using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using System.Collections.Concurrent;
using System.Web;
using RestSharp;
namespace NinjaSystem
{
    public partial class frm_reactionMyfriendgroup : Form
    {
        public frm_reactionMyfriendgroup(List<Account> list_acc, frm_MainLD frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        List<Account> list_acc;
        bool has_stop = false;
        object synAcc = new object();
        List<PositionLD> lsPosition = new List<PositionLD>();
        SettingTuongTac tuongtac = new SettingTuongTac();

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
        Random rdom = new Random();
        object synUID = new object();
        int countComplete = 0;
        frm_MainLD frm_main;
        List<string> list_group;
        private void frm_reactionMyfriendgroup_Load(object sender, EventArgs e)
        {
            method_LoadAccount();


            if (SettingTool.configld.language == "English")
            {
                label3.Text = "Seclect config setup";
                // cboConfig.Text = "Seclect config";

                this.Text = "Run reaction";
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
            if (chkCommentfriend.Checked)
            {
                if (!File.Exists(txtpathcommentfriend.Text))
                {
                    MessageBox.Show("Chưa chọn file nội dung comment.");
                    return;
                }
            }
            if (chkCommentgroup.Checked)
            {
                if (!File.Exists(txtPathcommentGroup.Text))
                {
                    MessageBox.Show("Chưa chọn file nội dung comment.");
                    return;
                }
            }

            has_stop = false;
            // ld.changeIp();
            list_ldrun = new List<LDRun>();
            list_ld = new List<string>();
            ClearMessage();
            string pathlog = Application.StartupPath + "\\logs";
            if (!Directory.Exists(pathlog))
            {
                Directory.CreateDirectory(pathlog);
            }
            startTuongTac();
        }

        private void startTuongTac()
        {

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
                MessageBox.Show("Hãy chọn những tài khoản cần chạy");
                pibStatus.Visible = false;
                return;
            }
            else
            {
                pibStatus.Visible = true;
                list_group = new List<string>();
                if (File.Exists(tuongtac.strPath))
                {
                    list_group = File.ReadAllLines(tuongtac.strPath).ToList().ToList();
                }

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
                // runTuongTac();
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
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void runTuongTacWaitAny()
        {
            try
            {
                int lap = 0;

                int numthread = SettingTool.configld.numthread;
                if (numthread > list_ld.Count)
                {
                    numthread = list_ld.Count;
                }
                if (SettingTool.configld.timeout == 0)
                {
                    SettingTool.configld.timeout = 20;
                }
                //khoi tao list task
                Task[] list_task = TaskController.createTask(numthread);
                xcontroller.createProxy(numthread);

                int maxproxy = 0;
            Lb_quayvong:
                tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
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
                        if (token.IsCancellationRequested == false)
                        {
                            int index = TaskController.getAvailableTask(list_task);
                            String ldid = "";
                            List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
                            if (index >= 0)
                            {
                                if (changeIpHelper.checkGetProxyWaitAny())
                                {
                                    method_log("Đang lấy IP ");
                                    string proxy = xcontroller.getProxy();
                                    if (!string.IsNullOrEmpty(proxy))
                                    {
                                        method_log("Đã lấy IP: " + proxy);
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
                                            // string ldid = "";
                                            string proxy = "";
                                            //  List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
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
                            }

                            Thread.Sleep(SettingTool.configld.timedelay * 1000);
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
                    if (list_ld.Count > 0 && has_stop == false)
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

                        if (tuongtac.chkLoop_tuongtac)
                        {
                            lap++;
                            if (lap <= tuongtac.nummaxtuongtac)
                            {
                                if (SettingTool.configld.language == "English")
                                    sendLogs(String.Format("Please wait {0} minutes to continue interacting", tuongtac.numLoop_tuongtac));
                                else
                                    sendLogs(String.Format("Vui lòng đợi {0} phút để tiếp tục tương tác", tuongtac.numLoop_tuongtac));

                                Thread.Sleep(tuongtac.numLoop_tuongtac * 60000);
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
                                {
                                    foreach (DataGridViewRow row2 in this.dgvUser.Rows)
                                    {
                                        row2.Cells[0].Value = true;
                                        Account acc = (Account)row2.Tag;
                                        list_dr.Add(row2);
                                        list_ld.Add(acc.ldid);
                                        row2.Cells["Message"].Value = "";
                                    }
                                    list_ld = list_ld.Distinct().ToList();
                                    goto Lb_quayvong;
                                }
                            }
                            else
                                method_StopAddFriend();
                        }
                        else
                            method_StopAddFriend();
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
            try
            {
                int lap = 0;
            Lb_quayvong:
                tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                int numthread = SettingTool.configld.numthread;
                if (numthread > list_ld.Count)
                {
                    numthread = list_ld.Count;

                }
                SettingTool.configld.numthreadxproxy = numthread;
                Task[] tasks = new Task[numthread];
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
                    }
                    #endregion
                    int k = 0;
                    object synDevice = new object();
                    for (int i = 0; i < numthread; i++)
                    {
                        int t = i;
                        tasks[t] = Task.Factory.StartNew(() =>
                        {
                            if (has_stop == false)
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
                                        proxy = list_proxy[k];
                                        k++;
                                        if (k >= list_proxy.Count)
                                        {
                                            k = 0;
                                        }

                                    }
                                }
                                method_Start(ldid, list_acc, proxy, token);
                            }

                        }, token);

                        Thread.Sleep(SettingTool.configld.timedelay * 1000);
                    }
                    sendLogs(String.Format("Total Thread 1 : {0} ", tasks.Count()));
                    tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                    try
                    {
                        Task.WaitAll(tasks);
                    }
                    catch (OperationCanceledException)
                    {

                    }
                    if (list_ld.Count > 0 && has_stop == false)
                    {
                        goto Lb_quayvong;
                    }
                    else
                    {
                        if (tuongtac.chkLoop_tuongtac)
                        {
                            lap++;
                            if (lap <= tuongtac.nummaxtuongtac)
                            {
                                if (SettingTool.configld.language == "English")
                                    sendLogs(String.Format("Please wait {0} minutes to continue interacting", tuongtac.numLoop_tuongtac));
                                else
                                    sendLogs(String.Format("Vui lòng đợi {0} phút để tiếp tục tương tác", tuongtac.numLoop_tuongtac));
                                Thread.Sleep(tuongtac.numLoop_tuongtac * 60000);
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
                                {
                                    foreach (DataGridViewRow row2 in this.dgvUser.Rows)
                                    {
                                        row2.Cells[0].Value = true;
                                        Account acc = (Account)row2.Tag;
                                        list_dr.Add(row2);
                                        list_ld.Add(acc.ldid);
                                        row2.Cells["Message"].Value = "";
                                    }
                                    list_ld = list_ld.Distinct().ToList();
                                    goto Lb_quayvong;
                                }
                            }
                        }
                        else
                            method_StopAddFriend();
                    }


                }
            }
            catch (Exception ex)
            {
                method_log(ex.ToString());
            }
        }
        private void method_StopAddFriend()
        {
            pibStatus.Visible = false;

            has_stop = true;
            if (thread_1 != null)
                thread_1.Abort();

        }
        private void method_Start(string ldID, List<DataGridViewRow> list_acc, string proxy, CancellationToken token)
        {
            try
            {
                method_log("Open LDPlayer Id: " + ldID);
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
                        u.setStatus(ldID, "Connect successful LD...");
                    }
                    else
                    {
                        u.setStatus(ldID, "Disconnected...");
                        method_log("Disconnected LD: " + ldID);
                        goto Lb_Finish;
                    }
                }
                // 
                if (SettingTool.configld.sock5)
                {
                    if (ld.checkApp(ldID, "org.proxydroid") == false)
                    {
                        string path = Application.StartupPath + "\\app\\proxydroid.apk";
                        if (File.Exists(path))
                        {
                            u.setStatus(ldID, "Install app droid proxy...");
                            ld.installApp(ldID, path);
                            Thread.Sleep(3000);
                        }
                    }
                }
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
                    if (SettingTool.configld.appversion == "Facebook 299")
                    {
                        string path = Application.StartupPath + "\\app\\Facebook299.apk";
                        if (File.Exists(path))
                        {
                            u.setStatus(ldID, "Install App Facebook...");
                            ld.installApp(ldID, path);
                            Thread.Sleep(15000);
                            while (ld.checkApp(ldID, "com.facebook.katana") == false)
                            {
                                Thread.Sleep(1000);
                            }
                            Thread.Sleep(3000);
                        }


                    }
                    else
                    {
                        if (SettingTool.configld.appversion == "Facebook 302")
                        {
                            string path = Application.StartupPath + "\\app\\Facebook302.apk";
                            if (File.Exists(path))
                            {
                                u.setStatus(ldID, "Install App Facebook...");
                                ld.installApp(ldID, path);
                                Thread.Sleep(15000);
                                while (ld.checkApp(ldID, "com.facebook.katana") == false)
                                {
                                    Thread.Sleep(1000);
                                }
                                Thread.Sleep(3000);
                            }
                        }
                        else
                        {
                            if (SettingTool.configld.appversion == "Facebook Lite")
                            {
                                if (ld.checkApp(ldID, "com.facebook.lite") == false)
                                {
                                    string path = Application.StartupPath + "\\app\\FacebookLite.apk";
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
                            }
                            else
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
                        }
                    }
                }
                //set status
                ld.setKeyboard(ldID);

                if (tuongtac.runrandomtuongtac)
                {
                    if (tuongtac.numrandomtuongtac < list_acc.Count)
                    {
                        Random rd = new Random();
                        int deleteAcc = list_acc.Count - tuongtac.numrandomtuongtac;

                        for (int n = 0; n < deleteAcc; n++)
                        {
                            list_acc.RemoveAt(rd.Next(0, list_acc.Count));
                        }
                    }
                }
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
                int dem = 0;
                string message = "";
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
                    if (token.IsCancellationRequested)
                    {
                        goto Lb_Finish;
                    }
                    try
                    {
                        dem++;
                        DataGridViewRow dr = null;
                        lock (synAcc)
                        {
                            dr = list_acc[0];
                            list_acc.Remove(dr);
                        }
                        if (dr != null)
                        {
                            if (SettingTool.configld.typeip == 4 && dem > 1)
                            {
                                if (dem % SettingTool.configld.accountIP == 0)
                                {
                                    changeIpHelper.connectAfterOpen(u, richLogs, ldID, token);
                                }
                            }

                            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                            changeColor(dr, Color.Yellow);
                            dr.Cells["Message"].Value = "Running";
                            Account acc = (Account)dr.Tag;
                            try
                            {

                                if (ld.checkAppCurrent(acc) == false)
                                    ld.restoreAccount(acc.ldid, acc);

                                ld.killApp(acc.ldid, SettingTool.configld.package);
                                ld.runApp(acc.ldid, SettingTool.configld.package);
                                ld.checkOpenFacebookFinish(u, acc.ldid);
                            }
                            catch
                            {

                            }

                            dr.Cells["Message"].Value = "Login Facebook";
                            u.setStatus(ldID, "Login Facebook...");

                            if (SettingTool.configld.appversion == "Facebook Lite")
                            {
                                Thread.Sleep(2000);
                                LiteController lite = new LiteController();
                                bool haslogin = lite.loginFacebook(u, acc, token);
                                if (haslogin)
                                {
                                    dr.Cells["Message"].Value = "Login successful";
                                    dr.Cells["clstatus"].Value = "Live";
                                    acc.TrangThai = "Live";
                                    acc.Thongbao = "Login successful";
                                    u.setStatus(ldID, "Login successful...");
                                    // tuongtacLite(u, acc, dr, token);
                                }
                                else
                                {
                                    dr.Cells["Message"].Value = "Login fail";
                                    u.setStatus(ldID, " Login fail...");
                                }
                            }
                            else
                            {

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
                                    u.setStatus(ldID, "Login Facebook Sucess...");
                                    dr.Cells["Message"].Value = "Login Facebook Successful";
                                    acc.Thongbao = "Login Successful";
                                    acc.TrangThai = "Live";
                                    dr.Cells["clStatus"].Value = acc.TrangThai;
                                    //bat dau tuong tac
                                    message = "";
                                    Random rd = new Random();
                                    int min_delay = (int)numdelayMin.Value;
                                    int max_delay = (int)numdelayMax.Value;
                                    if (chkLikefriend.Checked || chkCommentfriend.Checked)
                                    {
                                        dr.Cells["Message"].Value = "Đang tương tác vào bạn bè";

                                        List<string> lsFriend = new List<string>();

                                        string filelsFriend = Application.StartupPath + "\\logs\\" + "listFriendof_" + acc.id + ".txt";
                                        string pathcmtfriend = txtpathcommentfriend.Text;
                                        string commnetfriend = "";
                                        if (File.Exists(pathcmtfriend))
                                        {
                                            commnetfriend = File.ReadAllText(pathcmtfriend);
                                        }


                                        if (!File.Exists(filelsFriend))
                                        {
                                            lsFriend = getListfriend(acc.ldid, acc);
                                            File.WriteAllLines(filelsFriend, lsFriend.ToArray());
                                        }
                                        else
                                        {
                                            lsFriend = File.ReadAllLines(filelsFriend).ToList();
                                            if (lsFriend.Count == 0)
                                            {
                                                lsFriend = getListfriend(acc.ldid, acc);
                                                File.WriteAllLines(filelsFriend, lsFriend.ToArray());
                                            }
                                        }
                                        if (lsFriend.Count > 0)
                                        {
                                            #region like friend
                                            if (chkLikefriend.Checked)
                                            {
                                                dr.Cells["Message"].Value = "Like bài viết bạn bè";
                                                u.setStatus(ldID, "Like bài viết bạn bè...");
                                                int numlike = rd.Next((int)numminLikefriend.Value, (int)nummaxLikefriend.Value);
                                                u.setStatusSum(numlike);
                                                int int_thanhcong = 0;
                                                for (int i = 0; i < numlike; i++)
                                                {
                                                    if (lsFriend.Count > 0)
                                                    {
                                                        string uid = lsFriend[rd.Next(0, lsFriend.Count)];
                                                        lsFriend.Remove(uid);
                                                        ld.OpenLink(ldID, "com.facebook.katana", "fb://profile/" + uid);
                                                        u.setStatus(ldID, "Like bài viết bạn bè: " + uid);
                                                        Delay(2);
                                                        ld.scroll_up(ldID);
                                                        ld.scroll_up(ldID);

                                                        if (chkLuot.Checked)
                                                        {
                                                            for (int n = 0; n < (int)numLuot.Value; n++)
                                                            {
                                                                if (token.IsCancellationRequested)
                                                                {
                                                                    token.ThrowIfCancellationRequested();
                                                                }
                                                                ld.scroll_up(ldID);
                                                                Delay(1);
                                                            }
                                                        }

                                                        for (int int_like = 0; int_like < 5; int_like++)
                                                        {
                                                            bool has_like = ld.likePost(acc, ldID, token);
                                                            if (has_like)
                                                            {
                                                                int_thanhcong++;
                                                                u.setStatusResult(int_thanhcong);
                                                                int delay = rd.Next(min_delay, max_delay);
                                                                Delay(delay);

                                                                break;
                                                            }
                                                            else
                                                            {
                                                                if (ld.checkContent(ldID, "Trang này chưa thể hiển thị ngay"))
                                                                {
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    ld.scroll_up(ldID);

                                                                }

                                                            }
                                                        }

                                                    }
                                                }
                                                File.WriteAllLines(filelsFriend, lsFriend.ToArray());
                                                message += "Like friend: " + int_thanhcong.ToString() + "/" + numlike.ToString();
                                            }
                                            #endregion
                                            #region comment friend
                                            if (chkCommentfriend.Checked)
                                            {
                                                if (string.IsNullOrEmpty(commnetfriend) == false)
                                                {
                                                    dr.Cells["Message"].Value = "Comment bài viết bạn bè";
                                                    u.setStatus(ldID, "Comment bài viết bạn bè...");
                                                    int numcmtfriend = rd.Next((int)numminCommentfriend.Value, (int)numMaxCommentfriend.Value);
                                                    u.setStatusSum(numcmtfriend);
                                                    int int_thanhcong = 0;
                                                    for (int i = 0; i < numcmtfriend; i++)
                                                    {
                                                        if (lsFriend.Count > 0)
                                                        {
                                                            string uid = lsFriend[rd.Next(0, lsFriend.Count)];
                                                            lsFriend.Remove(uid);
                                                            ld.OpenLink(ldID, "com.facebook.katana", "fb://profile/" + uid);
                                                            u.setStatus(ldID, "Comment bài viết bạn bè: " + uid);
                                                            Delay(2);
                                                            ld.scroll_up(ldID);
                                                            ld.scroll_up(ldID);
                                                            if (chkLuot.Checked)
                                                            {
                                                                for (int n = 0; n < (int)numLuot.Value; n++)
                                                                {
                                                                    if (token.IsCancellationRequested)
                                                                    {
                                                                        token.ThrowIfCancellationRequested();
                                                                    }
                                                                    ld.scroll_up(ldID);
                                                                    Delay(1);
                                                                }
                                                            }
                                                            for (int int_comment = 0; int_comment < 5; int_comment++)
                                                            {
                                                                bool has_like = ld.commentPost(u, acc, ldID, commnetfriend, token);
                                                                if (has_like)
                                                                {
                                                                    int_thanhcong++;
                                                                    u.setStatusResult(int_thanhcong);
                                                                    int delay = rd.Next(min_delay, max_delay);
                                                                    Delay(delay);

                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    if (ld.checkContent(ldID, "Trang này chưa thể hiển thị ngay"))
                                                                    {
                                                                        break;
                                                                    }
                                                                    else
                                                                    {
                                                                        ld.scroll_up(ldID);

                                                                    }
                                                                }
                                                            }

                                                        }
                                                    }
                                                    File.WriteAllLines(filelsFriend, lsFriend.ToArray());
                                                    message += " Comment friend: " + int_thanhcong.ToString() + "/" + numcmtfriend.ToString();
                                                }
                                                else
                                                {
                                                    u.setStatus(ldID, "Chưa thiết lập nội dung comment");
                                                }
                                            }

                                            //List<string> lsreactionFriend = new List<string>();
                                            //for (int i = 0; i < (int)numFriends.Value; i++)
                                            //{
                                            //    if (lsFriend.Count > 0)
                                            //    {
                                            //        lsreactionFriend.Add(lsFriend[0]);
                                            //        lsFriend.RemoveAt(0);
                                            //    }
                                            //    else
                                            //        break;

                                            //}
                                            //message = ld.likecommentID(acc, dr, acc.ldid, "com.facebook.katana", rd.Next((int)numminLikefriend.Value, (int)nummaxLikefriend.Value), rd.Next((int)numminCommentfriend.Value, (int)numMaxCommentfriend.Value), chkLikefriend.Checked, chkCommentfriend.Checked, lsreactionFriend, commnetfriend, 1, rd.Next((int)numdelayMin.Value, (int)numdelayMax.Value), false, token);
                                            //File.WriteAllLines(filelsFriend, lsFriend.ToArray());
                                            #endregion
                                        }

                                    }
                                    if (chkLikegroup.Checked || chkCommentgroup.Checked)
                                    {

                                        dr.Cells["Message"].Value = "Đang tương tác vào nhóm";
                                        u.setStatus(ldID, "Đang tương tác vào nhóm...");
                                        List<string> lsGroups = new List<string>();
                                        string filelsGroup = Application.StartupPath + "\\logs\\" + "listGroupof_" + acc.id + ".txt";


                                        LDController controler = new LDController();

                                        string access_token = controler.getToken(acc);
                                        Profile_Controller profile = new Profile_Controller();
                                        if (!File.Exists(filelsGroup))
                                        {
                                            List<GroupFB> ls_groupFB = profile.LoadInfoGroup(access_token, "", acc, null);
                                            foreach (GroupFB grfb in ls_groupFB)
                                            {
                                                lsGroups.Add(grfb.id);
                                            }
                                            File.WriteAllLines(filelsGroup, lsGroups.ToArray());
                                        }
                                        else
                                        {
                                            lsGroups = File.ReadAllLines(filelsGroup).ToList();
                                            if (lsGroups.Count == 0)
                                            {
                                                List<GroupFB> ls_groupFB = profile.LoadInfoGroup(access_token, "", acc, null);
                                                foreach (GroupFB grfb in ls_groupFB)
                                                {
                                                    lsGroups.Add(grfb.id);
                                                }
                                                File.WriteAllLines(filelsGroup, lsGroups.ToArray());
                                            }
                                        }
                                        if (lsGroups.Count > 0)
                                        {
                                            if (chkLikegroup.Checked)
                                            {
                                                #region likegroup
                                                dr.Cells["Message"].Value = "Like bài viết Group";
                                                u.setStatus(ldID, "Like bài viết Group...");

                                                int numlike = rd.Next((int)numminLikegroup.Value, (int)nummaxLikegroup.Value);
                                                int maxlike = (int)numGroups.Value;
                                                u.setStatusSum(numlike);

                                                int int_thanhcong = 0;
                                                for (int i = 0; i < numlike; i++)
                                                {
                                                    if (lsGroups.Count > 0)
                                                    {
                                                        string uid = lsGroups[rd.Next(0, lsGroups.Count)];
                                                        lsGroups.Remove(uid);
                                                        ld.OpenLink(ldID, "com.facebook.katana", "fb://group/" + uid + "/?ref=group_browse");
                                                        u.setStatus(ldID, "Like bài viết group: " + uid);
                                                        Delay(2);
                                                        ld.scroll_up(ldID);
                                                        ld.scroll_up(ldID);
                                                        if (chkLuot.Checked)
                                                        {
                                                            for (int n = 0; n < (int)numLuot.Value; n++)
                                                            {
                                                                if (token.IsCancellationRequested)
                                                                {
                                                                    token.ThrowIfCancellationRequested();
                                                                }
                                                                ld.scroll_up(ldID);
                                                                Delay(1);
                                                            }
                                                        }
                                                        int int_tonglikegroup = 0;

                                                        int loop = maxlike + 5;
                                                        for (int int_like = 0; int_like < loop; int_like++)
                                                        {
                                                            bool has_like = ld.likePost(acc, ldID, token);
                                                            if (has_like)
                                                            {
                                                                int_thanhcong++;
                                                                int_tonglikegroup++;
                                                                u.setStatusResult(int_thanhcong);
                                                                int delay = rd.Next(min_delay, max_delay);
                                                                Delay(delay);
                                                                if (int_tonglikegroup >= maxlike)
                                                                {
                                                                    // neu 1 group like nhieu bai thi break
                                                                    break;
                                                                }
                                                                if (int_thanhcong > numlike)
                                                                {
                                                                    // neu tong thanh cong 

                                                                    break;
                                                                }

                                                            }
                                                            else
                                                            {
                                                                if (ld.checkContent(ldID, "Trang này chưa thể hiển thị ngay"))
                                                                {
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    ld.scroll_up(ldID);

                                                                }

                                                            }
                                                        }
                                                        if (int_thanhcong > numlike)
                                                        {
                                                            // neu tong thanh cong 
                                                            break;
                                                        }

                                                    }
                                                }
                                                File.WriteAllLines(filelsGroup, lsGroups.ToArray());
                                                message += " Like group: " + int_thanhcong.ToString() + "/" + (numlike * (int)numGroups.Value).ToString();
                                                #endregion

                                            }
                                            if (chkCommentgroup.Checked)
                                            {
                                                #region comment group
                                                string pathcmtgroup = txtPathcommentGroup.Text;
                                                if (File.Exists(pathcmtgroup))
                                                {
                                                    string commentgroup = File.ReadAllText(pathcmtgroup);
                                                    if (string.IsNullOrEmpty(commentgroup) == false)
                                                    {
                                                        u.setStatus(ldID, "Comment bài viết Group...");
                                                        dr.Cells["Message"].Value = "Comment bài viết Group";
                                                        int numlike = rd.Next((int)numminCommentgroup.Value, (int)nummaxCommentgroup.Value);
                                                        int maxlike = (int)numGroups.Value;
                                                        u.setStatusSum(numlike);

                                                        int int_thanhcong = 0;
                                                        for (int i = 0; i < numlike; i++)
                                                        {
                                                            if (lsGroups.Count > 0)
                                                            {
                                                                string uid = lsGroups[rd.Next(0, lsGroups.Count)];
                                                                lsGroups.Remove(uid);
                                                                ld.OpenLink(ldID, "com.facebook.katana", "fb://group/" + uid + "/?ref=group_browse");
                                                                u.setStatus(ldID, "Comment bài viết group: " + uid);
                                                                Delay(2);
                                                                ld.scroll_up(ldID);
                                                                if (chkLuot.Checked)
                                                                {
                                                                    for (int n = 0; n < (int)numLuot.Value; n++)
                                                                    {
                                                                        if (token.IsCancellationRequested)
                                                                        {
                                                                            token.ThrowIfCancellationRequested();
                                                                        }
                                                                        ld.scroll_up(ldID);
                                                                        Delay(1);
                                                                    }
                                                                }
                                                                int int_tonglikegroup = 0;

                                                                int loop = maxlike + 5;
                                                                for (int int_like = 0; int_like < loop; int_like++)
                                                                {
                                                                    ld.scroll_up(ldID);
                                                                    bool has_like = ld.commentPost(u, acc, ldID, commentgroup, token);
                                                                    if (has_like)
                                                                    {
                                                                        int_thanhcong++;
                                                                        int_tonglikegroup++;
                                                                        u.setStatusResult(int_thanhcong);
                                                                        int delay = rd.Next(min_delay, max_delay);
                                                                        Delay(delay);
                                                                        ld.back(ldID, 2);
                                                                        if (int_tonglikegroup >= maxlike)
                                                                        {
                                                                            // neu 1 group like nhieu bai thi break

                                                                            break;
                                                                        }
                                                                        if (int_thanhcong > numlike)
                                                                        {
                                                                            // neu tong thanh cong 

                                                                            break;
                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        if (ld.checkContent(ldID, "Trang này chưa thể hiển thị ngay"))
                                                                        {
                                                                            break;
                                                                        }
                                                                        else
                                                                        {
                                                                            ld.scroll_up(ldID);

                                                                        }

                                                                    }
                                                                }
                                                                if (int_thanhcong > numlike)
                                                                {
                                                                    // neu tong thanh cong 
                                                                    break;
                                                                }

                                                            }
                                                        }
                                                        File.WriteAllLines(filelsGroup, lsGroups.ToArray());
                                                        message += " Comment group: " + int_thanhcong.ToString() + "/" + (numlike * (int)numGroups.Value).ToString();
                                                    }
                                                    else
                                                    {
                                                        u.setStatus(ldID, "Chưa thiết lập nội dung comment");
                                                    }
                                                }
                                                #endregion
                                            }

                                        }
                                    }

                                    u.setStatus(ldID, "Finish");
                                    dr.Cells["Message"].Value = message;
                                    //  File.WriteAllLines(acc.pathUID, list_uid);
                                    changeColor(dr, Color.White);
                                }
                                else
                                {
                                    if (acc.Thongbao != null)
                                    {
                                        if (acc.Thongbao == "Login Successful")
                                        {
                                            dr.Cells["Message"].Value = "Đăng nhập không thành công";
                                            acc.Thongbao = "Đăng nhập không thành công";
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(acc.Thongbao))
                                            {
                                                dr.Cells["Message"].Value = acc.Thongbao;

                                            }
                                            else
                                            {
                                                dr.Cells["Message"].Value = "Đăng nhập không thành công";
                                                acc.Thongbao = "Đăng nhập không thành công";
                                            }
                                        }
                                    }
                                    else
                                        dr.Cells["Message"].Value = "Đăng nhập không thành công";

                                    acc.TrangThai = "Die";
                                }
                            }
                            nguoidung.updateNoti(acc);
                            nguoidung.updateLastRun(acc);
                            changeColor(dr, Color.White);
                            dr.Cells[0].Value = false;

                        }
                    }
                    catch
                    { }
                    if (list_acc.Count > 0 && has_stop == false)
                    {
                        goto Lb_Acc;
                    }
                }
            Lb_Finish:
                sendLogs("Tương tác thành công LD : " + ldID);
                if (string.IsNullOrEmpty(proxy) == false)
                {
                    ld.setProxyAdb(ldID, ":0");
                }
                if (!SettingTool.configld.has_quitLD)
                {
                    ld.quit(ldID);
                    frm_main.removeLDToPanel(u);
                }
                //if (File.Exists(tuongtac.strPath))
                //    File.WriteAllLines(tuongtac.strPath, list_group);
            }
            catch (Exception ex)
            {
                sendLogs(ex.ToString());
            }
            if (changeIpHelper.checkGetProxyWaitAny())
            {
                xcontroller.finishProxy(proxy);
            }
        }

        private List<string> getListfriend(string ldID, Account acc)
        {

            LDController controler = new LDController();
            string tk = controler.getToken(acc);

            var client = new RestClient("https://graph.facebook.com/graphql/");
            var request = new RestRequest(Method.POST);
            string userAgent = controler.getUserAgentLD(ldID);

            request.AddHeader("Authorization", "OAuth " + tk.Trim());
            request.AddParameter("q", "me(){friends}");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            // get list uid friend
            IRestResponse response = client.Execute(request);
            string data = response.Content;
            JObject obj = JObject.Parse(data);
            List<string> ls_friend = new List<string>();
            try
            {
                foreach (var item in obj[acc.id.Trim()]["friends"]["nodes"])
                {
                    ls_friend.Add(item["id"].ToString());
                }
            }
            catch
            {

            }

            return ls_friend;

        }

        private string GetIPAddress()
        {
            string add = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    add = Convert.ToString(IP);
                }
            }
            return add;
        }

        private string AddFriendbyUID(userLD u, string deviceid, DataGridViewRow dr, Account acc, int minlike, int maxlike, int delay, CancellationToken token)
        {
            Random rd = new Random();
            int luot = rd.Next(minlike, maxlike);
            u.setStatusSum(luot);
            string path = "";
            try
            {
                path = String.Format("{0}\\Config\\configadd.data", Application.StartupPath);
                SettingTool.configadd = new ConfigAdd();
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    SettingTool.configadd = JsonConvert.DeserializeObject<ConfigAdd>(json);
                }
            }
            catch
            {

            }

            string uid;
            int count = 0;
            path = String.Format("{0}\\logs\\{1}_add.txt", Application.StartupPath, acc.email);
            string historyadd = "";
            try
            {
                historyadd = File.ReadAllText(path);
            }
            catch { }

            if (!File.Exists(acc.pathUID))
            {

                // MessageBox.Show("Chưa thiết lập file UID cho account " + acc.email, "Thông Báo");
                sendLogs("Chưa thiết lập file UID cho account " + acc.email);
                return "Chưa thiết lập file UID cho account ";
            }

            if (acc.pathUID == "")
            {
                sendLogs("Chưa thiết lập file UID cho account " + acc.email);
                return "Add friend by UID: Chưa thiết lập file UID cho account ";
            }

            List<string> list_uid = new List<string>();
            list_uid = File.ReadAllLines(acc.pathUID).ToList().Distinct().ToList();
            if (list_uid.Count <= 0)
            {
                sendLogs("Vui lòng thêm UID để add friend: " + acc.pathUID);
                return "|Add friend by UID: thêm UID để add friend";
            }
            StringBuilder list_history = new StringBuilder();
            int int_loilientiep = 0;
            while (count < luot)
            {
                if (token.IsCancellationRequested)
                    break;
            Lb_Start:
                if (list_uid.Count <= 0)
                {
                    sendLogs("Đã hết UID");
                    goto Lb_FinishAcc;
                }

                uid = list_uid[rd.Next(0, list_uid.Count)];
                list_uid.Remove(uid);
                if (historyadd.Contains(uid))
                {
                    goto Lb_Start;
                }

                int status = ld.addFriendUID(acc, uid, token);

                if (status == 1)
                {
                    int_loilientiep = 0;
                    list_history.AppendLine(uid);
                    count++;
                    u.setStatusResult(count);
                    sendLogs(String.Format("Tài Khoan {0} kết bạn với {1} thành công", acc.email, uid));
                    Delay(delay);
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
                if (count >= luot)
                {
                    goto Lb_FinishAcc;
                }
            }
        Lb_FinishAcc:
            sendLogs("Finish" + acc.email);
            File.AppendAllText(path, list_history.ToString());

            if (SettingTool.configadd.DeleteUID)
            {
                if (!string.IsNullOrEmpty(acc.pathUID))
                    File.WriteAllLines(acc.pathUID, list_uid);
            }

            return "Add friend by UID hoàn thành " + count.ToString() + "/" + luot.ToString();
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
                        this.string_0 = "Đang xử lý dừng tương tác";
                    }
                    this.richTextBox_0.Text = string.Format("{0}:{1}\n", DateTime.Now.ToString("HH:mm:ss"), this.string_0) + this.richTextBox_0.Text;

                }
                catch { }
            }

        }



        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            has_stop = true;
            tokenSource.Cancel();
            method_StopAddFriend();
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
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("USER32.dll")]

        static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        private void richLogs_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("https://youtu.be/4KPJjYC6TJM");
        }

        private void chọnDòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row2 in this.dgvUser.SelectedRows)
            {
                row2.Cells[0].Value = true;
            }
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

        private void btncommentFriend_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtpathcommentfriend.Text = dialog.FileName;
            }
        }

        private void btncommentGroup_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathcommentGroup.Text = dialog.FileName;
            }
        }


        ////////private void GetPosition(int thread)
        ////////{
        ////////    int x = 0;
        ////////    int y = 0;
        ////////    int h = 480;
        ////////    int w = 270;
        ////////    int numLD = thread;
        ////////    int rows = 0;
        ////////    int cols = 0;
        ////////    int formWidth = newform.Width;
        ////////    int formHeigh = newform.Height;
        ////////    int setParents = 0;
        ////////    rows = formHeigh / h;
        ////////    cols = (formWidth / w);

        ////////    while (numLD > rows * (cols))
        ////////    {
        ////////        h = Convert.ToInt16(h * 0.8);
        ////////        w = Convert.ToInt16(w * 0.8);
        ////////        rows = formHeigh / h;
        ////////        cols = (formWidth / w);
        ////////    }

        ////////    for (int r = 0; r < rows; r++)
        ////////    {
        ////////        x = 0;
        ////////        for (int cl = 0; cl < cols; cl++)
        ////////        {

        ////////            if (setParents < numLD)
        ////////            {
        ////////                setParent(x, y, w, h, "index = " + setParents.ToString());
        ////////                PositionLD position = new PositionLD();
        ////////                position.X = x;
        ////////                position.Y = y;
        ////////                position.Heigh = h;
        ////////                position.Width = w;

        ////////                lsPosition.Add(position);
        ////////                setParents++;
        ////////            }

        ////////            else
        ////////                break;

        ////////            x += w + 15;
        ////////        }
        ////////        if (setParents >= numLD)
        ////////            break;
        ////////        else
        ////////            y += h + 15;
        ////////    }
        ////////}
    }
}
