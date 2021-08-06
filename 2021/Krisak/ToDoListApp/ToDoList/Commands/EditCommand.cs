using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class EditCommand: ICommand
    {
        public int Index;
        public string Text;
        public List<Task> Tasks;

        public void PerformCommand()
        {
            if (Index < 0 || Index >= Tasks.Count)
                throw new WrongEnteredCommandException($"Task not found with number {Index + 1}.");

            Tasks[Index].Text = Text;
        }
    }
}