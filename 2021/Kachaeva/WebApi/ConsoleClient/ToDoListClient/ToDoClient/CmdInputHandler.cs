using System;
using System.Threading.Tasks;

namespace ToDoClient
{
    public class CmdInputHandler
    {
        private readonly IToDoLogger _logger;
        private readonly string[] _input;
        private int _inputIndex;
        private readonly CommandExecutor _commandExecutor;

        public CmdInputHandler(IToDoLogger logger, string[] input, IApiClient client)
        {
            _logger = logger;
            _input = input;
            _inputIndex = 0;
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
            catch (IndexOutOfRangeException)
            {
                _logger.Log("Некорректные аргументы");
            }
        }

        private async Task GetCommands()
        {
            while (_inputIndex < _input.Length)
            {
                var command = _input[_inputIndex];
                _inputIndex++;
                switch (command)
                {
                    case ToDoCommands.DisplayToDoList:
                        await _commandExecutor.HandleToDoListDisplay();
                        break;

                    case ToDoCommands.AddTask:
                        var taskText = InputVerifier.GetValidTaskText(_input[_inputIndex]);
                        var taskStatus = InputVerifier.GetValidTaskStatus(_input[_inputIndex + 1]);
                        _inputIndex += 2;
                        await _commandExecutor.HandleTaskAddition(taskText, taskStatus);
                        break;

                    case ToDoCommands.RemoveTask:
                        var taskId = InputVerifier.GetValidTaskId(_input[_inputIndex]);
                        _inputIndex++;
                        await _commandExecutor.HandleTaskRemove(taskId);
                        break;

                    case ToDoCommands.UpdateTaskStatus:
                        taskId = InputVerifier.GetValidTaskId(_input[_inputIndex]);
                        taskStatus = InputVerifier.GetValidTaskStatus(_input[_inputIndex + 1]);
                        _inputIndex += 2;
                        await _commandExecutor.HandleTaskStatusUpdate(taskId, taskStatus);
                        break;

                    case ToDoCommands.UpdateTaskText:
                        taskId = InputVerifier.GetValidTaskId(_input[_inputIndex]);
                        taskText = InputVerifier.GetValidTaskText(_input[_inputIndex + 1]);
                        _inputIndex += 2;
                        await _commandExecutor.HandleTaskTextUpdate(taskId, taskText);
                        break;

                    default:
                        throw new ArgumentException("Некорректная команда");
                }
            }
        }
    }
}