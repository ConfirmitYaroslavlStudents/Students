using System.IO;
using System.Text;
namespace Mp3Lib
{    
    public partial class Mp3Manipulations
    {
        public void Rename(string pattern, IFileExistenceChecker checker)
        {
            var newName = new StringBuilder(pattern);

            newName.Replace(TagPatterns.Artist, _mp3File.Mp3Tags.Artist);
            newName.Replace(TagPatterns.Title, _mp3File.Mp3Tags.Title);
            newName.Replace(TagPatterns.Genre, _mp3File.Mp3Tags.Genre);
            newName.Replace(TagPatterns.Album, _mp3File.Mp3Tags.Album);
            newName.Replace(TagPatterns.Track, _mp3File.Mp3Tags.Track.ToString());

            var directory = Path.GetDirectoryName(_mp3File.Path);
            var destinationPath = CreateUniquePath(directory, newName.ToString(), checker);

            _mp3File.MoveTo(destinationPath);  
        }

        private string CreateUniquePath(string directory, string newName, IFileExistenceChecker checker)
        {
            var index = 1;
            var destinationPath = Path.Combine(directory, newName + @".mp3");

            while (checker.CheckIfExists(destinationPath))
            {
                destinationPath = Path.Combine(directory, newName + @" (" + index + ").mp3");
                index++;
            }
      
            return destinationPath;
        }
    }
}
