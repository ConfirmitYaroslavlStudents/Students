namespace SyncLib
{
    public class ExistDirectoryConflict : IConflict
    {
        public string DirectoryPath;

        public ExistDirectoryConflict(string path)
        {
            DirectoryPath = path;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}