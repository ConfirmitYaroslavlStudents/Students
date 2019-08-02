using Sync.Wrappers;

namespace Sync
{
    public interface IConflictDetectionPolicy
    {
        Conflict GetConflict(IFileSystemElementWrapper first, IFileSystemElementWrapper second);
        bool MakesConflict(IFileSystemElementWrapper first, IFileSystemElementWrapper second);
    }
}