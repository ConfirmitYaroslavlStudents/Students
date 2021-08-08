using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ToDoApp.CustomClient;
using ToDoApp.Models;
using Xunit;

namespace ToDoListClientTests.IntegrationTests
{
    public class IntegrationTests : IDisposable
    {
        private readonly Client _client;
        private readonly string _prefix = "TestTask_";

        public IntegrationTests()
        {
            _client = new Client(new TestClientSettings());
        }

        private int GetToDoItemID(HttpResponseMessage response)
        {
            return int.Parse(response.Headers.Location.Segments[2]);
        }

        [Fact]
        public async Task PostToDoItemSuccess()
        {
            var response = await _client.Post(new ToDoItem {Description = "Walk the dog", Status = ToDoItemStatus.NotComplete});
            var id = GetToDoItemID(response);
            var todoItem = await _client.GetToDoItem(id).Result.Content.ReadAsAsync<ToDoItem>();

            Assert.Equal("Walk the dog", todoItem.Description);
            Assert.Equal(ToDoItemStatus.NotComplete, todoItem.Status);
        }

        [Fact]
        public async Task PostToDoItemWithIncorrectStatusReturnsBadRequest()
        {
            var response = await _client.Post(new ToDoItem {Description = "Walk the dog", Status = (ToDoItemStatus) 10});

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public async Task EditToDoItemDescriptionSuccess()
        {
            var response =
                await _client.Post(new ToDoItem {Description = "Walk the dog", Status = ToDoItemStatus.NotComplete});
            var id = GetToDoItemID(response);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(t => t.Description, "Wipe the table");
            await _client.Patch(id, patchDoc);
            var todoItem = await _client.GetToDoItem(id).Result.Content.ReadAsAsync<ToDoItem>();

            Assert.Equal("Wipe the table", todoItem.Description);
            Assert.Equal(ToDoItemStatus.NotComplete, todoItem.Status);
        }
        
        [Fact]
        public async Task CompleteToDoItemSuccess()
        {
            var response = await _client.Post(new ToDoItem { Description = "Walk the dog", Status = ToDoItemStatus.NotComplete });
            var id = GetToDoItemID(response);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(t => t.Status, ToDoItemStatus.Complete);
            await _client.Patch(id, patchDoc);
            var todoItem = await _client.GetToDoItem(id).Result.Content.ReadAsAsync<ToDoItem>();

            Assert.Equal("Walk the dog", todoItem.Description);
            Assert.Equal(ToDoItemStatus.Complete, todoItem.Status);
        }

        [Fact]
        public async Task ChangeToDoItemStatusToIncorrectReturnsBadRequest()
        {
            var postResponse = await _client.Post(new ToDoItem { Description = "Walk the dog", Status = ToDoItemStatus.NotComplete });
            var id = GetToDoItemID(postResponse);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(t => t.Status, (ToDoItemStatus) 10);
            var response = await _client.Patch(id, patchDoc);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task IncompleteToDoItemSuccess()
        {
            var response = await _client.Post(new ToDoItem { Description = "Walk the dog", Status = ToDoItemStatus.Complete });
            var id = GetToDoItemID(response);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(t => t.Status, ToDoItemStatus.NotComplete);
            await _client.Patch(id, patchDoc);
            var todoItem = await _client.GetToDoItem(id).Result.Content.ReadAsAsync<ToDoItem>();

            Assert.Equal("Walk the dog", todoItem.Description);
            Assert.Equal(ToDoItemStatus.NotComplete, todoItem.Status);
        }

        [Fact]
        public async Task DeleteToDoItemSuccess()
        {
            var response = await _client.Post(new ToDoItem { Description = "Walk the dog", Status = ToDoItemStatus.Complete });
            var id = GetToDoItemID(response);
            await _client.Delete(id);
            var result = await _client.GetToDoItem(id);
            
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task DeleteNonExistingToDoItemReturnsNotFound()
        {
            var response = await _client.Delete(int.MinValue);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        public void Dispose()
        {
            //var response = _client.GetItemsStartingWith(_prefix);
            //var items = response.Result.Content.ReadAsAsync<IEnumerable<ToDoItem>>().Result;
            //foreach (var item in items)
            //{
            //    _client.Delete(item.Id);
            //}
        }
    }
}
