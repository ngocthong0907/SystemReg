using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public object Tag { set; get; }
        public override string ToString()
        {
            return Text;
        }
    }
}
