using System;

namespace Library.TaskAppointmentManager.Models
{
    public class Item
    {
        private static int currentId = 1;

        public Item()
        {
            Id = currentId++;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}
