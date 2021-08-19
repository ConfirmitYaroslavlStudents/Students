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
            return _consoleCommand[0].ToLower() switch
            {
                "create" => ListCommandMenu.CreateTask,
                "delete" => ListCommandMenu.DeleteTask,
                "change" => ListCommandMenu.ChangeDescription,
                "complete" => ListCommandMenu.CompleteTask,
                "list" => ListCommandMenu.WriteTasks,
                _ => ListCommandMenu.Exit
            };
        }

        public int GetTaskId()
        {
            return int.Parse(_consoleCommand[1]);
        }

        public string GetTaskDescription()
        {
            return _consoleCommand[^1];
        }

        public bool ContinueWork()
        {
            return false;
        }
    }
}
