using System;
using System.Collections.Generic;
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
        /// A sequence of <see cref="IEnumerable{Card}"/> sets that can be subscribed to.
        /// </summary>
        public IObservable<IEnumerable<Card>> Hands { get; }

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
                .Select(TransformToCards)
                .Where(x =>
                {
                    var allUnique = DuplicateCardCheck(x);

                    if (!allUnique)
                        Console.WriteLine($"{x} => input must not have duplicates");
                    return allUnique;
                });
        }

        internal bool IsValidSyntax(string handInput)
        {
            return Regex.IsMatch(handInput, handRegex);
        }

        internal IEnumerable<Card> TransformToCards(string handInput)
        {
            var cards = handInput.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => (face: x.First(), suit: x.Last()))
                .Select(x => new Card(EnumUtils.TransformSuit(x.suit), EnumUtils.TransformFace(x.face)));

            return cards;
        }

        internal bool DuplicateCardCheck(IEnumerable<Card> cards)
        {
            if (cards.Distinct().Count() != 5)
                return false;
            return true;
        }
    }
}
