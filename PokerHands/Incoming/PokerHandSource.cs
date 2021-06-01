using System;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using PokerHands.Model;
using PokerHands.Util;

namespace PokerHands.Incoming
{
    class PokerHandSource
    {
        /// <summary>
        /// A sequence of <see cref="Hand"/> objects that can be subscribed to.
        /// </summary>
        public IObservable<Hand> Hands { get; }

        private static readonly string cardRegex = "[23456789TJQKA][HDSC]";
        private static readonly string handRegex = @$"^{cardRegex} {cardRegex} {cardRegex} {cardRegex} {cardRegex}\s*$";

        public PokerHandSource(ISourceAdapter adapter)
        {
            Hands = adapter.HandInput
                .Where(x=>
                {
                    var valid = IsValidSyntax(x);

                    if(!valid)
                        Console.WriteLine($"input '{x}' is not valid syntax");
                    return valid;
                })
                .Select(TransformToHand)
                .Where(x =>
                {
                    var valid = IsValidHand(x);

                    if (!valid)
                        Console.WriteLine($"input '{x}' is not a valid hand");
                    return valid;
                });
        }

        internal bool IsValidSyntax(string handInput)
        {
            return Regex.IsMatch(handInput, handRegex);
        }

        internal Hand TransformToHand(string handInput)
        {
            var cards = handInput.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => (face: x.First(), suit: x.Last()))
                .Select(x => new Card(EnumUtils.TransformSuit(x.suit), EnumUtils.TransformFace(x.face)));

            return new Hand(cards);
        }

        internal bool IsValidHand(Hand hand)
        {
            if (hand.Cards.Distinct().Count() != 5)
                return false;
            return true;
        }
    }
}
