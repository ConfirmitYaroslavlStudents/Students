using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Id3;

namespace Mp3Handler
{
    public class Id3LibFileHandler:IFileHandler
    {
        public Id3LibFileHandler(string path)
        {
            FilePath = path;
        }

        public Id3LibFileHandler()
        {
            
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if(_mp3File!=null)
                    _mp3File.Dispose();
                if (!File.Exists(value))
                    throw new FileNotFoundException();
                if (Path.GetExtension(value) != ".mp3")
                    throw new ArgumentException("Not mp3 file");
                _mp3File = new Mp3File(value, Mp3Permissions.ReadWrite);
                _filePath = value;
                FileName = value;
            }
        }

        public string FileName {
            get { return _fileName; }
            private set { _fileName = Path.GetFileNameWithoutExtension(value); }
        }

        public void Dispose()
        {
            _mp3File.Dispose();
        }

        public Dictionary<FrameType, string> GetTags(GetTagsOption option)
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

            if (option == GetTagsOption.RemoveEmptyTags)
                return (from tag in tagsDictionary where tag.Value != "" select tag).ToDictionary(k => k.Key,
                    v => v.Value);
            else
                return tagsDictionary;
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

        private string _filePath;
        private Mp3File _mp3File;
        private string _fileName;
    }
}
