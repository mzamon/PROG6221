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
            //Byete Array
            byte[] bNumbers = new byte[5];
            //double Array
            double[] DoubleNumbers = new double[5];
            //String Array
            string[] sNames = new string[5];

            //Populate iNumbers Array
            for (int i = 0; i < iNumbers.Length; i++)
            {
                Console.Write($"Enter a number for index value: {i} : ");
                iNumbers[i] = int.Parse(Console.ReadLine());
            }
            //Populate bNumbers Array
            for (int i = 0; i < bNumbers.Length; i++)
            {
                Console.Write($"Enter a byte number for index value: {i} : ");
                bNumbers[i] = byte.Parse(Console.ReadLine());
            }
            //Populate DoubleNumbers Array
            for (int i = 0; i < DoubleNumbers.Length; i++)
            {
                Console.Write($"Enter a double number for index value: {i} : ");
                DoubleNumbers[i] = double.Parse(Console.ReadLine());
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
            //Print bNumbers array
            Console.WriteLine("\nThe values for byte number are:");
            foreach (byte Num in bNumbers)
            {
                Console.WriteLine(Num);
            }
            //Print DoubleNumbers array
            Console.WriteLine("\nThe values for double number are:");
            foreach (double Num in DoubleNumbers)
            {
                Console.WriteLine(Num);
            }
            //Print sNames array
            Console.WriteLine("\nThe values for number are:");
            foreach (string name in sNames)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("\nEND OF LINE");
        }
    }
}