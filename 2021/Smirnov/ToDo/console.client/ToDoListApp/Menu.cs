using System;
using System.Net;
using System.Threading.Tasks;
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

        public async Task StartMenu()
        {
            do
            {
                try
                {
                    var selectedAction = _reader.GetCommand();

                    if (selectedAction == ListCommandMenu.SaveAndExit)
                        return;

                    var action = GetAction(selectedAction);

                    if (action == null)
                    {
                        _writer.WriteIncorrectCommand();
                        return;
                    }

                    await action();

                }
                catch (Exception e)
                {
                    _writer.WriteExceptionMessage(e);
                }

            } while (_reader.ContinueWork());

        }

        private Func<Task> GetAction(ListCommandMenu selectedAction)
        {
            return selectedAction switch
            {
                ListCommandMenu.CreateTask => CreateTask,
                ListCommandMenu.DeleteTask => DeleteTask,
                ListCommandMenu.ChangeDescription => ChangeDescription,
                ListCommandMenu.CompleteTask => CompleteTask,
                ListCommandMenu.WriteTasks => WriteAllTask,
                _ => null
            };
        }

        private async Task CreateTask()
        {
            await _httpRequestGenerator.AddTask(_reader.GetDescription());
            _writer.WriteTaskCreated();
        }

        private async Task ChangeDescription()
        {
            var request = await _httpRequestGenerator.ChangeTaskDescription(_reader.GetTaskId(), _reader.GetDescription());

            if (request.StatusCode == HttpStatusCode.OK)
                _writer.WriteDescriptionChanged();
            else
            {
                if(request.StatusCode == HttpStatusCode.NotFound)
                    _writer.WriteTaskNotFound();
            }
        }

        private async Task DeleteTask()
        {
            var request = await _httpRequestGenerator.DeleteTask(_reader.GetTaskId());
            if (request.StatusCode == HttpStatusCode.OK)
                _writer.WriteTaskDeleted();
            else
            {
                if (request.StatusCode == HttpStatusCode.NotFound)
                    _writer.WriteTaskNotFound();
            }
        }

        private async Task CompleteTask()
        {
            var request = await _httpRequestGenerator.DeleteTask(_reader.GetTaskId());
            if (request.StatusCode == HttpStatusCode.OK)
                _writer.WriteTaskCompleted();
            else
            {
                if (request.StatusCode == HttpStatusCode.NotFound)
                    _writer.WriteTaskNotFound();
            }
        }

        private async Task WriteAllTask()
        {
            await _httpRequestGenerator.GetAllTask();

            _writer.WriteTasks(_httpRequestGenerator.GetAllTask().Result.Content.ReadAsStringAsync().Result);
        }
    }
}
