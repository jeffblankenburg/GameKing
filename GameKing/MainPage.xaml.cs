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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DeucesWild_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DEUCESWILD");
        }

        private void JacksOrBetter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "JACKSORBETTER");
        }

        private void BonusPokerDeluxe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "BONUSPOKERDELUXE");
        }

        private void DoubleBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DOUBLEBONUSPOKER");
        }

        private void RoyalAcesBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "ROYALACESBONUSPOKER");
        }

        private void BonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "BONUSPOKER");
        }

        private void TripleBonusPokerPlus_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "TRIPLEBONUSPOKERPLUS");
        }

        private void WhiteHotAces_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "WHITEHOTACES");
        }
    }
}
