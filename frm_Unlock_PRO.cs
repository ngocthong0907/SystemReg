using Newtonsoft.Json.Linq;
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

namespace NinjaSystem
{
    public partial class frm_Unlock_PRO : Form
    {
        public frm_Unlock_PRO(frm_MainLD_PRO frm_main, List<Account> list_acc)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        xProxyController xcontroller = new xProxyController();
        List<Account> list_acc;

        bool stop = false;
        Thread thread_1;
        object synAcc = new object();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        LDController ld = new LDController();
        int countComplete = 0;
        //  runLDs formLD = new runLDs();
        public frm_MainLD_PRO frm_main;
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        private void frm_Unlock_Load(object sender, EventArgs e)
        {

            method_LoadAccount();

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
                cell7.Value = acc.birthday;
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

            tokenSource = new CancellationTokenSource();
            startLogin();
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

        private void runLoginWaitAny()
        {
            var token = tokenSource.Token;
            xProxyController xcontroller = new xProxyController();
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
            xProxyController xcontroller = new xProxyController();
        Lb_quayvong:
            var token = tokenSource.Token;
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
                    if (stop == false)
                    {

                        tasks[t] = Task.Factory.StartNew(() =>
                        {
                            if (token.IsCancellationRequested == false)
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
        private void method_Start(string ldID, DataGridViewRow dr, string proxy, CancellationToken token)
        {
            method_log("Open LDPlayer Id: " + ldID);

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
                    u.setStatus(ldID, "Connect successful LD...");
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

                    dr.Cells["Message"].Value = "Login";
                    u.setStatus(ldID, " Login facebook...");
                    bool status = ld.checkIsLogin(acc);
                    if (status)
                    {
                        status = true;
                    }
                    else
                    {
                        ld.loginAvatarLD(acc);
                        status = ld.loginFacebookLD(acc, token);
                    }

                    if (status)
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
                                if (acc.cookies.Contains(acc.id))
                                {
                                    // acc.useragent = ld.getUserAgentLD(ldID);
                                    nguoidung.updateTokenCookie(acc);
                                }
                            }

                        }
                        u.setStatus(ldID, "Đang backup dataprofile");
                        ld.Zip(acc, ldID);

                    }
                    else
                    {
                        string newpass = txtPassword.Text.Trim();
                        if (chkRandomPass.Checked)
                        {
                            newpass = FunctionHelper.RandomString1(9);
                        }

                        if (ld.unlockNgaySinh(u, acc, dr, newpass, token))
                        {
                            acc.TrangThai = "Live";
                            acc.Thongbao = "Mở checkpoint thành công";
                            dr.Cells["Message"].Value = "Mở checkpoint thành công";
                        }
                        else
                        {
                            acc.Thongbao += " Mở checkpoint không thành công";
                            dr.Cells["Message"].Value = acc.Thongbao + " Mở checkpoint không thành công";
                        }
                    }
                    changeColor(dr, Color.White);
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
            // u.setStatus(ldID, "Backup Profile LD...");

            if (string.IsNullOrEmpty(proxy) == false)
            {
                ld.setProxyAdb(ldID, ":0");
            }
            if (changeIpHelper.checkGetProxyWaitAny())
            {
                xcontroller.finishProxy(proxy);
            }
            ld.quit(acc, ldID);
            frm_main.removeLDToPanel(u);
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

        private void dgvUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (DataGridViewRow row2 in this.dgvUser.SelectedRows)
                {
                    row2.Cells[0].Value = true;

                }
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

    }
}
