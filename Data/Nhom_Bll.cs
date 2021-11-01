using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class Nhom_Bll
    {
        Data data = new Data();
        public List<Groups> selectAll()
        {
            string sql = "Select  * From danhmuc order by id ASC";
            return data.method_GroupsSelect(sql);
        }
       
    }
}
