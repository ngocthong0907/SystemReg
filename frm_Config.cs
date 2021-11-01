using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_Config : Form
    {
        public frm_Config()
        {
            InitializeComponent();
        }
        CauHinh cauhinh = new CauHinh();
        private Data dt = new Data();
        private DataTable configId = new DataTable();
        private void frmConfig_Load(object sender, EventArgs e)
        {
            if (SettingTool.configld.language == "English")
            {
                setupLanguage();
            }

            string[] arr = { "4.x", "3.x" };
            cboLDVersion.DataSource = arr;
            cboLDVersion.SelectedItem = 0;

            string[] arrSize = { "320", "240", "160" };
            cboSize.DataSource = arrSize;
            cboSize.SelectedItem = 0;

            method_Config();
            loadConfigLD();
            if (string.IsNullOrEmpty(SettingTool.note) == false)
            {
                if (SettingTool.note.Contains("vip"))
                {
                    chkRemovePost.Visible = true;
                    numRemovePost.Visible = true;
                    label12.Visible = true;
                }
            }
            string[] appfb = { "Facebook 251", "Facebook 299", "Facebook 302", "Facebook Lite" };
            cboAppFacebook.DataSource = appfb;
            cboAppFacebook.Text = SettingTool.configld.appversion;
        }

        private void method_SetupPathLD()
        {
            try
            {
                string pathld = "";

                SettingTool.configld = new ConfigLD();
                try
                {
                    string path = String.Format("{0}\\Config\\ConfigLD.data", Application.StartupPath);

                    using (StreamReader r = new StreamReader(path))
                    {
                        string json = r.ReadToEnd();
                        SettingTool.configld = JsonConvert.DeserializeObject<ConfigLD>(json);
                    }

                }
                catch (Exception ex)
                {
                    File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");

                }

                if (String.IsNullOrEmpty(SettingTool.configld.pathLD))
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Changzhi\\LDPlayer"))
                    {
                        if (key != null)
                        {
                            pathld = (string)key.GetValue("InstallDir");
                        }
                    }
                    SettingTool.pathfolderld = pathld;
                    pathld += "dnconsole.exe";
                }
                else
                {
                    SettingTool.pathfolderld = SettingTool.configld.pathLD;
                    pathld = SettingTool.configld.pathLD + "\\dnconsole.exe";
                }
                SettingTool.pathLD = pathld;
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
            }
        }
        private void method_Config()
        {
            try
            {
                cboConfig.Items.Clear();
                List<CauHinh> list_danhmuc = new List<CauHinh>();
                CauHinh_Bll cauhinh_bll = new CauHinh_Bll();
                list_danhmuc = cauhinh_bll.selectAll();
                ComboboxItem item = new ComboboxItem();
                item.Text = "Ngẫu Nhiên";
                // cboConfig.Items.Add(item);
                foreach (CauHinh dm in list_danhmuc)
                {
                    item = new ComboboxItem();

                    item.Text = dm.Name;
                    item.Value = dm.ID;
                    item.Tag = dm;
                    cboConfig.Items.Add(item);

                }
                if (cboConfig.Items.Count > 0)
                {
                    cboConfig.SelectedIndex = 0;
                    cboConfig.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
            }
        }
        public void saveConfigLD()
        {
            try
            {
                SettingTool.configld = new ConfigLD();
                SettingTool.configld.numthread = (int)numThead.Value;
                SettingTool.configld.timedelay = (int)numDelayLD.Value;
                SettingTool.configld.numRunLD = (int)numRunLD.Value;
                SettingTool.configld.pathLD = txtPathLD.Text.Trim();
                SettingTool.configld.cookies = txtCookies.Text.Trim();
                SettingTool.configld.pathHMA = txtPathHma.Text.Trim();
                SettingTool.configld.accountIP = (int)numAccountIp.Value;

                SettingTool.configld.pathsavedata = txtPathData.Text.Trim();

                if (chkSaveToken.Checked)
                {
                    SettingTool.configld.has_savetoken = true;
                }
                else
                {
                    SettingTool.configld.has_savetoken = false;
                }
                if (chkQuitLD.Checked)
                {
                    SettingTool.configld.has_quitLD = true;
                }
                else
                {
                    SettingTool.configld.has_quitLD = false;
                }

                if (rbNoip.Checked)
                {
                    SettingTool.configld.typeip = 1;
                }
                if (rbHMA.Checked)
                {
                    SettingTool.configld.typeip = 2;
                }
                if (rbDcom.Checked)
                {
                    SettingTool.configld.typeip = 3;
                }
                if (rbVPN111.Checked)
                {
                    SettingTool.configld.typeip = 4;
                }
                if (rbProxy.Checked)
                {
                    SettingTool.configld.typeip = 5;
                }
                if (rbTinsoft.Checked)
                {
                    SettingTool.configld.typeip = 6;
                }
                if (rbxProxy.Checked)
                {
                    SettingTool.configld.typeip = 7;
                }
                if (rdTmproxy.Checked)
                {
                    SettingTool.configld.typeip = 8;
                }
                if (rdNinjaProxy.Checked)
                {
                    SettingTool.configld.typeip = 9;
                }
                if (rbOBCProxy.Checked)
                {
                    SettingTool.configld.typeip = 10;
                }

                if (rbProxyv6.Checked)
                {
                    SettingTool.configld.typeip = 11;
                }

               

                if (rbdefaultapi.Checked)
                {
                    SettingTool.configld.typedefaulV6 = 1;
                }
                else
                {
                    SettingTool.configld.typedefaulV6 = 2;
                }

                SettingTool.configld.apiproxyv6 = txtproxyv6.Text;

                SettingTool.configld.delaydcomxproxy = (int) numDelayDcomxproxy.Value;

                SettingTool.configld.apitinsoft = txtApiTinsoft.Text.Trim();
                SettingTool.configld.delaydcom = (int)numDelayDcom.Value;
                SettingTool.configld.linkxproxy = txtLinkxProxy.Text.Trim();
                SettingTool.configld.versionld = cboLDVersion.Text.Trim();
                SettingTool.configld.language = cboLanguage.Text.Trim();

                SettingTool.configld.proxytype = cboproxytype.Text;

                SettingTool.configld.appproxy = cboApp.Text;

                ComboboxItem item2 = (ComboboxItem)cboLocation.SelectedItem;
                SettingTool.configld.apiTMproxy = txtTmproxy.Text;
                try
                {
                    TinsoftLocation ts = (TinsoftLocation)item2.Tag;
                    SettingTool.configld.tinsoftname = ts.name;
                    SettingTool.configld.tinsoftid = ts.id;
                }
                catch
                {
                    SettingTool.configld.tinsoftid = "0";
                }
                SettingTool.configld.token = txtToken.Text.Trim();
                SettingTool.configld.timeout = (int)numTimeOut.Value;
                if (chkSock5.Checked)
                {
                    SettingTool.configld.sock5 = true;
                }
                else
                {
                    SettingTool.configld.sock5 = false;
                }
                SettingTool.configld.appversion = cboAppFacebook.Text.Trim();
                SettingTool.configld.linkobc = txtLinkObc.Text.Trim();
                SettingTool.configld.sizeld = cboSize.Text.Trim();

                if (chkCheckProxy.Checked)
                {
                    SettingTool.configld.checkproxy = true;
                }
                else
                {
                    SettingTool.configld.checkproxy = false;
                }
                if(SettingTool.configld.appversion=="Facebook Lite")
                {
                    SettingTool.configld.package = "com.facebook.lite";
                }
                else
                {
                    SettingTool.configld.package = "com.facebook.katana";
                }
              
                string path = String.Format("{0}\\Config\\ConfigLD.data", Application.StartupPath);
                File.WriteAllText(path, JsonConvert.SerializeObject(SettingTool.configld));
                // MessageBox.Show("Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
            }
        }
        private void loadConfigLD()
        {
            try
            {
                SettingTool.configld = new ConfigLD();
                string path = String.Format("{0}\\Config\\ConfigLD.data", Application.StartupPath);

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    SettingTool.configld = JsonConvert.DeserializeObject<ConfigLD>(json);
                }
                numThead.Value = SettingTool.configld.numthread;
                numDelayLD.Value = SettingTool.configld.timedelay;
                txtPathLD.Text = SettingTool.configld.pathLD;
                txtCookies.Text = SettingTool.configld.cookies;
                chkSaveToken.Checked = SettingTool.configld.has_savetoken;
                chkQuitLD.Checked = SettingTool.configld.has_quitLD;
                txtPathHma.Text = SettingTool.configld.pathHMA;
                txtPathData.Text = SettingTool.configld.pathsavedata ;
                numAccountIp.Value = SettingTool.configld.accountIP;
                numRunLD.Value = SettingTool.configld.numRunLD;

                if (SettingTool.configld.typeip == 1)
                {
                    rbNoip.Checked = true;
                }
                if (SettingTool.configld.typeip == 2)
                {
                    rbHMA.Checked = true;
                }
                if (SettingTool.configld.typeip == 3)
                {
                    rbDcom.Checked = true;
                }
                if (SettingTool.configld.typeip == 4)
                {
                    rbVPN111.Checked = true;
                }
                if (SettingTool.configld.typeip == 5)
                {
                    rbProxy.Checked = true;
                }
                if (SettingTool.configld.typeip == 6)
                {
                    rbTinsoft.Checked = true;
                }
                if (SettingTool.configld.typeip == 7)
                {
                    rbxProxy.Checked = true;
                }

                if (SettingTool.configld.typeip == 8)
                {
                    rdTmproxy.Checked = true;
                }
                if (SettingTool.configld.typeip == 9)
                {
                    rdNinjaProxy.Checked = true;
                }
                if (SettingTool.configld.typeip == 10)
                {
                    rbOBCProxy.Checked = true;
                }

                if (SettingTool.configld.typeip == 11 )
                {
                    rbProxyv6.Checked = true;
                }

                if (SettingTool.configld.typedefaulV6 == 1)
                {
                    rbdefaultapi.Checked = true;
                }
                else
                {
                    rbdefaultlist.Checked = true;
                }

                txtproxyv6.Text = SettingTool.configld.apiproxyv6;

                numDelayDcomxproxy.Value = SettingTool.configld.delaydcomxproxy;

                txtTmproxy.Text = SettingTool.configld.apiTMproxy;
                txtApiTinsoft.Text = SettingTool.configld.apitinsoft;
                numDelayDcom.Value = SettingTool.configld.delaydcom;
                txtLinkxProxy.Text = SettingTool.configld.linkxproxy;
                if (string.IsNullOrEmpty(SettingTool.configld.versionld))
                {
                    SettingTool.configld.versionld = "4.x";
                    cboLDVersion.Text = "4.x";
                }
                else
                {
                    cboLDVersion.Text = SettingTool.configld.versionld;
                }

                if (string.IsNullOrEmpty(SettingTool.configld.language))
                {
                    SettingTool.configld.language = "Tiếng việt";
                    cboLanguage.Text = "Tiếng việt";
                }
                else
                {
                    cboLanguage.Text = SettingTool.configld.language;
                }

                cboproxytype.Text = SettingTool.configld.proxytype;
                cboApp.Text = SettingTool.configld.appproxy;
                txtToken.Text = SettingTool.configld.token;
                numTimeOut.Value = SettingTool.configld.timeout;
                chkSock5.Checked = SettingTool.configld.sock5;

                txtLinkObc.Text = SettingTool.configld.linkobc;
                if (string.IsNullOrEmpty(SettingTool.configld.sizeld))
                {
                    SettingTool.configld.sizeld = "320";

                }
                else
                {
                    cboSize.Text = SettingTool.configld.sizeld;
                }
                if(SettingTool.configld.appversion=="Facebook Lite")
                {
                    SettingTool.configld.package = "com.facebook.lite";
                }
                else
                {
                    SettingTool.configld.package = "com.facebook.katana";
                }
                chkCheckProxy.Checked = SettingTool.configld.checkproxy;
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
            }
        }
        public void method_SaveTuongTacNgay(int id)
        {
            SettingTuongTac settingtuongtac = new SettingTuongTac();
            settingtuongtac.delaymin = (int)numDelayMin.Value;
            settingtuongtac.delaymax = (int)numDelayMax.Value;
            settingtuongtac.action = (int)numAction.Value;
            settingtuongtac.nameConfig = txt_name_config.Text;

            settingtuongtac.numslidemin = (int)numSlideMin.Value;
            settingtuongtac.numslidemax = (int)numSlideMax.Value;
            settingtuongtac.getBirthday = chkgetBirthday.Checked;

            if (chkLuotNewfeed.Checked)
                settingtuongtac.luotnewfeed = true;
            else
                settingtuongtac.luotnewfeed = false;

            settingtuongtac.luotStory = chkluotStory.Checked;

            settingtuongtac.numslidestorymin = (int)numluotStoryMin.Value;
            settingtuongtac.numslidestorymax = (int)numluotStoryMax.Value;
            if (chkLikeNewFeed.Checked)
                settingtuongtac.likenewfeed = true;
            else
                settingtuongtac.likenewfeed = false;
            settingtuongtac.numlikenewfeedmin = (int)numLikeNewFeedMin.Value;
            settingtuongtac.numlikenewfeedmax = (int)numLikeNewFeedMax.Value;
            if (chkCommentNewfeed.Checked)
                settingtuongtac.commentnewfeed = true;
            else
                settingtuongtac.commentnewfeed = false;
            settingtuongtac.numcommentnewfeedmin = (int)numCommentNewfeedsMin.Value;
            settingtuongtac.numcommentnewfeedmax = (int)numCommentNewfeedsMax.Value;
            settingtuongtac.message = txtMess.Text.Trim();

            if (chkSharePost.Checked)
                settingtuongtac.sharepost = true;
            else
                settingtuongtac.sharepost = false;
            settingtuongtac.numsharepostmin = (int)numSharePostMin.Value;
            settingtuongtac.numsharepostmax = (int)numSharePostMax.Value;

            if (chkAddFriend.Checked)
                settingtuongtac.addfriend = true;
            else
                settingtuongtac.addfriend = false;
            settingtuongtac.numaddfriendmin = (int)numAddFriendMin.Value;
            settingtuongtac.numaddfriendmax = (int)numAddFriendMax.Value;

            if (chkAddFriendbyNewfeed.Checked)
                settingtuongtac.addfriendNewfeed = true;
            else
                settingtuongtac.addfriendNewfeed = false;
            settingtuongtac.numaddfriendNewfeedmin = (int)numAddfriendbyNewfeedMin.Value;
            settingtuongtac.numaddfriendNewfeedmax = (int)numAddfriendbyNewfeedMax.Value;

            if (chkAddfriendUID.Checked)
                settingtuongtac.chkAddFriendUID = true;
            else
                settingtuongtac.chkAddFriendUID = false;
            settingtuongtac.numaddfrienduidmin = (int)numAddfriendUIDMin.Value;
            settingtuongtac.numaddfrienduidmax = (int)numAddfriendUIDMax.Value;

            settingtuongtac.numcancelfrienduidmin = (int)numcancelfriendMin.Value;
            settingtuongtac.numcancelfrienduidmax = (int)numcancelfriendMax.Value;
            settingtuongtac.chkCanceldFriendUID = chkcancelFriend.Checked;
            settingtuongtac.pathCancelFriendUID = txt_pathUIDcancel.Text;
            settingtuongtac.chkCanceldFriendRandom = chkcancelrandom.Checked;

            settingtuongtac.chklikestory = chklikestory.Checked;
            settingtuongtac.chkcommentstory = chkcommentstory.Checked;
            settingtuongtac.numlikestorymin = (int)numlikestoryMin.Value;
            settingtuongtac.numlikestorymax = (int)numlikestoryMax.Value;
            settingtuongtac.numcommentstorymin = (int)numcommentstoryMin.Value;
            settingtuongtac.numcommentstorymax = (int)numcommentstoryMax.Value;

            settingtuongtac.chkcommentImage = chkcommentImage.Checked;
            settingtuongtac.chkdelcommentImage = chkdeleteIamgecomment.Checked;
            settingtuongtac.pathImagecomment = txtPathIamgecomment.Text;
            settingtuongtac.numcommentImage = (int)numcommentImage.Value;
            settingtuongtac.numcommentImage_max = (int)numcommentImage_max.Value;
            settingtuongtac.chkcontentImage = chkContentImange.Checked;
            
            //comment image group
            settingtuongtac.chkcommentImage_gr = chkcommentImage_gr.Checked;
            settingtuongtac.chkdelcommentImage_gr =chkdeleteIamgecomment_gr.Checked ;
            settingtuongtac.pathImagecomment_gr = txtPathIamgecomment_gr.Text ;
            settingtuongtac.numcommentImage_gr = (int)numcommentImage_gr.Value;
            settingtuongtac.numcommentImage_max_gr = (int) numcommentImage_max_gr.Value;
            settingtuongtac.chkcontentImage_gr = chkContentImange_gr.Checked;

            if (chkAcceptFriend.Checked)
                settingtuongtac.acceptfriend = true;
            else
                settingtuongtac.acceptfriend = false;
            settingtuongtac.numacceptfriendmin = (int)numAcceptFriendMin.Value;
            settingtuongtac.numacceptfriendmax = (int)numAcceptFriendMax.Value;

            if (chkcancelRequest.Checked)
                settingtuongtac.cancelrequest = true;
            else
                settingtuongtac.cancelrequest = false;
            settingtuongtac.numcacelrequestmin = (int)numcacelRequestMin.Value;
            settingtuongtac.numcacelrequestmax = (int)numcacelRequestMax.Value;
            settingtuongtac.chkjoingruoupsuggest = chkjoingroupsuggest.Checked;
            settingtuongtac.numjoingroupsuggestmin = (int)numjoingroupsuggestmin.Value;
            settingtuongtac.numjoingroupsuggestmax = (int)numjoingroupsuggestmax.Value;
            //fanpage

            settingtuongtac.strkeywordfanpage = txtSeachFanpage.Text.Trim();
            if (chkLikePage.Checked)
                settingtuongtac.likefanpage = true;
            else
                settingtuongtac.likefanpage = false;
            settingtuongtac.numlikefanpagemin = (int)numLikeFanpageMin.Value;
            settingtuongtac.numlikefanpagemax = (int)numLikeFanpageMax.Value;
            if (chkLikePostFanpage.Checked)
                settingtuongtac.likepostfanpage = true;
            else
                settingtuongtac.likepostfanpage = false;
            settingtuongtac.numlikepostfanpagemin = (int)numLikePostFanpageMin.Value;
            settingtuongtac.numlikepostfanpagemax = (int)numLikePostFanpageMax.Value;

            if (chkCommentPostFanpage.Checked)
                settingtuongtac.commentpostfanpage = true;
            else
                settingtuongtac.commentpostfanpage = false;
            settingtuongtac.numcommentpostfanpagemin = (int)numCommentPostFanpageMin.Value;
            settingtuongtac.numcommentpostfanpagemax = (int)numCommentPostFanpageMax.Value;

            if (chkReadNoti.Checked)
                settingtuongtac.readnoti = true;
            else
                settingtuongtac.readnoti = false;
            //group

            settingtuongtac.strkeywordseach = txtKeywordSeach.Text.Trim();

            settingtuongtac.strseachInfo = txtSeachInfo.Text.Trim();

            if (chkJoinGroupKeyword.Checked)
                settingtuongtac.joingroupkeyword = true;
            else
                settingtuongtac.joingroupkeyword = false;
            settingtuongtac.numjoingroupkeywordmin = (int)numJoinGroupKeywordMin.Value;
            settingtuongtac.numjoingroupkeywordmax = (int)numJoinGroupKeywordMax.Value;
            if (chkscrollgroup.Checked)
                settingtuongtac.chkscrollgroup = true;
            else
                settingtuongtac.chkscrollgroup = false;
            settingtuongtac.numscrollgroupmin = (int)numScrollgroupMin.Value;
            settingtuongtac.numscrollgroupmax = (int)numScrollgroupMax.Value;

            if (chkLikePostGroup.Checked)
                settingtuongtac.likepostgroup = true;
            else
                settingtuongtac.likepostgroup = false;
            settingtuongtac.numlikepostgroupmin = (int)numLikePostGroupMin.Value;
            settingtuongtac.numlikepostgroupmax = (int)numLikePostGroupMax.Value;
            if (chkCommentPostGroup.Checked)
                settingtuongtac.commentpostgroup = true;
            else
                settingtuongtac.commentpostgroup = false;
            settingtuongtac.numcommentpostgroupmin = (int)numCommentPostGroupMin.Value;
            settingtuongtac.numcommentpostgroupmax = (int)numCommentPostGroupMax.Value;
            //fanpage
            //fanpage


            //join group by UID
            if (chkUID.Checked)
                settingtuongtac.chkUID = true;
            else
                settingtuongtac.chkUID = false;
            settingtuongtac.strPath = txtPathAdd.Text;
            if (chkAutoAnwser.Checked)
                settingtuongtac.chkAutoAnwser = true;
            else
                settingtuongtac.chkAutoAnwser = false;

            settingtuongtac.numGroupUIDMin = (int)numGroupUIDMin.Value;
            settingtuongtac.numGroupUIDMax = (int)numGroupUIDMax.Value;
            settingtuongtac.strPathAnswer = txtAnswer.Text;
            settingtuongtac.strPathCommentGroup = txtPathcommentGroup.Text;
            //loop tuong tac
            if (chkLoop_tuongtac.Checked)
                settingtuongtac.chkLoop_tuongtac = true;
            else
                settingtuongtac.chkLoop_tuongtac = false;

            settingtuongtac.numLoop_tuongtac = (int)numLoop_tuongtac.Value;


            //join group by UID


            settingtuongtac.timestart = (int)numTimeStart.Value;
            settingtuongtac.timestop = (int)numTimeStop.Value;

            settingtuongtac.numthread = (int)numThead.Value;
            settingtuongtac.numdelayld = (int)numDelayLD.Value;


            if (chkRunRandom.Checked)
                settingtuongtac.runRandom = true;
            else
                settingtuongtac.runRandom = false;

            if (chkSearch.Checked)
                settingtuongtac.searchInfo = true;
            else
                settingtuongtac.searchInfo = false;

            if (chkLikecommentGroupId.Checked)
                settingtuongtac.likeCommentGroupId = true;
            else
                settingtuongtac.likeCommentGroupId = false;

            settingtuongtac.pathGroupIdLikecomment = txt_pathGroupId.Text;


            if (chkMessenger.Checked)
                settingtuongtac.chkMessenger = true;
            else
                settingtuongtac.chkMessenger = false;
            settingtuongtac.numMessenger = (int)numMessenger.Value;

            settingtuongtac.pathReaction = txtPathReaction.Text;

            //watch
            if (chkNewfeedWatch.Checked)
            {
                settingtuongtac.has_newfeed_watch = true;
            }
            else
            {
                settingtuongtac.has_newfeed_watch = false;
            }
            settingtuongtac.timenewfeedwatchmin = (int)numTimeNewfeedWatchMin.Value;
            settingtuongtac.timenewfeedwatchmax = (int)numTimeNewfeedWatchMax.Value;
            if (chkLikeWatch.Checked)
            {
                settingtuongtac.has_like_newfeed_watch = true;
            }
            else
            {
                settingtuongtac.has_like_newfeed_watch = false;
            }
            settingtuongtac.likenewfeedwatchmin = (int)numLikeNewfeedWatchMin.Value;
            settingtuongtac.likenewfeedwatchmax = (int)numLikeNewfeedWatchMax.Value;
            //group
            if (chkNewfeedMarketPlace.Checked)
            {
                settingtuongtac.has_newfeed_marketplace = true;
            }
            else
            {
                settingtuongtac.has_newfeed_marketplace = false;
            }
            settingtuongtac.timenewfeedmarketplacemin = (int)numTimeNewfeedMarketplaceMin.Value;
            settingtuongtac.timenewfeedmarketplacemax = (int)numTimeNewfeedMarketplaceMax.Value;
            if (chkLikeNewfeedGroup.Checked)
            {
                settingtuongtac.has_like_newfeed_group = true;
            }
            else
            {
                settingtuongtac.has_like_newfeed_group = false;
            }
            settingtuongtac.num_like_newfeed_groupmin = (int)numLikeNewfeedGroupMin.Value;
            settingtuongtac.num_like_newfeed_groupmax = (int)numLikeNewfeedGroupMax.Value;

            //remove group
            if (chkRemoveGroup.Checked)
            {
                settingtuongtac.removegroup = true;
            }
            else
            {
                settingtuongtac.removegroup = false;
            }
            settingtuongtac.numremovegroup_min = (int)numRemoveGroup.Value;
            settingtuongtac.numremovegroup_max = (int)numRemoveGroup_max.Value;

            if (chkChoDuyet.Checked)
            {
                settingtuongtac.remove_choduyet = true;
            }
            else
            {
                settingtuongtac.remove_choduyet = false;
            }
            if (chkRemoveMember.Checked)
            {
                settingtuongtac.remove_thanhvien = true;
            }
            else
            {
                settingtuongtac.remove_thanhvien = false;
            }
            settingtuongtac.remove_num_thanhvien = (int)numRemoveThanhVien.Value;

            if (chkRemovePost.Checked)
            {
                settingtuongtac.remove_post = true;
            }
            else
            {
                settingtuongtac.remove_post = false;
            }
            settingtuongtac.remove_num_post = (int)numRemovePost.Value;

            if (chkJoinGroupNoApprove.Checked)
            {
                settingtuongtac.joingroupnoapprove = true;
            }
            else
            {
                settingtuongtac.joingroupnoapprove = false;
            }
            //post group
            if (chkPostGroup.Checked)
            {
                settingtuongtac.postgroup = true;
            }
            else
            {
                settingtuongtac.postgroup = false;
            }
            settingtuongtac.postgroup_minday = (int)numPostGroupDay.Value;
            settingtuongtac.postgroup_maxday = (int)numPostGroupMax.Value;

            if (chkRandomPostGroup.Checked)
            {
                settingtuongtac.postgrouprandompost = true;
            }
            else
            {
                settingtuongtac.postgrouprandompost = false;
            }

            //post profile
            if (chkPostProfile.Checked)
            {
                settingtuongtac.postprofile = true;
            }
            else
            {
                settingtuongtac.postprofile = false;
            }
            settingtuongtac.postprofile_minday = (int)numPostProfile.Value;
            settingtuongtac.postprofile_maxday = (int)numMaxPostProfile.Value;
            if (chkRandomPost.Checked)
            {
                settingtuongtac.postprofile_randompost = true;
            }
            else
            {
                settingtuongtac.postprofile_randompost = false;
            }

            if (chkRunRandomTuongTac.Checked)
            {
                settingtuongtac.runrandomtuongtac = true;
            }
            else
            {
                settingtuongtac.runrandomtuongtac = false;
            }
            settingtuongtac.numrandomtuongtac = (int)numRandomTuongTac.Value;

            if (chkKhangSpam.Checked)
            {
                settingtuongtac.khangspam = true;
            }
            else
            {
                settingtuongtac.khangspam = false;
            }
            settingtuongtac.nummaxtuongtac = (int)numMaxTuongTac.Value;

            settingtuongtac.numaddfriendgroupmin = (int)numaddfriendingroupMin.Value;
            settingtuongtac.numaddfriendgroupmax = (int)numaddfriendingroupMax.Value;

            settingtuongtac.has_addfriendgroup = chkaddfriendinGroup.Checked;

            if(chkLikeAvatar.Checked)
            {
                settingtuongtac.likeavatar = true; 
            }
            else
            {
                settingtuongtac.likeavatar = false;
            }

            if (chkCommentNewfeedGroup.Checked)
            {
                settingtuongtac.commentnewfeedgroup = true;
            }
            else
            {
                settingtuongtac.commentnewfeedgroup = false;
            }
            settingtuongtac.numcommentnewfeedgroupmin = (int)numCommentNewfeedGroupMin.Value;
            settingtuongtac.numcommentnewfeedgroupmax = (int)numCommentNewfeedGroupMax.Value;

            string path = String.Format("{0}\\Config\\{1}.data", Application.StartupPath, id);
            File.WriteAllText(path, JsonConvert.SerializeObject(settingtuongtac));

            if (SettingTool.configld.language == "English")
                MessageBox.Show("Save Successful", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else

                MessageBox.Show("Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void method_LoadTuongTacNgay(int id)
        {
            try
            {

                string path = String.Format("{0}\\Config\\{1}.data", Application.StartupPath, id);
                SettingTuongTac settingtuongtac = new SettingTuongTac();
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    settingtuongtac = JsonConvert.DeserializeObject<SettingTuongTac>(json);
                }
                txt_name_config.Text = settingtuongtac.nameConfig;
                numDelayMin.Value = settingtuongtac.delaymin;
                numDelayMax.Value = settingtuongtac.delaymax;
                chkReadNoti.Checked = settingtuongtac.readnoti;
                numAction.Value = settingtuongtac.action;

                chkLuotNewfeed.Checked = settingtuongtac.luotnewfeed;
                chkLikeNewFeed.Checked = settingtuongtac.likenewfeed;
                numLikeNewFeedMin.Value = settingtuongtac.numlikenewfeedmin;
                numLikeNewFeedMax.Value = settingtuongtac.numlikenewfeedmax;
                numSlideMin.Value = settingtuongtac.numslidemin;
                numSlideMax.Value = settingtuongtac.numslidemax;
                chklikestory.Checked = settingtuongtac.chklikestory;
                chkcommentstory.Checked = settingtuongtac.chkcommentstory;
                numlikestoryMin.Value = settingtuongtac.numlikestorymin;
                numlikestoryMax.Value = settingtuongtac.numlikestorymax;

                numcommentstoryMin.Value = settingtuongtac.numcommentstorymin;
                numcommentstoryMax.Value = settingtuongtac.numcommentstorymax;

                chkcommentImage.Checked = settingtuongtac.chkcommentImage;
                chkdeleteIamgecomment.Checked = settingtuongtac.chkdelcommentImage;
                txtPathIamgecomment.Text = settingtuongtac.pathImagecomment;
                numcommentImage.Value = settingtuongtac.numcommentImage;
                numcommentImage_max.Value = settingtuongtac.numcommentImage_max;
                chkContentImange.Checked = settingtuongtac.chkcontentImage;

                //comment image group
                chkcommentImage_gr.Checked = settingtuongtac.chkcommentImage_gr;
                chkdeleteIamgecomment_gr.Checked = settingtuongtac.chkdelcommentImage_gr;
                txtPathIamgecomment_gr.Text = settingtuongtac.pathImagecomment_gr;
                numcommentImage_gr.Value = settingtuongtac.numcommentImage_gr;
                numcommentImage_max_gr.Value = settingtuongtac.numcommentImage_max_gr;
                chkContentImange_gr.Checked = settingtuongtac.chkcontentImage_gr;

                chkaddfriendinGroup.Checked = settingtuongtac.has_addfriendgroup;

                chkluotStory.Checked = settingtuongtac.luotStory;
                numluotStoryMin.Value = settingtuongtac.numslidestorymin;
                numluotStoryMax.Value = settingtuongtac.numslidestorymax;
                chkgetBirthday.Checked = settingtuongtac.getBirthday;

                chkCommentNewfeed.Checked = settingtuongtac.commentnewfeed;
                numCommentNewfeedsMin.Value = settingtuongtac.numcommentnewfeedmin;
                numCommentNewfeedsMax.Value = settingtuongtac.numcommentnewfeedmax;
                txtMess.Text = settingtuongtac.message;

                chkSharePost.Checked = settingtuongtac.sharepost;
                numSharePostMin.Value = settingtuongtac.numsharepostmin;
                numSharePostMax.Value = settingtuongtac.numsharepostmax;
                chkAddFriend.Checked = settingtuongtac.addfriend;
                numAddFriendMin.Value = settingtuongtac.numaddfriendmin;
                numAddFriendMax.Value = settingtuongtac.numaddfriendmax;
                chkAddFriendbyNewfeed.Checked = settingtuongtac.addfriendNewfeed;
                numAddfriendbyNewfeedMin.Value = settingtuongtac.numaddfriendNewfeedmin;
                numAddfriendbyNewfeedMax.Value = settingtuongtac.numaddfriendNewfeedmax;
                chkAcceptFriend.Checked = settingtuongtac.acceptfriend;
                numAcceptFriendMin.Value = settingtuongtac.numacceptfriendmin;
                numAcceptFriendMax.Value = settingtuongtac.numacceptfriendmax;

                chkcancelRequest.Checked = settingtuongtac.cancelrequest;
                numcacelRequestMin.Value = settingtuongtac.numcacelrequestmin;
                numcacelRequestMax.Value = settingtuongtac.numcacelrequestmax;
                chkAddfriendUID.Checked = settingtuongtac.chkAddFriendUID;
                numAddfriendUIDMin.Value = settingtuongtac.numaddfrienduidmin;
                numAddfriendUIDMax.Value = settingtuongtac.numaddfrienduidmax;
                numcancelfriendMin.Value = settingtuongtac.numcancelfrienduidmin;
                numcancelfriendMax.Value = settingtuongtac.numcancelfrienduidmax;
                chkcancelFriend.Checked = settingtuongtac.chkCanceldFriendUID;
                txt_pathUIDcancel.Text = settingtuongtac.pathCancelFriendUID;

                chkcancelrandom.Checked = settingtuongtac.chkCanceldFriendRandom;

                chkjoingroupsuggest.Checked = settingtuongtac.chkjoingruoupsuggest;
                numjoingroupsuggestmin.Value = settingtuongtac.numjoingroupsuggestmin;
                numjoingroupsuggestmax.Value = settingtuongtac.numjoingroupsuggestmax;
                //fanpage
                txtSeachFanpage.Text = settingtuongtac.strkeywordfanpage;
                chkLikePage.Checked = settingtuongtac.likefanpage;
                numLikeFanpageMin.Value = settingtuongtac.numlikefanpagemin;
                numLikeFanpageMax.Value = settingtuongtac.numlikefanpagemax;
                chkLikePostFanpage.Checked = settingtuongtac.likepostfanpage;
                numLikePostFanpageMin.Value = settingtuongtac.numlikepostfanpagemin;
                numLikePostFanpageMax.Value = settingtuongtac.numlikepostfanpagemax;
                chkCommentPostFanpage.Checked = settingtuongtac.commentpostfanpage;
                numCommentPostFanpageMin.Value = settingtuongtac.numcommentpostfanpagemin;
                numCommentPostFanpageMax.Value = settingtuongtac.numcommentpostfanpagemax;
                //group

                txtKeywordSeach.Text = settingtuongtac.strkeywordseach;
                txtSeachInfo.Text = settingtuongtac.strseachInfo;

                chkJoinGroupKeyword.Checked = settingtuongtac.joingroupkeyword;
                numJoinGroupKeywordMin.Value = settingtuongtac.numjoingroupkeywordmin;
                numJoinGroupKeywordMax.Value = settingtuongtac.numjoingroupkeywordmax;
                chkLikePostGroup.Checked = settingtuongtac.likepostgroup;
                numLikePostGroupMin.Value = settingtuongtac.numlikepostgroupmin;
                numLikePostGroupMax.Value = settingtuongtac.numlikepostgroupmax;
                chkCommentPostGroup.Checked = settingtuongtac.commentpostgroup;
                numCommentPostGroupMin.Value = settingtuongtac.numcommentpostgroupmin;
                numCommentPostGroupMax.Value = settingtuongtac.numcommentpostgroupmax;

                numScrollgroupMin.Value = settingtuongtac.numscrollgroupmin;
                numScrollgroupMax.Value = settingtuongtac.numscrollgroupmax;
                chkscrollgroup.Checked = settingtuongtac.chkscrollgroup;
                //join group by UID
                chkUID.Checked = settingtuongtac.chkUID;
                txtPathAdd.Text = settingtuongtac.strPath;
                numGroupUIDMin.Value = settingtuongtac.numGroupUIDMin;
                numGroupUIDMax.Value = settingtuongtac.numGroupUIDMax;
                chkAutoAnwser.Checked = settingtuongtac.chkAutoAnwser;
                txtAnswer.Text = settingtuongtac.strPathAnswer;
                txtPathcommentGroup.Text = settingtuongtac.strPathCommentGroup;
                //3g


                //loop tuong tac
                chkLoop_tuongtac.Checked = settingtuongtac.chkLoop_tuongtac;
                numLoop_tuongtac.Value = settingtuongtac.numLoop_tuongtac;

                numTimeStart.Value = settingtuongtac.timestart;
                numTimeStop.Value = settingtuongtac.timestop;

                numThead.Value = settingtuongtac.numthread;
                numDelayLD.Value = settingtuongtac.numdelayld;

                chkRunRandom.Checked = settingtuongtac.runRandom;

                chkSearch.Checked = settingtuongtac.searchInfo;

                chkLikecommentGroupId.Checked = settingtuongtac.likeCommentGroupId;
                txt_pathGroupId.Text = settingtuongtac.pathGroupIdLikecomment;

                chkMessenger.Checked = settingtuongtac.chkMessenger;
                numMessenger.Value = settingtuongtac.numMessenger;
                txtPathReaction.Text = settingtuongtac.pathReaction;

                chkNewfeedWatch.Checked = settingtuongtac.has_newfeed_watch;
                numTimeNewfeedWatchMin.Value = settingtuongtac.timenewfeedwatchmin;
                numTimeNewfeedWatchMax.Value = settingtuongtac.timenewfeedwatchmax;

                chkLikeWatch.Checked = settingtuongtac.has_like_newfeed_watch;

                numLikeNewfeedWatchMin.Value = settingtuongtac.likenewfeedwatchmin;
                numLikeNewfeedWatchMax.Value = settingtuongtac.likenewfeedwatchmax;

                chkNewfeedMarketPlace.Checked = settingtuongtac.has_newfeed_marketplace;
                numTimeNewfeedMarketplaceMin.Value = settingtuongtac.timenewfeedmarketplacemin;
                numTimeNewfeedMarketplaceMax.Value = settingtuongtac.timenewfeedmarketplacemax;

                chkLikeNewfeedGroup.Checked = settingtuongtac.has_like_newfeed_group;
                numLikeNewfeedGroupMin.Value = settingtuongtac.num_like_newfeed_groupmin;
                numLikeNewfeedGroupMax.Value = settingtuongtac.num_like_newfeed_groupmax;
                //remove group
                //remove group
                chkRemoveGroup.Checked = settingtuongtac.removegroup;
                numRemoveGroup.Value = settingtuongtac.numremovegroup_min;
                numRemoveGroup_max.Value = settingtuongtac.numremovegroup_max;
                chkChoDuyet.Checked = settingtuongtac.remove_choduyet;
                chkRemoveMember.Checked = settingtuongtac.remove_thanhvien;
                numRemoveThanhVien.Value = settingtuongtac.remove_num_thanhvien;
                chkRemovePost.Checked = settingtuongtac.remove_post;
                numRemovePost.Value = settingtuongtac.remove_num_post;

                chkJoinGroupNoApprove.Checked = settingtuongtac.joingroupnoapprove;

                //post group
                chkPostGroup.Checked = settingtuongtac.postgroup;
                numPostGroupDay.Value = settingtuongtac.postgroup_minday;
                numPostGroupMax.Value = settingtuongtac.postgroup_maxday;
                chkRandomPostGroup.Checked = settingtuongtac.postgrouprandompost;



                //post profile
                chkPostProfile.Checked = settingtuongtac.postprofile;
                numPostProfile.Value = settingtuongtac.postprofile_minday;
                numMaxPostProfile.Value = settingtuongtac.postprofile_maxday;
                chkRandomPost.Checked = settingtuongtac.postprofile_randompost;


                chkRunRandomTuongTac.Checked = settingtuongtac.runrandomtuongtac;
                numRandomTuongTac.Value = settingtuongtac.numrandomtuongtac;

                chkKhangSpam.Checked = settingtuongtac.khangspam;
                numMaxTuongTac.Value = settingtuongtac.nummaxtuongtac;

                numaddfriendingroupMin.Value = settingtuongtac.numaddfriendgroupmin;
                numaddfriendingroupMax.Value = settingtuongtac.numaddfriendgroupmax;

                chkLikeAvatar.Checked = settingtuongtac.likeavatar;

                chkCommentNewfeedGroup.Checked=settingtuongtac.commentnewfeedgroup;
               
                numCommentNewfeedGroupMin.Value=settingtuongtac.numcommentnewfeedgroupmin;
                numCommentNewfeedGroupMax.Value=settingtuongtac.numcommentnewfeedgroupmax;
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
            }
        }
        private void unCheck()
        {
            chkLuotNewfeed.Checked = false;
            chkRunRandom.Checked = false;
            chkSearch.Checked = false;
            chkLikeNewFeed.Checked = false;
            chkCommentNewfeed.Checked = false;

            chkSharePost.Checked = false;

            chkLikePage.Checked = false;
            chkLikePostFanpage.Checked = false;
            chkCommentPostFanpage.Checked = false;

            chkAddFriend.Checked = false;
            chkAcceptFriend.Checked = false;

            chkLikePostGroup.Checked = false;
            chkCommentPostGroup.Checked = false;
            chkJoinGroupKeyword.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //saveConfigLD();
                //method_SetupPathLD();
                if (chkUID.Checked)
                {
                    if (txtPathAdd.Text == "Chọn đường dẫn" || txtPathAdd.Text == "")
                    {
                        if (SettingTool.configld.language == "English")
                            MessageBox.Show("Let's choose file group Id");
                        else
                            MessageBox.Show("Hãy chọn file group Id");
                        return;
                    }
                    else
                    {
                        if (!File.Exists(txtPathAdd.Text))
                        {
                            if (SettingTool.configld.language == "English")
                                MessageBox.Show("Let's choose file group Id");
                            else
                                MessageBox.Show("Hãy chọn file group Id");
                            return;
                        }
                    }

                }

                if (chkLikePage.Checked)
                {
                    if (txtSeachFanpage.Text == "")
                    {
                        if (SettingTool.configld.language == "English")
                            MessageBox.Show("Let's input text to search");
                        else
                            MessageBox.Show("Hãy nhập nội dung tìm kiếm Page");
                        return;
                    }

                }
                if (chkJoinGroupKeyword.Checked)
                {
                    if (txtKeywordSeach.Text == "")
                    {
                        if (SettingTool.configld.language == "English")
                            MessageBox.Show("Let's input text to sreach group");
                        else
                            MessageBox.Show("Hãy nhập nội dung tìm kiếm Group");
                        return;
                    }
                }
                if (rdo_new.Checked)
                {
                    cauhinh = new CauHinh();
                    cauhinh.Name = txt_name_config.Text.Trim();
                    CauHinh_Bll cauhinh_bll = new CauHinh_Bll();
                    if (cauhinh_bll.insert(cauhinh))
                    {
                        int id = cauhinh_bll.method_InsertDataID();
                        if (id != -1)
                        {
                            method_SaveTuongTacNgay(id);
                        }
                        method_Config();
                    }
                }
                else
                {
                    ComboboxItem item2 = (ComboboxItem)cboConfig.SelectedItem;
                    CauHinh dm = (CauHinh)item2.Tag;
                    dm.Name = txt_name_config.Text;
                    method_SaveTuongTacNgay(dm.ID);
                    CauHinh_Bll cauhinh_bll = new CauHinh_Bll();
                    cauhinh_bll.update(dm);

                }
                // this.Close();
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                File.AppendAllText(String.Format("{0}\\logImage.txt", Application.StartupPath), DateTime.Now.ToString() + ": FORM CONFIG Error - " + ex.Message + "\n");
            }
        }

        private void cboConfig_Click(object sender, EventArgs e)
        {

        }
        private void rdo_new_CheckedChanged(object sender, EventArgs e)
        {
            //    cboConfig.Enabled = false;
            //    unCheck();
        }

        private void rdoEdit_CheckedChanged(object sender, EventArgs e)
        {
            cboConfig.Enabled = true;
        }
        private void cboConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index=cboConfig.SelectedIndex;
            //if(index==0)
            //{
            //Random rdom = new Random();
            //SettingTuongTac tuongtac = new SettingTuongTac();
            //tuongtac.luotnewfeed = true;
            //tuongtac.likenewfeed = true;
            //tuongtac.numlikenewfeed = rdom.Next(1, 5);
            //tuongtac.commentnewfeed = true;
            //tuongtac.numcommentnewfeed = rdom.Next(1, 5);
            //tuongtac.message = "{Hi|Xin chao}";
            //tuongtac.sharevideo = true;
            //tuongtac.numsharevideo = rdom.Next(1, 3);
            //tuongtac.sharepost = true;
            //tuongtac.numsharepost = rdom.Next(1, 3);
            //tuongtac.strkeywordfanpage = "lam dep,thoi trang,cong nghe,am thuc,nau an";
            //tuongtac.likefanpage = true;
            //tuongtac.numlikefanpage = rdom.Next(1, 5);
            //tuongtac.likepostfanpage = true;
            //tuongtac.numlikepostfanpage = rdom.Next(1, 5);
            //tuongtac.commentpostfanpage = true;
            //tuongtac.numcommentpostfanpage = rdom.Next(1, 5);
            //tuongtac.addfriend = true;
            //tuongtac.numaddfriend = rdom.Next(1, 5);
            //tuongtac.acceptfriend = true;
            //tuongtac.numacceptfriend = rdom.Next(1, 5);
            //tuongtac.strkeywordseach = "lam dep,thoi trang,cong nghe,am thuc,nau an";
            //tuongtac.joingroupkeyword = true;
            //tuongtac.numjoingroupkeyword = rdom.Next(1, 5);
            //tuongtac.likepostgroup = true;
            //tuongtac.numlikepostgroup = rdom.Next(1, 5);
            //tuongtac.commentpostgroup = true;
            //tuongtac.numcommentpostgroup = rdom.Next(1, 5);
            //tuongtac.delaymin = rdom.Next(1, 5);
            //tuongtac.delaymax = rdom.Next(20, 60);
            //tuongtac.action = rdom.Next(1, 5);
            //tuongtac.readnoti = true;
            //string path = String.Format("{0}\\Config\\0.data", Application.StartupPath);
            //File.WriteAllText(path, JsonConvert.SerializeObject(tuongtac));
            //method_LoadTuongTacNgay(index);
            //}
            //else
            //{
            //ComboboxItem item2 = (ComboboxItem)cboConfig.SelectedItem;
            //CauHinh dm = (CauHinh)item2.Tag;
            //method_LoadTuongTacNgay(dm.ID);
            // }

            ComboboxItem item2 = (ComboboxItem)cboConfig.SelectedItem;
            CauHinh dm = (CauHinh)item2.Tag;
            method_LoadTuongTacNgay(dm.ID);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string message = "";
            if (SettingTool.configld.language == "English")

                message = string.Format("Do you want to delete config {0}?", cboConfig.SelectedText);
            else
                message = string.Format("Bạn có muốn xóa cấu hình {0}?", cboConfig.SelectedText);

            string caption = "Thông báo";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                RequestParams para = new RequestParams();
                ComboboxItem item = new ComboboxItem();
                item = (ComboboxItem)cboConfig.Items[cboConfig.SelectedIndex];

                para.Add(new KeyValuePair<string, string>("Id", item.Value.ToString()));
                dt.delete("tbl_CauHinh", para);
            }
            method_Config();
        }

        private void rdo_new_Click(object sender, EventArgs e)
        {
            cboConfig.Enabled = false;
            unCheck();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathLD.Text = fldrDlg.SelectedPath;
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            saveConfigLD();
            method_SetupPathLD();
            if (SettingTool.configld.language == "English")
                MessageBox.Show("Save Successful", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Lưu Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPathReaction_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathReaction.Text = dialog.FileName;
            }
        }



        private void btncommentGroup_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathcommentGroup.Text = dialog.FileName;
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtSeachFanpage.Text = dialog.FileName;
            }
        }

        private void btnOpen_Click_1(object sender, EventArgs e)
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


        private void btncommentGroup_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathcommentGroup.Text = dialog.FileName;
            }
        }

        private void bunifuFlatButton3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_pathGroupId.Text = dialog.FileName;
            }
        }

        private void btnPathHma_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathHma.Text = fldrDlg.SelectedPath;
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtMess.Text = dialog.FileName;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3)
            {
                button1.Visible = false;
                btnXoa.Visible = false;
            }
            else
            {
                button1.Visible = true;
                btnXoa.Visible = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + "\\xproxy.txt";
            if (File.Exists(path) == false)
            {
                File.WriteAllText(path, " ");
            }
            Process.Start(path);
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {

            loadLocationTinsoft();

        }
        public void loadLocationTinsoft()
        {
            YahooController yahoo = new YahooController();
            cboLocation.Items.Clear();
            List<TinsoftLocation> list_tinsoftlocation = yahoo.GetLocationTinsoft();
            if (list_tinsoftlocation.Count > 0)
            {
                foreach (TinsoftLocation ts in list_tinsoftlocation)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = ts.name;
                    item.Tag = ts;
                    cboLocation.Items.Add(item);
                }
                cboLocation.SelectedIndex = 0;
            }
            else
            {
                FunctionHelper.showMessage("Không thể load Loation từ Tinsoft");
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 2)
            {
                loadLocationTinsoft();
                cboLocation.Text = SettingTool.configld.tinsoftname;
            }
        }
        private void setupLanguage()
        {
            tabControl1.TabPages[0].Text = "General"; //Configuration
            tabControl1.TabPages[1].Text = "Profile";
            tabControl1.TabPages[2].Text = "Group";
            tabControl1.TabPages[3].Text = "Config LD";

            button1.Text = "Save";
            btnXoa.Text = "Delete";
            btnCancel.Text = "Close";

            bunifuFlatButton2.Text = "Save Config LD";
            //general
            groupBox5.Text = "Select mode";
            rdo_new.Text = "Add new";
            rdoEdit.Text = "Update";
            cboConfig.Text = "Select Configuration";
            groupBox3.Text = "General config";
            lblKc2lanchay.Text = "Delay";
            lblSohanhdong.Text = "Amount action";
            chkRunRandom.Text = "Run action random";
            chkLoop_tuongtac.Text = "Run loop action after";
            lblphut.Text = "Minutes";
            lblhour.Text = "Hour";
            lblintime.Text = "Only run in time";
            lblto.Text = "to";
            chkReadNoti.Text = "Check Notification";
            chkMessenger.Text = "Send messenger";
            label31.Text = "File content to send Messenger";
            chkSearch.Text = "Search info by key word";


            chkLikePage.Text = "Follow page";
           
            label15.Text = "seconds";
            //profile
            chkLuotNewfeed.Text = "Slide newfeed";
            chkLikeNewFeed.Text = "Like newfeed";
            chkCommentNewfeed.Text = "Comment newfeed";
            label1.Text = "File comment fomart {nd1|nd2}";
            chkSharePost.Text = "Share post into newfeed";
            chkAddFriend.Text = "Add friend suggest";
            chkAcceptFriend.Text = "Accept add friend";
            chkAddfriendUID.Text = "Add friend by UID";
            chkAddFriendbyNewfeed.Text = "Add friend by newfeed";
            chkcancelRequest.Text = "Cacel request invite friend";
            chkNewfeedWatch.Text = "Slide on watch video";
            chkNewfeedMarketPlace.Text = "Slide on market place";
            chkLikeWatch.Text = "Like video";
            label25.Text = "Attention, setup  file path UID for account Facebook";
            label34.Text = "minutes";
            label35.Text = "minutes";
            label46.Text = "minutes";




            //Group
            chkscrollgroup.Text = "Slide newfeed on group";
            chkLikeNewfeedGroup.Text = "Like newfeed on group";
            chkCommentPostGroup.Text = "Comment newfeed on group";
            chkJoinGroupKeyword.Text = "Join group by search";
            label13.Text = "key word search";
            label9.Text = "groups / 1 time";
            chkAutoAnwser.Text = "Auto answer";

            chkLikePostGroup.Text = "Like post on group";
            label33.Text = "File comment content";
            chkUID.Text = "Join group by group ID";
            label17.Text = "groups / 1 time";

            chkLikecommentGroupId.Text = "Like, comment into group ID";
            label11.Text = "Chose File Group ID (*.txt)";
            //LD
            label26.Text = "Delay open LD";
            label6.Text = "seconds";
            label42.Text = "seconds";
            label41.Text = "Time for 1 LD started";
            label28.Text = "Amount LDs run";
            label43.Text = "> 10 seconds";
            label27.Text = "Setup path install LD Player";
            label53.Text = "Choose version LD";
            label30.Text = "Cookies to check info UID";
            label55.Text = "Language";
            chkQuitLD.Text = "Don't close LD ";
            chkSaveToken.Text = "Auto save token,cookie after finish";
            label37.Text = "Change IP after ";
            label38.Text = "accounts ";

            groupBox4.Text = "Config to change Ip";
            rbNoip.Text = "Don't change Ip";
            rbDcom.Text = "Change IP by Dcom";
            rbHMA.Text = "Change IP by HMA";
            rbVPN111.Text = "Change IP by VPN 1.1.1.1 CloudFlare";
            rbProxy.Text = "Change Ip by Proxy";
            rbTinsoft.Text = "Change ip by Proxy Tinsoft";
            rbxProxy.Text = "Change Ip by xProxy";
            label51.Text = "Delay connect ip Dcom";
            label50.Text = "seconds";
            rbDialup.Text = "Change dcom by Dialup";
            label36.Text = "Path install HMA";
            label39.Text = "Only Version 2.8.24.0";
            label49.Text = "Api key proxy.tinsoftsv.com one key 1 line";
            label54.Text = "Choose Location";
            linkLabel1.Text = "List IP xProxy";

            chkRunRandomTuongTac.Text = "Run random";
            label29.Text = "account/ 1 LD";
            chkPostProfile.Text = "Post on profile (please configure the content in the widget section -> create article)";
            label24.Text = "Per post";
            label23.Text = "posts. Maximum";
            label22.Text = "posts/day";
            chkRandomPost.Text = "Randomly generated created post";
            chkPostGroup.Text = "Post on group (please configure the content in the widget section -> create article)";
            label18.Text = "Per post";
            label20.Text = "posts. Maximum";
            label21.Text = "posts/day";

            chkChoDuyet.Text = "Leaving group waiting for approval";
            chkRemoveMember.Text = "leaves the group with fewer members";
            chkRemovePost.Text = "Leaving group has fewer posts/day";
            label12.Text = "posts";
            chkJoinGroupNoApprove.Text = "Only join group without waiting for approval";
            chkRandomPostGroup.Text = "Random post";
            label16.Text = "Maximum interaction time 1 ld";
            label2.Text = "Use token to post by uid";
            chkRemoveGroup.Text = "Leave the group by your choice";

            label14.Text = "minute";
        }

        private void chkRemoveMember_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://youtu.be/VHE9bKcAMLI");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://zut.vn/tinsoft");
        }

        private void btnfileUIDcancel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_pathUIDcancel.Text = dialog.FileName;
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + "\\Ninjaproxy.txt";
            if (File.Exists(path) == false)
            {
                File.WriteAllText(path, " ");
            }
            SettingTool.linkproxyninja = Application.StartupPath + "\\Ninjaproxy.txt";
            Process.Start(path);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + "\\obcproxy.txt";
            if (File.Exists(path) == false)
            {
                File.WriteAllText(path, " ");
            }
            Process.Start(path);
        }

        private void btnPathIamge_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathIamgecomment.Text = fldrDlg.SelectedPath;
            }
        }
        private void bunifuFlatButton5_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtMess.Text = dialog.FileName;
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_pathGroupId.Text = dialog.FileName;
            }

        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + "\\ListgroupId.txt";
            if (File.Exists(path) == false)
            {
                File.WriteAllText(path, " ");
            }
            SettingTool.linkproxyninja = Application.StartupPath + "\\ListgroupId.txt";
            Process.Start(path);
        }

        private void btnPathIamge_gr_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathIamgecomment_gr.Text = fldrDlg.SelectedPath;
            }
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + "\\proxyV6.txt";
            if (File.Exists(path) == false)
            {
                File.WriteAllText(path, " ");
            }
            SettingTool.linkproxyninja = Application.StartupPath + "\\proxyV6.txt";
            Process.Start(path);
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://proxyv6.net");
        }

       


    }
}
