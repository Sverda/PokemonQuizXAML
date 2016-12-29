using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.ComponentModel;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace PokemonQuizXAML.SettingsPage
{
    class Settings :INotifyPropertyChanged
    {
        private string hiddenPath;
        private string shownPath;
        private string defaultPath;

        public string HiddenPath { get { return hiddenPath; } }
        public string ShownPath { get { return shownPath; } }
        public string DefaultPath { get { return defaultPath; } }


        public Settings()
        {
            LoadSettings();
        }

        private async void showErrorDialog(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }

        public async void LoadSettings()
        {
            IStorageFolder localFolder = ApplicationData.Current.LocalFolder;
            defaultPath = localFolder.Path;
            IStorageFile settingsFile;
            try
            {
                settingsFile = await localFolder.GetFileAsync("settings.txt");
                IList<string> paths = await FileIO.ReadLinesAsync(settingsFile);
                hiddenPath = paths[0];
                shownPath = paths[1];
            }
            catch (FileNotFoundException)
            {
                settingsFile = await localFolder.CreateFileAsync("settings.txt");
                showErrorDialog("You have to choose folders with pokemon pictures. ");
            }
            catch (ArgumentOutOfRangeException)
            {
                hiddenPath = "";
                shownPath = "";
            }
            OnPropertyChanged("HiddenPath");
            OnPropertyChanged("ShownPath");
        }
        
        public async void BrowseFolderWithHiddenPokemon()
        {
            FolderPicker folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            folderPicker.FileTypeFilter.Add(".txt");
            IStorageFolder hpf= await folderPicker.PickSingleFolderAsync();
            if (hpf == null)
            {
                if (String.IsNullOrEmpty(hiddenPath))
                {
                    showErrorDialog("You have to choose folders with blank pokemon pictures. ");
                }
                return;
            }
            hiddenPath = hpf.Path;
            OnPropertyChanged("HiddenPath");
        }

        public async void BrowseFolderWithShownPokemon()
        {
            FolderPicker folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            folderPicker.FileTypeFilter.Add(".txt");
            IStorageFolder hpf = await folderPicker.PickSingleFolderAsync();
            if (hpf == null)
            {
                showErrorDialog("You have to choose folder with no blank pokemon pictures. ");
                shownPath = defaultPath;
            }
            else
            {
                shownPath = hpf.Path;
            }
            OnPropertyChanged("ShownPath");
        }

        public async void SaveSettings()
        {
            IStorageFolder localFolder = ApplicationData.Current.LocalFolder;
            IStorageFile settingsFile;
            try
            {
                settingsFile = await localFolder.CreateFileAsync("settings.txt");
            }
            catch (Exception)
            {
                settingsFile = await localFolder.GetFileAsync("settings.txt");
            }
            List<string> list = new List<string>();
            if (!String.IsNullOrEmpty(hiddenPath))
            {
                list.Add(hiddenPath);
            }
            if (!String.IsNullOrEmpty(shownPath))
            {
                list.Add(shownPath);
            }
            if (list.Count != 2)
            {
                showErrorDialog("There should be two folder to load pokemon");
                for (int i = 0; i < list.Count; i++)
                {
                    list.RemoveAt(i);
                }
                list.Add(defaultPath);
                list.Add(defaultPath);
            }
            await FileIO.WriteLinesAsync(settingsFile, list);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
