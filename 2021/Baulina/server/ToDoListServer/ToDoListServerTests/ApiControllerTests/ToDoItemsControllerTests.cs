using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ToDoApi.Controllers;
using ToDoApi.Models;
using Xunit;

namespace ToDoListServerTests.ApiControllerTests
{
    public class ToDoItemsControllerTests
    {
        private readonly Mock<ILogger<ToDoItemsController>> _loggerMock;
        private readonly IEnumerable<ToDoItem> _toDoList;

        public ToDoItemsControllerTests()
        {
            _loggerMock = new Mock<ILogger<ToDoItemsController>>();
            _toDoList = new[]
            {
                new ToDoItem
                {
                    Description = "Clean the house", Tags = new[] {new Tag {Name = "important"}, new() {Name = "home"}}
                },
                new() {Description = "Clean the toilet"}
            };
        }

       [Fact]
        public  void GetReturnsAllItemsWhenNotEmptyToDoList()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            var result = controller.GetAllToDoItems().Value.ToList();

            Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(result);
            Assert.Equal(controller.ToDoList.Count, result.Count());
            Assert.Equal(controller.ToDoList, result);
        }

        [Fact]
        public void GetReturnsEmptyCollectionWhenToDoListIsEmpty()
        {
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            var result = controller.GetAllToDoItems().Value;

            Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ToDoListCountIncreasesWhenAddToDoItem()
        {
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            var result = controller.AddToDoItem(new ToDoItem {Description = "Iron the clothes"});

            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Single(controller.ToDoList);
            Assert.Equal(LogLevel.Information, _loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public void ToDoListCountDecreasesAfterCorrectDeleteRequest()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            var result = controller.DeleteToDoItem(0);
            
            Assert.IsType<NoContentResult>(result);
            Assert.Single(controller.ToDoList);
            Assert.Equal(LogLevel.Information, _loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public void ErrorNotFoundWhenDeleteFromEmptyList()
        {
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            Assert.Throws<InvalidOperationException>(() => controller.DeleteToDoItem(0));
        }

        [Fact]
        public void ErrorNotFoundWhenGetToDoItemWithIncorrectIndex()
        {
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            Assert.Throws<InvalidOperationException>(() => controller.DeleteToDoItem(-1));
        }

        [Fact]
        public void ErrorNotFoundWhenDeleteRequestWithIndexOutOfRange()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            Assert.Throws<InvalidOperationException>(() => controller.DeleteToDoItem(8));
        }

        [Fact]
        public void DescriptionChangedWhenEditToDoItemDescription()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);
            var patchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.Description, "Go grocery shopping");

            var result = controller.EditToDoItem(0, patchDoc);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal("Go grocery shopping", controller.ToDoList[0].Description);
            Assert.Equal(LogLevel.Information, _loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public void ToDoItemStatusChangedWhenCompleteToDoItem()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);
            var patchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.Status, ToDoItemStatus.Complete);

            var result = controller.EditToDoItem(0, patchDoc);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal(ToDoItemStatus.Complete, controller.ToDoList[0].Status);
            Assert.Equal(LogLevel.Information, _loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public void ErrorNotFoundWhenCompleteWithIndexOutOfRange()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);
            var patchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.Status, ToDoItemStatus.Complete);
            
            Assert.Throws<InvalidOperationException>(() => controller.EditToDoItem(9, patchDoc));
        }

        [Fact]
        public void ErrorWhenAddToDoItemWithIncorrectStatus()
        {
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            Assert.Throws<InvalidEnumArgumentException>(() =>
                controller.AddToDoItem(new() {Description = "Iron the clothes", Status = (ToDoItemStatus) 4}));
        }

        [Fact]
        public void RenumberIdAutomaticallyWhenAddToDoItemWithAlreadyExistingId()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            var createdResponse = controller.AddToDoItem(new() {Description = "Iron the clothes", Id = 0}) as CreatedAtActionResult;
            var toDoItem = createdResponse?.Value as ToDoItem;

            Assert.IsType<CreatedAtActionResult>(createdResponse);
            Assert.IsType<ToDoItem>(toDoItem);
            Assert.Equal(2, toDoItem.Id);
            Assert.Equal(LogLevel.Information, _loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public void GetTodoItemsStartingWithSuccess()
        {
            var toDoList = new ToDoList(new[]
            {
                new ToDoItem {Description = "Clean the house"},
                new() {Description = "Clean the toilet"}
            });
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);

            var result = controller.GetTodoItemsStartingWith("Clean");

            Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void PatchTodoItemWithLessTagsSuccess()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(o => o.Tags, new[] {new Tag {Name = "important"}});

            controller.EditToDoItem(0, patchDoc);

            Assert.Single(toDoList[0].Tags);
        }

        [Fact]
        public void PatchTodoItemWithMoreTagsSuccess()
        {
            var toDoList = new ToDoList(_toDoList);
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(_loggerMock.Object, toDoListProvider);
            var tags = new List<Tag>(new[]
                {new Tag {Name = "important"}, new() {Name = "home"}, new() {Name = "weekend"}});
            var patchDoc = new JsonPatchDocument<ToDoItem>().Replace(o => o.Tags, tags);

            controller.EditToDoItem(0, patchDoc);

            Assert.Equal(3,toDoList[0].Tags.Count());
        }
    }
}
