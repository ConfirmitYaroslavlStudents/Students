using System;
using System.Linq;
using System.Text;

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

        public void Save()
        {
            if (_currentFile == null)
                throw new NullReferenceException("File is not loaded");
            _currentFile.Save();
        }

        public void ChangeTags(Mp3Tags tags)
        {
            if (_currentFile == null)
                throw new NullReferenceException("File is not loaded");
            if(tags==null)
                throw new ArgumentException("Incorrect tags");
            _currentFile.SetTags(tags);
        }

        public void ChangeName(Mask newNameMask)
        {
            if (_currentFile == null)
                throw new NullReferenceException("File is not loaded");
            if (newNameMask == null)
                throw new ArgumentException("Incorrect mask");
            var newName = newNameMask.ToString();
            var currentTags = _currentFile.GetTags();
            foreach (var item in newNameMask)
            {
                newName = newName.Replace(item, currentTags.GetTag(item));
            }
            newName=newName.Replace("}","");
            newName=newName.Replace("{", "");
            _currentFile.ChangeName(newName);
        }

        public bool ValidateFileName(Mask mask)
        {
            try
            {
                var posibleTagValuesFromName = mask.GetTagValuesFromString(_currentFile.Name);
                var tagsFromFile = _currentFile.GetTags();
                var fileNameIsOK = false;
                foreach (var tagValues in posibleTagValuesFromName)
                {
                    if (fileNameIsOK)
                        break;
                    foreach (var tagValue in tagValues)
                    {

                        if (tagValue.Value != tagsFromFile.GetTag(tagValue.Key))
                        {
                            fileNameIsOK = false;
                            break;
                        }
                        fileNameIsOK = true;
                    }
                }
                return fileNameIsOK;
            }
            catch
            {
                return false;
            }
        }
    }
}
