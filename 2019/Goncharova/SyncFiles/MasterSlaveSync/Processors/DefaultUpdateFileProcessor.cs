using System;
using MasterSlaveSync.Conflicts;

namespace MasterSlaveSync
{
    internal class DefaultUpdateFileProcessor : IUpdateFileProcessor
    {
        public event EventHandler<ResolverEventArgs> FileUpdated;

        public void Execute(FileConflict fileConflict)
        {
            fileConflict.MasterFile.CopyTo(fileConflict.SlaveFile.FullName, true);

            var args = new ResolverEventArgs
            {
                ElementPath = fileConflict.SlaveFile.FullName
            };
            OnFileUpdated(args);
        }
        protected virtual void OnFileUpdated(ResolverEventArgs e)
        {
            FileUpdated?.Invoke(this, e);
        }
    }
}
