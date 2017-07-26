using Mp3UtilLib.FileSystem;

namespace Mp3UtilLib
{
    public abstract class AudioFile
    {
        protected readonly IFileSystem FileSystem;

        public string Artist { get; set; }
        public string Title { get; set; }

        public string FullName { get; protected set; }

        protected AudioFile(string path) 
            : this(path, new FileSystem.FileSystem())
        {

        }

        protected AudioFile(string path, IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
            FullName = path;
        }

        public virtual void MoveTo(string path)
        {
            FileSystem.Move(FullName, path);

            FullName = path;
        }

        public abstract void Save();
    }
}