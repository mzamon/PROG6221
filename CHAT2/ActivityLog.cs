using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT2
{
    public class ActivityLog
    {
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public LogEntryType Type { get; set; }

        // Fixing CS1520: Adding a return type to the constructor method
        public ActivityLog(string description, LogEntryType type = LogEntryType.Info)
        {
            Description = description;
            Timestamp = DateTime.Now;
            Type = type;
        }

        public override string ToString()
        {
            return $"[{Timestamp.ToString("MMM dd, HH:mm")}] {Description}";
        }
    }

    public enum LogEntryType
    {
        Info,
        TaskAdded,
        TaskCompleted,
        TaskDeleted,
        ReminderSet,
        QuizStarted,
        QuizCompleted,
        Error
    }

    public class ActivityLogger
    {
        private readonly List<LogEntry> _logEntries = new List<LogEntry>();
        private const int MaxDisplayEntries = 10;

        public void AddEntry(string description, LogEntryType type = LogEntryType.Info)
        {
            _logEntries.Add(new LogEntry(description, type));
        }

        public void LogTaskAdded(string taskTitle)
        {
            AddEntry($"Task added: '{taskTitle}'.", LogEntryType.TaskAdded);
        }

        public void LogTaskCompleted(string taskTitle)
        {
            AddEntry($"Task completed: '{taskTitle}'.", LogEntryType.TaskCompleted);
        }

        public void LogTaskDeleted(string taskTitle)
        {
            AddEntry($"Task deleted: '{taskTitle}'.", LogEntryType.TaskDeleted);
        }

        public void LogReminderSet(string taskTitle, DateTime reminderDate)
        {
            AddEntry($"Reminder set: '{taskTitle}' on {reminderDate.ToString("MMM dd, yyyy")}.", LogEntryType.ReminderSet);
        }

        public void LogQuizStarted()
        {
            AddEntry("Cybersecurity quiz started.", LogEntryType.QuizStarted);
        }

        public void LogQuizCompleted(int score, int total)
        {
            AddEntry($"Quiz completed with score: {score}/{total}.", LogEntryType.QuizCompleted);
        }

        public void LogError(string errorMessage)
        {
            AddEntry($"Error: {errorMessage}", LogEntryType.Error);
        }

        public List<string> GetRecentLogs(int count = MaxDisplayEntries)
        {
            return _logEntries
                .OrderByDescending(e => e.Timestamp)
                .Take(count)
                .Select(e => e.ToString())
                .ToList();
        }
    }

    internal class LogEntry
    {
        public DateTime Timestamp { get; private set; }
        private string description;
        private LogEntryType type;

        public LogEntry(string description, LogEntryType type)
        {
            this.description = description;
            this.type = type;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Timestamp.ToString("MMM dd, HH:mm")}] {description}";
        }
    }
}
