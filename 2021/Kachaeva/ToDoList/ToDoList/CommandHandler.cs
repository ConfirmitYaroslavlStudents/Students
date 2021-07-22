using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    public abstract class CommandHandler
    {
        protected readonly IToDoListLoaderSaver _loaderSaver;
        protected readonly IWriterReader _writerReader;
        protected readonly ToDoList _toDoList;

        public CommandHandler(IToDoListLoaderSaver loaderSaver, IWriterReader writerReader)
        {
            _loaderSaver = loaderSaver;
            _writerReader = writerReader;
            _toDoList = _loaderSaver.Load();
        }

        public abstract void HandleUsersInput();

        protected void ExecuteCommand(string selectedAction)
        {
            var command = GetCommand(selectedAction);
            RunCommand(command);
        }

        private Action GetCommand(string selectedAction)
        {
            return selectedAction switch
            {
                Menu.DisplayToDoList => HandleToDoListDisplay,
                Menu.AddTask => HandleTaskAddition,
                Menu.RemoveTask => HandleTaskRemove,
                Menu.ChangeTaskText => HandleTaskTextChange,
                Menu.ChangeTaskStatus => HandleTaskStatusChange,
                _ => HandleIncorrectCommand
            };
        }

        private void RunCommand(Action command)
        {
            try
            {
                command();
            }
            catch (ArgumentException e)
            {
                _writerReader.Write(e.Message);
            }
        }

        private void HandleToDoListDisplay()
        {
            if (_toDoList.Count == 0)
                _writerReader.Write("Список пуст\r\n");
            else
                _writerReader.Write(_toDoList.ToString());
        }

        private void HandleTaskAddition()
        {
            RunCommand(() =>
            {
                var newText = GetNewTaskText();
                _toDoList.Add(new Task(newText));
                _writerReader.Write("Задание добавлено\r\n");
            });
        }

        private void HandleTaskRemove()
        {
            RunCommand(() =>
            {
                var number = GetTaskNumber();
                _toDoList.Remove(number - 1);
                _writerReader.Write("Задание удалено\r\n");
            });
        }

        private void HandleTaskTextChange()
        {
            RunCommand(() =>
            {
                var number = GetTaskNumber();
                var newText = GetNewTaskText();
                _toDoList[number - 1].Text = newText;
                _writerReader.Write("Текст задания изменен\r\n");
            });
        }

        private void HandleTaskStatusChange()
        {
            RunCommand(() =>
            {
                var number = GetTaskNumber();
                var task = _toDoList[number - 1];
                task.IsDone = !task.IsDone;
                _writerReader.Write("Статус задания изменен\r\n");
            });
        }

        private void HandleIncorrectCommand()
        {
            throw new ArgumentException("Некорректная команда\r\n");
        }

        private string GetNewTaskText()
        {
            var newText = GetTextInput();
            if (String.IsNullOrEmpty(newText))
                throw new ArgumentException("Нельзя добавить пустое задание\r\n");
            return newText;
        }
        private int GetTaskNumber()
        {
            var input = GetNumberInput();
            if (!int.TryParse(input, out int number))
                throw new ArgumentException("Нужно ввести число\r\n");
            if (number > _toDoList.Count || number < 1)
                throw new ArgumentException("Задания с таким номером не существует\r\n");
            return number;
        }

        protected abstract string GetTextInput();

        protected abstract string GetNumberInput();
    }
}
