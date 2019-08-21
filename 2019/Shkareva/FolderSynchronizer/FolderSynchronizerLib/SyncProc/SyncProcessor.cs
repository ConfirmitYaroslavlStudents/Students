using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncProcessor
    {
        private readonly ISyncProcManager _syncProcManager;

        public SyncProcessor(ISyncProcManager syncProcManager)
        {
            _syncProcManager = syncProcManager;
        }

        public void Synchronize(SyncData syncData)
        {
            CopyFiles(syncData.FilesToCopy);
            CopyFiles(syncData.FilesToUpdate);

            if (syncData.NoDeleteFlag)
            {
                return;
            }

            DeleteFiles(syncData.FilesToDelete);
        }

        private void DeleteFiles(List<string> filesToDelete)
        {
            _syncProcManager.Delete(filesToDelete);
        }

        private void CopyFiles(Dictionary<string, string> filesToCopy)
        {
            _syncProcManager.Copy(filesToCopy);
        }
    }
}
