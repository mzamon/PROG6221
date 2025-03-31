using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;
//using NAudio.Wave;
using static System.Net.Mime.MediaTypeNames;

namespace MzamoLm
{
    internal class Program
    {
        public static string Chatbot; //private string so the loop runs many times
        static void Main(string[] args)
        {
            //personalised recorded voice greeting
            VoiceGreeting();
            //display ASCII 
            DisplayAsciiArt();
            //welcoming and interactive experience.
            TextUserInteractionGreeting();
            BasicResponse();
            Chatbot = "hello"; //Activate
            //Creates an AI feel
            TypeEffect();
            //Shift text from user to Bot
            UserColor();
            BotColor();
        }

        public static void BotColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public static void UserColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void TypeEffect()
        {
            //throw new NotImplementedException();
            foreach (char c in Chatbot)
            {
                Console.Write(c);
                Thread.Sleep(50); // type effect
            }
        }

        private static void VoiceGreeting()
        {
            // Attempt to GREETING.wav with spectrum 
            //Spectrum works on higher version of C# /*
            //https://youtu.be/uZlz1SSisYY
            try
            {   //Play first
                PlayMusic("GREETING.wav");
                BotColor();
                Console.WriteLine("Greeting Audio");

                /*//Load GREETING.wav 
                using var audioFile = new AudioFileReader("GREETING.wav");
                object filename = null;
                using var outputDevice = new WaveOutEvent(filename);
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Perform FFT and visualize (pseudo-code)
                while (audioFile.Position < audioFile.Length)
                {
                    var fftData = PerformFFT(audioFile); // Your FFT processing here
                    UpdateSpectrum(fftData);            // Render the spectrum visualization
                    Thread.Sleep(16);                   // ~60 FPS
                }*/
            }
            catch (DllNotFoundException ex)
            {
                Console.WriteLine("WASAPI not available, switching to an alternative: " + ex.Message);
                // Fallback to DirectSound
            }
        }
        public static void PlayMusic(string filepath) {
            SoundPlayer musicPlayer = new SoundPlayer();
            musicPlayer.SoundLocation = filepath;
            musicPlayer.Play();
        }

        /*
        // Placeholder for the FFT processing method
        private static float[] PerformFFT(AudioFileReader audioFile)
        {
            // Implement FFT analysis logic here
            // For now, return a dummy array representing frequency bins
            return new float[256];
        }

        // Placeholder for the spectrum visualization method
        private static void UpdateSpectrum(float[] fftData)
        {
            // Render the spectrum based on FFT data
            // Example: Print the maximum frequency amplitude to console (just for demonstration)
            float maxAmplitude = 0f;
            foreach (var amplitude in fftData)
            {
                if (amplitude > maxAmplitude) maxAmplitude = amplitude;
            }
            Console.WriteLine($"Max Amplitude: {maxAmplitude}");
        }
         
     }*/

        //Used method is private.. change to public.
        private static void BasicResponse()
        {
            //throw new NotImplementedException();
            /*
             * Create responses for questions like "How are you?", "What’s your purpose?", and "What can I ask you about?" 
               Include topics related to password safety, phishing, and safe browsing.
             */

            //array for questions 
            BotColor();
            string[] BasicQuestions = { @"[1]How are you?" 
                                        , "[2]What's your purpose?"
                                        , "[3]What can I ask you about?"
                                        //eol
                                      };
            //TypeEffect()
            foreach (string type in BasicQuestions) { 
                foreach (char c in type)
                {
                    Console.Write(c);
                    Thread.Sleep(50); // type effect
                }
                Console.WriteLine();//new line per question
            }
        }

