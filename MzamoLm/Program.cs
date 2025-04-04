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
    public class Program
    {
        public static string Chatbot; //private string so the loop runs many times
        //global array
        public static string[] Questions = {
                                            "\nHow are you?",
                                            "\nWhat's your purpose?",
                                            "\nWhat can I ask you about?",
                                            "\nWhat is password safety?",
                                            "\nWhat is phishing?",
                                            "\nWhat is safe browsing?"
                                            };
        public static string[] Responses = {
                                            "\nI'm just a bot, but I'm here to help you stay safe online!\nI have no feelings whatsoever!",
                                            "\nMy purpose is to educate users about cybersecurity and help them stay safe online!\nALL THIS WHILST LEARNING MORE!",
                                            "\nYou can ask me about the following!\nPassword safety, Phishing, Safe browsing & any other Cybersecurity related topics!",
                                            "\nAlways use strong, unique passwords for each account. Avoid using personal details in your passwords.\nDefinition: Password Safety\nPassword safety is the practice of creating, managing, and protecting strong passwords to prevent unauthorized access to accounts and personal information. It includes using unique, complex passwords, enabling two-factor authentication (2FA), avoiding password reuse, and being cautious of phishing attacks and data breaches. ",
                                            "\nBe cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.\nDefinition :Phishing\nPhishing is a cyberattack where scammers trick people into revealing sensitive information—like passwords, credit card details, or personal data—by pretending to be a trusted entity, usually through fake emails, messages, or websites.",
                                            "\nOnly visit trusted websites and be wary of suspicious links or downloads.\neg: Use links that are secure like this one\nhttps://www.google.com/"
                                            };
        public static string[] UserInputs;//array to store user inputs

        public static Boolean bFlag = false;
        static void Main(string[] args)
        { 
             Chatbot = "hello"; //Activate
            //personalised recorded voice greeting
            VoiceGreeting();
            Thread.Sleep(500); 
            
            //display ASCII 
            DisplayAsciiArt();

            //new instance logo
            new AsciiLogo() { };

            //welcoming and interactive experience.
            TextUserInteractionGreeting();
            
            //Creates an AI feel
            TypeEffect();
            //Shift text from user to Bot
            UserColor();
            BotColor();
            
            //Chatbot strign
            Chatbot = Console.ReadLine();
            while (!bFlag)
            {
                BasicResponse();//Run until out of loop
                ChekcIfExit();
            }
            
        }

        private static void ChekcIfExit()
        {
            //throw new NotImplementedException();
            Chatbot = Console.ReadLine();
            if (Chatbot.Contains("exit"))
            {
                BotColor();
                Console.WriteLine("\nEXTT DETECTED!\nCLOSING APP!");
                bFlag = true;
            }
        }

        public static void BotColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nBot: ");
            
        }
        public static void UserColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nUser:  ");
            
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

        public static void VoiceGreeting()
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
                Console.WriteLine("Error while playing voice greeting.wav " + ex.Message);
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
        public static void BasicResponse()
        {
            //throw new NotImplementedException();
            /*
             * Create responses for questions like "How are you?", "What’s your purpose?", and "What can I ask you about?" 
               Include topics related to password safety, phishing, and safe browsing.
             */
            while (!bFlag)
            {
                ChekcIfExit();
                //array for questions 
                BotColor();
                Chatbot = "You can start asking me a question cybersecurity related!";
                TypeEffect();

                //User inputs question
                UserColor();
                string sUserInput = Console.ReadLine().ToLower();  //lowercase
                ChekcIfExit();

                for (int k = 0; k < Questions.Length; k++)
                {
                    if (Questions[k] == sUserInput)
                    {
                        BotColor();
                        Chatbot = Responses[k];
                        TypeEffect();
                        //Console.WriteLine(Responses[k]);
                        break;
                    }
                    else if (k == Questions.Length - 1)
                    {
                        BotColor();
                        Chatbot = "I'm not sure how to respond to that question!.\nCan you ask me something else related to cybersecurity?\nDo you need help?";
                        TypeEffect();
                    }
                }
                Console.WriteLine();
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
            ChekcIfExit();
            BotColor();
            Entername = "\nEnter your surname:\n";
            //TypeEffect
            Chatbot = Entername;
            TypeEffect();            
            UserColor();
            string sSurname = Console.ReadLine();
            ChekcIfExit();
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
        public static void DisplayAsciiArt()
        {
            // Now do the bitmap image
           
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
