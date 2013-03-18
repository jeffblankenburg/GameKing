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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Phone.Info;
using Microsoft.Phone.Tasks;

namespace GameKingWP8
{
    public partial class Game : PhoneApplicationPage
    {
        VideoPokerGame PokerGame;
        Player GamePlayer = new Player();
        string GameType;
        bool HoldRound = false;
        int handCounter = 0;
        Hand HandStart;
        Hand HandEnd;

        SolidColorBrush Blue = new SolidColorBrush { Color = new Utility().HexToColor("#FF000064") };
        SolidColorBrush Red = new SolidColorBrush { Color = new Utility().HexToColor("#FFB00000") };
        
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
            App.settings.Save();
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

        private void LoadAudioFiles()
        {
            //App.settings["credits"] = 10;
        }

        private void PlayOneBet()
        {
            var stream = TitleContainer.OpenStream("Assets/audio/onebet.wav");
            SoundEffect effect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
            effect.Play();
        }

        private void PlayCardDeal()
        {
            var stream = TitleContainer.OpenStream("Assets/audio/carddeal.wav");
            SoundEffect effect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
            effect.Play();
        }

        private void PlayHoldAlert()
        {
            var stream = TitleContainer.OpenStream("Assets/audio/holdalert.wav");
            SoundEffect effect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
            effect.Play();
        }

        private void LoadCurrentBet()
        {
            ChangeBetHighlight();
        }

        private void LoadPayTable()
        {
            PayTableNames.ItemsSource = PokerGame.PayTable;
            PayTableCoin1.ItemsSource = PokerGame.PayTable;
            PayTableCoin2.ItemsSource = PokerGame.PayTable;
            PayTableCoin3.ItemsSource = PokerGame.PayTable;
            PayTableCoin4.ItemsSource = PokerGame.PayTable;
            PayTableCoin5.ItemsSource = PokerGame.PayTable;
        }

        private void Card_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HoldCard(sender as Image);
        }

        private void HoldCard(Image image)
        {
            if (!IsShowingCards)
            {
                if (HoldRound)
                {
                    int place = Int32.Parse(image.Name.Substring(4, 1));
                    if (PokerGame.Hand.Held[place]) PokerGame.Hand.Hold(place, false);
                    else PokerGame.Hand.Hold(place, true);
                    UpdateHold();
                }
            }
        }

        private void UpdateHold()
        {
            for (int i = 0; i <= 4; i++)
            {
                TextBlock hold = (TextBlock)FindName("Hold" + i);
                if (PokerGame.Hand.Held[i]) hold.Opacity = 1;
                else hold.Opacity = .024;
            }
        }

