using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncDataReaderNoDeleteStrategy : ISyncDataReaderStrategy
    {
        private Dictionary<string, string> _addDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> _updateDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> _deleteDictionary = new Dictionary<string, string>();

        public SyncData MakeSyncData(FolderSet folderSet)
        {
           foreach (var folderPair in folderSet.FolderList)
            {
               _addDictionary = FindNewFiles(folderPair, _addDictionary);
               _updateDictionary = FindUpdateFiles(folderPair, _updateDictionary);
               _deleteDictionary = FindDeleteFiles(folderPair, _deleteDictionary);
            }

            var syncData = new SyncData(_addDictionary, _updateDictionary, new Dictionary<string, string>());

            return syncData;
        }

        private Dictionary<string,string> FindDeleteFiles(FolderPair folderPair, Dictionary<string,string> deleteDictionary)
        {
            var newFolder = folderPair.New;
            var oldFolder = folderPair.Old;

            foreach (var oldFile in oldFolder.FilesList)
            {
                var newFile = GetItemByPath(newFolder, oldFile.Path);

                if (newFile == null && !deleteDictionary.ContainsKey(oldFile.Path))
                {
                    if (_updateDictionary.ContainsKey(oldFile.Path))
                    {
                        continue;
                    }

                    deleteDictionary.Add(oldFile.Path, oldFolder.Path);
                }
            }

            return deleteDictionary;
        }

        private Dictionary<string, string> FindNewFiles(FolderPair folderPair, Dictionary<string, string> addDictionary)
        {
            var newFolder = folderPair.New;
            var oldFolder = folderPair.Old;

            foreach (var newFile in newFolder.FilesList)
            {
                var oldFile = GetItemByPath(oldFolder, newFile.Path);

                if (oldFile == null && !addDictionary.ContainsKey(newFile.Path))
                {
                    addDictionary.Add(newFile.Path, newFolder.Path);
                }
            }

            return addDictionary;
        }

        private Dictionary<string, string> FindUpdateFiles(FolderPair folderPair, Dictionary<string, string> updateDictionary)
        {
            var newFolder = folderPair.New;
            var oldFolder = folderPair.Old;

            foreach (var newFile in newFolder.FilesList)
            {
                var oldFile = GetItemByPath(oldFolder, newFile.Path);

                if (oldFile == null)
                {
                    continue;
                }

                bool haveDifferentContent = (newFile.Hash != oldFile.Hash);

                if (haveDifferentContent && !updateDictionary.ContainsKey(newFile.Path) && !_deleteDictionary.ContainsKey(newFile.Path))
                {
                    updateDictionary.Add(newFile.Path, newFolder.Path);
                }
            }

            return updateDictionary;
        }

        private static FileDescriptor GetItemByPath(Folder folder, string path)
        {
            foreach (FileDescriptor file in folder.FilesList)
            {
                if (file.Path == path)
                {
                    return file;
                }
            }
            return null;
        }        
    }
}
