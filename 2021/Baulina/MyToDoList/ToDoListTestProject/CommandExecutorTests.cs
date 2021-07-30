using Xunit;
using MyToDoList;
using ToDoApp;

namespace ToDoListTestProject
{
    public class CommandExecutorTests
    {
        [Fact]
        public void IsWorkingIsTrueAfterInitialization()
        {
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(new ToDoList(), console);

            Assert.True(commandExecutor.IsWorking);
        }

        [Fact]
        public void ToDoListCountIncreasesAfterAddOperation()
        {
            var console = new AppConsoleFake(new[] { "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), console);

            commandExecutor.RunCommand(() => commandExecutor.Add());

            Assert.Single(commandExecutor.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterAddIsChosen()
        {
            var console = new AppConsoleFake(new[] { "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), console);

            commandExecutor.RunCommand(() => commandExecutor.Add());

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public void NewDescriptionRequestIsPrintedAfterEditIsChosen()
        {
            var console = new AppConsoleFake(new[] { "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);

            commandExecutor.RunCommand(() => commandExecutor.Edit());

            Assert.Contains("Type in a new description", console.Messages[1]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterEditIsChosen()
        {
            var console = new AppConsoleFake(new[] { "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);

            commandExecutor.RunCommand(() => commandExecutor.Edit());

            Assert.Equal("Water the plants", commandExecutor.MyToDoList[0].Description);
            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterMarkAsCompleteIsChosen()
        {
            var console = new AppConsoleFake(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);

            commandExecutor.RunCommand(() => commandExecutor.Complete());

            Assert.True(commandExecutor.MyToDoList[0].IsComplete);
            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterDeleteIsChosen()
        {
            var console = new AppConsoleFake(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);

            commandExecutor.RunCommand(() => commandExecutor.Delete());

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public void ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new AppConsoleFake(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);

            commandExecutor.RunCommand(() => commandExecutor.Delete());

            Assert.Empty(commandExecutor.MyToDoList);
        }

        [Fact]
        public void IsWorkingIsFalseAfterExitIsChosen()
        {
            var console = new AppConsoleFake();
            var commandExecutor = new CommandExecutor(new ToDoList(), console);

            commandExecutor.RunCommand(() => commandExecutor.Exit());

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public void TableIsPrintedWhenViewAllTasksIsChosen()
        {
            var console = new AppConsoleFake();
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);

            commandExecutor.RunCommand(() => commandExecutor.List());

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public void NumberRequestIsPrintedWhenChooseTaskNumber()
        {
            var console = new AppConsoleFake(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);

            commandExecutor.ChooseTaskNumber();

            Assert.Contains("Choose the task number", console.Messages[0]);
        }
    }
}

