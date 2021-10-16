using Library.TaskAppointmentManager.Models;
using TaskAppointmentManager.UWP.Dialogs;
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

namespace TaskAppointmentManager.ViewModels
{ 
    public class MainViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; set; }
        public Item SelectedItem { get; set; }
        private ObservableCollection<Item> filteredItems;
        public ObservableCollection<Item> FilteredItems
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Query))
                {
                    return Items;
                }
                else
                {
                    //CHECK! search for attendees too?
                    filteredItems = new ObservableCollection<Item>(Items
                        .Where(s => s.Description.ToUpper().Contains(Query.ToUpper())
                        || s.Name.ToUpper().Contains(Query.ToUpper())).ToList());
                    return filteredItems;
                }
            }
        }
        public string Query { get; set; }

        //private string persistencePath;
        //private JsonSerializerSettings serializationSettings;
        public MainViewModel()
        {
            Items = new ObservableCollection<Item>();
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

        public void RefreshList()
        {
            NotifyPropertyChanged("FilteredTickets");
        }
    }
}
