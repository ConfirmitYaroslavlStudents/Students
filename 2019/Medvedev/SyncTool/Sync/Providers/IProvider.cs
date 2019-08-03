using Microsoft.SqlServer.Server;
using Sync.Wrappers;

namespace Sync.Loaders
{
    public interface IProvider
    {
        DirectoryWrapper LoadDirectory();
        void Delete(IFileSystemElementWrapper element);
        void CopyTo(IFileSystemElementWrapper element, string path);
    }
}