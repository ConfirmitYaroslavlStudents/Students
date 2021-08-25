using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;
using ToDoClient.Controllers;

namespace TodoClientTest.ConsoleTest
{
    [TestClass]
    public class ToDoMenuTest
    {
        class FakeConnector : IToDoConnector
        {
            private ToDoList _todo;

            public FakeConnector(ToDoList todo)
            {
                _todo = todo;
            }

            public void Add(string name)
            {
                _todo.Add(name);
            }

            public void ChangeName(int id, string name)
            {
                _todo[id].ChangeName(name);
            }

            public void Complete(int id)
            {
                _todo[id].SetCompleted();
            }

            public void Delete(int id)
            {
                _todo[id].SetDeleted();
            }

            public ToDoList FindAll(bool completed, bool deleted)
            {
                return _todo.FindAll(completed, deleted);
            }

            public ToDoItem GetItem(int id)
            {
                return _todo[id];
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            Console.In.Close();
            Console.Out.Close();
            if (File.Exists("test.txt"))
                File.Delete("test.txt");
        }

        [TestInitialize]
        public void Initialize()
        {
            File.Create("test.txt").Close();
            File.Create("outtest.txt").Close();
        }

        [TestMethod]
        public void AddTest()
        {
            var writer = new StreamWriter("test.txt");
            writer.WriteLine(ConsoleKey.Enter);
            writer.WriteLine("test");
            writer.Close();
            Console.SetIn(new StreamReader("test.txt"));
            Console.SetOut(new StreamWriter("outtest.txt"));
            var todolist = new ToDoList();
            var menu = new ToDoConsoleMenu(new FakeConnector(todolist), true);

            menu.WorkWithMenu();

            Assert.AreEqual("test", todolist[0].Name);
        }

        [TestMethod]
        public void DeleteItemTest()
        {
            var todolist = new ToDoList()
            {
                new ToDoItem() { Id = 0, Name = "test1" },
                new ToDoItem() { Id = 1, Name = "test2", Completed = true },
                new ToDoItem() { Id = 2, Name = "test3", Deleted = true }
            };
            var writer = new StreamWriter("test.txt");
            writer.WriteLine(ConsoleKey.DownArrow);
            writer.WriteLine(ConsoleKey.Delete);
            writer.Close();
            Console.SetIn(new StreamReader("test.txt"));
            Console.SetOut(new StreamWriter("outtest.txt"));
            var menu = new ToDoConsoleMenu(new FakeConnector(todolist), true);

            menu.WorkWithMenu();
            menu.WorkWithMenu();
            menu.PrintMenu();
            Console.Out.Close();
            var reader = new StreamReader("outtest.txt");
            var input = reader.ReadToEnd().Split('\n');
            reader.Close();

            Assert.AreEqual(7, input.Length);
            Assert.IsTrue(todolist[0].Deleted);
        }

        [TestMethod]
        public void CompleteItemTest()
        {
            var todolist = new ToDoList()
            {
                new ToDoItem() { Id = 0, Name = "test1" },
                new ToDoItem() { Id = 1, Name = "test2", Completed = true },
                new ToDoItem() { Id = 2, Name = "test3", Deleted = true }
            };
            var writer = new StreamWriter("test.txt");
            writer.WriteLine(ConsoleKey.DownArrow);
            writer.WriteLine(ConsoleKey.RightArrow);
            writer.WriteLine(ConsoleKey.Enter);
            writer.Close();
            Console.SetIn(new StreamReader("test.txt"));
            Console.SetOut(new StreamWriter("outtest.txt"));
            var menu = new ToDoConsoleMenu(new FakeConnector(todolist), true);

            menu.WorkWithMenu();
            menu.WorkWithMenu();
            menu.WorkWithMenu();
            menu.PrintMenu();
            Console.Out.Close();
            var reader = new StreamReader("outtest.txt");
            var input = reader.ReadToEnd().Split('\n');
            reader.Close();

            Assert.AreEqual(9, input.Length);
            Assert.IsTrue(todolist[0].Completed);
        }

