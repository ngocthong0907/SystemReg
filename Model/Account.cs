using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public class Account
    {
        public int Id_account { set; get; }
        public int id_danhmuc { set; get; }

        public string name { set; get; }
        public string email { set; get; }
        public string Password { set; get; }
        public string privatekey { set; get; }      
        public string id { set; get; }
        public string TrangThai { set; get; }
        public string Thongbao { set; get; }
        public string nox { set; get; }
        public string Device_mobile { set; get; }
        public string app { set; get; }
        public string appname { set; get; }
        public string os { set; get; }
        public string pathpic { set; get; }
        public string pathpost { set; get; }
        public string ldid { set; get; }
        public string pathUID { set; get; }
        public string friend_count { get; set; }
        public string group_count { get; set; }
        public string tendanhmuc { get; set; }
        public string token { set; get; }
        public string cookies { set; get; }

        public string pathCover { set; get; }
        public string gender { set; get; }
        public string first_name { set; get; }
        public string last_name { set; get; }
        public string phone { set; get; }
        public string useragent { set; get; }
        public string ghichu { set; get; }
        public string lastrun { set; get; }
        public string dataprofile { set; get; }
        public string datagroup { set; get; }
        public string birthday { set; get; }
        public string country { set; get; }
        public string state { set; get; }
        public string city { set; get; }
        public string proxy { set; get; }
        public string dns { set; get; }
        public string avatar { set; get; }

        public string backupld { set; get; }


    }
    public class NoxManager
    {
        public string deviceid { set; get; }
        public string nox { set; get; }
        public List<DataGridViewRow> list_dr { set; get; }
        public List<Account> list_acc { set; get; }
        public List<string> list_app { set; get; }
    }
}
