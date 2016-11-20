using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;

namespace PokemonQuizXAML
{
    class Quiz : INotifyPropertyChanged
    {
        public Quiz()
        {
            random = new Random();
            pokemonHolder = new PokemonHolder(random);
            refreshGame();
            CheckAnswerEvent += Quiz_CheckAnswerEvent;
        }

        Random random;

        private int correctCount;
        public string CorrectLabel { get { return "Correct answers: " + correctCount + "/" + demandWins; } }

        private int wrongCount;
        public string WrongLabel { get { return "Wrong answers: " + wrongCount + "/3"; } }

        private int demandWins;
        private int demandLoses;
        private int levelCount;
        public string LevelLabel { get { return "Level " + levelCount; } }

        private bool displayPokemon;

        public string Button1 { get; private set; }
        public string Button2 { get; private set; }
        public string Button3 { get; private set; }
        public string Button4 { get; private set; }

        private PokemonHolder pokemonHolder;
        public PokemonHolder PokemonHolder
        {
            private get
            {
                return pokemonHolder;
            }
            set
            {
                if (pokemonHolder == null)
                    pokemonHolder = value;
            }
        }

        public Pokemon CurrentPokemon { get; private set; }
        public BitmapImage Image
        {
            get
            {
                Uri uri;

                try
                {
                    uri = new Uri(CurrentPokemon.ImagesSource[Convert.ToInt32(displayPokemon)]);
                }
                catch (NullReferenceException)
                {
                    uri = new Uri("ms-appx:///Assets//pokemonLogo.jpg");
                }

                return new BitmapImage(uri);
            }
        }

        public void SetOneRound()
        {
            displayPokemon = false;
            Pokemon[] pokemonsForRound = new Pokemon[4]
            {
                pokemonHolder.RandomPokemon(),
                pokemonHolder.RandomPokemon(),
                pokemonHolder.RandomPokemon(),
                pokemonHolder.RandomPokemon(),
            };

            Button1 = pokemonsForRound[0].Name;
            Button2 = pokemonsForRound[1].Name;
            Button3 = pokemonsForRound[2].Name;
            Button4 = pokemonsForRound[3].Name;
            OnPropertyChanged("Button1");
            OnPropertyChanged("Button2");
            OnPropertyChanged("Button3");
            OnPropertyChanged("Button4");

            CurrentPokemon = pokemonsForRound[random.Next(pokemonsForRound.Length)];
            OnPropertyChanged("CurrentPokemon");
            OnPropertyChanged("Image");
        }

        private void Quiz_CheckAnswerEvent(CheckAnswerArgs e)
        {
            CheckAnswer(e);
        }
        private void CheckAnswer(CheckAnswerArgs e)
        {
            if (e.ButtonContent == CurrentPokemon.Name)
            {
                correctCount++;
                OnPropertyChanged("CorrectLabel");
            }
            else
            {
                wrongCount++;
                OnPropertyChanged("WrongLabel");
            }
            displayPokemon = true;
            OnPropertyChanged("Image");
            assentLevel();
        }

        private async void assentLevel()
        {
            if (correctCount >= demandWins)
            {
                correctCount = 0;
                OnPropertyChanged("CorrectLabel");
                levelCount++;
                demandWins++;
                OnPropertyChanged("LevelLabel");
                MessageDialog dialog = new MessageDialog("New level accomplished! ");
                dialog.Commands.Add(new UICommand("I catch em all!"));
                await dialog.ShowAsync();
            }
            if (wrongCount >= demandLoses)
            {
                await new MessageDialog("Game Over").ShowAsync();
                OnGameOverEvent(new GameOverArgs());
                refreshGame();
            }
        }

        private void refreshGame()
        {
            Button1 = "Welcome";
            Button2 = "to";
            Button3 = "POKEMON";
            Button4 = "QUIZ";
            OnPropertyChanged("Button1");
            OnPropertyChanged("Button2");
            OnPropertyChanged("Button3");
            OnPropertyChanged("Button4");
            CurrentPokemon = null;
            OnPropertyChanged("Image");
            levelCount = 1;
            OnPropertyChanged("LevelLabel");
            correctCount = 0;
            OnPropertyChanged("CorrectLabel");
            wrongCount = 0;
            OnPropertyChanged("WrongLabel");
            demandWins = 3;
            demandLoses = 3;
            displayPokemon = false;
        }

        public event GameOverHandler GameOverEvent;
        public void OnGameOverEvent(GameOverArgs e)
        {
            GameOverHandler gameOverEvent = GameOverEvent;
            if (gameOverEvent != null)
                gameOverEvent(e);
        }

        public event CheckAnswerHandler CheckAnswerEvent;
        public void OnCheckAnswerEvent(CheckAnswerArgs e)
        {
            CheckAnswerHandler checkAnswerEvent = CheckAnswerEvent;
            if (checkAnswerEvent != null)
                checkAnswerEvent(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
