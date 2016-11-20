using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PokemonQuizXAML
{
    using System.ComponentModel;
    using Windows.UI.Popups;

    class LoadingPageClass :INotifyPropertyChanged
    {
        public string ProgressLabel { get; private set; }
        public PokemonHolder PokemonHolder { get; private set; }
        public int ProgressBarValue { get; private set; }
        public string Manual { get; private set; }

        public async void LoadFiles()
        {
            Manual = "Loading... Please Wait";
            OnPropertyChange("Manual");
            ProgressLabel = "Waiting for pokemon's data";
            OnPropertyChange("ProgressLabel");
            ProgressBarValue = 0;
            OnPropertyChange("ProgressBarValue");

            try
            {
                PokemonHolder = new PokemonHolder(new Random());
            }
            catch (NoPokemonException ex)
            {
                MessageDialog dialog = new MessageDialog("Failure", ex.Message);
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
                ProgressLabel = "Can't get pokemon's data";
                OnPropertyChange("ProgressLabel");
            }

            ProgressBarValue = 100;
            OnPropertyChange("ProgressBarValue");
            ProgressLabel = "Data are correct. ";
            OnPropertyChange("ProgressLabel");
            Manual = "Press ENTER to continue";
            OnPropertyChange("Manual");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