        static void TextUserInteractionGreeting()
        {
            //add welcome + interactive xp
            BotColor();
            string welcomeMessage = "\n\n================================"
                                    + "\n|        NOW ONLINE!          |"
                                    + "\n================================\n";
            //TypeEffect
            Chatbot = welcomeMessage;
            TypeEffect();
            
            //configure
            Random random = new Random();
            welcomeMessage = "\nloading..." + "\n[    ]" + "\n[=   ]" + "\n[==  ]" + "\n[=== ]" + "\n[====]" + "\n[=====]" + "\n[  ===]" + "\n[   ==]\n" ;
            foreach (char c in welcomeMessage)
            {
                Console.ForegroundColor = (ConsoleColor)random.Next(1, 16); // Random color
                Console.Write(c);
                Thread.Sleep(50); // type effect
            }
            Thread.Sleep(100);           
            BotColor();
            Console.WriteLine("\n\n   ***SUCCESSFULLY LOADED!***\n\n");
            Console.WriteLine("\n================================\n");
            welcomeMessage = "\n👾 Welcome to Mzamo LM, a Cybersecurity Awareness Bot! 🛡️\n";
            //TypeEffect
            Chatbot = welcomeMessage;
            TypeEffect();
            //user enter name
            Console.WriteLine("\n================================\n");
            string Entername = "\nEnter your name:\n";
            //TypeEffect
            Chatbot = Entername;
            TypeEffect();
            UserColor();
            string sName = Console.ReadLine();
            BotColor();
            Entername = "\nEnter your surname:\n";
            //TypeEffect
            Chatbot = Entername;
            TypeEffect();            
            UserColor();
            string sSurname = Console.ReadLine();
            //personalized greeting 
            UserColor();
            //28/03/25
            BotColor();
            Console.WriteLine(    "----------------------------------------"
                                + "\n|                 HI THERE!             |\n"
                                + "\nUSER:    <" + sName + "   " + sSurname 
                                + "\n----------------------------------------\n\n"
                               + "Ask me something !\n\n"
                                );            
        }
        static void DisplayAsciiArt()
        {
            string[] asciiArt = { @"
                              
                                  _____                    _____                    _____                    _____                   _______                           _____            _____          
                                 /\    \                  /\    \                  /\    \                  /\    \                 /::\    \                         /\    \          /\    \         
                                /::\____\                /::\    \                /::\    \                /::\____\               /::::\    \                       /::\____\        /::\____\        
                               /::::|   |                \:::\    \              /::::\    \              /::::|   |              /::::::\    \                     /:::/    /       /::::|   |        
                              /:::::|   |                 \:::\    \            /::::::\    \            /:::::|   |             /::::::::\    \                   /:::/    /       /:::::|   |        
                             /::::::|   |                  \:::\    \          /:::/\:::\    \          /::::::|   |            /:::/~~\:::\    \                 /:::/    /       /::::::|   |        
                            /:::/|::|   |                   \:::\    \        /:::/__\:::\    \        /:::/|::|   |           /:::/    \:::\    \               /:::/    /       /:::/|::|   |        
                           /:::/ |::|   |                    \:::\    \      /::::\   \:::\    \      /:::/ |::|   |          /:::/    / \:::\    \             /:::/    /       /:::/ |::|   |        
                          /:::/  |::|___|______               \:::\    \    /::::::\   \:::\    \    /:::/  |::|___|______   /:::/____/   \:::\____\           /:::/    /       /:::/  |::|___|______  
                         /:::/   |::::::::\    \               \:::\    \  /:::/\:::\   \:::\    \  /:::/   |::::::::\    \ |:::|    |     |:::|    |         /:::/    /       /:::/   |::::::::\    \ 
                        /:::/    |:::::::::\____\_______________\:::\____\/:::/  \:::\   \:::\____\/:::/    |:::::::::\____\|:::|____|     |:::|    |        /:::/____/       /:::/    |:::::::::\____\
                        \::/    / ~~~~~/:::/    /\::::::::::::::::::/    /\::/    \:::\  /:::/    /\::/    / ~~~~~/:::/    / \:::\    \   /:::/    /         \:::\    \       \::/    / ~~~~~/:::/    /
                         \/____/      /:::/    /  \::::::::::::::::/____/  \/____/ \:::\/:::/    /  \/____/      /:::/    /   \:::\    \ /:::/    /           \:::\    \       \/____/      /:::/    / 
                                     /:::/    /    \:::\~~~~\~~~~~~                 \::::::/    /               /:::/    /     \:::\    /:::/    /             \:::\    \                  /:::/    /  
                                    /:::/    /      \:::\    \                       \::::/    /               /:::/    /       \:::\__/:::/    /               \:::\    \                /:::/    /   
                                   /:::/    /        \:::\    \                      /:::/    /               /:::/    /         \::::::::/    /                 \:::\    \              /:::/    /    
                                  /:::/    /          \:::\    \                    /:::/    /               /:::/    /           \::::::/    /                   \:::\    \            /:::/    /     
                                 /:::/    /            \:::\    \                  /:::/    /               /:::/    /             \::::/    /                     \:::\    \          /:::/    /      
                                /:::/    /              \:::\____\                /:::/    /               /:::/    /               \::/____/                       \:::\____\        /:::/    /       
                                \::/    /                \::/    /                \::/    /                \::/    /                 ~~                              \::/    /        \::/    /        
                                 \/____/                  \/____/                  \/____/                  \/____/                                                   \/____/          \/____/         
                                                                                                                                                                               
                        © 2025 MZAMO LM. All Rights Reserved."
            };
            //string Display = Array.AsReadOnly(asciiArt); ;

            /*int Count = 1; /* set at 0
                            * CHANGE THIS VALUE >> FASTER DEBUG TIME
                            */
            foreach (string line in asciiArt)
            {
                foreach (char c in line)
                {
                    Console.Write(c);  
                }
                Console.WriteLine(); // Nextln
            }
            Console.Clear();

            int screenWidth = Console.WindowWidth;
            int iCount = 0; // show ascii 10 times
            bool bLoopBreak = false; //bflag

                /*CODE TO MOVE THE ASCII AROUND
                 * 
                for (int round = 0; round < 2; round++) // 2 right
                {
                    if (LoopBreak == true) break; // loopkill

                    for (int i = 0; i < screenWidth - 10; i++) // Move right
                    {
                        if (LoopBreak == true) break; // loopkill
                        Console.Clear();
                        for (int j = 0; j < asciiArt.Length; j++)
                        {
                            if (LoopBreak == true) break; // loopkill
                            iCount++;
                            Console.SetCursorPosition(i, j);
                            Console.Write(asciiArt[j]);
                            if (iCount == 10)
                            {
                                LoopBreak = true; //ser to true so all other loops may break
                                break; //exit loop
                            }


                        }
                        Thread.Sleep(50); // Control speed of movement
                    }
                */
            // Loop through the ASCII Art for movement
            for (int iRround = 0; iRround < 2; iRround++) //To twice
            {
                if (bLoopBreak) break;// Exit loop if bflag

                for (int i = 0; i < screenWidth - 10; i++) //To right
                {
                    if (bLoopBreak) break; //Exit loop if bflag
                    Console.Clear();

                    // Instantiate display
                    string display = "";
                    Random random = new Random();

                    for (int j = 0; j < asciiArt.Length; j++)
                    {
                        if (bLoopBreak) break; // Exit loop if bflag

                        iCount++;

                        // Random color change per character
                        Console.ForegroundColor = (ConsoleColor)random.Next(1, 16); // Random color
                        display += asciiArt[j]; // char to string

                        // Set cursor position 
                        Console.SetCursorPosition(i, j);
                        Console.Write(asciiArt[j]);

                        if (iCount == 10)
                        {
                            bLoopBreak = true; //bflag
                            break; // Exit the loop
                        }
                    }

                    Thread.Sleep(50); // Control speed
                }
            }

            // Reset the console color back to default after the loop
            Console.ResetColor();
        }
    }


}
