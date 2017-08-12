using System;
using System.Diagnostics;

namespace HomeWork3
{
    public class RenamerWithTimer : IRenamer
    {
        private IRenamer _renamer;
        public TimeSpan Elapsed { get; private set; }

        public RenamerWithTimer(IRenamer renamer)
        {
            _renamer = renamer;
        }

        public void Rename(MP3File file)
        {
            var sw = Stopwatch.StartNew();
            _renamer.Rename(file);
            sw.Stop();
            Elapsed += sw.Elapsed;
        }
    }
}
