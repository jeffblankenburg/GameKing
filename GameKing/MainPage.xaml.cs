using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GameKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //AdRotatorControl.Invalidate();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SizeChanged += Game_SizeChanged;
            Loaded += MainPage_Loaded;
            SettingsPane.GetForCurrentView().CommandsRequested += MainPage_CommandsRequested;
            SetLoginButtonState();
            ShowAlertBox();
        }

        private void ShowAlertBox()
        {
            if (((bool)App.settings.Values["showalertbox"]))
            {
                AlertBox.Visibility = Visibility.Visible;
            }
            else AlertBox.Visibility = Visibility.Collapsed;
        }

        void MainPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            AdBox.Suspend();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Loaded -= MainPage_Loaded;
            SettingsPane.GetForCurrentView().CommandsRequested -= MainPage_CommandsRequested;
        }

        private void Game_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Windows.UI.Xaml.Window.Current.Bounds.Width <= 320)
            {
                FullScreen.Visibility = Visibility.Collapsed;
                SnappedState.Visibility = Visibility.Visible;
            }
            else
            {
                FullScreen.Visibility = Visibility.Visible;
                SnappedState.Visibility = Visibility.Collapsed;
            }
        }

        private void DeucesWild_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DEUCESWILD");
        }

        private void JacksOrBetter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "JACKSORBETTER");
        }

        private void BonusPokerDeluxe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "BONUSPOKERDELUXE");
        }

        private void DoubleBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DOUBLEBONUSPOKER");
        }

        private void RoyalAcesBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "ROYALACESBONUSPOKER");
        }

        private void BonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "BONUSPOKER");
        }

        private void TripleBonusPokerPlus_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "TRIPLEBONUSPOKERPLUS");
        }

        private void WhiteHotAces_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "WHITEHOTACES");
        }

        private void SuperAcesBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "SUPERACESBONUSPOKER");
        }

        private void AcesAndFacesPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "ACESANDFACESPOKER");
        }

        private void DoubleBonusDeucesWild_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DOUBLEBONUSDEUCESWILD");
        }

        private void DeucesWildBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DEUCESWILDBONUSPOKER");
        }

        private void JokerPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "JOKERPOKER");
        }

        private void BlackJackBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "BLACKJACKBONUSPOKER");
        }

        private void DoubleDoubleBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DOUBLEDOUBLEBONUSPOKER");
        }

        private void LayoutRoot_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (AdBox.IsSuspended)
            {
                AdBox.Resume();
            }
        }

        private async void Microsoft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!App.settings.Values["microsoftuserid"].ToString().Contains("MicrosoftAccount"))
            {
                try
                {
                    MobileServiceUser user = await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);
                    App.settings.Values["microsoftuserid"] = user.UserId;

                }
                catch (InvalidOperationException)
                {

                }

                SetLoginButtonState();
            }
            else
            {
                App.settings.Values["microsoftuserid"] = String.Empty;
                SetLoginButtonState();
            }
        }

        private void SetLoginButtonState()
        {
            if (App.settings.Values["microsoftuserid"].ToString().Contains("MicrosoftAccount"))
            {
                string imagepath = "ms-appx:/Assets/buttons/LOGOUT.png";
                BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                MicrosoftLoginButton.Source = imagesource;
                App.SaveOldHandData();
            }
            else
            {
                string imagepath = "ms-appx:/Assets/buttons/LOGIN.png";
                BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                MicrosoftLoginButton.Source = imagesource;
            }
            
        }

        private void AlertBoxClose_Click(object sender, RoutedEventArgs e)
        {
            AlertBox.Visibility = Visibility.Collapsed;
            App.settings.Values["showalertbox"] = false;
            App.settings.Values["alertdate"] = DateTime.Now.ToString();
        }

        private void MouseOver(object sender, PointerRoutedEventArgs e)
        {
            Image i = sender as Image;
            i.Opacity = .75;
        }

        private void MouseOut(object sender, PointerRoutedEventArgs e)
        {
            Image i = sender as Image;
            i.Opacity = 1;
        }

        private void Achievements_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Achievements));
        }
    }
}
