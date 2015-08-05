using System.IO;
using System.Text;
using Mp3Lib;

namespace CommandCreation
{
    internal class RenameCommand : Command
    {      
        private readonly string _path;
        private readonly string _pattern;

        protected override sealed int[] NumberOfArguments { get; set; }
        public override sealed string CommandName { get; protected set; }

        public RenameCommand(string[] args)
        {
            NumberOfArguments = new[] {3};
            CommandName = CommandNames.Rename;
            CheckIfCanBeExecuted(args);
            _path = args[1];
            _pattern = args[2];
        }

        public override void Execute()
        {
            var audioFile = new Mp3File(_path);
            var newName = GetNewName(audioFile);
            RenameFile(audioFile, newName);
        }

        private string GetNewName(IMp3File audioFile)
        {
            var newName = new StringBuilder(_pattern);
            newName.Replace(TagNames.Artist, audioFile.Tag.Performers[0]);
            newName.Replace(TagNames.Title, audioFile.Tag.Title);
            newName.Replace(TagNames.Genre, audioFile.Tag.Genres[0]);
            newName.Replace(TagNames.Album, audioFile.Tag.Album);
            newName.Replace(TagNames.Track, audioFile.Tag.Track.ToString());
            return newName.ToString();
        }

        private void RenameFile(IMp3File audioFile, string newName)
        {
            var destinationPath = CreateUniquePath(audioFile.DirectoryName, newName);
            var temp = new FileInfo(audioFile.Path);
            temp.MoveTo(destinationPath);
        }

        private string CreateUniquePath(string directory, string fileNameWithoutExtension)
        {
            var index = 1;
            var destinationPath = directory + @"\" + fileNameWithoutExtension + ".mp3";
            
            while (File.Exists(destinationPath))
            {
                destinationPath = string.Format(@"{0}\{1} ({2}).mp3", directory, fileNameWithoutExtension, index);
                index++;
            }

            return destinationPath;
        }
    }
}
