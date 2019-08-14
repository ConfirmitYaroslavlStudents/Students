using System;
using System.IO;

namespace MasterSlaveSync
{
    internal class SummaryLogger : ILogger
    {
        public SummaryLogger(Action<string> logListener)
        {
            LogListener = logListener;
        }
        public Action<string> LogListener { get; private set; }

        public void LogDirectoryCopy(object sender, ResolverEventArgs e)
        {
            var directoryPath = Path.GetDirectoryName(e.ElementPath);

            LogListener($"Copied \"{e.ElementPath.Substring(directoryPath.Length + 1)}\" directory");
        }

        public void LogDirectoryDeletion(object sender, ResolverEventArgs e)
        {
            var directoryPath = Path.GetDirectoryName(e.ElementPath);

            LogListener($"Deleted \"{e.ElementPath.Substring(directoryPath.Length + 1)}\" directory");
        }

        public void LogFileCopy(object sender, ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            LogListener($"Copied \"{fileName}\" file");
        }

        public void LogFileDeletion(object sender, ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            LogListener($"Copied \"{fileName}\" file");
        }

        public void LogFileUpdate(object sender, ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            LogListener($"Updated \"{fileName}\" file");
        }
    }
}