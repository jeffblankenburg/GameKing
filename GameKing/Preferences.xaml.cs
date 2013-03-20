using System;
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
    public sealed partial class Preferences : UserControl
    {
        public Preferences()
        {
            this.InitializeComponent();
            InitializeElements();
        }

        private void InitializeElements()
        {
            bool keyboard = (bool)App.settings.Values["iskeyboardactive"];
            KeyboardCheckbox.IsChecked = keyboard;
            DisplayName.Text = App.settings.Values["playername"].ToString();
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

        private void KeyboardCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            App.settings.Values["iskeyboardactive"] = true;
        }

        private void KeyboardCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            App.settings.Values["iskeyboardactive"] = false;
        }

        private void DisplayName_TextChanged(object sender, TextChangedEventArgs e)
        {
            App.settings.Values["playername"] = DisplayName.Text;
        }
    }
}
