using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonQuizXAML
{
    delegate void CheckAnswerHandler(CheckAnswerArgs e);

    class CheckAnswerArgs :EventArgs
    {
        public readonly string ButtonContent;
        public CheckAnswerArgs(string buttonContent)
        {
            ButtonContent = buttonContent;
        }
    }
}
