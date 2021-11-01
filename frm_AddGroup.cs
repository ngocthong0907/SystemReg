using System;
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
    public partial class frm_AddGroup : Form
    {
        public frm_AddGroup()
        {
            InitializeComponent();
        }

        Data dt = new Data();
        string[] ssub = new string[2];
        private void button1_Click(object sender, EventArgs e)
        {
            Data dt = new Data();
            DataTable dt_stt = dt.select("select max(sothutu) from danhmuc ");
            RequestParams para = new RequestParams();
            RequestParams para_where = new RequestParams();
            para.Add(new KeyValuePair<string, string>("tendanhmuc", textBox1.Text));
            para_where.Add(new KeyValuePair<string, string>("Id_danhmuc", ssub[1]));
            if (ssub[0] == "Update")
            {
                dt.update(para, "danhmuc", para_where);
            }
            else
            {
                int sothutu = 0;
                if (dt_stt.Rows[0][0] == null || dt_stt.Rows[0][0].ToString() == "")
                    sothutu = 1;
                else
                    sothutu = int.Parse(dt_stt.Rows[0][0].ToString()) + 1;

                para.Add(new KeyValuePair<string, string>("sothutu", sothutu.ToString()));
                dt.insert(para, "danhmuc");
            }

            this.Close();

        }

        private void frmAddGroup_Load(object sender, EventArgs e)
        {
            DataTable source = new DataTable();

            if (this.AccessibleDescription != null)
            {
                ssub = this.AccessibleDescription.Split('$');

            }
            if (ssub[0] == "Update")
            {
                DataTable result = dt.select(" select  * from danhmuc where id = " + ssub[1]);
                if (result.Rows.Count > 0)
                {
                    textBox1.Text = result.Rows[0]["tendanhmuc"].ToString();

                    if (SettingTool.configld.language == "English")
                        button1.Text = "Update";
                    else
                        button1.Text = "Cập nhật";

                }
            }
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }
        }
        private void setupLanguage()
        {
            this.Text = "Add group";
            label1.Text = "Group name";
        }
    }
}
