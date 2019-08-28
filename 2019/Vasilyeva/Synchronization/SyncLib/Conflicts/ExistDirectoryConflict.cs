using System.IO;

namespace SyncLib
{
    public class ExistDirectoryConflict : IConflict
    {
        public string path;

        public ExistDirectoryConflict(string path)
        {
            this.path = path;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}