using System.Collections.Generic;
using System.IO;

namespace FolderSynchronizerLib
{
    public class SyncProcessorManager : ISyncProcessorManager
    {
        public void Copy(Dictionary<string, string> filesToCopy, string path, ILog log)
        {
           foreach(var copyInfo in filesToCopy)
            {
                string source = Path.Combine(copyInfo.Value, copyInfo.Key);
                string destination = Path.Combine(path, copyInfo.Key);

                File.Copy(source, destination,true);
                log.GetInfoAboutAddFiles(source, destination);
            }
        }

        public void Delete(Dictionary<string, string> filesToDelete, string path, ILog log)
        {
            foreach (var deleteInfo in filesToDelete)
            {
                string pathDelete = Path.Combine(path, deleteInfo.Key);

                if (!File.Exists(pathDelete))
                {
                    continue;
                }

                File.Delete(pathDelete);
                log.GetInfoAboutDeleteFiles(pathDelete);
            }
        }

        public void Update(Dictionary<string, string> filesToUpdate, string path, ILog log)
        {
            foreach (var copyInfo in filesToUpdate)
            {
                string pathUpdate = Path.Combine(path, copyInfo.Key);
                string source = Path.Combine(copyInfo.Value, copyInfo.Key);

                if (!File.Exists(pathUpdate))
                {
                    continue;
                }

                File.Copy(source, pathUpdate, true);
                log.GetInfoAboutUpdateFiles(source, pathUpdate);
            }
        }
    }
}
