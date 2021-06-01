using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Incoming
{
    public interface ISourceAdapter
    {
        //A stream of unparsed hands
        IObservable<string> HandInput { get; }
    }
}
