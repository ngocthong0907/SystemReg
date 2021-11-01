using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class CareFacebookModel
    {
        public string access_token { get; set; }
        public string aid { get; set; }
        public string data { get; set; }
        public string did { get; set; }
        public string fid { get; set; }
        public int limit { get; set; }
        public List<string> list_uid { get; set; }
        public string mid { get; set; }
        public int time { get; set; }
        public string token { get; set; }
        public int type { get; set; }
        public string uid { get; set; }
        public string uidfriend { get; set; }
        public string useragent_android { get; set; }
        public string ninjatoken { get; set; }
        public string version { set; get; }
        public string cookies { set; get; }
        public string dtsg { set; get; }
        public string email { set; get; }
        public string password { set; get; }
        public string hid { set; get; }
    }
}
