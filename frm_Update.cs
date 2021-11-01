using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;
namespace NinjaSystem
{
    public partial class frm_Update : Form
    {
        public frm_Update(List<Update> listupdate)
        {
            InitializeComponent();
            StringBuilder builder = new StringBuilder();
            int i = 1;
            foreach (Update up in listupdate)
            {
                builder.AppendLine("");
                builder.AppendLine(i + ". " + up.Title + " - Version : " + up.Version);
                builder.AppendLine(up.Description.Replace("<br>", ""));
                i++;
            }
            richTextBox1.Text = builder.ToString();
            link = listupdate[0].Link;
            version = listupdate[0].Version;
        }
        double version;
        string link;
        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            process.Visible = true;
            Thread th = new Thread(new ThreadStart(method_AutoUpdate));
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        private void method_AutoUpdate()
        {
            string str;

            try
            {
                ZipFile file;
                int num = 0;

                FileStream stream2 = null;
                Stream responseStream = null;
                try
                {
                    try
                    {
                        this.process.Value = 0;
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
                        request.Proxy = null;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        request.Credentials = CredentialCache.DefaultCredentials;
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        long contentLength = response.ContentLength;

                        string path = String.Format("{0}\\update.zip", Application.StartupPath);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        stream2 = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                        responseStream = response.GetResponseStream();
                        //  num7 = ((this.versiune + num3) - num) + 1;

                        int count = 0;
                        byte[] buffer = new byte[0x400];
                        while ((count = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            stream2.Write(buffer, 0, count);
                            long num6 = (stream2.Length * 100L) / contentLength;

                            this.Invoke(new MethodInvoker(delegate()
                            {
                                process.Value = (int)num6;

                            }));
                        }
                    }
                    catch (Exception)
                    {
                    }
                    //continue;
                }
                finally
                {
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                    if (responseStream != null)
                    {
                        responseStream.Close();
                    }
                    num--;
                }
                FileInfo[] files = new DirectoryInfo(Application.StartupPath).GetFiles("*.zip");
                if (files[0] != null)
                {
                    FileInfo infoArray2 = files[0];
                    int index = 0;

                    try
                    {
                        file = ZipFile.Read(infoArray2.FullName);
                        file.ExtractAll(Application.StartupPath, true);
                        MessageBox.Show("Cập nhật thành công.Vui lòng mở phiên bản mới để sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string path = String.Format("{0}\\{1}", Application.StartupPath, infoArray2.FullName);

                        Process.Start(Application.StartupPath);
                        Application.Exit();

                    }
                    finally
                    {

                    }
                }
                else
                    MessageBox.Show("Lỗi tự động cập nhật! Vui lòng download phiên bản mới của phần mềm và copy vào thư mục để chạy", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Lỗi tự động cập nhật! Vui lòng download phiên bản mới của phần mềm và copy vào thư mục để chạy", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Process.Start(link);
        }

        private void bunifuImageButton7_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

         
    }
}
