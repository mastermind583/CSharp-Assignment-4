using Library.TaskAppointmentManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppointmentManager.UWP.ViewModels
{
    public class SaveLoadViewModel : INotifyPropertyChanged
    {
        public string SaveName { get; set; }
        public string LoadName { get; set; }
        public bool FileNotFound
        {
            get
            {
                if (File.Exists($"{path}\\{LoadName}.json"))
                    return true;
                else
                    return false;
            }
        }

        private string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public SaveLoadViewModel()
        {
        }

        public void SaveList(IList<Item> itemList)
        {
            File.WriteAllText($"{path}\\{SaveName}.json", JsonConvert.SerializeObject(itemList, settings));
        }

        public void LoadList(IList<Item> itemList)
        {
            if (File.Exists($"{path}\\{LoadName}.json"))
            {              
                var items = JsonConvert.DeserializeObject<IList<Item>>(File.ReadAllText($"{path}\\{LoadName}.json"), settings);
                itemList.Clear();
                foreach (var item in items)
                    itemList.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
