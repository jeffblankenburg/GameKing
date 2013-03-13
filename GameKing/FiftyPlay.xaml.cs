using PokerLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GameKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FiftyPlay : Page
    {
        VideoPokerGame PokerGame;
        List<VideoPokerGame> VideoPokerGames;
        Player GamePlayer = new Player();
        BitmapImage CardBackImage;
        int HandCount = 34;
        int OldCredits;
        bool IsShowingCards = false;
        bool IsDrawingCredits = false;
        bool HoldRound = false;
        int cardCounter = 0;
        bool ShouldPayUser = false;
        string GameType;
        
        public FiftyPlay()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GameType = e.Parameter.ToString();
            CreditPause.Completed += CreditPause_Completed;
            CardPause.Completed += CardPause_Completed;
            //Loaded += Game_Loaded;
            GameSetup();
            
        }

        private void GameSetup()
        {
            HandCount = 34;
            CaptureGame();
            SetupCards();
            SetHandCovers();
            //LoadAudioFiles();
            //LoadCurrentBet();
            //LoadPayTable();
            DrawCredits((int)App.settings.Values["credits"]);
        }

        private void SetupCards()
        {
            string imagepath = "ms-appx:/Assets/cards/BACK.png";
            CardBackImage = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
        }

        private void CaptureGame()
        {
            PokerGame = new VideoPokerGame(GameType);

            VideoPokerGames = new List<VideoPokerGame>();

            for (int i = 1; i <= HandCount; i++)
            {
                VideoPokerGames.Add(new VideoPokerGame(GameType){ Deck = PokerGame.Deck, Hand = PokerGame.Hand});
            }
        }

        private void DrawCredits(int credits)
        {
            CreditsPanel.Children.Clear();
            for (int i = credits.ToString().Length - 1; i >= 0; i--)
            {
                Image j = new Image { Width = 38 };
                string imagepath = "ms-appx:/Assets/numbers/" + credits.ToString()[i] + ".png";
                BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
                j.Source = imagesource;
                CreditsPanel.Children.Add(j);
            }
        }

        private void Card_MouseLeftButtonDown(object sender, TappedRoutedEventArgs e)
        {
            if (!IsShowingCards)
            {
                if (HoldRound)
                {
                    Image image = sender as Image;
                    int place = Int32.Parse(image.Name.Substring(4, 1));
                    if (PokerGame.Hand.Held[place])
                    {
                        PokerGame.Hand.Hold(place, false);
                        for (int i = 1; i < HandCount; i++)
                        {
                            VideoPokerGames[i].Hand.Hold(place, false);
                        }
                    }
                    else
                    {
                        PokerGame.Hand.Hold(place, true);
                        for (int i = 1; i < HandCount; i++)
                        {
                            VideoPokerGames[i].Hand.Hold(place, true);
                        }
                    }
                    UpdateHold();
                }
            }
        }

        private void UpdateHold()
        {
            for (int i = 0; i <= 4; i++)
            {
                Image hold = (Image)FindName("Hold" + i);
                if (PokerGame.Hand.Held[i])
                {
                    hold.Visibility = Visibility.Visible;
                    for (int j = 1; j <= HandCount; j++)
                    {
                        Image card = FindName("Card" + j.ToString() + i.ToString()) as Image;
                        card.Source = GetCardImage(i);
                    }
                }
                else
                {
                    hold.Visibility = Visibility.Collapsed;
                    for (int j = 1; j <= HandCount; j++)
                    {
                        Image card = FindName("Card" + j.ToString() + i.ToString()) as Image;
                        card.Source = CardBackImage;
                    }
                }
            }
        }

        private void Deal_Click(object sender, TappedRoutedEventArgs e)
        {
            Deal();
        }

        private void BetMax_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!HoldRound && !IsShowingCards && !IsDrawingCredits)
            {
                if (GamePlayer.IncreaseBet(5)) Deal();
                //TODO: Build an animation that shows the red box travel to the 5 coin slot.
                //ChangeBetHighlight();
            }
        }

        private void BetOne_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HandCount++;
            if (HandCount == 50) HandCount = 1;
            SetHandCovers();
        }

        private void MoreGames_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

        public void SetHandCovers()
        {
            for (int i = 1; i <= HandCount; i++)
            {
                Rectangle r = FindName("Cover" + i) as Rectangle;
                r.Opacity = 0;
            }
        }

        private void ResetHandCovers(int HandCount)
        {
            for (int i = 1; i <= 49; i++)
            {
                Rectangle r = FindName("Cover" + i) as Rectangle;
                r.Opacity = .3;
            }
        }

        private void Deal()
        {

            if (!IsShowingCards)
            {
                if (!IsDrawingCredits)
                {
                    //StopPayTableAnimations();
                    //ResetBox.Visibility = Visibility.Collapsed;
                    if (!HoldRound)
                    {
                        ClearHolds();
                        ResetCardBacks();
                        ChargeCredits();
                        PokerGame = new VideoPokerGame(GameType);
                        IncrementHandCount();
                        //handCounter++;
                        HoldRound = true;
                        
                    }
                    else
                    {
                        PokerGame.Draw();
                        ResetCardBacks();
                        HoldRound = false;
                    }
                    ShowCards(!HoldRound);
                }
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

        private void ResetCardBacks()
        {
            for (int i = 0; i <= 4; i++)
            {
                if (!PokerGame.Hand.Held[i])
                {
                    Image image = (Image)FindName("Card" + i);
                    image.Source = CardBackImage;
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

        private void IncrementHandCount()
        {
            int handcount = (int)App.settings.Values["totalhandsplayed"];
            App.settings.Values["totalhandsplayed"] = handcount + 1;
        }

        public void ShowCards(bool payUserFlag)
        {
            IsShowingCards = true;
            ShouldPayUser = payUserFlag;

            if (cardCounter <= 4)
            {
                if (!PokerGame.Hand.Held[cardCounter])
                {
                    Image image = (Image)FindName("Card" + cardCounter);
                    image.Source = GetCardImage(cardCounter);
                    cardCounter++;
                    //CardFlip.Play();
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
                    //ResetBox.Visibility = Visibility.Visible;
                    App.settings.Values["credits"] = 10000;
                    DrawCredits(10000);
                }
                //HighlightWinningBetValue(PayTable, ShouldPayUser);
                IsShowingCards = false;
            }
        }

        private BitmapImage GetCardImage(int place)
        {
            string x = String.Empty;
            switch (GameType)
            {
                case "DEUCESWILD":
                case "DOUBLEBONUSDEUCESWILD":
                case "DEUCESWILDBONUSPOKER":
                    if (PokerGame.Hand.Cards[place].Value.Number == 2) x = "w";
                    break;
            }
            string imagepath = "ms-appx:/Assets/cards/" + PokerGame.Hand.Cards[place].Suit.ID.ToString() + PokerGame.Hand.Cards[place].Value.Number.ToString() + x.ToString() + ".png";
            BitmapImage image = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
            return image;
        }

        void CardPause_Completed(object sender, object e)
        {
            ShowCards(ShouldPayUser);
        }

        void CreditPause_Completed(object sender, object e)
        {
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
                //WinLoop.Stop();
                IsDrawingCredits = false;
            }
        }
    }
}
