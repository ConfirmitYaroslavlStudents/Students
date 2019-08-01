using System.Collections.Generic;
using System.IO;
using System.Linq;
using SyncTool.Wrappers;

namespace SyncTool
{
    public static class DirectoryInfoExtension
    {
        public static HashSet<IFileSystemElementInfoWrapper> GetContainment(this DirectoryInfo dir)
        {
            return (
                from x in dir.EnumerateFiles()
                select (IFileSystemElementInfoWrapper) new FileInfoInfoWrapper(x)).Union(
                from y in dir.EnumerateDirectories()
                select (IFileSystemElementInfoWrapper) new DirectoryInfoInfoWrapper(y)
            ).ToHashSet();
        }
    }
}