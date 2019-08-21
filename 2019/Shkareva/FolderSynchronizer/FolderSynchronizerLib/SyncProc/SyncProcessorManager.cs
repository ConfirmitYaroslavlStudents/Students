using System.Collections.Generic;
using System.IO;

namespace FolderSynchronizerLib
{
    public class SyncProcessorManager : ISyncProcessorManager
    {
        public void Copy(Dictionary<string, string> filesToCopy, string path)
        {
           foreach(var copyInfo in filesToCopy)
            {
                File.Copy(Path.Combine(copyInfo.Value, copyInfo.Key), Path.Combine(path, copyInfo.Key),true);
            }
        }

        public void Delete(Dictionary<string, string> filesToDelete, string path)
        {
            foreach (var deleteInfo in filesToDelete)
            {
                string pathDelete = Path.Combine(path, deleteInfo.Key);

                if (!File.Exists(pathDelete))
                {
                    continue;
                }

                File.Delete(pathDelete);
            }
        }

        public void Update(Dictionary<string, string> filesToUpdate, string path)
        {
            foreach (var copyInfo in filesToUpdate)
            {
                string pathUpdate = Path.Combine(path, copyInfo.Key);

                if (!File.Exists(pathUpdate))
                {
                    continue;
                }
                File.Copy(Path.Combine(copyInfo.Value, copyInfo.Key), pathUpdate, true);
            }
        }
    }
}
