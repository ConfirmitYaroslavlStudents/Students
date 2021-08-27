using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class EditTaskCommand: ICommand
    {
        public EditTextCommand EditTextCommand;
        public ToggleStatusCommand ToggleStatusCommand;

        public List<Task> PerformCommand(List<Task> tasks)
        {
            EditTextCommand?.PerformCommand(tasks);
            ToggleStatusCommand?.PerformCommand(tasks);

            return tasks;
        }
    }
}