namespace MasterSlaveSync
{
    internal interface ILogger
    {
        void LogFileDeletion(object sender, ResolverEventArgs e);
        void LogFileCopy(object sender, ResolverEventArgs e);
        void LogFileUpdate(object sender, ResolverEventArgs e);
        void LogDirectoryDeletion(object sender, ResolverEventArgs e);
        void LogDirectoryCopy(object sender, ResolverEventArgs e);
    }
}