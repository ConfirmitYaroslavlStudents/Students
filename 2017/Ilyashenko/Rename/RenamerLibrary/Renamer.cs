using System.IO;

namespace RenamerLibrary
{
    public class Renamer
    {
        private string _baseDirectory;

        public Renamer(string dir)
        {
            _baseDirectory = dir;
        }

        public void Rename(string[] args)
        {
            var arguments = (new ArgumentParser()).Parse(args);

            switch (arguments.Action)
            {
                case "MakeFilename":
                    MakeFileNames(GetFilePaths(_baseDirectory, arguments.SearchPattern, arguments.IsRecursive));
                    break;
                case "MakeTags":
                    MakeFileTags(GetFilePaths(_baseDirectory, arguments.SearchPattern, arguments.IsRecursive));
                    break;
            }
        }

        public string[] GetFilePaths(string currentDirectory, string pattern, bool recursive)
        {
            return recursive ? Directory.GetFiles(currentDirectory, pattern, SearchOption.AllDirectories) : Directory.GetFiles(currentDirectory, pattern);
        }

        public void MakeFileNames(string[] filePaths)
        {
            foreach (var fPath in filePaths)
            {
                (new Mp3File(fPath)).MakeFilename();
            }
        }

        public void MakeFileTags(string[] filePaths)
        {
            foreach (var fPath in filePaths)
            {
                (new Mp3File(fPath)).MakeTags();
            }
        }
    }
}
