using System.Collections.Generic;

namespace MasterSlaveSync.Conflicts
{
    public class ConflictsCollection
    {
        public ConflictsCollection() { }
        public ConflictsCollection(List<FileConflict> fileConflicts)
        {
            FileConflicts = fileConflicts;
        }
        public ConflictsCollection(List<DirectoryConflict> directoryConflicts)
        {
            DirectoryConflicts = directoryConflicts;
        }

        internal ConflictsCollection Concat(ConflictsCollection second)
        {
            FileConflicts.AddRange(second.FileConflicts);
            DirectoryConflicts.AddRange(second.DirectoryConflicts);

            return this;
        }
        public List<FileConflict> FileConflicts { get; } = new List<FileConflict>();
        public List<DirectoryConflict> DirectoryConflicts { get; } = new List<DirectoryConflict>();
    }
}
