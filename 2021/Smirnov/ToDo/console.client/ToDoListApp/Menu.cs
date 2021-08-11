using System;
using ToDoListApp.Client;
using ToDoListApp.Reader;
using ToDoListApp.Writer;

namespace ToDoListApp
{
    public class Menu
    {
        private readonly IReader _reader;
        private readonly IWriter _writer;
        private readonly HttpRequestGenerator _httpRequestGenerator;

        public Menu(HttpRequestGenerator httpRequestGenerator, IReader reader, IWriter writer)
        {
            _httpRequestGenerator = httpRequestGenerator;
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
            _httpRequestGenerator.AddTask(_reader.GetDescription());
            _writer.WriteTaskCreated();
        }

        private void ChangeDescription()
        {
            var request = _httpRequestGenerator.ChangeTaskDescription(_reader.GetTaskId(), _reader.GetDescription());

            Console.WriteLine(request.Status.ToString());
            _writer.WriteDescriptionChanged();
        }

        private void DeleteTask()
        {
            _httpRequestGenerator.DeleteTask(_reader.GetTaskId());
            _writer.WriteTaskDeleted();
        }

        private void CompleteTask()
        {
            _httpRequestGenerator.CompleteTask(_reader.GetTaskId());
            _writer.WriteTaskCompleted();
        }

        private void WriteAllTask()
        {
            _httpRequestGenerator.GetAllTask();

            _writer.WriteAllTask(_httpRequestGenerator.GetAllTask().Result.Content.ReadAsStringAsync().Result);
        }
    }
}
