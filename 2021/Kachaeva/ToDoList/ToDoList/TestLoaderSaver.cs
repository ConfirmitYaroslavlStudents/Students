using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListProject
{
    public class TestLoaderSaver : IToDoListLoaderSaver
    {
        public ToDoList Load()
        {
            return new ToDoList();
        }

        public void Save(ToDoList toDoList)
        {
        }
    }
}
