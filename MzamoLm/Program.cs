﻿using System;
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
                                                "\nWhat is safe browsing?",
                                                "\nWhat are strong passwords?",
                                                "\nHow do I avoid scams online?",
                                                "\nWhat should I do if I get hacked?",
                                                "\nHow do I recognize a phishing email?",
                                                "\nIs public Wi-Fi safe?",
                                                "\nWhat is two-factor authentication?",
                                                "\nWhy should I update my software?",
                                                "\nWhat is malware?",
                                                "\nHow do I protect my personal information?",
                                                "\nWhat should I do if I click a suspicious link?",
                                                "\nWhat are secure websites?",
                                                "\nWhy is antivirus important?",
                                                "\nCan social media be risky?",
                                                "\nWhat is a firewall?"
                                            };
        public static string[] Responses = {
                                                "\nI'm just a bot, but I'm here to help you stay safe online!\nI have no feelings whatsoever!",
                                                "\nMy purpose is to educate users about cybersecurity and help them stay safe online!\nALL THIS WHILST LEARNING MORE!",
                                                "\nYou can ask me about the following!\nPassword safety, Phishing, Safe browsing & any other Cybersecurity related topics!",
                                                "\nAlways use strong, unique passwords for each account. Avoid using personal details in your passwords.\nDefinition: Password Safety\nPassword safety is the practice of creating, managing, and protecting strong passwords to prevent unauthorized access to accounts and personal information. It includes using unique, complex passwords, enabling two-factor authentication (2FA), avoiding password reuse, and being cautious of phishing attacks and data breaches.",
                                                "\nBe cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.\nDefinition :Phishing\nPhishing is a cyberattack where scammers trick people into revealing sensitive information—like passwords, credit card details, or personal data—by pretending to be a trusted entity, usually through fake emails, messages, or websites.",
                                                "\nOnly visit trusted websites and be wary of suspicious links or downloads.\neg: Use links that are secure like this one\nhttps://www.google.com/",
                                                "\nA strong password is at least 12 characters long, uses a mix of letters, numbers, and symbols, and avoids dictionary words or patterns.\nTry something like: G3k#82!r9Lp%",
                                                "\nAvoid scams by not clicking on suspicious links, verifying sources before sharing information, and never sending personal details through unknown emails or messages.",
                                                "\nIf you suspect you've been hacked, change your passwords immediately, enable 2FA, run antivirus software, and inform your contacts.",
                                                "\nPhishing emails often create urgency, contain grammatical errors, and ask you to click suspicious links or share sensitive info. Always verify the sender!",
                                                "\nPublic Wi-Fi can be risky. Avoid logging into sensitive accounts or making purchases on public networks unless you're using a VPN.",
                                                "\nTwo-Factor Authentication (2FA) adds an extra layer of security by requiring something you know (like a password) and something you have (like a phone).",
                                                "\nSoftware updates fix security vulnerabilities. Always keep your system, apps, and antivirus software up to date to stay protected.",
                                                "\nMalware is any malicious software designed to harm, exploit, or damage a system. It includes viruses, worms, trojans, spyware, and ransomware.",
                                                "\nProtect personal info by using strong passwords, limiting what you share online, and reviewing app and website permissions.",
                                                "\nIf you click a suspicious link, disconnect from the internet, run a virus scan, and change passwords. Don’t enter any personal info if prompted!",
                                                "\nSecure websites start with 'https://' and show a padlock icon in the address bar. These encrypt your data for safer browsing.",
                                                "\nAntivirus software detects and removes malicious software. It’s a must-have defense tool against many forms of cyber threats.",
                                                "\nSocial media can expose personal info. Adjust privacy settings, think before you post, and don’t accept friend requests from strangers.",
                                                "\nA firewall is a security system that monitors and controls incoming and outgoing network traffic based on predetermined security rules. It acts as a barrier between your device and potential threats."
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
            /*//Shift text from user to Bot
            UserColor();
            BotColor();
            */
            //Chatbot strign
            Chatbot = Console.ReadLine();
            while (!bFlag)
            {
                if (Chatbot.Contains("exit"))
                {
                    BotColor();
                    Console.WriteLine("EXIT DETECTED!\nCLOSING APP!");
                    bFlag = true;
                }
                else
                {
                    //Run until out of loop
                    BotColor();
                    Chatbot = "You can start asking me a question cybersecurity related!";
                    TypeEffect();
                }
                BasicResponse();//Run until out of loop               
            }
            
        }
        private static void ChekcIfExit()
        {
            //throw new NotImplementedException();
            Chatbot = Console.ReadLine();
            if (Chatbot.Contains("exit"))
            {
                BotColor();
                Console.WriteLine("EXTT DETECTED!\nCLOSING APP!");
                bFlag = true;
            }
        }
        public static void BotColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bot: ");
            
        }
        public static void UserColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("User: ");
            
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
            //https://youtu.be/uZlz1SSisYY
            try
            {   //Play first
                PlayMusic("GREETING.wav");
                BotColor();
                Console.WriteLine("Welcome audio is playing");
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
        //Used method is private.. change to public.
        public static void BasicResponse()
        {
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
                    //run nested if for basic one word responses
                    if (sUserInput != null)
                    {
                        sUserInput = sUserInput.ToLower();

                        if (sUserInput.Contains("password"))
                        {
                            if (sUserInput.Contains("strong") || sUserInput.Contains("create"))
                            {
                                Console.WriteLine(Responses[6]); // Strong password
                            }
                            else
                            {
                                Console.WriteLine(Responses[3]); // General password safety
                            }
                        }
                        else if (sUserInput.Contains("phishing"))
                        {
                            if (sUserInput.Contains("email") || sUserInput.Contains("detect"))
                            {
                                Console.WriteLine(Responses[9]); // Phishing email signs
                            }
                            else
                            {
                                Console.WriteLine(Responses[4]); // General phishing info
                            }
                        }
                        else if (sUserInput.Contains("safe") && sUserInput.Contains("browsing"))
                        {
                            Console.WriteLine(Responses[5]); // Safe browsing
                        }
                        else if (sUserInput.Contains("scam") || sUserInput.Contains("avoid"))
                        {
                            Console.WriteLine(Responses[7]); // Avoiding scams
                        }
                        else if (sUserInput.Contains("hacked"))
                        {
                            Console.WriteLine(Responses[8]); // If you're hacked
                        }
                        else if (sUserInput.Contains("wifi") || sUserInput.Contains("public network"))
                        {
                            Console.WriteLine(Responses[10]); // Public Wi-Fi warning
                        }
                        else if (sUserInput.Contains("2fa") || sUserInput.Contains("two factor"))
                        {
                            Console.WriteLine(Responses[11]); // 2FA explanation
                        }
                        else if (sUserInput.Contains("update") || sUserInput.Contains("software"))
                        {
                            Console.WriteLine(Responses[12]); // Importance of updates
                        }
                        else if (sUserInput.Contains("malware"))
                        {
                            Console.WriteLine(Responses[13]); // Malware explanation
                        }
                        else if (sUserInput.Contains("personal information") || sUserInput.Contains("data privacy"))
                        {
                            Console.WriteLine(Responses[14]); // Protecting personal info
                        }
                        else if (sUserInput.Contains("link"))
                        {
                            Console.WriteLine(Responses[15]); // If you click a bad link
                        }
                        else if (sUserInput.Contains("https") || sUserInput.Contains("secure site"))
                        {
                            Console.WriteLine(Responses[16]); // Secure website tips
                        }
                        else if (sUserInput.Contains("antivirus"))
                        {
                            Console.WriteLine(Responses[17]); // Antivirus usage
                        }
                        else if (sUserInput.Contains("social media") || sUserInput.Contains("privacy"))
                        {
                            Console.WriteLine(Responses[18]); // Social media tips
                        }
                        else if (sUserInput.Contains("firewall"))
                        {
                            Console.WriteLine(Responses[19]); // Firewall explanation
                        }
                        else
                        {
                            Console.WriteLine("I'm not sure how to respond to that. Try asking about passwords, phishing, or safe browsing!");
                        }
                    }

                    if (Questions[k] == sUserInput)
                    {
                        ChekcIfExit();
                        BotColor();
                        Chatbot = Responses[k];
                        TypeEffect();
                        break;
                    }
                    else if (k == Questions.Length - 1)
                    {
                        BotColor();
                        Chatbot = "I'm not sure how to respond to that question!.\nCan you ask me something else related to cybersecurity?\nDo you need help?";
                        TypeEffect();
                        ChekcIfExit();
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
