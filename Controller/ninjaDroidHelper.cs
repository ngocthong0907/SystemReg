using KAutoHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpAdbClient;
using System.IO;
using SharpAdbClient.DeviceCommands;
using RestSharp;
using System.Windows.Forms;
namespace NinjaSystem
{
    public class ninjaDroidHelper
    {
        //mo ung dung facebook
        NinjaADB ninjaadb = new NinjaADB();
      

        
        public List<DeviceData> getDevices()
        {
            try
            {
                return AdbClient.Instance.GetDevices();
            }
            catch
            {
                return new List<DeviceData>();
            }

        }
        public List<AndroidProcess> list_process(DeviceData device)
        {
            List<AndroidProcess> list = new List<AndroidProcess>();
            var list_process = DeviceExtensions.ListProcesses(device).ToList();
            foreach (var process in list_process)
            {
                if (process.Name.Contains("com.facebook.katan"))
                {
                    list.Add(process);
                    var x = DeviceExtensions.GetPackageVersion(device, process.Name);
                    var a = DeviceExtensions.GetEnvironmentVariables(device);
                }
            }
            return list;
        }
        public void startAdbserver(string cmd)
        {
            ninjaadb.startAdb(cmd);
        }
        public void ResetDcom(string deviceID)
        {
            //adb backup -f 1.ab - apk com.facebook.katana
            string INPUT_TEXT_DEVICES = String.Format(SettingTool.data["reset3g"], deviceID);
            string str2 = ninjaadb.ExecuteCMD(INPUT_TEXT_DEVICES);
        }
        public List<string> getAppName(DeviceData device)
        {
            return ninjaadb.GetApp(device);
        }
        public void closeAllAppFacebook(DeviceData device)
        {
            List<AndroidProcess> list = new List<AndroidProcess>();
            var list_process = DeviceExtensions.ListProcesses(device).ToList();
            foreach (var process in list_process)
            {
                if (process.Name.Contains("com.facebook.katan"))
                {
                    try
                    {
                        ninjaadb.ForceStop(device, "com.facebook.katan");
                    }
                    catch
                    { }
                }
            }
            Thread.Sleep(2000);
            closeAllApp(device);

        }

        public void closeAllApp(DeviceData device)
        {
            ninjaadb.runComand(device, "input keyevent  KEYCODE_APP_SWITCH");

            Delay(2);
            List<DetechModel> list_detech = new List<DetechModel>();
            DetechModel data = new DetechModel();
            data.parent = "";
            data.content = "Clear";
            data.text = "Clear";
            data.node = "//node[contains(@class,'android.view.View')]";
            data.function = 1;
            list_detech.Add(data);
            
            data = new DetechModel();
            data.parent = "";
            data.content = "Close";
            data.text = "Close";
            data.node = "//node[contains(@class,'android.view.View')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Clear";
            data.text = "Clear";
            data.node = "//node[contains(@class,'android.widget.Button')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Close";
            data.text = "Close";
            data.node = "//node[contains(@class,'android.widget.Button')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Xóa tất cả";
            data.text = "Xóa tất cả";
            data.node = "//node[contains(@class,'android.widget.Button')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Xóa tất cả";
            data.text = "Xóa tất cả";
            data.node = "//node[contains(@class,'android.view.View')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Clear all";
            data.text = "Clear all";
            data.node = "//node[contains(@class,'android.widget.Button')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Clear all";
            data.text = "Clear all";
            data.node = "//node[contains(@class,'android.view.View')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Clear";
            data.text = "Clear";
            data.node = "//node[contains(@class,'android.widget.TextView')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Close";
            data.text = "Close";
            data.node = "//node[contains(@class,'android.widget.TextView')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Clear";
            data.text = "Clear";
            data.node = "//node[contains(@class,'android.widget.ImageView')]";
            data.function = 1;
            list_detech.Add(data);

            data = new DetechModel();
            data.parent = "";
            data.content = "Close";
            data.text = "Close";
            data.node = "//node[contains(@class,'android.widget.ImageView')]";
            data.function = 1;
            list_detech.Add(data);


            DetechModel kq = ninjaadb.RunDetechFunctionResourceID(device, list_detech);
            if (kq.status)
                ClickOnLeapdroidPosition(device, kq.point);
            else
                back(device,1);


        }

        public void KillApp(DeviceData device, string app)
        {
            ninjaadb.ForceStop(device, app);
        }

       

