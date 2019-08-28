using System.IO;

namespace SyncLib
{
    public  class ExistFileConflict : IConflict
    {
        public string FilePath;

        public ExistFileConflict(string path)
        {
            this.FilePath = path;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}