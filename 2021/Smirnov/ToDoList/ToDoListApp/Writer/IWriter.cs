﻿using System;
using System.Collections.Generic;
using ToDoListLib.Models;

namespace ToDoListApp.Writer
{
    public interface IWriter
    {
        public void WriteIncorrectCommand();
        public void WriteExceptionMessage(Exception e);
        public void WriteTaskCreated();
        public void WriteDescriptionChanged();
        public void WriteTaskCompleted();
        public void WriteTaskDeleted();
        public void WriteAllTask(IEnumerable<Task> tpDoList);
    }
}
