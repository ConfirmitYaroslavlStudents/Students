using FolderSynchronizerLib;
using System.Collections.Generic;

namespace Tests
{
    public class TestSyncProcessorManager : ISyncProcessorManager
    {
        List<FileDescriptor> _masterFiles;
        List<FileDescriptor> _slaveFiles;

        public TestSyncProcessorManager(List<FileDescriptor> master, List<FileDescriptor> slave)
        {
            _masterFiles = master;
            _slaveFiles = slave;
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
