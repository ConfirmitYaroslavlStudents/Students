using System.IO;

namespace SyncLib.Loggers
{
    public interface ILogger : IVisitor
    {
        TextWriter Writer { get; }
        void Log();
    }
}