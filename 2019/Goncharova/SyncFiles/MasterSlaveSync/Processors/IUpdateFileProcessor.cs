using MasterSlaveSync.Conflict;

namespace MasterSlaveSync
{
    internal interface IUpdateFileProcessor
    {
        void Execute(FileConflict fileConflict);
    }
}