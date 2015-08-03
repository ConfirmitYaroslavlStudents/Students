using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Id3;

namespace Mp3Handler
{
    class Id3LibFileHandler:IFileHandler, IDisposable
    {
        public Id3LibFileHandler(string path)
        {
            if(!File.Exists(path))
                throw new FileNotFoundException();
            if(Path.GetExtension(path)!=".mp3")
                throw new ArgumentException("Not mp3 file");
            FilePath = path;
            FileName = Path.GetFileNameWithoutExtension(path);
            _mp3File = new Mp3File(FilePath, Mp3Permissions.ReadWrite);
        }

        public string FilePath { get; private set; }

        public string FileName { get; private set; }

        public void Dispose()
        {
            _mp3File.Dispose();
        }

        public Dictionary<FrameType, string> GetTags()
        {
            var idTag = _mp3File.GetTag(Id3TagFamily.FileStartTag);

            var tagsDictionary = new Dictionary<FrameType, string>
            {
                {FrameType.Title, idTag.Title},
                {FrameType.Album, idTag.Album},
                {FrameType.Artist, idTag.Artists},
                {FrameType.Track, idTag.Track},
                {FrameType.Year, idTag.Year}
            };

            return (from tag in tagsDictionary where tag.Value != "" select tag).ToDictionary(k => k.Key, v => v.Value);
        }

        public void SetTags(Dictionary<FrameType, string> tags)
        {
            var fileTags = _mp3File.GetTag(Id3TagFamily.FileStartTag);

            foreach (var tag in tags)
            {
                switch (tag.Key)
                {
                        //I dont know how to kill this
                    case FrameType.Artist:
                        fileTags.Artists.Value = tags[FrameType.Artist];
                        break;
                    case FrameType.Title:
                        fileTags.Title.Value = tags[FrameType.Title];
                        break;
                    case FrameType.Album:
                        fileTags.Album.Value = tags[FrameType.Album];
                        break;
                    case FrameType.Track:
                        fileTags.Track.Value = tags[FrameType.Track];
                        break;
                    case FrameType.Year:
                        fileTags.Year.Value = tags[FrameType.Year];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _mp3File.WriteTag(fileTags, WriteConflictAction.Replace);
        }

        public void Rename(string newName)
        {
            _mp3File.Dispose();
            var newFilePath = FilePath.Replace(FileName + ".mp3", newName + ".mp3");
            if(File.Exists(newFilePath))
                throw new Exception("File exist");
            File.Move(FilePath,newFilePath);
            FilePath = newFilePath;
            FileName = newName;
            _mp3File = new Mp3File(FilePath,Mp3Permissions.ReadWrite);
        }

        private Mp3File _mp3File;
    }
}
