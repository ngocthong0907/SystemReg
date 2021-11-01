using DotRas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    //public class ConnectDcom : DcomHelper
    //{
    //    private RichTextBox lbnStatus;
    //    public ConnectDcom(RichTextBox _lbnStatus)
    //        : base(Global.AutoIt)
    //    {
    //        this.lbnStatus = _lbnStatus;
    //    }
    //    protected override void ShowStatus()
    //    {
    //        this.lbnStatus.Text = string.Format("{0}:{1}\n", DateTime.Now.ToString("HH:mm:ss"), base.status) + this.lbnStatus.Text;
    //    }
    //}
    //public static class Global
    //{
    //    public static object AutoIt = new object();
    //}
    public class DcomHelper
    {
        protected string status;

        public DcomHelper(RichTextBox richTextBox_0)
        {
           this.richTextBox_0 = richTextBox_0;
            Dialer = new RasDialer();

        }
        RichTextBox richTextBox_0;
        protected virtual void ShowStatus()
        {
        }

        public const string EntryName = "Viettel";
        private RasHandle rasHandle_0;
        private RasDialer rasDialer_0;
        public int int_0 = -1;

        public void method_Connect()
        {  
            this.Dialer.EntryName = "Viettel";
            this.Dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User);
            this.Dialer.PhoneNumber = "*99#";
            try
            {
                this.Dialer.Credentials = new NetworkCredential()
                {
                    Domain = "broadband"
                };
                this.rasHandle_0 = this.Dialer.DialAsync();

            }
            catch (Exception exception1)
            {

                Exception exception = exception1;
                method_log(richTextBox_0,exception.Message.ToString());

            }

        }
        public void method_Connect(string viettel)
        {
            if(Dialer!=null)
            {
                if (!this.Dialer.IsBusy)
                {
                    RasConnection activeConnectionByHandle = RasConnection.GetActiveConnectionByHandle(this.rasHandle_0);
                    if (activeConnectionByHandle != null)
                    {

                        activeConnectionByHandle.HangUp();

                    }
                }
                else
                {
                    this.Dialer.DialAsyncCancel();
                }
            }
            this.Dialer.EntryName = viettel;
            this.Dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User);
            this.Dialer.PhoneNumber = "*99#";
            try
            {
                this.Dialer.Credentials = new NetworkCredential()
                {
                    Domain = "broadband"
                };
                this.rasHandle_0 = this.Dialer.DialAsync();

            }
            catch (Exception exception1)
            {

                Exception exception = exception1;
                method_log(richTextBox_0, exception.Message.ToString());

            }

        }
        internal virtual RasDialer Dialer
        {
            get
            {
                return this.rasDialer_0;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler<StateChangedEventArgs> eventHandler = new EventHandler<StateChangedEventArgs>(this.method_5);
                EventHandler<DialCompletedEventArgs> eventHandler1 = new EventHandler<DialCompletedEventArgs>(this.method_6);
                RasDialer rasDialer0 = this.rasDialer_0;
                if (rasDialer0 != null)
                {
                    rasDialer0.StateChanged -= eventHandler;
                    rasDialer0.DialCompleted -= eventHandler1;
                }
                this.rasDialer_0 = value;
                rasDialer0 = this.rasDialer_0;
                if (rasDialer0 != null)
                {
                    rasDialer0.StateChanged += eventHandler;
                    rasDialer0.DialCompleted += eventHandler1;
                }
            }
        }
        private void method_5(object sender, StateChangedEventArgs e)
        {
            status = e.State.ToString();
            ShowStatus();

        }
        private void method_log(RichTextBox richLogs,string string_15)
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
                // this.Invoke(method);
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
                if (this.string_0.Contains("being aborted"))
                {
                    this.string_0 = "Luồng đang chạy bị tạm ngừng -> STOP !!!";
                }
                this.richTextBox_0.Text = string.Format("{0}:{1}\n", DateTime.Now.ToString("HH:mm:ss"), this.string_0) + this.richTextBox_0.Text;

            }
        }
        private void method_6(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                status = "Cancelled";
                ShowStatus();
            }
            else if (e.TimedOut)
            {
                status = "Connection attempt timed out";
                ShowStatus();
            }
            else if (e.Error != null)
            {
                status = e.Error.ToString();
                ShowStatus();
            }
            else if (e.Connected)
            {
                status = "Kết nối thành công";
                ShowStatus();
                int_0 = 1;
                return;
            }
            int_0 = 0;
        }
        public void method_Disconnect()
        {
            if (Dialer == null || rasHandle_0 == null)
            {
                status = "Bạn phải bật dcom bằng phần mềm mới tắt được!";
                ShowStatus();
            }
            else
            {
                if (!this.Dialer.IsBusy)
                {
                    RasConnection activeConnectionByHandle = RasConnection.GetActiveConnectionByHandle(this.rasHandle_0);
                    if (activeConnectionByHandle != null)
                    {

                        activeConnectionByHandle.HangUp();

                    }
                }
                else
                {
                    this.Dialer.DialAsyncCancel();
                }
                int_0 = -1;
                status = "Tắt kết nối Dcom!";
                ShowStatus();
            }
        }
    }
}
