using System;
using System.Collections.Generic;

namespace ToDoLibrary
{
    public class ShowInformationInConsole
    {
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
