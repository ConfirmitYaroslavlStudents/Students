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
            set { _currentFile = value; }
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
                var tagValue = currentTags.GetTag(item);
                if(string.IsNullOrEmpty(tagValue))
                    throw new InvalidOperationException("tag is empty");
                newName = newName.Replace("{"+item+"}", tagValue);
            }
            _currentFile.ChangeName(newName);
        }

        public bool ValidateFileName(Mask mask)
        {
            try
            {
                var posibleTagValuesFromName = mask.GetTagValuesFromString(_currentFile.Name);
                var tagsFromFile = _currentFile.GetTags();
                var fileNameIsOk = false;
                foreach (var tagValues in posibleTagValuesFromName)
                {
                    if (fileNameIsOk)
                        break;
                    foreach (var tagValue in tagValues)
                    {
                        var currentTagFromFile = tagsFromFile.GetTag(tagValue.Key);
                        if (tagValue.Value != currentTagFromFile)
                        {
                            if (tagValue.Key == "track"&&tagValue.Value.StartsWith("0"))
                            {
                                if(tagValue.Value.Substring(1) == currentTagFromFile)
                                continue;
                            }
                            fileNameIsOk = false;
                            break;
                        }
                        fileNameIsOk = true;
                    }
                }
                return fileNameIsOk;
            }
            catch
            {
                return false;
            }
        }
    }
}
