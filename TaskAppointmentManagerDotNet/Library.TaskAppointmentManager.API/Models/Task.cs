using Library.TaskAppointmentManager.Persistence;
using Newtonsoft.Json;
using System;

namespace Library.TaskAppointmentManager.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class Task : Item
    {
        public Task() : base()
        {
        }

        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public override string ToString()
        {
            string s1 = IsCompleted ? "Completed" : "Incomplete";
            return $"TASK {Id} - {Name} - {Description} - Priority: {Priority} - Deadline: {Deadline.Date:MM-dd-yyyy} - {s1}";
        }
    }
}