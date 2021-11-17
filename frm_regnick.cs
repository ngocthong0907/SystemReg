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

using System.Net;
using System.Collections.Concurrent;
using System.Web;
using RestSharp;
namespace NinjaSystem
{
    public partial class frm_regnick : Form
    {
        public frm_regnick(frm_MainLD_PRO frm_main)
        {
            InitializeComponent();

            this.frm_main = frm_main;
        }
        List<Account> list_acc = new List<Account>();
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

        LDController ld = new LDController();
        List<string> list_uid = new List<string>();
        Random rdom = new Random();
        object synUID = new object();

        frm_MainLD_PRO frm_main;
        List<string> list_group;
        xProxyController xcontroller = new xProxyController();
        private void frm_TuongTacLD_Load(object sender, EventArgs e)
        {
            DataTable source = new DataTable();
            Data dt = new Data();
            source = dt.select("select * from Danhmuc");
            cboNhom.DisplayMember = "Tendanhmuc";
            cboNhom.ValueMember = "Id_danhmuc";
            cboNhom.DataSource = source;

            regnick regcf = new regnick();
            string path = String.Format("{0}\\Config\\Configreg.data", Application.StartupPath);
            if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    regcf = JsonConvert.DeserializeObject<regnick>(json);
                }
            }
            txtTen.Text = regcf.ten;
            txtHo.Text = regcf.ho;
            txtcover.Text = regcf.cover;
            txtPassword.Text = regcf.password;
            txtApi.Text = regcf.api;
            txtavatar.Text = regcf.avatar;

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

