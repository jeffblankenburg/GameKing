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
    public sealed partial class Game : Page
    {
        List<PayTableItem> payTable;
        VideoPokerGame PokerGame;
        Player GamePlayer = new Player();
        string GameType;
        bool HoldRound = false;


        SolidColorBrush Blue = new SolidColorBrush { Color = new Utility().HexToColor("#FF000064") };
        SolidColorBrush Red = new SolidColorBrush { Color = new Utility().HexToColor("#FFB00000") };
        
        public Game()
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
            GameSetup();
        }

        private void GameSetup()
        {
            PokerGame = new VideoPokerGame(GameType);
            LoadPayTable();
        }

        private void LoadPayTable()
        {
            payTable = new List<PayTableItem>();

            switch (GameType)
            {
                case "DEUCESWILD":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH NO DEUCES........", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "4 DEUCES.....................................", Coin1 = 200, Coin2 = 400, Coin3 = 600, Coin4 = 800, Coin5 = 1000 });
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH WITH DEUCES....", Coin1 = 20, Coin2 = 40, Coin3 = 60, Coin4 = 80, Coin5 = 100 });
                    payTable.Add(new PayTableItem { Title = "5 OF A KIND................................", Coin1 = 12, Coin2 = 24, Coin3 = 36, Coin4 = 48, Coin5 = 60 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
                case "JACKSORBETTER":
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH..............................", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH.......................", Coin1 = 50, Coin2 = 100, Coin3 = 150, Coin4 = 200, Coin5 = 250 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND................................", Coin1 = 25, Coin2 = 50, Coin3 = 75, Coin4 = 100, Coin5 = 125 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE................................", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "FLUSH..........................................", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT....................................", Coin1 = 4, Coin2 = 8, Coin3 = 12, Coin4 = 16, Coin5 = 20 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND................................", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "TWO PAIR....................................", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "JACKS OR BETTER.......................", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });
                    break;
            }

            PayTable.ItemsSource = payTable;
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
            //if (!HoldRound)
            //{
            //    ClearHolds();
            //    PokerGame = new VideoPokerGame(GameType);
            //    HoldRound = true;
            //}
            //else
            //{
            //    PokerGame.Draw();
            //    HoldRound = false;
            //    ShowHandResult();
            //}
            //ShowCards();
        }

        private void ShowHandResult()
        {
            switch (PokerGame.CheckHand())
            {
                case "ROYALFLUSH":

                    break;
                case "THREEOFAKIND":
                    break;
                case "JACKSORBETTER":

                    break;
            }
        }

        public void ShowCards()
        {
            for (int i = 0; i <= 4; i++)
            {
                Image image = (Image)FindName("Card" + i);
                image.Source = PokerGame.Hand.Cards[i].Image;
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
    }
}
