using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class ShowCommand: ICommand
    {
        public List<Task> Tasks;
        public void PerformCommand()
        {
            ShowInformationToUser.ShowAllNotesInConsole(Tasks);
        }
    }
}