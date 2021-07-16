using System;
using Spectre.Console;

namespace ToDoListConsole
{
    public interface IConsolePrint
    {
        void PrintErrorMessage();
        void PrintNewDescriptionRequest();
    }

    public interface IAnsiConsolePrint
    {
        void PrintDoneMessage();
        void PrintTaskNumberRequest();
        void PrintIncorrectNumberWarning();
    }

    public class MessagePrinter : IConsolePrint, IAnsiConsolePrint
    {
        public void PrintErrorMessage()
        {
            Console.WriteLine("You've just entered something wrong...You might want to try one more time");
        }

        public void PrintNewDescriptionRequest()
        {
            Console.WriteLine("[lightgoldenrod2_1]Type in a new description[/]");
        }
        public void PrintDoneMessage()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]Done![/]");
        }

        public void PrintTaskNumberRequest()
        {
            AnsiConsole.MarkupLine("[lightgoldenrod2_1]Choose the task number[/]");
        }

        public void PrintIncorrectNumberWarning()
        {
            AnsiConsole.MarkupLine("[red]Incorrect number[/]");
        }
    }
}

