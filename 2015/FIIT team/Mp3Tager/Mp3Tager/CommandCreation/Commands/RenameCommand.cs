using FileLib;
using System;
using System.IO;
using System.Text;
using TagLib;

namespace CommandCreation
{
    internal class RenameCommand : Command
    {
        private IMp3File _mp3File;
        private readonly string _mask;
        BaseFileExistenceChecker _checker;    

        public RenameCommand(IMp3File audioFile, BaseFileExistenceChecker checker, string mask)
        {      
            _mp3File = audioFile;
            _mask = mask;
            _checker = checker;
        }

        public override void Execute()
        {
            if (_mask == String.Empty)
                throw new ArgumentException(_mask);

            var newName = GetNewName();

            var directory = Path.GetDirectoryName(_mp3File.FullName);
            _mp3File.MoveTo(_checker, Path.Combine(directory, newName + @".mp3"));
        }

        private string GetNewName()
        {
            var newName = new StringBuilder(_mask);

            newName.Replace(TagNames.Artist, _mp3File.Tags.Artist);
            newName.Replace(TagNames.Title, _mp3File.Tags.Title);
            newName.Replace(TagNames.Genre, _mp3File.Tags.Genre);
            newName.Replace(TagNames.Album, _mp3File.Tags.Album);
            newName.Replace(TagNames.Track, _mp3File.Tags.Track.ToString());

            return newName.ToString();
        }

        
    }
}
