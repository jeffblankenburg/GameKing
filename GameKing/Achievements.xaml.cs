using Newtonsoft.Json;
using PokerLogic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GameKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Achievements : Page
    {
        public Achievements()
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
            LoadAchievements();
        }

        private async void LoadAchievements()
        {
            StorageFile file = await App.files.CreateFileAsync("achievements.txt", CreationCollisionOption.OpenIfExists);
            string achievementtext = await FileIO.ReadTextAsync(file);
            List<Achievement> achievements = JsonConvert.DeserializeObject<List<Achievement>>(achievementtext);
            AchievementList.ItemsSource = from a in achievements orderby a.IsCompleted orderby a.Id ascending select a;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
