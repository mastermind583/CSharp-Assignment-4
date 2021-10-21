using Library.TaskAppointmentManager.Models;
using TaskAppointmentManager.UWP.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

//Date time stuff, oct 18th lecture
//Might have to change item.cs id stuff

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

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var itemToEdit = (DataContext as ItemDialogViewModel)?.BackingItem;

            //Converts the string of words to list items
            if (itemToEdit is Appointment && (DataContext as ItemDialogViewModel)?.AppointmentAttendees != null)
            {
                string[] words = (DataContext as ItemDialogViewModel)?.AppointmentAttendees.Split(',');
                foreach (var word in words)
                {
                    string s = word.Trim();
                    (itemToEdit as Appointment).Attendees.Add(s);
                }
            }

            //sets the id to currentId++ if it is a new ID
            if (itemToEdit.Id == 0)
            {
                itemToEdit.Id = 0;
            }
                
            var i = itemList.IndexOf(itemToEdit);
            if (i >= 0)
            {
                itemList.Remove(itemToEdit);
                itemList.Insert(i, itemToEdit);
            }
            else
                itemList.Add(itemToEdit);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
