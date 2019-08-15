using System.IO;

namespace SyncLib
{
    public class InfoLog
    {
        public InfoLog(string action, DirectoryInfo directoryInfo)
        {
            Action = action;
            TypeFile = "Directory";
            FullName = directoryInfo.Name;
            Source = directoryInfo.FullName;
        }

        public InfoLog(string action, FileInfo fileInfo)
        {
            Action = action;
            TypeFile = "File";
            FullName = fileInfo.Name;
            Source = fileInfo.FullName;
        }

        public InfoLog(string action, string fullname, string source)
        {
            Action = action;
            TypeFile = "File";
            FullName = fullname;
            Source = source;
        }

        public string Action;
        public string TypeFile { get; internal set; }
        public string FullName { get; internal set; }
        public string Source { get; internal set; }
    }
}