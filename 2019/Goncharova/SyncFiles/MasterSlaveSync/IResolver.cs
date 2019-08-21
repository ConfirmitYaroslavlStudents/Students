using MasterSlaveSync.Conflicts;

namespace MasterSlaveSync
{
    public interface IResolver
    {
        IDeleteFileProcessor DeleteFileProcessor { get; set; }
        ICopyFileProcessor CopyFileProcessor { get; set; }
        IUpdateFileProcessor UpdateFileProcessor { get; set; }
        IDeleteDirectoryProcessor DeleteDirectoryProcessor { get; set; }
        ICopyDirectoryProcessor CopyDirectoryProcessor { get; set; }

        void ResolveConflicts(ConflictsCollection conflictsCollection);
    }
}