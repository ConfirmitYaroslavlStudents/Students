using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public interface IDeleteDirectoryProcessor
    {
        event EventHandler<ResolverEventArgs> DirectoryDeleted;
        void Execute(IDirectoryInfo slaveDirectory);
    }
}