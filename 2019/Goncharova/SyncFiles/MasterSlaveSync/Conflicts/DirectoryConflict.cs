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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType() 
                || (MasterDirectory == null && SlaveDirectory == null))
            {
                return false;
            }

            DirectoryConflict conflict = (DirectoryConflict)obj;

            if (MasterDirectory == null)
            {
                return conflict.MasterDirectory == null 
                    && (SlaveDirectory.FullName == conflict.SlaveDirectory.FullName);
            }
            if (SlaveDirectory == null)
            {
                return conflict.SlaveDirectory == null 
                    && (MasterDirectory.FullName == conflict.MasterDirectory.FullName);
            }

            return (SlaveDirectory.FullName == conflict.SlaveDirectory.FullName)
                && (MasterDirectory.FullName == conflict.MasterDirectory.FullName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}