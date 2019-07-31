using System.Collections.Generic;
using System.IO;
using System.Linq;
using SyncTool.Wrappers;

namespace SyncTool
{
    public static class DirectoryInfoExtension
    {
        public static HashSet<IFileSystemElementWrapper> GetContainment(this DirectoryInfo dir)
        {
            return (
                from x in dir.EnumerateFiles()
                select (IFileSystemElementWrapper)new FileInfoWrapper(x)).Union(
                from y in dir.EnumerateDirectories()
                select (IFileSystemElementWrapper)new DirectoryInfoWrapper(y)
            ).ToHashSet();
        }
    }
}