using KAutoHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;

namespace NinjaPhone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        IntPtr handleLeapdroid;
        int point_X;
        int point_Y;
        int hight;
        int width;
        private void button1_Click(object sender, EventArgs e)
        {
            handleLeapdroid = getLeapdroidHandle("Leapdroid (v1.8.0.0)");
            MessageBox.Show(handleLeapdroid.ToString());
            //ClickOnLeapdroidPosition(75, 150);
            // postStatus();
        }

        void ClickOnLeapdroidPosition(int x, int y)
        {

            AutoControl.SendClickOnPosition(handleLeapdroid, x, y);
        }
        void PressEnterOnLeapdroid( NinjaPhone.VKeys key)
        {

            AutoControl.SendKeyBoardPress(handleLeapdroid, key);
        }
        void PressOnLeapdroid(string keys)
        {
            AutoControl.SendTextKeyBoard(handleLeapdroid, keys);


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

        private void button2_Click(object sender, EventArgs e)
        {
            scrollNewfeed();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            scrollLikeNewFeed(7);
        }
        #region function
        private void postStatus()
        {

            ClickOnLeapdroidPosition(108, 219);
            Delay(10);
            ClickOnLeapdroidPosition(225, 160);
            Delay(2);
            ClickOnLeapdroidPosition(85, 180);
            Delay(1);
            PressOnLeapdroid("Cuoi tuan vui ve nhe ca nha");
            Delay(2);
            ClickOnLeapdroidPosition(430, 750);
            Delay(2);
            ClickOnLeapdroidPosition(230, 500);
            Delay(2);
            ClickOnLeapdroidPosition(258, 748);
        }
        IntPtr getLeapdroidHandle(string name)
        {
            IntPtr handle = AutoControl.FindWindowHandle(null, name);

            return handle;
        }
        private void scrollNewfeed()
        {
            get_point(handleLeapdroid);

            for (int i = 0; i < 7; i++)
            {
                scroll_up(20);
                Delay(2);
                scroll_up(20);
                Delay(2);
            }
        }
        private bool likePost()
        {
            try
            {
                get_point(handleLeapdroid);
                // var screen = KAutoHelper.CaptureHelper.CaptureWindow(handleLeapdroid);

                var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                // var screen = capture();
               // screen.Save("main.png");

                var like = ImageScanOpenCV.GetImage("img\\like.png");

               // var img = ImageScanOpenCV.Find((Bitmap)screen, like);
               // img.Save("img.png");

                var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                if (point != null)
                {

                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);

                    return true;
                }
            }
            catch 
            {
                //MessageBox.Show(e.ToString());
            }
            return false;
        }
        private void scrollLikeNewFeed(int numLike)
        {

            int dem = 0;
            while (dem < numLike)
            {

                scroll_up(30);
                Delay(2);
                if (likePost())
                {
                    dem++;
                }
                Delay(2);

            }
        }

        private Bitmap capture()
        {
            Bitmap screenBitmap;
            Graphics screenGraphics;

            screenBitmap = new Bitmap(480, 800, PixelFormat.Format32bppArgb);
            screenGraphics = Graphics.FromImage(screenBitmap);
            screenGraphics.CopyFromScreen(806, 87, 0, 0, new Size(480, 800), CopyPixelOperation.SourceCopy);

            return screenBitmap;
        }
        private void get_point(IntPtr handle)
        {
            IntPtr windowDC = User32.GetWindowDC(handle);
            User32.RECT rect = new User32.RECT();
            User32.GetWindowRect(handle, ref rect);

            point_X = rect.left;
            point_Y = rect.top;

            width = rect.right - rect.left;
            hight = rect.bottom - rect.top;
        }


        public static Image CaptureWindow(IntPtr handle)
        {
            IntPtr windowDC = User32.GetWindowDC(handle);
            User32.RECT rect = new User32.RECT();
            User32.GetWindowRect(handle, ref rect);
            int nWidth = rect.right - rect.left;
            int nHeight = rect.bottom - rect.top;
            IntPtr hObject = GDI32.CreateCompatibleDC(windowDC);
            IntPtr ptr3 = GDI32.CreateCompatibleBitmap(windowDC, nWidth, nHeight);
            GDI32.BitBlt(hObject, 0, 0, nWidth, nHeight, windowDC, 0, 0, 13369376);
            GDI32.SelectObject(hObject, GDI32.SelectObject(hObject, ptr3));
            GDI32.DeleteDC(hObject);
            User32.ReleaseDC(handle, windowDC);
            Image image = Image.FromHbitmap(ptr3);
            GDI32.DeleteObject(ptr3);
            return image;
        }

        private class User32
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
        }

        private class GDI32
        {
            public const int SRCCOPY = 13369376;

            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        private void seachLikePage(string keyword, int numLike)
        {

            get_point(handleLeapdroid);
            //  var screen = KAutoHelper.CaptureHelper.CaptureWindow(handleLeapdroid);
            var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
            //var screen = CaptureHelper.CaptureWindow(handleLeapdroid);
            //  screen.Save("main.png");
            var like = ImageScanOpenCV.GetImage("img\\search.png");

            var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
            if (point != null)
            {
                ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                Delay(0.5);
                PressOnLeapdroid(keyword);
                Delay(3);
                screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                //screen.Save("main.png");
                like = ImageScanOpenCV.GetImage("img\\seeresults.png");

                point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                if (point != null)
                {
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                    Delay(7);
                }


                screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                like = ImageScanOpenCV.GetImage("img\\page.png");
                point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                if (point != null)
                {
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                    Delay(6);
                    int dem = 0;
                    while (dem < numLike)
                    {

                        if (likePage())
                        {
                            dem++;
                            Delay(1);
                        }
                        // khi het ket qua tim kiem thi back ve 
                        if (checkRelateSeach())
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                likePage();
                            }
                            back(3);
                            return;
                        }
                        scroll_up(15);
                        Delay(2);
                    }
                    back(3);
                }

            }



        }
        private bool checkRelateSeach()
        {
            bool check = false;
            var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
            var like = ImageScanOpenCV.GetImage("img\\relatesearch.png");
            var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
            if (point != null)
            { check = true; }
            return check;

        }
        private void back(int times)
        {
            var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
            var like = ImageScanOpenCV.GetImage("img\\back.png");
            var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
            if (point != null)
            {
                for (int i = 0; i < times; i++)
                {
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                    Delay(0.5);
                }

            }

        }

        private bool likePage()
        {
            try
            {
                var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                //screen.Save("main.png");
                var like = ImageScanOpenCV.GetImage("img\\likepage.png");
                //var img = ImageScanOpenCV.Find((Bitmap)screen, like);
                //img.Save("img.png");
                var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                if (point != null)
                {
                    ClickOnLeapdroidPosition(point.Value.X + 12, point.Value.Y + 12);
                    return true;
                }
            }
            catch
            { }
            return false;
        }
        private bool commentPost()
        {
            try
            {

                get_point(handleLeapdroid);
                //  var screen = KAutoHelper.CaptureHelper.CaptureWindow(handleLeapdroid);
                var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                //var screen = CaptureHelper.CaptureWindow(handleLeapdroid);
              //  screen.Save("main.png");
                var like = ImageScanOpenCV.GetImage("img\\comment.png");

                //var img = ImageScanOpenCV.Find((Bitmap)screen, like);
                //img.Save("img.png");

                var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                if (point != null)
                {
                    //click vao nut comment
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                    Delay(7);

                    //click vao textbox input
                    screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                    // screen.Save("main.png");
                    like = ImageScanOpenCV.GetImage("img\\write_comment.png");
                    //img = ImageScanOpenCV.Find((Bitmap)screen, like);
                    //img.Save("img.png");
                    point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                    Delay(1);

                    // input noi dung
                    PressOnLeapdroid("hi");
                    Delay(2);

                    //click vao mui ten gui
                    screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                    //  screen.Save("main.png");
                    like = ImageScanOpenCV.GetImage("img\\enter.png");
                    //img = ImageScanOpenCV.Find((Bitmap)screen, like);
                    //img.Save("img.png");
                    point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                    Delay(3);

                    // click vao nut back de quay lai
                    back(1);
                   
                    return true;
                }
            }
            catch
            { }
            return false;
        }
        private void scrollCommentNewFeed(int numLike)
        {
            int dem = 0;

            while (dem < numLike)
            {
                scroll_up(20);
                Delay(3);
                if (commentPost())
                {
                    dem++;

                }
                Delay(2);
            }
        }
        private void scroll(int delay)
        {
            ClickOnLeapdroidPosition(309, 420);
            Delay(3);
            ClickOnLeapdroidPosition(309, 395);
        }
        private void viewVideoShare(int num)
        {
            get_point(handleLeapdroid);
            var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
            var like = ImageScanOpenCV.GetImage("img\\menu.png");
            var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
            var like_empty = ImageScanOpenCV.GetImage("img\\menuempty.png");
            var point_empty = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like_empty);

            if (point != null || point_empty != null)
            {
                if (point != null)
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                else
                    ClickOnLeapdroidPosition(point_empty.Value.X, point_empty.Value.Y);

                Delay(3);
                screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                like = ImageScanOpenCV.GetImage("img\\videos.png");
                point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);

                Delay(5);
                int dem = 0;
                while (dem < num)
                {

                    scroll_up(30);
                    if (sharePost())
                    {
                        dem++;
                    }
                    Delay(5);

                }
                back(2);

            }
        }
        private bool sharePost()
        {
            try
            {
                var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));;
               
                var like = ImageScanOpenCV.GetImage("img\\share.png");
                
                var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                if (point != null)
                {
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);

                    Delay(1);
                     screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32)); ;

                     like = ImageScanOpenCV.GetImage("img\\sharepostnow.png");

                     point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);

                    if (point != null)
                        ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);

                    return true;
                }
            }
            catch
            { }
            return false;
        }
        private void viewAddFriend(int num)
        {
            int dem = 0;
            get_point(handleLeapdroid);

            var point = intoFriends();


            if (point != null)
            {
                ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                Delay(2);
                while (dem < num)
                {
                    if (addFriend())
                    {
                        dem++;
                        Delay(1.5);
                    }
                }
                back(2);
                return;
            }




        }

        private bool addFriend()
        {
            try
            {
                var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                //screen.Save("main.png");
                var like = ImageScanOpenCV.GetImage("img\\addfriend.png");
                //var img = ImageScanOpenCV.Find((Bitmap)screen, like);
                //img.Save("img.png");
                var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                if (point != null)
                {
                    // point.Save("add.png");
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);

                    return true;
                }
            }
            catch
            { }
            return false;
        }
        private bool nextPage()
        {
            try
            {
                var screen = CaptureHelper.CaptureWindow(handleLeapdroid);
                var like = ImageScanOpenCV.GetImage("img\\nextadd.png");

                var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                if (point != null)
                {
                    ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                    Delay(5);
                    return true;
                }
            }
            catch
            { }
            return false;
        }
        private bool scrollPage(int delay)
        {
            try
            {

                ClickOnLeapdroidPosition(459, 648);
                Delay(delay);
                ClickOnLeapdroidPosition(462, 620);
            }
            catch
            { }
            return false;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            if (txt_search.Text == "")
            {
                MessageBox.Show("Nhập nội dung tìm kiếm");
                return;
            }
            seachLikePage(txt_search.Text, 5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            scrollCommentNewFeed(3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            viewVideoShare(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            viewAddFriend(10);
        }
        noxController nox;
        private void button8_Click(object sender, EventArgs e)
        {
            handleLeapdroid = getLeapdroidHandle("NoxPlayer");
            nox = new noxController(handleLeapdroid);
            MessageBox.Show(handleLeapdroid.ToString());

        }

        private void button9_Click(object sender, EventArgs e)
        {
            clickApp(383, 719);
        }
        private void clickApp(int x, int y)
        {
            AutoControl.SendClickDownOnPosition(handleLeapdroid, x, y, EMouseKey.LEFT);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            nox.scrollLikeNewFeed(4);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void Login()
        {
            ClickOnLeapdroidPosition(143, 335);
            Delay(2);
            PressOnLeapdroid("100002092801681");
            Delay(2);
            ClickOnLeapdroidPosition(210, 389);
            Delay(2);
            PressOnLeapdroid("ninja14");
            Delay(2);
            ClickOnLeapdroidPosition(237, 448);
        }
        private void methodScroll()
        {
            Point p = new Point(46, 760);
            uint X = (uint)p.X;
            uint Y = (uint)p.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        private void button12_Click(object sender, EventArgs e)
        {
            SendClickDownOnPosition(handleLeapdroid, 74, 797, 0);
            Delay(3);
            SendClickDownOnPosition(handleLeapdroid, 35, 200, 0x0004);

        }

        public static void SendClickDownOnPosition(IntPtr controlHandle, int x, int y, int msg, int clickTimes = 1)
        {

            IntPtr lParam = MAKELPARAM(x, y);
            for (int i = 0; i < clickTimes; i++)
            {
                SendMessage(controlHandle, msg, new IntPtr(1), lParam);
            }
        }
        internal static IntPtr MAKELPARAM(int x, int y)
        {
            return (IntPtr)((y << 0x10) | (x));
        }
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);


        private void scroll_up(int length)
        {
            Point old_point = Cursor.Position;
            AutoControl.MouseDragY(new Point(point_X + 200, point_Y + 400), length, true);
            Cursor.Position = old_point;

        }

        private void scroll_down()
        {

            Point old_point = Cursor.Position;
            AutoControl.MouseDragY(new Point(point_X + 200, point_Y + 150), 250, false);
            Cursor.Position = old_point;

        }

        private void MouseDragY(Point startPoint, int deltaY, bool isNegative = false)
        {
            Point old_point = Cursor.Position;

            SendClickDownOnPosition(handleLeapdroid, 160, 160, 1);

            //Cursor.Position = startPoint;
            AutoControl.mouse_event(2u, 0, 0, 0, UIntPtr.Zero);
            for (int i = 0; i < deltaY; i++)
            {
                bool flag = !isNegative;
                if (flag)
                {
                    AutoControl.mouse_event(1u, 0, 1, 0, UIntPtr.Zero);
                }
                else
                {
                    AutoControl.mouse_event(1u, 0, -1, 0, UIntPtr.Zero);
                }
            }
            AutoControl.mouse_event(32772u, 0, 0, 0, UIntPtr.Zero);
            Cursor.Position = old_point;
        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            scrollAceeptFriend(5);
        }

        private void scrollAceeptFriend(int numLike)
        {
            int dem = 0;

            get_point(handleLeapdroid);
            var point = intoFriends();

            if (point != null)
            {

                ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);

                Delay(3);
                var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                var like = ImageScanOpenCV.GetImage("img\\requests.png");
                point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);
                Delay(2);
                scroll_down();
                Delay(2);
                while (dem < numLike)
                {
                    if (AcceptFriend())
                    {
                        Delay(1);
                        dem++;

                    }

                    Delay(2);
                }
                back(2);

            }


        }

        private Point? intoFriends()
        {

            Point? pt = null;


            var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
            screen.Save("main.png");
            var like = ImageScanOpenCV.GetImage("img\\menu.png");
            var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
            if (point != null)
            {
                pt = point;
            }

            var like_empty = ImageScanOpenCV.GetImage("img\\menuempty.png");
            var img_empty = ImageScanOpenCV.Find((Bitmap)screen, like_empty);
            // img_empty.Save("img_empty.png");
            var point_empty = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like_empty);
            if (point_empty != null)
            {
                pt = point_empty;
            }

            ClickOnLeapdroidPosition(pt.Value.X, pt.Value.Y);
            Delay(2);
            screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
            like = ImageScanOpenCV.GetImage("img\\friends.png");
            point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);



            return point;


        }

        private bool AcceptFriend()
        {

            try
            {

                for (int i = 0; i < 3; i++)
                {
                    var screen = KAutoHelper.CaptureHelper.CaptureImage(new Size(width - 9, hight - 32), new Point(point_X + 9, point_Y + 32));
                    //var screen = CaptureHelper.CaptureWindow(handleLeapdroid);
                    screen.Save("main.png");
                    var like = ImageScanOpenCV.GetImage("img\\confirm.png");

                    var img = ImageScanOpenCV.Find((Bitmap)screen, like);
                    img.Save("img.png");
                    var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, like);
                    if (point != null)
                    {
                        ClickOnLeapdroidPosition(point.Value.X, point.Value.Y);

                    }
                }
                return true;

            }
            catch { }
            return false;

        }
    }
}
