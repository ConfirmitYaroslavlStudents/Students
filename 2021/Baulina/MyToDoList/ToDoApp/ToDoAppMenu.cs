namespace ToDoApp
{
    public class ToDoAppMenu
    {
        private readonly OperationGetter _operationGetter;
        private readonly CommandExecutor _commandExecutor;

        public ToDoAppMenu(OperationGetter operationGetter, CommandExecutor commandExecutor)
        {
            _operationGetter = operationGetter;
            _commandExecutor = commandExecutor;
        }

        public void DoWork()
        {
            var operation = _operationGetter.GetOperation();
            _commandExecutor.RunCommand(operation);
        }
    }
}
