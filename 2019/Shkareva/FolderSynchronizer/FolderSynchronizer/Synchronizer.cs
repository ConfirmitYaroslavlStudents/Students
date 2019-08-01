using System.Linq;
using System.IO;

// TODO : изменять поля Slave/Master Folder при добавлении папок/файлов

namespace FolderSynchronizer
{
    static class Synchronizer
    {
        public static void Synchronize(Folder MasterFolder, Folder SlaveFolder)
        {
            AddAbsentFiles(MasterFolder, SlaveFolder);
            AddAbsentFolders(MasterFolder, SlaveFolder);
            AddAbsentFiles(SlaveFolder, MasterFolder);
            AddAbsentFolders(SlaveFolder, MasterFolder);
            UpdateFilesWithTheSameName(MasterFolder, SlaveFolder);
        }

        private static void AddAbsentFolders(Folder masterFolder, Folder slaveFolder)
        {
            foreach(Folder inMasterFolder in masterFolder.internalFolders)
            {
                var slavePath = slaveFolder.Path + "\\" + GetShortName(inMasterFolder.Path);

                if (!Directory.Exists(slavePath))
                {
                    Directory.CreateDirectory(slavePath);
                    slaveFolder.internalFoldersPaths.Add(slavePath);
                    slaveFolder.internalFolders.Add(new Folder(slavePath));                    
                }

                var inSlaveFolder = GetFolderByPath(slaveFolder, slavePath);
                Synchronize(inMasterFolder, inSlaveFolder);
            }
        }

        private static Folder GetFolderByPath(Folder folder, string path)
        {
            Folder returnFolder = new Folder();

            foreach(Folder internalFolder in folder.internalFolders)
            {
                if (internalFolder.Path == path)
                {
                    returnFolder = internalFolder;
                    break;
                }
            }

            return returnFolder;
        }

        private static void UpdateFilesWithTheSameName(Folder masterFolder, Folder slaveFolder)
        {
            foreach (string masterPath in masterFolder.FilesPaths)
            {
                var slavePath = slaveFolder.Path + "\\" + GetShortName(masterPath);
                bool haveDifferentContent = !File.ReadAllBytes(masterPath).SequenceEqual(File.ReadAllBytes(slavePath));

                if (File.Exists(slavePath) && haveDifferentContent)
                {
                    File.Copy(masterPath, slavePath, true);
                }
            }
        }

        private static string GetShortName(string masterPath)
        {
            var lines = masterPath.Split('\\');
            return lines[lines.Length - 1];
        }

        private static void AddAbsentFiles(Folder firstFolder, Folder secondFolder)
        {
            foreach(string firstPath in firstFolder.FilesPaths)
            {
                var secondPath = secondFolder.Path + "\\" + GetShortName(firstPath);

                if (!File.Exists(secondPath))
                {
                    File.Copy(firstPath, secondPath);
                }
            }
        }        
    }
}
