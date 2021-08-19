using System;
using ToDoListApp.Models;

namespace ToDoListApp.Writer
{
    public class ConsoleWriter: IWriter
    {
        public void WriteIncorrectCommand()
        {
            Console.WriteLine("Incorrect command");
        }

        public void WriteExceptionMessage(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        public void WriteTaskCreated(ToDoItem createdTask)
        {
            Console.WriteLine("Task created");
            Console.WriteLine(createdTask);
        }

        public void WriteDescriptionChanged(ToDoItem changedTask)
        {
            Console.WriteLine("Description changed");
            Console.WriteLine(changedTask);
        }   

        public void WriteTaskCompleted(ToDoItem changedTask)
        {
            Console.WriteLine("Task completed");
            Console.WriteLine(changedTask);
        }

        public void WriteTaskDeleted()
        {
            Console.WriteLine("Task deleted");
        }

        public void WriteTasks(string allTask)
        {
            Console.WriteLine("......................................");
            Console.WriteLine(allTask);
            Console.WriteLine("......................................");
        }
    }
}
