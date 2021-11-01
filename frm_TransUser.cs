using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_TransUser : Form
    {
        public frm_TransUser()
        {
            InitializeComponent();
        }
        Data dt = new Data();
       public ArrayList lstUserTrans = new ArrayList();
      
        private void frmTransUser_Load(object sender, EventArgs e)
        {
           
            DataTable source = new DataTable();
            source = dt.select("select * from danhmuc");
            cboNhom.DisplayMember = "Tendanhmuc";
            cboNhom.ValueMember = "Id_danhmuc";
            cboNhom.DataSource = source;
        }
        
        private void btnTao_Click(object sender, EventArgs e)
        {
            RequestParams para = new RequestParams();
            RequestParams para_where = new RequestParams();

            para.Add(new KeyValuePair<string, string>("Id_danhmuc", cboNhom.SelectedValue.ToString()));

            foreach ( var userid in lstUserTrans)
            {
                para_where.Clear();
                para_where.Add(new KeyValuePair<string, string>("Id_account", userid.ToString()));
                dt.update(para, "Account", para_where);
            }

            lstUserTrans.Clear();
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
            
        }
    }
}
