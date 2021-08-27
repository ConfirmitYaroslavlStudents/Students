using System.Collections.Generic;
using ToDoLibrary.CommandHandler;
using ToDoLibrary.Loggers;
using ToDoLibrary.SaveAndLoad;
using ToDoLibrary.Storages;

namespace ToDoLibrary
{
    public class ToDoApp
    {
        private readonly TasksStorage _tasksStorage = new TasksStorage();
        private readonly ISaveTasks _saver;
        private readonly HandleRunner _handleRunner;

        public ToDoApp(ILogger logger, ISaveTasks saver, ILoadTasks loader)
        {
            _saver = saver;
            _handleRunner = new HandleRunner(logger);
            _tasksStorage.Set(loader.Load());
        }

        public List<Task> ShowTasks() 
            => _tasksStorage.Get();

        public void HandleCommand(ICommandHandler commandHandler)
        {
            commandHandler.SetStorage(_tasksStorage);
            _handleRunner.Run(commandHandler);

            SaveTasks();
        }

        private void SaveTasks()
            => _saver.Save(_tasksStorage.Get());
    }
}