using System.Collections.Generic;
using System.IO;

namespace SyncLib.Loggers
{
    public class VerboseLoggerVisitor : ILogger
    {
        public VerboseLoggerVisitor(TextWriter writer)
        {
            Writer = writer;
        }

        public TextWriter Writer { get; }

        private List<string> logs = new List<string>();

        public void Log()
        {
            logs.ForEach(message => Writer.WriteLine(message));
        }

        public void Visit(DifferentContentConflict conflict)
        {
            logs.Add($"File {conflict.target} was changed to {conflict.source}");
        }

        public void Visit(ExistDirectoryConflict conflict)
        {
            logs.Add($"Directory {conflict.path} was deleted");
        }

        public void Visit(ExistFileConflict conflict)
        {
            logs.Add($"File {conflict.FilePath} was deleted");
        }

        public void Visit(NoExistDirectoryConflict conflict)
        {
            logs.Add($"Directory {conflict.path} was created");
        }

        public void Visit(NoExistFileConflict conflict)
        {
            logs.Add($"File {conflict.target} was copied from {conflict.source}");
        }
    }
}
