using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3.Models
{
    public class Task
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsCompleted { get; set; }
        public TaskStatus TaskState { get; set; }
        public TaskCategory Category { get; set; }
        public TimeSpan Timer
        {
            get; set;
        }
        public enum TaskStatus
        {
            InProgress,
            Completed,
            NotStarted,
            Late,
            Archieved,
            Deleted,
        }
        public enum TaskCategory
        {
            Work,
            Personal,
            Urgent,
            Important,
            Optional,
        }
    }
}