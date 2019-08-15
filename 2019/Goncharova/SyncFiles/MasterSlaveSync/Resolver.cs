using MasterSlaveSync.Conflicts;
using System.IO;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class Resolver
    {
        private readonly string masterPath;
        private readonly string slavePath;

        public Resolver(string master, string slave)
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
            foreach (var fileConflict in conflicts.FileConflicts)
            {
                if (fileConflict.MasterFile == null)
                {
                    ResolveByDeletion(fileConflict.SlaveFile);
                }
                else if (fileConflict.SlaveFile == null)
                {
                    ResolveByCopy(fileConflict.MasterFile);
                }
                else
                {
                    ResolveByUpdate(fileConflict);
                }
            }

            foreach (var directoryConflict in conflicts.DirectoryConflicts)
            {
                if (directoryConflict.MasterDirectory == null)
                {
                    ResolveByDeletion(directoryConflict.SlaveDirectory);
                }
                else
                {
                    ResolveByCopy(directoryConflict.MasterDirectory);
                }
            }
        }

        private void ResolveByCopy(IDirectoryInfo masterDirectory)
        {
            string directoryPath = masterDirectory.FullName.Substring(masterPath.Length);

            IDirectoryInfo target = masterDirectory.FileSystem
                .Directory.CreateDirectory(slavePath + "//" + directoryPath);

            CopyDirectoryProcessor.Execute(masterDirectory, target);
        }

        private void ResolveByDeletion(IDirectoryInfo slaveDirectory)
        {
            DeleteDirectoryProcessor.Execute(slaveDirectory);
        }

        private void ResolveByUpdate(FileConflict fileConflict)
        {
            UpdateFileProcessor.Execute(fileConflict);
        }

        private void ResolveByCopy(IFileInfo masterFile)
        {
            CopyFileProcessor.Execute(masterFile, masterPath, slavePath);
        }

        private void ResolveByDeletion(IFileInfo slaveFile)
        {
            DeleteFileProcessor.Execute(slaveFile);
        }
    }
}
