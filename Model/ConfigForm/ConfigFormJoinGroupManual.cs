using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class ConfigFormJoinGroupManual
    {
        public string groupid { set; get; }
       
        public int numgroup { set; get; }
        public int delaymin { set; get; }
        public int delaymax { set; get; }
        public string answer { set; get; }
        public string pathanswer { set; get; }
        public bool jointrung { set; get; }
    }
}
