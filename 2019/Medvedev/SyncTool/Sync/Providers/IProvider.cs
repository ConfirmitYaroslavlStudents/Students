using Sync.Wrappers;

namespace Sync.Providers
{
    public interface IProvider
    {
        DirectoryWrapper LoadDirectory(string pathToDirectory);
        void Delete(IFileSystemElementWrapper element);
        void CopyTo(IFileSystemElementWrapper element, string path);
    }
}