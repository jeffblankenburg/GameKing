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
    public class DEUCESWILD_Tests
    {
        [TestMethod]
        public void DEUCESWILD_DeckCount()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            Assert.AreEqual(47, vpg.Deck.Cards.Count);
        }

        [TestMethod]
        public void DEUCESWILD_RoyalFlush_NoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
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
            Assert.AreEqual("ROYAL FLUSH NO DEUCES", vpg.Hand.Check("DEUCESWILD"));
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
        public void DEUCESWILD_RoyalFlushWithDeuces_OneDeuce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("ROYAL FLUSH WITH DEUCES", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_RoyalFlushWithDeuces_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("ROYAL FLUSH WITH DEUCES", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_RoyalFlushWithDeuces_ThreeDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[2].Value = new Value { Number = 12, Name = "Queen" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("ROYAL FLUSH WITH DEUCES", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FiveOfAKind_OneDeuce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 11, Name = "Jack" };
            Assert.AreEqual("5 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FiveOfAKind_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 11, Name = "Jack" };
            Assert.AreEqual("5 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FiveOfAKind_ThreeDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 11, Name = "Jack" };
            Assert.AreEqual("5 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_StraightFlush_NoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 2, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 6, Name = "Six" };
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
        public void DEUCESWILD_StraightFlush_OneDeuce()
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
        public void DEUCESWILD_StraightFlush_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("STRAIGHT FLUSH", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_StraightFlush_ThreeDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 7, Name = "Seven" };
            Assert.AreEqual("STRAIGHT FLUSH", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FourOfAKind_NoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 10, Name = "Ten" };
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
        public void DEUCESWILD_FourOfAKind_OneDeuce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("4 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FourOfAKind_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("4 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FourOfAKind_ThreeDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("4 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FullHouse_NoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 8, Name = "Eight" };
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
        public void DEUCESWILD_FullHouse_OneDeuce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 8, Name = "Eight" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("FULL HOUSE", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_Flush_NoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[0].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("FLUSH", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_Flush_OneDeuce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("FLUSH", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_Flush_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[3].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("FLUSH", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_Straight_NoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 11, Name = "Jack" };
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
        public void DEUCESWILD_Straight_OneDeuce()
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
        public void DEUCESWILD_Straight_OneDeuce_WithAce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 3, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 14, Name = "Ace" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_Straight_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 7, Name = "Seven" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 9, Name = "Nine" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 8, Name = "Eight" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_Straight_TwoDeuces_WithAce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 5, Name = "Five" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 4, Name = "Four" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 14, Name = "Ace" };
            Assert.AreEqual("STRAIGHT", vpg.Hand.Check("DEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_ThreeOfAKind_NoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 10, Name = "Ten" };
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
        public void DEUCESWILD_ThreeOfAKind_OneDeuce()
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
        public void DEUCESWILD_ThreeOfAKind_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 10, Name = "Ten" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[4].Value = new Value { Number = 6, Name = "Six" };
            Assert.AreEqual("3 OF A KIND", vpg.Hand.Check("DEUCESWILD"));
        }

        

        

        

        

        

        

        [TestMethod]
        public void DEUCESWILD_FourDeucesPlusAce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSDEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 2, Name = "Three" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Three" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Three" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Three" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Seven" };
            Assert.AreEqual("4 DEUCES PLUS ACE", vpg.Hand.Check("DOUBLEBONUSDEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FiveAces_OneDeuce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSDEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[4].Value = new Value { Number = 14, Name = "Ace" };
            Assert.AreEqual("5 ACES", vpg.Hand.Check("DOUBLEBONUSDEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FiveAces_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSDEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("5 ACES", vpg.Hand.Check("DOUBLEBONUSDEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FiveAces_ThreeDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSDEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 14, Name = "Ace" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Two" };
            Assert.AreEqual("5 ACES", vpg.Hand.Check("DOUBLEBONUSDEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FiveSixesThroughKings_OneDeuce()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSDEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[1].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Seven" };
            Assert.AreEqual("5 6s THRU KINGS", vpg.Hand.Check("DOUBLEBONUSDEUCESWILD"));
        }

        [TestMethod]
        public void DEUCESWILD_FiveSixesThroughKings_TwoDeuces()
        {
            VideoPokerGame vpg = new VideoPokerGame("DOUBLEBONUSDEUCESWILD");
            vpg.Hand.Cards[0].Suit = new Suit { ID = 1, Name = "Hearts" };
            vpg.Hand.Cards[0].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[1].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[1].Value = new Value { Number = 2, Name = "Two" };
            vpg.Hand.Cards[2].Suit = new Suit { ID = 3, Name = "Clubs" };
            vpg.Hand.Cards[2].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[3].Suit = new Suit { ID = 4, Name = "Spades" };
            vpg.Hand.Cards[3].Value = new Value { Number = 11, Name = "Jack" };
            vpg.Hand.Cards[4].Suit = new Suit { ID = 2, Name = "Diamonds" };
            vpg.Hand.Cards[4].Value = new Value { Number = 2, Name = "Seven" };
            Assert.AreEqual("5 6s THRU KINGS", vpg.Hand.Check("DOUBLEBONUSDEUCESWILD"));
        }
    }
}
