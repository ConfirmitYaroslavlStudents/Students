using System;
namespace ToDoListApp.Writer
{
    public class ConsoleWriter: IWriter
    {
        public void WriteIncorrectCommand()
        {
            Console.WriteLine("Incorrect command");
        }
        public void WriteTaskNotFound()
        {
            Console.WriteLine("Task not found");
        }

        public void WriteExceptionMessage(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        public void WriteTaskCreated()
        {
            Console.WriteLine("Task created");
        }

        public void WriteDescriptionChanged()
        {
            Console.WriteLine("Description changed");
        }   

        public void WriteTaskCompleted()
        {
            Console.WriteLine("Task completed");
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
