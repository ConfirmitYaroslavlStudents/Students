using Spectre.Console;
using Spectre.Console.Rendering;

namespace ToDoApp
{
    public interface IAnsiConsolePrint
    {
        void PrintErrorMessage();
        void PrintNewDescriptionRequest();
        void PrintDoneMessage();
        void PrintTaskNumberRequest();
        void PrintIncorrectNumberWarning();
    }

    public class MessagePrinter : IAnsiConsolePrint, IConsole
    {
        private readonly IConsole _console;

        public MessagePrinter(IConsole console) => _console = console;

        public void PrintErrorMessage()
        {
            _console.WriteLine("[red]Something went wrong...You might want to try one more time[/]");
        }

        public void PrintNewDescriptionRequest()
        {
            _console.WriteLine("[lightgoldenrod2_1]Type in a new description[/]");
        }
        public void PrintDoneMessage()
        {
            _console.Clear();
            _console.WriteLine("[bold green]Done![/]");
        }

        public void PrintTaskNumberRequest()
        {
            _console.WriteLine("[lightgoldenrod2_1]Choose the task number[/]");
        }

        public void PrintIncorrectNumberWarning()
        {
            _console.WriteLine("[red]Incorrect number[/]");
        }

        public void WriteLine(string message) => AnsiConsole.MarkupLine(message);
        public string ReadLine() => _console.ReadLine();
        public void RenderTable(IRenderable table) => _console.RenderTable(table);
        public void Clear() => _console.Clear();
        public string GetDescription() => _console.GetDescription();
        public string GetMenuItemName() => _console.GetMenuItemName();
    }
}

