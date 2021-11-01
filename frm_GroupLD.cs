using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_GroupLD : Form
    {
        public frm_GroupLD(frm_MainLD frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        GroupLD_BLL groupld_bll = new GroupLD_BLL();
        DetailLD_BLL detailld_bll = new DetailLD_BLL();
        frm_MainLD frm;
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn xoá LD ra khỏi nhóm không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                try
                { 
                    foreach (DataGridViewRow dr in dgvLDin.SelectedRows)
                    {
                        DetailLDModel model = (DetailLDModel)dr.Tag;
                        detailld_bll.delete(model);
                    } 
                    loadGroupLDDetail();
                }
                catch
                { }
            }
        }

        private void frm_GroupLD_Load(object sender, EventArgs e)
        {
            loadGroupLD();
        }
        private void loadGroupLD()
        {
            try
            {
                cboGroupLD.Items.Clear();
                ComboboxItem item1 = new ComboboxItem();
                item1.Text = "Chọn nhóm LD";
                cboGroupLD.Items.Add(item1);
                List<GroupLDModel> list_groupld = new List<GroupLDModel>();
                list_groupld = groupld_bll.selectGroupLD();
                foreach (GroupLDModel group in list_groupld)
                {
                    ComboboxItem item = new ComboboxItem();
                    item = new ComboboxItem();
                    item.Text = group.Name;
                    item.Tag = group;
                    cboGroupLD.Items.Add(item);

                }
                cboGroupLD.SelectedIndex = 0;
            }
            catch
            {

            }


        }

        private void cboGroupLD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                loadGroupLDDetail();
            }
            catch
            {

            }
        }
        private void loadGroupLDDetail()
        {
            ComboboxItem item2 = (ComboboxItem)cboGroupLD.SelectedItem;
            if (item2.Text == "Chọn nhóm LD")
            {

            }
            else
            {
                GroupLDModel group = (GroupLDModel)item2.Tag;
                grbLD.Text = "Danh sách LD đã add vào nhóm: " + group.Name;
                //lay toan bo danh sach ld da add
                List<DetailLDModel> list_ld = new List<DetailLDModel>();
                list_ld = detailld_bll.selectDetailLD(group.GroupLDID);
                dgvLDOut.Rows.Clear();
                dgvLDin.Rows.Clear();
                foreach (DetailLDModel dt in list_ld)
                {
                    method_DatagridviewLD(dt);
                }
                //lay toan bo danh sach ld
                LDController ld = new LDController();
                List<LDModel> list_all = ld.getLdPlay();

                //lay toan bo danh sach ld da add
             
                List<DetailLDModel> list_detailall = detailld_bll.selectDetailLD();
                foreach (LDModel model in list_all)
                {
                    if (checkExits(list_detailall, model) == false)
                    {
                        method_DatagridviewGroupLD(model);
                    }

                }
                
            }
        }
        public void loadLDinGroup(int groupLDID)
        {
            dgvLDin.Rows.Clear();
            List<DetailLDModel> list_detailall = detailld_bll.selectDetailLD(groupLDID);
            foreach (DetailLDModel model in list_detailall)
            {
                method_DatagridviewLD(model);

            }
        }
        public bool checkExits(List<DetailLDModel> list_detailall,LDModel model)
        {
            foreach(DetailLDModel detail in list_detailall)
            {
                if (detail.LDID == model.id)
                    return true;
            }
            return false;

        }
        private void method_DatagridviewGroupLD(LDModel model)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewImageCell check = new DataGridViewImageCell();
                //   check.Value = true;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = model.id+"."+model.name;
                dataGridViewRow.Cells.Add(cell1);
                dataGridViewRow.Tag = model;
                dataGridViewRow.Height = 50;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvLDOut.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }

        private void method_DatagridviewLD(DetailLDModel l)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewImageCell check = new DataGridViewImageCell();
                //   check.Value = true;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = l.LDID + "." + l.LDName;
                dataGridViewRow.Cells.Add(cell1);

                dataGridViewRow.Tag = l;
                dataGridViewRow.Height = 50;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvLDin.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_AddGroupLD frm = new frm_AddGroupLD();
            frm.ShowDialog();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem item2 = (ComboboxItem)cboGroupLD.SelectedItem;
                if (item2.Text == "Chọn nhóm LD")
                {
                    MessageBox.Show("Vui lòng chọn nhóm LD","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                   
                    GroupLDModel group = (GroupLDModel)item2.Tag;
                    //lay toan bo danh sach ld da add
                     foreach(DataGridViewRow dr in dgvLDOut.SelectedRows)
                     {
                         LDModel model = (LDModel)dr.Tag;
                         DetailLDModel detail = new DetailLDModel();
                         detail.GroupLDID = group.GroupLDID;
                         detail.LDID = model.id;
                         detail.LDName = model.name;
                         detailld_bll.insert(detail);
                     }
                    
                    loadGroupLDDetail();
                    
                }

            }
            catch
            { }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            frm_CreateLD frmcreate = new frm_CreateLD(frm);
            frmcreate.Show();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.youtube.com/watch?v=ngFwQopYzs8");
        }
    }
}
