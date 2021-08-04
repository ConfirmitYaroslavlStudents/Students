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
            Assert.AreEqual("", toDoList);
        }

        [TestMethod]
        public async Task PostTaskSuccess()
        {
            var body = GetBody("wash dishes");

            var response = await _client.PostAsync(Url, body);
            var getResponse = await _client.GetAsync(Url);
            var toDoList=await getResponse.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", toDoList);
        }

        [TestMethod]
        public async Task DeleteTaskSuccess()
        {
            var body = GetBody("wash dishes");
            await _client.PostAsync(Url, body);

            var response = await _client.DeleteAsync(Url+"/1");
            var getResponse = await _client.GetAsync(Url);
            var toDoList=await getResponse.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("", toDoList);
        }

        //[TestMethod]
        //public async Task PatchTaskTextSuccess()
        //{
        //    var body = GetBody("wash dishes");
        //    await _client.PostAsync(Url, body);
        //    var body2 = GetBody("clean the room");

        //    var response = await _client.PatchAsync(Url + "/1", body2);
        //    var getResponse = await _client.GetAsync(Url);
        //    var toDoList=await getResponse.Content.ReadAsStringAsync();

        //    response.EnsureSuccessStatusCode();
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //    Assert.AreEqual("1. clean  the room  [ ]\r\n", toDoList);
        //}

        //[TestMethod]
        //public async Task PatchTaskStatusSuccess()
        //{
        //    var body = GetBody("wash dishes");
        //    await _client.PostAsync(Url, body);
        //    var body2 = GetBody(true);

        //    var response = await _client.PatchAsync(Url + "/1", body2);
        //    var getResponse = await _client.GetAsync(Url);
        //    var toDoList=await getResponse.Content.ReadAsStringAsync();

        //    response.EnsureSuccessStatusCode();
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //    Assert.AreEqual("1. wash dishes  [v]\r\n", toDoList);
        //}
    }
}
