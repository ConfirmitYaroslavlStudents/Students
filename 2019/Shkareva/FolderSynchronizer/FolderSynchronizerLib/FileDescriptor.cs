namespace FolderSynchronizerLib
{
    public class FileDescriptor
    {
        public string Path;
        public int Hash;

        public FileDescriptor(string filePath, int hash)
        {
            Path = filePath;
            Hash = hash;
        }
    }
}
