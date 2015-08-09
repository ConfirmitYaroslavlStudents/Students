namespace Mp3Lib
{
    public class Mp3Tags : IMp3Tags
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Album { get; set; }
        public uint Track { get; set; }
    }
}