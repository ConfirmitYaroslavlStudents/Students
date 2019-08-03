using Sync.Wrappers;

namespace Sync.Providers
{
    public interface IProvider
    {
        DirectoryWrapper LoadDirectory();
        void Delete(IFileSystemElementWrapper element);
        void CopyTo(IFileSystemElementWrapper element, string path);
    }
}