namespace FileLib
{
    public class Mp3Tags
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Album { get; set; }
        public uint Track { get; set; }

        public void CopyTo(Mp3Tags destination)
        {
            destination.Album = Album;
            destination.Artist = Artist;
            destination.Genre = Genre;
            destination.Title = Title;
            destination.Track = Track;
        }

        public override string ToString()
        {
            return Artist + ", " 
                + Title + ", " 
                + Genre + ", " 
                + Album + ", " 
                + Track;
        }
    }
}