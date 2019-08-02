using System;
using System.Collections.Generic;
using Sync.Wrappers;

namespace Sync.Comparers
{
    public class DefaultDirectoryComparer : IComparer<DirectoryWrapper>
    {
        public int Compare(DirectoryWrapper x, DirectoryWrapper y)
        {
            if (y is null)
                return -1;
            if (x is null)
                return 1;

            return string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}