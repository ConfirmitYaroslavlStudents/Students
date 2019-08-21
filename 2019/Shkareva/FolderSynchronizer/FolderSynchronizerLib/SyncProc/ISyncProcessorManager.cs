using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public interface ISyncProcessorManager
    {
        void Copy(Dictionary<string, string> fileToCopy, string path);
        void Delete(Dictionary<string, string> fileToDelete, string path);
        void Update(Dictionary<string, string> fileToUpdate, string path);
    }
}
