using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT2
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }

        public QuizQuestion(string question, List<string> options, int correctAnswerIndex, string explanation)
        {
            Question = question;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
            Explanation = explanation;
        }

        public bool CheckAnswer(int selectedIndex)
        {
            return selectedIndex == CorrectAnswerIndex;
        }
    }

    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private Random random = new Random();
        private int currentScore = 0;
        private int questionsAnswered = 0;

        public QuizManager()
        {
            InitializeQuizQuestions();
        }

        private void InitializeQuizQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion(
                    "What should you do if you receive an email asking for your password?",
                    new List<string>
                    {
                        "Reply with your password",
                        "Delete the email",
                        "Report the email as phishing",
                        "Ignore it"
                    },
                    2,
                    "Reporting phishing emails helps prevent scams. Legitimate organizations never ask for passwords via email."
                ),
                new QuizQuestion(
                    "Which of these is the strongest password?",
                    new List<string>
                    {
                        "password123",
                        "P@ssw0rd!",
                        "TK4!e9r*zL2@mB",
                        "your birthdate"
                    },
                    2,
                    "Strong passwords contain a mix of uppercase, lowercase, numbers, and special characters, and are at least 12 characters long."
                ),
                new QuizQuestion(
                    "What is two-factor authentication (2FA)?",
                    new List<string>
                    {
                        "Using two passwords",
                        "Using a password and another verification method",
                        "Having two users authenticate",
                        "Using two different devices"
                    },
                    1,
                    "2FA adds an extra layer of security by requiring both a password and a second verification method like a code sent to your phone."
                ),
                new QuizQuestion(
                    "Which action is safe to take when using public Wi-Fi?",
                    new List<string>
                    {
                        "Checking your bank account",
                        "Using a VPN",
                        "Sharing personal information",
                        "Disabling your firewall"
                    },
                    1,
                    "Using a VPN encrypts your internet connection, protecting your data on public Wi-Fi."
                ),
                new QuizQuestion(
                    "What is social engineering in cybersecurity?",
                    new List<string>
                    {
                        "Building secure social media profiles",
                        "Programming social media apps",
                        "Manipulating people to reveal confidential information",
                        "Creating social networks"
                    },
                    2,
                    "Social engineering attacks trick people into breaking security procedures or giving away sensitive information."
                ),
                new QuizQuestion(
                    "True or False: You should use the same password across multiple accounts for better recall.",
                    new List<string> { "True", "False" },
                    1,
                    "Using unique passwords for each account limits damage if one account is compromised."
                ),
                new QuizQuestion(
                    "What is ransomware?",
                    new List<string>
                    {
                        "Software that speeds up your computer",
                        "Malware that locks your files until you pay a ransom",
                        "A type of computer virus",
                        "A tool to recover deleted files"
                    },
                    1,
                    "Ransomware encrypts your files and demands payment for the decryption key. Regular backups help mitigate this threat."
                ),
                new QuizQuestion(
                    "Which of these is a sign of a phishing website?",
                    new List<string>
                    {
                        "HTTPS in the URL",
                        "A padlock icon in the browser",
                        "Misspelled company name",
                        "A privacy policy"
                    },
                    2,
                    "Phishing sites often have subtle errors like misspelled domain names or company names."
                ),
                new QuizQuestion(
                    "What should you do before downloading software from the internet?",
                    new List<string>
                    {
                        "Turn off your antivirus",
                        "Verify the source is legitimate",
                        "Share the download link with friends",
                        "Delete temporary files"
                    },
                    1,
                    "Always verify software comes from a legitimate source to avoid malware. Official websites or trusted app stores are safest."
                ),
                new QuizQuestion(
                    "What is the purpose of software updates?",
                    new List<string>
                    {
                        "To add new features only",
                        "To fix security vulnerabilities",
                        "To collect user data",
                        "To slow down older devices"
                    },
                    1,
                    "Updates often patch security vulnerabilities that could be exploited by attackers."
                )
            };
        }

        public QuizQuestion GetRandomQuestion()
        {
            if (questions.Count > 0)
            {
                int index = random.Next(questions.Count);
                return questions[index];
            }
            return null;
        }

        public bool SubmitAnswer(QuizQuestion question, int selectedOptionIndex)
        {
            questionsAnswered++;
            bool isCorrect = question.CheckAnswer(selectedOptionIndex);
            if (isCorrect)
            {
                currentScore++;
            }
            return isCorrect;
        }

        public int GetCurrentScore() => currentScore;

        public int GetQuestionsAnswered() => questionsAnswered;

        public void ResetQuiz()
        {
            currentScore = 0;
            questionsAnswered = 0;
        }

        public string GetFeedback()
        {
            double percentage = questionsAnswered > 0 ? (double)currentScore / questionsAnswered * 100 : 0;

            if (percentage >= 90)
                return "Excellent! You're a cybersecurity expert!";
            else if (percentage >= 70)
                return "Good job! You have solid cybersecurity knowledge.";
            else if (percentage >= 50)
                return "Not bad, but there's room for improvement.";
            else
                return "You should learn more about cybersecurity to stay safe online.";
        }
    }
}