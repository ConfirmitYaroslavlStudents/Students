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

            foreach (FileDescriptor oldItem in oldFolder.FilesList)
            {
                string path = Path.Combine(otherFolder.Path, oldItem.Path);
                var file = GetItemByPath(otherFolder, oldItem.Path);
                var newFile = GetItemByPath(newFolder, oldItem.Path);

                if (newFile==null && file!=null)
                {
                    deleteFiles.Add(path);
                }
            }

            return deleteFiles;
        }

        

        private static Dictionary<string, string> FindFilesToUpdate()
        {
            var updateDictionary = new Dictionary<string, string>();

            foreach (FileDescriptor masterItem in _newMaster.FilesList)
            {
                FileDescriptor slaveItem = GetItemByPath(_newSlave, masterItem.Path);

                if (slaveItem==null)
                {
                    continue;
                }

                bool haveDifferentContent = (slaveItem.Hash != masterItem.Hash);

                if (haveDifferentContent)
                {
                    var slavePath = Path.Combine(_newSlave.Path, masterItem.Path);
                    var masterPath = Path.Combine(_newMaster.Path, masterItem.Path);
                    updateDictionary.Add(masterPath, slavePath);
                }
            }
            return updateDictionary;
        }

        private static FileDescriptor GetItemByPath(Folder folder,string path)
        {
            foreach(FileDescriptor file in folder.FilesList)
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
            AddPairToFilesToAdd(filesToAdd, _newMaster, _newSlave);
            AddPairToFilesToAdd(filesToAdd, _newSlave, _newMaster);
            return filesToAdd;
        }

        private static void AddPairToFilesToAdd(Dictionary<string, string> filesToAdd, Folder first, Folder second)
        {
            var firstAddFiles = GetNewFiles(first, second);

            foreach (string path in firstAddFiles)
            {
                var firstPath = Path.Combine(first.Path, path);
                var secondPath = Path.Combine(second.Path, path);
                filesToAdd.Add(firstPath, secondPath);
            }
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

            foreach (FileDescriptor firstItem in firstFolder.FilesList)
            {
                var secondFilePath = Path.Combine(secondFolder.Path, firstItem.Path);
                var secondItem = GetItemByPath(secondFolder, firstItem.Path);

                if (secondItem==null)
                {
                    newFilesList.Add(firstItem.Path);
                }
            }

            return newFilesList;
        }
    }
}
