using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;
//using NAudio.Wave;    
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace MzamoLm
{
    public class Program
    {
        public static string Chatbot; //private string so the loop runs many times
        public static string Username; //user name
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
            //DisplayAsciiArt();

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
                new Interaction() { };
                BasicResponse();                
            }

            BotColor();
            Console.WriteLine("EXIT DETECTED!\nCLOSING APP!");
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
            Console.Write("\nBot: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        public static void UserColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nUser: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow; // Set the rest of the text to orange    
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
            if (File.Exists("GREETING.wav"))
            {
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
            }else{
                //File not found
                Console.WriteLine("GREETING.wav not found!");
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
                
                //array for questions 
                BotColor();
                Chatbot = "You can start asking me a question cybersecurity related!";
                TypeEffect();
                //User inputs question
                UserColor();
                string sUserInput = Console.ReadLine().ToLower();  //lowercase
                if (sUserInput == "exit")
                {
                    bFlag = true;
                    BotColor();
                    Console.WriteLine("EXIT DETECTED!\nCLOSING APP!");
                    break;
                }
                string response = GetResponse(sUserInput);
                BotColor();
                Chatbot = response;
                TypeEffect();
            }
        }
        private static string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "Please enter a valid question.";
            }
            userInput = userInput.ToLower();
            if (userInput == "yes" && Chatbot.Contains("Do you need help?"))
            {
                return DisplayExampleQuestions();
            }
            if (userInput.Contains("password"))
            {
                if (userInput.Contains("strong") || userInput.Contains("create"))
                {
                    return Responses[6]; // Strong password
                }
                return Responses[3]; // General password safety
            }
            if (userInput.Contains("phishing"))
            {
                if (userInput.Contains("email") || userInput.Contains("detect"))
                {
                    return Responses[9]; // Phishing email signs
                }
                return Responses[4]; // General phishing info
            }
            if (userInput.Contains("safe") && userInput.Contains("browsing"))
            {
                return Responses[5]; // Safe browsing
            }
            if (userInput.Contains("scam") || userInput.Contains("avoid"))
            {
                return Responses[7]; // Avoiding scams
            }
            if (userInput.Contains("hacked"))
            {
                return Responses[8]; // If you're hacked
            }
            if (userInput.Contains("wifi") || userInput.Contains("public network"))
            {
                return Responses[10]; // Public Wi-Fi warning
            }
            if (userInput.Contains("2fa") || userInput.Contains("two factor"))
            {
                return Responses[11]; // 2FA explanation
            }
            if (userInput.Contains("update") || userInput.Contains("software"))
            {
                return Responses[12]; // Importance of updates
            }
            if (userInput.Contains("malware"))
            {
                return Responses[13]; // Malware explanation
            }
            if (userInput.Contains("personal information") || userInput.Contains("data privacy"))
            {
                return Responses[14]; // Protecting personal info
            }
            if (userInput.Contains("link"))
            {
                return Responses[15]; // If you click a bad link
            }
            if (userInput.Contains("https") || userInput.Contains("secure site"))
            {
                return Responses[16]; // Secure website tips
            }
            if (userInput.Contains("antivirus"))
            {
                return Responses[17]; // Antivirus usage
            }
            if (userInput.Contains("social media") || userInput.Contains("privacy"))
            {
                return Responses[18]; // Social media tips
            }
            if (userInput.Contains("firewall"))
            {
                return Responses[19]; // Firewall explanation
            }
            for (int k = 0; k < Questions.Length; k++)
            {
                if (Questions[k].ToLower() == userInput)
                {
                    return Responses[k];
                }
            }
            Chatbot = "Do you need help?";
            return "I'm not sure how to respond to that. Try asking about passwords, phishing, or safe browsing!";
        }
        private static string DisplayExampleQuestions()
        {
            string examples = "Here are some example questions you can ask:\n";
            examples += "1. What is password safety?\n";
            examples += "2. What is phishing?\n";
            examples += "3. What is safe browsing?\n";
            examples += "4. How do I avoid scams online?\n";
            examples += "5. What should I do if I get hacked?\n";
            return examples;
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
            Username = sName + " " + sSurname;
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
