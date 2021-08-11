using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoClient;
using Microsoft.Extensions.Configuration;

namespace ClientTests
{
    [TestClass]
    public class IntegrationTests
    {
        private static readonly IConfiguration Config = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
        private static readonly string Url = Config.GetSection("Url").Value;
        private string taskId;

        [TestMethod]
        public async Task ToDoTaskExistsAfterAdd()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "test task", "false", "list", "q" });
            var client = new WrappedHttpClient(Url);
            var menuInputHandler = new MenuInputHandler(logger, reader, client);
            
            await menuInputHandler.HandleUsersInput();
            taskId = logger.Messages[3].Split(' ').Last();
            var actualTask = GetTaskFromToDoList(logger.Messages[5], taskId);

            Assert.AreEqual($"{taskId}. test task  [ ]", actualTask);
        }

        [TestMethod]
        public async Task ToDoTaskDoesNotExistAfterRemove()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "test task", "false", "q" });
            var client = new WrappedHttpClient(Url);
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();
            taskId = logger.Messages[3].Split(' ').Last();
            var reader2 = new FakeToDoReader(new List<string> { "remove", taskId, "list", "q" });
            menuInputHandler = new MenuInputHandler(logger, reader2, client);
            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual(-1, logger.Messages[7].IndexOf(taskId));
        }

        [TestMethod]
        public async Task ToDoTaskTextChangesAfterUpdate()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "test task", "false", "q" });
            var client = new WrappedHttpClient(Url);
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();
            taskId = logger.Messages[3].Split(' ').Last();
            var reader2 = new FakeToDoReader(new List<string> { "text", taskId, "new test task", "list", "q" });
            menuInputHandler = new MenuInputHandler(logger, reader2, client);
            await menuInputHandler.HandleUsersInput();
            var actualTask = GetTaskFromToDoList(logger.Messages[10], taskId);

            Assert.AreEqual($"{taskId}. new test task  [ ]", actualTask);
        }

        [TestMethod]
        public async Task ToDoTaskStatusChangesAfterUpdate()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "test task", "false", "q" });
            var client = new WrappedHttpClient(Url);
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();
            taskId = logger.Messages[3].Split(' ').Last();
            var reader2 = new FakeToDoReader(new List<string> { "status", taskId, "true", "list", "q" });
            menuInputHandler = new MenuInputHandler(logger, reader2, client);
            await menuInputHandler.HandleUsersInput();
            var actualTask = GetTaskFromToDoList(logger.Messages[10], taskId);

            Assert.AreEqual($"{taskId}. test task  [v]", actualTask);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "remove", taskId, "q" });
            var client = new WrappedHttpClient(Url);
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();
        }

        private string GetTaskFromToDoList(string toDoList, string taskId)
        {
            var startIndex = toDoList.IndexOf(taskId);
            var length = toDoList.Substring(startIndex).IndexOf("\r\n");
            return toDoList.Substring(startIndex, length);
        }
    }
}