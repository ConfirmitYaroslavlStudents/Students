using ToDoLibrary.CommandCreator;
using ToDoLibrary.Storages;

namespace ToDoLibrary.CommandHandler
{
    public class CmdCommandHandler : ICommandHandler
    {
        private  TasksStorage _tasksStorage;
        private readonly string[] _userInput;

        public CmdCommandHandler(string[] userInput)
        => _userInput = userInput;

        public void SetStorage(TasksStorage tasksStorage)
            => _tasksStorage = tasksStorage;

        public void Run()
        { 
            var command = GetChainOfCreators(_userInput).GetCommand();

            switch (command)
            {
                case null:
                    throw new WrongEnteredCommandException("Unknown command.");
                default:
                    var tasks = command.PerformCommand(_tasksStorage.Get());
                    _tasksStorage.Set(tasks);
                    return;
            }
        }

        private ICreator GetChainOfCreators(string[] userInput)
        {
            var addCreator = new AddCommandCreator(userInput);
            var editCreator = new EditTextCommandCreator(userInput);
            var toggleCreator = new ToggleCommandCreator(userInput);
            var deleteCreator = new DeleteCommandCreator(userInput);

            addCreator.SetNext(editCreator).SetNext(toggleCreator).SetNext(deleteCreator);

            return addCreator;
        }
    }
}