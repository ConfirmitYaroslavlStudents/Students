using Sync.Wrappers;

namespace Sync.Providers
{
    public interface IProvider
    {
        DirectoryWrapper LoadDirectory(string pathToDirectory);
    }
}