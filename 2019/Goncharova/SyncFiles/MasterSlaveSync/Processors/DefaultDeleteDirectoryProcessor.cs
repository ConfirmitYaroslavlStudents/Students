using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class DefaultDeleteDirectoryProcessor : IDeleteDirectoryProcessor
    {
        public event EventHandler<ResolverEventArgs> DirectoryDeleted;

        public void Execute(IDirectoryInfo slaveDirectory)
        {
            slaveDirectory.Delete();

            var args = new ResolverEventArgs
            {
                ElementPath = slaveDirectory.FullName
            };
            OnDirectoryDeleted(args);
        }
        protected virtual void OnDirectoryDeleted(ResolverEventArgs e)
        {
            DirectoryDeleted?.Invoke(this, e);
        }
    }
}
