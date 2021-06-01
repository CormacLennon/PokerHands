using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PokerHands.Model;

namespace PokerHands.Util
{
    internal static class EnumUtils
    {

        internal static Face TransformFace(char symbol) => symbol switch
        {
            '2' => Face.Two,
            '3' => Face.Three,
            '4' => Face.Four,
            '5' => Face.Five,
            '6' => Face.Six,
            '7' => Face.Seven,
            '8' => Face.Eight,
            '9' => Face.Nine,
            'T' => Face.Ten,
            'J' => Face.Jack,
            'Q' => Face.Queen,
            'K' => Face.King,
            'A' => Face.Ace,
            _ => throw new SwitchExpressionException($"Can't handle face {symbol}")
        };

        internal static char TransformFace(Face symbol) => symbol switch
        {
            Face.Two => '2',
            Face.Three => '3',
            Face.Four => '4',
            Face.Five => '5',
            Face.Six => '6',
            Face.Seven => '7',
            Face.Eight => '8',
            Face.Nine => '9',
            Face.Ten => 'T',
            Face.Jack => 'J',
            Face.Queen => 'Q', 
            Face.King => 'K',
            Face.Ace => 'A',
            _ => throw new SwitchExpressionException($"Can't handle face {symbol}")
        };

        internal static Suit TransformSuit(char symbol) => symbol switch
        {
            'H' => Suit.Heart,
            'D' => Suit.Diamond,
            'S' => Suit.Spade,
            'C' => Suit.Club,
            _ => throw new SwitchExpressionException($"Can't handle input {symbol}")
        };

        internal static char TransformSuit(Suit suit) => suit switch
        {
            
            Suit.Heart => 'H',
            Suit.Diamond => 'D',
            Suit.Spade => 'S',
            Suit.Club => 'C',
            _ => throw new SwitchExpressionException($"Can't handle suit {suit}")
        };
    }
}
