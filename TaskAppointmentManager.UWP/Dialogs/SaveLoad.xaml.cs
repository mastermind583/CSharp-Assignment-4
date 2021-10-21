using Library.TaskAppointmentManager.Models;
using Newtonsoft.Json;
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

namespace TaskAppointmentManager.UWP.Dialogs
{
    public sealed partial class SaveLoad : ContentDialog
    {

        public string SaveName { get; set; }
        public string LoadName { get; set; }
        
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        private IList<Item> itemList;
        public SaveLoad(IList<Item> itemList)
        {
            this.InitializeComponent();
            this.itemList = itemList;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            File.WriteAllText($"{path}\\SaveData.json", JsonConvert.SerializeObject(itemList, settings));
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (File.Exists($"{path}\\" + LoadName + ".json"))
            {
                itemList = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText("SaveData.json"), settings);
                Console.WriteLine("\nList successfully loaded.");
            }
        }
    }
}
