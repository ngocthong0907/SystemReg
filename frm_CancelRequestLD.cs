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
    public partial class frm_CancelRequestLD : Form
    {
        public frm_CancelRequestLD(List<Account> list_acc, frm_MainLD frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        List<Account> list_acc;
        bool stop = false;
        object synAcc = new object();
        frm_MainLD frm_main;
      
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
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void frm_CancelRequestLD_Load(object sender, EventArgs e)
        {
            method_LoadAccount();
            method_Config();
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
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
            ClearMessage();
            list_ldrun = new List<LDRun>();
            list_ld = new List<string>();

            string pathlog = Application.StartupPath + "\\logs";
            if (!Directory.Exists(pathlog))
            {
                Directory.CreateDirectory(pathlog);
            }
            startTuongTac();
        }
        private void startTuongTac()
        {
            stop = false;

            pibStatus.Visible = true;


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
            else
            {
                MessageBox.Show("Vui lòng chọn cấu hình trước khi chạy tương tác", "Thông báo");
            }
        }
        private void runTuongTacWaitAny()
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
                                       // resetproxy();
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

                    method_Stop();

                }
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

                //if (SettingTool.configld.typeip == 6)
                //{

                //Lb_Start:
                //    method_log("Bắt đầu đổi ip bằng Tinsoft");
                //    TinsoftResult tinsoftresult = changeIpHelper.method_ChangeTinSoft(SettingTool.configld.apitinsoft);

                //    foreach (TinSoftModel ts in tinsoftresult.list_model)
                //    {
                //        method_log(String.Format("Api {0} - IP {1} - Next change {2} - Timout - {3} - {4}", ts.api, ts.proxy, ts.next_change, ts.timeout, ts.description));
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
                            list_ld.RemoveAt(0);
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
                { }

                if (list_ld.Count > 0)
                {
                    goto Lb_quayvong;
                }
                else
                {
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
        private void method_Start(string ldID, List<DataGridViewRow> list_acc, string proxy, CancellationToken token)
        {

            method_log("Open LDPlayer Id: " + ldID);

            userLD u = new userLD(ldID);
            frm_main.addLDToPanel(u);

            if (ld.launchSetPosion(ldID, u, token))
            {
                u.setStatus(ldID, "Open Finish...");
            }
            else
            {
                if (ld.autoRunLDSetPosition(ldID, u, token))
                {
                    u.setStatus(ldID, "Open Finish...");
                }
                else
                {
                    u.setStatus(ldID, "Disconnected...");
                    method_log("Không kết nối được với LD: " + ldID);
                    return;
                }
            }

            DetailLD_BLL detail_bll = new DetailLD_BLL();

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
                        changeColor(dr, Color.Yellow);
                        Account acc = (Account)dr.Tag;
                        if (ld.checkAppCurrent(acc) == false)
                            ld.restoreAccount(acc.ldid, acc);
                        ld.killApp(acc.ldid, "com.facebook.katana");
                        ld.runApp(acc.ldid, "com.facebook.katana");
                        ld.checkOpenFacebookFinish(u, acc.ldid);
                        dr.Cells["Message"].Value = "Login Facebook";
                        u.setStatus(ldID, "Login Facebook...");

                        //ld.check_Facebook_has_stopped(u,acc.ldid, acc, token);
                        bool status = ld.loginFacebookTuongTac(u,acc,token);
                         
                        if (status)
                        {
                            if (stop)
                            {
                                goto Lb_Finish;
                            }
                            dr.Cells["Message"].Value = "Đăng nhập thành công";
                            u.setStatus(ldID, "Đăng nhập thành công...");
                            acc.TrangThai = "Live";
                            dr.Cells["clStatus"].Value = acc.TrangThai;

                            #region bat dau tuong tac
                            string message = "";
                            int delay = (int)numDelay.Value;
                            acc.app = "com.facebook.katana";
                            dr.Cells["Message"].Value = "Huỷ lời mời kết bạn";
                            u.setStatus(ldID, "Đang huỷ lời mời kết bạn");
                            DeviceData device = new DeviceData();
                            device.Serial = acc.Device_mobile;
                            message = ld.viewCancelAddFriend(ldID, acc.app, (int)numCancelRequest.Value, delay, device, token);

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
                                dr.Cells["Message"].Value = "Đăng nhập thất bại";
                            u.setStatus(ldID, "Đăng nhập thất bại...");

                            dr.Cells["Status"].Value = "Die";
                            acc.TrangThai = "Die";
                            changeColor(dr, Color.White);
                        }
                        NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                        nguoidung.updateNoti(acc);
                        changeColor(dr, Color.White);
                    }
                }
                catch
                { }
                if (list_acc.Count > 0 && stop == false)
                {
                    goto Lb_Acc;
                }

            }
        Lb_Finish:
            method_log("Dừng LD : " + ldID);
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
                if ((bool)dgvUser.Rows[i].Cells[0].Value)
                    dgvUser.Rows[i].Cells["Message"].Value = "";

                DataGridViewRow dr = dgvUser.Rows[i];
                changeColor(dr, Color.White);

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
            method_Stop();
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
            label3.Text = "Amount want to cancel";
            this.Text = "";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool has_click = false;
            if (checkBox2.Checked)
            {
                has_click = true;
            }
            foreach (DataGridViewRow row2 in dgvUser.Rows)
            {
                row2.Cells[0].Value = has_click;

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
    }
}
