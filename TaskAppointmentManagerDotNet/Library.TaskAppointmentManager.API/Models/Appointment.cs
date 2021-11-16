using Library.TaskAppointmentManager.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Library.TaskAppointmentManager.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class Appointment : Item
    {
        public Appointment() : base()
        {
            Attendees = new List<string>();
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<string> Attendees { get; set; }
        public override string ToString()
        {
            string attendees = null;
            for (int i = 0; i < Attendees.Count; i++)
            {
                attendees += Attendees[i];
                if (i != Attendees.Count - 1)
                    attendees += ", ";
            }
            return $"APPOINTMENT {Id} - {Name} - " +
                $"{Description} - Priority: {Priority} - Start Date: {Start.Date:MM-dd-yyyy} - " +
                $"End Date: {End.Date:MM-dd-yyyy} - Attendees: {attendees}";
        }
    }
}
