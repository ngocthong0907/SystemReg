using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class ConfigFormProfile
    {
        public bool has_name { set; get; }
        public string ho { set; get; }
        public string ten { set; get; }
        public bool has_password { set; get; }
        public string password { set; get; }
        public bool has_avatar { set; get; }
        public bool has_cover { set; get; }
        public int delay { set; get; }
        public bool has_removepic { set; get; }
        public bool has_nick { set; get; }
        public int num_nick { set; get; }
        public bool randompass { set; get; }
        public bool turnoffnoti { set; get; }

        public string pathcity { set; get; }
        public string pathcompany { set; get; }
        public string pathschool { set; get; }
        public string pathhometown { set; get; }
     
    }
}
