using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PokerLogic;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GameKing
{
    public sealed partial class HandStats : UserControl
    {
        public static List<Suit> keys = new List<Suit>();
        
        public HandStats()
        {
            this.InitializeComponent();
            Loaded += HandStats_Loaded;
        }

        void HandStats_Loaded(object sender, RoutedEventArgs e)
        {
            GrabStats();
            
        }

        public void GrabStats()
        {
            int length = App.settings.Values.Count();
            keys.Clear();

            for (int i = 0; i < length; i++)
            {
                if (App.settings.Values.ElementAt(i).Key.Contains("COUNT_") && ((int)App.settings.Values.ElementAt(i).Value != 0))
                {
                    keys.Add(new Suit{ Name = App.settings.Values.ElementAt(i).Key.Replace("COUNT_", ""), ID = (int)App.settings.Values.ElementAt(i).Value}); 
                }
            }

            StatsList.ItemsSource = from k in keys
                                    orderby k.ID descending
                                    select k;

            HandCountText.Text = "IN " + ((int)App.settings.Values["totalhandsplayed"]).ToString("N0") + " HANDS, YOU HAD";
            CreditCountText.Text = "YOU PLAYED " + ((int)App.settings.Values["totalcreditsplayed"]).ToString("N0") + " CREDITS";
        }
    }
}
