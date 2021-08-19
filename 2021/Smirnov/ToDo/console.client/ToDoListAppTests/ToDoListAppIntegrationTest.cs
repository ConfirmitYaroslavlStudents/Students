using System.Threading.Tasks;
using ToDoListApp;
using ToDoListApp.Client;
using Xunit;

namespace ToDoListAppTests
{
    public class ToDoListAppIntegrationTest
    {
        [Fact]
        public async Task TryCreateTask_TryGetTasks_ShouldReturnTasksWithCreatedTask()
        {
            //arrange
            var input = new FakeUserInputReader();
            var output = new FakeConsoleWriter();
            var menu = new Menu(new HttpRequestGenerator("https://localhost:5001", "api/ToDoItems"), input, output);
            //act
            input.Description = "testCreating";
            input.Command = ListCommandMenu.CreateTask;
            await menu.StartMenu();

            input.Command = ListCommandMenu.WriteTasks;
            await menu.StartMenu();
            //assert
            Assert.True(output.IsCreated);
            Assert.Contains(output.CreatedTask.Id.ToString(), output.Tasks);
        }
        [Fact]
        public async Task TryDeleteTask_TryGetTasks_ShouldReturnTasksWithoutDeleteTask()
        {
            //arrange
            var input = new FakeUserInputReader();
            var output = new FakeConsoleWriter();
            var menu = new Menu(new HttpRequestGenerator("https://localhost:5001", "api/ToDoItems"), input, output);
            //act
            input.Description = "testDeleting";
            input.Command = ListCommandMenu.CreateTask;
            await menu.StartMenu();

            input.Command = ListCommandMenu.DeleteTask;
            input.Id = output.CreatedTask.Id;
            await menu.StartMenu();

            input.Command = ListCommandMenu.WriteTasks;
            await menu.StartMenu();
            //assert
            Assert.True(output.IsCreated);
            Assert.True(output.IsDeleted);
            Assert.DoesNotContain(output.CreatedTask.Id.ToString(), output.Tasks);
        }
        [Fact]
        public async Task TryChangeDescription_TryGetTasks_ShouldReturnTasksWithChangedTask()
        {
            //arrange
            var input = new FakeUserInputReader();
            var output = new FakeConsoleWriter();
            var menu = new Menu(new HttpRequestGenerator("https://localhost:5001", "api/ToDoItems"), input, output);
            //act
            input.Description = "testChanged";
            input.Command = ListCommandMenu.CreateTask;
            await menu.StartMenu();

            input.Command = ListCommandMenu.ChangeDescription;
            input.Id = output.CreatedTask.Id;
            input.Description = $"new Description {input.Id}";
            await menu.StartMenu();

            input.Command = ListCommandMenu.WriteTasks;
            await menu.StartMenu();
            //assert
            Assert.True(output.IsCreated);
            Assert.Contains(input.Id.ToString(), output.Tasks);
            Assert.True(output.IsDescriptionChanged);
            Assert.Contains(input.Description, output.Tasks);
        }
        [Fact]
        public async Task TryCompleteTask_TryGetTasks_ShouldReturnTasksWithChangedTask()
        {
            //arrange
            var input = new FakeUserInputReader();
            var output = new FakeConsoleWriter();
            var menu = new Menu(new HttpRequestGenerator("https://localhost:5001", "api/ToDoItems"), input, output);
            //act
            input.Description = "testChanged";
            input.Command = ListCommandMenu.CreateTask;
            await menu.StartMenu();

            input.Command = ListCommandMenu.CompleteTask;
            input.Id = output.CreatedTask.Id;
            await menu.StartMenu();

            input.Command = ListCommandMenu.WriteTasks;
            await menu.StartMenu();
            //assert
            Assert.True(output.IsCreated);
            Assert.True(output.IsTaskCompleted);
            Assert.Contains(input.Id.ToString(), output.Tasks);
            Assert.Equal("Done", output.ChangedTask.Status);
        }
        [Fact]
        public async Task TryDeleteTask_ShouldReturnNotFound()
        {
            //arrange
            var input = new FakeUserInputReader();
            var output = new FakeConsoleWriter();
            var menu = new Menu(new HttpRequestGenerator("https://localhost:5001", "api/ToDoItems"), input, output);

            //act
            input.Command = ListCommandMenu.DeleteTask;
            input.Id = 0;
            await menu.StartMenu();

            //assert
            Assert.False(output.IsDeleted);
            Assert.Contains("Not Found", output.ExceptionMessage);
        }
        [Fact]
        public async Task TryChangeDescription_ShouldReturnNotFound()
        {
            //arrange
            var input = new FakeUserInputReader();
            var output = new FakeConsoleWriter();
            var menu = new Menu(new HttpRequestGenerator("https://localhost:5001", "api/ToDoItems"), input, output);

            //act
            input.Command = ListCommandMenu.ChangeDescription;
            input.Id = 0;
            await menu.StartMenu();

            //assert
            Assert.False(output.IsDeleted);
            Assert.Contains("Not Found", output.ExceptionMessage);
        }
        [Fact]
        public async Task TryCompleteTask_ShouldReturnNotFound()
        {
            //arrange
            var input = new FakeUserInputReader();
            var output = new FakeConsoleWriter();
            var menu = new Menu(new HttpRequestGenerator("https://localhost:5001", "api/ToDoItems"), input, output);

            //act
            input.Command = ListCommandMenu.CompleteTask;
            input.Id = 0;
            await menu.StartMenu();

            //assert
            Assert.False(output.IsDeleted);
            Assert.Contains("Not Found", output.ExceptionMessage);
        }
    }
}
