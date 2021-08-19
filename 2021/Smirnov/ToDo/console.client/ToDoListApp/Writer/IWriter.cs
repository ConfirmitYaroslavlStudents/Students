using System;
using ToDoListApp.Models;

namespace ToDoListApp.Writer
{
    public interface IWriter
    {
        public void WriteIncorrectCommand();
        public void WriteExceptionMessage(Exception e);
        public void WriteTaskCreated(ToDoItem createdTask);
        public void WriteDescriptionChanged(ToDoItem changedTask);
        public void WriteTaskCompleted(ToDoItem changedTask);
        public void WriteTaskDeleted();
        public void WriteTasks(string allTask);
    }
}
