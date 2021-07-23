using Spectre.Console.Rendering;

namespace InputOutputManagers
{
    public class InputOutputManager : IConsoleExtended
    {
        private readonly IConsoleExtended _console;

        public InputOutputManager(IConsoleExtended console) => _console = console;

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

        public void WriteLine(string message) => _console.WriteLine(message);
        public string ReadLine() => _console.ReadLine();
        public void RenderTable(IRenderable table) => _console.RenderTable(table);
        public void Clear() => _console.Clear();
        public string GetDescription() => _console.GetDescription();
        public string GetMenuItemName() => _console.GetMenuItemName();
    }
}

