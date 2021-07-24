using InputOutputManagers;

namespace ToDoApp
{
    public class AppController
    {
        public CommandExecutor CommandExecutor { get; }
        private readonly IConsoleExtended _console;

        public AppController(CommandExecutor commandExecutor, IConsoleExtended console)
        {
            CommandExecutor = commandExecutor;
            _console = console;
        }

        public string GetOperationName()
        {
            return _console.GetMenuItemName();
        }

        public void SendOperationToExecutor()
        {
            var operationName = GetOperationName();
            CommandExecutor.ProcessOperation(operationName);
        }
    }
}
