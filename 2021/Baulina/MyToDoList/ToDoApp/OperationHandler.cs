using InputOutputManagers;

namespace ToDoApp
{
    interface IOperationHandler
    {
        void HandleOperation(string operation);
    }

    class OperationHandler:IOperationHandler
    {
        private readonly CommandExecutor _commandExecutor;
        private readonly ErrorPrinter _errorPrinter;
        public OperationHandler(CommandExecutor executor, ErrorPrinter errorPrinter)
        {
            _commandExecutor = executor;
            _errorPrinter = errorPrinter;
        }

        public void HandleOperation(string operation)
        {
            switch (operation)
            {
                case "add":
                {
                    _commandExecutor.Add();
                    break;
                }
                case "edit":
                {_commandExecutor.Edit();
                    break;
                }
                case "complete":
                {
                    _commandExecutor.Complete();
                    break;
                }
                case "delete":
                {
                    _commandExecutor.Delete();
                    break;
                }
                case "list":
                {
                    _commandExecutor.List();
                    break;
                }
                case "exit":
                {
                    _commandExecutor.Exit();
                    break;
                }
                default:
                {
                    _errorPrinter.PrintErrorMessage();
                    break;
                }
            }
        }
    }
}
