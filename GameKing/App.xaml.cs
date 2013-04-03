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
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using System.Threading.Tasks;

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
        public DateTime AlertDate;
        
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
            AlertDate = new DateTime(2013,3,22);
            
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

            if (!settings.Values.ContainsKey("alertdate"))
            {
                settings.Values["showalertbox"] = true;
                settings.Values["alertdate"] = DateTime.Now.ToString();
            }

            if (DateTime.Parse(settings.Values["alertdate"].ToString()) < AlertDate)
            {
                settings.Values["showalertbox"] = true;
            }

            if (!settings.Values.ContainsKey("achievements"))
            {
                WriteAchievements();
                settings.Values["achievements"] = true;
            }

            WriteAchievements();

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

        private async void WriteAchievements()
        {
            List<Achievement> achievementlist = BuildAchievements();
            StorageFile file = await App.files.CreateFileAsync("achievements.txt", CreationCollisionOption.OpenIfExists);
            string achievementtext = JsonConvert.SerializeObject(achievementlist);
            file = await App.files.CreateFileAsync("achievements.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, achievementtext);
        }

        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand command = new SettingsCommand("about", "Feedback & Support", (handler) =>
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

            args.Request.ApplicationCommands.Add(command4);
            args.Request.ApplicationCommands.Add(command2);
            args.Request.ApplicationCommands.Add(command3);
            args.Request.ApplicationCommands.Add(command);
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
            if (history != null)
            {
                if (history.Count != 0)
                {
                    List<BothHands> historySorted = (from h in history orderby h.TimeStamp descending select h).Take(50).ToList<BothHands>();

                    for (int i = 0; i < history.Count; i++)
                    {
                        if (!history[i].IsOnline)
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
                    }

                    handtext = JsonConvert.SerializeObject(historySorted);
                    file = await App.files.CreateFileAsync("handhistory2.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file, handtext);

                    try
                    {
                        if (App.settings.Values["microsoftuserid"].ToString().Contains("MicrosoftAccount"))
                        {
                            IMobileServiceTable<HandHistory> table = App.MobileService.GetTable<HandHistory>();
                            MobileServiceTableQuery<HandHistory> query = table.Where(i => i.MicrosoftAccountID == settings.Values["microsoftuserid"].ToString()).OrderByDescending(m => m.DatePlayed).Select(k => k).Take(1);
                            List<HandHistory> credits = await query.ToListAsync();
                            if (credits.Count != 0)
                            {
                                settings.Values["credits"] = credits[0].Credits;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        if (App.settings.Values["microsoftuserid"].ToString().Contains("MicrosoftAccount"))
                        {
                            IMobileServiceTable<HandHistory> table = App.MobileService.GetTable<HandHistory>();
                            MobileServiceTableQuery<HandHistory> query = table.Where(i => i.MicrosoftAccountID == settings.Values["microsoftuserid"].ToString()).OrderByDescending(m => m.DatePlayed).Select(k => k).Take(1);
                            List<HandHistory> credits = await query.ToListAsync();
                            if (credits.Count != 0)
                            {
                                settings.Values["credits"] = credits[0].Credits;
                            }
                        }
                    }
                catch (Exception ex)
                {

                }
                }
            }
            else
            {
                try
                {
                    if (App.settings.Values["microsoftuserid"].ToString().Contains("MicrosoftAccount"))
                    {
                        IMobileServiceTable<HandHistory> table = App.MobileService.GetTable<HandHistory>();
                        MobileServiceTableQuery<HandHistory> query = table.Where(i => i.MicrosoftAccountID == settings.Values["microsoftuserid"].ToString()).OrderByDescending(m => m.DatePlayed).Select(k => k).Take(1);
                        List<HandHistory> credits = await query.ToListAsync();
                        if (credits.Count != 0)
                        {
                            settings.Values["credits"] = credits[0].Credits;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void UpdateLiveTiles(string outcome, string gametype)
        {
            //Create the Large Tile exactly the same way.
            XmlDocument largeTileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWidePeekImage02);
            XmlNodeList largeTextData = largeTileData.GetElementsByTagName("text");
            XmlNodeList imageData = largeTileData.GetElementsByTagName("image");
            largeTextData[0].InnerText = settings.Values["credits"].ToString() + " credits";
            largeTextData[2].InnerText = outcome;
            largeTextData[3].InnerText = DateTime.Now.ToString();
            ((XmlElement)imageData[0]).SetAttribute("src", "ms-appx:///Assets/gamelogo/" + gametype + ".png");
            

            //Create a Small Tile notification also (not required, but recommended.)
            XmlDocument smallTileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText03);
            XmlNodeList smallTileText = smallTileData.GetElementsByTagName("text");
            smallTileText[0].InnerText = settings.Values["credits"].ToString() + " CREDITS";
            smallTileText[1].InnerText = outcome;
            smallTileText[2].InnerText = DateTime.Now.ToString("d");
            smallTileText[3].InnerText = DateTime.Now.ToString("t");

            //Merge the two updates into one <visual> XML node
            IXmlNode newNode = largeTileData.ImportNode(smallTileData.GetElementsByTagName("binding").Item(0), true);
            largeTileData.GetElementsByTagName("visual").Item(0).AppendChild(newNode);

            //Create the notification the same way.
            TileNotification notification = new TileNotification(largeTileData);
            notification.ExpirationTime = DateTimeOffset.UtcNow.AddDays(5);

            //Push the update to the tile.
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);

        }

        public List<Achievement> BuildAchievements()
        {
            List<Achievement> list = new List<Achievement>();

            list.Add(new Achievement { Id = 101, Icon = "ms-appx:///Assets/achievements/DEUCES_3 OF A KIND_100.png", Title = "100 3s of a Kind", Text = "Complete 100 hands with 3 of a Kind in a Deuces Wild game.", Points = 10, Outcome = "3 OF A KIND", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 102, Icon = "ms-appx:///Assets/achievements/DEUCES_3 OF A KIND_250.png", Title = "250 3s of a Kind", Text = "Complete 250 hands with 3 of a Kind in a Deuces Wild game.", Points = 25, Outcome = "3 OF A KIND", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 103, Icon = "ms-appx:///Assets/achievements/DEUCES_3 OF A KIND_500.png", Title = "500 3s of a Kind", Text = "Complete 500 hands with 3 of a Kind in a Deuces Wild game.", Points = 50, Outcome = "3 OF A KIND", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 104, Icon = "ms-appx:///Assets/achievements/DEUCES_3 OF A KIND_1000.png", Title = "100 3s of a Kind", Text = "Complete 1000 hands with 3 of a Kind in a Deuces Wild game.", Points = 100, Outcome = "3 OF A KIND", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 105, Icon = "ms-appx:///Assets/achievements/DEUCES_STRAIGHT_100.png", Title = "100 Straights", Text = "Complete 100 hands with a Straight in a Deuces Wild game.", Points = 10, Outcome = "STRAIGHT", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 106, Icon = "ms-appx:///Assets/achievements/DEUCES_STRAIGHT_250.png", Title = "250 Straights", Text = "Complete 250 hands with a Straight in a Deuces Wild game.", Points = 25, Outcome = "STRAIGHT", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 107, Icon = "ms-appx:///Assets/achievements/DEUCES_STRAIGHT_500.png", Title = "500 Straights", Text = "Complete 500 hands with a Straight in a Deuces Wild game.", Points = 50, Outcome = "STRAIGHT", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 108, Icon = "ms-appx:///Assets/achievements/DEUCES_STRAIGHT_1000.png", Title = "1000 Straights", Text = "Complete 1000 hands with a Straight in a Deuces Wild game.", Points = 100, Outcome = "STRAIGHT", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 109, Icon = "ms-appx:///Assets/achievements/DEUCES_FLUSH_100.png", Title = "100 Flushes", Text = "Complete 100 hands with a Flush in a Deuces Wild game.", Points = 10, Outcome = "FLUSH", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 110, Icon = "ms-appx:///Assets/achievements/DEUCES_FLUSH_250.png", Title = "250 Flushes", Text = "Complete 250 hands with a Flush in a Deuces Wild game.", Points = 25, Outcome = "FLUSH", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 111, Icon = "ms-appx:///Assets/achievements/DEUCES_FLUSH_500.png", Title = "500 Flushes", Text = "Complete 500 hands with a Flush in a Deuces Wild game.", Points = 50, Outcome = "FLUSH", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 112, Icon = "ms-appx:///Assets/achievements/DEUCES_FLUSH_1000.png", Title = "1000 Flushes", Text = "Complete 1000 hands with a Flush in a Deuces Wild game.", Points = 100, Outcome = "FLUSH", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 113, Icon = "ms-appx:///Assets/achievements/DEUCES_FULL HOUSE_100.png", Title = "100 Full Houses", Text = "Complete 100 hands with a Full House in a Deuces Wild game.", Points = 10, Outcome = "FULL HOUSE", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 114, Icon = "ms-appx:///Assets/achievements/DEUCES_FULL HOUSE_250.png", Title = "250 Full Houses", Text = "Complete 250 hands with a Full House in a Deuces Wild game.", Points = 25, Outcome = "FULL HOUSE", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 115, Icon = "ms-appx:///Assets/achievements/DEUCES_FULL HOUSE_500.png", Title = "500 Full Houses", Text = "Complete 500 hands with a Full House in a Deuces Wild game.", Points = 50, Outcome = "FULL HOUSE", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 116, Icon = "ms-appx:///Assets/achievements/DEUCES_FULL HOUSE_1000.png", Title = "1000 Full Houses", Text = "Complete 1000 hands with a Full House in a Deuces Wild game.", Points = 100, Outcome = "FULL HOUSE", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 117, Icon = "ms-appx:///Assets/achievements/DEUCES_4 OF A KIND_100.png", Title = "100 4s of a Kind", Text = "Complete 100 hands with 4 of a Kind in a Deuces Wild game.", Points = 10, Outcome = "4 OF A KIND", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 118, Icon = "ms-appx:///Assets/achievements/DEUCES_4 OF A KIND_250.png", Title = "250 4s of a Kind", Text = "Complete 250 hands with 4 of a Kind in a Deuces Wild game.", Points = 25, Outcome = "4 OF A KIND", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 119, Icon = "ms-appx:///Assets/achievements/DEUCES_4 OF A KIND_500.png", Title = "500 4s of a Kind", Text = "Complete 500 hands with 4 of a Kind in a Deuces Wild game.", Points = 50, Outcome = "4 OF A KIND", GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 120, Icon = "ms-appx:///Assets/achievements/DEUCES_4 OF A KIND_1000.png", Title = "1000 4s of a Kind", Text = "Complete 1000 hands with 4 of a Kind in a Deuces Wild game.", Points = 100, Outcome = "4 OF A KIND", GameType = "DEUCESWILD" });

            return list;
        }

        public static async Task CheckForAchievement(string gametype, string outcome)
        {
            StorageFile file = await App.files.CreateFileAsync("achievements.txt", CreationCollisionOption.OpenIfExists);
            string achievementtext = await FileIO.ReadTextAsync(file);
            List<Achievement> achievements = JsonConvert.DeserializeObject<List<Achievement>>(achievementtext);

            int x = 0;
            Achievement a = null;

            if (gametype.Contains("DEUCES"))
            {
                if (App.settings.Values.ContainsKey("COUNT_DEUCESWILD_" + outcome)) x += (int)App.settings.Values["COUNT_DEUCESWILD_" + outcome];
                if (App.settings.Values.ContainsKey("COUNT_DEUCESWILDBONUSPOKER_" + outcome)) x += (int)App.settings.Values["COUNT_DEUCESWILDBONUSPOKER_" + outcome];
                if (App.settings.Values.ContainsKey("COUNT_DOUBLEDONUSDEUCESWILD_" + outcome)) x += (int)App.settings.Values["COUNT_DOUBLEDONUSDEUCESWILD_" + outcome];
            }
            else if (gametype.Contains("JOKER"))
            {
                if (App.settings.Values.ContainsKey("COUNT_JOKERPOKER_" + outcome)) x += (int)App.settings.Values["COUNT_JOKERPOKER_" + outcome];
            }
            else
            {

            }
            

            switch(outcome)
            {
                case "3 OF A KIND":
                    if (gametype.Contains("DEUCES"))
                    {
                        if ((x >= 1000) && (achievements.Find(n => n.Id == 104).IsCompleted == false)) { a = achievements.Find(n => n.Id == 104); }
                        else if ((x >= 500) && (achievements.Find(n => n.Id == 103).IsCompleted == false)) { a = achievements.Find(n => n.Id == 103); }
                        else if ((x >= 250) && (achievements.Find(n => n.Id == 102).IsCompleted == false)) { a = achievements.Find(n => n.Id == 102); }
                        else if ((x >= 100) && (achievements.Find(n => n.Id == 101).IsCompleted == false)) { a = achievements.Find(n => n.Id == 101); }
                    }
                    break;
                case "STRAIGHT":
                    if (gametype.Contains("DEUCES"))
                    {
                        if ((x >= 1000) && (achievements.Find(n => n.Id == 108).IsCompleted == false)) { a = achievements.Find(n => n.Id == 108); }
                        else if ((x >= 500) && (achievements.Find(n => n.Id == 107).IsCompleted == false)) { a = achievements.Find(n => n.Id == 107); }
                        else if ((x >= 250) && (achievements.Find(n => n.Id == 106).IsCompleted == false)) { a = achievements.Find(n => n.Id == 106); }
                        else if ((x >= 100) && (achievements.Find(n => n.Id == 105).IsCompleted == false)) { a = achievements.Find(n => n.Id == 105); }
                    }
                    break;
                case "FLUSH":
                    if (gametype.Contains("DEUCES"))
                    {
                        if ((x >= 1000) && (achievements.Find(n => n.Id == 112).IsCompleted == false)) { a = achievements.Find(n => n.Id == 112); }
                        else if ((x >= 500) && (achievements.Find(n => n.Id == 111).IsCompleted == false)) { a = achievements.Find(n => n.Id == 111); }
                        else if ((x >= 250) && (achievements.Find(n => n.Id == 110).IsCompleted == false)) { a = achievements.Find(n => n.Id == 110); }
                        else if ((x >= 100) && (achievements.Find(n => n.Id == 109).IsCompleted == false)) { a = achievements.Find(n => n.Id == 109); }
                    }
                    break;
                case "FULL HOUSE":
                    if (gametype.Contains("DEUCES"))
                    {
                        if ((x >= 1000) && (achievements.Find(n => n.Id == 116).IsCompleted == false)) { a = achievements.Find(n => n.Id == 116); }
                        else if ((x >= 500) && (achievements.Find(n => n.Id == 115).IsCompleted == false)) { a = achievements.Find(n => n.Id == 115); }
                        else if ((x >= 250) && (achievements.Find(n => n.Id == 114).IsCompleted == false)) { a = achievements.Find(n => n.Id == 114); }
                        else if ((x >= 100) && (achievements.Find(n => n.Id == 113).IsCompleted == false)) { a = achievements.Find(n => n.Id == 113); }
                    }
                    break;
                case "4 OF A KIND":
                    if (gametype.Contains("DEUCES"))
                    {
                        if ((x >= 1000) && (achievements.Find(n => n.Id == 120).IsCompleted == false)) { a = achievements.Find(n => n.Id == 120); }
                        else if ((x >= 500) && (achievements.Find(n => n.Id == 119).IsCompleted == false)) { a = achievements.Find(n => n.Id == 119); }
                        else if ((x >= 250) && (achievements.Find(n => n.Id == 118).IsCompleted == false)) { a = achievements.Find(n => n.Id == 118); }
                        else if ((x >= 100) && (achievements.Find(n => n.Id == 117).IsCompleted == false)) { a = achievements.Find(n => n.Id == 117); }
                    }
                    break;
                case "STRAIGHT FLUSH":
                    if (gametype.Contains("DEUCES"))
                    {
                        if ((x >= 1000) && (achievements.Find(n => n.Id == 124).IsCompleted == false)) { a = achievements.Find(n => n.Id == 124); }
                        else if ((x >= 500) && (achievements.Find(n => n.Id == 123).IsCompleted == false)) { a = achievements.Find(n => n.Id == 123); }
                        else if ((x >= 250) && (achievements.Find(n => n.Id == 122).IsCompleted == false)) { a = achievements.Find(n => n.Id == 122); }
                        else if ((x >= 100) && (achievements.Find(n => n.Id == 121).IsCompleted == false)) { a = achievements.Find(n => n.Id == 121); }
                    }
                    break;
            }

            if (a != null)
            {
                a.IsCompleted = true;
                ToastTemplateType toastType = ToastTemplateType.ToastImageAndText02;
                XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(toastType);
                XmlNodeList toastText = toastXML.GetElementsByTagName("text");
                XmlNodeList toastImages = toastXML.GetElementsByTagName("image");
                toastText[0].InnerText = a.Title;
                toastText[1].InnerText = a.Text;
                ((XmlElement)toastImages[0]).SetAttribute("src", a.Icon);
                ((XmlElement)toastImages[0]).SetAttribute("alt", "King Poker");

                ToastNotification toast = new ToastNotification(toastXML);
                
                ToastNotificationManager.CreateToastNotifier().Show(toast);

                achievementtext = JsonConvert.SerializeObject(achievements);
                file = await App.files.CreateFileAsync("achievements.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, achievementtext);
            }
        }
    }
}
