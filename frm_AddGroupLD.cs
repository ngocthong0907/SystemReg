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
    public partial class frm_AddGroupLD : Form
    {
        public frm_AddGroupLD()
        {
            InitializeComponent();
        }
        GroupLD_BLL group_bll = new GroupLD_BLL();
        GroupLDModel model = new GroupLDModel();
        private void button1_Click(object sender, EventArgs e)
        {
            GroupLDModel model = new GroupLDModel();
            model.Name = txtName.Text.Trim();
            if(group_bll.insertGroupLD(model))
            {
                loadGroup();
                txtName.Text = "";
            }

        }
        private void loadGroup()
        {
            dgvGroupLD.Rows.Clear();
            List<GroupLDModel> list_group = new List<GroupLDModel>();
            list_group = group_bll.selectGroupLD();
            foreach(GroupLDModel model in list_group)
            {
                method_DatagridviewGroupLD(model);
            }

        }
        private void method_DatagridviewGroupLD(GroupLDModel model)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewTextBoxCell check = new DataGridViewTextBoxCell();
                check.Value = dgvGroupLD.Rows.Count + 1;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = model.Name;
                dataGridViewRow.Cells.Add(cell1);
                dataGridViewRow.Tag = model;             
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvGroupLD.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }

        private void frm_AddGroupLD_Load(object sender, EventArgs e)
        {
            loadGroup();
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }
        }

        private void dgvGroupLD_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dr = dgvGroupLD.CurrentRow;
                model = (GroupLDModel)dr.Tag;
                txtName.Text = model.Name;
            }
            catch
            { }
        } 
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
          try
          {
              model.Name = txtName.Text.Trim();
              if (group_bll.update(model))
              {
                  txtName.Text = "";
                  loadGroup();
              }
          }catch
          { }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xoá nhóm LD này không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataGridViewRow dr = dgvGroupLD.CurrentRow;
                    GroupLDModel model = (GroupLDModel)dr.Tag;
                    DetailLD_BLL detail_bll = new DetailLD_BLL();
                    detail_bll.deleteByGroupID(model.GroupLDID);
                    if(group_bll.delete(model))
                    {
                        loadGroup();
                    }
                }

            }
            catch
            { }
           
        }
        private void setupLanguage()
        {
            this.Text = "Manager LD's group";
            label1.Text = "Group name";
            button1.Text = "Add new";
            bunifuFlatButton1.Text = "Edit";
            bunifuFlatButton2.Text = "Delete";

            groupBox2.Text = "List LDs of Group";

        }
    }
}
