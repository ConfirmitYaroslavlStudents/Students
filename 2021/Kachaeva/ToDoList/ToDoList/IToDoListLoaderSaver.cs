using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListProject
{
    public interface IToDoListLoaderSaver
    {
        public ToDoList Load();
        public void Save(ToDoList toDoList);
    }
}
