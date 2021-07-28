using System;

namespace ToDoListApp.Reader
{
    class UserInputReader : IReader
    {
        public ToDoListMenuEnum GetSelectedAction()
        {
            WriteMenu();
            return (ToDoListMenuEnum)int.Parse(Console.ReadLine());
        }
        private static void WriteMenu()
        {
            Console.WriteLine("......................................");
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. DeleteTask task");
            Console.WriteLine("3. Change task description");
            Console.WriteLine("4. Complete task");
            Console.WriteLine("5. Write list all task");
            Console.WriteLine("6. Save and exit");
            Console.WriteLine("......................................");
        }

        public int GetTaskId()
        {
            Console.WriteLine("Choose Id of task");

            return int.Parse(Console.ReadLine());
        }

        public string GetDescription()
        {
            Console.WriteLine("Write description of task");

            return Console.ReadLine();
        }

        public bool ContinueWork()
        {
            return true;
        }
    }
}
