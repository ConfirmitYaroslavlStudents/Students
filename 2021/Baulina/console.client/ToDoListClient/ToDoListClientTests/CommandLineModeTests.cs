using System.Threading.Tasks;
using InputOutputManagers;
using ToDoApp;
using ToDoApp.CustomClient;
using ToDoListClientTests.Models;
using Xunit;

namespace ToDoListClientTests
{
    public class CommandLineModeTests
    {
        [Fact]
        public void GetMenuItemNameIsNotCaseSensitive()
        {
            var cmdParserOne = new CommandLineInteractor(new []{"Add"});
            var cmdParserTwo = new CommandLineInteractor(new[] {"aDD"});
            
            Assert.Equal("add",cmdParserOne.GetMenuItemName());
            Assert.Equal("add",cmdParserTwo.GetMenuItemName());
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterAddOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new ClConsoleFake(new[] { "Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public async Task NewDescriptionRequestIsPrintedAfterEditOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new ClConsoleFake(new[] { "Edit", "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Type in a new description", console.Messages[1]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterEditOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new ClConsoleFake(new[] {"Edit", "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterCompleteOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new ClConsoleFake(new[] { "Complete", "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterIncompleteOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new ClConsoleFake(new[] { "Incomplete", "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterDeleteOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new ClConsoleFake(new[] {"Delete", "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task IsWorkingIsFalseAfterExitOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new ClConsoleFake(new []{"Exit"});
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public async Task TableIsPrintedWhenListOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new ClConsoleFake(new[] {"List"});
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public async Task ErrorMessageIsPrintedWhenNotEnoughArgsToDelete()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClientThatReturnsBadRequest());
            var console = new ClConsoleFake(new[] { "Delete"});
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        [Fact]
        public async Task ErrorMessageIsPrintedWhenIncorrectCommand()
        {
            var console = new ClConsoleFake(new[] { "fdntgrj6d", "0" });
            var httpClient = new Client(HttpClientFake.GetFakeHttpClientThatReturnsBadRequest());
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        [Fact]
        public async Task ErrorNumberMessageIsPrintedWhenTryingToEditEmptyList()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClientThatReturnsBadRequest());
            var console = new ClConsoleFake(new[] { "Edit" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        [Fact]
        public async Task ErrorMessageIsPrintedWhenTryingToCompleteInEmptyList()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClientThatReturnsBadRequest());
            var console = new ClConsoleFake(new[] { "Complete" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        [Fact]
        public async Task ErrorMessageIsPrintedWhenTryingToDeleteFromEmptyList()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClientThatReturnsBadRequest());
            var console = new ClConsoleFake(new[] { "Delete" });
            var commandExecutor = new CommandExecutor(console, httpClient);
            var appController = new AppController(commandExecutor, console);

            await appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }
    }
}
