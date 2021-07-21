using MyToDoList;
using ToDoApp;
using Xunit;
using ConsoleInteractors;

namespace ToDoListTestProject
{
    public class CommandLineModeTests
    {
        [Fact]
        public void GetMenuItemNameIsNotCaseSensitive()
        {
            var cmdParserOne = new CommandLineHandler(new []{"Add"});
            var cmdParserTwo = new CommandLineHandler(new[] {"aDD"});
            
            Assert.Equal("add",cmdParserOne.GetMenuItemName());
            Assert.Equal("add",cmdParserTwo.GetMenuItemName());
        }

        [Fact]
        public void ToDoListCountIncreasesAfterAddOperation()
        {
            var console = new ClTestConsole(new[] {"Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Single(commandExecutor.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterAddIsChosen()
        {
            var console = new ClTestConsole(new[] { "Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ErrorNumberMessageIsPrintedWhenTryingToEditEmptyList()
        {
            var console = new ClTestConsole(new[] { "Edit"});
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[red]Something went wrong...You might want to try one more time[/]", console.Messages);
        }

        [Fact]
        public void NewDescriptionRequestIsPrintedAfterEditIsChosen()
        {
            var console = new ClTestConsole(new[] {"Edit", "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[lightgoldenrod2_1]Type in a new description[/]", console.Messages);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterEditIsChosen()
        {
            var console = new ClTestConsole(new[] {"Edit", "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterMarkAsCompleteIsChosen()
        {
            var console = new ClTestConsole(new[] {"Mark as complete", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToMarkAsCompleteInEmptyList()
        {
            var console = new ClTestConsole(new[] {"Mark as complete"});
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[red]Something went wrong...You might want to try one more time[/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToDeleteFromEmptyList()
        {
            var console = new ClTestConsole(new[] { "Delete" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[red]Something went wrong...You might want to try one more time[/]", console.Messages);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterDeleteIsChosen()
        {
            var console = new ClTestConsole(new[] {"Delete", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new ClTestConsole(new[] {"Delete", "0"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Empty(commandExecutor.MyToDoList);
        }

        [Fact]
        public void IsWorkingIsFalseAfterExitIsChosen()
        {
            var console = new ClTestConsole(new []{"Exit"});
            var commandExecutor = new CommandExecutor(new ToDoList(), new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public void TableIsPrintedWhenViewAllTasksIsChosen()
        {
            var console = new ClTestConsole(new[] {"View all tasks"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenNotEnoughArgsToDelete()
        {
            var console = new ClTestConsole(new[] { "Delete"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[red]Something went wrong...You might want to try one more time[/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenNotEnoughArgsToEdit()
        {
            var console = new ClTestConsole(new[] { "Edit", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new ConsoleHandler(console));
            var menuHandler = new MenuHandler(new OperationGetter(console), commandExecutor);

            menuHandler.Handle();

            Assert.Contains("[red]Something went wrong...You might want to try one more time[/]", console.Messages);
        }
    }
}
