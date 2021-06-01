using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerHands.Incoming.Adaptors
{
    /// <summary>
    /// An adapter that montors the console for input and processes any
    /// For development purposes mainly
    /// </summary>
    class ConsoleAdaptor : ISourceAdapter
    {
        public IObservable<string> HandInput { get; }

        public ConsoleAdaptor()
        {
            HandInput = Observable
                    .FromAsync(() => Console.In.ReadLineAsync())
                    .Repeat()
                    .SubscribeOn(Scheduler.Default);
        }
    
    }
}
