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
    public partial class frm_Makemoney_PRO : Form
    {
        public frm_Makemoney_PRO(List<Account> list_acc, frm_MainLD_PRO frm_main)
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

        LDController ld = new LDController();
        List<string> list_uid = new List<string>();
        Random rdom = new Random();
        object synUID = new object();

        frm_MainLD_PRO frm_main;
        List<string> list_group;
        xProxyController xcontroller = new xProxyController();
        private void frm_TuongTacLD_Load(object sender, EventArgs e)
        {
            try
            {
                configmakemoney cfmoney = new configmakemoney();
                string path = String.Format("{0}\\Config\\Configmakemoney.data", Application.StartupPath);
                if (File.Exists(path))
                {
                    try
                    {
                        using (StreamReader r = new StreamReader(path))
                        {
                            string json = r.ReadToEnd();
                            cfmoney = JsonConvert.DeserializeObject<configmakemoney>(json);
                        }
                        txtPass.Text = cfmoney.pass;
                        txtUser.Text = cfmoney.user;
                        numDelayMin.Value = int.Parse(cfmoney.delaymin);
                        numDelayMax.Value = int.Parse(cfmoney.delaymax);

                        numJobMin.Value = int.Parse(cfmoney.jobmin);
                        numJobMax.Value = int.Parse(cfmoney.jobmax);

                        numloopmin.Value = int.Parse(cfmoney.numloopmin);
                        numloopmax.Value = int.Parse(cfmoney.numloopmax);

                        numAmountmin.Value = int.Parse(cfmoney.numAmountmin);
                        numAmountmax.Value = int.Parse(cfmoney.numAmountmax);
                    }
                    catch { }
                }




                method_LoadAccount();
                method_Config();

                if (SettingTool.configld.language == "English")
                {

                    this.Text = "Run reaction";
                }
            }
            catch
            { }
        }
        private void changeColor(DataGridViewRow dataGridViewRow_0, Color color_0)
        {
            try
            {
                Class34 class2 = new Class34
                {
                    dataGridViewRow_0 = dataGridViewRow_0,
                    color_0 = color_0
                };
                this.Invoke(new MethodInvoker(class2.method_0));
            }
            catch
            {

            }

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
            try
            {



                list_tuongtac = new List<int>();
                if (tuongtac.luotnewfeed)
                {
                    list_tuongtac.Add(1);
                }
                if (tuongtac.likenewfeed)
                {
                    list_tuongtac.Add(2);
                }
                if (tuongtac.commentnewfeed)
                {
                    list_tuongtac.Add(3);
                }
                if (tuongtac.sharevideo)
                {
                    list_tuongtac.Add(4);
                }
                if (tuongtac.likefanpage)
                {
                    list_tuongtac.Add(5);
                }
                if (tuongtac.likepostfanpage)
                {
                    list_tuongtac.Add(6);
                }
                if (tuongtac.commentpostfanpage)
                {
                    list_tuongtac.Add(7);
                }
                if (tuongtac.addfriend)
                {
                    list_tuongtac.Add(8);
                }
                if (tuongtac.acceptfriend)
                {
                    list_tuongtac.Add(9);
                }
                if (tuongtac.joingroupkeyword)
                {
                    list_tuongtac.Add(10);
                }
                if (tuongtac.likepostgroup)
                {
                    list_tuongtac.Add(11);
                }
                if (tuongtac.commentpostgroup)
                {
                    list_tuongtac.Add(12);
                }
                if (tuongtac.sharepost)
                {
                    list_tuongtac.Add(13);
                }
                if (tuongtac.readnoti)
                {
                    list_tuongtac.Add(14);
                }

                if (tuongtac.seeding)
                {
                    list_tuongtac.Add(15);
                }

                if (tuongtac.chkUID)
                {
                    list_tuongtac.Add(18);
                    if (!File.Exists(tuongtac.strPath))
                    {
                        MessageBox.Show("Hãy kiểm tra lại nội dung file Group ID  để join");
                        return false;

                    }
                    list_uid = File.ReadAllLines(tuongtac.strPath).ToList().Distinct().ToList();
                    if (list_uid.Count == 0)
                        sendLogs("Hãy thêm UID group vào file: " + tuongtac.strPath);

                }

                if (tuongtac.addfriendNewfeed)
                {
                    list_tuongtac.Add(19);
                }

                if (tuongtac.chkAddFriendUID)
                {
                    list_tuongtac.Add(20);

                }
                if (tuongtac.likeCommentGroupId)
                {
                    list_tuongtac.Add(21);
                }

                if (tuongtac.chkMessenger)
                {
                    list_tuongtac.Add(22);

                }

                if (tuongtac.searchInfo)
                {
                    list_tuongtac.Add(23);

                }
                if (tuongtac.has_newfeed_watch)
                {
                    list_tuongtac.Add(24);//lướt newfeed watch
                }
                if (tuongtac.has_like_newfeed_watch)
                {
                    list_tuongtac.Add(25);//lướt newfeed watch
                }
                if (tuongtac.has_newfeed_marketplace)
                {
                    list_tuongtac.Add(26);//lướt newfeed market place
                }
                if (tuongtac.chkscrollgroup)
                {
                    list_tuongtac.Add(27);

                }
                if (tuongtac.has_like_newfeed_group)
                {
                    list_tuongtac.Add(28);

                }

                if (tuongtac.cancelrequest)
                {
                    list_tuongtac.Add(29);
                }

                if (tuongtac.removegroup)
                {
                    list_tuongtac.Add(30);
                }
                if (tuongtac.postgroup)
                {
                    list_tuongtac.Add(31);
                }
                if (tuongtac.postprofile)
                {
                    list_tuongtac.Add(32);
                }
                if (tuongtac.khangspam)
                {
                    list_tuongtac.Add(33);
                }

                if (tuongtac.chkCanceldFriendUID)
                {
                    list_tuongtac.Add(34);
                }
                if (tuongtac.chkCanceldFriendRandom)
                {
                    list_tuongtac.Add(35);
                }

                if (tuongtac.luotStory)
                {
                    list_tuongtac.Add(36);
                }

                if (tuongtac.has_addfriendgroup)
                {
                    list_tuongtac.Add(37);
                }
                if (tuongtac.chklikestory)
                {
                    list_tuongtac.Add(38);
                }
                if (tuongtac.chkcommentstory)
                {
                    list_tuongtac.Add(39);
                }
                if (tuongtac.getBirthday)
                {
                    list_tuongtac.Add(40);
                }

                if (tuongtac.chkjoingruoupsuggest)
                {
                    list_tuongtac.Add(41);
                }
                if (tuongtac.commentnewfeedgroup)
                {
                    list_tuongtac.Add(42);
                }
                if (list_tuongtac.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {

            }
            return false;
        }
        private void method_Config()
        {

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

                DataGridViewTextBoxCell cell6a = new DataGridViewTextBoxCell();
                cell6a.Value = acc.friend_count;
                dataGridViewRow.Cells.Add(cell6a);

                DataGridViewTextBoxCell cell6b = new DataGridViewTextBoxCell();
                cell6b.Value = 0;
                dataGridViewRow.Cells.Add(cell6b);

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
        string tokenmoney = "";
        private void btnRun_Click(object sender, EventArgs e)
        {
            string pathcoin = string.Format("{0}\\totalcoin.txt", Application.StartupPath + "\\Config");
            if (File.Exists(pathcoin))
            {
                try
                {
                    if (!string.IsNullOrEmpty(File.ReadAllText(pathcoin)))
                        lbLuyke.Text = string.Format("{0:N0}", int.Parse(File.ReadAllText(pathcoin)));
                }
                catch { }
               
            }
            else
            {
                lbLuyke.Text = "0";
            }
            lbltotalcoin.Text = "0";
            configmakemoney cfmoney = new configmakemoney();
            cfmoney.user = txtUser.Text;
            cfmoney.pass = txtPass.Text;
            cfmoney.delaymin = numDelayMin.Value.ToString();
            cfmoney.delaymax = numDelayMax.Value.ToString();

            cfmoney.jobmin = numJobMin.Value.ToString();
            cfmoney.jobmax = numJobMax.Value.ToString();

            cfmoney.numloopmin = numloopmin.Value.ToString();
            cfmoney.numloopmax = numloopmax.Value.ToString();

            cfmoney.numAmountmin = numAmountmin.Value.ToString();
            cfmoney.numAmountmax = numAmountmax.Value.ToString();

            string path = String.Format("{0}\\Config\\Configmakemoney.data", Application.StartupPath);
            File.WriteAllText(path, JsonConvert.SerializeObject(cfmoney));

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
        List<fbmoney> lsfbmoney = new List<fbmoney>();
        List<string> ls_uidmmoney = new List<string>();

        private void startTuongTac()
        {

            //chon cau hinh
            try
            {
                var client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/login");
                var request = new RestRequest(Method.POST);
                string user = txtUser.Text.Trim();

                request.AddParameter("username", user);
                request.AddParameter("password", txtPass.Text);
                IRestResponse response = client.Execute(request);
                var data = response.Content;
                JObject obj = JObject.Parse(data);
                tokenmoney = obj["data"]["token"].ToString();

                if (user != "ninjagroup")
                {
                    client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/facebook-account/cruPhonefarm");
                    request = new RestRequest(Method.POST);

                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", "bearer " + tokenmoney);
                    request.AddHeader("accept", "application/json");
                    request.AddParameter("ref_code", "AM224536");
                    request.AddParameter("amai_key", txt_amaikey.Text);
                    request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                    response = client.Execute(request);
                    data = response.Content;
                    obj = JObject.Parse(data);
                    if (obj["success"].ToString() != "True")
                    {
                        MessageBox.Show("Mã giới thiệu không trùng khớp!");
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin đăng nhập!");
                return;
            }
            pibStatus.Visible = true;
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
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void runTuongTacWaitAny()
        {
            try
            {
                setupLDGoc();
                int lap = 0;
                Random rd = new Random();
                int nummaxtuongtac = rd.Next((int)numAmountmin.Value, (int)numAmountmax.Value);

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
                        DataGridViewRow drcheckjob = new DataGridViewRow();
                        drcheckjob = list_dr[0];
                        drcheckjob.Cells["Message"].Value = "Đang lấy job lần đầu tiên";
                        Account acc = (Account)drcheckjob.Tag;
                        fbmoney fb_money = getfbmoney(acc);
                        List<job> ls_Job = new List<job>();
                        ls_Job = getListJob(fb_money.id);
                        if (ls_Job.Count > 0)
                        {
                            drcheckjob.Cells[0].Value = false;
                            drcheckjob.Cells["Message"].Value = "";
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

                                                        method_Start(ldid, dr, proxy, tokenmoney, ls_Job, token);
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
                                                    method_Start(ldid, dr, proxy, tokenmoney, ls_Job, token);

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
                        else
                        {
                            drcheckjob.Cells["Message"].Value = "Chưa có job mới";
                            list_dr.Remove(drcheckjob);
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

                        if (chklooprun.Checked)
                        {
                            lap++;
                            if (lap <= nummaxtuongtac)
                            {
                                int delay = rd.Next((int)numloopmin.Value, (int)numloopmax.Value);


                                if (SettingTool.configld.language == "English")
                                    sendLogs(String.Format("Please wait {0} minutes to continue interacting", delay));
                                else
                                    sendLogs(String.Format("Vui lòng đợi {0} phút để tiếp tục tương tác", delay));

                                Thread.Sleep(delay * 60000);
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
            try
            {
                setupLDGoc();
                int lap = 0;

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
                                        List<job> lsJob = new List<job>();
                                        method_Start(ldid, dr, proxy, tokenmoney, lsJob, token);
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
        private List<job> getListJob(string fb_money_id)
        {
            List<job> ls_Job = new List<job>();
            try
            {
                int loop = 0;
                while (loop < 10)
                {
                    var client = new RestClient(string.Format("http://duy-tool.amaiteam.com/farmer/api/v1/facebook-jobs/lPhonefarm?fb_account_id={0}&amai_key=qB8VtVreIaxzNqmEeeqqquB%20&job_type=", fb_money_id));
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("authorization", string.Format("bearer {0}", tokenmoney));
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                    var response = client.Execute(request);
                    var data = response.Content;
                    var obj = JObject.Parse(data);

                    if (obj["success"].ToString() == "True")
                    {
                        foreach (var item in obj["data"])
                        {
                            job j = new job();
                            j.id = item["id"].ToString();
                            j.post_id = item["post_id"].ToString();
                            j.web_post_link = item["web_post_link"].ToString();
                            j.seeding_type = item["seeding_type"].ToString();
                            j.reaction_id = item["reaction_id"].ToString();
                            j.comment_need = item["comment_need"].ToString();
                            j.num_seeding_remain = item["num_seeding_remain"].ToString();
                            j.coin = item["coin"].ToString();
                            j.title = item["title"].ToString();
                            j.owner_id = item["owner_id"].ToString();
                            ls_Job.Add(j);
                        }
                        if (ls_Job.Count > 0)
                            break;
                    }

                    Thread.Sleep(1000);
                    loop++;
                }
                return ls_Job;
            }
            catch
            {

            }
            return ls_Job;
        }
        private fbmoney getfbmoney(Account acc)
        {
            fbmoney fb_money = new fbmoney();
            try
            {

                var client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/facebook-account/daPhonefarm");
                var request = new RestRequest(Method.POST);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "bearer " + tokenmoney);
                request.AddHeader("accept", "application/json");
                request.AddParameter("uid", acc.id);
                request.AddParameter("amai_key", txt_amaikey.Text);
                request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                IRestResponse response = client.Execute(request);
                var data = response.Content;
                JObject obj = JObject.Parse(data);
                if (obj["success"].ToString() != "True")
                {
                    try
                    {
                        string account = "{\n    \"accounts\": [\n      {\n      \"uid\": \"uidfacebook\",\n   \"name\": \"namefacebook\",\n   \"friends\": \"friendfb\",\n  \"profile_pic\": \"\"\n  }\n  ], \n    \"amai_key\" : \"qB8VtVreIaxzNqmEeeqqquB\"\n}";
                        account = account.Replace("uidfacebook", acc.id);
                        account = account.Replace("namefacebook", acc.name);
                        account = account.Replace("friendfb", acc.friend_count);
                        client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/facebook-account/amaPhonefarm");
                        request = new RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("authorization", string.Format("bearer {0}", tokenmoney));
                        request.AddHeader("accept", "application/json");
                        request.AddParameter("application/json", account, ParameterType.RequestBody);
                        response = client.Execute(request);
                        data = response.Content;
                        obj = JObject.Parse(data);


                        client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/facebook-account/daPhonefarm");
                        request = new RestRequest(Method.POST);

                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9kdXktdG9vbC5hbWFpdGVhbS5jb21cL2Zhcm1lclwvYXBpXC92MVwvbG9naW4iLCJpYXQiOjE2MjYwNjE4MjMsImV4cCI6MTYyNjY2NjYyMywibmJmIjoxNjI2MDYxODIzLCJqdGkiOiJpbllZUGxiNldQOGg3RzlQIiwic3ViIjoyNTIwNTIsInBydiI6Ijg3ZTBhZjFlZjlmZDE1ODEyZmRlYzk3MTUzYTE0ZTBiMDQ3NTQ2YWEifQ.NiEXalurbg4t9uX-BhKd7sk0FrPcm_w8hnmP9Um8b40");
                        request.AddHeader("accept", "application/json");
                        request.AddParameter("uid", acc.id);
                        request.AddParameter("amai_key", txt_amaikey.Text);
                        request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                        response = client.Execute(request);
                        data = response.Content;
                        obj = JObject.Parse(data);
                        if (obj["success"].ToString() == "True")
                        {
                            fb_money = JsonConvert.DeserializeObject<fbmoney>(obj["data"].ToString());
                        }
                        //else
                        //{
                        //    dr.Cells["Message"].Value = "Tài khoản chưa được cập nhập hệ thống";
                        //    return;
                        //}

                    }
                    catch
                    {
                    }
                }
                else
                {
                    fb_money = JsonConvert.DeserializeObject<fbmoney>(obj["data"].ToString());
                }
                return fb_money;
            }
            catch
            {

            }

            return fb_money;
        }
        int totalcoin = 0;

        private void method_Start(string ldID, DataGridViewRow dr, string proxy, string tokenmoney, List<job> i_lsJob, CancellationToken token)
        {
            List<job> ls_Job = i_lsJob;
            int coins = 0;
            fbmoney fb_money = new fbmoney();
            try
            {
                Account acc = (Account)dr.Tag;

                fb_money = getfbmoney(acc);

                int successs = 0;


                if (ls_Job.Count == 0)
                {
                    int getjob = 0;
                    while (getjob < numericUpDown1.Value)
                    {
                        ls_Job = getListJob(fb_money.id);
                        if (ls_Job.Count > 0)
                            break;
                        else
                            getjob++;
                    }
                }

                method_log("Open LDPlayer Id: " + ldID);
                changeColor(dr, Color.Yellow);
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

                ld.setKeyboard(ldID);

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
                                sendLogs("Tắt LD do không lấy được ip public proxy: " + proxy);
                                goto Lb_Finish;

                            }
                        }
                        u.setDevice(ldID, acc.id, proxy + " - " + yourip);
                    }
                }

                changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
                #endregion
                int dem = 0;
                string message = "";
                //get id_account running

                int numjobs = rd.Next((int)numJobMin.Value, (int)numJobMax.Value);
                if (token.IsCancellationRequested)
                {
                    goto Lb_Finish;
                }
                try
                {
                    dem++;
                    if (SettingTool.configld.typeip == 4 && dem > 1)
                    {
                        changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);
                    }

                    NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                    changeColor(dr, Color.Yellow);
                    dr.Cells["Message"].Value = "Running";

                    try
                    {
                        ld.killApp(acc.ldid, "com.facebook.katana");
                        Delay(1);
                        ld.restoredatafb(acc.ldid, acc.id);
                        ld.runApp(acc.ldid, "com.facebook.katana");
                        Thread.Sleep(10000);
                        ld.checkOpenFacebookFinish(u, acc.ldid);
                    }
                    catch
                    {
                    }
                    dr.Cells["Message"].Value = "Login Facebook";
                    u.setStatus(ldID, "Login Facebook...");


                    //bool status = ld.checkIsLogin(acc);
                    //if (status)
                    //{
                    //    status = true;
                    //}
                    //else
                    //{
                    bool status = ld.loginFacebookTuongTac(u, acc, token);
                    //ld.loginAvatarLD(acc);
                    // status = ld.loginFacebookLD(acc, token);
                    //}

                    if (status)
                    {
                        u.setStatus(ldID, "Login Facebook Sucess...");
                        dr.Cells["Message"].Value = "Login Facebook Successful";
                        acc.Thongbao = "Login Successful";
                        acc.TrangThai = "Live";
                        dr.Cells["clStatus"].Value = acc.TrangThai;
                        //bat dau tuong tac
                        message = "";
                        int type = 0;
                        string answer = tuongtac.strPathAnswer;

                        string comment = "";
                        if (File.Exists(tuongtac.message))
                        {
                            comment = File.ReadAllText(tuongtac.message);
                        }
                        List<string> list_tuongtac_done = new List<string>();

                        int maxrequesjob = 0;
                        int maxaction = 0;
                        while (successs < numjobs)
                        {
                            ld.activeNewfeed(acc.ldid, "com.facebook.katana");

                            if (!ld.checkIsLogin(acc))
                                ld.loginFacebookTuongTac(u, acc, token);


                            if (ld.checkContentLD(acc.ldid, "can't connect"))
                                break;
                            if (token.IsCancellationRequested)
                                break;

                            if (ls_Job.Count == 0)
                            {
                                if (maxrequesjob < numericUpDown1.Value)
                                {
                                    u.setStatus(ldID, "Đang lấy job lần: " + (maxrequesjob + 1).ToString());
                                    dr.Cells["Message"].Value = "Đang lấy job lần: " + (maxrequesjob + 1).ToString();
                                    ls_Job = getListJob(fb_money.id);
                                }
                                else
                                    break;

                            }
                            if (ls_Job.Count == 0)
                            {
                                if (maxrequesjob > numericUpDown1.Value)
                                    break;
                                maxrequesjob++;
                            }
                            else
                                maxrequesjob = 0;

                            foreach (job jb in ls_Job)
                            {
                                if (token.IsCancellationRequested)
                                    break;

                                #region bat dau tuong tac
                                type = int.Parse(jb.seeding_type);
                                int delay = rdom.Next((int)numDelayMin.Value, (int)numDelayMax.Value);

                                acc.app = "com.facebook.katana";
                                switch (type)
                                {
                                    case 1:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        Thread.Sleep(1000);


                                        if (ld.sharepost2profile(acc.ldid, "com.facebook.katana", " ", token))
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            reportJob_error(fb_money, jb);
                                        }
                                        break;
                                    case 2:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        Thread.Sleep(1000);
                                        if (jb.web_post_link.Contains("video"))
                                        {
                                            ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);

                                            if (ld.seedingLikeVideo(ldID, token))
                                            {
                                                successs++;
                                                coins += int.Parse(jb.coin);
                                                totalcoin += int.Parse(jb.coin);
                                                lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                                dr.Cells["coins"].Value = coins;
                                                Thread.Sleep(1000);
                                                reportJob(fb_money, jb);
                                            }
                                            else
                                            {
                                                reportJob_error(fb_money, jb);
                                            }

                                        }
                                        else
                                        {
                                            if (ld.seedingLike(ldID, token))
                                            {
                                                successs++;
                                                coins += int.Parse(jb.coin);
                                                totalcoin += int.Parse(jb.coin);
                                                lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                                dr.Cells["coins"].Value = coins;
                                                Thread.Sleep(1000);
                                                reportJob(fb_money, jb);
                                            }
                                            else
                                            {
                                                reportJob_error(fb_money, jb);

                                            }
                                        }

                                        //message += ld.scrollLikeNewFeed(u, acc, ldID, acc.app, tuongtac.numlikenewfeedmin, tuongtac.numlikenewfeedmax, tuongtac, token);
                                        break;
                                    case 3:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;

                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        Thread.Sleep(1000);

                                        if (ld.commentPost(u, acc, acc.ldid, jb.comment_need, token))
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            reportJob_error(fb_money, jb);

                                        }

                                        break;
                                    case 4:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;

                                        if (ld.followPagemakemoney(ldID, jb.post_id))
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            if (ld.followPage(ldID, jb.post_id))
                                            {
                                                successs++;
                                                coins += int.Parse(jb.coin);
                                                totalcoin += int.Parse(jb.coin);
                                                lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                                dr.Cells["coins"].Value = coins;
                                                Thread.Sleep(1000);
                                                reportJob(fb_money, jb);
                                            }
                                            else
                                                reportJob_error(fb_money, jb);
                                        }

                                        break;

                                    case 5:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;
                                        if (ld.followFriendUID(acc, jb.post_id, token) == 1)
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            reportJob_error(fb_money, jb);
                                        }

                                        break;
                                    case 6:
                                        u.setStatus(ldID, "Like Post Fanpage...");
                                        dr.Cells["Message"].Value = "Like Post Fanpage";
                                        message += ld.scrollLikePostPage(u, acc, ldID, acc.app, tuongtac.numlikefanpagemin, tuongtac.numlikefanpagemax, token);
                                        break;
                                    case 7:
                                        u.setStatus(ldID, "Comment Page...");
                                        dr.Cells["Message"].Value = "Comment Page";
                                        if (File.Exists(tuongtac.message))
                                            message += ld.scrollCommentPostPage(u, acc, ldID, acc.app, tuongtac.numcommentpostfanpagemin, tuongtac.numcommentpostfanpagemax, File.ReadAllText(tuongtac.message), token);
                                        else
                                        {
                                            if (SettingTool.configld.language == "English")
                                                message += "No comment content set";
                                            else
                                                message += "Chưa thiết lập nội dung comment";
                                        }
                                        break;
                                    case 8:
                                        u.setStatus(ldID, "Add Friend...");
                                        dr.Cells["Message"].Value = "Add Friend";
                                        message += ld.viewAddFriend(u, acc, ldID, acc.app, tuongtac.numaddfriendmin, tuongtac.numaddfriendmax, delay, token);
                                        break;
                                    // tang like cho bai viet
                                    case 9:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        Thread.Sleep(1000);

                                        if (ld.seedingLike(ldID, token))
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            reportJob_error(fb_money, jb);
                                        }
                                        break;
                                    case 10:

                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        Thread.Sleep(1000);
                                        if (ld.seedingLike(ldID, token))
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            reportJob_error(fb_money, jb);
                                        }
                                        break;
                                    case 11:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        Thread.Sleep(1000);
                                        if (ld.seedingLike(ldID, token))
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            reportJob_error(fb_money, jb);
                                        }
                                        break;
                                    case 12:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        Thread.Sleep(1000);

                                        if (ld.seedingLike(ldID, token))
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            reportJob_error(fb_money, jb);
                                        }
                                        break;
                                    case 15:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;

                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);

                                        successs++;
                                        coins += int.Parse(jb.coin);
                                        totalcoin += int.Parse(jb.coin);
                                        lbltotalcoin.Text = string.Format("{0:N0}", totalcoin);
                                        dr.Cells["coins"].Value = coins;
                                        Thread.Sleep(1000);
                                        reportJob(fb_money, jb);

                                        // message += ld.scrollSharePost(u, acc, ldID, acc.app, tuongtac.numsharepostmin, tuongtac.numsharepostmax, comment, token);
                                        break;


                                    case 19:
                                        u.setStatus(ldID, "Add friend by Newfeed...");
                                        dr.Cells["Message"].Value = "Add friend by Newfeed";
                                        message += ld.viewAddFriendbyNewFeed(u, ldID, acc, tuongtac.numaddfriendNewfeedmin, tuongtac.numaddfriendNewfeedmax, delay, token);
                                        break;
                                    case 24:
                                        u.setStatus(ldID, jb.title);
                                        dr.Cells["Message"].Value = jb.title;
                                        ld.OpenLink(ldID, "com.facebook.katana", jb.web_post_link);
                                        if (ld.seedingLike(ldID, token))
                                        {
                                            successs++;
                                            coins += int.Parse(jb.coin);
                                            totalcoin += int.Parse(jb.coin);
                                            dr.Cells["coins"].Value = coins;
                                            Thread.Sleep(1000);
                                            reportJob(fb_money, jb);
                                        }
                                        else
                                        {
                                            reportJob_error(fb_money, jb);
                                        }
                                        break;

                                }
                                dr.Cells["Job"].Value = successs.ToString() + "/" + numjobs.ToString();
                                #endregion
                                if (token.IsCancellationRequested)
                                    break;
                                while (delay >= 0)
                                {
                                    dr.Cells["Message"].Value = "Delay " + delay.ToString() + "s";
                                    u.setStatus(ldID, "Delay..." + delay.ToString() + "s");
                                    Thread.Sleep(1000);
                                    delay--;
                                }
                            }
                            ls_Job.Clear();
                            maxaction++;
                            if (maxaction > 30)
                                break;
                        }
                        u.setStatus(ldID, "Finish");
                        dr.Cells["Message"].Value = message;
                        //  File.WriteAllLines(acc.pathUID, list_uid);

                        u.setStatus(ldID, "Đang backup dataprofile");
                        ld.Zip(acc, ldID);

                    }
                    else
                    {
                        if (acc.Thongbao != null)
                        {
                            if (acc.Thongbao == "Login Successful" || acc.Thongbao == "Đăng nhập thành công")
                            {
                                dr.Cells["Message"].Value = "Đăng nhập không thành công";
                                acc.Thongbao = "Đăng nhập không thành công";
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(acc.Thongbao))
                                {
                                    dr.Cells["Message"].Value = "Đăng nhập không thành công";
                                    acc.Thongbao = "Đăng nhập không thành công";
                                }
                                else
                                {
                                    dr.Cells["Message"].Value = acc.Thongbao;
                                    acc.Thongbao = acc.Thongbao;
                                }
                            }
                        }
                        else
                            dr.Cells["Message"].Value = "Đăng nhập không thành công";

                        acc.TrangThai = "Die";
                        dr.Cells["clStatus"].Value = "Die";
                    }

                    dr.Cells["Message"].Value = "Hoàn thành: " + successs.ToString() + "/" + numjobs.ToString() + " job";

                    nguoidung.updateNoti(acc);
                    nguoidung.updateLastRun(acc);
                    changeColor(dr, Color.White);
                    if (tuongtac.runrandomtuongtac)
                        dr.Cells[0].Value = false;
                }
                catch
                { }

            Lb_Finish:
                // u.setStatus(ldID, "Backup Profile LD...");
                sendLogs("Tương tác thành công LD : " + ldID);
                if (string.IsNullOrEmpty(proxy) == false)
                {
                    ld.setProxyAdb(ldID, ":0");
                }
                ld.quit(acc, ldID);
                frm_main.removeLDToPanel(u);
            }
            catch (Exception ex)
            {
                sendLogs(ex.ToString());
            }
            changeColor(dr, Color.White);
            if (changeIpHelper.checkGetProxyWaitAny())
            {
                xcontroller.finishProxy(proxy);
            }
            string pathcoin = string.Format("{0}\\totalcoin.txt", Application.StartupPath + "\\Config");
            lock (syncObjUID)
            {
                int coin_lk = 0;
                if (File.Exists(pathcoin))
                    coin_lk = int.Parse(File.ReadAllText(pathcoin));

                File.WriteAllText(pathcoin, (coin_lk + coins).ToString());
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

        private string AddFriendbyUID(userLD u, string deviceid, DataGridViewRow dr, Account acc, int minlike, int maxlike, int delay, bool likeavatar, CancellationToken token)
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

                uid = list_uid[0];
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
                    if (likeavatar)
                    {
                        ld.likeAvatar(acc, uid, token);
                    }
                    sendLogs(String.Format("Tài Khoan {0} kết bạn với {1} thành công", acc.email, uid));
                    u.setStatusResult(count);

                }
                else
                {

                    int_loilientiep++;
                    if (int_loilientiep >= SettingTool.configadd.maxerror)
                    {
                        sendLogs(String.Format("Dừng Tài Khoan {0} đã Lỗi liên tiếp {1}lần", acc.email, int_loilientiep));
                        goto Lb_FinishAcc;
                    }
                    if (ld.check_Facebook_has_stopped(u, deviceid, acc, token) == false)
                    {
                        sendLogs(String.Format("Dừng Tài Khoan {0} Lỗi đăng nhập Facebook", acc.email));
                        goto Lb_FinishAcc;
                    }
                }
                if (count >= luot)
                {
                    break;
                }
            }
        Lb_FinishAcc:
            sendLogs("Finish" + acc.email);
            File.AppendAllText(path, list_history.ToString());
            lock (SettingTool.synld)
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

        private void btn_config_Click(object sender, EventArgs e)
        {

            frm_Config_PRO frm = new frm_Config_PRO();
            frm.ShowDialog();
            method_Config();
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
        private void reportJob(fbmoney accfb, job jb)
        {
            try
            {
                var client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/facebook-jobs/ePhonefarm");
                var request = new RestRequest(Method.POST);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", string.Format("bearer {0}", tokenmoney));
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                request.AddParameter("fb_account_id", accfb.id);
                request.AddParameter("job_id", jb.id);
                request.AddParameter("amai_key", txt_amaikey.Text);
                IRestResponse response = client.Execute(request);
                var data = response.Content;
                JObject kq = JObject.Parse(data);
            }
            catch
            {

            }

        }
        private void reportJob_error(fbmoney accfb, job jb)
        {
            try
            {
                var client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/facebook-jobs/ePhonefarm");
                var request = new RestRequest(Method.POST);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", string.Format("bearer {0}", tokenmoney));
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                request.AddParameter("fb_account_id", accfb.id);
                request.AddParameter("job_id", jb.id);
                request.AddParameter("amai_key", txt_amaikey.Text);
                IRestResponse response = client.Execute(request);
                var data = response.Content;
                JObject kq = JObject.Parse(data);
            }
            catch
            {

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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://fb.vieclamonline.org/");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string pathcoin = string.Format("{0}\\totalcoin.txt", Application.StartupPath + "\\Config");
            if (File.Exists(pathcoin) == false)
            {
                File.WriteAllText(pathcoin, " ");
            }
            Process.Start(pathcoin);
        }

        private void btListproxy_Click(object sender, EventArgs e)
        {
            List<string> lsproxy = new List<string>();
            string str = "";

            int i = 1;
            foreach (string proxy in SettingTool.list_running)
            {
                if (!lsproxy.Contains(proxy))
                {
                    lsproxy.Add(proxy);
                    str += i.ToString() + " : " + proxy + "\n";
                    i++;
                }
            }
            MessageBox.Show(str);
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            frm_RegisterHana frm = new frm_RegisterHana(tokenmoney);
            frm.ShowDialog();
        }



    }
}
