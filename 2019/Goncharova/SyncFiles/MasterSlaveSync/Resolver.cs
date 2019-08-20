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
                if (CanBeResolvedByDeletion(fileConflict))
                {
                    ResolveByDeletion(fileConflict.SlaveFile);
                }
                else if (CanBeResolvedByCopying(fileConflict))
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
                if (CanBeResolvedByDeletion(directoryConflict))
                {
                    ResolveByDeletion(directoryConflict.SlaveDirectory);
                }
                else
                {
                    ResolveByCopy(directoryConflict.MasterDirectory);
                }
            }
        }

        private static bool CanBeResolvedByDeletion(DirectoryConflict directoryConflict)
        {
            return directoryConflict.MasterDirectory == null;
        }

        private static bool CanBeResolvedByCopying(FileConflict fileConflict)
        {
            return fileConflict.SlaveFile == null;
        }

        private static bool CanBeResolvedByDeletion(FileConflict fileConflict)
        {
            return fileConflict.MasterFile == null;
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
