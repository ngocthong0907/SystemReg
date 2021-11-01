using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_SettingAddFriend : Form
    {
        public frm_SettingAddFriend()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathAdd.Text = dialog.FileName;
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            //if (chkCookie.Checked && txtCookie.Text == "")
            //{
            //    MessageBox.Show("Yêu cầu nhập cookie!");
            //    return;
            //}
            method_SaveSetting();
        }
        public void method_SaveSetting()
        {
            if (rbGoiY.Checked)
                SettingTool.configadd.typeadd = 1;
            else
            {
                SettingTool.configadd.typeadd = 2;
            }
            SettingTool.configadd.maxaddfriend = (int)numMaxAddFriend.Value;
            SettingTool.configadd.maxfriend = (int)nummaxfriend.Value;
            SettingTool.configadd.delaymin = (int)numDelayMin.Value;
            SettingTool.configadd.delaymax = (int)numDelayMax.Value;
            SettingTool.configadd.maxerror = (int)numMaxError.Value;
            SettingTool.configadd.pathadd = txtPathAdd.Text;
            
            if (chkLocGioiTinh.Checked)
            {
                SettingTool.configadd.locgioitinh = true;
                SettingTool.configadd.gioitinh = cboGioiTinh.Text.Trim();
            }
            else
            {
                SettingTool.configadd.locgioitinh = false;
            }
            if (chkLocLocation.Checked)
            {
                SettingTool.configadd.loclocation = true;
                SettingTool.configadd.location = txtLocation.Text.Trim();
            }
            else
            {
                SettingTool.configadd.loclocation = false;
            }

            if (chkFriendUIDMin.Checked)
            {
                SettingTool.configadd.frienduidmin = true;
                SettingTool.configadd.uidmin = (int)numuidmin.Value;

            }
            else
            {
                SettingTool.configadd.frienduidmin = false;
                SettingTool.configadd.uidmin = 0;
            }

            if (chkFriendUIDMax.Checked)
            {
                SettingTool.configadd.frienduidmax = true;
                SettingTool.configadd.uidmax = (int)numuidmax.Value;

            }
            else
            {
                SettingTool.configadd.frienduidmax = false;
                SettingTool.configadd.uidmax = 0;
            }

            if (chkCookie.Checked)
                SettingTool.configadd.chkCookie = true;
            else
                SettingTool.configadd.chkCookie = false ;

            if (chk3G.Checked)
                SettingTool.configadd.dcom = true;
            else
                SettingTool.configadd.dcom = false;

            if (chkQuayVong.Checked)
                SettingTool.configadd.quayvong = true;
            else
                SettingTool.configadd.quayvong = false;

            if (chkDeleteUID.Checked)
                SettingTool.configadd.DeleteUID = true;
            else
                SettingTool.configadd.DeleteUID = false;


            if (chkTrung.Checked)
                SettingTool.configadd.trung = true;
            else
                SettingTool.configadd.trung = false;
            if (chkTrung.Checked)
            {
                SettingTool.configadd.trung = true;
            }
            else
                SettingTool.configadd.trung = false;

            if (chkAnFriend.Checked)
            {
                SettingTool.configadd.anfriend = true;
            }
            else
                SettingTool.configadd.anfriend = false;


            if (chkNickAo.Checked)
            {
                SettingTool.configadd.locnickao = true;
            }
            else
                SettingTool.configadd.locnickao = false;
            SettingTool.configadd.cookie = "";//txtCookie.Text.Trim();

            if (chkLikeAvatar.Checked)
            {
                SettingTool.configadd.likeavatar = true;
            }
            else
                SettingTool.configadd.likeavatar = false;
            string path = String.Format("{0}\\Config\\configadd.data", Application.StartupPath);
            File.WriteAllText(path, JsonConvert.SerializeObject(SettingTool.configadd));
            MessageBox.Show("Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void frm_SettingAddFriend_Load(object sender, EventArgs e)
        {
            string[] arr = { "male", "female" };
            cboGioiTinh.DataSource = arr;
            cboGioiTinh.SelectedIndex = 0;
            method_LoadConfig();
        }
        private void method_LoadConfig()
        {
            try
            {
                string path = String.Format("{0}\\Config\\configadd.data", Application.StartupPath);

                SettingTool.configadd = new ConfigAdd();
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    SettingTool.configadd = JsonConvert.DeserializeObject<ConfigAdd>(json);
                }

                if (SettingTool.configadd.typeadd == 1)
                {
                    rbGoiY.Checked = true;
                }
                if (SettingTool.configadd.typeadd == 2)
                {
                    rbFile.Checked = true;
                }
                numMaxAddFriend.Value = SettingTool.configadd.maxaddfriend;
                nummaxfriend.Value = SettingTool.configadd.maxfriend;
                numDelayMin.Value = SettingTool.configadd.delaymin;
                numDelayMax.Value = SettingTool.configadd.delaymax;
                numMaxError.Value = SettingTool.configadd.maxerror;
                chkLocGioiTinh.Checked = SettingTool.configadd.locgioitinh;
                cboGioiTinh.Text = SettingTool.configadd.gioitinh;
                txtPathAdd.Text = SettingTool.configadd.pathadd;

                chkLocLocation.Checked = SettingTool.configadd.loclocation;
                txtLocation.Text = SettingTool.configadd.location;

                chkFriendUIDMin.Checked = SettingTool.configadd.frienduidmin;
                numuidmin.Value = SettingTool.configadd.uidmin;
                chkFriendUIDMax.Checked = SettingTool.configadd.frienduidmax;
                numuidmax.Value = SettingTool.configadd.uidmax;
                chkQuayVong.Checked = SettingTool.configadd.quayvong;
                chkDeleteUID.Checked = SettingTool.configadd.DeleteUID;
                chkTrung.Checked = SettingTool.configadd.trung;
                chkAnFriend.Checked = SettingTool.configadd.anfriend;
                chkNickAo.Checked = SettingTool.configadd.locnickao;
                txtCookie.Text = SettingTool.configadd.cookie;
                chk3G.Checked = SettingTool.configadd.dcom;
            
                chkCookie.Checked = SettingTool.configadd.chkCookie;
                chkLikeAvatar.Checked = SettingTool.configadd.likeavatar;
            }
            catch
            { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFillter.Checked)
                groupBox1.Enabled = true;
            else
                groupBox1.Enabled = false;


        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numDelayMax_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numDelayMin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nummaxfriend_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numMaxAddFriend_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chk3G_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkQuayVong_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numMaxError_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCookie_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
