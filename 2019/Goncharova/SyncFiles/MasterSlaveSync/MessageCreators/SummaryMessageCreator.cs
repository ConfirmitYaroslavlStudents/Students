using System.IO;

namespace MasterSlaveSync.Loggers
{
    public class SummaryMessageCreator
    {
        public static string DirectoryDeleted(ResolverEventArgs e)
        {
            var directoryPath = Path.GetDirectoryName(e.ElementPath);

            return $"Deleted \"{e.ElementPath.Substring(directoryPath.Length + 1)}\" directory";
        }

        public static string DirectoryCopied(ResolverEventArgs e)
        {
            var directoryPath = Path.GetDirectoryName(e.ElementPath);

            return $"Copied \"{e.ElementPath.Substring(directoryPath.Length + 1)}\" directory";
        }

        public static string FileDeleted(ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            return $"Deleted \"{fileName}\" file";
        }
        public static string FileCopied(ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            return $"Copied \"{fileName}\" file";
        }

        public static string FileUpdated(ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            return $"Updated \"{fileName}\" file";
        }
    }
}