using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public interface ICommand
    {
        public List<Task> PerformCommand(List<Task> tasks);
    }
}
