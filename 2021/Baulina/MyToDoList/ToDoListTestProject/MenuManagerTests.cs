using System.Collections.Generic;
using Xunit;
using MyToDoList;
using ToDoListConsole;
using Spectre.Console.Rendering;

namespace ToDoListTestProject
{
    class TestConsole : IConsole
    {
        public List<string> Messages = new List<string>();
        private readonly List<string> _linesToRead = new List<string>();

        public TestConsole() { }
        public TestConsole(IEnumerable<string> linesToRead)
        {
            _linesToRead.AddRange(linesToRead);
        }

        public void WriteLine(string message) => Messages.Add(message);
        public string ReadLine()
        {
            var result = _linesToRead[0];
            _linesToRead.RemoveAt(0);
            return result;
        }
        public void RenderTable(IRenderable table) => Messages.Add("Rendered");
        public void Clear() => Messages.Add("Cleared");
        public string GetDescription() => ReadLine();
        public string GetMenuItemName() => ReadLine();
    }

    public class MenuManagerTests
    {
        [Fact]
        public void IsWorkingIsTrueAfterInitialization()
        {
            var console = new TestConsole();
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            Assert.True(menuManager.IsWorking);
        }

        [Fact]
        public void ToDoListCountIncreasesAfterAddOperation()
        {
            var console = new TestConsole(new[] { "Water the plants" });
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            menuManager.Add();

            Assert.Single(menuManager.MyToDoList);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterAddIsChosen()
        {
            var console = new TestConsole(new[] { "Water the plants" });
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            menuManager.Add();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToEditEmptyList()
        {
            var console = new TestConsole();
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            menuManager.Edit();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        [Fact]
        public void NewDescriptionRequestIsPrintedAfterEditIsChosen()
        {
            var console = new TestConsole(new[] { "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var menuManager = new MenuManager(toDoList, new MessagePrinter(console));

            menuManager.Edit();

            Assert.Contains("[lightgoldenrod2_1]Type in a new description[/]", console.Messages);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterEditIsChosen()
        {
            var console = new TestConsole(new[] { "0", "Water the plants" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var menuManager = new MenuManager(toDoList, new MessagePrinter(console));

            menuManager.Edit();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterMarkAsCompleteIsChosen()
        {
            var console = new TestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var menuManager = new MenuManager(toDoList, new MessagePrinter(console));

            menuManager.MarkAsComplete();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToMarkAsCompleteInEmptyList()
        {
            var console = new TestConsole();
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            menuManager.MarkAsComplete();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToDeleteFromEmptyList()
        {
            var console = new TestConsole();
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            menuManager.Delete();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        [Fact]
        public void DoneMessageIsPrintedAfterDeleteIsChosen()
        {
            var console = new TestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var menuManager = new MenuManager(toDoList, new MessagePrinter(console));

            menuManager.Delete();

            Assert.Contains("[bold green]Done![/]", console.Messages);
        }

        [Fact]
        public void ToDoListCountDecreasesAfterDeleteOperation()
        {
            var console = new TestConsole(new[] {"0"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var menuManager = new MenuManager(toDoList, new MessagePrinter(console));

            menuManager.Delete();

            Assert.Empty(menuManager.MyToDoList);
        }

        [Fact]
        public void IsWorkingIsFalseAfterExitIsChosen()
        {
            var console = new TestConsole();
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            menuManager.Exit();

            Assert.False(menuManager.IsWorking);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenTryingToViewEmptyList()
        {
            var console = new TestConsole();
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            menuManager.ViewAllTasks();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }

        [Fact]
        public void TableIsPrintedWhenViewAllTasksIsChosen()
        {
            var console = new TestConsole();
            var toDoList = new ToDoList() {"Wash the dishes"};
            var menuManager = new MenuManager(toDoList, new MessagePrinter(console));

            menuManager.ViewAllTasks();

            Assert.Contains("Rendered", console.Messages);
        }

        [Fact]
        public void NumberRequestIsPrintedWhenChooseTaskNumber()
        {
            var console = new TestConsole(new[] { "0" });
            var toDoList = new ToDoList() { "Wash the dishes" };
            var menuManager = new MenuManager(toDoList, new MessagePrinter(console));

            menuManager.ChooseTaskNumber();

            Assert.Contains("Choose the task number", console.Messages[0]);
        }

        [Fact]
        public void IncorrectNumberWarningIsPrintedWhenChooseWrongTaskNumber()
        {
            var console = new TestConsole(new []{"5","0"});
            var toDoList = new ToDoList() { "Wash the dishes" };
            var menuManager = new MenuManager(toDoList, new MessagePrinter(console));

            menuManager.ChooseTaskNumber();

            Assert.Contains("[red]Incorrect number[/]", console.Messages);
        }

        [Fact]
        public void ErrorMessageIsPrintedWhenPrintErrorMessage()
        {
            var console = new TestConsole();
            var menuManager = new MenuManager(new ToDoList(), new MessagePrinter(console));

            menuManager.PrintErrorMessage();

            Assert.Contains("Something went wrong...You might want to try one more time", console.Messages[0]);
        }
    }
}
