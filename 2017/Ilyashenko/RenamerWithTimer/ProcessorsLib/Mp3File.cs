namespace RenamersLib
{
    public class Mp3File
    {
        public string Path { get; private set; }

        public Mp3File(string _path)
        {
            Path = _path;
        }

        public void Move(string newPath)
        {
            Path = newPath;
        }
    }
}
