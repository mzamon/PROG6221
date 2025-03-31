
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_World
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!" );

            Console.WriteLine("Enter your name");
            String name = Console.ReadLine();

            Console.WriteLine("Enter your surname");
            String surname = Console.ReadLine();

            Console.WriteLine("Enter your age");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("\nDetails Extend:");
            Console.WriteLine("Name" + name);
            Console.WriteLine("Surname" + surname);
            Console.WriteLine("Age" + age);

        }
    }
}
