using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSystem
{
    public class ImageScanOpenCV
    {
        // Token: 0x0600005D RID: 93 RVA: 0x00003840 File Offset: 0x00001A40
        public static Bitmap GetImage(string path)
        {
            return new Bitmap(path);
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00003858 File Offset: 0x00001A58
        public static Bitmap Find(string main, string sub, double percent = 0.9)
        {
            Bitmap image = ImageScanOpenCV.GetImage(main);
            Bitmap image2 = ImageScanOpenCV.GetImage(sub);
            return ImageScanOpenCV.Find(main, sub, percent);
        }

        // Token: 0x0600005F RID: 95 RVA: 0x00003884 File Offset: 0x00001A84
        public static Bitmap Find(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
            Image<Bgr, byte> image2 = new Image<Bgr, byte>(subBitmap);
            Image<Bgr, byte> image3 = image.Copy();
            using (Image<Gray, float> image4 = image.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed))
            {
                double[] array;
                double[] array2;
                Point[] array3;
                Point[] array4;
                image4.MinMax(out array, out array2, out array3, out array4);
                bool flag = array2[0] > percent;
                if (flag)
                {
                    Rectangle rect = new Rectangle(array4[0], image2.Size);
                    image3.Draw(rect, new Bgr(Color.Red), 2, LineType.EightConnected, 0);
                }
                else
                {
                    image3 = null;
                }
            }
            return (image3 == null) ? null : image3.ToBitmap();
        }

        // Token: 0x06000060 RID: 96 RVA: 0x00003930 File Offset: 0x00001B30
        public static Point FindOutPoint(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            Point result = new Point();
            try
            {
                Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
                Image<Bgr, byte> template = new Image<Bgr, byte>(subBitmap);

                using (Image<Gray, float> image2 = image.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
                {
                    double[] array;
                    double[] array2;
                    Point[] array3;
                    Point[] array4;
                    image2.MinMax(out array, out array2, out array3, out array4);
                    bool flag = array2[0] > percent;
                    if (flag)
                    {
                        result.X = array4[0].X;
                        result.Y = array4[0].Y;
                    }
                }
            }
            catch { }
            return result;
        }


    }
}
