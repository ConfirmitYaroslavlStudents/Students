using System;

namespace MusicFileRenameLibrary
{
    public class MusicFileRenamer
    {
        private IRenamer _renamer;
        private Parser _parser;
        private ShellMaker _shellMaker;
        private string[] _knownExtensions;

        public MusicFileRenamer(IRenamer renamer)
        {
            _renamer = renamer;
            _parser = new Parser();
            _shellMaker = new ShellMaker();
            _knownExtensions = new string[] { ".mp3", ".mp4" };
        }

        public FileShell RenameMusicFile(string filePath, string fileArtistTag, string fileTitleTag)
        {
            var parsedFile = _parser.ParseFile(filePath, fileArtistTag, fileTitleTag);

            var fileShell = _shellMaker.MakeFileShell(parsedFile);

            if (Array.Exists(_knownExtensions, (x => x == fileShell.Extension)))
                _renamer.Rename(fileShell);

            _parser.CollectFilePath(fileShell);

            return fileShell;
        }
    }
}
