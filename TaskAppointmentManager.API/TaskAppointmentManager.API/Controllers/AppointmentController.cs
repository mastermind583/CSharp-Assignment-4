using Api.TaskAppointmentManager.Persistence;
using Library.TaskAppointmentManager.Models;
using Library.TaskAppointmentManager.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.TaskAppointmentManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private object _lock = new object();
        [HttpGet("GetItem")]
        public Item GetTestItem()
        {
            return new Appointment();
        }

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return Database.Appointments;
        }

        [HttpPost("AddOrUpdate")]
        public Appointment AddOrUpdate([FromBody] Appointment appointment)
        {
            if (appointment.Id <= 0)
            {
                lock (_lock)
                {
                    int lastUsedId = 0;
                    if (Database.Appointments.Count != 0)
                        lastUsedId = Database.Appointments.Select(a => a.Id).Max();
                    appointment.Id = lastUsedId + 1;
                    Database.Appointments.Add(appointment);
                }
            }
            else
            {
                var item = Database.Appointments.FirstOrDefault(t => t.Id == appointment.Id);
                var index = Database.Appointments.IndexOf(item);
                Database.Appointments.RemoveAt(index);
                Database.Appointments.Insert(index, appointment);
            }

            return appointment;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] Appointment appointment)
        {
            var item = Database.Appointments.FirstOrDefault(t => t.Id == appointment.Id);
            var index = Database.Appointments.IndexOf(item);
            Database.Appointments.RemoveAt(index);
        }
    }
}