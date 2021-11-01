using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem.Controller
{
    public class controlUpdate
    {
        Data data = new Data();
        public bool updateData23()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["Os"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool updateDataColName()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["Name"] = "TEXT";
                method_Create_Column(parameters, "Account");
                 

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool updateDataColId()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["Id"] = "TEXT";
                method_Create_Column(parameters, "Account");


            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool updateDataDanhmuc()
        {
            try
            {

                RequestParams  parameters = new RequestParams();
                parameters["sothutu"] = "INTEGER";
                method_Create_Column(parameters, "Danhmuc");

            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool updateDisplay()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["display"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool updateName()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["name"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool updatePathUID()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["pathUID"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool updatePathPost()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["pathpic"] = "TEXT";
                parameters["pathpost"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool updateDataDisplay()
        {
            try
            {

                string sql = "Update Account Set Display='Yes'";                 
                data.method_DeleteData(sql);

            }
            catch
            {
                return false;
            }
            return true;
        }
        public void updateLDID()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["ldid"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
            }
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["noti"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                
            }
            
        }
        public void createTableGroupLD()
        {
            try
            {
                string sql = "CREATE TABLE \"GroupLD\" (`GroupLDID`	INTEGER PRIMARY KEY AUTOINCREMENT,`Name` TEXT)";
                data.method_DeleteData(sql);
            }
            catch
            { }
        }
        public void createTableDetailLD()
        {
            try
            {
                string sql = "CREATE TABLE \"DetailLD\" (`ID` INTEGER PRIMARY KEY AUTOINCREMENT,`GroupLDID` INTEGER,`LDID` INTEGER,`LDName` TEXT)";
                data.method_DeleteData(sql);
            }
            catch
            { }
        }

        public bool update_SyncDB_account()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["Nox"] = "TEXT";
                parameters["Device_mobile"] = "TEXT";
                parameters["App"] = "TEXT";
                parameters["AppName"] = "TEXT";
                parameters["Os"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool method_Create_Column(RequestParams p, string nametable)
        {
            try
            {
                foreach (var param in p)
                {
                    if (string.IsNullOrEmpty(param.Key))
                    {
                        continue;
                    }
                    string sql = String.Format("ALTER TABLE {0} ADD [{1}] {2}", nametable, param.Key, param.Value);
                    data.method_DeleteData(sql);
                }
            }
            catch
            {
            }
            return false;
        }
        public bool createColumnProxy()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["Proxy"] = "TEXT";
                method_Create_Column(parameters, "DetailLD");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnFriend()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["friend_count"] = "TEXT";
                parameters["group_count"] = "TEXT";
                parameters["tendanhmuc"] = "TEXT"; 
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnToken()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["token"] = "TEXT";
                parameters["cookies"] = "TEXT";                 
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool createColumnPathCover()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["pathCover"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnKeyVPN()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["Keyvpn"] = "TEXT";
                method_Create_Column(parameters, "DetailLD");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnKeyUseragent()
        {
            try
            {

                RequestParams parameters = new RequestParams();
                parameters["ghichu"] = "TEXT";
                parameters["useragent"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnKeyLastRun()
        {
            try
            { 
                RequestParams parameters = new RequestParams();
                parameters["lastrun"] = "TEXT";              
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnKeyDataPost()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["dataprofile"] = "TEXT";
                parameters["datagroup"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnKeyBirthday()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["birthday"] = "TEXT";
                parameters["avatar"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnDNS()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["dns"] = "TEXT";
                method_Create_Column(parameters, "DetailLD");
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnDNSAccount()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["dns"] = "TEXT";
                parameters["proxy"] = "TEXT";
                method_Create_Column(parameters, "Account");
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool createColumnCity()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["country"] = "TEXT";
                parameters["state"] = "TEXT";
                parameters["city"] = "TEXT";
                method_Create_Column(parameters, "Account");
            }
            catch
            {
                return false;
            }
            return true;
        }


        public bool deletedoubleUID(string sql)
        {
            return data.method_DeleteData(sql);
        }
        public bool createColumnBackupLD()
        {
            try
            {
                RequestParams parameters = new RequestParams();
                parameters["backupld"] = "TEXT";
                method_Create_Column(parameters, "Account");

            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
