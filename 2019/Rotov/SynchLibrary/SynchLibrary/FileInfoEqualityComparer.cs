using System.IO;
using System.Collections.Generic;

namespace SynchLibrary
{
    public class FileInfoEqualityComparer : IEqualityComparer<FileInfo>
    {
        public bool Equals(FileInfo x, FileInfo y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(FileInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
