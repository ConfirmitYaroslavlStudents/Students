using System;
using System.Collections.Generic;

namespace Mp3TagLib
{
    //[TODO] use enum instead of strings in program
    public enum Tags { Title,Artist,Album,Year,Comment,Genre,Track}
    public class Mp3Tags
    {
        private Dictionary<Tags, string> _tags;
        public Dictionary<Tags, string> TagsDictionary { get { return _tags; } } 

        public Mp3Tags()
        {
            _tags = new Dictionary<Tags, string>
            {
                {Tags.Title, ""},
                {Tags.Artist, ""},
                {Tags.Album, ""},
                {Tags.Year, ""},
                {Tags.Comment, ""},
                {Tags.Genre, ""},
                {Tags.Track, ""}
            };
        }
     
        public string Title
        {
            get { return _tags[Tags.Title]; }
            set { _tags[Tags.Title] = value; }
        }
      
        public string Artist
        {
            get { return _tags[Tags.Artist]; }
            set { _tags[Tags.Artist] = value; }
        }
       
        public string Album
        {
            get { return _tags[Tags.Album]; }
            set { _tags[Tags.Album] = value; }
        }
       
        public string Comment
        {
            get { return _tags[Tags.Comment]; }
            set { _tags[Tags.Comment] = value; }
        }
       
        public string Genre
        {
            get { return _tags[Tags.Genre]; }
            set { _tags[Tags.Genre] = value; }
        }
        public uint Year
        {
            get
            {
                uint year;
                if (uint.TryParse(_tags[Tags.Year], out year))
                {
                    return year;
                }
                return 0;
            }
            set { _tags[Tags.Year] = value.ToString(); }
        }
        public uint Track
        {
            get
            {
                uint year;
                if (uint.TryParse(_tags[Tags.Track], out year))
                {
                    return year;
                }
                return 0;
            }
            set { _tags[Tags.Track] = value.ToString(); }
        }

        public void SetTag(string name,string value)
        {
            if (name == null)
                throw new ArgumentException("Bad tag name");
           
            if (value == null)
                throw new ArgumentException("Bad value");
            //[ToDO] remove if
           
            Tags tag;
            if (Enum.TryParse(name, true, out tag))
            {
                _tags[tag] = value;
            }
            else
            {
                throw new ArgumentException("Bad tag name");
            }
           
        }

        public string GetTag(string name)
        {
            Tags tag;
           
            if (Enum.TryParse(name, true, out tag))
            {
                return _tags[tag];
            }
            throw new ArgumentException("bad tag name");
        }
    }
}
