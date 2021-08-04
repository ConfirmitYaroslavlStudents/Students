using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ToDoListClient
{
    public class CommandExecutor
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _url = "https://localcost:44314/todo";

        //public CommandExecutor()
        //{
        //    _client.BaseAddress = new Uri(_url);
        //    _client.DefaultRequestHeaders.Accept.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("todo/json"));
        //}

        public StringContent GetBody(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj),
                Encoding.UTF8, "application/json");
        }

        public async void HandleToDoListDisplay()
        {
            await _client.GetAsync(_url);
        }

        public async void HandleTaskAddition(string taskText)
        {
            await _client.PostAsync(_url,GetBody(taskText));
        }

        public async void HandleTaskRemove(int taskNumber)
        {
            await _client.DeleteAsync(_url+$"/{taskNumber}");
        }

        public async void HandleTaskTextChange(int taskNumber, string taskText)
        {
            //Patch
        }

        public async void HandleTaskStatusToggle(int taskNumber, bool taskStatus)
        {
            //Patch
        }
    }
}
