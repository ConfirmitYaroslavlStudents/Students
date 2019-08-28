namespace SyncLib
{
    public class DifferentContentConflict : IConflict
    {
        public string SourcePath;
        public string DestinationPath;

        public DifferentContentConflict(string source, string destination)
        {
            SourcePath = source;
            DestinationPath = destination;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}