using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public class Profile_Controller
    {
        public string checkCookies(string cookies)
        {
            string dtsg = null;
            try
            {
                var client = new RestClient("https://m.facebook.com/home.php");
                var request = new RestRequest(Method.GET);

                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        if (c[0].Trim() != "" && c[1].Trim() != "")
                            request.AddCookie(c[0].Trim(), c[1].Trim());
                    }
                    catch
                    { }
                }

                // Mozilla/5.0 (Linux; Android 7.1.2; SM-N976N Build/N2G48C; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.70 Mobile Safari/537.36
                //login Mozilla/5.0 (Linux; Android 7.1.2; SM-N976N Build/N2G48C; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.70 Mobile Safari/537.36
                //  client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0";
                client.UserAgent = "Mozilla/5.0 (Linux; Android 7.1.2; SM-N976N Build/N2G48C; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.70 Mobile Safari/537.36";
                var response = client.Execute(request);

                response = client.Execute(request);
                string html = response.Content;

                if (response.ResponseUri.AbsolutePath.Contains("checkpoint/block") == false)
                {
                    dtsg = smethod_6(html, html.IndexOf("fb_dtsg\" value=") + 16, "\"");
                }

            }
            catch
            { }
            return dtsg;
        }
        public string smethod_6(string string_0, int int_0, string string_1)
        {
            try
            {
                string str = "";
                for (int i = int_0; i < string_0.Length; i++)
                {
                    char ch = string_0[i];
                    if (!(ch.ToString() != string_1))
                    {
                        break;
                    }
                    str = str + string_0[i];
                }
                return str;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public bool LoadInfo2(string cookies, string dtsg, Account acc, string userAgent)
        {
            try
            {
                var client = new RestClient("https://www.facebook.com/api/graphql/");
                var request = new RestRequest(Method.POST);
                if (String.IsNullOrEmpty(userAgent))
                    client.UserAgent = "Mozilla/5.0 (Linux; Android 7.1.2; SM-N976N Build/N2G48C; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.70 Mobile Safari/537.36";// "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0";
                else
                    client.UserAgent = userAgent;
                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        if (c[0].Trim() != "" && c[1].Trim() != "")
                            request.AddCookie(c[0].Trim(), c[1].Trim());
                    }
                    catch
                    { }
                }
                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                request.AddParameter("q", "user(" + acc.id + "){friends{count},groups{count},id,name,gender,birthday,email_addresses,username}");
                request.AddParameter("fb_dtsg", dtsg);
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);
                acc.name = obj[acc.id.Trim()]["name"].ToString().Replace("'", "");

                acc.friend_count = obj[acc.id.Trim()]["friends"]["count"].ToString().Trim();
                acc.group_count = obj[acc.id.Trim()]["groups"]["count"].ToString().Trim();

                //if (obj[acc.id.Trim()]["email_addresses"].Count() > 0)
                //{
                //    acc.email = obj[acc.id]["email_addresses"][0].ToString();

                //}

                return true;

            }
            catch
            { }
            return false;

        }
        public bool LoadInfoUIDToken(string token, Account acc, string userAgent)
        {
            try
            {
                var client = new RestClient("https://graph.facebook.com/graphql/");
                var request = new RestRequest(Method.POST);
                if (String.IsNullOrEmpty(userAgent))
                    client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36";// "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0";
                else
                    client.UserAgent = userAgent;
                
                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                request.AddParameter("q", "user(" + acc.id + "){friends{count},groups{count},id,name,gender,birthday,email_addresses,username}");
                request.AddParameter("access_token", token);
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);
                acc.name = obj[acc.id.Trim()]["name"].ToString().Replace("'", "");

                acc.friend_count = obj[acc.id.Trim()]["friends"]["count"].ToString().Trim();
                acc.group_count = obj[acc.id.Trim()]["groups"]["count"].ToString().Trim();

                //if (obj[acc.id.Trim()]["email_addresses"].Count() > 0)
                //{
                //    acc.email = obj[acc.id]["email_addresses"][0].ToString();

                //}

                return true;

            }
            catch
            { }
            return false;

        }
        public bool LoadInfoToken(Account acc, string userAgent)
        {
            try
            {
                string uid = acc.id;
                if (string.IsNullOrEmpty(uid))
                {
                    uid = acc.email;
                }
                var client = new RestClient("https://graph.facebook.com/graphql");
                var request = new RestRequest(Method.POST);
                if (String.IsNullOrEmpty(userAgent))
                    client.UserAgent = "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-N976N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]";
                else
                    client.UserAgent = userAgent;

                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                //   acc.token = "EAAAAUaZA8jlABAKYkzThZBBwwhfZA1cUtCKZAADVTkdiVh4WUQrS5byWigN631OpbHnUz8JpbXCX0YvAcpIBxtoLBSK5aLOrDCoOio0ZA7GrjbdJDsZBCw3QglZAKgriL01zS7Eg1YE15a8jTO2KhliWmX4SzJLZBDKfmFY5zS9wTtesBZCwB1DgU1gCAPg7ZCjeUZD";
                // uid = "100049096267967";
                request.AddParameter("q", "me(){id,name,gender,birthday,email_addresses,username,friends{count},groups{count}}");
                request.AddHeader("Authorization", "OAuth " + acc.token.Trim());
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);

                acc.name = obj[uid]["name"].ToString();

                acc.friend_count = obj[uid]["friends"]["count"].ToString().Trim();
                acc.group_count = obj[uid]["groups"]["count"].ToString().Trim();
                if (string.IsNullOrEmpty(acc.email))
                {
                    try
                    {
                        acc.email = obj[uid]["email_addresses"][0].ToString().Trim();


                    }
                    catch
                    { }
                }
                acc.birthday = obj[uid]["birthday"].ToString();
                acc.TrangThai = "Live";
                NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
                nguoidung_bll.updateAccount(acc);
                return true;

            }
            catch (Exception ex)
            {

            }
            return false;

        }
        public bool checkLiveUID(string uid)
        {
            try
            {

                var client = new RestClient(string.Format("https://graph.facebook.com/{0}/picture?type=normal", uid));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-N976N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]";


                request.AddHeader("content-type", "application/x-www-form-urlencoded");


                IRestResponse response = client.Execute(request);
                //  string data = response.Content;

                if (response.ResponseUri.AbsoluteUri.Contains("https://scontent"))
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

            }
            return false;

        }
        public bool checkLiveToken(string token, string useragent = null)
        {
            try
            {
                var client = new RestClient("https://graph.facebook.com/me");
                var request = new RestRequest(Method.GET);
                if (string.IsNullOrEmpty(useragent))
                {
                    client.UserAgent = "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-N976N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]";

                }
                else
                    client.UserAgent = useragent;

                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                request.AddParameter("access_token", token);
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);
                if (!string.IsNullOrEmpty(obj["id"].ToString()))
                {

                    return true;
                }
            }
            catch
            { }
            return false;

        }
        public bool checkAvatar(string uid, string token)
        {
            ResultRequest kq = new ResultRequest();
            try
            {

                var client = new RestClient(String.Format("https://graph.facebook.com/{0}/picture?redirect=false&fields=is_silhouette&access_token={1}", uid, token));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0";


                request.AddHeader("content-type", "application/x-www-form-urlencoded");


                IRestResponse response = client.Execute(request);
                string data = response.Content;
                if (data.Contains("false"))
                    return true;
            }
            catch
            {
            }


            return false;
        }
        public List<GroupFB> LoadInfoGroup(string token, string dtsg, Account acc, string userAgent)
        {
            List<GroupFB> list_group = new List<GroupFB>();
            StringBuilder ls_grid = new StringBuilder();
            try
            {
                var client = new RestClient("https://graph.facebook.com/graphql");
                var request = new RestRequest(Method.POST);
                LDController ld = new LDController();
                client.UserAgent = userAgent;


                request.AddHeader("Authorization", "OAuth " + token);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                // request.AddParameter("q", "user(" + acc.id + "){friends{count},groups{count},id,name,gender,birthday,email_addresses,username}");

                request.AddParameter("q", "me(){groups{nodes{id,name,group_members{count},viewer_post_status,visibility}}}");

                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);
                string id = acc.id;
                if (string.IsNullOrEmpty(id))
                    id = acc.email;
              
                foreach (var item in obj[id]["groups"]["nodes"])
                {
                    GroupFB f = new GroupFB();
                    f.id = item["id"].ToString();
                    f.name = item["name"].ToString();
                    try
                    {
                        f.member = Convert.ToInt32(item["group_members"]["count"].ToString());
                    }
                    catch { f.member = 0; }
                    f.status = item["viewer_post_status"].ToString();
                    list_group.Add(f);
                    ls_grid.AppendLine(f.id);
                }
                
               
            }
            catch
            { }

            try
            {
                string path = Application.StartupPath + "\\logs\\" + acc.id + "_dathamgia.txt";

                File.WriteAllText(path, ls_grid.ToString());
            }
            catch
            { }
           

            return list_group;

        }
        public string checkIP()
        {
            try
            {
                var client = new RestClient("https://api.ipify.org/");
                var request = new RestRequest(Method.GET);


                IRestResponse response = client.Execute(request);
                return response.Content.Trim();

            }
            catch
            { }
            return null;

        }

        public bool LoadInfo2Mobile(string cookies, string dtsg, Account acc, string userAgent)
        {
            try
            {
                var client = new RestClient("https://www.facebook.com/api/graphql/");
                var request = new RestRequest(Method.POST);
                if (String.IsNullOrEmpty(userAgent))
                    client.UserAgent = "Mozilla/5.0 (Linux; Android 8.1.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Mobile Safari/537.36";
                else
                    client.UserAgent = userAgent;
                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        if (c[0].Trim() != "" && c[1].Trim() != "")
                            request.AddCookie(c[0].Trim(), c[1].Trim());
                    }
                    catch
                    { }
                }
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                string uid = "";
                if (string.IsNullOrEmpty(acc.id))
                    uid = acc.email;
                else
                    uid = acc.id;
                request.AddParameter("q", "user(" + uid + "){friends{count},groups{count},id,name,gender,birthday,email_addresses,username}");
                request.AddParameter("fb_dtsg", dtsg);
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);
                acc.name = obj[uid]["name"].ToString().Replace("'", "");

                acc.friend_count = obj[uid]["friends"]["count"].ToString().Trim();
                acc.group_count = obj[uid]["groups"]["count"].ToString().Trim();

                if (obj[uid]["email_addresses"].Count() > 0)
                {
                    acc.email = obj[acc.id]["email_addresses"][0].ToString();

                }
                acc.TrangThai = "Live";
                NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                nguoidung.updateAccount(acc);
                return true;

            }
            catch
            { }
            return false;

        }

        public bool getInfoToken(Account acc, string useragent)
        {
            try
            {
                var client = new RestClient("https://graph.facebook.com/me");
                var request = new RestRequest(Method.GET);
                client.UserAgent = useragent;

                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                request.AddParameter("access_token", acc.token);
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);
                if (!string.IsNullOrEmpty(obj["id"].ToString()))
                {
                    try
                    {
                        acc.name = obj["name"].ToString();
                        acc.phone = obj["mobile_phone"].ToString();
                        acc.TrangThai = "Live";
                        NguoiDung_Bll nguoidung = new NguoiDung_Bll();
                        nguoidung.updateAccount(acc);
                    }
                    catch
                    { }
                    return true;
                }
            }
            catch
            { }
            return false;

        }
        public CareFacebookResult addFriendtoken(Account acc, List<string> list_uid, string useragent, string proxy)
        {
            CareFacebookResult kq = new CareFacebookResult();
            try
            {

                var client = new RestClient("https://graph.facebook.com/graphql");//"https://graph.facebook.com/graphql"

                var request = new RestRequest(Method.POST);
                if (String.IsNullOrEmpty(useragent))
                {
                    client.UserAgent = "[FBAN/FB4A;FBAV/175.0.0.40.97;FBBV/111983749;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel;FBMF/Xiaomi;FBBD/xiaomi;FBPN/com.facebook.katana;FBDV/Redmi Note 4x;FBSV/6.0.1;FBBK/0;FBOP/1;FBCA/armeabi-v7a:armeabi;]";
                }
                else
                    client.UserAgent = useragent;
                if (string.IsNullOrEmpty(proxy))
                {
                    string[] arr = proxy.Split(':');
                    if (arr.Length == 2)
                    {
                        client.Proxy = new WebProxy(proxy);

                    }

                }

                request.AddHeader("authorization", "OAuth " + acc.token);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("doc_id", "1577255185642828");
                request.AddParameter("method", "post");
                request.AddParameter("locale", "vi_VN");
                request.AddParameter("pretty", "false");
                request.AddParameter("format", "json");
                request.AddParameter("variables", "{\"0\":{\"source\":\"quick_friending\",\"should_stage\":true,\"refs\":[\"quick_friending\"],\"friend_requestee_ids\":" + JsonConvert.SerializeObject(list_uid) + ",\"client_mutation_id\":\"" + Guid.NewGuid().ToString() + "\",\"warn_ack_for_ids\":" + JsonConvert.SerializeObject(list_uid) + ",\"stage_duration\":27,\"actor_id\":\"" + acc.id + "\"}}");
                request.AddParameter("fb_api_req_friendly_name", "FriendRequestSendCoreMutation");
                request.AddParameter("fb_api_caller_class", "graphservice");
                request.AddParameter("fb_api_analytics_tags", "[\"visitation_id=null\",\"GraphServices\"]");
                request.AddParameter("server_timestamps", "true");
                IRestResponse response = client.Execute(request);
                string html = response.Content;

                if (html.Contains("validating access token"))
                {
                    kq.status = false;
                    kq.mess = "checkpoint";
                }
                else
                {
                    JObject obj = JObject.Parse(html);
                    if (html.Contains("understood by the server"))
                    {
                        kq.status = false;
                        kq.mess = "understood by the server";
                    }
                    else
                    {
                        try
                        {
                            int count = obj["data"]["friend_request_send"]["friend_requestees"].Count();
                            if (count > 0)
                            {
                                kq.status = true;
                                kq.data = count.ToString();
                                kq.mess = "thành công";
                            }
                            else
                            {
                                kq.status = false;
                                kq.mess = "lỗi kết bạn";
                            }

                        }
                        catch
                        {
                            kq.status = false;
                            kq.mess = obj["errors"][0]["description"].ToString();
                        }

                    }
                }

            }
            catch
            {
                kq.status = false;
                kq.data = "0";
            }
            return kq;

        }

        public bool checkGroupApprove(string cookie, string groupid)
        {
            try
            {
                var client = new RestClient("https://m.facebook.com/groups/" + groupid + "/madminpanel/");
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";
                string[] arr = cookie.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        if (c[0].Trim() != "" && c[1].Trim() != "")
                            request.AddCookie(c[0].Trim(), c[1].Trim());
                    }
                    catch
                    { }
                }
                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                request.AddHeader("referer", "https://m.facebook.com/groups/" + groupid + "/madminpanel/");
                IRestResponse response = client.Execute(request);
                string html = response.Content;
                if (html.Contains("madminpanel/pending"))
                {
                    return true;
                }



            }
            catch
            {

            }
            return false;

        }
        public int countPostToday(string cookie, string groupid)
        {
            int total = 0;
            try
            {
                var client = new RestClient("https://www.facebook.com/groups/" + groupid + "/about/");
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36";
                string[] arr = cookie.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        if (c[0].Trim() != "" && c[1].Trim() != "")
                            request.AddCookie(c[0].Trim(), c[1].Trim());
                    }
                    catch
                    { }
                }
                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                request.AddHeader("referer", "https://www.facebook.com/groups/" + groupid + "/about/");
                IRestResponse response = client.Execute(request);
                string html = response.Content;

                //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                //document.LoadHtml(html);

                //HtmlNode node = document.DocumentNode.SelectSingleNode("//div[contains(@id,'pagelet_group_about')]");
                if (html.Contains("pagelet_group_about"))
                {
                    //  html = node.OuterHtml;
                    if (html.Contains("Bài viết mới hôm nay"))
                    {
                        int index = html.LastIndexOf("Bài viết mới hôm nay");
                        string input = html.Substring(index - 150, 200).Replace(",", "").Replace(".", "");
                        total = int.Parse(Regex.Match(input, "(\\d+)<i.*?Bài viết mới hôm nay").Groups[1].Value);
                    }
                    else
                    {
                        if (html.Contains("New posts today"))
                        {
                            int index = html.IndexOf("New posts today");
                            string input2 = html.Substring(index - 150, 200).Replace(",", "").Replace(".", "");
                            total = int.Parse(Regex.Match(input2, "(\\d+)<i.*?New posts today").Groups[1].Value);
                        }
                        else
                        {
                            int index = html.IndexOf("New post today");
                            string input3 = html.Substring(index - 150, 200).Replace(",", "").Replace(".", "");
                            total = int.Parse(Regex.Match(input3, "(\\d+)<i.*?New post today").Groups[1].Value);
                        }
                    }
                }


            }
            catch
            {

            }
            return total;

        }
        public string LoadInfoGroup(string uid, string token, string userAgent)
        {
            try
            {
                return "Ok"; //mr thoong sua bo qua check

                var client = new RestClient(string.Format("https://graph.facebook.com/{0}?access_token={1}", uid, token));
                var request = new RestRequest(Method.GET);
                if (String.IsNullOrEmpty(userAgent))
                    client.UserAgent = "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-N976N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]";
                else
                    client.UserAgent = userAgent;

                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                IRestResponse response = client.Execute(request);
                string data = response.Content;
                if (data.Contains("access token"))
                {
                    return "Vui lòng thêm token trung gian để check group live";
                }
                else
                {
                    if (data.Contains("does not exist"))
                    {
                        return "Group không tồn tại";
                    }
                    else
                    {
                        JObject obj = JObject.Parse(data);

                        string id = obj["id"].ToString();

                        if (string.IsNullOrEmpty(id) == false)
                        {
                            return "Ok";
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }


            return "Group lỗi";

        }
        public List<string> scanMemberGroup(string uid, string token, string gender, ref string nextpage)
        {
            List<string> list_uid = new List<string>();
            try
            {

                var client = new RestClient("https://graph.facebook.com/graphql");
                var request = new RestRequest(Method.GET);

                client.UserAgent = "[FBAN/FB4A;FBAV/251.0.0.31.111;FBBV/188827990;FBDM/{density=3.0,width=1080,height=1920};FBLC/vi_VN;FBRV/0;FBCR/Viettel Telecom;FBMF/samsung;FBBD/samsung;FBPN/com.facebook.katana;FBDV/SM-N976N;FBSV/7.1.2;FBOP/1;FBCA/x86:armeabi-v7a;]";


                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("access_token", token);
                if (string.IsNullOrEmpty(nextpage))
                {
                    request.AddParameter("q", "group(" + uid + "){group_member_profiles.first(5000){page_info{end_cursor,has_next_page},nodes{id,name,gender}}}");
                }
                else
                {
                    request.AddParameter("q", "group(" + uid + "){group_member_profiles.first(5000).after(" + nextpage + "){page_info{end_cursor,has_next_page},nodes{id,name,gender}}}");
                }

                IRestResponse response = client.Execute(request);
                string data = response.Content;

                JObject obj = JObject.Parse(data);
                foreach (var item in obj[uid]["group_member_profiles"]["nodes"])
                {
                    if (gender == "All")
                    {
                        list_uid.Add(item["id"].ToString());
                    }
                    else
                    {
                        string genderuid = item["gender"].ToString();
                        if (gender == genderuid)
                        {
                            list_uid.Add(item["id"].ToString());
                        }
                    }
                }
                bool has_next = Convert.ToBoolean(obj[uid]["group_member_profiles"]["page_info"]["has_next_page"].ToString());
                if (has_next)
                {
                    nextpage = obj[uid]["group_member_profiles"]["page_info"]["end_cursor"].ToString();
                }
                else
                {
                    nextpage = null;
                }

            }
            catch
            {

            }
            return list_uid;

        }

        public List<string> LoadFriend(string cookies, string dtsg, Account acc, string userAgent)
        {
            List<string> lsfr = new List<string>();
            try
            {

                var client = new RestClient("https://www.facebook.com/api/graphql/");
                var request = new RestRequest(Method.POST);
                if (String.IsNullOrEmpty(userAgent))
                    client.UserAgent = "Mozilla/5.0 (Linux; Android 7.1.2; SM-N976N Build/N2G48C; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/68.0.3440.70 Mobile Safari/537.36";// "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0";
                else
                    client.UserAgent = userAgent;
                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        if (c[0].Trim() != "" && c[1].Trim() != "")
                            request.AddCookie(c[0].Trim(), c[1].Trim());
                    }
                    catch
                    { }
                }
                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                request.AddParameter("q", "user(" + acc.id + "){friends{count},groups{count},id,name,gender,birthday,email_addresses,username}");
                request.AddParameter("fb_dtsg", dtsg);
                IRestResponse response = client.Execute(request);
                string data = response.Content;
                JObject obj = JObject.Parse(data);
                acc.name = obj[acc.id.Trim()]["name"].ToString().Replace("'", "");

                acc.friend_count = obj[acc.id.Trim()]["friends"]["count"].ToString().Trim();
                acc.group_count = obj[acc.id.Trim()]["groups"]["count"].ToString().Trim();

                //if (obj[acc.id.Trim()]["email_addresses"].Count() > 0)
                //{
                //    acc.email = obj[acc.id]["email_addresses"][0].ToString();

                //}

                return lsfr;

            }
            catch
            { }
            return lsfr;

        }

    }
}
