namespace MasterSlaveSync.Loggers
{
    public interface IMessageCreator
    {
        string CreateFileDeletedMessage(ResolverEventArgs e);
        string CreateFileCopiedMessage(ResolverEventArgs e);
        string CreateFileUpdatedMessage(ResolverEventArgs e);
        string CreateDirectoryDeletedMessage(ResolverEventArgs e);
        string CreateDirectoryCopiedMessage(ResolverEventArgs e);
    }
}