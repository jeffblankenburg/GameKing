﻿using System;
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
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Core;
using Windows.UI.ApplicationSettings;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace GameKing
{
    public sealed partial class Game : Page
    {
        DataTransferManager dtm;
        VideoPokerGame PokerGame;
        Player GamePlayer = new Player();
        string GameType;
        bool HoldRound = false;
        bool ShouldResize = false;
        bool CanShare = false;
        bool IsSnapped = false;
        int handCounter = 0;
        Hand HandStart;
        Hand HandEnd;
        Popup HelpPopup;

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
            dtm = DataTransferManager.GetForCurrentView();
            dtm.DataRequested += dtm_DataRequested;
            SizeChanged += Game_SizeChanged;
            CreditPause.Completed += CreditPause_Completed;
            CardPause.Completed += CardPause_Completed;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            SettingsPane.GetForCurrentView().CommandsRequested += Game_CommandsRequested;
            HelpPopup = App.BuildSettingsItem(new HelpPage(GameType), 480);
            HelpPopup.Closed += popup_Closed;

            GameSetup();
        }

        private void Game_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            AdBox.Suspend();
        }

        void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if ((bool)App.settings.Values["iskeyboardactive"])
            {
                switch (args.VirtualKey)
                {
                    case Windows.System.VirtualKey.Number1:
                        if (HoldRound) HoldCard(Card0);
                        else SetBet(1);
                        break;
                    case Windows.System.VirtualKey.Number2:
                        HoldCard(Card1);
                        break;
                    case Windows.System.VirtualKey.Number3:
                        HoldCard(Card2);
                        break;
                    case Windows.System.VirtualKey.Number4:
                        HoldCard(Card3);
                        break;
                    case Windows.System.VirtualKey.Number5:
                        if (HoldRound) HoldCard(Card4);
                        else SetBet(5);
                        break;
                    case Windows.System.VirtualKey.Space:
                        Deal();
                        break;
                }
            }
        }

        void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            string textSource = "I just caught a " + PokerGame.Hand.Check(GameType) + " in King Poker for Windows 8!";
            string textTitle = PokerGame.Hand.Check(GameType) + " in King Poker!";
            string textDescription = "Share your " + PokerGame.Hand.Check(GameType) + " with your friends!";

            Uri linkSource = new Uri("http://apps.microsoft.com/windows/app/king-poker/bc8d046c-e35d-49fa-824e-eccf675c7a12");

            string HTMLSource = "<table style='background-color:#00019F;'><tr><td colspan='5'><img src='http://jeffblankenburg.com/KingPoker/gamelogos/" + GameType + ".png' /></td></tr><tr>";

            for (int i = 0; i < 5; i++)
            {
                string x = String.Empty;
                switch (GameType)
                {
                    case "DEUCESWILD": case "DOUBLEBONUSDEUCESWILD": case "DEUCESWILDBONUSPOKER":
                        if (PokerGame.Hand.Cards[i].Value.Number == 2) x = "w";
                        break;
                }
                HTMLSource += "<td><img src='http://jeffblankenburg.com/KingPoker/cards/" + PokerGame.Hand.Cards[i].Suit.ID.ToString() + PokerGame.Hand.Cards[i].Value.Number + x + ".png' /></td><td>";

            }

            HTMLSource += "</tr></table><br/><a href='http://apps.microsoft.com/windows/app/king-poker/bc8d046c-e35d-49fa-824e-eccf675c7a12'><img src='http://jeffblankenburg.com/KingPoker/SplashScreen.png' /></a>";

            DataPackage data = args.Request.Data;
            data.Properties.Title = textTitle;
            data.Properties.Description = textDescription;
            //data.SetText(HTMLSource);
            //data.SetUri(linkSource);
            data.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(HTMLSource));
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
                IsSnapped = true;
                SnappedStateGrid.Visibility = Visibility.Visible;
                //PayTableGrid.Visibility = Visibility.Collapsed;
                //CardGrid.Visibility = Visibility.Collapsed;
                //ButtonsGrid.Visibility = Visibility.Collapsed;
                //SnappedStats.GrabStats();
                //SnappedStats.Visibility = Visibility.Visible;
            }
            else
            {
                IsSnapped = false;
                SnappedStateGrid.Visibility = Visibility.Collapsed;
                //PayTableGrid.Visibility = Visibility.Visible;
                //CardGrid.Visibility = Visibility.Visible;
                //ButtonsGrid.Visibility = Visibility.Visible;
                //SnappedStats.Visibility = Visibility.Collapsed;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            CreditPause.Completed -= CreditPause_Completed;
            CardPause.Completed -= CardPause_Completed;
            dtm.DataRequested -= dtm_DataRequested;
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
            HelpPopup.Closed -= popup_Closed;
            SettingsPane.GetForCurrentView().CommandsRequested -= Game_CommandsRequested;
        }

        private void GameSetup()
        {
            if (PokerGame == null) PokerGame = new VideoPokerGame(GameType);
            HelpContent h = new HelpContent(GameType);
            GameName.Text = h.Title;
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

            PayTableNamesSNAP.ItemsSource = PokerGame.PayTable;
            PayTableCoin1SNAP.ItemsSource = PokerGame.PayTable;
            PayTableCoin2SNAP.ItemsSource = PokerGame.PayTable;
            PayTableCoin3SNAP.ItemsSource = PokerGame.PayTable;
            PayTableCoin4SNAP.ItemsSource = PokerGame.PayTable;
            PayTableCoin5SNAP.ItemsSource = PokerGame.PayTable;
        }

        private void Card_MouseLeftButtonDown(object sender, TappedRoutedEventArgs e)
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
                TextBlock held = (TextBlock)FindName("Held" + i);
                if (PokerGame.Hand.Held[i])
                {
                    hold.Opacity = 1;
                    held.Opacity = 1;
                }
                else
                {
                    hold.Opacity = 0.024;
                    held.Opacity = 0.024;
                }
            }
        }

        private void ClearHolds()
        {
            Hold0.Opacity = 0.024;
            Hold1.Opacity = 0.024;
            Hold2.Opacity = 0.024;
            Hold3.Opacity = 0.024;
            Hold4.Opacity = 0.024;
            Held0.Opacity = 0.024;
            Held1.Opacity = 0.024;
            Held2.Opacity = 0.024;
            Held3.Opacity = 0.024;
            Held4.Opacity = 0.024;
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

            CoinBox1SNAP.Fill = Blue;
            CoinBox2SNAP.Fill = Blue;
            CoinBox3SNAP.Fill = Blue;
            CoinBox4SNAP.Fill = Blue;
            CoinBox5SNAP.Fill = Blue;
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
                        DisableShareButton();
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
                        ActivateShareButton();
                        //WriteDataToMobileService();
                        HoldRound = false;
                        HandEnd = new Hand(PokerGame.Hand.Cards, PokerGame.Hand.Held);
                    }
                    ShowCards(!HoldRound);
                }
            }
        }

        private void ActivateShareButton()
        {
            CanShare = true;
            string imagepath = "ms-appx:/Assets/buttons/SHARE.png";
            BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
            ShareButton.Source = imagesource;
        }

        private void DisableShareButton()
        {
            CanShare = false;
            string imagepath = "ms-appx:/Assets/buttons/BLANK.png";
            BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
            ShareButton.Source = imagesource;
        }

        private void IncrementHandCount()
        {
            int handcount = (int)App.settings.Values["totalhandsplayed"];
            App.settings.Values["totalhandsplayed"] = handcount + 1;
        }

        private async void SaveHands()
        {
            StorageFile file = await App.files.CreateFileAsync("handhistory2.txt", CreationCollisionOption.OpenIfExists);
            string handtext = await FileIO.ReadTextAsync(file);
            List<BothHands> handhistory = JsonConvert.DeserializeObject<List<BothHands>>(handtext);
            if (handhistory == null) handhistory = new List<BothHands>();
            BothHands bothhands = new BothHands { OpeningHand = HandStart, ClosingHand = HandEnd, GameType = GameType, CreditCount = (int)App.settings.Values["credits"], IsOnline = false, IsSnapped = IsSnapped };
            handhistory.Add(bothhands);
            handtext = JsonConvert.SerializeObject(handhistory);

            file = await App.files.CreateFileAsync("handhistory2.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, handtext);
            App.UpdateLiveTiles(HandEnd.Check(GameType), GameType);
            if (App.settings.Values["microsoftuserid"].ToString().Contains("MicrosoftAccount"))
            {
                App.SaveOldHandData();
            }
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
                    Image imageSNAP = (Image)FindName("Snap" + i);
                    string imagepath = "ms-appx:/Assets/cards/BACK.png";
                    BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                    image.Source = imagesource;
                    imageSNAP.Source = imagesource;
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
                    Image imageSNAP = (Image)FindName("Snap" + cardCounter);
                    string imagepath = "ms-appx:/Assets/cards/" + PokerGame.Hand.Cards[cardCounter].Suit.ID.ToString() + PokerGame.Hand.Cards[cardCounter].Value.Number.ToString() + x.ToString() + ".png";
                    BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                    image.Source = imagesource;
                    imageSNAP.Source = imagesource;
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
                HighlightPayTableSNAP(PayTableNamesSNAP, (ItemsControl)FindName("PayTableCoin" + GamePlayer.CurrentBet + "SNAP"), false);
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
            SetBet(5);
            //TODO: Build an animation that shows the red box travel to the 5 coin slot.
        }

        private void BetOne_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SetBet(1);
        }

        private void SetBet(int bet)
        {
            if (!HoldRound && !IsShowingCards && !IsDrawingCredits)
            {
                if (bet == 1) OneBet.Play();
                if (GamePlayer.IncreaseBet(bet)) Deal();
                ChangeBetHighlight();
            }
        }

        private void ChangeBetHighlight()
        {
            ResetReds();
            Rectangle r = (Rectangle)FindName("CoinBox" + GamePlayer.CurrentBet);
            Rectangle rSNAP = (Rectangle)FindName("CoinBox" + GamePlayer.CurrentBet + "SNAP");
            r.Fill = Red;
            rSNAP.Fill = Red;
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
                        else HoldAlert.Play();
                        return;
                    }
                }
                else HighlightPayTable(child, child2, ShouldPayUser);
                
            }
        }

        private void HighlightPayTableSNAP(DependencyObject target, DependencyObject target2, bool ShouldAwardWinnings)
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
                        if (targetItem != null)
                        {
                            Storyboard.SetTarget(PayTableTitleBlinkSNAP, targetItem);
                            PayTableTitleBlinkSNAP.Begin();
                        }
                        if (targetItem2 != null)
                        {
                            Storyboard.SetTarget(PayTableNumberBlinkSNAP, targetItem2);
                            PayTableNumberBlinkSNAP.Begin();
                        }

                        if (ShouldAwardWinnings)
                        {
                            AwardWinnings(Int32.Parse(targetItem2.Text));
                            RecordHand(targetItem.Text.Replace(".", ""));
                        }
                        else HoldAlert.Play();
                        return;
                    }
                }
                else HighlightPayTableSNAP(child, child2, ShouldPayUser);

            }
        }

        private void RecordHand(string HandRank)
        {
            HandRank = HandRank.Replace(",", "");

            if (App.settings.Values.ContainsKey("COUNT_" + GameType + "_" + HandRank))
                App.settings.Values["COUNT_" + GameType + "_" + HandRank] = (int)App.settings.Values["COUNT_" + GameType + "_" + HandRank] + 1;
            else
                App.settings.Values["COUNT_" + GameType + "_" + HandRank] = 1;
            App.CheckForAchievement(GameType, HandRank);
        }

        private void StopPayTableAnimations()
        {
            PayTableTitleBlink.Stop();
            PayTableNumberBlink.Stop();
            PayTableTitleBlinkSNAP.Stop();
            PayTableNumberBlinkSNAP.Stop();
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
            CreditsPanelSNAP.Children.Clear();
            for (int i = credits.ToString().Length-1; i >=0 ; i--)
            {
                Image j = new Image();
                string imagepath = "ms-appx:/Assets/numbers/" + credits.ToString()[i] + ".png";
                BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                j.Source = imagesource;
                CreditsPanel.Children.Add(j);

                Image jSNAP = new Image();
                jSNAP.Source = imagesource;
                CreditsPanelSNAP.Children.Add(jSNAP);
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

        private void ShareButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (CanShare) DataTransferManager.ShowShareUI();
        }

        private void Help_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AdBox.Suspend();
            HelpPopup.IsOpen = true;
        }

        void popup_Closed(object sender, object e)
        {
            AdBox.Resume();
        }

        private void LayoutRoot_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (AdBox.IsSuspended)
            {
                AdBox.Resume();
            }
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

        private async void LogIn_Tapped(object sender, TappedRoutedEventArgs e)
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

        
    }
}
