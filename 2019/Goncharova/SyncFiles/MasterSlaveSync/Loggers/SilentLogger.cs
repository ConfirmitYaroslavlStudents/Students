using System;

namespace MasterSlaveSync
{
    internal class SilentLogger : ILogger
    {
        public Action<string> LogListener { get; private set; }
        public SilentLogger(Action<string> logListener)
        {
            LogListener = logListener;
        }

        public void LogDirectoryCopy(object sender, ResolverEventArgs e)
        {
            LogListener(String.Empty);
        }

        public void LogDirectoryDeletion(object sender, ResolverEventArgs e)
        {
            LogListener(String.Empty);
        }

        public void LogFileCopy(object sender, ResolverEventArgs e)
        {
            LogListener(String.Empty);
        }

        public void LogFileDeletion(object sender, ResolverEventArgs e)
        {
            LogListener(String.Empty);
        }

        public void LogFileUpdate(object sender, ResolverEventArgs e)
        {
            LogListener(String.Empty);
        }
    }
}