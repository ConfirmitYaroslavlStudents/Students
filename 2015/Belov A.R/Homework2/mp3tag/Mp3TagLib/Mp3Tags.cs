using System;
namespace Mp3TagLib
{
    //[TODO] use enum instead of strings in program
    public enum TagList { Title,Artist,Album,Year,Comment,Genre,Track}
    public class Mp3Tags
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Comment { get; set; }
        public string Genre { get; set; }
        public uint Year { get; set; }
        public uint Track { get; set; }

        public void SetTag(string name,string value)
        {
            if (name == null)
                throw new ArgumentException("Bad tag name");
            //[ToDO] remove switch
            switch (name.ToLower())
            {
                case "artist":
                    Artist=value;
                    break;
                case "title":
                    Title=value;
                    break;
                case "album":
                    Album=value;
                    break;
                case "year":
                    uint year;
                    if(!uint.TryParse(value,out year))
                        throw new ArgumentException("Bad tag name");
                    Year = year;
                    break;
                case "track":
                    uint track;
                    if (!uint.TryParse(value, out track))
                        throw new ArgumentException("Bad tag name");
                    Track = track;
                    break;
                case "comment":
                    Comment=value;
                    break;
                case "genre":
                    Genre=value;
                    break;
                default:
                    throw new ArgumentException("Bad tag name");
            }
        }

        public string GetTag(string name)
        {
            switch (name.ToLower())
            {
                case"artist":
                    return Artist;
                case "title":
                    return Title;
                case "album":
                    return Album;
                case "year":
                    return Year.ToString();
                case "track":
                    return Track.ToString();
                case "comment":
                    return Comment;
                case "genre":
                    return Genre;
                default:
                    throw new ArgumentException("Bad tag name");
            }
        }
    }
}
