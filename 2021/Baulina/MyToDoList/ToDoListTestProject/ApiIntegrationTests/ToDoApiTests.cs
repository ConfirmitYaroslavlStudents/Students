using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MyToDoList;
using Newtonsoft.Json;
using Xunit;
using System.Linq;
using System.Text;

namespace ToDoListTestProject.ApiIntegrationTests
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
    
    public class ToDoApiTests : IDisposable
    {
        private readonly WebApplicationFactory _factory;
        private readonly HttpClient _client;

        public ToDoApiTests()
        {
            Environment.SetEnvironmentVariable("RawConfigProperty", "OverriddenValue");
            _factory = new WebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetWithIntParameterReturnsBadRequest()
        {
            var response = await _client.GetAsync("todoItems/8");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task GetWithStringParameterReturnsBadRequest()
        {
            var response = await _client.GetAsync("todoItems/something");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PostWithIncorrectBodyReturnsBadRequest()
        {
            var response = await _client.PostAsync("todoItems", ContentHelper.GetStringContent(8));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostWithParametersAndBodyIsNotAllowed()
        {
            var response = await _client.PostAsync("todoItems/something", ContentHelper.GetStringContent(8));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PostWithParametersIsNotAllowed()
        {
            var response = await _client.PostAsync("todoItems/something", null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithIncorrectParametersReturnsBadRequest()
        {
            var response = await _client.DeleteAsync("todoItems/something");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithoutParametersIsNotAllowed()
        {
            var response = await _client.DeleteAsync("todoItems");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithOutOfRangeParametersReturnsNotFound()
        {
            var response = await _client.DeleteAsync("todoItems/-1");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutWithoutParametersWithIncorrectBodyReturnsBadRequest()
        {
            var response = await _client.PutAsync("todoItems", ContentHelper.GetStringContent(5));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutWithoutParametersAndBodyReturnsUnsupportedMediaType()
        {
            var response = await _client.PutAsync("todoItems", null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Fact]
        public async Task PutWithIncorrectParameterReturnsBadRequest()
        {
            var response = await _client.PutAsync("todoItems/something", null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetWithCorrectParameterReturnsOk()
        {
            var response = await _client.GetAsync("todoItems");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString).ToList();
            Assert.Equal(_factory.ToDoList, toDoList);
        }

        [Fact]
        public async Task PostWithCorrectParameterReturnsOk()
        {
            var response = await _client.PostAsync("todoItems", ContentHelper.GetStringContent("Walk the dog"));
            var getResponse = await _client.GetAsync("todoItems");
            var responseString = await getResponse.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString).ToList();
            Assert.Contains(toDoList[2].Description, "Walk the dog");
        }

        [Fact]
        public async Task DeleteWithCorrectParameterReturnsOk()
        {
            var response = await _client.DeleteAsync("todoItems/0");
            var getResponse = await _client.GetAsync("todoItems");
            var responseString = await getResponse.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString).ToList();
            Assert.Single(toDoList);
        }

        [Fact]
        public async Task PutWithCorrectBodyReturnsOk()
        {
            var requestBody = new
            {
                Index = 0,
                NewDescription = "Walk the dog"

            };

            var response = await _client.PutAsync("todoItems", ContentHelper.GetStringContent(requestBody));
            var getResponse = await _client.GetAsync("todoItems");
            var responseString = await getResponse.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString).ToList();
            Assert.Equal("Walk the dog", toDoList[0].Description);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable("RawConfigProperty", "");
            _factory?.Dispose();
        }
    }
}    
