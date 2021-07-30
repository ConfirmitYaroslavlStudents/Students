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
        public async Task GetToDoListSuccess()
        {
            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //Assert.Equal("", response.ToString());
        }

        [Fact]
        public async Task PostTaskSuccess()
        {
            var body = GetBody("wash dishes");

            var response = await _client.PostAsync(url, body);
            //var toDoList = ?

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //Assert.Equal("1. wash dishes  [ ]\r\n", toDoList);
        }

        [Fact]
        public async Task DeleteTaskSuccess()
        {
            var body = GetBody("wash dishes");
            await _client.PostAsync(url, body);

            var response = await _client.DeleteAsync(url+"/1");
            //var toDoList = 

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //Assert.Equal("", toDoList);
        }

        [Fact]
        public async Task PatchTaskTextSuccess()
        {
            var body = GetBody("wash dishes");
            await _client.PostAsync(url, body);
            var body2 = GetBody("clean the room");

            var response = await _client.PatchAsync(url + "/1", body2);
            //var toDoList = 

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //Assert.Equal("1. wash dishes  [v]\r\n", toDoList;
        }

        [Fact]
        public async Task PatchTaskStatusSuccess()
        {
            var body = GetBody("wash dishes");
            await _client.PostAsync(url, body);
            var body2 = GetBody(true);

            var response = await _client.PatchAsync(url + "/1", body2);
            //var toDoList = 

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //Assert.Equal("1. wash dishes  [v]\r\n", toDoList;
        }
    }
}
