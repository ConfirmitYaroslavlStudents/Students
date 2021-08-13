using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class EditRollbackCommand : ICommand
    {
        public int Index { get; private set; }
        public string Text { get; private set; }
        
        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks[Index].Text = Text;
            return tasks;
        }

        public void SetParameters(EditCommand command, List<Task> tasks)
        {
            Index = command.Index;
            Text = tasks[command.Index].Text;
        }
    }
}