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
                //SKBitmap image = SKBitmap.Decode("AsciiLogo.jpeg");
                AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);// for Linux setup
                //file path
                string sProjectPath = AppDomain.CurrentDomain.BaseDirectory;
                //bin/debug
                string sNewProjectPath = sProjectPath.Replace("bin\\Debug\\", "");
                //full path
                string sFullPath = Path.Combine(sNewProjectPath, "AsciiLogo.bmp");
                //Ascii conversion 
                //prepare to load image
                if (!File.Exists(sFullPath))
                {
                    Program.BotColor();
                    Console.WriteLine("ERROR!\nAsciiLogo.bmp WAS NOT FOUND!");
                    return;
                }

                //Load image
                using (Bitmap originalImage = new Bitmap(sFullPath))
                {
                    // Resize the image to a specific width and height
                    image = new Bitmap(originalImage, new Size(200, 200)); 
                }
                
                Console.WriteLine("Successfully loaded bitmap!");
                //Show ASCII art
                Console.ForegroundColor = ConsoleColor.Blue;   
                AsciiLogoT();
            }
            catch ( Exception error)
            {
                Program.BotColor();
                Console.WriteLine("ERROR!\nAsciiLogo.bmp WAS NOT FOUND!" +  error.Message);
            }
        }
        public static void AsciiLogoT()
        {
            
            Bitmap bmp = new Bitmap("AsciiLogo.bmp");

            if (bmp == null)
            {
                Console.WriteLine("No image to convert to ASCII.");
                return;
            }

            for (int y = 0; y < bmp.Height; y += 5)
            {
                for (int x = 0; x < bmp.Width; x += 2)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    char asciiChar = gray < 128 ? '#' : '.';
                    Console.Write(asciiChar);
                }
                Console.WriteLine();
            }
        }
    }
}