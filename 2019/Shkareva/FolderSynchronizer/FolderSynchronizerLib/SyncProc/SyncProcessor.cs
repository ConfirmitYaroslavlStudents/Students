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
                CopyFiles(syncData.FilesToCopy, path);
            }

            foreach (var path in folderPaths)
            {
                UpdateFiles(syncData.FilesToUpdate, path);
            }
            
            foreach (var path in folderPaths)
            {
                DeleteFiles(syncData.FilesToDelete, path);
            }
        }

        private void UpdateFiles(Dictionary<string, string> filesToUpdate, string path)
        {
            _syncProcManager.Update(filesToUpdate, path);
        }

        private void DeleteFiles(Dictionary<string, string> filesToDelete, string path)
        {
            _syncProcManager.Delete(filesToDelete, path);
        }

        private void CopyFiles(Dictionary<string, string> filesToCopy, string path)
        {
            _syncProcManager.Copy(filesToCopy, path);
        }
    }
}
