using System; // This is found in learning unit 3
using System.Collections.Generic;
using System.Linq;
using System.Text; // This is Learning unit 6
using System.Threading.Tasks;

namespace StudentManager
{
    // LO3: Class definition
    public class Student
    {
        // LO1 (Theme 1): Access modifiers and encapsulation
        public int ID { get; set; }                 // Automatic property (LO3, Theme 1)
        public string Name { get; set; }             // Automatic property
        public int? Age { get; set; }                // Nullable type (LO8)

        // Constructor
        public Student(int id, string name, int? age)
        {
            ID = id;
            Name = name;
            Age = age;
        }

        // Method to display student info
        public void DisplayInfo()
        {
            // LO6: String manipulation using StringBuilder
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Student Information:");
            sb.AppendLine($"ID: {ID}");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Age: {(Age.HasValue ? Age.ToString() : "Not Provided")}");
            Console.WriteLine(sb.ToString());
        }
    }

    // Main Program (Entry point)
    internal class Program
    {
        // LO1 (Theme 2): Main method (console program)
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Student Management System");

            // LO5: User input
            Console.Write("Enter Student ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Student Age (or leave blank): ");
            string ageInput = Console.ReadLine();
            int? age = string.IsNullOrEmpty(ageInput) ? (int?)null : int.Parse(ageInput);

            // LO7: Implicitly typed variable
            var student = new Student(id, name, age);

            // Display student information
            student.DisplayInfo();

            // LO2: Displaying .NET platform information
            Console.WriteLine("\n.NET Platform Information:");
            Console.WriteLine($".NET Version: {Environment.Version}");
            Console.WriteLine($"OS Version: {Environment.OSVersion}");

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
