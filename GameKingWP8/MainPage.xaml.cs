using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GameKingWP8.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.WindowsAzure.MobileServices;

namespace GameKingWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetLoginButtonState();
        }

        private void DeucesWild_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=DEUCESWILD", UriKind.Relative));
        }

        private void JacksOrBetter_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=JACKSORBETTER", UriKind.Relative));
        }

        private void BonusPokerDeluxe_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=BONUSPOKERDELUXE", UriKind.Relative));
        }

        private void DoubleBonusPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=DOUBLEBONUSPOKER", UriKind.Relative));
        }

        private void TripleBonusPokerPlus_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=TRIPLEBONUSPOKERPLUS", UriKind.Relative));
        }

        private void RoyalAcesBonusPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=ROYALACESBONUSPOKER", UriKind.Relative));
        }

        private void BonusPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=BONUSPOKER", UriKind.Relative));
        }

        private void SuperAcesBonusPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=SUPERACESBONUSPOKER", UriKind.Relative));
        }

        private void WhiteHotAces_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=WHITEHOTACES", UriKind.Relative));
        }

        private void PlayClick()
        {
            var stream = TitleContainer.OpenStream("Assets/audio/click.wav");
            SoundEffect effect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
            effect.Play();
        }

        private void AcesAndFacesPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=ACESANDFACESPOKER", UriKind.Relative));
        }

        private void DoubleBonusDeucesWild_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=DOUBLEBONUSDEUCESWILD", UriKind.Relative));
        }

        private void DeucesWildBonusPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=DEUCESWILDBONUSPOKER", UriKind.Relative));
        }

        private async void Stats_Click(object sender, EventArgs e)
        {
            //NavigationService.Navigate(new Uri("/Stats.xaml", UriKind.Relative));
            if (!App.settings["microsoftuserid"].ToString().Contains("MicrosoftAccount"))
            {
                try
                {
                    MobileServiceUser user = await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);
                    App.settings["microsoftuserid"] = user.UserId;
                    App.settings.Save();

                }
                catch (InvalidOperationException)
                {

                }

                SetLoginButtonState();
            }
            else
            {
                App.settings["microsoftuserid"] = String.Empty;
                SetLoginButtonState();
            }
        }

        

        private void SetLoginButtonState()
        {
            ApplicationBarIconButton abib = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            
            if (App.settings["microsoftuserid"].ToString().Contains("MicrosoftAccount"))
            {
                
                abib.Text = "log out";
            //    string imagepath = "ms-appx:/Assets/LOGGEDIN_Microsoft.png";
            //    BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
            //    MicrosoftLoginButton.Source = imagesource;
                App.SaveOldHandData();
            }
            else
            {
                abib.Text = "log in";
            }
            
        }

        private void JokerPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=JOKERPOKER", UriKind.Relative));
        }

        private void DoubleDoubleBonusPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=DOUBLEDOUBLEBONUSPOKER", UriKind.Relative));
        }

        private void BlackJackBonusPoker_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayClick();
            NavigationService.Navigate(new Uri("/Game.xaml?game=BLACKJACKBONUSPOKER", UriKind.Relative));
        }
    }
}