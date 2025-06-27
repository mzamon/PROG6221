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

namespace POE_Attempt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void show_chats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void Reminder(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Question submitted");

            //collect data from text box
            String CollectedQuestion = user_question.Text.ToString();
            //validation
            if (!CollectedQuestion.Equals(""))
            {
                //check if user wants to add a task
                if (CollectedQuestion.ToLower().Contains("add task"))
                {
                    //add the task to the list view but get date and time
                    DateTime Date = DateTime.Now.Date;
                    DateTime Time = DateTime.Now.ToLocalTime();

                    //set up for date
                    String Date_Format = Date.ToString("yyyy-MM-dd");
                    String Time_Format = Time.ToString("HH:mm:ss");
                    MessageBox.Show("Task added successfully");
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
            else
            {
                //error message
                MessageBox.Show("Please prompt the AI\nYou have entered and empty string!");
            }
        }//event handler

        private void AskQuestion(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Question submitted");
        }
    }
}