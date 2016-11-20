using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace PokemonQuizXAML
{
    class Pokemon
    {
        public Pokemon(string name, string hiddenImagePath, string displayImagePath)
        {
            this.name = name;
            imagesSource = new string[2] { hiddenImagePath, displayImagePath };
        }

        private string name;
        public string Name { get { return name; } }
        private string[] imagesSource;
        public string[] ImagesSource { get { return imagesSource; } }
    }
}
