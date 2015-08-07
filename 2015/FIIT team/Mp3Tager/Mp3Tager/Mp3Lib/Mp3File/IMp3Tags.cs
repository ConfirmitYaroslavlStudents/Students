namespace Mp3Lib
{
    public interface IMp3Tags
    {
        string Artist { get; set; }
        string Title { get; set; }
        string Genre { get; set; }
        string Album { get; set; }
        uint Track { get; set; }
    }
}