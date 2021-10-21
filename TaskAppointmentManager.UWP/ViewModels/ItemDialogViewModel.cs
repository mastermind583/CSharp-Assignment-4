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

        public bool IsClickable
        {
            get
            {
                if (BackingItem != null && BackingItem.Id > 0)
                    return false;
                else
                    return true;
            }
        }

        private DateTimeOffset taskDeadline;
        public DateTimeOffset TaskDeadline
        {
            get
            {
                return taskDeadline;
            }
            set
            {
                taskDeadline = value;
                if (BackingItem is Library.TaskAppointmentManager.Models.Task)
                {
                    (BackingItem as Library.TaskAppointmentManager.Models.Task).Deadline = taskDeadline.Date;
                    NotifyPropertyChanged("BackingItem");
                }
            }
        }
        private DateTimeOffset appointmentStart;
        public DateTimeOffset AppointmentStart
        {
            get
            {
                return appointmentStart;
            }
            set
            {
                appointmentStart = value;
                if (BackingItem is Appointment)
                {
                    (BackingItem as Appointment).Start = appointmentStart.Date;
                    NotifyPropertyChanged("BackingItem");
                }
            }
        }

        private DateTimeOffset appointmentEnd;
        public DateTimeOffset AppointmentEnd
        {
            get
            {
                return appointmentEnd;
            }
            set
            {
                appointmentEnd = value;
                if (BackingItem is Appointment)
                {
                    (BackingItem as Appointment).End = appointmentEnd.Date;
                    NotifyPropertyChanged("BackingItem");
                }
            }
        }

        public string AppointmentAttendees { get; set; }

        private bool istaskCompleted;
        public bool IsTaskCompleted
        {
            get
            {
                return istaskCompleted;
            }
            set
            {
                istaskCompleted = value;
                if (BackingItem is Library.TaskAppointmentManager.Models.Task)
                {
                    (BackingItem as Library.TaskAppointmentManager.Models.Task).IsCompleted = istaskCompleted;
                    NotifyPropertyChanged("BackingItem");
                }
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
                if (BackingItem != null && BackingItem.Id > 0)
                {
                    NotifyPropertyChanged("IsClickable");
                    return;
                }

                if (value != null && !value.Equals(itemType, StringComparison.InvariantCultureIgnoreCase))
                {
                    itemType = value;
                    if (value.Equals("Task", StringComparison.InvariantCultureIgnoreCase))
                    {
                        BackingItem = new Library.TaskAppointmentManager.Models.Task();
                        (BackingItem as Library.TaskAppointmentManager.Models.Task).Deadline = taskDeadline.Date;
                        (BackingItem as Library.TaskAppointmentManager.Models.Task).IsCompleted = istaskCompleted;
                        BackingItem.Priority = 1;
                    }

                    else if (value.Equals("Appointment", StringComparison.InvariantCultureIgnoreCase))
                    {
                        BackingItem = new Appointment();
                        (BackingItem as Appointment).Start = appointmentStart.Date;
                        (BackingItem as Appointment).End = appointmentEnd.Date;
                        BackingItem.Priority = 1;
                    }

                    else
                        BackingItem = null;

                    NotifyPropertyChanged();
                    NotifyPropertyChanged("BackingItem");
                    NotifyPropertyChanged("ShowTask");
                    NotifyPropertyChanged("ShowAppointment");
                }
            }
        }

        public ItemDialogViewModel()
        {
            ItemType = null;
            TaskDeadline = DateTime.Now;
            AppointmentStart = DateTime.Now;
            AppointmentEnd = DateTime.Now;
        }

        public ItemDialogViewModel(Item item)
        {
            BackingItem = item;
            if (BackingItem is Library.TaskAppointmentManager.Models.Task)
            {
                ItemType = "Task";
                TaskDeadline = (BackingItem as Library.TaskAppointmentManager.Models.Task).Deadline;
            }

            else if (BackingItem is Appointment)
            {
                ItemType = "Appointment";
                AppointmentStart = (BackingItem as Appointment).Start;
                AppointmentEnd = (BackingItem as Appointment).End;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}