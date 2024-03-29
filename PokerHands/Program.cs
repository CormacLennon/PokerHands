﻿using System;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using CommandLine;
using PokerHands.Incoming;
using PokerHands.Incoming.Adaptors;
using PokerHands.Logic;
using PokerHands.Util;

[assembly: InternalsVisibleTo("PokerHands.Test")]
namespace PokerHands
{
    class Program
    {
        private static System.Threading.AutoResetEvent ShutdownEvent => new System.Threading.AutoResetEvent(false);

        static void Main(string[] args)
        {
            var error = CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
                .MapResult(
                    opts => Run(opts),
                    errs => 1);

            if(error == 0)
                ShutdownEvent.WaitOne();
        }

        internal static int Run(CommandLineOptions args)
        {
            ISourceAdapter adaptor;

            if (args.UseConsole)
            {
                adaptor = new ConsoleAdaptor();
            }
            else
            {
                try
                {
                    adaptor = new FileSystemAdapter(args.Path);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    return 1;
                }
            }

            var source = new PokerHandSource(adaptor);

            source.Hands
                .Subscribe(
                    cards =>
                    {
                        var result = HandDeterminationAlgorithm.ResolvePokerHand(cards);
                        Console.WriteLine($"{cards.ToElementsString()} => {result}");
                    },
                    exception => Console.Error.WriteLine($"Unhandled exception: {exception}"));

            return 0;
        }

        internal class CommandLineOptions
        {
            [Option("path", Required = false, HelpText = "The path to the file if using filesystem adapter")]
            public string Path { get; set; }

            [Option('c', "useConsole", Required = false, Default = false, HelpText = "if you want to use the console as input instead of the filesystem")]
            public bool UseConsole { get; set; }
        }
    }
}
