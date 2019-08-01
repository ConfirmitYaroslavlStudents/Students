using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SynchLibrary
{
    public class WrapperForFile
    {
        public string RelativePath;

        public WrapperForFile(FileInfo file, string root)
        {
            SetAbsolutePath(file, root);
        }

        public void SetAbsolutePath(FileInfo file, string root)
        {
            var count = root.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).Length;
            var parts = file.FullName.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            var relative = new string[parts.Length - count];
            for (int i = count; i < parts.Length; i++)
                relative[i - count] = parts[i];
            RelativePath =  string.Join(@"\", relative);
        }
    }


    public class FileWrapperComparer: IEqualityComparer<WrapperForFile>
    {
        public bool Equals(WrapperForFile x, WrapperForFile y)
        {
            return x.RelativePath.SequenceEqual(y.RelativePath);
        }

        public int GetHashCode(WrapperForFile obj)
        {
            return obj.RelativePath.GetHashCode();
        }
    }

}
