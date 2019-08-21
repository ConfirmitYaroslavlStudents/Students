namespace Sync.Wrappers
{
    public interface IFileSystemElementWrapper
    {
        string Name { get; }
        DirectoryWrapper ParentDirectory { get; }
        string ElementType { get; }
        string FullName { get; }
    }
}