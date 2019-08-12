using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public interface ISyncProcManager
    {
        void Copy(Dictionary<string, string> fileToCopy);
        void Delete(List<string> fileToDelete);
    }
}
