﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GameKing
{
    public sealed partial class AboutPage : UserControl
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }

        private async void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var mailto = new Uri("mailto:kingpoker@jexed.com");
            await Windows.System.Launcher.LaunchUriAsync(mailto);

        }

        private void Phone_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Win8_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Popup parent = this.Parent as Popup;
            if (parent != null)
            {
                parent.IsOpen = false;
            }

            // If the app is not snapped, then the back button shows the Settings pane again.
            if (Windows.UI.ViewManagement.ApplicationView.Value != Windows.UI.ViewManagement.ApplicationViewState.Snapped)
            {
                SettingsPane.Show();
            }

        }
    }
}
