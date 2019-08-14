using System;
using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class TestSyncProcManager : ISyncProcManager
    {
        List<Item> _masterFiles;
        List<Item> _slaveFiles;

        public TestSyncProcManager(List<Item> master, List<Item> slave)
        {
            _masterFiles = master;
            _slaveFiles = slave;
        }
        
        public void Copy(Dictionary<string, string> filesToCopy)
        {
           foreach(KeyValuePair<string,string> itemPair in filesToCopy)
            {
                Item itemKeyM = GetItemByPath(_masterFiles, itemPair.Key);
                Item itemKeyS = GetItemByPath(_slaveFiles, itemPair.Key);
                Item itemValueM = GetItemByPath(_masterFiles, itemPair.Value);
                Item itemValueS = GetItemByPath(_slaveFiles, itemPair.Value);

                if (itemKeyM != null && itemValueS==null)
                {
                    _slaveFiles.Add(new Item(itemPair.Value, itemKeyM.Hash));
                }

                if (itemKeyS != null && itemValueM==null)
                {
                    _masterFiles.Add(new Item(itemPair.Value, itemKeyS.Hash));
                }

                if (itemKeyM != null && itemValueS != null)
                {
                    itemValueS.Hash = itemKeyM.Hash;
                }
            }
        }

        private Item GetItemByPath(List<Item> files, string path)
        {
            foreach (Item file in files)
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
