using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // green
            Console.WriteLine("This program will have numbers");     
            // Create a list for responses
            List<String> responses = new List<String>() { "num1", "num2", "num3", "num4", "num5"};
            List<String> ranndom_numbers = new List<String>();




            Random find_index = new Random();  

            find_index = new Random(); // Create a new instance of the Random class
            for (int k = 0; k < responses.Count; k++)
            { 
                //get a randomized index
                int random_index = find_index.Next(responses.Count);
                // Print the random index   //2 ways
                System.Console.WriteLine("Response[" + random_index + "]");
                System.Console.WriteLine(responses[random_index]);
            }


            //check a text file. called class1memory.txt
            //If file not found, create a new file
            //If file found, store the generic lists in the file
            //Ask the user for thir favourate food. Store the food in the list (responses) and text file

            //Check !filePath
            string file_path = "class1memory.txt";

            if (!File.Exists(file_path)){
                File.Create(file_path).Close();
            }

            using (StreamWriter writer = new StreamWriter(file_path, true))
            {
                foreach (var response in responses){
                    writer.WriteLine(response);
                }
            }
            //Ask the user for their favorite food
            Console.Write("What's your favorite food? ");
            string favoriteFood = Console.ReadLine();
            responses.Add(favoriteFood);
            //Store favorite food in 
            using (StreamWriter writefile = new StreamWriter(file_path, true))
            {
                writefile.WriteLine("Favorite Food: " + favoriteFood);
            }
            Console.WriteLine("Added '{favoriteFood}' to responses list and saved to file.");

        }
    }
}
