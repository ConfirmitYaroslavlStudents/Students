using System;
using System.IO;

namespace MasterSlaveSync.Loggers
{
    public class VerboseLogger : ILogger
    {
        public VerboseLogger(Action<string> logListener)
        {
            LogListener = logListener;
        }

        public Action<string> LogListener { get; private set; }

        public void LogDirectoryCopy(object sender, ResolverEventArgs e)
        {
            string directoryPath = Path.GetDirectoryName(e.ElementPath);

            LogListener($"Copied \"{e.ElementPath.Substring(directoryPath.Length + 1)}\" directory " +
                $"from {directoryPath}");
        }

        public void LogDirectoryDeletion(object sender, ResolverEventArgs e)
        {
            string directoryPath = Path.GetDirectoryName(e.ElementPath);

            LogListener($"Deleted \"{e.ElementPath.Substring(directoryPath.Length + 1)}\" directory " +
                $"from {directoryPath}");
        }

        public void LogFileCopy(object sender, ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            LogListener($"Copied \"{fileName}\" file " +
                $"from {Path.GetDirectoryName(e.ElementPath)}");
        }

        public void LogFileDeletion(object sender, ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            LogListener($"Deleted \"{fileName}\" file " +
                $"from {Path.GetDirectoryName(e.ElementPath)}");
        }

        public void LogFileUpdate(object sender, ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            LogListener($"Updated \"{fileName}\" file");
        }
    }
}