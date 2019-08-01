using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SynchLibrary
{
    public class FolderHandler
    {
        string MasterPath { get; set; }
        string SlavePath { get; set; }

        public FolderHandler(string master, string slave)
        {
            MasterPath = master;
            SlavePath = slave;
        }

        public void MigrateForFolders()
        {
            var dirMaster = GetAllDirecroty(new DirectoryInfo(MasterPath));
            var dirSlave = GetAllDirecroty(new DirectoryInfo(SlavePath));
            var masterWithoutSlave = dirMaster.Except(dirSlave, new FolderWrapperComparer());
            var slaveWithoutMaster = dirSlave.Except(dirMaster, new FolderWrapperComparer());
            MoveSwapFolders(masterWithoutSlave, slaveWithoutMaster);
        }

        private List<FolderWrapper> GetAllDirecroty(DirectoryInfo inputPath)
        {
            List<FolderWrapper> result = new List<FolderWrapper>();
            void DFS(FolderWrapper curDir)
            {
                var listOfDir = curDir.dir.GetDirectories();
                foreach (var dir in listOfDir)
                {
                    var newList = new List<string>(curDir.folders);
                    FolderWrapper current = new FolderWrapper(newList, dir);
                    current.AddValue(dir.Name);
                    result.Add(current);
                    DFS(current);
                }
            }
            DFS(new FolderWrapper(new List<string>(), inputPath));
            return result;
        }

        private void MoveSwapFolders(IEnumerable<FolderWrapper> master, IEnumerable<FolderWrapper> slave)
        {
            foreach (var folder in slave)
            {
                var currentDir = MasterPath;
                foreach (var item in folder.folders)
                {
                    currentDir = Path.Combine(currentDir, item);
                    if (Directory.Exists(currentDir))
                        continue;
                    Directory.CreateDirectory(currentDir);
                }
            }

            foreach (var folder in master)
            {
                var currentDir = SlavePath;
                foreach (var item in folder.folders)
                {
                    currentDir = Path.Combine(currentDir, item);
                    if (Directory.Exists(currentDir))
                        continue;
                    Directory.CreateDirectory(currentDir);
                }
            }
        }
    }
}
