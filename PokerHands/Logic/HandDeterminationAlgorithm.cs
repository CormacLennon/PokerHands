using System.Collections.Generic;
using System.Linq;
using PokerHands.Util;
using PokerHands.Model;

namespace PokerHands.Logic
{
    /// <summary>
    /// Class encapsulating the primary game logic
    /// </summary>
    internal class HandDeterminationAlgorithm
    {
        private static readonly Face[] AcesLowStraight = { Face.Two, Face.Three, Face.Four, Face.Five, Face.Ace };

        public static Hand ResolvePokerHand(IEnumerable<Card> cards)
        {
            var isFlush = IsFlush(cards);
            var isStraight = IsStraight(cards.Select(x => x.Face));

            var faceGroupCount = cards.GroupBy(x => x.Face)
                                      .Select(x => x.Count());

            // royal flush
            if (isFlush &&
                isStraight &&
                cards.Any(x => x.Face == Face.King) &&
                cards.Any(x => x.Face == Face.Ace))
            {
                return Hand.RoyalFlush;
            }

            //straight flush
            if (isFlush && isStraight)
            {
                return Hand.StraightFlush;
            }

            //four of a kind
            if (faceGroupCount.Any(x => x == 4))
            {
                return Hand.FourOfAKind;
            }

            //full house
            //If theres only two groups but we didnt return in the above four of a kind check it must be a full house
            if (faceGroupCount.Count() == 2)
            {
                return Hand.FullHouse;
            }

            //flush
            if (isFlush)
            {
                return Hand.Flush;
            }

            //straight
            if (isStraight)
            {
                return Hand.Straight;
            }

            //three of a kind
            if (faceGroupCount.Any(x => x == 3))
            {
                return Hand.ThreeOfAKind;
            }

            //two pair
            if (faceGroupCount.Count(x => x == 2) == 2)
            {
                return Hand.TwoPair;
            }

            //pair
            if (faceGroupCount.Any(x => x == 2))
            {
                return Hand.Pair;
            }
            var result = Hand.HighCard;
            //result.HighCard = cards.Last().Face;
            return result;
        }

        internal static bool IsFlush(IEnumerable<Card> cards)
        {
            return cards.DistinctOn(x => x.Suit)
                        .Count() == 1;
        }

        internal static bool IsStraight(IEnumerable<Face> faces)
        {
            faces = faces.OrderBy(x => x);

            //Test the special case of a straight with aces low
            if (faces.SequenceEqual(AcesLowStraight))
                return true;

            var count = faces.Count();
            var index = 0;
            foreach (var current in faces)
            {
                if (count == index + 1)
                    continue;
                var next = faces.ElementAt(index + 1);
                if (next - current != 1)
                     return false;
                index++;
            }
            return true;
        }
    }
}
