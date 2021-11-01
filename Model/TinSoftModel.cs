using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class TinSoftModel
    {
       
        public bool success { set; get; }
        public string proxy { set; get; }
        public string next_change { set; get; }
        public int timeout { set; get; }
        public string description { set; get; }
        public string api { set; get; }
      
    }
    public class TinsoftLocation
    {
        public string name { set; get; }
        public string id { set; get; }
    }
    public class TinsoftResult
    {
        public bool status { set; get; }
        public List<string> list_proxy { set; get; }
        public List<TinSoftModel> list_model { set; get; }
    }
}
