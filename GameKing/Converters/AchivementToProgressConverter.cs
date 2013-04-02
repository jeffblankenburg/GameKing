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
            if (a.IsCompleted) return a.Points + " KING POKER POINTS";
            else
            {
                if (App.settings.Values.ContainsKey("COUNT_" + a.GameType + "_" + a.Outcome))
                {
                    return "CURRENTLY " + App.settings.Values["COUNT_" + a.GameType + "_" + a.Outcome];
                }
                else return "CURRENTLY ZERO";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