        private void Deal_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Deal();
        }

        private void Deal()
        {
        
            if (!IsShowingCards)
            {
                if (!IsDrawingCredits)
                {
                    StopPayTableAnimations();
                    ResetBox.Visibility = Visibility.Collapsed;

                    if (!HoldRound)
                    {
                        ClearHolds();
                        ResetCardBacks();
                        ChargeCredits();
                        DisableShareButton();
                        PokerGame = new VideoPokerGame(GameType);
                        handCounter++;
                        HoldRound = true;
                        HandStart = new Hand(PokerGame.Hand.Cards, PokerGame.Hand.Held);
                    }
                    else
                    {
                        PokerGame.Draw();
                        ResetCardBacks();
                        ActivateShareButton();
                        HoldRound = false;
                        HandEnd = new Hand(PokerGame.Hand.Cards, PokerGame.Hand.Held);
                    }
                    ShowCards(!HoldRound);
                }
            }
        }

        private void ActivateShareButton()
        {
            //ApplicationBarIconButton abib = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            //abib.IsEnabled = true;
        }

        private void DisableShareButton()
        {
            //ApplicationBarIconButton abib = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            //abib.IsEnabled = false;
        }

        private void SaveHands()
        {
            List<BothHands> handhistory = (List<BothHands>)App.settings["handhistory"];
            string ANID = "";// UserExtendedProperties.GetValue("ANID2").ToString().Substring(2, 32);
            BothHands bothhands = new BothHands { OpeningHand = HandStart, ClosingHand = HandEnd, GameType = GameType, CreditCount = (int)App.settings["credits"], ANID = ANID };
            handhistory.Add(bothhands);
            App.settings["handhistory"] = handhistory;
            //WriteDataToMobileService(bothhands);
            //App.settings.Save();
        }

        private async void WriteDataToMobileService(BothHands bh)
        {
            await App.MobileService.GetTable<BothHands>().InsertAsync(bh);
        }

        private void ChargeCredits()
        {
            int credits = (int)App.settings["credits"];

            if ((credits - GamePlayer.CurrentBet) >= 0)
            {
                int total = (int)App.settings["totalcreditsplayed"];
                credits -= GamePlayer.CurrentBet;
                total += GamePlayer.CurrentBet;
                App.settings["credits"] = credits;
                App.settings["totalcreditsplayed"] = total;
                DrawCredits(credits);
            }
            else if ((credits - GamePlayer.CurrentBet) < 0)
            {
                GamePlayer.CurrentBet = credits;
                Deal();
            }
        }

        int cardCounter = 0;
        bool ShouldPayUser = false;
        bool IsShowingCards = false;

        public void ShowCards(bool payUserFlag)
        {
            IsShowingCards = true;
            ShouldPayUser = payUserFlag;

            if (cardCounter <= 4)
            {
                if (!PokerGame.Hand.Held[cardCounter])
                {
                    string x = String.Empty;
                    switch (GameType)
                    {
                        case "DEUCESWILD": case "DOUBLEBONUSDEUCESWILD": case "DEUCESWILDBONUSPOKER":
                            if (PokerGame.Hand.Cards[cardCounter].Value.Number == 2) x = "w";
                            break;
                    }

                    Image image = (Image)FindName("Card" + cardCounter);
                    string imagepath = "Assets/cards/" + PokerGame.Hand.Cards[cardCounter].Suit.ID.ToString() + PokerGame.Hand.Cards[cardCounter].Value.Number.ToString() + x.ToString() + ".png";
                    BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Relative));
                    image.Source = imagesource;
                    cardCounter++;
                    PlayCardDeal();
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
                if ((!HoldRound) && (int)App.settings["credits"] == 0)
                {
                    ResetBox.Visibility = Visibility.Visible;
                    App.settings["credits"] = 10000;
                    DrawCredits(10000);
                }
                HighlightPayTable(PayTableNames, (ItemsControl)FindName("PayTableCoin" + GamePlayer.CurrentBet), ShouldPayUser);
                if (ShouldPayUser) SaveHands();
                IsShowingCards = false;
            }
        }

        //int volume = 2;

        //private void Volume_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    volume++;

        //    if (volume == 4) volume = 0;

        //    string imagepath = "Assets/buttons/Volume" + volume + ".png";
        //    BitmapImage i = new BitmapImage(new Uri(imagepath, UriKind.Relative));
        //    VolumeButton.Source = i;

        //    switch (volume)
        //    {
        //        case 0:
        //            WinLoop.Volume = 0;
        //            OneBet.Volume = 0;
        //            break;
        //        case 1:
        //            WinLoop.Volume = .33;
        //            OneBet.Volume = .33;
        //            break;
        //        case 2:
        //            WinLoop.Volume = 66;
        //            OneBet.Volume = .66;
        //            break;
        //        case 3:
        //            WinLoop.Volume = 1;
        //            OneBet.Volume = 1;
        //            break;

        //    }
        //}

        private void BetMax_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!HoldRound && !IsShowingCards && !IsDrawingCredits)
            {
                if (GamePlayer.IncreaseBet(5)) Deal();
                //TODO: Build an animation that shows the red box travel to the 5 coin slot.
                ChangeBetHighlight();
            }
        }

        private void BetOne_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!HoldRound && !IsShowingCards && !IsDrawingCredits)
            {
                PlayOneBet();
                if (GamePlayer.IncreaseBet(1)) Deal();
                ChangeBetHighlight();
            }
        }

        private void ChangeBetHighlight()
        {
            ResetReds();
            System.Windows.Shapes.Rectangle r = (System.Windows.Shapes.Rectangle)FindName("CoinBox" + GamePlayer.CurrentBet);
            r.Fill = Red;
            BetText.Text = "BET   " + GamePlayer.CurrentBet;
        }

        private void HighlightPayTable(DependencyObject target, DependencyObject target2, bool ShouldAwardWinnings)
        {
            var count = VisualTreeHelper.GetChildrenCount(target);
            if (count == 0) return;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(target, i);
                var child2 = VisualTreeHelper.GetChild(target2, i);

                if (child is TextBlock)
                {
                    TextBlock targetItem = (TextBlock)child;
                    TextBlock targetItem2 = (TextBlock)child2;
                    if (targetItem.Text.Replace(".", "") == PokerGame.CheckHand(GameType))
                    {
                        Storyboard.SetTarget(PayTableTitleBlink, targetItem);
                        Storyboard.SetTarget(PayTableNumberBlink, targetItem2);
                        PayTableTitleBlink.Begin();
                        PayTableNumberBlink.Begin();

                        if (ShouldAwardWinnings)
                        {
                            AwardWinnings(Int32.Parse(targetItem2.Text));
                            RecordHand(targetItem.Text.Replace(".", ""));
                        }
                        else PlayHoldAlert();
                        return;
                    }
                }
                else HighlightPayTable(child, child2, ShouldPayUser);
                
            }
        }

        private void RecordHand(string HandRank)
        {
            if (App.settings.Contains("COUNT_" + HandRank))
                App.settings["COUNT_" + HandRank] = (int)App.settings["COUNT_" + HandRank] + 1;
            else
                App.settings["COUNT_" + HandRank] = 1;


            if (!App.settings.Contains("COUNT_4OFAKIND"))
                App.settings["COUNT_4OFAKIND"] = 0;
            

            switch (HandRank)
            {
                case "4ACES":
                case "42s3s4s":
                case "45sTHRUKINGS":
                    App.settings["COUNT_4OFAKIND"] = (int)App.settings["COUNT_4OFAKIND"] + 1;
                    break;
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
            UpdateCredits();
        }

        bool IsDrawingCredits = false;

        private void UpdateCredits()
        {
            IsDrawingCredits = true;
            if (OldCredits < (int)App.settings["credits"])
            {
                OldCredits++;
                DrawCredits(OldCredits);
                PlayOneBet();
                CreditPause.Begin();
            }
            else
            {
                IsDrawingCredits = false;
            }
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
            Hold0.Opacity = 0.024;
            Hold1.Opacity = 0.024;
            Hold2.Opacity = 0.024;
            Hold3.Opacity = 0.024;
            Hold4.Opacity = 0.024;
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

        private void ResetBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ResetBox.Visibility = Visibility.Collapsed;
        }

        private void Help_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }

        private void Stats_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Stats.xaml", UriKind.Relative));
        }

        private void Card_ImageOpened(object sender, RoutedEventArgs e)
        {
            ResizeSingleCard(sender as Image);
        }

        private void ResizeSingleCard(Image i)
        {
            i.MinWidth = i.ActualWidth;
            i.MaxWidth = i.ActualWidth;
            i.MinHeight = i.ActualHeight;
            i.MaxHeight = i.ActualHeight;
        }

        private void ResetBox_Tapped(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ResetBox.Visibility = Visibility.Collapsed;
        }

        private void Share_Click(object sender, EventArgs e)
        {
            string HTMLSource = "<table style='background-color:#00019F;'><tr><td colspan='5'><img src='http://jeffblankenburg.com/KingPoker/gamelogos/" + GameType + ".png' /></td></tr><tr>";

            for (int i = 0; i < 5; i++)
            {
                string x = String.Empty;
                switch (GameType)
                {
                    case "DEUCESWILD":
                    case "DOUBLEBONUSDEUCESWILD":
                    case "DEUCESWILDBONUSPOKER":
                        if (PokerGame.Hand.Cards[i].Value.Number == 2) x = "w";
                        break;
                }
                HTMLSource += "<td><img src='http://jeffblankenburg.com/KingPoker/cards/" + PokerGame.Hand.Cards[i].Suit.ID.ToString() + PokerGame.Hand.Cards[i].Value.Number + x + ".png' /></td><td>";

            }

            HTMLSource += "</tr></table><br/><a href='http://apps.microsoft.com/windows/app/king-poker/bc8d046c-e35d-49fa-824e-eccf675c7a12'><img src='http://jeffblankenburg.com/KingPoker/SplashScreen.png' /></a>";

            EmailComposeTask ect = new EmailComposeTask();
            ect.Body = HTMLSource;
            ect.Show();
        }

        private void Review_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask mrt = new MarketplaceReviewTask();
            mrt.Show();
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
    }
}