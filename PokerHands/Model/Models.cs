using System;
using System.Collections.Generic;
using System.Text;
using PokerHands.Util;

namespace PokerHands.Model
{
    //these would normally be broken out into different files of course
    public enum Suit
    {
        Diamond,
        Spade,
        Heart,
        Club
    }

    public enum Face
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace,
    }

    public enum PokerHand
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public struct Card
    {
        public Suit Suit { get; }

        public Face Face { get; }

        public Card(Suit suit, Face face)
        {
            Face = face;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Suit} {Face}";
        }
    }

    public class Hand
    {
        public IEnumerable<Card> Cards { get; }

        public Hand(IEnumerable<Card> cards)
        {
            Cards = cards;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var card in Cards)
            {
                stringBuilder.Append($"{EnumUtils.TransformFace(card.Face)}{EnumUtils.TransformSuit(card.Suit)} ");
            }

            return stringBuilder.ToString();
        }
    }

    public class HandResult
    {
        public Hand Input { get; }
        public PokerHand Result { get; }
        public Face HighCard { get; set; }

        public HandResult(Hand input, PokerHand result)
        {
            Input = input;
            Result = result;
        }

        public override string ToString()
        {
            return $"{Input} => {Result}";
        }
    }
}
