using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
namespace MzamoLm
{
    public class Interaction
    {

        //global variables
        public string user_asking = string.Empty;
        public string sUserInput;
        public Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>();
        public string _previousTopic = "";
        public Dictionary<string, List<string>> userMemory = new Dictionary<string, List<string>>(); // Store multiple user data
        public string _sentiment = "neutral";
        private bool exit;
        private Dictionary<string, List<string>> _keywordResponses = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> _userMemory = new Dictionary<string, List<string>>(); // Corrected type

        public static void ColorBot()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nMzamoLM2000:->");
            
        }
        public static void ColorUser()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\n" + Program.Username + ":->");
            
        }
        public Interaction()
        {
        Program.BotColor();
        Console.WriteLine("Hello there! Please enter your name:");

        Program.UserColor();
        Console.Write(" UserName :->");
        Program.Username = Console.ReadLine();
        LogToFile($"Session started with user: {Program.Username}");


            if (string.IsNullOrWhiteSpace(Program.Username))
        {
            Program.Username = "Mysterious Vistor!";
            Program.BotColor();
            Console.WriteLine("  :-> I will call you Vistor since no name was provided.");
            Console.ResetColor();
        }

        //calling method with questions and answers
        InitializeRepliesAndKeywords();
        ColorBot();
        Console.WriteLine($" Hey!! {Program.Username},Inform me how  will I assist you today.?");

        bool bFlag = false;

        while (!bFlag)
        {
            ColorBot();
            Console.Write($"{Program.Username}:->");
            Console.ResetColor();
            sUserInput = Console.ReadLine();
            LogToFile($"{Program.Username}: {sUserInput}");

            // Sentiment Detection
            DetectSentiment(sUserInput);

            if (sUserInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                exit = true;
                ColorBot();               
                Console.ResetColor();
                Console.WriteLine("Thanks for using MzamoLM2000,Please be alert when using web pages!");
                LogToFile("Session ended.");
                break;
            }
            else if (sUserInput.Equals("how are you", StringComparison.OrdinalIgnoreCase))
            {
                ColorBot();
                Console.WriteLine("I'm just a talkingbot,but I can help you with anything you need!");
                ColorUser();
                sUserInput = Console.ReadLine();
            }
            else if (sUserInput.Equals("what are the things that I can ask about", StringComparison.OrdinalIgnoreCase))
            {
                ColorBot();
                Console.WriteLine($"plaese ask me about about phishing password,how to stay safe online,emails");
                ColorUser();
                sUserInput = Console.ReadLine();
            }

            ProcessUserInput(sUserInput);
        }
    }//end of constructor

        private void InitializeRepliesAndKeywords()
        {
            // Use Dictionary for better organization and to handle multiple responses per keyword  
            _keywordResponses.Add("password", new List<string>
            {
                "A strong password should be long, unique, and include a mix of uppercase and lowercase letters, numbers, and symbols.",
                "Use different passwords for every account and consider a password manager.",
                "Avoid common words or personal info in your password. Think passphrases!"
            });

            _keywordResponses.Add("phishing", new List<string>
            {
                "Phishing is when someone tries to trick you into giving away sensitive info using fake emails or sites.",
                "Look out for urgent language, poor spelling, and unknown links in emails.",
                "Don't trust links or attachments unless you're sure they're safe.",
                "Hover over links to check their real destination before clicking."
            });

            _keywordResponses.Add("scam", _keywordResponses["phishing"]);

            _keywordResponses.Add("telephone", new List<string>
            {
                "Phone scams, or vishing, involve calls pretending to be banks or companies to steal data.",
                "Never share passwords or OTPs over the phone — even if they say they're from your bank."
            });

            _keywordResponses.Add("browsing", new List<string>
            {
                "Stick to websites with HTTPS and a padlock symbol in the URL.",
                "Avoid clicking popups or ads from sketchy sources.",
                "Use privacy-focused browsers or enable safe search."
            });

            _keywordResponses.Add("malware", new List<string>
            {
                "Malware includes viruses, trojans, and ransomware — avoid unknown downloads.",
                "Always scan USB devices or files from strangers.",
                "Keep your antivirus updated to detect and block threats."
            });

            _keywordResponses.Add("objective", new List<string>
            {
                "My goal is to help you understand cybersecurity and stay safe online.",
                "I provide awareness on threats, safety tips, and secure practices."
            });

            _keywordResponses.Add("attachment", new List<string>
            {
                "Only open attachments from verified senders — malware often hides in them.",
                "If unsure, call the sender to verify before opening any file.",
                "Never download files ending in .exe, .bat, or .zip from unknown sources."
            });

            _keywordResponses.Add("two-factor authentication", new List<string>
            {
                "2FA requires both a password and a second code — this stops 99% of attacks.",
                "Use an authenticator app like Google Authenticator or Microsoft Authenticator for 2FA.",
                "2FA is a must for banking, email, and social media."
            });

            _keywordResponses.Add("2fa", _keywordResponses["two-factor authentication"]);

            _keywordResponses.Add("public wi-fi", new List<string>
            {
                "Avoid logging into banking or sensitive accounts on public Wi-Fi.",
                "Use a VPN to encrypt your data on public networks.",
                "Disable auto-connect to unknown Wi-Fi networks."
            });

            _keywordResponses.Add("software update", new List<string>
            {
                "Updates often patch security holes hackers can exploit.",
                "Turn on auto-update for your OS and antivirus software.",
                "Outdated apps can be entry points for malware."
            });

            _keywordResponses.Add("privacy", new List<string>
            {
                "Limit what you share online, especially location and personal details.",
                "Review app permissions and social media privacy settings often.",
                "Use aliases or nicknames instead of your real name online."
            });

            _keywordResponses.Add("firewall", new List<string>
            {
                "A firewall monitors and controls traffic to keep intruders out.",
                "Use both a software firewall and a router firewall if possible.",
                "Never disable your firewall unless you understand the risks."
            });

            _keywordResponses.Add("vpn", new List<string>
            {
                "A VPN encrypts your internet traffic and hides your IP address.",
                "Use VPNs on public Wi-Fi or when browsing privately.",
                "Free VPNs may sell your data — choose trusted providers."
            });

            _keywordResponses.Add("ransomware", new List<string>
            {
                "Ransomware locks your files and demands payment — often in crypto.",
                "Never pay the ransom — report the attack and restore from backup.",
                "Keep backups offline to protect from ransomware attacks."
            });

            _keywordResponses.Add("social media", new List<string>
            {
                "Don’t overshare — what you post can be used in identity theft.",
                "Turn on 2FA and set your profile to private.",
                "Think twice before accepting friend requests from strangers."
            });

            _keywordResponses.Add("identity theft", new List<string>
            {
                "Identity theft is when someone steals your info to impersonate you.",
                "Protect your ID number, bank info, and login credentials.",
                "Report suspicious account changes or credit alerts immediately."
            });

            _keywordResponses.Add("data breach", new List<string>
            {
                "A data breach is when a company’s database is hacked and your info leaks.",
                "Use haveibeenpwned.com to check if your data was compromised.",
                "Change your password immediately if you're affected by a breach."
            });

            _keywordResponses.Add("email security", new List<string>
            {
                "Avoid clicking links in emails unless you're 100% sure they're safe.",
                "Never respond to unexpected password reset emails.",
                "Use spam filters and report suspicious messages."
            });
        }

    private void ProcessUserInput(string input)
    {
        string lowerInput = input.ToLower();
        bool found = false;
        string response = "";
        string matchedKeyword = ""; // To store the matched keyword

        // Use Regex for more robust keyword matching
        foreach (var keyword in _keywordResponses.Keys)
        {
            string pattern = $@"(?:\b{Regex.Escape(keyword)}\b|\b{Regex.Escape(keyword)}s\b)";
            if (Regex.IsMatch(lowerInput, pattern))
            {
                response = GetRandomResponse(keyword);
                found = true;
                _previousTopic = keyword;
                matchedKeyword = keyword; // Store the keyword
                break;
            }
        }

        if (found)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Talkingbot:->");
            Console.ResetColor();
            Console.Write($" {response}");
            LogToFile($"Bot: {response}");

            // Memory and Recall
            RememberUserInterest(matchedKeyword); // Remember the user's interest
            DisplayKnownUserInterests();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Talkingbot:->");
            Console.ResetColor();
            Console.WriteLine("Do you still need more help from me!?");
        }
        else
        {
            // Error Handling and Edge Cases + Conversation Flow
            HandleUnrecognizedInput(input);
        }
    }

    private string GetRandomResponse(string keyword)
    {
        List<string> responses = _keywordResponses[keyword];
        if (responses.Count > 0)
        {
            Random random = new Random();
            int index = random.Next(responses.Count);
            return responses[index];
        }
        return "I don't have a response for that.";
    }//randomres

    private void HandleUnrecognizedInput(string input)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Talkingbot:->");
        Console.ResetColor();

        if (_previousTopic != "" && input.ToLower().Contains("more"))
        {
            Console.WriteLine($"I can provide more information on {_previousTopic}.");
            Console.WriteLine(GetAdditionalInfo(_previousTopic));
        }
        else
        {
            Console.WriteLine("I didn't quite understand that. Could you rephrase your question or ask about phishing, passwords, online safety, or emails?");
            LogToFile("Bot: Unrecognized input handled.");
        }

    }

    private string GetAdditionalInfo(string topic)
    {
            switch (topic.ToLower())
            {
                case "password":
                    return "Additional tip: Use a password manager and enable 2FA on important accounts.";
                case "phishing":
                    return "Always check the sender's email address carefully and avoid clicking unknown links.";
                case "privacy":
                    return "Avoid oversharing online and use privacy settings on apps and social media.";
                case "scam":
                    return "Scams can come via email, calls, or SMS. If it sounds too good to be true, it probably is.";
                case "telephone":
                    return "Hang up on unsolicited calls asking for personal or banking information.";
                case "browsing":
                    return "Consider using browser extensions that block trackers and malicious sites.";
                case "malware":
                    return "Avoid pirated software — it often contains malware.";
                case "objective":
                    return "My main purpose is to keep you informed and safe from online threats.";
                case "attachment":
                    return "Double-check file extensions and never open .exe or .zip files from unknown sources.";
                case "two-factor authentication":
                case "2fa":
                    return "Authenticator apps are safer than SMS-based 2FA. Avoid using the same 2FA method on all services.";
                case "public wi-fi":
                    return "Always turn off file sharing when on public Wi-Fi. Use a VPN for added security.";
                case "software update":
                    return "Enable automatic updates to stay protected without having to think about it.";
                case "firewall":
                    return "Firewalls act as a digital gatekeeper. Keep yours enabled and properly configured.";
                case "vpn":
                    return "VPNs are most useful when traveling or accessing sensitive services on unsecured networks.";
                case "ransomware":
                    return "Backup your data regularly so you won’t lose files in a ransomware attack.";
                case "social media":
                    return "Don’t post travel plans or sensitive data — cybercriminals monitor public profiles.";
                case "identity theft":
                    return "Shred sensitive documents and monitor your credit reports for unauthorized activity.";
                case "data breach":
                    return "If you're in a breach, change passwords and enable 2FA immediately.";
                case "email security":
                    return "Be cautious with links and don't download unexpected attachments. Use strong spam filters.";
                default:
                    return "I do not have additional information on that topic yet.";
            }//case
        }

    private void DetectSentiment(string input)
    {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("worried"))
            {
                _sentiment = "worried";
                Console.WriteLine("It's understandable to feel worried. I'm here to help you.");
                LogToFile("Bot detected sentiment: worried");
            }
            else if (lowerInput.Contains("curious"))
            {
                _sentiment = "curious";
                Console.WriteLine("That's great! I can provide you with some information.");
                LogToFile("Bot detected sentiment: curious");
            }
            else if (lowerInput.Contains("frustrated"))
            {
                _sentiment = "frustrated";
                Console.WriteLine("I'm sorry you're feeling frustrated. I'll do my best to assist you.");
                LogToFile("Bot detected sentiment: frustrated");
            }
            else if (lowerInput.Contains("angry"))
            {
                _sentiment = "angry";
                Console.WriteLine("I'm sensing frustration or anger. Let's find a solution together.");
                LogToFile("Bot detected sentiment: angry");
            }
            else if (lowerInput.Contains("happy") || lowerInput.Contains("glad"))
            {
                _sentiment = "happy";
                Console.WriteLine("I'm glad to hear that you're feeling happy! Let’s keep the good vibes going.");
                LogToFile("Bot detected sentiment: happy");
            }
            else if (lowerInput.Contains("sad") || lowerInput.Contains("depressed"))
            {
                _sentiment = "sad";
                Console.WriteLine("I'm sorry you're feeling down. Remember, you're not alone — stay safe and supported.");
                LogToFile("Bot detected sentiment: sad");
            }
            else if (lowerInput.Contains("confused") || lowerInput.Contains("lost"))
            {
                _sentiment = "confused";
                Console.WriteLine("No worries — I’m here to help you make sense of it all.");
                LogToFile("Bot detected sentiment: confused");
            }
            else if (lowerInput.Contains("excited") || lowerInput.Contains("awesome"))
            {
                _sentiment = "excited";
                Console.WriteLine("I’m thrilled to hear that! Let’s dive into something interesting.");
                LogToFile("Bot detected sentiment: excited");
            }
            else
            {
                _sentiment = "neutral";
                LogToFile("Bot detected no clear sentiment.");
            }//sentiments
        }

        private void RememberUserInterest(string topic)
        {
            if (!_userMemory.ContainsKey("interests")) // Fixed CS1061 by ensuring _userMemory is of type Dictionary<string, List<string>>
            {
                _userMemory.Add("interests", new List<string>());
            }
            List<string> interests = _userMemory["interests"];
            if (!interests.Contains(topic))
            {
                interests.Add(topic);
                Console.WriteLine($"\nTalkingbot:-> I'll remember that you're interested in {topic}.");
            }
        }
        

        private void DisplayKnownUserInterests()
        {
            if (_userMemory.ContainsKey("interests")) 
            {
                Console.Write("Talkingbot:-> I know you are interested in: ");
                List<string> interests = _userMemory["interests"];
                Console.WriteLine(string.Join(", ", interests));
            }
        }
        private void LogToFile(string Text)
        {
            try
            {
                string folderPath = AppDomain.CurrentDomain.BaseDirectory;
                string filename = Program.Username + ".txt";
                string fullPath = Path.Combine(folderPath, filename);

                using (StreamWriter writer = new StreamWriter(fullPath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {Text}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logging error: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}