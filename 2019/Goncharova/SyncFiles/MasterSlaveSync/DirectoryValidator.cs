using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public static class DirectoryValidator
    {
        public static bool MasterExists(string masterPath, IFileSystem fileSystem)
        {
            if (!fileSystem.Directory.Exists(masterPath))
            {
                return false;
            }

            return true;
        }

        public static bool DirectoriesDoNotContainEachOther(string masterPath, string slavePath, IFileSystem fileSystem)
        {
            string masterAbsolutePath = fileSystem.Path.GetFullPath(masterPath);
            string slaveAbsolutePath = fileSystem.Path.GetFullPath(slavePath);

            if (slavePath.StartsWith(masterAbsolutePath) || masterPath.StartsWith(slaveAbsolutePath))
            {
                return false;
            }

            return true;
        }
    }
}
