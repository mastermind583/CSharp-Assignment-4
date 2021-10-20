using System;

namespace Library.TaskAppointmentManager.Models
{
    public class Task : Item
    {
        public Task() : base()
        {
        }

        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public override string ToString()
        {
            return $"TASK - {Name} - {Description} - {Id} - {Priority} - {Deadline.Date:MM-dd-yyyy}";
        }
    }
}