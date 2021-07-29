using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using ToDoApi;
using System.Text;
using Newtonsoft.Json;

namespace ToDoApiTests
{
    public class RequestTests
    {
        private readonly HttpClient _client;
        private const string url = "todo";

        public RequestTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        public StringContent GetBody(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj),
                Encoding.UTF8, "application/json");
        }

        [Fact]
        public async Task GetToDoList()
        {
            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostTask()
        {
            var body = GetBody("wash dishes");

            var response = await _client.PostAsync(url, body);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTask()
        {
            var response = await _client.DeleteAsync(url+"/1");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PutTaskText()
        {
            var body = GetBody(new { TaskNumber = 1, TaskText = "clean the room" });

            var response = await _client.PutAsync(url, body);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("PUT", 1)]
        public async Task PutTaskStatus(string method, int? taskNumber = null)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"https://localhost:44329/todo/ {taskNumber}");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
