using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
       [Fact]
        public  void GetReturnsAllItemsWhenNotEmptyToDoList()
        {
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = controller.GetAllToDoItems().ToList();

            Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(result);
            Assert.Equal(controller.ToDoList.Count, result.Count());
            Assert.Equal(controller.ToDoList, result);
        }

        [Fact]
        public void GetReturnsEmptyCollectionWhenToDoListIsEmpty()
        {
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = controller.GetAllToDoItems();

            Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ToDoListCountIncreasesWhenAddToDoItem()
        {
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.AddToDoItem("Iron the clothes");

            Assert.IsType<OkResult>(result);
            Assert.Single(controller.ToDoList);
            Assert.Equal(LogLevel.Information, loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public async Task ToDoListCountDecreasesAfterCorrectDeleteRequest()
        {
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.DeleteToDoItem(0);

            Assert.IsType<OkResult>(result);
            Assert.Single(controller.ToDoList);
            Assert.Equal(LogLevel.Information, loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public async Task ErrorNotFoundWhenDeleteFromEmptyList()
        {
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.DeleteToDoItem(0));
        }

        [Fact]
        public async Task ErrorNotFoundWhenDeleteRequestWithIndexOutOfRange()
        {
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.DeleteToDoItem(8));
        }

        [Fact]
        public async Task DescriptionChangedWhenEditToDoItemDescription()
        {
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);
            var patchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.Description, "Go grocery shopping");

            var result = await controller.EditToDoItem(0, patchDoc);

            Assert.IsType<OkResult>(result);
            Assert.Equal("Go grocery shopping", controller.ToDoList[0].Description);
            Assert.Equal(LogLevel.Information, loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public async Task IsCompleteChangedWhenCompleteToDoItem()
        {
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);
            var patchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.IsComplete, true);

            var result = await controller.EditToDoItem(0, patchDoc);

            Assert.IsType<OkResult>(result);
            Assert.True(controller.ToDoList[0].IsComplete);
            Assert.Equal(LogLevel.Information, loggerMock.Invocations[0].Arguments[0]);
        }

        [Fact]
        public async Task ErrorNotFoundWhenCompleteWithIndexOutOfRange()
        {
            var loggerMock = new Mock<ILogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);
            var patchDoc = new JsonPatchDocument<ToDoItem>()
                .Replace(o => o.IsComplete, true);


            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.EditToDoItem(9, patchDoc));
        }
    }
}
