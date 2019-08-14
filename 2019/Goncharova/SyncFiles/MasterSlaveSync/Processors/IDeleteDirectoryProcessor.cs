using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface IDeleteDirectoryProcessor
    {
        event EventHandler<ResolverEventArgs> DirectoryDeleted;
        void Execute(IDirectoryInfo slaveDirectory);
    }
}