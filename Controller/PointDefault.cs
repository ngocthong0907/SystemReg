using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class PointDefault
    {
        public static Point p_login_email{set;get;}
        public static Point p_login_password { set; get; }
        public static Point p_login_buttonlogin { set; get; }
        public static Point p_login_2fa{ set; get; }
        //image
        public static Bitmap img_login_inputphone { set; get; }
        public static Bitmap img_login_buttonaccept { set; get; }

        //logout
        public static Bitmap img_logout_menu { set; get; }
        public static Bitmap img_logout_button { set; get; }
        public static Point p_logoutmenu { set; get; }

        //avatar
        public static Point p_avatar_hd { set; get; }
        public static Point p_avatar_img { set; get; }

        //cover
        public static Point p_cover_button { set; get; }//click button thêm ảnh cover
        public static Point p_cover_hd { set; get; }
        public static Point p_cover_img { set; get; }

        //change name
        public static Bitmap img_profile_ten { set; get; }
        public static Point p_profile_ten { set; get; }
        public static Point p_profile_ho { set; get; }
        public static Point p_profile_tendem { set; get; }
        public static Point p_profile_button { set; get; }
        public static Point p_profile_password { set; get; }
        public static Point p_profile_save { set; get; }
        //change password
        public static Bitmap img_profile_change_pass { set; get; }
        public static Bitmap img_profile_change_pass_en { set; get; }
        public static Bitmap img_profile_password_curent { set; get; }
        public static Bitmap img_profile_password_curent_en { set; get; }
        public static Bitmap img_profile_password_retype { set; get; }
        public static Bitmap img_profile_password_retype_en { set; get; }
        public static Bitmap img_profile_password_new { set; get; }
        public static Bitmap img_profile_password_new_en { set; get; }
        public static Bitmap img_profile_password_save { set; get; }
        public static Bitmap img_profile_password_save_en { set; get; }
        public static Bitmap img_profile_duytri { set; get; }
        public static Bitmap img_profile_duytri_en { set; get; }
        public static Bitmap img_profile_saipass { set; get; }
        public static Bitmap img_profile_saipass2 { set; get; }
        public static Point p_profile_change_pass{ set; get; }//click vào đổi mật khẩu
        public static Point p_profile_changepass_finish { set; get; }
        public static Point p_profile_changepass_logout { set; get; }
        public static Bitmap img_profile_logout_all { set; get; }
        public static Bitmap img_profile_logout_all2 { set; get; }
        //2fa
        public static Bitmap img_2fa_2yeuto { set; get; }
        public static Bitmap img_2fa_input { set; get; }
        //post group
        public static Bitmap img_group_postimg { set; get; }

        public static Bitmap img_group_postimg_new { set; get; }

        public static Bitmap img_group_thaoluan { set; get; }

        public static Bitmap img_post_profile_ban_dang_nghi_gi { set; get; }

        public static Bitmap img_post_ban_viet_gi_di { set; get; }

        public static Bitmap img_group_postimg_en { set; get; }

        public static Bitmap img_group_poststatus { set; get; }
        //share video watch
        public static Bitmap img_sharevideo_buttonshare { set; get; }
        //view video
        public static Point p_group_view_video { set; get; }

        //addfriend
        public static Bitmap img_addfriend { set; get; }

        //join group
        public static Bitmap img_joingroup_check { set; get; }
        public static Bitmap img_joingroup_radio { set; get; }
        public static Bitmap img_joingroup_writeanswer{ set; get; }
        public static Bitmap img_joingroup_buttonanswer { set; get; }
        //like video watch
        public static Bitmap img_like_videowatch { set; get; }
        public static Bitmap img_liked_videowatch { set; get; }
        public static Bitmap img_3x_like_watch_2 { set; get; }
        public static Point p_share_video_watch_seach { set; get; }


        //playlist
        public static Bitmap img_3x_share_playlist_phat { set; get; }
        public static Bitmap img_3x_share_playlist_3cham { set; get; }
        public static Bitmap img_3x_share_playlist_chia_se { set; get; }
        public static Bitmap img_3x_share_playlist_3cham_trang { set; get; }
        public static Bitmap img_3x_share_playlist_chia_se_trang { set; get; }
        public static void setValue()
        {
            if(SettingTool.configld.versionld=="4.x")
            {
                //login
                p_login_email = new Point(300,910);
                p_login_password = new Point(300, 1045);
                p_login_buttonlogin = new Point(300, 1230);
                p_login_2fa = new Point(300, 1120);
                img_login_inputphone = Properties.Resources._2_inputid_sodienthoai;
                img_login_buttonaccept = Properties.Resources._3_allow_access;

                //logout
                img_logout_menu = Properties.Resources._1_memu_more;
                img_logout_button = Properties.Resources.menu_logout;
                p_logoutmenu = new Point(300, 80);

                //avatar
                p_avatar_hd = new Point(500, 100);
                p_avatar_img = new Point(370, 380);

                //avatar
                p_cover_button = new Point(372, 417);
                p_cover_hd = new Point(500, 100);
                p_cover_img = new Point(10, 380);

                //name        
                img_profile_ten = Properties.Resources._2_inputname;
                p_profile_ho = new Point(140, 185);
                p_profile_ten = new Point(140, 260);
                p_profile_button = new Point(140, 380);
                p_profile_password = new Point(140, 260);
                p_profile_save = new Point(140, 290);
                p_profile_tendem = new Point(140, 215);
                //password
                img_profile_change_pass = Properties.Resources._3x_profile_changepass;
               
                img_profile_password_curent = Properties.Resources._2_password_curent;
              
                img_profile_password_new = Properties.Resources._2_password_new;
               
                img_profile_password_save = Properties.Resources._3x_profile_password_save;
               
                img_profile_duytri = Properties.Resources._3x_profile_duytri;
               
                img_profile_saipass = Properties.Resources._3x_profile_saipass;
                img_profile_saipass2 = Properties.Resources._3x_profile_saipass2;
                img_profile_logout_all = Properties.Resources._3x_change_pass_logout_all;
                img_profile_logout_all2 = Properties.Resources._3x_change_pass_logout_all2;
                p_profile_change_pass = new Point(150, 360);
                p_profile_changepass_finish = new Point(160, 240);
                p_profile_changepass_logout = new Point(160, 240);
                //2fa
                img_2fa_2yeuto = Properties.Resources._2_inputcode_2fa;
                img_2fa_input =  Properties.Resources._2_inputcode_2fa;

                //post group
                img_group_postimg = Properties.Resources._1_postimage;

                img_group_poststatus = Properties.Resources._2_post_content;

                //share video
                img_sharevideo_buttonshare = Properties.Resources._1_sharevideo;

                //view video
                p_group_view_video = new Point(500, 1000);

                //addfriend
                img_addfriend = Properties.Resources._1_addfriend;

                //join group
                img_joingroup_check =Properties.Resources._2_checkanswer;
                img_joingroup_writeanswer =Properties.Resources._2_writeanswer;
                img_joingroup_buttonanswer = Properties.Resources._1_buttonanswer;
                img_joingroup_radio = Properties.Resources._3x_joingroup_radio;

                //like video watch
                img_like_videowatch = Properties.Resources._1_likevideo;
                img_liked_videowatch = Properties.Resources._3x_sharegroup_dalike;
                p_share_video_watch_seach=new Point(10, 95);
                img_3x_like_watch_2 = Properties.Resources._3x_like_watch_2;


                img_3x_share_playlist_phat = Properties.Resources._3x_share_playlist_phat;
                img_3x_share_playlist_3cham = Properties.Resources._3x_share_playlist_3cham;
                img_3x_share_playlist_chia_se = Properties.Resources._3x_share_playlist_chia_se;
                img_3x_share_playlist_3cham_trang = Properties.Resources._3x_share_playlist_3cham_trang;
                img_3x_share_playlist_chia_se_trang = Properties.Resources._3x_share_playlist_chia_se_trang;
            }
            else
            {
                if(SettingTool.configld.appversion=="Facebook 299")
                {
                    p_login_email = new Point(150, 180);
                    p_login_password = new Point(150, 235);
                    p_login_buttonlogin = new Point(150, 275);
                    p_login_2fa = new Point(150, 255);
                }
                else
                {
                    p_login_email = new Point(150, 195);
                    p_login_password = new Point(150, 240);
                  
                    p_login_buttonlogin = new Point(150, 285);
                    p_login_2fa = new Point(150, 255);
                }
               
             
             
                img_login_inputphone = Properties.Resources._3x_login_email;
                img_login_buttonaccept = Properties.Resources._3_allow_access;

                //logout
                img_logout_menu = Properties.Resources._3x_logout_menu;
                img_logout_button = Properties.Resources._3x_logout_menu2;//4x
                p_logoutmenu = new Point(290, 80);

                //avatar
                p_avatar_hd = new Point(150, 20);
                p_avatar_img = new Point(100, 100);

                //cover              
                p_cover_button = new Point(150, 120);
                p_cover_hd = new Point(150, 30);
                p_cover_img = new Point(30, 100);

                //name        
                img_profile_ten = Properties.Resources._3x_profile_ho;
                p_profile_ho = new Point(140, 185);
                p_profile_ten = new Point(140, 260);
                p_profile_button = new Point(140, 380);
                p_profile_password = new Point(140, 260);
                p_profile_save = new Point(140, 290);
                p_profile_tendem = new Point(140, 215);
                //change pas
                img_profile_change_pass = Properties.Resources._3x_profile_changepass;
                img_profile_change_pass_en = Properties.Resources._3x_profile_changepass_en;
                img_profile_password_curent = Properties.Resources._3x_profile_password_current;
                img_profile_password_curent_en = Properties.Resources._2_password_curent_en;
                img_profile_password_retype = Properties.Resources._3x_profile_password_retype;
                img_profile_password_retype_en = Properties.Resources._3x_profile_password_retype_en;
                img_profile_password_save_en = Properties.Resources._3x_profile_password_save_en;
                img_profile_password_new = Properties.Resources._3x_profile_password_new;
                img_profile_password_new_en = Properties.Resources._2_password_new_en;
                img_profile_password_save = Properties.Resources._3x_profile_password_save;
                img_profile_duytri_en = Properties.Resources._3x_profile_duytri_en;
                img_profile_duytri = Properties.Resources._3x_profile_duytri;               
                img_profile_saipass = Properties.Resources._3x_profile_saipass;
                img_profile_saipass2 = Properties.Resources._3x_profile_saipass2;
                p_profile_changepass_finish = new Point(160, 240);
                p_profile_change_pass = new Point(150, 360);
                p_profile_changepass_logout = new Point(100, 150);
                img_profile_logout_all = Properties.Resources._3x_change_pass_logout_all;
                img_profile_logout_all2 = Properties.Resources._3x_change_pass_logout_all2;
                //2fa
                img_2fa_2yeuto = Properties.Resources._3x_2fa_2yeuto;
                img_2fa_input = Properties.Resources._3x_2fa_input;
                //post group
                img_group_postimg = Properties.Resources._3x_group_postimg;
                img_group_poststatus = Properties.Resources._3x_group_poststatus;

                //share video
                img_sharevideo_buttonshare = Properties.Resources._1_sharevideo_3x;

                //view video
                p_group_view_video = new Point(133, 240);

                //addfriend
                img_addfriend = Properties.Resources._3x_addfriend;

                img_group_postimg = Properties.Resources._1_postimage_3x;

                img_group_thaoluan = Properties.Resources.zl_batdauthaoluan;

                //join group
                img_joingroup_check = Properties.Resources._3x_joingroup_check;
                img_joingroup_writeanswer = Properties.Resources._3x_write_group_answer;
                img_joingroup_buttonanswer = Properties.Resources._1_buttonanswer_3x;
                img_joingroup_radio = Properties.Resources._3x_joingroup_radio;
                img_group_postimg_en = Properties.Resources._1_postgroup_en;

                //like video watch
                img_like_videowatch = Properties.Resources._1_likevideo_3x;
                img_liked_videowatch = Properties.Resources._3x_sharegroup_dalike;
                p_share_video_watch_seach = new Point(10, 95);
                img_3x_like_watch_2 = Properties.Resources._3x_like_watch_2;

                img_3x_share_playlist_phat = Properties.Resources._3x_share_playlist_phat;
                img_3x_share_playlist_3cham = Properties.Resources._3x_share_playlist_3cham;
                img_3x_share_playlist_chia_se = Properties.Resources._3x_share_playlist_chia_se;
                img_3x_share_playlist_3cham_trang = Properties.Resources._3x_share_playlist_3cham_trang;
                img_3x_share_playlist_chia_se_trang = Properties.Resources._3x_share_playlist_chia_se_trang;

                img_group_postimg_new = Properties.Resources._3x_group_postimg_new;

                img_post_profile_ban_dang_nghi_gi = Properties.Resources._3x_post_profile_ban_dang_nghi_gi;
                img_post_ban_viet_gi_di = Properties.Resources._3x_banvietgidi;
            }
        }
    }
}
