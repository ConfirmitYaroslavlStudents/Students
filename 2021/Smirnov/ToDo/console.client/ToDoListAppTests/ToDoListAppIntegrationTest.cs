using System.Diagnostics;
using System.Threading.Tasks;
using ToDoListApp;
using ToDoListApp.Client;
using Xunit;

namespace ToDoListAppTests
{
    public class ToDoListAppIntegrationTest
    {
        public ToDoListAppIntegrationTest()
        {
            Process.Start("Server\\ToDoWebApi.exe");
        }
        [Fact]
        public async Task TryCreateTask_TryGetTasks_ShouldReturnTasksWithCreatedTask()
        {
            var input = new FakeUserInputReader();
            var output = new FakeConsoleWriter();

            var menu = new Menu(new HttpRequestGenerator(), input, output);

            input.Description = "testCreating";
            input.Command = ListCommandMenu.CreateTask;
            await menu.StartMenu();

            input.Command = ListCommandMenu.WriteTasks;
            await menu.StartMenu();

            Assert.True(output.TaskCreated);
            Assert.Contains(input.Description, output.Tasks);
        }
    }
}
