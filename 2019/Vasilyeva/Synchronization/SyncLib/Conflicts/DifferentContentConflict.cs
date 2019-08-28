using System.IO;

namespace SyncLib
{
    public class DifferentContentConflict : IConflict
    {
        public string source;
        public string target;

        public DifferentContentConflict(string source, string target)
        {
            this.source = source;
            this.target = target;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}