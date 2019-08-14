using MasterSlaveSync.Conflict;

namespace MasterSlaveSync
{
    internal class DefaultUpdateFileProcessor : IUpdateFileProcessor
    {
        public bool Execute(FileConflict fileConflict)
        {
            fileConflict.MasterFile.CopyTo(fileConflict.SlaveFile.FullName, true);

            return true;
        }
    }
}
