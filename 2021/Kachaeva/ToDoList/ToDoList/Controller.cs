using System;

namespace ToDoListProject
{
    public class Controller
    {
        private readonly IToDoListLoaderSaver _loaderSaver;
        private readonly IWriterReader _writerReader;
        private readonly ToDoList _toDoList;

        public Controller(IToDoListLoaderSaver loaderSaver, IWriterReader writerReader)
        {
            _loaderSaver = loaderSaver;
            _writerReader = writerReader;
            _toDoList = _loaderSaver.Load();
        }

        public void HandleUsersInput()
        {
            while (true)
            {
                var selectedAction = GetActionChoice();

                if (selectedAction == Menu.Quit)
                {
                    _loaderSaver.Save(_toDoList);
                    break;
                }

                var command = GetCommand(selectedAction);
                RunCommand(command);
            }
        }

        private string GetActionChoice()
        {
            Menu.PrintMenu(_writerReader);
            var selectedAction = _writerReader.Read();
            _writerReader.Write("");
            return selectedAction;
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
            catch(ArgumentException e)
            {
                _writerReader.Write(e.Message);
            }
        }

        private void HandleIncorrectCommand()
        {
            throw new ArgumentException("Некорректная команда\r\n");
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
            });
        }

        private void HandleTaskRemove()
        {
            RunCommand(() =>
            {
                var number = GetTaskNumber();
                _toDoList.Remove(number - 1);
            });
        }

        private void HandleTaskTextChange()
        {
            RunCommand(() =>
            {
                var number = GetTaskNumber();
                var newText = GetNewTaskText();
                _toDoList[number - 1].Text = newText;
            });
        }

        private void HandleTaskStatusChange()
        {
            RunCommand(() =>
            {
                var number = GetTaskNumber();
                var task = _toDoList[number - 1];
                task.IsDone = !task.IsDone;
            });
        }

        private string GetNewTaskText()
        {
            _writerReader.Write("Введите текст задания");
            var newText = _writerReader.Read();
            _writerReader.Write("");
            if (String.IsNullOrEmpty(newText))
                throw new ArgumentException("Нельзя добавить пустое задание\r\n");
            return newText;
        }

        private int GetTaskNumber()
        {
            _writerReader.Write("Введите номер задания");
            var input = _writerReader.Read();
            _writerReader.Write("");
            if (!int.TryParse(input, out int number))
                throw new ArgumentException("Нужно ввести число\r\n");
            if (number > _toDoList.Count || number < 1)
                throw new ArgumentException("Задания с таким номером не существует\r\n");
            return number;
        }
    }
}
