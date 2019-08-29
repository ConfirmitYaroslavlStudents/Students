using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public interface IDeleteFileProcessor
    {
        event EventHandler<ResolverEventArgs> FileDeleted;
        void Execute(IFileInfo slaveFile);
    }
}