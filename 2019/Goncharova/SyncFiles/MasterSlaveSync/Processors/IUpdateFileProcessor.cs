using MasterSlaveSync.Conflict;

namespace MasterSlaveSync
{
    internal interface IUpdateFileProcessor
    {
        bool Execute(FileConflict fileConflict);
    }
}