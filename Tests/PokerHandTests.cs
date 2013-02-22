using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PokerLogic;


namespace Tests
{
    [TestClass]
    public class PokerHandTests
    {
        [TestMethod]
        public void NOWILD_RoyalFlush()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit {ID = 1, Name="Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 13, Name = "King" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 10, Name = "Ten" };
            Assert.AreEqual("ROYAL FLUSH", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void NOWILD_StraightFlush()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 6, Name = "Six" };
            Assert.AreEqual("STRAIGHT FLUSH", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void NOWILD_FourOfAKind()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 6, Name = "Six" };
            Assert.AreEqual("4 OF A KIND", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void NOWILD_FullHouse()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 11, Name = "Jack" };
            Assert.AreEqual("FULL HOUSE", vpg.Hand.Check("JACKSORBETTER"));
        }
        
        [TestMethod]
        public void NOWILD_Flush()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 3, Name = "Three" };
            Assert.AreEqual("FLUSH", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void NOWILD_Straight()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[1].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 10, Name = "Ten" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void NOWILD_ThreeOfAKind()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Clubs" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 9, Name = "Nine" };
            Assert.AreEqual("3 OF A KIND", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void NOWILD_TwoPair()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Clubs" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 9, Name = "Nine" };
            Assert.AreEqual("TWO PAIR", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void NOWILD_JacksOrBetter()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Clubs" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 9, Name = "Nine" };
            Assert.AreEqual("JACKS OR BETTER", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void NOWILD_Pair()
        {
            VideoPokerGame vpg = new VideoPokerGame("JACKSORBETTER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Clubs" };
            vpg.Hand.Cards[1].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Spades" };
            vpg.Hand.Cards[2].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 9, Name = "Nine" };
            Assert.AreEqual("PAIR", vpg.Hand.Check("JACKSORBETTER"));
        }

        [TestMethod]
        public void DEUCESWILD_RoyalFlushWithDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("ROYAL FLUSH WITH DEUCES", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FourDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 3, Name = "Three" };
            Assert.AreEqual("4 DEUCES", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_ThreeOfAKind()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 6, Name = "Six" };
            Assert.AreEqual("3 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_Straight()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_Flush()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("FLUSH", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FullHouse()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("FULL HOUSE", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FourOfAKind()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("4 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_StraightFlush()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("STRAIGHT FLUSH", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void NOWILD_FourAces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("4 ACES", vpg.Hand.Check("DOUBLEBONUSPOKER"));
        }

        [TestMethod]
        public void NOWILD_FourTwosThreesOrFours()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 3, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 3, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 3, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("4 2s, 3s, 4s", vpg.Hand.Check("DOUBLEBONUSPOKER"));
        }

        [TestMethod]
        public void NOWILD_FourFivesThruKings()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSPOKER");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 9, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 9, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 9, Name = "Ace" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("4 5s THRU KINGS", vpg.Hand.Check("DOUBLEBONUSPOKER"));
        }
    }
}
