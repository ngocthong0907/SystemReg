using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using NinjaSystem.Model;
namespace NinjaSystem
{
    public class RequestParams : List<KeyValuePair<string, string>>
    {
        public object this[string paramName]
        {
            set
            {
                #region  

                if (paramName == null)
                {
                    throw new ArgumentNullException("paramName");
                }

                if (paramName.Length == 0)
                {
                    throw new ArgumentNullException("paramName");
                }

                #endregion

                string str = (value == null ? string.Empty : value.ToString());

                Add(new KeyValuePair<string, string>(paramName, str));
            }
        }
    }
    public class Data
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        public List<Schedule> method_SelectSchedule(string sql)
        {
            List<Schedule> list_da = new List<Schedule>();
            method_SetConn();
            sql_con.Open();
            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_da.Add(new Schedule()
                    {
                        idConfig = Convert.ToInt32(reader["idConfig"].ToString()),
                        id = Convert.ToInt32(reader["id"].ToString()),
                        toDate = reader["toDate"].ToString(),
                        fromDate = reader["fromDate"].ToString(),
                        hours = reader["hours"].ToString(),
                        accounts = reader["accounts"].ToString(),
                        type = Convert.ToInt32(reader["type"].ToString()),
                        name = reader["name"].ToString(),
                    });
                }
            }
            catch
            {
            }
            return list_da;
        }
        public void method_SetConn()
        {
            string path = String.Format("{0}\\Config\\configpathDB.data", Application.StartupPath);

            DatabaseConfig settingDB = new DatabaseConfig();
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    settingDB = JsonConvert.DeserializeObject<DatabaseConfig>(json);
                }

                if (settingDB.pathDB == null || settingDB.pathDB == "")
                    sql_con = new SQLiteConnection(String.Format("Data Source={0}\\Data\\Ninja.db;Version=3;", Application.StartupPath));
                else
                    sql_con = new SQLiteConnection(String.Format("Data Source={0};Version=3;", settingDB.pathDB));
            }
            catch
            {
                sql_con = new SQLiteConnection(String.Format("Data Source={0}\\Data\\Ninja.db;Version=3;", Application.StartupPath));
            }
            
        }
        public bool insert(RequestParams parameters, string tablename)
        {
            try
            {
                string sql = method_CreateSqlInsert(parameters, tablename);
                return method_InsertData(sql);
            }
            catch
            {
                return false;
            }

        }
        public bool method_InsertData(string sql)
        {
            try
            {
                method_SetConn();
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                command.ExecuteNonQuery();
                sql_con.Close();
            }
            catch (Exception er)
            {
               
                return false;

            }
            return true;
        }
        public string method_CreateSqlInsert(RequestParams parameters, string tablename)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            var queryBuilder = new StringBuilder();
            var queryValue = new StringBuilder();
            foreach (var param in parameters)
            {
                if (string.IsNullOrEmpty(param.Key))
                {
                    continue;
                }

                queryBuilder.Append(param.Key);
                queryBuilder.Append(',');

                queryValue.Append('\'');
                queryValue.Append(param.Value);
                queryValue.Append('\'');
                queryValue.Append(',');
            }
            if (queryBuilder.Length != 0)
            {
                queryBuilder.Remove(queryBuilder.Length - 1, 1);
            }
            if (queryValue.Length != 0)
            {
                queryValue.Remove(queryValue.Length - 1, 1);
            }
            string sql = String.Format("INSERT INTO {0} ({1}) VALUES ({2});", tablename, queryBuilder.ToString(), queryValue);
            return sql;
        }


        public DataTable select(string sql)
        {
            try
            {

                return method_NoiDungSelect(sql);
            }
            catch
            {
                return new DataTable();
            }

        }
        public bool delete(string tablename, RequestParams pawhere = null)
        {
            try
            {
                string sql = method_CreateSqlDelete(tablename, pawhere);
                return method_DeleteData(sql);
            }
            catch
            {
                return false;
            }

        }
        public bool method_DeleteData(string sql)
        {
            try
            {
                method_SetConn();
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public string method_CreateSqlDelete(string tablename, RequestParams parameters = null)
        {

            var queryBuilder = new StringBuilder();

            foreach (var param in parameters)
            {
                if (string.IsNullOrEmpty(param.Key))
                {
                    continue;
                }

                queryBuilder.Append(param.Key);
                queryBuilder.Append('=');
                queryBuilder.Append('\'');
                queryBuilder.Append(param.Value);
                queryBuilder.Append('\'');
                queryBuilder.Append(',');
                queryBuilder.Append(" AND ");
            }
            if (queryBuilder.Length != 0)
            {

                queryBuilder.Remove(queryBuilder.Length - 6, 5);
            }
            string sql = String.Format("DELETE FROM  {0} WHERE {1}", tablename, queryBuilder.ToString());

            return sql;
        }
        private DataTable method_NoiDungSelect(string sql)
        {
            DataTable dt = new DataTable();
            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                using (SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command))
                {
                    sqlDataAdapter.Fill(dt);
                    return dt;

                }

            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        public string selectone(string select, string tablename, RequestParams pawhere = null)
        {
            try
            {
                string sql = method_CreateSqlSelectOne(select, tablename, pawhere);
                return method_SelectOne(sql);
            }
            catch
            {
                return null;
            }
        }
        public bool update(RequestParams parameters, string tablename, RequestParams pawhere = null)
        {
            try
            {
                string sql = method_CreateSqlUpdate(parameters, tablename, pawhere);
                return method_InsertData(sql);
            }
            catch
            {
                return false;
            }

        }
        public string method_CreateSqlUpdate(RequestParams parameters, string tablename, RequestParams pawhere = null)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            var queryBuilder = new StringBuilder();

            foreach (var param in parameters)
            {
                if (string.IsNullOrEmpty(param.Key))
                {
                    continue;
                }

                queryBuilder.Append(param.Key);
                queryBuilder.Append('=');
                queryBuilder.Append('\'');
                queryBuilder.Append(param.Value);
                queryBuilder.Append('\'');
                queryBuilder.Append(',');
            }
            if (queryBuilder.Length != 0)
            {

                queryBuilder.Remove(queryBuilder.Length - 1, 1);
            }
            string sql = String.Format("UPDATE {0} SET {1}", tablename, queryBuilder.ToString());
            if (pawhere != null)
            {
                var querywhere = new StringBuilder();
                foreach (var p in pawhere)
                {
                    if (string.IsNullOrEmpty(p.Key))
                    {
                        continue;
                    }

                    querywhere.Append(p.Key);
                    querywhere.Append('=');
                    querywhere.Append('\'');
                    querywhere.Append(p.Value);
                    querywhere.Append('\'');
                    querywhere.Append(" AND ");
                }
                if (querywhere.Length != 0)
                {
                    querywhere.Remove(querywhere.Length - 4, 4);
                }
                sql = String.Format("{0} WHERE {1}", sql, querywhere.ToString());
            }

            return sql;
        }
        public string method_SelectOne(string sql)
        {
            string kq = null;
            try
            {
                method_SetConn();
                sql_con.Open();

                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                kq = command.ExecuteScalar().ToString();
            }
            catch
            {
            }
            return kq;
        }
        public string method_CreateSqlSelectOne(string select, string tablename, RequestParams parameters = null)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            var queryBuilder = new StringBuilder();

            foreach (var param in parameters)
            {
                if (string.IsNullOrEmpty(param.Key))
                {
                    continue;
                }

                queryBuilder.Append(param.Key);
                queryBuilder.Append('=');
                queryBuilder.Append('\'');
                queryBuilder.Append(param.Value);
                queryBuilder.Append('\'');
                queryBuilder.Append(',');
                queryBuilder.Append(" AND ");
            }
            if (queryBuilder.Length != 0)
            {

                queryBuilder.Remove(queryBuilder.Length - 6, 5);
            }

            string sql = String.Format("SELECT {0} FROM {1} WHERE {2} LIMIT 1", select, tablename, queryBuilder.ToString());
            return sql;
        }
        public List<CauHinh> method_CauHinhSelect(string sql)
        {
            List<CauHinh> list_da = new List<CauHinh>();

            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_da.Add(new CauHinh()
                    {
                        ID = Convert.ToInt32(reader["id"].ToString()),
                        Name = reader["Name"].ToString(),

                    });

                }
            }
            catch
            {
            }
            return list_da;
        }

        public List<Groups> method_GroupsSelect(string sql)
        {
            List<Groups> list_da = new List<Groups>();

            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_da.Add(new Groups()
                    {
                        Id_danhmuc = Convert.ToInt32(reader["id_danhmuc"].ToString()),
                        tendanhmuc = reader["ten_danhmuc"].ToString(),
                    });

                }
            }
            catch (Exception er)
            {
               
            }
            return list_da;
        }

        public List<Account> method_AccountSelect(string sql)
        {
            List<Account> list_da = new List<Account>();

            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_da.Add(new Account()
                    {
                        Id_account = Convert.ToInt32(reader["Id_account"].ToString()),
                        id_danhmuc = Convert.ToInt32(reader["id_danhmuc"].ToString()),
                        email = reader["email"].ToString(),
                        Password = reader["Password"].ToString(),
                        TrangThai = reader["TrangThai"].ToString(),
                        Thongbao = reader["noti"].ToString(),
                        privatekey = reader["privatekey"].ToString(),
                        nox = reader["nox"].ToString(),
                        Device_mobile = reader["Device_mobile"].ToString(), 
                        app = reader["app"].ToString(),
                        appname = reader["appname"].ToString(),
                        os = reader["os"].ToString(),
                        name = reader["name"].ToString(),
                        id = reader["id"].ToString(),
                        pathpic = reader["pathpic"].ToString(),
                        pathpost = reader["pathpost"].ToString(),   
                        ldid= reader["ldid"].ToString(),
                        pathUID = reader["pathUID"].ToString(),
                        friend_count = reader["friend_count"].ToString(),
                        group_count = reader["group_count"].ToString(),
                        tendanhmuc = reader["tendanhmuc"].ToString(),
                        pathCover = reader["pathCover"].ToString(),
                        token = reader["token"].ToString(),
                        cookies = reader["cookies"].ToString(),
                        ghichu = reader["ghichu"].ToString(),
                        lastrun = reader["lastrun"].ToString(),
                        dataprofile = reader["dataprofile"].ToString(),
                        datagroup = reader["datagroup"].ToString(),
                        birthday = reader["birthday"].ToString(),
                        avatar = reader["avatar"].ToString(),
                        country = reader["country"].ToString(),
                        state = reader["state"].ToString(),
                        city = reader["city"].ToString(),
                        proxy = reader["proxy"].ToString(),
                        dns = reader["dns"].ToString(),
                    });

                }
            }
            catch 
            {
            }
            return list_da;
        }
        public List<DanhMuc> loadDanhMuc(string sql)
        {
            List<DanhMuc> list_danhmuc = new List<DanhMuc>();
            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_danhmuc.Add(new DanhMuc()
                    {
                        id_danhmuc = reader["id_danhmuc"].ToString(),
                        tendanhmuc = reader["tendanhmuc"].ToString(),
                    });

                }
            }
            catch
            {
            }
            return list_danhmuc;
        }
        public List<Account> method_AccountSelectSearch(string sql)
        {
            List<Account> list_da = new List<Account>();

            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_da.Add(new Account()
                    {
                        Id_account = Convert.ToInt32(reader["Id_account"].ToString()),
                        id_danhmuc = Convert.ToInt32(reader["id_danhmuc"].ToString()),
                        email = reader["email"].ToString(),
                        Password = reader["Password"].ToString(),
                        TrangThai = reader["TrangThai"].ToString(),
                        Thongbao = reader["noti"].ToString(),
                        privatekey = reader["privatekey"].ToString(),
                        nox = reader["nox"].ToString(),
                        Device_mobile = reader["Device_mobile"].ToString(),
                        app = reader["app"].ToString(),
                        appname = reader["appname"].ToString(),
                        os = reader["os"].ToString(),
                        name = reader["name"].ToString(),
                        id = reader["id"].ToString(),
                        pathpic = reader["pathpic"].ToString(),
                        pathpost = reader["pathpost"].ToString(),
                        ldid = reader["ldid"].ToString(),
                        pathUID = reader["pathUID"].ToString(),
                        friend_count = reader["friend_count"].ToString(),
                        group_count = reader["group_count"].ToString(),
                        tendanhmuc = reader["tendanhmuc"].ToString(),
                        pathCover = reader["pathCover"].ToString(),
                        token = reader["token"].ToString(),
                        cookies = reader["cookies"].ToString(),
                        ghichu = reader["ghichu"].ToString(),
                        lastrun = reader["lastrun"].ToString(),
                        dataprofile = reader["dataprofile"].ToString(),
                        datagroup = reader["datagroup"].ToString(),
                        birthday = reader["birthday"].ToString(),
                        avatar = reader["avatar"].ToString(),
                        country = reader["country"].ToString(),
                        state = reader["state"].ToString(),
                        city = reader["city"].ToString(),
                       
                    });

                }
            }
            catch
            {
            }
            return list_da;
        }

        /// <summary>
        /// lay thong tin groupld
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<GroupLDModel> selectGroupLD(string sql)
        {
            List<GroupLDModel> list_da = new List<GroupLDModel>();
            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_da.Add(new GroupLDModel()
                    {
                        GroupLDID = Convert.ToInt32(reader["GroupLDID"].ToString()),                      
                        Name = reader["Name"].ToString(),
                       
                    });

                }
            }
            catch
            {
            }
            return list_da;
        }
        /// <summary>
        /// 
        /// lay thong tin chi tiet ld
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        ///   public List<GroupLDModel> selectGroupLD(string sql)
        public List<DetailLDModel> selectDetailLD(string sql)
        {
            List<DetailLDModel> list_da = new List<DetailLDModel>();
            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list_da.Add(new DetailLDModel()
                     {
                        ID = Convert.ToInt32(reader["ID"].ToString()),
                        GroupLDID = Convert.ToInt32(reader["GroupLDID"].ToString()),
                        LDID = Convert.ToInt32(reader["LDID"].ToString()),
                        LDName = reader["LDName"].ToString(),
                        Proxy = reader["Proxy"].ToString(),
                        Keyvpn = reader["Keyvpn"].ToString(),
                        dns = reader["dns"].ToString(),
                    });

                }
            }
            catch
            {
            }
            return list_da;
        }
        public DetailLDModel selectOneDetailLD(string sql)
        {
            DetailLDModel data = new DetailLDModel();
            method_SetConn();
            sql_con.Open();

            try
            {
                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql, sql_con);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.ID = Convert.ToInt32(reader["ID"].ToString());
                    data.GroupLDID = Convert.ToInt32(reader["GroupLDID"].ToString());
                    data.LDID = Convert.ToInt32(reader["LDID"].ToString());
                    data.LDName = reader["LDName"].ToString();
                    data.Proxy = reader["Proxy"].ToString();
                    data.dns = reader["dns"].ToString();
                }
                
                
            }
            catch
            {
            }
            return data;
        }
        public int method_InsertDataID(string sql)
        {
            try
            {
                method_SetConn();
                sql_con.Open();

                sql_cmd = sql_con.CreateCommand();
                SQLiteCommand command = new SQLiteCommand(sql_con);
                command.CommandText = sql;
                var i = command.ExecuteScalar();


                sql_con.Close();
                return Convert.ToInt32(i);
            }
            catch 
            {


            }
            return -1;
        }
    }

}
