using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PokerLogic;
using System.Windows.Media.Imaging;

namespace GameKingWP8
{
    public partial class Help : PhoneApplicationPage
    {
        string GameType;
        
        public Help()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GameType = NavigationContext.QueryString["game"].ToString();
            HelpContent h = new HelpContent(GameType);
            HelpTitle.Text = "STRATEGY FOR " + h.Title;
            SetGameLogo();
            HelpList.ItemsSource = h.HelpItems;
        }

        private void SetGameLogo()
        {
            string imagepath = "Assets/gamelogo/" + GameType + ".png";
            BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Relative));
            GameLogo.Source = imagesource;
        }
    }
}