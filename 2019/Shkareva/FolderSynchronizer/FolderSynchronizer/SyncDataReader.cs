using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderSynchronizer
{
    public static class SyncDataReader
    {
        private static Folder _oldMaster;
        private static Folder _oldSlave;
        private static Folder _newMaster;
        private static Folder _newSlave;

        public static SyncData Load(string[] args)
        {
            if (args.Length < 2)
            {
                throw new SyncException("invalid format command");
            }

            var syncData = new SyncData();
            const string noDelete = "--no-delete";
            const string logLevel = "-loglevel";

            _newMaster = new FolderWorker().LoadFolder(args[0]);
            _newSlave = new FolderWorker().LoadFolder(args[1]);
            _oldMaster = new FolderWorker().LoadSerializedFolder(args[0]);
            _oldSlave = new FolderWorker().LoadSerializedFolder(args[1]);

            syncData.FilesToDelete = FindFileToDelete();
            var filesToAdd = FindFilesToAdd();
            var filesToUpdate = FindFilesToUpdate();
            syncData.FilesToCopy = filesToAdd.Union(filesToUpdate).ToDictionary(x => x.Key, x => x.Value);
            syncData = RemoveCollision(syncData);
            var flagList = new List<string>();
            int count = 2;

            while (count < args.Length)
            {
                flagList.Add(args[count]);
                count++;
            }

            if (flagList.Contains(noDelete))
            {
                syncData.NoDeleteFlag = true;
            }

            if (flagList.Contains(logLevel))
            {
                syncData.LogFlag = GetLogFlag(flagList, logLevel);
            }

            return syncData;
        }

        private static string GetLogFlag(List<string> flagList, string logLevel)
        {
            List<string> validLogFlags = new List<string>() { "verbose", "summary", "silent" };
            string logFlag = "";

            try
            {
                logFlag = flagList[flagList.IndexOf(logLevel) + 1];
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Do not specify the type of logging");
            }

            if (!validLogFlags.Contains(logFlag))
            {
                throw new Exception("Invalid type of logging");
            }

            return logFlag;
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
