using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    class NoDeleteFileProcessor : IDeleteFileProcessor
    {
        public event EventHandler<ResolverEventArgs> FileDeleted;
        public void Execute(IFileInfo slaveFile)
        {
        }
    }
}
