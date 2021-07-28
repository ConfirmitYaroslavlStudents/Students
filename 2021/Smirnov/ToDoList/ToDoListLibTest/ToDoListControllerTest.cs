using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ToDoListLib.SaveAndLoad;
using ToDoListLib.Controllers;
using ToDoListLib.Models;

namespace ToDoListLibTests
{
    public class SaveAndLoadForTestClass : ISaveAndLoad
    {
        public IEnumerable<Task> Load()
        {
            return new List<Task>();
        }

        public void Save(IEnumerable<Task> toDoList)
        {
        }
    }

    public class ToDoListControllerTest
    {
        private const int N = 10000;

        [Fact]
        public void AddTask_CountN()
        {
            var toDoListController = new ToDoListController(new SaveAndLoadForTestClass());

            for (var i = 0; i < N; i++)
                toDoListController.AddTask(new Task { Description = "" });

            Assert.Equal(N, toDoListController.GetToDoList().Count());
        }

        [Fact]
        public void AddTask_CountN_IdFrom0toN()
        {
            var toDoListController = new ToDoListController(new SaveAndLoadForTestClass());
            var random = new Random();

            for (var i = 0; i < N; i++)
            {
                toDoListController.AddTask(new Task { Description = "" });
            }

            for (var i = 0; i < N; i++)
            {
                Assert.Equal(i, toDoListController.GetTask(i).Id);
            }
        }

        [Fact]

        public void AddTask_CheckDescription()
        {
            var toDoListController = new ToDoListController(new SaveAndLoadForTestClass());
            var random = new Random();
            var randomToDoList = new List<string>();

            for (var i = 0; i < N; i++)
            {
                var description = random.Next().ToString();

                randomToDoList.Add(description);
                toDoListController.AddTask(new Task { Description = description });
            }

            for (var i = 0; i < N; i++)
            {
                Assert.Equal(randomToDoList[i], (toDoListController.GetTask(i)).Description);
            }
        }

        [Fact]
        public void Delete_to1000From3000_Count8000()
        {
            var toDoListController = new ToDoListController(new SaveAndLoadForTestClass());
            var toDoListWithDeletedTask = new ToDoListController(new SaveAndLoadForTestClass());
            for (var i = 0; i < N; i++)
            {
                toDoListController.AddTask(new Task { Description = i.ToString() });
                toDoListWithDeletedTask.AddTask(new Task { Description = i.ToString() });
            }

            for (var i = 1000; i < 3000; i++)
            {
                toDoListController.DeleteTask(i);
            }

            Assert.Equal(N - 2000, toDoListController.GetToDoList().Count());
        }

        [Fact]
        public void ChangeDescription_to1000From3000_OldDescription0_NewDescription1()
        {
            var toDoListController = new ToDoListController(new SaveAndLoadForTestClass());

            for (var i = 0; i < N; i++)
            {
                toDoListController.AddTask(new Task { Description = "0" });
            }

            for (var i = 1000; i < 3000; i++)
            {
                toDoListController.ChangeDescription(i, "1");
            }

            for (var i = 0; i < 1000; i++)
            {
                Assert.Equal("0", toDoListController.GetTask(i).Description);
            }

            for (var i = 1000; i < 3000; i++)
            {
                Assert.Equal("1", toDoListController.GetTask(i).Description);
            }

            for (var i = 3001; i < N; i++)
            {
                Assert.Equal("0", toDoListController.GetTask(i).Description);
            }
        }
        [Fact]
        public void ChangeStatus_to1000From3000_OldStatusNotDone_NewStatusDone()
        {
            var toDoListController = new ToDoListController(new SaveAndLoadForTestClass());

            for (var i = 0; i < N; i++)
            {
                toDoListController.AddTask(new Task { Description = "" });
            }

            for (var i = 1000; i < 3000; i++)
            {
                toDoListController.ChangeTaskStatus(i, TaskStatus.Done);
            }

            for (var i = 0; i < 1000; i++)
            {
                Assert.Equal(TaskStatus.NotDone, toDoListController.GetTask(i).Status);
            }

            for (var i = 1000; i < 3000; i++)
            {
                Assert.Equal(TaskStatus.Done, toDoListController.GetTask(i).Status);
            }

            for (var i = 3001; i < N; i++)
            {
                Assert.Equal(TaskStatus.NotDone, toDoListController.GetTask(i).Status);
            }
        }
    }
}