using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Security.Cryptography;
using System.Net;
using System.IO; 
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Xml;


namespace NinjaSystem
{
    public class Class18
    {
        public Class18(string str)
        {
            string_ninja = str;
            SettingTool.hid = "NINJA-SYSTEM-3B5B-05AD-6EBD-CCDA-B076-1264-A2C7-E022";//"NINJA-SYSTEM-225D-6D58-FE5A-EEF3-C90B-E829-7C28-F184";// method_0();
            System.IO.File.WriteAllText(Application.StartupPath + "\\logs.txt", SettingTool.hid);

        }
        private string string_ninja;
        private string string_0;

        private string string_1;

        private string string_2;
        private string string_3;
        private bool flag = false;
        private bool flag2 = false;
        string str_1;
        public string method_0()
        {
            int i;
            string str;
            string empty = string.Empty;
            string empty1 = string.Empty;
            string str1 = string.Empty;
            try
            {
                SelectQuery selectQuery = new SelectQuery("Win32_processor");
                ManagementObjectCollection.ManagementObjectEnumerator enumerator = (new ManagementObjectSearcher(selectQuery)).Get().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    empty = ((ManagementObject)enumerator.Current)["processorId"].ToString();
                }
            }
            catch (Exception exception)
            {
            }
            try
            {
                ManagementObject managementObject = new ManagementObject(string.Format("win32_logicaldisk.deviceid=\"{0}:\"", "C"));
                managementObject.Get();
                empty1 = managementObject["VolumeSerialNumber"].ToString();
            }
            catch (Exception exception1)
            {
            }
            try
            {
                SelectQuery selectQuery1 = new SelectQuery("Win32_BaseBoard");
                ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = (new ManagementObjectSearcher(selectQuery1)).Get().GetEnumerator();
                while (managementObjectEnumerator.MoveNext())
                {
                    str1 = ((ManagementObject)managementObjectEnumerator.Current)["SerialNumber"].ToString();
                }
            }
            catch (Exception exception2)
            {
            }
            try
            {
                str = (empty == "" ? "kudhfnksyrucfjhyguijtvftedrbytguhyghihnkfunh" : empty);
            }
            catch (Exception exception3)
            {
                str = "kudhfnksyrucfjhyguijtvftedrbytguhyghihnkfunh";
            }
            try
            {
                str = (empty1 == "" ? string.Concat(str, "hsdfngskyunnghfcbjhvnkuhgjhgbnkbunlmhsfdagkjh") : string.Concat(str, empty1));
            }
            catch (Exception exception4)
            {
                str = string.Concat(str, "hsdfngskyunnghfcbjhvnkuhgjhgbnkbunlmhsfdagkjh");
            }
            try
            {
                str = (str1 == "" ? string.Concat(str, "hsdfngskyunnghfcbjhvnkuhgjhgbnkbunlmhsfdagkjh") : string.Concat(str, str1));
            }
            catch (Exception exception5)
            {
                str = string.Concat(str, "hsdfngskyunnghfcbjhvnkuhgjhgbnkbunlmhsfdagkjh");
            }
            if (str.Length < 10)
            {
                str = "hgnjykb3426465cdrhscyt4654542dsgymhjj3454ftecftr5633uhnyf456dghb";
            }
            str = str.ToLower().Replace("\r\n", "").Replace("\t", "").Replace(" ", "").Replace("a", "ddsfvf").Replace("b", "wasvh").Replace("c", "mfnjy").Replace("d", "qhgjw").Replace("e", "fxczgn").Replace("f", "aercac").Replace("g", "agjmkmw").Replace("h", "awasec").Replace("i", "aavrstyw").Replace("j", "esrdvthc").Replace("k", "yascfgsu").Replace("l", "mvfdak").Replace("m", "rvdghbnt").Replace("n", "mscfj").Replace("o", "rumkgmt").Replace("p", "arbns").Replace("q", "rzdfct").Replace("r", "abmks").Replace("s", "tdfvy").Replace("t", "gsrdvcgb").Replace("u", "enmgr").Replace("v", "taecwg").Replace("w", "awfsvh").Replace("x", "xaasfgv").Replace("y", "kawecj").Replace("z", "aasercw");
            for (i = 99; i <= 1; i++)
            {
                int num = i + 14;
                str = str.Replace(i.ToString(), num.ToString());
            }
            try
            {
                this.string_0 = "";
                int length = str.Length;
                for (i = 1; i <= length; i = i + 30)
                {
                    if (i < str.Length)
                    {
                        this.string_0 = string.Concat(this.string_0, str.Substring(i, 1));
                    }
                }
            }
            catch (Exception exception6)
            {
                this.string_0 = str;
            }
            this.string_1 = this.string_0;
            this.string_1 = this.string_1.ToLower();
            this.string_1 = this.string_1.Replace("\r\n", "");
            this.string_1 = this.string_1.Replace("\t", "");
            this.string_1 = this.string_1.Replace(" ", "");
            this.string_1 = this.string_1.Replace("a", "29 386 21");
            this.string_1 = this.string_1.Replace("b", "14 232 37");
            this.string_1 = this.string_1.Replace("c", "52 474 68");
            this.method_1();
            i = 10;
            do
            {
                int num1 = i - 2;
                this.string_1 = this.string_1.Replace(i.ToString(), num1.ToString());
                i++;
            }
            while (i <= 95);
            this.string_1 = this.string_1.Replace("\r\n", "");
            this.string_1 = this.string_1.Replace(" ", "");
            try
            {
                this.string_2 = "";
                int length1 = this.string_1.Length;
                for (i = 1; i <= length1; i = i + 7)
                {
                    if (i < this.string_1.Length)
                    {
                        this.string_2 = string.Concat(this.string_2, this.string_1.Substring(i, 1));
                    }
                }
            }
            catch (Exception exception7)
            {
                this.string_2 = this.string_1;
            }

