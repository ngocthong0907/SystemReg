using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NinjaSystem
{
    public class NinjaADB
    {
        object synDevice = new object();
        public string runComand(DeviceData device, string cmdCommand)
        {
            var receiver = new ConsoleOutputReceiver();
            try
            {
                ConsoleOutputReceiver creciever = new ConsoleOutputReceiver();
                SharpAdbClient.DeviceCommands.DeviceExtensions.ExecuteShellCommand(device, cmdCommand, creciever);

            }
            catch
            { }
            return receiver.ToString();
        }
        public string runMultiComand(DeviceData device, string data)
        {
            var receiver = new ConsoleOutputReceiver();
            try
            {

                string[] arr = data.Split('&');
                foreach (string line in arr)
                {
                    receiver.AddOutput(line);
                }
                receiver.Flush();


            }
            catch
            { }
            return receiver.ToString();
        }
        public string OpenFacebook(DeviceData device, string app)
        {
            string cmdCommand = string.Format(SettingTool.data["openfacebook1"], app);
            return runComand(device, cmdCommand);
        }


        public string ExecuteCMD(string cmdCommand)
        {
            string result;
            try
            {

                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Application.StartupPath,
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,

                };
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                //lock (synDevice)
                {
                    process.StandardInput.Flush();
                }
                process.StandardInput.Close();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;

            }
            catch
            {
                result = null;
            }
            return result;
        }

        public string ExecuteCMDNotWait(string cmdCommand)
        {
            string result;
            try
            {

                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Application.StartupPath,
                    FileName = "cmd.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,

                };
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                //lock (synDevice)
                {
                    process.StandardInput.Flush();
                }
                process.StandardInput.Close();
                //process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;

            }
            catch
            {
                result = null;
            }
            return result;
        }
        public void Tap(DeviceData device, int x, int y)
        {
            string cmdCommand = string.Format("input tap {0} {1}", x, y);
            runComand(device, cmdCommand);
        }
        public string OpenLink(DeviceData device, string app, string link)
        {
            string cmdCommand = string.Format(SettingTool.data["openlink"], device.Serial, link, app);
            string text = ExecuteCMD(cmdCommand);
            return text;
        }
        public void TapByPercent(DeviceData device, double x, double y)
        {
            Point screenResolution = GetScreenResolution(device.Serial);
            int num = (int)(x * ((double)screenResolution.X * 1.0 / 100.0));
            int num2 = (int)(y * ((double)screenResolution.Y * 1.0 / 100.0));

            Tap(device, num, num2);
        }
        public void Return(DeviceData device)
        {
            //string cmdCommand = string.Format("adb -s {0} shell input keyevent KEYCODE_BACK", deviceID);
            string cmdCommand = SettingTool.data["return"];
            runComand(device, cmdCommand);
            //  string text = ExecuteCMD(cmdCommand);
        }

        public void ClearCache(DeviceData device, string app)
        {
            string cmdCommand = string.Format(SettingTool.data["clearcache"], app);
            //string cmdCommand = string.Format("adb -s {0} shell pm clear {1}", device.Serial, app);
            runComand(device, cmdCommand);

        }
        public void KillFacebook(DeviceData device, string app)
        {
            string cmdCommand = string.Format(SettingTool.data["kill"], app);
            runComand(device, cmdCommand);
        }
      
        public void ForceStop(DeviceData device, string app)
        {
            string cmdCommand = string.Format(SettingTool.data["forcestop"], app);
            runComand(device, cmdCommand);
        }
        public void pressEnter(DeviceData device)
        {
            string cmdCommand = "input keyevent KEYCODE_ENTER";
            runComand(device, cmdCommand);
        }


        public void InputText(DeviceData device, string text)
        {
            string cmdCommand = String.Format(SettingTool.data["inputtext"], text.Replace(" ", "%s"));
            runComand(device, cmdCommand);



        }


        public void InputText_vietnamese(DeviceData device, string text)
        {
            string cmdCommand = String.Format("adb -s {0} shell am broadcast -a ADB_INPUT_B64 --es msg '{1}' ", device.Serial, text);
            ExecuteCMD(cmdCommand);

        }

        public void Backup(string deviceID, string text)
        {
            //adb backup -f 1.ab - apk com.facebook.katana
            string INPUT_TEXT_DEVICES = String.Format("adb -s {0} shell backup -f {1} - apk com.facebook.katana", deviceID, text);
            string str2 = ExecuteCMD(INPUT_TEXT_DEVICES);
        }
        public void startAdb(string text)
        {
            string str2 = ExecuteCMD(text);
        }

        public Point GetScreenResolution(string deviceID)
        {
            string cmdCommand = string.Format(GET_SCREEN_RESOLUTION, deviceID);
            string text = ExecuteCMD(cmdCommand);
            text = text.Substring(text.IndexOf("- "));
            text = text.Substring(text.IndexOf(' '), text.IndexOf(')') - text.IndexOf(' '));
            string[] array = text.Split(new char[]
    {
        ','
    });
            int x = Convert.ToInt32(array[0].Trim());
            int y = Convert.ToInt32(array[1].Trim());
            return new Point(x, y);
        }

        // Token: 0x04000003 RID: 3


        // Token: 0x04000004 RID: 4
        //private string KEY_DEVICES = "adb -s {0} shell input keyevent {1}";

        // Token: 0x04000005 RID: 5
        //    private string INPUT_TEXT_DEVICES = "adb -s {0} shell input text \"{1}\"";

        // Token: 0x04000006 RID: 6


        // Token: 0x04000007 RID: 7
        //     private string PULL_SCREEN_FROM_DEVICES = "adb -s {0} pull \"{1}\"";

        // Token: 0x04000008 RID: 8
        //  private string REMOVE_SCREEN_FROM_DEVICES = "adb -s {0} shell rm -f \"{1}\"";

        // Token: 0x04000009 RID: 9
        private string GET_SCREEN_RESOLUTION = "adb -s {0} shell dumpsys display | Find \"mCurrentDisplayRect\"";

        // Token: 0x0400000A RID: 10
        private const int DEFAULT_SWIPE_DURATION = 100;

        // Token: 0x0400000B RID: 11
        private string ADB_FOLDER_PATH = "";

        // Token: 0x0400000C RID: 12
        private string ADB_PATH = "";

        public List<string> GetKeyboad()
        {
            List<string> list = new List<string>();
            string input = ExecuteCMD("adb shell ime list -a  ");
            string[] arr = input.Split('\n');

            // List<string> list_devices= new List<string>();

            return list;
        }
        public List<string> GetDevices()
        {
            List<string> list = new List<string>();
            string input = ExecuteCMD(SettingTool.data["devices"]);
            string[] arr = input.Split('\n');

            // List<string> list_devices= new List<string>();
            foreach (string str in arr)
            {
                if (str.Contains("device\r"))
                {
                    string[] arr1 = str.Trim().Split('\t');
                    list.Add(arr1[0].Trim());
                }
            }
            return list;
        }
        public List<string> GetApp(DeviceData device)
        {
            List<string> list = new List<string>();
            try
            {
                var receiver = new ConsoleOutputReceiver();
                SharpAdbClient.DeviceCommands.PackageManager package = new SharpAdbClient.DeviceCommands.PackageManager(device, true);
                foreach (var item in package.Packages)
                {
                    if (item.Key.Contains("com.facebook.kata"))
                    {

                        list.Add(item.Key);
                    }
                }
            }
            catch
            {
            }
            return list;
        }

        public Bitmap ScreenShoot(DeviceData device, bool isDeleteImageAfterCapture = true, string fileName = "screenShoot.png")
        {
            string CAPTURE_SCREEN_TO_DEVICES = SettingTool.data["capturescreen"];
            Random rd = new Random();
            string filename = rd.Next(100000) + ".png";
            string text = Application.StartupPath + "\\" + filename;

            if (File.Exists(text))
            {
                File.Delete(text);
            }

            string function = string.Format(CAPTURE_SCREEN_TO_DEVICES, device.Serial, "/sdcard/" + filename);

            ExecuteCMD(string.Format(CAPTURE_SCREEN_TO_DEVICES, device.Serial, "/sdcard/" + filename));
            ExecuteCMD(string.Format(SettingTool.data["pullscreen"], device.Serial, "/sdcard/" + filename));
            ExecuteCMD(string.Format(SettingTool.data["removescreen"], device.Serial, "/sdcard/" + filename));
            // text2 = text2 + Environment.NewLine + string.Format(PULL_SCREEN_FROM_DEVICES, deviceID, "/sdcard/" + text);
            //  text2 = text2 + Environment.NewLine + string.Format(REMOVE_SCREEN_FROM_DEVICES, deviceID, "/sdcard/" + text) + Environment.NewLine;
            //  string text3 = ExecuteCMD(text2);
            Bitmap result;
            using (Bitmap bitmap = new Bitmap(Application.StartupPath + "\\" + filename))
            {
                result = new Bitmap(bitmap);
            }
            if (isDeleteImageAfterCapture)
            {
                try
                {
                    File.Delete(text);
                }
                catch
                {
                }
            }
            return result;
        }
        public Point FindByXpath(DeviceData device, string xpath)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    HtmlNode.ElementsFlags.Remove("form");
                    document.LoadHtml(html);
                    HtmlNode node = document.DocumentNode.SelectSingleNode(xpath);

                    if (node != null)
                    {
                        string text = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                        string[] arr = text.Split(']');
                        if (arr.Length > 0)
                        {
                            string[] arr1 = arr[0].Split(',');
                            point.X = Convert.ToInt32(arr1[0]);
                            point.Y = Convert.ToInt32(arr1[1]);
                            return point;
                        }
                    }
                //}

            }
            catch { }
            return point;
        }
        public void pullfile(DeviceData device, string path)
        {
            string cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);


        }

        public Point FindByXpath(DeviceData device, string xpath, int num)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    HtmlNode.ElementsFlags.Remove("form");
                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    HtmlNode node = nodes[num];
                    {
                        string text = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                        string[] arr = text.Split(']');
                        if (arr.Length > 0)
                        {
                            string[] arr1 = arr[0].Split(',');
                            point.X = Convert.ToInt32(arr1[0]);
                            point.Y = Convert.ToInt32(arr1[1]);
                            return point;
                        }
                    }
                //}

            }
            catch { }
            return point;
        }


        public List<Point> FindListPointByXpath(DeviceData device, string xpath)
        {
            Random rd = new Random();
            List<Point> list_point = new List<Point>();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    HtmlNode.ElementsFlags.Remove("form");
                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {
                        string text = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                        string[] arr = text.Split(']');
                        if (arr.Length > 0)
                        {
                            string[] arr1 = arr[0].Split(',');
                            Point point = new Point();
                            point.X = Convert.ToInt32(arr1[0]);
                            point.Y = Convert.ToInt32(arr1[1]);
                            list_point.Add(point);
                        }
                    }
               // }

            }
            catch { }
            return list_point;
        }
        #region detechfunction
        public DetechModel detechFunction(DeviceData device, List<DetechModel> list_detech)
        {
            DetechModel kq = new DetechModel();

            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path).ToLower();
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);

                foreach (DetechModel model in list_detech)
                {
                    if (html.Contains(model.content.ToLower()))
                    {
                        if (String.IsNullOrEmpty(model.node))
                        {
                            kq = model;
                            kq.status = true;
                            kq.function = model.function;
                            kq.point = point;
                            kq.parent = model.parent;
                            return kq;
                        }
                        else
                        {
                            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(model.node.ToLower());
                            if (nodes != null)
                            {
                                foreach (HtmlNode node in nodes)
                                {
                                    string enable = node.Attributes["enabled"].Value.ToString();
                                    if (enable == "true")
                                    {
                                        try
                                        {
                                            string text = node.Attributes["text"].Value.ToString();
                                            string desc = node.Attributes["content-desc"].Value.ToString();
                                            if (text.Contains(model.text.ToLower()) || desc.Contains(model.text.ToLower()))
                                            {
                                                string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                                string[] arr = bound.Split(']');
                                                if (arr.Length > 0)
                                                {
                                                    kq = model;
                                                    string[] arr1 = arr[0].Split(',');
                                                    point.X = Convert.ToInt32(arr1[0]);
                                                    point.Y = Convert.ToInt32(arr1[1]);
                                                    kq.status = true;
                                                    kq.function = model.function;
                                                    kq.point = point;
                                                    kq.parent = model.parent;
                                                    return kq;
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }

                                    }

                                }
                            }

                        }
                    }
                }


            }
            catch { }
            return kq;
        }

        public DetechModel detechFunction_resourceID(DeviceData device, List<DetechModel> list_detech)
        {
            DetechModel kq = new DetechModel();

            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path).ToLower();
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);

                foreach (DetechModel model in list_detech)
                {
                    if (html.Contains(model.content.ToLower()))
                    {
                        if (String.IsNullOrEmpty(model.node))
                        {
                            kq = model;
                            kq.status = true;
                            kq.function = model.function;
                            kq.point = point;
                            kq.parent = model.parent;
                            return kq;
                        }
                        else
                        {
                            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(model.node.ToLower());
                            if (nodes != null)
                            {
                                foreach (HtmlNode node in nodes)
                                {
                                    string enable = node.Attributes["enabled"].Value.ToString();
                                    if (enable == "true")
                                    {
                                        try
                                        {
                                            string text = node.Attributes["resource-id"].Value.ToString();
                                            string desc = node.Attributes["resource-id"].Value.ToString();
                                            if (text.Contains(model.text.ToLower()) || desc.Contains(model.text.ToLower()))
                                            {
                                                string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                                string[] arr = bound.Split(']');
                                                if (arr.Length > 0)
                                                {
                                                    kq = model;
                                                    string[] arr1 = arr[0].Split(',');
                                                    point.X = Convert.ToInt32(arr1[0]);
                                                    point.Y = Convert.ToInt32(arr1[1]);
                                                    kq.status = true;
                                                    kq.function = model.function;
                                                    kq.point = point;
                                                    kq.parent = model.parent;
                                                    return kq;
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }

                                    }

                                }
                            }

                        }
                    }
                }


            }
            catch { }
            return kq;
        }

        public DetechModel RunDetechFunction(DeviceData device, List<DetechModel> list_detech, int i = 5)
        {
            DetechModel kq = new DetechModel();
            try
            {
                while (i > 0)
                {
                    i--;
                    kq = detechFunction(device, list_detech);
                    if (kq.status)
                    {
                        break;
                    }
                }
            }
            catch
            {

            }
            return kq;
        }

        public DetechModel RunDetechFunctionResourceID(DeviceData device, List<DetechModel> list_detech, int i = 5)
        {
            DetechModel kq = new DetechModel();
            try
            {
                while (i > 0)
                {
                    i--;
                    kq = detechFunction_resourceID(device, list_detech);
                    if (kq.status)
                    {
                        break;
                    }
                }
            }
            catch
            {

            }
            return kq;
        }

        #endregion
        public Point FindByXpath(DeviceData device, string xpath, string datatext)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //  if (data.Contains("100"))

                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path);
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                document.LoadHtml(html);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    string text = node.Attributes["text"].Value.ToString().ToLower();
                    if (text.Contains(datatext.ToLower()))
                    {
                        string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                        string[] arr = bound.Split(']');
                        if (arr.Length > 0)
                        {
                            string[] arr1 = arr[0].Split(',');
                            point.X = Convert.ToInt32(arr1[0]);
                            point.Y = Convert.ToInt32(arr1[1]);
                            return point;
                        }
                    }
                }


            }
            catch { }
            return point;
        }
        public Point FindByXpath(DeviceData device, string xpath, List<string> list_text)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);

                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path);
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                document.LoadHtml(html);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    try
                    {
                        string text = node.Attributes["text"].Value.ToString().ToLower();
                        foreach (string template in list_text)
                        {
                            if (text.Contains(template.ToLower()))
                            {
                                if (template.ToLower() == "ok")
                                {
                                    if (text.Contains("facebook") == false)
                                    {
                                        string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                        string[] arr = bound.Split(']');
                                        if (arr.Length > 0)
                                        {
                                            string[] arr1 = arr[0].Split(',');
                                            point.X = Convert.ToInt32(arr1[0]);
                                            point.Y = Convert.ToInt32(arr1[1]);
                                            return point;
                                        }
                                    }

                                }
                                else
                                {
                                    string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                    string[] arr = bound.Split(']');
                                    if (arr.Length > 0)
                                    {
                                        string[] arr1 = arr[0].Split(',');
                                        point.X = Convert.ToInt32(arr1[0]);
                                        point.Y = Convert.ToInt32(arr1[1]);
                                        return point;
                                    }

                                }


                            }
                        }

                    }
                    catch
                    { }

                }


            }
            catch { }
            return point;
        }

        //find list xpath
        public Point FindByListXpath(DeviceData device, List<string> list_xpath, List<string> list_text)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);

                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path);
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);
                foreach (string xpath in list_xpath)
                {
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    if (nodes != null)
                    {
                        foreach (HtmlNode node in nodes)
                        {
                            try
                            {
                                string text = node.Attributes["text"].Value.ToString().ToLower();
                                foreach (string template in list_text)
                                {
                                    if (text.Contains(template.ToLower()))
                                    {
                                        if (template.ToLower() == "ok")
                                        {
                                            if (text.Contains("facebook") == false)
                                            {
                                                string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                                string[] arr = bound.Split(']');
                                                if (arr.Length > 0)
                                                {
                                                    string[] arr1 = arr[0].Split(',');
                                                    point.X = Convert.ToInt32(arr1[0]);
                                                    point.Y = Convert.ToInt32(arr1[1]);
                                                    return point;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                            string[] arr = bound.Split(']');
                                            if (arr.Length > 0)
                                            {
                                                string[] arr1 = arr[0].Split(',');
                                                point.X = Convert.ToInt32(arr1[0]);
                                                point.Y = Convert.ToInt32(arr1[1]);
                                                return point;
                                            }
                                        }
                                    }
                                }

                            }
                            catch
                            { }
                        }
                    }
                }



            }
            catch { }
            return point;
        }

        public List<Point> FindByDescList(DeviceData device, string nodecha, string nodecon)
        {
            Random rd = new Random();
            List<Point> list_point = new List<Point>();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    HtmlNode.ElementsFlags.Remove("form");
                    document.LoadHtml(html);
                    HtmlNode nodeparent = document.DocumentNode.SelectSingleNode(nodecha);
                    if (nodeparent != null)
                    {
                        html = nodeparent.InnerHtml;
                        document = new HtmlAgilityPack.HtmlDocument();
                        HtmlNode.ElementsFlags.Remove("form");
                        document.LoadHtml(html);
                        HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(nodecon);
                        foreach (HtmlNode node in nodes)
                        {
                            string text = node.Attributes["bounds"].Value.ToString().Remove(0, 1).ToLower();
                            string[] arr = text.Split(']');
                            if (arr.Length > 0)
                            {
                                string[] arr1 = arr[0].Split(',');
                                Point point = new Point();
                                point.X = Convert.ToInt32(arr1[0]);
                                point.Y = Convert.ToInt32(arr1[1]);
                                list_point.Add(point);
                            }
                        }
                    }

                //}

            }
            catch { }
            return list_point;
        }

        public List<Point> FindByXpathListUncheck(DeviceData device, string xpath)
        {
            Random rd = new Random();
            List<Point> list_point = new List<Point>();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    HtmlNode.ElementsFlags.Remove("form");
                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {
                        string text_checked = node.Attributes["checked"].Value.ToString().ToLower();

                        if (text_checked == "false")
                        {
                            string text = node.Attributes["bounds"].Value.ToString().Remove(0, 1).ToLower();
                            string[] arr = text.Split(']');
                            if (arr.Length > 0)
                            {
                                string[] arr1 = arr[0].Split(',');
                                Point point = new Point();
                                point.X = Convert.ToInt32(arr1[0]);
                                point.Y = Convert.ToInt32(arr1[1]);
                                list_point.Add(point);
                            }
                        }
                    }
                //}

            }
            catch { }
            return list_point;
        }

        public List<Point> FindByXpathList(DeviceData device, string xpath)
        {
            Random rd = new Random();
            List<Point> list_point = new List<Point>();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    HtmlNode.ElementsFlags.Remove("form");
                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {

                        string text = node.Attributes["bounds"].Value.ToString().Remove(0, 1).ToLower();
                        string[] arr = text.Split(']');
                        if (arr.Length > 0)
                        {
                            string[] arr1 = arr[0].Split(',');
                            Point point = new Point();
                            point.X = Convert.ToInt32(arr1[0]);
                            point.Y = Convert.ToInt32(arr1[1]);
                            list_point.Add(point);
                        }
                    }
                //}

            }
            catch { }
            return list_point;
        }

        public List<Point> FindByXpathList(DeviceData device, string xpath, List<string> list_text)
        {
            Random rd = new Random();
            List<Point> list_point = new List<Point>();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    HtmlNode.ElementsFlags.Remove("form");
                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {

                        string text = node.Attributes["text"].Value.ToString().ToLower();
                        foreach (string template in list_text)
                        {
                            if (text.Contains(template.ToLower()))
                            {
                                text = node.Attributes["bounds"].Value.ToString().Remove(0, 1).ToLower();
                                string[] arr = text.Split(']');
                                if (arr.Length > 0)
                                {
                                    string[] arr1 = arr[0].Split(',');
                                    Point point = new Point();
                                    point.X = Convert.ToInt32(arr1[0]);
                                    point.Y = Convert.ToInt32(arr1[1]);
                                    list_point.Add(point);
                                }

                            }
                        }

                    }
               // }

            }
            catch { }
            return list_point;
        }

        public Point FindByXpathDesc(DeviceData device, string xpath, string content)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {
                        string text = node.Attributes["content-desc"].Value.ToString().ToLower();
                        if (text.Contains(content.ToLower()))
                        {
                            string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                            string[] arr = bound.Split(']');
                            if (arr.Length > 0)
                            {
                                string[] arr1 = arr[0].Split(',');
                                point.X = Convert.ToInt32(arr1[0]);
                                point.Y = Convert.ToInt32(arr1[1]);
                                return point;
                            }
                        }
                    }
               // }

            }
            catch { }
            return point;
        }
        public Point FindByXpathIndex(DeviceData device, string xpath, string content)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {
                        string text = node.Attributes["Index"].Value.ToString().ToLower();
                        if (text.Contains(content.ToLower()))
                        {
                            string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                            string[] arr = bound.Split(']');
                            if (arr.Length > 0)
                            {
                                string[] arr1 = arr[0].Split(',');
                                point.X = Convert.ToInt32(arr1[0]);
                                point.Y = Convert.ToInt32(arr1[1]);
                                return point;
                            }
                        }
                    }
                //}

            }
            catch { }
            return point;
        }

        public Point FindByXpathDesc(DeviceData device, string xpath, List<string> list_text)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);

                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path);
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                document.LoadHtml(html);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    string text = node.Attributes["content-desc"].Value.ToString().ToLower();
                    foreach (string content in list_text)
                    {
                        if (text.Contains(content.ToLower()))
                        {
                            string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                            string[] arr = bound.Split(']');
                            if (arr.Length > 0)
                            {
                                string[] arr1 = arr[0].Split(',');
                                point.X = Convert.ToInt32(arr1[0]);
                                point.Y = Convert.ToInt32(arr1[1]);
                                return point;
                            }
                        }
                    }
                }


            }
            catch { }
            return point;
        }

        public Point FindByListXpathDesc(DeviceData device, List<string> list_xpath, List<string> list_text)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                    document.LoadHtml(html);
                    foreach (string xpath in list_xpath)
                    {
                        HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                        if (nodes != null)
                        {
                            foreach (HtmlNode node in nodes)
                            {
                                string text = node.Attributes["content-desc"].Value.ToString().ToLower();
                                foreach (string content in list_text)
                                {
                                    if (text.Contains(content.ToLower()))
                                    {
                                        string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                        string[] arr = bound.Split(']');
                                        if (arr.Length > 0)
                                        {
                                            string[] arr1 = arr[0].Split(',');
                                            point.X = Convert.ToInt32(arr1[0]);
                                            point.Y = Convert.ToInt32(arr1[1]);
                                            return point;
                                        }
                                    }
                                }
                            }
                        }
                    }
               // }

            }
            catch { }
            return point;
        }

        public Point FindByListXpathDesc(DeviceData device, List<string> list_xpath, string content)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                    document.LoadHtml(html);
                    foreach (string xpath in list_xpath)
                    {
                        HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                        if (nodes != null)
                        {
                            foreach (HtmlNode node in nodes)
                            {
                                string text = node.Attributes["content-desc"].Value.ToString().ToLower();

                                if (text.Contains(content.ToLower()))
                                {
                                    string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                    string[] arr = bound.Split(']');
                                    if (arr.Length > 0)
                                    {
                                        string[] arr1 = arr[0].Split(',');
                                        point.X = Convert.ToInt32(arr1[0]);
                                        point.Y = Convert.ToInt32(arr1[1]);
                                        return point;
                                    }
                                }

                            }
                        }
                    }
                //}

            }
            catch { }
            return point;
        }
        public List<Point> FindByXpathDescList(DeviceData device, string xpath, List<string> list_text)
        {
            Random rd = new Random();
            List<Point> list_point = new List<Point>();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    HtmlNode.ElementsFlags.Remove("form");
                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {

                        string text = node.Attributes["content-desc"].Value.ToString().ToLower();
                        foreach (string template in list_text)
                        {
                            if (text.Contains(template.ToLower()))
                            {
                                text = node.Attributes["bounds"].Value.ToString().Remove(0, 1).ToLower();
                                string[] arr = text.Split(']');
                                if (arr.Length > 0)
                                {
                                    string[] arr1 = arr[0].Split(',');
                                    Point point = new Point();
                                    point.X = Convert.ToInt32(arr1[0]);
                                    point.Y = Convert.ToInt32(arr1[1]);
                                    list_point.Add(point);
                                }

                            }
                        }

                    }
                //}

            }
            catch { }
            return list_point;
        }

        public void FindByXpathBound(DeviceData device, string xpath, ref Point point1, ref Point point2)
        {
            Random rd = new Random();

            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {
                        string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);

                        string[] arr = bound.Split(']');
                        if (arr.Length > 0)
                        {
                            string[] arr1 = arr[0].Split(',');
                            point1.X = Convert.ToInt32(arr1[0]);
                            point1.Y = Convert.ToInt32(arr1[1]);

                            string databound = arr[1].Replace("[", "").Replace("]", "");
                            string[] arr2 = databound.Split(',');
                            point2.X = Convert.ToInt32(arr2[0]);
                            point2.Y = Convert.ToInt32(arr2[1]);
                        }
                        return;
                    }
                //}

            }
            catch { }

        }
        public void FindByXpathDescBound(DeviceData device, string xpath, string content, ref Point point1, ref Point point2)
        {
            Random rd = new Random();

            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {
                        string text = node.Attributes["content-desc"].Value.ToString().ToLower();
                        if (text.Contains(content.ToLower()))
                        {
                            string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);

                            string[] arr = bound.Split(']');
                            if (arr.Length > 0)
                            {
                                string[] arr1 = arr[0].Split(',');
                                point1.X = Convert.ToInt32(arr1[0]);
                                point1.Y = Convert.ToInt32(arr1[1]);

                                string databound = arr[1].Replace("[", "").Replace("]", "");
                                string[] arr2 = databound.Split(',');
                                point2.X = Convert.ToInt32(arr2[0]);
                                point2.Y = Convert.ToInt32(arr2[1]);
                            }
                            return;
                        }
                    }
                //}

            }
            catch { }

        }

        public void FindByXpathDescBound(DeviceData device, string xpath, List<string> list_des, ref Point point1, ref Point point2)
        {
            Random rd = new Random();

            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path);
                    if (File.Exists(path))
                        File.Delete(path);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                    document.LoadHtml(html);
                    HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                    foreach (HtmlNode node in nodes)
                    {
                        string text = node.Attributes["content-desc"].Value.ToString().ToLower();
                        foreach (string content in list_des)
                        {
                            if (text.Contains(content.ToLower()))
                            {
                                string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);

                                string[] arr = bound.Split(']');
                                if (arr.Length > 0)
                                {
                                    string[] arr1 = arr[0].Split(',');
                                    point1.X = Convert.ToInt32(arr1[0]);
                                    point1.Y = Convert.ToInt32(arr1[1]);

                                    string databound = arr[1].Replace("[", "").Replace("]", "");
                                    string[] arr2 = databound.Split(',');
                                    point2.X = Convert.ToInt32(arr2[0]);
                                    point2.Y = Convert.ToInt32(arr2[1]);
                                }
                                return;


                            }
                        }
                    }
                //}

            }
            catch { }

        }
        public bool checkContent(DeviceData device, string content)
        {
            Random rd = new Random();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);
            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path).ToLower();
                    if (File.Exists(path))
                        File.Delete(path);
                    if (html.Contains(content.ToLower()))
                        return true;
                //}

            }
            catch { }
            return false;
        }
        public string checkListContent(DeviceData device, List<string> list_content)
        {
            Random rd = new Random();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);
            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //if (data.Contains("100"))
                //{
                    cmdCommand = "rm -f /sdcard/window_dump.xml";
                    data = runComand(device, cmdCommand);
                    string html = File.ReadAllText(path).ToLower();
                    if (File.Exists(path))
                        File.Delete(path);
                    foreach (string content in list_content)
                    {
                        if (html.Contains(content.ToLower()))
                            return content;
                    }

               // }

            }
            catch { }
            return null;
        }

        public Point FindByXpathResourceID(DeviceData device, string xpath, string datatext)
        {
            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //  if (data.Contains("100"))

                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path);
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                document.LoadHtml(html);
                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);
                foreach (HtmlNode node in nodes)
                {
                    string text = node.Attributes["resource-id"].Value.ToString().ToLower();
                    if (text.Contains(datatext.ToLower()))
                    {
                        string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                        string[] arr = bound.Split(']');
                        if (arr.Length > 0)
                        {
                            string[] arr1 = arr[0].Split(',');
                            point.X = Convert.ToInt32(arr1[0]);
                            point.Y = Convert.ToInt32(arr1[1]);
                            return point;
                        }
                    }
                }


            }
            catch { }
            return point;
        }
        public void FindBoundDetech_unLower(DeviceData device, List<DetechModel> list_detech, ref Point point1, ref Point point2)
        {
            DetechModel kq = new DetechModel();
            Random rd = new Random();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);
            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                //  if (data.Contains("100"))

                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path);
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                document.LoadHtml(html);
                foreach (DetechModel model in list_detech)
                {
                    if (html.Contains(model.content))
                    {
                        HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(model.node);
                        if (nodes != null)
                        {
                            foreach (HtmlNode node in nodes)
                            {
                                string enable = node.Attributes["enabled"].Value.ToString();
                                if (enable == "true")
                                {
                                    try
                                    {
                                        string text = node.Attributes["text"].Value.ToString();
                                        string desc = node.Attributes["content-desc"].Value.ToString();
                                        if (text.Contains(model.text) || desc.Contains(model.text))
                                        {
                                            string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);

                                            string[] arr = bound.Split(']');
                                            if (arr.Length > 0)
                                            {
                                                string[] arr1 = arr[0].Split(',');
                                                point1.X = Convert.ToInt32(arr1[0]);
                                                point1.Y = Convert.ToInt32(arr1[1]);

                                                string databound = arr[1].Replace("[", "").Replace("]", "");
                                                string[] arr2 = databound.Split(',');
                                                point2.X = Convert.ToInt32(arr2[0]);
                                                point2.Y = Convert.ToInt32(arr2[1]);
                                            }
                                            return;
                                        }
                                    }
                                    catch
                                    {

                                    }

                                }

                            }
                        }
                    }
                }

            }
            catch { }

        }

        public DetechModel detechFunction_exact(DeviceData device, List<DetechModel> list_detech)
        {
            DetechModel kq = new DetechModel();

            Random rd = new Random();
            Point point = new Point();
            string filename = rd.Next(9999999) + ".xml";
            string path = "C:\\test";
            path = path + "\\" + filename;
            if (File.Exists(path))
                File.Delete(path);

            try
            {
                string cmdCommand = String.Format("uiautomator dump", path);
                string data = runComand(device, cmdCommand);

                cmdCommand = String.Format("adb -s {0} pull /sdcard/window_dump.xml {1}", device.Serial, path);
                data = ExecuteCMD(cmdCommand);
                cmdCommand = "rm -f /sdcard/window_dump.xml";
                data = runComand(device, cmdCommand);
                string html = File.ReadAllText(path).ToLower();
                if (File.Exists(path))
                    File.Delete(path);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);

                foreach (DetechModel model in list_detech)
                {
                    if (html.Contains(model.content.ToLower()))
                    {
                        if (String.IsNullOrEmpty(model.node))
                        {
                            kq = model;
                            kq.status = true;
                            kq.function = model.function;
                            kq.point = point;
                            kq.parent = model.parent;
                            return kq;
                        }
                        else
                        {
                            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(model.node.ToLower());
                            if (nodes != null)
                            {
                                foreach (HtmlNode node in nodes)
                                {
                                    string enable = node.Attributes["enabled"].Value.ToString();
                                    if (enable == "true")
                                    {
                                        try
                                        {
                                            string text = node.Attributes["text"].Value.ToString();
                                            string desc = node.Attributes["content-desc"].Value.ToString();
                                            if (text == model.text.ToLower() || desc == model.text.ToLower())
                                            {
                                                string bound = node.Attributes["bounds"].Value.ToString().Remove(0, 1);
                                                string[] arr = bound.Split(']');
                                                if (arr.Length > 0)
                                                {
                                                    kq = model;
                                                    string[] arr1 = arr[0].Split(',');
                                                    point.X = Convert.ToInt32(arr1[0]);
                                                    point.Y = Convert.ToInt32(arr1[1]);
                                                    kq.status = true;
                                                    kq.function = model.function;
                                                    kq.point = point;
                                                    kq.parent = model.parent;
                                                    return kq;
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }

                                    }

                                }
                            }

                        }
                    }
                }


            }
            catch { }
            return kq;
        }
    }
}
