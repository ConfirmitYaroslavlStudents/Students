using Sync.Wrappers;

namespace Sync.Loaders
{
    public interface ILoader
    {
        DirectoryWrapper LoadDirectory();
    }
}