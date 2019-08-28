using System.IO;
using Sync.Resolutions;

namespace Sync.Visitors
{
    public class SilentLoggerVisitor : ILoggerVisitor
    {
        public void Visit(CopyResolution resolution)
        {
        }

        public void Visit(DeleteResolution resolution)
        {
        }

        public void Visit(UpdateResolution resolution)
        {
        }

        public void WriteLog(TextWriter writer)
        {
        }
    }
}