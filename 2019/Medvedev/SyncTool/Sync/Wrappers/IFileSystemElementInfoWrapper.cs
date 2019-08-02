namespace Sync.Wrappers
{
    public interface IFileSystemElementInfoWrapper
    {
        void Delete();
        void CopyTo(string destination);
        string Name { get; }
        string ParentDirectory { get; }
        string ElementType { get; }
    }
}