using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace CyberSecurityAwarenessAssistant
{
    public class interaction
    {
        private string _userName = string.Empty;
        private string _userInput;
        private string _previousTopic = string.Empty;
        private string _sentiment = "neutral";
        private Dictionary<string, List<string>> _keywordResponses = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> _userMemory = new Dictionary<string, List<string>>();

        public interaction()
        {
            new MzamoLm.AsciiLogo();
            DisplayWelcome();
            GetUserName();
            InitializeReplies();
            StartConversation();
        }

        private void DisplayWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                      Hello! Welcome To Talkingbot_AI");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.ResetColor();
        }

        private void GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Talkingbot → ");
            Console.ResetColor();
            Console.WriteLine("Hello there! What should I call you?");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("User → ");
            Console.ResetColor();
            _userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(_userName))
            {
                _userName = "Visitor";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Talkingbot → I’ll call you Visitor since no name was provided.");
                Console.ResetColor();
            }
        }

        private void InitializeReplies()
        {
            _keywordResponses.Add("password", new List<string>
            {
                "Use strong, unique passwords with letters, numbers, and symbols. Avoid personal info.",
                "Consider using a password manager to help you stay secure.",
                "Change your passwords regularly to reduce risk."
            });
            _keywordResponses.Add("phishing", new List<string>
            {
                "Beware of emails asking for sensitive info—verify before clicking.",
                "Phishing often uses urgency to trick you. Always check the sender.",
                "Don’t download attachments or click unknown links without checking."
            });
            _keywordResponses.Add("browsing", new List<string>
            {
                "Use HTTPS websites and avoid suspicious popups or downloads.",
                "Use a secure browser and enable safe search filters."
            });
            _keywordResponses.Add("malware", new List<string>
            {
                "Install antivirus software and scan regularly.",
                "Avoid downloading files from untrusted sources to prevent malware."
            });
            _keywordResponses.Add("2fa", new List<string>
            {
                "Two-Factor Authentication helps protect your account even if your password is compromised.",
                "Enable 2FA for your email, banking, and social media accounts."
            });
            _keywordResponses.Add("privacy", new List<string>
            {
                "Review privacy settings on all accounts regularly.",
                "Think twice before sharing personal details online."
            });
        }

        private void StartConversation()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Talkingbot → ");
            Console.ResetColor();
            Console.WriteLine($"Hi {_userName}, how can I help you today?");

            bool exit = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{_userName} → ");
                Console.ResetColor();
                _userInput = Console.ReadLine();

                DetectSentiment(_userInput);

                if (string.IsNullOrWhiteSpace(_userInput)) continue;
                if (_userInput.ToLower() == "exit")
                {
                    exit = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Talkingbot → Stay safe online. Goodbye!");
                    break;
                }

                ProcessInput(_userInput);
            }
        }

        private void ProcessInput(string input)
        {
            string lowerInput = input.ToLower();
            bool found = false;

            foreach (var keyword in _keywordResponses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    string response = GetRandomResponse(keyword);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Talkingbot → " + response);
                    _previousTopic = keyword;
                    RememberInterest(keyword);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                if (!string.IsNullOrEmpty(_previousTopic) && lowerInput.Contains("more"))
                {
                    Console.WriteLine("Talkingbot → Here's something more about " + _previousTopic);
                    Console.WriteLine(GetAdditionalInfo(_previousTopic));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Talkingbot → I'm not sure I understand. Try asking about passwords, phishing, or browsing safety.");
                }
            }

            DisplayInterests();
        }

        private string GetRandomResponse(string keyword)
        {
            List<string> responses = _keywordResponses[keyword];
            Random rand = new Random();
            return responses[rand.Next(responses.Count)];
        }

        private string GetAdditionalInfo(string topic)
        {
            if (topic == "password") return "Tip: Use a password manager and never reuse passwords.";
            if (topic == "phishing") return "Tip: Hover over links to verify before clicking.";
            if (topic == "privacy") return "Tip: Always check app permissions and limit data sharing.";
            return "I don't have more on that topic right now.";
        }

        private void DetectSentiment(string input)
        {
            string lower = input.ToLower();
            if (lower.Contains("worried")) Console.WriteLine("Talkingbot → It's okay to feel worried. I'm here to help.");
            else if (lower.Contains("curious")) Console.WriteLine("Talkingbot → Great! Let’s explore that together.");
            else if (lower.Contains("frustrated")) Console.WriteLine("Talkingbot → I’m sorry you're frustrated. Let’s fix that.");
        }

        private void RememberInterest(string topic)
        {
            if (!_userMemory.ContainsKey("interests"))
                _userMemory["interests"] = new List<string>();

            if (!_userMemory["interests"].Contains(topic))
            {
                _userMemory["interests"].Add(topic);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Talkingbot → I’ll remember that you’re interested in " + topic);
            }
        }

        private void DisplayInterests()
        {
            if (_userMemory.ContainsKey("interests"))
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Talkingbot → So far you’re interested in: " + string.Join(", ", _userMemory["interests"]));
            }
        }
    }
}