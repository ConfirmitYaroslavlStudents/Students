using System.Collections.Generic;
using System.IO;

namespace GeneralizeSynchLibrary
{
    public class FileWrapperCollection
    {
        List<FileWrapper> _collection { get; }
        public int Count { get; set; }

        public List<FileWrapper> GetListCopy()
        {
            return new List<FileWrapper>(_collection);
        }

        public FileWrapperCollection(List<string> input)
        {
            _collection = new List<FileWrapper>();
            Count = input.Count;
            Initialize(input);
        }

        public FileWrapperCollection(List<FileWrapper> list)
        {
            int maxPriority = -1;
            _collection = new List<FileWrapper>();
            foreach (var item in list)
            {
                if (item.Priority > maxPriority)
                    maxPriority = item.Priority;
                _collection.Add(item);
            }
            Count = ++maxPriority;
        }

        public void Initialize(List<string> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                AddItemFromFolder(input[i], i);
            }
        }

        private void AddItemFromFolder(string root, int priority)
        {
            foreach (var file in Directory.GetFiles(root, "*", SearchOption.AllDirectories))
                _collection.Add(new FileWrapper(priority, root, SetRelativePath(file, root)));
        }

        private string SetRelativePath(string fullPath, string root)
        {
            var current = fullPath.Replace(root, "");
            if (current[0] == '\\')
                current = current.TrimStart('\\');
            return current;
        }
    }
}
