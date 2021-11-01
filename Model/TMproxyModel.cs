using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NinjaSystem
{
    public class TMproxyModel
    {
        public string code { set; get; }
        public string api { set; get; }
        public string ip_allow { set; get; }
        public string socks5 { set; get; }
        public string proxy { set; get; }
        public string message { set; get; }
        public int timeout { set; get; }
        public string expired_at { set; get; }
        public int next_request { set; get; }



    }
    public class TMproxyResult
    {
        public bool status { set; get; }
        public List<string> list_proxy { set; get; }
        public List<TMproxyModel> list_model { set; get; }
    }
    //30/10
}
