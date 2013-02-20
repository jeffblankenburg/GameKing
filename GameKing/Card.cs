using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GameKing
{
    public class Card
    {
        public Suit Suit { get; set; }
        public Value Value { get; set; }
        //public BitmapImage Image { get; set; }

        //public Card(Suit suit, Value value, BitmapImage image)
        public Card(Suit suit, Value value)
        {
            this.Suit = suit;
            this.Value = value;
            //this.image = image;
        }
    }
}
