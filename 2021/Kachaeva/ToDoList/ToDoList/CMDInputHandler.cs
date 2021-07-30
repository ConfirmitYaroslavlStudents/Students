using System;

namespace ToDo
{
    public class CMDInputHandler
    {
        private readonly ILoaderAndSaver _loaderAndSaver;
        private readonly ILogger _logger;
        private readonly ToDoList _toDoList;
        private readonly string[] _input;
        private int _inputIndex;
        private readonly CommandExecutor _commandExecutor;

        public CMDInputHandler(ILoaderAndSaver loaderAndSaver, ILogger logger, string[] input)
        {
            _loaderAndSaver = loaderAndSaver;
            _logger = logger;
            _toDoList = _loaderAndSaver.Load();
            _input = input;
            _inputIndex = 0;
            _commandExecutor = new CommandExecutor(_logger, _toDoList);
        }

        public void HandleUsersInput()
        {
            try
            {
                GetCommands();
            }
            catch(ArgumentException)
            {
                _logger.Log("Некорректная команда");
            }
            catch(IndexOutOfRangeException)
            {
                _logger.Log("Некорректные аргументы");
            }
        }

        private void GetCommands()
        {
            while (_inputIndex < _input.Length)
            {
                var command = _input[_inputIndex];
                _inputIndex++;
                switch(command)
                {
                    case ToDoCommands.DisplayToDoList:
                        _commandExecutor.TryToRunCommand(command, new string[] { });
                        break;
                    case ToDoCommands.AddTask:
                    case ToDoCommands.RemoveTask:
                    case ToDoCommands.ToggleTaskStatus:
                        _commandExecutor.TryToRunCommand(command, new string[] { _input[_inputIndex] });
                        _inputIndex++;
                        break;
                    case ToDoCommands.ChangeTaskText:
                        _commandExecutor.TryToRunCommand(command, new string[] { _input[_inputIndex], _input[_inputIndex + 1] });
                        _inputIndex+=2;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            _loaderAndSaver.Save(_toDoList);
        }
    }
}
