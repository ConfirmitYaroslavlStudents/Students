using ToDoLibrary.CommandCreator;
using ToDoLibrary.Storages;

namespace ToDoLibrary.CommandHandler
{
    public class ConsoleCommandHandler : ICommandHandler
    {
        private  TasksStorage _tasksStorage = new TasksStorage();
        private readonly RollbacksStorage _rollbacksStorage;
        private readonly string[] _userInput;

        public ConsoleCommandHandler(RollbacksStorage rollbacksStorage, string[] userInput)
        {
            _rollbacksStorage = rollbacksStorage;
            _userInput = userInput;
        }
        public void SetStorage(TasksStorage tasksStorage)
            => _tasksStorage = tasksStorage;

        public void Run()
        {
            if (_userInput.Length == 0)
                throw new WrongEnteredCommandException("Empty command");

            var command = GetChainOfCreators().GetCommand();

            switch (command)
            {
                case null:
                    throw new WrongEnteredCommandException("Unknown command.");
                default:
                    var tasks = command.PerformCommand(_tasksStorage.Get());

                    _rollbacksStorage.TryPush(command,_tasksStorage.Get());

                    _tasksStorage.Set(tasks);
                    return;
            }
        }

        private ICreator GetChainOfCreators()
        {
            var addCreator = new AddCommandCreator(_userInput);
            var editTextCreator = new EditTextCommandCreator(_userInput);
            var toggleCreator = new ToggleCommandCreator(_userInput);
            var deleteCreator = new DeleteCommandCreator(_userInput);
            var rollbackCreator = new StartRollbackCommandCreator(_rollbacksStorage,_userInput);

            addCreator.SetNext(editTextCreator).SetNext(toggleCreator).SetNext(deleteCreator).SetNext(rollbackCreator);

            return addCreator;
        }
    }
}