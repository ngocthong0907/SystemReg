using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class TwoFaModel
    {
        public string uid { set; get; }
        public string token { set; get; }
        public string message { set; get; }
        public bool status { set; get; }
        public string privatekey { set; get; }
    }
}
