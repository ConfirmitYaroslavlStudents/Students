using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public interface ISyncProcessorManager
    {
        void Copy(Dictionary<string, string> fileToCopy, string path, ILog log);
        void Delete(Dictionary<string, string> fileToDelete, string path, ILog log);
        void Update(Dictionary<string, string> fileToUpdate, string path, ILog log);
    }
}
