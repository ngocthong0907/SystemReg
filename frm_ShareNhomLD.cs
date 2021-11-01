using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SharpAdbClient;
using System.Text.RegularExpressions;
using System.Diagnostics;
namespace NinjaSystem
{
    public partial class frm_ShareNhomLD : Form
    {
        public frm_ShareNhomLD(List<Account> list_acc, frm_MainLD frm_main)
        {
            InitializeComponent();
            this.list_acc = list_acc;
            this.frm_main = frm_main;
            if (string.IsNullOrEmpty(SettingTool.note) == false)
            {
                if (SettingTool.note.Contains("playlist"))
                {
                    rbPlaylist.Visible = true;
                }
            }
        }
        frm_MainLD frm_main;
        List<Account> list_acc;
        bool stop = false;
        object synAcc = new object();


        ninjaDroidHelper droid = new ninjaDroidHelper();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        Thread thread_1;
        static object syncObjUID = new object();
        //List<string> list_uid = new List<string>();
        Random rd = new Random();
        SettingTuongTac tuongtac = new SettingTuongTac();
        List<LDRun> list_ldrun = new List<LDRun>();
        List<string> list_ld = new List<string>();
        LDController ld = new LDController();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        int countComplete = 0;
        StringBuilder historyShare = new StringBuilder();
        private void ClearMessage()
        {
            foreach (DataGridViewRow row in dgvUser.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    row.Cells["Message"].Value = "";
                    row.Cells["clSuccess"].Value = "0";
                }
                changeColor(row, Color.White);
            }
        }
        int int_tabindex;
        private void btnRun_Click(object sender, EventArgs e)
        {
            if ((int)numWait.Value > (int)numWait_max.Value)
            {
                MessageBox.Show("Lưu ý! Giá trị max phải lớn hơn min");
                return;
            }
            if ((int)numrepeatmin.Value > (int)numrepeatmax.Value)
            {
                MessageBox.Show("Lưu ý! Giá trị max phải lớn hơn min");
                return;
            }
            if ((int)numDelayMin.Value > (int)numDelayMax.Value)
            {
                MessageBox.Show("Lưu ý! Giá trị max phải lớn hơn min");
                return;
            }
            if ((int)numMinDelayClick.Value > (int)numMaxDelayClick.Value)
            {
                MessageBox.Show("Lưu ý! Giá trị max phải lớn hơn min");
                return;
            }

            historyShare = new StringBuilder();
            tokenSource = new CancellationTokenSource();

            SettingTool.commentContent = new CommentContent();

            if (chkcomment.Checked)
            {
                if (txt_contentComment.Text == "")
                {
                    if (SettingTool.configld.language == "English")
                        MessageBox.Show("Let's input comment content");
                    else
                        MessageBox.Show("Hãy nhập nội dung bình luận");
                    return;
                }
            }


            if (chkShareStatic.Checked)
            {
                if (txtGroupName.Text == "")
                {
                    if (SettingTool.configld.language == "English")
                        MessageBox.Show("Let's input group name to share");
                    else
                        MessageBox.Show("Hãy nhập tên nhóm cần share");
                    return;
                }
            }
            ClearMessage();
            if (txt_linkshare.Text != "")
            {
                // btnRun.Enabled = false;
                list_ldrun = new List<LDRun>();
                list_ld = new List<string>();
                startTuongTac();
            }
            else
            {
                if (SettingTool.configld.language == "English")
                    MessageBox.Show("Let's input links to share");
                else
                    MessageBox.Show("Hãy nhập link để share!");
            }

        }
        private void startTuongTac()
        {
            SettingTool.configShare = new configFormShare();
            SettingTool.configShare.link = txt_linkshare.Text;
            SettingTool.configShare.contentShare = txt_contentShare.Text;
            SettingTool.configShare.contentComment = txt_contentComment.Text;

            if (chkLimitAcc.Checked)
            {
                SettingTool.configShare.limit = true;
            }
            else
            {
                SettingTool.configShare.limit = false;
            }
            SettingTool.configShare.numlimit = (int)numLimitAcc.Value;
            if (chkLoopRun.Checked)
            {
                SettingTool.configShare.loop = true;
            }
            else
            {
                SettingTool.configShare.loop = false;
            }
            SettingTool.configShare.timeloop = (int)numWait.Value;
            SettingTool.configShare.delaymin = (int)numDelayMin.Value;
            SettingTool.configShare.delaymax = (int)numDelayMax.Value;

            if (chkLoopRun.Checked)
            {
                SettingTool.configShare.loop = true;
            }
            else
            {
                SettingTool.configShare.loop = false;
            }

            if (chkViewlivestream.Checked)
            {
                SettingTool.configShare.has_view = true;
            }
            else
            {
                SettingTool.configShare.has_view = false;
            }

            SettingTool.configShare.timeviewmin = (int)numViewLivestreamMin.Value;
            SettingTool.configShare.timeviewmax = (int)numViewLivestreamMax.Value;

            if (chkInviteLivestream.Checked)
            {
                SettingTool.configShare.has_invite = true;
            }
            else
            {
                SettingTool.configShare.has_invite = false;
            }
            SettingTool.configShare.num_invitemin = (int)numInviteMin.Value;
            SettingTool.configShare.num_invitemax = (int)numInviteMax.Value;

            if (chkLike.Checked)
            {
                SettingTool.configShare.has_like = true;
            }
            else
            {
                SettingTool.configShare.has_like = false;
            }

            if (chkcomment.Checked)
            {
                SettingTool.configShare.has_comment = true;
            }
            else
            {
                SettingTool.configShare.has_comment = false;
            }

            if (chkShareNewfeed.Checked)
            {
                SettingTool.configShare.has_sharenewfeed = true;
            }
            else
            {
                SettingTool.configShare.has_sharenewfeed = false;
            }

            if (chkShareGroup.Checked)
            {
                SettingTool.configShare.has_sharegroup = true;
            }
            else
            {
                SettingTool.configShare.has_sharegroup = false;
            }

            SettingTool.configShare.num_sharemin = (int)numShareMin.Value;
            SettingTool.configShare.num_sharemax = (int)numShareMax.Value;

            SettingTool.configShare.num_member = (int)numMember.Value;

            if (chkShareRandom.Checked)
            {
                SettingTool.configShare.has_share_random = true;
            }
            else
            {
                SettingTool.configShare.has_share_random = false;
            }

            if (chkShareStatic.Checked)
            {
                SettingTool.configShare.has_share_file = true;
            }
            else
            {
                SettingTool.configShare.has_share_file = false;
            }

            if (chkShareNoApproval.Checked)
            {
                SettingTool.configShare.has_share_noapprove = true;
            }
            else
            {
                SettingTool.configShare.has_share_noapprove = false;
            }

            if (rbShareLive.Checked)
            {
                SettingTool.configShare.typeshare = 1;
            }
            if (rbSharePost.Checked)
            {
                SettingTool.configShare.typeshare = 2;
            }
            if (rbShareVideo.Checked)
            {
                SettingTool.configShare.typeshare = 3;
            }
            if (rbPlaylist.Checked)
            {
                SettingTool.configShare.typeshare = 4;
            }
            SettingTool.configShare.pathgroup = txtGroupName.Text.Trim();
            SettingTool.configShare.delay_click_min = (int)numDelayMin.Value;
            SettingTool.configShare.delay_click_max = (int)numDelayMax.Value;

            string path = String.Format("{0}\\Config\\ConfigFormShare.data", Application.StartupPath);
            File.WriteAllText(path, JsonConvert.SerializeObject(SettingTool.configShare));

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
                if (SettingTool.configld.language == "English")
                    MessageBox.Show("Let's select accounts to run");
                else
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
            int loop = 0;
            int numthread = SettingTool.configld.numthread;
            int maxloop = rd.Next((int)numrepeatmin.Value, (int)numrepeatmax.Value);
            Task[] list_task = TaskController.createTask(numthread);
            xcontroller.createProxy(numthread);

        Lb_quayvong:

            var token = tokenSource.Token;

            if (numthread > list_ld.Count)
            {
                numthread = list_ld.Count;
            }

            if (list_ld.Count > 0)
            {
                TinSoftModel tinsoft = new TinSoftModel();
                tinsoft.success = false;

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
                string mess = txt_contentShare.Text;
                //   List<string> list_link = new List<string>();
                string link = txt_linkshare.Text.Trim();
                //if (link.Contains(","))
                //{
                //    string[] arr = link.Split(',');
                //    foreach (string line in arr)
                //    {
                //        if (String.IsNullOrEmpty(line) == false)
                //        {
                //            list_link.Add(line.Trim());
                //        }
                //    }
                //}
                //else
                //{
                //    list_link.Add(link.Trim());
                //}
                object synDevice = new object();
                int maxproxy = 0;
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
                                            method_Start(ldid, list_acc, link, mess, proxy, token);

                                        }


                                    }, token);
                                    list_task[index] = task;
                                    Thread.Sleep(SettingTool.configld.timedelay * 1000);

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
                                        method_Start(ldid, list_acc, link, mess, proxy, token);
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

                    if (chkLoopRun.Checked)
                    {
                        loop++;
                        if (loop > maxloop)
                            method_Stop();
                        int numwait = rd.Next((int)numWait.Value, (int)numWait_max.Value);
                        method_log(String.Format("Vui lòng đợi {0} giây để tiếp tục", numwait));

                        while (numwait >= 0)
                        {
                            method_log(String.Format("Vui lòng đợi {0} giây để tiếp tục", numwait));
                            Thread.Sleep(1000);
                            numwait--;
                            if (token.IsCancellationRequested)
                                break;
                        }

                        list_dr = new List<DataGridViewRow>();
                        list_ld = new List<string>();

                        foreach (DataGridViewRow row in dgvUser.Rows)
                        {
                            if ((bool)row.Cells[0].Value)
                            {
                                list_dr.Add(row);
                                Account accnew = (Account)row.Tag;
                                list_ld.Add(accnew.ldid);
                            }
                        }

                        if (list_dr.Count > 0)
                        {
                            list_ld = list_ld.Distinct().ToList();
                            method_log(String.Format("Quay vòng {0} tài khoản", list_dr.Count));
                            goto Lb_quayvong;
                        }
                        else
                            method_Stop();
                    }
                    else if (chkrepeatcomment.Checked)
                    {
                        chkViewlivestream.Checked = false;
                        chkLike.Checked = false;
                        chkShareNewfeed.Checked = false;
                        chkShareGroup.Checked = false;
                        chkShareRandom.Checked = false;
                        chkShareStatic.Checked = false;
                        chkShareNoApproval.Checked = false;
                        loop++;
                        if (loop > maxloop)
                            method_Stop();
                        int numwait = rd.Next((int)numWait.Value, (int)numWait_max.Value);
                        while (numwait >= 0)
                        {
                            method_log(String.Format("Vui lòng đợi {0} giây để tiếp tục", numwait));
                            Thread.Sleep(1000);
                            numwait--;
                            if (token.IsCancellationRequested)
                                break;
                        }
                        list_dr = new List<DataGridViewRow>();
                        list_ld = new List<string>();

                        foreach (DataGridViewRow row in dgvUser.Rows)
                        {
                            if ((bool)row.Cells[0].Value)
                            {
                                list_dr.Add(row);
                                Account accnew = (Account)row.Tag;
                                list_ld.Add(accnew.ldid);
                            }
                        }

                        if (list_dr.Count > 0)
                        {
                            list_ld = list_ld.Distinct().ToList();
                            method_log(String.Format("Quay vòng {0} tài khoản", list_dr.Count));
                            goto Lb_quayvong;
                        }
                        else
                            method_Stop();
                    }
                    else
                        method_Stop();
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
        private void runTuongTac()
        {
            int loop = 0;
            int maxloop = rd.Next((int)numrepeatmin.Value, (int)numrepeatmax.Value);
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
                TinSoftModel tinsoft = new TinSoftModel();
                tinsoft.success = false;

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
                    if (SettingTool.configld.typeip == 8)
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

                            method_log("Lỗi lấy proxy TM.Tiếp tục request");
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
                string mess = txt_contentShare.Text.Trim();
                //List<string> list_link = new List<string>();
                string link = txt_linkshare.Text.Trim();

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
                        method_Start(ldid, list_acc, link, mess, proxy, token);
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
                    method_log(String.Format("Total LD {0} - Thread {1}", list_ld.Count, numthread));
                    goto Lb_quayvong;
                }
                else
                {
                    if (chkLoopRun.Checked)
                    {
                        loop++;
                        if (loop > maxloop)
                            method_Stop();
                        int numwait = rd.Next((int)numWait.Value, (int)numWait_max.Value);
                        method_log(String.Format("Vui lòng đợi {0} giây để tiếp tục", numwait));
                        Thread.Sleep(numwait * 1000);
                        list_dr = new List<DataGridViewRow>();
                        list_ld = new List<string>();

                        foreach (DataGridViewRow row in dgvUser.Rows)
                        {
                            if ((bool)row.Cells[0].Value)
                            {
                                list_dr.Add(row);
                                Account accnew = (Account)row.Tag;
                                list_ld.Add(accnew.ldid);
                            }
                        }

                        if (list_dr.Count > 0)
                        {
                            list_ld = list_ld.Distinct().ToList();
                            method_log(String.Format("Quay vòng {0} tài khoản", list_dr.Count));
                            goto Lb_quayvong;
                        }
                        else
                        {
                            method_log("Dừng Share");
                            method_Stop();
                        }

                    }
                    else if (chkrepeatcomment.Checked)
                    {
                        chkViewlivestream.Checked = false;
                        chkLike.Checked = false;
                        chkShareNewfeed.Checked = false;
                        chkShareGroup.Checked = false;
                        chkShareRandom.Checked = false;
                        chkShareStatic.Checked = false;
                        chkShareNoApproval.Checked = false;
                        loop++;
                        if (loop > maxloop)
                            method_Stop();
                        int numwait = rd.Next((int)numWait.Value, (int)numWait_max.Value);
                        method_log(String.Format("Vui lòng đợi {0} giây để tiếp tục", numwait));
                        Thread.Sleep(numwait * 1000);
                        list_dr = new List<DataGridViewRow>();
                        list_ld = new List<string>();

                        foreach (DataGridViewRow row in dgvUser.Rows)
                        {
                            if ((bool)row.Cells[0].Value)
                            {
                                list_dr.Add(row);
                                Account accnew = (Account)row.Tag;
                                list_ld.Add(accnew.ldid);
                            }
                        }

                        if (list_dr.Count > 0)
                        {
                            list_ld = list_ld.Distinct().ToList();
                            method_log(String.Format("Quay vòng {0} tài khoản", list_dr.Count));
                            goto Lb_quayvong;
                        }
                        else
                            method_Stop();
                    }
                    else
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
        private void method_Start(string ldID, List<DataGridViewRow> list_acc, string link, string mess, string proxy, CancellationToken token)
        {
            NguoiDung_Bll nguoidung = new NguoiDung_Bll();

            method_log("Open LDPlayer Id: " + ldID);
            userLD u = frm_main.checkExits(ldID);
            try
            {
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
                //   ld.connectHMA(ldID);
                DetailLD_BLL detail_bll = new DetailLD_BLL();
                DetailLDModel detailLd = new DetailLDModel();

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
                                method_log("Tắt LD do không lấy được ip public proxy: " + proxy);
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
                                method_log("Tắt LD do không lấy được ip public proxy: " + proxy);
                                goto Lb_Finish;

                            }
                        }
                    }
                }

                changeIpHelper.connectAfterOpen(u, richLogs, ldID, token);
                #endregion
                Random rd = new Random();
                if (chkLimitAcc.Checked)
                {
                    if ((int)numLimitAcc.Value < list_acc.Count)
                    {
                        int deleteAcc = list_acc.Count - (int)numLimitAcc.Value;
                        for (int n = 0; n < deleteAcc; n++)
                        {
                            list_acc.RemoveAt(rd.Next(0, list_acc.Count));
                        }
                    }
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
                            string messageSuccess = "";
                            changeColor(dr, Color.Yellow);
                            dr.Cells["Message"].Value = "Running";
                            Account acc = (Account)dr.Tag;
                            if (ld.checkAppCurrent(acc) == false)
                                ld.restoreAccount(acc.ldid, acc);

                            if (chk_skipLoginfb.Checked)
                            {
                                dr.Cells["Message"].Value = "Open link";
                                u.setStatus(ldID, "Open link...");
                                int max_open = 0;
                            start_open:
                                string linkopen = FunctionHelper.method_Spin(link);
                                if (linkopen.Contains("http"))
                                {
                                    if (chklinkprofile.Checked)
                                        ld.openlivestreamprofile(acc.ldid, linkopen);
                                    else
                                    {
                                        string result = ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                                        if (result.Contains("Error") || result == "")
                                        {
                                            dr.Cells["Message"].Value = "link không mở được";
                                            goto LD_Next;
                                        }
                                    }
                                }
                                else
                                {
                                    u.setStatus(ldID, "Đang tìm livestream");
                                    if (!ld.searchlivestream(acc, dr, link, token))
                                    {
                                        u.setStatus(ldID, "Không tìm được livestream");
                                        dr.Cells["Message"].Value = "Không tìm được livestream";
                                        goto LD_Next;
                                    }

                                    
                                }
                                    
                                int delay = (int)numdelayopenlink.Value;
                                while (delay >= 0)
                                {

                                    dr.Cells["Message"].Value = "Delay open link " + delay.ToString() + "s";
                                    u.setStatus(ldID, "Delay open link..." + delay.ToString() + "s");
                                    Thread.Sleep(1000);
                                    delay--;
                                    if (token.IsCancellationRequested)
                                        break;
                                }
                                dr.Cells["Message"].Value = " ";
                                u.setStatus(ldID, " ");

                                DetechModel screen = ld.checkScreen(acc.ldid);
                                if (screen.status)
                                {
                                    if (!ld.checkContentLD(ldID, "khóa tài khoản|Web View"))
                                    {
                                        if (screen.parent != "login" & screen.parent != "loading" & screen.parent != "loginavatar")
                                        {
                                            messageSuccess = perform(u, acc, dr, link, mess, token);
                                            dr.Cells["Message"].Value = messageSuccess;
                                            u.setStatus(ldID, messageSuccess);
                                            goto LD_Next;
                                        }
                                    }
                                }
                                else
                                {
                                    max_open++;
                                    if (max_open < 2)
                                        goto start_open;
                                }

                            }
                            ld.killApp(acc.ldid, "com.facebook.katana");
                            ld.runApp(acc.ldid, "com.facebook.katana");
                            // ld.checkOpenFacebookFinish(u, acc.ldid);
                            Thread.Sleep(6000);
                            dr.Cells["Message"].Value = "Login Facebook";
                            u.setStatus(ldID, "Login Facebook...");

                            bool status = ld.loginFacebookTuongTac(u, acc, token);

                            if (status)
                            {
                                u.setStatus(ldID, "Login Facebook successful...");
                                dr.Cells["Message"].Value = "Login Facebook successful";
                                acc.TrangThai = "Live";
                                acc.Thongbao = "Login Facebook successful";

                                messageSuccess = perform(u, acc, dr, link, mess, token);
                                dr.Cells["Message"].Value = messageSuccess;
                                u.setStatus(ldID, messageSuccess);

                                nguoidung.updateLastRun(acc);

                                //change IP
                                countComplete++;
                            }
                            else
                            {
                                dr.Cells["Message"].Value = "Login Facebook fail";
                                u.setStatus(ldID, "Login Facebook fail...");
                                //   dr.Cells["Status"].Value = "Die";
                                acc.TrangThai = "Die";
                                acc.Thongbao = "Login Facebook fail";
                            }

                        LD_Next:
                            changeColor(dr, Color.White);
                            nguoidung.updateStatus(acc);
                            if (chkLimitAcc.Checked)
                                dr.Cells[0].Value = false;
                        }
                    }
                    catch
                    { }
                    if (list_acc.Count > 0 && stop == false)
                    {
                        goto Lb_Acc;
                    }

                }
            }
            catch
            {

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
        private string perform(userLD u, Account acc, DataGridViewRow dr, string link, string mess, CancellationToken token)
        {
            string ldID = acc.ldid;
            string messageSuccess = "";
            try
            {

                List<GroupFB> ls_groupname = new List<GroupFB>();
                List<GroupFB> ls_group = new List<GroupFB>();
                if (chkShareNoApproval.Checked || chkShareStatic.Checked)
                {
                    LDController controler = new LDController();
                    string access_token = controler.getToken(acc);
                    Profile_Controller profile = new Profile_Controller();
                    ls_group = profile.LoadInfoGroup(access_token, "", acc, null);
                    if (ls_group.Count == 0)
                    {
                        // dr.Cells["Message"].Value = " Chưa tham gia nhóm hoặc lấy thông tin nhóm không thành công";
                        // return " Chưa tham gia nhóm hoặc lấy thông tin nhóm không thành công";//goto LD_Next;
                    }
                    int member = (int)numMember.Value;
                    foreach (GroupFB gr in ls_group)
                    {
                        if (gr.member > member)
                        {
                            if (gr.status == "CAN_POST_WITHOUT_APPROVAL")
                            {
                                ls_groupname.Add(gr);
                            }
                        }
                    }
                }
                //bat dau share
                //buoc 1 mo link share
                int loop = 0;
                int sharesucess = 0;
                string linkopen = FunctionHelper.method_Spin(link);
                if (!chk_skipLoginfb.Checked)
                {
                    if (linkopen.Contains("http"))
                    {
                        if (chklinkprofile.Checked)
                            ld.openlivestreamprofile(acc.ldid, linkopen);
                        else
                        {
                            string result = ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                            if (result.Contains("Error") || result == "")
                            {
                                dr.Cells["Message"].Value = "link không mở được";
                                return "link không mở được"; //  goto LD_Next;
                            }
                        }
                    }
                    else
                    {
                        u.setStatus(ldID, "Đang tìm livestream");
                        if (!ld.searchlivestream(acc, dr, link, token))
                        {
                            u.setStatus(ldID, "Không tìm được livestream");
                            dr.Cells["Message"].Value = "Không tìm được livestream";
                            return "Không tìm được livestream";
                        }
                    }
                }

                if (rbShareLive.Checked)
                {
                    #region share live
                    //share livestream
                    if (chkViewlivestream.Checked)
                    {
                        if (!ld.checkscreenLivestream(acc))
                        {
                            if (link.Contains("http"))
                            {
                                if (chklinkprofile.Checked)
                                    ld.openlivestreamprofile(acc.ldid, link);
                                else
                                    ld.OpenLink(acc.ldid, "com.facebook.katana", link);
                            }
                            else
                            {
                                u.setStatus(ldID, "Đang tìm livestream");
                                if (!ld.searchlivestream(acc, dr, link, token))
                                {
                                    u.setStatus(ldID, "Không tìm được livestream");
                                    dr.Cells["Message"].Value = "Không tìm được livestream";
                                    return "Không tìm được livestream";
                                }
                            }
                        }

                        //view live
                        if (SettingTool.configld.language == "English")
                        {
                            dr.Cells["Message"].Value = "Watching livestream";
                            u.setStatus(ldID, "Watching livestream...");
                        }
                        else
                        {
                            dr.Cells["Message"].Value = "Đang xem livestream";
                            u.setStatus(ldID, "Đang xem livestream...");
                        }

                        int delay = rd.Next((int)numViewLivestreamMin.Value, (int)numViewLivestreamMax.Value);
                        if (SettingTool.configld.language == "English")
                            messageSuccess += "| Watched livestream " + delay + " seconds";
                        else
                            messageSuccess += "| Đã xem livestream " + delay + " giây";

                        DetechModel data = new DetechModel();
                        data.parent = "fullscreen";
                        data.content = "Toàn màn hình";
                        data.text = "Toàn màn hình";
                        data.node = "//node[contains(@class,'android.view.View')]";
                        data.function = 1;
                        List<DetechModel> ls_full = new List<DetechModel>();
                        ls_full.Add(data);
                        try
                        {
                            while (delay >= 0)
                            {
                                if (token.IsCancellationRequested)
                                    token.ThrowIfCancellationRequested();
                                dr.Cells["Message"].Value = "Đang xem livestream " + delay.ToString() + "s";
                                u.setStatus(ldID, "Đang xem livestream..." + delay.ToString() + "s");
                                Thread.Sleep(1000);
                                delay--;
                            }
                        }
                        catch
                        { }
                    }

                    if (chkLike.Checked)
                    {
                        dr.Cells["Message"].Value = " Like livestream";
                        u.setStatus(ldID, " Like livestream...");
                        if (ld.seedingLikeLivesteam(acc.ldid, token))
                        {
                            u.setStatus(ldID, " Like Success...");
                            int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                            Thread.Sleep(delay * 1000);
                            messageSuccess += "Like success ";
                        }

                    }
                    if (chkcomment.Checked)
                    {
                        dr.Cells["Message"].Value = "Comment livestream";
                        u.setStatus(ldID, "Comment livestream...");
                        string description = txt_contentComment.Text.Trim();
                        if (ld.seedingCommentLivesteam(acc.ldid, description, link, token))
                        {
                            u.setStatus(ldID, "Comment Success...");
                            dr.Cells["Message"].Value = " Comment Success";
                            int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                            Thread.Sleep(delay * 1000);
                            messageSuccess += " Comment success ";
                        }
                    }
                    if (chkShareNewfeed.Checked)
                    {
                        dr.Cells["Message"].Value = "Share tường cá nhân";
                        u.setStatus(ldID, "Share tường cá nhân...");
                        messageSuccess += ld.ShareLiveProfile(u, acc, dr, link, mess, (int)numDelayMin.Value, token);
                        int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                        Thread.Sleep(delay * 1000);
                    }
                    if (chkShareGroup.Checked || chkShareRandom.Checked || chkShareStatic.Checked || chkShareNoApproval.Checked)
                    {
                        dr.Cells["Message"].Value = "Share Group";
                        u.setStatus(ldID, "Share group...");
                        int numshare = rd.Next((int)numShareMin.Value, (int)numShareMax.Value);
                        int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                        if (!ld.checkscreenLivestream(acc))
                        {
                            if (link.Contains("http"))
                            {
                                if (chklinkprofile.Checked)
                                    ld.openlivestreamprofile(acc.ldid, link);
                                else
                                    ld.OpenLink(acc.ldid, "com.facebook.katana", link);
                            }
                            else
                            {
                                u.setStatus(ldID, "Đang tìm livestream");
                                if (!ld.searchlivestream(acc, dr, link, token))
                                {
                                    u.setStatus(ldID, "Không tìm được livestream");
                                    dr.Cells["Message"].Value = "Không tìm được livestream";
                                    return "Không tìm được livestream";
                                }
                            }
                        }
                        u.setStatusSum(numshare);
                        //share live
                        if (chkShareRandom.Checked)
                        {
                            //share livestream vao group
                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, "Share livestream random group...");
                                dr.Cells["Message"].Value = "Share livestream random group";
                            }
                            else
                            {
                                u.setStatus(ldID, "Share livestream vào nhóm ngẫu nhiên...");
                                dr.Cells["Message"].Value = "Share livestream vào nhóm ngẫu nhiên";
                            }
                            while (loop <= (int)numnaxLoop.Value)
                            {
                                if (loop > 0)
                                {
                                    if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                    {
                                        u.setStatus(ldID, "Share livestream vào nhóm ngẫu nhiên...");
                                    }

                                    if (!ld.checkscreenLivestream(acc))
                                    {
                                        if (link.Contains("http"))
                                        {
                                            if (chklinkprofile.Checked)
                                                ld.openlivestreamprofile(acc.ldid, link);
                                            else
                                                ld.OpenLink(acc.ldid, "com.facebook.katana", link);
                                        }
                                        else
                                        {
                                            u.setStatus(ldID, "Đang tìm livestream");
                                            if (!ld.searchlivestream(acc, dr, link, token))
                                            {
                                                u.setStatus(ldID, "Không tìm được livestream");
                                                dr.Cells["Message"].Value = "Không tìm được livestream";
                                                return "Không tìm được livestream";
                                            }
                                        }
                                    }
                                }
                                if (link.Contains("http"))
                                    sharesucess += ld.ShareLiveStreamGroup(u, acc, dr, link, mess, numshare - sharesucess, delay, token);
                                else
                                    sharesucess += ld.ShareLiveStreamGroupbyName(u, acc, dr, link, mess, numshare - sharesucess, delay, token);

                                u.setStatusSum(numshare);
                                u.setStatusResult(sharesucess);
                                dr.Cells["clSuccess"].Value = sharesucess;
                                if (sharesucess >= numshare)
                                    break;

                                loop++;
                            }
                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, " Share random group: " + sharesucess.ToString() + "/" + numshare.ToString());
                                messageSuccess += "Share random group: " + sharesucess.ToString() + "/" + numshare.ToString();
                                dr.Cells["Message"].Value = messageSuccess;
                            }
                            else
                            {
                                u.setStatus(ldID, " Share vào nhóm ngẫu nhiên: " + sharesucess.ToString() + "/" + numshare.ToString());
                                messageSuccess += " Share vào nhóm ngẫu nhiên: " + sharesucess.ToString() + "/" + numshare.ToString();
                                dr.Cells["Message"].Value = messageSuccess;
                            }
                        }
                        if (chkShareNoApproval.Checked)
                        {
                            if (ls_groupname.Count > 0)
                            {
                                if (numshare > ls_groupname.Count)
                                {
                                    numshare = ls_groupname.Count;
                                }
                                u.setStatusSum(numshare);
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, "Share group don't approval...");
                                    dr.Cells["Message"].Value = "Share group don't approval";
                                }
                                else
                                {
                                    u.setStatus(ldID, " Share nhóm không cần duyệt...");
                                    dr.Cells["Message"].Value = "Share nhóm không cần duyệt";
                                }
                                while (loop <= (int)numnaxLoop.Value)
                                {
                                    if (loop > 0)
                                    {
                                        if (ls_groupname.Count <= 0)
                                        {
                                            break;
                                        }
                                        if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                        {
                                            u.setStatus(ldID, "Lỗi đăng nhập Facebook");
                                            break;
                                        }
                                        //kiem tra xem da vao livestream chua
                                        if (!ld.checkscreenLivestream(acc))
                                        {
                                            if (link.Contains("http"))
                                            {
                                                if (chklinkprofile.Checked)
                                                    ld.openlivestreamprofile(acc.ldid, link);
                                                else
                                                    ld.OpenLink(acc.ldid, "com.facebook.katana", link);
                                            }
                                            else
                                            {
                                                u.setStatus(ldID, "Đang tìm livestream");
                                                if (!ld.searchlivestream(acc, dr, link, token))
                                                {
                                                    u.setStatus(ldID, "Không tìm được livestream");
                                                    dr.Cells["Message"].Value = "Không tìm được livestream";
                                                    return "Không tìm được livestream";
                                                }
                                            }
                                        }
                                    }
                                    sharesucess += ld.ShareLiveStreamGroup_NoApproval(u, acc, dr, link, mess, numshare - sharesucess, delay, ls_groupname, ref historyShare, token);
                                    dr.Cells["clSuccess"].Value = sharesucess;
                                    u.setStatusSum(numshare);
                                    u.setStatusResult(sharesucess);
                                    if (sharesucess >= numshare)
                                        break;
                                    loop++;
                                    if (ls_groupname.Count <= 0)
                                    {
                                        break;
                                    }
                                }
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, " Share group don't approval: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " Share group don't approval: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                                else
                                {
                                    u.setStatus(ldID, "Share nhóm không cần duyệt: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += "Share nhóm không cần duyệt: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                            }
                            else
                            {
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, "No avaiable don't approval group...");
                                    dr.Cells["Message"].Value = "No avaiable don't approval group";
                                    messageSuccess += "No avaiable don't approval group";
                                }
                                else
                                {
                                    u.setStatus(ldID, "Không có nhóm không cần duyệt...");
                                    dr.Cells["Message"].Value = "Không có nhóm không cần duyệt";
                                    messageSuccess += "Không có nhóm không cần duyệt";
                                }
                            }
                        }
                        if (chkShareStatic.Checked)
                        {
                            List<GroupFB> list_groupshare = new List<GroupFB>();
                            if (File.Exists(txtGroupName.Text))
                            {
                                string history = File.ReadAllText(txtGroupName.Text);
                                foreach (GroupFB gr in ls_group)
                                {
                                    if (history.Contains(gr.id) && historyShare.ToString().Contains(gr.id) == false)
                                    {
                                        list_groupshare.Add(gr);
                                    }
                                }
                            }

                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, "Share livestream into static group...");
                                dr.Cells["Message"].Value = "Share livestream into static group";
                            }
                            else
                            {
                                u.setStatus(ldID, "Share livestream vào nhóm cố định...");
                                dr.Cells["Message"].Value = "Share livestream vào nhóm cố định";
                            }

                            if (list_groupshare.Count > 0)
                            {
                                while (loop <= (int)numnaxLoop.Value)
                                {
                                    if (list_groupshare.Count == 0)
                                        break;
                                    if (loop > 0)
                                    {
                                        if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                        {
                                            u.setStatus(ldID, "Lỗi đăng nhập Facebook");
                                            break;
                                        }
                                        //kiem tra xem da vao livestream chua
                                        if (!ld.checkscreenLivestream(acc))
                                        {
                                            if (link.Contains("http"))
                                            {
                                                if (chklinkprofile.Checked)
                                                    ld.openlivestreamprofile(acc.ldid, link);
                                                else
                                                    ld.OpenLink(acc.ldid, "com.facebook.katana", link);
                                            }
                                            else
                                            {
                                                u.setStatus(ldID, "Đang tìm livestream");
                                                if (!ld.searchlivestream(acc, dr, link, token))
                                                {
                                                    u.setStatus(ldID, "Không tìm được livestream");
                                                    dr.Cells["Message"].Value = "Không tìm được livestream";
                                                    return "Không tìm được livestream";
                                                }
                                            }
                                        }
                                    }
                                    sharesucess += ld.ShareLiveStreamGroup_NoApproval(u, acc, dr, link, mess, numshare - sharesucess, delay, list_groupshare, ref historyShare, token);
                                    dr.Cells["clSuccess"].Value = sharesucess;
                                    u.setStatusSum(numshare);
                                    u.setStatusResult(sharesucess);
                                    if (sharesucess >= numshare)
                                        break;

                                    loop++;
                                }

                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, "Share into static group: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += "Share into static group: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                                else
                                {
                                    u.setStatus(ldID, "Share vào nhóm cố định: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += "Share vào nhóm cố định: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                            }
                            else
                            {
                                if (SettingTool.configld.language == "English")
                                {
                                    messageSuccess = "Let's choose file group name ";
                                    dr.Cells["Message"].Value = "Let's choose file group UID";
                                }
                                else
                                {
                                    messageSuccess += "Không có nhóm đầu vào";
                                    dr.Cells["Message"].Value = "Không có nhóm đầu vào";
                                }
                            }
                        }
                        if (chkLoopView.Checked)
                        {

                            //view live
                            if (SettingTool.configld.language == "English")
                            {
                                dr.Cells["Message"].Value = "Watching livestream";
                                u.setStatus(ldID, "Watching livestream...");
                            }
                            else
                            {
                                dr.Cells["Message"].Value = "Đang xem livestream";
                                u.setStatus(ldID, "Đang xem livestream...");
                            }
                            delay = rd.Next((int)numViewLivestreamMin.Value, (int)numViewLivestreamMax.Value);
                            if (SettingTool.configld.language == "English")
                                messageSuccess += "| Watched livestream " + delay + " seconds";
                            else
                                messageSuccess += "| Đã xem livestream " + delay + " giây";

                            try
                            {
                                while (delay >= 0)
                                {
                                    dr.Cells["Message"].Value = "Đang xem livestream " + delay.ToString() + "s";
                                    u.setStatus(ldID, "Đang xem livestream..." + delay.ToString() + "s");
                                    Thread.Sleep(1000);
                                    delay--;
                                }
                            }
                            catch
                            { }
                        }
                    }
                    #endregion
                }
                if (rbSharePost.Checked)
                {
                    ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                    if (linkopen.Contains("groups"))
                        ld.openlinkgroup(acc.ldid);

                    #region share post
                    //share livestream
                    if (chkViewlivestream.Checked)
                    {
                        //view live
                        int delay = rd.Next((int)numViewLivestreamMin.Value, (int)numViewLivestreamMax.Value);
                        if (SettingTool.configld.language == "English")
                        {
                            dr.Cells["Message"].Value = "Watching Post";
                            u.setStatus(ldID, "Watching Post...");
                        }
                        else
                        {
                            dr.Cells["Message"].Value = "Đang view Post";
                            u.setStatus(ldID, "Đang view Post...");
                        }

                        while (delay >= 0)
                        {
                            dr.Cells["Message"].Value = "Đang xem video " + delay.ToString() + "s";
                            u.setStatus(ldID, "Đang xem video..." + delay.ToString() + "s");

                            Thread.Sleep(1000);
                            delay--;
                        }

                        if (SettingTool.configld.language == "English")

                            messageSuccess += "| Watched Post " + delay + " seconds";
                        else
                            messageSuccess += "| Đã view Post " + delay + " giây";

                    }
                    if (chkLike.Checked)
                    {
                        dr.Cells["Message"].Value = "Like Post";
                        u.setStatus(ldID, "Like Post...");
                        if (ld.seedingLike(acc.ldid, token))
                        {
                            u.setStatus(ldID, "Like Success...");
                            messageSuccess += "|Like Success";
                            int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                            Thread.Sleep(delay * 1000);
                        }
                        else
                        {
                            u.setStatus(ldID, "Like not Success...");
                            messageSuccess += "|Like not Success";
                        }
                    }
                    if (chkcomment.Checked)
                    {
                        dr.Cells["Message"].Value = "Comment Post";
                        u.setStatus(ldID, "Comment Post...");
                        string description = txt_contentComment.Text.Trim();
                        if (ld.commentPost(u, acc, acc.ldid, description, token))
                        {
                            ld.back(acc.ldid, 2);
                            u.setStatus(ldID, "|Comment Success...");
                            dr.Cells["Message"].Value = "Comment Success";
                            messageSuccess += "| Comment Success";
                            int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                            Thread.Sleep(delay * 1000);

                        }
                        else
                        {
                            u.setStatus(ldID, "|Comment not Success...");
                            dr.Cells["Message"].Value = "Comment not Success";
                            messageSuccess += "Comment not Success";
                        }
                    }
                    if (chkShareNewfeed.Checked)
                    {
                        ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                        if (linkopen.Contains("groups"))
                            ld.openlinkgroup(acc.ldid);

                        dr.Cells["Message"].Value = "Share tường cá nhân";
                        u.setStatus(ldID, "Share tường cá nhân...");
                        if (ld.sharepost2profile(acc.ldid, "com.facebook.katana", mess, token))
                            messageSuccess += " |Share tường thành công";
                        else
                            messageSuccess += " |Share tường không thành công"; ;
                        int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                        Thread.Sleep(delay * 1000);
                    }
                    if (chkShareGroup.Checked || chkShareRandom.Checked || chkShareStatic.Checked || chkShareNoApproval.Checked)
                    {
                        //ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                        //if (linkopen.Contains("groups"))
                        //    ld.openlinkgroup(acc.ldid);

                        dr.Cells["Message"].Value = "Share Group";
                        u.setStatus(ldID, "Share group...");
                        int numshare = rd.Next((int)numShareMin.Value, (int)numShareMax.Value);
                        int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                        //share live
                        if (chkShareRandom.Checked)
                        {
                            //share livestream vao group
                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, " Share random group...");
                                dr.Cells["Message"].Value = " Share random group";
                            }
                            else
                            {
                                u.setStatus(ldID, " Share vào nhóm ngẫu nhiên...");
                                dr.Cells["Message"].Value = " Share vào nhóm ngẫu nhiên";
                            }

                            while (loop <= (int)numnaxLoop.Value)
                            {
                                if (loop > 0)
                                {
                                    if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                    {
                                        u.setStatus(ldID, "Lỗi đăng nhập Facebook");
                                        break;
                                    }
                                    ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                                    if (linkopen.Contains("groups"))
                                        ld.openlinkgroup(acc.ldid);
                                }
                                sharesucess += ld.SharePostGroup(u, acc, dr, link, mess, numshare - sharesucess, delay, token);
                                dr.Cells["clSuccess"].Value = sharesucess;
                                if (sharesucess >= numshare)
                                    break;
                                loop++;
                            }
                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, " Share random group: " + sharesucess.ToString() + "/" + numshare.ToString());
                                messageSuccess += " Share random group: " + sharesucess.ToString() + "/" + numshare.ToString();
                                dr.Cells["Message"].Value = messageSuccess;
                            }
                            else
                            {
                                u.setStatus(ldID, " Share vào nhóm ngẫu nhiên: " + sharesucess.ToString() + "/" + numshare.ToString());
                                messageSuccess += " Share vào nhóm ngẫu nhiên: " + sharesucess.ToString() + "/" + numshare.ToString();
                                dr.Cells["Message"].Value = messageSuccess;
                            }
                        }
                        if (chkShareNoApproval.Checked)
                        {
                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, " Share group don't approval ...");
                                dr.Cells["Message"].Value = " Share group don't approval";
                            }
                            else
                            {
                                u.setStatus(ldID, " Share vào nhóm không cần duyệt...");
                                dr.Cells["Message"].Value = " Share vào nhóm không cần duyệt";
                            }

                            ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                            if (linkopen.Contains("groups"))
                                ld.openlinkgroup(acc.ldid);
                            if (numshare > ls_groupname.Count)
                            {
                                numshare = ls_groupname.Count;
                            }
                            u.setStatusSum(numshare);
                            if (ls_groupname.Count > 0)
                            {
                                while (loop <= (int)numnaxLoop.Value)
                                {
                                    if (loop > 0)
                                    {
                                        if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                        {
                                            u.setStatus(ldID, "Lỗi đăng nhập Facebook");
                                            break;
                                        }
                                        //kiem tra xem da vao livestream chua
                                        ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                                        if (linkopen.Contains("groups"))
                                            ld.openlinkgroup(acc.ldid);
                                    }
                                    sharesucess += ld.SharePostGroup_NoApproval(u, acc, dr, link, mess, numshare - sharesucess, delay, ls_groupname, ref historyShare, token);
                                    dr.Cells["clSuccess"].Value = sharesucess;
                                    if (sharesucess >= numshare)
                                        break;
                                    loop++;
                                    if (ls_groupname.Count <= 0)
                                    {
                                        break;
                                    }
                                }
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, " |Share group don't approval: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " |Share group don't approval: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                                else
                                {
                                    u.setStatus(ldID, " |Share vào nhóm không cần duyệt: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " |Share vào nhóm không cần duyệt: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                            }
                            else
                            {
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, "No avaiable don't approval group...");
                                    dr.Cells["Message"].Value = "No avaiable don't approval group";
                                    messageSuccess += "No avaiable don't approval group";
                                }
                                else
                                {
                                    u.setStatus(ldID, " Không có nhóm không cần duyệt...");
                                    dr.Cells["Message"].Value = " Không có nhóm không cần duyệt";
                                    messageSuccess += " Không có nhóm không cần duyệt";
                                }
                            }
                        }
                        if (chkShareStatic.Checked)
                        {
                            List<GroupFB> list_groupshare = new List<GroupFB>();
                            if (File.Exists(txtGroupName.Text))
                            {
                                string history = File.ReadAllText(txtGroupName.Text);
                                foreach (GroupFB gr in ls_group)
                                {
                                    if (history.Contains(gr.id) && historyShare.ToString().Contains(gr.id) == false)
                                    {
                                        list_groupshare.Add(gr);
                                    }
                                }
                            }

                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, "Share into static group...");
                                dr.Cells["Message"].Value = "Share into static group";
                            }
                            else
                            {
                                u.setStatus(ldID, "Share theo danh sách nhóm...");
                                dr.Cells["Message"].Value = "Share theo danh sách nhóm";
                            }
                            if (list_groupshare.Count > 0)
                            {
                                while (loop <= (int)numnaxLoop.Value)
                                {
                                    if (loop > 0)
                                    {
                                        if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                        {
                                            u.setStatus(ldID, "Lỗi đăng nhập Facebook");
                                            break;
                                        }
                                        //kiem tra xem da vao livestream chua
                                        ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);
                                        if (linkopen.Contains("groups"))
                                            ld.openlinkgroup(acc.ldid);
                                    }

                                    sharesucess += ld.SharePostGroup_NoApproval(u, acc, dr, link, mess, numshare - sharesucess, delay, list_groupshare, ref historyShare, token);
                                    dr.Cells["clSuccess"].Value = sharesucess;
                                    if (sharesucess >= numshare)
                                        break;
                                    loop++;
                                    if (list_groupshare.Count <= 0)
                                    {
                                        break;
                                    }
                                }
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, " Share group don't approval: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " Share group don't approval: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                                else
                                {
                                    u.setStatus(ldID, " Share vào nhóm không cần duyệt: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " Share vào nhóm không cần duyệt: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                            }

                            else
                            {
                                if (SettingTool.configld.language == "English")
                                {
                                    messageSuccess = "Let's choose file UID group ";
                                    dr.Cells["Message"].Value = "Let's choose file UID group";
                                }
                                else
                                {
                                    messageSuccess = "Vui lòng điền file nhóm cố định.";
                                    dr.Cells["Message"].Value = "Không có nhóm đầu vào";
                                }
                            }
                        }
                    }
                    #endregion
                }
                if (rbShareVideo.Checked)
                {
                    #region share video

                    //ld.openlinkvideo(acc.ldid, linkopen);

                    //share livestream
                    if (chkViewlivestream.Checked)
                    {
                        //view live
                        if (SettingTool.configld.language == "English")
                        {
                            dr.Cells["Message"].Value = "Watching Video";
                            u.setStatus(ldID, "Watching Video...");
                        }
                        else
                        {
                            dr.Cells["Message"].Value = "Đang xem Video";
                            u.setStatus(ldID, "Đang xem video...");
                        }
                        Thread.Sleep(2000);

                        int delay = rd.Next((int)numViewLivestreamMin.Value, (int)numViewLivestreamMax.Value);
                        if (SettingTool.configld.language == "English")

                            messageSuccess += "| Watched Video " + numViewLivestreamMin.Value.ToString() + " seconds";
                        else
                            messageSuccess += "| Đã xem Video " + numViewLivestreamMin.Value.ToString() + " giây";

                        try
                        {
                            while (delay >= 0)
                            {
                                dr.Cells["Message"].Value = "Đang xem video " + delay.ToString() + "s";
                                u.setStatus(ldID, "Đang xem video..." + delay.ToString() + "s");

                                Thread.Sleep(1000);
                                delay--;
                            }
                        }
                        catch
                        {
                        }

                    }
                    //ld.ClickOnLeapdroidPosition(acc.ldid, PointDefault.p_group_view_video);
                    //Thread.Sleep(500);
                    //ld.ClickOnLeapdroidPosition(acc.ldid, PointDefault.p_group_view_video);
                    if (chkLike.Checked)
                    {
                        if (!ld.checkContentLD(acc.ldid, "share|comment|chia sẻ|bình luận|thích"))
                            ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);

                        dr.Cells["Message"].Value = "Like Video";
                        u.setStatus(ldID, "Like video...");
                        if (ld.seedingLikeVideo(acc.ldid, token))
                        {
                            u.setStatus(ldID, "Like Success...");
                            int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                            Thread.Sleep(delay * 1000);
                            messageSuccess += "|Like Success";
                            //if (!ld.checkScreen(acc.ldid, "com.facebook.video"))
                        }
                    }
                    if (chkcomment.Checked)
                    {
                        if (!ld.checkContentLD(acc.ldid, "share|comment|chia sẻ|bình luận|thích"))
                            ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);

                        dr.Cells["Message"].Value = "Comment Video";
                        u.setStatus(ldID, "Comment Video...");
                        string description = txt_contentComment.Text.Trim();
                        if (ld.commentVideo(acc.ldid, description, token))
                        {
                            ld.back(acc.ldid, 2);
                            u.setStatus(ldID, "Comment Success...");
                            dr.Cells["Message"].Value = "Comment Success";
                            messageSuccess += "|Comment Success";
                            int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                            Thread.Sleep(delay * 1000);

                            //ld.scroll_down(ldID);
                            //ld.OpenLink(acc.ldid, "com.facebook.katana", linkopen);
                        }
                    }
                    if (chkShareNewfeed.Checked)
                    {
                        if (!ld.checkContentLD(acc.ldid, "share|chia sẻ"))
                            ld.functionOpenLink(acc.ldid, "com.facebook.katana", linkopen);

                        dr.Cells["Message"].Value = "Share tường cá nhân";
                        u.setStatus(ldID, "Share tường cá nhân...");
                        if (ld.shareVideo2Newfeed(acc.ldid, mess, token))
                        {
                            messageSuccess += " |Share tường thành công";
                        }
                        int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                        Thread.Sleep(delay * 1000);
                    }
                    if (chkShareGroup.Checked || chkShareRandom.Checked || chkShareStatic.Checked || chkShareNoApproval.Checked)
                    {
                        // if (!ld.checkContentLD(acc.ldid, "share|comment|chia sẻ|bình luận|thích"))
                        bool boolfullscreen = false;
                        if (chkfullscreen.Checked)
                            boolfullscreen = ld.openlinkvideo(acc.ldid, linkopen, chkVideodoc.Checked);
                        else
                            ld.OpenLink(acc.ldid, "com.facebook.katana", linkopen);

                        dr.Cells["Message"].Value = "Share Group";
                        u.setStatus(ldID, "Share group...");
                        int numshare = rd.Next((int)numShareMin.Value, (int)numShareMax.Value);
                        int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);

                        //share live
                        if (chkShareRandom.Checked)
                        {
                            //share video vao group
                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, "Share Video random group...");
                                dr.Cells["Message"].Value = "Share Video random group";
                            }
                            else
                            {
                                u.setStatus(ldID, "Share Video vào nhóm ngẫu nhiên...");
                                dr.Cells["Message"].Value = "Share Video vào nhóm ngẫu nhiên";
                            }
                            while (loop <= (int)numnaxLoop.Value)
                            {
                                if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                {
                                    u.setStatus(ldID, "Lỗi đăng nhập Facebook");
                                    break;
                                }
                                if (loop > 0)
                                {
                                    if (chkfullscreen.Checked)
                                        boolfullscreen = ld.openlinkvideo(acc.ldid, linkopen, chkVideodoc.Checked);
                                    else
                                        ld.OpenLink(acc.ldid, "com.facebook.katana", linkopen);
                                }
                                sharesucess += ld.shareVideo2Group(u, acc, mess, numshare - sharesucess, delay, linkopen, token, boolfullscreen);
                                dr.Cells["clSuccess"].Value = sharesucess;
                                u.setStatusSum(numshare);
                                u.setStatusResult(sharesucess);
                                if (sharesucess >= numshare)
                                    break;
                                loop++;
                            }
                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, " Share random group: " + sharesucess.ToString() + "/" + numshare.ToString());
                                messageSuccess += " Share random group: " + sharesucess.ToString() + "/" + numshare.ToString();
                                dr.Cells["Message"].Value = messageSuccess;
                            }
                            else
                            {
                                u.setStatus(ldID, " Share vào nhóm ngẫu nhiên: " + sharesucess.ToString() + "/" + numshare.ToString());
                                messageSuccess += " Share vào nhóm ngẫu nhiên: " + sharesucess.ToString() + "/" + numshare.ToString();
                                dr.Cells["Message"].Value = messageSuccess;
                            }
                        }
                        if (chkShareNoApproval.Checked)
                        {
                            if (SettingTool.configld.language == "English")
                            {
                                u.setStatus(ldID, "Share Video group don't approval...");
                                dr.Cells["Message"].Value = "Share Video group don't approval";
                            }
                            else
                            {
                                u.setStatus(ldID, "Share Video vào nhóm không cần duyệt...");
                                dr.Cells["Message"].Value = "Share Video vào nhóm không cần duyệt";
                            }
                            if (ls_groupname.Count > 0)
                            {
                                if (numshare > ls_groupname.Count)
                                {
                                    numshare = ls_groupname.Count;
                                }
                                u.setStatusSum(numshare);
                                while (loop <= (int)numnaxLoop.Value)
                                {
                                    if (ls_groupname.Count == 0)
                                        break;
                                    if (loop > 0)
                                    {
                                        if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                        {
                                            u.setStatus(ldID, "Lỗi đăng nhập Facebook");
                                            break;
                                        }
                                        //kiem tra xem da vao video chua
                                        if (chkfullscreen.Checked)
                                            boolfullscreen = ld.openlinkvideo(acc.ldid, linkopen, chkVideodoc.Checked);
                                        else
                                            ld.OpenLink(acc.ldid, "com.facebook.katana", linkopen);
                                    }
                                    sharesucess += ld.ShareVideo_random_noApproval(u, dr, acc, linkopen, numshare - sharesucess, delay, ls_groupname, txt_contentShare.Text.Trim(), ref historyShare, boolfullscreen, token);
                                    dr.Cells["clSuccess"].Value = sharesucess;
                                    u.setStatusSum(numshare);
                                    u.setStatusResult(sharesucess);
                                    if (sharesucess >= numshare)
                                        break;
                                    loop++;
                                }

                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, " Share group don't approval: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " Share group don't approval: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                                else
                                {
                                    u.setStatus(ldID, " Share vào nhóm không cần duyệt: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " Share vào nhóm không cần duyệt: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                            }
                            else
                            {
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, "No avaiable don't approval group...");
                                    dr.Cells["Message"].Value = "No avaiable don't approval group";
                                    messageSuccess += "No avaiable don't approval group";
                                }
                                else
                                {
                                    u.setStatus(ldID, "Không có nhóm không cần duyệt...");
                                    dr.Cells["Message"].Value = "Không có nhóm không cần duyệt";
                                    messageSuccess += "Không có nhóm không cần duyệt";
                                }
                            }
                        }
                        if (chkShareStatic.Checked)
                        {
                            List<GroupFB> list_groupshare = new List<GroupFB>();
                            if (File.Exists(txtGroupName.Text))
                            {
                                string history = File.ReadAllText(txtGroupName.Text);
                                foreach (GroupFB gr in ls_group)
                                {
                                    if (history.Contains(gr.id) && historyShare.ToString().Contains(gr.id) == false)
                                    {
                                        list_groupshare.Add(gr);
                                    }
                                }
                            }
                            if (list_groupshare.Count > 0)
                            {
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, "Share Video static group...");
                                    dr.Cells["Message"].Value = "Share Video static group";
                                }
                                else
                                {
                                    u.setStatus(ldID, "Share Video vào nhóm cố định...");
                                    dr.Cells["Message"].Value = "Share Video vào nhóm cố định";
                                }
                                while (loop <= (int)numnaxLoop.Value)
                                {
                                    if (list_groupshare.Count == 0)
                                        break;
                                    if (loop > 0)
                                    {
                                        if (ld.check_Facebook_has_stopped(u, acc.ldid, acc, token) == false)
                                        {
                                            u.setStatus(ldID, "Lỗi đăng nhập Facebook");
                                            break;
                                        }
                                        if (chkfullscreen.Checked)
                                            boolfullscreen = ld.openlinkvideo(acc.ldid, linkopen, chkVideodoc.Checked);
                                        else
                                            ld.OpenLink(acc.ldid, "com.facebook.katana", linkopen);
                                    }
                                    sharesucess += ld.ShareVideo_random_noApproval(u, dr, acc, linkopen, numshare - sharesucess, delay, list_groupshare, txt_contentShare.Text.Trim(), ref historyShare, boolfullscreen, token);
                                    dr.Cells["clSuccess"].Value = sharesucess;
                                    u.setStatusSum(numshare);
                                    u.setStatusResult(sharesucess);

                                    if (sharesucess >= numshare)
                                        break;
                                    loop++;
                                }
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, " Share static group: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " Share static group: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                                else
                                {
                                    u.setStatus(ldID, " Share vào nhóm cố định: " + sharesucess.ToString() + "/" + numshare.ToString());
                                    messageSuccess += " Share vào nhóm cố định: " + sharesucess.ToString() + "/" + numshare.ToString();
                                    dr.Cells["Message"].Value = messageSuccess;
                                }
                            }
                            else
                            {
                                if (SettingTool.configld.language == "English")
                                {
                                    messageSuccess = "Let's choose file group name ";
                                    dr.Cells["Message"].Value = "Let's choose file group UID";
                                }
                                else
                                {
                                    messageSuccess = "Vui lòng điền tên file nhóm cố định.";
                                    dr.Cells["Message"].Value = "Không có nhóm đầu vào";
                                }
                            }
                        }
                    }
                    #endregion
                }
                if (rbPlaylist.Checked)
                {
                    #region share playlist
                    if (chkShareGroup.Checked || chkShareRandom.Checked || chkShareStatic.Checked || chkShareNoApproval.Checked)
                    {

                        dr.Cells["Message"].Value = "Share Group";
                        u.setStatus(ldID, "Share group...");
                        int numshare = rd.Next((int)numShareMin.Value, (int)numShareMax.Value);
                        int delay = rd.Next((int)numDelayMin.Value, (int)numDelayMax.Value);
                        int delayclick = rd.Next((int)numMinDelayClick.Value, (int)numMaxDelayClick.Value);
                        Thread.Sleep(delayclick * 1000);
                        int open = 3;
                        bool has_open = false;
                        while (open > 0)
                        {
                            has_open = ld.openLinkPlaylist(acc.ldid, delayclick);
                            if (has_open)
                            {
                                break;
                            }
                            else
                            {
                                linkopen = FunctionHelper.method_Spin(link);
                                Thread.Sleep(1000);
                            }
                            open--;
                        }
                        if (has_open == false)
                        {
                            dr.Cells["Message"].Value = "Không thể mở video Playlist";
                        }
                        else
                        {
                            //share live
                            if (chkShareRandom.Checked)
                            {
                                //share livestream vao group
                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, "Share Video random group...");
                                    dr.Cells["Message"].Value = "Share Video random group";
                                }
                                else
                                {
                                    u.setStatus(ldID, "Share Video vào nhóm ngẫu nhiên...");
                                    dr.Cells["Message"].Value = "Share Video vào nhóm ngẫu nhiên";
                                }
                                messageSuccess += ld.shareVideoPlaylist(acc.ldid, dr, mess, numshare, delay);
                            }
                            if (chkShareNoApproval.Checked)
                            {
                                if (ls_groupname.Count > 0)
                                {
                                    messageSuccess += ld.SharePlaylist_random_noApproval(u, dr, acc, link, numshare, delay, ls_groupname, txt_contentShare.Text.Trim(), ref historyShare, token);
                                }
                                else
                                {
                                    if (SettingTool.configld.language == "English")
                                    {
                                        u.setStatus(ldID, "No avaiable don't approval group...");
                                        dr.Cells["Message"].Value = "No avaiable don't approval group";
                                        messageSuccess += "No avaiable don't approval group";
                                    }
                                    else
                                    {
                                        u.setStatus(ldID, "Không có nhóm không cần duyệt...");
                                        dr.Cells["Message"].Value = "Không có nhóm không cần duyệt";
                                        messageSuccess += "Không có nhóm không cần duyệt";
                                    }

                                }
                            }
                            if (chkShareStatic.Checked)
                            {
                                List<GroupFB> list_groupshare = new List<GroupFB>();
                                if (File.Exists(txtGroupName.Text))
                                {
                                    string history = File.ReadAllText(txtGroupName.Text);
                                    foreach (GroupFB gr in ls_group)
                                    {
                                        if (history.Contains(gr.id) && historyShare.ToString().Contains(gr.id) == false)
                                        {
                                            list_groupshare.Add(gr);
                                        }
                                    }
                                }

                                if (SettingTool.configld.language == "English")
                                {
                                    u.setStatus(ldID, "Share Video into static group...");
                                    dr.Cells["Message"].Value = "Share Video into static group";
                                }
                                else
                                {
                                    u.setStatus(ldID, "Share Video vào nhóm cố định...");
                                    dr.Cells["Message"].Value = "Share Video vào nhóm cố định";
                                }
                                if (list_groupshare.Count > 0)
                                    messageSuccess += ld.SharePlaylist_random_noApproval(u, dr, acc, link, numshare, delay, list_groupshare, txt_contentShare.Text.Trim(), ref historyShare, token);
                                else
                                {
                                    if (SettingTool.configld.language == "English")
                                    {
                                        messageSuccess = "Let's choose file group name ";
                                        dr.Cells["Message"].Value = "Let's choose file group UID";
                                    }
                                    else
                                    {
                                        messageSuccess = "Vui lòng điền tên file nhóm cố định.";
                                        dr.Cells["Message"].Value = "Không có nhóm đầu vào";
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            catch
            {

            }
            return messageSuccess;
        }
        xProxyController xcontroller = new xProxyController();
        private void frm_ShareLD_Load(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(btnLink, "Mẫu link share");
            SettingTool.configPost = new configFormPost();
            try
            {
                List<string> lsUser = new List<string>();
                lsUser = File.ReadAllLines(Application.StartupPath + "\\Config\\ListUser.txt").ToList();
                if (lsUser.Contains(SettingTool.email))
                {
                    chkrepeatcomment.Visible = true;
                }
            }
            catch
            {
            }
            try
            {
                string path = String.Format("{0}\\Config\\ConfigFormShare.data", Application.StartupPath);
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    SettingTool.configShare = JsonConvert.DeserializeObject<configFormShare>(json);
                }

                txt_linkshare.Text = SettingTool.configShare.link;
                txt_contentShare.Text = SettingTool.configShare.contentShare;
                txt_contentComment.Text = SettingTool.configShare.contentComment;

                chkLimitAcc.Checked = SettingTool.configShare.limit;

                numLimitAcc.Value = SettingTool.configShare.numlimit;
                chkLoopRun.Checked = SettingTool.configShare.loop;

                numWait.Value = SettingTool.configShare.timeloop;
                numDelayMin.Value = SettingTool.configShare.delaymin;
                numDelayMax.Value = SettingTool.configShare.delaymax;

                chkViewlivestream.Checked = SettingTool.configShare.has_view;
                numViewLivestreamMin.Value = SettingTool.configShare.timeviewmin;
                numViewLivestreamMax.Value = SettingTool.configShare.timeviewmax;

                chkInviteLivestream.Checked = SettingTool.configShare.has_invite;

                numInviteMin.Value = SettingTool.configShare.num_invitemin;
                numInviteMax.Value = SettingTool.configShare.num_invitemax;

                chkLike.Checked = SettingTool.configShare.has_like;

                chkcomment.Checked = SettingTool.configShare.has_comment;

                chkShareNewfeed.Checked = SettingTool.configShare.has_sharenewfeed;

                chkShareGroup.Checked = SettingTool.configShare.has_sharegroup;

                numShareMin.Value = SettingTool.configShare.num_sharemin;
                numShareMax.Value = SettingTool.configShare.num_sharemax;

                numMember.Value = SettingTool.configShare.num_member;
                chkShareRandom.Checked = SettingTool.configShare.has_share_random;
                chkShareStatic.Checked = SettingTool.configShare.has_share_file;

                chkShareNoApproval.Checked = SettingTool.configShare.has_share_noapprove;

                if (SettingTool.configShare.typeshare == 1)
                {
                    rbShareLive.Checked = true;
                }
                if (SettingTool.configShare.typeshare == 2)
                {

                    rbSharePost.Checked = true;
                }
                if (SettingTool.configShare.typeshare == 3)
                {

                    rbShareVideo.Checked = true;
                }
                if (SettingTool.configShare.typeshare == 4)
                {

                    rbPlaylist.Checked = true;
                }
                //share live

                txtGroupName.Text = SettingTool.configShare.pathgroup;
                numMinDelayClick.Value = SettingTool.configShare.delay_click_min;
                numMaxDelayClick.Value = SettingTool.configShare.delay_click_max;
            }
            catch
            {

            }
            method_LoadAccount();
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }
        }
        private void setupLanguage()
        {
            this.Text = "Share livestream,video,post";
            label8.Text = "seconds";
            label2.Text = "Share content";
            label3.Text = "Each link separate by comma";

            chkLimitAcc.Text = "Limit amount account ";
            chkLoopRun.Text = "Loop";
            chkShareNoApproval.Text = "Share into without approval group";

            label10.Text = "minutes";
            chkShareRandom.Text = "Share into random group";

            chkInviteLivestream.Text = "Invite friend to view livestream";
            chkShareNewfeed.Text = "Share into newfeed";
            chkShareStatic.Text = "Share into static group";
            label12.Text = "File group name";
            chkShareGroup.Text = "Share group";
            label17.Text = "Members";

            lblLoop.Text = "Loop Share";
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


                DataGridViewTextBoxCell cell71 = new DataGridViewTextBoxCell();
                cell71.Value = 0;
                dataGridViewRow.Cells.Add(cell71);

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


        private void btnLink_Click(object sender, EventArgs e)
        {
            frm_TemplateLink frm = new frm_TemplateLink();
            frm.ShowDialog();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            method_Stop();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = false;
            if (checkBox1.Checked)
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

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            dialog.Filter = "File txt (*.txt)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtGroupName.Text = dialog.FileName;

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
        private sealed class Class34
        {
            public Color color_0;
            public DataGridViewRow dataGridViewRow_0;

            public void method_0()
            {
                this.dataGridViewRow_0.DefaultCellStyle.BackColor = this.color_0;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            dialog.Filter = "File txt (*.txt)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_contentShare.Text = dialog.FileName;

            }
        }

        private void richLogs_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("https://youtu.be/GFXBKIsAbno");
        }

        private void chọnDòngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkIntoGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShareRandom.Checked)
            {
                chkShareGroup.Checked = true;

                chkShareRandom.Checked = true;
                chkShareStatic.Checked = false;
                chkShareNoApproval.Checked = false;

            }
        }

        private void chkLiveStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShareStatic.Checked)
            {
                chkShareGroup.Checked = true;
                chkShareRandom.Checked = false;
                chkShareStatic.Checked = true;
                chkShareNoApproval.Checked = false;
            }
        }

        private void chkNoApproval_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShareNoApproval.Checked)
            {
                chkShareGroup.Checked = true;
                chkShareRandom.Checked = false;
                chkShareStatic.Checked = false;
                chkShareNoApproval.Checked = true;
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

        private void rbShareLive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbShareLive.Checked)
            {
                lblivestream.Visible = true;
                chksharebyname.Visible = true;
                chklinkprofile.Visible = true;

            }
            else
            {
                lblivestream.Visible = false;
                chksharebyname.Visible = false;
                chklinkprofile.Visible = false;
            }
        }

        private void rbShareVideo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbShareVideo.Checked)
            {
                chkVideodoc.Visible = true;
                chkfullscreen.Visible = true;
            }

            else
            {
                chkVideodoc.Visible = false;
                chkfullscreen.Visible = false;
            }

        }

    }
}
