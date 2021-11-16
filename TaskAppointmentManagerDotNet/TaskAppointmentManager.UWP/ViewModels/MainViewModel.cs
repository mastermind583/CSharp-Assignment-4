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
using Library.TaskAppointmentManager.Communication;

namespace TaskAppointmentManager.UWP.ViewModels
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
                //Show all items that contain the query
                if (!string.IsNullOrWhiteSpace(Query) && show_incomplete == false)
                {
                    filteredItems = new ObservableCollection<Item>(Items
                        .Where(s => (s.Description != null && s.Description.ToUpper().Contains(Query.ToUpper())) ||
                        (s.Name != null && s.Name.ToUpper().Contains(Query.ToUpper())) ||
                        ((s is Appointment) && (s as Appointment).Attendees.Any(a => a.ToUpper().Contains(Query.ToUpper()))
                        )).ToList());
                }

                //Show incomplete tasks when there is no query
                else if (show_incomplete == true && string.IsNullOrWhiteSpace(Query))
                {
                    filteredItems = new ObservableCollection<Item>(Items
                        .Where(s => (s is Library.TaskAppointmentManager.Models.Task) &&
                        (s as Library.TaskAppointmentManager.Models.Task).IsCompleted == false).ToList());
                }

                //Show incomplete tasks that contain the query
                else if (show_incomplete == true && !string.IsNullOrWhiteSpace(Query))
                {
                    filteredItems = new ObservableCollection<Item>(Items
                        .Where(s => (s is Library.TaskAppointmentManager.Models.Task) &&
                        (s as Library.TaskAppointmentManager.Models.Task).IsCompleted == false &&
                        (s.Description != null && s.Description.ToUpper().Contains(Query.ToUpper()) ||
                        (s.Name != null && s.Name.ToUpper().Contains(Query.ToUpper())))
                        ).ToList());
                }

                //If the sort is selected and there is no query, sort Items by priority and return so it doesn't sort again below this
                else if (priority_sort == true)
                {
                    filteredItems = new ObservableCollection<Item>(Items.OrderByDescending(p => p.Priority).ToList());
                    return filteredItems;
                }

                //If search is clicked with no query, and no buttons are pushed, just return the Items list
                else
                    return Items;

                //If the list was filtered and sort is selected, sort filteredItems by priority
                if (priority_sort == true)
                    filteredItems = new ObservableCollection<Item>(filteredItems.OrderByDescending(p => p.Priority).ToList());

                return filteredItems;
            }
        }

        public string Query { get; set; }

        private bool show_incomplete = false;
        private bool priority_sort = false;

        public MainViewModel()
        {
            Items = new ObservableCollection<Item>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async System.Threading.Tasks.Task AddItem()
        {
            var diag = new ItemDialog(Items);
            await diag.ShowAsync();
            NotifyPropertyChanged("FilteredItems");
        }

        public async void DeleteItem()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem is Library.TaskAppointmentManager.Models.Task)
                    await new WebRequestHandler().Post("http://localhost:3916/Task/Delete", SelectedItem);
                else
                    await new WebRequestHandler().Post("http://localhost:3916/Appointment/Delete", SelectedItem);

                Items.Remove(SelectedItem);
                NotifyPropertyChanged("FilteredItems");
            }
        }

        public async System.Threading.Tasks.Task EditItem()
        {
            if (SelectedItem != null)
            {
                var diag = new ItemDialog(Items, SelectedItem);
                NotifyPropertyChanged("SelectedItem");
                await diag.ShowAsync();
                NotifyPropertyChanged("FilteredItems");
            }
        }

        public async System.Threading.Tasks.Task SaveOrLoadItem()
        {
            var diag = new SaveLoad(Items);
            await diag.ShowAsync();
            NotifyPropertyChanged("FilteredItems");
        }

        public void RefreshList()
        {           
            NotifyPropertyChanged("FilteredItems");
        }

        public void ShowIncompleteTasks()
        {
            //change to the opposite value
            if (show_incomplete == false)
                show_incomplete = true;
            else
                show_incomplete = false;
            NotifyPropertyChanged("FilteredItems");
        }

        public void SortByPriority()
        {
            //change to the opposite value
            if (priority_sort == false)
                priority_sort = true;
            else
                priority_sort = false;
            NotifyPropertyChanged("FilteredItems");
        }
    }
}
