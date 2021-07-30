using System;

namespace ToDo
{
    public class CommandExecutor
    {
        private readonly ILogger _logger;
        private readonly ToDoList _toDoList;

        public CommandExecutor(ILogger logger, ToDoList toDoList)
        {
            _logger = logger;
            _toDoList = toDoList;
        }

        public void TryToRunCommand(string command, string[] arguments)
        {
            try
            {
                RunCommand(command, arguments);
            }
            catch (ArgumentException e)
            {
                _logger.Log(e.Message);
            }
        }

        private void RunCommand(string command, string[] arguments)
        {
            switch (command)
            {
                case ToDoCommands.DisplayToDoList:
                    HandleToDoListDisplay();
                    break;
                case ToDoCommands.AddTask:
                    var taskText = GetValidTaskText(arguments[0]);
                    HandleTaskAddition(taskText);
                    break;
                case ToDoCommands.RemoveTask:
                    var taskNumber = GetValidTaskNumber(arguments[0]);
                    HandleTaskRemove(taskNumber);
                    break;
                case ToDoCommands.ChangeTaskText:
                    taskNumber = GetValidTaskNumber(arguments[0]);
                    taskText = GetValidTaskText(arguments[1]);
                    HandleTaskTextChange(taskNumber, taskText);
                    break;
                case ToDoCommands.ToggleTaskStatus:
                    taskNumber = GetValidTaskNumber(arguments[0]);
                    HandleTaskStatusToggle(taskNumber);
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
            _toDoList.RemoveAt(taskNumber - 1);
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

        private string GetValidTaskText(string taskTextInput)
        {
            if (String.IsNullOrEmpty(taskTextInput))
                throw new ArgumentException("Нельзя добавить пустое задание");
            return taskTextInput;
        }

        private int GetValidTaskNumber(string taskNumberInput)
        {
            if (!int.TryParse(taskNumberInput, out int taskNumber))
                throw new ArgumentException("Нужно ввести число");
            if (taskNumber > _toDoList.Count || taskNumber < 1)
                throw new ArgumentException("Задания с таким номером не существует");
            return taskNumber;
        }

    }
}
