using PokerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GameKing
{
    public class AchivementToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Achievement a = value as Achievement;
            int x = 0;
            if (a.IsCompleted) return a.Points + " KING POKER POINTS";
            else
            {
                if (a.GameType.Contains("DEUCES"))
                {
                    if (App.settings.Values.ContainsKey("COUNT_DEUCESWILD_" + a.Outcome)) x += (int)App.settings.Values["COUNT_DEUCESWILD_" + a.Outcome];
                    if (App.settings.Values.ContainsKey("COUNT_DEUCESWILDBONUSPOKER_" + a.Outcome)) x += (int)App.settings.Values["COUNT_DEUCESWILDBONUSPOKER_" + a.Outcome];
                    if (App.settings.Values.ContainsKey("COUNT_DOUBLEDONUSDEUCESWILD_" + a.Outcome)) x += (int)App.settings.Values["COUNT_DOUBLEDONUSDEUCESWILD_" + a.Outcome];
                }
                else if (a.GameType.Contains("JOKER"))
                {
                    if (App.settings.Values.ContainsKey("COUNT_JOKERPOKER_" + a.Outcome)) x += (int)App.settings.Values["COUNT_JOKERPOKER_" + a.Outcome];
                }
                else
                {

                }

                if (x != 0) return "CURRENTLY " + x;
                else return "CURRENTLY ZERO";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