        public bool checkAppFinish(DeviceData device, string app, bool removeaccount = false)
        {
            try
            {
                int i = 0;
            Lb_Start:

                List<string> list_check = new List<string>();

                string check = ninjaadb.checkListContent(device, SettingTool.lang.list_appfinish);
                if (check != null)
                {
                    return true;
                }
                Point point = new Point();
                list_check = new List<string>();
                list_check.Add("Next time you log in");
                list_check.Add("gỡ tài khoản nào đó khỏi thiết bị này");
                if (ninjaadb.checkListContent(device, list_check) != null)
                {
                    back(device, 1);
                    Thread.Sleep(1000);
                    goto Lb_Start;
                }
                List<string> list_xpath = new List<string>();
                if (removeaccount)
                {
                    list_check = new List<string>();
                    list_check.Add("Menu");
                    point = ninjaadb.FindByXpathDesc(device, "//node[contains(@class,'android.widget.ImageView')]", list_check);
                    if (point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);

                        list_check = new List<string>();
                        list_check.Add("Remove account from device");
                        list_check.Add("Gỡ tài khoản khỏi thiết bị"); //thoong add
                        list_check.Add("Gỡ tài khoản");
                        list_check.Add("REMOVE ACCOUNT");
                        point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.TextView')]", list_check);
                        if (point.Y > 0)
                        {
                            ClickOnLeapdroidPosition(device, point);
                            point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.Button')]", list_check);
                            if (point.Y > 0)
                                ClickOnLeapdroidPosition(device, point);
                            goto Lb_Start;
                        }
                    }
                }
                list_check = new List<string>();
                list_check.Add("Log Into Another Account");
                list_check.Add("Đăng nhập bằng tài khoản khác");
                list_xpath = new List<string>();
                list_xpath.Add("//node[contains(@class,'android.widget.TextView')]");
                list_xpath.Add("//node[contains(@class,'android.widget.Button')]");
                point = ninjaadb.FindByListXpath(device, list_xpath, list_check);
                if (point.X > 0 || point.Y > 0)
                {
                    //ClickOnLeapdroidPosition(device, point);
                    return true;
                }
                i++;
                if (i == 5)
                {
                    ninjaadb.KillFacebook(device, app);
                    //openAppFacebook(device, app); //thoong edit =>
                    ninjaadb.OpenFacebook(device, app);
                    goto Lb_Start;
                }
                point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.Button')]", SettingTool.lang.list_skip);
                if (point.X > 0 || point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point);
                }
                if (i < 10)
                {
                    Thread.Sleep(1000);
                    goto Lb_Start;
                }

            }
            catch
            { }
            return false;

        }
       
        public bool openAppFacebook(DeviceData device, string app)
        {
            try
            {
                int i = 0;
                ninjaadb.OpenFacebook(device, app);
                return checkAppFinish(device, app);
            }
            catch (Exception ex)
            {
                //frm.sendLogs(ex.ToString());
            }
            return false;
        }

        public void joinGroupManual(DeviceData device, string app, Account acc, List<string> lsUID, bool autoAnswer)
        {
            for (int i = 0; i < lsUID.Count(); i++)
            {
                ninjaadb.OpenLink(device, app, "fb://group/" + lsUID[i]);

                if (autoAnswer)
                {
                   
                    joinGroupbyUID(device, app, lsUID[i], 3, autoAnswer);
                }
                else
                    MessageBox.Show("Hãy trả lời câu hỏi tham gia của nhóm. Bấm OK để tiếp tục ID khác!");
            }

        }

        public bool LoginFacebook(DeviceData device, string app, Account acc)
        {
            int i = 0;
            bool has_pass = false;

            DetechModel dtmodel = new DetechModel();
            dtmodel.content = "Xóa tài khoản khỏi thiết bị";
            dtmodel.text = "Xóa tài khoản khỏi thiết bị";
            dtmodel.function = 1;
            dtmodel.node = "//node[contains(@class,'android.widget.TextView')]";
            SettingTool.lang.list_detech.Add(dtmodel);

            dtmodel = new DetechModel();
            dtmodel.content = "Lưu mật khẩu Facebook với Smart Lock?";
            dtmodel.text = "Không bao giờ";
            dtmodel.function = 1;
            dtmodel.node = "//node[contains(@class,'android.widget.Button')]";
            SettingTool.lang.list_detech.Add(dtmodel);

        Lb_start:
            DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_detech);
            if (kq.status)
            {
                i = 0;
                switch (kq.function)
                {
                    case -1:
                        {
                            acc.Thongbao = kq.text;
                            return false;
                        }
                    case 0:
                        {
                            back(device, 1);
                            break;
                        }
                    case 1:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            if (has_pass)
                            {
                                Thread.Sleep(3000);
                                has_pass = false;
                            }
                            else
                                Thread.Sleep(1000);
                            break;
                        }
                    case 2:
                        {
                            if (kq.parent == "email")
                            {
                                ClickOnLeapdroidPosition(device, kq.point);
                                Thread.Sleep(1000);
                                if (acc.id == null || acc.id == "")
                                    PressOnLeapdroid(device, acc.email);
                                else
                                    PressOnLeapdroid(device, acc.id);
                                ClickOnLeapdroidPosition(device, kq.point.X, kq.point.Y + 30);
                                //if (acc.email.Length > 20 || acc.id.Length > 20)
                                Delay(3);
                                var kqpass = ninjaadb.RunDetechFunction(device, SettingTool.lang.list_detechpass);

                                if (kqpass.status)
                                {

                                    ClickOnLeapdroidPosition(device, kqpass.point);
                                    Delay(1);
                                    PressOnLeapdroid(device, acc.Password);
                                    has_pass = true;
                                }

                            }
                            else
                            {
                                if (kq.parent == "twofa")
                                {
                                    ClickOnLeapdroidPosition(device, kq.point);

                                    CustomerController control = new CustomerController();
                                    TwoFaModel model = control.getCodeTwofa(acc.email, acc.privatekey);
                                    if (string.IsNullOrEmpty(model.message) == false)
                                    {
                                        PressOnLeapdroid(device, model.message);
                                    }
                                    else
                                    {
                                        acc.TrangThai = "Authentication Error";
                                        return false;
                                    }
                                }

                            }


                            break;
                        }
                    case 3:
                        {
                            return true;
                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                i++;
                if (i <= 10)
                {
                    goto Lb_start;

                }

            }
            return false;

        }
        public bool checkIsLogin(DeviceData device)
        {
            //try
            //{
            //    SettingTool.lang.list_checklogin.Add("Camera");
            //    SettingTool.lang.list_checklogin.Add("Search");
            //    SettingTool.lang.list_checklogin.Add("Máy ảnh");
            //    SettingTool.lang.list_checklogin.Add("Notification");
            //    SettingTool.lang.list_checklogin.Add("Thông báo");
            //    SettingTool.lang.list_checklogin.Add("More");
            //    SettingTool.lang.list_checklogin.Add("Xem thêm");
            //    string check = ninjaadb.checkListContent(device, SettingTool.lang.list_checklogin);
            //    if (check != null)
            //    {
            //        return true;
            //    }

            //}
            //catch
            //{
            //}
            //return false;
            try
            {
                int i = 0;
            Lb_start:
                DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_detech);
                if (kq.status & kq.point.Y > 0)
                {
                    i = 0;
                    switch (kq.function)
                    {
                        case 0:
                            {
                                return false;
                            }
                        case 1:
                            {
                                return false;
                            }
                        case 2:
                            {
                                return false;
                            }
                        case 3:
                            {
                                return true;
                            }
                        default:
                            break;
                    }
                    goto Lb_start;
                }
                else
                {
                    i++;
                    if (i <= 10)
                    {
                        Delay(3);
                        goto Lb_start;
                    }
                }
            }
            catch
            {
            }
            return false;
        }
        public bool loginAvatar(DeviceData device, Account acc)
        {
            try
            {
                List<string> list_text = new List<string>();
                list_text.Add("Menu");
                List<Point> list_point = ninjaadb.FindByXpathDescList(device, "//node[contains(@class,'android.widget.ImageView')]", list_text);
                if (list_point.Count == 1)
                {

                    ClickOnLeapdroidPosition(device, list_point[0].X - 200, list_point[0].X);
                    var point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.EditText')]");
                    if (point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);
                        PressOnLeapdroid(device, acc.Password);
                        list_text = new List<string>();
                        list_text.Add("LOG IN");
                        list_text.Add("Đăng nhập");
                        point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.Button')]", list_text);
                        if (point.Y > 0)
                        {
                            ClickOnLeapdroidPosition(device, point);
                            Thread.Sleep(2000);
                        }

                    }
                    if (checkIsLogin(device))
                    {
                        return true;
                    }
                    else
                    {
                        List<string> list_login = new List<string>();
                        list_login.Add("Phiên đã hết hạn");
                        list_login.Add("Session Expired");
                        string check = ninjaadb.checkListContent(device, SettingTool.lang.list_checklogin);
                        if (check != null)
                        {
                            skipLogin(device);
                        }
                        return false;
                    }

                }

            }
            catch
            {
            }
            return false;
        }

        public bool loginAvatarLD(DeviceData device, Account acc)
        {
            try
            {
                List<string> list_text = new List<string>();
                list_text.Add("Menu");

                //List<DetechModel> list_detech = new List<DetechModel>();
                //DetechModel data = new DetechModel();
                //data.parent = "password";
                //data.content = "Menu";
                //data.text = "Menu";
                //data.node = "//node[contains(@class,'android.widget.ImageView')]";
                //data.function =1;
                //list_detech.Add(data);

                List<Point> list_point = ninjaadb.FindByXpathDescList(device, "//node[contains(@class,'android.widget.ImageView')]", list_text);
                if (list_point.Count == 1)
                {
                    ClickOnLeapdroidPosition(device, list_point[0].X - 400, list_point[0].Y);
                    DetechModel kq = ninjaadb.RunDetechFunction(device, SettingTool.lang.list_detechpass);
                    if (kq.status)
                    {
                        if (kq.function == 2)
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            Delay(1);
                            PressOnLeapdroid(device, acc.Password);
                        }
                    }

                    return true;
                }

            }
            catch
            {
            }
            return false;
        }

        public void ClickOnLeapdroidPosition(DeviceData device, Point point)
        {
            ninjaadb.Tap(device, point.X, point.Y);
        }
        public void ClickOnLeapdroidPosition(DeviceData device, int x, int y)
        {
            ninjaadb.Tap(device, x, y);
        }
        public bool hasLoginFail(DeviceData device, Account acc)
        {
            try
            {
                string check = ninjaadb.checkListContent(device, SettingTool.lang.list_loi);
                if (check != null)
                {
                    acc.Thongbao = check;
                    return true;
                }

            }
            catch
            {
            }
            return false;
        }
        public bool skipLogin(DeviceData device)
        {
            try
            {
                int i = 0;
            Lb_Start:
                bool has_login = checkIsLogin(device);
                if (has_login)
                {
                    return true;
                }

                var point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.Button')]", SettingTool.lang.list_skip);
                if (point.X > 0 || point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point);
                }
                Delay(1);
                i++;
                if (i < 10)
                    goto Lb_Start;

            }
            catch
            {
            }
            return false;
        }
        private bool verify(DeviceData device, string app, Account acc)
        {
            bool result = false;
        Lb_loggin:

            if (ninjaadb.checkContent(device, "android.widget.ProgressBar"))
            {
                Delay(1);
                goto Lb_loggin;
            }
            else
            {
                if (hasLoginFail(device, acc) == false)
                {
                    return skipLogin(device);
                }
                else
                {
                    var point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.Button')]", SettingTool.lang.list_skip);
                    if (point.X > 0 || point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);
                    Lb_process:
                        if (ninjaadb.checkContent(device, "android.widget.ProgressBar"))
                        {
                            Delay(1);
                            goto Lb_process;
                        }
                    }

                    point = ninjaadb.FindByXpathResourceID(device, "//node[contains(@class,'android.widget.EditText')]", "approvals_code");
                    List<string> ls_text = new List<string>();
                    ls_text.Add("Login Code");
                    ls_text.Add("Mã đăng nhập");
                    var point_198 = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.EditText')]", ls_text);
                    if (point.X > 0 || point_198.Y > 0)
                    {
                        if (point.X > 0)
                            ClickOnLeapdroidPosition(device, point);
                        else
                            ClickOnLeapdroidPosition(device, point_198);
                        CustomerController control = new CustomerController();
                        TwoFaModel model = control.getCodeTwofa(acc.email, acc.privatekey);
                        if (string.IsNullOrEmpty(model.message) == false)
                        {
                            PressOnLeapdroid(device, model.message);

                            point = ninjaadb.FindByXpathResourceID(device, "//node[contains(@class,'android.widget.Button')]", "checkpointSubmitButton-actual-button");
                            ls_text.Clear();
                            ls_text.Add("Continue");
                            ls_text.Add("Tiếp tục");
                            point_198 = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.Button')]", ls_text);
                            if (point.Y > 0 || point_198.Y > 0)
                            {
                                if (point.X > 0)
                                    ClickOnLeapdroidPosition(device, point.X + 10, point.Y + 10);
                                else
                                    ClickOnLeapdroidPosition(device, point_198.X + 10, point_198.Y + 10);
                                Delay(1);
                                if (point.X > 0) //neu ban 175 thi back
                                {
                                    back(device, 1);
                                    Delay(1);
                                    ninjaadb.OpenLink(device, acc.app, "fb://newsfeed");
                                }
                                else
                                {
                                    Delay(3);
                                    point_198 = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.Button')]", "OK");
                                    if (point_198.Y > 0)
                                    {
                                        bool result_check = ninjaadb.checkContent(device, "Authentication Error");
                                        ClickOnLeapdroidPosition(device, point_198.X + 10, point_198.Y + 10);

                                        if (result_check)
                                            return false;
                                        else
                                            return true;
                                    }

                                }
                            }
                        }
                    }
                    ls_text.Clear();
                    ls_text.Add("Get Started");
                    ls_text.Add("Bắt đầu");
                    point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.Button')]", ls_text);
                    if (point.X > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);
                        back(device, 1);

                    }
                }

            }

            return result;
        }

        public void open(DeviceData device, string app)
        {
            ninjaadb.OpenLink(device, app, "fb://profile");
        }
        public void seachLikePage(DeviceData device, string app, string keyword, int numLike)
        {
            int max = 0;

         
            activeNewfeed(device, app);
            List<string> ls_text = new List<string>();
            ls_text.Add("Tìm kiếm");
            ls_text.Add("Search");

            var point = ninjaadb.FindByXpath(device, SettingTool.data["likepage"], ls_text);
            if (point.X > 0 || point.Y > 0)
            {
                ClickOnLeapdroidPosition(device, point);
                Delay(1);
                PressOnLeapdroid_vietnamese(device, Base64Encode(FunctionHelper.method_Spin(keyword)));
                Delay(1);
                ls_text.Clear();
                ls_text.Add("Xem kết quả cho");
                ls_text.Add("See results for");
                point = ninjaadb.FindByXpathDesc(device, "//node[contains(@class,'android.view.View')]", ls_text);
                if (point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point);
                    Delay(2);
                    point = ninjaadb.FindByXpathDesc(device, "//node[contains(@class,'android.view.ViewGroup')]", "Video");
                    slide(device, point);
                    ls_text.Clear();
                    ls_text.Add("Pages");
                    ls_text.Add("Trang");
                    Delay(1);
                    point = ninjaadb.FindByXpathDesc(device, "//node[contains(@class,'android.view.View')]", ls_text);
                    if (point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);
                        Delay(2);
                        int dem = 0;
                        while (dem < numLike)
                        {
                            if (likePage(device))
                            {
                                max = 0;
                                dem++;
                                Delay(1);
                                scroll_up_short(device);
                            }
                            else
                            {
                                scroll_up_short(device);
                                Delay(1);
                                max++;
                                if (max >= 3)
                                {
                                    activeNewfeed(device, app);
                                    return;
                                }
                            }
                        }
                    }
                }

            }
            activeNewfeed(device, app);
        }
        public bool likePage(DeviceData device)
        {
            try
            {
                Random rd = new Random();
                var point = new Point();
                int max = 0;
                List<Point> ls_point = ninjaadb.FindByXpathList(device, "((//node[contains(@class,'android.support.v7.widget.RecyclerView')])[2]//node[contains(@class,'android.view.View')])");

                while (max < 5)
                {
                    point = ls_point[rd.Next(0, ls_point.Count)];

                    if (point.Y > 0 & point.X > 0)
                    {
                        ClickOnLeapdroidPosition(device, ls_point[rd.Next(0, ls_point.Count)]);
                        Delay(2);
                        point = ninjaadb.FindByXpath(device, "(//node[contains(@class,'android.view.View')])[5]");
                        if (point.Y > 0)
                        {
                            ClickOnLeapdroidPosition(device, point.X + 435, point.Y + 45);
                            back(device, 1);
                            return true;
                        }

                    }
                    max++;
                }

            }
            catch
            { }
            return false;
        }
        public string viewAddFriend(DeviceData ldID, string app, int num, int delay)
        {
            try
            {

                int dem = 0;
                string success = ninjaadb.OpenLink(ldID, app, "fb://friends/center");
                Delay(3);
                #region bat dau addfriend
                try
                {
                    int i = 0;
                Lb_start:
                    DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_detechaddfriensuggest);
                    if (kq.status)
                    {
                        i = 0;
                        switch (kq.function)
                        {
                            case -1:
                                {
                                    ClickOnLeapdroidPosition(ldID, kq.point);
                                    return "| Add Friend Không hoàn thành";
                                }
                            case 1:
                                {
                                    ClickOnLeapdroidPosition(ldID, kq.point);
                                    break;
                                }
                            case 2:
                                {
                                    ClickOnLeapdroidPosition(ldID, kq.point);
                                    dem++;
                                    if (dem >= num)
                                    {
                                        goto Finish;
                                    }
                                    else
                                    {
                                        Delay(delay);
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        goto Lb_start;
                    }
                    else
                    {
                        Delay(6);
                        i++;
                        if (i < 5)
                        {
                            goto Lb_start;
                        }
                    }
                }
                catch
                { }
                #endregion
            //}
            Finish:
                activeNewfeed(ldID, app);
                return "| Add Friend  hoàn thành:" + dem.ToString() + "/" + num.ToString();
            }
            catch
            {
                return "| Add Friend  hoàn thành:" + "0/" + num.ToString();
            }

        }

        private void setupDetechLikePage()
        {

        }
        public string viewInviteLikePage(DeviceData device, string app, int numInvite, string lsID, int delay)
        {
            string message = "";
            try
            {
                if (lsID != "")
                {
                    string[] ls_link = lsID.Split(',');

                    DetechModel data = new DetechModel();
                    data.parent = "search";
                    data.content = "Search";
                    data.text = "Search";
                    data.node = "//node[contains(@class,'android.widget.TextView')]";
                    data.function = 1;
                    SettingTool.lang.list_invite2page.Add(data);
                    data = new DetechModel();
                    data.parent = "search";
                    data.content = "tìm kiếm";
                    data.text = "tìm kiếm";
                    data.node = "//node[contains(@class,'android.widget.TextView')]";
                    data.function = 1;
                    SettingTool.lang.list_invite2page.Add(data);

                    List<DetechModel> ls_send = new List<DetechModel>();
                    DetechModel detech = new DetechModel();
                    detech.content = "Invite selected friends";
                    detech.text = "Invite selected friends";
                    detech.node = "//node[contains(@class,'android.widget.ImageView')]";
                    ls_send.Add(detech);
                    detech = new DetechModel();
                    detech.content = "Mời bạn bè đã chọn";
                    detech.text = "Mời bạn bè đã chọn";
                    detech.node = "//node[contains(@class,'android.widget.ImageView')]";
                    ls_send.Add(detech);

                    for (int i = 0; i < ls_link.Length; i++)
                    {
                        int count = 0;
                        int step = 0;
                        int exit_check = 0;
                        string result = ninjaadb.OpenLink(device, app, "fb://page/" + ls_link[i]);
                        Delay(3);
                        if (!result.Contains("Error"))
                        {
                        Lb_continued:
                            DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_invite2page);
                            if (kq.status)
                            {
                                switch (kq.function)
                                {
                                    case -1:
                                        {
                                            Thread.Sleep(5000);
                                            break;
                                        }
                                    case 0:
                                        {
                                            back(device, 1);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (kq.parent == "invite2page")
                                                ClickOnLeapdroidPosition(device, kq.point.X, kq.point.Y);
                                            if (kq.parent == "search")
                                            {
                                            lb_check:
                                                scroll_up_random(device);
                                                List<Point> ls_check = new List<Point>();
                                                ls_check = ninjaadb.FindByXpathListUncheck(device, "//node[contains(@class,'android.widget.CheckBox')]");
                                                if (ls_check.Count > 0)
                                                {
                                                    exit_check = 0;
                                                    for (int y = 0; y < ls_check.Count; y++)
                                                    {
                                                        ClickOnLeapdroidPosition(device, ls_check[y]);
                                                        Delay(1);
                                                        count++;
                                                        if (count >= numInvite)
                                                        {
                                                            kq = ninjaadb.detechFunction(device, ls_send);
                                                            if (kq.point.Y > 0)
                                                            {
                                                                ClickOnLeapdroidPosition(device, kq.point);
                                                            }
                                                            message += " |Page " + (i + 1).ToString() + ": invite " + count.ToString() + "/" + numInvite.ToString();
                                                            goto lb_finish;
                                                        }
                                                    }
                                                    if (count < numInvite)
                                                    {
                                                        goto lb_check;
                                                    }
                                                }
                                                else
                                                {
                                                    exit_check++;
                                                    if (exit_check >= 3)
                                                    {
                                                        kq = ninjaadb.detechFunction(device, ls_send);
                                                        ClickOnLeapdroidPosition(device, kq.point);
                                                        message += " |Page " + (i + 1).ToString() + ": invite " + count.ToString() + "/" + numInvite.ToString();
                                                        goto lb_finish;
                                                    }
                                                    goto lb_check;
                                                }
                                            }
                                            //if (kq.parent == "search")
                                            goto Lb_continued;

                                        }
                                }

                            }
                            else
                            {
                                step++;
                                if (step <= 5)
                                {
                                    goto Lb_continued;
                                }
                            }

                        }
                    lb_finish:
                        Delay(delay);
                    }
                }
            }
            catch
            {
            }
            return message;
        }

        public string viewInviteGroup(DeviceData ldID, string app, int numInvite, string lsID, int delay)
        {
            string message = "";
            try
            {
                if (lsID != "")
                {
                    string[] ls_link = lsID.Split(',');

                    List<DetechModel> list_detech = new List<DetechModel>();
                    DetechModel data = new DetechModel();
                    data = new DetechModel();
                    data.parent = "search";
                    data.content = "Search friends";
                    data.text = "Search friends";
                    data.node = "//node[contains(@class,'android.widget.EditText')]";
                    data.function = 1;
                    list_detech.Add(data);

                    data = new DetechModel();
                    data.parent = "search";
                    data.content = "Tìm kiếm bạn bè";
                    data.text = "Tìm kiếm bạn bè";
                    data.node = "//node[contains(@class,'android.widget.EditText')]";
                    data.function = 1;
                    list_detech.Add(data);

                    data = new DetechModel();
                    data.parent = "invite2group";
                    data.content = "Add";
                    data.text = "Add";
                    data.node = "//node[contains(@class,'android.view.View')]";
                    data.function = 1;
                    list_detech.Add(data);

                    data = new DetechModel();
                    data.parent = "invite2group";
                    data.content = "Thêm";
                    data.text = "Thêm";
                    data.node = "//node[contains(@class,'android.view.View')]";
                    data.function = 1;
                    list_detech.Add(data);

                    for (int i = 0; i < ls_link.Length; i++)
                    {
                        int count = 0;
                        int step = 0;
                        int exit_check = 0;
                        string result = ninjaadb.OpenLink(ldID, app, "fb://group/" + ls_link[i]);
                        Delay(3);
                        if (!result.Contains("Error"))
                        {
                        Lb_continued:
                            DetechModel kq = ninjaadb.detechFunction(ldID, list_detech);
                            if (kq.status)
                            {
                                switch (kq.function)
                                {
                                    case -1:
                                        {
                                            Thread.Sleep(5000);
                                            break;
                                        }
                                    case 0:
                                        {
                                            back(ldID, 1);
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (kq.parent == "invite2group")
                                                ClickOnLeapdroidPosition(ldID, kq.point.X, kq.point.Y);
                                            else if (kq.parent == "search")
                                            {
                                            lb_check:
                                                Random rd = new Random();
                                                int random = rd.Next(0, 6);
                                                if (random == 3)
                                                {
                                                    scroll_up_short(ldID);

                                                    scroll_up_mid(ldID);
                                                }

                                                else if (random == 2)
                                                {

                                                    scroll_up_mid(ldID);
                                                }
                                                else
                                                    scroll_up(ldID);

                                                List<Point> ls_check = new List<Point>();
                                                List<string> ls_text = new List<string>();
                                                ls_text.Add("Thêm");
                                                ls_text.Add("Invite");
                                                ls_text.Add("Add");
                                                List<string> ls_path = new List<string>();
                                                ls_check = ninjaadb.FindByXpathDescList(ldID, "//node[contains(@class,'android.view.View')]", ls_text);
                                                if (ls_check.Count > 0)
                                                {
                                                    exit_check = 0;
                                                    for (int y = 0; y < ls_check.Count; y++)
                                                    {
                                                        ClickOnLeapdroidPosition(ldID, ls_check[y]);
                                                        count++;
                                                        if (count >= numInvite)
                                                        {
                                                            message += " |Group " + (i + 1).ToString() + ": invite " + count.ToString() + "/" + numInvite.ToString();
                                                            goto lb_finish;
                                                        }

                                                        Delay(delay);
                                                    }
                                                    if (count < numInvite)
                                                    {
                                                        goto lb_check;
                                                    }
                                                }
                                                else
                                                {
                                                    exit_check++;
                                                    if (exit_check >= 3)
                                                    {
                                                        message += " |Group " + (i + 1).ToString() + ": invite " + count.ToString() + "/" + numInvite.ToString();
                                                        goto lb_finish;
                                                    }
                                                    else
                                                    {
                                                        goto lb_check;
                                                    }
                                                }

                                            }
                                            if (kq.parent == "invite2group")
                                                goto Lb_continued;
                                            break;
                                        }
                                }

                            }
                            else
                            {
                                step++;
                                if (step <= 5)
                                {
                                    goto Lb_continued;
                                }
                                else
                                    goto lb_finish;
                            }

                        }
                    lb_finish:
                        Delay(delay);
                    }
                }
                activeNewfeed(ldID, app);
            }
            catch
            {
            }
            return message;
        }

        public void viewCancelFriend(DeviceData deviceid, string app, int num, int delay)
        {
            try
            {
                int dem = 0;
                int max = 0;
                string check;
                //  check_Facebook_has_stopped(deviceid);

                string result = ninjaadb.OpenLink(deviceid, app, "fb://faceweb/f?href=%2Ffriends%2Fcenter%2Frequests%2Foutgoing%2F");
                Delay(3);
                if (!result.Contains("Error"))
                {
                    List<string> xDesc = new List<string>();
                    xDesc.Clear();
                    xDesc.Add("Undo");
                    xDesc.Add("Hoàn tác");
                lb_start:
                    var lsPoint = ninjaadb.FindByXpathDescList(deviceid, "//node[contains(@class,'android.widget.Button')]", xDesc);
                    if (lsPoint.Count > 0)
                    {
                        max = 0;
                        for (int i = 0; i < lsPoint.Count; i++)
                        {
                            if (lsPoint[i].X > 0 & lsPoint[i].Y > 0)
                            {
                                ClickOnLeapdroidPosition(deviceid, lsPoint[i].X + 10, lsPoint[i].Y + 10);
                                dem++;
                            }

                        }

                        if (dem <= num)
                        {
                            Delay(2);
                            scroll_up(deviceid);
                            goto lb_start;
                        }

                        else
                        {
                            activeNewfeed(deviceid, app);
                            return;
                        }

                    }
                    else
                    {
                        max++;
                        if (max >= 3)
                        {
                            activeNewfeed(deviceid, app);
                            return;
                        }
                        scroll_up(deviceid);
                        goto lb_start;

                    }
                }
            }
            catch
            {
            }
        }
        public string viewAddFriendbyNewFeed(DeviceData ldID, string app, int num, int delay)
        {
            try
            {
                int dem = 0;
                int checkShare = 0;
                int int_scroll = 0;
                int step = 0;
            Lb_continued:
                backNewfeed(ldID);
                scroll_up_random(ldID);
                DetechModel kqnewfeed = ninjaadb.RunDetechFunction(ldID, SettingTool.lang.list_newfeed);
                if (kqnewfeed.status)
                {
                Lb_start:
                    DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_addfriendnewfeed);
                    if (kq.status)
                    {
                        switch (kq.function)
                        {
                            case -2:
                                {
                                    goto Lb_continued;
                                }
                            case -1:
                                {
                                    Thread.Sleep(5000);
                                    break;
                                }
                            case 0:
                                {
                                    back(ldID, 1);
                                    break;
                                }
                            case 1:
                                {
                                    ClickOnLeapdroidPosition(ldID, kq.point);
                                    Delay(1);

                                    if (kq.content.ToLower().Contains("bình luận") || kq.content.ToLower().Contains("comment"))
                                    {
                                        checkShare++;
                                        if (checkShare >= 3)
                                        {
                                            backNewfeed(ldID);
                                            checkShare = 0;
                                            scroll_up_random(ldID);
                                            goto Lb_start;
                                        }

                                    }
                                    else
                                        checkShare = 0;

                                    if (kq.content.ToLower() == "hãy là người đầu tiên thích nội dung này" || kq.content.ToLower().Contains("first"))
                                    {
                                        backNewfeed(ldID);
                                        scroll_up_random(ldID);
                                    }

                                    break;
                                }
                            case 2:
                                {

                                    step = 0;
                                Lb_Send:
                                    DetechModel kqsend = ninjaadb.detechFunction(ldID, SettingTool.lang.list_buttonadd);
                                    if (kqsend.status)
                                    {
                                        ClickOnLeapdroidPosition(ldID, kqsend.point);
                                        dem++;
                                        if (dem >= num)
                                        {
                                            backNewfeed(ldID);
                                            return "| Add Friend by newfeed  hoàn thành:" + dem.ToString() + "/" + num.ToString();
                                        }
                                        Thread.Sleep(delay);
                                        goto Lb_Send;
                                    }
                                    else
                                    {
                                        scroll_up_mid(ldID);
                                        step++;
                                        if (step <= 3)
                                            goto Lb_Send;
                                        else
                                        {
                                            scroll_up_mid(ldID);
                                            step++;
                                            if (step <= 3)
                                                goto Lb_Send;
                                            else
                                            {
                                                goto Lb_continued;
                                            }
                                        }

                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        goto Lb_start;
                    }
                    else
                    {
                        scroll_up_random(ldID);
                        step++;
                        if (step <= 5)
                        {
                            goto Lb_start;

                        }
                        else
                        {


                            int_scroll++;

                            if (int_scroll <= 3)
                            {
                                backNewfeed(ldID);
                                scroll_up_random(ldID);

                                goto Lb_continued;
                            }
                            else
                            {
                                int_scroll++;
                                if (int_scroll <= 2)
                                {
                                    goto Lb_continued;
                                }
                                else
                                {
                                    return "| Add Friend by newfeed hoàn thành:" + dem.ToString() + "/" + num.ToString(); ;
                                }

                            }
                        }
                    }
                    //else
                    //{
                    //    back(ldID, 1);
                    //    goto Lb_continued;

                    //}
                }
                return "| Add Friend by newfeed hoàn thành:" + dem.ToString() + "/" + num.ToString();
            }
            catch
            {
            }
            return "| Add Friend by newfeed  hoàn thành:" + "0/" + num.ToString();

        }
        public bool AcceptFriend(DeviceData device)
        {
            try
            {
                List<string> ls_text = new List<string>();
                ls_text.Add("Confirm");
                ls_text.Add("Chấp nhận");
                var point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.TextView')]", ls_text);
                if (point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point.X + 20, point.Y + 10);
                    return true;
                }
            }
            catch { }
            return false;
        }
        public int addFriendUID(DeviceData deviceID, Account acc, string uid)
        {
            try
            {
                ninjaadb.OpenLink(deviceID, acc.app, "fb://profile/" + uid);
                Thread.Sleep(3000);
                List<string> ls_xpath = new List<string>();
                // ls_xpath.Add("//node[contains(@class,'android.view.ViewGroup')]");
                ls_xpath.Add("//node[contains(@class,'android.view.View')]");
                int max = 0;
            Lb_loop:
                var point = ninjaadb.FindByListXpath(deviceID, ls_xpath, SettingTool.lang.list_addfriend);
                if (point.Y == 0)
                {
                    string xpath = "//node[contains(@class,'android.support.v7.widget.RecyclerView')]//node";
                    point = ninjaadb.FindByXpath(deviceID, xpath, 7);
                }
                if (point.X == 0 || point.Y > 0)
                {
                    ClickOnLeapdroidPosition(deviceID, point.X + 100, point.Y + 10);
                    back(deviceID, 1);
                    return 1;
                }
                else
                {
                    max++;
                    Delay(3);
                    if (max < 5)
                        goto Lb_loop;

                }
            }
            catch { }
            return 2;
        }
        public string scrollAceeptFriend(DeviceData ldID, string app, int numLike, int delay)
        {
            try
            {
                int dem = 0;
                activeNewfeed(ldID, app);
                ninjaadb.OpenLink(ldID, app, "fb://friends/requests_tab");
                Delay(3);
                #region bat dau acceptfriend
                try
                {
                    int i = 0;
                Lb_start:
                    DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_detechacceptfriend);
                    if (kq.status)
                    {
                        i = 0;
                        ClickOnLeapdroidPosition(ldID, kq.point);
                        dem++;
                        if (dem >= numLike)
                        {
                            goto Finish;
                        }
                        else
                        {
                            Delay(delay);
                        }
                        goto Lb_start;
                    }
                    else
                    {
                        i++;
                        Delay(3);
                        ninjaadb.OpenLink(ldID, app, "fb://friends/requests_tab");
                        if (i < 5)
                        {
                            goto Lb_start;
                        }
                    }
                }
                catch
                { }
                #endregion
          
            Finish:
                activeNewfeed(ldID, app);
                return "| Accept Friend  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();

            }
            catch
            {
                return "| Accept Friend  hoàn thành:" + "0/" + numLike.ToString();
            }
        }
        public void check_Facebook_has_stopped(DeviceData device, Account acc)
        {
            List<DetechModel> ls_detech = new List<DetechModel>();
            DetechModel detech = new DetechModel();
            detech.parent = "";
            detech.content = "Phiên đã hết hạn";
            detech.text = "ĐỒNG Ý";
            detech.node = "//node[contains(@class,'android.widget.Button')]";
            detech.function = 1;
            ls_detech.Add(detech);

            detech = new DetechModel();
            detech.parent = "";
            detech.content = "Facebook isn't responding.";
            detech.text = "OK";
            detech.node = "//node[contains(@class,'android.widget.Button')]";
            detech.function = 1;
            ls_detech.Add(detech);

            detech = new DetechModel();
            detech.parent = "";
            detech.content = "Session";
            detech.text = "OK";
            detech.node = "//node[contains(@class,'android.widget.Button')]";
            detech.function = 1;
            ls_detech.Add(detech);

            bool haslogin = false;
            DetechModel kq = ninjaadb.detechFunction(device, ls_detech);
            if (kq.status)
            {
                ClickOnLeapdroidPosition(device, kq.point);
                Delay(6);

                bool status = checkIsLogin(device);
                if (status)
                {
                    haslogin = true;
                }
                else
                {

                    loginAvatarLD(device, acc);
                    Delay(3);
                    haslogin = LoginFacebook(device, acc.app, acc);
                }

            }
        }

        public bool shareVideo(DeviceData device)
        {
            try
            {
                var screen = ninjaadb.ScreenShoot(device); ;
                var like = ImageScanOpenCV.GetImage("img\\11-share.png");
                var like2 = ImageScanOpenCV.GetImage("img\\48-shareblack.png");
                var point = ImageScanOpenCV.FindOutPoint(screen, like);
                var point2 = ImageScanOpenCV.FindOutPoint(screen, like2);
                if (point.X > 0 || point2.X > 0)
                {
                    if (point.X > 0 || point.Y > 0)
                        ClickOnLeapdroidPosition(device, point);
                    else
                        ClickOnLeapdroidPosition(device, point2);

                    Delay(3);
                    screen = ninjaadb.ScreenShoot(device); ;
                    like = ImageScanOpenCV.GetImage("img\\17-sharenow.png");

                    point = ImageScanOpenCV.FindOutPoint(screen, like);
                    if (point.X > 0 || point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point.X, point.Y);
                        Delay(2);
                        return true;
                    }

                    like = ImageScanOpenCV.GetImage("img\\67-sharepostnow.png");
                    point = ImageScanOpenCV.FindOutPoint(screen, like);
                    if (point.X > 0 || point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point.X, point.Y);
                        Delay(2);
                        return true;
                    }
                    like = ImageScanOpenCV.GetImage("img\\68-writepost.png");
                    point = ImageScanOpenCV.FindOutPoint(screen, like);
                    if (point.X > 0 || point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point.X, point.Y);
                        Delay(4);
                        screen = ninjaadb.ScreenShoot(device); ;
                        like = ImageScanOpenCV.GetImage("img\\post.png");
                        point = ImageScanOpenCV.FindOutPoint(screen, like);
                        if (point.X > 0 || point.Y > 0)
                        {
                            ClickOnLeapdroidPosition(device, point.X, point.Y);
                            Delay(2);

                            return true;
                        }
                    }


                }
                return false;
            }
            catch
            { }
            return false;
        }
        public string scrollSharePost(DeviceData Intpt, string app, int numLike, string content)
        {
            int dem = 0;
            int max = 0;
           
            activeNewfeed(Intpt, app);
            while (dem < numLike)
            {
                scroll_up_random(Intpt);
                if (SharePostIntoNewfeed(Intpt, FunctionHelper.method_Spin(content)))
                {
                    dem++;
                    max = 0;
                }
                else
                {
                    max++;
                    if (max == 5)
                        return "| Share post  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
                }
            }
            return "| Share post  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
        }

        public void Notification(DeviceData Intpt, string app)
        {

            ninjaadb.OpenLink(Intpt, app, "fb://notifications");
            Random rd = new Random();
            Thread.Sleep(3000);
            scroll_up_random(Intpt);
            DetechModel kq = ninjaadb.RunDetechFunction(Intpt, SettingTool.lang.list_notification);
            if (kq.status)
            {
                ClickOnLeapdroidPosition(Intpt, kq.point);
                Delay(3);

            }
            activeNewfeed(Intpt, app);
        }
        public string ShareIntoGroup(DeviceData device, DataGridViewRow dr, string link, string content, int numGroup, string app, int i_delay)
        {
            string message = "";
            if (link != "")
            {
                List<DetechModel> ls_detechShare = new List<DetechModel>();
                DetechModel detech = new DetechModel();
                detech.parent = "sharephoto";
                detech.content = "Chia sẻ lên Facebook";
                detech.text = "Chia sẻ lên Facebook";
                detech.node = "//node[contains(@class,'android.widget.Button')]";
                detech.function = 1;
                ls_detechShare.Add(detech);

                detech = new DetechModel();
                detech.parent = "sharephoto";
                detech.content = "Share to Facebook";
                detech.text = "Share to Facebook";
                detech.node = "//node[contains(@class,'android.widget.Button')]";
                detech.function = 1;
                ls_detechShare.Add(detech);

                detech = new DetechModel();
                detech.parent = "sharephoto";
                detech.content = "In a group";
                detech.text = "In a group";
                detech.node = "//node[contains(@class,'android.widget.TextView')]";
                detech.function = 1;
                ls_detechShare.Add(detech);

                detech = new DetechModel();
                detech.parent = "sharephoto";
                detech.content = "trong nhóm";
                detech.text = "trong nhóm";
                detech.node = "//node[contains(@class,'android.widget.TextView')]";
                detech.function = 1;
                ls_detechShare.Add(detech);

                detech = new DetechModel();
                detech.parent = "sharephoto";
                detech.content = "Choose Group";
                detech.text = "Choose Group";
                detech.node = "//node[contains(@class,'android.widget.TextView')]";
                detech.function = 2;
                ls_detechShare.Add(detech);
                detech = new DetechModel();
                detech.parent = "sharephoto";
                detech.content = "chọn nhóm";
                detech.text = "chọn nhóm";
                detech.node = "//node[contains(@class,'android.widget.TextView')]";
                detech.function = 2;
                ls_detechShare.Add(detech);

                List<DetechModel> list_write = new List<DetechModel>();
                DetechModel data = new DetechModel();
                data.parent = "post";
                data.content = "Write something…";
                data.text = "Write something…";
                data.node = "//node[contains(@class,'android.widget.EditText')]";
                data.function = 2;
                list_write.Add(data);

                data = new DetechModel();
                data.parent = "post";
                data.content = "Viết gì đó...";
                data.text = "Viết gì đó...";
                data.node = "//node[contains(@class,'android.widget.EditText')]";
                data.function = 2;
                list_write.Add(data);

                Random rd = new Random();

                string[] ls_link = link.Split(',');
                List<string> list_content = new List<string>();
                for (int i = 0; i < ls_link.Length; i++)
                {
                    int step = 0;
                    bool has_share = false;
                    int totalshare = 0;
                    string result = functionOpenLink(device, app, ls_link[i]);
                    Delay(2);
                    if (!result.Contains("Error"))
                    {
                    Lb_start:
                        if (ls_link[i].Contains("photo"))// xu ly them voi link la photo
                        {
                            DetechModel kq_plus = ninjaadb.detechFunction(device, ls_detechShare);
                            if (kq_plus.status)
                            {
                                step = 0;
                                switch (kq_plus.function)
                                {
                                    case 1:
                                        {
                                            ClickOnLeapdroidPosition(device, kq_plus.point);
                                            Delay(2);
                                            goto Lb_start;
                                        }
                                    case 2:
                                        {
                                            choosegroupandcommnet(device, content);
                                            Delay(1);
                                            has_share = true;
                                            goto Lb_start;
                                        }
                                }
                            }
                        }
                        DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_sharepost);
                        if (kq.status)
                        {
                            step = 0;
                            switch (kq.function)
                            {
                                case -2:
                                    {
                                        dr.Cells["Message"].Value = kq.content;
                                        //return;
                                        message += " | link " + (i + 1).ToString() + ": không hoàn thành";
                                        goto Lb_finish;
                                    }
                                case -1:
                                    {
                                        Thread.Sleep(5000);
                                        break;
                                    }
                                case 0:
                                    {
                                        back(device, 1);
                                        break;
                                    }
                                case 1:
                                    {
                                        ClickOnLeapdroidPosition(device, kq.point);
                                        if (has_share)
                                        {
                                            totalshare++;
                                            dr.Cells["Status"].Value = totalshare;
                                            if (totalshare >= numGroup)
                                            {
                                                message += " | link " + (i + 1).ToString() + ": hoàn thành -" + totalshare.ToString() + "/" + numGroup.ToString();
                                                goto Lb_finish;
                                            }
                                            Delay(i_delay);
                                            has_share = false;
                                        }
                                        Delay(1);
                                        break;
                                    }
                                case 2:
                                    {
                                        ClickOnLeapdroidPosition(device, kq.point);
                                        Delay(2);
                                        //chon 1 nhom bat ki
                                        choosegroupandcommnet(device, content);
                                        has_share = true;
                                        break;
                                    }
                                default:
                                    break;
                            }
                            goto Lb_start;
                        }
                        else
                        {
                            step++;
                            if (step <= 7)
                            {
                                scroll_up_mid(device);
                                goto Lb_start;
                            }
                            else
                                message += " | link " + (i + 1).ToString() + ": share group không hoàn thành";
                        }
                    //het
                    Lb_finish:
                        totalshare = 0;
                    }
                }
            }
            if (message == "")
                message = "Không hoàn thành";
            return message;
        }

        public bool SharePostIntoNewfeed(DeviceData device, string content)
        {
            List<DetechModel> list_write = new List<DetechModel>();
            DetechModel data = new DetechModel();
            data.parent = "post";
            data.content = "something";
            data.text = "something";
            data.node = "//node[contains(@class,'android.widget.EditText')]";
            data.function = 2;
            list_write.Add(data);
            data = new DetechModel();
            data.parent = "post";
            data.content = "gì đó";
            data.text = "gì đó";
            data.node = "//node[contains(@class,'android.widget.EditText')]";
            data.function = 2;
            list_write.Add(data);

            data = new DetechModel();
            data.parent = "post";
            data.content = "post";
            data.text = "post";
            data.node = "//node[contains(@class,'android.widget.Button')]";
            data.function = 2;
            SettingTool.lang.list_shareposttonewfeed.Add(data);
            data = new DetechModel();
            data.parent = "post";
            data.content = "đăng";
            data.text = "đăng";
            data.node = "//node[contains(@class,'android.widget.Button')]";
            data.function = 2;
            SettingTool.lang.list_shareposttonewfeed.Add(data);

            data = new DetechModel();
            data.parent = "sharepost";
            data.content = "chia sẻ";
            data.text = "chia sẻ";
            data.node = "//node[contains(@class,'android.widget.TextView')]";
            data.function = 1;
            SettingTool.lang.list_shareposttonewfeed.Add(data);


            List<string> list_content = new List<string>();
            int step = 0;
            Delay(2);
        Lb_start:
            DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_shareposttonewfeed);
            if (kq.status)
            {
                step = 0;
                switch (kq.function)
                {
                    case -2:
                        {
                            return false;
                        }
                    case -1:
                        {
                            Thread.Sleep(5000);
                            break;
                        }
                    case 0:
                        {
                            back(device, 1);
                            break;
                        }
                    case 1:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            Delay(1);
                            break;
                        }
                    case 2:
                        {
                            DetechModel kqwrite = ninjaadb.RunDetechFunction(device, list_write);
                            if (kqwrite.status)
                            {
                                ClickOnLeapdroidPosition(device, kqwrite.point);
                                Thread.Sleep(1000);
                                PressOnLeapdroid_vietnamese(device, Base64Encode(FunctionHelper.method_Spin(content)));
                                Delay(1);
                            }
                            ClickOnLeapdroidPosition(device, kq.point);
                            goto Lb_finish;
                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                step++;
                if (step <= 10)
                {
                    scroll_up_mid(device);
                    goto Lb_start;
                }
            }
        //het
        Lb_finish:
            return true;

        }
        private void clickSharePost2Newfeed(DeviceData device, string app, string content, Point point)
        {
            ClickOnLeapdroidPosition(device, point);
            List<string> list_content = new List<string>();
            if (content != "")
            {
                list_content = new List<string>();
                list_content.Add("Say something about");
                list_content.Add("Hãy nói gì đó");
                point = ninjaadb.FindByXpath(device, "(//node[contains(@class,'android.widget.EditText')])", list_content);

                if (point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point);
                    string ls_content = FunctionHelper.method_Spin(content);
                    PressOnLeapdroid_vietnamese(device, Base64Encode(ls_content));
                    Delay(1);
                }

            }
            list_content = new List<string>();
            list_content.Add("Chia sẻ ngay");
            list_content.Add("Share Now");
            point = ninjaadb.FindByXpathDesc(device, "(//node[contains(@class,'android.view.View')])", list_content);
            if (point.Y > 0)
            {
                ClickOnLeapdroidPosition(device, point);
                Delay(1);
                activeNewfeed(device, app);
            }
            else
            {
                activeNewfeed(device, app);
            }
        }
        private void getUID(string postID, ref string uID, ref string postIdReturn)
        {
            var client = new RestClient(string.Format("https://m.facebook.com/{0}", postID));
            var request = new RestRequest(Method.GET);
            client.UserAgent = "Mozilla/5.0 (Windows NT 6.3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
            IRestResponse response = client.Execute(request);
            string html = response.Content;
            string key = "story_fbid=";
            uID = FunctionHelper.smethod_6(html, html.IndexOf(key) + key.Length, "\"").Trim();
            uID = FunctionHelper.smethod_6(uID, uID.IndexOf("id=") + 3, "&");
        }

        private string functionOpenLink(DeviceData device, string app, string link)
        {
            string result = "";
            string uid = "";
            string linkOpen = "";
            string postId = "";

            if (link.Contains("photos"))
            {
                postId = FunctionHelper.smethod_6(link, link.IndexOf("/photos/") + 8, "?");
                postId = FunctionHelper.smethod_6(postId, postId.IndexOf("/") + 1, "/");
                linkOpen = string.Format("fb://photo/{0}", postId);
                result = ninjaadb.OpenLink(device, app, linkOpen);

            }
            else if (link.Contains("photo"))
            {

                postId = FunctionHelper.smethod_6(link, link.IndexOf("fbid=") + 5, "&");
                linkOpen = string.Format("fb://photo/{0}", postId);
                result = ninjaadb.OpenLink(device, app, linkOpen);
            }

            else if (link.Contains("story_fbid"))
            {
                uid = FunctionHelper.smethod_6(link, link.IndexOf("&id=") + 4, "&");
                postId = FunctionHelper.smethod_6(link, link.IndexOf("fbid=") + 5, "&");
                linkOpen = string.Format("fb://faceweb/f?href=%2Fstory.php%3Fstory_fbid%3D{0}%26id%3D{1}%26post_id%3D{1}_{0}", postId, uid);
                result = ninjaadb.OpenLink(device, app, linkOpen);
            }
            else
                result = ninjaadb.OpenLink(device, app, link);

            return result;
        }

        public string SeedingLikeCommentSharePost(DeviceData device, SettingTuongTac setTuongtac, string link, string[] content, string contentShare, decimal numGroup, string app, bool blLikepost, bool blCommentpost, bool blsharepost)
        {
            string message = "";
            string kq = "";
            if (link != "")
            {
                string[] ls_link = link.Split(',');
                List<string> list_content = new List<string>();
                for (int i = 0; i < ls_link.Length; i++)
                {
                    kq = "";
                    string result = functionOpenLink(device, app, ls_link[i]);
                    Delay(2);
                    if (!result.Contains("Error"))
                    {
                        if (blLikepost)
                        {
                            if (likePostLD(device))
                                kq += "- Like hoàn thành";
                            else
                                kq += "- Like không hoàn thành";
                        }
                        if (blCommentpost)
                        {
                            Random rd = new Random();
                            if (commentPostLD(device, content[rd.Next(0, content.Count())]))
                            {
                                back(device, 2);
                                kq += "- Comment hoàn thành";
                            }
                            else
                                kq += "- Comment không hoàn thành";
                        }
                        if (blsharepost)
                        {
                            functionOpenLink(device, app, ls_link[i]);
                            Delay(3);
                            if (SharePostIntoNewfeed(device, FunctionHelper.method_Spin(contentShare)))
                                kq += "- Share tường hoàn thành";
                            else
                                kq += "- Share tường không hoàn thành";
                        }
                        message += "| Link " + (i + 1).ToString() + ":" + kq;
                        Delay(3);
                    }
                    else
                        message += "| Link " + (i + 1).ToString() + ": không mở được";
                }
                activeNewfeed(device, app);
            }
            if (message == "")
                message = "Không hoàn thành";
            return message;
        }

        public void scroll_up_short(DeviceData handle)
        {
            ADBHelper.Swipe(handle.Serial, 300, 900, 300, 700);
        }
        public void scroll_up_2short(DeviceData handle)
        {
            ADBHelper.Swipe(handle.Serial, 300, 900, 300, 850);
        }
        public void scroll_up_short(DeviceData handle, Point pt)
        {
            ADBHelper.Swipe(handle.Serial, pt.X, pt.Y, pt.X, pt.Y - 150);
        }

        private void runShare2Group(DeviceData Intpt, int numGroup, string content)
        {
            int dem = 0;
            int max = 0;
            while (dem < numGroup)
            {
                Delay(2);
                bool hasShareGroup = true;
                if (sharePostIntoGroup(Intpt, content, ref hasShareGroup))
                {
                    dem++;
                    Delay(2);
                }
                if (!hasShareGroup)
                {
                    back(Intpt, 1);
                    return;
                }

                max++;
                if (max == 10)
                {
                    back(Intpt, 1);
                    return;
                }
            }
            back(Intpt, 1);
        }
        public void Seeding(DeviceData device, SettingTuongTac setTuongtac, Account acc)
        {
            List<string> lsReaction = new List<string>();

            if (setTuongtac.chklike)
                lsReaction.Add("chklike");
            if (setTuongtac.chkhaha)
                lsReaction.Add("chkhaha");
            if (setTuongtac.chklove)
                lsReaction.Add("chklove");
            if (setTuongtac.chkwow)
                lsReaction.Add("chkwow");
            if (setTuongtac.chksad)
                lsReaction.Add("chksad");
            if (setTuongtac.chkangry)
                lsReaction.Add("chkangry");
            ninjaadb.OpenLink(device, acc.app, setTuongtac.seedinglink);
            Delay(6);

            if (lsReaction.Count > 0)
            {
                var point = ninjaadb.FindByXpathDesc(device, "//node[contains(@class,'android.widget.Button')]", "Like");
                if (point.X > 0 && point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point);
                }
            }

            back(device, 1);
        }
        public string scrollCommentPostPage(DeviceData handle, string app, int numLike, string content)
        {
            int max = 0;
            string mess = "";
            
            ninjaadb.OpenLink(handle, app, "fb://pages/launchpoint/");

            Delay(6);

            List<DetechModel> detechFeed = new List<DetechModel>();
            List<DetechModel> detechexit = new List<DetechModel>();
            DetechModel model = new DetechModel();
            model.parent = "exit";
            model.content = "Không thể kết nối";
            model.text = "Không thể kết nối";
            model.node = "//node[contains(@class,'android.widget.TextView')]";
            model.function = 1;
            detechexit.Add(model);

            model = new DetechModel();
            model.parent = "exit";
            model.content = "can't connect";
            model.text = "can't connect";
            model.node = "//node[contains(@class,'android.widget.TextView')]";
            model.function = 1;
            detechexit.Add(model);
            model = new DetechModel();
            model.parent = "clickfeed";
            model.content = "Feed";
            model.text = "Feed";
            model.node = "//node[contains(@class,'android.widget.TextView')]";
            model.function = 1;
            detechFeed.Add(model);

            model = new DetechModel();
            model.parent = "clickfeed";
            model.content = "Bảng tin";
            model.text = "Bảng tin";
            model.node = "//node[contains(@class,'android.widget.TextView')]";
            model.function = 1;
            detechFeed.Add(model);
        lb_start:
            DetechModel kq = ninjaadb.RunDetechFunction(handle, detechFeed);
            if (kq.status)
            {
                ClickOnLeapdroidPosition(handle, kq.point);
                Delay(4);
                kq = ninjaadb.RunDetechFunction(handle, detechexit);
                if (kq.parent == "exit")
                {
                    activeNewfeed(handle, app);
                    return "| Không có newfeed của page";
                }
                int dem = 0;
                while (dem < numLike)
                {
                    scroll_up_random(handle);
                    if (commentPostLD(handle, FunctionHelper.method_Spin(content)))
                    {
                        max = 0;
                        dem++;
                        Delay(1);
                        back(handle, 2);
                    }
                    else
                    {
                        max++;
                        if (max >= 5)
                        {
                            activeNewfeed(handle, app);
                            return mess = "| Comment fanpage hoàn thành:" + dem.ToString() + "/" + numLike;
                        }
                    }
                }
                activeNewfeed(handle, app);
                return mess = "| Comment fanpage  hoàn thành:" + dem.ToString() + "/" + numLike;
            }
            else
            {
                max++;
                if (max <= 5)
                {
                    goto lb_start;
                }
                else
                {
                    activeNewfeed(handle, app);
                    return mess = "| Comment fanpage  hoàn thành:" + "0 /" + numLike;
                }
            }
        }

        public string scrollCommentNewFeed(DeviceData device, string app, int numLike, string content)
        {
            int dem = 0;
            int max = 0;
            
            activeNewfeed(device, app);
            while (dem < numLike)
            {
                scroll_up(device);
                scroll_up(device);
                scroll_up(device);
                if (commentPostLD(device, content))
                {
                    max = 0;
                    dem++;
                    Delay(1);
                    back(device, 2);
                }
                else
                {
                    max++;
                    backNewfeed(device);
                }


                if (max >= 5)
                    return "| Comment newfeed  hoàn thành:" + dem.ToString() + "/" + numLike;
            }
            return "| Comment newfeed  hoàn thành:" + dem.ToString() + "/" + numLike;
        }


        public bool commentPostLD(DeviceData ldID, string mess)
        {
            try
            {
                int i = 0;
                int max = 0;
                DetechModel model = new DetechModel();
                model.parent = "closepopup";
                model.content = "How to Watch Without Comments";
                model.text = "Close";
                model.node = "//node[contains(@class,'android.widget.TextView')]";
                model.function = 1;
                SettingTool.lang.list_detechcomment.Add(model);
            Lb_start:
                DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_detechcomment);
                if (kq.status & kq.point.Y > 400)
                {
                    i = 0;
                    switch (kq.function)
                    {
                        case 1:
                            {
                                if (kq.parent == "comment")
                                {
                                    if (max > 3)
                                        return false;
                                    max++;
                                }
                                ClickOnLeapdroidPosition(ldID, kq.point);
                                break;
                            }
                        case 2:
                            {
                                ClickOnLeapdroidPosition(ldID, kq.point);
                                PressOnLeapdroid_vietnamese(ldID, Base64Encode(FunctionHelper.method_Spin(mess)));
                                Delay(1);
                                i = 0;
                            Lb_Send:
                                DetechModel kqsend = ninjaadb.RunDetechFunction(ldID, SettingTool.lang.list_detechsend);
                                if (kqsend.status)
                                {
                                    ClickOnLeapdroidPosition(ldID, kqsend.point);
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    i++;
                                    if (i <= 5)
                                        goto Lb_Send;
                                }
                                return true;

                            }
                        default:
                            break;
                    }
                    goto Lb_start;
                }
                else
                {
                    scroll_up_mid(ldID);
                    i++;
                    if (i <= 5)
                    {
                        goto Lb_start;

                    }

                }
            }
            catch
            { }
            return false;
        }

        public string scrollLikeNewFeed(DeviceData device, string app, int numLike)
        {

            string message = "";
           
            activeNewfeed(device, app);
            int max = 0;
            int dem = 0;
            while (dem < numLike)
            {
                Delay(2);
                if (likePostLD(device))
                {
                    max = 0;
                    dem++;
                }
                else
                {
                    max++;
                    if (max >= 5)
                        return message = "| Like newfeed  hoàn thành:" + dem.ToString() + "/" + numLike;
                }
                scroll_up(device);
                scroll_up(device);
                scroll_up(device);
            }
            return "| Like newfeed  hoàn thành:" + dem.ToString() + "/" + numLike;
        }

        public string scrollLikePostPage(DeviceData handle, string app, int numLike)
        {
            int max = 0;
            string mess = "";
            ninjaadb.OpenLink(handle, app, "fb://pages/launchpoint/");
            Delay(3);
            List<DetechModel> detechFeed = new List<DetechModel>();
            DetechModel model = new DetechModel();
            model.parent = "clickfeed";
            model.content = "Feed";
            model.text = "Feed";
            model.node = "//node[contains(@class,'android.widget.TextView')]";
            model.function = 1;
            detechFeed.Add(model);

            model = new DetechModel();
            model.parent = "clickfeed";
            model.content = "Bảng tin";
            model.text = "Bảng tin";
            model.node = "//node[contains(@class,'android.widget.TextView')]";
            model.function = 1;
            detechFeed.Add(model);
        lb_start:
            DetechModel kq = ninjaadb.RunDetechFunction(handle, detechFeed);
            if (kq.status)
            {
                ClickOnLeapdroidPosition(handle, kq.point);
                Delay(3);
                max = 0;
                int dem = 0;
                while (dem < numLike)
                {
                    scroll_up_random(handle);
                    if (likePostLD(handle))
                    {
                        max = 0;
                        dem++;
                        Delay(1);
                    }
                    else
                    {
                        max++;
                        if (max >= 3)
                        {
                            activeNewfeed(handle, app);
                            return mess = "| Like post fanpage hoàn thành:" + dem.ToString() + "/" + numLike;
                        }
                    }
                }
                activeNewfeed(handle, app);
                return mess = "| Like post fanpage  hoàn thành:" + dem.ToString() + "/" + numLike;
            }
            else
            {
                max++;
                if (max <= 5)
                    goto lb_start;
            }

            return "| Like post fanpage  hoàn thành:" + "0/" + numLike;
        }
        public string scrollLikeGroup(DeviceData handle, string app, int numLike)
        {
            int dem = 0;
            int step = 0;
            
            //var point = intoGroups(handle);
            int max = 0;
            ninjaadb.OpenLink(handle, app, "fb://groups/gridtab");
        lb_start:
            var pt_group = clickGroup(handle);
            if (pt_group.X > 0)
            {
                Delay(1);
                while (dem < numLike)
                {
                    scroll_up(handle);
                    scroll_up(handle);
                    scroll_up(handle);
                    if (likePostLD(handle))
                    {
                        step = 0;
                        dem++;
                        max = 0;
                    }
                    else
                    {
                        max++;
                        if (max > 6)
                        {
                            activeNewfeed(handle, app);
                            return "| Like post Group  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
                        }
                    }
                }
                activeNewfeed(handle, app);
                return "| Like post Group  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
            }
            else
            {
                step++;
                if (step > 5)
                {
                    activeNewfeed(handle, app);
                    return "| Like post Group  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
                }
                else
                    goto lb_start;
            }
        }

        public void scrollNewfeed(DeviceData device, string app,int max)
        {
            activeNewfeed(device, app);
            for (int i = 0; i < max; i++)
            {
                DetechModel kq = ninjaadb.RunDetechFunction(device, SettingTool.lang.list_newfeed);
                if (kq.status)
                {
                    scroll_up(device);
                    Thread.Sleep(1000);
                }
                else
                {
                    back(device, 1);
                }
            }
        }
        public void activeProfile(DeviceData device, string app)
        {
            ninjaadb.OpenLink(device, app, " fb://profile");
        }
        public void activeNewfeed(DeviceData device, string app)
        {
            ninjaadb.OpenLink(device, app, "fb://newsfeed");

        }
        public string scrollJoinGroup(DeviceData Intpt, string app, int numLike, string keyword, int delay, bool autoAnswer)
        {
            return joinGroupbySerach(Intpt, app, numLike, keyword, delay, autoAnswer);

        }

        public bool scrollJoinGroupbyUID(DeviceData Intpt, string app, string uid, int delay, bool autoAnswer)
        {
            return joinGroupbyUID(Intpt, app, uid, delay, autoAnswer);
        }
        private string joinGroupbySerach(DeviceData ldid, string app, int numLike, string keyword, int delay, bool autoAnswer)
        {
            int dem = 0;
            int max = 0;
            int loop = 0;
            Random rd = new Random();
            ninjaadb.OpenLink(ldid, app, "fb://groups/gridtab");
            Delay(3);
            List<DetechModel> detechsearch = new List<DetechModel>();
            DetechModel model = new DetechModel();
            model.parent = "clicksearch";
            model.content = "Search Groups";
            model.text = "Search Groups";
            model.node = "//node[contains(@class,'android.widget.EditText')]";
            model.function = 1;
            detechsearch.Add(model);

            model = new DetechModel();
            model.parent = "clicksearch";
            model.content = "Tìm kiếm nhóm";
            model.text = "Tìm kiếm nhóm";
            model.node = "//node[contains(@class,'android.widget.EditText')]";
            model.function = 1;
            detechsearch.Add(model);
        lb_loop:
            DetechModel kq = ninjaadb.RunDetechFunction(ldid, detechsearch);
            if (kq.status)
            {
                ClickOnLeapdroidPosition(ldid, kq.point);
                Delay(1);
                PressOnLeapdroid_vietnamese(ldid, FunctionHelper.method_Spin(keyword));
                Delay(2);
                loop = 0;
            lb_start:
                while (dem < numLike)
                {
                    List<Point> ls_point_group = new List<Point>();
                    ls_point_group = ninjaadb.FindByDescList(ldid, "(//node[contains(@class,'android.widget.ScrollView')])", "(//node[contains(@class,'android.widget.ImageView')])");
                    if (ls_point_group.Count == 0)
                    {
                        back(ldid, 1);
                        loop++;
                        if (loop < 10)
                            goto lb_start;
                    }
                    var point = ls_point_group[rd.Next(0, ls_point_group.Count)];
                    if (point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(ldid, point);
                        if (joinGroupLD(ldid, autoAnswer))
                        {
                            dem++;
                            max = 0;
                            scroll_up_mid(ldid);
                        }
                        else
                        {
                            max++;
                            if (max >= 5)
                            {
                                activeNewfeed(ldid, app);
                                return "| Join Group hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
                            }
                        }
                        scroll_up_mid(ldid);
                    }
                    else
                    {
                        max++;
                        if (max >= 5)
                        {
                            activeNewfeed(ldid, app);
                            return "| Join Group hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
                        }
                    }
                }
            }
            else
            {
                loop++;
                if (loop <= 6)
                {
                    goto lb_loop;
                }
            }
            activeNewfeed(ldid, app);
            return "| Join Group hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
        }
        private bool sendToAdmin(DeviceData Intpt, Point point, bool autoAnswer)
        {
            if (!autoAnswer)
            {
                var result = MessageBox.Show("Hãy điền xong câu trả lời, rồi chọn đồng ý để tiếp tục", "Thông báo", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    return true;
                }
            }
            var point_198 = ninjaadb.FindByXpath(Intpt, "//node[contains(@class,'android.widget.EditText')]");
            List<string> list_content = new List<string>();
            list_content.Add("SEND TO ADMINS");
            list_content.Add("GỬI CHO QUẢN TRỊ VIÊN");

            if (point_198.Y > 0)
            {
                ClickOnLeapdroidPosition(Intpt, point_198);
                PressOnLeapdroid_vietnamese(Intpt, Base64Encode(FunctionHelper.method_Spin("{Ok|có|đồng ý|chấp thuận|sẵn sàng|quan tâm|thực sự}")));
                Delay(1);
                point = ninjaadb.FindByXpathDesc(Intpt, "//node[contains(@class,'android.view.ViewGroup')]", list_content);
                if (point.Y > 0)
                {
                    ClickOnLeapdroidPosition(Intpt, point);
                    return true;
                }
                return false;
            }
            else
            {
                var screen = ninjaadb.ScreenShoot(Intpt);
                var like1 = ImageScanOpenCV.GetImage("img\\1_write_answer.png");
                var point1 = ImageScanOpenCV.FindOutPoint(screen, like1);
                var like2 = ImageScanOpenCV.GetImage("img\\1_write_answer_vn.png");
                var point2 = ImageScanOpenCV.FindOutPoint(screen, like2);
                if (point1.Y > 0 || point2.Y > 0)
                {
                    if (point1.Y > 0)
                        ClickOnLeapdroidPosition(Intpt, point1);
                    else
                        ClickOnLeapdroidPosition(Intpt, point2);

                    PressOnLeapdroid(Intpt, "Ok");
                    Delay(1);
                    point2 = ninjaadb.FindByXpathDesc(Intpt, "//node[contains(@class,'android.widget.Button')]", list_content);
                    if (point2.Y > 0)
                    {
                        ClickOnLeapdroidPosition(Intpt, point2);
                        return true;
                    }
                    else
                    {
                        point2 = ninjaadb.FindByXpathDesc(Intpt, "//node[contains(@class,'android.view.ViewGroup')]", list_content);
                        if (point2.Y > 0)
                        {
                            ClickOnLeapdroidPosition(Intpt, point2);
                        }
                    }
                }
                else
                {
                    return false;
                }
                return false;
            }
        }
        private bool joinGroupbyUID(DeviceData Intpt, string app, string uid, int delay, bool autoAnswer)
        {
            
            ninjaadb.OpenLink(Intpt, app, "fb://group/" + uid);
            Delay(delay);
            return joinGroupLD(Intpt, autoAnswer);

        }

        private bool joinGroupLD(DeviceData ldID, bool autoAnswer)
        {
            try
            {
                int i = 0;
                int step_send = 0;
                bool success = false;
            Lb_start:
                DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_joingroup);
                if (kq.status)
                {
                    i = 0;
                    switch (kq.function)
                    {
                        case 0:
                            {
                                back(ldID, 1);
                                if (success)
                                    return true;
                                else
                                    return false;
                              
                            }
                        case 1:
                            {
                                ClickOnLeapdroidPosition(ldID, kq.point);
                                if (kq.parent == "clickJoin" )
                                    success = true;

                                Delay(1);
                                break;
                            }
                        case 2:
                            {
                                ClickOnLeapdroidPosition(ldID, kq.point);
                                Delay(1);
                                if (!autoAnswer)
                                {
                                    var result = MessageBox.Show("Hãy điền xong câu trả lời, rồi chọn đồng ý để tiếp tục", "Thông báo", MessageBoxButtons.OKCancel);
                                    if (result == DialogResult.OK)
                                    {
                                        return true;
                                    }
                                }
                                List<DetechModel> ls_detechSend = new List<DetechModel>();
                                DetechModel model = new DetechModel();
                                model.parent = "clicksend";
                                model.content = "SEND TO ADMINS";
                                model.text = "SEND TO ADMINS";
                                model.node = "//node[contains(@class,'android.view.View')]";
                                model.function = 1;
                                ls_detechSend.Add(model);

                                model = new DetechModel();
                                model.parent = "clicksend";
                                model.content = "GỬI CHO QUẢN TRỊ VIÊN";
                                model.text = "GỬI CHO QUẢN TRỊ VIÊN";
                                model.node = "//node[contains(@class,'android.view.View')]";
                                model.function = 1;
                                ls_detechSend.Add(model);

                                model = new DetechModel();
                                model.parent = "clicksend";
                                model.content = "Exit Now";
                                model.text = "Exit Now";
                                model.node = "//node[contains(@class,'android.widget.Button')]";
                                model.function = 1;
                                ls_detechSend.Add(model);

                                model = new DetechModel();
                                model.parent = "clicksend";
                                model.content = "Thoát";
                                model.text = "Thoát";
                                model.node = "//node[contains(@class,'android.widget.Button')]";
                                model.function = 1;
                                ls_detechSend.Add(model);

                                PressOnLeapdroid_vietnamese(ldID, Base64Encode(FunctionHelper.method_Spin("{Ok|có|đồng ý|chấp thuận|sẵn sàng|quan tâm|thực sự}")));
                                Delay(1);
                            lb_send:
                                DetechModel kq_send = ninjaadb.detechFunction(ldID, ls_detechSend);
                                if (kq_send.status & kq_send.point.Y > 0)
                                {
                                    ClickOnLeapdroidPosition(ldID, kq_send.point);
                                    back(ldID, 1);
                                    return true;
                                }
                                else
                                {
                                    step_send++;
                                    if (step_send <= 5)
                                    {
                                        Delay(1);
                                        goto lb_send;
                                    }
                                }

                                break;
                            }
                        default:
                            break;
                    }
                    goto Lb_start;
                }
                else
                {
                    i++;
                    if (i < 5)
                    {

                        Delay(1);
                        goto Lb_start;
                    }
                }
            }
            catch
            {
            }
            back(ldID, 1);
            return false;
        }

        private bool joinGroup(DeviceData Intpt, bool autoAnswer)
        {
            List<string> list_cacel = new List<string>();
            list_cacel.Add("Cancel Join Request");
            list_cacel.Add("Hủy yêu cầu tham gia");
            string check = ninjaadb.checkListContent(Intpt, list_cacel);
            if (check == null)
            {
                List<string> list_join = new List<string>();
                list_join.Add("Join Group");
                list_join.Add("Tham gia nhóm");
                var point = ninjaadb.FindByXpathDesc(Intpt, "//node[contains(@class,'android.view.ViewGroup')]", list_join);
                if (point.X == 0 & point.Y == 0)
                    point = ninjaadb.FindByXpathDesc(Intpt, "//node[contains(@class,'android.widget.Button')]", list_join);
                if (point.X > 0 & point.Y > 0)
                {
                    ClickOnLeapdroidPosition(Intpt, point.X, point.Y);
                    Delay(1);
                    List<string> list_discuss = new List<string>();
                    list_discuss.Add("SEND TO ADMINS");
                    list_discuss.Add("GỬI CHO QUẢN TRỊ VIÊN");
                    point = ninjaadb.FindByXpathDesc(Intpt, "//node[contains(@class,'android.view.View')]", list_discuss);

                    if (point.Y > 0)
                        return clickJoinGroup(Intpt, point, autoAnswer);
                    else
                    {
                        back(Intpt, 1);
                        return true;
                    }
                }
                else
                {
                    List<string> list_check_joined = new List<string>();
                    list_check_joined.Add("Start Discussion");
                    list_check_joined.Add("Announcements");
                    check = ninjaadb.checkListContent(Intpt, list_check_joined);
                    if (check == null)
                    {
                        List<string> list_discuss = new List<string>();
                        list_discuss.Add("Discussion");
                        list_discuss.Add("THẢO LUẬN");
                        list_discuss.Add("About");
                        point = ninjaadb.FindByXpath(Intpt, "//node[contains(@class,'android.widget.TextView')]", list_discuss);
                        if (point.Y > 0)
                        {
                            ClickOnLeapdroidPosition(Intpt, point.X + 150, point.Y - 70);
                            Delay(2);
                            list_discuss = new List<string>();
                            list_discuss.Add("SEND TO ADMINS");
                            list_discuss.Add("GỬI CHO QUẢN TRỊ VIÊN");
                            point = ninjaadb.FindByXpathDesc(Intpt, "//node[contains(@class,'android.view.View')]", list_discuss);
                            if (point.Y > 0)
                            {
                                return clickJoinGroup(Intpt, point, autoAnswer);
                            }

                            else
                            {
                                back(Intpt, 1);
                                return true;
                            }
                        }
                        else
                        {
                            back(Intpt, 1);
                            return false;
                        }
                    }
                }
                back(Intpt, 1);
                return false;
            }
            else
            {
                back(Intpt, 1);
                return false;
            }

        }
        private bool clickJoinGroup(DeviceData Intpt, Point point, bool autoAnswer)
        {
            bool result = false;
            if (point.Y > 0)
            {
                if (sendToAdmin(Intpt, point, autoAnswer))
                {
                    back(Intpt, 1);
                    Delay(1);
                    return true;
                }
                else
                {
                    back(Intpt, 1);
                    point = ninjaadb.FindByXpath(Intpt, "//node[contains(@class,'android.widget.Button')]", "Leave This Page");
                    if (point.X > 0)
                    {
                        ClickOnLeapdroidPosition(Intpt, point);
                        return false;
                    }
                }
            }
            else
                back(Intpt, 1);

            return result;
        }
        public void slide(DeviceData device, Point point)
        {

            ADBHelper.Swipe(device.Serial, 200, point.Y + 30, 25, point.Y + 30, 100);

        }

        public string scrollCommentGroup(DeviceData Intpt, string app, int numLike, string content)
        {
            int dem = 0;
            int max = 0;
            int step = 0;
            List<string> list_text = new List<string>();
            list_text.Add("Discussion");
            list_text.Add("Thảo luận");
          
            ninjaadb.OpenLink(Intpt, app, "fb://groups/gridtab");
        lb_start:
            var pt_group = clickGroup(Intpt);
            Delay(1);
            if (pt_group.X > 0)
            {
                while (dem < numLike)
                {
                    scroll_up_random(Intpt);
                    if (commentPostLD(Intpt, FunctionHelper.method_Spin(content)))
                    {
                        max = 0;
                        dem++;
                        back(Intpt, 1);
                        Delay(1);
                        //truong hop commnet live stream chi back 1 lan
                        string check = ninjaadb.checkListContent(Intpt, list_text);
                        if (check == null)
                        {
                            back(Intpt, 1);
                        }
                    }
                    else
                    {
                        backNewfeedGroup(Intpt);
                        max++;
                        if (max >= 3)
                        {
                            activeNewfeed(Intpt, app);
                            return "| Comment group  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
                        }
                    }
                }
                activeNewfeed(Intpt, app);
                return "| Comment group  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
            }
            else
            {
                step++;
                if (step < 6)
                    goto lb_start;
                else
                {
                    activeNewfeed(Intpt, app);
                    return "| Comment group  hoàn thành:" + dem.ToString() + "/" + numLike.ToString();
                }
            }
        }

        private Point clickGroup(DeviceData device)
        {
            Random rnd = new Random();
            int count = 0;
            Delay(1);
            List<string> list_text = new List<string>();
            list_text.Add("Discussion");
            list_text.Add("Thảo luận");
            list_text.Add("Announcements");
            list_text.Add("Photos");
            list_text.Add("Ảnh");
            list_text.Add("Sự kiện");

            Random rd = new Random();
            int loop = 0;
            loop = rd.Next(0, 4);
            if (loop == 2)
            {
                scroll_up_mid(device);
                scroll_up_short(device);
            }
            else if (loop == 1)
                scroll_up_mid(device);
            else
                scroll_up(device);

            // var vd = ninjaadb.FindByXpath(device, "(//node[contains(@class,'android.support.v7.widget.RecyclerView')])[1]");
            List<Point> ls_point = ninjaadb.FindByXpathList(device, "((//node[contains(@class,'android.support.v7.widget.RecyclerView')])[1]//node[contains(@class,'android.widget.FrameLayout')])");
            if (ls_point.Count > 0)
            {
            lb_check_isGroup:

                ClickOnLeapdroidPosition(device, ls_point[rd.Next(0, ls_point.Count)]);
                Delay(3);
                string check = ninjaadb.checkListContent(device, list_text);
                if (check != null)
                {
                    return new Point(1, 1);
                }
                else
                {
                    if (count >= ls_point.Count)
                        return new Point(0, 0);

                    count++;
                    goto lb_check_isGroup;

                }

            }
            return new Point(0, 0);
        }
        public bool sharePost(DeviceData device)
        {
            try
            {
                List<string> ls_text = new List<string>();
                ls_text.Add("Share");
                ls_text.Add("Chia sẻ");
                var point = ninjaadb.FindByXpath(device, SettingTool.data["sharepost"], ls_text);
                if (point.X > 0)
                {
                    ClickOnLeapdroidPosition(device, point.X + 7, point.Y + 7);
                    Delay(1);
                    List<string> ls_title = new List<string>();
                    List<string> ls_path = new List<string>();
                    ls_title.Add("Share Post Now");
                    ls_title.Add("Share Now");
                    ls_title.Add("Chia sẻ ngay");
                    ls_path.Add("//node[contains(@class,'android.widget.TextView')]");
                    ls_path.Add("//node[contains(@class,'android.view.View')]");
                    point = ninjaadb.FindByListXpathDesc(device, ls_path, ls_title);
                    if (point.X > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);
                        Delay(1);
                        return true;
                    }
                    //TRUONG HOP SHARE TO GROUP
                    point = ninjaadb.FindByXpath(device, "(//node[contains(@class,'android.view.View')])[5]");
                    if (point.X > 0 || point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point.X, point.Y);
                        Delay(4);
                        point = point = ninjaadb.FindByXpath(device, "(//node[contains(@class,'android.view.View')])[8]");
                        if (point.X > 0 || point.Y > 0)
                        {
                            ClickOnLeapdroidPosition(device, point.X, point.Y);
                            Delay(2);
                            return true;
                        }
                    }
                    back(device, 1);
                }
                return false;
            }
            catch
            { }
            return false;
        }

        public bool sharePostIntoGroup(DeviceData device, string content, ref bool has)
        {
            int i_back = 0;
            try
            {
                var screen = ninjaadb.ScreenShoot(device); ;
                var like = ImageScanOpenCV.GetImage("img\\11-share.png");
                var point = ImageScanOpenCV.FindOutPoint(screen, like);
                if (point.X > 0 || point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point.X, point.Y);
                    i_back = 1;
                    Delay(2);
                    screen = ninjaadb.ScreenShoot(device);
                    like = ImageScanOpenCV.GetImage("img\\92-sharetogroup.png");
                    point = ImageScanOpenCV.FindOutPoint(screen, like);
                    if (point.X > 0 || point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);
                        i_back = 2;
                        var pt = clickGroupShare(device);
                        if (pt.X > 0)
                        {
                            Delay(2);
                            screen = ninjaadb.ScreenShoot(device);
                            like = ImageScanOpenCV.GetImage("img\\94-saysomething.png");
                            point = ImageScanOpenCV.FindOutPoint(screen, like);
                            var like2 = ImageScanOpenCV.GetImage("img\\95-writesomething.png");
                            var point2 = ImageScanOpenCV.FindOutPoint(screen, like2);

                            if (point.X > 0 || point2.X > 0)
                            {
                                if (point.X > 0 || point.Y > 0)
                                    ClickOnLeapdroidPosition(device, point);
                                else
                                    ClickOnLeapdroidPosition(device, point2);

                                Delay(1);
                                PressOnLeapdroid_vietnamese(device, Base64Encode(FunctionHelper.method_Spin(content)));

                            }
                            Delay(3);
                            ClickOnLeapdroidPosition(device, pt);
                            return true;
                        }
                    }
                    else
                        has = false;

                }
                else
                    has = false;

                back(device, i_back);
                return false;
            }
            catch
            { }
            return false;
        }
        public Point clickGroupShare(DeviceData handle)
        {
            Delay(2);
            Random rdom = new Random();
            Point pt = new Point();
            var screen = ninjaadb.ScreenShoot(handle);
            var like = ImageScanOpenCV.GetImage("img\\93-groupshare.png");
            var point = ImageScanOpenCV.FindOutPoint(screen, like);
            int i = 0;
            while (i < 10)
            {
                ClickOnLeapdroidPosition(handle, new Point(rdom.Next(30, 400), rdom.Next(140, 799)));
                Delay(2);
                screen = ninjaadb.ScreenShoot(handle);
                like = ImageScanOpenCV.GetImage("img\\post.png");
                pt = ImageScanOpenCV.FindOutPoint(screen, like);
                if (pt.X > 0)
                {
                    break;
                }
                i++;
            }
            if (pt.X > 0)
                return pt;
            else if (point.X > 0)
                return point;
            else
                return new Point(0, 0);
        }


        public void keyboard()
        {
            List<string> keyboad = ninjaadb.GetKeyboad();

        }
        public bool checkKeyboard(DeviceData device)
        {
            string cmd = String.Format("adb -s {0} shell ime list -a  ", device.Serial);
            string str2 = ninjaadb.ExecuteCMD(cmd);
            if (str2.Contains("com.android.adbkeyboard"))
            {
                return true;
            }
            return false;
        }
        public bool installApk(DeviceData device, string path)
        {
            try
            {
                // if (checkKeyboard(device) == false)
                //{
                string cmd = string.Format("adb -s {0} install  \"{1}\"", device.Serial, path);
                startAdbserver(cmd);
                //   PackageManager manager = new PackageManager(device);
                // manager.InstallPackage(path, reinstall: true);
                startAdbserver(String.Format("adb -s {0} shell ime set com.android.adbkeyboard/.AdbIME", device.Serial));
                return true;
                //}
                //else
                //{
                //    startAdbserver(String.Format("adb -s {0} shell ime set com.android.adbkeyboard/.AdbIME", device.Serial));
                //    return true;
                //}

            }
            catch
            { }
            return false;
        }
        public string PostImages(DeviceData device, string app, string content, List<string> list_file, int numPhoto, bool removepic = true)
        {
            List<string> list_photo = new List<string>();
            List<string> list_photoCopy = new List<string>();
            string cmd = "";
            if (list_file.Count > 0)
            {
                cmd = string.Format("adb -s {0} shell rm -r sdcard/dcim/pic1&&adb -s {0} shell mkdir /sdcard/dcim/pic1", device.Serial);
                startAdbserver(cmd);
                for (int i = 0; i < numPhoto; i++)
                {
                    if (list_file.Count > 0)
                    {
                        Random rd = new Random();
                        string filePath = list_file[rd.Next(0, list_file.Count)];
                        list_file.Remove(filePath);
                        string fileName = Path.GetFileName(filePath);
                        string fileNameCopy = rd.Next(10000, 99999).ToString() + ".jpg";
                        string filePathCopy = "c:\\test\\" + fileNameCopy;
                        System.IO.File.Copy(filePath, filePathCopy, true);
                        list_photoCopy.Add(filePathCopy);
                        cmd = string.Format("push \"{0}\" /sdcard/dcim", filePathCopy);
                        //ninjaadb.runComand(ldID, cmd);
                        //ninjaadb.runComand(ldID, String.Format("shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/dcim/{0}", fileNameCopy));
                        cmd = string.Format("adb -s {2} push \"{0}\" /sdcard/dcim/pic1&&adb -s {2} shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/dcim/pic1/{1}", filePathCopy, fileNameCopy, device.Serial);
                        startAdbserver(cmd);
                        list_photo.Add(filePath);
                    }
                    else
                        break;
                }
                cmd = string.Format("adb -s {0} shell am broadcast -a android.intent.action.MEDIA_MOUNTED -d file:///sdcard/dcim", device.Serial);

            }
            //xoa file o folder test
            foreach (string file in list_photoCopy)
            {
                System.IO.File.Delete(file);
            }
            activeNewfeed(device, app);
            int step = 0;
            string message = "";

            DetechModel Model = new DetechModel();
            Model = new DetechModel();
            Model.parent = "";
            Model.content = "share";
            Model.text = "share";
            Model.node = "//node[contains(@class,'android.view.View')]";
            Model.function = 1;
            SettingTool.lang.list_detechpost.Add(Model);

            Model = new DetechModel();
            Model.parent = "";
            Model.content = "chia sẻ";
            Model.text = "chia sẻ";
            Model.node = "//node[contains(@class,'android.view.View')]";
            Model.function = 1;
            SettingTool.lang.list_detechpost.Add(Model);

        Lb_start:
            DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_detechpost);
            if (kq.status)
            {
                step = 0;
                switch (kq.function)
                {
                    case -1:
                        {
                            Thread.Sleep(5000);
                            break;
                        }
                    case 0:
                        {
                            back(device, 1);
                            break;
                        }
                    case 1:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            Delay(1);
                            break;
                        }
                    case 2:
                        {
                            List<Point> ls_point = ninjaadb.FindByXpathList(device, "//node[contains(@class,'android.widget.ImageView')]");
                            for (int i = 1; i <= numPhoto; i++)
                            {
                                ClickOnLeapdroidPosition(device, ls_point[i]);
                            }
                            List<DetechModel> list_next = new List<DetechModel>();
                            DetechModel data = new DetechModel();
                            data.parent = "post";
                            data.content = "Next";
                            data.text = "Next";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "Tiếp";
                            data.text = "Tiếp";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "Xong";
                            data.text = "Xong";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "Done";
                            data.text = "Done";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);
                        Lb_Send:
                            DetechModel kqsend = ninjaadb.detechFunction(device, list_next);
                            if (kqsend.status)
                            {
                                ClickOnLeapdroidPosition(device, kqsend.point);
                                Delay(1);
                            }
                            else
                            {
                                step++;
                                if (step <= 3)
                                    goto Lb_Send;
                            }
                            break;
                        }
                    case 3:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            Delay(1);
                            string[] content_split = SplitByLength(content, 200);
                            if (content_split.Count() > 0)
                            {
                                for (int i = 0; i < content_split.Count(); i++)
                                {
                                    PressOnLeapdroid_vietnamese(device, Base64Encode(content_split[i]));
                                    Delay(2);
                                }
                            }
                            Delay(2);
                            step = 0;
                        Lb_Send:
                            DetechModel kqsend = ninjaadb.detechFunction(device, SettingTool.lang.list_buttonpost);
                            if (kqsend.status)
                            {
                                ClickOnLeapdroidPosition(device, kqsend.point);
                                // truong hop la nut share phai click them lan nua
                                if (kqsend.content.ToLower() == "share" || kqsend.content.ToLower() == "chia sẻ")
                                {
                                    List<DetechModel> lsShare = new List<DetechModel>();
                                    DetechModel detech = new DetechModel();
                                    detech = new DetechModel();
                                    detech.parent = "";
                                    detech.content = "share now";
                                    detech.text = "share now";
                                    detech.node = "//node[contains(@class,'android.view.View')]";
                                    detech.function = 1;
                                    lsShare.Add(detech);

                                    detech = new DetechModel();
                                    detech.parent = "";
                                    detech.content = "chia sẻ ngay";
                                    detech.text = "chia sẻ ngay";
                                    detech.node = "//node[contains(@class,'android.view.View')]";
                                    detech.function = 1;
                                    lsShare.Add(detech);
                                    DetechModel kqShare = new DetechModel();
                                    kqShare = ninjaadb.RunDetechFunction(device, lsShare, 3);
                                    if (kqShare.point.Y > 0)
                                        ClickOnLeapdroidPosition(device, kqShare.point);
                                }
                                Thread.Sleep(5000);
                                if (removepic)
                                {
                                    foreach (string pathPhoto in list_photo)
                                    {
                                        File.Delete(pathPhoto);
                                    }
                                }
                                List<string> ls_desc = new List<string>();
                                ls_desc.Add("ProgressBar");
                                if (ninjaadb.checkListContent(device, ls_desc) != null)
                                {
                                    Delay(10);
                                }
                                message = "Đăng bài hoàn thành";
                            }
                            else
                            {
                                step++;
                                if (step <= 5)
                                    goto Lb_Send;
                            }
                            return "Đăng bài hoàn thành";
                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                step++;
                if (step <= 5)
                {
                    goto Lb_start;
                }
            }
            return message;
        }

        public string PostContent(DeviceData ldID, string app, string content)
        {

            activeNewfeed(ldID, app);
            List<DetechModel> list_post = new List<DetechModel>();
            DetechModel data = new DetechModel();
            data.parent = "post";
            data.content = "Photo";
            data.text = "Photo";
            data.node = "//node[contains(@class,'android.view.ViewGroup')]";
            data.function = 4;
            list_post.Add(data);

            data = new DetechModel();
            data.parent = "post";
            data.content = "Photo";
            data.text = "Photo";
            data.node = "//node[contains(@class,'android.view.View')]";
            data.function = 4;
            list_post.Add(data);

            data = new DetechModel();
            data.parent = "post";
            data.content = "Ảnh";
            data.text = "Ảnh";
            data.node = "//node[contains(@class,'android.view.ViewGroup')]";
            data.function = 4;
            list_post.Add(data);

            data = new DetechModel();
            data.parent = "post";
            data.content = "Ảnh";
            data.text = "Ảnh";
            data.node = "//node[contains(@class,'android.view.View')]";
            data.function = 4;
            list_post.Add(data);
            DetechModel kqclick = ninjaadb.RunDetechFunction(ldID, list_post);
            if (kqclick.status)
            {
                ClickOnLeapdroidPosition(ldID, kqclick.point.X - 100, kqclick.point.Y);
                Delay(1);
            }
            int step = 0;
        Lb_start:
            DetechModel kq = ninjaadb.RunDetechFunction(ldID, SettingTool.lang.list_postcontent);
            if (kq.status)
            {
                step = 0;
                switch (kq.function)
                {

                    case 1:
                        {
                            ClickOnLeapdroidPosition(ldID, kq.point);
                            Delay(1);
                            break;
                        }
                    case 3:
                        {
                            ClickOnLeapdroidPosition(ldID, kq.point);
                            Delay(2);

                            string[] content_split = SplitByLength(content, 200);
                            if (content_split.Count() > 0)
                            {
                                for (int i = 0; i < content_split.Count(); i++)
                                {
                                    PressOnLeapdroid_vietnamese(ldID, content_split[i]);
                                    Delay(2);
                                }
                            }


                            Delay(1);
                            List<DetechModel> list_hinhnen = new List<DetechModel>();
                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "hình nền";
                            data.text = "hình nền";
                            data.node = "//node[contains(@class,'android.view.View')]";
                            data.function = 1;
                            list_hinhnen.Add(data);

                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "background image";
                            data.text = "background image";
                            data.node = "//node[contains(@class,'android.view.View')]";
                            data.function = 1;
                            list_hinhnen.Add(data);
                            DetechModel kqhinhnen = ninjaadb.RunDetechFunction(ldID, list_hinhnen);
                            if (kqhinhnen.status)
                            {
                                ClickOnLeapdroidPosition(ldID, kqhinhnen.point);
                            }

                            DetechModel kqsend = ninjaadb.RunDetechFunction(ldID, SettingTool.lang.list_buttonpost);
                            if (kqsend.status)
                            {
                                ClickOnLeapdroidPosition(ldID, kqsend.point);
                                Thread.Sleep(10000);
                                List<string> ls_desc = new List<string>();
                                ls_desc.Add("ProgressBar");
                                if (ninjaadb.checkListContent(ldID, ls_desc) != null)
                                {
                                    Delay(10);
                                }
                            }
                            return "Đăng bài hoàn thành";
                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                step++;
                if (step <= 5)
                {
                    goto Lb_start;

                }
            }
            return "Đăng bài không hoàn thành";
        }


        public bool ChangeAvatar(DeviceData ldID, string app, List<string> list_file, bool removepic = true)
        {
            bool status = false;
            List<string> list_photo = new List<string>();
            List<string> list_photoCopy = new List<string>();

            string cmd = "";
            if (list_file.Count > 0)
            {
                cmd = string.Format("adb -s {0} shell rm -r sdcard/dcim/pic1&&adb -s {0} shell mkdir /sdcard/dcim/pic1", ldID.Serial);
                startAdbserver(cmd);
                if (list_file.Count > 0)
                {
                    Random rd = new Random();
                    string filePath = list_file[rd.Next(0, list_file.Count)];
                    list_file.Remove(filePath);
                    string fileName = Path.GetFileName(filePath);
                    string fileNameCopy = rd.Next(100000, 999999).ToString() + ".jpg";
                    string filePathCopy = "c:\\test\\" + fileNameCopy;
                    System.IO.File.Copy(filePath, filePathCopy);
                    list_photoCopy.Add(filePathCopy);

                    cmd = string.Format("adb -s {2} push \"{0}\" /sdcard/dcim/pic1&&adb -s {2} shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/dcim/pic1/{1}", filePathCopy, fileNameCopy, ldID.Serial);
                    startAdbserver(cmd);

                    //   ninjaadb.runComand(ldID, String.Format("shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/dcim/{0}", fileNameCopy));
                    list_photo.Add(filePath);
                }

                //cmd = "shell am broadcast -a android.intent.action.MEDIA_MOUNTED -d file:///sdcard/dcim";
                //ninjaadb.runComand(ldID, cmd);
                foreach (string file in list_photoCopy)
                {
                    System.IO.File.Delete(file);
                }
            }

            ninjaadb.OpenLink(ldID, app, "fb://profile");

            DetechModel Model = new DetechModel();
            Model.parent = "changeavatar";
            Model.content = "Add a photo of yourself so friends can find you.";
            Model.text = "Add a photo of yourself so friends can find you.";
            Model.node = "";
            Model.function = 0;
            SettingTool.lang.list_changeavatar.Add(Model);
            bool menuSelect = false;
            int step = 0;
        Lb_start:
            DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_changeavatar);
            if (kq.status)
            {
                step = 0;
                switch (kq.function)
                {
                    case -1:
                        {
                            Thread.Sleep(5000);
                            break;
                        }
                    case 0:
                        {
                            back(ldID, 1);
                            break;
                        }
                    case 1:
                        {
                            ClickOnLeapdroidPosition(ldID, kq.point);
                            Delay(1);
                            if (kq.text.ToLower() == "chỉnh sửa ảnh đại diện" || kq.text.ToLower() == "edit profile picture")
                                menuSelect = true;
                            break;
                        }
                    case 2:
                        {
                            Delay(2);
                            List<Point> ls_point = ninjaadb.FindByXpathList(ldID, "//node[contains(@class,'android.widget.ImageView')]");
                            if (ls_point.Count > 2)
                            {
                                ClickOnLeapdroidPosition(ldID, ls_point[4]);
                            }
                            List<DetechModel> list_next = new List<DetechModel>();
                            DetechModel data = new DetechModel();
                            data.parent = "post";
                            data.content = "Save";
                            data.text = "Save";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "Lưu";
                            data.text = "Lưu";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                        Lb_Send:
                            DetechModel kqsend = ninjaadb.detechFunction(ldID, list_next);
                            if (kqsend.status)
                            {
                                ClickOnLeapdroidPosition(ldID, kqsend.point);

                                if (removepic)
                                {
                                    foreach (string pathPhoto in list_photo)
                                    {
                                        File.Delete(pathPhoto);
                                    }
                                }
                                Thread.Sleep(7000);

                                return true;
                            }
                            else
                            {
                                step++;
                                if (step <= 3)
                                    goto Lb_Send;
                            }
                            break;

                        }
                    case 3:
                        {
                            ClickOnLeapdroidPosition(ldID, kq.point);
                            Delay(1);
                            changeInfo(ldID);
                            break;
                        }
                    case 4:
                        {
                            List<Point> ls_point = ninjaadb.FindByXpathList(ldID, "//node[contains(@class,'android.widget.ImageView')]");
                            if (ls_point.Count > 2)
                            {
                                ClickOnLeapdroidPosition(ldID, ls_point[4]);
                            }
                            List<DetechModel> list_next = new List<DetechModel>();
                            DetechModel data = new DetechModel();
                            data.parent = "post";
                            data.content = "Save";
                            data.text = "Save";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "Lưu";
                            data.text = "Lưu";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                        Lb_Send:
                            DetechModel kqsend = ninjaadb.detechFunction(ldID, list_next);
                            if (kqsend.status)
                            {
                                ClickOnLeapdroidPosition(ldID, kqsend.point);
                                Thread.Sleep(7000);
                                return true;
                            }
                            else
                            {
                                step++;
                                if (step <= 3)
                                    goto Lb_Send;
                            }

                            break;
                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                if (menuSelect)
                {
                    Delay(2);
                    List<Point> ls_menu = ninjaadb.FindByDescList(ldID, "//node[contains(@class,'android.support.v7.widget.RecyclerView')]", "//node[contains(@class,'android.view.View')]");
                    Point mnu_selectPic = new Point();
                    mnu_selectPic = ls_menu[ls_menu.Count - 1];
                    ClickOnLeapdroidPosition(ldID, mnu_selectPic);
                    Delay(1);
                    menuSelect = false;
                    goto Lb_start;
                }
                step++;
                if (step <= 5)
                {
                    goto Lb_start;

                }

            }
            return status;
        }

        public bool ChangeCover(DeviceData ldID, string app, List<string> list_file, bool removepic = true)
        {
            bool status = false;
            List<string> list_photo = new List<string>();
            List<string> list_photoCopy = new List<string>();

            string cmd = "";
            if (list_file.Count > 0)
            {
                cmd = string.Format("adb -s {0} shell rm -r sdcard/dcim/pic1&&adb -s {0} shell mkdir /sdcard/dcim/pic1", ldID.Serial);
                startAdbserver(cmd);
                if (list_file.Count > 0)
                {
                    Random rd = new Random();
                    string filePath = list_file[rd.Next(0, list_file.Count)];
                    list_file.Remove(filePath);
                    string fileName = Path.GetFileName(filePath);
                    string fileNameCopy = rd.Next(100000, 999999).ToString() + ".jpg";
                    string filePathCopy = "c:\\test\\" + fileNameCopy;
                    System.IO.File.Copy(filePath, filePathCopy);
                    list_photoCopy.Add(filePathCopy);
                    cmd = string.Format("adb -s {2} push \"{0}\" /sdcard/dcim/pic1&&adb -s {2} shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/dcim/pic1/{1}", filePathCopy, fileNameCopy, ldID.Serial);
                    startAdbserver(cmd);
                    //   ninjaadb.runComand(ldID, String.Format("shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/dcim/{0}", fileNameCopy));
                    list_photo.Add(filePath);
                }

                cmd = string.Format("adb -s {0} shell am broadcast -a android.intent.action.MEDIA_MOUNTED -d file:///sdcard/dcim", ldID.Serial);
                startAdbserver(cmd);
                foreach (string file in list_photoCopy)
                {
                    System.IO.File.Delete(file);
                }
            }
            ninjaadb.OpenLink(ldID, app, "fb://profile");
            bool menuSelect = false;
            bool first = false;
            bool addCover = true;
            int step = 0;
        Lb_start:
            DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_changecover);
            if (kq.status)
            {
                step = 0;
                switch (kq.function)
                {
                    case -1:
                        {
                            Thread.Sleep(5000);
                            break;
                        }
                    case 0:
                        {
                            back(ldID, 1);
                            break;
                        }
                    case 1:
                        {
                            ClickOnLeapdroidPosition(ldID, kq.point);
                            if (kq.text == "Cover photo of" || kq.text.ToLower() == "ảnh bìa của")
                                menuSelect = true;
                            Delay(1);
                            break;
                        }
                    case 2:
                        {
                            Delay(2);
                            List<Point> ls_point = ninjaadb.FindByXpathList(ldID, "//node[contains(@class,'android.widget.ImageView')]");
                            if (ls_point.Count >= 2)
                            {
                                ClickOnLeapdroidPosition(ldID, ls_point[1]);
                            }
                            List<DetechModel> list_next = new List<DetechModel>();
                            DetechModel data = new DetechModel();
                            data.parent = "post";
                            data.content = "Save";
                            data.text = "Save";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "Lưu";
                            data.text = "Lưu";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                        Lb_Send:
                            DetechModel kqsend = ninjaadb.detechFunction(ldID, list_next);
                            if (kqsend.status)
                            {
                                ClickOnLeapdroidPosition(ldID, kqsend.point);

                                if (removepic)
                                {
                                    foreach (string pathPhoto in list_photo)
                                    {
                                        File.Delete(pathPhoto);
                                    }
                                }
                                Thread.Sleep(7000);

                                return true;
                            }
                            else
                            {
                                step++;
                                if (step <= 3)
                                    goto Lb_Send;
                            }
                            break;

                        }
                    case 3:
                        {
                            changeInfo(ldID);
                            break;
                        }
                    case 4:
                        {
                            List<Point> ls_point = ninjaadb.FindByXpathList(ldID, "//node[contains(@class,'android.widget.ImageView')]");
                            if (ls_point.Count > 2)
                            {
                                ClickOnLeapdroidPosition(ldID, ls_point[4]);
                            }
                            List<DetechModel> list_next = new List<DetechModel>();
                            DetechModel data = new DetechModel();
                            data.parent = "post";
                            data.content = "Save";
                            data.text = "Save";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                            data = new DetechModel();
                            data.parent = "post";
                            data.content = "Lưu";
                            data.text = "Lưu";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_next.Add(data);

                        Lb_Send:
                            DetechModel kqsend = ninjaadb.detechFunction(ldID, list_next);
                            if (kqsend.status)
                            {
                                ClickOnLeapdroidPosition(ldID, kqsend.point);
                                Thread.Sleep(7000);
                                return true;

                            }
                            else
                            {
                                step++;
                                if (step <= 3)
                                    goto Lb_Send;
                            }

                            break;
                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                step++;
                if (step >= 2)
                {
                    if (menuSelect)
                    {
                        Delay(2);
                        List<Point> ls_menu = ninjaadb.FindByDescList(ldID, "//node[contains(@class,'android.support.v7.widget.RecyclerView')]", "//node[contains(@class,'android.view.View')]");
                        Point mnu_selectPic = new Point();

                        if (first)
                            mnu_selectPic = ls_menu[0];  //chua co cover menu o vtri dau tien
                        else
                            mnu_selectPic = ls_menu[1];  //da co cover menu o vtri thu 2
                        ClickOnLeapdroidPosition(ldID, mnu_selectPic);
                        Delay(1);
                        menuSelect = false;
                        goto Lb_start;
                    }
                    //click them anh bia
                    if (addCover)
                    {
                        Delay(2);
                        List<Point> ls_menu = ninjaadb.FindByDescList(ldID, "//node[contains(@class,'android.support.v7.widget.RecyclerView')]", "//node[contains(@class,'android.view.View')]");
                        Point mnu_selectPic = new Point();
                        mnu_selectPic = ls_menu[0];
                        ClickOnLeapdroidPosition(ldID, mnu_selectPic);
                        Delay(1);
                        menuSelect = true;
                        first = true;
                        addCover = false;
                        goto Lb_start;
                    }

                }
                //click tai anh len

                if (step <= 5)
                {
                    goto Lb_start;

                }

            }
            return status;
        }
        public bool changeInfo(DeviceData ldID)
        {
            bool status = false;
            int step = 0;
        Lb_start:
            DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_changeinfo);
            if (kq.status)
            {
                step = 0;
                switch (kq.function)
                {
                    case -1:
                        {
                            Thread.Sleep(5000);
                            break;
                        }
                    case 0:
                        {
                            back(ldID, 1);
                            break;
                        }
                    case 1:
                        {
                            ClickOnLeapdroidPosition(ldID, kq.point);
                            Delay(1);
                            break;
                        }
                    case 2:
                        {
                            List<Point> list_goiy = ninjaadb.FindByDescList(ldID, "//node[contains(@class,'android.widget.HorizontalScrollView')]", "//node[contains(@class,'android.widget.Button')]");
                            if (list_goiy.Count > 0)
                            {
                                ClickOnLeapdroidPosition(ldID, list_goiy[0]);

                                List<DetechModel> list_OK = new List<DetechModel>();
                                DetechModel data = new DetechModel();
                                data.parent = "Avatar";
                                data.content = "LƯU";
                                data.text = "LƯU";
                                data.node = "//node[contains(@class,'android.widget.TextView')]";
                                data.function = 1;
                                list_OK.Add(data);

                                data = new DetechModel();
                                data.parent = "Avatar";
                                data.content = "SAVE";
                                data.text = "SAVE";
                                data.node = "//node[contains(@class,'android.widget.TextView')]";
                                data.function = 1;
                                list_OK.Add(data);

                                data = new DetechModel();
                                data.parent = "Avatar";
                                data.content = "OK";
                                data.text = "OK";
                                data.node = "//node[contains(@class,'android.widget.TextView')]";
                                data.function = 1;
                                list_OK.Add(data);

                                DetechModel kqok = ninjaadb.RunDetechFunction(ldID, list_OK, 3);
                                if (kqok.status)
                                {
                                    ClickOnLeapdroidPosition(ldID, kqok.point);
                                    Delay(3);
                                }
                            }
                            else
                            {
                                List<DetechModel> list_OK = new List<DetechModel>();
                                DetechModel data = new DetechModel();
                                data = new DetechModel();
                                data.parent = "Avatar";
                                data.content = "Bỏ qua";
                                data.text = "Bỏ qua";
                                data.node = "//node[contains(@class,'android.widget.Button')]";
                                data.function = 1;
                                list_OK.Add(data);

                                data = new DetechModel();
                                data.parent = "Avatar";
                                data.content = "Skip";
                                data.text = "Skip";
                                data.node = "//node[contains(@class,'android.widget.Button')]";
                                data.function = 1;
                                list_OK.Add(data);

                                DetechModel kqok = ninjaadb.RunDetechFunction(ldID, list_OK, 3);
                                if (kqok.status)
                                {
                                    ClickOnLeapdroidPosition(ldID, kqok.point);
                                    Delay(3);
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            ClickOnLeapdroidPosition(ldID, kq.point);
                            Delay(1);
                            break;
                        }
                    case 4:
                        {
                            ClickOnLeapdroidPosition(ldID, kq.point);
                            Delay(1);
                            status = true;
                            return status;

                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                step++;
                if (step <= 5)
                {
                    goto Lb_start;

                }

            }
            return status;
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public void viewVideoShare(DeviceData device, int num)
        {
            var screen = ninjaadb.ScreenShoot(device);
            var like = ImageScanOpenCV.GetImage("img\\24-videonewfeed.png");
            var point = ImageScanOpenCV.FindOutPoint(screen, like);
            int max = 0;
            if (point.X > 0 || point.Y > 0)
            {
                ClickOnLeapdroidPosition(device, point);
                Delay(1);
            }
            else
            {
                point = intoMenu(device);
                if (point.X > 0 || point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point);
                    Delay(1);

                    screen = ninjaadb.ScreenShoot(device);
                    like = ImageScanOpenCV.GetImage("img\\25-videomenu.png");
                    point = ImageScanOpenCV.FindOutPoint(screen, like);
                    if (point.X > 0 || point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);
                        Delay(2);
                        // truong hop click nhung ko vao dc
                        screen = ninjaadb.ScreenShoot(device);
                        like = ImageScanOpenCV.GetImage("img\\53-searchvideo.png");
                        point = ImageScanOpenCV.FindOutPoint(screen, like);
                        if (point.X == 0)
                        {
                            back(device, 1);
                            return;
                        }

                    }
                    else
                    {
                        back(device, 1);
                        return;
                    }

                }
                else
                {
                    return;
                }
            }
            int dem = 0;
            while (dem < num)
            {
                Delay(1);
                scroll_up(device);
                Delay(4);
                if (shareVideo(device))
                {
                    dem++;
                    Delay(2);
                }

                max++;
                if (max == 10)
                {
                    back(device, 1);
                    return;
                }
            }
            back(device, 1);
        }


        private Point intoMenu(DeviceData ldID)
        {
            List<DetechModel> list_datamenu = new List<DetechModel>();
            DetechModel data = new DetechModel();
            data.parent = "menu";
            data.content = "Xem thêm";
            data.text = "Xem thêm";
            data.node = "//node[contains(@class,'android.view.View')]";
            data.function = 1;
            list_datamenu.Add(data);

            data = new DetechModel();
            data.parent = "menu";
            data.content = "More";
            data.text = "More";
            data.node = "//node[contains(@class,'android.view.View')]";
            data.function = 1;
            list_datamenu.Add(data);


            int i = 0;
        Lb_Start:
            Delay(3);
            DetechModel kq = ninjaadb.detechFunction(ldID, list_datamenu);
            if (kq.status)
            {
                if (kq.function == 1)
                    return kq.point;
            }
            else
            {
                i++;
                if (i <= 2)
                {
                    goto Lb_Start;
                }
                else
                {
                    Point point1 = new Point();
                    Point point2 = new Point();
                    ninjaadb.FindBoundDetech_unLower(ldID, SettingTool.lang.list_menu, ref point1, ref point2);

                    if (point2.X > 0 || point2.Y > 0)
                    {
                        Point point = new Point();
                        point.X = point2.X + 80;
                        point.Y = point2.Y -10;
                        return point;
                    } // ko get dc point menu thi => tu messenger
                    else
                    {
                        List<DetechModel> list_messenger = new List<DetechModel>();
                        DetechModel model = new DetechModel();
                        model.parent = "menu";
                        model.content = "cuộc trò chuyện";
                        model.text = "cuộc trò chuyện";
                        model.node = "//node[contains(@class,'android.widget.Button')]";
                        model.function = 1;
                        list_messenger.Add(model);
                        model = new DetechModel();
                        model.parent = "menu";
                        model.content = "Messaging";
                        model.text = "Messaging";
                        model.node = "//node[contains(@class,'android.widget.Button')]";
                        model.function = 1;
                        list_messenger.Add(model);

                        model = new DetechModel();
                        model.parent = "menu";
                        model.content = "Nhắn tin";
                        model.text = "Nhắn tin";
                        model.node = "//node[contains(@class,'android.widget.Button')]";
                        model.function = 1;
                        list_messenger.Add(model);
                        model = new DetechModel();
                        model.parent = "menu";
                        model.content = "conversations";
                        model.text = "conversations";
                        model.node = "//node[contains(@class,'android.widget.Button')]";
                        model.function = 1;
                        list_messenger.Add(model);
                        point1 = new Point();
                        point2 = new Point();
                        ninjaadb.FindBoundDetech_unLower(ldID, list_messenger, ref point1, ref point2);
                        if (point2.X > 0 || point2.Y > 0)
                        {
                            Point point = new Point();
                            point.X = point2.X - 20;
                            point.Y = point2.Y + 70;
                            return point;
                        }

                    }
                }
            }
            return new Point();
        }
        public string ShareLikesteam_intoGroup(DeviceData device, DataGridViewRow dr, string link, string content, int numgroup, string app, int i_delay)
        {
            string message = "";
            if (link != "")
            {
                string[] ls_link = link.Split(',');
                List<DetechModel> list_write = new List<DetechModel>();
                DetechModel data = new DetechModel();
                data.parent = "post";
                data.content = "LIVE";
                data.text = "Write a message…";
                data.node = "//node[contains(@class,'android.widget.EditText')]";
                data.function = 2;
                list_write.Add(data);

                data = new DetechModel();
                data.parent = "post";
                data.content = "TRỰC TIẾP";
                data.text = "Viết tin nhắn…";
                data.node = "//node[contains(@class,'android.widget.EditText')]";
                data.function = 2;
                list_write.Add(data);

                List<string> list_content = new List<string>();
                Random rdom = new Random();
                for (int i = 0; i < ls_link.Length; i++)
                {
                    ninjaadb.OpenLink(device, app, ls_link[i]);
                    Delay(5);
                    ClickOnLeapdroidPosition(device, 300, 300);
                    ClickOnLeapdroidPosition(device, 300, 300);
                    int step = 0;
                    bool has_share = false;
                    int numshare = 0;
                Lb_start:
                    DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_sharelive);
                    if (kq.status)
                    {
                        step = 0;
                        switch (kq.function)
                        {
                            case -2:
                                {
                                    dr.Cells["Message"].Value = kq.content;
                                    goto Lb_finish;
                                }
                            case -1:
                                {
                                    Thread.Sleep(5000);
                                    break;
                                }
                            case 0:
                                {
                                    back(device, 1);
                                    break;
                                }
                            case 1:
                                {
                                    if (has_share)
                                    {
                                        ClickOnLeapdroidPosition(device, kq.point);
                                        numshare++;
                                        dr.Cells["clSuccess"].Value = numshare;
                                        Delay(i_delay);
                                        if (numshare >= numgroup)
                                        {
                                            message += "| link " + (i + 1).ToString() + "share group hoàn thành " + numshare.ToString() + "/" + numgroup.ToString();
                                            goto Lb_finish;
                                        }
                                        scroll_up_short(device, kq.point);
                                    }
                                    else
                                    {
                                        ClickOnLeapdroidPosition(device, kq.point);
                                    }
                                    Delay(1);
                                    break;
                                }
                            case 2:
                                {
                                    ClickOnLeapdroidPosition(device, kq.point);
                                    Delay(2);
                                    DetechModel kqwrite = ninjaadb.RunDetechFunction(device, list_write);
                                    if (kqwrite.status)
                                    {
                                        ClickOnLeapdroidPosition(device, kqwrite.point);
                                        Thread.Sleep(1000);
                                        PressOnLeapdroid_vietnamese(device, Base64Encode(FunctionHelper.method_Spin(content)));
                                        scroll_up_randomShare(device);
                                    }
                                    has_share = true;
                                    break;
                                }
                            default:
                                break;
                        }
                        goto Lb_start;
                    }
                    else
                    {
                        step++;
                        if (step <= 5)
                        {
                            if (step == 4)
                            {
                                ClickOnLeapdroidPosition(device, 300, 300);
                                ClickOnLeapdroidPosition(device, 300, 300);
                            }
                            goto Lb_start;
                        }
                        message += " | link " + (i + 1).ToString() + ": share group không hoàn thành";
                    }
                Lb_finish:
                    numshare = 0;
                }
            }
            return message;
        }

        public string ViewLikeCommentShareVideo(DeviceData device, SettingTuongTac setTuongtac, string link, string content, decimal numGroup, string app, int i_delay, int timevideo, int timevideoEnd, bool blView, bool blLike, bool blcomment, bool blshare)
        {
            string message = "";
            string kq = "";
            if (link != "")
            {
                string[] ls_link = link.Split(',');
                for (int i = 0; i < ls_link.Length; i++)
                {
                    kq = "";
                    ninjaadb.OpenLink(device, app, ls_link[i]);
                    Random rd = new Random();
                    int timeView = rd.Next(timevideo, timevideoEnd);
                    if (blView)
                    {
                        Delay(timeView); //timeView * 60
                        kq += "-view " + timeView.ToString() + "s";
                    }
                    if (blLike)
                    {
                        if (likePostLD(device))
                            kq += "-like hoàn thành";
                        else
                            kq += "-like không hoàn thành ";

                    }
                    if (blcomment)
                    {
                        if (commentPostLD(device, FunctionHelper.method_Spin(content)))
                        {
                            kq += "-comment hoàn thành";
                            back(device, 2);
                        }
                        else
                            kq += "-comment không hoàn thành ";
                    }

                    if (blshare)
                    {
                        ninjaadb.OpenLink(device, app, ls_link[i]);
                        if (!blLike & !blcomment)
                            Delay(7);
                        if (shareVideo2NewfeedLD(device, app, content))
                            kq += "-share tường hoàn thành";
                        else
                            kq += "-share tường không hoàn thành ";
                    }
                    Delay(i_delay);
                    message += "|Link " + (i + 1).ToString() + kq;
                }
            }
            if (message == "")
                message = "không hoàn thành ";
            return message;
        }

        public string ShareLikesteam_intoNewfeed(DeviceData device, DataGridViewRow dr, string link, string content, string app, int i_delay)
        {
            string messeage = "";
            if (link != "")
            {
                List<DetechModel> list_write = new List<DetechModel>();
                DetechModel data = new DetechModel();
                data.parent = "post";
                data.content = "Write something…";
                data.text = "Write something…";
                data.node = "//node[contains(@class,'android.widget.EditText')]";
                data.function = 2;
                list_write.Add(data);

                data = new DetechModel();
                data.parent = "post";
                data.content = "Viết gì đó...";
                data.text = "Viết gì đó...";
                data.node = "//node[contains(@class,'android.widget.EditText')]";
                data.function = 2;
                list_write.Add(data);
                string[] ls_link = link.Split(',');
                List<string> list_content = new List<string>();
                for (int i = 0; i < ls_link.Length; i++)
                {
                    ninjaadb.OpenLink(device, app, ls_link[i]);
                    Delay(5);
                    ClickOnLeapdroidPosition(device, 300, 300);
                    ClickOnLeapdroidPosition(device, 300, 300);
                    int step = 0;
                    bool has_share = false;
                Lb_start:
                    DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_shareprofile);
                    if (kq.status)
                    {
                        step = 0;
                        switch (kq.function)
                        {
                            case -2:
                                {
                                    dr.Cells["Message"].Value = kq.content;
                                    return " |Link " + (i + 1).ToString() + ": không hoàn thành";
                                }
                            case -1:
                                {
                                    Thread.Sleep(5000);
                                    break;
                                }
                            case 0:
                                {
                                    back(device, 1);
                                    break;
                                }
                            case 1:
                                {
                                    ClickOnLeapdroidPosition(device, kq.point);
                                    if (has_share)
                                    {
                                        dr.Cells["clSuccess"].Value = 1;
                                        Delay(i_delay);
                                        messeage += " |Link " + (i + 1).ToString() + ": hoàn thành";
                                        goto Lb_finish;
                                    }
                                    Delay(1);
                                    break;
                                }
                            case 2:
                                {
                                    ClickOnLeapdroidPosition(device, kq.point);
                                    Delay(2);
                                    DetechModel kqwrite = ninjaadb.RunDetechFunction(device, list_write);
                                    if (kqwrite.status)
                                    {
                                        ClickOnLeapdroidPosition(device, kqwrite.point);
                                        Thread.Sleep(1000);
                                        PressOnLeapdroid_vietnamese(device, Base64Encode(FunctionHelper.method_Spin(content)));
                                        Delay(1);
                                    }
                                    has_share = true;
                                    break;
                                }
                            default:
                                break;
                        }
                        goto Lb_start;
                    }
                    else
                    {
                        step++;
                        if (step <= 5)
                        {
                            if (step == 4)
                            {
                                ClickOnLeapdroidPosition(device, 300, 300);
                                ClickOnLeapdroidPosition(device, 300, 300);
                            }
                            goto Lb_start;
                        }
                        messeage += "|Link " + (i + 1).ToString() + ": không hoàn thành";
                    }
                Lb_finish:
                    Delay(i_delay);
                }
                activeNewfeed(device, app);
            }
            return messeage;
        }

        public string InviteLivesteam(DeviceData device, SettingTuongTac setTuongtac, string link, string content, decimal num, string app, int i_delay)
        {
            string message = "";
            if (link != "")
            {
                int step = 0;
                string[] ls_link = link.Split(',');
                List<string> list_content = new List<string>();
                list_content.Add("Send");
                list_content.Add("Gửi");
                for (int i = 0; i < ls_link.Length; i++)
                {
                    int count = 0;
                    ninjaadb.OpenLink(device, app, ls_link[i]);
                    Delay(3);
                Lb_start:
                    DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_shareprofile);
                    if (kq.status)
                    {
                        switch (kq.function)
                        {
                            case -2:
                                {
                                    return "| Link " + (i + 1).ToString() + ": mời bạn bè không hoàn thành";
                                }
                            case -1:
                                {
                                    Thread.Sleep(5000);
                                    break;
                                }
                            case 0:
                                {
                                    back(device, 1);
                                    break;
                                }
                            case 1:
                                {
                                    ClickOnLeapdroidPosition(device, kq.point);
                                    Delay(2);
                                lb_send:
                                    scroll_up_livestream(device);
                                    List<Point> ls_point = new List<Point>();

                                    ls_point = ninjaadb.FindByXpathList(device, "//node[contains(@class,'android.widget.Button')]", list_content);
                                    if (ls_point.Count > 0)
                                    {
                                        for (int n = 0; n < ls_point.Count; n++)
                                        {
                                            ClickOnLeapdroidPosition(device, ls_point[n]);
                                            count++;
                                            if (count >= num)
                                            {
                                                activeNewfeed(device, app);
                                                message += "|Link " + (i + 1).ToString() + " mời bạn bè hoàn thành:" + count.ToString() + "/" + num.ToString();
                                                goto lb_finish;
                                            }
                                        }
                                        goto lb_send;
                                    }
                                    else
                                        activeNewfeed(device, app);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    else
                    {
                        step++;
                        if (step <= 5)
                        {
                            if (step == 4)
                            {
                                ClickOnLeapdroidPosition(device, 300, 300);
                                ClickOnLeapdroidPosition(device, 300, 300);
                            }
                            goto Lb_start;
                        }
                        message += "|Link " + (i + 1).ToString() + " mời bạn bè không hoàn thành";
                    }
                lb_finish:
                    count = 0;
                    Delay(i_delay);
                }

            }
            if (message == "")
                message = "mời bạn bè không hoàn thành";
            return message;

        }

        public string ShareVideo_intoGroup(DeviceData device, SettingTuongTac setTuongtac, string link, string content, decimal numGroup, string app, int i_delay, string groupName)
        {
            string message = "";
            if (link != "")
            {
                Random rd = new Random();
                string[] ls_link = link.Split(',');
                List<string> list_content = new List<string>();
                for (int i = 0; i < ls_link.Length; i++)
                {
                    ninjaadb.OpenLink(device, app, ls_link[i]);
                    Delay(2);
                    if (ShareVideo_StaticGroup(device, groupName, FunctionHelper.method_Spin(content)))
                    {
                        message += " |Link " + (i + 1).ToString() + ": share cố định hoàn thành"; ;
                    }
                    else
                    {
                        message += " |Link " + (i + 1).ToString() + ": share cố định không hoàn thành";
                    }
                    Delay(i_delay);
                }
                activeNewfeed(device, app);
            }
            if (message == "")
                message = "Không hoàn thành";
            return message;
        }

        private bool ShareVideo_StaticGroup(DeviceData device, string namegroup, string content)
        {
            try
            {
                int max = 0;
                string nodecha = "//node[contains(@class,'android.widget.ListView')]";
                string nodecon = "//node[contains(@class,'android.widget.LinearLayout')]";
                DetechModel data = new DetechModel();
                data.parent = "Post";
                data.content = "Post";
                data.text = "Post";
                data.node = "//node[contains(@class,'android.widget.Button')]";
                data.function = 1;
                SettingTool.lang.list_shareVideo.Add(data);

                data = new DetechModel();
                data.parent = "ĐĂNG";
                data.content = "ĐĂNG";
                data.text = "ĐĂNG";
                data.function = 1;
                data.node = "//node[contains(@class,'android.widget.Button')]";
                SettingTool.lang.list_shareVideo.Add(data);

                Random rd = new Random();
                int i = 0;

            Lb_start:
                DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_shareVideo);
                if (kq.status)
                {
                    i = 0;
                    switch (kq.function)
                    {
                        case 1:
                            {
                                ClickOnLeapdroidPosition(device, kq.point);
                                Delay(2);
                                if (kq.text.ToLower() == "share" || kq.text.ToLower() == "chia sẻ")
                                {
                                lbl_clickGroup:
                                    var point = ninjaadb.FindByXpathIndex(device, "//node[contains(@class,'android.view.View')]", "7");
                                    if (point.Y > 0)
                                    {
                                        ClickOnLeapdroidPosition(device, point.X, point.Y);
                                        Delay(2);
                                        PressOnLeapdroid_vietnamese(device, Base64Encode(namegroup));
                                        Delay(2);
                                        List<Point> ls_point = ninjaadb.FindByDescList(device, nodecha, nodecon);
                                        if (ls_point.Count > 0)
                                        {
                                            point = ls_point[rd.Next(0, ls_point.Count)];
                                            ClickOnLeapdroidPosition(device, point.X + 30, point.Y);
                                        }
                                        goto Lb_start;
                                    }
                                    else
                                    {
                                        max++;
                                        if (max < 7)
                                            goto lbl_clickGroup;
                                    }
                                }
                                if (kq.text.ToLower() == "post" || kq.text.ToLower() == "đăng")
                                    return true;

                                break;
                            }
                        case 2:
                            {
                                ClickOnLeapdroidPosition(device, kq.point);
                                Delay(1);
                                PressOnLeapdroid_vietnamese(device, Base64Encode(content));
                                break;
                            }

                        default:
                            break;
                    }
                    goto Lb_start;
                }
                else
                {
                    i++;
                    if (i <= 10)
                    {
                        goto Lb_start;
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        public string ShareVideo_random(DeviceData device, SettingTuongTac setTuongtac, string link, string content, decimal numGroup, string app, int i_delay, string groupName)
        {
            string message = "";
            if (link != "")
            {
                int count = 0;
                Random rd = new Random();
                int max = 0;
                string[] ls_link = link.Split(',');
                List<string> list_content = new List<string>();
                for (int i = 0; i < ls_link.Length; i++)
                {
                    ninjaadb.OpenLink(device, app, ls_link[i]);
                    Delay(2);
                    while (count < numGroup)
                    {
                        if (shareVideo2Group(device, content))
                        {
                            max = 0;
                            Delay(i_delay);
                            count++;
                        }
                        else
                        {
                            max++;
                            if (max >= 3)
                            {
                                message += " |Link " + (i + 1).ToString() + ": share ngẫu nhiên hoàn thành " + count.ToString() + "/" + numGroup.ToString();
                                goto lb_finish;
                            }
                        }
                    }
                    message += " |Link " + (i + 1).ToString() + ": share ngẫu nhiên hoàn thành " + count.ToString() + "/" + numGroup.ToString();
                lb_finish:
                    count = 0;
                }
                activeNewfeed(device, app);
            }
            if (message == "")
                message = "share ngẫu nhiên không hoàn thành";
            return message;
        }

        private bool shareVideo2NewfeedLD(DeviceData device, string app, string content)
        {
            try
            {
                int i = 0;
            Lb_start:
                DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_shareVideo);
                if (kq.status)
                {
                    i = 0;
                    switch (kq.function)
                    {
                        case 1:
                            {
                                ClickOnLeapdroidPosition(device, kq.point);
                                if (kq.text.ToLower() == "share now" || kq.text.ToLower() == "chia sẻ ngay")
                                    return true;
                                break;
                            }
                        case 2:
                            {
                                ClickOnLeapdroidPosition(device, kq.point);
                                Delay(1);
                                string ls_content = FunctionHelper.method_Spin(content);
                                PressOnLeapdroid_vietnamese(device, Base64Encode(ls_content));
                                break;
                            }

                        default:
                            break;
                    }
                    goto Lb_start;
                }
                else
                {
                    i++;
                    if (i <= 7)
                    {
                        scroll_up_short(device);
                        goto Lb_start;

                    }

                }
            }
            catch
            {
            }
            return false;
        }
        private void shareVideo2Newfeed(DeviceData device, string app, string content)
        {
            int max = 0;
        lb_clickShare:
            List<string> ls_text = new List<string>();
            ls_text.Add("Chia sẻ");
            ls_text.Add("Share");
            var point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.TextView')]", ls_text);
            if (point.Y > 0)
            {
                ClickOnLeapdroidPosition(device, point.X, point.Y);
                Delay(1);
                var list_content = new List<string>();
                list_content.Add("Hãy nói gì đó về nội dung này");
                list_content.Add("Say something about this");
                point = ninjaadb.FindByXpath(device, "//node[contains(@class,'android.widget.EditText')]");
                if (point.Y > 0)
                {
                    ClickOnLeapdroidPosition(device, point.X, point.Y);
                    Delay(1);
                    string ls_content = FunctionHelper.method_Spin(content);
                    PressOnLeapdroid_vietnamese(device, Base64Encode(ls_content));
                    list_content = new List<string>();
                    list_content.Add("CHIA SẺ NGAY");
                    list_content.Add("Share  Now");
                    point = ninjaadb.FindByXpathDesc(device, "(//node[contains(@class,'android.view.View')])", list_content);
                    if (point.Y > 0)
                    {
                        ClickOnLeapdroidPosition(device, point);
                        Delay(1);
                        point = ninjaadb.FindByXpathDesc(device, "(//node[contains(@class,'android.view.View')])", list_content);
                        if (point.X > 0)
                            ClickOnLeapdroidPosition(device, point);

                        activeNewfeed(device, app);
                    }
                    else
                    {
                        back(device, 1);
                    }

                }
            }
            else
            {
                max++;
                if (max <= 3)
                    goto lb_clickShare;
            }
        }
        public void scroll_down(DeviceData device)
        {
            ADBHelper.Swipe(device.Serial, 300, 400, 300, 800);
        }
        public void scroll_up(DeviceData device)
        {
            ADBHelper.Swipe(device.Serial, 300, 900, 300, 400);

        }

        public void scroll_up_random(DeviceData device)
        {
            Random rd = new Random();
            int loop = rd.Next(1, 7);
            if (loop == 1 || loop == 3 || loop == 5)
            {
                scroll_up(device);
                scroll_up(device);
                scroll_up(device);
            }
            else if (loop == 6 || loop == 2 || loop == 4)
            {
                scroll_up(device);
                scroll_up(device);
            }
            else
                scroll_up(device);

        }

        public void scroll_up_randomShare(DeviceData device)
        {
            Random rd = new Random();
            int loop = rd.Next(1, 7);
            if (loop == 1 || loop == 3 || loop == 5)
            {
                scroll_up_mid(device);

                scroll_up_mid(device);
            }
            else if (loop == 6 || loop == 2 || loop == 4)
            {
                scroll_up_mid(device);
            }



        }

        public void scroll_up_livestream(DeviceData device)
        {
            Point point = new Point();
            point = ninjaadb.GetScreenResolution(device.ToString());
            ADBHelper.Swipe(device.Serial, 300, point.Y - 200, 300, point.Y - 1000, 500);

        }

        public void scroll_up_mid(DeviceData device)
        {
            ADBHelper.Swipe(device.Serial, 300, 900, 300, 600);
        }
        public void back(DeviceData device, int times, int delay = 1)
        {
            for (int i = 0; i < times; i++)
            {
                Delay(delay);
                ninjaadb.Return(device);
                Delay(delay);
            }

        }

        void PressOnLeapdroid(DeviceData device, string keys)
        {
            ninjaadb.InputText(device, keys);
        }
        void PressOnLeapdroid_vietnamese(DeviceData device, string keys)
        {
            ninjaadb.InputText_vietnamese(device, keys);
        }
        public void PressEnter(DeviceData device)
        {
            ninjaadb.pressEnter(device);
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
        public bool logOut(DeviceData device, string app)
        {
            int i = 0;
        Lb_start:
            DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_detech);
            if (kq.status)
            {
                i = 0;
                switch (kq.function)
                {
                    case 0:
                        {
                            back(device, 1);
                            break;
                        }
                    case 1:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            Delay(1);
                            if (kq.content.Contains("just tap your profile picture instead") || kq.content.Contains("lưu thông tin đăng nhập") || kq.content.Contains("lưu đăng nhập"))
                            {
                                Thread.Sleep(2000);
                            }
                            break;
                        }
                    case 2:
                        {
                            return true;
                        }
                    case 3:
                        {
                            var point = intoMenu(device);
                            if (point.X > 0 || point.Y > 0)
                            {
                                ClickOnLeapdroidPosition(device, point);
                                List<DetechModel> detechLog = new List<DetechModel>();
                                DetechModel model = new DetechModel();
                                model.parent = "";
                                model.content = "Log Out";
                                model.text = "Log Out";
                                model.node = "//node[contains(@class,'android.view.View')]";
                                model.function = 1;
                                detechLog.Add(model);

                                model = new DetechModel();
                                model.parent = "";
                                model.content = "Đăng xuất";
                                model.text = "Đăng xuất";
                                model.node = "//node[contains(@class,'android.view.View')]";
                                model.function = 1;
                                detechLog.Add(model);

                            lbl_logout:
                                Delay(1);
                                scroll_up(device);
                                DetechModel kqLog = ninjaadb.detechFunction(device, detechLog);
                                if (!kqLog.status)
                                {
                                    i++;
                                    if (i < 6)
                                        goto lbl_logout;
                                }
                                i = 0;
                                if (kqLog.point.Y > 0)
                                {
                                    ClickOnLeapdroidPosition(device, kqLog.point);
                                    Thread.Sleep(1000);
                                    List<DetechModel> list_data = new List<DetechModel>();
                                    DetechModel data = new DetechModel();
                                    data.parent = "login";
                                    data.content = "Gỡ tài khoản khỏi thiết bị";
                                    data.text = "Gỡ tài khoản khỏi thiết bị";
                                    data.node = "//node[contains(@class,'android.widget.TextView')]";
                                    data.function = 1;
                                    list_data.Add(data);
                                    data = new DetechModel();
                                    data.parent = "login";
                                    data.content = "Log out now?";
                                    data.text = "Log Out";
                                    data.node = "//node[contains(@class,'android.widget.Button')]";
                                    data.function = 1;
                                    list_data.Add(data);

                                    data = new DetechModel();
                                    data.parent = "login";
                                    data.content = "Đăng xuất ngay bây giờ?";
                                    data.text = "Đăng xuất";
                                    data.node = "//node[contains(@class,'android.widget.Button')]";
                                    data.function = 1;
                                    list_data.Add(data);

                                    data = new DetechModel();
                                    data.parent = "login";
                                    data.content = "Remove account from device?";
                                    data.text = "Remove Account";
                                    data.node = "//node[contains(@class,'android.widget.Button')]";
                                    data.function = 1;
                                    list_data.Add(data);

                                    DetechModel kqremove = ninjaadb.RunDetechFunction(device, list_data);
                                    if (kqremove.status)
                                    {
                                        ClickOnLeapdroidPosition(device, kqremove.point);
                                    }
                                    return true;
                                }
                                i++;

                            }
                            return true;
                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                i++;
                if (i <= 10)
                {
                    goto Lb_start;
                }
            }
            return false;
        }

        public bool regNick(DeviceData device, Account acc)
        {

            List<DetechModel> list_next = new List<DetechModel>();
            DetechModel data = new DetechModel();
            data.parent = "reg";
            data.content = "Tiếp";
            data.text = "Tiếp";
            data.node = "//node[contains(@class,'android.widget.Button')]";
            data.function = 1;
            list_next.Add(data);
            int step = 0;
            bool has_share = false;
        Lb_start:
            DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_reg);
            if (kq.status)
            {
                step = 0;
                switch (kq.function)
                {
                    case -2:
                        {
                            return false;
                        }
                    case -1:
                        {
                            Thread.Sleep(5000);
                            break;
                        }
                    case 0:
                        {
                            back(device, 1);
                            break;
                        }
                    case 1:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);

                            Delay(1);
                            break;
                        }
                    case 2:
                        {
                            List<DetechModel> list_write = new List<DetechModel>();
                            data = new DetechModel();
                            data.parent = "reg";
                            data.content = "Bạn tên gì?";
                            data.text = "Tên";
                            data.node = "//node[contains(@class,'TextInputLayout')]";
                            data.function = 2;
                            list_write.Add(data);

                            DetechModel kqwrite = ninjaadb.RunDetechFunction(device, list_write);

                            if (kqwrite.status)
                            {
                                ClickOnLeapdroidPosition(device, kqwrite.point);
                                Thread.Sleep(1000);
                                PressOnLeapdroid(device, FunctionHelper.method_Spin("{Nga|Trang}"));
                                Delay(1);
                            }
                            list_write = new List<DetechModel>();
                            data = new DetechModel();
                            data.parent = "reg";
                            data.content = "Bạn tên gì?";
                            data.text = "Họ";
                            data.node = "//node[contains(@class,'TextInputLayout')]";
                            data.function = 2;
                            list_write.Add(data);

                            kqwrite = ninjaadb.RunDetechFunction(device, list_write);
                            if (kqwrite.status)
                            {
                                ClickOnLeapdroidPosition(device, kqwrite.point.X, kqwrite.point.Y + 100);
                                Thread.Sleep(1000);
                                PressOnLeapdroid(device, FunctionHelper.method_Spin("{Tran|Le|Nguyen|Hoang|Phan|Ly}"));
                                Delay(1);
                            }


                            kqwrite = ninjaadb.RunDetechFunction(device, list_next);
                            if (kqwrite.status)
                            {
                                ClickOnLeapdroidPosition(device, kqwrite.point);
                                Thread.Sleep(1000);

                            }
                            break;

                        }
                    case 3:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            List<DetechModel> list_write = new List<DetechModel>();
                            data = new DetechModel();
                            data.parent = "reg";
                            data.content = "Giới tính của bạn là gì?";
                            data.text = "Tiếp";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_write.Add(data);

                            DetechModel kqwrite = ninjaadb.RunDetechFunction(device, list_write);

                            if (kqwrite.status)
                            {
                                ClickOnLeapdroidPosition(device, kqwrite.point);
                                Thread.Sleep(1000);
                                Random rd = new Random();
                                PressOnLeapdroid(device, "091" + rd.Next(1000000, 9999999));
                                kqwrite = ninjaadb.detechFunction(device, list_next);
                                if (kqwrite.status)
                                {
                                    ClickOnLeapdroidPosition(device, kqwrite.point);
                                    Thread.Sleep(1000);
                                    PressOnLeapdroid(device, acc.Password);
                                    //next
                                    kqwrite = ninjaadb.detechFunction(device, list_next);
                                    if (kqwrite.status)
                                    {
                                        ClickOnLeapdroidPosition(device, kqwrite.point);
                                        Thread.Sleep(1000);

                                    }
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            Thread.Sleep(100);
                            PressOnLeapdroid(device, acc.email);

                            DetechModel kqwrite = ninjaadb.RunDetechFunction(device, list_next);

                            if (kqwrite.status)
                            {
                                ClickOnLeapdroidPosition(device, kqwrite.point);

                            }
                            break;
                        }
                    case 5:
                        {
                            ClickOnLeapdroidPosition(device, kq.point);
                            while (SettingTool.codefacebook == null)
                            {
                                Thread.Sleep(3000);

                            }
                            PressOnLeapdroid(device, SettingTool.codefacebook);
                            Thread.Sleep(1000);
                            List<DetechModel> list_xacnhan = new List<DetechModel>();
                            data = new DetechModel();
                            data.parent = "reg";
                            data.content = "Xác nhận";
                            data.text = "Xác nhận";
                            data.node = "//node[contains(@class,'android.widget.Button')]";
                            data.function = 1;
                            list_xacnhan.Add(data);

                            DetechModel kqwrite = ninjaadb.RunDetechFunction(device, list_xacnhan);

                            if (kqwrite.status)
                            {
                                ClickOnLeapdroidPosition(device, kqwrite.point);

                            }
                            break;
                        }
                    case 6:
                        {
                            ClickOnLeapdroidPosition(device, 100, 100);
                            break;
                        }
                    case 7:
                        {
                            return true;
                        }
                    default:
                        break;
                }
                goto Lb_start;
            }
            else
            {
                step++;
                if (step <= 5)
                {
                    goto Lb_start;

                }

            }
            return false;

        }
        private void choosegroupandcommnet(DeviceData ldid, string content)
        {
            scroll_up_random(ldid);
            List<Point> ls_point = new List<Point>();
            ls_point = ninjaadb.FindByDescList(ldid, "//node[contains(@class,'android.widget.ListView')]", "//node[contains(@class,'android.widget.LinearLayout')]");
            if (ls_point.Count > 0)
            {
                Random rd = new Random();
                Point pointed = ls_point[rd.Next(0, ls_point.Count)];
                ClickOnLeapdroidPosition(ldid, pointed.X + 30, pointed.Y);
                Delay(2);
                List<DetechModel> list_write = new List<DetechModel>();
                DetechModel data = new DetechModel();
                data = new DetechModel();
                data.parent = "post";
                data.content = "Write something";
                data.text = "Write something…";
                data.node = "//node[contains(@class,'android.widget.EditText')]";
                data.function = 2;
                list_write.Add(data);

                data = new DetechModel();
                data.parent = "post";
                data.content = "Viết gì đó";
                data.text = "Viết gì đó...";
                data.node = "//node[contains(@class,'android.widget.EditText')]";
                data.function = 2;
                list_write.Add(data);
                DetechModel kqwrite = ninjaadb.RunDetechFunction(ldid, list_write);
                if (kqwrite.status)
                {
                    ClickOnLeapdroidPosition(ldid, kqwrite.point);
                    Thread.Sleep(1000);
                    PressOnLeapdroid_vietnamese(ldid, Base64Encode(FunctionHelper.method_Spin(content)));
                    Delay(1);
                }
            }
        }
        public bool likePostLD(DeviceData ldID)
        {
            try
            {
                int i = 0;
            Lb_start:
                DetechModel kq = ninjaadb.detechFunction(ldID, SettingTool.lang.list_detechlike);
                if (kq.status)
                {
                    ClickOnLeapdroidPosition(ldID, kq.point);
                    return true;
                }
                else
                {
                    i++;
                    if (i <= 3)
                    {
                        scroll_up_short(ldID);
                        Delay(1);
                        goto Lb_start;
                    }
                }
            }
            catch
            {
            }
            return false;
        }
        private bool shareVideo2Group(DeviceData device, string content)
        {
            try
            {
                int max = 0;
                string nodecha = "//node[contains(@class,'android.widget.ListView')]";
                string nodecon = "//node[contains(@class,'android.widget.LinearLayout')]";
                DetechModel data = new DetechModel();
                data.parent = "Post";
                data.content = "Post";
                data.text = "Post";
                data.node = "//node[contains(@class,'android.widget.Button')]";
                data.function = 1;
                SettingTool.lang.list_shareVideo.Add(data);

                data = new DetechModel();
                data.parent = "ĐĂNG";
                data.content = "ĐĂNG";
                data.text = "ĐĂNG";
                data.function = 1;
                data.node = "//node[contains(@class,'android.widget.Button')]";
                SettingTool.lang.list_shareVideo.Add(data);

                Random rd = new Random();
                int i = 0;
            Lb_start:
                DetechModel kq = ninjaadb.detechFunction(device, SettingTool.lang.list_shareVideo);
                if (kq.status)
                {
                    i = 0;
                    switch (kq.function)
                    {
                        case 1:
                            {
                                ClickOnLeapdroidPosition(device, kq.point);
                                Delay(2);
                                if (kq.text.ToLower() == "share" || kq.text.ToLower() == "chia sẻ")
                                {
                                lbl_clickGroup:
                                    var point = ninjaadb.FindByXpathIndex(device, "//node[contains(@class,'android.view.View')]", "7");
                                    if (point.Y > 0)
                                    {
                                        max = 0;
                                        ClickOnLeapdroidPosition(device, point.X, point.Y);
                                        Delay(5);
                                        int scroll = rd.Next(0, 5);
                                        if (scroll == 2)
                                        {
                                            scroll_up(device);
                                            scroll_up_mid(device);
                                        }
                                        else if (scroll == 1)
                                            scroll_up(device);

                                        List<Point> ls_point = new List<Point>();
                                        ls_point = ninjaadb.FindByDescList(device, nodecha, nodecon);
                                        point = ls_point[rd.Next(0, ls_point.Count)];
                                        ClickOnLeapdroidPosition(device, point.X + 30, point.Y);
                                        Delay(2);
                                    }
                                    else
                                    {
                                        max++;
                                        if (max < 7)
                                            goto lbl_clickGroup;
                                    }
                                }
                                if (kq.text.ToLower() == "post" || kq.text.ToLower() == "đăng")
                                    return true;

                                break;
                            }
                        case 2:
                            {
                                ClickOnLeapdroidPosition(device, kq.point);
                                Delay(1);
                                string ls_content = FunctionHelper.method_Spin(content);
                                PressOnLeapdroid_vietnamese(device, Base64Encode(ls_content));
                                break;
                            }

                        default:
                            break;
                    }
                    goto Lb_start;
                }
                else
                {
                    if (i < 2)
                        scroll_up_short(device);
                    i++;
                    if (i < 6)
                    {
                        goto Lb_start;
                    }
                }
            }
            catch
            {
            }

            return false;
        }
        public void backNewfeed(DeviceData device)
        {
            for (int i = 0; i < 5; i++)
            {
                DetechModel kq = ninjaadb.RunDetechFunction(device, SettingTool.lang.list_newfeed, 2);
                if (kq.status)
                {
                    break;
                }
                else
                {
                    back(device, 1);
                }
            }
        }

        public void backNewfeedGroup(DeviceData device)
        {
            for (int i = 0; i < 5; i++)
            {
                DetechModel kq = ninjaadb.RunDetechFunction(device, SettingTool.lang.list_joingroup, 2);
                if (kq.status & kq.point.Y > 0)
                {
                    break;
                }
                else
                {
                    back(device, 1);
                }
            }
        }
        public bool createFolder2(DeviceData ldID, string folder)
        {
            try
            {
                string cmd = String.Format("shell mkdir {0}", folder);
                string html = ninjaadb.runComand(ldID, cmd);
                if (html.Contains("No such file or directory"))
                    return false;
            }
            catch { }
            return true;
        }
        public bool deleteFolder2(DeviceData ldID, string folder)
        {
            try
            {
                string cmd = String.Format("shell  rm -r {0}", folder);
                string html = ninjaadb.runComand(ldID, cmd);
                if (html.Contains("No such file or directory"))
                    return false;
            }
            catch { }
            return true;
        }
        public string commentGroupID(DataGridViewRow dr, DeviceData ldID, string app, int numLike, int numcommet, List<string> lsID, string lsComment, bool like, bool comment, bool isGroup, int delay)
        {
            string message = "";
            try
            {
                for (int i = 0; i < lsID.Count; i++)
                {
                    string result = "";
                    if (isGroup)
                        result = ninjaadb.OpenLink(ldID, app, "fb://group/" + lsID[i]);
                    else
                        result = ninjaadb.OpenLink(ldID, app, "fb://profile/" + lsID[i]);

                    Delay(3);
                    if (!result.Contains("Error"))
                    {
                        dr.Cells["Message"].Value = "Tương tác ID: " + lsID[i];
                        message += scrollLikeCommentID(ldID, app, numLike, numcommet, like, comment, lsComment, lsID[i]);

                    }
                    dr.Cells["Message"].Value = "Delay " + delay.ToString() + " giây chuyển ID";
                    Delay(delay);
                }

                activeNewfeed(ldID, app);
            }
            catch
            {
            }
            return message;
        }
        private string scrollLikeCommentID(DeviceData device, string app, int numLike, int numComment, bool like, bool comment, string content, string gid, int numdelay = 1)
        {
            int dem = 0;
            int max = 0;
            string mess = "";

            if (like & !comment)
            {
                while (dem < numLike)
                {
                    scroll_up_random(device);

                    if (likePostLD(device))
                    {
                        max = 0;
                        dem++;
                    }
                    else
                        max++;
                    if (max >= 3)
                        break;
                }
                mess += "|" + gid + " like hoàn thành:" + dem.ToString() + "/" + numLike.ToString();

            }

            else if (!like & comment)
            {
                while (dem < numComment)
                {
                    scroll_up_random(device);
                    if (commentPostLD(device, FunctionHelper.method_Spin(content)))
                    {
                        max = 0;
                        dem++;
                        Delay(numdelay);
                        back(device, 2);
                    }
                    else
                        max++;
                    if (max >= 3)
                        break;
                }
                mess += "|" + gid + " comment hoàn thành:" + dem.ToString() + "/" + numComment.ToString();
            }
            else
            {
                int numMax = 0;
                if (numLike > numComment)
                    numMax = numLike;
                else
                    numMax = numComment;

                int count_like = 0;

                while (dem < numMax)
                {
                    scroll_up_random(device);
                    if (count_like < numLike)
                    {
                        if (likePostLD(device))
                        {
                            max = 0;
                            count_like++;
                        }
                    }

                    if (dem < numComment)
                    {
                        if (commentPostLD(device, FunctionHelper.method_Spin(content)))
                        {
                            max = 0;
                            dem++;
                            back(device, 2);
                        }
                    }
                    if (count_like > numLike & dem > numComment)
                        break;
                    else
                    {
                        max++;
                        if (max > 3)
                            break;
                    }
                }
                mess += "|" + gid + " hoàn thành: like " + count_like.ToString() + "/" + numLike.ToString() + " comment " + dem.ToString() + "/" + numComment.ToString();
            }
            return mess;
        }
        private string[] SplitByLength(string s, int d)
        {
            List<string> stringList = new List<string>();
            if (s.Length <= d) stringList.Add(s);
            else
            {
                int x = 0;
                for (; (x + d) < s.Length; x += d)
                {
                    stringList.Add(s.Substring(x, d));
                }
                stringList.Add(s.Substring(x));
            }
            return stringList.ToArray();
        }


    }
}
