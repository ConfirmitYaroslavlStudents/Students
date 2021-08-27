using ToDoLibrary.Const;

namespace ToDoConsole
{
    public static class ConsoleHelper
    {
        private const string AddCommandHelp = UpdateCommands.Add + " <notes>";
        private const string EditCommandHelp = UpdateCommands.Edit + " 00 <new notes>";
        private const string ToggleCommandHelp = UpdateCommands.Toggle + " 00 <0/1/2>";
        private const string DeleteCommandHelp = UpdateCommands.Delete + " 00";
        private const string RollbackCommandHelp = UpdateCommands.Rollback + " 00";

        public static readonly string[] Help = new string[]
        {
            $"{AddCommandHelp,-30}creating new task",
            $"{EditCommandHelp,-30}edit text task",
            $"{ToggleCommandHelp,-30}toggle progress status task (0 - To do/ 1 - In progress/ 2 - Done)",
            $"{DeleteCommandHelp,-30}delete task",
            $"{RollbackCommandHelp,-30}rollback commands",
            $"{QueriesCommand.Show,-30}show all tasks",
            $"{QueriesCommand.Exit,-30}exit"
        };
    }
}