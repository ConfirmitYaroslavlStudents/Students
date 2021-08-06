using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class AddRollbackCommand: ICommand
    {
        public int Index;
        public Task @Task;
        public List<Task> Tasks;

        public void PerformCommand()
        {
            if (Index < 0 || Index > Tasks.Count)
                throw new WrongEnteredCommandException($"Task not found with number {Index + 1}.");

            Tasks.Insert(Index, Task);
        }
    }
}