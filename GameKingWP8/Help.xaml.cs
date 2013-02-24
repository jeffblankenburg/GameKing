﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Windows.System;

namespace GameKingWP8
{
    public partial class Help : PhoneApplicationPage
    {
        public Help()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EmailComposeTask ect = new EmailComposeTask();
            ect.To = "kingpoker@jexed.com";
            ect.Subject = "Comment from King Poker on Windows Phone";
            ect.Show();
        }

        private async void Phone_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://www.windowsphone.com/en-us/store/app/king-poker/0eccfa62-3566-420f-9955-d4e108ba247a", UriKind.Absolute));
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://jeffblankenburg.com");
            wbt.Show();
        }
    }
}