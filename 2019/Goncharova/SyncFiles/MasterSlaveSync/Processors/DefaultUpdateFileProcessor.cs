using MasterSlaveSync.Conflicts;
using System;

namespace MasterSlaveSync
{
    public class DefaultUpdateFileProcessor : IUpdateFileProcessor
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
