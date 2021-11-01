using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class FacebookModel
    {
        public string data { set; get; }
        public int type { set; get; }
        public int appid { set; get; }
    }
    public class CareFacebookResult
    {
        public string data { get; set; }
        public string mess { get; set; }
        public bool status { get; set; }
    }
}
