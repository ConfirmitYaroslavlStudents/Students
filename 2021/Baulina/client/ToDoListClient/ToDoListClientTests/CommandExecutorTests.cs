using System.Threading.Tasks;
using ToDoApp;
using ToDoListClientTests.Models;
using Xunit;

namespace ToDoListClientTests
{
    public class CommandExecutorTests
    {
       

        [Fact]
        public void IsWorkingIsTrueAfterInitialization()
        {
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, httpClient);

            Assert.True(commandExecutor.IsWorking);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterAddOperation()
        {
            var console = new AppConsoleFake(new[] { "Water the plants" });
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Add();

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public async Task NewDescriptionRequestIsPrintedAfterEditOperation()
        {
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var console = new AppConsoleFake(new[] { "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Edit();

            Assert.Contains("Type in a new description", console.Messages[1]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterEditOperation()
        {
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var console = new AppConsoleFake(new[] { "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Edit();
            
            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterCompleteOperation()
        {
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Complete();
            
            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterDeleteOperation()
        {
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Delete();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task IsWorkingIsFalseAfterExitOperation()
        {
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Exit();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public async Task TableIsPrintedWhenListOperation()
        {
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.List();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public async Task NumberRequestIsPrintedWhenChooseTaskNumber()
        {
            var httpClient = HttpClientFake.GetFakeHttpClient();
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            var list = await commandExecutor.GetActualToDoList();
            commandExecutor.ChooseTaskNumber(list);

            Assert.Contains("Choose the task number", console.Messages[0]);
        }
    }
}

