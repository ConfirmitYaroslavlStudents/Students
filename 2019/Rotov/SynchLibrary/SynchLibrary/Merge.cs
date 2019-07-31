using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SynchLibrary
{
    public class Merge
    {
        string MasterPath { get; set; }
        string SlavePath { get; set; }

        public Merge(string master, string slave)
        {
            MasterPath = master;
            SlavePath = slave;
        }

        public void Working()
        {
            DirectoryInfo master = new DirectoryInfo(MasterPath);
            DirectoryInfo slave = new DirectoryInfo(SlavePath);
            var masterSet = master.GetFiles();
            var slaveSet = slave.GetFiles();
            var intersection = masterSet.Intersect(slaveSet, new FileInfoEqualityComparer());
            var masterWithoutSlave = masterSet.Except(slaveSet, new FileInfoEqualityComparer());
            var slaveWithoutMaster = slaveSet.Except(masterSet, new FileInfoEqualityComparer());
            MoveSwap(masterWithoutSlave, slaveWithoutMaster);
            MoveIntersection(intersection);
        }

        private void MoveIntersection(IEnumerable<FileInfo> files)
        {
            foreach(var file in files)
            {
                Replace(file);
            }
        }

        private void MoveSwap(IEnumerable<FileInfo> master, IEnumerable<FileInfo> slave)
        {
            foreach(var file in master)
            {
                File.Copy(file.FullName, Path.Combine(SlavePath, file.Name));
            }

            foreach (var file in slave)
            {
                File.Copy(file.FullName, Path.Combine(MasterPath, file.Name));
            }
        }

        private void Replace(FileInfo file)
        {
            File.Copy(Path.Combine(MasterPath, file.Name), Path.Combine(SlavePath, file.Name), true);
        }
    }
}
