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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GameKing
{
    public sealed partial class HelpPage : UserControl
    {
        string GameName;
        
        public HelpPage()
        {
            InitializeComponent();
        }

        public HelpPage(string GameType)
        {
            InitializeComponent();
            GameName = GameType;
            HelpContent h = new HelpContent(GameName);
            HelpTitle.Text = h.Title;
            SetGameLogo();
            HelpList.ItemsSource = h.HelpItems;
        }

        private void SetGameLogo()
        {
            string imagepath = "ms-appx:/Assets/gamelogo/" + GameName + ".png";
            BitmapImage imagesource = new BitmapImage(new Uri(imagepath, UriKind.Absolute));
            GameLogo.Source = imagesource;
        }
    }
}
