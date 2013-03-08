﻿using System;
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

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //AdRotatorControl.Invalidate();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SizeChanged += Game_SizeChanged;
            Loaded += MainPage_Loaded;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Loaded -= MainPage_Loaded;
        }

        private void Game_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Windows.UI.Xaml.Window.Current.Bounds.Width <= 320)
            {
                FullScreen.Visibility = Visibility.Collapsed;
                SnappedState.GrabStats();
                SnappedState.Visibility = Visibility.Visible;
            }
            else
            {
                FullScreen.Visibility = Visibility.Visible;
                SnappedState.Visibility = Visibility.Collapsed;
            }
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

        private void SuperAcesBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "SUPERACESBONUSPOKER");
        }

        private void AcesAndFacesPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "ACESANDFACESPOKER");
        }

        private void DoubleBonusDeucesWild_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DOUBLEBONUSDEUCESWILD");
        }

        private void DeucesWildBonusPoker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Game), "DEUCESWILDBONUSPOKER");
        }
    }
}
