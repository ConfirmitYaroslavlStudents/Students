using System;
using System.Collections.Generic;

namespace ToDoListNikeshina
{
    public class ToDoList
    {
        private List<Task> _list;
        private FileOperation _fileEditor;
        ILogger _logger;

        public ToDoList(ILogger logger)
        {
            _list = new List<Task>();
            _fileEditor = new FileOperation();
            _logger = logger;
        }

        public int Count()
        {
            return _list.Count;
        }

        public void Read()
        {
            _list = _fileEditor.Read();
        }

        public void Write()
        {
            _fileEditor.Write(_list);
        }

        public void Print()
        {
            int i = 1;
            foreach (var task in _list)
            {
                _logger.WriteLine(i + ". " + task.Print());
                i++;
            }
        }

        public void Add(Task item)
        {
            _list.Add(item);
        }

        public void Delete(int num)
        {
            _list.RemoveAt(num - 1);
        }

        public void ChangeStatus(int index)
        {
            int indexInList = index - 1;
            _list[indexInList].ChangeStatus();
        }

        public void Edit(int index, string dscr)
        {
            int indexInList = index - 1;
            _list[indexInList].ChangeName(dscr);
        }

    }
}
