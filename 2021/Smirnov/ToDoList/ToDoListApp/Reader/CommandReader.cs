namespace ToDoListApp.Reader
{
    public class CommandReader : IReader
    {
        private readonly string[] _consoleCommand;
        public CommandReader(string[] consoleCommand)
        {
            _consoleCommand = consoleCommand;
        }

        public ToDoListMenuEnum GetSelectedAction()
        {
            return _consoleCommand[0] switch
            {
                "create" => ToDoListMenuEnum.CreateTask,
                "delete" => ToDoListMenuEnum.DeleteTask,
                "change" => ToDoListMenuEnum.ChangeDescription,
                "complete" => ToDoListMenuEnum.CompleteTask,
                "list" => ToDoListMenuEnum.WriteAllTask,
                _ => ToDoListMenuEnum.SaveAndExit
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
