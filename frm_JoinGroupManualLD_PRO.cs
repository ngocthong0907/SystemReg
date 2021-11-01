using Newtonsoft.Json;
using SharpAdbClient;
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
    public partial class frm_JoinGroupManualLD_PRO : Form
    {
        public frm_JoinGroupManualLD_PRO(List<Account> list_acc, frm_MainLD_PRO frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        xProxyController xcontroller = new xProxyController();
        frm_MainLD_PRO frm_main;
        List<Account> list_acc;
        bool stop = false;
        object synAcc = new object();
        SettingTuongTac tuongtac = new SettingTuongTac();
      
        ninjaDroidHelper droid = new ninjaDroidHelper();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        Thread thread_1;
        static object syncObjUID = new object();
        //List<string> list_uid = new List<string>();
        Random rd = new Random();
        List<int> list_tuongtac = new List<int>();

        LDController ld = new LDController();
        List<string> list_uid = new List<string>();
        Random rdom = new Random();
        object synUID = new object();
        // runLDs formLD = new runLDs();
        int countComplete = 0;

        List<string> lines = new List<string>();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void frm_JoinGroupManualLD_Load(object sender, EventArgs e)
        {
            method_LoadAccount();
            try
            {
                ConfigFormJoinGroupManual config = new ConfigFormJoinGroupManual();
                string path = String.Format("{0}\\Config\\ConfigJoinGroupManual.data", Application.StartupPath);
                if (File.Exists(path))
                {
                    using (StreamReader r = new StreamReader(path))
                    {
                        string json = r.ReadToEnd();
                        config = JsonConvert.DeserializeObject<ConfigFormJoinGroupManual>(json);
                    }
                    txtUID.Text = config.groupid;
                    numDelayMin.Value = config.delaymin;
                    numDelayMax.Value = config.delaymax;
                    txtPathAnswer.Text = config.answer;
                    chkJoinTrung.Checked = config.jointrung;
                }
                if (SettingTool.configld.language == "English")
                {
                    setupLanguage();
                }
            }
            catch
            { }
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
                //  richTextBox_0 = richLogs,
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
            lines = txtUID.Lines.ToList();
            tokenSource = new CancellationTokenSource();

            ClearMessage();
            string pathlog = Application.StartupPath + "\\logs";
            if (!Directory.Exists(pathlog))
            {
                Directory.CreateDirectory(pathlog);
            }
            saveConfig();
            //save config
            startTuongTac();
        }
        public void saveConfig()
        {
            ConfigFormJoinGroupManual config = new ConfigFormJoinGroupManual();
            config.groupid = txtUID.Text.Trim();
            config.delaymin = (int)numDelayMin.Value;
            config.delaymax = (int)numDelayMax.Value;
            config.answer = txtPathAnswer.Text.Trim();
            if (chkJoinTrung.Checked)
            {
                config.jointrung = true;
            }
            else
            {
                config.jointrung = false;
            }

            config.pathanswer = txtPathAnswer.Text.Trim();
            File.WriteAllText(Application.StartupPath + "\\Config\\ConfigJoinGroupManual.data", JsonConvert.SerializeObject(config));
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
                    this.thread_1 = new Thread(new ThreadStart(this.runTuongTac));
                    thread_1.IsBackground = true;
                    this.thread_1.Start();
                }

            }
        }

        private void runLoginWaitAny()
        {
            try
            {
                int maxproxy = 0;
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
                                method_Stop();
                        }
                        else
                            method_Stop();
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

            var token = tokenSource.Token;
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
                tokenSource.CancelAfter(SettingTool.configld.timeout * 60000);
                try
                {
                    Task.WaitAll(tasks);
                }
                catch
                { }

                if (list_dr.Count > 0)
                {
                    goto Lb_quayvong;
                }
                else
                {
                    method_Stop();
                }
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
        private void method_Stop()
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
                u.setStatus(ldID, "Kết nối thành công LD...");
            }
            else
            {
                if (ld.autoRunLDSetPosition(ldID, u, token))
                {
                    u.setStatus(ldID, "Kết nối thành công LD...");
                }
                else
                {
                    u.setStatus(ldID, "Disconnected...");
                    method_log("Không kết nối được với LD: " + ldID);
                    goto Lb_Finish;
                }
            }
            ld.setKeyboard(ldID);
            ld.restoredatafb(acc.ldid, acc.id);
            try
            {
                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
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

                //get id_account running
                int maxerror = 0;
                try
                {

                    dem++;
                    if (SettingTool.configld.typeip == 4 && dem > 1)
                    {
                        changeIpHelper.connectAfterOpen(u, richLogs, ldID, acc, token);

                    }
                    changeColor(dr, Color.Yellow);
                    dr.Cells["Message"].Value = "Running";

                    ld.killApp(acc.ldid, "com.facebook.katana");
                    ld.restoredatafb(acc.ldid, acc.id);
                    ld.runApp(acc.ldid, "com.facebook.katana");
                    ld.checkOpenFacebookFinish(u, acc.ldid);

                    dr.Cells["Message"].Value = "Login Facebook";
                    u.setStatus(ldID, "Đăng nhập Facebook...");
                    ld.check_Facebook_has_stopped(u,acc.ldid, acc, token);
                    bool status = ld.loginFacebookTuongTac(u, acc, token);
                    

                    if (status)
                    {
                        if (stop)
                            goto Lb_Finish;
                        u.setStatus(ldID, "Đăng nhập Facebook thành công...");
                        dr.Cells["Message"].Value = "Đăng nhập thành công";
                        acc.TrangThai = "Live";
                        dr.Cells["clStatus"].Value = acc.TrangThai;

                        acc.app = "com.facebook.katana";
                        int delay = (int)numDelayMin.Value;
                        u.setStatus(ldID, "tham gia nhóm thủ công...");
                        dr.Cells["Message"].Value = "tham gia nhóm thủ công";

                        string answer = txtPathAnswer.Text.Trim();
                        Profile_Controller profile = new Profile_Controller();
                        int count = 0;
                        int maxjoin = (int)numGroupJoin.Value;
                        if (chkJoinTrung.Checked)
                        {
                            foreach (string groupid in lines)
                            {
                                
                                u.setStatus(ldID, "Join group : " + groupid);
                                if (ld.scrollJoinGroupbyUID(u, acc, ldID, acc.app, groupid.Trim(), delay, true, answer, token))
                                {
                                    count++;
                                    maxerror = 0;
                                    Delay(delay);
                                    dr.Cells["Message"].Value = count.ToString();
                                    if (count >= maxjoin)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    maxerror++;
                                    if (maxerror > (int)numError.Value)
                                        goto Lb_Next;
                                    ld.check_Facebook_has_stopped(u, ldID, acc, token);
                                } 
                            }

                        }
                        else
                        {
                        Lb_start:
                            if (lines.Count > 0)
                            {
                                string groupid = "";
                                lock (synUID)
                                {
                                    groupid = lines[0];
                                    lines.Remove(groupid);
                                }

                                string datacheck = profile.LoadInfoGroup(groupid, SettingTool.configld.token, null);
                                if (datacheck == "Ok")
                                {
                                    u.setStatus(ldID, "Join group : " + groupid);
                                    if (ld.scrollJoinGroupbyUID(u,acc,ldID, acc.app, groupid, delay, true, answer, token))
                                    {
                                        maxerror = 0;
                                        count++;
                                        dr.Cells["Message"].Value = count.ToString();
                                        Delay(delay);

                                        if (count >= maxjoin)
                                        {
                                            method_log("Hoàn thành tham gia nhóm");
                                            goto Lb_Next;
                                        }
                                    }
                                    else
                                    { 
                                        if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token)==false)
                                        {  
                                            goto Lb_Next;
                                             
                                        }
                                        maxerror++;
                                        if (maxerror > (int)numError.Value)
                                            goto Lb_Next;
                                       
                                    }

                                }
                                else
                                {
                                    if (datacheck.Contains("token"))
                                    {
                                        dr.Cells["Message"].Value = datacheck;
                                        method_log(string.Format("Group : {0} - {1}", groupid, datacheck));
                                        goto Lb_Next;
                                    }
                                    else
                                    {
                                        method_log(string.Format("Group : {0} - {1}", groupid, datacheck));
                                    }

                                }
                                if (count < maxjoin)
                                {
                                    goto Lb_start;
                                }
                            }
                        }
                    Lb_Next:
                        u.setStatus(ldID, "Đang backup dataprofile");
                        ld.Zip(acc, ldID);                    
                        changeColor(dr, Color.White);

                    }
                    else
                    {
                        if (acc.Thongbao != null)
                            dr.Cells["Message"].Value = acc.Thongbao;
                        else
                            dr.Cells["Message"].Value = "Đăng nhập thất bại";
                        u.setStatus(ldID, "Đăng nhập Facebook thất bại...");
                        dr.Cells["Message"].Value = "Die";
                        acc.TrangThai = "Die";
                        //  dr.Cells[0].Value = false;
                    }

                    nguoidung.updateNoti(acc);
                    changeColor(dr, Color.White);

                }
                catch
                { }

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
            if (changeIpHelper.checkGetProxyWaitAny())
            {
                xcontroller.finishProxy(proxy);
            }
            ld.quit(acc, ldID);
            frm_main.removeLDToPanel(u);

        }
        private void AddFriendbyUID(string deviceid, DataGridViewRow dr, Account acc, int numFriend, int delay, CancellationToken token)
        {
            string uid;
            int count = 0;

            string path = String.Format("{0}\\logs\\{1}_add.txt", Application.StartupPath, acc.email);
            string historyadd = "";
            try
            {
                historyadd = File.ReadAllText(path);
            }
            catch { }
            if (acc.pathUID == "")
            {
                sendLogs("Chưa thiết lập file UID cho account " + acc.email);
                return;
            }
            List<string> list_uid = new List<string>();
            list_uid = File.ReadAllLines(acc.pathUID).ToList().Distinct().ToList();
            if (list_uid.Count <= 0)
            {
                sendLogs("Vui lòng thêm UID: " + acc.pathUID);
                return;
            }
            StringBuilder list_history = new StringBuilder();
            int int_loilientiep = 0;
            while (count < numFriend)
            {
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
                    sendLogs(String.Format("Tài Khoan {0} kết bạn với {1} thành công", acc.email, uid));

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
                if (count >= numFriend)
                {
                    goto Lb_FinishAcc;
                }
            }
        Lb_FinishAcc:
            sendLogs("Finish" + acc.email);
            File.AppendAllText(path, list_history.ToString());
            File.WriteAllLines(acc.pathUID, list_uid);
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



        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            method_Stop();
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

        private void pibStatus_Click(object sender, EventArgs e)
        {

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

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathAnswer.Text = dialog.FileName;
            }
        }

        private void richLogs_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=cIaLcxcnuN8");
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
            this.Text = "Join group";
            label1.Text = "List ID group, 1 ID 1 line ";

            label32.Text = "Auto answer question";
        }

        private void dgvUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row2 in dgvUser.SelectedRows)
            {
                row2.Cells[0].Value = true;

            }
        }

        private void dgvUser_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void txtUID_MouseClick(object sender, MouseEventArgs e)
        {

        }

    }
}
