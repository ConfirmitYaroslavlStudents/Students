using ConsoleInteractors;

namespace ToDoApp
{
    public class MenuHandler
    {
        private readonly OperationGetter _operationGetter;
        private readonly OperationHandler _operationHandler;
        public CommandExecutor CommandExecutor { get; }

        public MenuHandler(OperationGetter operationGetter, CommandExecutor executor)
        {
            _operationGetter = operationGetter;
            CommandExecutor = executor;
            _operationHandler =
                new OperationHandler(CommandExecutor, new ErrorPrinter(new ConsoleHandler(new MyConsole())));
        }

        public void Handle()
        {
            var operation = _operationGetter.GetOperationName();
            _operationHandler.HandleOperation(operation);
        }
    }
}
