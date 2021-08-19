using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToDoListApp.Client
{
    public class HttpRequestGenerator
    {
        private readonly string _appPath;
        private readonly string _requestPath;

        public HttpRequestGenerator(string appPath, string requestPath)
        {
            _appPath = appPath;
            _requestPath = requestPath;
        }
        public async Task<HttpResponseMessage> GetToDoItems()
        {
            using var client = new HttpClient();
            return await client.GetAsync($"{_appPath}/{_requestPath}");
        }
        public async Task<HttpResponseMessage> GetToDoItem(long id)
        {
            using var client = new HttpClient();
            return await client.GetAsync($"{_appPath}/{_requestPath}/{id}");
        }

        public async Task<HttpResponseMessage> AddToDoItem(string description, string status = "NotDone")
        {
            using var client = new HttpClient();
            return await client.PostAsync($"{_appPath}/{_requestPath}", GetBodyForRequest(description, status));
        }

        public async Task<HttpResponseMessage> ChangeToDoItem(long id, string description, string status)
        {
            using var client = new HttpClient();
            return await client.PatchAsync($"{_appPath}/{_requestPath}/{id}", GetBodyForRequest(description, status));
        }

        public async Task<HttpResponseMessage> DeleteToDoItem(long id)
        {
            using var client = new HttpClient();
            return await client.DeleteAsync($"{_appPath}/{_requestPath}/{id}");
        }
        private StringContent GetBodyForRequest(string description, string status)
        {
            return new StringContent(JsonConvert.SerializeObject(new
            {
                Description = description,
                Status = status
            }), Encoding.UTF8, "application/json");
        }
    }
}
