using System.Collections.Generic;
using System.IO;
using Sync.Resolutions;

namespace Sync.Visitors
{
    public class VerboseLoggerVisitor : ILoggerVisitor
    {
        private List<string> _logs = new List<string>();

        public void Visit(CopyResolution resolution)
        {
            _logs.Add($"Copy {resolution.Source.FullName} to {resolution.Destination}");
        }

        public void Visit(DeleteResolution resolution)
        {
            _logs.Add($"Delete {resolution.Source.FullName}");
        }

        public void Visit(UpdateResolution resolution)
        {
            _logs.Add($"Update {resolution.Source.FullName} with {resolution.Destination.FullName}");
        }

        public void WriteLog(TextWriter writer)
        {
            _logs.ForEach(writer.WriteLine);
        }
    }
}