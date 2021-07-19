using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToDoListProject
{
    public class Controller
    {
        private IToDoListLoaderSaver _loaderSaver;
        private IWriterReader _writerReader;
        private ToDoList _toDoList;

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
                var selectedAction = GetActioinChoice();

                if (selectedAction == Menu.quit)
                {
                    _loaderSaver.Save(_toDoList);
                    break;
                }

                switch (selectedAction)
                {
                    case Menu.displayToDoList:
                        HandleToDoListDisplay();
                        break;

                    case Menu.addTask:
                        HandleTaskAddition();
                        break;

                    case Menu.removeTask:
                        HandleTaskRemove();
                        break;

                    case Menu.changeTaskText:
                        HandleTaskTextChange();
                        break;

                    case Menu.changeTaskStatus:
                        HandleTaskStatusChange();
                        break;

                    default:
                        _writerReader.Write("Некорректная команда\r\n");
                        break;
                }
            }
        }

        private string GetActioinChoice()
        {
            Menu.PrintMenu(_writerReader);
            var selectedAction = _writerReader.Read();
            _writerReader.Write("");
            return selectedAction;
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
            try
            {
                var newText = GetNewTaskText();
                _toDoList.Add(new Task(newText));
            }
            catch (ArgumentException e)
            {
                _writerReader.Write(e.Message);
            }
        }

        private void HandleTaskRemove()
        {
            try
            {
                var number = GetTaskNumber();
                _toDoList.Remove(number - 1);
            }
            catch (ArgumentException e)
            {
                _writerReader.Write(e.Message);
            }
        }

        private void HandleTaskTextChange()
        {
            try
            {
                var number = GetTaskNumber();
                var newText = GetNewTaskText();
                _toDoList[number - 1].ChangeText(newText);
            }
            catch (ArgumentException e)
            {
                _writerReader.Write(e.Message);
            }
        }

        private void HandleTaskStatusChange()
        {
            try
            {
                var number = GetTaskNumber();
                _toDoList[number - 1].ChangeStatus();
            }
            catch (ArgumentException e)
            {
                _writerReader.Write(e.Message);
            }
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
            if (!int.TryParse(_writerReader.Read(), out int number))
            {
                _writerReader.Write("");
                throw new ArgumentException("Нужно ввести число\r\n");
            }
            if (number > _toDoList.Count || number < 1)
            {
                _writerReader.Write("");
                throw new ArgumentException("Задания с таким номером не существует\r\n");
            }
            _writerReader.Write("");
            return number;
        }
    }
}
