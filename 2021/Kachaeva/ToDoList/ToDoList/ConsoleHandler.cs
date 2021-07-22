using System;

namespace ToDo
{
    public class ConsoleHandler : CommandHandler
    {
        public ConsoleHandler(IToDoListLoaderSaver loaderSaver, IWriterReader writerReader) : base(loaderSaver, writerReader) { }

        public override void HandleUsersInput()
        {
            while (true)
            {
                var selectedAction = GetActionChoice();

                if (selectedAction == Menu.Quit)
                {
                    _loaderSaver.Save(_toDoList);
                    break;
                }

                ExecuteCommand(selectedAction);
            }
        }

        private string GetActionChoice()
        {
            Menu.PrintMenu(_writerReader);
            var selectedAction = _writerReader.Read();
            _writerReader.Write("");
            return selectedAction;
        }

        protected override string GetTextInput()
        {
            _writerReader.Write("Введите текст задания");
            var newText = _writerReader.Read();
            _writerReader.Write("");
            return newText;
        }

        protected override string GetNumberInput()
        {
            _writerReader.Write("Введите номер задания");
            var number = _writerReader.Read();
            _writerReader.Write("");
            return number;
        }
    }
}
