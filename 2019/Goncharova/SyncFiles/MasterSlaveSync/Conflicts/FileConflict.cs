using System.IO.Abstractions;

namespace MasterSlaveSync.Conflicts
{
    public class FileConflict
    {
        public FileConflict(IFileInfo masterFile, IFileInfo slaveFile)
        {
            this.MasterFile = masterFile;
            this.SlaveFile = slaveFile;
        }
        public IFileInfo MasterFile { get; set; }
        public IFileInfo SlaveFile { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()
                || (MasterFile == null && SlaveFile == null))
            {
                return false;
            }

            FileConflict conflict = (FileConflict)obj;

            if (MasterFile == null)
            {
                return conflict.MasterFile == null && (SlaveFile.FullName == conflict.SlaveFile.FullName);
            }
            if (SlaveFile == null)
            {
                return conflict.SlaveFile == null && (MasterFile.FullName == conflict.MasterFile.FullName);
            }

            return (SlaveFile.FullName == conflict.SlaveFile.FullName)
                && (MasterFile.FullName == conflict.MasterFile.FullName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
