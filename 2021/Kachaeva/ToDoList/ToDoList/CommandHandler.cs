using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    public class CommandHandler
    {
        protected readonly IToDoListLoaderSaver _loaderSaver;
        protected readonly ILogger _logger;
        protected readonly ToDoList _toDoList;
        protected Func<string> _getTaskTextInput;
        protected Func<string> _getTaskNumberInput;

        public CommandHandler(IToDoListLoaderSaver loaderSaver, ILogger logger, Func<string> getTaskTextInput, Func<string> getTaskNumberInput)
        {
            _loaderSaver = loaderSaver;
            _logger = logger;
            _toDoList = _loaderSaver.Load();
            _getTaskTextInput = getTaskTextInput;
            _getTaskNumberInput = getTaskNumberInput;
        }

        public CommandHandler(IToDoListLoaderSaver loaderSaver, ILogger logger)
        {
            _loaderSaver = loaderSaver;
            _logger = logger;
            _toDoList = _loaderSaver.Load();
        }


        public void TryToRunCommand(string selectedAction)
        {
            try
            {
                RunCommand(selectedAction);
            }
            catch (ArgumentException e)
            {
                _logger.Log(e.Message);
            }
        }

        private void RunCommand(string selectedAction)
        {
            switch (selectedAction)
            {
                case ToDoListCommands.DisplayToDoList:
                    HandleToDoListDisplay();
                    break;
                case ToDoListCommands.AddTask:
                    var taskText = GetTaskText();
                    HandleTaskAddition(taskText);
                    break;
                case ToDoListCommands.RemoveTask:
                    var taskNumber = GetTaskNumber();
                    HandleTaskRemove(taskNumber);
                    break;
                case ToDoListCommands.ChangeTaskText:
                    taskNumber = GetTaskNumber();
                    taskText = GetTaskText();
                    HandleTaskTextChange(taskNumber, taskText);
                    break;
                case ToDoListCommands.ToggleTaskStatus:
                    taskNumber = GetTaskNumber();
                    HandleTaskStatusToggle(taskNumber);
                    break;
                default:
                    HandleIncorrectCommand();
                    break;
            }
        }

        private void HandleToDoListDisplay()
        {
            if (_toDoList.Count == 0)
                throw new ArgumentException("Список пуст");
            _logger.Log(_toDoList.ToString());
        }

        private void HandleTaskAddition(string taskText)
        {
            _toDoList.Add(new Task(taskText));
            _logger.Log("Задание добавлено");
        }

        private void HandleTaskRemove(int taskNumber)
        {
            _toDoList.Remove(taskNumber - 1);
            _logger.Log("Задание удалено");
        }

        private void HandleTaskTextChange(int taskNumber, string taskText)
        {
            _toDoList[taskNumber - 1].Text = taskText;
            _logger.Log("Текст задания изменен");
        }

        private void HandleTaskStatusToggle(int taskNumber)
        {
            var task = _toDoList[taskNumber - 1];
            task.IsDone = !task.IsDone;
            _logger.Log("Статус задания изменен");
        }

        private void HandleIncorrectCommand()
        {
            throw new ArgumentException("Некорректная команда");
        }

        private string GetTaskText()
        {
            var taskTextInput = _getTaskTextInput();
            if (String.IsNullOrEmpty(taskTextInput))
                throw new ArgumentException("Нельзя добавить пустое задание");
            return taskTextInput;
        }

        private int GetTaskNumber()
        {
            var taskNumberInput = _getTaskNumberInput();
            if (!int.TryParse(taskNumberInput, out int taskNumber))
                throw new ArgumentException("Нужно ввести число");
            if (taskNumber > _toDoList.Count || taskNumber < 1)
                throw new ArgumentException("Задания с таким номером не существует");
            return taskNumber;
        }

    }
}
