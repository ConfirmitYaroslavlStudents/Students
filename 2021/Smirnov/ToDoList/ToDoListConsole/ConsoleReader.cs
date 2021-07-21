using System;

namespace ToDoListConsole
{
    class ConsoleReader : IReader
    {
        public ToDoListMenuEnum GetSelectedAction()
        {
            WriteMenu();
            return (ToDoListMenuEnum)int.Parse(GetDescription());
        }
        private static void WriteMenu()
        {
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. Delete task");
            Console.WriteLine("3. Change task description");
            Console.WriteLine("4. Complete task");
            Console.WriteLine("5. Write list all task");
            Console.WriteLine("6. Save and exit");
        }

        public int GetNumberTask()
        {
            Console.WriteLine("Choose number of task");

            return int.Parse(Console.ReadLine()) - 1;
        }

        public string GetDescription()
        {
            return Console.ReadLine();
        }
    }
}
