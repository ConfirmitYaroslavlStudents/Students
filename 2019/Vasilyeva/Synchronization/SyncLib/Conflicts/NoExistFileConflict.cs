namespace SyncLib
{
    public class NoExistFileConflict : IConflict
    {
        public string SourcePath;
        public string DestinationPath;

        public NoExistFileConflict(string source, string dectination)
        {
            SourcePath = source;
            DestinationPath = dectination;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}