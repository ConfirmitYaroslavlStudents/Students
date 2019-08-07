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
            var current = file.FullName.Replace(root, "");
            if (current[0] == '\\')
                current = current.TrimStart('\\');
            RelativePath = current;
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
