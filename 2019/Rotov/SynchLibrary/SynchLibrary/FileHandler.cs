using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SynchLibrary
{
    public class FileHandler
    {
        string MasterPath { get; set; }
        string SlavePath { get; set; }

        Logger _logger;

        public FileHandler(string master, string slave, TypesOfLogging type)
        {
            MasterPath = master;
            SlavePath = slave;
            _logger = new Logger(type);
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
            SwapFiles(masterWithoutSlave, slaveWithoutMaster);
            _logger.PrintLogs();
        }

        private void MoveIntersectionFiles(IEnumerable<FileWrapper> files)
        {
            foreach (var file in files)
            {
                var old = Path.Combine(MasterPath, string.Join(@"\", file.RelativePath));
                var fresh = Path.Combine(SlavePath, string.Join(@"\", file.RelativePath));
                File.Copy(old, fresh, true);
                _logger.AddReplace();
            }
        }

        private void SwapFiles(IEnumerable<FileWrapper> master, IEnumerable<FileWrapper> slave)
        {
            foreach (var file in master)
            {
                CreateAllDirsForFile(file.RelativePath, SlavePath);
                File.Copy(Path.Combine(MasterPath, file.RelativePath), Path.Combine(SlavePath, file.RelativePath));
                _logger.AddCopy();
            }
            foreach (var file in slave)
            {
                CreateAllDirsForFile(file.RelativePath, MasterPath);
                File.Copy(Path.Combine(SlavePath, file.RelativePath), Path.Combine(MasterPath, file.RelativePath));
                _logger.AddCopy();
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
