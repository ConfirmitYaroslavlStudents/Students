using System;
using System.Collections.Generic;
using System.Linq;

namespace Mp3TagLib
{
    public class Tager
    {
        private readonly IFileLoader _fileLoader;
        private IMp3File _currentFile;
     
        public Tager(IFileLoader fileLoader)
        {
            _fileLoader = fileLoader;
            _currentFile = null;
        }

        public IMp3File CurrentFile
        {
            get { return  _currentFile; }
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
            _currentFile.ChangeName(GenerateName(newNameMask));
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

        public IEnumerable<Tags> GetIncorectTags(Mask mask)
        {
            var tags = _currentFile.GetTags();
            return (from tag in tags.TagsDictionary where string.IsNullOrEmpty(tag.Value) && mask.Contains(tag.Key.ToString().ToLower()) select tag.Key).ToList();
        }



        internal List<FileProblem> GetFileProblems(Mask mask)
        {
            var fileProblem=new List<FileProblem>();
            if (GetIncorectTags(mask).Any())
            {
                fileProblem.Add(FileProblem.BadTags);
            }
            try
            {
                mask.GetTagValuesFromString(CurrentFile.Name);
            }
            catch
            {
                fileProblem.Add(FileProblem.BadName);
            }
            return fileProblem;


        }

        internal string GenerateName(Mask mask)
        {
            if (_currentFile == null)
                throw new NullReferenceException("File is not loaded");

            if (mask == null)
                throw new ArgumentException("Incorrect mask");

            var newName = mask.ToString();
            var currentTags = _currentFile.GetTags();


            foreach (var item in mask)
            {
                var tagValue = currentTags.GetTag(item);
                if (string.IsNullOrEmpty(tagValue))
                    throw new InvalidOperationException("tag is empty");
                newName = newName.Replace("{" + item + "}", tagValue);
            }
            return newName;
        }

        internal Mp3Tags GetTagsFromName(Mask mask)
        {
            var tags = new Mp3Tags();
            var tagsFromName = mask.GetTagValuesFromString(CurrentFile.Name);
            foreach (var tag in tagsFromName.First())
            {
                tags.SetTag(tag.Key, tag.Value);
            }
            return tags;
        }
    }
}
