using System.Collections.Generic;

namespace MasterSlaveSync.Conflicts
{
    public class ConflictsCollection
    {
        public List<FileConflict> fileConflicts { get; } = new List<FileConflict>();
        public List<DirectoryConflict> directoryConflicts { get; } = new List<DirectoryConflict>();

        public ConflictsCollection Concat(ConflictsCollection second)
        {
            fileConflicts.AddRange(second.fileConflicts);
            directoryConflicts.AddRange(second.directoryConflicts);

            return this;
        }
    }
}
