using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHAT2
{
    public class SecurityTask : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private DateTime? _reminderDate;
        private bool _isCompleted;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public DateTime? ReminderDate
        {
            get => _reminderDate;
            set
            {
                if (_reminderDate != value)
                {
                    _reminderDate = value;
                    OnPropertyChanged(nameof(ReminderDate));
                    OnPropertyChanged(nameof(ReminderText));
                }
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        public string ReminderText
        {
            get
            {
                if (ReminderDate.HasValue)
                {
                    return ReminderDate.Value.ToString("MMM dd, yyyy");
                }
                return "No reminder set";
            }
        }

        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAt { get; } = DateTime.Now;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TaskManager
    {
        private List<SecurityTask> _tasks = new List<SecurityTask>();

        public IReadOnlyList<SecurityTask> Tasks => _tasks.AsReadOnly();

        public void AddTask(string title, string description, DateTime? reminderDate = null)
        {
            var task = new SecurityTask
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate
            };

            _tasks.Add(task);
        }

        public void RemoveTask(Guid taskId)
        {
            var taskToRemove = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (taskToRemove != null)
            {
                _tasks.Remove(taskToRemove);
            }
        }

        public void CompleteTask(Guid taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.IsCompleted = true;
            }
        }

        public List<SecurityTask> GetDueReminders()
        {
            return _tasks.Where(t =>
                !t.IsCompleted &&
                t.ReminderDate.HasValue &&
                t.ReminderDate.Value.Date <= DateTime.Now.Date)
                .ToList();
        }
    }
}