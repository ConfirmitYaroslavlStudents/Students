using System.IO;

namespace MasterSlaveSync
{
    internal static class WatcherHelpers
    {
        internal static FileInfo GetFileWithTheSameName(string directoryPath, string fileName)
        {
            var directory = new DirectoryInfo(directoryPath);
            var result = directory.GetFiles(fileName);

            return result[0];
        }

        internal static void CreateFile(string directoryPath, string fileName)
        {
            File.Create(Path.Combine(directoryPath, fileName));
        }

        internal static void DeleteFile(string directoryPath, string fileName)
        {
            File.Delete(Path.Combine(directoryPath, fileName));
        }
        internal static void CopyFile(string srcDirectoryPath, string destDirectoryPath, string fileName)
        {
            var srcFile = GetFileWithTheSameName(srcDirectoryPath, fileName);
            File.Copy(srcFile.FullName, Path.Combine(destDirectoryPath, fileName));
        }
    }
}
