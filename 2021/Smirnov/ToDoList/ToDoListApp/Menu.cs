using System;
using ToDoListApp.Reader;
using ToDoListApp.Writer;
using ToDoListLib.Controllers;
using ToDoListLib.Models;

namespace ToDoListApp
{
    public class Menu
    {
        private readonly IReader _reader;
        private readonly IWriter _writer;
        private readonly ToDoListController _toDoListController;

        public Menu(ToDoListController toDoListController, IReader reader, IWriter writer)
        {
            _toDoListController = toDoListController;
            _reader = reader;
            _writer = writer;
        }

        public void StartMenu()
        {
            do
            {
                try
                {
                    var selectedAction = _reader.GetSelectedAction();

                    if (selectedAction == ToDoListMenuEnum.SaveAndExit)
                        return;

                    var action = GetAction(selectedAction);

                    if (action == null)
                    {
                        _writer.WriteIncorrectCommand();
                        return;
                    }

                    action();
                }
                catch (Exception e)
                {
                    _writer.WriteExceptionMessage(e);
                }

            } while (ContinueWork());

        }
        private bool ContinueWork()
        {
            return _reader.ContinueWork();
        }

        private Action GetAction(ToDoListMenuEnum selectedAction)
        {
            return selectedAction switch
            {
                ToDoListMenuEnum.CreateTask => CreateTask,
                ToDoListMenuEnum.DeleteTask => DeleteTask,
                ToDoListMenuEnum.ChangeDescription => ChangeDescription,
                ToDoListMenuEnum.CompleteTask => CompleteTask,
                ToDoListMenuEnum.WriteAllTask => WriteAllTask,
                _ => null
            };
        }

        private void CreateTask()
        {
            _toDoListController.AddTask(new Task{Description = _reader.GetDescription()});
            _writer.WriteTaskCreated();
        }

        private void ChangeDescription()
        {
            _toDoListController.ChangeDescription(_reader.GetTaskId(), _reader.GetDescription());
            _writer.WriteDescriptionChanged();
        }

        private void DeleteTask()
        {
            _toDoListController.DeleteTask(_reader.GetTaskId());
            _writer.WriteTaskDeleted();
        }

        private void CompleteTask()
        {
            _toDoListController.ChangeTaskStatus(_reader.GetTaskId(), TaskStatus.Done);
            _writer.WriteTaskCompleted();
        }

        private void WriteAllTask()
        {
            _writer.WriteAllTask(_toDoListController.GetToDoList());
        }
    }
}
