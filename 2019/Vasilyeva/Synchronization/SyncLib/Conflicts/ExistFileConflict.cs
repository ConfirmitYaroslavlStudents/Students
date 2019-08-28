namespace SyncLib
{
    public  class ExistFileConflict : IConflict
    {
        public string FilePath;

        public ExistFileConflict(string path)
        {
            FilePath = path;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}