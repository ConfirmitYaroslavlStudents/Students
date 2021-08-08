using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ToDoApi.Models;
using Xunit;

namespace ToDoListServerTests.IntegrationTests
{
    public class ToDoApiTests : IDisposable
    {
        private readonly WebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly LoggerFake _loggerFake = new();

        public ToDoApiTests()
        {
            _factory = new WebApplicationFactory(_loggerFake);
            _client = _factory.CreateClient();
        }

        private async Task<List<ToDoItem>> GetActualToDoList()
        {
            var getResponse = await _client.GetAsync("todo-list");
            var result = await getResponse.Content.ReadAsAsync<IEnumerable<ToDoItem>>();
            return result.ToList();
        }

        [Fact]
        public async Task GetTodoItemsStartingWithExistingPrefixSuccess()
        {
            var response = await _client.GetAsync("todo-list/Test");
            var result = await response.Content.ReadAsAsync<IEnumerable<ToDoItem>>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Single(result);
        }

        [Fact]
        public async Task PostWithParametersAndBodyIsNotAllowed()
        {
            var response = await _client.PostAsync("todo-list/something", RequestContentHelper.GetStringContent(8));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PostWithStringParameterIsNotAllowed()
        {
            var response = await _client.PostAsync("todo-list/something", RequestContentHelper.GetStringContent("task"));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PostWithParametersIsNotAllowed()
        {
            var response = await _client.PostAsync("todo-list/something", null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithIncorrectParametersReturnsBadRequest()
        {
            var response = await _client.DeleteAsync("todo-list/something");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithoutParametersIsNotAllowed()
        {
            var response = await _client.DeleteAsync("todo-list");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithOutOfRangeParametersReturnsNotFound()
        {
            var response = await _client.DeleteAsync("todo-list/-1");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Contains("Sequence contains no matching element", _loggerFake.Message[0]);
        }

        [Fact]
        public async Task PatchWithoutParametersWithIncorrectBodyIsNotAllowed()
        {
            var response = await _client.PatchAsync("todo-list", RequestContentHelper.GetPatchStringContent(5));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PatchWithoutParametersAndBodyIsNotAllowed()
        {
            var response = await _client.PatchAsync("todo-list", null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task GetToDoItemWithWithOutOfRangeParameterReturnsNotFound()
        {
            var response = await _client.GetAsync("todo-list/-1");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PatchWithIncorrectParameterReturnsUnsupportedMediaType()
        {
            var response = await _client.PatchAsync("todo-list/something", null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Fact]
        public async Task GetWithCorrectParameterReturnsOk()
        {
            var response = await _client.GetAsync("todo-list");
            var toDoList = await response.Content.ReadAsAsync<IEnumerable<ToDoItem>>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(_factory.ToDoList, toDoList.ToList(), new ToDoItemComparer());
        }

        [Fact]
        public async Task PostWithIncorrectStatusReturnsBadRequest()
        {
            var todoItemToPost = new ToDoItem {Description = "Walk the dog", Status = (ToDoItemStatus) 6};

            var response = await _client.PostAsync("todo-list", RequestContentHelper.GetStringContent(todoItemToPost));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostWithCorrectParameterReturnsOk()
        {
            var todoItemToPost = new ToDoItem {Description = "Walk the dog"};

            var response = await _client.PostAsync("todo-list", RequestContentHelper.GetStringContent(todoItemToPost));
            var toDoList = await GetActualToDoList();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(toDoList[2].Description, "Walk the dog");
        }

        [Fact]
        public async Task DeleteWithCorrectParameterReturnsNoContent()
        {
            var response = await _client.DeleteAsync("todo-list/0");
            var toDoList = await GetActualToDoList();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Single(toDoList);
        }

        [Fact]
        public async Task PatchWithCorrectBodyChangesIsCompleteAndReturnsNoContent()
        {
            var requestPatchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.Status, ToDoItemStatus.Complete);

            var response = await _client.PatchAsync("todo-list/0", RequestContentHelper.GetPatchStringContent(requestPatchDoc));
            var toDoList = await GetActualToDoList();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(ToDoItemStatus.Complete, toDoList[0].Status);
            Assert.Equal("Test task", toDoList[0].Description);
        }

        [Fact]
        public async Task PatchWithCorrectBodyChangesDescriptionAndReturnsNoContent()
        {
            var requestPatchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.Description, "Walk the dog");

            var response = await _client.PatchAsync("todo-list/0", RequestContentHelper.GetPatchStringContent(requestPatchDoc));
            var toDoList = await GetActualToDoList();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal("Walk the dog", toDoList[0].Description);
            Assert.Equal(ToDoItemStatus.NotComplete,toDoList[0].Status);
        }

        [Fact]
        public async Task PatchWithIncorrectStatusReturnsBadRequest()
        {
            var requestPatchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.Status, (ToDoItemStatus) 4);

            var response = await _client.PatchAsync("todo-list/0", RequestContentHelper.GetPatchStringContent(requestPatchDoc));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Value does not fall within the expected range", _loggerFake.Message[0]);
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}    
