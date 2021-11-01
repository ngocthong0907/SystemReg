using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace NinjaSystem
{
    public class changeIpHelper : Form
    {

        public static bool checkWaitAny()
        {
            //doi voi doi ip bang proxy, noip , 1111 thi cho chạy tiếp khi hoàn thành xong 1 ld
            //doi với đổi ip bằng dcom hma tinsoft xproxy phải đợi
            switch (SettingTool.configld.typeip)
            {
                case 1:
                    return true;
                case 4:
                    {
                        //proxy
                        return true;
                    }
                case 5:
                    {
                        //111
                        return true;
                    }
                case 6:
                    {
                        //tinsoft;
                        return true;
                    }
                case 7:
                    {
                        //xproxy;
                        return true;
                    }
                case 8:
                    {
                        //tmproxy
                        return true;
                    }
                case 9:
                    {
                        //ninja proxy
                        return true;
                    }
                case 10:
                    {
                        //obc proxy
                        return true;
                    }
                case 11:
                    {
                        //v6 proxy
                        return true;
                    }

            }
            return false;
        }
        public static bool checkGetProxyWaitAny()
        {
            if (SettingTool.configld.typeip == 7 || SettingTool.configld.typeip == 6 || SettingTool.configld.typeip == 8 || SettingTool.configld.typeip == 9 || SettingTool.configld.typeip == 10 || SettingTool.configld.typeip == 11)
            {
                return true;
            }
            return false;
        }
        public static ResultRequest connectBeforeOpen(RichTextBox richLog)
        {
            ResultRequest kq = new ResultRequest();

            LDController ld = new LDController();

            switch (SettingTool.configld.typeip)
            {
                case 1:

                    kq.status = true;
                    kq.mess = "";
                    kq.data = "";//no ip
                    break;
                case 2:
                    {
                        //hma
                        kq = changeHMA();
                        break;
                    }
                case 3:
                    //change ip dcom 
                    kq = changeDcom(richLog);
                    break;
                case 7:
                    {
                        //reset toan bo port của xproxy
                        SettingTool.list_xproxy = new List<string>();
                        //   string pathlog=Application.StartupPath + "\\logxproxy.txt";
                        string path = Application.StartupPath + "\\xproxy.txt";
                        if (File.Exists(path))
                        {
                            // File.AppendAllText(pathlog, "Mở file xproxy\r\n");
                            List<string> list_proxy = File.ReadAllLines(path).ToList();
                            if (list_proxy.Count > 0)
                            {
                                //    File.AppendAllText(pathlog, "Total Proxy : " + list_proxy.Count+"\r\n");
                                int numthread = list_proxy.Count;
                                if (numthread > SettingTool.configld.numthread)
                                {
                                    numthread = SettingTool.configld.numthread;
                                }
                                //  File.AppendAllText(pathlog, "Num Thread : " + numthread + "\r\n");
                                Task[] tasks = new Task[numthread];
                                object synDevice = new object();
                                for (int i = 0; i < numthread; i++)
                                {
                                    int t = i;
                                    tasks[t] = Task.Factory.StartNew(() =>
                                    {
                                        string proxy = "";
                                        lock (synDevice)
                                        {
                                            proxy = list_proxy[t];
                                            SettingTool.list_xproxy.Add(proxy);
                                            //   File.AppendAllText(pathlog, "Change Proxy : " + proxy + "\r\n");
                                        }

                                        method_StartChangeXproxy(proxy);
                                    });

                                    Thread.Sleep(10);
                                }

                                try
                                {
                                    Task.WaitAny(tasks);
                                }
                                catch
                                {

                                }

                                kq.status = true;
                            }
                            else
                            {
                                kq.status = false;
                            }
                        }
                        else
                        {
                            kq.status = false;
                        }
                        break;
                    }

            }

            return kq;

        }
        public static void method_StartChangeXproxy(string proxy)
        {
            YahooController yahoo = new YahooController();
            yahoo.changeXproxy(SettingTool.configld.linkxproxy, proxy);
        }
        public static ResultRequest connectAfterOpen(userLD u, RichTextBox richLog, string ldid, CancellationToken token)
        {
            ResultRequest kq = new ResultRequest();
            kq.status = false;
            LDController ld = new LDController();

            switch (SettingTool.configld.typeip)
            {
                case 1:
                    {
                        ld.setProxyAdb(ldid, ":0");
                        //hien thi ip doi voi doi i
                        string ip = ld.checkIP();
                        u.setDevice(ldid, ip);
                        break;
                    }
                case 2:
                    {
                        //hien thi ip doi voi doi hma
                        string ip = ld.checkIP();
                        u.setDevice(ldid, ip);
                        break;
                    }
                case 3:
                    {
                        //hien thi ip doi voi doi dcom
                        string ip = ld.checkIP();
                        u.setDevice(ldid, ip);
                        break;
                    }
                case 4:
                    {
                        kq.status = false;
                        kq.mess = "";
                        kq.data = "";//Your IP;                    
                        ld.runApp(ldid, "com.cloudflare.onedotonedotonedotone");
                        Thread.Sleep(3000);
                        u.setStatus(ldid, "Bắt đầu đổi ip VPN 1.1.1.1");
                        kq.status = ld.setVPN1111(ldid);
                        break;
                    }
                case 5:
                    {
                        string proxy = getProxyLD(ldid);
                        if (string.IsNullOrEmpty(proxy) == false)
                        {
                            u.setDevice(ldid, proxy);
                            u.setStatus(ldid, "Đổi Proxy: " + proxy);

                            string[] arrproxy = proxy.Split(':');
                            if (arrproxy.Length == 2)
                            {
                                if (SettingTool.configld.proxytype.Contains("http"))
                                {
                                    if (ld.setProxyAdb(ldid, proxy))
                                    {
                                        kq.status = true;
                                    }
                                    else
                                    {
                                        kq.status = false;
                                    }
                                }
                                else
                                    ld.setProxyAuthentica_proxydroid(ldid, proxy, token);
                            }
                            else
                            {
                                if (SettingTool.configld.appproxy.Contains("College"))
                                    ld.setProxyAuthentica(ldid, proxy, token);
                                else
                                    ld.setProxyAuthentica_proxydroid(ldid, proxy, token);
                            }

                        }
                        string dns = getDnsLD(ldid);
                        if (string.IsNullOrEmpty(dns) == false)
                        {
                            string[] arrproxy = dns.Split('|');
                            if (arrproxy.Length > 1)
                                ld.setDNS(ldid, dns, token);
                        }

                        break;
                    }
                //case 7:
                //    {
                //        //xproxy
                //        if (SettingTool.list_xproxy.Count > 0)
                //        {
                //            string proxy = SettingTool.list_xproxy[0];
                //            SettingTool.list_xproxy.Remove(proxy);
                //            if (string.IsNullOrEmpty(proxy) == false)
                //            {

                //                u.setStatus(ldid, "Đổi xProxy: " + proxy);
                //                string proxychange = "";
                //                if (SettingTool.configld.sock5)
                //                {
                //                    proxychange = proxy.Replace("500", "400");
                //                }
                //                else
                //                {
                //                    proxychange = proxy;
                //                }
                //                string ip = method_ChangeXProxy(proxychange);
                //                u.setDevice(ldid, ip);
                //                if (SettingTool.configld.sock5)
                //                {
                //                    if (ld.setProxyAuthentica(ldid, proxy, token))
                //                    {
                //                        u.setDevice(ldid, ip);
                //                        kq.status = true;
                //                    }
                //                    else
                //                    {
                //                        kq.status = false;
                //                    }
                //                }
                //                else
                //                {
                //                    if (ld.setProxyAdb(ldid, proxy))
                //                    {
                //                        u.setDevice(ldid, ip);
                //                        kq.status = true;
                //                    }
                //                    else
                //                    {
                //                        kq.status = false;
                //                    }
                //                }

                //            }
                //            else
                //            {
                //                kq.status = false;
                //            }

                //        }
                //        else
                //        {
                //            SettingTool.list_xproxy = new List<string>();
                //            string path = Application.StartupPath + "\\xproxy.txt";
                //            if (File.Exists(path))
                //            {
                //                SettingTool.list_xproxy = File.ReadAllLines(path).ToList();
                //                if (SettingTool.list_xproxy.Count > 0)
                //                {
                //                    string proxy = SettingTool.list_xproxy[0];
                //                    SettingTool.list_xproxy.Remove(proxy);
                //                    if (string.IsNullOrEmpty(proxy) == false)
                //                    {

                //                        u.setStatus(ldid, "Đổi Proxy: " + proxy);
                //                        string proxychange = "";
                //                        if (SettingTool.configld.sock5)
                //                        {
                //                            proxychange = proxy.Replace("500", "400");
                //                        }
                //                        else
                //                        {
                //                            proxychange = proxy;
                //                        }
                //                        string ip = method_ChangeXProxy(proxychange);
                //                        if (SettingTool.configld.sock5)
                //                        {
                //                            if (ld.setProxyAuthentica(ldid, proxy, token))
                //                            {
                //                                u.setDevice(ldid, ip);
                //                                kq.status = true;
                //                            }
                //                            else
                //                            {
                //                                kq.status = false;
                //                            }
                //                        }
                //                        else
                //                        {
                //                            if (ld.setProxyAdb(ldid, proxy))
                //                            {
                //                                u.setDevice(ldid, ip);
                //                                kq.status = true;
                //                            }
                //                            else
                //                            {
                //                                kq.status = false;
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {
                //                        kq.status = false;
                //                    }
                //                }
                //                else
                //                {
                //                    kq.status = false;
                //                }
                //            }
                //            else
                //            {
                //                kq.status = false;
                //            }
                //        }
                //        break;
                //    }

            }

            return kq;

        }

        public static ResultRequest connectAfterOpen(userLD u, RichTextBox richLog, string ldid, Account acc, CancellationToken token)
        {
            ResultRequest kq = new ResultRequest();
            kq.status = false;
            LDController ld = new LDController();

            switch (SettingTool.configld.typeip)
            {
                case 1:
                    {
                        ld.setProxyAdb(ldid, ":0");
                        //hien thi ip doi voi doi i
                        string ip = ld.checkIP();
                        u.setDevice(ldid, acc.id, ip);
                        break;
                    }
                case 2:
                    {
                        //hien thi ip doi voi doi hma
                        string ip = ld.checkIP();
                        u.setDevice(ldid, acc.id, ip);
                        break;
                    }
                case 3:
                    {
                        //hien thi ip doi voi doi dcom
                        string ip = ld.checkIP();
                        u.setDevice(ldid, acc.id, ip);
                        break;
                    }
                case 4:
                    {
                        kq.status = false;
                        kq.mess = "";
                        kq.data = "";//Your IP;                    
                        ld.runApp(ldid, "com.cloudflare.onedotonedotonedotone");
                        Thread.Sleep(3000);
                        u.setStatus(ldid, "Bắt đầu đổi ip VPN 1.1.1.1");
                        kq.status = ld.setVPN1111(ldid);
                        break;
                    }
                case 5:
                    {
                        string proxy = acc.Device_mobile; // getProxyLD(ldid);
                        if (string.IsNullOrEmpty(proxy) == false)
                        {
                            u.setDevice(ldid, acc.id, proxy);
                            u.setStatus(ldid, "Đổi Proxy: " + proxy);
                            string[] arrproxy = proxy.Split(':');
                            if (arrproxy.Length == 2)
                            {
                                if (ld.setProxyAdb(ldid, proxy))
                                {
                                    kq.status = true;
                                }
                                else
                                {
                                    kq.status = false;
                                }

                            }
                            else
                            {
                                ld.setProxyAuthentica(ldid, proxy, token);
                            }
                        }
                        break;
                    }
                //case 7:
                //    {
                //        //xproxy
                //        if (SettingTool.list_xproxy.Count > 0)
                //        {
                //            string proxy = SettingTool.list_xproxy[0];
                //            SettingTool.list_xproxy.Remove(proxy);
                //            if (string.IsNullOrEmpty(proxy) == false)
                //            {

                //                u.setStatus(ldid, "Đổi xProxy: " + proxy);
                //                string proxychange = "";
                //                if (SettingTool.configld.sock5)
                //                {
                //                    proxychange = proxy.Replace("500", "400");
                //                }
                //                else
                //                {
                //                    proxychange = proxy;
                //                }
                //                string ip = method_ChangeXProxy(proxychange);
                //                u.setDevice(ldid, ip);
                //                if (SettingTool.configld.sock5)
                //                {
                //                    if (ld.setProxyAuthentica(ldid, proxy, token))
                //                    {
                //                        u.setDevice(ldid, ip);
                //                        kq.status = true;
                //                    }
                //                    else
                //                    {
                //                        kq.status = false;
                //                    }
                //                }
                //                else
                //                {
                //                    if (ld.setProxyAdb(ldid, proxy))
                //                    {
                //                        u.setDevice(ldid, ip);
                //                        kq.status = true;
                //                    }
                //                    else
                //                    {
                //                        kq.status = false;
                //                    }
                //                }

                //            }
                //            else
                //            {
                //                kq.status = false;
                //            }

                //        }
                //        else
                //        {
                //            SettingTool.list_xproxy = new List<string>();
                //            string path = Application.StartupPath + "\\xproxy.txt";
                //            if (File.Exists(path))
                //            {
                //                SettingTool.list_xproxy = File.ReadAllLines(path).ToList();
                //                if (SettingTool.list_xproxy.Count > 0)
                //                {
                //                    string proxy = SettingTool.list_xproxy[0];
                //                    SettingTool.list_xproxy.Remove(proxy);
                //                    if (string.IsNullOrEmpty(proxy) == false)
                //                    {

                //                        u.setStatus(ldid, "Đổi Proxy: " + proxy);
                //                        string proxychange = "";
                //                        if (SettingTool.configld.sock5)
                //                        {
                //                            proxychange = proxy.Replace("500", "400");
                //                        }
                //                        else
                //                        {
                //                            proxychange = proxy;
                //                        }
                //                        string ip = method_ChangeXProxy(proxychange);
                //                        if (SettingTool.configld.sock5)
                //                        {
                //                            if (ld.setProxyAuthentica(ldid, proxy, token))
                //                            {
                //                                u.setDevice(ldid, ip);
                //                                kq.status = true;
                //                            }
                //                            else
                //                            {
                //                                kq.status = false;
                //                            }
                //                        }
                //                        else
                //                        {
                //                            if (ld.setProxyAdb(ldid, proxy))
                //                            {
                //                                u.setDevice(ldid, ip);
                //                                kq.status = true;
                //                            }
                //                            else
                //                            {
                //                                kq.status = false;
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {
                //                        kq.status = false;
                //                    }
                //                }
                //                else
                //                {
                //                    kq.status = false;
                //                }
                //            }
                //            else
                //            {
                //                kq.status = false;
                //            }
                //        }
                //        break;
                //    }

            }
            return kq;
        }

        static bool has_dcom = false;
        static DcomHelper dcom = null;
        public static ResultRequest changeHMA()
        {
            ResultRequest kq = new ResultRequest();
            kq.status = false;
            kq.mess = "";
            try
            {
                LDController ld = new LDController();
                ld.changeIp();
                int i = 3;
                while (i > 0)
                {
                    string yourip = ld.checkIP();
                    if (string.IsNullOrEmpty(yourip))
                    {
                        ld.changeIp();
                        Thread.Sleep(6000);
                    }
                    else
                    {
                        kq.data = "Your IP : " + yourip;
                        kq.status = true;
                        break;
                    }
                    i--;
                }
            }
            catch
            { }
            return kq;
        }
        public static ResultRequest changeDcom(RichTextBox richLogs)
        {
            ResultRequest kq = new ResultRequest();
            kq.status = false;
            kq.mess = "";
            try
            {

                if (has_dcom == false)
                {
                    dcom = new DcomHelper(richLogs);
                    has_dcom = true;
                }

                dcom.method_Disconnect();
                dcom.method_Connect();

                Thread.Sleep(SettingTool.configld.delaydcom * 1000);
                LDController ld = new LDController();
                int i = 10;
                while (i > 0)
                {
                    string yourip = ld.checkIP();
                    if (string.IsNullOrEmpty(yourip))
                    {

                        Thread.Sleep(1000);
                    }
                    else
                    {
                        kq.data = "Your IP : " + yourip;
                        kq.status = true;
                        break;
                    }
                    i--;
                }
            }
            catch
            { }
            return kq;
        }
        public static bool changeProxyAdb(string ldid)
        {
            try
            {
                string proxy = getProxyLD(ldid);
                if (string.IsNullOrEmpty(proxy) == false)
                {
                    LDController ld = new LDController();
                    string[] arrproxy = proxy.Split(':');
                    if (arrproxy.Length == 2)
                    {
                        ld.setProxyAdb(ldid, proxy);
                        return true;
                    }

                }
            }
            catch
            { }
            return false;
        }
        public static bool changeProxyAdb(string ldid, string proxy)
        {
            try
            {
                LDController ld = new LDController();
                return ld.setProxyAdb(ldid, proxy);
            }
            catch
            { }
            return false;
        }
        public static string getProxyLD(string ldid)
        {
            DetailLD_BLL detail_bll = new DetailLD_BLL();
            DetailLDModel detailLd = new DetailLDModel();
            detailLd = detail_bll.selectOneDetailLD(int.Parse(ldid));
            return detailLd.Proxy;
        }
        public static string getDnsLD(string ldid)
        {
            DetailLD_BLL detail_bll = new DetailLD_BLL();
            DetailLDModel detailLd = new DetailLDModel();
            detailLd = detail_bll.selectOneDetailLD(int.Parse(ldid));
            return detailLd.dns;
        }
        public static TinsoftResult method_ChangeTinSoft(string list_api)
        {
            YahooController yahoo = new YahooController();
            TinsoftResult result = new TinsoftResult();
            List<string> list_proxy = new List<string>();
            List<TinSoftModel> list_tinsoft = new List<TinSoftModel>();
            try
            {
                string[] arrapi = list_api.Split('\n');
                foreach (string api in arrapi)
                {
                    if (string.IsNullOrEmpty(api.Trim()) == false)
                    {
                        for (int i = 0; i <= 10; i++)
                        {
                            TinSoftModel kq = yahoo.changeProxyTinSoft(api, SettingTool.configld.tinsoftid);
                            kq.api = api;
                            if (kq.success == false)
                            {
                                if (kq.description.Contains("expired") || kq.description.Contains("wrong key"))
                                {

                                    list_tinsoft.Add(kq);
                                    break;
                                }
                                else
                                {
                                    if (kq.description.Contains("wait") || kq.description.Contains("proxy not found"))
                                    {
                                        int delay = 10;
                                        try
                                        {
                                            delay = Convert.ToInt32(kq.next_change);
                                        }
                                        catch
                                        { }
                                        if (delay == 0)
                                            delay = 6;
                                        Thread.Sleep(delay * 1000);

                                    }
                                    else
                                    {
                                        list_tinsoft.Add(kq);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                list_tinsoft.Add(kq);
                                list_proxy.Add(kq.proxy);
                                break;
                            }

                        }
                    }
                }
            }
            catch
            { }
            result.list_proxy = list_proxy;
            result.list_model = list_tinsoft;
            if (result.list_proxy.Count > 0)
            {
                result.status = true;
            }
            else
                result.status = false;
            return result;
        }
        public static string method_ChangeTinSoftOneApi(string api)
        {
            string proxy = null;
            YahooController yahoo = new YahooController();
            try
            {

                for (int i = 0; i <= 10; i++)
                {
                    TinSoftModel kq = yahoo.changeProxyTinSoft(api, SettingTool.configld.tinsoftid);
                    kq.api = api;
                    if (kq.success == false)
                    {
                        if (kq.description.Contains("expired") || kq.description.Contains("wrong key"))
                        {
                            MessageBox.Show(api + ": Key hết hạn, hoặc key sai"); 
                            return null;
                        }
                        else
                        {
                            if (kq.description.Contains("wait") || kq.description.Contains("proxy not found"))
                            {
                                int delay = 10;
                                try
                                {
                                    delay = Convert.ToInt32(kq.next_change);
                                }
                                catch
                                { }
                                if (delay == 0)
                                    delay = 6;
                                Thread.Sleep(delay * 1000);

                            }
                            else
                            {
                                return kq.proxy;
                               
                            }
                        }
                    }
                    else
                    {
                        return kq.proxy; 
                    }

                }

            }
            catch
            { }
            return proxy;
        }
        public static TinSoftModel method_GetProxyTinSoft(string api)
        {
            YahooController yahoo = new YahooController();

            TinSoftModel kq = yahoo.getProxyTinsoftStatus(api, SettingTool.configld.tinsoftid);
            if (kq.success == false)
            {
                kq = yahoo.changeProxyTinSoft(api, SettingTool.configld.tinsoftid);
            }
            else
            {
                if (kq.timeout < 10)
                {
                    kq = yahoo.changeProxyTinSoft(api, SettingTool.configld.tinsoftid);
                }
            }
            return kq;
        }

        public static string method_ChangeXProxy(string proxy)
        {
            int i = 0;
            if (SettingTool.configld.delaydcom == 0)
            {
                SettingTool.configld.delaydcom = 10;
            }
            LDController ld = new LDController();
            YahooController yahoo = new YahooController();
        Lb_Start:
            yahoo.changeXproxy(SettingTool.configld.linkxproxy, proxy);
        lb_Next:
            i++;
            Thread.Sleep(SettingTool.configld.delaydcom * 1000);
            string ip = ld.checkIP(proxy);
            if (string.IsNullOrEmpty(ip))
            {
                if (i < 3)
                {
                    goto lb_Next;
                }
                else
                {
                    if (i < 10)
                    {
                        goto Lb_Start;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            else
            {
                return ip;
            }
        }
        public static TMproxyResult method_ChangeTMproxy(string list_api)
        {
            YahooController yahoo = new YahooController();
            TMproxyResult result = new TMproxyResult();
            List<string> list_proxy = new List<string>();
            List<TMproxyModel> list_tmproxy = new List<TMproxyModel>();
            try
            {
                string[] arrapi = list_api.Split('\n');
                foreach (string api in arrapi)
                {
                    if (string.IsNullOrEmpty(api.Trim()) == false)
                    {
                        for (int i = 0; i <= 3; i++)
                        {
                            TMproxyModel kq = yahoo.changeTMproxy(api.Trim(), SettingTool.configld.tinsoftid);
                            kq.api = api.Trim();

                            if (kq.proxy.Length > 0)
                            {
                                list_proxy.Add(kq.proxy);
                                list_tmproxy.Add(kq);
                                break;
                            }
                            else
                            {
                                if (kq.message.Contains("retry after"))
                                {
                                    int delay = 60;
                                    try
                                    {
                                        delay = int.Parse(Regex.Match(kq.message, @"\d+").Value);
                                    }
                                    catch
                                    { }

                                    Thread.Sleep(delay * 1000);
                                }
                            }

                        }
                    }
                }
            }
            catch
            { }
            result.list_proxy = list_proxy;
            result.list_model = list_tmproxy;
            if (result.list_proxy.Count > 0)
            {
                result.status = true;
            }
            else
                result.status = false;
            return result;
        }

        public static string method_ChangeTMproxyOneApi(string api)
        {
            YahooController yahoo = new YahooController();
            string proxy = "";
            try
            {

                for (int i = 0; i <= 3; i++)
                {
                    TMproxyModel kq = yahoo.changeTMproxy(api.Trim(), SettingTool.configld.tinsoftid);
                    kq.api = api;

                    if (kq.proxy.Length > 0)
                    {
                        return kq.proxy.Trim();
                    }
                    else
                    {
                        if (kq.message.Contains("retry after"))
                        {
                            int delay = 60;
                            try
                            {
                                delay = int.Parse(Regex.Match(kq.message, @"\d+").Value);
                            }
                            catch
                            { }

                            Thread.Sleep(delay * 1000);
                        }
                    }

                }
            }
            catch
            { }
            return proxy;
        }
        public static void createLDID(int num)
        {
            LDController ld = new LDController();
            lock (SettingTool.synld)
            {
                SettingTool.list_ld = new List<string>();
                for (int i = 1; i <= num; i++)
                {
                    SettingTool.list_ld.Add(i.ToString());
                }
            }
        }
        public static void createLDID2nd(int num)
        {
            LDController ld = new LDController();
            lock (SettingTool.synld)
            {
                for (int i = 1; i <= num; i++)
                {
                    string cmd = String.Format("isrunning --index {0}", i.ToString());
                    string html = ld.ExecuteAsAdmin(SettingTool.pathLD, cmd);
                    if (html != null)
                    {
                        if (html.Contains("stop"))
                        {
                            if (!SettingTool.list_ld.Contains(i.ToString()))
                                SettingTool.list_ld.Add(i.ToString());
                        }
                    }


                }
            }
        }
        public static string getLD()
        {
            lock (SettingTool.synld)
            {
                if (SettingTool.list_ld.Count > 0)
                {
                    string ldid = SettingTool.list_ld[0];
                    SettingTool.list_ld.Remove(ldid);
                    return ldid;
                }
                else
                {
                    createLDID2nd(SettingTool.configld.numthread);
                    if (SettingTool.list_ld.Count > 0)
                    {
                        string ldid = SettingTool.list_ld[0];
                        SettingTool.list_ld.Remove(ldid);
                        return ldid;
                    }
                    else
                        return "-1";

                }
            }
        }
        public static void finishLD(string ldid)
        {
            lock (SettingTool.synld)
            {
                SettingTool.list_ld.Add(ldid);
            }
        }
    }
}
