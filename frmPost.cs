using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using KAutoHelper;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Data.SQLite;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SharpAdbClient;
using Newtonsoft.Json.Linq;
namespace NinjaSystem
{
    public partial class frmPost : Form
    {
        public frmPost(List<Account> list_acc)
        {

            InitializeComponent();
            this.list_acc = list_acc;
        }
        NinjaADB ninjadb = new NinjaADB();
        int groupId = 0;
        bool stop = false;
        object synAcc = new object();
        Thread thread_1;
        CancellationTokenSource ts = new CancellationTokenSource();
        CancellationToken ct = new CancellationToken();
        Data dt = new Data();
        List<Account> list_acc;
        ArrayList lstUser = new ArrayList();
        static object syncObj = new object();
        public MainNinja frm;
        CheckBox headerCheckBox = new CheckBox();
        SettingTuongTac tuongtac = new SettingTuongTac();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        List<DeviceData> lsDevices;
        List<string> lsKeyword = new List<string>();
        Random rdom = new Random();
        List<int> list_tuongtac = new List<int>();
        List<DeviceData> list_devices;
        ninjaDroidHelper droid = new ninjaDroidHelper();
        void ClickOnLeapdroidPosition(DeviceData device, Point point)
        {
            //AutoControl.SendClickOnPosition(handle, x, y);
            ADBHelper.Tap(device.Serial, point.X, point.Y);
        }
        void ClickOnLeapdroidPosition(DeviceData device, int x, int y)
        {
            //AutoControl.SendClickOnPosition(handle, x, y);
            ADBHelper.Tap(device.Serial, x, y);
        }

