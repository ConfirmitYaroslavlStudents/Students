using System;

namespace ToDo
{
    public class MenuInputHandler
    {
        private readonly ILoaderAndSaver _loaderAndSaver;
        private readonly ILogger _logger;
        private readonly IReader _reader;
        private readonly ToDoList _toDoList;
        private readonly CommandExecutor _commandExecutor;

        public MenuInputHandler(ILoaderAndSaver loaderAndSaver, ILogger logger, IReader reader) 
        {
            _loaderAndSaver = loaderAndSaver;
            _logger = logger;
            _reader = reader;
            _toDoList = _loaderAndSaver.Load();
            _commandExecutor = new CommandExecutor(_logger,_toDoList);
        }

        public void HandleUsersInput()
        {
            try
            {
                GetCommands();
            }
            catch (ArgumentException)
            {
                _logger.Log("Некорректная команда");
            }
        }

        private void GetCommands()
        {
            while (true)
            {
                var command = GetCommandChoice();

                if (command == ToDoCommands.Quit)
                {
                    _loaderAndSaver.Save(_toDoList);
                    break;
                }
                switch (command)
                {
                    case ToDoCommands.DisplayToDoList:
                        _commandExecutor.TryToRunCommand(command, new string[] { });
                        break;
                    case ToDoCommands.AddTask:
                        _commandExecutor.TryToRunCommand(command, new string[] { GetTaskTextInput() });
                        break;
                    case ToDoCommands.RemoveTask:
                    case ToDoCommands.ToggleTaskStatus:
                        _commandExecutor.TryToRunCommand(command, new string[] { GetTaskNumberInput() });
                        break;
                    case ToDoCommands.ChangeTaskText:
                        _commandExecutor.TryToRunCommand(command, new string[] { GetTaskNumberInput(), GetTaskTextInput() });
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            _loaderAndSaver.Save(_toDoList);
        }

        private string GetCommandChoice()
        {
            _logger.Log(ToDoCommands.GetMenu());
            var selectedAction = _reader.ReadLine();
            return selectedAction;
        }

        private string GetTaskTextInput()
        {
            _logger.Log("Введите текст задания");
            return _reader.ReadLine();
        }

        private string GetTaskNumberInput()
        {
            _logger.Log("Введите номер задания");
            return _reader.ReadLine();
        }
    }
}
