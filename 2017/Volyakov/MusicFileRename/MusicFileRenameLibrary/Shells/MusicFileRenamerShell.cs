namespace MusicFileRenameLibrary
{
    public class MusicFileRenamerShell
    {
        private ArgsShell _args;
        private FileWorker _fileWorker;
        private ShellMaker _shellMaker;
        private MusicFileRenamer _fileRenamer;

        public MusicFileRenamerShell(string[] args)
        {
            _shellMaker = new ShellMaker();
            _args = _shellMaker.MakeArgsShell(args);
            _fileWorker = new FileWorker(_args);
            _fileRenamer = new MusicFileRenamer(_args.Renamer);
        }

        public void RenameMusicFiles()
        {
            var filesForRename = _fileWorker.GetFiles();
            foreach(var file in filesForRename)
            {
                var artistTag = _fileWorker.GetFileArtistTag(file);
                var titleTag = _fileWorker.GetFileTitleTag(file);
                var renamedFile = _fileRenamer.RenameMusicFile(file, artistTag, titleTag);
                _fileWorker.SaveFile(file, renamedFile);
            }
        }
    }
}