        void Delay(double delay)
        {
            double delayTime = 0;
            while (delayTime < delay)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delayTime++;
            }
        }
        private void method_LoadAccount()
        {
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }
        }
        private void load_user_by_groupId(int id)
        {
            dgvUser.Rows.Clear();
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            List<Account> list_acc = new List<Account>();
            list_acc = nguoidung_bll.loadUserbyNhomID(id);
            foreach (Account acc in list_acc)
            {
                method_Datagridview(acc);
            }

        }
        private void method_Datagridview(Account acc)
        {
            try
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();

                DataGridViewCheckBoxCell check = new DataGridViewCheckBoxCell();
                check.Value = true;
                dataGridViewRow.Cells.Add(check);

                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = (dgvUser.Rows.Count + 1).ToString();
                dataGridViewRow.Cells.Add(cell1);

                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = acc.email;
                dataGridViewRow.Cells.Add(cell2);

                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                cell5.Value = acc.os;
                dataGridViewRow.Cells.Add(cell5);

                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                cell6.Value = acc.Device_mobile;
                dataGridViewRow.Cells.Add(cell6);

                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                cell7.Value = acc.appname;
                dataGridViewRow.Cells.Add(cell7);

                DataGridViewTextBoxCell cell71 = new DataGridViewTextBoxCell();
                cell71.Value = acc.pathpic;
                dataGridViewRow.Cells.Add(cell71);

                DataGridViewTextBoxCell cell72 = new DataGridViewTextBoxCell();
                cell72.Value = acc.pathpost;
                dataGridViewRow.Cells.Add(cell72);

                DataGridViewTextBoxCell cell73 = new DataGridViewTextBoxCell();
                cell73.Value = acc.pathCover;
                dataGridViewRow.Cells.Add(cell73);

                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                cell8.Value = acc.TrangThai;
                dataGridViewRow.Cells.Add(cell8);
                dataGridViewRow.Tag = acc;

                DataGridViewTextBoxCell cell9 = new DataGridViewTextBoxCell();
                cell9.Value = acc.Thongbao;
                dataGridViewRow.Cells.Add(cell9);
                dataGridViewRow.Tag = acc;
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.dgvUser.Rows.Add(dataGridViewRow);

                }));

            }
            catch
            {
            }
        }

        private void frmPost_Load(object sender, EventArgs e)
        {
            method_LoadAccount();
            droid.frm = frm;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                if (checkBox1.Checked)
                {
                    dr.Cells[0].Value = true;
                }
                else
                    dr.Cells[0].Value = false;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ClearMessage();
            stop = false;
            setData();
            startTuongTac();
        }
        private void ClearMessage()
        {
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                dgvUser.Rows[i].Cells["Message"].Value = "";

            }
        }

        private void startTuongTac()
        {

            list_devices = droid.getDevices();
            pibStatus.Visible = true;
            //chon cau hinh
            if (setupCauHinh())
            {

                if (list_devices.Count == 0)
                {
                    MessageBox.Show("Vui lòng mở chương trình giả lập");
                    pibStatus.Visible = false;
                    return;
                }
                else
                {
                    list_dr = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in dgvUser.Rows)
                    {
                        if ((bool)row.Cells[0].Value)
                        {
                            Account acc = (Account)row.Tag;
                            if (checkDeviceAccount(acc))
                            {
                                list_dr.Add(row);
                            }
                            else
                            {
                                row.Cells["Message"].Value = "Chưa bật đúng thiết bị Nox";
                            }
                        }
                    }
                    if (list_dr.Count == 0)
                    {
                        MessageBox.Show("Hãy chọn những tài khoản cần chạy");
                        pibStatus.Visible = false;
                        return;
                    }
                    else
                    {
                        this.thread_1 = new Thread(new ThreadStart(this.runTuongTac));
                        thread_1.IsBackground = true;
                        this.thread_1.Start();
                    }

                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn cấu hình trước khi chạy tương tác", "Thông báo");
            }
        }
        public bool checkDeviceAccount(Account acc)
        {
            try
            {
                foreach (DeviceData device in list_devices)
                {
                    if (acc.Device_mobile == device.Serial)
                        return true;
                }
            }
            catch
            {
            }
            return false;
        }
        private bool setupCauHinh()
        {
            try
            {
                tuongtac = new SettingTuongTac();

                tuongtac.chkShareGroup = true;

                list_tuongtac = new List<int>();

                list_tuongtac.Add(19);

                if (list_tuongtac.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {

            }
            return false;
        }


        private void runTuongTac()
        {
            ct = ts.Token;
            int numthread = list_devices.Count;
            if (list_dr.Count > 0)
            {
                object synDevice = new object();
                Task[] tasks = new Task[numthread];
                for (int p = 0; p < numthread; p++)
                {
                    int t = p;
                    tasks[t] = Task.Factory.StartNew(() =>
                    {
                        DeviceData device = new DeviceData();
                        List<DataGridViewRow> list_acc = new List<DataGridViewRow>();
                        lock (synDevice)
                        {
                            device = list_devices[0];
                            list_devices.RemoveAt(0);
                            //chon tai khoan cua device

                            foreach (DataGridViewRow dr in list_dr)
                            {
                                Account acc = (Account)dr.Tag;
                                if (acc.Device_mobile == device.Serial)
                                {
                                    list_acc.Add(dr);
                                }
                            }

                        }
                        method_Start(device, list_acc);
                    });
                    Delay(3);
                }
                Task.WaitAll(tasks);
            }
            stopAll();
        }

        private void stopAll()
        {
            stop = true;
            this.pibStatus.Visible = false;

            if (this.thread_1 != null)
            {
                this.thread_1.Abort();
            }
        }

        private void method_Start(DeviceData device, List<DataGridViewRow> list_acc)
        {
            //cai dat apk
            string pathapk = String.Format("{0}\\app\\ADBKeyboard.apk", Application.StartupPath);
            if (File.Exists(pathapk))
            {
                droid.installApk(device, pathapk);

            }

        Lb_Acc:
            if (list_acc.Count > 0)
            {
                try
                {
                    DataGridViewRow dr = null;
                    lock (synAcc)
                    {
                        dr = list_acc[0];
                        list_acc.Remove(dr);
                    }
                    if (dr != null)
                    {
                        dr.Cells["Message"].Value = "Running";

                        if (chk3g.Checked)
                        {
                            droid.ResetDcom(device.Serial);
                            dr.Cells["Message"].Value = "Reset 3g";
                        }
                        //close all app
                        droid.closeAllAppFacebook(device);
                        bool haslogin = false;
                        Account acc = new Account();
                        acc = (Account)dr.Tag;
                        //buoc 1 kiem tra app     
                        dr.Cells["Message"].Value = "Open Facebook";

                        droid.openAppFacebook(device, acc.app);
                        dr.Cells["Message"].Value = "Login Facebook";
                        droid.check_Facebook_has_stopped(device, acc);
                        bool status = droid.checkIsLogin(device);
                        if (status)
                        {
                            haslogin = true;
                        }
                        else
                        {
                            //haslogin = droid.loginAvatarLD(device, acc);
                            //if (haslogin == false)
                            //    haslogin = droid.LoginFacebook(device, acc.app, acc);
                            droid.loginAvatarLD(device, acc);
                            haslogin = droid.LoginFacebook(device, acc.app, acc);
                        }

                        if (haslogin)
                        {
                            dr.Cells["Message"].Value = "Đăng nhập thành công";
                            acc.TrangThai = "Live";
                            string ghichu = acc.nox;
                            #region bat dau tuong tac
                            int delay = (int)numDelay.Value;
                            string message = "";
                            string content = "";
                            if (File.Exists(acc.pathpost))
                            {
                                string contentFile = File.ReadAllText(acc.pathpost);
                                content = FunctionHelper.method_Spin(contentFile);
                            }
                            bool removepic = false;
                            if (chkXoaAnh.Checked)
                                removepic = true;
                            else
                                removepic = false;
                            List<string> list_file = new List<string>();
                            List<string> list_file_cover = new List<string>();
                            if (rdoPost.Checked)
                            {
                                dr.Cells["Message"].Value = "Đăng bài";
                                if (Directory.Exists(acc.pathpic))
                                {
                                    list_file = System.IO.Directory.GetFiles(acc.pathpic, "*.jpg").ToList();

                                }
                                if (list_file.Count > 0)
                                {
                                    message += droid.PostImages(device, acc.app, content, list_file, (int)numPhoto.Value, removepic);
                                    Thread.Sleep((int)numDelay.Value);
                                }
                                else
                                {
                                    droid.PostContent(device, acc.app, content);
                                    Thread.Sleep((int)numDelay.Value);
                                }
                            }
                            else if (rdoChangeAvatar.Checked)
                            {
                                list_file = System.IO.Directory.GetFiles(acc.pathpic, "*.jpg").ToList();
                                list_file_cover = System.IO.Directory.GetFiles(acc.pathCover, "*.jpg").ToList();

                                if (chkChangeAvatar.Checked)
                                {
                                    if (list_file.Count > 0)
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu đổi avatar";
                                        if (droid.ChangeAvatar(device, acc.app, list_file, removepic))
                                        {
                                            dr.Cells["Message"].Value = "Đã thay đổi avatar";
                                            ghichu += " avatar";
                                            message += "Đã thay đổi avatar";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Không thể thay đổi avatar";
                                        }
                                    }
                                    else
                                        dr.Cells["Message"].Value = "Vui lòng chọn folder chứa ảnh";
                                }

                                if (chkChangeCover.Checked)
                                {
                                    if (list_file_cover.Count > 0)
                                    {
                                        dr.Cells["Message"].Value = "Bắt đầu đổi cover";
                                        if (droid.ChangeCover(device, acc.app, list_file_cover, removepic))
                                        {
                                            dr.Cells["Message"].Value = "Đã thay đổi Cover";
                                            ghichu += " Cover";
                                            message += " Đã thay đổi Cover";
                                        }
                                        else
                                        {
                                            dr.Cells["Message"].Value = "Không thể thay đổi Cover";
                                        }
                                    }
                                    else

                                        dr.Cells["Message"].Value = "Vui lòng chọn folder chứa ảnh";
                                }

                                RequestParams para = new RequestParams();
                                RequestParams para_where = new RequestParams();

                                para_where["Id_account"] = acc.Id_account.ToString();
                                para["Nox"] = ghichu;
                                Data dt = new Data();
                                dt.update(para, "Account", para_where);


                            }
                            else if (rdoAll.Checked)
                            {
                                if (txtPathImagePast.Text == "" || txtContent.Text == "")
                                {
                                    MessageBox.Show("Yêu cầu thông tin folder ảnh và nội dung bài viết");
                                    return;
                                }

                                dr.Cells["Message"].Value = "Đăng bài";
                                list_file = System.IO.Directory.GetFiles(txtPathImagePast.Text, "*.jpg").ToList();
                                content = FunctionHelper.method_Spin(txtContent.Text);
                                message += droid.PostImages(device, acc.app, content, list_file, (int)numPhoto.Value, removepic);
                                Thread.Sleep((int)numDelay.Value);
                            }

                            #endregion
                            if (stop)
                                dr.Cells["Message"].Value = "Delay : " + delay + "s";

                            Delay(delay);

                            dr.Cells["Message"].Value = message;
                        }
                        else
                        {
                            if (acc.Thongbao != null)
                                dr.Cells["Message"].Value = acc.Thongbao;
                            else
                                dr.Cells["Message"].Value = "Đăng nhập thất bại";
                            // dr.Cells["trangthai"].Value = "Die";
                            acc.Thongbao = "Die";
                        }
                        if (list_acc.Count > 0 && stop == false)
                        {
                            goto Lb_Acc;
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public void setData()
        {
            CustomerController customer = new CustomerController();
            ResultRequest kq = customer.sendLogs("login");
            Dictionary<string, string> mydata = new Dictionary<string, string>();
            if (kq.status)
            {
                JArray jarr = JArray.Parse(kq.data);
                foreach (var item in jarr)
                {
                    mydata.Add(item["Key"].ToString(), item["Value"].ToString());
                }
            }
            SettingTool.data = mydata;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopAll();
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            // btnRun_Click(null, null);

            //ninjaDroidHelper droid = new ninjaDroidHelper();
            //tuongtac = new SettingTuongTac();
            //setData();
            //lsDevices = droid.getDevices();
            //tuongtac.chkShareGroup = true;
            //List<string> lsKeyword = new List<string>();
            //Random rd = new Random();
            //  droid.keyboard();
            //droid.PostContent(lsDevices[0], "com.facebook.katank","nguyễn văn ân");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            PathFolderImage();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                RestoreDirectory = true
            };

            dialog.Filter = "File txt (*.txt)|*.txt";
            dialog.ShowDialog();
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            string pathpost = dialog.FileName;
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                dr.Cells["clPost"].Value = pathpost;
                Account acc = (Account)dr.Tag;
                acc.pathpost = pathpost;
                nguoidung_bll.updatePost(acc);

            }

        }

        private void btnImageAvatar_Click(object sender, EventArgs e)
        {
            PathFolderImage();
        }
        private void PathFolderImage()
        {
            var fldrDlg = new FolderBrowserDialog();
            fldrDlg.ShowDialog();
            //if (fldrDlg.ShowDialog() == DialogResult.OK)
            //{
            string pathpic = fldrDlg.SelectedPath;
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                dr.Cells["clPic"].Value = pathpic;
                Account acc = (Account)dr.Tag;
                acc.pathpic = pathpic;
                nguoidung_bll.updatePost(acc);

            }

        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoAll.Checked = false;
            rdoChangeAvatar.Checked = false;
            rdoPost.Checked = false;

            switch (tab.SelectedIndex)
            {
                case 0:
                    rdoAll.Checked = true;
                    break;
                case 1:
                    rdoPost.Checked = true;
                    break;
                case 2:
                    rdoChangeAvatar.Checked = true;
                    break;

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathImagePast.Text = fldrDlg.SelectedPath;
            }
        }

        private void btnPathCover_Click(object sender, EventArgs e)
        {
            var fldrDlg = new FolderBrowserDialog();
            fldrDlg.ShowDialog();
            string pathpic = fldrDlg.SelectedPath;
            NguoiDung_Bll nguoidung_bll = new NguoiDung_Bll();
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                dr.Cells["clCover"].Value = pathpic;
                Account acc = (Account)dr.Tag;
                acc.pathCover = pathpic;
                nguoidung_bll.updateAvatarCover(acc);
            }
        }
    }
}
