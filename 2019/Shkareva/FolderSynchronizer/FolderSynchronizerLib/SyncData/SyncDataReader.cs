using System;
using System.Collections.Generic;
using System.IO;

namespace FolderSynchronizerLib
{
    public static class SyncDataReader
    {
        private static Folder _oldMaster;
        private static Folder _oldSlave;
        private static Folder _newMaster;
        private static Folder _newSlave;

        public static SyncData Load(FolderSet folderSet)
        {
            var syncData = new SyncData();
            _newMaster = folderSet.NewMaster;
            _newSlave = folderSet.NewSlave;
            _oldMaster = folderSet.OldMaster;
            _oldSlave = folderSet.OldSlave;

            syncData.FilesToDelete = FindFileToDelete();
            syncData.FilesToCopy = FindFilesToAdd();
            syncData.FilesToUpdate = FindFilesToUpdate();
            syncData = RemoveCollision(syncData);
            syncData.LogFlag = folderSet.Loglevel;
            syncData.NoDeleteFlag = folderSet.NoDeleteFlag;

            return syncData;
        }      
        
        private static List<string> FindFileToDelete()
        {
            var filesToDelete = new List<string>();

            filesToDelete.AddRange(GetFilesToDelete(_oldMaster, _newMaster, _newSlave));
            filesToDelete.AddRange(GetFilesToDelete(_oldSlave, _newSlave, _newMaster));

            return filesToDelete;
        }

        private static List<string> GetFilesToDelete(Folder oldFolder, Folder newFolder, Folder otherFolder)
        {
            var deleteFiles = new List<string>();

            foreach (Item oldItem in oldFolder.FilesList)
            {
                string path = Path.Combine(otherFolder.Path, GetSubPath(oldItem.Path, oldFolder.Path));
                var file = GetItemByPath(otherFolder, path);
                var newFile = GetItemByPath(newFolder, oldItem.Path);

                if (newFile==null && file!=null)
                {
                    deleteFiles.Add(path);
                }
            }

            return deleteFiles;
        }

        private static string GetSubPath(string longPath, string firstPartPath)
        {
            return longPath.Substring(firstPartPath.Length + 1);
        }

        private static Dictionary<string, string> FindFilesToUpdate()
        {
            var updateDictionary = new Dictionary<string, string>();

            foreach (Item masterItem in _newMaster.FilesList)
            {
                var slavePath = Path.Combine(_newSlave.Path, GetSubPath(masterItem.Path, _newMaster.Path));
                Item slaveItem = GetItemByPath(_newSlave, slavePath);

                if (slaveItem==null)
                {
                    continue;
                }

                bool haveDifferentContent = (slaveItem.Hash != masterItem.Hash);

                if (haveDifferentContent)
                {
                    updateDictionary.Add(masterItem.Path, slavePath);
                }
            }
            return updateDictionary;
        }

        private static Item GetItemByPath(Folder folder,string path)
        {
            foreach(Item file in folder.FilesList)
            {
                if (file.Path == path)
                {
                    return file;
                }
            }
            return null;
        }

        private static Dictionary<string, string> FindFilesToAdd()
        {
            var filesToAdd = new Dictionary<string, string>();
            var masterAddFiles = GetNewFiles(_newMaster, _newSlave);

            foreach (string path in masterAddFiles)
            {
                var slavePath = Path.Combine(_newSlave.Path, GetSubPath(path, _newMaster.Path));
                filesToAdd.Add(path, slavePath);
            }

            var slaveAddFiles = GetNewFiles(_newSlave, _newMaster);

            foreach (string path in slaveAddFiles)
            {
                var masterPath = Path.Combine(_newMaster.Path, GetSubPath(path, _newSlave.Path));
                filesToAdd.Add(path, masterPath);
            }

            return filesToAdd;
        }

        private static SyncData RemoveCollision(SyncData syncData)
        {
            foreach (string path in syncData.FilesToDelete)
            {
                if (syncData.FilesToCopy.ContainsKey(path))
                {
                    syncData.FilesToCopy.Remove(path);
                }
            }

            return syncData;
        }

        private static List<string> GetNewFiles(Folder firstFolder, Folder secondFolder)
        {
            var newFilesList = new List<string>();

            foreach (Item firstItem in firstFolder.FilesList)
            {
                var secondFilePath = Path.Combine(secondFolder.Path, GetSubPath(firstItem.Path, firstFolder.Path));
                var secondItem = GetItemByPath(secondFolder, secondFilePath);

                if (secondItem==null)
                {
                    newFilesList.Add(firstItem.Path);
                }
            }

            return newFilesList;
        }
    }
}
