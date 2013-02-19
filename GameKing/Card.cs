using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GameKing
{
    class Card
    {
        private readonly Suit suit;
        private readonly Value value;
        private readonly BitmapImage image;

        public Suit Suit { get { return suit; } }
        public Value Value { get { return value; } }
        public BitmapImage Image { get { return image; } }

        public Card(Suit suit, Value value, BitmapImage image)
        {
            this.suit = suit;
            this.value = value;
            this.image = image;
        }
    }
}
