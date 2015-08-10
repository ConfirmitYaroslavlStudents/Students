using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Lib
{
    public partial class Mp3Manipulations
    {
        public void Analyse(string mask)
        {
            throw new NotImplementedException();
        }

        private string GetTagValueByTagPattern(string tagPattern)
        {
            switch (tagPattern)
            {
                case TagPatterns.Artist:
                    return _mp3File.Mp3Tags.Artist;
                case TagPatterns.Title:
                    return _mp3File.Mp3Tags.Title;
                case TagPatterns.Genre:
                    return _mp3File.Mp3Tags.Genre;
                case TagPatterns.Album:
                    return _mp3File.Mp3Tags.Album;
                case TagPatterns.Track:
                    return _mp3File.Mp3Tags.Track.ToString();
                default:
                    throw new ArgumentException(tagPattern);
            }
        }
    }
}
