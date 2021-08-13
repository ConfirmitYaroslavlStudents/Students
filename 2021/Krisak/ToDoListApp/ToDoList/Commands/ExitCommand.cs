using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class ExitCommand: ICommand
    {
        public List<Task> PerformCommand(List<Task> tasks) => tasks;
    }
}