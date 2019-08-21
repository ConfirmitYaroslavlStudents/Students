using System;
using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class TestSyncProcManager : ISyncProcManager
    {
        List<FileDescriptor> _masterFiles;
        List<FileDescriptor> _slaveFiles;

        public TestSyncProcManager(List<FileDescriptor> master, List<FileDescriptor> slave)
        {
            _masterFiles = master;
            _slaveFiles = slave;
        }
        
        public void Copy(Dictionary<string, string> filesToCopy)
        {
           foreach(KeyValuePair<string,string> itemPair in filesToCopy)
            {
                FileDescriptor itemKeyM = GetItemByPath(_masterFiles, itemPair.Key);
                FileDescriptor itemKeyS = GetItemByPath(_slaveFiles, itemPair.Key);
                FileDescriptor itemValueM = GetItemByPath(_masterFiles, itemPair.Value);
                FileDescriptor itemValueS = GetItemByPath(_slaveFiles, itemPair.Value);

                if (itemKeyM != null && itemValueS==null)
                {
                    _slaveFiles.Add(new FileDescriptor(itemPair.Value, itemKeyM.Hash));
                }

                if (itemKeyS != null && itemValueM==null)
                {
                    _masterFiles.Add(new FileDescriptor(itemPair.Value, itemKeyS.Hash));
                }

                if (itemKeyM != null && itemValueS != null)
                {
                    itemValueS.Hash = itemKeyM.Hash;
                }
            }
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

        public void Delete(List<string> filesToDelete)
        {
            foreach(string path in filesToDelete)
            {
                var itemM = GetItemByPath(_masterFiles, path);
                var itemS = GetItemByPath(_slaveFiles, path);

                if (itemM != null)
                {
                    _masterFiles.Remove(itemM);
                }
                if (itemS != null)
                {
                    _slaveFiles.Remove(itemS);
                }

            }
        }
    }
}
