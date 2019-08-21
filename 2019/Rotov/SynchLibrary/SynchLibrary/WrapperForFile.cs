using System.IO;

namespace GeneralizeSynchLibrary
{
    public class FileWrapper
    {
        public string RelativePath;

        public FileWrapper(FileInfo file , string root)
        {
            SetRelativePath(file , root);
        }

        public FileWrapper(string relativaPath)
        {
            RelativePath = relativaPath;
        }

        public void SetRelativePath(FileInfo file , string root)
        {
            var current = file.FullName.Replace(root , "");
            if(current[0] == '\\')
                current = current.TrimStart('\\');
            RelativePath = current;
        }

        public override bool Equals(object file)
        {
            FileWrapper obj = (FileWrapper) file;
            return RelativePath.Equals(obj.RelativePath);
        }

        public override int GetHashCode()
        {
            return RelativePath.GetHashCode();
        }
    }
}
