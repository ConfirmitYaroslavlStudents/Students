using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SynchLibrary
{
    public class FileHandler
    {
        string MasterPath { get; set; }
        string SlavePath { get; set; }
        bool CanRemove { get; set; }

        ILogger _logger;

        public FileHandler(Sync owner)
        {
            MasterPath = owner.MasterPath;
            SlavePath = owner.SlavePath;
            _logger = LoggerFactory.Create("summary");
            CanRemove = owner.CanRemove;
        }

        public void MigrateForFiles()
        {
            var listMaster = new List<FileWrapper>();
            foreach (var file in Directory.GetFiles(MasterPath, "*", SearchOption.AllDirectories))
                listMaster.Add(new FileWrapper(new FileInfo(file), MasterPath));
            var listSlave = new List<FileWrapper>();
            foreach (var file in Directory.GetFiles(SlavePath, "*", SearchOption.AllDirectories))
                listSlave.Add(new FileWrapper(new FileInfo(file), SlavePath));

            var intersection = listMaster.Intersect(listSlave);
            var masterWithoutSlave = listMaster.Except(listSlave);
            var slaveWithoutMaster = listSlave.Except(listMaster);
            MoveIntersectionFiles(intersection);
            if (CanRemove)
                RemoveDisapperedFiles(slaveWithoutMaster);
            else
                SwapFiles(masterWithoutSlave, slaveWithoutMaster);
            LoggerForConsole.PrintLog(_logger.GetLogs());
        }

        private void MoveIntersectionFiles(IEnumerable<FileWrapper> files)
        {
            foreach (var file in files)
            {
                var old = Path.Combine(MasterPath, file.RelativePath);
                var fresh = Path.Combine(SlavePath, string.Join(@"\", file.RelativePath));
                if (FilesChanged(old, fresh))
                {
                    File.Copy(old, fresh, true);
                    _logger.AddReplace(file.RelativePath, SlavePath);
                }
            }
        }

        private bool FilesChanged(string file1, string file2)
        {
            FileInfo first = new FileInfo(file1);
            FileInfo second = new FileInfo(file2);
            if (first.Length != second.Length)
                return true;
            else
            {
                var firstSize = File.ReadAllBytes(first.FullName);
                var secondSize = File.ReadAllBytes(second.FullName);
                for (int i = 0; i < firstSize.Length; i++)
                {
                    if (firstSize[i] != secondSize[i])
                        return true;
                }
                return false;
            }
        }

        public void RemoveDisapperedFiles(IEnumerable<FileWrapper> files)
        {
            foreach (var file in files)
            {
                File.Delete(Path.Combine(SlavePath, file.RelativePath));
                _logger.AddRemove(file.RelativePath, SlavePath);
            }
        }

        private void SwapFiles(IEnumerable<FileWrapper> master, IEnumerable<FileWrapper> slave)
        {
            foreach (var file in master)
            {
                CreateAllDirsForFile(file.RelativePath, SlavePath);
                File.Copy(Path.Combine(MasterPath, file.RelativePath), Path.Combine(SlavePath, file.RelativePath));
                _logger.AddCopy(file.RelativePath, MasterPath, SlavePath);
            }
            foreach (var file in slave)
            {
                CreateAllDirsForFile(file.RelativePath, MasterPath);
                File.Copy(Path.Combine(SlavePath, file.RelativePath), Path.Combine(MasterPath, file.RelativePath));
                _logger.AddCopy(file.RelativePath, SlavePath, MasterPath);
            }
        }

        public void CreateAllDirsForFile(string path, string root)
        {
            string current = root;
            string[] folders = path.Split(new char[] { '\\' });
            for (int i = 0; i < folders.Length - 1; i++)
            {
                current = Path.Combine(current, folders[i]);
                if (!Directory.Exists(current))
                    Directory.CreateDirectory(current);
            }
        }
    }
}
