using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp;
using ToDoListApp.Reader;
using Xunit;

namespace ToDoListAppTests
{
    public class CommandReaderTest
    {
        [Fact]
        public void TryGetCommandCreate_ShouldReturnListCommandMenuCreateTask()
        {
            //arrange
            var commandReader = new CommandReader(new[] {"create", "new test"});
            //act
            var command = commandReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.CreateTask, command);
        }
        [Fact]
        public void TryGetCommandDelete_ShouldReturnListCommandMenuDeleteTask()
        {
            //arrange
            var commandReader = new CommandReader(new[] { "delete", "1" });
            //act
            var command = commandReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.DeleteTask, command);
        }
        [Fact]
        public void TryGetCommandChangeDescription_ShouldReturnListCommandMenuChangeDescription()
        {
            //arrange
            var commandReader = new CommandReader(new[] { "change", "new test" });
            //act
            var command = commandReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.ChangeDescription, command);
        }
        [Fact]
        public void TryGetCommandComplete_ShouldReturnListCommandMenuCompleteTask()
        {
            //arrange
            var commandReader = new CommandReader(new[] { "complete", "1" });
            //act
            var command = commandReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.CompleteTask, command);
        }
        [Fact]
        public void TryGetCommandList_ShouldReturnListCommandMenuWriteTasks()
        {
            //arrange
            var commandReader = new CommandReader(new[] { "list", "new test" });
            //act
            var command = commandReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.WriteTasks, command);
        }
        [Fact]
        public void TryGetCommandExit_ShouldReturnListCommandMenuExit()
        {
            //arrange
            var commandReader = new CommandReader(new[] { "anything"});
            //act
            var command = commandReader.GetCommand();
            //assert
            Assert.Equal(ListCommandMenu.Exit, command);
        }
        [Fact]
        public void TryGetTaskId_ShouldReturnTaskId10()
        {
            //arrange
            var commandReader = new CommandReader(new[] { "change", "10", "check"});
            //act
            var taskId = commandReader.GetTaskId();
            //assert
            Assert.Equal(10, taskId);
        }
        [Fact]
        public void TryGetTaskDescription_ShouldReturnDescriptionTest()
        {
            //arrange
            var commandReader = new CommandReader(new[] { "create", "test" });
            //act
            var taskDescription = commandReader.GetTaskDescription();
            //assert
            Assert.Equal("test", taskDescription);
        }
        [Fact]
        public void TryContinueWork_ShouldReturnFalse()
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
