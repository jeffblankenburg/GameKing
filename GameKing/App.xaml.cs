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

            list.Add(new Achievement { Id = 101, Icon = "ms-appx:///Assets/Logo.png", Title = "100 Jacks or Better", Text = "Complete 100 hands with Jacks or Better in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 102, Icon = "ms-appx:///Assets/Logo.png", Title = "250 Jacks or Better", Text = "Complete 250 hands with Jacks or Better in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 103, Icon = "ms-appx:///Assets/Logo.png", Title = "500 Jacks or Better", Text = "Complete 100 hands with Jacks or Better in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 104, Icon = "ms-appx:///Assets/Logo.png", Title = "1000 Jacks or Better", Text = "Complete 1000 hands with Jacks or Better in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 105, Icon = "ms-appx:///Assets/Logo.png", Title = "100 Straights", Text = "Complete 100 hands with a Straight in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 106, Icon = "ms-appx:///Assets/Logo.png", Title = "250 Straights", Text = "Complete 250 hands with a Straight in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 107, Icon = "ms-appx:///Assets/Logo.png", Title = "500 Straights", Text = "Complete 100 hands with a Straight in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 108, Icon = "ms-appx:///Assets/Logo.png", Title = "1000 Straights", Text = "Complete 1000 hands with a Straight in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 109, Icon = "ms-appx:///Assets/Logo.png", Title = "100 Flushes", Text = "Complete 100 hands with a Flush in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 110, Icon = "ms-appx:///Assets/Logo.png", Title = "250 Flushes", Text = "Complete 250 hands with a Flush in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 111, Icon = "ms-appx:///Assets/Logo.png", Title = "500 Flushes", Text = "Complete 100 hands with a Flush in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 112, Icon = "ms-appx:///Assets/Logo.png", Title = "1000 Flushes", Text = "Complete 1000 hands with a Flush in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 113, Icon = "ms-appx:///Assets/Logo.png", Title = "100 Full Houses", Text = "Complete 100 hands with a Full House in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 114, Icon = "ms-appx:///Assets/Logo.png", Title = "250 Full Houses", Text = "Complete 250 hands with a Full House in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 115, Icon = "ms-appx:///Assets/Logo.png", Title = "500 Full Houses", Text = "Complete 100 hands with a Full House in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 116, Icon = "ms-appx:///Assets/Logo.png", Title = "1000 Full Houses", Text = "Complete 1000 hands with a Full House in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 117, Icon = "ms-appx:///Assets/Logo.png", Title = "100 4s of a Kind", Text = "Complete 100 hands with 4 of a Kind in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 118, Icon = "ms-appx:///Assets/Logo.png", Title = "250 4s of a Kind", Text = "Complete 250 hands with 4 of a Kind in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 119, Icon = "ms-appx:///Assets/Logo.png", Title = "500 4s of a Kind", Text = "Complete 100 hands with 4 of a Kind in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 120, Icon = "ms-appx:///Assets/Logo.png", Title = "1000 4s of a Kind", Text = "Complete 1000 hands with 4 of a Kind in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 121, Icon = "ms-appx:///Assets/Logo.png", Title = "100 Straight Flushes", Text = "Complete 100 hands with a Straight Flush in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 122, Icon = "ms-appx:///Assets/Logo.png", Title = "250 Straight Flushes", Text = "Complete 250 hands with a Straight Flush in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 123, Icon = "ms-appx:///Assets/Logo.png", Title = "500 Straight Flushes", Text = "Complete 100 hands with a Straight Flush in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 124, Icon = "ms-appx:///Assets/Logo.png", Title = "1000 Straight Flushes", Text = "Complete 1000 hands with a Straight Flush in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 125, Icon = "ms-appx:///Assets/Logo.png", Title = "100 5s of a Kind", Text = "Complete 100 hands with 5 of a Kind in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 126, Icon = "ms-appx:///Assets/Logo.png", Title = "250 5s of a Kind", Text = "Complete 250 hands with 5 of a Kind in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 127, Icon = "ms-appx:///Assets/Logo.png", Title = "500 5s of a Kind", Text = "Complete 100 hands with 5 of a Kind in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 128, Icon = "ms-appx:///Assets/Logo.png", Title = "1000 5s of a Kind", Text = "Complete 1000 hands with 5 of a Kind in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 129, Icon = "ms-appx:///Assets/Logo.png", Title = "50 Royal Flushes with Deuces", Text = "Complete 50 hands with a Royal Flush with Deuces in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 130, Icon = "ms-appx:///Assets/Logo.png", Title = "100 Royal Flushes with Deuces", Text = "Complete 100 hands with a Royal Flush with Deuces in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 131, Icon = "ms-appx:///Assets/Logo.png", Title = "250 Royal Flushes with Deuces", Text = "Complete 250 hands with a Royal Flush with Deuces in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 132, Icon = "ms-appx:///Assets/Logo.png", Title = "500 Royal Flushes with Deuces", Text = "Complete 500 hands with a Royal Flush with Deuces in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 133, Icon = "ms-appx:///Assets/Logo.png", Title = "1 Four Deuces", Text = "Complete 1 hand with 4 Deuces in a Deuces Wild game.", Points = 10, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 134, Icon = "ms-appx:///Assets/Logo.png", Title = "5 Four Deuces", Text = "Complete 5 hands with 4 Deuces in a Deuces Wild game.", Points = 25, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 135, Icon = "ms-appx:///Assets/Logo.png", Title = "10 Four Deuces", Text = "Complete 10 hands with 4 Deuces in a Deuces Wild game.", Points = 50, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 136, Icon = "ms-appx:///Assets/Logo.png", Title = "25 Four Deuces", Text = "Complete 25 hands with 4 Deuces in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 137, Icon = "ms-appx:///Assets/Logo.png", Title = "1 Royal Flush", Text = "Complete 1 hand with a Royal Flush in a Deuces Wild game.", Points = 100, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 138, Icon = "ms-appx:///Assets/Logo.png", Title = "3 Royal Flushes", Text = "Complete 3 hands with a Royal Flush in a Deuces Wild game.", Points = 250, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 139, Icon = "ms-appx:///Assets/Logo.png", Title = "5 Royal Flushes", Text = "Complete 5 hands with a Royal Flush in a Deuces Wild game.", Points = 500, GameType = "DEUCESWILD" });
            list.Add(new Achievement { Id = 140, Icon = "ms-appx:///Assets/Logo.png", Title = "10 Royal Flushes", Text = "Complete 10 hands with a Royal Flush in a Deuces Wild game.", Points = 1000, GameType = "DEUCESWILD" });
            return list;
        }

        public static async Task CheckForAchievement(string gametype, string outcome)
        {
            StorageFile file = await App.files.CreateFileAsync("achievements.txt", CreationCollisionOption.OpenIfExists);
            string achievementtext = await FileIO.ReadTextAsync(file);
            List<Achievement> achievements = JsonConvert.DeserializeObject<List<Achievement>>(achievementtext);

            int x = 0;
            Achievement a = null;

            switch (gametype)
            {
                case "DEUCESWILD": case "DOUBLEBONUSDEUCESWILD": case "DEUCESWILDBONUSPOKER":
                    //3 OF A KIND
                    x = 0;
                    if (App.settings.Values.ContainsKey("COUNT_DEUCESWILD_3 OF A KIND")) x += (int)App.settings.Values["COUNT_DEUCESWILD_3 OF A KIND"];
                    if (App.settings.Values.ContainsKey("COUNT_DOUBLEBONUSDEUCESWILD_3 OF A KIND")) x += (int)App.settings.Values["COUNT_DOUBLEBONUSDEUCESWILD_3 OF A KIND"];
                    if (App.settings.Values.ContainsKey("COUNT_DEUCESWILDBONUSPOKER_3 OF A KIND")) x += (int)App.settings.Values["COUNT_DEUCESWILDBONUSPOKER_3 OF A KIND"];
                    if ((x >= 1000) && (achievements.Find(n => n.Id == 104).IsCompleted == false)) { a = achievements.Find(n => n.Id == 104); break; }
                    else if ((x >= 500) && (achievements.Find(n => n.Id == 103).IsCompleted == false)) { a = achievements.Find(n => n.Id == 103); break; }
                    else if ((x >= 250) && (achievements.Find(n => n.Id == 102).IsCompleted == false)) { a = achievements.Find(n => n.Id == 102); break; }
                    else if ((x >= 100) && (achievements.Find(n => n.Id == 101).IsCompleted == false)) { a = achievements.Find(n => n.Id == 101); break; }
                    //STRAIGHT
                    x = 0;
                    if (App.settings.Values.ContainsKey("COUNT_DEUCESWILD_STRAIGHT")) x += (int)App.settings.Values["COUNT_DEUCESWILD_STRAIGHT"];
                    if (App.settings.Values.ContainsKey("COUNT_DOUBLEBONUSDEUCESWILD_STRAIGHT")) x += (int)App.settings.Values["COUNT_DOUBLEBONUSDEUCESWILD_STRAIGHT"];
                    if (App.settings.Values.ContainsKey("COUNT_DEUCESWILDBONUSPOKER_STRAIGHT")) x += (int)App.settings.Values["COUNT_DEUCESWILDBONUSPOKER_STRAIGHT"];
                    if ((x >= 1000) && (achievements.Find(n => n.Id == 108).IsCompleted == false)) { a = achievements.Find(n => n.Id == 108); break; }
                    else if ((x >= 500) && (achievements.Find(n => n.Id == 107).IsCompleted == false)) { a = achievements.Find(n => n.Id == 107); break; }
                    else if ((x >= 250) && (achievements.Find(n => n.Id == 106).IsCompleted == false)) { a = achievements.Find(n => n.Id == 106); break; }
                    else if ((x >= 100) && (achievements.Find(n => n.Id == 105).IsCompleted == false)) { a = achievements.Find(n => n.Id == 105); break; }
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
