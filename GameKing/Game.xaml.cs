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
using PokerLogic;
using Newtonsoft.Json;

namespace GameKing
{
    public sealed partial class Game : Page
    {
        VideoPokerGame PokerGame;
        Player GamePlayer = new Player();
        string GameType;
        bool HoldRound = false;
        bool ShouldResize = false;
        int handCounter = 0;
        Hand HandStart;
        Hand HandEnd;

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
            SizeChanged += Game_SizeChanged;
            CreditPause.Completed += CreditPause_Completed;
            CardPause.Completed += CardPause_Completed;
            GameSetup();
        }

        void Game_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeCards();
            CheckSnappedView();
        }

        private void CheckSnappedView()
        {
            if (Windows.UI.Xaml.Window.Current.Bounds.Width <= 320)
            {
                PayTableGrid.Visibility = Visibility.Collapsed;
                CardGrid.Visibility = Visibility.Collapsed;
                ButtonsGrid.Visibility = Visibility.Collapsed;
                SnappedStats.GrabStats();
                SnappedStats.Visibility = Visibility.Visible;
            }
            else
            {
                PayTableGrid.Visibility = Visibility.Visible;
                CardGrid.Visibility = Visibility.Visible;
                ButtonsGrid.Visibility = Visibility.Visible;
                SnappedStats.Visibility = Visibility.Collapsed;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            CreditPause.Completed -= CreditPause_Completed;
            CardPause.Completed -= CardPause_Completed;
        }

        private void GameSetup()
        {
            PokerGame = new VideoPokerGame(GameType);
            LoadAudioFiles();
            LoadCurrentBet();
            LoadPayTable();
            DrawCredits((int)App.settings.Values["credits"]);
        }

        private void ResizeCards()
        {
            for (int i = 0;i<5;i++)
            {
                Image image = FindName("Card" + i) as Image;
                image.MinHeight = 0;
                image.MinWidth = 0;
                image.MaxWidth = 1000;
                image.MaxHeight = 1000;
            }
            ShouldResize = true;
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
            WinLoop.Source = new Uri("ms-appx:/Assets/audio/jackpot.wav", UriKind.Absolute);
            LayoutRoot.Children.Add(WinLoop);
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

        private void Card_MouseLeftButtonDown(object sender, TappedRoutedEventArgs e)
        {
            if (!IsShowingCards)
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
        }

        private void UpdateHold()
        {
            for (int i = 0; i <= 4; i++)
            {
                TextBlock hold = (TextBlock)FindName("Hold" + i);
                if (PokerGame.Hand.Held[i]) hold.Opacity = 1;
                else hold.Opacity = 0.024;
            }
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

        private void Deal_Click(object sender, TappedRoutedEventArgs e)
        {
            if (ShouldResize)
            {
                ResizeSingleCard(Card0);
                ResizeSingleCard(Card1);
                ResizeSingleCard(Card2);
                ResizeSingleCard(Card3);
                ResizeSingleCard(Card4);
                ShouldResize = false;
            }
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
                        PokerGame = new VideoPokerGame(GameType);
                        IncrementHandCount();
                        handCounter++;
                        HoldRound = true;
                        HandStart = new Hand(PokerGame.Hand.Cards, PokerGame.Hand.Held);
                    }
                    else
                    {
                        PokerGame.Draw();
                        ResetCardBacks();
                        //WriteDataToMobileService();
                        HoldRound = false;
                        HandEnd = new Hand(PokerGame.Hand.Cards, PokerGame.Hand.Held);
                    }
                    ShowCards(!HoldRound);
                }
            }
        }

        private void IncrementHandCount()
        {
            int handcount = (int)App.settings.Values["totalhandsplayed"];
            App.settings.Values["totalhandsplayed"] = handcount + 1;
        }

        private async void SaveHands()
        {
            StorageFile file = await App.files.CreateFileAsync("handhistory.txt", CreationCollisionOption.OpenIfExists);
            string handtext = await FileIO.ReadTextAsync(file);


            List<BothHands> handhistory = JsonConvert.DeserializeObject<List<BothHands>>(handtext);
            if (handhistory == null) handhistory = new List<BothHands>();
            BothHands bothhands = new BothHands { OpeningHand = HandStart, ClosingHand = HandEnd, GameType = GameType, CreditCount = (int)App.settings.Values["credits"], ANID = App.settings.Values["userid"].ToString() };
            handhistory.Add(bothhands);
            handtext = JsonConvert.SerializeObject(handhistory);
            //WriteDataToMobileService(handtext);

            file = await App.files.CreateFileAsync("handhistory.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, handtext);
        }

        private async void WriteDataToMobileService(string bh)
        {
            await App.MobileService.GetTable<string>().InsertAsync(bh);
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
            int credits = (int)App.settings.Values["credits"];
            int total = (int)App.settings.Values["totalcreditsplayed"];
            credits -= GamePlayer.CurrentBet;
            total += GamePlayer.CurrentBet;
            App.settings.Values["credits"] = credits;
            App.settings.Values["totalcreditsplayed"] = total;
            DrawCredits(credits);
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
                if ((!HoldRound) && (int)App.settings.Values["credits"] == 0)
                {
                    ResetBox.Visibility = Visibility.Visible;
                    App.settings.Values["credits"] = 10000;
                    DrawCredits(10000);
                }
                HighlightPayTable(PayTableNames, (ItemsControl)FindName("PayTableCoin" + GamePlayer.CurrentBet), ShouldPayUser);
                if (ShouldPayUser) SaveHands();
                IsShowingCards = false;
            }
        }

        void CardPause_Completed(object sender, object e)
        {
            ShowCards(ShouldPayUser);
        }

        //int volume = 2;

        private void Volume_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //volume++;

            //if (volume == 4) volume = 0;

            //string imagepath = "ms-appx:/Assets/buttons/Volume" + volume + ".png";
            //BitmapImage i = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
            //VolumeButton.Source = i;

            //switch (volume)
            //{
            //    case 0:
            //        WinLoop.Volume = 0;
            //        OneBet.Volume = 0;
            //        break;
            //    case 1:
            //        WinLoop.Volume = .33;
            //        OneBet.Volume = .33;
            //        break;
            //    case 2:
            //        WinLoop.Volume = 66;
            //        OneBet.Volume = .66;
            //        break;
            //    case 3:
            //        WinLoop.Volume = 1;
            //        OneBet.Volume = 1;
            //        break;

            //}
        }

        private void BetMax_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!HoldRound && !IsShowingCards && !IsDrawingCredits)
            {
                if (GamePlayer.IncreaseBet(5)) Deal();
                //TODO: Build an animation that shows the red box travel to the 5 coin slot.
                ChangeBetHighlight();
            }
        }

        private void BetOne_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!HoldRound && !IsShowingCards && !IsDrawingCredits)
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

        private void HighlightWinningBetName(DependencyObject targetElement, bool ShouldAwardWinnings)
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
                        //TextBlock coinslot = (TextBlock)VisualTreeHelper.GetChild(targetElement, i + GamePlayer.CurrentBet);
                        
                        //PayTableNumberBlink.Begin();
                        if (ShouldAwardWinnings)
                        {
                            IEnumerable<int> pay;
                            switch (GamePlayer.CurrentBet)
                            {
                                case 1:
                                    pay = (from p in PokerGame.PayTable where p.Title == targetItem.Text select p.Coin1).Take(1);
                                    AwardWinnings(pay.First());
                                    break;
                                case 2:
                                    pay = (from p in PokerGame.PayTable where p.Title == targetItem.Text select p.Coin2).Take(1);
                                    AwardWinnings(pay.First());
                                    break;
                                case 3:
                                    pay = (from p in PokerGame.PayTable where p.Title == targetItem.Text select p.Coin3).Take(1);
                                    AwardWinnings(pay.First());
                                    break;
                                case 4:
                                    pay = (from p in PokerGame.PayTable where p.Title == targetItem.Text select p.Coin4).Take(1);
                                    AwardWinnings(pay.First());
                                    break;
                                case 5:
                                    pay = (from p in PokerGame.PayTable where p.Title == targetItem.Text select p.Coin5).Take(1);
                                    AwardWinnings(pay.First());
                                    break;
                            }
                            
                            
                        }
                        else HoldAlert.Play();
                        return;
                    }
                }
                else
                {
                    HighlightWinningBetName(child, ShouldAwardWinnings);
                }
            }

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
                        if (ShouldAwardWinnings)
                        {
                            Storyboard.SetTarget(PayTableTitleBlink, targetItem);
                            Storyboard.SetTarget(PayTableNumberBlink, targetItem2);
                            PayTableTitleBlink.Begin();
                            PayTableNumberBlink.Begin();
                            AwardWinnings(Int32.Parse(targetItem2.Text));
                            RecordHand(targetItem.Text.Replace(".", ""));
                        }
                        else HoldAlert.Play();
                        return;
                    }
                }
                else HighlightPayTable(child, child2, ShouldPayUser);
                
            }
        }

        private void RecordHand(string HandRank)
        {
            HandRank = HandRank.Replace(",", "");

            if (App.settings.Values.ContainsKey("COUNT_" + HandRank))
                App.settings.Values["COUNT_" + HandRank] = (int)App.settings.Values["COUNT_" + HandRank] + 1;
            else
                App.settings.Values["COUNT_" + HandRank] = 1;
        }

        private void StopPayTableAnimations()
        {
            PayTableTitleBlink.Stop();
            PayTableNumberBlink.Stop();
        }

        int OldCredits;
        bool IsDrawingCredits = false;

        private void AwardWinnings(int award)
        {
            int credits = (int)App.settings.Values["credits"];
            OldCredits = credits;
            credits += award;
            App.settings.Values["credits"] = credits;
            WinLoop.Play();
            UpdateCredits();
        }

        private void UpdateCredits()
        {
            IsDrawingCredits = true;
            if (OldCredits < (int)App.settings.Values["credits"])
            {
                OldCredits++;
                DrawCredits(OldCredits);
                CreditPause.Begin();
            }
            else
            {
                WinLoop.Stop();
                IsDrawingCredits = false;
            }
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
                Image j = new Image();
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

        private void ResetBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ResetBox.Visibility = Visibility.Collapsed;
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
    }
}
