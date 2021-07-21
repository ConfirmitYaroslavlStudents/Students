namespace ToDoListConsole
{
    public class CmdReader : IReader
    {
        private readonly string[] _consoleCommand;
        public CmdReader(string[] consoleCommand)
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

        public int GetNumberTask()
        {
            return int.Parse(_consoleCommand[1]) - 1;
        }

        public string GetDescription()
        {
            return _consoleCommand[^1];
        }
    }
}
