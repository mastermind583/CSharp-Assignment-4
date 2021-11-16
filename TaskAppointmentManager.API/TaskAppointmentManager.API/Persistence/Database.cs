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
        };
        public static ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>
        {
        };
    }
}