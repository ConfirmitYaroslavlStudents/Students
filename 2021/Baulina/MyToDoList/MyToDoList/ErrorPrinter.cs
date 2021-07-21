using Spectre.Console;

namespace ToDoApp
{
    class ErrorPrinter : IAnsiConsolePrint
    {
        public void PrintErrorMessage()
        {
            AnsiConsole.MarkupLine("[red]Something went wrong...You might want to try one more time[/]");
        }

        public void PrintNewDescriptionRequest() { }

        public void PrintDoneMessage() { }

        public void PrintTaskNumberRequest() { }

        public void PrintIncorrectNumberWarning()
        {
            AnsiConsole.MarkupLine("[red]Incorrect number[/]");
        }
    }
}
