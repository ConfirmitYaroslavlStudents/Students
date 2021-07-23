using InputOutputManagers;

namespace ToDoApp
{
    interface IOperationGetter
    {
        string GetOperationName();
    }

    public class OperationGetter:IOperationGetter
    {
        private readonly IConsoleExtended _console;
        public OperationGetter(IConsoleExtended console)
        {
            _console = console;
        }
        public string GetOperationName()
        {
            return _console.GetMenuItemName();
        }
    }
}
