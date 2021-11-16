using Api.TaskAppointmentManager.Persistence;
using Library.TaskAppointmentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.TaskAppointmentManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private object _lock = new object();
        [HttpGet("GetItem")]
        public Item GetTestItem()
        {
            return new Task();
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return Database.Tasks;
        }

        [HttpPost("AddOrUpdate")]
        public Task AddOrUpdate([FromBody] Task task)
        {
            if (task.Id <= 0)
            {
                lock (_lock)
                {
                    int lastUsedId = 0;
                    if (Database.Tasks.Count != 0)
                        lastUsedId = Database.Tasks.Select(a => a.Id).Max();
                    task.Id = lastUsedId + 1;
                    Database.Tasks.Add(task);
                }
            }
            else
            {
                var item = Database.Tasks.FirstOrDefault(t => t.Id == task.Id);
                var index = Database.Tasks.IndexOf(item);
                Database.Tasks.RemoveAt(index);
                Database.Tasks.Insert(index, task);
            }

            return task;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] Task task)
        {
            var item = Database.Tasks.FirstOrDefault(t => t.Id == task.Id);
            var index = Database.Tasks.IndexOf(item);
            Database.Tasks.RemoveAt(index);
        }
    }
}