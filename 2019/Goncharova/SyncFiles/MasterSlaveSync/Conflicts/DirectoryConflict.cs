using System.IO.Abstractions;

namespace MasterSlaveSync.Conflicts
{
    public class DirectoryConflict
    {
        public DirectoryConflict(IDirectoryInfo masterDirectory, IDirectoryInfo slaveDirectory)
        {
            MasterDirectory = masterDirectory;
            SlaveDirectory = slaveDirectory;
        }

        public IDirectoryInfo MasterDirectory { get; set; }
        public IDirectoryInfo SlaveDirectory { get; set; }
    }
}