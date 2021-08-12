using System;
using ToDoListApp.Writer;

namespace ToDoListAppTests
{
    public class FakeConsoleWriter : IWriter
    {
        public bool TaskCreated { set; get; }
        public string Tasks { set; get; }
        public void WriteIncorrectCommand()
        {
        }

        public void WriteTaskNotFound()
        {
        }

        public void WriteExceptionMessage(Exception e)
        {
        }

        public void WriteTaskCreated()
        {
            TaskCreated = true;
        }

        public void WriteDescriptionChanged()
        {
        }

        public void WriteTaskCompleted()
        {
        }

        public void WriteTaskDeleted()
        {
        }

        public void WriteTasks(string allTask)
        {
            Tasks = allTask;
        }
    }
}
