using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class LDModel
    {
        public int id { set; get; }
        public string name { set; get; }
        public int numacc { set; get; }
        public string manufacturer { set; get; }
        public string model { set; get; }
        public string phonenumber { set; get; }
        public string imei { set; get; }
        public string imsi { set; get; }
        public string simserial { set; get; }
        public string androidid { set; get; }
        public string mac { set; get; }
        public string country { set; get; }
        public string cpu { set; get; }
        public string ram { set; get; }
        public string timezone { set; get; }
    }
    public class LDRun
    {
        public LDModel model { set; get; }
        public List<Account> list_acc { set; get; }
    }
    
}
