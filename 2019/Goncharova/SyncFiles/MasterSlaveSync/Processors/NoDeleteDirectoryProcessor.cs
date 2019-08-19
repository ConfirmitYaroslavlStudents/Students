using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class NoDeleteDirectoryProcessor : IDeleteDirectoryProcessor
    {
        public event EventHandler<ResolverEventArgs> DirectoryDeleted;
        public void Execute(IDirectoryInfo slaveDirectory)
        {
        }

    }
}
