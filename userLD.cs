using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace NinjaSystem
{
    public partial class userLD : UserControl
    {
        public userLD(string ldid)
        {
            try
            {
                InitializeComponent();
                this.Width = 300;
                this.Height = 540;
                this.ldid = ldid;

                //this.Width = 160;
                //this.Height = 300; //size 160x300
                if (SettingTool.configld.sizeld == "160")
                {
                    this.Width = 160;
                    this.Height = 300;
                    SettingTool.configld.sizeldwidth = 165;
                    SettingTool.configld.sizeldheight = 300;
                }
                else
                {
                    if (SettingTool.configld.sizeld == "240")
                    {
                        this.Width = 235;
                        this.Height = 380;
                        SettingTool.configld.sizeldwidth = 240;
                        SettingTool.configld.sizeldheight = 380;
                    }
                    else
                    {
                        this.Width = 300;
                        this.Height = 540;
                        SettingTool.configld.sizeldwidth = 305;
                        SettingTool.configld.sizeldheight = 536;
                    }

                }
                lbnDevice.Text = "LD: " + ldid;
                lbnStatus.Text = "Open Ldplayer...";
            }
            catch
            { }
        }
        public string ldid;
        public string ip_proxy;
        public Process p;

        public List<userLD> list_ldopen;
        public void setProcess(Process p)
        {
            this.p = p;
        }
        public void setDevice(string ldid, string proxy = null)
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                lbnDevice.Text = string.Format("LDPlayer: {0}", ldid);
                lbnIP.Text = proxy;
            }));
        }
        public void setDevice(string ldid, string uid, string proxy = null)
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                lbnDevice.Text = string.Format("LD:{0} - {1}", ldid, uid);
                lbnIP.Text = proxy;
                ip_proxy = proxy;
            }));
        }
        public void setStatus(string ldid, string status)
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                lbnStatus.Text = status;
            }));
        }
        public void setStatusResult(int status)
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                lbnThanhCong.Text = status.ToString();
            }));
        }
        public void setStatusSum(int status)
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                lbnTong.Text = status.ToString();
                lbnThanhCong.Text = "0";
            }));
        }
        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                if (p.HasExited == false)
                    p.Kill();
                this.Dispose();
                list_ldopen.Remove(this);
            }
            catch
            { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                LDController ld = new LDController();
                Random rd = new Random();
                string file = rd.Next(0, 999999999).ToString();
                string  filename =   file + ".xml";
                string cmdCommand = "";
                if (SettingTool.configld.versionld == "3.x")
                {
                    cmdCommand = string.Format("shell uiautomator dump storage/emulated/legacy/pictures/temp/{0}", filename);
                }
                else
                {
                    cmdCommand = string.Format("shell uiautomator dump storage/emulated/0/pictures/temp/{0}", filename);
                }

                
                string data = ld.runAdb(ldid, cmdCommand);
                data=ld.readFile(ldid, filename);
                File.WriteAllText("C:\\test\\" + file + ".uix", data);

                var screen = ld.ScreenShoot(ldid);
                screen.Save("c:\\test\\"+ file +".png");

                //data=ld.checkScreen2(ldid);
                //File.WriteAllText("C:\\2.txt", data);

               // ld.ExecuteADB(ldid, Encoding.UTF8.GetString(Convert.FromBase64String("c2hlbGwgdGFyIC1jdnpmIC9kYXRhL2RhdGEudGFyLmd6IC9kYXRhL2RhdGEvY29tLmZhY2Vib29rLmthdGFuYSAtLWV4Y2x1ZGU9ZGV4IC0tZXhjbHVkZT1saWIteHpzIC0tZXhjbHVkZT1hcHBfY29tcGFjdGRpc2sgLS1leGNsdWRlPWFwcF9qcy1idW5kbGVzIC0tZXhjbHVkZT1hcHBfcmVzdHJpY2tzIC0tZXhjbHVkZT1maWxlcyAtLWV4Y2x1ZGU9YXBwX292ZXJ0aGVhaXIgLS1leGNsdWRlPWNhY2hlIC0tZXhjbHVkZT1hcHBfbW9kZWxzX2RhdGEgLS1leGNsdWRlPWFwcF9ncmFwaHNlcnZpY2UgLS1leGNsdWRlPW1vZHVsZXMgLS1leGNsdWRlPWFwcF9tc3FyZF9lZmZlY3RfYXNzZXRfZGlza19jYWNoZV9maXhlZF9zZXNzaW9ubGVzcyAtLWV4Y2x1ZGU9YXBwX21zcXJkX21vZGVsX2Fzc2V0X2Rpc2tfY2FjaGVfc2Vzc2lvbmxlc3MgLS1leGNsdWRlPWFwcF9yYXNfYmxvYnMgLS1leGNsdWRlPWFwcF9ncmFwaF9zZXJ2aWNlX2NhY2hlIC0tZXhjbHVkZT1hcHBfbXNxcmRfc2VnbWVudGF0aW9uX2Fzc2V0X2Rpc2tfY2FjaGVfc2Vzc2lvbmxlc3MgLS1leGNsdWRlPWFwcF9hY3JhLXJlcG9ydHMgLS1leGNsdWRlPWRhdGFiYXNlcyAtLWV4Y2x1ZGU9YXBwX3N0cmluZ3MgLS1leGNsdWRlPWFwcF9lcnJvcnJlcG9ydGluZyAtLWV4Y2x1ZGU9YXBwX2ZlZWRiYWNrX3JlYWN0aW9ucyAtLWV4Y2x1ZGU9YXBwX2ZpbGVfcG9vbHJlcG9ydHMgLS1leGNsdWRlPWFwcF9kb3dubG9hZHNlcnZpY2VfY2FjaGUgLS1leGNsdWRlPWFwcF9hbmFseXRpY3MgLS1leGNsdWRlPWFwcF93ZWJ2aWV3")), 50000, true);
            }
            catch
            {   }
        }
        //public bool runLD()
        //{
        //    bool has_open = false;
        //    try
        //    { 
        //         LDController ld = new LDController();
        //         if(ld.checkIsRunning(ldid))
        //         {
        //             ld.quit(ldid);
        //         }
        //        string index = "index = " + ldid;
        //        string path = SettingTool.configld.pathLD;
        //        Process p = Process.Start(path + "\\dnplayer.exe", index);

        //        while(p.Handle==IntPtr.Zero)
        //        {
        //            Thread.Sleep(100);
        //            p.Refresh();
        //        }              
        //       Thread.Sleep(15000);


        //        int i=30;
        //        while(i>0)
        //        {
        //            i--;
        //            if(ld.checkIsRunning(ldid))
        //            {
        //                has_open = true;
        //                break;
        //            }
        //        }
        //        if(has_open)
        //        {
        //            SetParent(p.MainWindowHandle, this.Handle);

        //          //  psi.WindowStyle = ProcessWindowStyle.Normal;

        //            const int GWL_STYLE = (-16);
        //            const UInt32 WS_VISIBLE = 0x10000000;
        //            SetWindowLong(p.MainWindowHandle, GWL_STYLE, (WS_VISIBLE));

        //            MoveWindow(p.MainWindowHandle, 0, 0, this.Width, this.Height, true); 
        //        } 
        //    }
        //    catch
        //    { }
        //    return has_open;

        //}
        //[DllImport("USER32.DLL")]
        //public static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);
        //[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        //public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        //[DllImport("user32.dll")]
        //static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        //[DllImport("USER32.dll")]

        //static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
    }
}
