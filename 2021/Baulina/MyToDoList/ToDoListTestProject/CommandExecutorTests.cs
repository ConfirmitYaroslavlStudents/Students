using Xunit;
using MyToDoList;
using ToDoApp;
using InputOutputManagers;

namespace ToDoListTestProject
{
    public class CommandExecutorTests
    {
        [Fact]
        public void IsWorkingIsTrueAfterInitialization()
        {
            var console = new AppTestConsole();
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console));

            Assert.True(commandExecutor.IsWorking);
        }

        [Fact]
        public void ToDoListCountIncreasesAfterAddOperation()
        {
            var console = new AppTestConsole(new[] { "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console));

            commandExecutor.Add();

            Assert.Single(commandExecutor.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterAddIsChosen()
        {
            var console = new AppTestConsole(new[] { "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console));

            commandExecutor.Add();

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public void NewDescriptionRequestIsPrintedAfterEditIsChosen()
        {
            var console = new AppTestConsole(new[] { "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));

            commandExecutor.Edit();

            Assert.Contains("Type in a new description", console.Messages[1]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterEditIsChosen()
        {
            var console = new AppTestConsole(new[] { "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));

            commandExecutor.Edit();

            Assert.Equal("Water the plants", commandExecutor.MyToDoList[0].Description);
            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterMarkAsCompleteIsChosen()
        {
            var console = new AppTestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));

            commandExecutor.Complete();

            Assert.True(commandExecutor.MyToDoList[0].IsComplete);
            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterDeleteIsChosen()
        {
            var console = new AppTestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));

            commandExecutor.Delete();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public void ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new AppTestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));

            commandExecutor.Delete();

            Assert.Empty(commandExecutor.MyToDoList);
        }

        [Fact]
        public void IsWorkingIsFalseAfterExitIsChosen()
        {
            var console = new AppTestConsole();
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console));

            commandExecutor.Exit();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public void TableIsPrintedWhenViewAllTasksIsChosen()
        {
            var console = new AppTestConsole();
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));

            commandExecutor.List();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public void NumberRequestIsPrintedWhenChooseTaskNumber()
        {
            var console = new AppTestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));

            commandExecutor.ChooseTaskNumber();

            Assert.Contains("Choose the task number", console.Messages[0]);
        }
    }
}

