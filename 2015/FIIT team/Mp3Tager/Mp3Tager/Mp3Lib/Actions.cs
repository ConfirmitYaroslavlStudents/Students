using System;
using System.Collections.Generic;
using System.Text;

namespace Mp3Lib
{
    public static class Actions
    {
        private static readonly HashSet<string> TagSet = new HashSet<string>
        {
            "{artist}", "{title}", "{genre}", "{album}", "{track}"
        };

        public static void Rename(IMp3File audioFile, string pattern)
        {
            var newName = new StringBuilder(pattern);
            newName.Replace("{artist}", audioFile.Tag.Performers[0]);
            newName.Replace("{title}", audioFile.Tag.Title);
            newName.Replace("{genre}", audioFile.Tag.Genres[0]);
            newName.Replace("{album}", audioFile.Tag.Album);
            newName.Replace("{track}", audioFile.Tag.Track.ToString());

            audioFile.MoveTo(audioFile.DirectoryName + @"\" + newName + ".mp3");
        }

        public static void ChangeTag(IMp3File audioFile, string tag, string newTagValue)
        {
            if (!TagSet.Contains(tag))
                throw new ArgumentException("There is no such tag.");

            switch (tag)
            {
                case "{artist}":
                    audioFile.Tag.Performers = new[] { newTagValue };
                    break;
                case "{title}":
                    audioFile.Tag.Title = newTagValue;
                    break;
                case "{genre}":
                    audioFile.Tag.Genres = new[] { newTagValue };
                    break;
                case "{album}":
                    audioFile.Tag.Album = newTagValue;
                    break;
                case "{track}":
                    audioFile.Tag.Track = Convert.ToUInt32(newTagValue);
                    break;
            }
            audioFile.Save();
        }
    }
}
