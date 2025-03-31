
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter  favorite color");
            String color = Console.ReadLine();

            Console.WriteLine("Enter your favorite food");
            String food = Console.ReadLine();

            Console.WriteLine("Enter your favorite number");
            int number = int.Parse(Console.ReadLine());

            Console.WriteLine("\nDetails in a formatted message");
            Console.WriteLine("Colour: " + color);
            Console.WriteLine("Food: " + food);
            Console.WriteLine("Number: " + number);

        }
    }
}
