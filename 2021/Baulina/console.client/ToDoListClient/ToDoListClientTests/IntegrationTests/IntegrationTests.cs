using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.JsonPatch;
using ToDoApp.CustomClient;
using ToDoApp.Models;
using Xunit;

namespace ToDoListClientTests.IntegrationTests
{
    public class IntegrationTests : IDisposable
    {
        private readonly Client _client;
        private readonly string _prefix = "TeskTask_";

        public IntegrationTests()
        {
            _client = new Client(new TestClientSettings());
        }

        private int GetToDoItemID(HttpResponseMessage response)
        {
            return int.Parse(response.Headers.Location.Segments[2]);
        }

        [Fact]
        public void PostToDoItemSuccess()
        {
            var response = _client.Post(new ToDoItem {Description = "Walk the dog", Status = ToDoItemStatus.NotComplete});
            var id = GetToDoItemID(response.Result);
            var todoItem = _client.GetToDoItem(id).Result.Content.ReadAsAsync<ToDoItem>().Result;

            Assert.Equal("Walk the dog", todoItem.Description);
            Assert.Equal(ToDoItemStatus.NotComplete, todoItem.Status);
        }

        [Fact]
        public void PostToDoItemWithIncorrectStatusReturnsBadRequest()
        {
            var response = _client.Post(new ToDoItem {Description = "Walk the dog", Status = (ToDoItemStatus) 10});

            Assert.False(response.Result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.Result.StatusCode);
        }
        
        [Fact]
        public void EditToDoItemDescriptionSuccess()
        {
            var response =
                _client.Post(new ToDoItem {Description = "Walk the dog", Status = ToDoItemStatus.NotComplete});
            var id = GetToDoItemID(response.Result);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(t => t.Description, "Wipe the table");
            _client.Patch(id, patchDoc);
            var todoItem = _client.GetToDoItem(id).Result.Content.ReadAsAsync<ToDoItem>().Result;

            Assert.Equal("Wipe the table", todoItem.Description);
            Assert.Equal(ToDoItemStatus.NotComplete, todoItem.Status);
        }
        
        [Fact]
        public void CompleteToDoItemSuccess()
        {
            var response = _client.Post(new ToDoItem { Description = "Walk the dog", Status = ToDoItemStatus.NotComplete });
            var id = GetToDoItemID(response.Result);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(t => t.Status, ToDoItemStatus.Complete);
            _client.Patch(id, patchDoc);
            var todoItem = _client.GetToDoItem(id).Result.Content.ReadAsAsync<ToDoItem>().Result;

            Assert.Equal("Walk the dog", todoItem.Description);
            Assert.Equal(ToDoItemStatus.Complete, todoItem.Status);
        }

        [Fact]
        public void ChangeToDoItemStatusToIncorrectReturnsBadRequest()
        {
            var postResponse = _client.Post(new ToDoItem { Description = "Walk the dog", Status = ToDoItemStatus.NotComplete });
            var id = GetToDoItemID(postResponse.Result);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(t => t.Status, (ToDoItemStatus) 10);
            var response = _client.Patch(id, patchDoc);

            Assert.False(response.Result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.Result.StatusCode);
        }

        [Fact]
        public void IncompleteToDoItemSuccess()
        {
            var response = _client.Post(new ToDoItem { Description = "Walk the dog", Status = ToDoItemStatus.Complete });
            var id = GetToDoItemID(response.Result);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(t => t.Status, ToDoItemStatus.NotComplete);
            _client.Patch(id, patchDoc);
            var todoItem = _client.GetToDoItem(id).Result.Content.ReadAsAsync<ToDoItem>().Result;

            Assert.Equal("Walk the dog", todoItem.Description);
            Assert.Equal(ToDoItemStatus.Complete, todoItem.Status);
        }

        [Fact]
        public void DeleteToDoItemSuccess()
        {
            var response = _client.Post(new ToDoItem { Description = "Walk the dog", Status = ToDoItemStatus.Complete });
            var id = GetToDoItemID(response.Result);
            _client.Delete(id);
            var result = _client.GetToDoItem(id).Result;
            
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public void DeleteNonExistingToDoItemReturnsNotFound()
        {
            var response = _client.Delete(int.MinValue);

            Assert.False(response.Result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.Result.StatusCode);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
