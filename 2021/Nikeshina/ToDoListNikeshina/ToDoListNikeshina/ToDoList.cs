﻿using System;
using System.Collections.Generic;

namespace ToDoListNikeshina
{
    public class ToDoList
    {
        private List<Task> _list;
        private WorkWithFile _fileEditor;

        public ToDoList()
        {
            _list = new List<Task>();
            _fileEditor = new WorkWithFile();
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
                Console.WriteLine(i + ". " + task.Print());
                i++;
            }
        }

        public void Add(Task item)
        {
            _list.Add(item);
        }

        public void Detete(int num)
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
