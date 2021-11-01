using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class CauHinh_Bll
    {
        Data data = new Data();
        public List<CauHinh> selectAll()
        {
            string sql = "Select  * From tbl_CauHinh order by id ASC";
            return data.method_CauHinhSelect(sql);
        }
        public bool insert(CauHinh da)
        {
            string sql = "Insert into tbl_CauHinh(name) Values('" + da.Name + "')";
            return data.method_InsertData(sql);
            //return false;
        }

        public bool update(CauHinh tk)
        {
            string sql = "Update tbl_CauHinh SET name='" + tk.Name + "' Where id='" + tk.ID + "'";
            return data.method_DeleteData(sql);
        }
        public bool delete(CauHinh tk)
        {
            string sql = "Delete from tbl_CauHinh Where id='" + tk.ID+ "'";
            return data.method_DeleteData(sql);
        }
        public int method_InsertDataID()
        {
            string sql = "SELECT ID from tbl_CauHinh  order by id DESC limit 1";
            return data.method_InsertDataID(sql);
        }
    }
}
