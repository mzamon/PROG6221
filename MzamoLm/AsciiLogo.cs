using System;
using SkiaSharp;//alternative
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace MzamoLm
{
    internal class AsciiLogo
    {
        private Bitmap image; //

        //constructor =
        public AsciiLogo()
        {
            try
            {
                SKBitmap image = SKBitmap.Decode("AsciiLogo.jpeg");
                AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);// for Linux setup
                //file path
                string sProjectPath = AppDomain.CurrentDomain.BaseDirectory;
                //bin/debug
                string sNewProjectPath = sProjectPath.Replace("bin\\Debug\\", "");
                //full path
                string sFullPath = Path.Combine(sNewProjectPath, "AsciiLogo.jpeg");
                //Ascii conversion 
                /*Bitmap = new Bitmap(sFullPath);
                image = new Bitmap(image, new Size(210, 200));
                */
                //prepare to load image
                if (File.Exists(sFullPath))
                {
                    image = new Bitmap(sFullPath);
                    image = new Bitmap(image, new(210, 200));//format
                    Console.WriteLine("AsciiLogo.jpeg WAS SUCCESFULLY LOADED!");
                }
                else
                {

                    Console.WriteLine("ERROR!\nAsciiLogo.jpeg WAS NOT FOUND!");
                }
            }
            catch ( Exception ex)
            {
                Console.WriteLine("ERROR!\nAsciiLogo.jpeg WAS NOT FOUND!");
            }



        }
    }
}