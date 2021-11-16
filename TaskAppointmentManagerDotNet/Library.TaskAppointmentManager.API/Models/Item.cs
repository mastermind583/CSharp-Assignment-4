using Library.TaskAppointmentManager.Persistence;
using Newtonsoft.Json;
using System;


namespace Library.TaskAppointmentManager.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class Item
    {
        public Item()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}
