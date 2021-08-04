﻿using System;
using System.Collections.Generic;
using System.Text;
using ToDo;

namespace ToDoApiTests
{
    public class FakeLoaderAndSaver : ILoaderAndSaver
    {
        public ToDoList ToDoList = new ToDoList();
        public ToDoList Load()
        {
            return ToDoList;
        }

        public void Save(ToDoList toDoList)
        {
            ToDoList = toDoList;
        }
    }
}
