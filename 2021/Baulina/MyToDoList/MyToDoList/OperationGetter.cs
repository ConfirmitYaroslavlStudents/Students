using ConsoleInteractors;

namespace ToDoApp
{
    interface IOperationGetter
    {
        string GetOperationName();
    }

    public class OperationGetter:IOperationGetter
    {
        private readonly IConsoleExtended _consoleHandler;
        public OperationGetter(IConsoleExtended consoleHandler)
        {
            _consoleHandler = consoleHandler;
        }
        public string GetOperationName()
        {
            return _consoleHandler.GetMenuItemName();
        }
    }
}
