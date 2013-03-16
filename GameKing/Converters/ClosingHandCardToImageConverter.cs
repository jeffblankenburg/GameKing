using PokerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GameKing
{
    public class ClosingHandCardToImageConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BothHands h = value as BothHands;
            int place = Int32.Parse(parameter.ToString());

            string x = String.Empty;
            switch (h.GameType)
            {
                case "DEUCESWILD":
                case "DOUBLEBONUSDEUCESWILD":
                case "DEUCESWILDBONUSPOKER":
                    if (h.ClosingHand.Cards[place].Value.Number == 2) x = "w";
                    break;
            }

            string imagepath = "Assets/cards/" + h.ClosingHand.Cards[place].Suit.ID.ToString() + h.ClosingHand.Cards[place].Value.Number.ToString() + x.ToString() + ".png";
            return imagepath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
