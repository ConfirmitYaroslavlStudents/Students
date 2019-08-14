using System.IO;
namespace DirectorySynchronizationApp
{
    public class Synch
    {
        private DirectoryInfo _masterDirectory;
        private DirectoryInfo _slaveDirectory;
        private Enums.Flags _flags;
        public Logger log;

        public Synch(DirectoryInfo masterDirectory, DirectoryInfo slaveDirectory, Enums.Flags flags)
        {
            _masterDirectory = masterDirectory;
            _slaveDirectory = slaveDirectory;
            _flags = flags;
            log = new Logger();
        }

        public void Synchronization()
        {
            if (_flags.HasFlag(Enums.Flags.NoDelete))
            {
                NoDeleteSynchronization(_masterDirectory, _slaveDirectory);
                NoDeleteSynchronization(_slaveDirectory, _masterDirectory);
            }
            else
            {
                DeleteSynchronization(_masterDirectory, _slaveDirectory);
                NoDeleteSynchronization(_slaveDirectory, _masterDirectory);
            }
        }

        private void NoDeleteSynchronization(DirectoryInfo masterDirectory, DirectoryInfo slaveDirectory)
        {
            foreach (var file in masterDirectory.GetFiles())
            {
                if (Contains(file, slaveDirectory) == -1)
                {
                    file.CopyTo(Path.Combine(slaveDirectory.FullName, file.Name));
                    log.IncreaseCopiedFilesCount();
                    string tmp = "copy <" + file.Name + "> -> " + slaveDirectory.FullName;
                    log.AddToListOfChanges(tmp);
                }
            }

            foreach (var subDirectory in masterDirectory.GetDirectories())
            {
                if (!Contains(subDirectory, slaveDirectory))
                    CopyDirectory(subDirectory, slaveDirectory.CreateSubdirectory(subDirectory.Name));
                else
                {
                    var tmpDirectory = new DirectoryInfo(Path.Combine(slaveDirectory.FullName, subDirectory.Name));
                    NoDeleteSynchronization(subDirectory, tmpDirectory);
                }
            }
        }
        private int Contains(FileInfo file, DirectoryInfo directory)
        {
            foreach (var i in directory.GetFiles())
            {
                if (i.Length == file.Length)
                    return 1;
                if (i.Name == file.Name)
                    return 0;
            }
            return -1;
        }

        private bool Contains(DirectoryInfo subDirectory, DirectoryInfo directory)
        {
            foreach (var i in directory.GetDirectories())
            {
                if (i.Name == subDirectory.Name)
                    return true;
            }
            return false;
        }

        private void DeleteSynchronization(DirectoryInfo masterDirectory, DirectoryInfo slaveDirectory)
        {
            foreach (var file in masterDirectory.GetFiles())
            {
                if (Contains(file, slaveDirectory) == -1)
                {
                    file.CopyTo(Path.Combine(slaveDirectory.FullName, file.Name));
                    log.IncreaseCopiedFilesCount();
                    string tmp = "copy <" + file.Name + "> -> " + slaveDirectory.FullName;
                    log.AddToListOfChanges(tmp);
                }
                else if (Contains(file, slaveDirectory) == 0)
                {
                    file.CopyTo(Path.Combine(slaveDirectory.FullName, file.Name), true);
                    log.IncreaseUpdatedFilesCount();
                    string tmp = "update <" + file.Name + "> -> " + slaveDirectory.FullName;
                    log.AddToListOfChanges(tmp);
                }
            }

            foreach (var msterSubDirectory in masterDirectory.GetDirectories())
            {
                if (!Contains(msterSubDirectory, slaveDirectory))
                    CopyDirectory(msterSubDirectory, slaveDirectory.CreateSubdirectory(msterSubDirectory.Name));
                else
                {
                    var tmpDirectory = new DirectoryInfo(Path.Combine(slaveDirectory.FullName, msterSubDirectory.Name));
                    DeleteSynchronization(msterSubDirectory, tmpDirectory);
                }
            }
        }

        private void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.ToString(), file.Name), true);
                log.IncreaseCopiedFilesCount();
                string tmp = "copy <" + file.Name + "> -> " + source.FullName;
                log.AddToListOfChanges(tmp);
            }
            foreach (DirectoryInfo subDirectory in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(subDirectory.Name);
                CopyDirectory(subDirectory, nextTargetSubDir);
            }
        }
    }
}
