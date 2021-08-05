using System;
using System.Threading.Tasks;

namespace ToDoClient
{
    public class MenuInputHandler
    {
        private readonly ILogger _logger;
        private readonly IReader _reader;
        private readonly CommandExecutor _commandExecutor;

        public MenuInputHandler(ILogger logger, IReader reader, IApiClient client) 
        {
            _logger = logger;
            _reader = reader;
            _commandExecutor = new CommandExecutor(_logger, client);
        }

        public async Task HandleUsersInput()
        {
            try
            {
                await GetCommands();
            }
            catch (ArgumentException e)
            {
                _logger.Log(e.Message);
            }
        }

        private async Task GetCommands()
        {
            while (true)
            {
                var command = GetCommandChoice();

                if (command == ToDoCommands.Quit)
                    break;
                switch (command)
                {
                    case ToDoCommands.DisplayToDoList:
                        await _commandExecutor.HandleToDoListDisplay();
                        break;
                    case ToDoCommands.AddTask:
                        var taskText = InputVerifier.GetValidTaskText(GetTaskTextInput());
                        var taskStatus = InputVerifier.GetValidTaskStatus(GetTaskStatusInput());
                        await _commandExecutor.HandleTaskAddition(taskText, taskStatus);
                        break;
                    case ToDoCommands.RemoveTask:
                        var taskNumber = InputVerifier.GetValidTaskNumber(GetTaskNumberInput());
                        await _commandExecutor.HandleTaskRemove(taskNumber);
                        break;
                    case ToDoCommands.UpdateTaskStatus:
                        taskNumber = InputVerifier.GetValidTaskNumber(GetTaskNumberInput());
                        taskStatus = InputVerifier.GetValidTaskStatus(GetTaskStatusInput());
                        await _commandExecutor.HandleTaskStatusUpdate(taskNumber,taskStatus);
                        break;
                    case ToDoCommands.UpdateTaskText:
                        taskNumber = InputVerifier.GetValidTaskNumber(GetTaskNumberInput());
                        taskText = InputVerifier.GetValidTaskText(GetTaskTextInput());
                        await _commandExecutor.HandleTaskTextUpdate(taskNumber,taskText);
                        break;
                    default:
                        throw new ArgumentException("Некорректная команда");
                }
            }
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

        private string GetTaskStatusInput()
        {
            _logger.Log("Введите статус задания");
            return _reader.ReadLine();
        }
    }
}
