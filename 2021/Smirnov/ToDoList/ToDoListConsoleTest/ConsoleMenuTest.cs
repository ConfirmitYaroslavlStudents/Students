using ToDoListConsole;
using ToDoListLib;
using Xunit;

namespace ToDoListConsoleTest
{
    public class ConsoleMenuTest
    {
        [Fact]
        public void CreateTask_DescriptionOOP()
        {
            var consoleMenu = new ConsoleMenu(new ToDoList(), new CmdReader(new[] { "create", "OOP" }));

            consoleMenu.StartMenu();

            Assert.Equal("OOP", consoleMenu.ToDoList[0].Description);
        }
        [Fact]
        public void DeleteTask_DescriptionOOP()
        {
            var consoleMenu = new ConsoleMenu(new ToDoList() { new Task{Description = "OOP"}}, new CmdReader(new[] { "delete", "1" }));

            consoleMenu.StartMenu();

            Assert.Empty(consoleMenu.ToDoList);
        }
        [Fact]
        public void ChangeTask_NewDescriptionOOP()
        {
            var consoleMenu = new ConsoleMenu(new ToDoList() { new Task { Description = "check" } }, new CmdReader(new[] { "change", "1", "OOP" }));

            consoleMenu.StartMenu();

            Assert.Equal("OOP", consoleMenu.ToDoList[0].Description);
        }
        [Fact]
        public void CompleteTask_DescriptionOOP()
        {
            var consoleMenu = new ConsoleMenu(new ToDoList() { new Task { Description = "OOP" } }, new CmdReader(new[] { "complete", "1",}));

            consoleMenu.StartMenu();

            Assert.Equal(TaskStatus.Done, consoleMenu.ToDoList[0].Status);
        }
    }
}
