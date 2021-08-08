using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using ToDoApi;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using ToDoApiDependencies;
using System.Collections.Generic;
using System.Linq;


namespace ToDoApiTests
{
    [TestClass]
    public class RequestTests
    {
        private readonly HttpClient _client;
        private const string Url = "todo";

        public RequestTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
            File.Delete("ToDoList.txt");
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("ToDoList.txt");
        }

        public StringContent GetBody(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj),
                Encoding.UTF8, "application/json");
        }

        [TestMethod]
        public async Task GetToDoListSuccess()
        {
            var response = await _client.GetAsync(Url);
            var toDoList=await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("[]", toDoList);
        }

        [TestMethod]
        public async Task PostTaskSuccess()
        {
            var body = GetBody(new { Text = "wash dishes", IsDone = false });

            var response = await _client.PostAsync(Url, body);
            var getResponse = await _client.GetAsync(Url);
            var toDoList = (await getResponse.Content.ReadAsAsync<IEnumerable<ToDoTask>>()).ToList();

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(1, toDoList.Count);
            Assert.AreEqual("1. wash dishes  [ ]", toDoList[0].ToString());
        }

        [TestMethod]
        public async Task DeleteTaskSuccess()
        {
            var body = GetBody(new { Text = "wash dishes", IsDone = false });
            await _client.PostAsync(Url, body);

            var response = await _client.DeleteAsync(Url + "/1");
            var getResponse = await _client.GetAsync(Url);
            var toDoList = (await getResponse.Content.ReadAsAsync<IEnumerable<ToDoTask>>()).ToList();

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Assert.AreEqual(0, toDoList.Count);
        }

        [TestMethod]
        public async Task PatchTaskTextSuccess()
        {
            var body = GetBody(new { Text = "wash dishes", IsDone = false });
            await _client.PostAsync(Url, body);
            var body2 = GetBody(new { Text = "clean the room" });

            var response = await _client.PatchAsync(Url + "/1", body2);
            var getResponse = await _client.GetAsync(Url);
            var toDoList = (await getResponse.Content.ReadAsAsync<IEnumerable<ToDoTask>>()).ToList();

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Assert.AreEqual(1, toDoList.Count);
            Assert.AreEqual("1. clean the room  [ ]", toDoList[0].ToString());
        }

        [TestMethod]
        public async Task PatchTaskStatusSuccess()
        {
            var body = GetBody(new { Text = "wash dishes", IsDone = false });
            await _client.PostAsync(Url, body);
            var body2 = GetBody(new { IsDone = true });

            var response = await _client.PatchAsync(Url + "/1", body2);
            var getResponse = await _client.GetAsync(Url);
            var toDoList = (await getResponse.Content.ReadAsAsync<IEnumerable<ToDoTask>>()).ToList();

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Assert.AreEqual(1, toDoList.Count);
            Assert.AreEqual("1. wash dishes  [v]", toDoList[0].ToString());
        }
    }
}
