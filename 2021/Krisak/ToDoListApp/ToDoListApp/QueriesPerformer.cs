using System;
using ToDoLibrary;

namespace ToDoConsole
{
    public class QueriesPerformer
    {
        private ToDoApp _toDoApp;

        public QueriesPerformer(ToDoApp toDoApp)
            => _toDoApp = toDoApp;

        public void ShowHelp()
        {
            Console.WriteLine();
            foreach (var help in ConsoleHelper.Help)
                Console.WriteLine(help);

            Console.WriteLine();
        }

        public void ShowTasks()
        {
            foreach (var task in _toDoApp.ShowTasks())
                Console.WriteLine(task);
        }

        public static void ShowGoodbye()
            => Console.WriteLine("Bye Bye ^-^");
    }
}