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
    public partial class frm_LoginMessenger : Form
    {
        public frm_LoginMessenger(List<Account> list_acc,frm_MainLD frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        frm_MainLD frm_main;
        List<Account> list_acc;
        List<LDRun> list_ldrun = new List<LDRun>();
        List<string> list_ld = new List<string>();
        bool stop = false;
        Thread thread_1;
        object synAcc = new object();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        LDController ld = new LDController();
      //  runLDs formLD = new runLDs();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        private void LoginMessenger_Load(object sender, EventArgs e)
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
            tokenSource = new CancellationTokenSource();
            list_ldrun = new List<LDRun>();
            list_ld = new List<string>();

            startTuongTac();
        }
        private void startTuongTac()
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
                    this.thread_1 = new Thread(new ThreadStart(this.runLogin));
                    thread_1.IsBackground = true;
                    this.thread_1.Start();
                }
                

            }
        }
        xProxyController xcontroller = new xProxyController();
        private void runLoginWaitAny()
        {
            var token = tokenSource.Token;            
       
            TinSoftModel tinsoft = new TinSoftModel();
            tinsoft.success = false;
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
                while (TaskController.checkAvailableTask(list_task))
                {
                    if (stop == false)
                    {
                        int index = TaskController.getAvailableTask(list_task);
                        if (index >= 0)
                        {

                            Task task = Task.Factory.StartNew(() =>
                            {
                                if (list_ld.Count > 0)
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
                                }
                            }, token);
                            list_task[index] = task;
                        }

                        Thread.Sleep(SettingTool.configld.timedelay * 1000);
                    }
                }
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
                    stopLogin();
                }
            }

        }
        private void runLogin()
        {
            var token = tokenSource.Token;
        Lb_quayvong:
            TinSoftModel tinsoft = new TinSoftModel();
            tinsoft.success = false;
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
                        if (stop == false)
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
                            method_Start(ldid, list_acc,proxy,token);
                        }

                    },token);
                    Thread.Sleep(SettingTool.configld.timedelay * 1000);
                }
                try
                {
                    Task.WaitAll(tasks);

                }
                catch { }
                
                if (list_ld.Count > 0)
                {
                    goto Lb_quayvong;
                }
                else
                {
                    stopLogin();
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
        private void stopLogin()
        {
            pibStatus.Visible = false;
            stop = true;
            if (thread_1 != null)
                thread_1.Abort();


        }
        private void method_Start(string ldID, List<DataGridViewRow> list_acc,string proxy, CancellationToken token)
        {
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();
            method_log("Open LDPlayer Id: " + ldID);
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
                    return;
                }
            }
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
                u.setDevice(ldID, proxy);
                u.setStatus(ldID, "Change proxy : " + proxy);
                if (SettingTool.configld.sock5 && SettingTool.configld.typeip == 7)
                {
                    SettingTool.configld.proxytype = "socks5";
                    ld.setProxyAuthentica_proxydroid(ldID, proxy, token);
                    string yourip = ld.checkIPSock5(proxy);
                    u.setDevice(ldID, proxy + " - " + yourip);

                }
                else
                {
                    changeIpHelper.changeProxyAdb(ldID, proxy);
                    //check ip
                    string yourip = ld.checkIP(proxy);
                    u.setDevice(ldID, proxy + " - " + yourip);
                }

            }
            changeIpHelper.connectAfterOpen(u, richLogs, ldID, token);
            #endregion
          

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
                        if (stop)
                            goto Lb_Finish;
                        changeColor(dr, Color.Yellow);
                        dr.Cells["Message"].Value = "Running";
                        Account acc = (Account)dr.Tag;
 
                        bool haslogin = false;

                        if (ld.checkApp(ldID, "com.android.adbkeyboard") == false)
                        {
                            string path = Application.StartupPath + "\\app\\ADBKeyboard.apk";
                            if (File.Exists(path))
                            {
                                u.setStatus(ldID, "Cài đặt Adbkeyboard...");
                                ld.installApp(ldID, path);
                                Thread.Sleep(3000);
                            }
                        }
                        if (ld.checkApp(ldID, "com.facebook.orca") == false)
                        {
                            string path = Application.StartupPath + "\\app\\Messenger.apk";
                            if (File.Exists(path))
                            {
                                u.setStatus(ldID, "Cài đặt App Messenger...");
                                ld.installApp(ldID, path);
                                while (ld.checkApp(ldID, "com.facebook.orca") == false)
                                {
                                    Thread.Sleep(3000);
                                }
                                Thread.Sleep(1000);
                            }
                        }
                        ld.setKeyboard(ldID);  
                         dr.Cells["Message"].Value = "Đăng nhập Messenger";
                        u.setStatus(ldID, "Đăng nhập Messenger...");
                        //if (ld.checkAppCurrentMess(acc) == false)
                        //    ld.restoreAccountMess(acc.ldid, acc);
                        ld.killApp(acc.ldid, "com.facebook.orca");
                        ld.runApp(acc.ldid, "com.facebook.orca");
                        Thread.Sleep(5000);
                        haslogin = ld.loginMess(acc,token);
                        if (haslogin)
                        {
                            u.setStatus(ldID, "Đăng nhập Messenger thành công...");
                            dr.Cells["Message"].Value = "Đăng nhập Messenger thành công";
                        } 
                        else
                        {
                            u.setStatus(ldID, "Đăng nhập Messenger không thành công...");
                            dr.Cells["Message"].Value = "Đăng nhập Messenger không thành công";

                        } 
                        changeColor(dr, Color.White);
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
            method_StopAddFriend();
        }
        private void method_StopAddFriend()
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
