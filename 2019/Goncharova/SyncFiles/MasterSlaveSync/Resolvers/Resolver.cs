using MasterSlaveSync.Conflicts;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class Resolver : IResolver
    {
        public IDeleteFileProcessor DeleteFileProcessor { get; set; } = new DefaultDeleteFileProcessor();
        public ICopyFileProcessor CopyFileProcessor { get; set; } = new DefaultCopyFileProcessor();
        public IUpdateFileProcessor UpdateFileProcessor { get; set; } = new DefaultUpdateFileProcessor();
        public IDeleteDirectoryProcessor DeleteDirectoryProcessor { get; set; } = new DefaultDeleteDirectoryProcessor();
        public ICopyDirectoryProcessor CopyDirectoryProcessor { get; set; } = new DefaultCopyDirectoryProcessor();


        public void ResolveConflicts(ConflictsCollection conflicts, string masterPath, string slavePath)
        {
            foreach (var fileConflict in conflicts.FileConflicts)
            {
                if (CanBeResolvedByDeletion(fileConflict))
                {
                    ResolveByDeletion(fileConflict.SlaveFile);
                }
                else if (CanBeResolvedByCopying(fileConflict))
                {
                    ResolveByCopy(fileConflict.MasterFile, masterPath, slavePath);
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
                    ResolveByCopy(directoryConflict.MasterDirectory, masterPath, slavePath);
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

        private void ResolveByCopy(IDirectoryInfo masterDirectory, string masterPath, string slavePath)
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

        private void ResolveByCopy(IFileInfo masterFile, string masterPath, string slavePath)
        {
            CopyFileProcessor.Execute(masterFile, masterPath, slavePath);
        }

        private void ResolveByDeletion(IFileInfo slaveFile)
        {
            DeleteFileProcessor.Execute(slaveFile);
        }
    }
}
