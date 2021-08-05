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
        private readonly IApiClient _client;
        private const string Url = "https://localhost:44314/todo";
        private readonly ILogger _logger;

        public CommandExecutor(ILogger logger, IApiClient client)
        {
            _logger = logger;
            _client = client;
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
            LogResult(response, toDoList.Length == 0 ? "Список пуст" : toDoList);
        }

        public async Task HandleTaskAddition(string taskText, bool taskStatus)
        {
            var response = await _client.PostAsync(Url,GetBody(new {Text=taskText, IsDone=taskStatus}));
            LogResult(response,"Задание добавлено");
        }

        public async Task HandleTaskRemove(int taskNumber)
        {
            var response = await _client.DeleteAsync(Url+$"/{taskNumber}");
            LogResult(response,"Задание удалено");
        }

        public async Task HandleTaskTextUpdate(int taskNumber, string taskText)
        {
            var response = await _client.PatchAsync(Url + $"/{taskNumber}", GetBody(new {Text = taskText}));
            LogResult(response, "Текст задания обновлен");
        }

        public async Task HandleTaskStatusUpdate(int taskNumber, bool taskStatus)
        {
            var response = await _client.PatchAsync(Url + $"/{taskNumber}", GetBody(new{IsDone=taskStatus}));
            LogResult(response, "Статус задания обновлен");
        }

        private void LogResult(HttpResponseMessage response, string successMessage)
        {
            if (!response.IsSuccessStatusCode)
                _logger.Log("Что-то пошло не так");
            else
                _logger.Log(successMessage);
        }
    }
}
