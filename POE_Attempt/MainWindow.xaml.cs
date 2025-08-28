using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Path = System.IO.Path;
using System.Threading.Tasks;

namespace POE_Attempt
{
    ///side class
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public bool IsOverdue => ReminderDate.HasValue && ReminderDate.Value < DateTime.Now && !IsCompleted;

    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TaskItem> TaskList = new List<TaskItem>();
        // Keep track of added activities for activity log
        private List<string> RecentActivities = new List<string>();
        private List<string> Quiz_Questions = new List<string>();
        private List<string> Correct_Answers = new List<string>();
        private List<string> Wrong_Answers = new List<string>();
        private string[] Current_Options = new string[4];
        private string Current_CorrectAnswer = "";
        private string PlayerName = "";
        private string UserName = "User"; // Store the user's actual name for personalization
        private int Score = 0;
        private int iCount = 0;
        private Stopwatch QuizTimer = new Stopwatch();
        private List<string> High_Score_Names = new List<string>();
        private List<TimeSpan> High_ScoreTimes = new List<TimeSpan>();
        private string GameState = "idle"; // possible: idle, awaiting_name, playing, finished

        // Lists for sentiment detection and responses
        private List<string> SentimentPositive = new List<string> { "happy", "excited", "glad", "pleased", "delighted", "joyful", "cheerful", "thrilled", "elated", "content" };
        private List<string> SentimentNegative = new List<string> { "sad", "worried", "anxious", "scared", "nervous", "frustrated", "angry", "upset", "concerned", "afraid" };
        private List<string> SentimentNeutral = new List<string> { "okay", "fine", "neutral", "unsure", "curious", "interested", "thoughtful", "contemplative", "pensive", "reflective" };
        private List<string> SentimentConfused = new List<string> { "confused", "perplexed", "puzzled", "bewildered", "baffled", "uncertain", "doubtful", "hesitant", "ambivalent", "indecisive" };

        // General responses for chatbot
        private List<string> GeneralResponses = new List<string> {
            "I'm here to help with your cybersecurity questions!",
            "Feel free to ask me anything about staying safe online.",
            "I can assist with password advice, phishing awareness, and more.",
            "How else can I help you secure your digital life?",
            "Cybersecurity is important - I'm glad you're taking it seriously!",
            "Is there a specific security topic you'd like to explore?",
            "Remember, strong passwords are your first line of defense.",
            "I'm programmed to help you navigate the digital world safely.",
            "What aspect of cybersecurity interests you most?",
            "I'm always learning new security tips to share with you."
        };

        // Responses to common questions
        private Dictionary<string, List<string>> CommonResponses = new Dictionary<string, List<string>>
        {
            {"how are you", new List<string>{
                "I'm functioning perfectly! How can I help with your cybersecurity needs today?",
                "I'm operational and ready to assist with your security questions!",
                "All systems running smoothly! What cybersecurity topic shall we discuss?"
            }},
            {"what's your purpose", new List<string>{
                "My purpose is to help you stay safe online by providing cybersecurity advice and tips.",
                "I'm designed to increase your awareness about cybersecurity threats and protection methods.",
                "I exist to make cybersecurity accessible and understandable for everyone."
            }},
            {"what can i ask you about", new List<string>{
                "You can ask me about password safety, phishing scams, safe browsing, two-factor authentication, data privacy, and many other cybersecurity topics!",
                "Feel free to ask about any cybersecurity concerns - from social engineering to malware protection, I've got you covered!",
                "I can discuss various security topics like secure online shopping, protecting your identity online, or setting up secure home networks."
            }}
        };

        // Keyword responses for cybersecurity topics
        private Dictionary<string, List<string>> KeywordResponses = new Dictionary<string, List<string>>
        {
            {"password", new List<string>{
                "Strong passwords should be at least 12 characters with a mix of letters, numbers, and symbols. Consider using a password manager!",
                "Never reuse passwords across multiple sites. Each account deserves its own unique, strong password.",
                "Consider using passphrases instead of single words - they're longer and easier to remember while being more secure.",
                "Remember to change your critical passwords every few months, especially for financial accounts.",
                "A random password like 'P7!tQ*9Lm@2s' is much stronger than 'Password123'."
            }},
            {"phishing", new List<string>{
                "Phishing emails often create urgency. Be suspicious of messages claiming your account will be closed unless you act immediately.",
                "Always check email sender addresses carefully - phishers often use addresses that look legitimate at first glance.",
                "Hover over links before clicking to see where they actually lead. If in doubt, navigate to the website directly instead of using the link.",
                "Legitimate companies won't ask for sensitive information via email. When in doubt, contact the company directly.",
                "Modern phishing attempts can be highly personalized. Even if an email mentions your name or recent activity, remain vigilant."
            }},
            {"privacy", new List<string>{
                "Regularly review and adjust your privacy settings on social media platforms to control who sees your information.",
                "Consider using privacy-focused alternatives to common services, such as DuckDuckGo instead of Google for searches.",
                "Be mindful of what you share online - once information is public, it can be difficult to make it private again.",
                "Use a VPN when connecting to public Wi-Fi to protect your browsing data from potential eavesdroppers.",
                "Regularly clear your cookies and browsing history to minimize tracking across websites."
            }},
            {"virus", new List<string>{
                "Keep your operating system and applications updated to protect against known vulnerabilities.",
                "Use reputable antivirus software and keep it updated to detect and remove malicious software.",
                "Be cautious when downloading files or software, especially from unofficial sources.",
                "Scan email attachments before opening them, even if they come from people you know.",
                "Back up your important data regularly to minimize the impact of ransomware or destructive malware."
            }},
            {"scam", new List<string>{
                "If an offer seems too good to be true, it probably is. Trust your instincts!",
                "Legitimate organizations won't ask you to wire money or pay with gift cards.",
                "Research unfamiliar companies before making purchases or providing personal information.",
                "Be wary of unexpected contacts, even if they claim to be from organizations you trust.",
                "Take your time before responding to requests for money or personal information - scammers often create artificial urgency."
            }}
        };
        private object task;

