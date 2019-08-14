using System.Collections.Generic;
using System.IO;

namespace FolderSynchronizerLib
{
    public class SyncProcManager : ISyncProcManager
    {
        public void Copy(Dictionary<string, string> filesToCopy)
        {
            foreach (KeyValuePair<string, string> addPair in filesToCopy)
            {
                File.Copy(addPair.Key, addPair.Value, true);
            }
        }

        public void Delete(List<string> filesToDelete)
        {
            foreach (string path in filesToDelete)
            {
                File.Delete(path);
            }
        }
    }
}
