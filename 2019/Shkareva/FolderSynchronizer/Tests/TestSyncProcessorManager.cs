using FolderSynchronizerLib;
using System.Collections.Generic;

namespace Tests
{
    public class TestSyncProcessorManager : ISyncProcessorManager
    {
        List<Folder> _folders;

        public TestSyncProcessorManager(List<Folder> folders)
        {
            _folders = folders;   
        }
        
        public void Copy(Dictionary<string, string> filesToCopy, string path, ILog log)
        {
           
        }

        private FileDescriptor GetItemByPath(List<FileDescriptor> files, string path)
        {
            foreach (FileDescriptor file in files)
            {
                if (file.Path == path)
                {
                    return file;
                }
            }
            return null;
        }

        public void Delete(Dictionary<string,string> filesToDelete, string path, ILog log)
        {
            
        }

        public void Update(Dictionary<string, string> fileToUpdate, string path, ILog log)
        {
        }
    }
}
