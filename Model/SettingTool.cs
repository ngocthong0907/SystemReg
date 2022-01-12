using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class SettingTool
    {
        public static string email { set; get; }
        public static string password { set; get; }
        public static string hid { set; get; }
        public static string mac { set; get; }
        public static string ram { set; get; }
        public static string hdd { set; get; }
        public static string day { set; get; }
        public static double version { set; get; }
        public static string versiontext { set; get; }
        public static string banquyen = "Trial";
        public static string privatekey { set; get; }
        public static string ninjatoken { set; get; }
        public static string ntoken { set; get; }
        public static string note { set; get; }
        public static string key { set; get; }
        public static ClientModel client { set; get; }
        public static Dictionary<string, string> data { set; get; }
        public static Language lang { set; get; }
        public static ConfigAdd configadd { set; get; }
        public static string dtsg { set; get; }
        public static string codefacebook { set; get; }
        public static ConfigLD configld { set; get; }
        public static string pathLD { set; get; }
        public static string pathfolderld { set; get; }

        public static configFormPost configPost { set; get; }

        public static configFormShare configShare { set; get; }

        public static configFormCareFanpage configCarepage { set; get; }

    //    public static string pathdocument { set; get; }

        public static CommentContent commentContent { set; get; }
        public static List<string> list_xproxy { set; get; }

        public static string linkproxyninja { set; get; }

        public static List<xproxy> list_freeproxy { set; get; }

        public static List<string> list_running { set; get; }

        public static List<string> list_ld = new List<string>();

        public static object synld = new object();

        public static object lockobj = new object();

    }
    public class ConfigLD
    {
        public string pathLD { set; get; }
        public int numthread { set; get; }
        public int numthreadxproxy { set; get; }
        public int timedelay { set; get; }
        public bool has_proxy { set; get; }
        public string cookies { set; get; }
        public string token { set; get; }
        public bool has_savetoken { set; get; }

        public bool has_quitLD { set; get; }

        public string pathHMA { set; get; }

        public int accountIP { set; get; }
      
        public int numRunLD { set; get; }
        public int typeip { set; get; }

        public int typedefaulV6 { set; get; }
        public string apitinsoft { set; get; }

        public string tinsoftname { set; get; }
        public string tinsoftid { set; get; }
        public string apiTMproxy { set; get; }
        public int delaydcom { set; get; }

        public int delaydcomxproxy { set; get; }

        public string linkxproxy { set; get; }
        public string versionld { set; get; }

        public string language { set; get; }
        public int timeout { set; get; }
        public bool sock5 { set; get; } 

        public static string linkproxyninja { set; get; }

        public  string apiproxyv6 { set; get; }

        public static List<xproxy> list_freeproxy { set; get; }

        public static List<string> list_running { set; get; }

        public string proxytype { set; get; }

        public string appproxy { set; get; }

        public string linkobc { set; get; }
        public string appversion { set; get; }

        public string pathsavedata { set; get; }
        public string sizeld { set; get; }
        public int sizeldwidth { set; get; }
        public int sizeldheight { set; get; }
        public bool checkproxy { set; get; }
        public string package { set; get; }

        public string cpu { set; get; }

        public string ram { set; get; }
    }

    public class configFormPost
    {
        public string folderAnh { set; get; }
        public string  noidung { set; get; }
      
        public int soluongAnh { set; get; }

        public int delay { set; get; }

        public bool chkProxy { set; get; }
        public bool chkDelete { set; get; }

        public string fileTen { set; get; }
        public string fileHo { set; get; }
        public string passdefaul { set; get; }
        public string lsID { set; get; }

        public string linkpost { set; get; }
        public string contentlink { set; get; }

    }

    public class configFormShare
    {
        public string link { set; get; }
        public string contentShare { set; get; }
        public string contentComment { set; get; }
        public bool limit { set; get; }
        public int numlimit { set; get; }
        public int delaymin { set; get; }
        public int delaymax { set; get; }
        public bool loop { set; get; }
        public int timeloop { set; get; }
        public bool has_view { set; get; }
        public int timeviewmin { set; get; }
        public int timeviewmax { set; get; }
        public bool has_invite { set; get; }
        public int num_invitemin { set; get; }
        public int num_invitemax { set; get; }
        public bool has_like { set; get; }
        public bool has_comment { set; get; }
        public bool has_sharenewfeed { set; get; }
        public bool has_sharegroup { set; get; }
        public int num_sharemin { set; get; }
        public int num_sharemax { set; get; }
        public int num_member { set; get; }
        public bool has_share_random { set; get; }
        public bool has_share_file { set; get; }
        public bool has_share_noapprove { set; get; }
        public int typeshare { set; get; }
        public string pathgroup { set; get; }
        public int delay_click_min { set; get; }
        public int delay_click_max { set; get; }
    }

    public class configFormCareFanpage
    {
        public string linkID { set; get; }
      
        public string contentComment { set; get; }

        public int numLikefrom { set; get; }
        public int numLiketo { set; get; }

        public int numCommentfrom { set; get; }
        public int numCommentto { set; get; }
        public int numTagfrom { set; get; }
        public int numTagto { set; get; }
        public int numSharefrom { set; get; }
        public int numShareto { set; get; }
        public int numLienquanfrom { set; get; }
        public int numLienquanto { set; get; }

        public int numQuangcaofrom { set; get; }
        public int numQuangcaoto { set; get; }

        public int numDelayfrom { set; get; }
        public int numDelayto { set; get; }
        public int numrandomaction { set; get; }

        public bool chkLikeFanpage { set; get; }
        public bool chkLike { set; get; }  
        public bool chkComment { set; get; }
        public bool chkTag { set; get; }
        public bool chkShare { set; get; }
        public bool chkLienquan { set; get; }
        public bool chkQuangcao { set; get; }
        public bool chkRandomaction { set; get; }
        public int numdelayscrollfrom { set; get; }
        public int numdelayscrollto { set; get; }

    }
    public class Language
    {
        public List<string> list_phone = new List<string>();
        public List<string> list_submit = new List<string>();
        public List<string> list_skip = new List<string>();
        public List<string> list_loi = new List<string>();
        public List<string> list_checklogin = new List<string>();
        public List<string> list_appfinish = new List<string>();
        public List<string> list_addfriend = new List<string>();
        public List<string> list_comment = new List<string>();
        public List<string> write_comment = new List<string>();
        public List<string> list_login = new List<string>();

        public List<DetechModel> list_detech = new List<DetechModel>();
        public List<DetechModel> list_detechpass = new List<DetechModel>();
        public List<DetechModel> list_detechadd = new List<DetechModel>();
        public List<DetechModel> list_detechlike = new List<DetechModel>();
        public List<DetechModel> list_detechcomment = new List<DetechModel>();
        public List<DetechModel> list_detechsend = new List<DetechModel>();
        public List<DetechModel> list_detechaddfriensuggest = new List<DetechModel>();
        public List<DetechModel> list_detechacceptfriend = new List<DetechModel>();
        public List<DetechModel> list_addfriendnewfeed = new List<DetechModel>();
        public List<DetechModel> list_canceladdfriend = new List<DetechModel>();
        public List<DetechModel> list_invite2page = new List<DetechModel>();
        public List<DetechModel> list_buttonadd = new List<DetechModel>();

        public List<DetechModel> list_detechpost = new List<DetechModel>();
        public List<DetechModel> list_postcontent = new List<DetechModel>();
        public List<DetechModel> list_changeavatar = new List<DetechModel>();
        public List<DetechModel> list_changecover = new List<DetechModel>();
        public List<DetechModel> list_changeinfo = new List<DetechModel>();
        public List<DetechModel> list_buttonpost = new List<DetechModel>();

        public List<DetechModel> list_sharelive = new List<DetechModel>();
        public List<DetechModel> list_sharelivenoapprove = new List<DetechModel>();
        
        public List<DetechModel> list_hma = new List<DetechModel>();
        public List<DetechModel> list_shareprofile = new List<DetechModel>();
        public List<DetechModel> list_sharepost = new List<DetechModel>();
        public List<DetechModel> list_shareposttonewfeed = new List<DetechModel>();
        public List<DetechModel> list_newfeed = new List<DetechModel>();
        public List<DetechModel> list_notification = new List<DetechModel>();
        public List<DetechModel> list_seeding_like = new List<DetechModel>();
        public List<DetechModel> list_seeding_comment = new List<DetechModel>();
        public List<DetechModel> list_seeding_share = new List<DetechModel>();
        public List<DetechModel> list_menu = new List<DetechModel>();
        public List<DetechModel> list_reg = new List<DetechModel>();
        public List<DetechModel> list_shareVideo = new List<DetechModel>();
        public List<DetechModel> list_joingroup = new List<DetechModel>();
        public List<DetechModel> list_Proxy = new List<DetechModel>();
        public List<DetechModel> list_AddContact = new List<DetechModel>();
        public List<DetechModel> list_2fa = new List<DetechModel>();
        public List<DetechModel> list_openMess = new List<DetechModel>();
        public List<DetechModel> list_regld = new List<DetechModel>();
        public List<DetechModel> list_keyvpn = new List<DetechModel>();
        public List<DetechModel> list_dump = new List<DetechModel>();
        public List<DetechModel> list_logout = new List<DetechModel>();
        public List<DetechModel> list_next= new List<DetechModel>();
        public List<DetechModel> list_logoutavatar = new List<DetechModel>();
        public List<DetechModel> list_sharevideowatch = new List<DetechModel>();
        public List<ImeiModel> list_imei = new List<ImeiModel>();
        public List<DetechModel> list_checkpoint = new List<DetechModel>();
        public List<DetechModel> list_khangspam = new List<DetechModel>();
        public void setDataLD()
        {
            try
            {
                
                CustomerController customer = new CustomerController();

                ResultRequest kq = customer.sendLogs("createimei2");
                list_imei = new List<ImeiModel>();
                if (kq.status)
                {
                    JArray jarr = JArray.Parse(kq.data);
                    foreach (var item in jarr)
                    {
                        try
                        {
                            ImeiModel imei = new ImeiModel();
                            string[] rowsplit = item["Value"].ToString().Split('|');

                            imei.brand = rowsplit[0];
                            imei.model = rowsplit[1];
                            imei.value = rowsplit[2];
                            list_imei.Add(imei);
                        }
                        catch
                        { }
                    }
                }
                if(SettingTool.configld.appversion=="Facebook 251"||string.IsNullOrEmpty(SettingTool.configld.appversion))
                {
                    kq = customer.sendLogs("loginldv4");
                }
                else
                {
                    if (SettingTool.configld.appversion == "Facebook 302")
                    {
                        kq = customer.sendLogs("loginld302");
                    }
                    else
                    {
                        if (SettingTool.configld.appversion == "Facebook Lite")
                        {
                            kq = customer.sendLogs("loginldlite");
                        }
                        else
                        {
                            kq = customer.sendLogs("loginld299");
                        }
                    }
                        
                }
                Dictionary<string, string> mydata = new Dictionary<string, string>();
                if (kq.status)
                {
                    JArray jarr = JArray.Parse(kq.data);
                    foreach (var item in jarr)
                    {
                        mydata.Add(item["Key"].ToString(), item["Value"].ToString());
                    }
                }
                SettingTool.data = mydata;
                list_detechpass = new List<DetechModel>();
                string[] inputpass = mydata["inputpass"].Split('|');
                DetechModel model = new DetechModel();
                model.parent = inputpass[0];
                model.content = inputpass[1];
                model.text = inputpass[2];
                model.node = inputpass[3];
                model.function = Convert.ToInt32(inputpass[4]);
                list_detechpass.Add(model);

                //datalogin
                list_detech = new List<DetechModel>();
                JArray arr = JArray.Parse(mydata["login"]);
                foreach(var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_detech.Add(model);

                    }
                    catch { }
                 
                }
                //list_logoutavatar
                list_logoutavatar = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_logoutavatar"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_logoutavatar.Add(model);

                    }
                    catch { }

                }
                //next
                list_next = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_next"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_next.Add(model);

                    }
                    catch { }

                }
                //data addfriend
                list_detechadd = new List<DetechModel>();
                arr = JArray.Parse(mydata["addfriend"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_detechadd.Add(model);

                    }
                    catch { }

                }
                //data like
                list_detechlike = new List<DetechModel>();
                arr = JArray.Parse(mydata["like"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_detechlike.Add(model);

                    }
                    catch { }

                }
                //data comment
                list_detechcomment = new List<DetechModel>();
                arr = JArray.Parse(mydata["comment"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_detechcomment.Add(model);

                    }
                    catch { }

                }//data send
                list_detechsend = new List<DetechModel>();
                arr = JArray.Parse(mydata["send"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_detechsend.Add(model);

                    }
                    catch { }

                }
                //data friendsuggest
                list_detechaddfriensuggest = new List<DetechModel>();
                arr = JArray.Parse(mydata["friendsuggest"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_detechaddfriensuggest.Add(model);

                    }
                    catch { }

                }//data acceptfriend
                list_detechacceptfriend = new List<DetechModel>();
                arr = JArray.Parse(mydata["acceptfriend"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_detechacceptfriend.Add(model);

                    }
                    catch { }

                }

                //data addfriendnewfeed
                list_addfriendnewfeed = new List<DetechModel>();
                arr = JArray.Parse(mydata["addfriendnewfeed"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_addfriendnewfeed.Add(model);

                    }
                    catch { }

                }
                //data addfriendnewfeed
                list_buttonadd = new List<DetechModel>();
                arr = JArray.Parse(mydata["buttonadd"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_buttonadd.Add(model);

                    }
                    catch { }

                }
                //data post image
                list_detechpost = new List<DetechModel>();
                 arr = JArray.Parse(mydata["postimage"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_detechpost.Add(model);

                    }
                    catch { }

                }  
                //data post image
                list_postcontent = new List<DetechModel>();
                arr = JArray.Parse(mydata["postcontent"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_postcontent.Add(model);

                    }
                    catch { }

                } 
                //buttonpost
                list_buttonpost = new List<DetechModel>();
                arr = JArray.Parse(mydata["buttonpost"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_buttonpost.Add(model);

                    }
                    catch { }

                }
                //data change avatar
                list_changeavatar = new List<DetechModel>();
                arr = JArray.Parse(mydata["changeavatar"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_changeavatar.Add(model);

                    }
                    catch { }

                }
                //data change avatar
                list_changeinfo = new List<DetechModel>();
                arr = JArray.Parse(mydata["changeinfo"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_changeinfo.Add(model);

                    }
                    catch { }

                }//data change avatar
                list_changecover= new List<DetechModel>();
                arr = JArray.Parse(mydata["changecover"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_changecover.Add(model);

                    }
                    catch { }

                }
                //data sharelive
                list_sharelive = new List<DetechModel>();
                arr = JArray.Parse(mydata["sharelive"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_sharelive.Add(model);

                    }
                    catch { }

                }
                //data sharelive
                list_sharelivenoapprove = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_sharelivenoapprove"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_sharelivenoapprove.Add(model);

                    }
                    catch { }

                }
                //data sharelive
                //data hma
                list_hma= new List<DetechModel>();
                arr = JArray.Parse(mydata["hma"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_hma.Add(model);

                    }
                    catch { }

                } 
                //share profile
                list_shareprofile = new List<DetechModel>();
                arr = JArray.Parse(mydata["shareprofile"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_shareprofile.Add(model);

                    }
                    catch { }

                }
                //share post
                list_sharepost = new List<DetechModel>();
                arr = JArray.Parse(mydata["sharepost"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_sharepost.Add(model);

                    }
                    catch { }

                }
                //share post to newfeed
                list_shareposttonewfeed = new List<DetechModel>();
                arr = JArray.Parse(mydata["shareposttonewfeed"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_shareposttonewfeed.Add(model);

                    }
                    catch { }

                }
            //share post to newfeed
                list_newfeed = new List<DetechModel>();
                arr = JArray.Parse(mydata["newfeed"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_newfeed.Add(model);

                    }
                    catch { }

                }
                //notifiacation
                list_notification = new List<DetechModel>();
                arr = JArray.Parse(mydata["notification"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_notification.Add(model);

                    }
                    catch { }

                }
                
             list_canceladdfriend = new List<DetechModel>();
                arr = JArray.Parse(mydata["cancelrequest"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_canceladdfriend.Add(model);

                    }
                    catch { }
                }

                list_invite2page = new List<DetechModel>();
                arr = JArray.Parse(mydata["invitefanpage"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_invite2page.Add(model);

                    }
                    catch { }

                } 
                list_menu = new List<DetechModel>();
                arr = JArray.Parse(mydata["menu"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_menu.Add(model);

                    }
                    catch { }

                } 
                list_reg = new List<DetechModel>();
                arr = JArray.Parse(mydata["reg"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_reg.Add(model);

                    }
                    catch { }

                }

                list_shareVideo = new List<DetechModel>();
                arr = JArray.Parse(mydata["sharevideo"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_shareVideo.Add(model);

                    }
                    catch { }
                }

                list_joingroup = new List<DetechModel>();
                arr = JArray.Parse(mydata["joingroup"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_joingroup.Add(model);

                    }
                    catch { }
                }

             

                list_Proxy = new List<DetechModel>();
                arr = JArray.Parse(mydata["proxy"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_Proxy.Add(model);

                    }
                    catch { }
                }

                list_AddContact = new List<DetechModel>();
                arr = JArray.Parse(mydata["addcontact"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_AddContact.Add(model);

                    }
                    catch { }
                }

                list_2fa = new List<DetechModel>();
                arr = JArray.Parse(mydata["2fa"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_2fa.Add(model);

                    }
                    catch { }
                }


                list_openMess = new List<DetechModel>();
                arr = JArray.Parse(mydata["openMess"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_openMess.Add(model);

                    }
                    catch { }
                }
                list_regld = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_regld"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_regld.Add(model);

                    }
                    catch { }
                }
                list_keyvpn = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_vnp111"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_keyvpn.Add(model);

                    }
                    catch { }
                }
                list_dump = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_dump"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        list_dump.Add(model);

                    }
                    catch { }
                }
                //share video watch
                list_sharevideowatch = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_sharevideowatch"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_sharevideowatch.Add(model);

                    }
                    catch { }

                }
                //share video watch
                list_seeding_share = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_seeding_share"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_seeding_share.Add(model);

                    }
                    catch { }

                }

                //list_checkpoint
                list_checkpoint = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_checkpoint"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_checkpoint.Add(model);

                    }
                    catch { }

                }  //list_checkpoint
                list_khangspam = new List<DetechModel>();
                arr = JArray.Parse(mydata["list_khangspam"]);
                foreach (var item in arr)
                {
                    try
                    {
                        string row = item.ToString();
                        string[] rowsplit = row.Split('|');
                        model = new DetechModel();
                        model.parent = rowsplit[0];
                        model.content = rowsplit[1];
                        model.text = rowsplit[2];
                        model.node = rowsplit[3];
                        model.function = Convert.ToInt32(rowsplit[4]);
                        list_khangspam.Add(model);

                    }
                    catch { }

                }
            }
            catch { }

            
           
        }
        public void setData()
        {
            list_phone = new List<string>();
            list_phone.Add("Phone or Email");
            list_phone.Add("Điện thoại hoặc email");
            //submit
            list_submit = new List<string>();
            list_submit.Add("ĐĂNG NHẬP");
            list_submit.Add("LOG IN");
            //skip login
            list_skip = new List<string>();
            list_skip.Add("Skip");
            list_skip.Add("Allow");
            list_skip.Add("NO THANKS");
            list_skip.Add("Next");
            list_skip.Add("OK");
            list_skip.Add("Bỏ qua");
            list_skip.Add("Cho phép");
            list_skip.Add("Tiếp tục");
            list_skip.Add("không bao giờ");
            list_skip.Add("Tiếp");
            list_skip.Add("Lúc Khác");
            list_skip.Add("NOT NOW");
            // list_skip.Add("Không");
            //list loi
            list_loi = new List<string>();
            list_loi.Add("Confirm Your Identity");
            list_loi.Add("Can't Find Account");
            list_loi.Add("Incorrect Password");
            list_loi.Add("Login Failed");
            list_loi.Add("You Entered an Older Password");
            list_loi.Add("sai mật khẩu");
            list_loi.Add("mật khẩu cũ");
            list_loi.Add("lỗi đăng nhập");
            list_loi.Add("bạn đã nhập mật khẩu cũ hơn");
            list_loi.Add("xác nhận danh tính");
            list_loi.Add("Login Code Required");
            list_loi.Add("Cần có mã đăng nhập");
            list_loi.Add("mã đăng nhập");
            list_loi.Add("Code Required");
            //check login
            list_checklogin = new List<string>();
            list_checklogin.Add("Opens camera");
            list_checklogin.Add("Messaging");
            list_checklogin.Add("What's on your mind?");
            list_checklogin.Add("Máy ảnh");
            list_checklogin.Add("trò chuyện");
            list_checklogin.Add("Bạn đang nghĩ gì?");
            list_checklogin.Add("Add to Story");
            //list_checklogin.Add("Search");
            list_checklogin.Add("Watch");
          
            //check app finish
            list_appfinish = new List<string>();
            list_appfinish.Add("Phone or Email");
           // list_appfinish.Add("Create New Facebook Account");
            list_appfinish.Add("Opens camera");
            list_appfinish.Add("Messaging");
            list_appfinish.Add("What's on your mind?");
            list_appfinish.Add("Add to Story");
            list_appfinish.Add("Search");
            list_appfinish.Add("Máy ảnh");
            list_appfinish.Add("trò chuyện");
            list_appfinish.Add("Bạn đang nghĩ gì?");
            list_appfinish.Add("Điện thoại hoặc email");
            list_addfriend = new List<string>();
            list_addfriend.Add("Add Friend");
            list_addfriend.Add("Thêm bạn bè");
            list_comment = new List<string>();
            list_comment.Add("Comment");
            list_comment.Add("Bình luận");
            write_comment = new List<string>();
            write_comment.Add("Write a comment");
            write_comment.Add("Hãy đề xuất một địa điểm");
            write_comment.Add("Viết bình luận...");


           
            //list content

            DetechModel data = new DetechModel();
        }

       
    }

  
    public class CommentContent
    {
        public List<string> contentshare_original = new List<string>();
        public List<string> contentshare_copy = new List<string>();

        public List<string> contentcomment_original = new List<string>();
        public List<string> contentcomment_copy = new List<string>();

        public void setConntent()
        {
            contentshare_original = new List<string>();
            contentshare_copy = new List<string>();
            contentcomment_original = new List<string>();
            contentcomment_copy = new List<string>();
        }


    }
}
 