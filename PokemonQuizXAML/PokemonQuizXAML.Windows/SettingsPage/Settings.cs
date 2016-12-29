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
            try
            {
                LoadSettings();
            }
            catch (FileNotFoundException)
            {
                ShowErrorDialog();
                return;
            }
            OnPropertyChanged(HiddenPath);
            OnPropertyChanged(ShownPath);
            OnPropertyChanged(DefaultPath);
        }

        public async void ShowErrorDialog()
        {
            MessageDialog dialog = new MessageDialog("You have to choose folders' path to pokemon. ");
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
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }
            IList<string> paths = await FileIO.ReadLinesAsync(settingsFile);
            hiddenPath = paths[0];
            shownPath = paths[1];
        }
        
        public async void BrowseFolderWithHiddenPokemon()
        {
            FolderPicker folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            IStorageFolder hpf= await folderPicker.PickSingleFolderAsync();
            hiddenPath = hpf.Path;
            OnPropertyChanged(HiddenPath);
        }

        public async void BrowseFolderWithShownPokemon()
        {
            FolderPicker folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            IStorageFolder hpf = await folderPicker.PickSingleFolderAsync();
            shownPath = hpf.Path;
            OnPropertyChanged(ShownPath);
        }

        public async void SaveSettings()
        {
            IStorageFolder localFolder = ApplicationData.Current.LocalFolder;
            IStorageFile settingsFile = await localFolder.CreateFileAsync("settings.txt");
            await FileIO.WriteTextAsync(settingsFile, hiddenPath + '\n');
            await FileIO.WriteTextAsync(settingsFile, shownPath);
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
