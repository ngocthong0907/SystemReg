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
    public partial class frm_CreateLD : Form
    {
        public frm_CreateLD(frm_MainLD frm_main)
        {
            InitializeComponent();
            this.frm_main = frm_main;
        }
        GroupLD_BLL groupld_bll = new GroupLD_BLL();
        DetailLD_BLL detailld_bll = new DetailLD_BLL();
        Thread thread_1;
        string text = "";
        bool has_top = false;
        int groupid;
        string timezone = "";
        CancellationTokenSource tokensource;
        frm_MainLD frm_main;
        private void frm_CreateLD_Load(object sender, EventArgs e)
        {
            string[] arr = { "1 CPU - Ram 1024", "2 CPU - 2048", "3 CPU - Ram 3072", "4 CPU - Ram 4096" };
            cboCpu.DataSource = arr;
            cboCpu.SelectedIndex = 0;
            loadGroupLD();

            //string[] arrtime = { "VN", "US" };
            //cboTimeZone.DataSource = arrtime;

            List<string> list_utc = new List<string>();
            if (File.Exists(Application.StartupPath + "\\lsUTC.txt"))
            {
                list_utc = File.ReadAllLines(Application.StartupPath + "\\lsUTC.txt").ToList();
                string[] arrUtc = list_utc.ToArray();
                if (arrUtc.Count() > 0)
                {
                    cboTimeZone.DataSource = arrUtc;
                    cboTimeZone.SelectedIndex = 0;
                }
                else
                    cboTimeZone.Text = "Asia/Ho_Chi_Minh";
             
            }
            else
                cboTimeZone.Text = "Asia/Ho_Chi_Minh";

         
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }
        }
        private void loadGroupLD()
        {
            try
            {
                cboGroupLD.Items.Clear();
                ComboboxItem item1 = new ComboboxItem();
                if (SettingTool.configld.language == "English")
                    item1.Text = "Choose group LD";
                else
                    item1.Text = "Chọn nhóm LD";
                cboGroupLD.Items.Add(item1);
                List<GroupLDModel> list_groupld = new List<GroupLDModel>();
                list_groupld = groupld_bll.selectGroupLD();
                foreach (GroupLDModel group in list_groupld)
                {
                    ComboboxItem item = new ComboboxItem();
                    item = new ComboboxItem();
                    item.Text = group.Name;
                    item.Tag = group;
                    cboGroupLD.Items.Add(item);

                }
                cboGroupLD.SelectedIndex = 0;
            }
            catch
            {

            }


        }
        public void saveLDPlayer()
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            text = cboCpu.Text.Trim();
            timezone = cboTimeZone.Text.Trim();

            //if (timezone == "VN")
            //{
            //    timezone = "Asia/Ho_Chi_Minh";
            //}
            //else
            //{
            //    timezone = "America/New_York";
            //}

            try
            {
                ComboboxItem item2 = (ComboboxItem)cboGroupLD.SelectedItem;
                if (item2.Text == "Chọn nhóm LD" || item2.Text == "Choose group LD")
                {
                    if (SettingTool.configld.language == "English")
                        MessageBox.Show("Let to select group LD", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Vui lòng chọn nhóm LD", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {

                    GroupLDModel group = (GroupLDModel)item2.Tag;
                    groupid = group.GroupLDID;

                    this.thread_1 = new Thread(new ThreadStart(this.createLDPlayer));
                    thread_1.IsBackground = true;
                    this.thread_1.Start();
                }

            }
            catch
            { }

        }
        public void createLDPlayer()
        {

            Random rd = new Random();
            LDController ldcontroller = new LDController();
            List<LDModel> list_ld = new List<LDModel>();
            int max = (int)numTotalLD.Value;

            string tiento = txtNameLD.Text.Trim();
            string dauso = txtPhone.Text.Trim();

            for (int i = 0; i < max; i++)
            {
                string nameld = rd.Next(10000, 200000).ToString();
                ldcontroller.add(nameld);
                List<LDModel> list_ldinstall = ldcontroller.getLdPlay();
                foreach (LDModel m in list_ldinstall)
                {
                    if (m.name == nameld)
                    {

                        string phone = dauso + FunctionHelper.RandomPhone(6);
                        //    ldcontroller.modify(m.id.ToString(), phone);
                        ldcontroller.rename(tiento, m.id.ToString());



                        if (text.Contains("1 CPU"))
                        {
                            m.cpu = "1";
                            m.ram = "1024";
                        }
                        else
                        {
                            if (text.Contains("2 CPU"))
                            {
                                m.cpu = "2";
                                m.ram = "2048";
                            }
                            else
                            {
                                if (text.Contains("3 CPU"))
                                {
                                    m.cpu = "3";
                                    m.ram = "3072";
                                }
                                else
                                {
                                    m.cpu = "4";
                                    m.ram = "4096";

                                }

                            }
                        }
                        m.phonenumber = phone;

                        ImeiModel im = FunctionHelper.createImei();
                        m.manufacturer = txtNSX.Text;
                        m.model = txtmodel.Text;
                        m.imei = im.value;
                        m.timezone = timezone;
                        ldcontroller.modify(m.id.ToString(), m);
                        list_ld.Add(m);
                        method_log("Tạo xong LD" + m.id);
                        break;
                    }
                }
            }
            if (list_ld.Count > 0)
                runTuongTacWaitAny(list_ld);

        }
        private void runTuongTacWaitAny(List<LDModel> list_ld)
        {
            try
            {
                tokensource = new CancellationTokenSource();
                var token = tokensource.Token;

                int numthread = SettingTool.configld.numthread;
                if (numthread > list_ld.Count)
                {
                    numthread = list_ld.Count;
                }
                //khoi tao list task
                Task[] list_task = TaskController.createTask(numthread);
            Lb_quayvong:
                if (list_ld.Count > 0)
                {

                    object synDevice = new object();

                    if (has_top == false)
                    {
                        int index = TaskController.getAvailableTask(list_task);
                        if (index >= 0)
                        {

                            Task task = Task.Factory.StartNew(() =>
                            {
                                if (list_ld.Count > 0)
                                {
                                    LDModel model = list_ld[0];
                                    list_ld.Remove(model);
                                    CreateLD(model, token);
                                }
                            }, token);
                            list_task[index] = task;
                        }

                        Thread.Sleep(SettingTool.configld.timedelay * 1000);
                    }
                   
                    try
                    {
                        Task.WaitAny(list_task);
                    }
                    catch
                    { }
                    if (list_ld.Count > 0 && has_top == false)
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

                        method_StopAddFriend();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        private void method_StopAddFriend()
        {
            has_top = true;
            if (thread_1 != null)
                thread_1.Abort();

        }
        public void CreateLD(LDModel ld, CancellationToken token)
        {
            //modify ld
            string ldID = ld.id.ToString();
            LDController ldcontroller = new LDController();

            method_log("Open LD: " + ldID);
            //cài app
            method_log("Open LDPlayer Id: " + ldID);
            userLD u = frm_main.checkExits(ldID);
            frm_main.addLDToPanel(u);
            if (ldcontroller.launchSetPosion(ldID, u, token))
            {
                u.setStatus(ldID, "Connect successful LD...");
            }
            else
            {
                if (ldcontroller.autoRunLDSetPosition(ldID, u, token))
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
                ldcontroller.disableGPS(ldID);
                //thiet lap tuy chinh ld
                ldcontroller.customLD(ld);
                method_log("Start install App Facebook");
                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                if (ldcontroller.checkApp(ldID, "com.android.adbkeyboard") == false)
                {
                    string path = Application.StartupPath + "\\app\\ADBKeyboard.apk";
                    if (File.Exists(path))
                    {
                        u.setStatus(ldID, "Start install App Adbkeyboard...");
                        ldcontroller.installApp(ldID, path);
                    }
                }
                if (ldcontroller.checkApp(ldID, "com.facebook.katana") == false)
                {
                    string path = "";
                    if(SettingTool.configld.appversion=="Facebook 299")
                    {
                        path = Application.StartupPath + "\\app\\Facebook299.apk";
                    }
                    else
                    {
                        path = Application.StartupPath + "\\app\\Facebook.apk";
                    }
                  
                    if (File.Exists(path))
                    {
                        u.setStatus(ldID, "Start install App Facebook...");
                        ldcontroller.installApp(ldID, path);
                        while (ldcontroller.checkApp(ldID, "com.facebook.katana") == false)
                        {
                            Thread.Sleep(10000);
                        }
                        Thread.Sleep(3000);
                    }
                }
                ldcontroller.setKeyboard(ldID);

            }
            catch
            { }
        Lb_Finish:
            u.setStatus(ldID, "Installed Successful...");
            ldcontroller.killApp(ldID, "com.facebook.katana");
            ldcontroller.disableNotificationFacebook(ldID);
            ldcontroller.killApp(ldID, "android.settings.APPLICATION_SETTINGS");
            ldcontroller.runApp(ldID, "com.facebook.katana");
            DetailLDModel detail = new DetailLDModel();
            detail.GroupLDID = groupid;
            detail.LDID = ld.id;
            detail.LDName = "LD" + ld.id;
            detailld_bll.insert(detail);
            ldcontroller.quit(ldID);
            frm_main.removeLDToPanel(u);

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

        private void button1_Click(object sender, EventArgs e)
        {
            frm_AddGroupLD frm = new frm_AddGroupLD();
            frm.ShowDialog();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            tokensource.Cancel();
            method_StopAddFriend();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImeiModel im = FunctionHelper.createImei();
            txtPhone.Text = im.value;
        }

        private void setupLanguage()
        {
            this.text = "Create new Ld Player ";
            label1.Text = "Choose Ld group";
            button1.Text = "Add";
            label2.Text = "Amount Ld create";
            label3.Text = "Name LD start";
            label4.Text = "Configuaration";
            label6.Text = "Prefix number phone";
            label7.Text = "6 character end random";
            bunifuFlatButton1.Text = "Run";
            btnInput.Text = "Stop";
        }

        private void lsMuigio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + "\\lsUTC.txt";
            if (File.Exists(path) == false)
            {
                File.WriteAllText(path, " ");
            }
            SettingTool.linkproxyninja = Application.StartupPath + "\\lsUTC.txt";
            Process.Start(path);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://en.wikipedia.org/wiki/List_of_tz_database_time_zones");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
