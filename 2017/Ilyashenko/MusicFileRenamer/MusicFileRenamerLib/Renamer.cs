using System.IO;

namespace MusicFileRenamerLib
{
    public class Renamer
    {
        private string _baseDirectory;
        private IFileProcessor _fileProcessor;
        private Arguments _arguments;

        public Renamer(Arguments args, string dir, IFileProcessor fileProcessor)
        {
            _arguments = args;
            _baseDirectory = dir;
            _fileProcessor = fileProcessor;
        }

        public void Rename(Mp3File file)
        {
            switch (_arguments.Action)
            {
                case "MakeFilename":
                    _fileProcessor.MakeFilename(file);
                    break;
                case "MakeTag":
                    _fileProcessor.MakeTags(file);
                    break;
            }
        }

        public string[] GetFilePaths()
        {
            return _arguments.IsRecursive ? Directory.GetFiles(_baseDirectory, _arguments.SearchPattern, SearchOption.AllDirectories) : Directory.GetFiles(_baseDirectory, _arguments.SearchPattern);
        }
    }
}
