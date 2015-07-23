using System;
namespace Mp3TagLib
{
    public class Tager
    {
        private IFileLoader _fileLoader;
        private IMp3File _currentFile;
        public Tager(IFileLoader fileLoader)
        {
            _fileLoader = fileLoader;
            _currentFile = null;
        }

        public IMp3File CurrentFile
        {
            get { return _currentFile; }
        }

        public bool Load(string path)
        {
            _currentFile=_fileLoader.Load(path);
            return _currentFile != null;
        }

        public bool Save()
        {
            if (_currentFile == null)
                return false;
            _currentFile.Save();
            return true;
        }

        public void ChangeTags(Mp3Tags tags)
        {
            if (_currentFile == null)
                throw new NullReferenceException("File is not loaded");
            if(tags==null)
                throw new ArgumentException("Incorrect tags");
            _currentFile.SetTags(tags);
        }
    }
}
