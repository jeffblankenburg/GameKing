using Newtonsoft.Json;
using PokerLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class Stats : UserControl
    {
        public Stats()
        {
            this.InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            StorageFile file = await App.files.CreateFileAsync("handhistory.txt", CreationCollisionOption.OpenIfExists);
            string handtext = await FileIO.ReadTextAsync(file);


            List<BothHands> history = JsonConvert.DeserializeObject<List<BothHands>>(handtext);
            if (history != null)
            {
                StatsList.ItemsSource = (from p in history orderby p.TimeStamp descending select p).Take(50);
            }
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
