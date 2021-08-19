using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp;
using ToDoListApp.Reader;
using Xunit;

namespace ToDoListAppTests
{
    public class userInputTest
    {
        [Fact]
        public void TryGetCommandCreate_ShouldReturnListCommandMenuCreateTask()
        {
            //arrange
            var userInputReader = new UserInputReader(new FakeConsoleInput {Input = "1"});
            //act
            var command = userInputReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.CreateTask, command);
        }
        [Fact]
        public void TryGetCommandDelete_ShouldReturnListCommandMenuDeleteTask()
        {
            //arrange
            var userInputReader = new UserInputReader(new FakeConsoleInput { Input = "2" });
            //act
            var command = userInputReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.DeleteTask, command);
        }
        [Fact]
        public void TryGetCommandChangeDescription_ShouldReturnListCommandMenuChangeDescription()
        {
            //arrange
            var userInputReader = new UserInputReader(new FakeConsoleInput { Input = "3" });
            //act
            var command = userInputReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.ChangeDescription, command);
        }
        [Fact]
        public void TryGetCommandComplete_ShouldReturnListCommandMenuCompleteTask()
        {
            //arrange
            var userInputReader = new UserInputReader(new FakeConsoleInput { Input = "4" });
            //act
            var command = userInputReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.CompleteTask, command);
        }
        [Fact]
        public void TryGetCommandList_ShouldReturnListCommandMenuWriteTasks()
        {
            //arrange
            var userInputReader = new UserInputReader(new FakeConsoleInput { Input = "5" });
            //act
            var command = userInputReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.WriteTasks, command);
        }
        [Fact]
        public void TryGetCommandExit_ShouldReturnListCommandMenuExit()
        {
            //arrange
            var userInputReader = new UserInputReader(new FakeConsoleInput { Input = "6" });
            //act
            var command = userInputReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.Exit, command);
        }
        [Fact]
        public void TryGetTaskId_ShouldReturnTaskId10()
        {
            //arrange
            var userInputReader = new UserInputReader(new FakeConsoleInput { Input = "10" });
            //act
            var taskId = userInputReader.GetTaskId();
            //assert
            Assert.Equal(10, taskId);
        }
        [Fact]
        public void TryGetTaskDescription_ShouldReturnDescriptionTest()
        {
            //arrange
            var userInputReader = new UserInputReader(new FakeConsoleInput { Input = "test" });
            //act
            var taskDescription = userInputReader.GetTaskDescription();
            //assert
            Assert.Equal("test", taskDescription);
        }
        [Fact]
        public void TryContinueWork_ShouldReturnTrue()
        {
            //arrange
            var commandReader = new CommandReader(new[] { "anything" });
            //act
            var command = commandReader.ContinueWork();
            //assert
            Assert.False(command);
        }
    }
}
