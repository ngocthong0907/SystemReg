using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace NinjaSystem
{
    public class xProxyController
    {
        public xProxyController()
        {
            reset = "One";
        }
        public string reset { set; get; }
        public bool resetNinjaProxy(string proxy)
        {
            bool status = false;
            int delay = SettingTool.configld.delaydcomxproxy;
            try
            {

                try
                {
                    TcpClient client = new TcpClient("127.0.0.1", 8888);
                    StreamReader reader = new StreamReader(client.GetStream());
                    StreamWriter write = new StreamWriter(client.GetStream());
                    string data = "{\"proxy\": \"$A\",\"name\": \"Ninja" + reset + "\",\"privatekey\":\"qytuoyetkz\",}";

                    while (!data.Equals("Exit"))
                    {
                        data = data.Replace("$A", proxy);
                        write.WriteLine(data);
                        write.Flush();
                        string server_string = reader.ReadLine();

                        if (server_string.Contains("status"))
                        {
                            JObject obj = JObject.Parse(server_string);
                            status = Convert.ToBoolean(obj["status"].ToString());
                            break;
                        }


                    }

                    reader.Close();
                    write.Close();
                    client.Close();
                }
                catch
                { }
            }
            catch
            {
            }
            Thread.Sleep(delay * 1000);
            return status;
        }
        public bool resetxProxy(string proxy)
        {
            try
            {
               
                int delay = SettingTool.configld.delaydcomxproxy;
                var client = new RestClient(string.Format("{0}/reset?proxy={1}", SettingTool.configld.linkxproxy, proxy));
                var request = new RestRequest(Method.GET);

                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                IRestResponse response = client.Execute(request);
                string html = response.Content.Trim();
                if (html.Contains("status"))
                {
                    Thread.Sleep(delay * 1000);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }


        public bool resetProxyV6(string proxy)
        {
            try
            {
                int max = 0;
            lb_reset:
                string[] ip_port = proxy.Split(':');
                var client = new RestClient(string.Format("https://api.proxyv6.net/api/reset-ip-manual?api_key={0}&host={1}&port={2}", SettingTool.configld.apiproxyv6, ip_port[0], ip_port[1]));
                var request = new RestRequest(Method.GET);


                IRestResponse response = client.Execute(request);
                string html = response.Content.Trim();
                if (html.Contains("SUCCESS"))
                {
                    Thread.Sleep(6000);
                    return true;
                }
                else
                {

                   if (html.Contains("RESET_TOO_FAST"))
                   {
                       if (max < 12)
                       {
                           max++;
                           Thread.Sleep(10000);
                           goto lb_reset;
                       }
                       else
                           return false;
                      
                   }
                }
            }
            catch
            {
            }
            return false;
        }
        public bool resetObcProxy(string proxy)
        {

            try
            {
                int delay = SettingTool.configld.delaydcomxproxy;
                string linkreset = string.Format("{0}/reset?proxy={1}", SettingTool.configld.linkobc, proxy);
                if (linkreset.Contains("//reset?"))
                {
                    linkreset = string.Format("{0}reset?proxy={1}", SettingTool.configld.linkobc, proxy);
                }
                var client = new RestClient(linkreset);
                var request = new RestRequest(Method.GET);

                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                IRestResponse response = client.Execute(request);
                string html = response.Content.Trim();
                if (html.Contains("status"))
                {
                    Thread.Sleep( delay * 1000);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }
        public void createProxy(int thread)
        {
            try
            {
                SettingTool.list_xproxy = new List<string>();
                string path = "";
                if (SettingTool.configld.typeip == 9)
                {

                    path = SettingTool.linkproxyninja;
                    if (File.Exists(path))
                    {
                        SettingTool.list_xproxy = File.ReadLines(path).ToList();

                    }
                }
                else if (SettingTool.configld.typeip == 7)
                {
                    SettingTool.list_xproxy = new List<string>(); //mr thoong add
                    //   string pathlog=Application.StartupPath + "\\logxproxy.txt";
                    path = Application.StartupPath + "\\xproxy.txt";
                    if (File.Exists(path))
                    {
                        // File.AppendAllText(pathlog, "Mở file xproxy\r\n");
                        SettingTool.list_xproxy = File.ReadAllLines(path).ToList();
                    }

                }
                else if (SettingTool.configld.typeip == 11)
                {
                    if (SettingTool.configld.typedefaulV6 == 1)
                    {
                        var client = new RestClient(string.Format("https://api.proxyv6.net/api/list-proxy?api_key={0}&proxy_type=2", SettingTool.configld.apiproxyv6));
                        var request = new RestRequest(Method.GET);

                        IRestResponse response = client.Execute(request);
                        var data = response.Content;
                        JObject obj = JObject.Parse(data);
                        if (obj["message"].ToString() == "SUCCESS")
                        {
                            foreach (var item in obj["data"]["data"])
                            {
                                SettingTool.list_xproxy.Add(item["host"] + ":" + item["port"]);
                            }
                        }
                    }
                    else
                    {
                        SettingTool.list_xproxy = new List<string>(); 
                        path = Application.StartupPath + "\\proxyV6.txt";
                        if (File.Exists(path))
                        {
                            SettingTool.list_xproxy = File.ReadAllLines(path).ToList();
                        }
                    }
                }
                else
                {
                    if (SettingTool.configld.typeip == 10)
                    {
                        path = Application.StartupPath + "\\obcproxy.txt";
                        if (File.Exists(path))
                        {
                            SettingTool.list_xproxy = File.ReadAllLines(path).ToList();
                        }
                    }
                    else
                    {
                        if (SettingTool.configld.typeip == 8)
                        {
                            string[] arrapi = SettingTool.configld.apiTMproxy.Split('\n');
                            foreach (string api in arrapi)
                            {
                                if (!string.IsNullOrEmpty(api.Trim()))
                                {
                                    SettingTool.list_xproxy.Add(api.Trim());
                                }
                            }
                        }
                        if (SettingTool.configld.typeip == 6)
                        {
                            string[] arrapi = SettingTool.configld.apitinsoft.Split('\n');
                            foreach (string api in arrapi)
                            {
                                if (!string.IsNullOrEmpty(api.Trim()))
                                {
                                    SettingTool.list_xproxy.Add(api.Trim());
                                }
                            }
                        }
                    }
                }

                if (SettingTool.list_xproxy.Count > 0)
                {
                    SettingTool.list_freeproxy = new List<xproxy>();
                    SettingTool.list_running = new List<string>();
                    int int_0 = 0;
                    int totaproxy = SettingTool.list_xproxy.Count;
                    for (int i = 0; i < thread; i++)
                    {
                        if (int_0 >= totaproxy)
                        {
                            int_0 = 0;
                        }
                        xproxy x = new xproxy();
                        x.proxy = SettingTool.list_xproxy[int_0].Trim();
                        x.use = false;
                        x.proxysucess = "";
                        SettingTool.list_freeproxy.Add(x);
                        int_0++;
                    }
                    SettingTool.list_freeproxy = SettingTool.list_freeproxy.OrderBy(x => x.proxy).ToList();
                    if (SettingTool.configld.typeip == 9)
                    {
                        reset = "All";
                        resetNinjaProxy("");
                    }
                }
            }
            catch
            {

            }

        }
        public string getProxy()
        {
            int type = SettingTool.configld.typeip;
            string proxy = null;
            string proxytm = null;
            try
            {
                foreach (xproxy xp in SettingTool.list_freeproxy)//list api
                {
                    if (xp.use == false)
                    {
                        proxy = xp.proxy;
                        xp.use = true;
                        //check proxy running
                        if (checkProxyRunning(proxy) == false)
                        {
                            if (type == 10)
                            {
                                resetObcProxy(proxy);
                            }
                            else if (type == 11)
                            {
                               if  (!resetProxyV6(proxy))
                               {
                                   xp.use = false;
                                   goto next_proxy; 
                               }
                                   
                            }
                            else
                            {
                                if (type == 7)
                                {
                                    resetxProxy(proxy);
                                }
                                else
                                {
                                    if (type == 8)
                                    {
                                        proxytm = changeIpHelper.method_ChangeTMproxyOneApi(xp.proxy);
                                        if (!string.IsNullOrEmpty(proxytm))
                                        {
                                            xp.proxysucess = proxytm;
                                        }
                                        else
                                        {
                                            xp.use = false;
                                            proxytm = null;
                                        }

                                    }
                                    else
                                    {
                                        if (type == 6)
                                        {
                                            //tinsoft
                                            proxytm = changeIpHelper.method_ChangeTinSoftOneApi(xp.proxy);
                                            if (!string.IsNullOrEmpty(proxytm))
                                            {
                                                xp.proxysucess = proxytm;
                                                xp.use = true;
                                            }
                                            else
                                            {
                                                xp.use = false;
                                                proxytm = null;
                                            }
                                        }
                                        else
                                        {
                                            //ninja proxy
                                            if (reset == "All")
                                            {

                                            }
                                            else
                                            {
                                                resetNinjaProxy(proxy);
                                            }

                                        }

                                    }

                                }
                            }
                            //reset dcom
                        }
                        else
                        {
                            //lay proxy của api voi voi tinsoft , timproxy
                            if (type == 8 || type == 6)
                            {
                                proxytm = checkProxyApiRunning(xp.proxy);
                                if (!string.IsNullOrEmpty(proxytm))
                                {
                                    xp.proxysucess = proxytm;
                                    xp.use = true;
                                }
                                else
                                {
                                    xp.use = false;
                                    proxytm = null;
                                }
                            }
                        }

                        if (type == 8 || type == 6)
                        {
                            //tm tinsoft
                            if (!string.IsNullOrEmpty(proxytm))
                            {
                                SettingTool.list_running.Add(proxy);
                            }
                        }
                        else
                        {
                            SettingTool.list_running.Add(proxy);
                        }
                        if (type == 8 || type == 6)
                        {
                            if (!string.IsNullOrEmpty(proxytm))
                                break;
                        }
                        else
                        {
                            break;
                        }

                    }

                next_proxy:
                    type = SettingTool.configld.typeip;
                }
            }
            catch
            { }
            if (type == 8 || type == 6)
            {

                return proxytm;
            }
            else
            {
                return proxy;
            }

        }
        public string checkProxyApiRunning(string api)
        {
            if (SettingTool.configld.typeip == 6 || SettingTool.configld.typeip == 8)
            {
                foreach (xproxy p in SettingTool.list_freeproxy)
                {
                    if (p.proxy == api && !string.IsNullOrEmpty(p.proxysucess))
                    {
                        //neu api truoc do da co proxy thi lay
                        return p.proxysucess;
                    }
                }
            }
            return null;
        }
        public string GetRandomProxyApiRunning()
        {
            if (SettingTool.configld.typeip == 6 || SettingTool.configld.typeip == 8)
            {
                foreach (xproxy p in SettingTool.list_freeproxy)
                {
                    if (!string.IsNullOrEmpty(p.proxysucess))
                    {
                        //neu api truoc do da co proxy thi lay
                        return p.proxysucess;
                    }
                }
            }
            return null;
        }
        public string getApifromproxy(string proxy)
        {
            try
            {
                if (SettingTool.configld.typeip == 6 || SettingTool.configld.typeip == 8)
                {
                    foreach (xproxy p in SettingTool.list_freeproxy)
                    {
                        if (p.proxysucess.Trim() == proxy.Trim() && !string.IsNullOrEmpty(p.proxysucess))
                        {
                            //lay api khi biet proxy
                            return p.proxy;//day la api cho tinsoft va tmproxy
                        }
                    }
                }

            }
            catch
            {

            }
            return null;
        }
        public bool checkProxyRunning(string proxy)
        {

            foreach (string p in SettingTool.list_running)
            {
                if (p == proxy)
                {
                    return true;
                }
            }
            return false;
        }
        public bool finishProxy(string proxy)
        {
            bool flag = false;
            reset = "One";
            int max_reset = 0;
        lb_removeproxy:
            try
            {

                lock (SettingTool.lockobj)
                {
                    //xóa proxy ra khoi running
                    if (SettingTool.configld.typeip == 6 || SettingTool.configld.typeip == 8)
                    {
                        proxy = getApifromproxy(proxy);//chuyen doi thanh api
                    }


                    foreach (string p in SettingTool.list_running)
                    {
                        if (p == proxy)
                        {
                            SettingTool.list_running.Remove(p);
                            flag = true;
                            break;
                        }
                    }
                    //neu running==0 thì reset lại
                    int dem = 0;
                    foreach (string p in SettingTool.list_running)
                    {
                        if (p == proxy)
                        {
                            dem++;
                        }
                    }
                    if (dem == 0)
                    {
                        foreach (xproxy xp in SettingTool.list_freeproxy)
                        {
                            if (xp.proxy == proxy)
                            {
                                xp.proxysucess = "";//thoong add
                                xp.use = false;
                            }

                        }
                    }
                }
            }
            catch
            {

            }
            if (!flag)
            {
                max_reset++;
                if (max_reset < 10)
                {
                    if (!string.IsNullOrEmpty(proxy))
                        goto lb_removeproxy;
                }

                else
                    return flag;
            }

            return flag;

        }
    }
    public class xproxy
    {
        public string proxysucess { set; get; }//proxy sau khi get từ tmproxy thành công
        public string proxy { set; get; }//doi voi tmproxy, tinsoft thi day la api
        public bool use { set; get; }

    }
}
//update 9.8 578 lines
