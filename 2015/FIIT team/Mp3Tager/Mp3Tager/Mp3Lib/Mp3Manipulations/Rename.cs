using System;
using System.IO;
using System.Text;
namespace Mp3Lib
{    
    public partial class Mp3Manipulations
    {
        public void Rename(string pattern)
        {
            if (pattern == String.Empty)
                throw new ArgumentException(pattern);

            var newName = new StringBuilder(pattern);

            newName.Replace(TagPatterns.Artist, _mp3File.Mp3Tags.Artist);
            newName.Replace(TagPatterns.Title, _mp3File.Mp3Tags.Title);
            newName.Replace(TagPatterns.Genre, _mp3File.Mp3Tags.Genre);
            newName.Replace(TagPatterns.Album, _mp3File.Mp3Tags.Album);
            newName.Replace(TagPatterns.Track, _mp3File.Mp3Tags.Track.ToString());

            var directory = Path.GetDirectoryName(_mp3File.Path);
            _mp3File.MoveTo(Path.Combine(directory, newName + @".mp3"));  
        }

        

        
    }
}
