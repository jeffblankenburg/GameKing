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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GameKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        List<PayTableItem> payTable;
        
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
            GameSetup(e.Parameter.ToString());
        }

        private void GameSetup(string gametype)
        {
            LoadPayTable(gametype);
        }

        private void LoadPayTable(string gametype)
        {
            payTable = new List<PayTableItem>();
            
            switch (gametype)
            {
                case "DEUCESWILD":

                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH NO DEUCES", Coin1 = 250, Coin2 = 500, Coin3 = 750, Coin4 = 1000, Coin5 = 4000 });
                    payTable.Add(new PayTableItem { Title = "4 DEUCES", Coin1 = 200, Coin2 = 400, Coin3 = 600, Coin4 = 800, Coin5 = 1000 });
                    payTable.Add(new PayTableItem { Title = "ROYAL FLUSH WITH DEUCES", Coin1 = 20, Coin2 = 40, Coin3 = 60, Coin4 = 80, Coin5 = 100 });
                    payTable.Add(new PayTableItem { Title = "5 OF A KIND", Coin1 = 12, Coin2 = 24, Coin3 = 36, Coin4 = 48, Coin5 = 60 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT FLUSH", Coin1 = 9, Coin2 = 18, Coin3 = 27, Coin4 = 36, Coin5 = 45 });
                    payTable.Add(new PayTableItem { Title = "4 OF A KIND", Coin1 = 5, Coin2 = 10, Coin3 = 15, Coin4 = 20, Coin5 = 25 });
                    payTable.Add(new PayTableItem { Title = "FULL HOUSE", Coin1 = 3, Coin2 = 6, Coin3 = 9, Coin4 = 12, Coin5 = 15 });
                    payTable.Add(new PayTableItem { Title = "FLUSH", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "STRAIGHT", Coin1 = 2, Coin2 = 4, Coin3 = 6, Coin4 = 8, Coin5 = 10 });
                    payTable.Add(new PayTableItem { Title = "3 OF A KIND", Coin1 = 1, Coin2 = 2, Coin3 = 3, Coin4 = 4, Coin5 = 5 });

                    break;
            }

            PayTable.ItemsSource = payTable;
        }
    }
}
