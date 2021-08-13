using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForEditCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "1", "war" });
            var result = command.PerformCommand(tasks);

            Assert.AreEqual("war []", result[0].ToString());
        }

        [TestMethod]
        public void CorrectSetParameters_OneWord()
        {
            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "1", "war" });

            Assert.AreEqual(0, command.Index);
            Assert.AreEqual("war", command.Text);
        }

        [TestMethod]
        public void CorrectSetParameters_SeveralWords()
        {
            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "1", "war", "or", "world" });
            Assert.AreEqual(0, command.Index);
            Assert.AreEqual("war or world", command.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenWrongIndex()
        {
            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "odin", "world" });
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsNegative()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "-1", "war" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsGreaterThanCount()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "2", "war" });
            command.RunValidate(tasks);
        }
        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionValidateWithLongTask()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };


            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "1", new string('o', 51) });
            command.RunValidate(tasks);
        }
    }
}