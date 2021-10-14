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
        private IList<Task> taskList;
        public ItemDialog(IList<Task> itemlist)
        {
            InitializeComponent();
            DataContext = new Task();
            this.taskList = itemlist;
        }

        public ItemDialog(IList<Task> taskList, Task item)
        {
            InitializeComponent();
            DataContext = item;
            this.taskList = taskList;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var itemToEdit = DataContext as Task;
            var i = taskList.IndexOf(itemToEdit);
            if (i >= 0)
            {
                taskList.Remove(itemToEdit);
                taskList.Insert(i, itemToEdit);
            }
            else
                taskList.Add(itemToEdit);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
