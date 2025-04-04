using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Bitmap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AsciiLogo();
        }
        public static void AsciiLogo()
        {
            try
            {
                Bitmap bmp = new Bitmap("your_bitmap_image.bmp");

                for (int y = 0; y < bmp.Height; y += 5)
                {
                    for (int x = 0; x < bmp.Width; x += 2)
                    {
                        Color pixelColor = bmp.GetPixel(x, y);
                        int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        char asciiChar = GetAsciiChar(gray);
                        Console.Write(asciiChar);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static char GetAsciiChar(int gray)
        {
            const string asciiChars = "@%#*+=-:. ";
            int index = (gray * (asciiChars.Length - 1)) / 255;
            return asciiChars[index];
        }
    }
}