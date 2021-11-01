using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
namespace NinjaSystem
{
    public class FunctionHelper
    {
        private static readonly Random random_0 = new Random();
        private const string string_0 = "\ud83d\ude37|\ud83d\ude37|\ud83d\ude13|\ud83d\ude30|\ud83d\ude25|\ud83d\ude2a|\ud83d\ude28|\ud83d\ude31|\ud83d\ude35|\ud83d\ude2d|\ud83d\ude20|\ud83d\ude33|\ud83d\ude32|\ud83d\ude24|\ud83d\udebd|\ud83d\udec0|\ud83d\udc59|\ud83d\udc84|\ud83d\udc55|\ud83d\udc58|\ud83d\udc57|\ud83d\udc62|\ud83d\udc60|\ud83d\udc61|\ud83d\udcbc|\ud83d\udc5c|\ud83d\udc54|\ud83c\udfa9|\ud83d\udc52|\ud83d\udc51|\ud83d\udc8d|\ud83d\udead|\ud83c\udfc8|\ud83c\udfc0|\ud83c\udfbe|\ud83c\udfb1|\ud83c\udfaf|\ud83c\udfbf|\ud83c\udf8c|\ud83c\udfc1|\ud83c\udfc6|\ud83d\udc4c|\ud83d\udc4e|\ud83d\ude4c|\ud83d\udcaa|\ud83d\udc4a|\ud83d\udc4f|\ud83d\udc46|\ud83d\udc49|\ud83d\udc48|\ud83d\udc47|\ud83d\udc94|\ud83d\udc99|\ud83d\udc9a|\ud83d\udc9b|\ud83d\udc9c|\ud83d\udc97|\ud83d\udc98|\ud83d\udc93|\ud83d\udc9d|\ud83d\udc96|\ud83d\udc9e|\ud83d\udc9f|\ud83d\udc8c|\ud83d\udc91|\ud83d\udc8b|\ud83d\udc44|\ud83d\ude0d|\ud83d\ude18|\ud83d\ude1a|\ud83d\ude0a|\ud83d\ude0f|\ud83d\ude0c|\ud83d\ude03|\ud83d\ude04|\ud83d\ude1e|\ud83d\ude22|\ud83d\ude1c|\ud83d\ude1d|\ud83d\ude09|\ud83d\ude14|\ud83d\ude12|\ud83d\ude02|\ud83d\ude21|\ud83d\udc7f|\ud83d\udc7d|\ud83d\udc7e|\ud83d\udc7b|\ud83d\udc7c|\ud83d\udc6f|\ud83d\udc82|\ud83d\udc73|\ud83c\udf85|\ud83d\udc6e|\ud83d\udc77|\ud83d\udc78|\ud83d\udc74|\ud83d\udc75|\ud83d\udc68|\ud83d\udc69|\ud83d\udc66|\ud83d\udc67|\ud83d\udc76|\ud83d\udc71|\ud83d\udc6b|\ud83c\udf8e|\ud83d\udc83|\ud83d\udc42|\ud83d\udc43|\ud83d\udc40|\ud83c\udf1f|\ud83c\udf19|\ud83c\udfb5|\ud83c\udfb6|\ud83d\udca4|\ud83d\udd25|\ud83c\ude50|\ud83c\udf80|\ud83c\udf02|\ud83d\udca7|\ud83d\udd28|\ud83d\udcba|\ud83d\udd31|\ud83d\udd30|\ud83c\udc04|\ud83d\udc8e|\ud83d\udca0|\ud83d\udd37|\ud83d\udd36";
        public static string smethod_6(string string_0, int int_0, string string_1)
        {
            try
            {
                string str = "";
                for (int i = int_0; i < string_0.Length; i++)
                {
                    char ch = string_0[i];
                    if (!(ch.ToString() != string_1))
                    {
                        break;
                    }
                    str = str + string_0[i];
                }
                return str;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        } 
        private static string method_RandomSmile()
        {
            string[] arr = string_0.Split('|');
            string str = arr[random_0.Next(0, arr.Length)];
            return str;
        }
        public static void showMessage(string mess)
        {
            MessageBox.Show(mess, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private const string _chars = "abcdefghiklmnopqrstxyzv";

        private static string method_random(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[random_0.Next(_chars.Length)];
            }
            return new string(buffer);
        }
        private static string method_RandomText()
        {
            int i = random_0.Next(3, 5);
            string str1 = method_random(random_0.Next(3, 5));
            string str2 = method_random(random_0.Next(3, 5));
            string str3 = method_random(random_0.Next(3, 5));
            string str4 = method_random(random_0.Next(3, 5));
            string str5 = method_random(random_0.Next(3, 5));
            string str6 = method_random(random_0.Next(3, 5));
            string str7 = method_random(random_0.Next(3, 5));
            string str8 = method_random(random_0.Next(3, 5));
            string str9 = method_random(random_0.Next(3, 5));
            string str10 = method_random(random_0.Next(3, 5));
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", str1, str2, str3, str4, str5, str6, str7, str8, str9, str10);
        }
        public static string method_Spin(string string_0)
        {
            string_0 = string_0.Replace("#number", "#"+random_0.Next(100000, 900000).ToString());
            if (string_0.Contains("$random"))
                string_0 = string_0.Replace("$random", random_0.Next(999999999).ToString());
            if (string_0.Contains("$time"))
                string_0 = string_0.Replace("$time", random_0.Next(0, 12).ToString() + ":" + random_0.Next(0, 60).ToString());
            if (string_0.Contains("$date"))
                string_0 = string_0.Replace("$date", DateTime.Now.ToShortDateString());
            if (string_0.Contains("$text"))
                string_0 = string_0.Replace("$text", method_RandomText());
            if (string_0.Contains("$number"))
                string_0 = string_0.Replace("$number", ConvertTimeSpan().ToString());
        lb_start:
            if (string_0.Contains("$smile"))
            {
                string_0 = method_ReplaceOne(string_0, "$smile", method_RandomSmile());
                goto lb_start;
            }

            int index = string_0.IndexOf('{');
            int length = string_0.IndexOf('}');
            if ((index == -1) && (length == -1))
            {
                return string_0;
            }
            if ((index == -1) || (length < index))
            {
                return string_0;
            }
            if (length == -1)
            {
                throw new ArgumentException("Unbalanced brace.");
            }
            string str2 = method_Spin(string_0.Substring(index + 1, string_0.Length - (index + 1)));
            length = str2.IndexOf('}');
            if (length == -1)
            {
                throw new ArgumentException("Unbalanced brace.");
            }
            string[] strArray = str2.Substring(0, length).Split(new char[] { '|' });
            string str3 = strArray[random_0.Next(0, strArray.Length)];
            return (string_0.Substring(0, index) + str3 + method_Spin(str2.Substring(length + 1, str2.Length - (length + 1))));
        }
        public static string method_ReplaceOne(string text, string oldchar, string newchar)
        {
            var regex = new Regex(Regex.Escape(oldchar));
            return regex.Replace(text, newchar, 1);
        }
        public static string RandomString(int size)
        {
            char[] chrArray = new char[size];
            for (int i = 0; i < size; i++)
            {
                chrArray[i] = "✩®♥﻿♬♫♪☺☻☹☠☃〠☢☯♨✉☊☭❁❀✿✾✽✣✵✡★✮✯❉❆❂☤❦✇☮✠☣♚♔♛♕♜♖♞♘♟♙♝♗웃유♎♈♉♊♋♑♍♎♏♐∞¥€£ƒ$©☬☫❦♠♤♥♡◆◇❤♣♧❥▲△❖○◎●✦✧▰▱◈◉✺☼☀☁◐◑☾☽✼☪☂✈✆☎☏☉●◯☿☥♀⊕♁♂♃♄♅♆♇☄✝✍✄✂✏✔☑☒✖✗✘☛☚☜☝☟✌㊚㊛"[random_0.Next("✩®♥﻿♬♫♪☺☻☹☠☃〠☢☯♨✉☊☭❁❀✿✾✽✣✵✡★✮✯❉❆❂☤❦✇☮✠☣♚♔♛♕♜♖♞♘♟♙♝♗웃유♎♈♉♊♋♑♍♎♏♐∞¥€£ƒ$©☬☫❦♠♤♥♡◆◇❤♣♧❥▲△❖○◎●✦✧▰▱◈◉✺☼☀☁◐◑☾☽✼☪☂✈✆☎☏☉●◯☿☥♀⊕♁♂♃♄♅♆♇☄✝✍✄✂✏✔☑☒✖✗✘☛☚☜☝☟✌㊚㊛".Length)];
            }
            return new string(chrArray);
        }
        public static string RandomString1(int length)
        {
            const string chars = "abcdefghiklmnopqrstuvxz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomPhone(int length)
        {
            const string chars = "123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
        public static int UnixTimeNow2()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (int)timeSpan.TotalSeconds;
        }
        public static long UnixTimeNow(DateTime time)
        {
            var timeSpan = (time - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
        public static string giaima(string strDecypt, string makey)
        {
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(strDecypt);
                byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(makey));
                TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
                {
                    Key = buffer,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                byte[] bytes = provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception)
            {
            }
            return "";
        }
        public static string mahoa(string strEnCrypt, string makey)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(strEnCrypt);
                byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(makey));
                TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
                {
                    Key = buffer,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                byte[] inArray = provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
                return Convert.ToBase64String(inArray, 0, inArray.Length);
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static string method_DongDau(String fileName)
        {
            Random rd = new Random();
            PrivateFontCollection fonts;
            //     FontFamily family = LoadFontFamily("font.ttf", out fonts);
            System.Drawing.Font myFont = new Font("Arial", 12, FontStyle.Bold);

            Image imgPhoto = Image.FromFile(fileName);
            try
            {
                // Get a graphics context
                Graphics g = Graphics.FromImage(imgPhoto);

                // Create a solid brush to write the watermark text on the image
                Brush myBrush = new SolidBrush(Color.FromArgb(255, Color.Red));

                // Calculate the size of the text
                SizeF sz = g.MeasureString(DateTime.Now.ToString(), myFont);

                // Creae a copy of variables to keep track of the
                // drawing position (X,Y)
                int X;
                int Y;

                // Set the drawing position based on the users
                // selection of placing the text at the bottom or
                // top of the image
                X = (int)(imgPhoto.Width - sz.Width);
                Y = (int)(imgPhoto.Height - sz.Height);

                // draw the water mark text
                g.DrawString(DateTime.Now.ToString(), myFont, myBrush, new Point(X, Y));

                string filenew = String.Format("{0}\\Logs\\{1}.jpg", Application.StartupPath, rd.Next(9999999));
                //save new image to file system.
                imgPhoto.Save(filenew, ImageFormat.Jpeg);
                imgPhoto.Dispose();
                //  imgWatermark.Dispose();
                return filenew;
            }
            catch (Exception exx)
            {

            }
            return fileName;
        }
        private static readonly string[] VietnameseSigns = new string[]{

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };
        public static string RemoveSign4VietnameseString(string str)
        {
            // Use the correct encoding this time to convert back to a string.
            if (str == null)
                return "";
            str = WebUtility.HtmlDecode(str).ToLower();
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            str = str.Replace("?", "").Replace("!", "").Replace("\"", "").Replace("”", "").Replace("“", "").Replace("‘", "").Replace("’", "").Replace("–", "").Replace("/", "");
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }
        public static int ConvertTimeSpan()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        public static ImeiModel createImei()
        {
            ImeiModel im = new ImeiModel();
            Random rd = new Random();
            List<ImeiModel> list_imei2 = new List<ImeiModel>();
            CustomerController customer = new CustomerController();
            ResultRequest kq = customer.sendLogs("createimei2");
            if (kq.status)
            {
                JArray jarr = JArray.Parse(kq.data);
                foreach (var item in jarr)
                {
                    try
                    {
                        ImeiModel imei = new ImeiModel();
                        string[] rowsplit = item["Value"].ToString().Split('|');
                        imei.brand = rowsplit[0];
                        imei.model = rowsplit[1];
                        imei.value = rowsplit[2];
                        list_imei2.Add(imei);
                    }
                    catch
                    { }
                }
            }

            if (list_imei2.Count > 0)
            {
                im = list_imei2[rd.Next(0, list_imei2.Count)];
                string data = im.value + FunctionHelper.RandomPhone(6);
                int x = 0;
                int i = 0;
                int tongle = 0;
                int tongchan = 0;
                foreach (char s in data)
                {
                    string a = s.ToString();
                    int number = Convert.ToInt32(a);
                    i++;
                    if (i % 2 == 0)
                    {
                        int numberx2 = number * 2;
                        if (numberx2 >= 10)
                        {
                            tongchan = tongchan + 1 + numberx2 - 10;
                        }
                        else
                        {
                            tongchan = tongchan + numberx2;
                        }
                    }
                    else
                    {
                        tongle = tongle + number;
                    }
                }
                int total = tongchan + tongle;
                if (total % 10 == 0)
                {
                    x = 0;
                }
                else
                {
                    int nguyen = total / 10;
                    int con = (nguyen + 1) * 10;
                    x = con - total;
                }
                im.value = data + x;
                return im;
            }
            return im;
        }
        public static ImeiModel createImei(string model)
        {
            ImeiModel im = new ImeiModel();
            Random rd = new Random();
            List<ImeiModel> list_imei2 = new List<ImeiModel>();
            CustomerController customer = new CustomerController();
            ResultRequest kq = customer.sendLogs("createimei2");
            if (kq.status)
            {
                JArray jarr = JArray.Parse(kq.data);
                foreach (var item in jarr)
                {
                    try
                    {
                        ImeiModel imei = new ImeiModel();
                        string[] rowsplit = item["Value"].ToString().Split('|');
                        imei.brand = rowsplit[0];
                        imei.model = rowsplit[1];
                        imei.value = rowsplit[2];
                        list_imei2.Add(imei);
                    }
                    catch
                    { }
                }
            }

            foreach (ImeiModel imie in list_imei2)
            {
                if (imie.model == model)
                {
                    im = imie;
                    break;
                }
            }

            if (list_imei2.Count > 0)
            {
                if (im == null)
                    im = list_imei2[rd.Next(0, list_imei2.Count)];
                string data = im.value + FunctionHelper.RandomPhone(6);
                int x = 0;
                int i = 0;
                int tongle = 0;
                int tongchan = 0;
                foreach (char s in data)
                {
                    string a = s.ToString();
                    int number = Convert.ToInt32(a);
                    i++;
                    if (i % 2 == 0)
                    {
                        int numberx2 = number * 2;
                        if (numberx2 >= 10)
                        {
                            tongchan = tongchan + 1 + numberx2 - 10;
                        }
                        else
                        {
                            tongchan = tongchan + numberx2;
                        }
                    }
                    else
                    {
                        tongle = tongle + number;
                    }
                }
                int total = tongchan + tongle;
                if (total % 10 == 0)
                {
                    x = 0;
                }
                else
                {
                    int nguyen = total / 10;
                    int con = (nguyen + 1) * 10;
                    x = con - total;
                }
                im.value = data + x;
                return im;
            }
            return im;
        }
        public static string md5(string chuoi)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(chuoi);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");//Nếu là "X2" thì kết quả sẽ tự chuyển sang ký tự in Hoa
            }

            return str_md5;
        }
   
    }
}
