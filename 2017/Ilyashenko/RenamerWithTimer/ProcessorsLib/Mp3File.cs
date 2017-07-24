namespace ProcessorsLib
{
    public class Mp3File
    {
        public string path { get; set; }

        public Mp3File(string _path)
        {
            path = _path;
        }

        public void Move(string newPath)
        {
            path = newPath;
        }
    }
}
