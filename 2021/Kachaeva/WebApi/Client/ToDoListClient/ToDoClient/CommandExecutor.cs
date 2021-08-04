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

        public async Task HandleTaskAddition(string taskText)
        {
            var response=await _client.PostAsync(Url,GetBody(taskText));
            _logger.Log("Задание добавлено");
        }

        public async Task HandleTaskRemove(int taskNumber)
        {
            await _client.DeleteAsync(Url+$"/{taskNumber}");
            _logger.Log("Задание удалено");
        }

        public async Task HandleTaskTextChange(int taskNumber, string taskText)
        {
            //Patch
        }

        public async Task HandleTaskStatusToggle(int taskNumber, bool taskStatus)
        {
            //Patch
        }
    }
}
