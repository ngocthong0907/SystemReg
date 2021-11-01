using Newtonsoft.Json;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace NinjaSystem
{
    public partial class frmImportContact : Form
    {
        public frmImportContact(frm_MainLD_PRO frm_main, List<DetailLDModel> list_detail)
        {
            InitializeComponent();

            this.frm_main = frm_main;
            this.list_detail = list_detail;
        }
        List<DetailLDModel> list_detail;
        frm_MainLD_PRO frm_main;
        List<Account> list_acc;
        bool stop = false;
        object synAcc = new object();

       
        ninjaDroidHelper droid = new ninjaDroidHelper();
        List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
        Thread thread_1;
        static object syncObjUID = new object();
        //List<string> list_uid = new List<string>();
        Random rd = new Random();
        List<int> list_tuongtac = new List<int>();
        List<LDRun> list_ldrun = new List<LDRun>();
        List<string> list_ld = new List<string>();
        LDController ld = new LDController();
        List<string> list_uid = new List<string>();
        Random rdom = new Random();
        object synUID = new object();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        List<string> list_contact = new List<string>();
        int num4LD = 0;
        private void frmImportContact_Load(object sender, EventArgs e)
        {

            for (int i = 0 ; i <  1; i++)
            {
                DetailLDModel ld = new DetailLDModel();
                ld.LDID = i;
                method_Datagridview(ld);
            }

          
          
        }
        public void sendLogs(string string_15)
        {
            MethodInvoker method = null;
            Class31 class2 = new Class31
            {
                richTextBox_0 = richLogs,
                string_0 = string_15
            };
            try
            {
                if (method == null)
                {
                    method = new MethodInvoker(class2.method_0);
                }
                this.Invoke(method);
            }
            catch (Exception)
            {
            }
        }

        private bool setupCauHinh()
        {

            return true;
        }
   
        private void method_LoadAccount(List<DetailLDModel> list_ld)
        {
            //Data dt = new Data();
            //List<DetailLDModel> ls = dt.selectDetailLD("select * FROM DetailLD order by ldid  ");


            foreach (DetailLDModel acc in list_ld)
            {
                method_Datagridview(acc);
            }
        }
        private void method_Datagridview(DetailLDModel acc)
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
                cell2.Value = acc.LDID;
                dataGridViewRow.Cells.Add(cell2);

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

        private void btnRun_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            // ClearMessage();
            list_ldrun = new List<LDRun>();
            list_ld = new List<string>();
            if (File.Exists(txtPathImagePast.Text))
           
                list_contact = File.ReadAllLines(txtPathImagePast.Text).ToList();
            else
            {
                MessageBox.Show("Chưa có số điện thoại. Hãy cung cấp số điện thoại!");
                return;
            }
                

            if (list_contact.Count == 0)
            {
                MessageBox.Show("Chưa có số điện thoại. Hãy cung cấp số điện thoại!");
                return;
            }
               

            string pathlog = Application.StartupPath + "\\logs";
            if (!Directory.Exists(pathlog))
            {
                Directory.CreateDirectory(pathlog);
            }
            startTuongTac();
        }
        private void startTuongTac()
        {
            stop = false;
            pibStatus.Visible = true;
            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    DetailLDModel acc = (DetailLDModel)row.Tag;
                    list_dr.Add(row);
                    list_ld.Add(acc.LDID.ToString());
                }
            }
            list_ld = list_ld.Distinct().ToList();
            if (list_ld.Count == 0)
            {
                MessageBox.Show("Hãy chọn những LD cần chạy");
                pibStatus.Visible = false;
                return;
            }
            num4LD = list_contact.Count / list_ld.Count;

            if (list_ld.Count == 0)
            {
                MessageBox.Show("Hãy chọn những LD cần chạy");
                pibStatus.Visible = false;
                return;
            }
            else
            {
                pibStatus.Visible = true;
                this.thread_1 = new Thread(new ThreadStart(this.runTuongTac));
                thread_1.IsBackground = true;
                this.thread_1.Start();
            }

        }

        private void runTuongTac()
        {
            var token = tokenSource.Token;
        Lb_quayvong:
            int numthread = SettingTool.configld.numthread;
            if (numthread > list_ld.Count)
            {
                numthread = list_ld.Count;
            }

            if (list_ld.Count > 0)
            {
                object synDevice = new object();
                Task[] tasks = new Task[numthread];
                for (int p = 0; p < numthread; p++)
                {
                    int t = p;
                    tasks[t] = Task.Factory.StartNew(() =>
                    {
                        String ldid = "";
                        List<DataGridViewRow> list_ac = new List<DataGridViewRow>();
                        List<string> lscontact4LD = new List<string>();
                        lock (synDevice)
                        {
                            ldid = list_ld[0];
                            list_ld.Remove(ldid);

                            if (chkChiadeu.Checked)
                            {
                                if (list_contact.Count < num4LD)
                                {
                                    for (int i = 0; i < list_contact.Count; i++)
                                    {
                                        lscontact4LD.Add(list_contact[0]);
                                        list_contact.RemoveAt(0);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < num4LD; i++)
                                    {
                                        lscontact4LD.Add(list_contact[0]);
                                        list_contact.RemoveAt(0);
                                    }
                                }
                            }

                            else
                                lscontact4LD = list_contact;

                            foreach (DataGridViewRow dr in list_dr)
                            {
                                DetailLDModel ld = (DetailLDModel)dr.Tag;
                                if (ld.LDID.ToString() == ldid)
                                {
                                    list_ac.Add(dr);
                                }
                            }
                        }
                        method_Start(ldid, lscontact4LD, list_ac,token);
                    },token);
                    Thread.Sleep(SettingTool.configld.timedelay * 1000);
                }
                try
                {
                    Task.WaitAll(tasks);
                }
                catch
                { }
              
                if (list_ld.Count > 0)
                {
                    goto Lb_quayvong;
                }
                else
                {
                    method_Stop();
                }
            }

        }
        private void method_Stop()
        {
            pibStatus.Visible = false;

            stop = true;
            if (thread_1 != null)
                thread_1.Abort();
        }
        private void method_Start(string ldID, List<string> lscontact, List<DataGridViewRow> list_ld,CancellationToken token)
        {
            try
            {
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                DataGridViewRow dr = list_ld[0];
                string mess = "";
                method_log("Open LDPlayer Id: " + ldID);
                userLD u = frm_main.checkExits(ldID);
                frm_main.addLDToPanel(u);
                if (ld.launchSetPosion(ldID, u, token))
                {
                    u.setStatus(ldID, "Kết nối thành công LD...");
                    Thread.Sleep(2000);
                    u.setStatus(ldID, " Bắt đầu cập nhật danh bạ...");
                    dr.Cells["Message"].Value = "Bắt đầu cập nhật danh bạ";
                    mess = Importcontact(ldID, lscontact);
                    u.setStatus(ldID, mess);
                    dr.Cells["Message"].Value = mess;
                }
                else
                {
                    if (ld.autoRunLDSetPosition(ldID, u, token))
                    {
                        u.setStatus(ldID, "Kết nối thành công LD...");
                        Thread.Sleep(2000);
                        u.setStatus(ldID, " Bắt đầu cập nhật danh bạ...");
                        dr.Cells["Message"].Value = "Bắt đầu cập nhật danh bạ";
                        mess = Importcontact(ldID, lscontact);
                        u.setStatus(ldID, mess);
                        dr.Cells["Message"].Value = mess;
                        Thread.Sleep((int)numDelay.Value * 1000);
                    }
                    else
                    {
                        u.setStatus(ldID, "Disconnected...");
                        method_log("Không kết nối được với LD: " + ldID);

                        return;
                    }
                } 
                if (!SettingTool.configld.has_quitLD)
                {
                    ld.quit1(ldID);
                    frm_main.removeLDToPanel(u);
                }
                Thread.Sleep(5000);
                runTuongTac1LD(token);
            }
            catch
            { }
            
        }
        private string Importcontact(string ldID, List<string> strNumber)
        {
            string cmd = string.Format(" shell pm clear com.android.providers.contacts");
            string output = ld.runAdb(ldID, cmd);

            // string[] strNumber = txtNumber.Lines;

            List<string> ls = new List<string>();
            string lsName = "{hùng|dũng|hoa|nụ|anh|tuấn|hồng|nhung|nguyệt|loan|thành|sỹ|phong|link|trinh|nga|mai|thảo|trang|ly|giang|tiến}";

            string fullname = "{đinh| đinh nguyễn| lê trần| lê|trần|đỗ văn| đỗ|tạ|nguyễn|hứa hữu|lương|hồ ngọc|lân|bạch|huỳnh|hoàng nguyễn|lê|đường|trần huyền|đinh ngọc|cao thái|lý tiểu}";

            Random rd = new Random();
            if (strNumber.Count > 0)
            {
                for (int n = 0; n < strNumber.Count; n++)
                {
                    ls.Add("BEGIN:VCARD");
                    ls.Add("VERSION:3.0");
                    ls.Add("FN:" + FunctionHelper.method_Spin(fullname) + " " + FunctionHelper.method_Spin(lsName));
                    ls.Add("TEL;TYPE=CELL:" + strNumber[n]);
                    ls.Add("END:VCARD");
                }
                string nameRandom = string.Format("contact_{0}.vcf", rd.Next(0, 99999).ToString());
                //  string namefilevcf = string.Format("c:\\test\\contact_{0}.vcf", nameRandom);

                string namefilevcf = string.Format("c:\\test\\{0}\\pictures\\temp\\{1}", ldID, nameRandom);

                File.AppendAllLines(namefilevcf, ls);
                if(SettingTool.configld.versionld=="3.x")
                    cmd = string.Format(" shell mv -i storage/emulated/legacy/pictures/temp/{0} sdcard/", nameRandom);
                else
                {
                    cmd = string.Format(" shell mv -i storage/emulated/0/pictures/temp/{0} sdcard/", nameRandom);
                }
                output = ld.runAdb(ldID, cmd);

                cmd = string.Format("shell am start -t text/x-vcard -d file:///storage/emulated/0/{0} -a android.intent.action.VIEW com.android.contacts", nameRandom);
                cmd = ld.runAdb(ldID, cmd);

                List<DetechModel> ls_detecth = new List<DetechModel>();
                DetechModel dtmodel = new DetechModel();
                dtmodel = new DetechModel();
                dtmodel.content = "Nhập liên hệ từ vCard?";
                dtmodel.text = "ok";
                dtmodel.function = 1;
                dtmodel.node = "//node[contains(@class,'android.widget.Button')]";
                ls_detecth.Add(dtmodel);

                dtmodel = new DetechModel();
                dtmodel = new DetechModel();
                dtmodel.content = "Cho phép";
                dtmodel.text = "Cho phép";
                dtmodel.function = 1;
                dtmodel.node = "//node[contains(@class,'android.widget.Button')]";
                ls_detecth.Add(dtmodel);


            lb_start:
                DetechModel result = ld.RunDetechFunction(ldID, ls_detecth);

                if (result.status)
                {
                    ld.ClickOnLeapdroidPosition(ldID, result.point);
                    Thread.Sleep(1000);
                    goto lb_start;
                }

                File.Delete(namefilevcf);
                return "Cập nhập thành công: " + strNumber.Count().ToString() + " số";
            }
            return "Không có số điện thoại";

        }
        private void method_log(string string_15)
        {
            MethodInvoker method = null;
            Class31 class2 = new Class31
            {
                richTextBox_0 = richLogs,
                string_0 = string_15
            };
            try
            {
                if (method == null)
                {
                    method = new MethodInvoker(class2.method_0);
                }
                this.Invoke(method);
            }
            catch (Exception)
            {
            }
        }
        [CompilerGenerated]
        private sealed class Class31
        {
            public RichTextBox richTextBox_0;
            public string string_0;

            public void method_0()
            {
                try
                {
                    if (richTextBox_0.Lines.Length > 50)
                        richTextBox_0.Text = "";

                    if (this.string_0.Contains("being aborted"))
                    {
                        this.string_0 = "Luồng đang chạy bị tạm ngừng -> STOP !!!";
                    }
                    this.richTextBox_0.Text = string.Format("{0}:{1}\n", DateTime.Now.ToString("HH:mm:ss"), this.string_0) + this.richTextBox_0.Text;
                }
                catch { }
            }

        }
        private void ClearMessage()
        {
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                dgvUser.Rows[i].Cells["Message"].Value = "";

            }
        }
        private void btn_config_Click(object sender, EventArgs e)
        {
            frm_Config_PRO frm = new frm_Config_PRO();
            frm.ShowDialog();
            //method_Config();
        }
        private sealed class Class34
        {
            public Color color_0;
            public DataGridViewRow dataGridViewRow_0;

            public void method_0()
            {
                this.dataGridViewRow_0.DefaultCellStyle.BackColor = this.color_0;
            }
        }
        private void changeColor(DataGridViewRow dataGridViewRow_0, Color color_0)
        {
            Class34 class2 = new Class34
            {
                dataGridViewRow_0 = dataGridViewRow_0,
                color_0 = color_0
            };
            this.Invoke(new MethodInvoker(class2.method_0));
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            method_Stop();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool check = false;
            if (checkBox2.Checked)
            {
                check = true;
            }
            else
                check = false;
            foreach (DataGridViewRow row2 in this.dgvUser.Rows)
            {
                row2.Cells[0].Value = check;

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var fldrDlg = new OpenFileDialog();

            if (fldrDlg.ShowDialog() == DialogResult.OK)
            {
                txtPathImagePast.Text = fldrDlg.FileName;
                list_contact = File.ReadAllLines(txtPathImagePast.Text).ToList();
                lblTotal.Text = "Danh sách có " + list_contact.Count.ToString() + " số";
            }
        }
        private void runTuongTac1LD(CancellationToken token)
        {
            try
            {
                int numthread = 1;
                if (numthread > list_ld.Count)
                {
                    numthread = list_ld.Count;
                }
                if (list_ld.Count > 0)
                {
                    object synDevice = new object();
                    Task[] tasks = new Task[numthread];
                    for (int p = 0; p < numthread; p++)
                    {
                        int t = p;
                        tasks[t] = Task.Factory.StartNew(() =>
                        {
                            String ldid = "";
                            List<DataGridViewRow> list_ac = new List<DataGridViewRow>();
                            List<string> lscontact4LD = new List<string>();
                            lock (synDevice)
                            {
                                ldid = list_ld[0];
                                list_ld.Remove(ldid);

                                if (chkChiadeu.Checked)
                                {
                                    if (list_contact.Count < num4LD)
                                    {
                                        for (int i = 0; i < list_contact.Count; i++)
                                        {
                                            lscontact4LD.Add(list_contact[0]);
                                            list_contact.RemoveAt(0);
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < num4LD; i++)
                                        {
                                            lscontact4LD.Add(list_contact[0]);
                                            list_contact.RemoveAt(0);
                                        }
                                    }
                                }

                                else
                                    lscontact4LD = list_contact;

                                foreach (DataGridViewRow dr in list_dr)
                                {
                                    DetailLDModel ld = (DetailLDModel)dr.Tag;
                                    if (ld.LDID.ToString() == ldid)
                                    {
                                        list_ac.Add(dr);
                                    }
                                }
                            }
                            method_Start(ldid, lscontact4LD, list_ac, token);
                        }, token);
                        Thread.Sleep(SettingTool.configld.timedelay * 1000);
                    }
                    try
                    {
                        Task.WaitAll(tasks);
                    }
                    catch
                    { }

                }
            }
            catch (Exception ex)
            {
                method_log(ex.ToString());
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            for (int i = (int) fromLD.Value ; i < (int) toLD.Value ; i++)
            {
                DetailLDModel ld = new DetailLDModel();
                ld.LDID = i;
                method_Datagridview(ld);
            }
        }
    }
}
