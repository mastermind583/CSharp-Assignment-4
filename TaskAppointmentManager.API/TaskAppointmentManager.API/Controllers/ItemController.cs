using Api.TaskAppointmentManager.Persistence;
using Library.TaskAppointmentManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAppointmentManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        [HttpPost("Delete")]
        public void Delete([FromBody] Item todo)
        {
            if (todo is Appointment)
            {
                var item = Database.Appointments.FirstOrDefault(t => t.Id == todo.Id);
                Database.Appointments.Remove(item);
            }
            else
            {
                var item = Database.Tasks.FirstOrDefault(t => t.Id == todo.Id);
                Database.Tasks.Remove(item);
            }
        }

        [HttpPost("Search")]
        public ObservableCollection<Item> Search([FromBody] string Query)
        {
            ObservableCollection<Item> filteredItems;
            ObservableCollection<Item> filteredItems2;
            filteredItems = new ObservableCollection<Item>(Database.Tasks
                .Where(s => (s.Description != null && s.Description.ToUpper().Contains(Query.ToUpper())) ||
                (s.Name != null && s.Name.ToUpper().Contains(Query.ToUpper()))));

            filteredItems2 = new ObservableCollection<Item>(Database.Appointments
                .Where(s => (s.Description != null && s.Description.ToUpper().Contains(Query.ToUpper())) ||
                (s.Name != null && s.Name.ToUpper().Contains(Query.ToUpper())) ||
                (s.Attendees.Any(a => a.ToUpper().Contains(Query.ToUpper()))
                )).ToList());

            foreach (var item in filteredItems2)
                filteredItems.Add(item);

            return filteredItems;
        }
    }
}
