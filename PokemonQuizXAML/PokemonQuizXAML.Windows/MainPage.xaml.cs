using PokemonQuizXAML.Common;
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
using Windows.UI.Xaml.Media.Imaging;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace PokemonQuizXAML
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            quiz.CheckAnswerEvent += Quiz_CheckAnswerEvent;
            quiz.GameOverEvent += Quiz_GameOverEvent;
        }


        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="Common.NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="Common.SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="Common.NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        
        private void addPokemon_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddPokemonPage));
        }

        private void deletePokemon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            quiz.OnCheckAnswerEvent(new CheckAnswerArgs(button1.Content.ToString()));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            quiz.OnCheckAnswerEvent(new CheckAnswerArgs(button2.Content.ToString()));
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            quiz.OnCheckAnswerEvent(new CheckAnswerArgs(button3.Content.ToString()));
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            quiz.OnCheckAnswerEvent(new CheckAnswerArgs(button4.Content.ToString()));
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            start.IsEnabled = false;
            setButtons();
            next.IsEnabled = false;
            quiz.SetOneRound();
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            quiz.SetOneRound();
            setButtons();
        }


        private void Quiz_CheckAnswerEvent(CheckAnswerArgs e)
        {
            setButtons();
        }
        private void setButtons()
        {
            if (button1.IsEnabled == true)
            {
                button1.IsEnabled = false;
                button2.IsEnabled = false;
                button3.IsEnabled = false;
                button4.IsEnabled = false;
                next.IsEnabled = true;
            }
            else
            {
                button1.IsEnabled = true;
                button2.IsEnabled = true;
                button3.IsEnabled = true;
                button4.IsEnabled = true;
                next.IsEnabled = false;
            }
        }

        private void Quiz_GameOverEvent(GameOverArgs e)
        {
            button1.IsEnabled = false;
            button2.IsEnabled = false;
            button3.IsEnabled = false;
            button4.IsEnabled = false;
            next.IsEnabled = false;
            start.IsEnabled = true;
        }
    }
}
