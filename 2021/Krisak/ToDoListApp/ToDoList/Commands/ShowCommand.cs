using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class ShowCommand: ICommand
    {
        private IShowTasks _show;

        public ShowCommand(IShowTasks showTasks) => _show = showTasks;

        public void PerformCommand(TaskStorage repository)
        {
            _show.ShowTasks(repository.Get());
        }

        public List<Task> PerformCommand(List<Task> tasks)
        {
            _show.ShowTasks(tasks);
            return tasks;
        }
    }
}