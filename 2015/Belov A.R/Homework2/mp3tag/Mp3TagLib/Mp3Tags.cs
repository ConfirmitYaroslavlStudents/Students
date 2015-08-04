using System;
using System.Collections.Generic;

namespace Mp3TagLib
{
    //[TODO] use enum instead of strings in program
    public enum TagList { Title,Artist,Album,Year,Comment,Genre,Track}
    public class Mp3Tags
    {
        private Dictionary<string, string> _tags;

        public Mp3Tags()
        {
            _tags = new Dictionary<string, string>
            {
                {"title", ""},
                {"artist", ""},
                {"album", ""},
                {"year", ""},
                {"comment", ""},
                {"genre", ""},
                {"track", ""}
            };
        }
        public string Title
        {
            get { return _tags["title"]; }
            set { _tags["title"] = value; }
        }
        public string Artist
        {
            get { return _tags["artist"]; }
            set { _tags["artist"] = value; }
        }
        public string Album
        {
            get { return _tags["album"]; }
            set { _tags["album"] = value; }
        }
        public string Comment
        {
            get { return _tags["comment"]; }
            set { _tags["comment"] = value; }
        }
        public string Genre
        {
            get { return _tags["genre"]; }
            set { _tags["genre"] = value; }
        }
        public uint Year
        {
            get
            {
                uint year;
                if (uint.TryParse(_tags["year"], out year))
                {
                    return year;
                }
                return 0;
            }
            set { _tags["year"] = value.ToString(); }
        }
        public uint Track
        {
            get
            {
                uint year;
                if (uint.TryParse(_tags["track"], out year))
                {
                    return year;
                }
                return 0;
            }
            set { _tags["track"] = value.ToString(); }
        }

        public void SetTag(string name,string value)
        {
            if (name == null)
                throw new ArgumentException("Bad tag name");
            name = name.ToLower();
            //[ToDO] remove switch
            if (_tags.ContainsKey(name))
            {
                if (name == "year" || name == "track")
                {
                    uint temp;
                    if (!uint.TryParse(value, out temp))
                        throw new ArgumentException("Bad tag name");
                    _tags[name] = temp.ToString();
                }
                else
                {
                    _tags[name] = value;
                }
            }
            else
            {
                throw new ArgumentException("Bad tag name");
            }
           
        }

        public string GetTag(string name)
        {
            if (_tags.ContainsKey(name.ToLower()))
            {
                return _tags[name];
            }
            throw new ArgumentException("bad tag name");
        }
    }
}
