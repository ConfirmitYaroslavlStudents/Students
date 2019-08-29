using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncProcessor
    {
        private readonly ISyncProcessorManager _syncProcManager;

        public SyncProcessor(ISyncProcessorManager syncProcManager)
        {
            _syncProcManager = syncProcManager;
        }

        public void Synchronize(SyncData syncData, List<string> folderPaths)
        {
            foreach (var path in folderPaths)
            {
                _syncProcManager.Copy(syncData.FilesToCopy, path,syncData.Log);
            }

            foreach (var path in folderPaths)
            {
                _syncProcManager.Update(syncData.FilesToCopy, path, syncData.Log);
            }

            foreach (var path in folderPaths)
            {
                _syncProcManager.Delete(syncData.FilesToDelete, path, syncData.Log);
            }
        }        
    }
}
