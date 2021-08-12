namespace ToDoListApp.Reader
{
    public class CommandReader : IReader
    {
        private readonly string[] _consoleCommand;
        public CommandReader(string[] consoleCommand)
        {
            _consoleCommand = consoleCommand;
        }

        public ListCommandMenu GetCommand()
        {
            return _consoleCommand[0] switch
            {
                "create" => ListCommandMenu.CreateTask,
                "delete" => ListCommandMenu.DeleteTask,
                "change" => ListCommandMenu.ChangeDescription,
                "complete" => ListCommandMenu.CompleteTask,
                "list" => ListCommandMenu.WriteTasks,
                _ => ListCommandMenu.SaveAndExit
            };
        }

        public int GetTaskId()
        {
            return int.Parse(_consoleCommand[1]) - 1;
        }

        public string GetDescription()
        {
            return _consoleCommand[^1];
        }

        public bool ContinueWork()
        {
            return false;
        }
    }
}
