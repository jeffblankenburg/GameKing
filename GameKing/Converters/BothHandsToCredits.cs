using PokerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GameKing
{
    public class BothHandsToCredits : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BothHands h = value as BothHands;
            string s = h.CreditCount.ToString();
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
