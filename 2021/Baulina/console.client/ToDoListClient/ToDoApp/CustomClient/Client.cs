using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Options;
using ToDoApp.Models;
using ToDoApp.Settings;

namespace ToDoApp.CustomClient
{
    public class Client
    {
        private readonly HttpClient _client;
        private readonly string _requestUri;

        public Client(HttpClient client)
        {
            _client = client;
        }

        public Client(IOptions<ClientSettingsConfiguration> config)
        {
            var configuration = config;
            _client = new HttpClient {BaseAddress = new Uri(configuration.Value.Parameters.ApplicationUrl)};
            _requestUri = configuration.Value.Parameters.RequestUri;
        }

        public Task<HttpResponseMessage> Get()
        {
            return _client.GetAsync(_requestUri);
        }

        public Task<HttpResponseMessage> GetItemsStartingWith(string prefix)
        {
            return _client.GetAsync($"{_requestUri}/{prefix}");
        }

        public Task<HttpResponseMessage> GetToDoItem(int taskId)
        {
            return _client.GetAsync($"{_requestUri}/{taskId}");
        }

        public Task<HttpResponseMessage> Post(ToDoItem toDoItem)
        {
            return _client.PostAsync(_requestUri, RequestContentHelper.GetStringContent(toDoItem));
        }

        public Task<HttpResponseMessage> Patch(int taskId, JsonPatchDocument<ToDoItem> patchDocument)
        {
            return _client.PatchAsync($"{_requestUri}/{taskId}",
                RequestContentHelper.GetPatchStringContent(patchDocument));
        }
        public Task<HttpResponseMessage> Delete(int taskId)
        {
            return _client.DeleteAsync($"{_requestUri}/{taskId}");
        }

    }
}
