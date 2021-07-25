using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    public class CMDHandler : CommandHandler
    {
        private readonly string[] _input;
        private int _inputIndex;

        public CMDHandler(IToDoListLoaderSaver loaderSaver, ILogger logger, string[] input) : base(loaderSaver, logger)
        {
            _input = input;
            _inputIndex = 0;
        }

        public override void HandleUsersInput()
        {
            while (_inputIndex < _input.Length)
            {
                var selectedAction = _input[_inputIndex];
                _inputIndex++;
                TryToRunCommand(selectedAction);
            }
            _loaderSaver.Save(_toDoList);
        }

        protected override string GetTaskTextInput()
        {
            var taskTextInput = _input[_inputIndex];
            _inputIndex++;
            return taskTextInput;
        }

        protected override string GetTaskNumberInput()
        {
            var taskNumberInput = _input[_inputIndex];
            _inputIndex++;
            return taskNumberInput;
        }
    }
}
