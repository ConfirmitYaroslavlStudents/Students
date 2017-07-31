using System.IO;

namespace MusicFileRenamerLib
{
    public class Renamer
    {
        private string _baseDirectory;
        private IFilenameMaker _filenameMaker;
        private ITagMaker _tagMaker;
        private Arguments arguments;

        public Renamer(string[] args, string dir, IFilenameMaker filenameMaker, ITagMaker tagMaker)
        {
            arguments = (new ArgumentParser()).Parse(args);
            _baseDirectory = dir;
            _filenameMaker = filenameMaker;
            _tagMaker = tagMaker;
        }

        public void Rename(Mp3File file)
        {
            switch (arguments.Action)
            {
                case "MakeFilename":
                    _filenameMaker.MakeFilename(file);
                    break;
                case "MakeTag":
                    _tagMaker.MakeTags(file);
                    break;
            }
        }

        public string[] GetFilePaths()
        {
            return arguments.IsRecursive ? Directory.GetFiles(_baseDirectory, arguments.SearchPattern, SearchOption.AllDirectories) : Directory.GetFiles(_baseDirectory, arguments.SearchPattern);
        }
    }
}
