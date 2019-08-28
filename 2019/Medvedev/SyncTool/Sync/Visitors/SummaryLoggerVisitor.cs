using System;
using System.IO;
using Sync.Resolutions;

namespace Sync.Visitors
{
    public class SummaryLoggerVisitor : ILoggerVisitor
    {
        private int _countOfCopies = 0;
        private int _countOfDeletions = 0;
        private int _countOfUpdates = 0;

        public void Visit(CopyResolution resolution)
        {
            _countOfCopies++;
        }

        public void Visit(DeleteResolution resolution)
        {
            _countOfDeletions++;
        }

        public void Visit(UpdateResolution resolution)
        {
            _countOfUpdates++;
        }

        public void WriteLog(TextWriter writer)
        {
            writer.WriteLine($"Copied {_countOfCopies} {Environment.NewLine} " +
                             $"Deleted {_countOfDeletions} {Environment.NewLine} " +
                             $"Updated {_countOfUpdates}");
        }
    }
}