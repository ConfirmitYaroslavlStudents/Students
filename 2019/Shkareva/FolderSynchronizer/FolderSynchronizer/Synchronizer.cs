using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

namespace FolderSynchronizer
{
    static class Synchronizer
    {
        public static void Synchronize(SyncData syncData)
        {
            AddFiles(syncData.FilesToCopy);

            if (syncData.NoDeleteFlag)
            {
                return;
            }

            DeleteFiles(syncData.FilesToDelete);
        }

        private static void DeleteFiles(List<string> filesToDelete)
        {
            foreach(string path in filesToDelete)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                
            }
        }

        private static void AddFiles(Dictionary<string, string> filesToAdd)
        {
            foreach(KeyValuePair<string, string> addPair in filesToAdd)
            {
                File.Copy(addPair.Key, addPair.Value, true);
            }
        }
    }
}
