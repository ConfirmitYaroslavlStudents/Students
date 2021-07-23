using System;
using InputOutputManagers;

namespace ToDoApp
{
    interface IOperationGetter
    {
        string GetOperationName();
        Action GetOperation();
    }

    public class OperationGetter:IOperationGetter
    {
        private readonly CommandExecutor _commandExecutor;
        private readonly IConsoleExtended _console;

        public OperationGetter(IConsoleExtended console, CommandExecutor executor)
        {
            _console = console;
            _commandExecutor = executor;
        }

        public string GetOperationName()
        {
            return _console.GetMenuItemName();
        }

        public Action GetOperation()
        {
            var operationName = GetOperationName();

            return operationName switch
            {
                "add" => _commandExecutor.Add(),
                "edit" => _commandExecutor.Edit(),
                "complete" => _commandExecutor.Complete(),
                "delete" => _commandExecutor.Delete(),
                "list" => _commandExecutor.List(),
                "exit" => _commandExecutor.Exit(),
                _ => () => _commandExecutor.Error()
            };
        }
    }
}
