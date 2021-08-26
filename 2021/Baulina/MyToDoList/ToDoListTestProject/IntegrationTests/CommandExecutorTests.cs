using System;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoApp;
using ToDoListTestProject.IntegrationTests.Models;
using Xunit;

namespace ToDoListTestProject.IntegrationTests
{
    public class CommandExecutorTests : IDisposable
    {
        private readonly WebApplicationFactory _factory;
        private readonly HttpClient _client;

        public CommandExecutorTests()
        {
            _factory = new WebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public void IsWorkingIsTrueAfterInitialization()
        {
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, _client);

            Assert.True(commandExecutor.IsWorking);
        }

        [Fact]
        public async Task ToDoListCountIncreasesAfterAddOperation()
        {
            var console = new AppConsoleFake(new[] { "Water the plants" });
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.Add();

            var list = await commandExecutor.GetActualToDoList();
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterAddOperation()
        {
            var console = new AppConsoleFake(new[] { "Water the plants" });
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.Add();

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public async Task NewDescriptionRequestIsPrintedAfterEditOperation()
        {
            var console = new AppConsoleFake(new[] { "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.Edit();

            Assert.Contains("Type in a new description", console.Messages[1]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterEditOperation()
        {
            var console = new AppConsoleFake(new[] { "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.Edit();

            var list = await commandExecutor.GetActualToDoList();
            Assert.Equal("Water the plants", list[0].Description);
            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterCompleteOperation()
        {
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.Complete();

            var list = await commandExecutor.GetActualToDoList();
            Assert.True(list[0].IsComplete);
            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterDeleteOperation()
        {
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.Delete();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.Delete();

            var list = await commandExecutor.GetActualToDoList();
            Assert.Single(list);
        }

        [Fact]
        public async Task IsWorkingIsFalseAfterExitOperation()
        {
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.Exit();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public async Task TableIsPrintedWhenListOperation()
        {
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, _client);

            await commandExecutor.List();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public async Task NumberRequestIsPrintedWhenChooseTaskNumber()
        {
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, _client);

            var list = await commandExecutor.GetActualToDoList();
            commandExecutor.ChooseTaskNumber(list);

            Assert.Contains("Choose the task number", console.Messages[0]);
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}

