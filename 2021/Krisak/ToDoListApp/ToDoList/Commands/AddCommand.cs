using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class AddCommand: ICommand
    {
        public Task NewTask;
        public List<Task> Tasks;

        public void PerformCommand()
        {
            Tasks.Add(NewTask);
        }
    }
}