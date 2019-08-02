using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sync.Wrappers;

namespace Sync
{
    public static class DirectoryInfoExtension
    {
        public static HashSet<IFileSystemElementInfoWrapper> GetContainment(this DirectoryInfo dir)
        {
            return (
                from x in dir.EnumerateFiles()
                select (IFileSystemElementInfoWrapper) new FileInfoWrapper(x)).Union(
                from y in dir.EnumerateDirectories()
                select (IFileSystemElementInfoWrapper) new DirectoryInfoWrapper(y)
            ).ToHashSet();
        }
    }
}