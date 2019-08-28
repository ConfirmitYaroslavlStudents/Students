using System.IO;

namespace Sync.Visitors
{
    public interface ILoggerVisitor : IVisitor
    {
        void WriteLog(TextWriter writer);
    }
}