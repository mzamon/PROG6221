using System;
using System.Drawing;
using System.IO;

namespace MzamoLm
{
    internal class AsciiLogo
    {
        public AsciiLogo()
        {
            try
            {
                AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
                string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "");
                string filePath = Path.Combine(path, "AsciiLogo.bmp");

                if (!File.Exists(filePath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: AsciiLogo.bmp not found!");
                    return;
                }

                Bitmap image = new Bitmap(filePath);
                DisplayAsciiFromBitmap(image);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error displaying ASCII logo: " + ex.Message);
            }
        }

        private void DisplayAsciiFromBitmap(Bitmap image)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Bitmap resized = new Bitmap(image, new Size(100, 50));

            for (int y = 0; y < resized.Height; y += 2)
            {
                for (int x = 0; x < resized.Width; x++)
                {
                    Color pixel = resized.GetPixel(x, y);
                    int brightness = (pixel.R + pixel.G + pixel.B) / 3;
                    char character = brightness > 200 ? '.' : brightness > 120 ? '*' : '#';
                    Console.Write(character);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}