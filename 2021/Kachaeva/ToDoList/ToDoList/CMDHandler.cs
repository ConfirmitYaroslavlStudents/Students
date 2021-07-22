using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    public class CMDHandler : CommandHandler
    {
        private readonly string[] _input;

        public CMDHandler(IToDoListLoaderSaver loaderSaver, IWriterReader writerReader, string[] input) : base(loaderSaver, writerReader)
        {
            _input = input;
        }

        public override void HandleUsersInput()
        {
            var selectedAction = _input[0];
            ExecuteCommand(selectedAction);
            _loaderSaver.Save(_toDoList);
        }

        protected override string GetTextInput()
        {
            if(_input.Length==2)
                return _input[1];
            return _input[2];
        }

        protected override string GetNumberInput()
        {
            return _input[1];
        }
    }
}
