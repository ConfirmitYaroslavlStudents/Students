using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MyToDoList;
using ToDoApi;
using ToDoApi.Controllers;
using Xunit;

namespace ToDoListTestProject
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

    public class TestToDoListProvider : IToDoListProvider
    {
        private readonly ToDoList _toDoList;

        public TestToDoListProvider(IEnumerable<ToDoItem> toDoItems)
        {
            _toDoList = new ToDoList(toDoItems);
        }

        public IEnumerable<ToDoItem> GetToDoList()
        {
            return _toDoList;
        }
    }

    public class ToDoItemsControllerTests
    {
        [Fact]
        public async Task GetReturnsAllItemsWhenNotEmptyToDoList()
        {
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new TestToDoListProvider(toDoList);
            var controller = new ToDoItemsController(new NullLogger<ToDoItemsController>() ,toDoListProvider);

            var result = await controller.GetAllToDoItemsAsync();

            Assert.IsAssignableFrom<IEnumerable<ToDoViewItem>>(result);
            Assert.Equal(controller.ToDoList.Count, result.Count());
        }

        [Fact]
        public async Task GetReturnsEmptyCollectionWhenToDoListIsEmpty()
        {
            var toDoListProvider = new TestToDoListProvider(new ToDoList());
            var controller = new ToDoItemsController(new NullLogger<ToDoItemsController>(), toDoListProvider);

            var result = await controller.GetAllToDoItemsAsync();

            Assert.IsAssignableFrom<IEnumerable<ToDoViewItem>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ToDoListCountIncreasesWhenAddToDoItem()
        {
            var toDoListProvider = new TestToDoListProvider(new ToDoList());
            var controller = new ToDoItemsController(new NullLogger<ToDoItemsController>(), toDoListProvider);

            var result = await controller.AddToDoItem("Iron the clothes");

            Assert.IsType<OkResult>(result);
            Assert.Single(controller.ToDoList);
        }

        [Fact]
        public async Task ToDoListCountDecreasesAfterCorrectDeleteRequest()
        {
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new TestToDoListProvider(toDoList);
            var controller = new ToDoItemsController(new NullLogger<ToDoItemsController>(), toDoListProvider);

            var result = await controller.DeleteToDoItem(0);

            Assert.IsType<OkResult>(result);
            Assert.Single(controller.ToDoList);
        }

        [Fact]
        public async Task ErrorNotFoundWhenDeleteFromEmptyList()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoListProvider = new TestToDoListProvider(new ToDoList());
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.DeleteToDoItem(0);

            Assert.IsType<NotFoundResult>(result);
            loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<ArgumentOutOfRangeException>(),
                    "Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')"),
                Times.Once);
            loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<ArgumentOutOfRangeException>(),
                    "Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')"),
                Times.Once);
        }

        [Fact]
        public async Task ErrorNotFoundWhenDeleteRequestWithIndexOutOfRange()
        {
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new TestToDoListProvider(toDoList);
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.DeleteToDoItem(8);

            Assert.IsType<NotFoundResult>(result);
            loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<ArgumentOutOfRangeException>(),
                    "Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')"),
                Times.Once);
            loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<ArgumentOutOfRangeException>(),
                    "Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')"),
                Times.Once);
        }

        [Fact]
        public async Task DescriptionChangedWhenEditToDoItemDescription()
        {
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new TestToDoListProvider(toDoList);
            var controller = new ToDoItemsController(new NullLogger<ToDoItemsController>(), toDoListProvider);
            var editRequest = new EditRequest() {Index = 0, NewDescription = "Go grocery shopping"};

            var result = await controller.EditToDoItemDescription(editRequest);

            Assert.IsType<OkResult>(result);
            Assert.Equal(editRequest.NewDescription, controller.ToDoList[0].Description);
        }

        [Fact]
        public async Task IsCompleteChangedWhenCompleteToDoItem()
        {
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new TestToDoListProvider(toDoList);
            var controller = new ToDoItemsController(new NullLogger<ToDoItemsController>(), toDoListProvider);

            var result = await controller.CompleteToDoItem(0);

            Assert.IsType<OkResult>(result);
            Assert.True(controller.ToDoList[0].IsComplete);
        }

        [Fact]
        public async Task ErrorNotFoundWhenCompleteWithIndexOutOfRange()
        {
            var loggerMock = new Mock<AbstractLogger<ToDoItemsController>>();
            var toDoList = new ToDoList {"Clean the house", "Water the plants"};
            var toDoListProvider = new TestToDoListProvider(toDoList);
            var controller = new ToDoItemsController(loggerMock.Object, toDoListProvider);

            var result = await controller.CompleteToDoItem(9);

            Assert.IsType<NotFoundResult>(result);
            loggerMock.Verify(
                x => x.Log(LogLevel.Error, It.IsAny<ArgumentOutOfRangeException>(),
                    "Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')"),
                Times.Once);
            loggerMock.Verify(
                x => x.Log(LogLevel.Information, It.IsAny<ArgumentOutOfRangeException>(),
                    "Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')"),
                Times.Once);
        }

        [Fact]
        public async Task ConvertListOfToDoItemsToListOfToDoViewItemsReturnsEquivalentList()
        {
            var toDoList = new ToDoList { "Clean the house", "Water the plants" };
            var toDoListProvider = new TestToDoListProvider(toDoList);
            var controller = new ToDoItemsController(new NullLogger<ToDoItemsController>(),  toDoListProvider);

            var result = await controller.GetAllToDoItemsAsync();
            var toDoViewItems = result as ToDoViewItem[] ?? result.ToArray();

            for (var i = 0; i < controller.ToDoList.Count; i++)
            {
                Assert.Equal(controller.ToDoList[i].Description, toDoViewItems[i].Description);
                Assert.Equal(controller.ToDoList[i].IsComplete, toDoViewItems[i].IsComplete);
            }
        }
    }
}
