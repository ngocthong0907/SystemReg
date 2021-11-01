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
    public partial class frm_Getbirthday : Form
    {
        public frm_Getbirthday(frm_MainLD_PRO frm_main, List<Account> list_acc)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        List<Account> list_acc;
        List<LDRun> list_ldrun = new List<LDRun>();

        bool stop = false;
        Thread thread_1;
        object synAcc = new object();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        LDController ld = new LDController();
        int countComplete = 0;
        //  runLDs formLD = new runLDs();
        public frm_MainLD_PRO frm_main;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void frm_Getbirthday_Load(object sender, EventArgs e)
        {
            method_LoadAccount();
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }
        }
        public void method_LoadAccount()
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
                cell5.Value = acc.name;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.Password;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = acc.privatekey;
                dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell7a = new DataGridViewTextBoxCell();
                cell7a.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell7a);

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

        private void button1_Click(object sender, EventArgs e)
        {
            changeIpHelper.createLDID(SettingTool.configld.numthread);
            tokenSource = new CancellationTokenSource();
            list_ldrun = new List<LDRun>();
            startLogin();
            ClearMessage();
        }
        private void startLogin()
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

            if (list_dr.Count <= 0)
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
        private void runLoginWaitAny()
        {
            setupLDGoc();

            method_logStatus("Bắt đầu đăng nhập tài khoản");
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

                #endregion

                //chay luon sau khi 1 ld hoan thanh
                object synDevice = new object();
                int i = 0;
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
                    stopLogin();

                }
            }
        }
        private void runLogin()
        {
            setupLDGoc();
            method_logStatus("Bắt đầu đăng nhập tài khoản");
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
                    stopLogin();

                }
            }
        }
        private void stopLogin()
        {
            pibStatus.Visible = false;

            stop = true;
            if (thread_1 != null)
                thread_1.Abort();

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
                ld.disableGPS(ldID);
                ld.setKeyboard(ldID);



                DetechModel kq = ld.checkOpenFacebookFinish(u, ldID);
                if (kq.status)
                {


                }
                Thread.Sleep(10000);
                ld.runAdb(ldID, "shell  rm -r storage/emulated/legacy/launcher/ad");//delete
                File.WriteAllText(pathgoc, "OK");
                ld.quit1(ldID);
                frm_main.removeLDToPanel(u);
            }

        }
        private void method_Start(string ldID, DataGridViewRow dr, string proxy, CancellationToken token)
        {
            changeColor(dr, Color.Yellow);
            Account acc = (Account)dr.Tag;
            dr.Cells["Message"].Value = "Restore Data LD";
            acc.ldid = ldID;
            method_log("Open LDPlayer Id: " + ldID);
            dr.Cells["LdId"].Value = ldID;
            ld.setupLD(acc, ldID);
            dr.Cells["Message"].Value = "Open LDPlayer Id: " + ldID;
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
            //  ld.restoredatafb(acc.ldid, acc.id);
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
                //if (ld.checkApp(ldID, "com.facebook.katana") == false)
                //{
                //    string path = Application.StartupPath + "\\app\\Facebook.apk";
                //    if (File.Exists(path))
                //    {
                //        u.setStatus(ldID, "Install App Facebook...");
                //        ld.installApp(ldID, path);
                //        while (ld.checkApp(ldID, "com.facebook.katana") == false)
                //        {
                //            Thread.Sleep(1000);
                //        }
                //        Thread.Sleep(3000);
                //    }
                //}
                ld.disableGPS(ldID);
                ld.setKeyboard(ldID);
                #region doi ip sau khi mo ld thanh cong
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
                if (string.IsNullOrEmpty(proxy) == false)
                {
                    u.setDevice(ldID, acc.id, proxy);
                    u.setStatus(ldID, "Change proxy : " + proxy);
                    if (SettingTool.configld.sock5 && SettingTool.configld.typeip == 7)
                    {
                        ld.killApp(ldID, "org.proxydroid");
                        ld.runApp(ldID, "org.proxydroid");
                        Thread.Sleep(3000);
                        ld.changeSock5Proxy(ldID, proxy);

                    }
                    else
                    {
                        changeIpHelper.changeProxyAdb(ldID, proxy);
                        //check ip
                        string yourip = ld.checkIP(proxy);
                        u.setDevice(ldID, acc.id, proxy + " - " + yourip);
                    }

                }
                changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
                #endregion

                try
                {
                    changeColor(dr, Color.Yellow);
                    dr.Cells["Message"].Value = "Running";

                    ld.killApp(acc.ldid, "com.facebook.katana");
                    ld.restoredatafb(acc.ldid, acc.id);
                    ld.runApp(acc.ldid, "com.facebook.katana");

                    DetechModel kq = ld.checkOpenFacebookFinish(u, acc.ldid);
                    if (kq.status)
                    {
                        switch (kq.parent)
                        {
                            case "loginsucess":
                                {
                                    //dr.Cells["Message"].Value = "Logout";
                                    //u.setStatus(ldID, " Logout...");
                                    //ld.logoutLD(u, acc);
                                }
                                break;
                            case "loginavatar":
                                {
                                    dr.Cells["Message"].Value = "Logout";
                                    u.setStatus(ldID, " Logout...");
                                    ld.logoutLD(u, acc);
                                }
                                break;
                        }

                    }

                    dr.Cells["Message"].Value = "Login";
                    u.setDevice(acc.ldid, acc.id);
                    u.setStatus(ldID, " Login facebook...");

                    bool haslogin = ld.loginFacebookLD(acc, token);
                    if (haslogin)
                    {
                        if (stop)
                        {
                            goto Lb_Finish;
                        }
                        dr.Cells["Message"].Value = "Login successful";
                        dr.Cells["clstatus"].Value = "Live";
                        acc.TrangThai = "Live";
                        acc.Thongbao = "Login successful";
                        u.setStatus(ldID, "Login successful...");
                        Thread.Sleep(2000);

                        u.setStatus(ldID, "Đang lấy thông tin ngày sinh...");
                        dr.Cells["Message"].Value = "Đang lấy thông tin ngày";
                        if (ld.Getbirthday(u,acc,ldID, token))
                        {
                            Thread.Sleep(2000);
                            acc.Thongbao += "Lấy thông tin ngày sinh thành công";
                            u.setStatus(ldID, "Lấy thông tin ngày sinh thành công...");
                            dr.Cells["Message"].Value = "Lấy thông tin ngày sinh thành công";
                        }
                        else
                        {
                            Thread.Sleep(2000);
                            acc.Thongbao += "Lấy thông tin ngày sinh không thành công";
                            u.setStatus(ldID, "Lấy thông tin ngày sinh không thành công...");
                            dr.Cells["Message"].Value = "Lấy thông tin ngày sinh không thành công";
                        }

                        u.setStatus(ldID, "Đang backup dataprofile");
                        ld.Zip(acc, ldID);
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

                        acc.TrangThai = "Die";
                        dr.Cells["clstatus"].Value = "Die";

                        u.setStatus(ldID, "Đang backup dataprofile");

                    }

                    // changeColor(dr, Color.White);
                    nguoidung.updateNoti(acc);
                }
                catch (Exception ee)
                {
                    File.AppendAllText(String.Format("\n {0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + " Error: " + ee.Message + "\n");
                }

            }
            catch
            { }
        Lb_Finish:
            if (string.IsNullOrEmpty(proxy) == false)
            {
                //remove proxy tinsoft
                ld.setProxyAdb(ldID, ":0");
            }
            if (changeIpHelper.checkGetProxyWaitAny())
            {
                xcontroller.finishProxy(proxy);
            }
            ld.quit(acc, ldID);
            frm_main.removeLDToPanel(u);
            changeColor(dr, Color.White);
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

        private void btnInput_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            method_Stop();
        }
        private void method_Stop()
        {
            pibStatus.Visible = false;
            stop = true;

            if (thread_1 != null)
                thread_1.Abort();
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
        private void method_logStatus(string string_15)
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
        private sealed class Class32
        {
            public ToolStripStatusLabel lbn;
            public string string_0;

            public void method_0()
            {
                try
                {
                    lbn.Text = string_0;

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
            this.Text = "Login facebook account into Ld player";
            button1.Text = "Login";
            btnInput.Text = "Stop";
            groupBox1.Text = "List accounts";
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
        //private void runTuongTac1LD(CancellationToken token)
        //{
        //    try
        //    {
        //        int numthread = 1;
        //        if (numthread > list_ld.Count)
        //        {
        //            numthread = list_ld.Count;
        //        }

        //        Task[] tasks = new Task[numthread];
        //        if (list_ld.Count > 0)
        //        {
        //            object synDevice = new object();
        //            for (int i = 0; i < numthread; i++)
        //            {
        //                int t = i;
        //                tasks[t] = Task.Factory.StartNew(() =>
        //                {
        //                    if (stop == false)
        //                    {
        //                        String ldid = "";
        //                        List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
        //                        lock (synDevice)
        //                        {
        //                            ldid = list_ld[0];
        //                            list_ld.Remove(ldid);
        //                            foreach (DataGridViewRow dr in list_dr)
        //                            {
        //                                Account acc = (Account)dr.Tag;
        //                                if (acc.ldid == ldid)
        //                                {
        //                                    list_acc.Add(dr);
        //                                }
        //                            }
        //                        }
        //                        method_Start(ldid, list_acc, token);
        //                    }

        //                }, token);
        //            }

        //            try
        //            {
        //                Task.WaitAll(tasks);
        //            }
        //            catch (OperationCanceledException)
        //            {

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        method_log(ex.ToString());
        //    }
        //}

        private void ClearMessage()
        {
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                if ((bool)dr.Cells[0].Value)
                {
                    dr.Cells["Message"].Value = "";
                    changeColor(dr, Color.White);
                    dr.Cells["LdId"].Value = "";
                }

            }
        }
        public void KhoiTaoLDGoc()
        {
            string ldid = "0";
            quit1(ldid);

            string LDsource = string.Format("{0}\\vms\\leidian0\\data.vmdk", SettingTool.configld.pathLD);
            if (File.Exists(LDsource))
            {
                File.Delete(LDsource);
            }
            createLD(ldid);
        }
        public void createLD(string ldid)
        {
            deleteLD(ldid);
            string cmd = String.Format("add --name {0} ", ldid);
            ExecuteAsAdmin(SettingTool.pathLD, cmd);

        }
        private void deleteLD(string ldid)
        {
            try
            {
                string path = string.Format("{0}\\vms\\config\\leidian{1}.config", SettingTool.configld.pathLD, ldid);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = string.Format("{0}\\vms\\leidian{1}", SettingTool.configld.pathLD, ldid);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);

                }
            }
            catch
            { }
        }
        public string ExecuteAsAdmin(string fileName, string cmd)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.Verb = "runas";
                proc.StartInfo.Arguments = cmd;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();


                string cv_out = null;
                Thread ot = new Thread(() => { cv_out = proc.StandardOutput.ReadToEnd(); });
                ot.Start();

                int timeout = 10000;
                proc.WaitForExit(timeout);
                // ot.Join();
                //   et.Join();

                // ot.Join();
                //   et.Join();

                return cv_out;

            }
            catch { }
            return null;

        }
        public void quit1(string ldID)
        {
            try
            {
                string cmd = String.Format("quit --index {0}", ldID);
                string html = ExecuteAsAdmin(SettingTool.pathLD, cmd);
                //Thread.Sleep(2000);
            }
            catch { }

        }
    }
}
