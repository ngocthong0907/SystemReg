using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class DanhMuc_Bll
    {
        Data data_bll = new Data();
        public List<DanhMuc> loadDanhMuc()
        {
            string sql = "SELECT * From DanhMuc";
            List<DanhMuc> list_danhmuc = data_bll.loadDanhMuc(sql);
            return list_danhmuc;
        }
        public bool removeDanhMuc(DanhMuc dm)
        {
            RequestParams para = new RequestParams();
            para["id_danhmuc"] = dm.id_danhmuc;
            return data_bll.delete("DanhMuc", para);

        }
    }
}
