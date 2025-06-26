using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CHAT2
{
    public enum IntentType
    {
        Greeting,
        Farewell,
        Question,
        AddTask,
        ViewTasks,
        CompleteTask,
        DeleteTask,
        SetReminder,
        StartQuiz,
        Unknown,
        ViewActivityLog,
        Help
    }

    public class UserInput
    {
        public string RawText { get; set; }
        public IntentType Intent { get; set; }
        public Dictionary<string, string> Entities { get; set; }
        public string SentimentType { get; set; }

        public UserInput(string text)
        {
            RawText = text;
            Entities = new Dictionary<string, string>();
            SentimentType = "neutral";
        }
    }

    public class NLPProcessor
    {
        private readonly Dictionary<string, List<string>> intentPatterns;
        private readonly Dictionary<string, List<string>> sentimentPatterns;

        public NLPProcessor()
        {
            // Initialize intent patterns for keyword detection
            intentPatterns = new Dictionary<string, List<string>>
            {
                [nameof(IntentType.Greeting)] = new List<string> { "hello", "hi", "hey", "good morning", "good afternoon", "good evening", "greetings" },

                [nameof(IntentType.Farewell)] = new List<string> { "bye", "goodbye", "see you", "exit", "quit" },

                [nameof(IntentType.Question)] = new List<string> { "what", "how", "why", "when", "where", "who", "can you", "could you", "tell me about", "explain" },

                [nameof(IntentType.AddTask)] = new List<string> { "add task", "create task", "new task", "make task", "add reminder", "create reminder", "remind me to", "add a new task" },

                [nameof(IntentType.ViewTasks)] = new List<string> { "show tasks", "view tasks", "list tasks", "my tasks", "show reminders", "view reminders", "what are my tasks", "what tasks" },

                [nameof(IntentType.CompleteTask)] = new List<string> { "complete task", "finish task", "mark task", "done task", "mark as complete", "task complete", "task done" },

                [nameof(IntentType.DeleteTask)] = new List<string> { "delete task", "remove task", "cancel task", "delete reminder", "remove reminder" },

                [nameof(IntentType.SetReminder)] = new List<string> { "set reminder", "remind me", "in a week", "tomorrow", "next week", "in a day", "in a month" },

                [nameof(IntentType.StartQuiz)] = new List<string> { "start quiz", "begin quiz", "play quiz", "take quiz", "test knowledge", "cybersecurity quiz" },

                [nameof(IntentType.ViewActivityLog)] = new List<string> { "show log", "view log", "activity log", "what have you done", "show history", "recent activity" },

                [nameof(IntentType.Help)] = new List<string> { "help", "assist", "commands", "what can you do", "features", "instructions", "guide" }
            };

            // Initialize sentiment patterns
            sentimentPatterns = new Dictionary<string, List<string>>
            {
                ["positive"] = new List<string> { "happy", "glad", "excited", "interested", "great", "good", "excellent", "amazing", "awesome", "wonderful" },

                ["negative"] = new List<string> { "worried", "concerned", "scared", "frustrated", "confused", "sad", "angry", "annoyed", "bad", "terrible", "afraid" },

                ["neutral"] = new List<string> { "okay", "fine", "alright", "neutral", "average" }
            };
        }

        public UserInput ProcessInput(string input)
        {
            string normalizedInput = input.ToLower().Trim();
            UserInput userInput = new UserInput(normalizedInput);

            // Detect intent
            userInput.Intent = DetectIntent(normalizedInput);

            // Extract entities based on intent
            ExtractEntities(userInput);

            // Detect sentiment
            userInput.SentimentType = DetectSentiment(normalizedInput);

            return userInput;
        }

        private IntentType DetectIntent(string input)
        {
            // Find the intent with the highest matching pattern
            foreach (var intentEntry in intentPatterns)
            {
                foreach (string pattern in intentEntry.Value)
                {
                    if (input.Contains(pattern))
                    {
                        // Parse the intent type from the dictionary key
                        if (Enum.TryParse<IntentType>(intentEntry.Key, out var intent))
                        {
                            return intent;
                        }
                    }
                }
            }

            return IntentType.Unknown;
        }

        private void ExtractEntities(UserInput userInput)
        {
            string input = userInput.RawText;

            // Task title extraction
            if (userInput.Intent == IntentType.AddTask || userInput.Intent == IntentType.SetReminder)
            {
                foreach (var pattern in intentPatterns[nameof(IntentType.AddTask)])
                {
                    if (input.Contains(pattern))
                    {
                        string taskTitle = input.Substring(input.IndexOf(pattern) + pattern.Length).Trim();

                        // Clean up the task title
                        taskTitle = Regex.Replace(taskTitle, "^[\\s,.:;-]+", "");

                        if (!string.IsNullOrEmpty(taskTitle))
                        {
                            userInput.Entities["task_title"] = taskTitle;
                            break;
                        }
                    }
                }
            }

            // Date/time extraction for reminders
            if (userInput.Intent == IntentType.SetReminder ||
                (userInput.Intent == IntentType.AddTask && input.Contains("remind")))
            {
                ExtractDateTimeEntity(userInput);
            }
        }

        private void ExtractDateTimeEntity(UserInput userInput)
        {
            string input = userInput.RawText.ToLower();
            DateTime now = DateTime.Now;

            // Look for common time frames
            if (input.Contains("tomorrow"))
            {
                userInput.Entities["reminder_date"] = now.AddDays(1).ToString("yyyy-MM-dd");
            }
            else if (input.Contains("next week") || input.Contains("in a week") || input.Contains("in 1 week"))
            {
                userInput.Entities["reminder_date"] = now.AddDays(7).ToString("yyyy-MM-dd");
            }
            else if (Regex.IsMatch(input, "in [0-9]+ days?"))
            {
                Match match = Regex.Match(input, "in ([0-9]+) days?");
                if (match.Success && int.TryParse(match.Groups[1].Value, out int days))
                {
                    userInput.Entities["reminder_date"] = now.AddDays(days).ToString("yyyy-MM-dd");
                }
            }
            else if (input.Contains("next month") || input.Contains("in a month") || input.Contains("in 1 month"))
            {
                userInput.Entities["reminder_date"] = now.AddMonths(1).ToString("yyyy-MM-dd");
            }
            else if (Regex.IsMatch(input, "in [0-9]+ months?"))
            {
                Match match = Regex.Match(input, "in ([0-9]+) months?");
                if (match.Success && int.TryParse(match.Groups[1].Value, out int months))
                {
                    userInput.Entities["reminder_date"] = now.AddMonths(months).ToString("yyyy-MM-dd");
                }
            }
        }

        private string DetectSentiment(string input)
        {
            foreach (var sentiment in sentimentPatterns)
            {
                foreach (string pattern in sentiment.Value)
                {
                    if (input.Contains(pattern))
                    {
                        return sentiment.Key;
                    }
                }
            }

            return "neutral"; // Default sentiment
        }

        public string GenerateResponse(UserInput userInput, string userName)
        {
            switch (userInput.Intent)
            {
                case IntentType.Greeting:
                    return $"Hello {userName}! How can I help you with cybersecurity today?";

                case IntentType.Farewell:
                    return $"Goodbye {userName}! Stay safe online.";

                case IntentType.Help:
                    return "I can help you with cybersecurity awareness. You can: \n" +
                           "- Ask about cybersecurity topics\n" +
                           "- Add tasks (e.g., 'Add task to update my passwords')\n" +
                           "- Set reminders (e.g., 'Remind me to check my privacy settings tomorrow')\n" +
                           "- Take a cybersecurity quiz (type 'Start quiz')\n" +
                           "- View your activity log (type 'Show activity log')";

                case IntentType.Unknown:
                    return GetRandomResponse(new List<string>
                    {
                        "I'm not sure I understand. Could you rephrase that?",
                        "I didn't catch that. Can you try again?",
                        "I'm still learning. Could you say that differently?",
                        $"Sorry {userName}, I didn't understand. Try asking for 'help' to see what I can do."
                    });

                case IntentType.Question:
                    if (userInput.RawText.Contains("password"))
                        return GetPasswordSecurityTip();
                    else if (userInput.RawText.Contains("phish"))
                        return GetPhishingSecurityTip();
                    else if (userInput.RawText.Contains("privacy"))
                        return GetPrivacySecurityTip();
                    else
                        return GetGeneralSecurityTip();

                default:
                    // For other intents like AddTask, ViewTasks, etc., the main application will handle them
                    // This method will only return a confirmation message
                    return string.Empty;
            }
        }

        private string GetRandomResponse(List<string> responses)
        {
            Random random = new Random();
            int index = random.Next(responses.Count);
            return responses[index];
        }

        private string GetPasswordSecurityTip()
        {
            return GetRandomResponse(new List<string>
            {
                "Use strong, unique passwords for each account. Consider using a password manager.",
                "A good password is at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols.",
                "Change your critical passwords every 3-6 months for better security.",
                "Never share your passwords with others or store them in plain text."
            });
        }

        private string GetPhishingSecurityTip()
        {
            return GetRandomResponse(new List<string>
            {
                "Be cautious of emails asking for personal information. Legitimate organizations won't ask for sensitive information via email.",
                "Check email sender addresses carefully. Phishers often use addresses that look similar to legitimate ones.",
                "Don't click on suspicious links. Hover over them first to see where they actually lead.",
                "If an offer seems too good to be true, it probably is. Be skeptical of unsolicited amazing deals."
            });
        }

        private string GetPrivacySecurityTip()
        {
            return GetRandomResponse(new List<string>
            {
                "Regularly review privacy settings on your social media and online accounts.",
                "Be mindful of what personal information you share online. Once it's out there, it's hard to take back.",
                "Use private browsing or VPN when on public networks to protect your data.",
                "Consider using privacy-focused search engines and browsers that don't track your activity."
            });
        }

        private string GetGeneralSecurityTip()
        {
            return GetRandomResponse(new List<string>
            {
                "Always keep your software and operating systems updated with the latest security patches.",
                "Enable two-factor authentication on all accounts that offer it.",
                "Regularly back up your important data to protect against ransomware attacks.",
                "Be cautious when using public Wi-Fi networks. Avoid accessing sensitive information when connected to them.",
                "Use antivirus software and keep it updated to protect against malware."
            });
        }
    }
}