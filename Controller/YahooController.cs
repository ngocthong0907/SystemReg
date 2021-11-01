using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using NinjaSystem.Controller;
using System.IO;
using System.Windows.Forms;
using System.Net;
namespace NinjaSystem
{
    public class YahooModel
    {
        public string id { set; get; }
        public string email { set; get; }
        public bool status { set; get; }
        public string ssid { set; get; }
    }
    public class RentCode
    {
        public bool status { set; get; }
        public int id { set; get; }
        public string phone { set; get; }
        public int balance { set; get; }
    }
    public class ThueCode
    {
        public bool status { set; get; }
        public string id { set; get; }
        public string phone { set; get; }
        public int balance { set; get; }
    }
    public class RaiSim
    {
        public bool status { set; get; }
        public string sessionid { set; get; }
        public string orderDetailId { set; get; }
        public string phone { set; get; }
        public int balance { set; get; }
    }
    public class YahooController
    {
        #region yahoo
        public YahooModel loginYahoo(string cookies, string youremail)
        {
            YahooModel model = new YahooModel();
            try
            {
                var client = new RestClient("https://mail.yahoo.com/d/settings/1");
                var request = new RestRequest(Method.GET);

                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        string value =str.Replace(c[0]+"=","");
                         
                            request.AddCookie(c[0].Trim(), value);
                    }
                    catch
                    { }
                }
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
               
                IRestResponse response = client.Execute(request);
                string html = response.Content;

                string mailWssid =FunctionHelper.smethod_6(html, html.IndexOf("mailWssid\":\"") + 12, "\"");



                mailWssid = Regex.Unescape(mailWssid);

                 
                //login
                string url = "https://mail.yahoo.com/ws/v3/batch?name=launch.skeleton&hash=3b5e3610&appId=YMailNorrin&wssid=" + mailWssid;//ymreqid=0192bc14-6b91-258a-1c75-2d0000017900
                client = new RestClient(url);
                request = new RestRequest(Method.POST);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
                request.AddHeader("origin", "https://mail.yahoo.com");
                request.AddHeader("referer", "https://mail.yahoo.com/");
               
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        string value = str.Replace(c[0] + "=", "");

