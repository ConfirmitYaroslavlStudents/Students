using System;
using ToDoListApp.Models;
using ToDoListApp.Writer;

namespace ToDoListAppTests
{
    public class FakeConsoleWriter : IWriter// bad name
    {
        public bool IsCreated { set; get; }
        public bool IsDeleted { set; get; }
        public bool IsDescriptionChanged { set; get; }
        public bool IsTaskCompleted { set; get; }
        public bool IsException { set; get; }
        public string Tasks { set; get; }
        public ToDoItem CreatedTask { set; get; }
        public ToDoItem ChangedTask { set; get; }
        public string ExceptionMessage { set; get; }
        public void WriteIncorrectCommand()
        {
        }

        public void WriteExceptionMessage(Exception e)
        {
            IsException = true;
            ExceptionMessage = e.Message;
        }

        public void WriteTaskCreated(ToDoItem createdTask)
        {
            IsCreated = true;
            CreatedTask = createdTask;
        }

        public void WriteDescriptionChanged(ToDoItem changedTask)
        {
            IsDescriptionChanged = true;
            ChangedTask = changedTask;
        }

        public void WriteTaskCompleted(ToDoItem changedTask)
        {
            IsTaskCompleted = true;
            ChangedTask = changedTask;
        }

        public void WriteTaskDeleted()
        {
            IsDeleted = true;
        }

        public void WriteTasks(string allTask)
        {
            Tasks = allTask;
        }
    }
}
