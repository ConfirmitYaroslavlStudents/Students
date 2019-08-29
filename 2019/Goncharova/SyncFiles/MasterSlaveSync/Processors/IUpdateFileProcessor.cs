using MasterSlaveSync.Conflicts;

namespace MasterSlaveSync
{
    public interface IUpdateFileProcessor
    {
        void Execute(FileConflict fileConflict);
    }
}