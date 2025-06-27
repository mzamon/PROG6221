──────────────────────────────────────────────────────
CYBERSECURITY AWARENESS CHATBOT
──────────────────────────────────────────────────────

Developer: Richmond Mzamo Ndlovu  
Submission Date: June 2025  
Course: Programming 2A (PROG6221)  
Institution: The Independent Institute of Education (IIE)

──────────────────────────────────────────────────────
PROJECT OVERVIEW
──────────────────────────────────────────────────────
This is a GUI-based cybersecurity chatbot developed in C# using WPF and Material Design.  
The chatbot promotes cybersecurity awareness through interactive conversations, reminders,  
task management, and a quiz game. It also simulates natural language input and sentiment detection.

This chatbot integrates features from Part 1, Part 2, and Part 3 of the Portfolio of Evidence brief.

──────────────────────────────────────────────────────
HOW TO RUN THE APPLICATION
──────────────────────────────────────────────────────
1. Open the solution in Visual Studio 2022 or later.
2. Ensure the following files are present in the root directory:
   - Greeting.wav       (voice greeting played on startup)
   - AsciiLogo.bmp      (displays ASCII art at launch)
3. Build and run the solution.
4. After the ASCII greeting, the chatbot will prompt for user input.

Requirements:
- .NET Framework 4.7.2 or later
- MaterialDesignInXAML toolkit (included via NuGet)

──────────────────────────────────────────────────────
KEY FEATURES
──────────────────────────────────────────────────────
- ASCII Art + Voice Greeting (Part 1)
- Personalised chatbot memory (remembers name and interests)
- Dynamic chatbot replies with variation (50+ prewritten)
- Sentiment detection with 40+ emotional phrases supported
- Keyword recognition for topics like password, scam, and privacy
- Task Assistant (add, view, complete, delete tasks)
- Reminders with date/time support
- Cybersecurity quiz game (with scoring, feedback, leaderboard)
- NLP simulation (flexible input interpretation)
- Activity logging (records and retrieves recent actions)
- All chatbot messages displayed via a unified chat window
- Structured conversation format: "User: ..." / "Bot: ..."
- Auto-saving of logs on feature use and exit
- Full GUI layout using Material Design cards, buttons, and sidebar

──────────────────────────────────────────────────────
TEST DATA SAMPLES
──────────────────────────────────────────────────────
User: My name is Mpho  
User: What is phishing?  
User: Add task - Enable 2FA  
User: Yes, remind me in 3 days  
User: I'm worried about online scams  
User: Tell me about passwords  
User: complete - Enable 2FA  
User: What have you done for me?  
User: Start quiz

──────────────────────────────────────────────────────
FILE STRUCTURE
──────────────────────────────────────────────────────
MainWindow.xaml           - GUI layout  
MainWindow.xaml.cs        - Core logic and chatbot functionality  
Greeting.wav              - Startup voice message  
AsciiLogo.bmp             - Bitmap file for ASCII art  
ChatLog_*.txt             - Saved logs (generated automatically)  
README.txt                - Project overview and setup instructions

──────────────────────────────────────────────────────
NOTES
──────────────────────────────────────────────────────
This chatbot meets all technical and functional requirements of the POE brief,  
including Parts 1, 2, and 3. It has been tested for both usability and error handling,  
and includes full integration of chatbot logic with an intuitive GUI.

Thank you for reviewing this project.
