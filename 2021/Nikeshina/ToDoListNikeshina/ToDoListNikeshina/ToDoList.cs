﻿using System;
using System.Collections.Generic;

namespace ToDoListNikeshina
{
    public class ToDoList
    {
        internal List<Task> _list;

        public ToDoList(List<Task> inputList)
        {
            _list = inputList;
        }

        public int Count()
        {
            return _list.Count;
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
