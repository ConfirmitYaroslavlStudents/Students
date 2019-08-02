using System;
using System.Collections.Generic;
using Sync.Wrappers;

namespace Sync.Comparers
{
    public class DefaultDirectoryComparer : IComparer<DirectoryInfoInfoWrapper>
    {
        public int Compare(DirectoryInfoInfoWrapper x, DirectoryInfoInfoWrapper y)
        {
            if (y is null)
                return -1;
            if (x is null)
                return 1;

            return string.Compare(x.CurrentDirectory.Name, y.CurrentDirectory.Name, StringComparison.Ordinal);
        }
    }
}