using System.Collections;
using System;

namespace Array_List_HashTable
{
    internal class StudentManager
    {
        public StudentManager()
        {
            //Create a Hashtable
            Hashtable StudentGrades = new Hashtable();
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Enter 3 students and their grades:");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Enter name of the student {i + 1}: ");
                string SudentName = Console.ReadLine();
                Console.Write($"Enter the mark for {SudentName}: ");
                int grade = int.Parse(Console.ReadLine());
                //store
                StudentGrades[SudentName] = grade;
            }
            Console.WriteLine("\nStudent Grades:");
            foreach (DictionaryEntry entry in StudentGrades)
            {
                Console.WriteLine($"{entry.Key} : {entry.Value}");
            }            
        }
    }
}