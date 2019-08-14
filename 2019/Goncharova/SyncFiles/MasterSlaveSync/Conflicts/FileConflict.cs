using System.IO.Abstractions;

namespace MasterSlaveSync.Conflict
{
    public class FileConflict : IConflict
    {
        public FileConflict(IFileInfo masterFile, IFileInfo slaveFile)
        {
            this.MasterFile = masterFile;
            this.SlaveFile = slaveFile;
        }

        public IFileInfo MasterFile { get; set; }
        public IFileInfo SlaveFile { get; set; }
    }
}
