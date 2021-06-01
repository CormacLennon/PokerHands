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

    public enum Hand
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
            return $"{EnumUtils.TransformFace(Face)}{EnumUtils.TransformSuit(Suit)}";
        }
    }
}
