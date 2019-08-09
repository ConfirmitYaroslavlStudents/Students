using MasterSlaveSync.Conflict;
using System.Collections.Generic;

namespace MasterSlaveSync
{
    public class SyncProcessor
    {
        private readonly string masterPath;
        private readonly string slavePath;

        private IDeleteFileProcessor deleteFileProcessor = new DefaultDeleteFileProcessor();
        private ICreateFileProcessor createFileProcessor = new DefaultCreateFileProcessor();
        private IUpdateFileProcessor updateFileProcessor = new DefaultUpdateFileProcessor();

        private IDeleteDirectoryProcessor deleteDirectoryProcessor = new DefaultDeleteDirectoryProcessor();
        private ICreateDirectoryProcessor createDirectoryProcessor = new DefaultCreateDirectoryProcessor();

        public SyncProcessor(SyncOptions syncOptions)
        {
            if(syncOptions.NoDelete == true)
            {
                deleteFileProcessor = new NoDeleteFileProcessor();
                deleteDirectoryProcessor = new NoDeleteDirectoryProcessor();
            }

        }

        public void Synchronize(List<IConflict> conflicts)
        {
            foreach(var conflict in conflicts)
            {
                if(conflict.GetType() == typeof(FileConflict))
                {
                    FileConflict fileConflict = (FileConflict)conflict;

                    if(fileConflict.MasterFile == null)
                    {
                        deleteFileProcessor.Execute(fileConflict.SlaveFile);
                    }
                    else if (fileConflict.SlaveFile == null)
                    {
                        createFileProcessor.Execute(fileConflict.MasterFile, masterPath, slavePath);
                    }
                    else
                    {
                        updateFileProcessor.Execute(fileConflict);
                    }
                }
                if (conflict.GetType() == typeof(DirectoryConflict))
                {
                    DirectoryConflict directoryConflict = (DirectoryConflict)conflict;

                    if (directoryConflict.MasterDirectory == null)
                    {
                        deleteDirectoryProcessor.Execute(directoryConflict.SlaveDirectory);
                    }
                    else
                    {
                        createDirectoryProcessor.Execute(directoryConflict.MasterDirectory, masterPath, slavePath);
                    }
                }
            }
        }
    }
}
