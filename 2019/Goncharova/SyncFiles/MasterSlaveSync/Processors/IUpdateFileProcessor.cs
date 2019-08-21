using MasterSlaveSync.Conflicts;
using System;

namespace MasterSlaveSync
{
    public interface IUpdateFileProcessor
    {
        event EventHandler<ResolverEventArgs> FileUpdated;
        void Execute(FileConflict fileConflict);
    }
}