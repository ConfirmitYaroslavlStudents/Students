using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SynchLibrary
{
    public class FileWrapper
    {
        public string RelativePath;

        public FileWrapper(FileInfo file, string root)
        {
            SetRelativePath(file, root);
        }

        public void SetRelativePath(FileInfo file, string root)
        {
            RelativePath = file.FullName.Replace(root, "");
        }

        public override bool Equals(object file)
        {
            FileWrapper obj = (FileWrapper)file;
            return RelativePath.Equals(obj.RelativePath);
        }

        public override int GetHashCode()
        {
            return RelativePath.GetHashCode();
        }
    }
}
