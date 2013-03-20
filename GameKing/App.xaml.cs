using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Storage;
using PokerLogic;
using Newtonsoft.Json;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Media.Animation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace GameKing
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://kingpoker.azure-mobile.net/","TKrwESHysONzEMdZtNMlQrPbNzdjPB94");
        public static ApplicationDataContainer settings;
        public static StorageFolder files;
        
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
            
            settings = ApplicationData.Current.LocalSettings;
            files = ApplicationData.Current.LocalFolder;

            if (!settings.Values.ContainsKey("credits"))
            {
                settings.Values["credits"] = 10000;
            }

            if (!settings.Values.ContainsKey("totalcreditsplayed"))
            {
                settings.Values["totalcreditsplayed"] = 0;
            }

            if (!settings.Values.ContainsKey("totalhandsplayed"))
            {
                settings.Values["totalhandsplayed"] = 0;
            }

            if (!settings.Values.ContainsKey("microsoftuserid"))
            {
                settings.Values["microsoftuserid"] = String.Empty;
            }

            if (!settings.Values.ContainsKey("iskeyboardactive"))
            {
                settings.Values["iskeyboardactive"] = true;
            }

            if (!settings.Values.ContainsKey("playername"))
            {
                settings.Values["playername"] = String.Empty;
            }

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand command = new SettingsCommand("about", "About This App", (handler) =>
            {
                Popup popup = App.BuildSettingsItem(new AboutPage(), 346);
                popup.IsOpen = true;
            });

            SettingsCommand command2 = new SettingsCommand("privacy", "Privacy Policy", (handler) =>
            {
                Popup popup = App.BuildSettingsItem(new PrivacyPolicyPage(), 346);
                popup.IsOpen = true;
            });

            SettingsCommand command3 = new SettingsCommand("stats", "Your Poker Stats", (handler) =>
            {
                Popup popup = App.BuildSettingsItem(new Stats(), 646);
                popup.IsOpen = true;
            });

            SettingsCommand command4 = new SettingsCommand("preferences", "Preferences", (handler) =>
            {
                Popup popup = App.BuildSettingsItem(new Preferences(), 346);
                popup.IsOpen = true;
            });

            args.Request.ApplicationCommands.Add(command);
            args.Request.ApplicationCommands.Add(command4);
            args.Request.ApplicationCommands.Add(command2);
            args.Request.ApplicationCommands.Add(command3);
        }

        public static Popup BuildSettingsItem(UserControl page, int width)
        {
            Popup p = new Popup();
            p.IsLightDismissEnabled = true;
            p.ChildTransitions = new TransitionCollection();
            p.ChildTransitions.Add(new PaneThemeTransition()
            {
                Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ?
                        EdgeTransitionLocation.Right :
                        EdgeTransitionLocation.Left
            });

            page.Width = width;
            page.Height = Window.Current.Bounds.Height;
            p.Child = page;

            p.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (Window.Current.Bounds.Width - width) : 0);
            p.SetValue(Canvas.TopProperty, 0);

            return p;

        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
           
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        public static async void SaveOldHandData()
        {
            StorageFile file = await App.files.CreateFileAsync("handhistory2.txt", CreationCollisionOption.OpenIfExists);
            string handtext = await FileIO.ReadTextAsync(file);
            List<BothHands> history = JsonConvert.DeserializeObject<List<BothHands>>(handtext);
            if (history.Count != 0)
            {
                List<BothHands> historySorted = (from h in history orderby h.TimeStamp descending select h).Take(50).ToList<BothHands>();

                for (int i = 0; i < history.Count; i++)
                {
                    try
                    {
                        HandHistory h = new HandHistory((string)App.settings.Values["microsoftuserid"], history[i].CreditCount, history[i].OpeningHand, history[i].ClosingHand, history[i].GameType, history[i].TimeStamp, history[i].IsSnapped, "WINDOWS 8");
                        await App.MobileService.GetTable<HandHistory>().InsertAsync(h);
                        history[i].IsOnline = true;
                    }
                    catch (Exception ex)
                    {

                    }
                }

                handtext = JsonConvert.SerializeObject(historySorted);
                file = await App.files.CreateFileAsync("handhistory2.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, handtext);

                IMobileServiceTable<HandHistory> table = App.MobileService.GetTable<HandHistory>();
                MobileServiceTableQuery<HandHistory> query = table.Where(i => i.MicrosoftAccountID == settings.Values["microsoftuserid"].ToString()).OrderByDescending(m => m.DatePlayed).Select(k => k).Take(1);
                List<HandHistory> credits = await query.ToListAsync();

                if (credits[0].DatePlayed > historySorted[0].TimeStamp)
                {
                    settings.Values["credits"] = credits[0].Credits;
                }
            }
            else
            {
                IMobileServiceTable<HandHistory> table = App.MobileService.GetTable<HandHistory>();
                MobileServiceTableQuery<HandHistory> query = table.Where(i => i.MicrosoftAccountID == settings.Values["microsoftuserid"].ToString()).OrderByDescending(m => m.DatePlayed).Select(k => k).Take(1);
                List<HandHistory> credits = await query.ToListAsync();
                
                settings.Values["credits"] = credits[0].Credits;
            }
        }
    }
}
