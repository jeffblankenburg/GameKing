using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
        MediaElement WinLoop = new MediaElement();
        MediaElement HoldAlert = new MediaElement();
        MediaElement CardFlip = new MediaElement();
        
        public Game()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GameType = e.Parameter.ToString();
            GamePlayer.Credits = (int)App.settings.Values["credits"];
            CreditPause.Completed += CreditPause_Completed;
            CardPause.Completed += CardPause_Completed;
            GameSetup();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            App.settings.Values["credits"] = GamePlayer.Credits;
            CreditPause.Completed -= CreditPause_Completed;
            CardPause.Completed -= CardPause_Completed;
        }

        private void GameSetup()
        {
            PokerGame = new VideoPokerGame(GameType);
            LoadAudioFiles();
            LoadCurrentBet();
            LoadPayTable();
            DrawCredits(GamePlayer.Credits);
        }

        private void LoadAudioFiles()
        {
            OneBet.AutoPlay = false;
            OneBet.Volume = .66;
            OneBet.Source = new Uri("ms-appx:/Assets/audio/slot_machine_bet_04.wav", UriKind.Absolute);
            LayoutRoot.Children.Add(OneBet);

            HoldAlert.AutoPlay = false;
            HoldAlert.Volume = .66;
            HoldAlert.Source = new Uri("ms-appx:/Assets/audio/slot_machine_win_03.wav", UriKind.Absolute);
            LayoutRoot.Children.Add(HoldAlert);

            CardFlip.AutoPlay = false;
            CardFlip.Volume = .66;
            CardFlip.Source = new Uri("ms-appx:/Assets/audio/carddeal.wav", UriKind.Absolute);
            LayoutRoot.Children.Add(CardFlip);

            WinLoop.AutoPlay = false;
            WinLoop.IsLooping = true;
            WinLoop.Volume = .66;
            WinLoop.Source = new Uri("ms-appx:/Assets/audio/slot_machine_win_jackpot_04.wav", UriKind.Absolute);
            LayoutRoot.Children.Add(WinLoop);
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
            PokerGame.Hand.Held[0] = false;
            PokerGame.Hand.Held[1] = false;
            PokerGame.Hand.Held[2] = false;
            PokerGame.Hand.Held[3] = false;
            PokerGame.Hand.Held[4] = false;
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
                ResetCardBacks();
                ChargeCredits();
                PokerGame = new VideoPokerGame(GameType);
                HoldRound = true;
            }
            else
            {
                PokerGame.Draw();
                ResetCardBacks();
                //WriteDataToMobileService();
                HoldRound = false;
            }
            ShowCards(!HoldRound);
        }

        private void WriteDataToMobileService()
        {
            //throw new NotImplementedException();
        }

        private void ResetCardBacks()
        {
            for (int i = 0; i <= 4; i++)
            {
                if (!PokerGame.Hand.Held[i])
                {
                    Image image = (Image)FindName("Card" + i);
                    string imagepath = "ms-appx:/Assets/cards/BACK.png";
                    BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                    image.Source = imagesource;
                }
            }
        }

        private void ChargeCredits()
        {
            GamePlayer.Credits -= GamePlayer.CurrentBet;
            DrawCredits(GamePlayer.Credits);
        }

        int cardCounter = 0;
        bool ShouldPayUser = false;

        public void ShowCards(bool payUserFlag)
        {
            ShouldPayUser = payUserFlag;

            if (cardCounter <= 4)
            {
                if (!PokerGame.Hand.Held[cardCounter])
                {
                    string x = String.Empty;
                    switch (GameType)
                    {
                        case "DEUCESWILD":
                            if (PokerGame.Hand.Cards[cardCounter].Value.Number == 2) x = "w";
                            break;
                    }

                    Image image = (Image)FindName("Card" + cardCounter);
                    string imagepath = "ms-appx:/Assets/cards/" + PokerGame.Hand.Cards[cardCounter].Suit.ID.ToString() + PokerGame.Hand.Cards[cardCounter].Value.Number.ToString() + x.ToString() + ".png";
                    BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                    image.Source = imagesource;
                    cardCounter++;
                    CardFlip.Play();
                    CardPause.Begin();
                }
                else
                {
                    cardCounter++;
                    ShowCards(ShouldPayUser);
                }
            }
            else
            {
                cardCounter = 0;
                HighlightWinningBetValue(PayTable, ShouldPayUser);
            }
        }

        void CardPause_Completed(object sender, object e)
        {
            ShowCards(ShouldPayUser);
        }

        int volume = 2;

        private void Volume_Tapped(object sender, TappedRoutedEventArgs e)
        {
            volume++;

            if (volume == 4) volume = 0;

            string imagepath = "ms-appx:/Assets/buttons/Volume" + volume + ".png";
            BitmapImage i = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
            VolumeButton.Source = i;

            switch (volume)
            {
                case 0:
                    WinLoop.Volume = 0;
                    OneBet.Volume = 0;
                    break;
                case 1:
                    WinLoop.Volume = .33;
                    OneBet.Volume = .33;
                    break;
                case 2:
                    WinLoop.Volume = 66;
                    OneBet.Volume = .66;
                    break;
                case 3:
                    WinLoop.Volume = 1;
                    OneBet.Volume = 1;
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

        private void HighlightWinningBetValue(DependencyObject targetElement, bool ShouldAwardWinnings)
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
                        if (ShouldAwardWinnings) AwardWinnings(Int32.Parse(coinslot.Text));
                        else HoldAlert.Play();
                        return;
                    }
                }
                else
                {
                    HighlightWinningBetValue(child, ShouldAwardWinnings);
                }
            }

        }

        private void StopPayTableAnimations()
        {
            PayTableTitleBlink.Stop();
            PayTableNumberBlink.Stop();
        }

        int OldCredits;

        private void AwardWinnings(int credits)
        {
            OldCredits = GamePlayer.Credits;
            GamePlayer.Credits += credits;
            WinLoop.Play();
            UpdateCredits();
        }

        private void UpdateCredits()
        {
            if (OldCredits < GamePlayer.Credits)
            {
                OldCredits++;
                DrawCredits(OldCredits);
                CreditPause.Begin();
            }
            else WinLoop.Stop();
        }

        void CreditPause_Completed(object sender, object e)
        {
 	        UpdateCredits();
        }

        private void DrawCredits(int credits)
        {
            CreditsPanel.Children.Clear();
            for (int i = credits.ToString().Length-1; i >=0 ; i--)
            {
                Image j = new Image { Width = 38 };
                string imagepath = "ms-appx:/Assets/numbers/" + credits.ToString()[i] + ".png";
                BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                j.Source = imagesource;
                CreditsPanel.Children.Add(j);
            }
        }

        private void MoreGames_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
