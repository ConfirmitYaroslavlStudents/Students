using System;

namespace ToDoListApp.Reader
{
    public class UserInputReader : IReader
    {
        private readonly IConsoleInput _consoleInput;

        public UserInputReader(IConsoleInput consoleInput)
        {
            _consoleInput = consoleInput;
        }
        public ListCommandMenu GetCommand()
        {
            WriteMenu();
            return (ListCommandMenu)int.Parse(_consoleInput.ReadLine());
        }
        private static void WriteMenu()
        {
            Console.WriteLine("......................................");
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. Delete task");
            Console.WriteLine("3. Change task description");
            Console.WriteLine("4. Complete task");
            Console.WriteLine("5. Write list all task");
            Console.WriteLine("6. Save and exit");
            Console.WriteLine("......................................");
        }

        public int GetTaskId()
        {
            Console.WriteLine("Choose Id of task");

            return int.Parse(_consoleInput.ReadLine());
        }

        public string GetTaskDescription()
        {
            Console.WriteLine("Write description of task");
            return _consoleInput.ReadLine();
        }

        public bool ContinueWork()
        {
            return true;
        }
    }
}
