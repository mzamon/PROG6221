using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Integer Array
            int[] iNumbers = new int[5];
            //String Array
            string[] sNames = new string[5];

            //Populate iNumbers Array
            for (int i = 0; i < iNumbers.Length; i++)
            {
                Console.Write($"Enter a number for index value: {i} : ");
                iNumbers[i] = int.Parse(Console.ReadLine());
            }
            //Populate sNames Array
            for (int i = 0; i < sNames.Length; i++)
            {
                Console.Write($"Enter a name for index value: {i} : ");
                sNames[i] = Console.ReadLine();
            }
            //Print iNumbers array
            Console.WriteLine("\nThe values for number are:");
            foreach (int Num in iNumbers)
            {
                Console.WriteLine(Num);
            }
            Console.WriteLine("\nEND OF LINE");
        }
    }
}