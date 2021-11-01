using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public partial class frm_SetVPN1111 : Form
    {
        public frm_SetVPN1111(List<DetailLDModel> list_detail, frm_MainLD frm)
        {
            InitializeComponent();
            this.list_detail = list_detail;
            this.frm_main = frm;
        }
        
       
        DetailLDModel model = new DetailLDModel();

        List<DetailLDModel> list_detail;
        CancellationTokenSource tokenSource;
        bool stop;
        List<DataGridViewRow> list_dr;
        Thread thread_1;
        frm_MainLD frm_main;
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                List<string> ls_proxy = new List<string>();
                if (clipboardText.Length > 0)
                {
                    ls_proxy = clipboardText.Split('\n').ToList();
                    if (ls_proxy.Count == 0)
                    {
                        ls_proxy.Add(clipboardText.Trim());
                    } 
                    ls_proxy.RemoveAll(x => x == "");
                    List<DataGridViewRow> list_dr = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in dgvUser.Rows)
                    {
                        if ((bool)row.Cells[0].Value)
                        {
                            list_dr.Add(row);
                        }
                    }

                    foreach (DataGridViewRow dr in list_dr)
                    {
                        if (ls_proxy.Count > 0)
                        {
                            DetailLDModel acc = (DetailLDModel)dr.Tag;
                            acc.Keyvpn = ls_proxy[0].Trim();
                            dr.Cells["clKey"].Value = ls_proxy[0].Trim();
                            DetailLD_BLL detail_bll = new DetailLD_BLL();

                            detail_bll.update(acc);
                            ls_proxy.RemoveAt(0);
                        }
                        else
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Không có nội dung để Paste");
                }
            }
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            stop = false;
            tokenSource = new CancellationTokenSource();

            startTuongTac();
        }
        private void startTuongTac()
        {
            stop = false;


            list_dr = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvUser.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    list_dr.Add(row);

                }
            }
            if (list_dr.Count == 0)
            {
                MessageBox.Show("Hãy chọn những LDPlayer cần chạy");

                return;
            }
            else
            {

                this.thread_1 = new Thread(new ThreadStart(this.setKeyVPN));
                thread_1.IsBackground = true;
                this.thread_1.Start();
            }

        }
        private void setKeyVPN()
        {
            //List<PositionLD> lsPosition = new List<PositionLD>();
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
        Lb_quayvong:
            int numthread = SettingTool.configld.numthread;
            if (numthread > list_dr.Count)
            {
                numthread = list_dr.Count;
            }
            if (list_dr.Count > 0)
            {
                object synDevice = new object();
                Task[] tasks = new Task[numthread];
                for (int p = 0; p < numthread; p++)
                {
                    int t = p;
                    tasks[t] = Task.Factory.StartNew(() =>
                    {
                        if (list_dr.Count > 0)
                        {
                            DataGridViewRow dr = list_dr[0];
                            list_dr.Remove(dr);
                            runsetProxy(dr, token);
                        }

                    }, token);
                    Thread.Sleep(SettingTool.configld.timedelay * 1000);
                }
                try
                {
                    Task.WaitAll(tasks);
                }
                catch
                {
                }

                if (list_dr.Count > 0 && stop == false)
                {
                    goto Lb_quayvong;
                }
            }

        }
        private void runsetProxy(DataGridViewRow dr, CancellationToken token)
        {
            DetailLDModel model = (DetailLDModel)dr.Tag;
            string ldID = model.LDID.ToString();
            LDController ld = new LDController();

            userLD u = frm_main.checkExits(ldID);
            frm_main.addLDToPanel(u);
            u.setStatus(ldID, "Open Ldplayer: " + ldID);
            if (ld.launchSetPosion(ldID, u, token))
            {
                u.setStatus(ldID, "Kết nối thành công LD...");
            }
            else
            {
                if (ld.autoRunLDSetPosition(ldID, u, token))
                {
                    u.setStatus(ldID, "Kết nối thành công LD...");

                }
                else
                {
                    u.setStatus(ldID, "Disconnected...");
                    goto Lb_Finish;
                }
            }
            string proxy = dr.Cells["clKey"].Value.ToString();
            ld.setKeyboard(ldID);
            //kiem tra app proxy
            if (ld.checkApp(ldID, "com.cloudflare.onedotonedotonedotone") == false)
            {
                u.setStatus(ldID, "Chưa cài app proxy...");
                dr.Cells["clStatus"].Value = "Chưa cài app proxy";
                return;
            }
            else
            {
                u.setStatus(ldID, "Bắt đầu thiết lập key");
                ld.runApp(ldID, "com.cloudflare.onedotonedotonedotone");
                Thread.Sleep(1000);
                if (ld.setKeyVPN1111(ldID, proxy))
                {
                    u.setStatus(ldID, "Hoàn thành đổi proxy");
                    dr.Cells["Message"].Value = "Hoàn thành thiết lập proxy";
                }
                else
                {
                    dr.Cells["Message"].Value = "Lỗi thiết lập proxy";
                }
            }

        Lb_Finish:
            ld.quit(ldID);
            frm_main.removeLDToPanel(u);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvUser.SelectedRows)
            {
                DetailLDModel acc = (DetailLDModel)dr.Tag;
                acc.Keyvpn = ""; ;
                dr.Cells["clKey"].Value = "";
                DetailLD_BLL detail_bll = new DetailLD_BLL();
                detail_bll.update(acc);

            }
        }

        private void frm_SetVPN1111_Load(object sender, EventArgs e)
        {

            method_LoadAccount(list_detail);
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

                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = acc.Keyvpn;
                dataGridViewRow.Cells.Add(cell3);


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


    }
}
