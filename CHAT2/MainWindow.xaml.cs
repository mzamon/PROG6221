using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CHAT2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public partial class MainWindow : Window
        {
            // Core functionality components
            private NLPProcessor nlpProcessor;
            private TaskManager taskManager;
            private QuizManager quizManager;
            private ActivityLogger activityLogger;

            private string userName = "Guest";
            private SecurityTask selectedTask;
            private QuizQuestion currentQuestion;
            private int selectedAnswerIndex = -1;

            // UI collections
            private ObservableCollection<SecurityTask> taskList = new ObservableCollection<SecurityTask>();

            public MainWindow()
            {
                InitializeComponent();
                InitializeComponents();
                DisplayAsciiArt();
            }

            private void InitializeComponents()
            {
                // Initialize core components
                nlpProcessor = new NLPProcessor();
                taskManager = new TaskManager();
                quizManager = new QuizManager();
                activityLogger = new ActivityLogger();

                // Set up task list binding
                TaskListBox.ItemsSource = taskList;

                activityLogger.AddEntry("Chatbot initialized and ready.");
            }

            private void DisplayAsciiArt()
            {
                // ASCII art for cybersecurity shield logo
                string asciiArt =
                    "    /\\     /\\     \n" +
                    "   /  \\___/  \\    \n" +
                    "  /           \\   \n" +
                    " /  CYBERBOT  \\  \n" +
                    "/___/\\___/\\___\\ \n" +
                    "    /  \\   \\    \n" +
                    "   /    \\   \\   \n" +
                    "  /      \\   \\  \n" +
                    " /        \\   \\ \n" +
                    "/          \\___\\\n";

                AsciiArtDisplay.Text = asciiArt;
            }

            private void PlayWelcomeSound()
            {
                try
                {
                    // Play a simple Windows sound as we don't have direct access to a WAV file
                    SystemSounds.Asterisk.Play();
                }
                catch (Exception ex)
                {
                    activityLogger.LogError($"Error playing welcome sound: {ex.Message}");
                }
            }

            #region Event Handlers

            private void StartButton_Click(object sender, RoutedEventArgs e)
            {
                // Get user name and start chat
                if (!string.IsNullOrWhiteSpace(UserNameInput.Text))
                {
                    userName = UserNameInput.Text.Trim();
                }

                UserNameDisplay.Text = $"User: {userName}";
                WelcomePanel.Visibility = Visibility.Collapsed;
                PlayWelcomeSound();

                // Add welcome message
                AddBotMessage($"Hello {userName}! Welcome to the Cybersecurity Awareness Chatbot. I'm here to help you stay safe online.");
                AddBotMessage("You can ask me about cybersecurity topics like password safety, phishing, and safe browsing.");
                AddBotMessage("You can also manage cybersecurity tasks, take a quiz, or view your activity log using the tabs on the right.");
                AddBotMessage("Type 'help' to see what else I can do!");

                activityLogger.AddEntry($"User {userName} started a new session.");
                RefreshActivityLog();
            }

            private void SendButton_Click(object sender, RoutedEventArgs e)
            {
                SendUserMessage();
            }

            private void UserInputBox_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter)
                {
                    SendUserMessage();
                }
            }

            private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                selectedTask = TaskListBox.SelectedItem as SecurityTask;
                CompleteTaskButton.IsEnabled = DeleteTaskButton.IsEnabled = (selectedTask != null);
            }

            private void AddTaskButton_Click(object sender, RoutedEventArgs e)
            {
                ShowAddTaskDialog();
            }

            private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
            {
                if (selectedTask != null)
                {
                    selectedTask.IsCompleted = true;
                    activityLogger.LogTaskCompleted(selectedTask.Title);
                    RefreshActivityLog();
                    RefreshTaskList();
                    AddBotMessage($"Task '{selectedTask.Title}' marked as completed.");
                }
            }

            private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
            {
                if (selectedTask != null)
                {
                    taskManager.RemoveTask(selectedTask.Id);
                    activityLogger.LogTaskDeleted(selectedTask.Title);
                    RefreshActivityLog();
                    RefreshTaskList();
                    AddBotMessage($"Task '{selectedTask.Title}' has been deleted.");
                }
            }

            private void DialogConfirmButton_Click(object sender, RoutedEventArgs e)
            {
                // Handle task creation from dialog
                string title = TaskTitleInput.Text.Trim();
                string description = TaskDescriptionInput.Text.Trim();

                if (string.IsNullOrEmpty(title))
                {
                    MessageBox.Show("Please enter a task title.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime? reminderDate = null;
                if (SetReminderCheckBox.IsChecked == true && ReminderDatePicker.SelectedDate.HasValue)
                {
                    reminderDate = ReminderDatePicker.SelectedDate.Value;
                }

                taskManager.AddTask(title, description, reminderDate);
                activityLogger.LogTaskAdded(title);

                if (reminderDate.HasValue)
                {
                    activityLogger.LogReminderSet(title, reminderDate.Value);
                    AddBotMessage($"Task '{title}' added with a reminder set for {reminderDate.Value.ToString("MMM dd, yyyy")}.");
                }
                else
                {
                    AddBotMessage($"Task '{title}' added.");
                }

                RefreshActivityLog();
                RefreshTaskList();
                CloseDialog();
            }

            private void DialogCancelButton_Click(object sender, RoutedEventArgs e)
            {
                CloseDialog();
            }

            private void SetReminderCheckBox_Checked(object sender, RoutedEventArgs e)
            {
                ReminderDateLabel.Visibility = Visibility.Visible;
                ReminderDatePicker.Visibility = Visibility.Visible;
                ReminderDatePicker.SelectedDate = DateTime.Now.AddDays(1);
            }

            private void SetReminderCheckBox_Unchecked(object sender, RoutedEventArgs e)
            {
                ReminderDateLabel.Visibility = Visibility.Collapsed;
                ReminderDatePicker.Visibility = Visibility.Collapsed;
            }

            private void StartQuizButton_Click(object sender, RoutedEventArgs e)
            {
                StartQuiz();
            }

            private void OptionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                selectedAnswerIndex = OptionsListBox.SelectedIndex;
                SubmitAnswerButton.IsEnabled = selectedAnswerIndex != -1;
            }

            private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
            {
                if (currentQuestion != null && selectedAnswerIndex >= 0)
                {
                    bool isCorrect = quizManager.SubmitAnswer(currentQuestion, selectedAnswerIndex);
                    ShowQuizFeedback(isCorrect, currentQuestion.Explanation);
                }
            }

            private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
            {
                ShowNextQuestion();
            }

            private void RefreshLogButton_Click(object sender, RoutedEventArgs e)
            {
                RefreshActivityLog();
            }

            #endregion

            #region Chat Functionality

            private void SendUserMessage()
            {
                string userInput = UserInputBox.Text.Trim();
                if (string.IsNullOrEmpty(userInput))
                    return;

                AddUserMessage(userInput);
                ProcessUserInput(userInput);
                UserInputBox.Clear();
            }

            private void ProcessUserInput(string userInput)
            {
                // Process input through NLP
                UserInput processedInput = nlpProcessor.ProcessInput(userInput);

                // Handle different intents
                switch (processedInput.Intent)
                {
                    case IntentType.AddTask:
                        HandleAddTaskIntent(processedInput);
                        break;

                    case IntentType.ViewTasks:
                        HandleViewTasksIntent();
                        break;

                    case IntentType.StartQuiz:
                        HandleStartQuizIntent();
                        break;

                    case IntentType.ViewActivityLog:
                        HandleViewActivityLogIntent();
                        break;

                    case IntentType.Help:
                        AddBotMessage("I can help you with cybersecurity awareness. You can:" +
                                     "\n- Ask about cybersecurity topics like 'password safety' or 'phishing'" +
                                     "\n- Add tasks (e.g., 'Add task to update my passwords')" +
                                     "\n- Set reminders (e.g., 'Remind me to check my privacy settings tomorrow')" +
                                     "\n- Take a cybersecurity quiz (type 'Start quiz')" +
                                     "\n- View your activity log (type 'Show activity log')");
                        break;

                    default:
                        // Handle general conversation
                        string response = nlpProcessor.GenerateResponse(processedInput, userName);
                        if (!string.IsNullOrEmpty(response))
                        {
                            AddBotMessage(response);
                        }
                        break;
                }
            }

            private void HandleAddTaskIntent(UserInput input)
            {
                if (input.Entities.ContainsKey("task_title"))
                {
                    string title = input.Entities["task_title"];
                    DateTime? reminderDate = null;

                    // Check if reminder date was extracted
                    if (input.Entities.ContainsKey("reminder_date") &&
                        DateTime.TryParse(input.Entities["reminder_date"], out DateTime date))
                    {
                        reminderDate = date;
                    }

                    taskManager.AddTask(title, "", reminderDate);
                    activityLogger.LogTaskAdded(title);

                    if (reminderDate.HasValue)
                    {
                        activityLogger.LogReminderSet(title, reminderDate.Value);
                        AddBotMessage($"I've added the task '{title}' with a reminder for {reminderDate.Value.ToString("MMM dd, yyyy")}.");
                    }
                    else
                    {
                        AddBotMessage($"I've added the task '{title}' to your list.");
                    }

                    RefreshTaskList();
                    RefreshActivityLog();
                }
                else
                {
                    AddBotMessage("What task would you like me to add?");
                }
            }

            private void HandleViewTasksIntent()
            {
                if (taskList.Count > 0)
                {
                    AddBotMessage("Here are your current tasks:");
                    foreach (var task in taskList)
                    {
                        AddBotMessage($"- {task.Title}" + (task.IsCompleted ? " (Completed)" : "") +
                                       (task.ReminderDate.HasValue ? $" - Reminder: {task.ReminderText}" : ""));
                    }
                }
                else
                {
                    AddBotMessage("You don't have any tasks yet. You can add one by saying 'Add task' followed by the task description.");
                }
            }

            private void HandleStartQuizIntent()
            {
                AddBotMessage("Great! Let's test your cybersecurity knowledge. Switch to the Quiz tab to begin.");
            }

            private void HandleViewActivityLogIntent()
            {
                List<string> recentLogs = activityLogger.GetRecentLogs();

                if (recentLogs.Count > 0)
                {
                    AddBotMessage("Here are your recent activities:");
                    foreach (string logEntry in recentLogs)
                    {
                        AddBotMessage($"- {logEntry}");
                    }
                }
                else
                {
                    AddBotMessage("No activities have been logged yet.");
                }
            }

            private void AddUserMessage(string message)
            {
                AddMessageToChatDisplay($"{userName}: {message}", "#333333");
            }

            private void AddBotMessage(string message)
            {
                AddMessageToChatDisplay($"Chatbot: {message}", "#0066cc");
            }

            private void AddMessageToChatDisplay(string message, string colorCode)
            {
                // Use dispatch if needed for UI thread
                Dispatcher.Invoke(() =>
                {
                    // Add a new line if there's already content
                    if (!string.IsNullOrEmpty(ChatDisplay.Text))
                    {
                        ChatDisplay.Text += "\n\n";
                    }

                    ChatDisplay.Text += message;

                    // Scroll to bottom
                    ChatScrollViewer.ScrollToEnd();
                });
            }

            #endregion

            #region Task Management

            private void ShowAddTaskDialog()
            {
                DialogTitleText.Text = "Add New Task";
                TaskTitleInput.Text = "";
                TaskDescriptionInput.Text = "";
                SetReminderCheckBox.IsChecked = false;
                ReminderDateLabel.Visibility = Visibility.Collapsed;
                ReminderDatePicker.Visibility = Visibility.Collapsed;

                OverlayPanel.Visibility = Visibility.Visible;
            }

            private void CloseDialog()
            {
                OverlayPanel.Visibility = Visibility.Collapsed;
            }

            private void RefreshTaskList()
            {
                taskList.Clear();
                foreach (var task in taskManager.Tasks)
                {
                    taskList.Add(task);
                }
                CheckForDueReminders();
            }

            private void CheckForDueReminders()
            {
                var dueReminders = taskManager.GetDueReminders();
                if (dueReminders.Count > 0)
                {
                    foreach (var reminder in dueReminders)
                    {
                        AddBotMessage($"⏰ Reminder: '{reminder.Title}' is due today.");
                    }
                }
            }

            #endregion

            #region Quiz Functionality

            private void StartQuiz()
            {
                quizManager.ResetQuiz();
                activityLogger.LogQuizStarted();
                RefreshActivityLog();

                QuizContent.Visibility = Visibility.Visible;
                QuizResults.Visibility = Visibility.Collapsed;
                StartQuizButton.Content = "Restart Quiz";
                StartQuizButton.Visibility = Visibility.Collapsed;

                ShowNextQuestion();
            }

            private void ShowNextQuestion()
            {
                // Reset UI elements
                FeedbackPanel.Visibility = Visibility.Collapsed;
                SubmitAnswerButton.IsEnabled = false;
                NextQuestionButton.Visibility = Visibility.Collapsed;

                // Get a new question
                currentQuestion = quizManager.GetRandomQuestion();
                if (currentQuestion != null)
                {
                    // Display question
                    QuestionText.Text = currentQuestion.Question;

                    // Set up options
                    OptionsListBox.Items.Clear();
                    foreach (string option in currentQuestion.Options)
                    {
                        OptionsListBox.Items.Add(option);
                    }

                    selectedAnswerIndex = -1;
                }
                else
                {
                    // Show results if no more questions
                    ShowQuizResults();
                }
            }

            private void ShowQuizFeedback(bool isCorrect, string explanation)
            {
                // Set up and show feedback
                if (isCorrect)
                {
                    FeedbackPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8F5E9"));
                    FeedbackPanel.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#81C784"));
                    FeedbackText.Text = $"Correct! {explanation}";
                }
                else
                {
                    FeedbackPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEBEE"));
                    FeedbackPanel.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E57373"));
                    FeedbackText.Text = $"Incorrect. {explanation}";
                }

                FeedbackPanel.Visibility = Visibility.Visible;
                SubmitAnswerButton.IsEnabled = false;
                NextQuestionButton.Visibility = Visibility.Visible;

                // If answered 10 questions, show the final results button
                if (quizManager.GetQuestionsAnswered() >= 10)
                {
                    NextQuestionButton.Content = "See Results";
                }
            }

            private void ShowQuizResults()
            {
                int score = quizManager.GetCurrentScore();
                int total = quizManager.GetQuestionsAnswered();
                string feedback = quizManager.GetFeedback();

                // Log quiz completion
                activityLogger.LogQuizCompleted(score, total);
                RefreshActivityLog();

                // Show results UI
                QuizContent.Visibility = Visibility.Collapsed;
                QuizResults.Visibility = Visibility.Visible;
                StartQuizButton.Content = "Take Quiz Again";
                StartQuizButton.Visibility = Visibility.Visible;

                // Update results text
                ScoreText.Text = $"Your score: {score}/{total}";
                FeedbackResultText.Text = feedback;

                // Also send results to chat
                AddBotMessage($"Quiz completed! Your score: {score}/{total}. {feedback}");
            }

            #endregion

            #region Activity Log

            private void RefreshActivityLog()
            {
                ActivityLogListBox.Items.Clear();
                foreach (string logEntry in activityLogger.GetRecentLogs())
                {
                    ActivityLogListBox.Items.Add(logEntry);
                }
            }

            #endregion
        }
    }