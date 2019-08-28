using System;
using System.IO;

namespace SyncLib.Loggers
{
    public class SummaryLoggerVisitor : ILogger
    {
        public SummaryLoggerVisitor(TextWriter writer)
        {
            Writer = writer;
        }
        public int DeletedCount { get; private set; } = 0;
        public int CopiedCount { get; private set; } = 0;
        public int UpdatedCount { get; private set; } = 0;

        public TextWriter Writer { get; }

        public void Visit(DifferentContentConflict conflict) => UpdatedCount++;

        public void Visit(ExistDirectoryConflict conflict) => DeletedCount++;

        public void Visit(ExistFileConflict conflict) => DeletedCount++;

        public void Visit(NoExistDirectoryConflict conflict) => CopiedCount++;
        public void Visit(NoExistFileConflict conflict) => CopiedCount++;

        public void Log()
        {
            writer.WriteLine( $"Was copied {CopiedCount}" + Environment.NewLine +
                $"Was deleted {DeletedCount}" + Environment.NewLine +
                $"Was updated {UpdatedCount}");
        }
    }
}
