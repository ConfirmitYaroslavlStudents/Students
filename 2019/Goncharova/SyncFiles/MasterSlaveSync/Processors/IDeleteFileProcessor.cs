using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface IDeleteFileProcessor
    {
        event EventHandler<ResolverEventArgs> FileDeleted;
        void Execute(IFileInfo slaveFile);
    }
}