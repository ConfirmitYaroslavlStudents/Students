namespace FolderSynchronizerLib
{
    public class Item
    {
        public string Path;
        public int Hash;

        public Item(string filePath, int hash)
        {
            Path = filePath;
            Hash = hash;
        }
    }
}
