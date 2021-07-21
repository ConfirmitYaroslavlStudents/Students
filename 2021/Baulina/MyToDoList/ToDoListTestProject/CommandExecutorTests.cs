using Xunit;
using MyToDoList;
using ToDoApp;
using ConsoleInteractors;

namespace ToDoListTestProject
{
    public class CommandExecutorTests
    {
        [Fact]
        public void IsWorkingIsTrueAfterInitialization()
        {
            var console = new AppTestConsole();
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));

            Assert.True(commandExecutor.IsWorking);
        }

        [Fact]
        public void ToDoListCountIncreasesAfterAddOperation()
        {
            var console = new AppTestConsole(new[] { "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));

            commandExecutor.Add();

            Assert.Single(commandExecutor.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterAddIsChosen()
        {
            var console = new AppTestConsole(new[] { "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));

            commandExecutor.Add();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToEditEmptyList()
        {
            var console = new AppTestConsole();
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));

            commandExecutor.Edit();

            Assert.Contains("[red]Incorrect number[/]", console.Messages);
        }

        [Fact]
        public void NewDescriptionRequestIsPrintedAfterEditIsChosen()
        {
            var console = new AppTestConsole(new[] { "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.Edit();

            Assert.Contains("[lightgoldenrod2_1]Type in a new description[/]", console.Messages);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterEditIsChosen()
        {
            var console = new AppTestConsole(new[] { "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.Edit();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterMarkAsCompleteIsChosen()
        {
            var console = new AppTestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.MarkAsComplete();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToMarkAsCompleteInEmptyList()
        {
            var console = new AppTestConsole();
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));

            commandExecutor.MarkAsComplete();

            Assert.Contains("[red]Incorrect number[/]", console.Messages); 
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToDeleteFromEmptyList()
        {
            var console = new AppTestConsole();
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));

            commandExecutor.Delete();

            Assert.Contains("[red]Incorrect number[/]", console.Messages);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterDeleteIsChosen()
        {
            var console = new AppTestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.Delete();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new AppTestConsole(new[] {"0"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.Delete();

            Assert.Empty(commandExecutor.MyToDoList);
        }

        [Fact]
        public void IsWorkingIsFalseAfterExitIsChosen()
        {
            var console = new AppTestConsole();
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));

            commandExecutor.Exit();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public void TableIsPrintedWhenViewAllTasksIsChosen()
        {
            var console = new AppTestConsole();
            var toDoList = new ToDoList() {"Wash the dishes"};
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.ViewAllTasks();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public void NumberRequestIsPrintedWhenChooseTaskNumber()
        {
            var console = new AppTestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.ChooseTaskNumber();

            Assert.Contains("Choose the task number", console.Messages[0]);
        }

        [Fact]
        public void IncorrectNumberMessageIsPrintedWhenChooseWrongTaskNumber()
        {
            var console = new AppTestConsole(new[] {"5", "0"});
            var toDoList = new ToDoList() {"Wash the dishes"};
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.MarkAsComplete();

            Assert.Contains("[red]Incorrect number[/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenChooseTaskNumberCantIntParse()
        {
            var console = new AppTestConsole(new[] { "nrtbfv" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));

            commandExecutor.MarkAsComplete();

            Assert.Contains("[red]Something went wrong...You might want to try one more time[/]", console.Messages);
        }
    }
}
