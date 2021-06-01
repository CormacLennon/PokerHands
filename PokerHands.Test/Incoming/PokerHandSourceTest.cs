using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NSubstitute;
using NUnit.Framework;
using PokerHands.Incoming;
using PokerHands.Model;


namespace PokerHands.Test.Incoming
{
    [TestFixture]
    class PokerHandSourceTest
    {

        [OneTimeSetUp]
        public void SetUp()
        {

        }

        [TestCase("fgsdrgse", false)]
        [TestCase("AD 2D 3D YD 5D", false)]
        [TestCase("AD 2D 3D 4D 5D 6D", false)]
        [TestCase("AD 2D 3D 4D", false)]
        [TestCase("AD 2D 3d 4D 5D", false)]
        [TestCase("AD 2D 3D 4D 5D", true)]
        [TestCase("AD 2C 3S 4H 5D", true)]
        [TestCase("AD JC TS QH KD", true)]
        public void TestIsValidSyntax(string input, bool expectedResult)
        {
         var adapter = Substitute.For<ISourceAdapter>();

         var source = new PokerHandSource(adapter);

         var result = source.IsValidSyntax(input);

         Assert.AreEqual(expectedResult, result);

        }

        [Test]
        public void TestIsValidSyntax()
        {
            var adapter = Substitute.For<ISourceAdapter>();

            var source = new PokerHandSource(adapter);

            var cards = new[]
            {
                new Card(Suit.Club, Face.Ace), new Card(Suit.Diamond, Face.Ace), new Card(Suit.Spade, Face.Ace),
                new Card(Suit.Heart, Face.Ace), new Card(Suit.Club, Face.Ace)
            };

            var result = source.DuplicateCardCheck(new Hand(cards));

            Assert.AreEqual(false, result);

            cards = new[]
            {
                new Card(Suit.Club, Face.Ace), new Card(Suit.Diamond, Face.Ace), new Card(Suit.Spade, Face.Ace),
                new Card(Suit.Heart, Face.Ace), new Card(Suit.Club, Face.Three)
            };

            result = source.DuplicateCardCheck(new Hand(cards));

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestTransformToHand()
        {
            var adapter = Substitute.For<ISourceAdapter>();

            var source = new PokerHandSource(adapter);

            var expectedCards = new[]
            {
                new Card(Suit.Heart, Face.Two), new Card(Suit.Diamond, Face.Ten), new Card(Suit.Spade, Face.Seven),
                new Card(Suit.Club, Face.King), new Card(Suit.Heart, Face.Ace)
            };

            var result = source.TransformToHand("2H TD 7S KC AH");

            Assert.AreEqual(result.Cards.Count(), expectedCards.Length);

            foreach (var expectedCard in expectedCards)
            {
                Assert.IsTrue(result.Cards.Contains(expectedCard));
            }
        }
    }

    internal class TestAdapter : ISourceAdapter
    {
        public IObservable<string> HandInput { get; }

        internal TestAdapter(string testData)
        {
            HandInput =  Observable.Create<string>(observer =>
                {
                    //foreach (var item in testData)
                    //{
                        observer.OnNext(testData);
                   // }
                    observer.OnCompleted();
                    return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
                });
        }
    }
}
