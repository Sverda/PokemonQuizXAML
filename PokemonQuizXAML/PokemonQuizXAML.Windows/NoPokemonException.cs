using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonQuizXAML
{
    class NoPokemonException :Exception
    {
        public NoPokemonException(string message="Can't load any pokemon. ") : base(message) { }
    }
}