        [TestMethod]
        public void ChangeNameItemTest()
        {
            var todolist = new ToDoList()
            {
                new ToDoItem() { Id = 0, Name = "test1" },
                new ToDoItem() { Id = 1, Name = "test2", Completed = true },
                new ToDoItem() { Id = 2, Name = "test3", Deleted = true }
            };
            var writer = new StreamWriter("test.txt");
            writer.WriteLine(ConsoleKey.DownArrow);
            writer.WriteLine(ConsoleKey.Enter);
            writer.WriteLine("test4");
            writer.WriteLine(ConsoleKey.DownArrow);
            writer.WriteLine(ConsoleKey.Enter);
            writer.WriteLine("test5");
            writer.Close();
            Console.SetIn(new StreamReader("test.txt"));
            Console.SetOut(new StreamWriter("outtest.txt"));
            var menu = new ToDoConsoleMenu(new FakeConnector(todolist), true);

            menu.WorkWithMenu();
            menu.PrintMenu();
            menu.WorkWithMenu();
            menu.WorkWithMenu();
            menu.PrintMenu();
            menu.WorkWithMenu();
            menu.PrintMenu();
            Console.Out.Close();
            var reader = new StreamReader("outtest.txt");
            var input = reader.ReadToEnd().Split('\n');
            reader.Close();

            Assert.AreEqual("test4", todolist[0].Name);
            Assert.AreEqual("test5", todolist[1].Name);
        }

        [TestMethod]
        public void ShowOnlyUncompletedNotDeleted()
        {
            var todolist = new ToDoList()
            {
                new ToDoItem() { Id = 0, Name = "test1" },
                new ToDoItem() { Id = 1, Name = "test2", Completed = true },
                new ToDoItem() { Id = 2, Name = "test3", Deleted = true }
            };
            var writer = new StreamWriter("test.txt");
            writer.WriteLine(ConsoleKey.RightArrow);
            writer.WriteLine(ConsoleKey.RightArrow);
            writer.WriteLine(ConsoleKey.Enter);
            writer.Close();
            Console.SetIn(new StreamReader("test.txt"));
            Console.SetOut(new StreamWriter("outtest.txt"));
            var menu = new ToDoConsoleMenu(new FakeConnector(todolist), true);

            menu.WorkWithMenu();
            menu.WorkWithMenu();
            menu.WorkWithMenu();
            menu.PrintMenu();
            Console.Out.Close();
            var reader = new StreamReader("outtest.txt");
            var input = reader.ReadToEnd().Split('\n');
            reader.Close();

            Assert.AreEqual(7, input.Length);
        }

        [TestMethod]
        public void ShowOnlyCompletedNotDeleted()
        {
            var todolist = new ToDoList()
            {
                new ToDoItem() { Id = 0, Name = "test1" },
                new ToDoItem() { Id = 1, Name = "test2", Completed = true },
                new ToDoItem() { Id = 2, Name = "test3", Deleted = true }
            };
            Console.SetIn(new StreamReader("test.txt"));
            Console.SetOut(new StreamWriter("outtest.txt"));
            var menu = new ToDoConsoleMenu(new FakeConnector(todolist), true);
            
            menu.PrintMenu();
            Console.Out.Close();
            var reader = new StreamReader("outtest.txt");
            var input = reader.ReadToEnd().Split('\n');
            reader.Close();

            Assert.AreEqual(9, input.Length);
        }

        [TestMethod]
        public void ShowAll()
        {
            var todolist = new ToDoList()
            {
                new ToDoItem() { Id = 0, Name = "test1" },
                new ToDoItem() { Id = 1, Name = "test2", Completed = true },
                new ToDoItem() { Id = 2, Name = "test3", Deleted = true }
            };
            var writer = new StreamWriter("test.txt");
            writer.WriteLine(ConsoleKey.RightArrow);
            writer.WriteLine(ConsoleKey.Enter);
            writer.Close();
            Console.SetIn(new StreamReader("test.txt"));
            Console.SetOut(new StreamWriter("outtest.txt"));
            var menu = new ToDoConsoleMenu(new FakeConnector(todolist), true);

            menu.WorkWithMenu();
            menu.WorkWithMenu();
            menu.PrintMenu();
            Console.Out.Close();
            var reader = new StreamReader("outtest.txt");
            var input = reader.ReadToEnd().Split('\n');
            reader.Close();

            Assert.AreEqual(11, input.Length);
        }
    }
}
