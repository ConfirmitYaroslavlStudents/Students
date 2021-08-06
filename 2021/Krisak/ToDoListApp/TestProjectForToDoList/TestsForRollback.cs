using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForRollback
    {
        [TestMethod]
        public void CorrectPerformAddCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };
            var rollback = new Rollback(tasks);

            var partsOfCommand = new string[] { "delete", "1" };

            rollback.AddNewRollback(partsOfCommand);
            tasks[0] = new Task { Text = "war", Status = StatusTask.Done };
            rollback.PerformRollbacks(1);

            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual("war [X]", tasks[1].ToString());
            Assert.AreEqual("world []", tasks[0].ToString());
        }

        [TestMethod]
        public void CorrectPerformEditCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };
            var rollback = new Rollback(tasks);

            var partsOfCommand = new string[] { "edit", "1", "war" };

            rollback.AddNewRollback(partsOfCommand);
            tasks[0].Text = "war";
            rollback.PerformRollbacks(1);

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("world []", tasks[0].ToString());
        }

        [TestMethod]
        public void CorrectPerformToggleCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };
            var rollback = new Rollback(tasks);

            var partsOfCommand = new string[] { "toggle", "1", "2" };

            rollback.AddNewRollback(partsOfCommand);
            tasks[0].Status = StatusTask.Done;
            rollback.PerformRollbacks(1);

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("world []", tasks[0].ToString());
        }

        [TestMethod]
        public void CorrectPerformDeleteCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };
            var rollback = new Rollback(tasks);

            var partsOfCommand = new string[] { "add", "world" };

            rollback.AddNewRollback(partsOfCommand);
            rollback.PerformRollbacks(1);

            Assert.AreEqual(0, tasks.Count);
        }

        [TestMethod]
        public void CorrectPerformDifferentCommands_OneRollback()
        {
            var tasks = new List<Task> { new Task { Text = "war"} };
            var rollback = new Rollback(tasks);
            
            rollback.AddNewRollback(new string[] { "add", "world" });
            tasks.Add(new Task{Text = "world"});
            
            rollback.AddNewRollback(new string[] { "edit", "2", "peace" });
            tasks[1].Text = "peace";
            
            rollback.AddNewRollback(new string[] {"add", "world"});
            tasks.Add(new Task{Text = "world"});

            rollback.AddNewRollback(new string[] { "toggle", "1", "1" });
            tasks[0].Status = StatusTask.IsProgress;

            rollback.AddNewRollback(new string[] { "delete", "2" });
            tasks.RemoveAt(1);

            rollback.AddNewRollback(new string[] { "delete", "1" });
            tasks.RemoveAt(1);
            
            rollback.AddNewRollback(new string[] { "edit", "1", "peace" });
            tasks[0].Text = "peace";
            
            rollback.AddNewRollback(new string[] { "toggle", "1", "2" });
            tasks[0].Status = StatusTask.Done;

            rollback.PerformRollbacks(8);

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("war", tasks[0].ToString());
        }

        [TestMethod]
        public void CorrectPerformDifferentCommands_SeveralRollback()
        {
            var tasks = new List<Task> { new Task { Text = "war" } };
            var rollback = new Rollback(tasks);

            rollback.AddNewRollback(new string[] { "toggle", "1","1" });
            tasks[0].Status = StatusTask.IsProgress;

            rollback.AddNewRollback(new string[] { "add", "world" });
            tasks.Add(new Task{Text = "world"});

            rollback.AddNewRollback(new string[] { "edit","2","peace" });
            tasks[1].Text = "peace";

            rollback.AddNewRollback(new string[] { "toggle", "2", "1" });
            tasks[1].Status = StatusTask.IsProgress;

            rollback.PerformRollbacks(2);

            rollback.AddNewRollback(new string[] { "delete", "1" });
            tasks.RemoveAt(0);

            rollback.AddNewRollback(new string[] { "delete", "1" });
            tasks.RemoveAt(0);

            rollback.PerformRollbacks(1);

            rollback.AddNewRollback(new string[] { "edit", "1", "peace" });
            tasks[0].Text = "peace";

            rollback.AddNewRollback(new string[] { "toggle", "1", "1" });
            tasks[0].Status = StatusTask.IsProgress;

            rollback.PerformRollbacks(5);

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("war", tasks[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredCountIsNegative()
        {
            var tasks = new List<Task> { };
            var rollback = new Rollback(tasks);

            rollback.PerformRollbacks(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredCountIsGreaterThanCountStack()
        {
            var tasks = new List<Task> {  };
            var rollback = new Rollback(tasks);
            rollback.PerformRollbacks(1);
        }
    }
}