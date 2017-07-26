using System;
using System.Diagnostics;

namespace RenamersLib
{
    public class RenamerWithTimeCounter : IFileRenamer
    {
        private IFileRenamer _renamer;
        private Stopwatch watch;
        
        public TimeSpan Elapsed { get; private set; }

        public RenamerWithTimeCounter(IFileRenamer renamer)
        {
            _renamer = renamer;
            watch = new Stopwatch();
        }

        public void Rename(Mp3File file)
        {
            watch.Start();
            _renamer.Rename(file);
            watch.Stop();

            Elapsed += watch.Elapsed;
            watch.Reset();
        }
    }
}
