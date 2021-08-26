using System;
using System.Net.Http;
using System.Threading.Tasks;
using InputOutputManagers;
using ToDoApp;
using ToDoListTestProject.IntegrationTests.Models;
using Xunit;

namespace ToDoListTestProject.IntegrationTests
{
    public class CommandLineModeTests : IDisposable
    {
        private readonly WebApplicationFactory _factory;
        private readonly HttpClient _client;

        public CommandLineModeTests()
        {
            _factory = new WebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public void GetMenuItemNameIsNotCaseSensitive()
        {
            var cmdParserOne = new CommandLineInteractor(new []{"Add"});
            var cmdParserTwo = new CommandLineInteractor(new[] {"aDD"});
            
            Assert.Equal("add",cmdParserOne.GetMenuItemName());
            Assert.Equal("add",cmdParserTwo.GetMenuItemName());
        }

        [Fact]
        public async Task ToDoListCountIncreasesAfterAddOperation()
        {
            var console = new ClConsoleFake(new[] { "Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            var list = await commandExecutor.GetActualToDoList();
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterAddOperation()
        {
            var console = new ClConsoleFake(new[] { "Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public async Task NewDescriptionRequestIsPrintedAfterEditOperation()
        {
            var console = new ClConsoleFake(new[] { "Edit", "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Type in a new description", console.Messages[1]);
            var list = await commandExecutor.GetActualToDoList();
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterEditOperation()
        {
            var console = new ClConsoleFake(new[] {"Edit", "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterCompleteOperation()
        {
            var console = new ClConsoleFake(new[] { "Complete", "0" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[2]);
            var list = await commandExecutor.GetActualToDoList();
            Assert.True(list[0].IsComplete);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterDeleteOperation()
        {
            var console = new ClConsoleFake(new[] {"Delete", "0" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new ClConsoleFake(new[] { "Delete", "0" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            var list = await commandExecutor.GetActualToDoList();
            Assert.Single(list);
        }

        [Fact]
        public async Task IsWorkingIsFalseAfterExitOperation()
        {
            var console = new ClConsoleFake(new []{"Exit"});
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public async Task TableIsPrintedWhenListOperation()
        {
            var console = new ClConsoleFake(new[] {"List"});
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public async Task ErrorMessageIsPrintedWhenNotEnoughArgsToDelete()
        {
            var console = new ClConsoleFake(new[] { "Delete"});
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public async Task ErrorMessageIsPrintedWhenIncorrectCommand()
        {
            var console = new ClConsoleFake(new[] { "fdntgrj6d", "0" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
