using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SynchLibrary
{
    public class FileHandler
    {
        string MasterPath { get; set; }
        string SlavePath { get; set; }

        public FileHandler(string master, string slave)
        {
            MasterPath = master;
            SlavePath = slave;
        }

        public void MigrateForFiles()
        {
            var listMaster = new List<WrapperForFile>();
            foreach (var file in Directory.GetFiles(MasterPath, "*", SearchOption.AllDirectories))
                listMaster.Add(new WrapperForFile(new FileInfo(file), MasterPath));
            var listSlave = new List<WrapperForFile>();
            foreach (var file in Directory.GetFiles(SlavePath, "*", SearchOption.AllDirectories))
                listSlave.Add(new WrapperForFile(new FileInfo(file), SlavePath));

            var intersection = listMaster.Intersect(listSlave, new FileWrapperComparer());
            var masterWithoutSlave = listMaster.Except(listSlave, new FileWrapperComparer());
            var slaveWithoutMaster = listSlave.Except(listMaster, new FileWrapperComparer());
            MoveIntersectionFiles(intersection);
            MoveSwapFiles(masterWithoutSlave, slaveWithoutMaster);
        }

        private void MoveIntersectionFiles(IEnumerable<WrapperForFile> files)
        {
            foreach (var file in files)
            {
                var old = Path.Combine(MasterPath, string.Join(@"\", file.RelativePath));
                var fresh = Path.Combine(SlavePath, string.Join(@"\", file.RelativePath));
                File.Copy(old, fresh, true);
            }
        }

        private void MoveSwapFiles(IEnumerable<WrapperForFile> master, IEnumerable<WrapperForFile> slave)
        {
            foreach (var file in master)
                File.Copy(Path.Combine(MasterPath, file.RelativePath), Path.Combine(SlavePath, file.RelativePath));
            foreach (var file in slave)
                File.Copy(Path.Combine(SlavePath, file.RelativePath), Path.Combine(MasterPath, file.RelativePath));
        }
    }
}
