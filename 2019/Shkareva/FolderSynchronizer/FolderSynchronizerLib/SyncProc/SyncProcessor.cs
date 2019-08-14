using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncProcessor
    {
        ISyncProcManager syncProcManager;

        public SyncProcessor()
        {
            syncProcManager = new SyncProcManager();
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
            syncProcManager.Delete(filesToDelete);
        }

        private void CopyFiles(Dictionary<string, string> filesToCopy)
        {
            syncProcManager.Copy(filesToCopy);
        }
    }
}
