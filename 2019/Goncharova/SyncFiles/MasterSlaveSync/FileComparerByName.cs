using System.Collections.Generic;
using System.IO;

namespace MasterSlaveSync
{
    public class FileComparerByName : IEqualityComparer<FileInfo>
    {
        public bool Equals(FileInfo x, FileInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(FileInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
