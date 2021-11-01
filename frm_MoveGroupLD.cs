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
    public partial class frm_MoveGroupLD : Form
    {
        public frm_MoveGroupLD(List<DetailLDModel> list_ld)
        {
            InitializeComponent();
            this.list_ld = list_ld;
        }
        List<DetailLDModel> list_ld = new List<DetailLDModel>();
        GroupLD_BLL groupld_bll = new GroupLD_BLL();
        DetailLD_BLL detailld_bll = new DetailLD_BLL();
        private void frm_MoveGroupLD_Load(object sender, EventArgs e)
        {

            loadGroupLD();
        }
        private void loadGroupLD()
        {
            try
            {
                cboGroupLD.Items.Clear();
                ComboboxItem item1 = new ComboboxItem();
                if (SettingTool.configld.language == "English")
                    item1.Text = "Choose group LD";
                else
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
                if (SettingTool.configld.language == "English")
                {
                    setupLanguage();
                }
            }
            catch
            {

            }


        }
        private void setupLanguage()
        {
            this.Text = "Move LD to group other";
            label1.Text = "Chosse Ld group";
            button1.Text = "Save";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ComboboxItem item2 = (ComboboxItem)cboGroupLD.SelectedItem;
            GroupLDModel group = (GroupLDModel)item2.Tag;
            //lay toan bo danh sach ld da add

            foreach (DetailLDModel detail in list_ld)
            {
                detail.GroupLDID = group.GroupLDID;

                detailld_bll.moveLD(detail);

            }
            this.Close();

        }
    }
}
