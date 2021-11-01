using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAutoHelper;
using System.Drawing;
namespace NinjaSystem
{
    public class HotspotController
    {
        public bool connectHotspot(string path)
        {
            try
            {
                Process p = Process.Start(path); 
               
                Thread.Sleep(3000);
               var x= KAutoHelper.AutoControl.FindHandle(p.Handle, "", "");
                
                var screen = CaptureHelper.CaptureScreen();
                screen.Save("pic.png");
                var stopbutton = Properties.Resources.check;
                var point = ImageScanOpenCV.FindOutPoint((Bitmap)screen, stopbutton);
                if (point.X > 0 || point.Y > 0)
                {
                    KAutoHelper.AutoControl.MouseClick(point);
                }
            }
            catch
            {

            }
            return false;
        }
    }
}
