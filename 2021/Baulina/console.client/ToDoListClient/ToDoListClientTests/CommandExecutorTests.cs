using System;
using System.Threading.Tasks;
using ToDoApp;
using ToDoApp.CustomClient;
using ToDoApp.Models;
using ToDoListClientTests.Models;
using Xunit;

namespace ToDoListClientTests
{
    public class CommandExecutorTests
    {
        [Fact]
        public void IsWorkingIsTrueAfterInitialization()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, httpClient);

            Assert.True(commandExecutor.IsWorking);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterAddOperation()
        {
            var console = new AppConsoleFake(new[] { "Water the plants" });
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Add();

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public async Task NewDescriptionRequestIsPrintedAfterEditOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake(new[] { "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Edit();

            Assert.Contains("Type in a new description", console.Messages[1]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterEditOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake(new[] { "0", "Water the plants" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Edit();
            
            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterCompleteOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Complete();
            
            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterIncompleteOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Incomplete();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task DoneMessageIsPrintedAfterDeleteOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake(new[] { "0" });
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Delete();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public async Task IsWorkingIsFalseAfterExitOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.Exit();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public async Task TableIsPrintedWhenListOperation()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(console, httpClient);

            await commandExecutor.List();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public void GetToDoItemStatusIsNotCaseSensitive()
        {
            var console = new AppConsoleFake(new[] { "Complete" , "complete" ,"COMPLETE", "cOmPlEtE" });

            for (var i = 0; i < 4; i++)
            {
                Assert.Equal((int)ToDoItemStatus.Complete, console.GetToDoItemStatus());
            }
        }

        [Fact]
        public void GetToDoItemStatusIsNotSpaceSensitive()
        {
            var console = new AppConsoleFake(new[] { "Not Complete", "not complete", "NOT COMPLETE", "not cOmPlEtE" });

            for (var i = 0; i < 4; i++)
            {
                Assert.Equal((int)ToDoItemStatus.NotComplete, console.GetToDoItemStatus());
            }
        }

        [Fact]
        public async Task NumberRequestIsPrintedWhenChooseTaskId()
        {
            var httpClient = new Client(HttpClientFake.GetFakeHttpClient());
            var console = new AppConsoleFake(new string[0]);
            var commandExecutor = new CommandExecutor(console, httpClient);

            var list = await commandExecutor.GetActualToDoList();
            
            Assert.Throws<ArgumentNullException>(() => commandExecutor.ChooseTaskId(list));
            Assert.Contains("Choose task id", console.Messages[0]);
        }
    }
}

