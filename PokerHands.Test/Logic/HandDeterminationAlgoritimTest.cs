using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;
using NUnit.Framework;
using PokerHands.Model;
using PokerHands.Logic;

namespace PokerHands.Test.Logic
{
    [TestFixture]
    public class HandDeterminationAlgoritimTest
    {

        [TestCase(new[] {Suit.Club, Suit.Club, Suit.Heart, Suit.Club, Suit.Club}, false)]
        [TestCase(new[] {Suit.Club, Suit.Heart, Suit.Spade, Suit.Diamond, Suit.Club}, false)]
        [TestCase(new[] {Suit.Club, Suit.Club, Suit.Club, Suit.Club, Suit.Club}, true)]
        [TestCase(new[] {Suit.Spade, Suit.Spade, Suit.Spade, Suit.Spade, Suit.Spade}, true)]
        [TestCase(new[] {Suit.Diamond, Suit.Diamond, Suit.Diamond, Suit.Diamond, Suit.Diamond}, true)]
        [TestCase(new[] {Suit.Heart, Suit.Heart, Suit.Heart, Suit.Heart, Suit.Heart}, true)]
        [TestCase(new[] {Suit.Club, Suit.Heart, Suit.Heart, Suit.Heart, Suit.Spade}, false)]
        public void TestIsFlush(Suit[] suits, bool expectedResult)
        {
            var cards = suits.Select(x => new Card(x, Face.Ace));

            var result = HandDeterminationAlgorithm.IsFlush(cards);
            Assert.AreEqual(expectedResult, result);
        }


        [TestCase(new[] {Face.Ace, Face.Two, Face.Three, Face.Four, Face.Five}, true)]
        [TestCase(new[] {Face.Six, Face.Two, Face.Three, Face.Four, Face.Five}, true)]
        [TestCase(new[] {Face.Seven, Face.Two, Face.Three, Face.Four, Face.Five}, false)]
        [TestCase(new[] {Face.Seven, Face.Five, Face.Eight, Face.Six, Face.Nine}, true)]
        [TestCase(new[] {Face.Ten, Face.Jack, Face.Queen, Face.King, Face.Ten}, false)]
        [TestCase(new[] {Face.Ace, Face.Jack, Face.Queen, Face.King, Face.Ten}, true)]
        [TestCase(new[] {Face.Ace, Face.Jack, Face.Queen, Face.King, Face.Ace}, false)]
        public void TestIsStraight(Face[] faces, bool expectedResult)
        {
            var result = HandDeterminationAlgorithm.IsStraight(faces);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCaseSource(nameof(TestHands))]
        public void TestResolvePokerHand(TestHand hand)
        {
            var result = HandDeterminationAlgorithm.ResolvePokerHand(hand.Cards);
            Assert.AreEqual(hand.ExpectedResult, result);
        }

        public static IEnumerable<TestHand> TestHands()
        {
            var cards = new[]
            {
                new Card(Suit.Heart, Face.Two),
                new Card(Suit.Diamond, Face.Ten),
                new Card(Suit.Spade, Face.Seven),
                new Card(Suit.Club, Face.King),
                new Card(Suit.Heart, Face.Ace)
            };

            yield return new TestHand(cards, Hand.HighCard);

            cards = new[]
            {
                new Card(Suit.Heart, Face.Two),
                new Card(Suit.Diamond, Face.Ten),
                new Card(Suit.Spade, Face.Ten),
                new Card(Suit.Club, Face.King),
                new Card(Suit.Heart, Face.Ace)
            };

            yield return new TestHand(cards, Hand.Pair);

            cards= new[]
            {
                new Card(Suit.Heart, Face.Two),
                new Card(Suit.Diamond, Face.Ten),
                new Card(Suit.Spade, Face.Ten),
                new Card(Suit.Club, Face.Two),
                new Card(Suit.Heart, Face.Ace)
            };

            yield return new TestHand(cards, Hand.TwoPair);

            cards = new[]
            {
                new Card(Suit.Heart, Face.Ten),
                new Card(Suit.Diamond, Face.Ten),
                new Card(Suit.Spade, Face.Three),
                new Card(Suit.Club, Face.King),
                new Card(Suit.Heart, Face.Ten)
            };

            yield return new TestHand(cards, Hand.ThreeOfAKind);

            cards = new[]
            {
                new Card(Suit.Heart, Face.Five),
                new Card(Suit.Diamond, Face.Three),
                new Card(Suit.Spade, Face.Four),
                new Card(Suit.Club, Face.Two),
                new Card(Suit.Heart, Face.Six)
            };

            yield return new TestHand(cards, Hand.Straight);

            cards = new[]
            {
                new Card(Suit.Heart, Face.Two),
                new Card(Suit.Heart, Face.Ten),
                new Card(Suit.Heart, Face.Seven),
                new Card(Suit.Heart, Face.King),
                new Card(Suit.Heart, Face.Ace)
            };

            yield return new TestHand(cards, Hand.Flush);

            cards = new[]
            {
                new Card(Suit.Heart, Face.Ace),
                new Card(Suit.Diamond, Face.Two),
                new Card(Suit.Spade, Face.Two),
                new Card(Suit.Club, Face.Ace),
                new Card(Suit.Heart, Face.Two)
            };

            yield return new TestHand(cards, Hand.FullHouse);

            cards = new[]
            {
                new Card(Suit.Heart, Face.King),
                new Card(Suit.Diamond, Face.King),
                new Card(Suit.Spade, Face.Seven),
                new Card(Suit.Club, Face.King),
                new Card(Suit.Heart, Face.King)
            };

            yield return new TestHand(cards, Hand.FourOfAKind);

            cards = new[]
            {
                new Card(Suit.Diamond, Face.Six),
                new Card(Suit.Diamond, Face.Five),
                new Card(Suit.Diamond, Face.Seven),
                new Card(Suit.Diamond, Face.Nine),
                new Card(Suit.Diamond, Face.Eight)
            };

            yield return new TestHand(cards, Hand.StraightFlush);

            cards = new[]
            {
                new Card(Suit.Club, Face.Queen),
                new Card(Suit.Club, Face.Ten),
                new Card(Suit.Club, Face.Jack),
                new Card(Suit.Club, Face.King),
                new Card(Suit.Club, Face.Ace)
            };

            yield return new TestHand(cards, Hand.RoyalFlush);
        }

        public class TestHand
        {
            public IEnumerable<Card> Cards { get; }
            public Hand ExpectedResult { get; }

            public TestHand(IEnumerable<Card> hand, Hand expectedResult)
            {
                Cards = hand;
                ExpectedResult = expectedResult;
            }
        }
    }

}