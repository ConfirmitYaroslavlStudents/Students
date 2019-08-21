using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralizeSynchLibrary
{
    public class FileWrapper
    {
        public int Priority { get; private set; }
        public string Root { get; private set; }
        public string Path { get; private set; }

        public FileWrapper(int priority, string root, string path)
        {
            Priority = priority;
            Root = root;
            Path = path;
        }

        public override bool Equals(object file)
        {
            FileWrapper obj = (FileWrapper)file;
            return Path.Equals(obj.Path);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}
