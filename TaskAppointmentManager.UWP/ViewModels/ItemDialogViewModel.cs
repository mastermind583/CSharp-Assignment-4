using Library.TaskAppointmentManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace TaskAppointmentManager.UWP.ViewModels
{
    public class ItemDialogViewModel : INotifyPropertyChanged
    {
        public Visibility ShowTask
        {
            get
            {
                if (BackingItem is Library.TaskAppointmentManager.Models.Task)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility ShowAppointment
        {
            get
            {
                if (BackingItem is Appointment)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Item BackingItem { get; set; }

        private string itemType;
        public string ItemType
        {
            get
            {
                return itemType;
            }
            set
            {
                if (value != null && !value.Equals(itemType, StringComparison.InvariantCultureIgnoreCase))
                {
                    itemType = value;
                    if (value.Equals("Task", StringComparison.InvariantCultureIgnoreCase))
                    {
                        BackingItem = new Library.TaskAppointmentManager.Models.Task();
                    }
                    else if (value.Equals("Appointment", StringComparison.InvariantCultureIgnoreCase))
                    {
                        BackingItem = new Appointment();
                    }
                    else
                    {
                        BackingItem = null;
                    }
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ShowTask");
                    NotifyPropertyChanged("ShowAppointment");
                }
            }
        }

        public ItemDialogViewModel()
        {
            ItemType = null;
        }

        public ItemDialogViewModel(Item item)
        {
            BackingItem = item;
            if (BackingItem is Library.TaskAppointmentManager.Models.Task)
                ItemType = "Task";
            else if (BackingItem is Appointment)
                ItemType = "Appointment";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}