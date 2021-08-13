using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForAddCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new AddCommand();
            command.SetParameters(new []{"add","war"});
            var result =command.PerformCommand(tasks);

            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual("world []",tasks[0].ToString());
            Assert.AreEqual("war",tasks[1].ToString());
        }

        [TestMethod]
        public void CorrectSetParameters_OneWord()
        {
            var command = new AddCommand();
            command.SetParameters(new[] { "add", "war" });
            
            Assert.AreEqual("war", command.NewTask.ToString());
        }

        [TestMethod]
        public void CorrectSetParameters_SeveralWords()
        {
            var command = new AddCommand();
            command.SetParameters(new[] { "add", "war", "or", "world" });

            Assert.AreEqual("war or world", command.NewTask.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionValidateWithLongTask()
        {
            var command = new AddCommand();
            command.SetParameters(new[] { "add", new string('o', 51) });
            command.RunValidate();
        }
    }
}