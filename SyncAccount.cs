using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Newtonsoft.Json;
using System.IO;
using NinjaSystem.Controller;
namespace NinjaSystem
{
    public partial class SyncAccount : Form
    {
        public SyncAccount()
        {
            InitializeComponent();
        }

        private void txtPathAdd_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };
            dialog.Filter = "Database (*.db)|*.db";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPathAdd.Text = dialog.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rdoNinjaCare.Checked)
                SyncNinjacare();
            else
                SyncVersion();

        }
        private void SyncVersion()
        {
            if (txtPathAdd.Text == "Chọn đường dẫn" || txtPathAdd.Text == "")
            {
                MessageBox.Show("Vui lòng chọn file data cần đồng bộ");
                return;
            }
            string message = "Bạn có chắc chắn thực hiện đồng bộ?";
            string caption = "Thông báo";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string fileName = "ninja.db";
                string targetPath = Application.StartupPath + "\\Data";
                System.IO.Directory.CreateDirectory(targetPath);
                string destFile = System.IO.Path.Combine(targetPath, fileName);
                string sourceFile = txtPathAdd.Text;
                System.IO.File.Copy(sourceFile, destFile, true);
                Data dt = new Data();
                string sqlcommand = "";
                sqlcommand = "CREATE TABLE Account_Delete (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,Id_danhmuc INTEGER NOT NULL,Email   TEXT NOT NULL,Password TEXT NOT NULL,TrangThai   TEXT)";
                dt.method_DeleteData(sqlcommand);

                sqlcommand = "CREATE TABLE Account (Id_account	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,id_danhmuc	INTEGER NOT NULL,Email	TEXT NOT NULL,Password	TEXT NOT NULL,PrivateKey	TEXT,TrangThai	TEXT,ThongBao	TEXT,Nox	TEXT,Device_mobile	TEXT,App	TEXT,	AppName	TEXT,	Os	TEXT)";
                dt.method_DeleteData(sqlcommand);
                sqlcommand = "INSERT INTO Account (Id_account,id_danhmuc,Email,Password,Privatekey,TrangThai,Thongbao,Nox,Device_mobile,App,AppName,os) SELECT  Id,Nhom_id,Email,Pass,Privatekey,Status,Message,Nox,Device,App,AppName,os FROM tbl_NguoiDung";
                dt.method_DeleteData(sqlcommand);

                sqlcommand = "CREATE TABLE Danhmuc (Id_danhmuc	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,tendanhmuc	TEXT NOT NULL)";
                dt.method_DeleteData(sqlcommand);
                sqlcommand = "insert into danhmuc(id_danhmuc, tendanhmuc) select id, ten_nhom from tbl_nhom";
                dt.method_DeleteData(sqlcommand);

                MessageBox.Show("Đã hoàn thành quá trình đồng bộ. \n\r \n\r Chương trình sẽ khởi động lại");
                Application.Restart();
            }

        }
        private void SyncNinjacare()
        {
            if (txtPathAdd.Text == "Chọn đường dẫn" || txtPathAdd.Text == "")
            {
                MessageBox.Show("Vui lòng chọn file data cần đồng bộ");
                return;
            }
            string message = "Bạn có chắc chắn thực hiện đồng bộ?";
            string caption = "Thông báo";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
               
                DatabaseConfig pathconfigDb = new DatabaseConfig();
                pathconfigDb.pathDB = txtPathAdd.Text;
                string path = String.Format("{0}\\Config\\configpathDB.data", Application.StartupPath);
                File.WriteAllText(path, JsonConvert.SerializeObject(pathconfigDb));

                controlUpdate up = new controlUpdate();
                up.update_SyncDB_account();
                Data dt = new Data();
                string create_table = "";
                create_table = "CREATE TABLE Account_Delete (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,Id_danhmuc INTEGER NOT NULL,Email   TEXT NOT NULL,Password TEXT NOT NULL,TrangThai   TEXT)";
                dt.method_DeleteData(create_table);
                create_table = "CREATE TABLE tbl_CauHinh (ID INTEGER PRIMARY KEY AUTOINCREMENT,Name TEXT)";
                dt.method_DeleteData(create_table);
                //sync table cauhinh
                DataTable dt_soure = new DataTable();
                SQLiteConnection sql_con = new SQLiteConnection(String.Format("Data Source={0}//data//ninja.db;Version=3;", Application.StartupPath));
                sql_con.Open();
                SQLiteCommand command = new SQLiteCommand("Select * from tbl_cauhinh", sql_con);
                try
                {
                    using (SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(command))
                    {
                        sqlDataAdapter.Fill(dt_soure);
                    }
                    dt.method_DeleteData("delete from tbl_cauhinh");
                    foreach (DataRow row in dt_soure.Rows)
                    {
                        RequestParams para = new RequestParams();
                        para.Add(new KeyValuePair<string, string>("Id", row["Id"].ToString()));
                        para.Add(new KeyValuePair<string, string>("Name", row["Name"].ToString()));
                        dt.insert(para, "tbl_cauhinh");
                    }
                }
                catch
                {

                }
                

                MessageBox.Show("Đã hoàn thành quá trình đồng bộ. \n\r \n\r Chương trình sẽ khởi động lại");
                Application.Restart();
               
            }

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {


        }

        private void SyncAccount_Load(object sender, EventArgs e)
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
                    lbLink.Text = "Không tìm thấy file database";
                else
                    lbLink.Text =  settingDB.pathDB;
            }
            catch
            {
                lbLink.Text =  "Kiểm tra lại file: " + path;
            }
        }

        private void rdoVersion_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
