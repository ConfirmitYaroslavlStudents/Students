using System;
using System.Collections.Generic;
using System.Text;

namespace Mp3Lib
{
    public  class Actions
    {
        private const string artist = "{artist}";
        private const string title = "{title}";
        private const string genre = "{genre}";
        private const string album = "{album}";
        private const string track = "{track}";

        private  readonly HashSet<string> TagSet = new HashSet<string>
        {
            artist, album, genre, title, track
        };

        public void Rename(IMp3File audioFile, string pattern)
        {            
            var newName = new StringBuilder(pattern);
            newName.Replace(artist, audioFile.Tag.Performers[0]);
            newName.Replace(title, audioFile.Tag.Title);
            newName.Replace(genre, audioFile.Tag.Genres[0]);
            newName.Replace(album, audioFile.Tag.Album);
            newName.Replace(track, audioFile.Tag.Track.ToString());

            audioFile.MoveTo(audioFile.DirectoryName + @"\" + newName + ".mp3");
        }

        public  void ChangeTag(IMp3File audioFile, string tag, string newTagValue)
        {
            if (!TagSet.Contains(tag))
                throw new ArgumentException("There is no such tag.");

            switch (tag)
            {
                case artist:
                    audioFile.Tag.Performers = new[] { newTagValue };
                    break;
                case title:
                    audioFile.Tag.Title = newTagValue;
                    break;
                case genre:
                    audioFile.Tag.Genres = new[] { newTagValue };
                    break;
                case album:
                    audioFile.Tag.Album = newTagValue;
                    break;
                case track:
                    audioFile.Tag.Track = Convert.ToUInt32(newTagValue);
                    break;
            }
            audioFile.Save();
        }
    }
}
