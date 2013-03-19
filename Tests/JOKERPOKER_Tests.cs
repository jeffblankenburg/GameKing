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
    public class JOKERPOKER_Tests
    {
        [TestMethod]
        public void JOKERPOKER_DeckCount()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            Assert.AreEqual(48, vpg.Deck.Cards.Count);
        }

        [TestMethod]
        public void JOKERPOKER_RoyalFlush_NoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 13, Name = "King" };
            Assert.AreEqual("ROYAL FLUSH NO WILD", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_FiveOfAKind_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[4].Value = new Value { Number = 1, Name = "Joker" };
            Assert.AreEqual("5 OF A KIND", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_FiveOfAKind_TwoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[3].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[4].Value = new Value { Number = 1, Name = "Joker" };
            Assert.AreEqual("5 OF A KIND", vpg.Hand.Check("JOKERPOKER"));
        }
        
        [TestMethod]
        public void JOKERPOKER_RoyalFlushWithWild_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[4].Value = new Value { Number = 1, Name = "Joker" };
            Assert.AreEqual("ROYAL FLUSH WITH WILD", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_RoyalFlushWithWild_TwoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[1].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[4].Value = new Value { Number = 1, Name = "Joker" };
            Assert.AreEqual("ROYAL FLUSH WITH WILD", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_StraightFlush_NoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("STRAIGHT FLUSH", vpg.Hand.Check("JOKERPOKER"));
        }
        
        [TestMethod]
        public void JOKERPOKER_StraightFlush_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 8, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[4].Value = new Value { Number = 1, Name = "Joker" };
            Assert.AreEqual("STRAIGHT FLUSH", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_StraightFlush_TwoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[0].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 8, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[4].Value = new Value { Number = 1, Name = "Joker" };
            Assert.AreEqual("STRAIGHT FLUSH", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_FourOfAKind_NoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("4 OF A KIND", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_FourOfAKind_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[1].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("4 OF A KIND", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_FourOfAKind_TwoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[0].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[1].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("4 OF A KIND", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_FullHouse_NoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("FULL HOUSE", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_FullHouse_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[0].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("FULL HOUSE", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Flush_NoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 9, Name = "Nine" };
            Assert.AreEqual("FLUSH", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Flush_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[2].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 9, Name = "Nine" };
            Assert.AreEqual("FLUSH", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Flush_TwoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[2].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[4].Value = new Value { Number = 1, Name = "Joker" };
            Assert.AreEqual("FLUSH", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Straight_NoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Straight_NoJokers_WithAce()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 4, Name = "Four" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Straight_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[0].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Straight_OneJoker_WithAce()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[1].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 4, Name = "Four" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Straight_TwoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[0].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[3].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_Straight_TwoJokers_WithAce()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[1].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[3].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 4, Name = "Four" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_ThreeOfAKind_NoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seveb" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("3 OF A KIND", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_ThreeOfAKind_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[2].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("3 OF A KIND", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_ThreeOfAKind_TwoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[1].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[2].Value = new Value { Number = 1, Name = "Joker" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("3 OF A KIND", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_KingsOrBetter_NoJokers()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "King" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 14, Name = "Ace" };
            Assert.AreEqual("KINGS OR BETTER", vpg.Hand.Check("JOKERPOKER"));
        }

        [TestMethod]
        public void JOKERPOKER_KingsOrBetter_OneJoker()
        {
            VideoPokerGame vpg = new VideoPokerGame("JOKERPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 13, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "King" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 5, Name = "Joker" };
            vpg.Hand.Cards[4].Value = new Value { Number = 1, Name = "Joker" };
            Assert.AreEqual("KINGS OR BETTER", vpg.Hand.Check("JOKERPOKER"));
        }

    }
}
