using System.Collections.Generic;
using MyToDoList;

namespace ToDoApi
{
    public class FileToDoListProvider : IToDoListProvider
    {
        private readonly FileManager _fileManager = new FileManager();
        public IEnumerable<ToDoItem> GetToDoList()
        {
           return _fileManager.LoadFromFile();
        }
    }
}
