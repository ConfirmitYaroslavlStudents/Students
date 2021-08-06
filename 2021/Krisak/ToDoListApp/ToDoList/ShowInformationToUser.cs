using System;
using System.Collections.Generic;

namespace ToDoLibrary
{
    public static class ShowInformationToUser
    {
        public static void ShowAllNotesInConsole(List<Task> notes)
        {
            for (var i = 0; i < notes.Count; i++)
                Console.WriteLine("{0}. {1}", i + 1, notes[i].ToString());
        }

        public static void ShowHelpInConsole()
        {
            Console.WriteLine();
            foreach (var help in AllCommands.Help)
                Console.WriteLine(help);

            Console.WriteLine();
        }

        public static void ShowGoodbyeInConsole()
        {
            Console.WriteLine("Bye Bye ^-^");
        }
    }
}
