namespace FolderSynchronizerLib
{
    class SilentLog : ILog
    {
        public string FormLogToPrint()
        {
            return "Folders are synchronized.";
        }

        public void GetInfoAboutAddFiles(string sourcePath, string destinationPath) { }

        public void GetInfoAboutDeleteFiles(string sourcePath) { }

        public void GetInfoAboutUpdateFiles(string sourcePath, string destinationPath) { }
    }
}
