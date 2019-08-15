using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class NoDeleteFileProcessor : IDeleteFileProcessor
    {
        public event EventHandler<ResolverEventArgs> FileDeleted;
        public void Execute(IFileInfo slaveFile)
        {
        }
    }
}
