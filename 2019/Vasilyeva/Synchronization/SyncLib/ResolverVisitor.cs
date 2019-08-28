using System;
using System.IO;

namespace SyncLib
{
    public class ResolverVisitor : IVisitor
    {
        public void Visit(DifferentContentConflict conflict)
        {
            File.Copy(conflict.SourcePath, conflict.DestinationPath, true);
        }

        public void Visit(ExistDirectoryConflict conflict)
        {
            Directory.Delete(conflict.DirectoryPath, true);
        }

        public void Visit(ExistFileConflict conflict)
        {
            File.Delete(conflict.FilePath);
        }

        public void Visit(NoExistDirectoryConflict conflict)
        {
            Directory.CreateDirectory(conflict.DirectoryPath);
        }

        public void Visit(NoExistFileConflict conflict)
        {
            File.Copy(conflict.SourcePath, conflict.DestinationPath);
        }
    }
}
