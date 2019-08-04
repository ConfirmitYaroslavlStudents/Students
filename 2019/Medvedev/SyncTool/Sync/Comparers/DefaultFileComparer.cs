using System.Collections.Generic;
using Sync.Wrappers;

namespace Sync.Comparers
{
    public class DefaultFileComparer : IComparer<FileWrapper>
    {
        public int Compare(FileWrapper x, FileWrapper y)
        {
            if (x is null && y is null)
                return 0;
            if (x is null)
                return 1;
            if (y is null)
                return -1;

            if (x.Attributes.Size < y.Attributes.Size)
                return 1;
            if (x.Attributes.Size > y.Attributes.Size)
                return -1;
            return -x.Attributes.LastWriteTime.CompareTo(y.Attributes.LastWriteTime);
        }
    }
}