using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyToDoList;
using ToDoApi;
using ToDoApi.Controllers;
using Xunit;

namespace ToDoListTestProject.ApiControllerTests
{
    public abstract class AbstractLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
            => throw new NotImplementedException();

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => Log(logLevel, exception, formatter(state, exception));

        public abstract void Log(LogLevel logLevel, Exception ex, string information);
    }

    public class ToDoItemsControllerTests
    {
       [Fact]
        public  void GetReturnsAllItemsWhenNotEmptyToDoList()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
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
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = controller.GetAllToDoItems();

            Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ToDoListCountIncreasesWhenAddToDoItem()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.AddToDoItem("Iron the clothes");

            Assert.IsType<OkResult>(result);
            Assert.Single(controller.ToDoList);
        }

        [Fact]
        public async Task ToDoListCountDecreasesAfterCorrectDeleteRequest()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.DeleteToDoItem(0);

            Assert.IsType<OkResult>(result);
            Assert.Single(controller.ToDoList);
        }

        [Fact]
        public async Task ErrorNotFoundWhenDeleteFromEmptyList()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoListProvider = new ListSaveAndLoadFake(new ToDoList());
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.DeleteToDoItem(0);

            Assert.IsType<NotFoundResult>(result);
            loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<InvalidOperationException>(),
                    "Sequence contains no matching element"),
                Times.Once);
            loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<InvalidOperationException>(),
                    "Sequence contains no matching element"),
                Times.Once);
        }

        [Fact]
        public async Task ErrorNotFoundWhenDeleteRequestWithIndexOutOfRange()
        {
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.DeleteToDoItem(8);

            Assert.IsType<NotFoundResult>(result);
            loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<InvalidOperationException>(),
                    "Sequence contains no matching element"),
                Times.Once);
            loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<InvalidOperationException>(),
                    "Sequence contains no matching element"),
                Times.Once);
        }

        [Fact]
        public async Task DescriptionChangedWhenEditToDoItemDescription()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);
            var editRequest = new EditRequest {Index = 0, NewDescription = "Go grocery shopping"};

            var result = await controller.EditToDoItemDescription(editRequest);

            Assert.IsType<OkResult>(result);
            Assert.Equal(editRequest.NewDescription, controller.ToDoList[0].Description);
        }

        [Fact]
        public async Task IsCompleteChangedWhenCompleteToDoItem()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.CompleteToDoItem(0);

            Assert.IsType<OkResult>(result);
            Assert.True(controller.ToDoList[0].IsComplete);
        }

        [Fact]
        public async Task ErrorNotFoundWhenCompleteWithIndexOutOfRange()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new ListSaveAndLoadFake(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.CompleteToDoItem(9);

            Assert.IsType<NotFoundResult>(result);
            loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<InvalidOperationException>(),
                    "Sequence contains no matching element"),
                Times.Once);
            loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<InvalidOperationException>(),
                    "Sequence contains no matching element"),
                Times.Once);
        }
    }
}
