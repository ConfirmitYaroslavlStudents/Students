using System;
using System.IO;

namespace SyncLib
{
    public class ResolverVisitor : IVisitor
    {
        public void Visit(DifferentContentConflict conflict)
        {
            File.Copy(conflict.source, conflict.target, true);
        }

        public void Visit(ExistDirectoryConflict conflict)
        {
            Directory.Delete(conflict.path, true);
        }

        public void Visit(ExistFileConflict conflict)
        {
            File.Delete(conflict.FilePath);
        }

        public void Visit(NoExistDirectoryConflict conflict)
        {
            Directory.CreateDirectory(conflict.path);
        }

        public void Visit(NoExistFileConflict conflict)
        {
            File.Copy(conflict.source, conflict.target);
        }
    }
}
