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
    public partial class frm_TuongTacLD_PRO : Form
    {
        public frm_TuongTacLD_PRO(List<Account> list_acc, frm_MainLD_PRO frm_main)
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
        int countComplete = 0;
        frm_MainLD_PRO frm_main;
        List<string> list_group;
        xProxyController xcontroller = new xProxyController();
        private void frm_TuongTacLD_Load(object sender, EventArgs e)
        {
            method_LoadAccount();
            method_Config();

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
         
        private bool setupCauHinh()
        {
            try
            {
                int index = cboConfig.SelectedIndex;
                if (cboConfig.Text == "Ngẫu Nhiên" || cboConfig.Text == "")
                {
                    Random rd = new Random();
                    tuongtac = new SettingTuongTac();

                    for (int i = 0; i < 7; i++)
                    {
                        switch (rd.Next(1, 14))
                        {
                            case 1:
                                tuongtac.luotnewfeed = true;
                                break;
                            case 2:
                                tuongtac.likenewfeed = true;
                                break;
                            case 3:
                                tuongtac.commentnewfeed = true;
                                break;
                            case 4:
                                tuongtac.sharepost = true;
                                break;
                            //case 5:
                            //    tuongtac.likefanpage = true;
                            //    break;
                            case 6:
                                tuongtac.likepostfanpage = true;
                                break;
                            case 7:
                                tuongtac.commentpostfanpage = true;
                                break;
                            case 8:
                                tuongtac.addfriend = true;
                                break;
                            case 9:
                                tuongtac.acceptfriend = true;
                                break;
                            case 10:
                                tuongtac.joingroupkeyword = true;
                                break;
                            case 11:
                                tuongtac.likepostgroup = true;
                                break;
                            case 12:
                                tuongtac.commentpostgroup = true;
                                break;
                            case 13:
                                tuongtac.sharepost = true;
                                break;
                            case 14:
                                tuongtac.readnoti = true;
                                break;

                            case 19:
                                tuongtac.addfriendNewfeed = true;
                                break;
                        }
                    }
                    tuongtac.numlikenewfeedmin = rdom.Next(1, 5);
                    tuongtac.numlikenewfeedmax = rdom.Next(5, 10);
                    tuongtac.numcommentnewfeedmin = rdom.Next(5, 10);
                    tuongtac.numcommentnewfeedmin = rdom.Next(5, 10);
                    tuongtac.message = "{Hi|Xin chao|aloha|bonjour|arigato|xiaxia|ciao}";

                    tuongtac.numsharevideo = rdom.Next(1, 3);

                    tuongtac.numsharepostmin = rdom.Next(1, 2);
                    tuongtac.numsharepostmax = rdom.Next(2, 3);
                    tuongtac.strkeywordfanpage = "lam dep, thoi trang, cong nghe, am thuc, nau an";
                    tuongtac.numlikefanpagemin = rdom.Next(1, 5);
                    tuongtac.numlikefanpagemax = rdom.Next(5, 7);
                    tuongtac.numlikepostfanpagemin = rdom.Next(1, 5);
                    tuongtac.numlikepostfanpagemax = rdom.Next(5, 10);
                    tuongtac.numcommentpostfanpagemin = rdom.Next(1, 5);
                    tuongtac.numcommentpostfanpagemax = rdom.Next(5, 10);
                    tuongtac.numaddfriendmin = rdom.Next(1, 5);
                    tuongtac.numaddfriendmax = rdom.Next(5, 7);
                    tuongtac.numaddfriendNewfeedmin = rdom.Next(1, 5);
                    tuongtac.numaddfriendNewfeedmax = rdom.Next(5, 10);
                    tuongtac.numacceptfriendmin = rdom.Next(1, 3);
                    tuongtac.numacceptfriendmax = rdom.Next(5, 10);
                    tuongtac.strkeywordseach = "lam dep, thoi trang, cong nghe, am thuc, nau an";
                    tuongtac.numjoingroupkeywordmin = rdom.Next(1, 3);
                    tuongtac.numjoingroupkeywordmax = rdom.Next(3, 5);
                    tuongtac.numlikepostgroupmin = rdom.Next(1, 3);
                    tuongtac.numlikepostgroupmax = rdom.Next(3, 5);
                    tuongtac.numcommentpostgroupmin = rdom.Next(1, 3);
                    tuongtac.numcommentpostgroupmax = rdom.Next(3, 5);
                    tuongtac.delaymin = rdom.Next(1, 5);
                    tuongtac.delaymax = rdom.Next(20, 60);
                    tuongtac.action = rdom.Next(1, 5);

                    tuongtac.delaymin = 2;
                    tuongtac.delaymax = 3;
                    tuongtac.timestart = 8;
                    tuongtac.timestop = 21;
                    tuongtac.numthread = 1;
                    tuongtac.numdelayld = 40;
                }
                else
                {
                    ComboboxItem item2 = (ComboboxItem)cboConfig.SelectedItem;
                    CauHinh dm = (CauHinh)item2.Tag;
                    try
                    {
                        string path = String.Format("{0}\\Config\\{1}.data", Application.StartupPath, dm.ID);

                        tuongtac = new SettingTuongTac();
                        using (StreamReader r = new StreamReader(path))
                        {
                            string json = r.ReadToEnd();
                            tuongtac = JsonConvert.DeserializeObject<SettingTuongTac>(json);
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Vui lòng chọn cấu hình trước khi chạy tương tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return false;
                    }

                }
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
                if (tuongtac.chkaddfriendlink)
                {
                    list_tuongtac.Add(43);
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
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
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
            
            for (int i= 1 ; i <= SettingTool.configld.numthread * 3; i++)
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
                setupLDGoc();
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
                                                    countAcc++;
                                                    if (countAcc % SettingTool.configld.numthread * 2 == 0)
                                                        closeLdfree();
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
        private void method_Start(string ldID, DataGridViewRow dr, string proxy, CancellationToken token)
        {
            userLD u = frm_main.checkExits(ldID);
            Account acc = (Account)dr.Tag;
            try
            {
                changeColor(dr, Color.Yellow);
                method_log("Open LDPlayer Id: " + ldID);
               
                dr.Cells["clID"].Value = ldID;
                acc.ldid = ldID;
                dr.Cells["Message"].Value = "Restore Data LD";
                ld.setupLD(acc, ldID);
                dr.Cells["Message"].Value = "Open LD";

               
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
                        dr.Cells["Message"].Value = "Disconnected LD: " + ldID;
                        method_log("Disconnected LD: " + ldID);
                        goto Lb_Finish;
                    }
                }

                // bo phan check app tu cai dat 
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
                // app phai cai vao ld0 doan nay ko can thiet nua

                //if (SettingTool.configld.appversion == "Facebook 299")
                //{
                //    if (ld.checkVersionApp(ldID, "299") == false)
                //    {
                //        string path = Application.StartupPath + "\\app\\Facebook299.apk";
                //        if (File.Exists(path))
                //        {
                //            u.setStatus(ldID, "Install App Facebook...");
                //            ld.installApp(ldID, path);

                //            Thread.Sleep(15000);
                //            while (ld.checkApp(ldID, "com.facebook.katana") == false)
                //            {
                //                Thread.Sleep(1000);
                //            }
                //            Thread.Sleep(3000);
                //        }

                //    }
                //}
                //else
                //{
                //    if (SettingTool.configld.appversion == "Facebook 302")
                //    {
                //        if (ld.checkVersionApp(ldID, "302") == false)
                //        {
                //            string path = Application.StartupPath + "\\app\\Facebook302.apk";
                //            if (File.Exists(path))
                //            {
                //                u.setStatus(ldID, "Install App Facebook...");
                //                ld.installApp(ldID, path);
                //                Thread.Sleep(15000);
                //                while (ld.checkApp(ldID, "com.facebook.katana") == false)
                //                {
                //                    Thread.Sleep(1000);
                //                }
                //                Thread.Sleep(3000);
                //            }

                //        }
                //    }
                //    else
                //    {
                //        if (ld.checkApp(ldID, "com.facebook.katana") == false)
                //        {
                //            string path = Application.StartupPath + "\\app\\Facebook.apk";
                //            if (File.Exists(path))
                //            {
                //                u.setStatus(ldID, "Install App Facebook...");
                //                ld.installApp(ldID, path);
                //                while (ld.checkApp(ldID, "com.facebook.katana") == false)
                //                {
                //                    Thread.Sleep(1000);
                //                }
                //                Thread.Sleep(3000);
                //            }
                //        }
                //    }
                //}
                //set status
                ld.setKeyboard(ldID);
                ld.runAdb(ldID, " shell pm disable-user --user 0 com.android.flysilkworm");
                //if (tuongtac.runrandomtuongtac)
                //{
                //    if (tuongtac.numrandomtuongtac < list_acc.Count)
                //    {
                //        Random rd = new Random();
                //        int deleteAcc = list_acc.Count - tuongtac.numrandomtuongtac;

                //        for (int n = 0; n < deleteAcc; n++)
                //        {
                //            list_acc.RemoveAt(rd.Next(0, list_acc.Count));
                //        }
                //    }
                //}
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
                        Delay(2);
                        ld.restoredatafb(acc.ldid, acc.id);
                        ld.runApp(acc.ldid, "com.facebook.katana");
                        ld.checkOpenFacebookFinish(u, acc.ldid);
                    }
                    catch
                    {
                    }
                    dr.Cells["Message"].Value = "Login Facebook";
                    u.setStatus(ldID, "Login Facebook...");
                    Thread.Sleep(1000);

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
                        int step = 0;
                        string answer = tuongtac.strPathAnswer;

                        string comment = "";
                        if (File.Exists(tuongtac.message))
                        {
                            comment = File.ReadAllText(tuongtac.message);
                        }
                        List<string> list_tuongtac_done = new List<string>();
                        for (int i = 0; i < tuongtac.action; i++)
                        {
                            if (token.IsCancellationRequested)
                                break;
                            ld.activeNewfeed(acc.ldid, "com.facebook.katana");
                            if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                            {
                                dr.Cells["Message"].Value = "Lỗi đăng nhập tài khoản Facebook";
                                acc.Thongbao = "Đăng nhập không thành công";
                                acc.TrangThai = "Die";
                                dr.Cells["clStatus"].Value = "Die";
                                break;
                            }


                            #region bat dau tuong tac
                            if (tuongtac.runRandom) //neu chay tuan tu
                            {
                                if (list_tuongtac_done.Count == list_tuongtac.Count)
                                    list_tuongtac_done.Clear();
                            lb_step:
                                step = list_tuongtac[rdom.Next(0, list_tuongtac.Count)];

                                if (list_tuongtac_done.Contains(step.ToString()))
                                    goto lb_step;
                                else
                                    list_tuongtac_done.Add(step.ToString());

                                type = step;

                            }
                            else
                                type = list_tuongtac[rdom.Next(0, list_tuongtac.Count)];

                            int delay = rdom.Next(tuongtac.delaymin, tuongtac.delaymax);
                            acc.app = "com.facebook.katana";
                            switch (type)
                            {
                                case 1:
                                    u.setStatus(ldID, "Scroll Newfeed...");
                                    dr.Cells["Message"].Value = "Scroll Newfeed";
                                    ld.activeNewfeed(ldID, acc.app);
                                    ld.scrollNewfeed(u, ldID, acc, tuongtac.numslidemin, tuongtac.numslidemax, token);
                                    message += "| Scroll newfeed complete";
                                    break;
                                case 2:
                                    u.setStatus(ldID, "Like Newfeed...");
                                    dr.Cells["Message"].Value = "Like Newfeed";
                                    message += ld.scrollLikeNewFeed(u, acc, ldID, acc.app, tuongtac.numlikenewfeedmin, tuongtac.numlikenewfeedmax, tuongtac, token);
                                    break;
                                case 3:
                                    u.setStatus(ldID, "Comment Newfeed...");
                                    dr.Cells["Message"].Value = "Comment Newfeed";
                                    if (File.Exists(tuongtac.message))
                                        message += ld.scrollCommentNewFeed(u, acc, ldID, acc.app, tuongtac.numcommentnewfeedmin, tuongtac.numcommentnewfeedmax, File.ReadAllText(tuongtac.message), token, delay);
                                    else
                                    {
                                        if (SettingTool.configld.language == "English")
                                            message += "No comment content set";
                                        else
                                            message += "Chưa thiết lập nội dung comment";
                                    }
                                    break;
                                //case 4:
                                //    dr.Cells["Message"].Value = "Share video";
                                //    droid.viewVideoShare(ldID, tuongtac.numsharevideo);
                                //    break;
                                case 5:
                                    if (File.Exists(tuongtac.strkeywordfanpage))
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            u.setStatus(ldID, "Follow page...");
                                            dr.Cells["Message"].Value = "Follow page";
                                        }
                                        else
                                        {
                                            u.setStatus(ldID, "Theo dõi trang...");
                                            dr.Cells["Message"].Value = "Theo dõi trang";
                                        }


                                        message += ld.seachLikePage(u, ldID, acc, tuongtac.numlikefanpagemin, tuongtac.numlikefanpagemax, tuongtac.strkeywordfanpage, token);
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
                                case 9:
                                    u.setStatus(ldID, "Accept Friend...");
                                    dr.Cells["Message"].Value = "Accept Friend";
                                    message += ld.scrollAceeptFriend(u, acc, ldID, acc.app, tuongtac.numacceptfriendmin, tuongtac.numacceptfriendmax, delay, token);
                                    break;
                                case 10:

                                    if (!string.IsNullOrEmpty(tuongtac.strkeywordseach))
                                    {
                                        u.setStatus(ldID, "Join Group by search...");
                                        dr.Cells["Message"].Value = "Join Group by search";

                                        message += ld.scrollJoinGroup(u, acc, ldID, acc.app, tuongtac.numjoingroupkeywordmin, tuongtac.numjoingroupkeywordmax, tuongtac.strkeywordseach, delay, tuongtac.chkAutoAnwser, answer, token);

                                    }
                                    break;
                                case 11:
                                    u.setStatus(ldID, "Like Newfeed Group...");
                                    dr.Cells["Message"].Value = "Like Newfeed Group";
                                    message += ld.scrollLikeGroup(u, acc, ldID, acc.app, tuongtac.numlikepostgroupmin, tuongtac.numlikepostgroupmax, token);
                                    break;
                                case 12:
                                    u.setStatus(ldID, "Comment Group...");
                                    dr.Cells["Message"].Value = "Comment Group";
                                    if (File.Exists(tuongtac.strPathCommentGroup))
                                    {
                                        comment = File.ReadAllText(tuongtac.strPathCommentGroup);
                                    }
                                    message += ld.scrollCommentGroup(u, acc, ldID, acc.app, tuongtac.numcommentpostgroupmin, tuongtac.numcommentpostgroupmax, comment, delay, token);
                                    break;
                                case 13:
                                    u.setStatus(ldID, "Share Post...");
                                    dr.Cells["Message"].Value = "Share Post";
                                    message += ld.scrollSharePost(u, acc, ldID, acc.app, tuongtac.numsharepostmin, tuongtac.numsharepostmax, comment, token);
                                    break;
                                case 14:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Read notification...");
                                        dr.Cells["Message"].Value = "Read notification";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Đọc thông báo...");
                                        dr.Cells["Message"].Value = "Đọc thông báo";
                                    }


                                    ld.Notification(ldID, "com.facebook.katana", delay);
                                    message += "| Read notification complete";
                                    break;

                                //case 15:
                                //    dr.Cells["Message"].Value = "Seeding";
                                //    droid.Seeding(device, tuongtac, acc);
                                //    break;

                                case 18:
                                    u.setStatus(ldID, "Join group by UID...");
                                    dr.Cells["Message"].Value = "Join group by UID";
                                    string uid = "";
                                    int count = 0;
                                    StringBuilder list_history = new StringBuilder();
                                    string path = "";

                                    if (string.IsNullOrEmpty(acc.email))
                                        path = String.Format("{0}\\logs\\{1}_joingroup.txt", Application.StartupPath, acc.id);
                                    else
                                        path = String.Format("{0}\\logs\\{1}_joingroup.txt", Application.StartupPath, acc.email);

                                    string historyadd = "";

                                    if (File.Exists(path))
                                        historyadd = File.ReadAllText(path);
                                    int int_joingroup = rd.Next(tuongtac.numGroupUIDMin, tuongtac.numGroupUIDMax);
                                    u.setStatusSum(int_joingroup);
                                    StringBuilder historyload = new StringBuilder();
                                Lb_Start:
                                    if (list_group.Count > 0)
                                    {
                                        lock (syncObjUID)
                                        {
                                            uid = list_group[0];
                                            list_group.Remove(uid);
                                        }

                                        if (historyadd.Contains(uid))
                                        {
                                            goto Lb_Start;
                                        }
                                        if (tuongtac.joingroupnoapprove)
                                        {
                                            Profile_Controller profile_controller = new Profile_Controller();
                                            bool has_approve = profile_controller.checkGroupApprove(SettingTool.configld.cookies, uid);
                                            if (has_approve)
                                            {
                                                if (SettingTool.configld.language == "English")
                                                    u.setStatus(ldID, "Skip the group waiting for approval :  " + uid);
                                                else
                                                    u.setStatus(ldID, "Bỏ qua group chờ duyệt :  " + uid);
                                                goto Lb_Start;
                                            }
                                        }
                                        Profile_Controller profile = new Profile_Controller();
                                        if (token.IsCancellationRequested)
                                            break;
                                        u.setStatus(ldID, "Join group ID " + uid);

                                        if (ld.scrollJoinGroupbyUID(u, acc, ldID, acc.app, uid, delay, tuongtac.chkAutoAnwser, answer, token))
                                        {
                                            count++;
                                            u.setStatusResult(count);
                                            Delay(delay);
                                            list_history.AppendLine(uid);
                                            if (count >= int_joingroup)
                                            {
                                                File.AppendAllText(path, list_history.ToString());
                                            }
                                            else
                                                goto Lb_Start;

                                        }
                                        else
                                        {
                                            list_history.AppendLine(uid);
                                            method_log(string.Format("Không thể load nhóm để tham gia: {0}", uid));
                                            historyload.AppendLine(uid);

                                            if (ld.check_Facebook_has_stopped(u, ldID, acc, token) == false)
                                            {
                                                sendLogs(String.Format("Dừng Tài Khoan {0} Lỗi đăng nhập Facebook", acc.email));
                                                goto Lb_FinishAcc;
                                            }

                                        }

                                        if (count < int_joingroup)
                                        {
                                            goto Lb_Start;

                                        }
                                    }
                                    else
                                    {
                                        method_log("Đã hết danh sách nhóm");
                                    }
                                Lb_FinishAcc:
                                    ld.activeNewfeed(ldID, acc.app);
                                    File.AppendAllText(path, list_history.ToString());
                                    if (historyload.Length > 0)
                                    {
                                        string pathgroup = Application.StartupPath + "\\grouperror.txt";
                                        File.AppendAllText(pathgroup, historyload.ToString());
                                    }
                                    //save danh sach group
                                    lock (syncObjUID)
                                    {
                                        File.WriteAllLines(tuongtac.strPath, list_group);

                                    }
                                    message += "| Join group by ID complete" + count.ToString() + "/" + int_joingroup.ToString();
                                    break;

                                case 19:
                                    u.setStatus(ldID, "Add friend by Newfeed...");
                                    dr.Cells["Message"].Value = "Add friend by Newfeed";
                                    message += ld.viewAddFriendbyNewFeed(u, ldID, acc, tuongtac.numaddfriendNewfeedmin, tuongtac.numaddfriendNewfeedmax, delay, token);
                                    break;
                                case 20:
                                    u.setStatus(ldID, "Add friend by UID...");
                                    dr.Cells["Message"].Value = "Add friend by UID";
                                    message += AddFriendbyUID(u, ldID, dr, acc, tuongtac.numaddfrienduidmin, tuongtac.numaddfrienduidmax, delay, tuongtac.likeavatar, token);

                                    break;
                                case 21:
                                    u.setStatus(ldID, "Reaction Group ID...");
                                    dr.Cells["Message"].Value = "Reaction Group ID";
                                    if (tuongtac.pathGroupIdLikecomment != "")
                                    {
                                        if (File.Exists(tuongtac.pathGroupIdLikecomment))
                                        {
                                            List<string> list_grid = new List<string>();
                                            list_grid = File.ReadAllLines(tuongtac.pathGroupIdLikecomment).ToList().Distinct().ToList();
                                            string[] lsId = new string[1];
                                            string groupid = list_grid[rd.Next(0, list_grid.Count)];
                                            u.setStatus(ldID, "Reaction Group ID " + groupid);
                                            dr.Cells["Message"].Value = "Reaction Group ID " + groupid;
                                            lsId[0] = groupid;
                                            if (File.Exists(tuongtac.strPathCommentGroup))
                                            {
                                                comment = File.ReadAllText(tuongtac.strPathCommentGroup);
                                                message += ld.likecommentID(u, acc, dr, ldID, acc.app, tuongtac.numlikepostgroupmin, tuongtac.numcommentpostgroupmin, true, true, lsId.ToList(), comment, 2, delay, false, token);
                                            }
                                            else
                                            {
                                                if (SettingTool.configld.language == "English")
                                                    dr.Cells["Message"].Value = "Please set coment content for group!";
                                                else
                                                    dr.Cells["Message"].Value = "Hãy thiết lập nội dung coment cho group!";
                                            }


                                        }
                                        else
                                        {
                                            if (SettingTool.configld.language == "English")
                                                dr.Cells["Message"].Value = "Review file ID group";
                                            else
                                                dr.Cells["Message"].Value = "Kiểm tra lại file ID group";
                                        }
                                    }
                                    else
                                    {
                                        if (SettingTool.configld.language == "English")
                                        {
                                            dr.Cells["Message"].Value = "Not yet set up file Group Id";
                                            message += "Not yet set up file Group Id";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Chưa thiết lập file Group Id";
                                            message += "Chưa thiết lập file Group Id";
                                        }

                                    }
                                    break;
                                case 22:
                                    u.setStatus(ldID, "Inbox Messenger...");
                                    dr.Cells["Message"].Value = "Inbox Messenger";
                                    // droid.viewInviteLikePage(device, acc.app, tuongtac.numInviteLikepage, tuongtac.strIDPage, delay);
                                    if (ld.checkAppCurrentMess(acc) == false)
                                        ld.restoreAccountMess(acc.ldid, acc);
                                    ld.killApp(acc.ldid, "com.facebook.orca");
                                    ld.runApp(acc.ldid, "com.facebook.orca");
                                    status = false;
                                    status = ld.checkIsLoginMess(acc);
                                    if (status)
                                    {
                                        status = true;
                                    }
                                    else
                                    {
                                        status = ld.loginMess(acc, token);
                                    }
                                    if (status)
                                    {
                                        if (File.Exists(tuongtac.pathReaction))
                                        {
                                            tuongtac.message = File.ReadAllText(tuongtac.pathReaction);
                                            message += ld.reactionMessengerFriend(acc.ldid, acc, "com.facebook.orca", tuongtac.message, tuongtac.numMessenger, delay, token);
                                        }
                                        else
                                        {
                                            if (SettingTool.configld.language == "English")
                                            {
                                                dr.Cells["Message"].Value = "Not yet set up file reaction messenger";
                                                message += "Not yet set up file reaction messenger";
                                            }
                                            else
                                            {
                                                dr.Cells["Message"].Value = "Chưa thiết lập file để tương tác messenger";
                                                message += "Chưa thiết lập file để tương tác messenger";
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (SettingTool.configld.language == "English")
                                            message += "| login messenger fail";
                                        else
                                            message += "| Đăng nhập messenger không thành công";
                                    }
                                    break;

                                case 23:
                                    u.setStatus(ldID, "Search info...");
                                    dr.Cells["Message"].Value = "Search info";

                                    if (!string.IsNullOrEmpty(tuongtac.strseachInfo))
                                    {
                                        dr.Cells["Message"].Value = "Search info";

                                        ld.SeachInfo(u, acc, ldID, acc.app, tuongtac.strseachInfo, delay, token);
                                        message += "| Search info successful";
                                    }
                                    else
                                        message += "| Not avaiable key search info";
                                    break;
                                case 24:
                                    u.setStatus(ldID, "Scroll Newfeed Watch...");
                                    dr.Cells["Message"].Value = "Scroll Newfeed Watch";
                                    ld.scrollNewfeedWatch(u, ldID, acc, tuongtac.timenewfeedwatchmin, tuongtac.timenewfeedwatchmax, token,tuongtac.timeviewvideomin,tuongtac.timeviewvideomax);
                                    message += "| Scroll newfeed watch successful";
                                    break;
                                case 25:
                                    u.setStatus(ldID, "Like Video Watch...");
                                    dr.Cells["Message"].Value = "Like Video Watch";
                                    message += ld.scrollLikeNewFeedWatch(u, acc, ldID, acc.app, tuongtac.likenewfeedwatchmin, tuongtac.likenewfeedwatchmax, tuongtac, token);
                                    break;

                                case 26:
                                    u.setStatus(ldID, "Scroll Newfeed Marketplace...");
                                    dr.Cells["Message"].Value = "Scroll Newfeed Marketplace";
                                    ld.scrollNewfeedMarketplace(u, ldID, acc, tuongtac.timenewfeedmarketplacemin, tuongtac.timenewfeedmarketplacemax, token);
                                    message += "| Scroll newfeed Marketplace successful";
                                    break;

                                case 27:
                                    u.setStatus(ldID, "Scroll newfeed group...");
                                    dr.Cells["Message"].Value = "Scroll newfeed group";

                                    ld.scrollNewfeedGroup(u, ldID, acc, tuongtac.numscrollgroupmin, tuongtac.numscrollgroupmax, token);
                                    message += "| Scroll newfeed successful";

                                    break;
                                case 28:
                                    u.setStatus(ldID, "Like post group...");
                                    dr.Cells["Message"].Value = "Like post group";
                                    message += ld.scrollLikeNewFeedGroup(u, acc, ldID, acc.app, tuongtac.num_like_newfeed_groupmin, tuongtac.num_like_newfeed_groupmax, tuongtac, token);

                                    break;
                                case 29:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Cancel the add friend request...");
                                        dr.Cells["Message"].Value = "Cancel the add friend request";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Hủy lời mời kết bạn...");
                                        dr.Cells["Message"].Value = "Hủy lời mời kết bạn";
                                    }
                                    message += ld.scrollCancelRequestFriend(u, acc, ldID, acc.app, tuongtac.numcacelrequestmin, tuongtac.numcacelrequestmax, delay, token);
                                    break;
                                case 30:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Leave group...");
                                        dr.Cells["Message"].Value = "Leave group";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Rời nhóm...");
                                        dr.Cells["Message"].Value = "Rời nhóm";
                                    }

                                    message += ld.removeGroup(u, acc, tuongtac, token);
                                    break;
                                case 31:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Post to the group...");
                                        dr.Cells["Message"].Value = "Post to the group";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Đăng bài group...");
                                        dr.Cells["Message"].Value = "Đăng bài group";
                                    }

                                    //kiem tra bài đăng 
                                    string pathlog = string.Format("{0}\\logs\\{1}", Application.StartupPath, acc.id);
                                    if (Directory.Exists(pathlog) == false)
                                    {
                                        Directory.CreateDirectory(pathlog);
                                    }
                                    int totalpost = 0;
                                    string pathlogfile = string.Format("{0}\\PostGroup_{1}.txt", pathlog, DateTime.Now.ToString("MM-dd-yy"));
                                    if (File.Exists(pathlogfile))
                                    {
                                        totalpost = Convert.ToInt32(File.ReadAllText(pathlogfile));
                                    }
                                    else
                                    {
                                        totalpost = 0;
                                    }
                                    //kiem tra file post ton tai hay ko
                                    string pathpost = string.Format("{0}\\Schedule\\{1}\\PostGroup", Application.StartupPath, acc.id);
                                    if (Directory.Exists(pathpost))
                                    {
                                        List<string> list_path = System.IO.Directory.GetFiles(pathpost, "*.txt").ToList();
                                        if (list_path.Count > 0)
                                        {
                                            if (totalpost >= tuongtac.postgroup_maxday)
                                            {
                                                if (SettingTool.configld.language == "English")
                                                {
                                                    dr.Cells["Message"].Value = "Post / day limit reached";
                                                    message += "Post/day limit reached";
                                                }
                                                else
                                                {
                                                    dr.Cells["Message"].Value = "Đã đạt giới hạn đăng bài/ngày";
                                                    message += "Đã đạt giới hạn đăng bài/ngày";
                                                }

                                            }
                                            else
                                            {
                                                if (SettingTool.configld.language == "English")
                                                {
                                                    u.setStatus(ldID, "Start posting group...");
                                                    dr.Cells["Message"].Value = "Start posting group";
                                                }
                                                else
                                                {
                                                    u.setStatus(ldID, "Bắt đầu đăng bài group...");
                                                    dr.Cells["Message"].Value = "Bắt đầu đăng bài group";
                                                }

                                                string result = ld.postGroup(u, acc, tuongtac, list_path, totalpost, token);

                                                message += result;
                                                dr.Cells["Message"].Value = result;
                                                u.setStatus(ldID, result);

                                                list_path = System.IO.Directory.GetFiles(pathpost, "*.txt").ToList();
                                                acc.datagroup = list_path.Count.ToString();
                                                nguoidung.updateDataPost(acc);
                                            }

                                        }
                                        else
                                        {
                                            acc.datagroup = "0";
                                            if (nguoidung.updateDataPost(acc))
                                            {

                                            }
                                            if (SettingTool.configld.language == "English")
                                            {
                                                message += "You have not configured the post content";
                                                dr.Cells["Message"].Value = "You have not configured the post content";
                                            }
                                            else
                                            {
                                                message += "Bạn chưa cấu hình nội dung đăng bài";
                                                dr.Cells["Message"].Value = "Bạn chưa cấu hình nội dung đăng bài";
                                            }

                                        }
                                    }
                                    else
                                    {
                                        acc.datagroup = "0";
                                        nguoidung.updateDataPost(acc);
                                        Directory.CreateDirectory(pathpost);
                                        if (SettingTool.configld.language == "English")
                                        {
                                            message += "You have not configured the post content";
                                            dr.Cells["Message"].Value = "You have not configured the post content";
                                        }
                                        else
                                        {
                                            message += "Bạn chưa cấu hình nội dung đăng bài";
                                            dr.Cells["Message"].Value = "Bạn chưa cấu hình nội dung đăng bài";
                                        }
                                    }

                                    break;
                                case 32:
                                    #region post profile
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Post profile...");
                                        dr.Cells["Message"].Value = "Post profile";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Đăng bài profile...");
                                        dr.Cells["Message"].Value = "Đăng bài profile";
                                    }
                                    if (!ld.checkContentLD(ldID, "Viết bài trên Facebook|Đi tới trang cá nhân"))
                                    {
                                        ld.killApp(acc.ldid, "com.facebook.katana");
                                        ld.runApp(acc.ldid, "com.facebook.katana");
                                        ld.checkIsLogin(acc);
                                    }

                                    // ld.loginFacebookLD(acc, token);
                                    //kiem tra bài đăng 
                                    string pathlogproifle = string.Format("{0}\\logs\\{1}", Application.StartupPath, acc.id);
                                    if (Directory.Exists(pathlogproifle) == false)
                                    {
                                        Directory.CreateDirectory(pathlogproifle);
                                    }
                                    int totalpostprofile = 0;
                                    string pathlogtotalprofile = string.Format("{0}\\PostProfile_{1}.txt", pathlogproifle, DateTime.Now.ToString("MM-dd-yy"));
                                    if (File.Exists(pathlogtotalprofile))
                                    {
                                        totalpostprofile = Convert.ToInt32(File.ReadAllText(pathlogtotalprofile));
                                    }
                                    else
                                    {
                                        totalpostprofile = 0;
                                    }
                                    //kiem tra file post ton tai hay ko
                                    string pathpostprofile = string.Format("{0}\\Schedule\\{1}\\PostProfile", Application.StartupPath, acc.id);
                                    if (Directory.Exists(pathpostprofile))
                                    {
                                        List<string> list_path = System.IO.Directory.GetFiles(pathpostprofile, "*.txt").ToList();
                                        if (list_path.Count > 0)
                                        {
                                            if (totalpostprofile >= tuongtac.postprofile_maxday)
                                            {
                                                if (SettingTool.configld.language == "English")
                                                {
                                                    u.setStatus(ldID, "Start posting profile...");
                                                    dr.Cells["Message"].Value = "Start posting profile";
                                                }
                                                else
                                                {
                                                    u.setStatus(ldID, "Bắt đầu đăng bài profile...");
                                                    dr.Cells["Message"].Value = "Bắt đầu đăng bài profile";
                                                }
                                            }
                                            else
                                            {
                                                if (SettingTool.configld.language == "English")
                                                {
                                                    u.setStatus(ldID, "Start posting profile...");
                                                    dr.Cells["Message"].Value = "Start posting profile";
                                                }
                                                else
                                                {
                                                    u.setStatus(ldID, "Bắt đầu đăng bài profile...");
                                                    dr.Cells["Message"].Value = "Bắt đầu đăng bài profile";
                                                }
                                                string result = ld.postProfile(u, acc, tuongtac, list_path, totalpostprofile, token);
                                                list_path = System.IO.Directory.GetFiles(pathpostprofile, "*.txt").ToList();
                                                acc.dataprofile = list_path.Count.ToString();
                                                nguoidung.updateDataPost(acc);
                                                message += result;
                                                dr.Cells["Message"].Value = result;
                                                u.setStatus(ldID, result);
                                            }

                                        }
                                        else
                                        {
                                            acc.dataprofile = "0";
                                            nguoidung.updateDataPost(acc);
                                            if (SettingTool.configld.language == "English")
                                            {
                                                message += "You have not configured the post content";
                                                dr.Cells["Message"].Value = "You have not configured the post content";
                                            }
                                            else
                                            {
                                                message += "Bạn chưa cấu hình nội dung đăng bài";
                                                dr.Cells["Message"].Value = "Bạn chưa cấu hình nội dung đăng bài";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        acc.dataprofile = "0";
                                        nguoidung.updateDataPost(acc);
                                        Directory.CreateDirectory(pathpostprofile);
                                        if (SettingTool.configld.language == "English")
                                        {
                                            message += "You have not configured the post content";
                                            dr.Cells["Message"].Value = "You have not configured the post content";
                                        }
                                        else
                                        {
                                            message += "Bạn chưa cấu hình nội dung đăng bài";
                                            dr.Cells["Message"].Value = "Bạn chưa cấu hình nội dung đăng bài";
                                        }
                                    }
                                    #endregion
                                    break;
                                case 33:
                                    #region khang spam
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Check Spam...");
                                        dr.Cells["Message"].Value = "Check Spam";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Kháng spam...");
                                        dr.Cells["Message"].Value = "Kháng spam";
                                    }
                                    for (int int_khang = 0; int_khang < 5; int_khang++)
                                    {
                                        if (ld.khangSpam(acc, token) == false)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Thread.Sleep(2000);
                                        }
                                    }
                                    message += " kháng spam ";
                                    #endregion
                                    break;

                                case 34:
                                    #region Hủy kết bạn uid
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Cancel Addfriend...");
                                        dr.Cells["Message"].Value = "Cancel Addfriend";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Hủy kết bạn...");
                                        dr.Cells["Message"].Value = "Hủy kết bạn";
                                    }
                                    List<string> lsUid = new List<string>();
                                    lsUid = File.ReadAllLines(tuongtac.pathCancelFriendUID).ToList();

                                    List<string> ls_friend = getListfriend(ldID, acc);
                                    int maxerror = 0;
                                    int countUnfriend = 0;
                                    int int_unfriend = rd.Next(tuongtac.numcancelfrienduidmin, tuongtac.numcancelfrienduidmax);
                                    u.setStatusSum(int_unfriend);
                                    foreach (string id in lsUid)
                                    {
                                        if (token.IsCancellationRequested)
                                            break;
                                        if (ls_friend.Count > 0)
                                        {
                                            if (ls_friend.Contains(id))
                                            {
                                                if (ld.unFriendUID(acc, id, token))
                                                {
                                                    countUnfriend++;

                                                    maxerror = 0;
                                                }
                                                else
                                                {
                                                    maxerror++;
                                                    if (maxerror > 20)
                                                        break;
                                                    if (ld.check_Facebook_has_stopped(u, ldID, acc, token) == false)
                                                    {
                                                        break;
                                                    }
                                                }

                                                if (countUnfriend >= int_unfriend)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (ld.unFriendUID(acc, id, token))
                                            {
                                                countUnfriend++;
                                                maxerror = 0;
                                            }
                                            else
                                            {
                                                maxerror++;
                                                if (maxerror > 20)
                                                    break;
                                            }
                                            if (countUnfriend >= int_unfriend)
                                            {
                                                break;
                                            }
                                        }
                                        u.setStatusResult(countUnfriend);
                                    }
                                    message += " |Hủy kết bạn hoàn thành:" + countUnfriend.ToString() + "/" + int_unfriend;
                                    #endregion
                                    break;
                                case 35:
                                    #region Hủy kết bạn random
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "Cancel Addfriend...");
                                        dr.Cells["Message"].Value = "Cancel Addfriend";

                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Hủy kết bạn random...");
                                        dr.Cells["Message"].Value = "Hủy kết bạn random";
                                    }
                                    int int_cancelfriend = rd.Next(tuongtac.numcancelfrienduidmin, tuongtac.numcancelfrienduidmax);
                                    u.setStatusSum(int_cancelfriend);
                                    List<string> lsfriend_rd = getListfriend(ldID, acc);
                                    int max = 0;
                                    int count_rd = 0;
                                    if (lsfriend_rd.Count > 0)
                                    {
                                        foreach (string id in lsfriend_rd)
                                        {
                                            if (token.IsCancellationRequested)
                                                break;
                                            if (ld.unFriendUID(acc, id, token))
                                            {
                                                count_rd++;
                                                u.setStatusResult(count_rd);
                                                max = 0;
                                            }
                                            else
                                            {
                                                max++;
                                                if (max > 20)
                                                    break;
                                                if (ld.check_Facebook_has_stopped(u, ldID, acc, token) == false)
                                                {
                                                    break;
                                                }
                                            }
                                            if (count_rd >= int_cancelfriend)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        count_rd = ld.unfriendManual(acc, int_cancelfriend, token);
                                    }

                                    message += " |Hủy kết bạn random hoàn thành:" + count_rd.ToString() + "/" + int_cancelfriend.ToString();
                                    #endregion
                                    break;
                                case 36:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, " Sliding strory...");
                                        dr.Cells["Message"].Value = "Sliding story";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, " Lướt story...");
                                        dr.Cells["Message"].Value = "Lướt story";
                                    }
                                    if (!ld.checkContentLD(ldID, "Viết bài trên Facebook|Đi tới trang cá nhân|Make a post on Facebook"))
                                    {
                                        ld.killApp(acc.ldid, "com.facebook.katana");
                                        ld.runApp(acc.ldid, "com.facebook.katana");
                                    }

                                    ld.scrollStory(u, ldID, acc, tuongtac.numslidestorymin, tuongtac.numslidestorymax, token);
                                    message += " Lướt xong story";
                                    break;
                                case 37:
                                    {
                                        #region addfriend group
                                        List<string> list_uidgroup = new List<string>();
                                        string pathgroupuid = Application.StartupPath + "\\ListgroupId.txt";
                                        list_uidgroup = File.ReadAllLines(pathgroupuid).ToList();
                                        if (list_uidgroup.Count > 0)
                                        {
                                            string uidgroup = list_uidgroup[rd.Next(0, list_uidgroup.Count)];
                                            u.setStatus(ldID, "Add friend Group: " + uidgroup);
                                            dr.Cells["Message"].Value = "Add friend Group";
                                            message += ld.viewAddFriendbyNewFeedGroup(u, acc, ldID, acc.app, uidgroup, tuongtac.numaddfriendgroupmin, tuongtac.numaddfriendgroupmax, delay, token);
                                        }
                                        #endregion
                                        break;
                                    }
                                case 38:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, " Like strory...");
                                        dr.Cells["Message"].Value = "Like story";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, " Like story...");
                                        dr.Cells["Message"].Value = "Like story";
                                    }
                                    if (!ld.checkContentLD(ldID, "Viết bài trên Facebook|Đi tới trang cá nhân|Make a post on Facebook"))
                                    {
                                        ld.killApp(acc.ldid, "com.facebook.katana");
                                        ld.runApp(acc.ldid, "com.facebook.katana");
                                        ld.checkIsLogin(acc);
                                    }
                                    message += ld.likeStory(u, ldID, acc, tuongtac.numlikestorymin, tuongtac.numlikestorymax, token);
                                    break;

                                case 39:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, " Comment strory...");
                                        dr.Cells["Message"].Value = "Comment story";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, " Comment story...");
                                        dr.Cells["Message"].Value = "Comment story";
                                    }
                                    if (!ld.checkContentLD(ldID, "Viết bài trên Facebook|Đi tới trang cá nhân|Make a post on Facebook"))
                                    {
                                        ld.killApp(acc.ldid, "com.facebook.katana");
                                        ld.runApp(acc.ldid, "com.facebook.katana");
                                    }
                                    message += ld.commentStory(u, ldID, acc, tuongtac.numcommentstorymin, tuongtac.numcommentstorymax, comment, token);
                                    break;

                                case 40:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, " Get birthday ...");
                                        dr.Cells["Message"].Value = "Get birthday...";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, " Lấy ngày sinh...");
                                        dr.Cells["Message"].Value = "Lấy ngày sinh";
                                    }

                                    if (ld.Getbirthday(u, acc, ldID, token))
                                    {
                                        u.setStatus(ldID, " Lấy ngày sinh thành công...");
                                        dr.Cells["Message"].Value = message + "| Lấy ngày sinh thành công";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, " Lấy ngày sinh không thành công...");
                                        dr.Cells["Message"].Value = message + "Lấy ngày sinh không thành công";
                                    }
                                    break;

                                case 41:
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, " Join group suggest ...");
                                        dr.Cells["Message"].Value = "Join group suggest...";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, " Tham gia nhóm theo gợi ý...");
                                        dr.Cells["Message"].Value = "Tham gia nhóm theo gợi ý ";
                                    }

                                    message += ld.joinGroupbysuggest(u, acc, ldID, acc.app, tuongtac.numjoingroupsuggestmin, tuongtac.numjoingroupsuggestmax, tuongtac.strkeywordseach, delay, tuongtac.chkAutoAnwser, answer, token);

                                    break;
                                case 42:
                                    u.setStatus(ldID, "Comment Newfeed Group...");
                                    dr.Cells["Message"].Value = "Comment Newfeed Group";
                                    if (File.Exists(tuongtac.strPathCommentGroup))
                                    {
                                        comment = File.ReadAllText(tuongtac.strPathCommentGroup);
                                    }
                                    message += ld.scrollCommentNefeedGroup(u, acc, ldID, acc.app, tuongtac.numcommentnewfeedgroupmin, tuongtac.numcommentnewfeedgroupmax, comment, delay, token);
                                    break;

                                case 43:
                                    u.setStatus(ldID, "kết bạn người đã comment link...");
                                    dr.Cells["Message"].Value = "kết bạn người đã comment link";
                                    if (File.Exists(tuongtac.txtfilelink))
                                    {
                                        comment = File.ReadAllText(tuongtac.txtfilelink);
                                    }
                                    ld.OpenLink(acc.ldid, "com.facebook.katana", comment);
                                  //  message += ld.addfriendbylink(u, acc, ldID, acc.app, tuongtac.numaddfriendlinkmin, tuongtac.numaddfriendlinkmax, token);
                                    break;
                            }
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
                        u.setStatus(ldID, "Finish");
                        dr.Cells["Message"].Value = message;
                        //  File.WriteAllLines(acc.pathUID, list_uid);

                        changeColor(dr, Color.White);
                        //change IP

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
                        //            Delay(1);
                        //            i--;
                        //        }
                        //    }
                        //}
                        //  update token cookie
                        if (SettingTool.configld.has_savetoken)
                        {
                            ld.SaveTokenCookies(acc);
                        }
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
               
            }
            catch (Exception ex)
            {
                sendLogs(ex.ToString());
            }
            if (string.IsNullOrEmpty(proxy) == false)
            {
                ld.setProxyAdb(ldID, ":0");
            }
            ld.quit(acc, ldID);
            frm_main.removeLDToPanel(u);
            changeColor(dr, Color.White);
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
