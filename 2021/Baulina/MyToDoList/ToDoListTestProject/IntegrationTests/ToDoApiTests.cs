using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using ToDoApi.Models;
using ToDoApp;
using ToDoListTestProject.IntegrationTests.Models;
using Xunit;

namespace ToDoListTestProject.IntegrationTests
{
    public class ToDoApiTests : IDisposable
    {
        private readonly WebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly LoggerFake _loggerFake = new LoggerFake();

        public ToDoApiTests()
        {
            Environment.SetEnvironmentVariable("RawConfigProperty", "OverriddenValue");
            _factory = new WebApplicationFactory(_loggerFake);
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetWithIntParameterReturnsMethodNotAllowed()
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
        public async Task PostWithParametersAndBodyIsNotAllowed()
        {
            var response = await _client.PostAsync("todoItems/something", RequestContentHelper.GetStringContent(8));

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
            Assert.Contains("Sequence contains no matching element", _loggerFake.Message[0]);
        }

        [Fact]
        public async Task PatchWithoutParametersWithIncorrectBodyIsNotAllowed()
        {
            var response = await _client.PatchAsync("todoItems", RequestContentHelper.GetPatchStringContent(5));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PatchWithoutParametersAndBodyIsNotAllowed()
        {
            var response = await _client.PatchAsync("todoItems", null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PatchWithIncorrectParameterReturnsUnsupportedMediaType()
        {
            var response = await _client.PatchAsync("todoItems/something", null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Fact]
        public async Task GetWithCorrectParameterReturnsOk()
        {
            var response = await _client.GetAsync("todoItems");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString)!.ToList();
            Assert.Equal(_factory.ToDoList, toDoList);
        }

        [Fact]
        public async Task PostWithCorrectParameterReturnsOk()
        {
            var response = await _client.PostAsync("todoItems", RequestContentHelper.GetStringContent("Walk the dog"));
            var getResponse = await _client.GetAsync("todoItems");
            var responseString = await getResponse.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString)!.ToList();
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
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString)!.ToList();
            Assert.Single(toDoList);
        }

        [Fact]
        public async Task PatchWithCorrectBodyChangesIsCompleteAndReturnsOk()
        {
            var requestPatchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.IsComplete, true);

            var response = await _client.PatchAsync("todoItems/0", RequestContentHelper.GetPatchStringContent(requestPatchDoc));
            var getResponse = await _client.GetAsync("todoItems");
            var responseString = await getResponse.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString)!.ToList();
            Assert.True(toDoList[0].IsComplete);
            Assert.Equal("Test task", toDoList[0].Description);
        }

        [Fact]
        public async Task PatchWithCorrectBodyChangesDescriptionAndReturnsOk()
        {
            var requestPatchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.Description, "Walk the dog");

            var response = await _client.PatchAsync("todoItems/0", RequestContentHelper.GetPatchStringContent(requestPatchDoc));
            var getResponse = await _client.GetAsync("todoItems");
            var responseString = await getResponse.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var toDoList = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(responseString)!.ToList();
            Assert.Equal("Walk the dog", toDoList[0].Description);
            Assert.False(toDoList[0].IsComplete);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable("RawConfigProperty", "");
            _factory.Dispose();
        }
    }
}    
