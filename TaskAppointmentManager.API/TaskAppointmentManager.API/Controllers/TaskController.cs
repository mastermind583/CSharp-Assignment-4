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
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;


        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return Database.ToDos;
        }

        [HttpGet("GetItem")]
        public Item GetTestItem()
        {
            return new Task();
        }

        [HttpPost("AddOrUpdate")]
        public Item Receive([FromBody] Task task)
        {
            return task;
        }
    }
}