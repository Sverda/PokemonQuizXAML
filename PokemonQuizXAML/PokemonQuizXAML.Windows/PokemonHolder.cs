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
            loadPokemonsAsync();
            getDittoFromList();
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

        // TODO: Load folders from file and depends to this load pokemon
        private async void loadPokemonsAsync()
        {
            IStorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder pokemonShowFolder = await StorageFolder.GetFolderFromPathAsync(localFolder.Path + @"\Data\Pokemons\Show");
            StorageFolder pokemonBlankFolder = await StorageFolder.GetFolderFromPathAsync(localFolder.Path + @"\Data\Pokemons\Blank");

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
        }

        public Pokemon RandomPokemon()
        {
            return PokemonsData[random.Next(PokemonsData.Count)];
        }

        public event EventHandler FolderNoFoundEvent;
        private void OnFolderNoFoundEvent()
        {
            EventHandler folderNoFoundEvent = FolderNoFoundEvent;
            if (folderNoFoundEvent != null)
            {
                folderNoFoundEvent(this, new EventArgs());
            }
        }
    }
}
