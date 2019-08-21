using System;

namespace MasterSlaveSync.Loggers
{
    public interface ILogger
    {
        Action<string> LogListener { get; }

        void LogFileDeletion(object sender, ResolverEventArgs e);
        void LogFileCopy(object sender, ResolverEventArgs e);
        void LogFileUpdate(object sender, ResolverEventArgs e);
        void LogDirectoryDeletion(object sender, ResolverEventArgs e);
        void LogDirectoryCopy(object sender, ResolverEventArgs e);
    }
}