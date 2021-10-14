using Library.TaskAppointmentManager.Models;
using Library.TaskAppointmentManager.UWP.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.TaskAppointmentManager.ViewModels
{ 
    public class MainViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Models.Task> Items { get; set; }

        //temporary list just for searching?
        //public ObservableCollection<Models.Task> SearchItems { get; set; }
        public Models.Task SelectedItem { get; set; }
        public string Query { get; set; }

        //private string persistencePath;
        //private JsonSerializerSettings serializationSettings;
        public MainViewModel()
        {
            Items = new ObservableCollection<Models.Task>();
            //persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            //serializationSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            //var myItems = JsonConvert.SerializeObject(this, serializationSettings);
            //File.WriteAllText($"{persistencePath}\\SaveData.txt", myItems);
            //var myItemDeserialized = JsonConvert.DeserializeObject<Item>(myItems, serializationSettings);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DeleteItem()
        {
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
            }
        }

        public async System.Threading.Tasks.Task EditItem()
        {
            if (SelectedItem != null)
            {
                var diag = new ItemDialog(Items, SelectedItem);
                NotifyPropertyChanged("SelectedItem");
                await diag.ShowAsync();
            }
        }

        public async System.Threading.Tasks.Task Search()
        {
            var searchList = Items.Where(i => i.Name.Contains(Query) || i.Description.Contains(Query));    
            Console.WriteLine(Query);
        }
    }
}
