using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class DefaultDeleteFileProcessor : IDeleteFileProcessor
    {
        public event EventHandler<ResolverEventArgs> FileDeleted;

        public void Execute(IFileInfo slaveFile)
        {
            slaveFile.Delete();

            var args = new ResolverEventArgs
            {
                ElementPath = slaveFile.FullName
            };
            OnFileDeleted(args);
        }
        protected virtual void OnFileDeleted(ResolverEventArgs e)
        {
            FileDeleted?.Invoke(this, e);
        }
    }
}
