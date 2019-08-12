using System.IO;
using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncProcessor
    {
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
            foreach(string path in filesToDelete)
            {
                File.Delete(path);          
            }
        }

        private void CopyFiles(Dictionary<string, string> filesToCopy)
        {
            foreach(KeyValuePair<string, string> addPair in filesToCopy)
            {
                File.Copy(addPair.Key, addPair.Value, true);
            }
        }
    }
}
