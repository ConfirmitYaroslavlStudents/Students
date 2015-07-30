using System.IO;
using System.Text;
using Mp3Lib;

namespace CommandCreation
{
    internal class RenameCommand : Command
    {
        private const string Artist = "{artist}";
        private const string Title = "{title}";
        private const string Genre = "{genre}";
        private const string Album = "{album}";
        private const string Track = "{track}";

        private new static readonly int[] NumberOfArguments = { 3 };
        private new static readonly string CommandName = CommandNames.Help;

        private readonly string _path;
        private readonly string _pattern;
        

        public RenameCommand(string[] args)
        {
            CheckIfCanBeExecuted(args);
            _path = args[1];
            _pattern = args[2];
        }

        public override void Execute()
        {
            RenameFile(new Mp3File(_path));
        }

        internal void RenameFile(IMp3File audioFile)
        {
            var newName = new StringBuilder(_pattern);
            newName.Replace(Artist, audioFile.Tag.Performers[0]);
            newName.Replace(Title, audioFile.Tag.Title);
            newName.Replace(Genre, audioFile.Tag.Genres[0]);
            newName.Replace(Album, audioFile.Tag.Album);
            newName.Replace(Track, audioFile.Tag.Track.ToString());

            MoveTo(audioFile.DirectoryName + @"\" + newName + ".mp3", audioFile);
        }

        private void MoveTo(string newPath, IMp3File audioFile)
        {
            var temp = new FileInfo(audioFile.Path);
            temp.MoveTo(newPath);
        }
    }
}
