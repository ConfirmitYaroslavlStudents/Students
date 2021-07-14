using System;
using Spectre.Console;

namespace ToDoListConsole
{
    public interface IMessagePrint
    {
        public void PrintErrorMessage();

        public void PrintDoneMessage();
    }

    public class MessagePrinter : IMessagePrint
    {
        public void PrintErrorMessage()
        {
            Console.WriteLine("You've just entered something wrong...You might want to try one more time");
        }

        public void PrintDoneMessage()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]Done![/]");
        }
    }
}

