using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncDataReaderNoDeleteStrategy : ISyncDataReaderStrategy
    {
        private static Dictionary<string, string> _addDictionary = new Dictionary<string, string>();
        private static Dictionary<string, string> _updateDictionary = new Dictionary<string, string>();
        private static Dictionary<string, string> _deleteDictionary = new Dictionary<string, string>();

        public SyncData MakeSyncData(FolderSet folderSet)
        {
            var syncData = new SyncData();


            foreach (var folderPair in folderSet.FolderList)
            {
                FindNewFiles(folderPair);
                FindUpdateFiles(folderPair);
                FindDeleteFiles(folderPair);
            }

            syncData.FilesToDelete = _deleteDictionary;
            syncData.FilesToCopy = _addDictionary;
            syncData.FilesToUpdate = _updateDictionary;
            syncData.LogFlag = folderSet.Loglevel;            
            syncData.FilesToDelete.Clear();

            return syncData;
        }

        private static void FindDeleteFiles(FolderPair folderPair)
        {
            var newFolder = folderPair.New;
            var oldFolder = folderPair.Old;

            foreach (var oldFile in oldFolder.FilesList)
            {
                var newFile = GetItemByPath(newFolder, oldFile.Path);

                if (newFile == null && !_deleteDictionary.ContainsKey(oldFile.Path))
                {
                    if (_updateDictionary.ContainsKey(oldFile.Path))
                    {
                        continue;
                    }

                    _deleteDictionary.Add(oldFile.Path, oldFolder.Path);
                }
            }
        }

        private static void FindNewFiles(FolderPair folderPair)
        {
            var newFolder = folderPair.New;
            var oldFolder = folderPair.Old;

            foreach (var newFile in newFolder.FilesList)
            {
                var oldFile = GetItemByPath(oldFolder, newFile.Path);

                if (oldFile == null && !_addDictionary.ContainsKey(newFile.Path))
                {
                    _addDictionary.Add(newFile.Path, newFolder.Path);
                }
            }
        }

        private static void FindUpdateFiles(FolderPair folderPair)
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

                if (haveDifferentContent && !_updateDictionary.ContainsKey(newFile.Path) && !_deleteDictionary.ContainsKey(newFile.Path))
                {
                    _updateDictionary.Add(newFile.Path, newFolder.Path);
                }
            }
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
