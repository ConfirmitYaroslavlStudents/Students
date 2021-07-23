using InputOutputManagers;

namespace ToDoApp
{
    public class ToDoAppMenu
    {
        private readonly OperationGetter _operationGetter;
        private readonly OperationHandler _operationHandler;
        public CommandExecutor CommandExecutor { get; }

        public ToDoAppMenu(OperationGetter operationGetter, CommandExecutor commandExecutor)
        {
            _operationGetter = operationGetter;
            CommandExecutor = commandExecutor;
            _operationHandler =
                new OperationHandler(CommandExecutor, new ErrorPrinter(new InputOutputManager(new ConsoleInteractor())));
        }

        public void DoWork()
        {
            var operation = _operationGetter.GetOperationName();
            _operationHandler.HandleOperation(operation);
        }
    }
}
