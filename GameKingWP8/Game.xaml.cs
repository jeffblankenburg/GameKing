using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PokerLogic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace GameKingWP8
{
    public partial class Game : PhoneApplicationPage
    {
        VideoPokerGame PokerGame;
        Player GamePlayer = new Player();
        string GameType;
        bool HoldRound = false;
        int handCounter = 0;

        SolidColorBrush Blue = new SolidColorBrush { Color = new Utility().HexToColor("#FF000064") };
        SolidColorBrush Red = new SolidColorBrush { Color = new Utility().HexToColor("#FFB00000") };

        MediaElement OneBet = new MediaElement();
        MediaElement WinLoop = new MediaElement();
        MediaElement HoldAlert = new MediaElement();
        MediaElement CardFlip = new MediaElement();
        
        public Game()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GameType = NavigationContext.QueryString["game"].ToString();
            CreditPause.Completed += CreditPause_Completed;
            CardPause.Completed += CardPause_Completed;
            GameSetup();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            CreditPause.Completed -= CreditPause_Completed;
            CardPause.Completed -= CardPause_Completed;
        }

        void CardPause_Completed(object sender, object e)
        {
            ShowCards(ShouldPayUser);
        }

        void CreditPause_Completed(object sender, object e)
        {
            UpdateCredits();
        }

        private void GameSetup()
        {
            PokerGame = new VideoPokerGame(GameType);
            LoadAudioFiles();
            LoadCurrentBet();
            LoadPayTable();
            DrawCredits((int)App.settings["credits"]);
        }

        private void LoadCurrentBet()
        {
            ChangeBetHighlight();
        }

        private void LoadPayTable()
        {
            PayTable.ItemsSource = PokerGame.PayTable;
        }

        private void LoadAudioFiles()
        {
            OneBet.AutoPlay = false;
            OneBet.Volume = .66;
            OneBet.Source = new Uri("Assets/audio/slot_machine_bet_04.wav", UriKind.RelativeOrAbsolute);
            LayoutRoot.Children.Add(OneBet);

            HoldAlert.AutoPlay = false;
            HoldAlert.Volume = .66;
            HoldAlert.Source = new Uri("Assets/audio/slot_machine_win_03.wav", UriKind.RelativeOrAbsolute);
            LayoutRoot.Children.Add(HoldAlert);

            CardFlip.AutoPlay = false;
            CardFlip.Volume = .66;
            CardFlip.Source = new Uri("Assets/audio/carddeal.wav", UriKind.RelativeOrAbsolute);
            LayoutRoot.Children.Add(CardFlip);

            WinLoop.AutoPlay = false;
            //WinLoop.IsLooping = true;
            WinLoop.Volume = .66;
            WinLoop.Source = new Uri("Assets/audio/slot_machine_win_jackpot_04.wav", UriKind.RelativeOrAbsolute);
            LayoutRoot.Children.Add(WinLoop);
        }

        private void Card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        private void Deal_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                handCounter++;
                HoldRound = true;
            }
            else
            {
                PokerGame.Draw();
                ResetCardBacks();
                //WriteDataToMobileService();
                HoldRound = false;
                if (handCounter == 10)
                {
                    //AdBox.Visibility = Visibility.Visible;
                    handCounter = 0;
                }
            }
            ShowCards(!HoldRound);
        }

        private void ChargeCredits()
        {
            int credits = (int)App.settings["credits"];
            int total = (int)App.settings["totalcreditsplayed"];
            credits -= GamePlayer.CurrentBet;
            total += GamePlayer.CurrentBet;
            App.settings["credits"] = credits;
            App.settings["totalcreditsplayed"] = total;
            DrawCredits(credits);
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
                    string imagepath = "Assets/cards/" + PokerGame.Hand.Cards[cardCounter].Suit.ID.ToString() + PokerGame.Hand.Cards[cardCounter].Value.Number.ToString() + x.ToString() + ".png";
                    BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Relative));
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

        int volume = 2;

        private void Volume_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            volume++;

            if (volume == 4) volume = 0;

            string imagepath = "Assets/buttons/Volume" + volume + ".png";
            BitmapImage i = new BitmapImage(new Uri(imagepath, UriKind.Relative));
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

        private void BetMax_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!HoldRound)
            {
                if (GamePlayer.IncreaseBet(5)) Deal();
                //TODO: Build an animation that shows the red box travel to the 5 coin slot.
                ChangeBetHighlight();
            }
        }

        private void BetOne_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        private void AwardWinnings(int award)
        {
            int credits = (int)App.settings["credits"];
            OldCredits = credits;
            credits += award;
            App.settings["credits"] = credits;
            WinLoop.Play();
            UpdateCredits();
        }

        private void UpdateCredits()
        {
            if (OldCredits < (int)App.settings["credits"])
            {
                OldCredits++;
                DrawCredits(OldCredits);
                CreditPause.Begin();
            }
            else WinLoop.Stop();
        }

        private void DrawCredits(int credits)
        {
            CreditsPanel.Children.Clear();
            for (int i = credits.ToString().Length - 1; i >= 0; i--)
            {
                Image j = new Image { Width = 19 };
                string imagepath = "Assets/numbers/" + credits.ToString()[i] + ".png";
                BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Relative));
                j.Source = imagesource;
                CreditsPanel.Children.Add(j);
            }
        }

        private void MoreGames_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
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

        private void ResetCardBacks()
        {
            for (int i = 0; i <= 4; i++)
            {
                if (!PokerGame.Hand.Held[i])
                {
                    Image image = (Image)FindName("Card" + i);
                    string imagepath = "Assets/cards/BACK.png";
                    BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Relative));
                    image.Source = imagesource;
                }
            }
        }
    }
}