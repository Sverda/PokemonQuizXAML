using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;

namespace PokemonQuizXAML
{
    class PokemonHolder
    {
        private List<Pokemon> PokemonsData;
        private Random random;

        public PokemonHolder(Random random)
        {
            this.random = random;
            PokemonsData = new List<Pokemon>();
            //LoadPokemonsAsync();
        }


        private Pokemon ditto;
        public Pokemon Ditto
        {
            get
            {
                if (ditto != null)
                    return ditto;
                else
                    throw new NoPokemonException("Can't load Ditto from files. ");
            }
        }
        private void getDittoFromList()
        {
            foreach (Pokemon pokemon in PokemonsData)
            {
                if (pokemon.Name == "Ditto")
                {
                    ditto = pokemon;
                    PokemonsData.RemoveAt(PokemonsData.IndexOf(pokemon));
                    return;
                }
            }
        }

        public async void LoadPokemonsAsync()
        {
            //get folders' name from settings.txt file
            IStorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile settingFile = null;
            IList<string> foldersList = null;
            string hiddenFolder;
            string shownFolder;
            StorageFolder pokemonShowFolder = null;
            StorageFolder pokemonBlankFolder = null;
            try
            {
                settingFile = await localFolder.GetFileAsync("settings.txt");
                foldersList = await FileIO.ReadLinesAsync(settingFile);
                hiddenFolder = foldersList[0];
                shownFolder = foldersList[1];
                pokemonShowFolder = await StorageFolder.GetFolderFromPathAsync(shownFolder);
                pokemonBlankFolder = await StorageFolder.GetFolderFromPathAsync(hiddenFolder);
            }
            catch (FileNotFoundException)
            {
                OnFileNotFoundEvent();
                return;
            }

            IReadOnlyList<IStorageFile> pokemonShowFiles = await pokemonShowFolder.GetFilesAsync();
            IReadOnlyList<IStorageFile> pokemonBlankFiles = await pokemonBlankFolder.GetFilesAsync();

            string name;
            string blankPath;
            string showPath;
            for (int i = 0; i < pokemonBlankFiles.Count; i++)
            {
                string[] splitName = pokemonBlankFiles[i].Name.Split(new char[] { '.', '\\' });

                name = splitName[splitName.Length - 2];
                blankPath = pokemonBlankFiles[i].Path;
                showPath = pokemonShowFiles[i].Path;

                PokemonsData.Add(new Pokemon(name, blankPath, showPath));
            }
            if (PokemonsData == null)
                throw new NoPokemonException();
            getDittoFromList();
        }

        public Pokemon RandomPokemon()
        {
            return PokemonsData[random.Next(PokemonsData.Count)];
        }
        
        public event EventHandler FileNotFoundEvent;
        private void OnFileNotFoundEvent()
        {
            EventHandler fileNotFoundEvent = FileNotFoundEvent;
            if (fileNotFoundEvent != null)
            {
                fileNotFoundEvent(this, new EventArgs());
            }
        }
    }
}
