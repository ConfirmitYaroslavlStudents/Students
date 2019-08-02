using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sync.Wrappers;

namespace Sync
{
    public static class DirectoryInfoExtension
    {
        public static HashSet<IFileSystemElementWrapper> GetContainment(this DirectoryInfo dir)
        {
            return (
                from x in dir.EnumerateFiles()
                select (IFileSystemElementWrapper) new FileWrapper(x)).Union(
                from y in dir.EnumerateDirectories()
                select (IFileSystemElementWrapper) new DirectoryWrapper(y)
            ).ToHashSet();
        }
    }
}