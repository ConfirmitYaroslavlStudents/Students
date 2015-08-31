using System;
using System.IO;
using System.Text;
using FileLib;

namespace CommandCreation
{
    public class RenameCommand : Command
    {
        internal IMp3File File;
        internal string OldFullName;
        private readonly string _mask;

        public RenameCommand(IMp3File mp3File, string mask)
        {
            File = mp3File;
            _mask = mask;
            OldFullName = mp3File.FullName;
        }

        public override void Execute()
        {
            if (_mask == String.Empty)
                throw new ArgumentException(_mask);

            var newName = GetNewName();
            var directory = Path.GetDirectoryName(File.FullName);        
            var newFullName = Path.Combine(directory, newName + @".mp3");
            File.FullName = newFullName;
        }

        public override void Undo()
        {
            File.FullName = OldFullName;
        }

        private string GetNewName()
        {
            var newName = new StringBuilder(_mask);
            // todo: generate exceptions for empty tags
            if (_mask.Contains(TagNames.Artist) && !string.IsNullOrEmpty(File.Tags.Artist))
                newName.Replace(TagNames.Artist, File.Tags.Artist);
            if (_mask.Contains(TagNames.Title) && !string.IsNullOrEmpty(File.Tags.Title))
                newName.Replace(TagNames.Title, File.Tags.Title);
            if (_mask.Contains(TagNames.Genre) && !string.IsNullOrEmpty(File.Tags.Genre))
                newName.Replace(TagNames.Genre, File.Tags.Genre);
            if (_mask.Contains(TagNames.Album) && !string.IsNullOrEmpty(File.Tags.Album))
                newName.Replace(TagNames.Album, File.Tags.Album);
            if (_mask.Contains(TagNames.Track) && !string.IsNullOrEmpty(File.Tags.Track.ToString()))
                newName.Replace(TagNames.Track, File.Tags.Track.ToString());

            return newName.ToString();
        }

        public override T Accept<T>(ICommandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override bool IsPlanningCommand()
        {
            return true;
        }
    }
}
