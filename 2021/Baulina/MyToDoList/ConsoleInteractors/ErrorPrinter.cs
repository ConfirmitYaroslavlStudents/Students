namespace ConsoleInteractors
{
    interface IErrorHandler
    {
        void PrintErrorMessage();
        void PrintIncorrectNumberWarning();
    }
    public class ErrorPrinter : IErrorHandler, IConsole
    {
        private readonly IConsoleExtended _console;
        public ErrorPrinter(IConsoleExtended console) => _console = console;
        public void PrintErrorMessage()
        {
            _console.WriteLine("[red]Something went wrong...You might want to try one more time[/]");
        }

        public void PrintIncorrectNumberWarning()
        {
           _console.WriteLine("[red]Incorrect number[/]");
        }

        public void WriteLine(string message) => _console.WriteLine(message);
        public string ReadLine() => _console.ReadLine();
    }
}