                        request.AddCookie(c[0].Trim(), value);
                    }
                    catch
                    { }
                }

                request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
                request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"batchJson\"\r\n\r\n{\"requests\":[{\"id\":\"GetMailboxes\",\"uri\":\"/ws/v3/mailboxes\",\"method\":\"GET\",\"payloadType\":\"embedded\",\"filters\":{\"select\":{\"mailboxId\":\"$..mailboxes[?(@.isSelected==true)].id\",\"rootMailboxId\":\"$..mailboxes[?(@.isSelected==true)].id\"}},\"requests\":[{\"id\":\"GetAccounts\",\"uri\":\"/ws/v3/mailboxes/@.id==$(mailboxId)/accounts\",\"method\":\"GET\"}]}],\"responseType\":\"json\"}\r\n-----011000010111000001101001--\r\n", ParameterType.RequestBody);
                response = client.Execute(request);

               
                html = response.Content;

                JObject obj = JObject.Parse(html);
                var item = obj["result"]["responses"][0]["response"]["result"]["mailboxes"][0];
                string email = item["email"].ToString();
                model.id = item["id"].ToString();

                var itemmacc = obj["result"]["responses"][1]["response"]["result"]["accounts"];


                if (itemmacc.Count() > 1)
                {
                    url = "https://mail.yahoo.com/ws/v3/batch?name=settings.deleteAccount&hash=3b5e3610&appId=YMailNorrin&wssid=" + mailWssid;

                    string deleteemail = itemmacc[1]["link"]["href"].ToString();

                    DeleteEmail(url, cookies, model.id, deleteemail);

                }
                url = "https://mail.yahoo.com/ws/v3/batch?name=settings.createAlias&hash=3b5e3610&appId=YMailNorrin&wssid=" + mailWssid;
                Random rd = new Random();

                if (CreateEmail(url, cookies, model.id, youremail))
                {
                    model.email = youremail;
                    model.status = true;
                    model.ssid = mailWssid;
                    return model;
                }
                else
                {
                    model.email = youremail;
                    model.status = false;
                    return model;
                }
                   
               
            }
            catch
            {
                model.status = false;
            
            }
            return model;

        }
        public string GetCode(string cookies,string id,string email,string ssid)
        {
            string code = null;
            try
            {
                string url = String.Format("https://mail.yahoo.com/ws/v3/mailboxes/@.id=={0}/messages/@.select==q?q=folderId:1 groupBy:conversationId count:1 offset:0 -folderType:(SYNC)-folderType:(INVISIBLE) -sort:date&hash=3418a7f6&appId=YMailNorrin&wssid={1}", id,ssid) ;
               int i=5;
               while(i>0)
               {
                   code = DocEmail(url, cookies, email);
                   if(code!=null)
                   {
                       break;
                   }
                   else
                   {
                       Thread.Sleep(2000);
                       i--;
                   }
               }
            }
            catch
            { }
            return code;
        }
        public bool CreateEmail(string url,string cookies,string id ,string email)
        {
            try
            {
               var client = new RestClient(url);
                var request = new RestRequest(Method.POST);

                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        string value = str.Replace(c[0] + "=", "");

                        request.AddCookie(c[0].Trim(), value);
                    }
                    catch
                    { }
                }
                 

                //login 
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
                request.AddHeader("origin", "https://mail.yahoo.com");
                request.AddHeader("referer", "https://mail.yahoo.com/");

                request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
                request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"batchJson\"\r\n\r\n{\"requests\":[{\"id\":\"AddAccount\",\"uri\":\"/ws/v3/mailboxes/@.id==" + id + "/accounts/\",\"method\":\"POST\",\"payload\":{\"account\":{\"email\":\"" + email + "\",\"type\":\"ALIAS\",\"description\":\"Extra email address account\"}},\"onFailure\":{\"id\":\"IdSuggestions\",\"uri\":\"/ws/comms/deasuggestion/api/v1/users/services/idSuggestions?yid=" + email + "\",\"method\":\"GET\",\"suppressResponse\":false},\"suppressResponse\":false}],\"responseType\":\"json\"}\r\n-----011000010111000001101001--\r\n", ParameterType.RequestBody);
                var response = client.Execute(request);

                response = client.Execute(request);
               string html = response.Content;

               if (html.Contains("successRequests"))
                   return true;
                
                //tao email bi danh


            }
            catch
            { }
            return false;

        }
        public bool DeleteEmail(string url, string cookies, string id, string email)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);

                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        string value = str.Replace(c[0] + "=", "");

                        request.AddCookie(c[0].Trim(), value);
                    }
                    catch
                    { }
                }


                //login 
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
                request.AddHeader("origin", "https://mail.yahoo.com");
                request.AddHeader("referer", "https://mail.yahoo.com/");

                request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
                request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"batchJson\"\r\n\r\n{\"requests\":[{\"id\":\"deleteAccount\",\"uri\":\"" + email + "\",\"method\":\"DELETE\"}],\"responseType\":\"json\"}\r\n-----011000010111000001101001--\r\n", ParameterType.RequestBody);

                var response = client.Execute(request);

                response = client.Execute(request);
                string html = response.Content;

                if (html.Contains("successRequests"))
                    return true;

                //tao email bi danh


            }
            catch
            { }
            return false;

        }

        public string DocEmail(string url, string cookies,string email)
        {
            string code = null;
            try
            {
            
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                string[] arr = cookies.Split(';');
                foreach (string str in arr)
                {
                    try
                    {
                        string[] c = str.Split('=');
                        string value = str.Replace(c[0] + "=", "");

                        request.AddCookie(c[0].Trim(), value);
                    }
                    catch
                    { }
                }


                //login 
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
                request.AddHeader("origin", "https://mail.yahoo.com");
                request.AddHeader("referer", "https://mail.yahoo.com/");
                var response = client.Execute(request);

                response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                var item = obj["result"]["messages"][0]["headers"];

                string title = item["subject"].ToString();
                //string emailto = item["to"][0]["email"].ToString();
                //if(emailto==email)
                {
                    code = Regex.Match(title, @"[0-9]{5}").Value;

                } 
            }
            catch
            { }
            return code;

        }
        #endregion
        #region phone
        public RaiSim GetPhoneApi(RegnickModel reg, string api, bool reset = false)
        {
            RaiSim rs = new RaiSim();
            try
            {
                int i = 60;
                while (i > 0)
                {
                    switch (reg.typeapi)
                    {
                        case 1:
                            rs = getPhoneThueCode(api);
                            break;
                        case 2:
                            rs = getPhoneRaiSim(api, reg.serverraisim.ToString());
                            break;
                        case 3:
                            rs = getPhoneSimThue(api);
                            break;
                        case 4:
                            rs = getPhoneOtp(api);
                            break;
                        case 5:
                            rs = getPhoneRentCode(api);
                            break;
                        case 6:
                            rs = getPhoneOtpsim(api, reg.serverraisim.ToString());
                            break;
                        case 7:
                            rs = getPhoneSimMart(api);
                            break;
                    }
                    if (rs.status)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        i--;
                    }
                }
            }
            catch
            { }
            return rs;
        }
        #endregion

        public string GetCodePhone(string api, string requestid, int type, bool reset = false)
        {
            string code = null;
            try
            {
                int i = 60;
                while (i > 0)
                {
                    switch (type)
                    {
                        case 1:
                            code = getCodeThueCode(api, requestid);
                            break;
                        case 2:
                            code = getCodeRaiSim(api, requestid, reset);
                            break;
                        case 3:
                            code = getCode(api, requestid);
                            break;
                        case 4:
                            code = getCodeOtp(api, requestid);
                            break;
                        case 5:
                            code = getCodeRentCode(api, requestid);
                            break;
                        case 6:
                            code = getCodeOtpSim(api, requestid);
                            break;
                        case 7:
                            code = getCodeSimSmart(api, requestid, reset);
                            break;
                        
                    } 

                    if (string.IsNullOrEmpty(code) == false)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        i--;
                    }
                }
            }
            catch
            { }
            return code;
        }
        public RaiSim getPhoneSimThue(string api)
        {
            RaiSim rs = new RaiSim();
            try
            {
                string requestid = getRequestID(api);
                if (requestid != null)
                {
                    rs.sessionid = requestid;
                    string phone = startGetPhoneSimThue(api, requestid);
                    if (string.IsNullOrEmpty(phone) == false)
                    {
                        rs.phone = phone;
                        rs.status = true;
                    }
                    else
                    {
                        rs.status = false;
                    }

                }
            }
            catch
            { }
            return rs;
        }
        public string getRequestID(string api)
        {
            try
            {
                var client = new RestClient(string.Format("http://api.pvaonline.net/request/create?key={0}&service_id=9", api));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["success"].ToString());
                if (status)
                {
                    string requestid = obj["id"].ToString();

                    return requestid;
                }
            }
            catch
            { }
            return null;
        }
        public string startGetPhoneSimThue(string api, string requestid)
        {
            string phone = null;
            try
            {
                int i = 60;
                while (i > 0)
                {
                    phone = getPhone(api, requestid);
                    if (string.IsNullOrEmpty(phone) == false)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        i--;
                    }
                }
            }
            catch
            { }
            return phone;
        }
        #region sim thue
        //get phone sim thue
        public string getPhone(string api, string requestid)
        {
            string phone = null;
            try
            {
                var client = new RestClient(string.Format("http://api.pvaonline.net/request/check?key={0}&id={1}", api, requestid));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                var response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["success"].ToString());
                if (status)
                {
                    phone = obj["number"].ToString();
                }
            }
            catch
            { }
            return phone;
        }
        public string getCode(string api, string requestid, bool reset = false)
        {
            string phone = null;
            try
            {

                var client = new RestClient(string.Format("http://api.pvaonline.net/request/check?key={0}&id={1}", api, requestid));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                var response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["success"].ToString());
                if (status)
                {
                    phone = obj["sms"].ToString();
                    JArray arr = JArray.Parse(phone);
                    foreach (var item in arr)
                    {
                        string data = item.ToString();
                        if (data.ToLower().Contains("facebook"))
                        {
                            string[] arrdata = data.Split('|');
                            if (reset)
                            {
                                string code = Regex.Match(arrdata[2], @"[0-9]{6}").Value;
                                return code;
                            }
                            else
                            {
                                string code = Regex.Match(arrdata[2], @"[0-9]{5}").Value;
                                return code;
                            }
                        }
                    }

                }
            }
            catch
            { }
            return null;
        }
        #endregion
        public RaiSim getBlance(string api)
        {
            RaiSim rs = new RaiSim();
            try
            {

                var client = new RestClient(string.Format("http://raisim.net/public/api/balance?api_token={0}", api));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                rs.status = Convert.ToBoolean(obj["success"].ToString());
                if (rs.status)
                {
                    rs.balance = Convert.ToInt32(obj["data"]["balance"].ToString());


                }
                else
                {
                    rs.status = false;
                }

            }
            catch
            { }
            return rs;
        }
        public List<string> getGroupRaiSim(string api)
        {
            List<string> list_server = new List<string>();
            try
            {

                var client = new RestClient(string.Format("http://raisim.net/public/api/groups?api_token={0}", api));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["success"].ToString());
                if (status)
                {
                    foreach (var item in obj["data"])
                    {
                        list_server.Add(item["id"].ToString());
                    }



                }
                else
                {

                }

            }
            catch
            { }
            return list_server;
        }
        public RaiSim getPhoneRaiSim(string api, string groupid)
        {
            RaiSim rs = new RaiSim();
            try
            {
                int dem = 0;
            Lb_Start:
                var client = new RestClient(string.Format("http://raisim.net/public/api/request?api_token={0}&id_group={1}&id_app=4", api, groupid));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                rs.status = Convert.ToBoolean(obj["success"].ToString());
                if (rs.status)
                {
                    rs.sessionid = obj["data"]["id_session"].ToString();
                    rs.phone = obj["data"]["phone"].ToString();

                }
                else
                {
                    rs.status = false;
                    dem++;
                    if (dem <= 60)
                    {
                        Thread.Sleep(2000);
                        goto Lb_Start;
                    }
                }

            }
            catch
            { }
            return rs;
        }
        public string getCodeRaiSim(string api, string requestid, bool reset = false)
        {
            try
            {
                var client = new RestClient(string.Format("http://raisim.net/public/api/getcode?api_token={0}&session={1}", api, requestid));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                var response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["success"].ToString());
                if (status)
                {

                    string data = obj["data"]["content"].ToString();
                    if (reset)
                    {
                        return Regex.Match(data, @"[0-9]{6}").Value;
                    }
                    else
                    {

                        string code = Regex.Match(data, @"[0-9]{5}").Value;
                        return code;
                    }

                }
            }
            catch
            { }
            return null;
        }
        public RaiSim getPhoneOtp(string api)
        {
            RaiSim rs = new RaiSim();
            rs.status = false;
            try
            {
                int dem = 0;
            Lb_Start:
                var client = new RestClient(string.Format("http://api.otpvn.com/?Accesskey={0}&Method=GetNumber&App=Facebook", api));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                if (html.Contains("Error"))
                {
                    dem++;
                    if (dem < 60)
                    {
                        Thread.Sleep(2000);
                        goto Lb_Start;

                    }

                }
                else
                {


                    if (string.IsNullOrEmpty(html) == false)
                    {
                        rs.status = true;
                        rs.phone = html.Trim();
                        return rs;
                    }
                    else
                    {

                        dem++;
                        if (dem < 20)
                        {
                            Thread.Sleep(2000);
                            goto Lb_Start;

                        }
                    }
                }

            }
            catch
            { }
            return rs;
        }
        public string getCodeOtp(string api, string requestid, bool reset = false)
        {
            try
            {
                var client = new RestClient(string.Format("http://api.otpvn.com/?Accesskey={0}&Method=ResponseKey&App=Facebook&Numberphone={1}", api, requestid));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                var response = client.Execute(request);
                string html = response.Content;
                if (reset)
                {
                    string code = Regex.Match(html, @"[0-9]{6}").Value;
                    return code;
                }
                else
                {
                    string code = Regex.Match(html, @"[0-9]{5}").Value;
                    return code;
                }
            }
            catch
            { }
            return null;
        }
        #region rentcode
        public RentCode getBlanceRentCode(string api)
        {
            RentCode rs = new RentCode();
            try
            {

                var client = new RestClient(string.Format("https://api.rentcode.net/api/ig/balance?apiKey={0}", api));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                rs.status = Convert.ToBoolean(obj["success"].ToString());
                if (rs.status)
                {
                    rs.balance = Convert.ToInt32(obj["results"]["balance"].ToString());


                }
                else
                {
                    rs.status = false;
                }

            }
            catch
            { }
            return rs;
        }
        public RaiSim getPhoneRentCode(string api)
        {
            RaiSim rc = new RaiSim();
            try
            {
                int dem = 0;

                var client = new RestClient(string.Format("https://api.rentcode.net/api/ig/create-request?apiKey={0}", api.Replace("=", "%3D")));
                var request = new RestRequest(Method.POST);
                //  request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\n  \"serviceProviderId\": 3,\n  \"networkProvider\": null\n}", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                rc.status = Convert.ToBoolean(obj["success"].ToString());
                if (rc.status)
                {
                    rc.sessionid = obj["results"]["id"].ToString();
                Lb_Start:
                    client = new RestClient(string.Format("https://api.rentcode.net/api/ig/orders/{0}/check-status?apiKey={1}", rc.sessionid, api.Replace("=", "%3D")));
                    request = new RestRequest(Method.GET);
                    response = client.Execute(request);
                    html = response.Content;
                    obj = JObject.Parse(html);
                    rc.status = Convert.ToBoolean(obj["success"].ToString());
                    if (rc.status)
                    {
                        rc.phone = obj["results"]["phoneNumber"].ToString();
                        if (string.IsNullOrEmpty(rc.phone))
                        {
                            dem++;
                            if (dem < 60)
                            {
                                Thread.Sleep(2000);
                                goto Lb_Start;
                            }
                        }

                    }

                }
                else
                {

                    rc.status = false;
                }

            }
            catch
            { }
            return rc;
        }
        public string getCodeRentCode(string api, string requestid, bool reset = false)
        {
            try
            {
                var client = new RestClient(string.Format("https://api.rentcode.net/api/ig/orders/{0}/results?apiKey={1}", requestid, api.Replace("=", "%3D")));
                var request = new RestRequest(Method.POST);
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"pageIndex\": 0,\"pageSize\": 0,\"sortColumnName\": \"string\",\"isAsc\": true,\"searchObject\": {\"additionalProp1\": {},\"additionalProp2\": {},\"additionalProp3\": {}}\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                int total = Convert.ToInt32(obj["total"].ToString());
                if (total > 0)
                {
                    JArray jarr = JArray.Parse(obj["results"].ToString());
                    foreach (var item in jarr)
                    {
                        if (item["sender"].ToString().ToLower().Contains("facebook"))
                        {
                            if (reset)
                            {
                                string code = Regex.Match(item["message"].ToString(), @"[0-9]{6}").Value;
                                if (code.Length >= 5)
                                    return code;
                            }
                            else
                            {
                                string code = Regex.Match(item["message"].ToString(), @"[0-9]{5}").Value;
                                if (code.Length >= 5)
                                    return code;
                            }

                        }
                    }
                }
            }
            catch
            { }
            return null;
        }
        #endregion

        #region smscodes.vn
        public RentCode getPhonesmscodes(string api)
        {
            RentCode rc = new RentCode();
            try
            {
                int dem = 0;

                var client = new RestClient(string.Format("https://smscodes.vn/api/service/phone?api_token={0}&service_code=SERVICE_CODE", api.Replace("=", "%3D")));
                var request = new RestRequest(Method.POST);
                //  request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\n  \"serviceProviderId\": 3,\n  \"networkProvider\": null\n}", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                rc.status = Convert.ToBoolean(obj["success"].ToString());
                if (rc.status)
                {
                    rc.id = Convert.ToInt32(obj["results"]["id"].ToString());
                Lb_Start:
                    client = new RestClient(string.Format("https://api.rentcode.net/api/ig/orders/{0}/check-status?apiKey={1}", rc.id, api.Replace("=", "%3D")));
                    request = new RestRequest(Method.GET);
                    response = client.Execute(request);
                    html = response.Content;
                    obj = JObject.Parse(html);
                    rc.status = Convert.ToBoolean(obj["success"].ToString());
                    if (rc.status)
                    {
                        rc.phone = obj["results"]["phoneNumber"].ToString();
                        if (string.IsNullOrEmpty(rc.phone))
                        {
                            dem++;
                            if (dem < 60)
                            {
                                Thread.Sleep(2000);
                                goto Lb_Start;
                            }
                        }

                    }

                }
                else
                {

                    rc.status = false;
                }

            }
            catch
            { }
            return rc;
        }
        #endregion
        #region otpsim
        public RaiSim getBlanceOtpSim(string api)
        {
            RaiSim rs = new RaiSim();
            try
            {

                var client = new RestClient(string.Format("http://otpsim.com/public/api/balance?api_token={0}", api));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                rs.status = Convert.ToBoolean(obj["success"].ToString());
                if (rs.status)
                {
                    rs.balance = Convert.ToInt32(obj["data"]["balance"].ToString());


                }
                else
                {
                    rs.status = false;
                }

            }
            catch
            { }
            return rs;
        }
        public List<string> getGroupOtpsim(string api)
        {
            List<string> list_server = new List<string>();
            try
            {

                var client = new RestClient(string.Format("http://otpsim.com/public/api/groups?api_token={0}", api));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["success"].ToString());
                if (status)
                {
                    foreach (var item in obj["data"])
                    {
                        list_server.Add(item["id"].ToString());
                    }



                }
                else
                {

                }

            }
            catch
            { }
            return list_server;
        }
        public RaiSim getPhoneOtpsim(string api, string groupid)
        {
            RaiSim rs = new RaiSim();
            try
            {
                int dem = 0;
            Lb_Start:
                var client = new RestClient(string.Format("http://otpsim.com/public/api/request?api_token={0}&id_group={1}&id_app=4", api, groupid));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                rs.status = Convert.ToBoolean(obj["success"].ToString());
                if (rs.status)
                {
                    rs.sessionid = obj["data"]["id_session"].ToString();
                    rs.phone = obj["data"]["phone"].ToString();

                }
                else
                {
                    rs.status = false;
                    dem++;
                    if (dem <= 60)
                    {
                        Thread.Sleep(2000);
                        goto Lb_Start;
                    }
                }

            }
            catch
            { }
            return rs;
        }
        public string getCodeOtpSim(string api, string requestid, bool reset = false)
        {
            try
            {
                var client = new RestClient(string.Format("http://otpsim.com/public/api/getcode?api_token={0}&session={1}", api, requestid));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                var response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["success"].ToString());
                if (status)
                {
                    string data = obj["data"]["content"].ToString();

                    if (reset)
                    {
                        string code = Regex.Match(data, @"[0-9]{6}").Value;
                        if (code.Length >= 5)
                            return code;
                    }
                    else
                    {
                        string code = Regex.Match(data, @"[0-9]{5}").Value;
                        if (code.Length >= 5)
                            return code;
                    }


                }
            }
            catch
            { }
            return null;
        }
        #endregion

        #region thuecode
        public ThueCode getBlanceThueCode(string api)
        {
            ThueCode rs = new ThueCode();
            try
            {

                var client = new RestClient(string.Format("http://thuecode.vn:1337/balance?token={0}", api));
                var request = new RestRequest(Method.GET);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                rs.status = Convert.ToBoolean(obj["success"].ToString());
                if (rs.status)
                {
                    rs.balance = Convert.ToInt32(obj["balance"].ToString());


                }
                else
                {
                    rs.status = false;
                }

            }
            catch
            { }
            return rs;
        }
        public RaiSim getPhoneThueCode(string api)
        {
            RaiSim rc = new RaiSim();
            try
            {
                int dem = 0;

                var client = new RestClient(string.Format("http://thuecode.vn:1337/request/create?token={0}&service_id=23", api));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                rc.status = Convert.ToBoolean(obj["success"].ToString());
                if (rc.status)
                {
                    rc.sessionid = obj["id"].ToString();
                Lb_Start:
                    client = new RestClient(string.Format("http://thuecode.vn:1337/request/check?token={0}&request_id={1}&index=0", api, rc.sessionid));
                    request = new RestRequest(Method.GET);
                    response = client.Execute(request);
                    html = response.Content;
                    obj = JObject.Parse(html);
                    rc.status = Convert.ToBoolean(obj["success"].ToString());
                    if (rc.status)
                    {
                        rc.phone = obj["number"].ToString();
                        if (string.IsNullOrEmpty(rc.phone) || rc.phone.Contains("find"))
                        {
                            dem++;
                            if (dem < 60)
                            {
                                Thread.Sleep(2000);
                                goto Lb_Start;
                            }
                        }

                    }
                    else
                    {
                        dem++;
                        if (dem < 30)
                        {
                            Thread.Sleep(2000);
                            goto Lb_Start;
                        }
                    }

                }
                else
                {

                    rc.status = false;
                }

            }
            catch
            { }
            return rc;
        }
        public string getCodeThueCode(string api, string requestid, bool reset = false)
        {
            try
            {
                var client = new RestClient(string.Format("http://thuecode.vn:1337/request/check?token={0}&request_id={1}&index=0", api, requestid));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                var response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["success"].ToString());
                if (status)
                {
                    string data = obj["sms"].ToString();

                    if (reset)
                    {
                        string code = Regex.Match(data, @"[0-9]{6}").Value;
                        if (code.Length >= 5)
                            return code;
                    }
                    else
                    {
                        string code = Regex.Match(data, @"[0-9]{5}").Value;
                        if (code.Length >= 5)
                            return code;
                    }

                }
            }
            catch
            { }
            return null;
        }
        #endregion

        #region simmart
        public RaiSim getPhoneSimMart(string api)
        {
            RaiSim rs = new RaiSim();
            try
            {
                int dem = 0;
            Lb_Start:
                var client = new RestClient("https://simmart.net/api/ServiceApi/BuySIMService");
                var request = new RestRequest(Method.POST);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                request.AddHeader("content-type", "application/json");
                request.AddHeader("token", api);
                request.AddParameter("application/json", "{\"ServiceId\": \"b2f206b7-2b09-4ac7-b733-08d7647df6f1\",\"Number\": 1,\"SecretKey\": \"" + api + "\"}", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                string html = response.Content;

                JObject obj = JObject.Parse(html);
                rs.status = Convert.ToBoolean(obj["isSuccess"].ToString());
                if (rs.status)
                {
                    client = new RestClient("https://simmart.net/api/ServiceApi/GetOrderBeingUsed");
                    request = new RestRequest(Method.POST);

                    client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("token", api);
                    request.AddParameter("application/json", "{\"pageSize\": 10,\"currentPage\": 1,\"IsOldRecord\" : 1,\"IsUsingAPI\" : 1,\"SecretKey\": \"" + api + "\"}", ParameterType.RequestBody);

                    response = client.Execute(request);
                    html = response.Content;
                    obj = JObject.Parse(html);
                    int total = Convert.ToInt32(obj["totalRecord"].ToString());
                    if (total > 0)
                    {
                        rs.status = true;
                        rs.phone = obj["data"][0]["phoneNumber"].ToString();
                        rs.sessionid = obj["data"][0]["orderId"].ToString();
                        rs.orderDetailId = obj["data"][0]["orderDetailId"].ToString();
                    }

                }
                else
                {
                    rs.status = false;
                    dem++;
                    if (dem <= 60)
                    {
                        Thread.Sleep(2000);
                        goto Lb_Start;
                    }
                }

            }
            catch
            { }
            return rs;
        }
        public string getCodeSimSmart(string api, string requestid, bool reset = false)
        {
            try
            {
                var client = new RestClient("https://simmart.net/api/ServiceApi/GetOrderBeingUsed");
                var request = new RestRequest(Method.POST);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                request.AddHeader("content-type", "application/json");
                request.AddHeader("token", api);
                request.AddParameter("application/json", "{\"pageSize\": 10,\"currentPage\": 1,\"IsOldRecord\" : 1,\"IsUsingAPI\" : 1,\"SecretKey\": \"" + api + "\"}", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                int total = Convert.ToInt32(obj["totalRecord"].ToString());
                if (total > 0)
                {
                    foreach (var item in obj["data"])
                    {
                        if (item["orderId"].ToString() == requestid)
                        {
                            foreach (var itemsms in item["smsValues"])
                            {
                                string nameservice = itemsms["nameService"].ToString().ToLower();
                                if (nameservice == "facebook")
                                {
                                    string data = itemsms["smsValue"].ToString();
                                    if (reset)
                                    {
                                        string code = Regex.Match(data, @"[0-9]{6}").Value;
                                        return code;
                                    }
                                    else
                                    {
                                        string code = Regex.Match(data, @"[0-9]{5}").Value;
                                        return code;
                                    }

                                }
                            }

                        }
                    }


                }
            }
            catch
            { }
            return null;
        }
        public bool baoSimLoi(string api, string requestid)
        {
            try
            {
                var client = new RestClient("https://simmart.net/api/ServiceApi/ReportSIMError");
                var request = new RestRequest(Method.POST);

                client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";

                request.AddHeader("content-type", "application/json");
                request.AddHeader("token", api);
                request.AddParameter("application/json", "{\"OrderDetailId\": \"" + requestid + "\",\"SecretKey\": \"" + api + "\"}", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                bool status = Convert.ToBoolean(obj["isSuccess"].ToString());
                return status;
            }
            catch
            { }
            return false;
        }
        #endregion

        #region add email
        public string getDomain()
        {
            try
            {

                var client = new RestClient("https://api4.temp-mail.org/request/domains/format/json");
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36";

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JArray jarr = JArray.Parse(html);
                List<string> list_domain = new List<string>();
                foreach (var item in jarr)
                {
                    list_domain.Add(item.ToString());
                }
                Random rd = new Random();
                return list_domain[rd.Next(0, list_domain.Count)];
            }
            catch
            {

            }
            return null;
        }
        #endregion
        public List<TinsoftLocation> GetLocationTinsoft()
        {
            List<TinsoftLocation> list_kq = new List<TinsoftLocation>();
            try
            {

                var client = new RestClient("http://proxy.tinsoftsv.com/api/getLocations.php");
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;";

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                bool success = bool.Parse(obj["success"].ToString());
                if (success)
                {
                    foreach(var item in obj["data"])
                    {
                        TinsoftLocation tinsoft = new TinsoftLocation();
                        tinsoft.id = item["location"].ToString();
                        tinsoft.name = item["name"].ToString();
                        list_kq.Add(tinsoft);
                    }
                   
                }
                else
                {
                    
                }
            }
            catch
            {
                
            }
            return list_kq;
        }
        public TinSoftModel changeProxyTinSoft(string api,string location)
        
       {
            TinSoftModel kq = new TinSoftModel();
            string path = Application.StartupPath + "\\logtinsoft.txt";
            try
            {

                var client = new RestClient(String.Format("http://proxy.tinsoftsv.com/api/changeProxy.php?key={0}&location={1}", api,location));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;";

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                kq.success = bool.Parse(obj["success"].ToString());
                if (kq.success)
                {
                    kq.proxy = obj["proxy"].ToString();
                    kq.next_change = obj["next_change"].ToString();
                    kq.timeout = Convert.ToInt32(obj["timeout"].ToString());
                }
                else
                {
                    kq.description = obj["description"].ToString();
                    kq.next_change = obj["next_change"].ToString();
                }
                File.AppendAllText(path, DateTime.Now.ToString() + " "+ api + " " + obj["description"].ToString() + "\n");
            }
            catch(Exception ex)
            {  
                File.AppendAllText(path, DateTime.Now.ToString() + " " + ex.ToString() + "\n");
                kq.success = false;
                kq.description = "";
            }
            return kq;
        }
        public TinSoftModel getProxyTinsoftStatus(string api,string location)
        {
            TinSoftModel kq = new TinSoftModel();
            try
            {

                var client = new RestClient(String.Format("http://proxy.tinsoftsv.com/api/getProxy.php?key={0}&location={1}", api,location));
                var request = new RestRequest(Method.GET);
                client.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;";

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                JObject obj = JObject.Parse(html);
                kq.success = bool.Parse(obj["success"].ToString());
                if (kq.success)
                {
                    kq.proxy = obj["proxy"].ToString();
                    kq.next_change = obj["next_change"].ToString();
                    kq.timeout = Convert.ToInt32(obj["timeout"].ToString());

                }
                else
                {
                    kq.description = obj["description"].ToString();
                    string path = Application.StartupPath + "\\logtinsoft.txt";
                    File.AppendAllText(path, DateTime.Now.ToString() + " " + kq.description + "\n");
                }
            }
            catch(Exception ex)
            {
                string path=Application.StartupPath+"\\logtinsoft.txt";
                File.AppendAllText(path, DateTime.Now.ToString() + " " + ex.ToString() + "\n");
                kq.success = false;
                kq.description = "";
            }
            return kq;
        }
        public bool changeXproxy(string link, string ip)
        {

            try
            {
                var client = new RestClient(string.Format("{0}/reset?proxy={1}", link, ip));
                var request = new RestRequest(Method.GET);

                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                IRestResponse response = client.Execute(request);
                string html = response.Content.Trim();
                if (html.Contains("true"))
                {
                    return true;
                }

            }
            catch
            {
            }
            return false;
        }
        public int currentTime()
        {
            int time = 0;
            try
            {
                var client = new RestClient("https://tmproxy.com/api/proxy/current-time");
                var request = new RestRequest(Method.GET);

                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                IRestResponse response = client.Execute(request);
                string html = response.Content.Trim();
                time = Convert.ToInt32(html);

            }
            catch
            {

            }
            return time;
        }
        public string createSigTmproxy(string api_key)
        {
            int random_code = 3068;
            string secret_key = "b30bc6b4a33323c90e0d840b9a0b8beb";
            int time=currentTime();
            string sig =FunctionHelper.md5(secret_key+api_key+(time/60+random_code));
            return sig;
        }
        public TMproxyModel changeTMproxy(string api, string location)
        {
            TMproxyModel kq = new TMproxyModel();
            kq.proxy = "";
            string path = Application.StartupPath + "\\logTMproxy.txt";
            try
            {
                string api_key = api;
                string sig = createSigTmproxy(api_key);
                string data = "{\"api_key\": \"" + api_key + "\",\"sign\":\""+sig+"\"}";
                var client = new System.Net.WebClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string html = client.UploadString("https://tmproxy.com/api/proxy/get-new-proxy", data);
               // string path = Application.StartupPath + "\\logTMproxy.txt";
               // File.AppendAllText(path, DateTime.Now.ToString() + " " + html.ToString() + "\n");
                JObject obj = JObject.Parse(html);
                kq.proxy = obj["data"]["https"].ToString();
                kq.message = obj["message"].ToString();
                kq.code = obj["code"].ToString();
                kq.next_request = (int)obj["data"]["next_request"];
                kq.timeout = (int)obj["data"]["timeout"];

                File.AppendAllText(path, DateTime.Now.ToString() + ": " + api + " " + kq.message + "\n");
            }
            catch (Exception ex)
            {
                File.AppendAllText(path, DateTime.Now.ToString() + " " + ex.ToString() + "\n");

            }
            return kq;
        }
    }


}
