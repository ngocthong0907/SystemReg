using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace NinjaSystem
{
    public partial class frm_ListDelete : Form
    {
        public frm_ListDelete()
        {
            InitializeComponent();
        }
        CheckBox headerCheckBox = new CheckBox();
        string user_id;
        private void frmListDelete_Load(object sender, EventArgs e)
        {
            //DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            //checkBoxColumn.HeaderText = "";
            //checkBoxColumn.Width = 30;
            //checkBoxColumn.Name = "checkBoxColumn";

            //dtgr.Columns.Insert(0, checkBoxColumn);

            //Point headerCellLocation = this.dtgr.GetCellDisplayRectangle(0, -1, true).Location;

            ////Place the Header CheckBox in the Location of the Header Cell.
            //headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
            //headerCheckBox.BackColor = Color.White;
            //headerCheckBox.Size = new Size(18, 18);
            //headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);

            //dtgr.Controls.Add(headerCheckBox);
            Load_grid();
        }
        private void Load_grid()
        {
            DataTable source = new DataTable();
            Data dt = new Data();
            source = dt.select("select * from Account_delete");
            dtgr.DataSource = source;
        }
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            dtgr.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dtgr.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            ArrayList lstUser = new ArrayList();
            lstUser.Clear();
            RequestParams para = new RequestParams();
            foreach (DataGridViewRow row in dtgr.SelectedRows)
            {
                
                    Data dt = new Data();
                    DataTable soure = new DataTable();
                    soure = dt.select( string.Format("select * from Account_delete where Id = {0}", row.Cells["Id"].Value.ToString() ));

                    para.Clear();
                    para.Add(new KeyValuePair<string, string>("Id", soure.Rows[0]["Id"].ToString()));
                   
                    dt.delete("Account_delete",para);
                    para.Clear();
                    para.Add(new KeyValuePair<string, string>("Id_danhmuc", soure.Rows[0]["id_danhmuc"].ToString()));
                    para.Add(new KeyValuePair<string, string>("Email", soure.Rows[0]["Email"].ToString()));
                    para.Add(new KeyValuePair<string, string>("Password", soure.Rows[0]["Password"].ToString().Replace("'","''")));
                    para.Add(new KeyValuePair<string, string>("TrangThai", soure.Rows[0]["TrangThai"].ToString()));
                    para.Add(new KeyValuePair<string, string>("Display", "Yes"));
                    dt.insert(para, "Account");
                
            }
            
            if (dtgr.RowCount == 1 )
            {
                this.Close();

            }
            Load_grid();
          
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            Data dt = new Data();
            RequestParams para = new RequestParams();
            foreach (DataGridViewRow row in dtgr.SelectedRows)
            {
                para.Clear();
                para.Add(new KeyValuePair<string, string>("Id", row.Cells["id"].Value.ToString()));
                dt.delete("Account_delete", para);
            }
           
            if (dtgr.RowCount == 1)
                this.Close();
            Load_grid();
        }

        private void dtgr_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            user_id = dtgr.Rows[e.RowIndex].Cells["Id"].Value.ToString();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            Data dt = new Data();
            RequestParams para = new RequestParams();
            foreach (DataGridViewRow row in dtgr.SelectedRows)
            {
                para.Clear();
                para.Add(new KeyValuePair<string, string>("Id", row.Cells["id"].Value.ToString()));
                dt.delete("Account_delete", para);
            }

            if (dtgr.RowCount == 1)
                this.Close();
            Load_grid();
        }
    }
}
