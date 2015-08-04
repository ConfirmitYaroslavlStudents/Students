using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3TagLib
{
    public class Synchronizer
    {
        private Tager _tager;
        public Synchronizer(Tager tager)
        {
            _tager = tager;
            ModifiedFiles=new List<IMp3File>();
            ErrorFiles=new Dictionary<string, string>();
        }
        public List<IMp3File> ModifiedFiles { get; set; }
        public Dictionary<string, string> ErrorFiles { get; set; }

        public void Sync(IEnumerable<IMp3File> files,Mask mask)
        {
            foreach (var mp3File in files)
            {
                _tager.CurrentFile = mp3File;
                if (!TryChangeName(mask))
                {
                    TryChangeTags(mask);
                }
            }
        }

        public bool TryChangeName(Mask mask)
        {
            try
            {
                _tager.ChangeName(mask);
                ModifiedFiles.Add(_tager.CurrentFile);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool TryChangeTags(Mask mask)
        {
            try
            {
                var tags = new Mp3Tags();
                var tagsFromName = mask.GetTagValuesFromString(_tager.CurrentFile.Name);
                foreach (var tag in tagsFromName.First())
                {
                    tags.SetTag(tag.Key, tag.Value);
                }
                _tager.ChangeTags(tags);
                ModifiedFiles.Add(_tager.CurrentFile);
                return true;
            }
            catch (Exception e)
            {
                ErrorFiles.Add(_tager.CurrentFile.Name,e.Message);
                return false;
            }
        }

        public void Save()
        {
            foreach (var modifiedFile in ModifiedFiles)
            {
                try
                {
                    modifiedFile.Save();
                }
                catch (Exception e)
                {
                      
                    ErrorFiles.Add(modifiedFile.Name,e.Message);
                }
            }
            ModifiedFiles.Clear();
        }
    }
}
