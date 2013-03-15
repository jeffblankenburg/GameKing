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
    public class BLACKJACKBONUSPOKER_Tests
    {
        [TestMethod]
        public void BLACKJACKBONUSPOKER_DeckCounter()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            Assert.AreEqual(47, vpg.Deck.Cards.Count);
        }

        [TestMethod]
        public void BLACKJACKBONUSPOKER_FourAcesWithBlackJack()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[3].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 14, Name = "Ace" };
            Assert.AreEqual("4 ACES WITH BLACK JACK", vpg.Hand.Check("BLACKJACKBONUSPOKER"));
        }

        [TestMethod]
        public void BLACKJACKBONUSPOKER_FourTwosWithBlackJack()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("4 2s,3s,4s W/ BLACK JACK", vpg.Hand.Check("BLACKJACKBONUSPOKER"));
        }

        [TestMethod]
        public void BLACKJACKBONUSPOKER_FourThreesWithBlackJack()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[3].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 3, Name = "Three" };
            Assert.AreEqual("4 2s,3s,4s W/ BLACK JACK", vpg.Hand.Check("BLACKJACKBONUSPOKER"));
        }

        [TestMethod]
        public void BLACKJACKBONUSPOKER_FourFoursWithBlackJack()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[3].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 4, Name = "Four" };
            Assert.AreEqual("4 2s,3s,4s W/ BLACK JACK", vpg.Hand.Check("BLACKJACKBONUSPOKER"));
        }

        [TestMethod]
        public void BLACKJACKBONUSPOKER_FourQueensWithBlackJack()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[3].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 12, Name = "Queen" };
            Assert.AreEqual("4 of a KIND W/ BLACK JACK", vpg.Hand.Check("BLACKJACKBONUSPOKER"));
        }

        [TestMethod]
        public void BLACKJACKBONUSPOKER_FourAcesWithRedJack()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[3].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 14, Name = "Ace" };
            Assert.AreEqual("4 ACES or JACKS", vpg.Hand.Check("BLACKJACKBONUSPOKER"));
        }

        [TestMethod]
        public void BLACKJACKBONUSPOKER_FourJacks()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 11, Name = "Jack" };
            Assert.AreEqual("4 ACES or JACKS", vpg.Hand.Check("BLACKJACKBONUSPOKER"));
        }

        [TestMethod]
        public void BLACKJACKBONUSPOKER_FourJacksWithFirstJackBeingBlack()
        {
            VideoPokerGame vpg = new VideoPokerGame("BLACKJACKBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[0].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 11, Name = "Jack" };
            Assert.AreEqual("4 ACES or JACKS", vpg.Hand.Check("BLACKJACKBONUSPOKER"));
        }
    }
}