        private bool setupCauHinh()
        {
            return true;
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
                cell2.Value = "";// acc.ldid;
                dataGridViewRow.Cells.Add(cell2);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = "";
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.name;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = "Live";
                dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell9 = new DataGridViewTextBoxCell();
                cell9.Value = "";
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
            regnick regcf = new regnick();
            regcf.ten = txtTen.Text;
            regcf.ho = txtHo.Text;
            regcf.password = txtPassword.Text;
            regcf.api = txtApi.Text;
            regcf.avatar = txtavatar.Text;
            regcf.cover = txtcover.Text;
            string path = String.Format("{0}\\Config\\Configreg.data", Application.StartupPath);
            File.WriteAllText(path, JsonConvert.SerializeObject(regcf));

            dgvUser.Rows.Clear();
            list_acc.Clear();
            try
            {
                for (int i = 1; i <= SettingTool.configld.numthread; i++)
                {
                    Account acc = new Account();
                    acc.name = "Thead: " + i.ToString();
                    list_acc.Add(acc);
                }
            }
            catch
            { }
            method_LoadAccount();
            Delay(1);
            changeIpHelper.createLDID(SettingTool.configld.numthread);
            has_stop = false;
            // ld.changeIp();
            list_ldrun = new List<LDRun>();
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
            //chon cau hinh
            if (setupCauHinh())
            {
                list_dr = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dgvUser.Rows)
                {
                    if ((bool)row.Cells[0].Value)
                    {
                        list_dr.Add(row);

                    }
                }

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
            else
            {
                if (SettingTool.configld.language == "English")
                    MessageBox.Show("Please select a configuration before running the interaction", "Notifiacation");
                else
                    MessageBox.Show("Vui lòng chọn cấu hình trước khi chạy tương tác", "Thông báo");
                pibStatus.Visible = false;
            }
        }
        private void setupLDGoc()
        {
            string pathgoc = Application.StartupPath + "\\Config\\SetupLD.txt";
            if (File.Exists(pathgoc) == false)
            {
                string ldID = "0";
                ld.KhoiTaoLDGoc();
                CancellationToken token = tokenSource.Token;

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
                        return;
                    }
                }
                if (SettingTool.configld.appversion == "Facebook 299")
                {
                    if (ld.checkVersionApp(ldID, "299") == false)
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
                ld.killApp(ldID, "com.facebook.katana");
                ld.runApp(ldID, "com.facebook.katana");
                Delay(10);
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

                if (ld.checkApp(ldID, "com.cell47.College_Proxy") == false)
                {
                    string path = Application.StartupPath + "\\app\\proxy.apk";
                    if (File.Exists(path))
                    {
                        u.setStatus(ldID, "Install app proxy...");
                        ld.installApp(ldID, path);
                        Thread.Sleep(3000);
                    }
                }

                ld.disableGPS(ldID);
                ld.setKeyboard(ldID);
                DetechModel kq = ld.checkOpenFacebookFinish(u, ldID);
                if (kq.status)
                {


                }
                Thread.Sleep(10000);
                File.WriteAllText(pathgoc, "OK");
                ld.quit1(ldID);
                frm_main.removeLDToPanel(u);
            }

        }
        private void closeLdfree()
        {
            List<string> Ldrun = new List<string>();
            foreach (userLD ldrun in this.frm_main.list_ldopen)
            {
                Ldrun.Add(ldrun.ldid);
            }

            for (int i = 1; i <= SettingTool.configld.numthread * 3; i++)
            {
                if (!Ldrun.Contains(i.ToString()))
                    ld.quit(i.ToString());
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
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void runTuongTacWaitAny()
        {
            try
            {
               // setupLDGoc();
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
                int countAcc = 0;
                //khoi tao list task
                Task[] list_task = TaskController.createTask(numthread);
                int maxproxy = 0;
            Lb_quayvong:
                tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
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
                        var token = tokenSource.Token;
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
                                                    //countAcc++;
                                                    //if (countAcc % SettingTool.configld.numthread * 2 == 0)
                                                    //    closeLdfree();
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
                                            // xcontroller.finishProxy(proxy);
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
                                                countAcc++;
                                                if (countAcc % SettingTool.configld.numthread * 2 == 0)
                                                    closeLdfree();
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
                    //tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                    try
                    {
                        Task.WaitAny(list_task);
                    }
                    catch
                    { }
                    if (list_dr.Count > 0 && has_stop == false)
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

                                    }
                                }

                                if (list_dr.Count > 0)
                                    goto Lb_quayvong;
                                else
                                {
                                    foreach (DataGridViewRow row2 in this.dgvUser.Rows)
                                    {
                                        row2.Cells[0].Value = true;
                                        Account acc = (Account)row2.Tag;
                                        list_dr.Add(row2);

                                        row2.Cells["Message"].Value = "";
                                    }
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
                setupLDGoc();
                int lap = 0;
                int countAcc = 0;
            Lb_quayvong:
                tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                var token = tokenSource.Token;
                int numthread = SettingTool.configld.numthread;
                if (numthread > list_dr.Count)
                {
                    numthread = list_dr.Count;

                }
                SettingTool.configld.numthreadxproxy = numthread;
                Task[] tasks = new Task[numthread];
                int int_proxy = 0;
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
                            method_log(String.Format("Api {0} - IP {1} - Next change {2}s - Timout - {3}ms - {4}", ts.api, ts.proxy, ts.next_change, ts.timeout, ts.description));
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
                    int k = 0;
                    object synDevice = new object();
                    for (int i = 0; i < numthread; i++)
                    {
                        int t = i;
                        tasks[t] = Task.Factory.StartNew(() =>
                        {
                            if (has_stop == false)
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
                                        if (countAcc % SettingTool.configld.numthread * 2 == 0)
                                            closeLdfree();
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
                            }

                        }, token);

                        Thread.Sleep(SettingTool.configld.timedelay * 1000);
                    }
                    sendLogs(String.Format("Total Thread 1 : {0} ", tasks.Count()));
                    //tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                    try
                    {
                        Task.WaitAll(tasks);
                    }
                    catch (OperationCanceledException)
                    {

                    }
                    if (list_dr.Count > 0 && has_stop == false)
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

                                    }
                                }
                                if (list_dr.Count > 0)
                                    goto Lb_quayvong;
                                else
                                {
                                    foreach (DataGridViewRow row2 in this.dgvUser.Rows)
                                    {
                                        row2.Cells[0].Value = true;
                                        Account acc = (Account)row2.Tag;
                                        list_dr.Add(row2);

                                        row2.Cells["Message"].Value = "";
                                    }
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
        private void changeDevice(string ldid)
        {
            ld.fakedevice(ldid);
        }

        private bool openLD(userLD u, string ldID, DataGridViewRow dr, Account acc, CancellationToken token)
        {
            changeColor(dr, Color.Yellow);
            method_log("Open LDPlayer Id: " + ldID);

            dr.Cells["clID"].Value = ldID;
            acc.ldid = ldID;
            // dr.Cells["Message"].Value = "Restore Data LD";
            // ld.setupLD(acc, ldID);
            dr.Cells["Message"].Value = "Open LD";

            frm_main.addLDToPanel(u);
            if (ld.launchSetPosion(ldID, u, token))
            {
                u.setStatus(ldID, "Connect successful LD...");
                dr.Cells["Message"].Value = "Connect successful LD: " + ldID;
                return true;
            }
            else
            {
                if (ld.autoRunLDSetPosition(ldID, u, token))
                {
                    u.setStatus(ldID, "Connect successful LD...");
                    dr.Cells["Message"].Value = "Connect successful LD: " + ldID;
                    return true;
                }
                else
                {
                    u.setStatus(ldID, "Disconnected...");
                    dr.Cells["Message"].Value = "Disconnected LD: " + ldID;
                    method_log("Disconnected LD: " + ldID);
                    return false;
                }
            }

        }


        private void method_Start(string ldID, DataGridViewRow dr, string proxy, CancellationToken token)
        {
            changeColor(dr, Color.Yellow);
            Account acc = (Account)dr.Tag;
            userLD u = frm_main.checkExits(ldID);
            if (!openLD(u, ldID, dr, acc, token))
            {
                return;
            }
            Delay(10);
            #region doi ip sau khi mo ld thanh cong
            if (string.IsNullOrEmpty(proxy) == false)
            {
                u.setDevice(ldID, acc.id, proxy);
                u.setStatus(ldID, "Change proxy : " + proxy);
                if (SettingTool.configld.sock5)
                {
                    SettingTool.configld.proxytype = "socks5";
                    ld.setProxyAuthentica_proxydroid(ldID, proxy, token);
                    string yourip = ld.checkIPSock5(proxy);
                    u.setDevice(ldID, proxy + " - " + yourip);
                    if (SettingTool.configld.checkproxy)
                    {
                        if (string.IsNullOrEmpty(yourip))
                        {
                            sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                            
                        }
                    }
                }
                else
                {
                    changeIpHelper.changeProxyAdb(ldID, proxy);
                    //check ip
                    string yourip = ld.checkIP(proxy);
                    if (SettingTool.configld.checkproxy)
                    {
                        if (string.IsNullOrEmpty(yourip))
                        {
                            sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                           
                        }
                    }
                    u.setDevice(ldID, acc.id, proxy + " - " + yourip);
                }
            }
            changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
            #endregion
            Delay(2);
            u.setStatus(ldID, "Change device...");
            dr.Cells["Message"].Value = "Change device... ";
            changeDevice(ldID);
            dr.Cells["Message"].Value = "Change device success";
            ld.quit(acc, ldID);
            frm_main.removeLDToPanel(u);

            List<string> ho = new List<string>();
            ho = File.ReadLines(txtHo.Text).ToList();
            List<string> ten = new List<string>();
            ten = File.ReadLines(txtTen.Text).ToList();
            int total = (int)numtotal.Value;
            int success = 0;
           
            try
            {
                while (success < total)
                {

                    if (token.IsCancellationRequested)
                    {
                        goto Lb_Finish;
                    }
                    u = frm_main.checkExits(ldID);

                    if (openLD(u, ldID, dr, acc, token))
                    {
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
                                        sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                                        //goto Lb_Finish;
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
                                        sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                                        //goto Lb_Finish;
                                    }
                                }
                                u.setDevice(ldID, acc.id, proxy + " - " + yourip);
                            }
                        }
                        changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
                        #endregion
                        ld.setKeyboard(ldID);
                        ld.runAdb(ldID, " shell pm disable-user --user 0 com.android.flysilkworm");
                        changeColor(dr, Color.Yellow);
                        dr.Cells["Message"].Value = "Running";
                        try
                        {
                            acc = (Account)dr.Tag;
                            List<string> list_file_image = new List<string>();
                            List<string> imagedelete = new List<string>();
                            list_file_image = System.IO.Directory.GetFiles(txtavatar.Text, "*.*").ToList();
                            imagedelete = ld.copyimagenew(acc.ldid, list_file_image, 1);

                            Delay(2);
                            dr.Cells["Message"].Value = "Create contact";
                            u.setStatus(ldID, "Create contact");
                            createContact(ldID);
                            Delay(1);

                            ld.killApp(acc.ldid, "com.facebook.katana");
                            Delay(1);
                            ld.clearappfb(acc.ldid);
                            ld.runApp(acc.ldid, "com.facebook.katana");
                            ld.checkOpenFacebookFinish(u, acc.ldid);
                           
                            dr.Cells["Message"].Value = "Reg nick";
                            u.setStatus(ldID, "Reg Nick");
                            u.setStatusSum(total);
                            u.setStatusResult(success);

                            acc.first_name = ho[rd.Next(0, ho.Count)];
                            acc.last_name = ten[rd.Next(0, ten.Count)];
                            acc.Password = txtPassword.Text;
                            acc.name = acc.first_name + " " + acc.last_name;

                            if (rdNam.Checked)
                            {
                                acc.gender = "Male";
                            }
                            else
                            {
                                acc.gender = "Female";
                            }
                            bool result_reg = false;
                            if (chkchangenumber.Checked)
                                result_reg = ld.regnick_V2(acc, dr, u, txtApi.Text, (int)numdelayclickmin.Value, (int)numdelayclickmax.Value, checkBox1.Checked, cboWebsite.Text, token);
                            else
                                result_reg = ld.regnick(acc, dr, u, txtApi.Text, (int)numdelayclickmin.Value, (int)numdelayclickmax.Value, checkBox1.Checked, cboWebsite.Text, token);

                            if (result_reg)
                            {
                                foreach (string del in imagedelete)
                                {
                                    File.Delete(del);
                                }
                                dr.Cells["Message"].Value = "Reg nick success";
                                u.setStatus(ldID, "Reg Nick success");
                                Delay(2);
                                success++;
                                acc.Password = txtPassword.Text;
                                int id_danhmuc = Convert.ToInt32(cboNhom.SelectedValue.ToString());
                                acc.id_danhmuc = id_danhmuc;
                                acc.tendanhmuc = cboNhom.Text;
                                acc.TrangThai = "Live";
                                acc.id = "@";
                                ld.SaveTokenCookies(acc);
                                acc.email = acc.phone.Trim();
                                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                                nguoidung.insertAccount(acc);

                                dr.Cells["Message"].Value = "Cover";
                                u.setStatus(ldID, "Cover");
                                list_file_image = System.IO.Directory.GetFiles(txtcover.Text, "*.*").ToList();
                                if (ld.ChangeCover(ldID, "com.facebook.katana", list_file_image, token, true))
                                {
                                    dr.Cells["Message"].Value = "Cover success";
                                    u.setStatus(ldID, "Cover success");
                                    Delay(3);
                                }
                                else
                                {
                                    dr.Cells["Message"].Value = "Cover fail";
                                    u.setStatus(ldID, "Cover fail");
                                    Delay(3);
                                }

                                dr.Cells["Message"].Value = "Sở thích";
                                u.setStatus(ldID, "Sở thích");

                               if( ld.ChangeHobby(ldID, acc, "", token))
                               {

                                   dr.Cells["Message"].Value = "Thêm Sở thích thành công";
                                   u.setStatus(ldID, "Thêm Sở thích thành công");
                               }
                               else
                               {

                                   dr.Cells["Message"].Value = "Thêm Sở thích không thành công";
                                   u.setStatus(ldID, "Thêm Sở thích không thành công");
                               }

                               dr.Cells["Message"].Value = "hôn nhân";
                               u.setStatus(ldID, "hôn nhân");

                               if (ld.ChangeMarriage(ldID, acc, "", token))
                               {

                                   dr.Cells["Message"].Value = "Thêm hôn nhân thành công";
                                   u.setStatus(ldID, "Thêm hôn nhân thành công");
                               }
                               else
                               {

                                   dr.Cells["Message"].Value = "Thêm hôn nhân không thành công";
                                   u.setStatus(ldID, "Thêm hôn nhân không thành công");
                               }

                                List<Account> ls = new List<Account>();
                                ls = nguoidung.loadUserbySql(string.Format("select * from Account where email = '{0}'", acc.phone.Trim()));
                                if (ls.Count == 1)
                                {
                                    dr.Cells["Message"].Value = "2fa";
                                    u.setStatus(ldID, "2fa");
                                    acc.Id_account = ls[0].Id_account;
                                    string result2fa = ld.bat_2fa(ldID, acc, "com.facebook.katana", token);
                                    dr.Cells["Message"].Value = result2fa;
                                    u.setStatus(ldID, result2fa);
                                    Delay(3);
                                }
                                dr.Cells["Message"].Value = "Watch video";
                                u.setStatus(ldID, "Watch video");
                                ld.scrollNewfeedWatch(u, ldID, acc, (int)numscrollmin.Value, (int)numscrollmax.Value, token, (int)numviewmin.Value, (int)numviewmax.Value);
                                ld.Zip(acc, ldID);
                               int delay = rd.Next((int)numdelayregmin.Value, (int)numdelayregmax.Value);

                                while (delay >= 0)
                                {
                                    dr.Cells["Message"].Value = "Delay " + delay.ToString() + "s";
                                    u.setStatus(ldID, "Delay..." + delay.ToString() + "s");
                                    Thread.Sleep(1000);
                                    delay--;
                                }
                            }
                            else
                            {
                                dr.Cells["Message"].Value = "Reg nick fail";
                                u.setStatus(ldID, "Reg Nick fail");
                                Delay(3);
                              
                            }
                        }
                        catch
                        {
                        }
                    }

                    if (success < total)
                    {
                        if (string.IsNullOrEmpty(proxy) == false)
                        {
                            ld.setProxyAdb(ldID, ":0");
                        }

                        if (changeIpHelper.checkGetProxyWaitAny())
                        {
                            xcontroller.finishProxy(proxy);
                        }

                        if (changeIpHelper.checkGetProxyWaitAny())
                        {
                            proxy = "";
                            while (string.IsNullOrEmpty(proxy))
                            {
                                dr.Cells["Message"].Value = "Get proxy: ";
                                method_log("Get proxy: " + proxy);
                                proxy = xcontroller.getProxy();
                                if (!string.IsNullOrEmpty(proxy))
                                    break;

                                method_log("Proxy chưa sẵn sàng2: " + proxy);
                                Thread.Sleep(6000);
                            }
                        }
                        Delay(10);
                        #region doi ip sau khi mo ld thanh cong
                        if (string.IsNullOrEmpty(proxy) == false)
                        {
                            u.setDevice(ldID, acc.id, proxy);
                            u.setStatus(ldID, "Change proxy : " + proxy);
                            if (SettingTool.configld.sock5)
                            {
                                SettingTool.configld.proxytype = "socks5";
                                ld.setProxyAuthentica_proxydroid(ldID, proxy, token);
                                string yourip = ld.checkIPSock5(proxy);
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
                                string yourip = ld.checkIP(proxy);
                                if (SettingTool.configld.checkproxy)
                                {
                                    if (string.IsNullOrEmpty(yourip))
                                    {
                                        sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                                        //goto Lb_Finish;
                                    }
                                }
                                u.setDevice(ldID, acc.id, proxy + " - " + yourip);
                            }
                        }
                        changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
                        #endregion

                        u.setStatus(ldID, "Change device...");
                        dr.Cells["Message"].Value = "Change device... ";
                        changeDevice(ldID);
                    }
                    ld.quit(acc, ldID);
                    frm_main.removeLDToPanel(u);
                }

            Lb_Finish:
                sendLogs("Finish Reg : " + ldID);
            }
            catch (Exception ex)
            {
                sendLogs(ex.ToString());
            }

            changeColor(dr, Color.White);
            dr.Cells["Message"].Value = "Reg success: " + success.ToString() + "/" + total.ToString();
        }

        private void createContact(string ldid)
        {
            ld.runAdb(ldid, " shell pm clear com.android.providers.contacts");

            List<string> strNumber = new List<string>();
            List<string> lscontact = new List<string>();
            lscontact = File.ReadLines(Application.StartupPath + "\\Contact.txt").ToList();
            Random rd = new Random();
            for (int i = 0; i < 300; i++)
            {
                strNumber.Add(lscontact[rd.Next(0,lscontact.Count)]);
            }
            Importcontact(ldid,strNumber);

        }

        private string Importcontact(string ldID, List<string> strNumber)
        {
            string cmd = string.Format(" shell pm clear com.android.providers.contacts");
            string output = ld.runAdb(ldID, cmd);

            // string[] strNumber = txtNumber.Lines;

            List<string> ls = new List<string>();
            string lsName = "{hùng|dũng|hoa|nụ|anh|tuấn|hồng|nhung|nguyệt|loan|thành|sỹ|phong|link|trinh|nga|mai|thảo|trang|ly|giang|tiến}";

            string fullname = "{đinh| đinh nguyễn| lê trần| lê|trần|đỗ văn| đỗ|tạ|nguyễn|hứa hữu|lương|hồ ngọc|lân|bạch|huỳnh|hoàng nguyễn|lê|đường|trần huyền|đinh ngọc|cao thái|lý tiểu}";

            Random rd = new Random();
            if (strNumber.Count > 0)
            {
                for (int n = 0; n < strNumber.Count; n++)
                {
                    ls.Add("BEGIN:VCARD");
                    ls.Add("VERSION:3.0");
                    ls.Add("FN:" + FunctionHelper.method_Spin(fullname) + " " + FunctionHelper.method_Spin(lsName) + " " + FunctionHelper.method_Spin(lsName));
                    ls.Add("TEL;TYPE=CELL:" + strNumber[n]);
                    ls.Add("END:VCARD");
                }
                string nameRandom = string.Format("contact_{0}.vcf", rd.Next(0, 99999).ToString());
                //  string namefilevcf = string.Format("c:\\test\\contact_{0}.vcf", nameRandom);

                string namefilevcf = string.Format("c:\\test\\{0}\\pictures\\{1}", ldID, nameRandom);


                File.AppendAllLines(namefilevcf, ls);
                //if (SettingTool.configld.versionld == "3.x")
                //    cmd = string.Format(" shell mv -i storage/emulated/legacy/pictures/{0} sdcard/", nameRandom);
                //else
                //{
                //    cmd = string.Format(" shell mv -i storage/emulated/0/pictures/{0} sdcard/", nameRandom);
                //}

                output = ld.runAdb(ldID, cmd);
                cmd = string.Format("shell am start -t text/x-vcard -d file:///storage/emulated/0/pictures/{0} -a android.intent.action.VIEW com.android.contacts", nameRandom);
                cmd = ld.runAdb(ldID, cmd);

                List<DetechModel> ls_detecth = new List<DetechModel>();
                DetechModel dtmodel = new DetechModel();
                dtmodel = new DetechModel();
                dtmodel.content = "Nhập liên hệ từ vCard?";
                dtmodel.text = "ok";
                dtmodel.function = 1;
                dtmodel.node = "//node[contains(@class,'android.widget.Button')]";
                ls_detecth.Add(dtmodel);

                dtmodel = new DetechModel();
                dtmodel = new DetechModel();
                dtmodel.content = "Cho phép";
                dtmodel.text = "Cho phép";
                dtmodel.function = 1;
                dtmodel.node = "//node[contains(@class,'android.widget.Button')]";
                ls_detecth.Add(dtmodel);

            lb_start:
                DetechModel result = ld.RunDetechFunction(ldID, ls_detecth, 2);

                if (result.status)
                {
                    ld.ClickOnLeapdroidPosition(ldID, result.point);
                    Thread.Sleep(1000);
                    goto lb_start;
                }

                File.Delete(namefilevcf);
                return "Cập nhập thành công: " + strNumber.Count().ToString() + " số";
            }
            return "Không có số điện thoại";

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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtavatar.Text = fldrDlg.SelectedPath;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtcover.Text = fldrDlg.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LDController ld = new LDController();
            RaiSim pone = ld.getPhoneCodetextnow(txtApi.Text);


            string code = ld.getCode2textnow(txtApi.Text, pone.sessionid);
        }



    }
}
