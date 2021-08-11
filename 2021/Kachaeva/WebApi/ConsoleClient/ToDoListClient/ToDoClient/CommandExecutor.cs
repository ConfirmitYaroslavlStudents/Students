using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;

namespace ToDoClient
{
    public class CommandExecutor
    {
        private readonly IApiClient _client;
        private readonly IToDoLogger _logger;
        private readonly string _url;

        public CommandExecutor(IToDoLogger logger, IApiClient client)
        {
            _logger = logger;
            _client = client;
            _url = _client.BaseApiServiceUrl;
        }

        public async Task HandleToDoListDisplay()
        {
            var response = await _client.GetAsync(_url);
            var toDoList = (await response.Content.ReadAsAsync<IEnumerable<ToDoTask>>()).ToList<ToDoTask>();
            if (toDoList.Count == 0)
                LogResult(response, "Список пуст");
            else
            {
                var toDoString = new StringBuilder();
                foreach (var toDoTask in toDoList)
                {
                    toDoString.AppendLine(toDoTask.ToString());
                }
                LogResult(response, toDoString.ToString());
            }
        }

        public async Task HandleTaskAddition(string taskText, bool taskStatus)
        {
            var response = await _client.PostAsync(_url, GetBody(new { Text = taskText, IsDone = taskStatus }));
            var taskId = int.Parse(response.Headers.Location.Segments[2]);
            LogResult(response, $"Задание добавлено под номером {taskId}");
        }

        public async Task HandleTaskRemove(int taskId)
        {
            var response = await _client.DeleteAsync(_url + $"/{taskId}");
            VerifyIndex(response);
            LogResult(response, "Задание удалено");
        }

        public async Task HandleTaskTextUpdate(int taskId, string taskText)
        {
            var response = await _client.PatchAsync($"{_url}/{taskId}", GetBody(new { Text = taskText }));
            VerifyIndex(response);
            LogResult(response, "Текст задания обновлен");
        }

        public async Task HandleTaskStatusUpdate(int taskId, bool taskStatus)
        {
            var response = await _client.PatchAsync($"{_url}/{taskId}", GetBody(new { IsDone = taskStatus }));
            VerifyIndex(response);
            LogResult(response, "Статус задания обновлен");
        }

        private StringContent GetBody(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj),
                Encoding.UTF8, "application/json");
        }

        private void LogResult(HttpResponseMessage response, string successMessage)
        {
            if (!response.IsSuccessStatusCode)
                _logger.Log("Что-то пошло не так");
            else
                _logger.Log(successMessage);
        }

        private void VerifyIndex(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new ArgumentException("Задания с таким номером не существует");
        }
    }
}