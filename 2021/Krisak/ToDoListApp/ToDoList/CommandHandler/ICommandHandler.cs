using ToDoLibrary.Storages;

namespace ToDoLibrary.CommandHandler
{
    public interface ICommandHandler
    {
        public void Run();
        public void SetStorage(TasksStorage tasksStorage);
    }
}