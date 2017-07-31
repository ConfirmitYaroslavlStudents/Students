namespace MusicFileRenamerLib
{
    public class Mp3File
    {
        public string path { get; set; }

        public string Artist { get; set; }
        public string Title { get; set; }

        public Mp3File(string _path)
        {
            path = _path;
        }
    }
}
