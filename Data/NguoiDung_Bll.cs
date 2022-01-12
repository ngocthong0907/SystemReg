using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaSystem.Model;
using System.IO;
using System.Windows.Forms;
namespace NinjaSystem
{
    public class NguoiDung_Bll
    {
        Data data = new Data();
        public List<Account> loadUserbyNhomID(int id)
        {
            string sql = "Select  * From Account where id_danhmuc=" + id + " and Display='Yes' order by id_account asc";
            return data.method_AccountSelect(sql);
        }
        public List<Account> loadUserbyDevice(string deviceID)
        {
            string sql = "Select  * From Account where Device_mobile='" + deviceID + "' order by id_account asc";
            return data.method_AccountSelect(sql);
        }
        public List<Account> loadUserbyListDevice(string listdevice)
        {
            string sql = "Select  * From Account where Device_mobile in(" + listdevice + ") order by id_account asc";
            return data.method_AccountSelect(sql);
        }
        public List<Account> loadUser()
        {
            string sql = "Select  * From Account order by id_account asc";
            return data.method_AccountSelect(sql);
        }

        public List<Account> loadUserByLDID(int ldid)
        {
           
            string sql = "";
            if (ldid == -1)
            {
                sql = "Select  * From Account Where ldid not NULL and ldid!='' order by id_account asc";
            }
            else
            {
                sql = string.Format("Select  * From Account Where ldid=" + ldid + "  order by id_account asc", ldid);
            }

            return data.method_AccountSelect(sql);
        }
        public List<Account> loadUserbySql(string sql)
        {

            return data.method_AccountSelect(sql);
        }
        public List<Account> loadUserbySqlSearch(string sql)
        {

            return data.method_AccountSelectSearch(sql);
        }
        public List<Account> loadUserbyApp(string deviceID, string app)
        {
            string sql = "Select  * From Account where device_mobile='" + deviceID + "' AND app='" + app + "' order by id_account asc";
            return data.method_AccountSelect(sql);
        }
        public bool updateAccount(Account acc)
        {
            RequestParams para = new RequestParams();
            para["id_danhmuc"] = acc.id_danhmuc;
            para["tendanhmuc"] = acc.tendanhmuc;
            para["email"] = acc.email;
            para["Password"] = acc.Password;
            para["privatekey"] = acc.privatekey;
            para["TrangThai"] = acc.TrangThai;
            para["token"] = acc.token;
            para["cookies"] = acc.cookies;
            para["nox"] = acc.nox;
            para["Device_mobile"] = acc.Device_mobile;
            para["app"] = acc.app;
            para["appname"] = acc.appname;
            para["os"] = acc.os;
            para["ldid"] = acc.ldid;
            para["name"] = acc.name;
            para["display"] = "Yes";
            para["id"] = acc.id;
            if (string.IsNullOrEmpty(acc.birthday) == false)
                para["birthday"] = acc.birthday;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateDevice(Account acc)
        {
            RequestParams para = new RequestParams();
            para["TrangThai"] = acc.TrangThai;
            para["nox"] = acc.nox;
            para["Device_mobile"] = acc.Device_mobile;
            para["app"] = acc.app;
            para["appname"] = acc.appname;
            para["os"] = acc.os;
            para["Noti"] = acc.Thongbao;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateStatus(Account acc)
        {
            RequestParams para = new RequestParams();
            para["TrangThai"] = acc.TrangThai;
            para["Noti"] = acc.Thongbao;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateAvatar(Account acc)
        {
            RequestParams para = new RequestParams();
            para["Avatar"] = acc.avatar;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updatePost(Account acc)
        {
            RequestParams para = new RequestParams();
            para["pathpic"] = acc.pathpic;
            para["pathpost"] = acc.pathpost;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateAvatarCover(Account acc)
        {
            RequestParams para = new RequestParams();

            para["pathcover"] = acc.pathCover;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }

        public bool updatePathUID(Account acc)
        {
            RequestParams para = new RequestParams();
            para["pathUID"] = acc.pathUID;

            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }

        public bool insertAccount(Account acc)
        {
            RequestParams para = new RequestParams();
            para["id_danhmuc"] = acc.id_danhmuc;
            para["tendanhmuc"] = acc.tendanhmuc;
            para["email"] = acc.email;
            para["name"] = acc.name;
            para["Password"] = acc.Password;
            para["privatekey"] = acc.privatekey;
            para["TrangThai"] = acc.TrangThai;
            para["nox"] = acc.nox;
            para["Device_mobile"] = acc.Device_mobile;
            para["app"] = acc.app;
            para["appname"] = acc.appname;
            para["os"] = acc.os;
            para["ldid"] = acc.ldid;
            para["id"] = acc.id;
            para["display"] = "Yes";
            para["birthday"] = acc.birthday;
            para["country"] = acc.country;
            para["state"] = acc.state;
            para["city"] = acc.city;
            para["gender"] = acc.gender;
            para["token"] = acc.token;
            para["cookies"] = acc.cookies;
            para["dataprofile"] = acc.dataprofile;
            return data.insert(para, "Account");
        }
        public bool updateAccountByUID(Account acc)
        {
            RequestParams para = new RequestParams();
            para["id_danhmuc"] = acc.id_danhmuc;
            para["tendanhmuc"] = acc.tendanhmuc;
            para["email"] = acc.email;
            para["Password"] = acc.Password;
            para["privatekey"] = acc.privatekey;
            para["TrangThai"] = acc.TrangThai;
            para["nox"] = acc.nox;
            para["Device_mobile"] = acc.Device_mobile;
            para["app"] = acc.app;
            para["appname"] = acc.appname;
            para["os"] = acc.os;
            para["ldid"] = acc.ldid;
            para["name"] = acc.name;
            para["display"] = "Yes";
            if (string.IsNullOrEmpty(acc.birthday))
            {
                para["birthday"] = acc.birthday;
            }
            para["country"] = acc.country;
            para["state"] = acc.state;
            para["city"] = acc.city;

            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateNote(Account acc)
        {
            RequestParams para = new RequestParams();
           
            para["nox"] = acc.nox;           
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        #region updateld
        public bool updateNoti(Account acc)
        {
            RequestParams para = new RequestParams();
            para["Noti"] = acc.Thongbao;
            para["TrangThai"] = acc.TrangThai;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updatePassword(Account acc)
        {
            RequestParams para = new RequestParams();
            para["Password"] = acc.Password;

            RequestParams where = new RequestParams();
            where["id"] = acc.id;
            return data.update(para, "Account", where);
        }
        public bool updateBirthday(Account acc)
        {
            RequestParams para = new RequestParams();
            para["birthday"] = acc.birthday;

            RequestParams where = new RequestParams();
            where["id"] = acc.id;
            return data.update(para, "Account", where);
        }
        public bool updateName(Account acc)
        {
            RequestParams para = new RequestParams();
            para["Noti"] = acc.Thongbao;
            para["Name"] = acc.name;
            para["friend_count"] = acc.friend_count;
            para["group_count"] = acc.group_count;
            para["birthday"] = acc.birthday;
            para["TrangThai"] = acc.TrangThai;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateTokenCookie(Account acc)
        {
            RequestParams para = new RequestParams();
            if (string.IsNullOrEmpty(acc.token) == false)
                para["token"] = acc.token;
            if (string.IsNullOrEmpty(acc.cookies) == false)
                para["cookies"] = acc.cookies;
            //if(string.IsNullOrEmpty(acc.useragent)==false)
            //    para["useragent"] = acc.useragent;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateuidEmail(Account acc)
        {
            RequestParams para = new RequestParams();
            if (string.IsNullOrEmpty(acc.id) == false)
                para["id"] = acc.id;
            if (string.IsNullOrEmpty(acc.email) == false)
                para["email"] = acc.email;
            //if(string.IsNullOrEmpty(acc.useragent)==false)
            //    para["useragent"] = acc.useragent;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateLastRun(Account acc)
        {
            RequestParams para = new RequestParams();
            para["lastrun"] = DateTime.Now.ToString();
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public bool updateDataPost(Account acc)
        {
            RequestParams para = new RequestParams();
            para["dataprofile"] = acc.dataprofile;
            para["datagroup"] = acc.datagroup;
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
        public List<Schedule> loadSchedule()
        {
            string sql = "Select  * From Schedule order by fromdate desc";
            return data.method_SelectSchedule(sql);
        }
        #endregion

        public List<Account> loadAccountByDanhMuc(List<string> list_danhmuc)
        {
            string where = String.Join(",", list_danhmuc);
            string sql = "Select  * From Account where id_danhmuc IN (" + where + ") and Display='Yes' order by id_account asc";
            return data.method_AccountSelect(sql);
        }
        public bool updateTimeBackup(Account acc)
        {
            RequestParams para = new RequestParams();
            para["backupld"] = "Backup : " + DateTime.Now.ToString();
            RequestParams where = new RequestParams();
            where["Id_account"] = acc.Id_account;
            return data.update(para, "Account", where);
        }
    }
}
