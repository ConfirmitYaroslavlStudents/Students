using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
   public static class PrintingConsoleMenuToDo
    {
        public static void PrintAllNotes(List<Note> notes)
        {
            for (var i = 0; i < notes.Count; i++)
                Console.WriteLine("{0}. {1}", i + 1, notes[i].ToString());
        }

        public static void PrintHelp()
        {
            Console.WriteLine();
            for (var i = 0; i < Commands.Help.Length; i++)
                Console.WriteLine(Commands.Help[i]);
            Console.WriteLine();
        }

        public static void PrintGoodbye()
        {
            Console.WriteLine("Bye Bye ^-^");
        }
    }
}
