using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SynchLibrary
{
    public class FolderWrapper
    {
        public DirectoryInfo dir;
        public List<string> folders;

        public FolderWrapper(List<string> paths, DirectoryInfo d)
        {
            dir = d;
            folders = paths;
        }

        public void AddValue(string val)
        {
            folders.Add(val);
        }
    }


    public class FolderWrapperComparer : IEqualityComparer<FolderWrapper>
    {
        public bool Equals(FolderWrapper x, FolderWrapper y)
        {
            return x.folders.SequenceEqual(y.folders);
        }

        public int GetHashCode(FolderWrapper obj)
        {
            return obj.folders.GetHashCode();
        }
    }

}
