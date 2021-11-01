using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class LiteController
    {
        //lite
        public Bitmap ConvertToFormat(System.Drawing.Image image)
        { 
            Bitmap copy = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }
            return copy;
        }
        public List<Point> DetechListPoint(string ldid,Bitmap templateImage)
        {
            List<Point> list_point = new List<Point>();
            try
            {
                LDController ld = new LDController();
                Bitmap sourceImage = ConvertToFormat(ld.ScreenShoot(ldid));
                list_point = searchBitmaps(templateImage, sourceImage, 0.2);  
                 
            }
            catch
            { }
            return list_point;
        }
        public Point DetechRandomPoint(string ldid, Bitmap templateImage)
        {
            List<Point> list_point = new List<Point>();
            try
            {
                LDController ld = new LDController();
                Bitmap sourceImage = ConvertToFormat(ld.ScreenShoot(ldid));
                list_point = searchBitmaps(templateImage, sourceImage, 0.2);                 
            }
            catch
            { }
            if (list_point.Count > 0)
            {
                Random rd = new Random();
                return list_point[rd.Next(0, list_point.Count)];
            }
            else
            {

                return new Point();
            }
        }
        public ResultDetechLite DetechPoint(string ldid,List<DetechImageLite> list_templateImage)
        {
            ResultDetechLite kq = new ResultDetechLite();
            kq.status = false;
            try
            {
                LDController ld = new LDController();
                Bitmap sourceImage =ld.ScreenShoot(ldid);
            
               
                // find all matchings with specified above similarity
                foreach (DetechImageLite template in list_templateImage)
                {
                    List<Point> list_point = new List<Point>();
                    //TemplateMatch[] matchings = tm.ProcessImage(sourceImage, template.img);
                    list_point = searchBitmaps(template.img, sourceImage, 0.2);
                    // highlight found matchings
                    if (list_point.Count>0)
                    { 
                        if(template.haschil)
                        {
                            list_point = new List<Point>();
                            list_point = searchBitmaps(sourceImage, template.imgchil,0.2);                             
                        }
                       
                        if (list_point.Count>0)
                        {
                            kq.status = true;
                            kq.function = template.function;
                            kq.name = template.name;
                            kq.list_point = list_point;
                            if(template.removenode)
                            {
                                list_templateImage.Remove(template);
                            }
                            return kq;
                        }
                        
                       
                    }

                }
            }
            catch
            { }
            return kq;
        }
        private Point searchBitmap(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
        {
            BitmapData smallData =
              smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bigData =
              bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int smallStride = smallData.Stride;
            int bigStride = bigData.Stride;

            int bigWidth = bigBmp.Width;
            int bigHeight = bigBmp.Height - smallBmp.Height + 1;
            int smallWidth = smallBmp.Width * 3;
            int smallHeight = smallBmp.Height;

            Point location = new Point();
            int margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                byte* pSmall = (byte*)(void*)smallData.Scan0;
                byte* pBig = (byte*)(void*)bigData.Scan0;

                int smallOffset = smallStride - smallBmp.Width * 3;
                int bigOffset = bigStride - bigBmp.Width * 3;

                bool matchFound = true;

                for (int y = 0; y < bigHeight; y++)
                {
                    for (int x = 0; x < bigWidth; x++)
                    {
                        byte* pBigBackup = pBig;
                        byte* pSmallBackup = pSmall;

                        //Look for the small picture.
                        for (int i = 0; i < smallHeight; i++)
                        {
                            int j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                //With tolerance: pSmall value should be between margins.
                                int inf = pBig[0] - margin;
                                int sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound) break;

                            //We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            //Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        //If match found, we return.
                        if (matchFound)
                        {
                            //location.X = x;
                            //location.Y = y;
                            //location.Width = smallBmp.Width;
                            //location.Height = smallBmp.Height;

                            int w = smallBmp.Width / 2;
                            location.X = x + w;
                            int h = smallBmp.Height / 2;
                            location.Y = y + h;

                            break;
                        }
                        //If no match found, we restore the pointers and continue.
                        else
                        {
                            pBig = pBigBackup;
                            pSmall = pSmallBackup;
                            pBig += 3;
                        }
                    }

                    if (matchFound) break;

                    pBig += bigOffset;
                }
            }

            bigBmp.UnlockBits(bigData);
            smallBmp.UnlockBits(smallData);

            return location;
        }
        private List<Point> searchBitmaps(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
        {
            List<Point> list_point = new List<Point>();
            try
            {
                BitmapData smallData =
                  smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                           System.Drawing.Imaging.ImageLockMode.ReadOnly,
                           System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                BitmapData bigData =
                  bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                           System.Drawing.Imaging.ImageLockMode.ReadOnly,
                           System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                int smallStride = smallData.Stride;
                int bigStride = bigData.Stride;

                int bigWidth = bigBmp.Width;
                int bigHeight = bigBmp.Height - smallBmp.Height + 1;
                int smallWidth = smallBmp.Width * 3;
                int smallHeight = smallBmp.Height;

                
                int margin = Convert.ToInt32(255.0 * tolerance);

                unsafe
                {
                    byte* pSmall = (byte*)(void*)smallData.Scan0;
                    byte* pBig = (byte*)(void*)bigData.Scan0;

                    int smallOffset = smallStride - smallBmp.Width * 3;
                    int bigOffset = bigStride - bigBmp.Width * 3;

                    bool matchFound = true;

                    for (int y = 0; y < bigHeight; y++)
                    {
                        for (int x = 0; x < bigWidth; x++)
                        {
                            byte* pBigBackup = pBig;
                            byte* pSmallBackup = pSmall;

                            //Look for the small picture.
                            for (int i = 0; i < smallHeight; i++)
                            {
                                int j = 0;
                                matchFound = true;
                                for (j = 0; j < smallWidth; j++)
                                {
                                    //With tolerance: pSmall value should be between margins.
                                    int inf = pBig[0] - margin;
                                    int sup = pBig[0] + margin;
                                    if (sup < pSmall[0] || inf > pSmall[0])
                                    {
                                        matchFound = false;
                                        break;
                                    }

                                    pBig++;
                                    pSmall++;
                                }

                                if (!matchFound) break;

                                //We restore the pointers.
                                pSmall = pSmallBackup;
                                pBig = pBigBackup;

                                //Next rows of the small and big pictures.
                                pSmall += smallStride * (1 + i);
                                pBig += bigStride * (1 + i);
                            }

                            //If match found, we return.
                            if (matchFound)
                            {
                                Point location = new Point();
                                int w = smallBmp.Width / 2;
                                location.X = x + w;
                                int h = smallBmp.Height / 2;
                                location.Y = y + h;

                                list_point.Add(location);

                                pBig = pBigBackup;
                                pSmall = pSmallBackup;
                                pBig += 3;
                            }
                            //If no match found, we restore the pointers and continue.
                            else
                            {
                                pBig = pBigBackup;
                                pSmall = pSmallBackup;
                                pBig += 3;
                            }
                        }

                        if (matchFound) break;

                        pBig += bigOffset;
                    }
                }

                bigBmp.UnlockBits(bigData);
                smallBmp.UnlockBits(smallData);
            }
            catch { }
            return list_point;
        }
        public List<ResultDetechLite> DetechListPoint(string ldid, List<DetechImageLite> list_templateImage)
        {
            List<ResultDetechLite> list_kq = new List<ResultDetechLite>();
            try
            {
                LDController ld = new LDController();
                Bitmap sourceImage = ld.ScreenShoot(ldid);
                // find all matchings with specified above similarity
                foreach (DetechImageLite template in list_templateImage)
                { 
                    List<Point> list_point = new List<Point>();
                    list_point = searchBitmaps(template.img, sourceImage, 0.2);  
                    // highlight found matchings
                    if (list_point.Count > 0)
                    {
                        list_point = new List<Point>();
                        if (template.haschil)
                        {
                            list_point = searchBitmaps(template.imgchil, sourceImage, 0.2);  
                            
                        }                        
                        if (list_point.Count > 0)
                        {
                            ResultDetechLite kq = new ResultDetechLite();
                            kq.status = true;
                            kq.function = template.function;
                            kq.name = template.name;
                            kq.list_point = list_point;
                            list_kq.Add(kq);
                        }


                    }

                }
            }
            catch
            { }
            return list_kq;
        }
        public bool loginFacebook(userLD u, Account acc, CancellationToken token)
        {
            try
            {
                int loop = 0;

                List<DetechImageLite> list_detechimg = new List<DetechImageLite>();

                DetechImageLite model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_label_2fa;                
                model.name = "2fa";
                model.function = 2;
                model.haschil = true;
                model.imgchil = Properties.Resources.lite_login_button_ok;
                model.removenode = true;
                list_detechimg.Add(model);

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_input_phone;
                model.name = "input phone";
                model.function = 2;
                model.haschil = false;
                model.removenode = false;
                list_detechimg.Add(model);

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_button_ok_blue;
                model.name = "Ok";
                model.function = 1;
                model.haschil = false;
                model.removenode = true;
                list_detechimg.Add(model);

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_button_another;
                model.name = "Ok";
                model.function = 1;
                model.haschil = false;
                model.removenode = true;
                list_detechimg.Add(model);

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_label_facebook_logo;
                model.name = "finish";
                model.function = 3;
                model.haschil = false;
                model.removenode = true;
                list_detechimg.Add(model);

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_button_finish;
                model.name = "finish";
                model.function = 3;
                model.haschil = false;
                model.removenode = true;
                list_detechimg.Add(model);

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_label_avatar;
                model.name = "avatar";
                model.function = 2;
                model.haschil = false;
                model.removenode = true;
                list_detechimg.Add(model);

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_label_input_password;
                model.name = "input password";
                model.function = 2;
                model.haschil = false;
                model.removenode = true;
                list_detechimg.Add(model);

                LDController ldcontroller = new LDController();
            Lb_start:
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                ResultDetechLite kq = DetechPoint(acc.ldid, list_detechimg);
                if(kq.status)
                {
                    ldcontroller.checkScreen(acc.ldid);
                    loop = 0;
                    switch(kq.function)
                    {
                        case 1:
                            {
                                ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq.list_point[0]);
                                break;
                            }
                        case 2:
                            {
                                if(kq.name=="input phone")
                                {
                                    ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq.list_point[0].X,kq.list_point[0].Y+25);
                                    if (string.IsNullOrEmpty(acc.id))
                                        ldcontroller.PressOnLeapdroid_vietnamese(acc.ldid, acc.email.Trim());
                                    else
                                        ldcontroller.PressOnLeapdroid_vietnamese(acc.ldid, acc.id.Trim());
                                    Thread.Sleep(1000);
                                    //tab xuong dang nhap
                                   ldcontroller.runAdb(acc.ldid, "shell input keyevent 61");
                                    // ClickOnLeapdroidPosition(acc.ldid, PointDefault.p_login_password);
                                   Thread.Sleep(1000);
                                   ldcontroller.PressOnLeapdroid_vietnamese(acc.ldid, acc.Password.Trim());
                                   Thread.Sleep(1000);                                  
                                    //press enter
                                   Point kq_buttonlogin = DetechRandomPoint(acc.ldid, Properties.Resources.lite_login_button_login);
                                    if(kq_buttonlogin.Y>0)
                                    {
                                        ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq_buttonlogin);
                                        Thread.Sleep(3000);
                                        
                                    }
                                }
                                else
                                {
                                    if(kq.name=="2fa")
                                    {
                                        ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq.list_point[0]);
                                        Thread.Sleep(1000);
                                        //press enter
                                        Point kq_inputphone = DetechRandomPoint(acc.ldid, Properties.Resources.lite_login_input_phone);
                                        if (kq_inputphone.Y > 0)
                                        {
                                            ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq_inputphone.X,kq_inputphone.Y+25);
                                            Thread.Sleep(100);
                                            if (string.IsNullOrEmpty(acc.id))
                                                ldcontroller.PressOnLeapdroid_vietnamese(acc.ldid, acc.email.Trim());
                                            else
                                                ldcontroller.PressOnLeapdroid_vietnamese(acc.ldid, acc.id.Trim());
                                            Thread.Sleep(1000);
                                            //tab xuong dang nhap
                                            ldcontroller.runAdb(acc.ldid, "shell input keyevent 61");
                                            // ClickOnLeapdroidPosition(acc.ldid, PointDefault.p_login_password);
                                            Thread.Sleep(1000);
                                            CustomerController control = new CustomerController();
                                            TwoFaModel model2fa = control.getCodeTwofa(acc.email.Trim(), acc.privatekey);
                                            if (string.IsNullOrEmpty(model2fa.message) == false)
                                            {
                                                ldcontroller.PressOnLeapdroid_vietnamese(acc.ldid, model2fa.message);
                                            }
                                            else
                                            {
                                                acc.TrangThai = "Authentication Error";
                                                return false;
                                            }
                                            
                                            Thread.Sleep(1000);
                                            //press enter
                                            Point kq_buttonlogin = DetechRandomPoint(acc.ldid, Properties.Resources.lite_login_button_login);
                                            if (kq_buttonlogin.Y > 0)
                                            {
                                                ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq_buttonlogin);
                                                Thread.Sleep(3000); 
                                                
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (kq.name == "avatar")
                                        {
                                            ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq.list_point[0].X, kq.list_point[0].Y+30);
                                        }
                                        else
                                        {
                                            if(kq.name=="input password")
                                            {
                                                ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq.list_point[0].X, kq.list_point[0].Y + 30);
                                                ldcontroller.PressOnLeapdroid_vietnamese(acc.ldid, acc.Password.Trim());
                                                Thread.Sleep(1000);
                                                //press enter
                                                ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq.list_point[0].X, kq.list_point[0].Y + 70);
                                                Point kq_buttonlogin = DetechRandomPoint(acc.ldid, Properties.Resources.lite_login_button_login);
                                            }
                                        }

                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                return true;
                            }
                    }
                    goto Lb_start;
                }
                else
                {
                    loop++;
                    if(loop<=5)
                    {
                        goto Lb_start;
                    }
                }

            }
            catch
            {

            }
            return false;
        }
        public bool checkIsLoginLite(Account acc)
        {
            try
            {

                List<DetechImageLite> list_detechimg = new List<DetechImageLite>();

                DetechImageLite model = new DetechImageLite(); 

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_label_facebook_logo;
                model.name = "finish";
                model.function = 3;
                model.haschil = false;
                model.removenode = true;
                list_detechimg.Add(model);

                model = new DetechImageLite();
                model.img = Properties.Resources.lite_login_button_finish;
                model.name = "finish";
                model.function = 3;
                model.haschil = false;
                model.removenode = true;
                list_detechimg.Add(model);

                for(int i=0;i<3;i++)
                {
                    ResultDetechLite kq = DetechPoint(acc.ldid, list_detechimg);
                    if (kq.status)
                    {
                        return true;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                 
            }
            catch
            { }
            return false;
        }

        #region tuong tac
        //luot newfeed
        public void scrollNewfeed(userLD u, string device, Account acc, int min, int max, CancellationToken token)
        {
            try
            {
                LDController ld = new LDController();
                ld.scroll_down(device);
                Random rd = new Random();
                int luot = rd.Next(min, max);
                u.setStatusSum(luot);
                int int_check = 1;
                for (int i = 0; i < luot; i++)
                {
                    int_check++;
                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }
                    if (i % 10 == 0)
                    {
                       
                    }
                    if (int_check % 30 == 0)
                    {
                        
                    }
                    ld.scroll_up(device);
                    Thread.Sleep(200);
                    u.setStatusResult(i);
                }
            }
            catch
            { }

        }

        public string scrollLikeNewFeed(userLD u, Account acc, string device, string app, int minlike, int maxlike, SettingTuongTac tuongtac, CancellationToken token)
        {
            string message = "";
            int dem = 0;
            Random rd = new Random();
            int luot = rd.Next(minlike, maxlike);
            u.setStatusSum(luot);
            try
            {
                LDController ld = new LDController();
                int max = 0;
                ld.scroll_down(device);
                Delay(1);

                while (dem < luot)
                {
                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }
                    ld.scroll_up_random(device, token);
                    Delay(1);
                    if (likePost(u,acc, token))
                    {
                        max = 0;
                        dem++;
                        u.setStatusResult(dem);
                    }
                    else
                    {
                        Delay(1);
                        max++;
                        if (max >= 6)
                            return message = "| Like newfeed complete:" + dem.ToString() + "/" + luot; ;
                    }

                }
            }
            catch
            { }
            return message = "| Like newfeed complete:" + dem.ToString() + "/" + luot;
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
        public bool likePost(userLD u, Account acc, CancellationToken token)
        {
            try
            {
                int loop = 0;

                List<DetechImageLite> list_detechimg = new List<DetechImageLite>();

                DetechImageLite model = new DetechImageLite();
                model.img = Properties.Resources.lite_button_like;
                model.name = "like";
                model.function = 1;
                model.haschil = false; 
                model.removenode = true;
                list_detechimg.Add(model);
                 

                LDController ldcontroller = new LDController();
            Lb_start:
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                ResultDetechLite kq = DetechPoint(acc.ldid, list_detechimg);
                if (kq.status)
                {
                    loop = 0;
                    switch (kq.function)
                    {
                        case 1:
                            {
                                ldcontroller.ClickOnLeapdroidPosition(acc.ldid, kq.list_point[0]);
                                return true;
                            }
                         
                    }
                    goto Lb_start;
                }
                else
                {
                    loop++;
                    if (loop <= 5)
                    {
                        goto Lb_start;
                    }
                }

            }
            catch
            {

            }
            return false;
        }
        #endregion
    }
}