        public MainWindow()
        {
            InitializeComponent();
            InitializeRecentActivities(); // Initialize the activity log with creation event
            Show_ChatBot_Ascii(this, null); // Auto-launch 
        }

        private void InitializeRecentActivities()
        {
            // Add initial startup entry to activity log
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            RecentActivities.Add($"{timestamp}: Chatbot started");
        }


        private void show_chats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Reminder(sender, e);

        }
        private void Reminder(object sender, RoutedEventArgs e)
        {
            // Get user input
            string userInput = user_question.Text.Trim();

            // Check if game is already running
            if (GameState == "playing")
            {
                show_chats.Items.Add("Bot: 👮‍♂️ Sorry, can't process regular chats during the quiz!");
                return;
            }

            // Check if we're waiting for the user's name
            if (GameState == "awaiting_user_name" && !string.IsNullOrEmpty(userInput))
            {
                UserName = userInput;
                show_chats.Items.Add($"User: {UserName}");
                show_chats.Items.Add($"Bot: 🤖 Nice to meet you, {UserName}! I'm here to help with all your cybersecurity needs.");
                show_chats.Items.Add("Bot: 💬 Ask me anything about cybersecurity — passwords, phishing, privacy, or use the sidebar icons to access more features!");
                show_chats.Items.Add(""); // Add spacing for readability

                // Update game state
                GameState = "idle";

                // Add to activity log
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                RecentActivities.Add($"{timestamp}: User identified as {UserName}");

                // Clear input and return
                user_question.Text = "";
                show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
                return;
            }

            // Input validation
            if (string.IsNullOrWhiteSpace(userInput))
            {
                // Don't clear the chat - just notify the user
                show_chats.Items.Add("Bot: Please enter a message. I can't respond to empty messages.");
                return;
            }

            //secret password test
            if (userInput.ToLower().Contains("password123"))
            {
                show_chats.Items.Add($"User: {userInput}");
                show_chats.Items.Add("Bot: 👮‍♂️ SECRET CONTROL PANEL ACCESS");
                show_chats.Items.Add("Bot: ⚙️ System Commands:");
                show_chats.Items.Add("Bot: ✏️ 'clear' - Clear chat history");
                show_chats.Items.Add("Bot: 📅 'export' - Save chat history");
                show_chats.Items.Add("Bot: ❌ 'reset' - Reset application state");
                show_chats.Items.Add("");

                // Add to activity log
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                RecentActivities.Add($"{timestamp}: Accessed secret control panel");

                user_question.Text = "";
                show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
                return;
            }

            //Control Commands Check
            switch (userInput.ToLower())
            {
                case "clear":
                    show_chats.Items.Add("User: clear");
                    show_chats.Items.Add("Bot: 🧹 Clearing chat history...");

                    // Add to activity log before clearing
                    string clearTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    RecentActivities.Add($"{clearTimestamp}: Chat history cleared by user");

                    // Don't actually clear - just add the message saying it was cleared
                    show_chats.Items.Add("Bot: 🤖 Chat history cleared!");
                    show_chats.Items.Add("");
                    user_question.Clear();
                    show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
                    return;

                case "export":
                    Export_Chat_History();
                    user_question.Clear();
                    return;

                case "reset":
                    show_chats.Items.Add("User: reset");
                    show_chats.Items.Add("Bot: 🔄 Resetting system...");

                    // Add to activity log
                    string resetTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    RecentActivities.Add($"{resetTimestamp}: System reset initiated");
                    Show_ChatBot_Ascii(this, null);
                    user_question.Clear();
                    return;
            }

            // Always display user input first
            show_chats.Items.Add($"User: {userInput}");

            // Attempt to process as NLP task command
            if (ProcessNLPTaskCommands(userInput))
            {
                // The NLP processor handled this command
                user_question.Text = "";
                show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
                return;
            }

            // Generate a response based on input
            string botResponse = GenerateResponse(userInput);
            show_chats.Items.Add($"Bot: {botResponse}");
            show_chats.Items.Add(""); // Add spacing for readability

            // Add to activity log
            string timestamp2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            RecentActivities.Add($"{timestamp2}: Responded to: {(userInput.Length > 30 ? userInput.Substring(0, 30) + "..." : userInput)}");

            // Clear the input and focus
            user_question.Text = "";
            show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
        }

        private void Export_Chat_History()
        {
            throw new NotImplementedException();
        }

        private string GenerateResponse(string userInput)
        {
            string inputLower = userInput.ToLower();
            Random random = new Random();

            // First, detect sentiment to adjust response tone
            string detectedSentiment = DetectSentiment(inputLower);

            // Check for common questions/greetings
            foreach (var pattern in CommonResponses.Keys)
            {
                if (inputLower.Contains(pattern))
                {
                    // Get random response from the appropriate list
                    List<string> responses = CommonResponses[pattern];
                    return responses[random.Next(responses.Count)];
                }
            }

            // Check for cybersecurity keywords
            foreach (var keyword in KeywordResponses.Keys)
            {
                if (inputLower.Contains(keyword))
                {
                    // Get random response for this keyword
                    List<string> responses = KeywordResponses[keyword];
                    string response = responses[random.Next(responses.Count)];

                    // Personalize with user's name if we have it
                    if (UserName != "User")
                    {
                        return $"{UserName}, {response}";
                    }
                    return response;
                }
            }

            // If we detected sentiment but no specific topic, respond to the sentiment
            if (detectedSentiment != "neutral")
            {
                switch (detectedSentiment)
                {
                    case "positive":
                        return $"I'm glad you're feeling good! 😊 Is there any cybersecurity topic you'd like to discuss?";
                    case "negative":
                        return $"I understand you might be feeling concerned. Cybersecurity can be overwhelming, but I'm here to help make it manageable.";
                    case "confused":
                        return $"It seems like you might be a bit confused. Let me know what specific cybersecurity topic you'd like me to explain more clearly.";
                }
            }

            // Default response if no patterns matched - use general responses list
            return GeneralResponses[random.Next(GeneralResponses.Count)];
        }

        private string DetectSentiment(string input)
        {
            // Check for positive sentiments
            foreach (var sentiment in SentimentPositive)
            {
                if (input.Contains(sentiment))
                {
                    return "positive";
                }
            }

            // Check for negative sentiments
            foreach (var sentiment in SentimentNegative)
            {
                if (input.Contains(sentiment))
                {
                    return "negative";
                }
            }

            // Check for confused sentiments
            foreach (var sentiment in SentimentConfused)
            {
                if (input.Contains(sentiment))
                {
                    return "confused";
                }
            }

            // Default to neutral
            return "neutral";
        }

        private bool ProcessNLPTaskCommands(string userInput)
        {
            string inputLower = userInput.ToLower();

            // Pattern for adding tasks
            if (inputLower.Contains("add task") || inputLower.Contains("create task") ||
                inputLower.Contains("new task") || inputLower.StartsWith("task:") ||
                (inputLower.Contains("remind") && inputLower.Contains("me")))
            {
                // Extract task details
                string taskDescription = userInput;

                // Remove command phrases
                taskDescription = Regex.Replace(taskDescription, "(?i)add task[:/s-]*", "");
                taskDescription = Regex.Replace(taskDescription, "(?i)create task[:/s-]*", "");
                taskDescription = Regex.Replace(taskDescription, "(?i)new task[:/s-]*", "");
                taskDescription = Regex.Replace(taskDescription, "(?i)task:[/s-]*", "");
                taskDescription = Regex.Replace(taskDescription, "(?i)remind me (to|about)[:/s-]*", "");

                // Check if we have a valid task
                if (!string.IsNullOrWhiteSpace(taskDescription))
                {
                    // Create and add the task
                    TaskItem newTask = new TaskItem
                    {
                        Id = Guid.NewGuid(),
                        Title = taskDescription,
                        Description = "Task created via chat",
                        IsCompleted = false
                    };

                    // Check for date/time mentions
                    if (inputLower.Contains("tomorrow"))
                    {
                        newTask.ReminderDate = DateTime.Now.AddDays(1);
                    }
                    else if (inputLower.Contains("next week"))
                    {
                        newTask.ReminderDate = DateTime.Now.AddDays(7);
                    }
                    else if (inputLower.Contains("in 2 days") || inputLower.Contains("in two days"))
                    {
                        newTask.ReminderDate = DateTime.Now.AddDays(2);
                    }
                    else if (inputLower.Contains("in 3 days") || inputLower.Contains("in three days"))
                    {
                        newTask.ReminderDate = DateTime.Now.AddDays(3);
                    }

                    // Add the task to our list
                    TaskList.Add(newTask);

                    // Confirm to user
                    show_chats.Items.Add($"Bot: ✅ Task added: '{taskDescription}'");

                    // Ask if they want to add a reminder if none was detected
                    if (!newTask.ReminderDate.HasValue)
                    {
                        show_chats.Items.Add($"Bot: Would you like to add a reminder for this task? (Reply: 'Yes, remind me in [time period]' or 'No thanks')");
                    }
                    else
                    {
                        show_chats.Items.Add($"Bot: 🔔 I'll remind you on {newTask.ReminderDate.Value.ToString("dddd, MMMM d")}");
                    }

                    // Add to activity log
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    RecentActivities.Add($"{timestamp}: Added task '{taskDescription}'");

                    return true;
                }
            }

            // Command to list tasks
            if (inputLower.Contains("show tasks") || inputLower.Contains("list tasks") ||
                inputLower.Contains("my tasks") || inputLower == "tasks")
            {
                DisplayTasksInChat();
                return true;
            }

            // Command to check activity log
            if (inputLower.Contains("activity log") || inputLower.Contains("show log") ||
                inputLower.Contains("what have you done") || inputLower.Contains("recent actions"))
            {
                show_Activity_Log(this, null);
                return true;
            }

            return false; // No task command detected
        }

        private void DisplayTasksInChat()
        {
            if (TaskList.Count > 0)
            {
                show_chats.Items.Add("Bot: 📋 Here are your current tasks:");

                int index = 1;
                foreach (var task in TaskList)
                {
                    string status = task.IsCompleted ? "✅ DONE" : "⏳ PENDING";
                    string dueInfo = task.ReminderDate.HasValue ? $" - Due: {task.ReminderDate.Value.ToString("MMM d")}" : "";
                    show_chats.Items.Add($"Bot: {index}. {status} - {task.Title}{dueInfo}");
                    index++;
                }
                show_chats.Items.Add(""); // Add spacing
            }
            else
            {
                show_chats.Items.Add("Bot: 📋 You don't have any tasks yet. You can add one by saying 'Add task: [task description]'");
                show_chats.Items.Add("");
            }

            // Add to activity log
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            RecentActivities.Add($"{timestamp}: Displayed task list");
        }
        private void show_Cyber_Security_Mini_Game(object sender, RoutedEventArgs e)
        {
            //Use string Builder
            StringBuilder asciiHeader = new StringBuilder();
            asciiHeader.AppendLine("===============================================");
            asciiHeader.AppendLine("|             CYBERSECURITY QUIZ              |");
            asciiHeader.AppendLine("===============================================");
            asciiHeader.AppendLine("|  Test your knowledge. Type answers carefully|");
            asciiHeader.AppendLine("|  Only type the correct LETTER (a, b, c, d)  |");
            asciiHeader.AppendLine("===============================================");

            show_chats.Items.Clear();
            show_chats.Items.Add(asciiHeader.ToString());

            //Prompt User for name
            user_question.Text = "Enter your name to begin the quiz:";
            user_question.IsReadOnly = false;

            //State
            GameState = "awaiting_name";
        }
        private void show_NLP_Simulation_GUI(object sender, MouseButtonEventArgs e)
        {
            // NLP Simulation GUI - Display information about natural language processing capabilities
            StringBuilder nlpHeader = new StringBuilder();
            nlpHeader.AppendLine("===============================================");
            nlpHeader.AppendLine("|           NATURAL LANGUAGE PROCESSING        |");
            nlpHeader.AppendLine("===============================================");
            nlpHeader.AppendLine("|  Try these phrases:                        |");
            nlpHeader.AppendLine("|  - Remind me to check my password tomorrow |");
            nlpHeader.AppendLine("|  - Create a task to enable 2FA            |");
            nlpHeader.AppendLine("|  - Show activity log                       |");
            nlpHeader.AppendLine("===============================================");

            show_chats.Items.Clear();
            show_chats.Items.Add(nlpHeader.ToString());
            user_question.Focus();

            // Add to recent activities
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            RecentActivities.Add($"{timestamp}: Opened NLP Simulation");
        }

        private void ReminderCore(object sender, RoutedEventArgs e)
        {
            //collect data from text box
            string CollectedQuestion = user_question.Text.ToString().Trim();

            //validation
            if (string.IsNullOrWhiteSpace(CollectedQuestion))
            {
                //error message
                MessageBox.Show("Please prompt the AI\nYou have entered an empty string!");
                return;
            }

            // === Awaiting Player Name ===
            if (GameState == "awaiting_name")
            {
                PlayerName = CollectedQuestion;
                user_question.Text = "";
                show_chats.Items.Add($"Welcome, {PlayerName}! Starting the Cybersecurity Quiz...");
                GameState = "playing";
                iCount = 0;
                Score = 0;
                QuizTimer.Restart();

                LoadQuestions();
                AskNextQuestion();
                return;
            }

            // === Playing: Answering Quiz ===
            if (GameState == "playing")
            {
                string answer = CollectedQuestion.ToLower();

                if (answer != "a" && answer != "b" && answer != "c" && answer != "d")
                {
                    MessageBox.Show("⚠ Please enter only one of the letters: a, b, c, or d");
                    return;
                }

                if (answer == Current_CorrectAnswer.ToLower())
                {
                    Score++;
                    show_chats.Items.Add($"✅ Correct! +1 point. Current Score: {Score}/10");
                    show_chats.Items.Add("Explanation: Great job! You understand this cybersecurity concept.");

                    // Add to activity log
                    RecentActivities.Add($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Answered quiz question correctly");
                }
                else
                {
                    show_chats.Items.Add($"❌ Incorrect. The correct answer was: {Current_CorrectAnswer.ToUpper()}");
                    show_chats.Items.Add("Explanation: Review this concept to improve your cyber safety.");

                    // Add to activity log
                    RecentActivities.Add($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Answered quiz question incorrectly");
                }

                AskNextQuestion();
                return;
            }

            // === NLP Simulation Section ===
            string lowered = CollectedQuestion.ToLower();

            // Task completion handling
            if (Regex.IsMatch(lowered, @"\bcomplete\s+-\s+(.+)\b") ||
                Regex.IsMatch(lowered, @"\bmark\s+(?:as\s+)?(?:complete|done|finished)\s+-\s+(.+)\b"))
            {
                string taskTitle = "";
                Match match = Regex.Match(lowered, @"(?:complete|mark|done|finished)\s+(?:as\s+)?(?:complete|done|finished)?\s+-\s+(.+)\b");
                if (match.Success)
                {
                    taskTitle = match.Groups[1].Value.Trim();
                    CompleteTask(taskTitle);

                    // Add to activity log
                    RecentActivities.Add($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Completed task '{taskTitle}'");
                }
                return;
            }

            // Task deletion handling
            if (Regex.IsMatch(lowered, @"\bdelete\s+-\s+(.+)\b") ||
                Regex.IsMatch(lowered, @"\bremove\s+(?:task)?\s+-\s+(.+)\b"))
            {
                string taskTitle = "";
                Match match = Regex.Match(lowered, @"(?:delete|remove)\s+(?:task)?\s+-\s+(.+)\b");
                if (match.Success)
                {
                    taskTitle = match.Groups[1].Value.Trim();
                    DeleteTask(taskTitle);

                    // Add to activity log
                    RecentActivities.Add($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Deleted task '{taskTitle}'");
                }
                return;
            }

            if (Regex.IsMatch(lowered, @"\b(remind|reminder|notify)\b.*\b(password|privacy|scan|check|update|login)\b"))
            {
                string reminderText = ExtractTaskText(CollectedQuestion);
                show_chats.Items.Add($"✅ Reminder set for: '{reminderText}' on {DateTime.Now.ToShortDateString()}");

                // Add to activity log
                RecentActivities.Add($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Set reminder for '{reminderText}'");
            }
            else if (Regex.IsMatch(lowered, @"\b(add|create|make|setup)\b.*\b(task|reminder|to-do)\b"))
            {
                string taskText = ExtractTaskText(CollectedQuestion);
                AddCyberTask(taskText, "Cybersecurity-related task.");

                // Add to activity log
                RecentActivities.Add($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Added task '{taskText}'");
            }
            else if (Regex.IsMatch(lowered, @"\bwhat have you done\b|\brecent actions\b|\bactivity log\b"))
            {
                DisplayRecentActions();
            }
            else if (lowered.Contains("remind me in"))
            {
                DateTime? reminder = ParseNaturalReminder(CollectedQuestion);

                if (TaskList.Count > 0)
                {
                    TaskItem lastTask = TaskList.LastOrDefault(t => !t.IsCompleted && t.ReminderDate == null);
                    if (lastTask != null && reminder.HasValue)
                    {
                        lastTask.ReminderDate = reminder;
                        show_chats.Items.Add($"✅ Reminder updated for task '{lastTask.Title}' on {reminder.Value.ToShortDateString()}");

                        // Add to activity log
                        RecentActivities.Add($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Updated reminder for task '{lastTask.Title}'");
                    }
                    else
                    {
                        show_chats.Items.Add("⚠ Couldn't find the recent task to update.");
                    }
                }
            }
            else
            {
                //fallbacks
                if (CollectedQuestion.Contains("add task"))
                {
                    DateTime Date = DateTime.Now.Date;
                    DateTime Time = DateTime.Now.ToLocalTime();

                    //set up for date
                    String Date_Format = Date.ToString("yyyy-MM-dd");
                    String Time_Format = Time.ToString("HH:mm:ss");

                    //add item
                    show_chats.Items.Add("User : " + CollectedQuestion + "\n" +
                                                "Date: " + Date_Format + "\n" +
                                                "Time: " + Time_Format + "\n");

                    //set list view
                    show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);
                }
                else if (CollectedQuestion.Contains("reminder") || CollectedQuestion.Contains("Reminder"))
                {
                    //show message box
                    MessageBox.Show("Reminder set successfully");
                }
                else
                {
                    //show message box
                    MessageBox.Show("AI response: " + CollectedQuestion);
                }
            }
        }

        private string ExtractTaskText(string input)
        {
            //simulate phrase removal
            string[] phrases = {
        "remind me to", "can you remind me to", "please remind me to",
        "add a task to", "create a task for", "set a reminder to"
    };

            string lowered = input.ToLower();

            foreach (var phrase in phrases)
            {
                if (lowered.Contains(phrase))
                {
                    int index = lowered.IndexOf(phrase) + phrase.Length;
                    return input.Substring(index).Trim();
                }
            }

            //fallback
            return input.Trim();
        }

        private void DisplayRecentActions()
        {
            //get summary from last tasks or reminders
            List<string> recent = show_chats.Items
                .Cast<object>()
                .Select(i => i.ToString())
                .Where(line => line.StartsWith("✅ Reminder set") || line.StartsWith("📝 Task added"))
                .TakeLast(5)
                .ToList();

            if (recent.Count == 0)
            {
                show_chats.Items.Add("ℹ No recent actions found.");
                return;
            }

            show_chats.Items.Add("📋 Here’s a summary of recent actions:");
            int index = 1;
            foreach (var item in recent)
            {
                show_chats.Items.Add($"{index++}. {item}");
            }
        }


        private void FinishQuiz()
        {
            QuizTimer.Stop();
            TimeSpan timeTaken = QuizTimer.Elapsed;
            string timeFormatted = timeTaken.Minutes.ToString("00") + ":" + timeTaken.Seconds.ToString("00");

            show_chats.Items.Add("\n🎉 Quiz Complete!");
            show_chats.Items.Add($"🏁 Final Score: {Score}/10");
            show_chats.Items.Add($"⏱ Time Taken: {timeFormatted}");

            // Dynamic score feedback
            if (Score >= 9)
            {
                show_chats.Items.Add("🎯 Excellent! You’re a cybersecurity pro!");
            }
            else if (Score >= 6)
            {
                show_chats.Items.Add("👍 Good job! You’re on your way to becoming cyber smart.");
            }
            else if (Score >= 4)
            {
                show_chats.Items.Add("⚠ Not bad, but keep learning to stay safe online.");
            }
            else
            {
                show_chats.Items.Add("🚨 Consider reviewing cybersecurity basics. Stay alert!");
            }

            // Add to high scores
            High_Score_Names.Add($"{PlayerName} ({timeFormatted})");
            High_ScoreTimes.Add(timeTaken);

            // Bubble sort high scores (sort by time — faster = better)
            for (int i = 0; i < High_Score_Names.Count - 1; i++)
            {
                for (int j = 0; j < High_Score_Names.Count - i - 1; j++)
                {
                    if (High_ScoreTimes[j] > High_ScoreTimes[j + 1])
                    {
                        // Swap names
                        var tempName = High_Score_Names[j];
                        High_Score_Names[j] = High_Score_Names[j + 1];
                        High_Score_Names[j + 1] = tempName;

                        // Swap times
                        var tempTime = High_ScoreTimes[j];
                        High_ScoreTimes[j] = High_ScoreTimes[j + 1];
                        High_ScoreTimes[j + 1] = tempTime;
                    }
                }
            }

            // Display leaderboard
            show_chats.Items.Add("\n🏆 High Scores:");
            for (int i = 0; i < High_Score_Names.Count; i++)
            {
                show_chats.Items.Add($"{i + 1}. {High_Score_Names[i]}");
            }

            // Save quiz result to file
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string fileName = $"Quiz_{PlayerName}_{timestamp}.txt";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Cybersecurity Quiz Result");
            sb.AppendLine("===========================");
            sb.AppendLine($"Name: {PlayerName}");
            sb.AppendLine($"Score: {Score}/10");
            sb.AppendLine($"Time: {timeFormatted}");

            sb.AppendLine("\nFeedback:");
            if (Score >= 9)
                sb.AppendLine("Excellent! You're a cybersecurity pro!");
            else if (Score >= 6)
                sb.AppendLine("Good job! Keep learning.");
            else if (Score >= 4)
                sb.AppendLine("You're getting there. Stay cautious online!");
            else
                sb.AppendLine("Review basic concepts to improve your safety.");

            sb.AppendLine("\nHigh Scores:");
            for (int i = 0; i < High_Score_Names.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {High_Score_Names[i]}");
            }

            File.WriteAllText(path, sb.ToString());
            MessageBox.Show($"✅ Quiz saved to:\n{path}", "Quiz Saved", MessageBoxButton.OK, MessageBoxImage.Information);

            // Ask user to play again
            MessageBoxResult result = MessageBox.Show("Would you like to play again?", "Restart Quiz", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                GameState = "awaiting_name";
                user_question.Text = "Enter your name to begin the quiz...";
                show_chats.Items.Clear();
                return;
            }
            else
            {
                GameState = "idle";
                user_question.Text = "";
                user_question.IsReadOnly = false;
            }
        }

        private void LoadQuestions()
        {
            Quiz_Questions.Clear();
            Correct_Answers.Clear();
            Wrong_Answers.Clear();

            // Question 1
            Quiz_Questions.Add("What's the proper response to a credential harvesting attempt via email?");
            Correct_Answers.Add("c");
            Wrong_Answers.AddRange(new[] { "a", "b", "d" });

            Quiz_Questions.Add("Which password demonstrates proper complexity and entropy?");
            Correct_Answers.Add("b");
            Wrong_Answers.AddRange(new[] { "a", "c", "d" });

            Quiz_Questions.Add("What exactly characterizes a phishing attack in cybersecurity?");
            Correct_Answers.Add("a");
            Wrong_Answers.AddRange(new[] { "b", "c", "d" });

            Quiz_Questions.Add("Which authentication method provides optimal security for online banking?");
            Correct_Answers.Add("d");
            Wrong_Answers.AddRange(new[] { "a", "b", "c" });

            Quiz_Questions.Add("How should you handle a potentially weaponized USB device found in your workspace?");
            Correct_Answers.Add("b");
            Wrong_Answers.AddRange(new[] { "a", "c", "d" });

            Quiz_Questions.Add("What's the recommended frequency for rotating credentials according to NIST guidelines?");
            Correct_Answers.Add("a");
            Wrong_Answers.AddRange(new[] { "b", "c", "d" });

            Quiz_Questions.Add("Which of these represents a specific malware classification?");
            Correct_Answers.Add("c");
            Wrong_Answers.AddRange(new[] { "a", "b", "d" });

            Quiz_Questions.Add("Which email characteristic most strongly indicates a spear-phishing attempt?");
            Correct_Answers.Add("d");
            Wrong_Answers.AddRange(new[] { "a", "b", "c" });

            Quiz_Questions.Add("The primary security benefit of two-factor authentication is:");
            Correct_Answers.Add("a");
            Wrong_Answers.AddRange(new[] { "b", "c", "d" });

            Quiz_Questions.Add("What's the most effective defense against crypto-ransomware attacks?");
            Correct_Answers.Add("c");
            Wrong_Answers.AddRange(new[] { "a", "b", "d" });

            Quiz_Questions.Add("Social engineering primarily exploits which vulnerability?");
            Correct_Answers.Add("b");
            Wrong_Answers.AddRange(new[] { "a", "c", "d" });

            Quiz_Questions.Add("Which hyperlink appears legitimate when inspecting its href attribute?");
            Correct_Answers.Add("a");
            Wrong_Answers.AddRange(new[] { "b", "c", "d" });

            Quiz_Questions.Add("What protocol prefix indicates TLS encryption is active?");
            Correct_Answers.Add("c");
            Wrong_Answers.AddRange(new[] { "a", "b", "d" });

            Quiz_Questions.Add("Which artifact most reliably identifies a spoofed website?");
            Correct_Answers.Add("d");
            Wrong_Answers.AddRange(new[] { "a", "b", "c" });

            Quiz_Questions.Add("What's the primary function of a network firewall?");
            Correct_Answers.Add("a");
            Wrong_Answers.AddRange(new[] { "b", "c", "d" });

            Quiz_Questions.Add("Why are MITM attacks particularly effective on open Wi-Fi networks?");
            Correct_Answers.Add("b");
            Wrong_Answers.AddRange(new[] { "a", "c", "d" });

            Quiz_Questions.Add("How does a VPN enhance your security posture?");
            Correct_Answers.Add("d");
            Wrong_Answers.AddRange(new[] { "a", "b", "c" });

            Quiz_Questions.Add("Which password meets contemporary complexity requirements?");
            Correct_Answers.Add("c");
            Wrong_Answers.AddRange(new[] { "a", "b", "d" });

            Quiz_Questions.Add("The primary function of spyware in the cyber kill chain is:");
            Correct_Answers.Add("a");
            Wrong_Answers.AddRange(new[] { "b", "c", "d" });

            Quiz_Questions.Add("What's the gold standard for protecting digital identities?");
            Correct_Answers.Add("b");
            Wrong_Answers.AddRange(new[] { "a", "c", "d" });
        }



        private void AskQuestion(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Question submitted");
        }
        private void show_Activity_Log(object sender, RoutedEventArgs e)
        {
            // Create an activity log header
            StringBuilder activityHeader = new StringBuilder();
            activityHeader.AppendLine("===============================================");
            activityHeader.AppendLine("|             ACTIVITY LOG                   |");
            activityHeader.AppendLine("===============================================");
            activityHeader.AppendLine("|  View your recent activity or saved logs   |");
            activityHeader.AppendLine("===============================================");

            show_chats.Items.Clear();
            show_chats.Items.Add(activityHeader.ToString());

            // Display the recent activities from memory
            if (RecentActivities.Count > 0)
            {
                show_chats.Items.Add("📋 Recent Activities:");
                int count = 1;
                // Get at most the last 15 activities
                int startIndex = Math.Max(0, RecentActivities.Count - 15);
                for (int i = startIndex; i < RecentActivities.Count; i++)
                {
                    show_chats.Items.Add($"{count++}. {RecentActivities[i]}");
                }
            }
            else
            {
                show_chats.Items.Add("ℹ No activities recorded yet.");
            }

            // Options for saved logs
            show_chats.Items.Add("\n📂 Saved Log Options:");
            show_chats.Items.Add("Type 'save log' to save current activities");
            show_chats.Items.Add("Type 'view saved logs' to browse previous logs");

            // Add to activity log itself
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            RecentActivities.Add($"{timestamp}: Viewed activity log");

            // Focus on input field
            user_question.Focus();

            // Old functionality for finding and loading chat files
            // Now only triggered when user specifically requests it
            if (false) // This part is kept but not immediately executed
            {
                // Find directory of existing files  
                String DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                String FilePattern = "ChatLog_*.txt";
                var ChatFiles = Directory.GetFiles(DirectoryPath, FilePattern);

                // Show how many files exist  
                MessageBox.Show($"📁 {ChatFiles.Length} chat history file(s) found.", "Activity Log");

                // Catch  
                if (ChatFiles.Length == 0)
                {
                    MessageBox.Show("No chat history files found in your documents folder.");
                    return;
                }

                // Ask user to select  
                String Input = Interaction.InputBox("Choose an option:\n1. Load and view a previous chat log\n2. Save current chat log\n\nType 1 or 2", "Activity Log Choice");

                if (Input == "1")
                {
                    //found in list
                    String DatePrefix = DateTime.Now.ToString("yyyy-MM-dd"); // Correctly define datePrefix  
                    var MatchFile = ChatFiles.FirstOrDefault(f => System.IO.Path.GetFileName(f).StartsWith($"ChatLog_{DatePrefix}"));

                    if (MatchFile == null)
                    {
                        MessageBox.Show($"❌ No chat file found with today's date: {DatePrefix}", "Not Found");
                        return;
                    }

                    //Load file contents  
                    var Lines = File.ReadAllLines(MatchFile)
                                .Where(line => !line.StartsWith("Date:") && !line.StartsWith("Time:"))
                                .ToList();
                    show_chats.Items.Clear();
                    foreach (String Line in Lines)
                    {
                        if (!String.IsNullOrWhiteSpace(Line))
                            show_chats.Items.Add(Line);
                    }

                    //Generate Summary  
                    var SummaryLines = Lines.Where(l => l.StartsWith("User :"))
                                     .Select(l => l.Replace("User : ", "•"))
                                     .ToList();
                    String Summary = String.Join("\n", SummaryLines);
                    MessageBox.Show($"📝 Summary of Chat:\n\n{Summary}", "Chat Summary");
                }
                else if (Input == "2")
                {
                    //Found in files
                    String Timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                    String Filename = $"ChatLog_{Timestamp}.txt";
                    String FullPath = Path.Combine(DirectoryPath, Filename);

                    StringBuilder SB = new StringBuilder();
                    foreach (var item in show_chats.Items)
                    {
                        SB.AppendLine(item.ToString());
                    }
                    File.WriteAllText(FullPath, SB.ToString());
                    MessageBox.Show($"✅ Chat saved to:\n{FullPath}", "Saved");
                }
                else
                {
                    MessageBox.Show("❌ Invalid option. Please type 1 or 2.", "Input Error");
                }
            }
        }

        private async void Exit_App(object sender, RoutedEventArgs e)
        {
            try
            {
                //TimeStame
                String TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                String FileName = $"ChatLog_{TimeStamp}.txt";
                String FilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FileName);
                //chat log string to save 
                StringBuilder ChatLog = new StringBuilder();
                foreach (var item in show_chats.Items)
                {
                    ChatLog.AppendLine(item.ToString());
                }
                //write
                File.WriteAllText(FilePath, ChatLog.ToString());
                //prompt user.
                show_chats.Items.Add("Bot: Chat log saved to: " + FilePath);
                MessageBox.Show("Exiting Application\n" +
                                "Chat log saved to: " + FilePath);
                await Task.Delay(1000);

                //exit application
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                //show error message
                MessageBox.Show("Error saving chat log: " + ex.Message);
                // Force exit even if there's an error
                Application.Current.Shutdown();
            }
        }
        private void AskNextQuestion()
        {
            if (iCount >= Quiz_Questions.Count)
            {
                // End the quiz if all questions have been asked
                FinishQuiz();
                return;
            }

            // Display the current question
            string question = Quiz_Questions[iCount];
            show_chats.Items.Add($"\nQuestion {iCount + 1}: {question}");

            // Generate options (assuming Current_Options is used for this purpose)
            Current_Options[0] = "a) Option A";
            Current_Options[1] = "b) Option B";
            Current_Options[2] = "c) Option C";
            Current_Options[3] = "d) Option D";

            foreach (string option in Current_Options)
            {
                show_chats.Items.Add(option);
            }

            // Set the correct answer for the current question
            Current_CorrectAnswer = Correct_Answers[iCount];

            // Increment the question counter
            iCount++;
        }
        private void AddCyberTask(string title, string description, DateTime? reminder = null)
        {
            TaskItem newTask = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                ReminderDate = reminder,
                IsCompleted = false
            };

            TaskList.Add(newTask);
            show_chats.Items.Add("📝 Task added: " + title);
            if (reminder.HasValue)
            {
                show_chats.Items.Add($"⏰ Reminder set for: {reminder.Value.ToShortDateString()}");
            }
            else
            {
                show_chats.Items.Add("🤔 Would you like to set a reminder for this task?");
            }

            RefreshTaskDisplay();
        }
        private DateTime? ParseNaturalReminder(string input)
        {
            Regex matchDays = new Regex(@"in (\d+) days");
            Match match = matchDays.Match(input.ToLower());

            if (match.Success && int.TryParse(match.Groups[1].Value, out int days))
            {
                return DateTime.Now.AddDays(days);
            }

            return null;
        }
        private void RefreshTaskDisplay()
        {
            show_chats.Items.Add("📋 Current Cyber Tasks:");
            foreach (var task in TaskList.Cast<TaskItem>().Where(t => !t.IsCompleted))
            {
                show_chats.Items.Add("• " + task.Title + " — " + task.Description +
                    (task.ReminderDate.HasValue ? $" (Remind on {task.ReminderDate.Value.ToShortDateString()})" : ""));
            }
        }
        private void CompleteTask(string title)
        {
            var task = TaskList.FirstOrDefault(t => t.Title.ToLower() == title.ToLower());
            if (task != null)
            {
                task.IsCompleted = true;
                show_chats.Items.Add($"✅ Marked task '{task.Title}' as complete.");
            }
            else
            {
                show_chats.Items.Add("⚠ Could not find task to complete.");
            }
        }

        private void DeleteTask(string title)
        {
            var task = TaskList.FirstOrDefault(t => t.Title.ToLower() == title.ToLower());
            if (task != null)
            {
                TaskList.Remove(task);
                show_chats.Items.Add($"🗑 Task '{task.Title}' deleted.");
            }
            else
            {
                show_chats.Items.Add("⚠ Could not find task to delete.");
            }
        }
        private void Initialize_Task_Assistant(object sender, MouseButtonEventArgs e)
        {
            // Display the task assistant header
            StringBuilder taskHeader = new StringBuilder();
            taskHeader.AppendLine("===============================================");
            taskHeader.AppendLine("|             TASK ASSISTANT                 |");
            taskHeader.AppendLine("===============================================");
            taskHeader.AppendLine("|  Manage your cybersecurity tasks here     |");
            taskHeader.AppendLine("|  Type 'add task - [task name]' to add     |");
            taskHeader.AppendLine("|  Type 'complete - [task name]' to finish  |");
            taskHeader.AppendLine("|  Type 'delete - [task name]' to remove    |");
            taskHeader.AppendLine("===============================================");

            show_chats.Items.Clear();
            show_chats.Items.Add(taskHeader.ToString());

            // Display current tasks if any
            RefreshTaskDisplay();

            // Focus the input field
            user_question.Text = "";
            user_question.Focus();

            // Add to recent activities
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            RecentActivities.Add($"{timestamp}: Opened Task Assistant");
        }

        private async void Show_ChatBot_Ascii(object sender, RoutedEventArgs e)
        {
            try
            {
                //Play Voice Greeting using System.Media
                System.Media.SoundPlayer VoiceGreeting = new System.Media.SoundPlayer("Greeting.wav");
                VoiceGreeting.Play();

                //Set up Bitmap ASCII Loader
                string imagePath = "AsciiLogo.bmp";
                if (!File.Exists(imagePath))
                {
                    MessageBox.Show("Missing ASCII logo: AsciiLogo.bmp", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                BitmapImage bitmapImage = new BitmapImage(new Uri(Path.GetFullPath(imagePath)));
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

                string asciiArt = GenerateAsciiArtFromBitmap(bitmapImage);
                AsciiArtDisplay.Text = asciiArt;
                //sleep(1000)
                await Task.Delay(3000); // In an async method
                //remove the ASCII art after displaying
                AsciiArtDisplay.Text = "";


                //Welcome message with decorative borders
                if (UserName == "User") // Only ask for name if we don't have it yet
                {
                    show_chats.Items.Clear(); // Only clear on first launch
                    show_chats.Items.Add("************************************************");
                    show_chats.Items.Add("*                                              *");
                    show_chats.Items.Add("*    CYBERSECURITY AWARENESS CHATBOT           *");
                    show_chats.Items.Add("*                                              *");
                    show_chats.Items.Add("************************************************");
                    show_chats.Items.Add("");
                    show_chats.Items.Add("Bot: 🤖 Hello! I'm your Cybersecurity Awareness Chatbot.");
                    show_chats.Items.Add("Bot: What's your name? I'd like to personalize our conversation.");

                    // Set state to awaiting name for personalization
                    GameState = "awaiting_user_name";
                }
                else
                {
                    // We already know the user, greet them by name
                    show_chats.Items.Add("");
                    show_chats.Items.Add($"Bot: 🤖 Welcome back, {UserName}! I'm here to help with your cybersecurity needs.");
                    show_chats.Items.Add("Bot: 💬 Ask me anything about cybersecurity — tasks, reminders, tips, or take a quiz!");
                    show_chats.Items.Add("Bot: ✅ All features have been loaded. Use the sidebar to explore or just chat with me!");

                    // Game State Reset
                    GameState = "idle";
                }

                //Focus the input field
                user_question.Text = "";
                user_question.Focus();

                // Add to activity log
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                RecentActivities.Add($"{timestamp}: Displayed welcome message");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ASCII greeting:\n" + ex.Message);
            }
        }
        private string GenerateAsciiArtFromBitmap(BitmapImage bitmap)
        {
            try
            {
                FormatConvertedBitmap converted = new FormatConvertedBitmap(bitmap, PixelFormats.Gray8, null, 0);
                int width = converted.PixelWidth;
                int height = converted.PixelHeight;
                int stride = width;
                byte[] pixels = new byte[height * stride];
                converted.CopyPixels(pixels, stride, 0);

                string chars = "@#&$%*o!;. ";
                StringBuilder sb = new StringBuilder();

                for (int y = 0; y < height; y += 2)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte pixel = pixels[y * stride + x];
                        int charIndex = (pixel * chars.Length) / 256;
                        sb.Append(chars[charIndex]);
                    }
                    sb.AppendLine();
                }

                return sb.ToString();
            }
            catch
            {
                return "[ERROR] Unable to generate ASCII from bitmap.";
            }
        }



    }
}