using System;

namespace MasterSlaveSync.Loggers
{
    public class SilentLogger : ILogger
    {
        public Action<string> LogListener => null;

        public void LogDirectoryCopy(object sender, ResolverEventArgs e) { }

        public void LogDirectoryDeletion(object sender, ResolverEventArgs e) { }

        public void LogFileCopy(object sender, ResolverEventArgs e) { }

        public void LogFileDeletion(object sender, ResolverEventArgs e) { }

        public void LogFileUpdate(object sender, ResolverEventArgs e) { }
    }
}
