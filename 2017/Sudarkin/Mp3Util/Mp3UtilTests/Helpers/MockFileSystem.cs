using System.Collections.Generic;
using System.IO;
using Mp3UtilLib;
using Mp3UtilLib.FileSystem;
using System.Linq;

namespace Mp3UtilTests.Helpers
{
    public class MockFileSystem : IFileSystem
    {
        private readonly Dictionary<string, AudioFile> _files;

        public string CurrentDirectory { get; set; }
        public string[] AllFiles => _files.Keys.ToArray();

        public MockFileSystem()
        {
            _files = new Dictionary<string, AudioFile>();
        }

        public MockFileSystem(IDictionary<string, AudioFile> files) : this()
        {
            AddRange(files);
        }

        public MockFileSystem(IEnumerable<string> files) : this()
        {
            AddRange(files);
        }

        public void AddRange(IDictionary<string, AudioFile> collection)
        {
            foreach (var item in collection)
            {
                _files.Add(item.Key, item.Value);
            }
        }

        public void Add(string path, AudioFile file)
        {
            if (!Exists(path))
            {
                _files.Add(path, file);
            }
        }

        public void AddRange(IEnumerable<string> collection)
        {
            foreach (string item in collection)
            {
                Add(item, null);
            }
        }

        public bool Exists(string path)
        {
            return _files.ContainsKey(path);
        }

        public void Move(string source, string dest)
        {
            if (Exists(dest))
            {
                throw new IOException("File is already exists!");
            }

            Add(dest, _files[source]);
            _files.Remove(source);
        }

        public IEnumerable<AudioFile> GetAudioFilesFromCurrentDirectory(string searchPattern, bool recursive)
        {
            return _files.Select(file => file.Value).ToArray();
        }
    }
}