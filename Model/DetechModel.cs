using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class DetechModel
    {
        public string text { set; get; }
        public string node { set; get; }
        public string content { set; get; }
        public string parent { set; get; }
        public int function { set; get; }

        public Point point { set; get; }
        public Point point2 { set; get; }
        public bool status { set; get; }

        public string data { set; get; }
        public string desc { set; get; }
    }
    
    public class ResultDetechLite
    {
        public bool status { set; get; }
        public int function { set; get; }        
        public string name { set; get; }
        public List<Point> list_point { set; get; }
    }
    public class DetechImageLite
    {
        public string name { set; get; }
        public Bitmap img { set; get; }
        public int function { set; get; }
        public bool haschil { set; get; }//kiem tra xem toa do cua nut con
        public Bitmap imgchil { set; get; }
        public bool removenode { set; get; }//xoa node sau khi tim kiem thanh cong
    }
  
}
