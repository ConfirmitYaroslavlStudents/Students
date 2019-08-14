using MasterSlaveSync.Conflicts;
using System;

namespace MasterSlaveSync
{
    internal interface IUpdateFileProcessor
    {
        event EventHandler<ResolverEventArgs> FileUpdated;
        void Execute(FileConflict fileConflict);
    }
}