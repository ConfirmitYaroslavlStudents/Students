using System.Collections.Generic;
using System.Linq;
using FolderLib;
using Mp3Handler;

namespace Tests
{
    class TestFileSystem:IFileHandler,IFolderHandler
    {
        public TestFileSystem()
        {
            _files = new Dictionary<string, TestFile>();
            _actualFile = new TestFile();
        }

        public string FilePath { get { return _actualFile.FilePath; } set { _actualFile = _files[value]; } }
        public string FileName { get { return _actualFile.FileName; } }
        public Dictionary<FrameType,string> Tags { get { return _actualFile.Tags; } }

        public TestFile ActualFile
        {
            get { return _actualFile; }
            set { _actualFile = value; }
        }
        public Dictionary<string, TestFile> Files
        {
            get { return _files; }
            set { _files = value; }
        }

        public Dictionary<FrameType, string> GetTags(GetTagsOption option)
        {
            if (option == GetTagsOption.RemoveEmptyTags)
                return _actualFile.Tags;
            else
            {
                var emptyDictionary = Frame.EnumKeyDictionary;
                foreach (var tag in _actualFile.Tags)
                {
                    emptyDictionary[tag.Key] = tag.Value;
                }
                return emptyDictionary;
            }
        }

        public void SetTags(Dictionary<FrameType, string> tags)
        {
            foreach (var tag in tags)
            {
                if (_actualFile.Tags.ContainsKey(tag.Key))
                {
                    _actualFile.Tags[tag.Key] = tag.Value;
                }
                else
                    _actualFile.Tags.Add(tag.Key, tag.Value);
            }
        }

        public void Rename(string newName)
        {
            _actualFile.FileName = newName;
            _actualFile.FilePath = newName;
        }

        public void Dispose()
        {
            
        }

        public List<string> GetAllMp3s(string path)
        {
            return _files.Keys.ToList();
        }

        private TestFile _actualFile;
        private Dictionary<string, TestFile> _files;
    }

    class FileSystemBuilder
    {
        public FileSystemBuilder()
        {
            _fileSystem = new TestFileSystem();
        }

        public FileSystemBuilder File(TestFile value)
        {
            _fileSystem.Files.Add(value.FilePath,value);
            return this;
        }

        public FileSystemBuilder ActualFile(TestFile value)
        {
            _fileSystem.Files.Add(value.FilePath, value);
            _fileSystem.ActualFile = value;
            return this;
        }

        public TestFileSystem Build()
        {
            return _fileSystem;
        }

        private TestFileSystem _fileSystem;
    }

    class TestFile
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public Dictionary<FrameType, string> Tags { get; set; }

        public TestFile()
        {
            Tags = new Dictionary<FrameType, string>();
        }
    }

    class FileBuilder
    {
        public FileBuilder()
        {
            _file = new TestFile();
        }

        public FileBuilder FilePath(string value)
        {
            _file.FilePath = value;
            _file.FileName = value;
            return this;
        }

        public FileBuilder Tag(FrameType type, string value)
        {
            _file.Tags.Add(type, value);
            return this;
        }

        public FileBuilder Tags(Dictionary<FrameType, string> value)
        {
            _file.Tags = value;
            return this;
        }

        public TestFile Build()
        {
            return _file;
        }

        private TestFile _file;
    }
}
