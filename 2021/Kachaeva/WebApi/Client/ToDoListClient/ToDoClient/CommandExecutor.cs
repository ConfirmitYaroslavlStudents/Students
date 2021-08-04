using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ToDoClient
{
    public class CommandExecutor
    {
        private readonly HttpClient _client = new HttpClient();
        private const string Url = "https://localhost:44314/todo";
        private readonly ILogger _logger;

        public CommandExecutor(ILogger logger)
        {
            _logger = logger;
        }

        public StringContent GetBody(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj),
                Encoding.UTF8, "application/json");
        }

        public async Task HandleToDoListDisplay()
        {
            var response = await _client.GetAsync(Url);
            var toDoList = await response.Content.ReadAsStringAsync();
            _logger.Log(toDoList.Length == 0 ? "Список пуст" : toDoList);
        }

        public async Task HandleTaskAddition(string taskText, bool taskStatus)
        {
            var response=await _client.PostAsync(Url,GetBody(new {Text=taskText, IsDone=taskStatus}));
            _logger.Log("Задание добавлено");
        }

        public async Task HandleTaskRemove(int taskNumber)
        {
            await _client.DeleteAsync(Url+$"/{taskNumber}");
            _logger.Log("Задание удалено");
        }

        public async Task HandleTaskTextUpdate(int taskNumber, string taskText)
        {
            await _client.PatchAsync(Url + $"/{taskNumber}", GetBody(new {Text = taskText}));
                _logger.Log("Текст задания обновлен");
        }

        public async Task HandleTaskStatusUpdate(int taskNumber, bool taskStatus)
        {
            await _client.PatchAsync(Url + $"/{taskNumber}", GetBody(new{IsDone=taskStatus}));
            _logger.Log("Статус задания обновлен");
        }
    }
}
