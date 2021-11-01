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
    public partial class frm_2Fa_PRO : Form
    {
        public frm_2Fa_PRO(List<Account> list_acc, frm_MainLD_PRO frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
        }
        frm_MainLD_PRO frm_main;
        List<Account> list_acc;

        bool stop = false;
        Thread thread_1;
        object synAcc = new object();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        LDController ld = new LDController();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        int countComplete = 0;

        private void frm_2Fa_Load(object sender, EventArgs e)
        {
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }

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
                this.thread_1 = new Thread(new ThreadStart(this.runLogin));
                thread_1.IsBackground = true;
                this.thread_1.Start();

            }
        }
        private void runLogin()
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
                        method_log(String.Format("Api {0} - IP {1} - Next change {2} - Timout - {3}", ts.api, ts.proxy, ts.next_change, ts.timeout));
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
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();

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
                    return;
                }
            }
            ld.restoredatafb(acc.ldid, acc.id);
            try
            {
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
                ld.setKeyboard(ldID);
                #region doi ip sau khi mo ld thanh cong

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
                changeIpHelper.connectAfterOpen(u, richLogs, ldID,acc, token);
                #endregion
                try
                { 
                    changeColor(dr, Color.Yellow);
                    dr.Cells["Message"].Value = "Running";
                    ld.killApp(acc.ldid, "com.facebook.katana");
                    ld.restoredatafb(acc.ldid, acc.id);

                    ld.runApp(acc.ldid, "com.facebook.katana");
                    u.setStatus(ldID, "Đăng nhập Facebook...");
                    if (chkLogout.Checked)
                    {
                        dr.Cells["Message"].Value = "Logout";
                        ld.logoutLD(acc);
                    }

                    dr.Cells["Message"].Value = "Login";
                 
                    bool haslogin = ld.loginFacebookTuongTac(u,acc, token);
                    if (haslogin)
                    {
                        u.setStatus(ldID, "Đăng nhập Facebook thành công...");
                        dr.Cells["Message"].Value = "Đăng nhập thành công";
                        dr.Cells["clstatus"].Value = "Live";
                        acc.TrangThai = "Live";
                        acc.Thongbao = "Đăng nhập thành công";
                        Thread.Sleep(1000);
                        if (chkTwoFA.Checked)
                        {
                            u.setStatus(ldID, "Đang bật bảo mật 2 lớp...");
                            dr.Cells["Message"].Value = "Đang bật bảo mật 2 lớp";
                            string mess = ld.bat_2fa(acc.ldid, acc, "com.facebook.katana", token);
                            dr.Cells["Message"].Value = mess;
                            u.setStatus(ldID, mess);
                        }

                        if (chkDeleteDevices.Checked)
                        {
                            u.setStatus(ldID, "Xóa các thiết bị đã đăng nhập...");
                            dr.Cells["Message"].Value = "Xóa các thiết bị đã đăng nhập";
                            string mess = ld.deleteDevices(acc.ldid, acc, "com.facebook.katana", token);
                            dr.Cells["Message"].Value = mess;
                            u.setStatus(ldID, mess);
                        }


                        if (chkEmail.Checked)
                        {
                            u.setStatus(ldID, "Đang thêm email...");
                            dr.Cells["Message"].Value = "Đang thêm Email";
                            string mess = ld.addEmail10phut(acc.ldid, acc, "com.facebook.katana", token);
                            dr.Cells["Message"].Value = mess;
                            u.setStatus(ldID, mess);
                        } 
                        u.setStatus(ldID, "Đang backup dataprofile");
                        ld.Zip(acc, ldID);
                    }
                    else
                    {
                        u.setStatus(ldID, "Đăng nhập Facebook thất bại...");
                        dr.Cells["Message"].Value = "Die";
                        acc.TrangThai = "Die";
                        dr.Cells["clstatus"].Value = "Die";
                    }
                    changeColor(dr, Color.White);
                    nguoidung.updateNoti(acc);
                    //change IP
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
                    //        u.setStatus(ldID, "Đang thay đổi IP...");
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
                    //            Thread.Sleep(2000);
                    //            i--;
                    //        }
                    //    }
                    //}


                }
                catch
                {
                }
            }
            catch
            { }
           // u.setStatus(ldID, "Backup Profile LD...");
           
            if (string.IsNullOrEmpty(proxy) == false)
            {
                ld.setProxyAdb(ldID, ":0");
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

        private void richLogs_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=gcbYvvF7LCo");
        }
        private void setupLanguage()
        {
            this.Text = "Two-Factor Authentication";
            chkTwoFA.Text = "Enable Two-Factor Authentication";
            chkDeleteDevices.Text = "Log out of all devices";
            button1.Text = "Run";
            btnInput.Text = "Stop";
            groupBox1.Text = "List Accounts";
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
    }
}
