using System;
using System.Collections;
namespace Array_List_HashTable
{
    internal class AttenanceRegister
    {
        internal class AttendanceRegister
        {
            // Declare ArrayList as a class-level field
            private ArrayList studentNamesList;
            public AttendanceRegister()
            {
                // Create ArrayList
                studentNamesList = new ArrayList();
                // Populate studentNamesList
                for (int k = 0; k < 3; k++)
                {
                    Console.Write($"Enter name of the student {k + 1}: ");
                    string studentName = Console.ReadLine();
                    studentNamesList.Add(studentName);
                }
            }
            public void ShowAttendance()
            {
                Console.WriteLine("\nStudent Attendance Register:");
                foreach (string name in studentNamesList)
                {
                    Console.WriteLine(name);
                }
            }
            static void Main(string[] args)
            {
                AttendanceRegister register = new AttendanceRegister();
                register.ShowAttendance();

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}