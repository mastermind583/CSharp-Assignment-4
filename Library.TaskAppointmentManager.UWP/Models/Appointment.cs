using System;
using System.Collections.Generic;

namespace Library.TaskAppointmentManager.Models
{
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
            return $"APPOINTMENT - NAME: {Name} - " +
                $"DESCRIPTION: {Description} - PRIORITY: {Priority} - START DATE: {Start.Date:MM-dd-yyyy} - " +
                $"END DATE: {End.Date:MM-dd-yyyy} - ATTENDEES: {attendees}";
        }
    }
}
