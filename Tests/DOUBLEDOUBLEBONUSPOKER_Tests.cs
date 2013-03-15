using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PokerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class DOUBLEDOUBLEBONUSPOKER_Tests
    {
        [TestMethod]
        public void DOUBLEDOUBLEBONUSPOKER_FourAcesWithAnyTwo()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEDOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("4 ACES WITH ANY 2,3,4", vpg.Hand.Check("DOUBLEDOUBLEBONUSPOKER"));
        }

        [TestMethod]
        public void DOUBLEDOUBLEBONUSPOKER_FourAcesWithAnyThree()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEDOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 3, Name = "Three" };
            Assert.AreEqual("4 ACES WITH ANY 2,3,4", vpg.Hand.Check("DOUBLEDOUBLEBONUSPOKER"));
        }

        [TestMethod]
        public void DOUBLEDOUBLEBONUSPOKER_FourAcesWithAnyFour()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEDOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 4, Name = "Four" };
            Assert.AreEqual("4 ACES WITH ANY 2,3,4", vpg.Hand.Check("DOUBLEDOUBLEBONUSPOKER"));
        }

        [TestMethod]
        public void DOUBLEDOUBLEBONUSPOKER_FourTwosWithAFour()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEDOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 4, Name = "Four" };
            Assert.AreEqual("4 2s,3s,4s W/ACE,2,3,4", vpg.Hand.Check("DOUBLEDOUBLEBONUSPOKER"));
        }

        [TestMethod]
        public void DOUBLEDOUBLEBONUSPOKER_FourThreesWithAnAce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEDOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 14, Name = "Ace" };
            Assert.AreEqual("4 2s,3s,4s W/ACE,2,3,4", vpg.Hand.Check("DOUBLEDOUBLEBONUSPOKER"));
        }

        [TestMethod]
        public void DOUBLEDOUBLEBONUSPOKER_FourFoursWithATwo()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEDOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("4 2s,3s,4s W/ACE,2,3,4", vpg.Hand.Check("DOUBLEDOUBLEBONUSPOKER"));
        }
    }
}
