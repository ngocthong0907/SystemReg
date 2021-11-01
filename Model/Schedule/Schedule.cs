using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem.Model
{
   public class Schedule
    {
        public int id { set; get; }
        public int idConfig { set; get; }
        public string fromDate { set; get; }
        public string toDate { set; get; }
        public string hours { set; get; }
        public string accounts { set; get; }

        public int type { set; get; }
        public string name { set; get; }
       //5/10
    }
}
