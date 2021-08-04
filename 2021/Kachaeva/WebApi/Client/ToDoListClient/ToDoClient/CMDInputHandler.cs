using System;
using  System.Threading.Tasks;

namespace ToDoClient
{
    public class CMDInputHandler
    {
        private readonly ILogger _logger;
        private readonly string[] _input;
        private int _inputIndex;
        private readonly CommandExecutor _commandExecutor;

        public CMDInputHandler(ILogger logger, string[] input)
        {
            _logger = logger;
            _input = input;
            _inputIndex = 0;
            _commandExecutor = new CommandExecutor(_logger);
        }

        public async Task HandleUsersInput()
        {
            try
            {
                await GetCommands();
            }
            catch(ArgumentException e)
            {
                _logger.Log(e.Message);
            }
            catch(IndexOutOfRangeException)
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
                switch(command)
                {
                    case ToDoCommands.DisplayToDoList:
                        await _commandExecutor.HandleToDoListDisplay();
                        break;
                    case ToDoCommands.AddTask:
                        var taskText = InputVerifier.GetValidTaskText(_input[_inputIndex]);
                        _inputIndex++;
                        await _commandExecutor.HandleTaskAddition(taskText);
                        break;
                    case ToDoCommands.RemoveTask:
                        var taskNumber = InputVerifier.GetValidTaskNumber(_input[_inputIndex]);
                        _inputIndex++;
                        await _commandExecutor.HandleTaskRemove(taskNumber);
                        break;
                    case ToDoCommands.ToggleTaskStatus:
                        taskNumber = InputVerifier.GetValidTaskNumber(_input[_inputIndex]);
                        var taskStatus = InputVerifier.GetValidTaskStatus(_input[_inputIndex + 1]);
                        _inputIndex+=2;
                        await _commandExecutor.HandleTaskStatusToggle(taskNumber, taskStatus);
                        break;
                    case ToDoCommands.ChangeTaskText:
                        taskNumber = InputVerifier.GetValidTaskNumber(_input[_inputIndex]);
                        taskText = InputVerifier.GetValidTaskText(_input[_inputIndex + 1]);
                        _inputIndex+=2;
                        await _commandExecutor.HandleTaskTextChange(taskNumber,taskText);
                        break;
                    default:
                        throw new ArgumentException("Некорректная команда");
                }
            }
        }
    }
}
