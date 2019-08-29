using System.Collections.Generic;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class DefaultFileComparer : IEqualityComparer<IFileInfo>
    {
        public bool Equals(IFileInfo x, IFileInfo y)
        {
            if ((x.Name == y.Name) &&
                (x.Length == y.Length) &&
                (x.LastWriteTime == y.LastWriteTime))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(IFileInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
