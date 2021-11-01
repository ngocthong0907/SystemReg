using Ionic.Zip;
using Microsoft.Win32;
using Newtonsoft.Json;
using NinjaSystem.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_Login_PRO : Form
    {
        public frm_Login_PRO()
        {
            InitializeComponent();
            method_Setup();
            Class18 cl2 = new Class18("NINJA-SYSTEM-");
            string path = String.Format("{0}\\app", Application.StartupPath);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            path = "C:\\test";
            if (Directory.Exists(path)==false)
            {
                Directory.CreateDirectory(path);
            }
       
            path = String.Format("{0}\\LD", Application.StartupPath);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            path = String.Format("{0}\\uid", Application.StartupPath);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);

            }
            path = String.Format("{0}\\logs", Application.StartupPath);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            //if (string.IsNullOrEmpty(SettingTool.configld.language)==false)
            //{
            //    if (SettingTool.configld.language == "English")
            //    {
            //        setupLanguage();
            //    }

            //}
           
         
        }
        private void setupLanguage()
        {
            bunifuCustomLabel5.Text = "You have not registered the copyright for Ninja System";
            bunifuCustomLabel1.Text = "Register Now";
        }
        CustomerController customercontroller = new CustomerController();
        private void method_Setup()
        {
            try
            {
                string path = String.Format("{0}\\login.data", Application.StartupPath);

                AccountNinja acc = new AccountNinja();
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    acc = JsonConvert.DeserializeObject<AccountNinja>(json);
                }
                txtEmail.Text = acc.email.Trim();
                txtPass.Text = acc.pass.Trim();
            }
            catch
            { } 
           SettingTool.version =12.9;
           SettingTool.versiontext = "12.9";
            SettingTool.privatekey = "9d31b084dd0d981de479c2a7abe4f557";
            lbnVersion.Text = "Version: " + SettingTool.versiontext;
        }
        private void method_UpdateDll(string link, string filename)
        {
            string str;

            try
            {
                ZipFile file;
                int num = 0;

                FileStream stream2 = null;
                Stream responseStream = null;
                try
                {
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
                        request.Proxy = null;
                        request.Credentials = CredentialCache.DefaultCredentials;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        long contentLength = response.ContentLength;

                        string path = String.Format("{0}\\{1}", Application.StartupPath, filename);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        stream2 = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                        responseStream = response.GetResponseStream();
                        //  num7 = ((this.versiune + num3) - num) + 1;

                        int count = 0;
                        byte[] buffer = new byte[0x400];
                        while ((count = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            stream2.Write(buffer, 0, count);
                            long num6 = (stream2.Length * 100L) / contentLength;

                        }
                    }
                    catch (Exception)
                    {
                    }
                    //continue;
                }
                finally
                {
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                    if (responseStream != null)
                    {
                        responseStream.Close();
                    }
                    num--;
                }
                FileInfo files = new FileInfo(Application.StartupPath + "\\" + filename);
                if (files != null)
                {
                    FileInfo infoArray2 = files;
                    int index = 0;

                    try
                    {
                        file = ZipFile.Read(infoArray2.FullName);
                        file.ExtractAll(Application.StartupPath, true);
                        string path = String.Format("{0}\\{1}", Application.StartupPath, infoArray2.FullName);

                        // Process.Start(Application.StartupPath);
                        // Application.Exit();

                    }
                    finally
                    {

                    }
                }
                else
                {

                    MessageBox.Show("Lỗi tự động cập nhật! Vui lòng tải full phần mềm để tiếp tục sử dụng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch
            {

                MessageBox.Show("Lỗi tự động cập nhật! Vui lòng tải full phần mềm để tiếp tục sử dụng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(method_CheckVersion));
            th.IsBackground = true;
            th.Start();
        }
        private void method_CheckVersion()
        {
            method_LogLabel("Kiểm tra version mới");

            string path = String.Format("{0}\\app\\ADBKeyboard.apk", Application.StartupPath);
            if (!File.Exists(path))
            {
                method_LogLabel("Cập nhật file hệ thống");
                method_UpdateDll("https://tai.ninjateam.vn/app.zip", "app.zip");
            }
            path = String.Format("{0}\\app\\Facebook.apk", Application.StartupPath);
            if (!File.Exists(path))
            {
                method_LogLabel("Cập nhật file hệ thống");
                method_UpdateDll("https://tai.ninjateam.vn/facebook.zip", "facebook.zip");
            }
            path = String.Format("{0}\\app\\Facebook299.apk", Application.StartupPath);
            if (!File.Exists(path))
            {
                method_LogLabel("Cập nhật file hệ thống");
                method_UpdateDll("https://tai.ninjateam.vn/facebook299.zip", "facebook299.zip");
            }
            path = String.Format("{0}\\app\\Facebook302.apk", Application.StartupPath);
            if (!File.Exists(path))
            {
                method_LogLabel("Cập nhật file hệ thống");
                method_UpdateDll("https://tai.ninjateam.vn/facebook302.zip", "facebook302.zip");
            }
            path = String.Format("{0}\\app\\proxydroid.apk", Application.StartupPath);
            if (!File.Exists(path))
            {
                method_LogLabel("Cập nhật file hệ thống");
                method_UpdateDll("https://tai.ninjateam.vn/proxydroid.zip", "proxydroid.zip");
            }
            method_LogLabel("Đăng nhập Ninja System");
            Random rd = new Random();

            CustomerTrialModel cus = new CustomerTrialModel();
            cus.Email = txtEmail.Text.Trim();
            cus.Password = txtPass.Text.Trim();
            cus.HID = SettingTool.hid;
            cus.Refer = "Login Ninja Ninja System";
            cus.Random = rd.Next(888888);
            cus.Version = SettingTool.version.ToString();
            SettingTool.email = cus.Email;

            ResultRequest result = customercontroller.method_Login(cus);
            if (result.status)
            {
                //   string data = Class60.giaima(result.data, "越南أنا أحب");
                CustomerTrialModel cusrequest = new CustomerTrialModel();
                cusrequest = JsonConvert.DeserializeObject<CustomerTrialModel>(result.data);
                if (cusrequest.Random == cus.Random && cusrequest.Email == cus.Email)
                {
                    path = String.Format("{0}\\login.data", Application.StartupPath);
                    SettingTool.ninjatoken = cusrequest.Token;
                    method_SetupPathLD();
                    AccountNinja acc = new AccountNinja();
                    acc.email = cus.Email;
                    acc.pass = cus.Password;
                    SettingTool.email = acc.email;
                    SettingTool.password = acc.pass;
                    SettingTool.note = cusrequest.Note;

                   
                    SettingTool.ntoken = cusrequest.ntoken;
                    SettingTool.key = cusrequest.privatekey;
                    if (SettingTool.note != null)
                    {
                        if (SettingTool.note.Contains("blacklist") == false)
                        {
                            ResultRequest kq = customercontroller.method_Update(33);
                            if (kq.status)
                            {
                                //if (MessageBox.Show("Đã có phiên bản mới bạn có muốn tiếp tục cập nhật không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //{
                                //    List<Update> list_update = JsonConvert.DeserializeObject<List<Update>>(kq.data);
                                //    Thread th = new Thread(method_OpenUpdate);
                                //    th.SetApartmentState(ApartmentState.STA);
                                //    th.Start(list_update);
                                //    return;
                                //}
                            }

                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                    else
                    {
                        ResultRequest kq = customercontroller.method_Update(33);
                        if (kq.status)
                        {
                            //if (MessageBox.Show("Đã có phiên bản mới bạn có muốn tiếp tục cập nhật không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            //{
                            //    List<Update> list_update = JsonConvert.DeserializeObject<List<Update>>(kq.data);
                            //    Thread th = new Thread(method_OpenUpdate);
                            //    th.SetApartmentState(ApartmentState.STA);
                            //    th.Start(list_update);
                            //    return;
                            //}
                        }

                    }
                    SettingTool.client = new ClientModel();
                    SettingTool.client.email = acc.email;
                    SettingTool.client.password = acc.pass;
                    SettingTool.client.hid = SettingTool.hid;
                    SettingTool.client.function = "login";
                    SettingTool.client.soft = "NinjaSystem";
                    SettingTool.client.version = SettingTool.version.ToString();
                    File.WriteAllText(path, JsonConvert.SerializeObject(acc));
                    if (cusrequest.Time > 1000)
                    {
                        Thread th = new Thread(method_OpenMain);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start(cusrequest);
                        SettingTool.banquyen = "pro";

                        Application.Exit();
                        return;
                    }
                    if (cusrequest.Time > 4)
                    {
                        Thread th = new Thread(method_OpenMain);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start(cusrequest);
                        Application.Exit();
                        SettingTool.banquyen = "pro";
                        return;
                    }

                    if (cusrequest.Time <= 4)
                    {
                        MessageBox.Show("Phiên bản dùng thử của bạn đã hết hạn.Vui lòng liên hệ Ninja Team để kích hoạt bản quyền. Hotline : 0978.888.466", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        method_LogLabel("Vui lòng liên hệ Ninja Team để kích hoạt bản quyền. Hotline : 0978.888.466");
                        Process.Start("https://www.phanmemninja.com/huong-dan-mua-hang-phan-mem-ninja-team/");
                        return;
                    }
                }
                else
                {
                    method_LogLabel("Tài khoản hoặc mật khẩu không đúng! Vui lòng kiểm tra lại");
                }
                method_LogLabel(result.mess);
            }
            else
            {
                method_LogLabel(result.mess);
            }
        }
        private void method_SetupPathLD()
        {
            try
            {
                string pathld = "";

                SettingTool.configld = new ConfigLD();
                try
                {
                    string path = String.Format("{0}\\Config\\ConfigLD.data", Application.StartupPath);

                    using (StreamReader r = new StreamReader(path))
                    {
                        string json = r.ReadToEnd();
                        SettingTool.configld = JsonConvert.DeserializeObject<ConfigLD>(json);
                    }
                    if (SettingTool.configld.appversion == "Facebook Lite")
                    {
                        SettingTool.configld.package = "com.facebook.lite";
                    }
                    else
                    {
                        SettingTool.configld.package = "com.facebook.katana";
                    }
                }
                catch
                { }

                if (String.IsNullOrEmpty(SettingTool.configld.pathLD))
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Changzhi\\LDPlayer"))
                    {
                        if (key != null)
                        {
                            pathld = (string)key.GetValue("InstallDir");

                        }
                    }
                    SettingTool.pathfolderld = pathld;
                    pathld += "dnconsole.exe";
                }
                else
                {
                    SettingTool.pathfolderld = SettingTool.configld.pathLD;
                    pathld = SettingTool.configld.pathLD + "\\dnconsole.exe";
                }
                SettingTool.pathLD = pathld;
                if(string.IsNullOrEmpty(SettingTool.configld.versionld))
                {
                    SettingTool.configld.versionld = "4.x";
                }
            }
            catch
            {
            }
        }
        private void method_OpenUpdate(Object obj)
        {
            //Object obj
            List<Update> listupdate = (List<Update>)obj;
            frm_Update frm = new frm_Update(listupdate);
            frm.ShowDialog();

        }
        private void method_OpenMain(Object obj)
        {
            try
            {
                //Object obj
                CustomerTrialModel li = (CustomerTrialModel)obj;
                frm_MainLD_PRO frmLD = new frm_MainLD_PRO(li);
                frmLD.ShowDialog();
                //MainNinja frm = new MainNinja(li);
                //frm.ShowDialog();
            }
            catch
            {

            }
        }
        private void method_LogLabel(string string_15)
        {
            MethodInvoker method = null;
            LogLabel class2 = new LogLabel
            {
                richTextBox_0 = bunifuCustomLabel5,
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
        private sealed class LogLabel
        {
            public Bunifu.Framework.UI.BunifuCustomLabel richTextBox_0;
            public string string_0;

            public void method_0()
            {
                try
                {
                    richTextBox_0.Text = string_0;

                }
                catch { }
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            controlUpdate up = new controlUpdate();


            SettingTool.linkproxyninja = Application.StartupPath + "\\Ninjaproxy.txt";
            string path = String.Format("{0}\\Config\\configpathDB.data", Application.StartupPath);

            DatabaseConfig settingDB = new DatabaseConfig();
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    settingDB = JsonConvert.DeserializeObject<DatabaseConfig>(json);
                }
                if (String.IsNullOrEmpty(settingDB.pathDB))
                {
                    up.updateDataDisplay();
                }

            }
            catch
            {

            }
            bunifuThinButton21_Click(null,null);
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {
            frm_DangKy frm = new frm_DangKy();
            frm.ShowDialog();
        }

    }
}
