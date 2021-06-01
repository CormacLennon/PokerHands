using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace PokerHands.Incoming.Adaptors
{
    /// <summary>
    /// An adapter what takes a txt file path and creates a sequence of inputs, monitoring the file for changes and processing it if any are detected
    /// </summary>
    class FileSystemAdapter : ISourceAdapter
    {
        public const string AcceptedFileType = ".txt";
        public IObservable<string> HandInput { get; }

        private string _filePath;

        public FileSystemAdapter(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath is empty");

            if (!File.Exists(filePath))
                throw new FileNotFoundException(
                    $"Provided path is invalid or the process does not have enough permissions to read");

            if (Path.GetExtension(filePath) != AcceptedFileType)
                throw new ArgumentException($"Provided file is of wrong type only {AcceptedFileType} files accepted");

            _filePath = filePath;
            var fileWatcher = new FileSystemWatcher();

            fileWatcher.Path = Path.GetDirectoryName(filePath);
            fileWatcher.Filter = Path.GetFileName(filePath);

            HandInput = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    x => fileWatcher.Changed += x,
                    x => fileWatcher.Changed -= x)
                .Do(x => Console.WriteLine("File changed, processing again"))
                .Select(x => x.EventArgs.FullPath)
                .Where(path => path == filePath)
                .Select(ReadLinesInFile)
                .StartWith(ReadLinesInFile(filePath))
                .SelectMany(x => x);

        }

        private IEnumerable<string> ReadLinesInFile(string path)
        {
            string line;
            var file = new StreamReader(_filePath);
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
