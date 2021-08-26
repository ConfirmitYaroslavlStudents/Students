using System;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoApp;
using ToDoListTestProject.IntegrationTests.Models;
using Xunit;

namespace ToDoListTestProject.IntegrationTests
{
    public class CommandLineModeEmptyListTests : IDisposable
    {
        private readonly WebApplicationFactoryWithEmptyList _factory;
        private readonly HttpClient _client;

        public CommandLineModeEmptyListTests()
        {
            _factory = new WebApplicationFactoryWithEmptyList();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task ErrorNumberMessageIsPrintedWhenTryingToEditEmptyList()
        {
            var console = new ClConsoleFake(new[] { "Edit" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public async Task ErrorMessageIsPrintedWhenTryingToCompleteInEmptyList()
        {
            var console = new ClConsoleFake(new[] { "Complete" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public async Task ErrorMessageIsPrintedWhenTryingToDeleteFromEmptyList()
        {
            var console = new ClConsoleFake(new[] { "Delete" });
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
