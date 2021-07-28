using System;

namespace ToDo
{
    public class MenuCommandHandler
    {
        private readonly ILoaderSaver _loaderSaver;
        private readonly ILogger _logger;
        private readonly ToDoList _toDoList;
        private readonly IReader _reader;
        private readonly CommandHandler _commandHandler;

        public MenuCommandHandler(ILoaderSaver loaderSaver, ILogger logger, IReader reader) 
        {
            _loaderSaver = loaderSaver;
            _logger = logger;
            _toDoList = _loaderSaver.Load();
            _reader = reader;
            _commandHandler = new CommandHandler(_loaderSaver, _logger, GetTaskTextInput, GetTaskNumberInput);
        }

        public void HandleUsersInput()
        {
            while (true)
            {
                var selectedAction = GetActionChoice();

                if (selectedAction == ToDoListCommands.Quit)
                {
                    _loaderSaver.Save(_toDoList);
                    break;
                }

                _commandHandler.TryToRunCommand(selectedAction);
            }
            _loaderSaver.Save(_toDoList);

        }

        private string GetActionChoice()
        {
            _logger.Log(ToDoListCommands.GetMenu());
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
