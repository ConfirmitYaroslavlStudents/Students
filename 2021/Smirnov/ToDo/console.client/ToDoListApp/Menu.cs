using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToDoListApp.Client;
using ToDoListApp.Models;
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

                    if (selectedAction == ListCommandMenu.Exit)
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
                ListCommandMenu.WriteTasks => WriteTasks,
                _ => null
            };
        }

        private async Task CreateTask()
        {
            var request = await _httpRequestGenerator.AddToDoItem(_reader.GetTaskDescription());
            if(request.StatusCode == HttpStatusCode.Created)
                _writer.WriteTaskCreated(JsonConvert.DeserializeObject<ToDoItem>(request.Content.ReadAsStringAsync().Result));
            else
                throw new InvalidOperationException(request.ReasonPhrase);
        }

        private async Task ChangeDescription()
        {
            var toDoItem = GetToDoItem().Result;
            var request = await _httpRequestGenerator.ChangeToDoItem(toDoItem.Id, _reader.GetTaskDescription(), toDoItem.Status);

            if (request.StatusCode == HttpStatusCode.OK)
                _writer.WriteDescriptionChanged(JsonConvert.DeserializeObject<ToDoItem>(request.Content.ReadAsStringAsync().Result));
            else
                throw new InvalidOperationException(request.ReasonPhrase);
        }
        private async Task CompleteTask()
        {
            var toDoItem = GetToDoItem().Result;

            var request = await _httpRequestGenerator.ChangeToDoItem(toDoItem.Id, toDoItem.Description, "Done");

            if (request.StatusCode == HttpStatusCode.OK)
                _writer.WriteTaskCompleted(JsonConvert.DeserializeObject<ToDoItem>(request.Content.ReadAsStringAsync().Result));
            else
                throw new InvalidOperationException(request.ReasonPhrase);
        }

        private async Task DeleteTask()
        {
            var request = await _httpRequestGenerator.DeleteToDoItem(_reader.GetTaskId());
            if (request.StatusCode == HttpStatusCode.NoContent)
                _writer.WriteTaskDeleted();
            else
                throw new InvalidOperationException(request.ReasonPhrase);
        }
        private async Task WriteTasks()
        {
            var request = await _httpRequestGenerator.GetToDoItems();
            _writer.WriteTasks(request.Content.ReadAsStringAsync().Result);
        }

        private async Task<ToDoItem> GetToDoItem()
        {
            var request = await _httpRequestGenerator.GetToDoItem(_reader.GetTaskId());

            if (request.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException(request.ReasonPhrase);
            }

            return JsonConvert.DeserializeObject<ToDoItem>(request.Content.ReadAsStringAsync().Result);
        }
    }
}
