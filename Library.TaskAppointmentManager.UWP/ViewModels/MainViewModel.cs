using Library.TaskAppointmentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskAppointmentManager.ViewModels
{ 
    public class MainViewModel
    {
        public List<Item> Items { get; set; }
        public Item SelectedItem { get; set; }
        public string Query { get; set; }
        private string persistencePath;

        public MainViewModel()
        {
            Items = new List<Item>();
            persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public void AddItem()
        {
            if (SelectedItem == null)
            {
                Items.Add(new Item());
            }
        }

        public void DeleteItem()
        {
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
            }
        }
    }
}
