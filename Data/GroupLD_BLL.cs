using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class GroupLD_BLL
    {
        Data data = new Data();
        public List<GroupLDModel> selectGroupLD()
        {
            string sql = "Select  * From GroupLD order by groupLDID asc";
            return data.selectGroupLD(sql);
        }
        public bool insertGroupLD(GroupLDModel groupld)
        {
            RequestParams para = new RequestParams();
            para["Name"] = groupld.Name;           
            return data.insert(para, "GroupLD");
        }
        public bool update(GroupLDModel groupld)
        {
            RequestParams para = new RequestParams();
            para["Name"] = groupld.Name;  
            RequestParams where = new RequestParams();
            where["GroupLDID"] = groupld.GroupLDID;
            return data.update(para, "GroupLD", where);
        }
        public bool delete(GroupLDModel groupld)
        {
            RequestParams where = new RequestParams();
            where["GroupLDID"] = groupld.GroupLDID;
            return data.delete("GroupLD", where);
        }
    }
}
