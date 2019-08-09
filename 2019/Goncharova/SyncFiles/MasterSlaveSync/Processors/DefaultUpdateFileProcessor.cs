using MasterSlaveSync.Conflict;

namespace MasterSlaveSync
{
    class DefaultUpdateFileProcessor : IUpdateFileProcessor
    {
        public void Execute(FileConflict fileConflict)
        {
            fileConflict.MasterFile.CopyTo(fileConflict.SlaveFile.FullName, true);
        }
    }
}
