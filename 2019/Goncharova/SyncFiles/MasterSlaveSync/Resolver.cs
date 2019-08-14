using MasterSlaveSync.Conflicts;

namespace MasterSlaveSync
{
    internal class Resolver
    {
        private string masterPath;
        private string slavePath;

        internal Resolver(string master, string slave)
        {
            masterPath = master;
            slavePath = slave;
        }

        internal IDeleteFileProcessor DeleteFileProcessor { get; set; } = new DefaultDeleteFileProcessor();
        internal ICopyFileProcessor CopyFileProcessor { get; set; } = new DefaultCopyFileProcessor();
        internal IUpdateFileProcessor UpdateFileProcessor { get; set; } = new DefaultUpdateFileProcessor();
        internal IDeleteDirectoryProcessor DeleteDirectoryProcessor { get; set; } = new DefaultDeleteDirectoryProcessor();
        internal ICopyDirectoryProcessor CopyDirectoryProcessor { get; set; } = new DefaultCopyDirectoryProcessor();

        public void ResolveConflicts(ConflictsCollection conflicts)
        {
            foreach (var fileConflict in conflicts.fileConflicts)
            {
                if (fileConflict.MasterFile == null)
                {
                    ResolveByDeletion(fileConflict);
                }
                else if (fileConflict.SlaveFile == null)
                {
                    ResolveByCopy(fileConflict);
                }
                else
                {
                    ResolveByUpdate(fileConflict);
                }
            }

            foreach (var directoryConflict in conflicts.directoryConflicts)
            {
                if (directoryConflict.MasterDirectory == null)
                {
                    ResolveByDeletion(directoryConflict);
                }
                else
                {
                    ResolveByCopy(directoryConflict);
                }
            }
        }

        private void ResolveByCopy(DirectoryConflict directoryConflict)
        {
            CopyDirectoryProcessor.Execute(directoryConflict.MasterDirectory,
                masterPath, slavePath);
        }

        private void ResolveByDeletion(DirectoryConflict directoryConflict)
        {
            DeleteDirectoryProcessor.Execute(directoryConflict.SlaveDirectory);
        }

        private void ResolveByUpdate(FileConflict fileConflict)
        {
            UpdateFileProcessor.Execute(fileConflict);
        }

        private void ResolveByCopy(FileConflict fileConflict)
        {
            CopyFileProcessor.Execute(fileConflict.MasterFile, masterPath, slavePath);
        }

        private void ResolveByDeletion(FileConflict fileConflict)
        {
            DeleteFileProcessor.Execute(fileConflict.SlaveFile);
        }
    }
}
