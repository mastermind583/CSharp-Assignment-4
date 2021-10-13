using Library.TaskAppointmentManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Library.TaskAppointmentManager.UWP.Dialogs
{
    public sealed partial class ItemDialog : ContentDialog
    {
        private IList<Item> itemList;
        public ItemDialog(IList<Item> itemlist)
        {
            InitializeComponent();
            DataContext = new Item();
            this.itemList = itemlist;
        }

        public ItemDialog(IList<Item> itemlist, Item item)
        {
            InitializeComponent();
            DataContext = item;
            this.itemList = itemlist;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var itemToEdit = DataContext as Item;
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
