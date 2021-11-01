using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_AddFriendLD : Form
    {
        public frm_AddFriendLD(List<Account> list_acc, frm_MainLD frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
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
        frm_MainLD frm_main;
        List<LDRun> list_ldrun = new List<LDRun>();
        List<string> list_ld = new List<string>();
        LDController ld = new LDController();
        //  runLDs formLD = new runLDs();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void btn_config_Click(object sender, EventArgs e)
        {
            frm_SettingAddFriend frm = new frm_SettingAddFriend();
            frm.ShowDialog();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ConfigFormAddFriendContact configadd = new ConfigFormAddFriendContact();
            configadd.delaycontact = (int)numDelayContact.Value;
            configadd.delayaddfriend = (int)numDelay.Value;
            configadd.phone = txtNumber.Text.Trim();
            configadd.numadd = (int)numAddFriend.Value;
            File.WriteAllText(Application.StartupPath + "\\Config\\AddfriendContact.data", JsonConvert.SerializeObject(configadd));

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
        private void ClearMessage()
        {
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                if ((bool)dgvUser.Rows[i].Cells[0].Value)
                    dgvUser.Rows[i].Cells["Message"].Value = "";

                
               

            }
        }
        private void method_StopAddFriend()
        {
            pibStatus.Visible = false;
            // formLD.Close();
            stop = true;
            if (thread_1 != null)
                thread_1.Abort();
        }
        private bool setupCauHinh()
        {
            try
            {
                string path = String.Format("{0}\\Config\\configadd.data", Application.StartupPath);
                SettingTool.configadd = new ConfigAdd();
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    SettingTool.configadd = JsonConvert.DeserializeObject<ConfigAdd>(json);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
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
                    if (SettingTool.configadd.chkCookie == false)
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
                    else
                    {
                        if (method_CheckLiveCookies(SettingTool.configadd.cookie))
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
                        else
                        {
                            MessageBox.Show("Vui lòng điền cookie live trước khi chạy add friend", "Thông Báo");
                            pibStatus.Visible = false;
                            return;
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn cấu hình trước khi chạy tương tác", "Thông báo");
            }
        }
        public bool method_CheckLiveCookies(string cookies)
        {
            try
            {
                var client = new RestClient("https://www.facebook.com/ajax/typeahead/search/facebar/bootstrap/?filter[0]=app&context=facebar&__a=1");
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Windows NT 6.3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        if (c[0] != "" && c[1] != "")
                            request.AddCookie(c[0].Trim(), c[1].Trim());
                    }
                    catch
                    { }
                }
                IRestResponse response = client.Execute(request);
                string html = response.Content.Replace("\"", "");

                if (html.Contains("error:1357001"))
                {
                    return false;

                }
                else
                {
                    return method_getDtsg(cookies);
                }

            }
            catch
            { }
            return false;
        }
        public bool method_getDtsg(string cookies)
        {
            try
            {
                var client = new RestClient("https://m.facebook.com/ajax/dtsg/?__ajax__=true");
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Windows NT 6.3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        if (c[0] != "" && c[1] != "")
                            request.AddCookie(c[0].Trim(), c[1].Trim());
                    }
                    catch
                    { }
                }
                IRestResponse response = client.Execute(request);
                string html = response.Content.Replace("for (;;);", "");

                JObject obj = JObject.Parse(html);
                SettingTool.dtsg = obj["payload"]["token"].ToString();
                return true;

            }
            catch
            { }
            return false;
        }
        xProxyController xcontroller = new xProxyController();
        private void runTuongTacWaitAny()
        {
            //List<PositionLD> lsPosition = new List<PositionLD>();
            tokenSource = new CancellationTokenSource();
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
              //  method_log("Đổi ip Type : " + SettingTool.configld.typeip);
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
                        method_log("Bắt đầu đổi ip bằng xProxy");
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
                object synDevice = new object();
                int i = 0;
                while (list_ld.Count > 0 & TaskController.checkAvailableTask(list_task))
                {
                    string proxy = "";
                    String ldid = "";
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
                                if ( !string.IsNullOrEmpty(proxy) )
                                {
                                    method_log("Đã lấy IP "+proxy);
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
                                }
                                else
                                {
                                    Thread.Sleep(3000);
                                    xcontroller.reset = "One";
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
                        Task.WaitAll(list_task);
                        method_StopAddFriend();

                    }
                }

            }
        }
        private void runTuongTac()
        {
        Lb_quayvong:
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

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

                object synDevice = new object();
                Task[] tasks = new Task[numthread];
                int i = 0;
                for (int p = 0; p < numthread; p++)
                {
                    int t = p;
                    tasks[t] = Task.Factory.StartNew(() =>
                    {
                        String ldid = "";
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
                    method_log("Đang xử lý dừng kết bạn");
                }

                if (list_ld.Count > 0 && stop == false)
                {
                    goto Lb_quayvong;
                }
                else
                {
                    method_StopAddFriend();
                }
            }

        }
        private void method_Start(string ldID, List<DataGridViewRow> list_acc, string proxy, CancellationToken token)
        {

            method_log("Open LDPlayer Id: " + ldID);
            userLD u = frm_main.checkExits(ldID);
            frm_main.addLDToPanel(u);
            u.setStatus(ldID, "Open Ldplayer: " + ldID);
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
            try
            {
                //#region doi ip sau khi mo ld thanh cong
                //if (SettingTool.configld.sock5)
                //{
                //    if (ld.checkApp(ldID, "org.proxydroid") == false)
                //    {
                //        string path = Application.StartupPath + "\\app\\proxydroid.apk";
                //        if (File.Exists(path))
                //        {
                //            u.setStatus(ldID, "Install app droid proxy...");
                //            ld.installApp(ldID, path);
                //            Thread.Sleep(3000);
                //        }
                //    }
                //}
                //if (string.IsNullOrEmpty(proxy) == false)
                //{
                //    u.setDevice(ldID, proxy);
                //    u.setStatus(ldID, "Change proxy : " + proxy);
                //    if (SettingTool.configld.sock5 && SettingTool.configld.typeip == 7)
                //    {
                //        ld.killApp(ldID, "org.proxydroid");
                //        ld.runApp(ldID, "org.proxydroid");
                //        Thread.Sleep(3000);
                //        SettingTool.configld.proxytype = "socks5";
                //        ld.setProxyAuthentica_proxydroid(ldID, proxy, token);
                //        string yourip = ld.checkIPSock5(proxy);
                //        u.setDevice(ldID, proxy + " - " + yourip);

                //    }
                //    else
                //    {
                //        changeIpHelper.changeProxyAdb(ldID, proxy);
                //        //check ip
                //        string yourip = ld.checkIP(proxy);
                //        u.setDevice(ldID, proxy + " - " + yourip);
                //    }

                //}
                //changeIpHelper.connectAfterOpen(u, richLogs, ldID, token);
                //#endregion

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

                if (tabControl1.SelectedIndex == 1)
                {
                    #region danh ba
                    string cmd = string.Format(" shell pm clear com.android.providers.contacts");
                    string output = ld.runAdb(ldID, cmd);

                    string[] strNumber = txtNumber.Lines;
                    List<string> ls = new List<string>();
                    string lsName = "{bạn|em|E|} {hùng|dũng|hoa|nụ|anh|tuấn|hồng|nhung|nguyệt|loan|thành|sỹ|phong|link|trinh|nga|mai|thảo|trang|ly|giang|tiến}";
                    Random rd = new Random();
                    if (strNumber.Count() > 0)
                    {
                        for (int n = 0; n < strNumber.Count(); n++)
                        {
                            ls.Add("BEGIN:VCARD");
                            ls.Add("VERSION:3.0");
                            ls.Add("FN:" + FunctionHelper.method_Spin(lsName));
                            ls.Add("TEL;TYPE=CELL:" + strNumber[n]);
                            ls.Add("END:VCARD");
                        }
                        string nameRandom = string.Format("contact_{0}.vcf", rd.Next(0, 99999).ToString());
                        //  string namefilevcf = string.Format("c:\\test\\contact_{0}.vcf", nameRandom);

                        string namefilevcf = string.Format("c:\\test\\{0}\\pictures\\temp\\{1}", ldID, nameRandom);

                        File.AppendAllLines(namefilevcf, ls);

                        if (SettingTool.configld.versionld == "3.x")
                            cmd = string.Format(" shell mv -i storage/emulated/legacy/pictures/temp/{0} sdcard/", nameRandom);
                        else
                        {
                            cmd = string.Format(" shell mv -i storage/emulated/0/pictures/temp/{0} sdcard/", nameRandom);
                        }

                        output = ld.runAdb(ldID, cmd);

                        cmd = string.Format("shell am start -t text/x-vcard -d file:///storage/emulated/0/{0} -a android.intent.action.VIEW com.android.contacts", nameRandom);
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
                        DetechModel result = ld.RunDetechFunction(ldID, ls_detecth);

                        if (result.status)
                        {
                            ld.ClickOnLeapdroidPosition(ldID, result.point);
                            Thread.Sleep(1000);
                            goto lb_start;
                        }

                        File.Delete(namefilevcf);
                        Thread.Sleep((int)numDelayContact.Value * 1000);
                    }
                    #endregion
                }

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
                            ld.checkOpenFacebookFinish(u, acc.ldid);

                            dr.Cells["Message"].Value = "Login Facebook";
                            u.setStatus(ldID, "Login Facebook ...");
                             
                            bool status = ld.loginFacebookTuongTac(u, acc, token);
                            if (status)
                            {
                                Profile_Controller profile_bll = new Profile_Controller();

                                dr.Cells["Message"].Value = "Login successful";
                                u.setStatus(ldID, "Login successful ...");

                                acc.TrangThai = "Live";
                                string useragent = "";
                                if (chkToken.Checked)
                                {
                                    useragent = ld.getUserAgentLD(acc.ldid);
                                    string access_token = ld.getToken(acc);
                                    if (string.IsNullOrEmpty(access_token) == false)
                                    {
                                        acc.token = access_token;
                                    }

                                }
                                if (stop)
                                {
                                    goto Lb_Finish;
                                }
                                if (tabControl1.SelectedIndex == 1)
                                {
                                    //add friend by numberfone
                                    #region addFriendNumberfone
                                    {

                                        dr.Cells["Message"].Value = "Add Friend by Contact";
                                        u.setStatus(ldID, "Add Friend by Contact ...");
                                        ld.viewAddFriendbyContact(dr, ldID, acc.app, (int)numAddFriend.Value, (int)numDelay.Value, token);
                                    }
                                    # endregion:
                                }
                                else
                                //bat dau addfriend by UID

                                #region addfriend
                                {
                                    StringBuilder list_history = new StringBuilder();
                                    int loiuid = 0;

                                    int int_total = SettingTool.configadd.maxaddfriend;
                                    int int_maxfriend = SettingTool.configadd.maxfriend;

                                    int int_maxerro = SettingTool.configadd.maxerror;
                                    int int_loilientiep = 0;
                                    int int_dem = Convert.ToInt32(dr.Cells["clSuccess"].Value.ToString());
                                    int int_demthanhcong = 0;

                                    if (SettingTool.configld.language == "English")
                                    {
                                        dr.Cells["Message"].Value = "Running add friend";
                                        u.setStatus(ldID, "Running add friend ...");
                                    }
                                    else
                                    {
                                        dr.Cells["Message"].Value = "Đang chạy kết bạn";
                                        u.setStatus(ldID, "Đang chạy kết bạn ...");
                                    }


                                    int int_tongloi = 0;
                                    string path = String.Format("{0}\\logs\\{1}_add.txt", Application.StartupPath, acc.id);
                                    string historyadd = "";
                                    try
                                    {
                                        historyadd = File.ReadAllText(path);
                                    }
                                    catch { }
                                    bool locnickao = false;
                                    if (SettingTool.configadd.locnickao)
                                        locnickao = true;
                                    else
                                        locnickao = false;

                                    string locgioitinh = null;
                                    if (SettingTool.configadd.locgioitinh)
                                    {
                                        locgioitinh = SettingTool.configadd.gioitinh;
                                    }
                                    else
                                    {
                                        locgioitinh = null;
                                    }
                                    string locthanhpho = null;
                                    if (SettingTool.configadd.loclocation)
                                    {
                                        locthanhpho = SettingTool.configadd.location;
                                    }
                                    else
                                    {
                                        locthanhpho = null;
                                    }
                                    bool locfriend = false;
                                    if (SettingTool.configadd.frienduidmin == true || SettingTool.configadd.frienduidmax == true)
                                    {
                                        locfriend = true;
                                    }
                                    else
                                        locfriend = false;

                                    List<string> list_uid = new List<string>();
                                    if (string.IsNullOrEmpty(acc.pathUID))
                                    {
                                        dr.Cells[0].Value = false;
                                        goto Lb_FinishAcc;
                                    }
                                    list_uid = File.ReadAllLines(acc.pathUID).ToList().Distinct().ToList();
                                    dr.Cells["clTotal"].Value = list_uid.Count;
                                    ld.check_Facebook_has_stopped(u,acc.ldid, acc, token);
                                Lb_Start:
                                   
                                if (token.IsCancellationRequested)
                                    goto Lb_FinishAcc;

                                    if (stop)
                                        goto Lb_Finish;
                                    if (list_uid.Count <= 0)
                                    {
                                        dr.Cells[0].Value = false;
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "No aviable UID";
                                            method_log("No aviable UID");
                                        }

                                        else
                                        {
                                            dr.Cells["Message"].Value = "Đã hết UID";
                                            method_log("Đã hết UID");
                                        }
                                        goto Lb_FinishAcc;
                                    }
                                    string uid = null;
                                    lock (syncObjUID)
                                    {
                                        uid = list_uid[0];
                                        list_uid.Remove(uid);
                                    }
                                    if (historyadd.Contains(uid))
                                    {
                                        goto Lb_Start;
                                    }
                                    try
                                    {
                                        if (SettingTool.configld.language == "English")
                                            u.setStatus(ldID, "Running add friend " + uid);
                                        else
                                            u.setStatus(ldID, "Đang chạy kết bạn " + uid);

                                        bool checkinfo = false;
                                        if (SettingTool.configadd.chkCookie)
                                        {
                                            checkinfo = CheckInfo1uid(SettingTool.configadd.cookie, SettingTool.dtsg, uid, locnickao, locgioitinh, locthanhpho);
                                        }
                                        else
                                        {
                                            checkinfo = true;
                                        }
                                        if (checkinfo)
                                        {
                                            int statusadd = 0;
                                            if (chkToken.Checked)
                                            {
                                                List<string> list_uidfriend = new List<string>();
                                                list_uidfriend.Add(uid);
                                                CareFacebookResult kq = profile_bll.addFriendtoken(acc, list_uidfriend, useragent, proxy);
                                                if (kq.status)
                                                {
                                                    statusadd = 1;
                                                }
                                                else
                                                {
                                                    statusadd = 0;
                                                }
                                            }
                                            else
                                            {
                                                statusadd = ld.addFriendUID(acc, uid, token);
                                            }
                                            if (statusadd == 1)
                                            {
                                                list_history.AppendLine(uid);
                                                int_loilientiep = 0;
                                                int_dem++;
                                                int_demthanhcong++;
                                                dr.Cells["clSuccess"].Value = int_dem;


                                                if (SettingTool.configld.language == "English")
                                                    method_log(String.Format(" {2}/{3} - Account {0}  add friend {1} successful", acc.email, uid, int_dem, list_uid.Count));
                                                else

                                                    method_log(String.Format(" {2}/{3} - Tài Khoan {0} kết bạn với {1} thành công", acc.email, uid, int_dem, list_uid.Count));

                                                if(SettingTool.configadd.likeavatar)
                                                {
                                                    ld.likeAvatar(acc, uid, token);
                                                }
                                                int int_delay = rd.Next(SettingTool.configadd.delaymin, SettingTool.configadd.delaymax);
                                                method_log(String.Format("Delay:{0}s", int_delay));
                                                Thread.Sleep(int_delay * 1000);


                                            }
                                            else
                                            {
                                                if (statusadd == 0)
                                                {
                                                    int int_delay = rd.Next(SettingTool.configadd.delaymin, SettingTool.configadd.delaymax);
                                                    method_log(String.Format("Delay:{0}s", int_delay));
                                                    Thread.Sleep(int_delay * 1000);
                                                }
                                                int_loilientiep++;
                                                int_tongloi++;
                                                dr.Cells["clFail"].Value = int_tongloi;
                                                if (int_loilientiep >= SettingTool.configadd.maxerror)
                                                {
                                                    if (SettingTool.configld.language == "English")
                                                        method_log(String.Format("Stoped account {0} has fail continuous {1} times", acc.email, int_loilientiep));
                                                    else
                                                        method_log(String.Format("Dừng Tài Khoan {0} đã Lỗi liên tiếp {1}lần", acc.email, int_loilientiep));

                                                    dr.Cells[0].Value = false;
                                                    goto Lb_FinishAcc;
                                                }
                                                if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                                {
                                                    dr.Cells["Message"].Value = "Lỗi đăng nhập Facebook";
                                                    u.setStatus(ldID, "Lỗi đăng nhập Facebook...");
                                                    goto Lb_FinishAcc;
                                                }
                                            }

                                            if (int_dem >= SettingTool.configadd.maxaddfriend)
                                            {
                                                dr.Cells[0].Value = false;
                                                goto Lb_FinishAcc;
                                            }
                                            if (int_demthanhcong >= SettingTool.configadd.maxfriend)
                                                goto Lb_FinishAcc;
                                            goto Lb_Start;
                                        }
                                        else
                                        {
                                            if (stop == false)
                                                goto Lb_Start;
                                            else
                                            {
                                                goto Lb_Finish;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //if (ex.ToString().Contains("error_msg"))
                                        //{
                                        goto Lb_FinishAcc;
                                        //  }
                                    }
                                Lb_FinishAcc:
                                  
                                    method_log("Finish" + acc.id);
                                    File.AppendAllText(path, list_history.ToString());
                                    if (SettingTool.configadd.DeleteUID)
                                    {
                                        if (!string.IsNullOrEmpty(acc.pathUID))
                                            File.WriteAllLines(acc.pathUID, list_uid);
                                    }
                                    dr.Cells["Message"].Value = "Finish";
                                }
                                #endregion
                                if (SettingTool.configld.language == "English")
                                {
                                    dr.Cells["Message"].Value = "Complete add friend";
                                    u.setStatus(ldID, "Complete add friend...");
                                }
                                else
                                {
                                    dr.Cells["Message"].Value = "Hoàn thành";
                                    u.setStatus(ldID, "Hoàn thành add friend...");
                                }



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
                                //        method_log("Đang thay đổi IP");
                                //        ld.changeIp();
                                //        int i = 10;
                                //        while (i > 0)
                                //        {
                                //            if (lbAddress.Text != ld.checkIP())
                                //            {
                                //                lbAddress.Text = ld.checkIP();
                                //                method_log("Đã thay đổi IP: " + lbAddress.Text);
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
                                {
                                    if (SettingTool.configld.language == "English")
                                        dr.Cells["Message"].Value = "Login fail";
                                    else
                                        dr.Cells["Message"].Value = "Đăng nhập thất bại";
                                }

                                // dr.Cells["Status"].Value = "Die";
                                acc.TrangThai = "Die";
                                dr.Cells[0].Value = false;
                            }

                        }
                    }
                    catch
                    { }
                    if (list_acc.Count > 0 && stop == false)
                    {
                        goto Lb_Acc;
                    }
                    else
                    {
                        if (SettingTool.configadd.quayvong && tabControl1.SelectedIndex == 0)
                        {
                            list_acc = new List<DataGridViewRow>();
                            foreach (DataGridViewRow dr in dgvUser.Rows)
                            {
                                if ((bool)dr.Cells[0].Value)
                                {
                                    Account acc = (Account)dr.Tag;
                                    if (acc.ldid == ldID)
                                    {
                                        list_acc.Add(dr);
                                    }

                                }

                            }
                            goto Lb_Acc;
                        }
                    }
                }
            }
            catch
            { }
        Lb_Finish:
            
            method_log("Dừng : " + ldID);
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
        public bool CheckInfo1uid(string cookies, string dtsg, string uid, bool locnickao, string locgioitinh, string locthanhpho)
        {
            List<string> list_kq = new List<string>();
            try
            {
                CareFacebookModel model = new CareFacebookModel();
                model.cookies = cookies;
                model.dtsg = dtsg;
                model.data = uid;


                string data = FunctionHelper.mahoa(JsonConvert.SerializeObject(model), "9d31b084dd0d981de479");


                var client = new RestClient("http://apiaddfriend.ninjateam.vn/api/NinjaAddFriend/checkInfoUID");
                var request = new RestRequest(Method.POST);

                request.AddHeader("Accept", "application/x-www-form-urlencoded");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("type", "2");
                request.AddParameter("appid", "22");
                request.AddParameter("data", data);

                IRestResponse response = client.Execute(request);
                string html = response.Content.Replace("\"", "");

                data = FunctionHelper.giaima(html, "9d31b084dd0d981de479");

                CareFacebookResult re = JsonConvert.DeserializeObject<CareFacebookResult>(data);
                if (re.status)
                {

                    int timespan = FunctionHelper.ConvertTimeSpan();
                    JObject item = JObject.Parse(re.data);

                    try
                    {
                        string friendship = item[uid]["friendship_status"].ToString();
                        //loc nick ao
                        //   string uid = item.Value["id"].ToString();
                        if (friendship != "cannot_request")
                        {
                            bool ao = true;
                            bool gioitinh = true;
                            bool thanhpho = true;

                            if (locnickao)
                            {
                                //kiem tra
                                int friendcount = Convert.ToInt32(item[uid]["friends"]["count"].ToString());
                                if (friendcount > 3000)
                                {
                                    ao = false;
                                }
                                else
                                {

                                    if (friendcount > 0 && friendcount <= 10)
                                    {
                                        ao = true;
                                    }
                                    else
                                    {
                                        //kiem tra avatar
                                        bool avatar = Convert.ToBoolean(item[uid]["profile_picture_is_silhouette"].ToString());
                                        if (avatar == false)
                                        {
                                            //co avatar
                                            int namtaonick = Convert.ToInt32(item[uid]["join_time"].ToString());
                                            int days = (timespan - namtaonick) / 86400;
                                            if (days <= 120)
                                            {
                                                //kiem tra gioi tinh
                                                string gioitinhnick = item[uid]["gender"].ToString();
                                                if (gioitinhnick == "null")
                                                {
                                                    ao = false;
                                                }
                                                else
                                                {
                                                    ao = true;
                                                }
                                            }
                                            else
                                            {
                                                //neu lon hon 4 thang
                                                string cover = item[uid]["timeline_cover_photo"].ToString();
                                                if (String.IsNullOrEmpty(cover))
                                                {
                                                    //ko co cover
                                                    ao = true;
                                                }
                                                else
                                                {
                                                    //co cover
                                                    //kiem tra anh upload
                                                    int uploadcount = Convert.ToInt32(item[uid]["uploaded_mediaset"]["media"]["count"].ToString());
                                                    if (uploadcount < 2)
                                                    {
                                                        ao = true;
                                                    }
                                                    else
                                                    {
                                                        ao = false;
                                                    }

                                                }
                                            }
                                        }
                                        else
                                        {
                                            //ko co avatar
                                            string cover = item[uid]["timeline_cover_photo"].ToString();
                                            if (String.IsNullOrEmpty(cover))
                                            {
                                                //ko co cover
                                                ao = true;
                                            }
                                            else
                                            {
                                                //co cover
                                                //kiem tra anh upload
                                                int uploadcount = Convert.ToInt32(item[uid]["uploaded_mediaset"]["media"]["count"].ToString());
                                                if (uploadcount < 2)
                                                {
                                                    ao = true;
                                                }
                                                else
                                                {
                                                    ao = false;
                                                }

                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {
                                ao = false;
                            }
                            if (locgioitinh != null)
                            {
                                string gioitinhnick = item[uid]["gender"].ToString().ToLower();
                                if (locgioitinh == gioitinhnick)
                                {
                                    gioitinh = true;
                                }
                                else
                                {
                                    gioitinh = false;
                                }
                            }
                            else
                            {
                                gioitinh = true;
                            }

                            //loc thanh pho
                            if (locthanhpho != null)
                            {
                                locthanhpho = locthanhpho.ToLower();
                                thanhpho = false;
                                string thanhphonick = item[uid]["current_city"].ToString();
                                if (thanhphonick == "null")
                                {
                                    thanhpho = false;
                                }
                                else
                                {
                                    if (locthanhpho.Contains(","))
                                    {
                                        string[] arrthanhpho = locthanhpho.Split(',');
                                        foreach (string str in arrthanhpho)
                                        {
                                            if (thanhphonick.Contains(str))
                                            {
                                                thanhpho = true;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (thanhphonick.Contains(locthanhpho))
                                        {
                                            thanhpho = true;

                                        }
                                    }

                                }
                            }
                            else
                            {
                                thanhpho = true;
                            }
                            //loc friend

                            if (ao == false && gioitinh == true && thanhpho == true)
                            {
                                return true;
                            }
                            else
                            {
                                if (SettingTool.configld.language == "English")
                                    method_log(String.Format("Skip UID {0} dissatisfied condition", uid));
                                else
                                    method_log(String.Format("Bỏ qua UID {0} không thõa mãn điều kiện", uid));
                                return false;
                            }
                        }
                    }
                    catch
                    {
                    }


                }

            }
            catch
            {
            }
            return false;
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
                cell5.Value = acc.name;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.ldid;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7a = new DataGridViewTextBoxCell();
                cell7a.Value = acc.pathUID;
                dataGridViewRow.Cells.Add(cell7a);

                DataGridViewTextBoxCell cell71 = new DataGridViewTextBoxCell();
                cell71.Value = 0;
                dataGridViewRow.Cells.Add(cell71);

                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                cell8.Value = 0;
                dataGridViewRow.Cells.Add(cell8);
                dataGridViewRow.Tag = acc;

                DataGridViewTextBoxCell cell81 = new DataGridViewTextBoxCell();
                cell81.Value = 0;
                dataGridViewRow.Cells.Add(cell81);
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            stop = true;
            tokenSource.Cancel();
            method_StopAddFriend();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            dialog.Filter = "File txt (*.txt)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string pathpost = dialog.FileName;
                NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                foreach (DataGridViewRow dr in dgvUser.SelectedRows)
                {
                    dr.Cells["pathUID"].Value = pathpost;
                    Account acc = (Account)dr.Tag;
                    acc.pathUID = pathpost;
                    nguoidung_bll.updatePathUID(acc);

                }
            }
        }

        private void frm_AddFriendLD_Load(object sender, EventArgs e)
        {
            method_LoadAccount();

            string path = String.Format("{0}\\Config\\AddfriendContact.data", Application.StartupPath);
            if (File.Exists(path))
            {
                try
                {
                    ConfigFormAddFriendContact config = new ConfigFormAddFriendContact();
                    using (StreamReader r = new StreamReader(path))
                    {
                        string json = r.ReadToEnd();
                        config = JsonConvert.DeserializeObject<ConfigFormAddFriendContact>(json);
                    }
                    numDelayContact.Value = config.delaycontact;
                    numDelay.Value = config.delayaddfriend;
                    numAddFriend.Value = config.numadd;
                    txtNumber.Text = config.phone;
                }
                catch
                { }

            }
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }

        }
        string currentpath = "";
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            dialog.Filter = "File txt (*.txt)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                setupCauHinh();
                currentpath = dialog.FileName;
                Thread thread_1 = new Thread(new ThreadStart(this.method_StartChia));
                thread_1.Start();
            }
        }
        private void method_StartChia()
        {
            List<string> list_uid = new List<string>();
            list_uid = File.ReadAllLines(currentpath).Distinct().ToList();

            int max = dgvUser.SelectedRows.Count;
            List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                list_dr.Add(dr);
            }
            if (list_uid.Count >= max)
            {
               
                string pathsave = String.Format("{0}\\uid", Application.StartupPath);
                if (Directory.Exists(pathsave) == false)
                {
                    Directory.CreateDirectory(pathsave);
                }
                List<string> list_kq = new List<string>();
                
                int totaluid = list_uid.Count;
                int total = totaluid / max;
                NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                int index = 0;
                foreach (string uid in list_uid)
                {
                    index++;
                    list_kq.Add(uid);

                    if (list_kq.Count == total || index >= totaluid)
                    {
                      
                      
                        //save
                        if (list_dr.Count > 0)
                        {
                            DataGridViewRow dr = list_dr[0];
                            list_dr.Remove(dr);
                            Account acc = (Account)dr.Tag;
                            string pathuid = String.Format("{0}\\{1}.txt", pathsave, acc.Id_account);

                            File.WriteAllLines(pathuid, list_kq);

                            acc.pathUID = pathuid;
                            nguoidung_bll.updatePathUID(acc);
                            list_kq = new List<string>();
                            dr.Cells["pathUID"].Value = pathuid;
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    
                }

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

        private void richLogs_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=8xkFdHuTSfw");
        }
        private void setupLanguage()
        {
            this.Text = "Add friend...";
            chkToken.Text = "Add friend by token";
            tabControl1.TabPages[0].Text = "Add friend by UID";
            tabControl1.TabPages[1].Text = "Add friend by number phone in contact";
            btnOpenFile.Text = "Choose File UID (*.txt)";
            bunifuFlatButton1.Text = "Split the file UID for every account";
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
    }
}
