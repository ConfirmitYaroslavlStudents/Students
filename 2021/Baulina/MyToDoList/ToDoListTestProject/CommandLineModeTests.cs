using MyToDoList;
using ToDoApp;
using Xunit;
using InputOutputManagers;

namespace ToDoListTestProject
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
        public void ToDoListCountIncreasesAfterAddOperation()
        {
            var console = new ClConsoleFake(new[] {"Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Single(commandExecutor.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterAddIsChosen()
        {
            var console = new ClConsoleFake(new[] { "Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public void ErrorNumberMessageIsPrintedWhenTryingToEditEmptyList()
        {
            var console = new ClConsoleFake(new[] { "Edit"});
            var commandExecutor = new CommandExecutor(new ToDoList(), console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public void NewDescriptionRequestIsPrintedAfterEditIsChosen()
        {
            var console = new ClConsoleFake(new[] {"Edit", "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Type in a new description", console.Messages[1]);
            Assert.Single(commandExecutor.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterEditIsChosen()
        {
            var console = new ClConsoleFake(new[] {"Edit", "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterMarkAsCompleteIsChosen()
        {
            var console = new ClConsoleFake(new[] {"Complete", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[2]);
            Assert.True(commandExecutor.MyToDoList[0].IsComplete);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToMarkAsCompleteInEmptyList()
        {
            var console = new ClConsoleFake(new[] {"Complete"});
            var commandExecutor = new CommandExecutor(new ToDoList(), console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToDeleteFromEmptyList()
        {
            var console = new ClConsoleFake(new[] { "Delete" });
            var commandExecutor = new CommandExecutor(new ToDoList(), console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterDeleteIsChosen()
        {
            var console = new ClConsoleFake(new[] {"Delete", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public void ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new ClConsoleFake(new[] {"Delete", "0"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Empty(commandExecutor.MyToDoList);
        }

        [Fact]
        public void IsWorkingIsFalseAfterExitIsChosen()
        {
            var console = new ClConsoleFake(new []{"Exit"});
            var commandExecutor = new CommandExecutor(new ToDoList(), console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public void TableIsPrintedWhenViewAllTasksIsChosen()
        {
            var console = new ClConsoleFake(new[] {"List"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenNotEnoughArgsToDelete()
        {
            var console = new ClConsoleFake(new[] { "Delete"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenNotEnoughArgsToEdit()
        {
            var console = new ClConsoleFake(new[] { "Edit", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[2]);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenIncorrectCommand()
        {
            var console = new ClConsoleFake(new[] { "fdntgrj6d", "0" });
            var commandExecutor = new CommandExecutor(new ToDoList(), console);
            var appController = new AppController(commandExecutor, console);

            appController.SendOperationToExecutor();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }
    }
}
