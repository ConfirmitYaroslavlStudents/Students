using System;
using System.Threading.Tasks;

namespace ToDoClient
{
    public class MenuInputHandler
    {
        private readonly IToDoLogger _logger;
        private readonly IToDoReader _reader;
        private readonly CommandExecutor _commandExecutor;

        public MenuInputHandler(IToDoLogger logger, IToDoReader reader, IApiClient client)
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
                        var taskId = InputVerifier.GetValidtaskId(GettaskIdInput());
                        await _commandExecutor.HandleTaskRemove(taskId);
                        break;

                    case ToDoCommands.UpdateTaskStatus:
                        taskId = InputVerifier.GetValidtaskId(GettaskIdInput());
                        taskStatus = InputVerifier.GetValidTaskStatus(GetTaskStatusInput());
                        await _commandExecutor.HandleTaskStatusUpdate(taskId, taskStatus);
                        break;

                    case ToDoCommands.UpdateTaskText:
                        taskId = InputVerifier.GetValidtaskId(GettaskIdInput());
                        taskText = InputVerifier.GetValidTaskText(GetTaskTextInput());
                        await _commandExecutor.HandleTaskTextUpdate(taskId, taskText);
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

        private string GettaskIdInput()
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