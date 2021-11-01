using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class DetailLDModel
    {
        public int ID { set;get;}
        public int GroupLDID { set; get; }
        public int LDID { set; get; }
        public string LDName { set; get; }
        public string Proxy { set; get; }
        public string Keyvpn { set; get; }

        public string dns { set; get; }
    }
}
