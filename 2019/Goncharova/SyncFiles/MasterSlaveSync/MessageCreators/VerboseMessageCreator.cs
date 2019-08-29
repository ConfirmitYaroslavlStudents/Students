using System.IO;

namespace MasterSlaveSync.Loggers
{
    public class VerboseMessageCreator
    {
        public static string DirectoryCopied(ResolverEventArgs e)
        {
            string directoryPath = Path.GetDirectoryName(e.ElementPath);

            return $"Copied \"{e.ElementPath.Substring(directoryPath.Length + 1)}\" directory " +
                $"to {directoryPath}";
        }

        public static string DirectoryDeleted(ResolverEventArgs e)
        {
            string directoryPath = Path.GetDirectoryName(e.ElementPath);

            return $"Deleted \"{e.ElementPath.Substring(directoryPath.Length + 1)}\" directory " +
                 $"from {directoryPath}";
        }

        public static string FileCopied(ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            return $"Copied \"{fileName}\" file " +
                 $"to {Path.GetDirectoryName(e.ElementPath)}";
        }

        public static string FileDeleted(ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            return $"Deleted \"{fileName}\" file " +
                $"from {Path.GetDirectoryName(e.ElementPath)}";
        }

        public static string FileUpdated(ResolverEventArgs e)
        {
            var fileName = Path.GetFileName(e.ElementPath);

            return $"Updated \"{fileName}\" file";
        }
    }
}