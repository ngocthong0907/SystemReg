using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class Update
    {
        public long ID { get; set; }
        public string Title { set; get; }
        public long ProductID { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string LinkFull { get; set; }
        public DateTime DateCreate { get; set; }
        public double Version { get; set; }
    }
}
