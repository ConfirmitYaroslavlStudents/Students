using Sync.Wrappers;

namespace Sync
{
    public interface IConflictDetectionPolicy
    {
        Conflict GetConflict(IFileSystemElementInfoWrapper first, IFileSystemElementInfoWrapper second);
        bool MakesConflict(IFileSystemElementInfoWrapper first, IFileSystemElementInfoWrapper second);
    }
}