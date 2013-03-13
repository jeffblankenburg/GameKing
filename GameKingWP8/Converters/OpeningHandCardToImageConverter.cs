using PokerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GameKingWP8
{
    public class OpeningHandCardToImageConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BothHands h = value as BothHands;
            int place = Int32.Parse(parameter.ToString());

            string x = String.Empty;
            switch (h.GameType)
            {
                case "DEUCESWILD":
                case "DOUBLEBONUSDEUCESWILD":
                case "DEUCESWILDBONUSPOKER":
                    if (h.OpeningHand.Cards[place].Value.Number == 2) x = "w";
                    break;
            }


            string imagepath = "Assets/cards/" + h.OpeningHand.Cards[place].Suit.ID.ToString() + h.OpeningHand.Cards[place].Value.Number.ToString() + x.ToString() + ".png";
            return imagepath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
