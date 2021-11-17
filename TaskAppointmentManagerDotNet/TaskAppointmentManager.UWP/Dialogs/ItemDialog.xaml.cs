using Library.TaskAppointmentManager.Models;
using TaskAppointmentManager.UWP.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Library.TaskAppointmentManager.Communication;
using Newtonsoft.Json;
using System.Linq;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace TaskAppointmentManager.UWP.Dialogs
{
    public sealed partial class ItemDialog : ContentDialog
    {
        private IList<Item> itemList;
        public ItemDialog(IList<Item> itemlist)
        {
            InitializeComponent();
            DataContext = new ItemDialogViewModel();
            this.itemList = itemlist;
        }

        public ItemDialog(IList<Item> itemList, Item item)
        {
            InitializeComponent();
            DataContext = new ItemDialogViewModel(item);
            this.itemList = itemList;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var itemToEdit = (DataContext as ItemDialogViewModel)?.BackingItem;
            
            //Converts the string of words to list items
            if (itemToEdit is Appointment && (DataContext as ItemDialogViewModel)?.AppointmentAttendees != null)
            {
                (itemToEdit as Appointment).Attendees.Clear();
                string[] words = (DataContext as ItemDialogViewModel)?.AppointmentAttendees.Split(',');
                foreach (var word in words)
                {
                    string s = word.Trim();
                    (itemToEdit as Appointment).Attendees.Add(s);
                }
            }

            //Set Id in the server, send back the item
            Item serverItem;
            if (itemToEdit is Task)
            {
                var returnString = await new WebRequestHandler().Post("http://localhost:3916/Task/AddOrUpdate", itemToEdit);
                serverItem = JsonConvert.DeserializeObject<Task>(returnString);
            }
            else
            {
                var returnString = await new WebRequestHandler().Post("http://localhost:3916/Appointment/AddOrUpdate", itemToEdit);
                serverItem = JsonConvert.DeserializeObject<Appointment>(returnString);
            }

            //Find the item in itemlist (itemToEdit may be from ServerSearchItems instead of Items)
            Item item;
            if (itemToEdit is Appointment)
            {
                item = itemList.FirstOrDefault(a => a is Appointment && a.Id == serverItem.Id);
            }
            else
            {
                item = itemList.FirstOrDefault(a => a is Task && a.Id == serverItem.Id);
            }

            var i = itemList.IndexOf(item);
            if (i >= 0)
            {
                itemList.Remove(item);
                itemList.Insert(i, serverItem);
            }
            else
                itemList.Add(serverItem);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
