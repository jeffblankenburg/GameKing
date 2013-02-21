using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GameKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        VideoPokerGame PokerGame;
        Player GamePlayer = new Player();
        string GameType;
        bool HoldRound = false;


        SolidColorBrush Blue = new SolidColorBrush { Color = new Utility().HexToColor("#FF000064") };
        SolidColorBrush Red = new SolidColorBrush { Color = new Utility().HexToColor("#FFB00000") };

        MediaElement OneBet = new MediaElement();
        
        public Game()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GameType = e.Parameter.ToString();
            GameSetup();
        }

        private void GameSetup()
        {
            PokerGame = new VideoPokerGame(GameType);
            LoadAudioFiles();
            LoadCurrentBet();
            LoadPayTable();
            DrawCredits();
        }

        private void LoadAudioFiles()
        {
            OneBet.AutoPlay = false;
            OneBet.Source = new Uri("ms-appx:/Assets/audio/slot_machine_bet_04.wav", UriKind.Absolute);
            LayoutRoot.Children.Add(OneBet);
        }

        private void LoadCurrentBet()
        {
            ChangeBetHighlight();
        }

        private void LoadPayTable()
        {
            PayTable.ItemsSource = PokerGame.PayTable;
        }

        private void Card_MouseLeftButtonDown(object sender, TappedRoutedEventArgs e)
        {
            if (HoldRound)
            {
                Image image = sender as Image;
                int place = Int32.Parse(image.Name.Substring(4, 1));
                if (PokerGame.Hand.Held[place]) PokerGame.Hand.Hold(place, false);
                else PokerGame.Hand.Hold(place, true);
                UpdateHold();
            }
        }

        private void UpdateHold()
        {
            for (int i = 0; i <= 4; i++)
            {
                Image hold = (Image)FindName("Hold" + i);
                if (PokerGame.Hand.Held[i]) hold.Visibility = Visibility.Visible;
                else hold.Visibility = Visibility.Collapsed;
            }
        }

        private void ClearHolds()
        {
            Hold0.Visibility = Visibility.Collapsed;
            Hold1.Visibility = Visibility.Collapsed;
            Hold2.Visibility = Visibility.Collapsed;
            Hold3.Visibility = Visibility.Collapsed;
            Hold4.Visibility = Visibility.Collapsed;
        }

        private void ResetReds()
        {
            StopPayTableAnimations();
            CoinBox1.Fill = Blue;
            CoinBox2.Fill = Blue;
            CoinBox3.Fill = Blue;
            CoinBox4.Fill = Blue;
            CoinBox5.Fill = Blue;
        }

        private void Deal_Click(object sender, TappedRoutedEventArgs e)
        {
            Deal();
        }

        private void Deal()
        {
            StopPayTableAnimations();
            if (!HoldRound)
            {
                ClearHolds();
                ChargeCredits();
                PokerGame = new VideoPokerGame(GameType);
                HoldRound = true;
            }
            else
            {
                PokerGame.Draw();
                HoldRound = false;
                HighlightWinningBetValue(PayTable);
            }
            ShowCards();
        }

        private void ChargeCredits()
        {
            GamePlayer.Credits -= GamePlayer.CurrentBet;
            DrawCredits();
        }

        public void ShowCards()
        {
            string x;
            
            for (int i = 0; i <= 4; i++)
            {
                x = String.Empty;
                switch (GameType)
                {
                    case "DEUCESWILD":
                        if (PokerGame.Hand.Cards[i].Value.Number == 2) x = "w";
                        break;
                }
                
                Image image = (Image)FindName("Card" + i);
                string imagepath = "ms-appx:/Assets/cards/" + PokerGame.Hand.Cards[i].Suit.ID.ToString() + PokerGame.Hand.Cards[i].Value.Number.ToString() + x.ToString() + ".png";
                BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                image.Source = imagesource;
            }
        }

        int volume = 1;

        private void Volume_Tapped(object sender, TappedRoutedEventArgs e)
        {
            volume++;

            if (volume == 4) volume = 0;

            string imagepath = "ms-appx:/Assets/buttons/Volume" + volume + ".png";
            BitmapImage i = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
            VolumeButton.Source = i;

            //TODO: Actually toggle the volume.

            switch (volume)
            {
                case 0:
                    
                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;

            }
        }

        private void BetMax_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!HoldRound)
            {
                if (GamePlayer.IncreaseBet(5)) Deal();
                //TODO: Build an animation that shows the red box travel to the 5 coin slot.
                ChangeBetHighlight();
            }
        }

        private void BetOne_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!HoldRound)
            {
                OneBet.Play();
                if (GamePlayer.IncreaseBet(1)) Deal();
                ChangeBetHighlight();
            }
        }

        private void ChangeBetHighlight()
        {
            ResetReds();
            Rectangle r = (Rectangle)FindName("CoinBox" + GamePlayer.CurrentBet);
            r.Fill = Red;
            BetText.Text = "BET   " + GamePlayer.CurrentBet;
        }

        private void HighlightWinningBetValue(DependencyObject targetElement)
        {
            var count = VisualTreeHelper.GetChildrenCount(targetElement);
            if (count == 0)
                return;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(targetElement, i);
                if (child is TextBlock)
                {
                    TextBlock targetItem = (TextBlock)child;

                    if (targetItem.Text.Replace(".", "") == PokerGame.CheckHand(GameType))
                    {
                        TextBlock coinslot = (TextBlock)VisualTreeHelper.GetChild(targetElement, i + GamePlayer.CurrentBet);
                        Storyboard.SetTarget(PayTableTitleBlink, targetItem);
                        Storyboard.SetTarget(PayTableNumberBlink, coinslot);
                        PayTableTitleBlink.Begin();
                        PayTableNumberBlink.Begin();
                        AwardWinnings(Int32.Parse(coinslot.Text));
                        return;
                    }
                }
                else
                {
                    HighlightWinningBetValue(child);
                }
            }

        }

        private void StopPayTableAnimations()
        {
            PayTableTitleBlink.Stop();
            PayTableNumberBlink.Stop();
        }

        private void AwardWinnings(int credits)
        {
            GamePlayer.Credits += credits;
            DrawCredits();
        }

        private void DrawCredits()
        {
            CreditsPanel.Children.Clear();
            for (int i = 0; i < GamePlayer.Credits.ToString().Length; i++)
            {
                Image j = new Image { Width = 38 };
                string imagepath = "ms-appx:/Assets/numbers/" + GamePlayer.Credits.ToString()[i] + ".png";
                BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                j.Source = imagesource;
                CreditsPanel.Children.Add(j);
            }
            Image creditImage = new Image { Height = 52 };
            string creditPath = "ms-appx:/Assets/numbers/CREDITS.png";
            BitmapImage creditBI = new BitmapImage(new Uri(creditPath, UriKind.Absolute));
            creditImage.Source = creditBI;
            CreditsPanel.Children.Add(creditImage);
        }
    }
}
