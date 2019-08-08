using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var filesToAdd = FindFilesToAdd();
            var filesToUpdate = FindFilesToUpdate();
            syncData.FilesToCopy = filesToAdd.Union(filesToUpdate).ToDictionary(x => x.Key, x => x.Value);
            syncData = RemoveCollision(syncData);
            syncData.LogFlag = folderSet.Loglevel;
            syncData.NoDeleteFlag = folderSet.NoDeleteFlag;

            return syncData;
        }      
        
        private static List<string> FindFileToDelete()
        {
            var filesToDelete = new List<string>();

            filesToDelete.AddRange(GetFilesToDelete(_oldMaster, _newMaster, _newSlave.Path));
            filesToDelete.AddRange(GetFilesToDelete(_oldSlave, _newSlave, _newMaster.Path));

            return filesToDelete;
        }

        private static List<string> GetFilesToDelete(Folder oldFolder, Folder newFolder, string otherPath)
        {
            var deleteFiles = new List<string>();

            foreach (string _oldPath in oldFolder.FilesPathList)
            {
                if (!newFolder.FilesPathList.Contains(_oldPath))
                {
                    string path = Path.Combine(otherPath, GetSubPath(_oldPath, oldFolder.Path));
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

            foreach (string masterPath in _newMaster.FilesPathList)
            {
                var slavePath = Path.Combine(_newSlave.Path, GetSubPath(masterPath, _newMaster.Path));

                if (!File.Exists(slavePath))
                {
                    continue;
                }

                bool haveDifferentContent = !File.ReadAllBytes(masterPath).SequenceEqual(File.ReadAllBytes(slavePath));

                if (haveDifferentContent)
                {
                    updateDictionary.Add(masterPath, slavePath);
                }
            }
            return updateDictionary;
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

            foreach (string firstPath in firstFolder.FilesPathList)
            {
                var secondPath = Path.Combine(secondFolder.Path, GetSubPath(firstPath, firstFolder.Path));

                if (!File.Exists(secondPath))
                {
                    newFilesList.Add(firstPath);
                }
            }

            return newFilesList;
        }
    }
}
