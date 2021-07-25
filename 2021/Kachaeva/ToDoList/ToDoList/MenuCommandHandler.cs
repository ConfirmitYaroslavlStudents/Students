using System;

namespace ToDo
{
    public class MenuCommandHandler : CommandHandler
    {
        private readonly IReader _reader;

        public MenuCommandHandler(IToDoListLoaderSaver loaderSaver, ILogger logger, IReader reader) : base(loaderSaver, logger)
        {
            _reader = reader;
        }

        public override void HandleUsersInput()
        {
            while (true)
            {
                var selectedAction = GetActionChoice();

                if (selectedAction == ToDoListCommands.Quit)
                {
                    _loaderSaver.Save(_toDoList);
                    break;
                }

                TryToRunCommand(selectedAction);
            }
            _loaderSaver.Save(_toDoList);

        }

        private string GetActionChoice()
        {
            _logger.Log(ToDoListCommands.GetMenu());
            var selectedAction = _reader.ReadLine();
            return selectedAction;
        }

        protected override string GetTaskTextInput()
        {
            _logger.Log("Введите текст задания");
            return _reader.ReadLine();
        }

        protected override string GetTaskNumberInput()
        {
            _logger.Log("Введите номер задания");
            return _reader.ReadLine();
        }
    }
}
