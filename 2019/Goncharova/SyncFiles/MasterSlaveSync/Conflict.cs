using System.IO;

namespace MasterSlaveSync
{
    internal class Conflict
    {
        public Conflict(FileInfo masterFile, FileInfo slaveFile)
        {
            this.MasterFile = masterFile;
            this.SlaveFile = slaveFile;
        }

        public FileInfo MasterFile { get; set; }
        public FileInfo SlaveFile { get; set; }
    }
}
