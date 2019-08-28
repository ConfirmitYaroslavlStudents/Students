namespace SyncLib
{
    public class NoExistDirectoryConflict : IConflict
    {
        public string DirectoryPath;

        public NoExistDirectoryConflict(string path)
        {
            DirectoryPath = path;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}