using System.IO;

namespace SyncLib
{
    public class NoExistDirectoryConflict : IConflict
    {
        public string path;

        public NoExistDirectoryConflict(string path)
        {
            this.path = path;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}