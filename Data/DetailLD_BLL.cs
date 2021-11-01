using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class DetailLD_BLL
    {
        Data data = new Data();
        public List<DetailLDModel> selectDetailLD()
        {
            string sql = "Select  * From DetailLD order by LDID asc";
            return data.selectDetailLD(sql);
        }

        public DetailLDModel selectOneDetailLD(int LDID)
        {
            string sql = "Select  * From DetailLD Where LDID=" + LDID + " order by LDID asc";
            return data.selectOneDetailLD(sql);
        }

        public List<DetailLDModel> selectDetailLD(int GroupLDID)
        {
            string sql = "Select  * From DetailLD Where GroupLDID="+GroupLDID+" order by LDID asc";
            return data.selectDetailLD(sql);
        }
        public bool insert(DetailLDModel model)
        {
            RequestParams para = new RequestParams();
            para["GroupLDID"] = model.GroupLDID;
            para["LDID"] = model.LDID;
            para["LDName"] = model.LDName;
            return data.insert(para, "DetailLD");
        }
        public bool update(DetailLDModel model)
        {
            RequestParams para = new RequestParams();
            para["GroupLDID"] = model.GroupLDID;
            para["LDID"] = model.LDID;
            para["LDName"] = model.LDName;
            para["Proxy"] = model.Proxy;
            para["keyvpn"] = model.Keyvpn;
            para["dns"] = model.dns;
            RequestParams where = new RequestParams();
            where["ID"] = model.ID;
            return data.update(para, "DetailLD", where);
        }
        public bool moveLD(DetailLDModel model)
        {
            RequestParams para = new RequestParams();
            para["GroupLDID"] = model.GroupLDID;          
            RequestParams where = new RequestParams();
            where["LDID"] = model.LDID;
            return data.update(para, "DetailLD", where);
        }
        public bool delete(DetailLDModel model)
        {
            RequestParams where = new RequestParams();
            where["ID"] = model.ID;
            return data.delete("DetailLD", where);
        }
        public bool deleteByGroupID(int groupLDID)
        {
            RequestParams where = new RequestParams();
            where["GroupLDID"] = groupLDID;
            return data.delete("DetailLD", where);
        }
    }
}
