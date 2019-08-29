using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncProcessor
    {
        public ISyncProcessorManager _syncProcManager;

        public SyncProcessor()
        {
            _syncProcManager = new SyncProcessorManager();
        }

        public void Synchronize(SyncData syncData, List<string> folderPaths, ILog log)
        {
            foreach (var path in folderPaths)
            {
                _syncProcManager.Copy(syncData.FilesToCopy, path, log);
            }

            foreach (var path in folderPaths)
            {
                _syncProcManager.Update(syncData.FilesToCopy, path, log);
            }

            foreach (var path in folderPaths)
            {
                _syncProcManager.Delete(syncData.FilesToDelete, path, log);
            }
        }        
    }
}
