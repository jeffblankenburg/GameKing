﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PokerLogic;

namespace GameKingWP8
{
    public partial class Stats : PhoneApplicationPage
    {
        public Stats()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<BothHands> history = App.settings["handhistory"] as List<BothHands>;
            StatsList.ItemsSource = (from p in history
                                    orderby p.TimeStamp descending
                                    select p).Take(100);
        }
    }
}