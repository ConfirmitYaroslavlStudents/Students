using ToDoLibrary.Commands;
using ToDoLibrary.Storages;

namespace ToDoLibrary.CommandHandler
{
    public class WebCommandHandler: ICommandHandler
    {
        private  TasksStorage _tasksStorage;
        private readonly ICommand _command;

        public WebCommandHandler(ICommand command)
            => _command = command;

        public void SetStorage(TasksStorage tasksStorage)
            => _tasksStorage = tasksStorage;

        public void Run()
        {
            switch (_command)
            {
                case null:
                    throw new WrongEnteredCommandException("Unknown command.");
                default:
                    var tasks = _command.PerformCommand(_tasksStorage.Get());
                    _tasksStorage.Set(tasks);
                    return;
            }
        }
    }
}