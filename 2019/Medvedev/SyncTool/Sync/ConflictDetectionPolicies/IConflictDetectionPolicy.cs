using Sync.Wrappers;

namespace Sync.ConflictDetectionPolicies
{
    public interface IConflictDetectionPolicy
    {
        Conflict GetConflict(IFileSystemElementWrapper first, IFileSystemElementWrapper second);
        bool ConflictExists(IFileSystemElementWrapper first, IFileSystemElementWrapper second);
    }
}