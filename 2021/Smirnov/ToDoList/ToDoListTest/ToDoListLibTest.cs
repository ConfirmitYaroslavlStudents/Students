
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListLib;
using System;

namespace ToDoListLibTest
{
    [TestClass]
    public class ToDoListLibTest
    {
        private Task task1 = new Task("first", "example", TaskStatus.NotDone);
        private Task task2 = new Task("task2", "example2", TaskStatus.NotDone);

        [TestMethod]
        public void AddTask_Count1()
        {
            var toDoList = new ToDoList();

            toDoList.AddTask(task1.Name, task1.Description, task1.Status);

            Assert.AreEqual(1, toDoList.Count);
        }
        [TestMethod]
        public void AddTask_InvalidOperationException_TaskHasBeenAdded()
        {
            var toDoList = new ToDoList();

            toDoList.AddTask(task1.Name, task1.Description, task1.Status);          

            Assert.ThrowsException<InvalidOperationException>(() => toDoList.AddTask(task1.Name, task1.Description, task1.Status));
        }
        [TestMethod]
        public void GetTask_task2()
        {
            var toDoList = new ToDoList();
            toDoList.AddTask(task1.Name, task1.Description, task1.Status);
            toDoList.AddTask(task2.Name, task2.Description, task2.Status);

            var actual = toDoList.GetTask(task2.Name);

            Assert.IsTrue(task2.Equals(actual));
        }
        [TestMethod]
        public void GetTask_InvalidOperationException_TaskNotFound()
        {
            var toDoList = new ToDoList();
            toDoList.AddTask(task1.Name, task1.Description, task1.Status);

            Assert.ThrowsException<InvalidOperationException>(() => toDoList.GetTask(task2.Name));
        }
        [TestMethod]
        public void DeleteTask_Count0()
        {
            var toDoList = new ToDoList();
            toDoList.AddTask(task1.Name, task1.Description, task1.Status);

            toDoList.DeleteTask(task1.Name);
            
            Assert.AreEqual(0, toDoList.Count);
        }
    }
}
