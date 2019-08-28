using System.IO;

namespace SyncLib.Loggers
{
    class SilentLoggerVisitor : ILogger
    {
        public SilentLoggerVisitor() { }
        public TextWriter Writer { get; }

        public void Log() { }

        public void Visit(DifferentContentConflict conflict) { }

        public void Visit(ExistDirectoryConflict conflict) { }

        public void Visit(ExistFileConflict conflict) { }

        public void Visit(NoExistDirectoryConflict conflict) { }

        public void Visit(NoExistFileConflict conflict) { };
    }
}
