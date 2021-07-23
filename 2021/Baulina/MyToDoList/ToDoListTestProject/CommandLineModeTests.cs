﻿using MyToDoList;
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
            var console = new ClTestConsole(new[] {"Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Single(commandExecutor.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterAddIsChosen()
        {
            var console = new ClTestConsole(new[] { "Add", "Water the plants" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Done!", console.Messages[1]);
        }

        [Fact]
        public void ErrorNumberMessageIsPrintedWhenTryingToEditEmptyList()
        {
            var console = new ClTestConsole(new[] { "Edit"});
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console), new ErrorPrinter(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public void NewDescriptionRequestIsPrintedAfterEditIsChosen()
        {
            var console = new ClTestConsole(new[] {"Edit", "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Type in a new description", console.Messages[1]);
            Assert.Single(commandExecutor.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterEditIsChosen()
        {
            var console = new ClTestConsole(new[] {"Edit", "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Done!", console.Messages[3]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterMarkAsCompleteIsChosen()
        {
            var console = new ClTestConsole(new[] {"Complete", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Done!", console.Messages[2]);
            Assert.True(commandExecutor.MyToDoList[0].IsComplete);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToMarkAsCompleteInEmptyList()
        {
            var console = new ClTestConsole(new[] {"Complete"});
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console), new ErrorPrinter(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToDeleteFromEmptyList()
        {
            var console = new ClTestConsole(new[] { "Delete" });
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console), new ErrorPrinter(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterDeleteIsChosen()
        {
            var console = new ClTestConsole(new[] {"Delete", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Done!", console.Messages[2]);
        }

        [Fact]
        public void ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new ClTestConsole(new[] {"Delete", "0"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Empty(commandExecutor.MyToDoList);
        }

        [Fact]
        public void IsWorkingIsFalseAfterExitIsChosen()
        {
            var console = new ClTestConsole(new []{"Exit"});
            var commandExecutor = new CommandExecutor(new ToDoList(), new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.False(commandExecutor.IsWorking);
        }

        [Fact]
        public void TableIsPrintedWhenViewAllTasksIsChosen()
        {
            var console = new ClTestConsole(new[] {"List"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenNotEnoughArgsToDelete()
        {
            var console = new ClTestConsole(new[] { "Delete"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console), new ErrorPrinter(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[1]);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenNotEnoughArgsToEdit()
        {
            var console = new ClTestConsole(new[] { "Edit", "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var commandExecutor = new CommandExecutor(toDoList, new InputOutputManager(console), new ErrorPrinter(console));
            var appMenu = new ToDoAppMenu(new OperationGetter(console, commandExecutor), commandExecutor);

            appMenu.DoWork();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[2]);
        }
    }
}
