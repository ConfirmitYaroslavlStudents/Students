using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    public class CMDHandler
    {
        private readonly IToDoListLoaderSaver _loaderSaver;
        private readonly ILogger _logger;
        private readonly ToDoList _toDoList;
        private readonly string[] _input;
        private int _inputIndex;
        private readonly CommandHandler _commandHandler;

        public CMDHandler(IToDoListLoaderSaver loaderSaver, ILogger logger, string[] input)
        {
            _loaderSaver = loaderSaver;
            _logger = logger;
            _toDoList = _loaderSaver.Load();
            _input = input;
            _inputIndex = 0;
            _commandHandler = new CommandHandler(_loaderSaver, _logger, GetTaskTextInput, GetTaskNumberInput);
        }

        public void HandleUsersInput()
        {
            while (_inputIndex < _input.Length)
            {
                var selectedAction = _input[_inputIndex];
                _inputIndex++;
                _commandHandler.TryToRunCommand(selectedAction);
            }
            _loaderSaver.Save(_toDoList);
        }

        private string GetTaskTextInput()
        {
            var taskTextInput = _input[_inputIndex];
            _inputIndex++;
            return taskTextInput;
        }

        private string GetTaskNumberInput()
        {
            var taskNumberInput = _input[_inputIndex];
            _inputIndex++;
            return taskNumberInput;
        }
    }
}