            return string_ninja + method_9(string_2);
        }
        private void method_1()
        {
            this.string_1 = this.string_1.Replace("d", "31 285 62");
            this.string_1 = this.string_1.Replace("e", "32 276 24");
            this.method_2();
            this.string_1 = this.string_1.Replace("f", "64 291 33");
        }
        private void method_2()
        {
            this.string_1 = this.string_1.Replace("g", "12 423 25");
            this.method_3();
            this.string_1 = this.string_1.Replace("h", "62 192 25");
        }

        private void method_3()
        {
            this.string_1 = this.string_1.Replace("i", "42 766 56");
            this.method_4();
            this.string_1 = this.string_1.Replace("j", "19 233 46");
        }

        private void method_4()
        {
            this.string_1 = this.string_1.Replace("k", "21 334 79");
            this.method_5();
            this.string_1 = this.string_1.Replace("l", "36 546 48");
        }

        private void method_5()
        {
            this.string_1 = this.string_1.Replace("m", "12 722 41");
            this.string_1 = this.string_1.Replace("n", "53 781 86");
            this.string_1 = this.string_1.Replace("o", "32 235 34");
            this.method_6();
            this.string_1 = this.string_1.Replace("p", "92 374 25");
            this.string_1 = this.string_1.Replace("q", "98 872 36");
            this.string_1 = this.string_1.Replace("r", "92 833 45");
        }

        private void method_6()
        {
            this.string_1 = this.string_1.Replace("s", "93 237 85");
            this.string_1 = this.string_1.Replace("t", "21 455 38");
            this.method_7();
            this.method_8();
        }

        private void method_7()
        {
            this.string_1 = this.string_1.Replace("u", "97 837 77");
            this.string_1 = this.string_1.Replace("v", "68 644 33");
            this.string_1 = this.string_1.Replace("w", "99 257 37");
        }

        private void method_8()
        {
            this.string_1 = this.string_1.Replace("x", "20 359 23");
            this.string_1 = this.string_1.Replace("y", "83 665 43");
            this.string_1 = this.string_1.Replace("z", "92 857 03");
        }
        public string method_9(string key)
        {
            int num;
            string str = key;
            for (num = 0; num < 100; num++)
            {
                str = this.method_10(str + num);
            }
            str = str.ToUpper();
            for (num = str.Length - 1; num >= 1; num--)
            {
                if ((num % 4) == 0)
                {
                    str = str.Insert(num, "-");
                }
            }
            str = str.Replace(" ", "");
            return str;
        }
        public string method_10(string chuoi)
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
   

        public string method_13(string key)
        {
            try
            {
                byte[] keyArr;
                byte[] EnCryptArr = UTF8Encoding.UTF8.GetBytes(str_1);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] arrResult = transform.TransformFinalBlock(EnCryptArr, 0, EnCryptArr.Length);
                return Convert.ToBase64String(arrResult, 0, arrResult.Length);
            }
            catch (Exception ex) { }
            return "";
        }

        public static string method_14(string strDecypt, string key)
        {
            try
            {
                byte[] keyArr;
                byte[] DeCryptArr = Convert.FromBase64String(strDecypt);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateDecryptor();
                byte[] arrResult = transform.TransformFinalBlock(DeCryptArr, 0, DeCryptArr.Length);
                return UTF8Encoding.UTF8.GetString(arrResult);
            }
            catch (Exception ex) { }
            return "";
        }
            
        public string method_19(string string_0)
        {
            string str2;
            try
            {
                string str = string.Empty;
                using (StreamReader reader = new StreamReader(string_0))
                {
                    str = str + reader.ReadToEnd();
                }
                str2 = str;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        
 

        public static string version = "1.5";
        public string mahoa(string strEnCrypt, string key)
        {
            try
            {
                byte[] keyArr;
                byte[] EnCryptArr = UTF8Encoding.UTF8.GetBytes(strEnCrypt);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] arrResult = transform.TransformFinalBlock(EnCryptArr, 0, EnCryptArr.Length);
                return Convert.ToBase64String(arrResult, 0, arrResult.Length);
            }
            catch (Exception ex) { }
            return "";
        }
        public string giaima(string strDecypt, string key)
        {
            try
            {
                byte[] keyArr;
                byte[] DeCryptArr = Convert.FromBase64String(strDecypt);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateDecryptor();
                byte[] arrResult = transform.TransformFinalBlock(DeCryptArr, 0, DeCryptArr.Length);
                return UTF8Encoding.UTF8.GetString(arrResult);
            }
            catch (Exception ex) { }
            return "";
        }
        public static string method_13(string str, string str1)
        {
            try
            {
                byte[] keyArr;
                byte[] EnCryptArr = UTF8Encoding.UTF8.GetBytes(str);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(str1));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] arrResult = transform.TransformFinalBlock(EnCryptArr, 0, EnCryptArr.Length);
                return Convert.ToBase64String(arrResult, 0, arrResult.Length);
            }
            catch (Exception ex) { }
            return "";
        }
    }
}
