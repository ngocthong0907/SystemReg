using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using KAutoHelper;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace NinjaSystem
{
    public class SendTextKeyBoard
    {
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
       
        public static void SendTextKey(IntPtr handle,string text, bool isPress = false)
        {
            foreach (char c in text.ToLower())
            {
                Thread.Sleep(100);
                NinjaSystem.VKeys key = NinjaSystem.VKeys.VK_SELECT;
                char c2 = c;
                if (c2 != ' ')
                {
                    switch (c2)
                    {
                        case '@':
                            key = NinjaSystem.VKeys.VK_AT;
                            break;

                        case '.':
                            key = NinjaSystem.VKeys.VK_DECIMAL;
                            break;
                        case '/':
                            break;
                        case '0':
                            key = NinjaSystem.VKeys.VK_0;
                            break;
                        case '1':
                            key = NinjaSystem.VKeys.VK_1;
                            break;
                        case '2':
                            key = NinjaSystem.VKeys.VK_2;
                            break;
                        case '3':
                            key = NinjaSystem.VKeys.VK_3;
                            break;
                        case '4':
                            key = NinjaSystem.VKeys.VK_4;
                            break;
                        case '5':
                            key = NinjaSystem.VKeys.VK_5;
                            break;
                        case '6':
                            key = NinjaSystem.VKeys.VK_6;
                            break;
                        case '7':
                            key = NinjaSystem.VKeys.VK_7;
                            break;
                        case '8':
                            key = NinjaSystem.VKeys.VK_8;
                            break;
                        case '9':
                            key = NinjaSystem.VKeys.VK_9;
                            break;
                        default:
                            switch (c2)
                            {
                                case 'a':
                                    key = NinjaSystem.VKeys.VK_A;
                                    break;
                                case 'b':
                                    key = NinjaSystem.VKeys.VK_B;
                                    break;
                                case 'c':
                                    key = NinjaSystem.VKeys.VK_C;
                                    break;
                                case 'd':
                                    key = NinjaSystem.VKeys.VK_D;
                                    break;
                                case 'e':
                                    key = NinjaSystem.VKeys.VK_E;
                                    break;
                                case 'f':
                                    key = NinjaSystem.VKeys.VK_F;
                                    break;
                                case 'g':
                                    key = NinjaSystem.VKeys.VK_G;
                                    break;
                                case 'h':
                                    key = NinjaSystem.VKeys.VK_H;
                                    break;
                                case 'i':
                                    key = NinjaSystem.VKeys.VK_I;
                                    break;
                                case 'j':
                                    key = NinjaSystem.VKeys.VK_J;
                                    break;
                                case 'k':
                                    key = NinjaSystem.VKeys.VK_K;
                                    break;
                                case 'l':
                                    key = NinjaSystem.VKeys.VK_L;
                                    break;
                                case 'm':
                                    key = NinjaSystem.VKeys.VK_M;
                                    break;
                                case 'n':
                                    key = NinjaSystem.VKeys.VK_N;
                                    break;
                                case 'o':
                                    key = NinjaSystem.VKeys.VK_O;
                                    break;
                                case 'p':
                                    key = NinjaSystem.VKeys.VK_P;
                                    break;
                                case 'q':
                                    key = NinjaSystem.VKeys.VK_Q;
                                    break;
                                case 'r':
                                    key = NinjaSystem.VKeys.VK_R;
                                    break;
                                case 's':
                                    key = NinjaSystem.VKeys.VK_S;
                                    break;
                                case 't':
                                    key = NinjaSystem.VKeys.VK_T;
                                    break;
                                case 'u':
                                    key = NinjaSystem.VKeys.VK_U;
                                    break;
                                case 'v':
                                    key = NinjaSystem.VKeys.VK_V;
                                    break;
                                case 'w':
                                    key = NinjaSystem.VKeys.VK_W;
                                    break;
                                case 'x':
                                    key = NinjaSystem.VKeys.VK_X;
                                    break;
                                case 'y':
                                    key = NinjaSystem.VKeys.VK_Y;
                                    break;
                                case 'z':
                                    key = NinjaSystem.VKeys.VK_Z;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    key = NinjaSystem.VKeys.VK_SPACE;
                }
                bool flag = !isPress;
                if (flag)
                {
                   // AutoControl.SendKeyBoardUp(handle, (KAutoHelper.VKeys) key);
                    PostMessage(handle, 257, new IntPtr((int)key), new IntPtr(0));
                    if (c2 == '@')
                    {
                       // int iHandle = NativeWin32.FindWindow(null, "Leapdroid (v1.8.0.0)");
                        NativeWin32.SetForegroundWindow((int)handle);
                        SendKeys.SendWait("+2");
                    }
                }
                else
                {
                    PostMessage(handle, 256, new IntPtr((int)key), new IntPtr(0));
                    PostMessage(handle, 257, new IntPtr((int)key), new IntPtr(0));
                }
            }
        }

        public static void PressOnLeapdroid(IntPtr handle, string text, bool isPress = false)
        {
            foreach (char c in text.ToLower())
            {
               
                NinjaSystem.VKeys key = NinjaSystem.VKeys.VK_SELECT;
                char c2 = c;
                if (c2 != ' ')
                {
                    switch (c2)
                    {
                        case '@':
                            key = NinjaSystem.VKeys.VK_AT;
                            break;

                        case '.':
                            key = NinjaSystem.VKeys.VK_DECIMAL;
                            break;
                        case '/':
                            break;
                        case '0':
                            key = NinjaSystem.VKeys.VK_0;
                            break;
                        case '1':
                            key = NinjaSystem.VKeys.VK_1;
                            break;
                        case '2':
                            key = NinjaSystem.VKeys.VK_2;
                            break;
                        case '3':
                            key = NinjaSystem.VKeys.VK_3;
                            break;
                        case '4':
                            key = NinjaSystem.VKeys.VK_4;
                            break;
                        case '5':
                            key = NinjaSystem.VKeys.VK_5;
                            break;
                        case '6':
                            key = NinjaSystem.VKeys.VK_6;
                            break;
                        case '7':
                            key = NinjaSystem.VKeys.VK_7;
                            break;
                        case '8':
                            key = NinjaSystem.VKeys.VK_8;
                            break;
                        case '9':
                            key = NinjaSystem.VKeys.VK_9;
                            break;
                        default:
                            switch (c2)
                            {
                                case 'a':
                                    key = NinjaSystem.VKeys.VK_A;
                                    break;
                                case 'b':
                                    key = NinjaSystem.VKeys.VK_B;
                                    break;
                                case 'c':
                                    key = NinjaSystem.VKeys.VK_C;
                                    break;
                                case 'd':
                                    key = NinjaSystem.VKeys.VK_D;
                                    break;
                                case 'e':
                                    key = NinjaSystem.VKeys.VK_E;
                                    break;
                                case 'f':
                                    key = NinjaSystem.VKeys.VK_F;
                                    break;
                                case 'g':
                                    key = NinjaSystem.VKeys.VK_G;
                                    break;
                                case 'h':
                                    key = NinjaSystem.VKeys.VK_H;
                                    break;
                                case 'i':
                                    key = NinjaSystem.VKeys.VK_I;
                                    break;
                                case 'j':
                                    key = NinjaSystem.VKeys.VK_J;
                                    break;
                                case 'k':
                                    key = NinjaSystem.VKeys.VK_K;
                                    break;
                                case 'l':
                                    key = NinjaSystem.VKeys.VK_L;
                                    break;
                                case 'm':
                                    key = NinjaSystem.VKeys.VK_M;
                                    break;
                                case 'n':
                                    key = NinjaSystem.VKeys.VK_N;
                                    break;
                                case 'o':
                                    key = NinjaSystem.VKeys.VK_O;
                                    break;
                                case 'p':
                                    key = NinjaSystem.VKeys.VK_P;
                                    break;
                                case 'q':
                                    key = NinjaSystem.VKeys.VK_Q;
                                    break;
                                case 'r':
                                    key = NinjaSystem.VKeys.VK_R;
                                    break;
                                case 's':
                                    key = NinjaSystem.VKeys.VK_S;
                                    break;
                                case 't':
                                    key = NinjaSystem.VKeys.VK_T;
                                    break;
                                case 'u':
                                    key = NinjaSystem.VKeys.VK_U;
                                    break;
                                case 'v':
                                    key = NinjaSystem.VKeys.VK_V;
                                    break;
                                case 'w':
                                    key = NinjaSystem.VKeys.VK_W;
                                    break;
                                case 'x':
                                    key = NinjaSystem.VKeys.VK_X;
                                    break;
                                case 'y':
                                    key = NinjaSystem.VKeys.VK_Y;
                                    break;
                                case 'z':
                                    key = NinjaSystem.VKeys.VK_Z;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    key = NinjaSystem.VKeys.VK_SPACE;
                }
                bool flag = !isPress;
                if (flag)
                {
                    PostMessage(handle, 256, new IntPtr((int)key), new IntPtr(0));
                }
                else
                {
                    PostMessage(handle, 256, new IntPtr((int)key), new IntPtr(0));
                    PostMessage(handle, 257, new IntPtr((int)key), new IntPtr(0));
                }
            }
        }
    }


}
