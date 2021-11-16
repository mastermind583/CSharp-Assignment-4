using Library.TaskAppointmentManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.TaskAppointmentManager.Persistence
{
    public static class Database
    {
        public static ObservableCollection<Library.TaskAppointmentManager.Models.Task> Tasks { get; set; } = new ObservableCollection<Library.TaskAppointmentManager.Models.Task> 
        {
            new Library.TaskAppointmentManager.Models.Task { Name = "First",
                       Description = "First ToDo"},
            new Library.TaskAppointmentManager.Models.Task { Name = "Second",
                       Description = "Second ToDo"}
        };
        public static ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>
        {
            new Appointment { Name = "1st",
                       Description = "First Appointment"},
            new Appointment { Name = "2nd",
                       Description = "Second Appointment"}
        };
    }
}